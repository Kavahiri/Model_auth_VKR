using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace model_auth
{
    class Reader
    {
        private int id_reader;
        private string symmetric_KeyR;
        private int nr;
        private string ny;
        public Reader(int id)
        {
            this.Id_reader = id;
            Random rnd=new Random();
            Nr = rnd.Next();
        }
        public Reader() { }
        
        public int Id_reader
        {
            get => id_reader;
            set => id_reader = value;
        }
        public string Symmetric_KeyR
        {
            get => symmetric_KeyR;
            set => symmetric_KeyR = value;
        }
        private protected int Nr
        {
            get => nr;
            set => nr = value;
        }
        private protected string Ny
        {
            get => ny;
            set => ny = value;
        }
        public string Set_hash_V2(List<string> Ma1, Reader r) // вычисление Ny и формирование V2
        {
            var sreader = Convert.ToSByte(r.Id_reader); // конвертация идентификатора в байт
            string v2 = "";
            var byte_key = Encoding.UTF8.GetBytes(r.Symmetric_KeyR); // создание массива байтов из симметричного ключа
            var byte_num = BitConverter.GetBytes(r.Nr); // создание массива байтов из сгенерированного числа
            r.Ny = Convert.ToBase64String(byte_key.Select((b, i) => (byte)(b ^ byte_num[i % byte_num.Length])).ToArray()); // XOR симметричного ключа и случ.числа
            string tohash = String.Concat(Ma1, r.Symmetric_KeyR, r.Nr);// конкатенация строк пакета Ma1 от метки, симметрич.ключа, nr
            byte[] tohash_byte = Encoding.UTF8.GetBytes(tohash); // перевод результата конкатенации в массив байтов
            var sha = SHA256.Create(); // cоздание объекта для использования хэш-функции
            var hash = sha.ComputeHash(tohash_byte); // вычисление хэша для массива байтов
            v2 = Convert.ToBase64String(hash); // перевод в строку Base64 для удобного хранения
            return v2;
        }
        public bool Check_hash_V3(string V3, Reader r) // проверка полученного v3
        {
            string tohash = String.Concat(r.Id_reader,Nr, r.Symmetric_KeyR);// конкатенация id считывателя, симметрич.ключа, nr
            byte[] tohash_byte = Encoding.UTF8.GetBytes(tohash); // перевод результата конкатенации в массив байтов
            var sha = SHA256.Create(); // cоздание объекта для использования хэш-функции
            var hash = sha.ComputeHash(tohash_byte); // вычисление хэша для массива байтов
            string comp_v3 = Convert.ToBase64String(hash); // перевод в строку Base64 для удобного хранения
            return (comp_v3.Equals(V3));
        }
        public List<string> Package_Ma2(Reader r, List <string> Ma1, string V2, Stopwatch tmr) // формирование пакета Ma1
        {
            List<string> package = new List<string> { r.Ny, Convert.ToString(r.Id_reader), V2 };
            for (int i = 0; i < 4; i++)
            {
                package.Add(Ma1[i]);
            }
            package.Add((tmr.ElapsedMilliseconds).ToString());
            return package;
        }
        public List<string> Package_Ma4(string v4, string Zt, Stopwatch sw) // формирование пакета Ma4
        {
            List<string> package = new List<string> {v4, Zt, sw.ElapsedMilliseconds.ToString()}; // формирование списка строк 
            return package;
        }
    }
}
