using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace model_auth
{
    class Server
    {
        private int nt;
        private int nr;
        private byte[] key;
        private byte[] salt;
        public int Nt
        {
            get => nt;
            set => nt = value;
        }
        public int Nr
        {
            get => nr;
            set => nr = value;
        }
        public byte[] Key
        {
            get => key;
            set => key = value;
        }
        public byte[] Salt
        {
            get => salt;
            set => salt = value;
        }
        public int Derive_N (string Key,string N)
        {
            var byte_Kts = Encoding.UTF8.GetBytes(Key);
            var byte_Nx = Convert.FromBase64String(N);
            var byte_Nt = byte_Kts.Select((b, i) => (byte)(b ^ byte_Nx[i % byte_Nx.Length])).ToArray();
            return BitConverter.ToInt32(byte_Nt, 0); 
        }
        public void Derive (string Krs, string Kts, string Nx, string Ny)
        {
            Nt = Derive_N(Kts, Nx);
            Nr = Derive_N(Krs, Ny);
        }
        public void Create_Key_Salt()
        {
            using (var random = new RNGCryptoServiceProvider())
            {
                Aes aes = Aes.Create();
                aes.GenerateKey();
                Key = aes.Key;
                Salt = new byte[16];
                random.GetBytes(salt);
                Key = new PasswordDeriveBytes(Key, Salt).GetBytes(16);
            }
        }
        public string Encrypt_AES(int idtag,byte[] salt)
        {
            byte[] encrypted;
            byte[] IV;
            string plainText = String.Concat(idtag, salt);
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;

                aesAlg.GenerateIV();
                IV = aesAlg.IV;

                aesAlg.Mode = CipherMode.CBC;

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Создание потоков для шифрования 
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Запись всех данных в потоке
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            var combinedIvCt = new byte[IV.Length + encrypted.Length];
            Array.Copy(IV, 0, combinedIvCt, 0, IV.Length);
            Array.Copy(encrypted, 0, combinedIvCt, IV.Length, encrypted.Length);

            // Возврат зашифрованных данных
            return Convert.ToBase64String(combinedIvCt);
        }
        public string Decrypt_AES(string AID)
        {
            byte[] cipherTextCombined = Convert.FromBase64String(AID);
            // переменная для расшифрованного текста
            string plaintext = null;

            // Объект AES c ключом и вектором IV 
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;

                byte[] IV = new byte[aesAlg.BlockSize / 8];
                byte[] cipherText = new byte[cipherTextCombined.Length - IV.Length];

                Array.Copy(cipherTextCombined, IV, IV.Length);
                Array.Copy(cipherTextCombined, IV.Length, cipherText, 0, cipherText.Length);

                aesAlg.IV = IV;

                aesAlg.Mode = CipherMode.CBC;

                // Создание дешифратора для выполнения преобразования потока
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Создание потоков, используемые для дешифрования
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Считывание расшифрованных байтов из потока дешифрования и помещение их в строку.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }
            return plaintext;
        }
        public string Compute_Nxy(string Key, int N)
        {
            var byte_key = Encoding.UTF8.GetBytes(Key);
            var byte_num = BitConverter.GetBytes(N);
            string Nx = Convert.ToBase64String(byte_key.Select((b, i) => (byte)(b ^ byte_num[i % byte_num.Length])).ToArray());
            return Nx;
        }
        //public bool Check_hash_V1andV2(string Kts, string Krs, string AID, int id_reader, string t1, string getv1, string getv2, string getaid)
        public bool Check_hash_V1andV2(string Kts, string Krs, string AID, int id_reader, string getv1, string getv2,string getaid)
        {
            var sreader = Convert.ToSByte(id_reader);
            string Nx=Compute_Nxy(Kts,Nt);
            string tohash = String.Concat(getaid, Kts, Nx, sreader);
            byte[] tohash_byte = Encoding.UTF8.GetBytes(tohash);
            var sha = SHA256.Create();
            var hash = sha.ComputeHash(tohash_byte);
            string v1 = Convert.ToBase64String(hash);

            List <string> package_ma1 = new List<string> { getaid, Convert.ToString(Nx), v1 };

            string tohash1 = String.Concat(package_ma1, Krs, Nr);
            byte[] tohash_byte1 = Encoding.UTF8.GetBytes(tohash1);
            var hash1= sha.ComputeHash(tohash_byte1);
            string v2 = Convert.ToBase64String(hash1); 
            return (v1.Equals(getv1) & v2.Equals(getv2) & AID.Equals(getaid));
        }
        public string Set_hash_V3(string Krs, int id_reader)
        {
            string tohash = String.Concat(id_reader, Nr, Krs);
            byte[] tohash_byte = Encoding.UTF8.GetBytes(tohash);
            var sha = SHA256.Create();
            var hash = sha.ComputeHash(tohash_byte);
            string v3 = Convert.ToBase64String(hash);
            return v3;
        }
        public string Set_hash_V4 (string Kts,  int id_tag)
        {
            string tohash = String.Concat(id_tag, Nt, Kts);
            byte[] tohash_byte = Encoding.UTF8.GetBytes(tohash);
            var sha = SHA256.Create();
            var hash = sha.ComputeHash(tohash_byte);
            string v4 = Convert.ToBase64String(hash);
            return v4;
        }
        public string Set_Zt(string AID, string Kts) // формирование Zt
        {
            var byte_Kts = Encoding.UTF8.GetBytes(Kts);
            var byte_AID = Encoding.UTF8.GetBytes(AID);
            string Zt = Convert.ToBase64String(byte_Kts.Select((b, i) => (byte)(b ^ byte_AID[i % byte_AID.Length])).ToArray());
            return Zt;
        }
        public List<string> Package_Ma3(string v3,string v4, string Zt,Stopwatch sw) // формирование пакета Ma3
        {
            List<string> package = new List<string> { v3, v4, Zt, sw.ElapsedMilliseconds.ToString()}; // формирование списка строк 
            return package;
        }
    }
}
