using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Diagnostics;

namespace model_auth
{
    class Tag 
    {
        private int id_tag;
        private string symmetric_key;
        private string aid;
        private int nt;
        private string nx;
        //свойства для id_tag
        public int Id_tag
        {
            get => id_tag;
            set => id_tag = value;
        }
        //свойства для symmetric_key
        public string Symmetric_key
        {
            get => symmetric_key;
            set => symmetric_key = value;
        }
        //свойства для AID
        public string AID
        {
            get => aid;
            set => aid = value;
        }
        //свойства для nt
        private protected int Nt
        {
            get => nt;
            set
            {
               nt = value;
            }
        }
        //свойства для nx
        public string Nx
        {
            get => nx;
            set => nx = value;
        }
        //конструктор
        public Tag()    
        {
            var rand = new Random();
            this.Id_tag = rand.Next(10000);
        }
        public Tag(int id,string Kts,string aid)
        {
            this.Id_tag = id;
            this.Symmetric_key = Kts ;
            this.AID = aid;
            Random rnd = new Random();
            Nt = rnd.Next();
        }
        public string Set_hash_V1(Tag t, int id_reader) // вычисление Nx и формирование V1
        {
            var sreader = Convert.ToSByte(id_reader); // конвертация идентификатора в байт
            string v1 = "";
            var byte_key = Encoding.UTF8.GetBytes(t.Symmetric_key); // создание массива байтов из симметричного ключа
            var byte_num = BitConverter.GetBytes(t.Nt); // создание массива байтов из сгенерированного числа
            t.Nx = Convert.ToBase64String(byte_key.Select((b, i) => (byte)(b ^ byte_num[i % byte_num.Length])).ToArray()); // XOR симметричного ключа и случ.числа
            string tohash = String.Concat(t.AID, t.Symmetric_key, Nx, sreader); // конкатенация строк однораз. id, симметрич.ключа, nx, id считывателя
            byte[] tohash_byte = Encoding.UTF8.GetBytes(tohash); // перевод результата конкатенации в массив байтов
            var sha = SHA256.Create(); // cоздание объекта для использования хэш-функции
            var hash = sha.ComputeHash(tohash_byte); // вычисление хэша для массива байтов
            v1 = Convert.ToBase64String(hash); // перевод в строку Base64 для удобного хранения
            return v1;
        }
        public bool Check_hash_V4(string V4, Tag t) // проверка полученного v3
        {
            string tohash = String.Concat(t.Id_tag, Nt, t.Symmetric_key);// конкатенация id считывателя, симметрич.ключа, nr
            byte[] tohash_byte = Encoding.UTF8.GetBytes(tohash); // перевод результата конкатенации в массив байтов
            var sha = SHA256.Create(); // cоздание объекта для использования хэш-функции
            var hash = sha.ComputeHash(tohash_byte); // вычисление хэша для массива байтов
            string comp_v4 = Convert.ToBase64String(hash); // перевод в строку Base64 для удобного хранения
            return (comp_v4.Equals(V4));
        }
        public string Set_new_AIDfromS(string Zt, string Kts) // расшифровка нового AID 
        {
            var byte_Kts = Encoding.UTF8.GetBytes(Kts);
            var byte_Zt = Encoding.UTF8.GetBytes(Zt);
            string AID = Convert.ToBase64String(byte_Kts.Select((b, i) => (byte)(b ^ byte_Zt[i % byte_Zt.Length])).ToArray());
            return AID;
        }
        public List<string> Package_Ma1 (Tag t,string V1, Stopwatch tmr) // формирование пакета Ma1
        {
            List<string> package = new List<string> { t.AID, Convert.ToString(t.Nx), V1, (tmr.ElapsedMilliseconds).ToString() }; // формирование списка строк 
            return package;
        }
    }
}
