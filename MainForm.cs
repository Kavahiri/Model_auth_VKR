using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace model_auth
{
    public partial class MainForm : Form
    {
        private SqlDataAdapter sqlDataAdapter = null;
        private DataSet dataset = null;
        private static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=server;Integrated Security=True";
        private SqlConnection connection = new SqlConnection(connectionString);
        private Random rand = new Random();

        public MainForm()
        {
            InitializeComponent();
            timeWD.Checked = true;
        }
        private void AddTag_Click(object sender, EventArgs e)
        {
            
            Server server = new Server();
            server.Create_Key_Salt();
            Tag tag = new Tag();
            string sqlExpression = "AddNewTag";
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            // указываем, что команда представляет хранимую процедуру
            command.CommandType = CommandType.StoredProcedure;
            // параметр для соли
            SqlParameter id_tag = new SqlParameter
            {
                ParameterName = "@id_tag",
                Value = tag.Id_tag

            };
            command.Parameters.Add(id_tag);
            // параметр для симметричного ключа
            SqlParameter Kts= new SqlParameter
            {
                ParameterName = "@Kts", Value = Convert.ToBase64String(server.Key)

            };
            command.Parameters.Add(Kts);
            // параметр для aid
            SqlParameter aid = new SqlParameter
            {
                ParameterName = "@aid",
                Value = server.Encrypt_AES(tag.Id_tag,server.Salt)

            };
            command.Parameters.Add(aid);
            // параметр для соли
            SqlParameter salt = new SqlParameter
            {
                ParameterName = "@salt",
                Value = BitConverter.ToInt32(server.Salt,0)

            };
            command.Parameters.Add(salt);
            command.ExecuteNonQuery();
            MessageBox.Show("Метка успешно добавлена");
        }
        private void AddReader_Click(object sender, EventArgs e)
        {
            Server server = new Server();
            server.Create_Key_Salt();
            Reader reader = new Reader();
            string sqlExpression = "AddNewReader";
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            // указываем, что команда представляет хранимую процедуру
            command.CommandType = CommandType.StoredProcedure;
            // параметр для симметричного ключа
            SqlParameter Krs = new SqlParameter
            {
                ParameterName = "@Krs",
                Value = Convert.ToBase64String(server.Key)

            };
            command.Parameters.Add(Krs);
            // параметр для соли
            SqlParameter status = new SqlParameter
            {
                ParameterName = "@status",
                Value = "YES"

            };
            command.Parameters.Add(status);
            command.ExecuteNonQuery();
            MessageBox.Show("Считыватель успешно добавлен");
        }

        private void Model_Start_Click(object sender, EventArgs e)
        {
            if (TagIsNull())
            {
                MessageBox.Show("В системе нет зарегестрированных меток. Зарегистрируйте хотя бы одну метку и попробуйте еще раз!");
                return;
            }
            if (ReaderIsNull())
            {
                MessageBox.Show("В системе нет зарегестрированных считывателей. Зарегистрируйте хотя бы один считыватель и попробуйте еще раз!");
                return;
            }
            int delta = 40;
            bool status = false;
            if (!int.TryParse(textBoxTag.Text, out int id_t))
            {
                MessageBox.Show("Не введен id метки. Попробуйте еще раз!");
                return;
            }
            else
            {
                if (!FindTagInSys(id_t))
                {
                    MessageBox.Show("Данная метка не зарегистрирована в системе. Попробуйте еще раз!");
                    return;
                }
            }
            if (!int.TryParse(textBoxReader.Text, out int id_reader))
            {
                MessageBox.Show("Не введен id считывателя. Попробуйте еще раз!");
                return;
            }
            else
            {
                if (!FindReaderInSys(id_reader))
                {
                    MessageBox.Show("Данный считыватель не зарегистрирован в системе. Попробуйте еще раз!");
                    return;
                }
            }
            
            ProcessForm processForm = new ProcessForm();
      
            Stopwatch sw = Stopwatch.StartNew(); // включение таймера             
            
            // сборка объекта метки
            Tag tag = new Tag();
            tag = CreateTag(id_t,tag);
            // создание объекта считывателя
            Reader read = new Reader(id_reader);
            read = CreateReader(id_reader, read);
            processForm.Show();
            processForm.ProcessText = DateTime.Now.ToString() + " Модель начала работу.";
            List<string> packageMa1 = InitAuthRequest(tag,read,sw);// запрос метки на аутентификацию считывателю       
            processForm.ProcessText = DateTime.Now.ToString() + " Пакет Ma1 cобран меткой (сформированы V1 и Nx)";
            int i = 0;
            if (time21.Checked == true)
            {
                i = 100;
            }
            Thread.Sleep(5+i); // задержка на 5 мсек 
            processForm.ProcessText = DateTime.Now.ToString() + " Пакет Ма1 получен считывателем ";
            if (Check_Time(sw,Convert.ToInt32(packageMa1.ElementAt(3)), delta))
            {
                processForm.ProcessText = DateTime.Now.ToString() + " Проверка на задержку времени между меткой и считывателем пройдена (T2-T1)";
                List<string> packageMa2 = AuthRequestToServer(read, packageMa1,sw);
                processForm.ProcessText = DateTime.Now.ToString() + " Пакет Ма2 собран считывателем (сформированы V2 и Ny)";
                if (time32.Checked == true)
                {
                    i = 100;
                }
                Thread.Sleep(5+i); // задержка на 5 мсек 
                processForm.ProcessText = DateTime.Now.ToString() + " Пакет Ма2 получен сервером.";
                if (Check_Time(sw,Convert.ToInt32(packageMa2.ElementAt(7)), delta))
                {
                    processForm.ProcessText = DateTime.Now.ToString() + " Проверка на задержку времени между считывателем и сервером пройдена (T3-T2)";
                    Server server = new Server();
                    getActualData(out string Kts, out string Krs, out string AID,tag,read);
                    
                    server.Derive(Krs, Kts, packageMa1.ElementAt(1), packageMa2.ElementAt(0)); //вычисление Nt и Nr сгенерир. меткой и считывателем
                    processForm.ProcessText = DateTime.Now.ToString() + " Вычислены Nt и Nr, сгенерированные меткой и считывателем";
                    server.Key = Convert.FromBase64String(Kts);
                    getSalt(server, tag);
                    
                    // проверка хэшированных V1 и V2
                    bool flag = server.Check_hash_V1andV2(Kts, Krs, AID, read.Id_reader, packageMa1.ElementAt(2), packageMa2.ElementAt(2), packageMa1.ElementAt(0));
                    if (flag)
                    {
                        processForm.ProcessText = DateTime.Now.ToString() + " Полученные значения V1, V2 и AID являются подлинными";
                        
                        DeriveV3_V4_newAID(out string newaid,out string v3,out string v4, out int  id,server, Krs,Kts, packageMa1, packageMa2);
                        processForm.ProcessText = DateTime.Now.ToString() + " Сформированы значения V3, V4 и новый AID";
                        List<string> packageMa3 = ResponseServer(server,newaid,v3,v4,Kts,sw);
                        processForm.ProcessText = DateTime.Now.ToString() + " Пакет Ma3 сформирован сервером";
                        if (time43.Checked == true)
                        {
                            i = 100;
                        }
                        Thread.Sleep(5+i); // задержка на 5 мсек
                        processForm.ProcessText = DateTime.Now.ToString() + " Пакет Ma3 получен считывателем";
                        if (Check_Time(sw, Convert.ToInt32(packageMa3.ElementAt(3)), delta))
                        {
                            processForm.ProcessText = DateTime.Now.ToString() + " Проверка на задержку времени между сервером и считывателем пройдена (T4-T3)";
                            bool flagv3 = read.Check_hash_V3(packageMa3.ElementAt(0), read);
                            if (flagv3) 
                            {
                                processForm.ProcessText = DateTime.Now.ToString() + " Полученное значение V3 является подлинным";
                                List<string> packageMa4 =ResponceReader(read,packageMa3,sw);
                                processForm.ProcessText = DateTime.Now.ToString() + " Пакет Ma4 сформирован считывателем";
                                if (time54.Checked == true)
                                {
                                    i = 100;
                                }
                                Thread.Sleep(5+i); // задержка на 5 мсек
                                processForm.ProcessText = DateTime.Now.ToString() + " Пакет Ma4 получен меткой";
                                if (Check_Time(sw, Convert.ToInt32(packageMa4.ElementAt(2)), delta))
                                {
                                    processForm.ProcessText = DateTime.Now.ToString() + " Проверка на задержку времени между считывателем и меткой пройдена (T5-T4)";
                                    bool flagv4 = tag.Check_hash_V4(packageMa4.ElementAt(0), tag);

                                    if (flagv4)
                                    {
                                        processForm.ProcessText = DateTime.Now.ToString() + " Полученное значение V4 является подлинным";
                                        tag.AID = tag.Set_new_AIDfromS(packageMa4.ElementAt(0), tag.Symmetric_key);
                                        processForm.ProcessText = DateTime.Now.ToString() + " Процесс аутентификации успешно пройден. Метка получила новый AID.";
                                        UpdateAIDTag(tag);
                                        status = true;
                                    }
                                    else 
                                    {
                                        processForm.ProcessText = DateTime.Now.ToString() + " Полученное значение V4 не является подлинным";
                                    }                                   
                                }
                                else
                                {
                                    processForm.ProcessText = DateTime.Now.ToString() + " Проверка на задержку времени между считывателем и меткой не пройдена (T5-T4)";
                                }                             
                            }
                            else 
                            {
                                processForm.ProcessText = DateTime.Now.ToString() + " Полученное значение V3 не является подлинным";
                            }                         
                        }
                        else
                        {
                            processForm.ProcessText = DateTime.Now.ToString() + " Проверка на задержку времени между сервером и считывателем не пройдена (T4-T3)";
                        }
                    }
                    else 
                    {
                        processForm.ProcessText = DateTime.Now.ToString() + " Полученные значения V1, V2 и AID не являются подлинными";
                    }
                }
                else 
                {
                    processForm.ProcessText = DateTime.Now.ToString() + " Проверка на задержку времени между считывателем и сервером не пройдена (T3-T2)";
                }
            }
            else
            {
                processForm.ProcessText = DateTime.Now.ToString() + " Проверка на задержку времени между меткой и считывателем не пройдена (T2-T1)";    
            }
            sw.Stop();
            if (status) 
            { 
                AddHistoryConnect(tag, read, "YES");
                processForm.ProcessText = DateTime.Now.ToString() + " Таблица подключений к серверу обновлена. Статус попытки - успешно.";
            }
            else 
            { 
                AddHistoryConnect(tag, read, "NO");
                processForm.ProcessText = DateTime.Now.ToString() + " Таблица подключений к серверу обновлена. Статус попытки - неудача.";
            }
            processForm.ProcessText = DateTime.Now.ToString() + " Завершение работы модели";
        }

        private bool ReaderIsNull()
        {
            bool flag = false;
            string sqlExpression = "SELECT COUNT(*) FROM readertable";
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.ExecuteNonQuery();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        int num = Convert.ToInt32(reader.GetValue(0));
                        if (num == 0) { flag = true; }
                    }
                }
            }
            return flag;
        }

        private bool TagIsNull()
        {
            bool flag = false;
            string sqlExpression = "SELECT COUNT(*) FROM tagtable";
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.ExecuteNonQuery();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        int num = Convert.ToInt32(reader.GetValue(0));
                        if (num == 0) { flag = true; }
                    }
                }
            }
            return flag;
        }

        private void UpdateAIDTag(Tag tag)
        {
            string sqlExpression = "UpdateAIDinTag";
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            // указываем, что команда представляет хранимую процедуру
            command.CommandType = CommandType.StoredProcedure;
            // параметр для имени
            SqlParameter id_tag = new SqlParameter
            {
                ParameterName = "@id",
                Value = tag.Id_tag
            };
            command.Parameters.Add(id_tag);
            SqlParameter new_aid = new SqlParameter
            {
                ParameterName = "@newaid",
                Value = tag.AID
            };
            command.Parameters.Add(new_aid);
            command.ExecuteNonQuery();
        }

        private List<string> ResponceReader(Reader read, List<string> packageMa3, Stopwatch sw)
        {
            List<string> packageMa4 = read.Package_Ma4(packageMa3.ElementAt(1),packageMa3.ElementAt(2), sw);
            byte[] zt_byte = Convert.FromBase64String(packageMa3.ElementAt(2));
            var checkedItems = replaceMa4.CheckedIndices;
            if (checkedItems.Contains(0))
            {
                packageMa4[0] = rand.Next(100000).ToString();
            }
            if (checkedItems.Contains(1))
            {
                var random = new RNGCryptoServiceProvider();
                random.GetBytes(zt_byte);
                packageMa3[2] = Convert.ToBase64String(zt_byte);
            }
            return packageMa4;
        }

        private bool FindReaderInSys(int id)
        {
            bool flag = false;
            string sqlExpression = "SELECT COUNT(id_reader) FROM readertable WHERE id_reader=@id_r";
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlParameter idr = new SqlParameter
            {
                ParameterName = "@id_r",
                Value = id
            };
            command.Parameters.Add(idr);
            command.ExecuteNonQuery();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        int num = Convert.ToInt32(reader.GetValue(0));
                        if (num == 1) { flag = true; }
                    }
                }
            }
            return flag;
        }

        private bool FindTagInSys(int id)
        {
            bool flag =false;
            string sqlExpression = "SELECT COUNT(id_tag) FROM tagtable WHERE id_tag=@id_t";
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlParameter idtag = new SqlParameter
            {
                ParameterName = "@id_t",
                Value = id
            };
            command.Parameters.Add(idtag);
            command.ExecuteNonQuery();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        int num = Convert.ToInt32(reader.GetValue(0));
                        if (num == 1) { flag = true; }
                    }
                }
            }
            return flag;

        }

        private void AddHistoryConnect(Tag tag, Reader read, string v)
        {
            string sqlExpression = "AddHistory";
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            // указываем, что команда представляет хранимую процедуру
            command.CommandType = CommandType.StoredProcedure;
            // параметр для имени
            SqlParameter idtag = new SqlParameter
            {
                ParameterName = "@id_tag",
                Value = tag.Id_tag
            };
            command.Parameters.Add(idtag);
            SqlParameter idreader = new SqlParameter
            {
                ParameterName = "@id_reader",
                Value = read.Id_reader
            };
            command.Parameters.Add(idreader);
            SqlParameter status = new SqlParameter
            {
                ParameterName = "@status_tag",
                Value = v
            };
            command.Parameters.Add(status);
            command.ExecuteNonQuery();
        }

        private List<string> ResponseServer(Server server, string newaid, string v3, string v4, string Kts,Stopwatch sw)
        {
            string zt = server.Set_Zt(newaid, Kts);
            byte[] zt_byte = Convert.FromBase64String(zt);
            List<string> packageMa3 = server.Package_Ma3(v3, v4, zt,sw);
            var checkedItems = replaceMa3.CheckedIndices;
            if (checkedItems.Contains(0))
            {
                packageMa3[0] = rand.Next(100000).ToString();
            }
            if (checkedItems.Contains(1))
            {
                packageMa3[1] = rand.Next(100000).ToString();
            }
            if (checkedItems.Contains(2))
            {
                var random = new RNGCryptoServiceProvider();
                random.GetBytes(zt_byte);
                packageMa3[2] = Convert.ToBase64String(zt_byte);
            }
            return packageMa3;
        }

        private void DeriveV3_V4_newAID(out string newaid, out string v3, out string v4,out int id, Server server, string Krs, string Kts, List<string> packageMa1, List<string> packageMa2)
        {
            v3 = server.Set_hash_V3(Krs, Convert.ToInt32(packageMa2.ElementAt(1)));
            string idandsalt = server.Decrypt_AES(packageMa1.ElementAt(0));
            string[] split = idandsalt.Split(Convert.ToString(server.Salt).ToCharArray());
            id = Convert.ToInt32(split.ElementAt(0));
            v4 = server.Set_hash_V4(Kts, Convert.ToInt32(split.ElementAt(0)));
            var random = new RNGCryptoServiceProvider();
            random.GetBytes(server.Salt);
            newaid = server.Encrypt_AES(id, server.Salt);
            UpdateAID(server,id,newaid);
            
        }

        private void UpdateAID(Server server, int id, string newaid)
        {
            string sqlExpression = "UpdateAIDinServer";
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            // указываем, что команда представляет хранимую процедуру
            command.CommandType = CommandType.StoredProcedure;
            // параметр для имени
            SqlParameter id_tag = new SqlParameter
            {
                ParameterName = "@id",
                Value = id
            };
            command.Parameters.Add(id_tag);
            SqlParameter new_salt = new SqlParameter
            {
                ParameterName = "@salt",
                Value = BitConverter.ToInt32(server.Salt, 0)
            };
            command.Parameters.Add(new_salt);
            SqlParameter new_aid = new SqlParameter
            {
                ParameterName = "@newaid",
                Value = newaid
            };
            command.Parameters.Add(new_aid);
            command.ExecuteNonQuery();
        }

        private void getSalt(Server server, Tag tag)
        {
            string sqlExpression = "SELECT * FROM getSalt(@id)";
            //получение случайного числа из сервера 
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            // параметр для id
            SqlParameter salt = new SqlParameter
            {
                ParameterName = "@id",
                Value = tag.Id_tag

            };
            command.Parameters.Add(salt);
            command.ExecuteNonQuery();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        int s = Convert.ToInt32(reader.GetValue(0));
                        server.Salt = BitConverter.GetBytes(s);
                    }
                }

            }
        }

        private void getActualData(out string Kts, out string Krs, out string AID,Tag tag, Reader read)
        {
            Kts = "";
            Krs = "";
            AID = "";
            // получение ключа и aid для метки с сервера
            string sqlExpression = "SELECT * FROM getKeyAndAIDFromServer(@id)";
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            // параметр для id
            SqlParameter id_tag = new SqlParameter
            {
                ParameterName = "@id",
                Value = tag.Id_tag

            };
            command.Parameters.Add(id_tag);
            command.ExecuteNonQuery();
            using (SqlDataReader reader =  command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {

                    while (reader.Read()) // построчно считываем данные
                    {
                        string key = Convert.ToString(reader.GetValue(0));
                        string aid = Convert.ToString(reader.GetValue(1));
                        Kts = key;
                        AID = aid;

                    }
                }

            }
            // получение ключа для считывателя с сервера
            sqlExpression = "SELECT * FROM getKeyFromReader(@id)";
            command = new SqlCommand(sqlExpression, connection);
            // параметр для id
            SqlParameter id_read = new SqlParameter
            {
                ParameterName = "@id",
                Value = read.Id_reader

            };
            command.Parameters.Add(id_read);
            command.ExecuteNonQuery();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {

                    while (reader.Read()) // построчно считываем данные
                    {
                        string key = Convert.ToString(reader.GetValue(0));
                        Krs = key;
                    }
                }

            }
        }

        private List<string> AuthRequestToServer(Reader read, List<string> packageMa1, Stopwatch sw)
        {
            //формирование хэша V2
            string v2 = read.Set_hash_V2(packageMa1, read);
            List<string> packageMa2 = read.Package_Ma2(read, packageMa1, v2, sw); // формирование пакета Ma2
            var checkedItems = replaceMa2.CheckedIndices;
            if (checkedItems.Contains(0))
            {
                packageMa2[0] = Convert.ToBase64String(BitConverter.GetBytes(rand.Next(100000)));
            }
            if (checkedItems.Contains(1))
            {
                packageMa2[2] = Convert.ToBase64String(BitConverter.GetBytes(rand.Next(100000)));
            }
            if (checkedItems.Contains(2))
            {
                packageMa2[1] = rand.Next(100000).ToString();
            }
            return packageMa2;
        }

        private List<string> InitAuthRequest(Tag tag, Reader read,Stopwatch sw)
        {
            string v1 = tag.Set_hash_V1(tag, read.Id_reader);
            List<string> packageMa1 = tag.Package_Ma1(tag, v1, sw); // формирование пакета Ma1
            var checkedItems = replaceMa1.CheckedIndices;
            if (checkedItems.Contains(0))
            {
                packageMa1[0] = rand.Next(100000).ToString();
            }
            if (checkedItems.Contains(1))
            {
                packageMa1[1] = Convert.ToBase64String(BitConverter.GetBytes(rand.Next(100000)));
            }
            if (checkedItems.Contains(2))
            {
                packageMa1[2] = Convert.ToBase64String(BitConverter.GetBytes(rand.Next(100000)));
            }
            return packageMa1;
        }

        private Reader CreateReader(int id_reader, Reader read)
        {
            string sqlExpression = "SELECT * FROM getKeyFromReader(@id)";
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            // параметр для id
            SqlParameter id_r = new SqlParameter
            {
                ParameterName = "@id",
                Value = id_reader

            };
            command.Parameters.Add(id_r);
            command.ExecuteNonQuery();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {

                    while (reader.Read()) // построчно считываем данные
                    {
                        string key = Convert.ToString(reader.GetValue(0));
                        read.Symmetric_KeyR = key;
                    }
                }
            }
            return read;
        }

        private  Tag CreateTag(int id_t, Tag tag)
        {
            string sqlExpression = "SELECT * FROM getKeyAndAID(@id)";
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            // параметр для id
            SqlParameter id_tag = new SqlParameter
            {
                ParameterName = "@id",
                Value = id_t

            };
            command.Parameters.Add(id_tag);

           command.ExecuteNonQuery();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {

                    while (reader.Read()) // построчно считываем данные
                    {
                        string key = Convert.ToString(reader.GetValue(0));
                        string aid = Convert.ToString(reader.GetValue(1));
                        tag.Id_tag = id_t;
                        tag.Symmetric_key = key;
                        tag.AID = aid;

                    }
                }

            }
            return tag;
        }

        private bool Check_Time(Stopwatch sw,int time, int delta)
        {
            if ((sw.ElapsedMilliseconds - time) < delta) { return true; } // текущее значение таймера - отметка времени из пакета
            else { return false; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            // Создание подключения
            try
            {
                // Открываем подключение
                connection.OpenAsync();
                MessageBox.Show("Соединение прошло успешно");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
        }

        private void getHistory_Click(object sender, EventArgs e)
        {
            string sqlExpression = "SELECT * FROM History";
            // Создание подключения
            sqlDataAdapter = new SqlDataAdapter(sqlExpression, connection);
            TableForm tableForm = new TableForm();
            dataset = new DataSet();
            sqlDataAdapter.Fill(dataset,"History");
            tableForm.DataSetForm = dataset;
            tableForm.ShowDialog();
        }

        private void getTagInSys_Click(object sender, EventArgs e)
        {
            string sqlExpression = "SELECT * FROM TagInSystem";
            // Создание подключения
            sqlDataAdapter = new SqlDataAdapter(sqlExpression, connection);
            TableForm tableForm = new TableForm();
            dataset = new DataSet();
            sqlDataAdapter.Fill(dataset, "TagInSystem");
            tableForm.DataSetForm = dataset;
            tableForm.ShowDialog();
        }

        private void getReaderInSys_Click(object sender, EventArgs e)
        {
            string sqlExpression = "SELECT * FROM ReaderInSystem";
            // Создание подключения
            sqlDataAdapter = new SqlDataAdapter(sqlExpression, connection);
            TableForm tableForm = new TableForm();
            dataset = new DataSet();
            sqlDataAdapter.Fill(dataset, "ReaderInSystem");
            tableForm.DataSetForm = dataset;
            tableForm.ShowDialog();
        }
    }
}