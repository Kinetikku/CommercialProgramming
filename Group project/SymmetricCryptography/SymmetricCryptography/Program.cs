using System.Net;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SymmettricCryptography
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string website = DownloadWebsite("https://www.lkbikes.com");
            string password = Password();
            SaveFile(website, Encoding.UTF8.GetBytes(password));
            string result = OpenFile("encrptedFile.txt");
            Console.WriteLine(result);
        }

        public static string DownloadWebsite(string url)
        {
            WebClient web = new WebClient();

            string resp;

            try
            {
                resp = web.DownloadString(url);
                return resp;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
            }

            return "";
        }

        public static string Password()
        {
            Console.Clear();

            string password = "";

            bool loop = true;
            while (loop)
            {
                Console.WriteLine("Please enter a password");
                password = Console.ReadLine();

                if (password.Length > 0)
                    loop = false;
                else
                    Console.ReadKey();
            }
            
            return password;
        }

        public static string OpenFile(string userFile)
        {
            string textFile = "";
            string rootFolder = Environment.CurrentDirectory + "\\";
            using (StreamReader file = new StreamReader(rootFolder + userFile))
            {
                int count = 0;
                string ln;

                while ((ln = file.ReadLine()) != null)
                {
                    textFile += ln;
                    count++;
                }

                file.Close();
            }

            string decrypted = "";

            using (Aes myAes = Aes.Create())
            {
                myAes.GenerateKey();
                myAes.GenerateIV();

                decrypted = DecryptStringFromBytes(Encoding.UTF8.GetBytes(textFile), myAes.Key, myAes.IV);
            }

            return decrypted;
        }

        public static void SaveFile(string file, byte[] password)
        {
            string crptPass = Convert.ToBase64String(password);

            //Need to implement the custom password bit
            using (Aes myAes = Aes.Create())
            {
                myAes.GenerateKey();
                myAes.GenerateIV();

                string encrypted = EncryptStringToBytes(file, myAes.Key, myAes.IV);

                FileStream fs = new FileStream("encrptedFile.txt", FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(encrypted);
                sw.Close();
            }
        }

        public static string EncryptStringToBytes(string file, byte[] Key, byte[] IV)
        {
            byte[] encrypted;

            //Creates an Aes object with a specified key and iv.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                //Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                //Create the streams used for encryption
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(file);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        static string DecryptStringFromBytes(byte[] file, byte[] Key, byte[] IV)
        {
            string text;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(file))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            text = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return text;
        }
    }
}