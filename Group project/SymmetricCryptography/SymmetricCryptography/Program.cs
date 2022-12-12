using System.Net;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.InteropServices;

namespace SymmettricCryptography
{
    internal class Program
    {
        public static byte[]? password;
        public static byte[]? ivNum;

        //These variables are used for the file explorer to be opened
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct OpenFileName
        {
            public int lStructSize;
            public IntPtr hwndOwner;
            public IntPtr hInstance;
            public string lpstrFilter;
            public string lpstrCustomFilter;
            public int nMaxCustFilter;
            public int nFilterIndex;
            public string lpstrFile;
            public int nMaxFile;
            public string lpstrFileTitle;
            public int nMaxFileTitle;
            public string lpstrInitialDir;
            public string lpstrTitle;
            public int Flags;
            public short nFileOffset;
            public short nFileExtension;
            public string lpstrDefExt;
            public IntPtr lCustData;
            public IntPtr lpfnHook;
            public string lpTemplateName;
            public IntPtr pvReserved;
            public int dwReserved;
            public int flagsEx;
        }

        public static void Main(string[] args)
        {
            while (true)
            {
                //Menu
                Menu();

                int choice = Convert.ToInt32(Console.ReadLine());
                string website = "";
                string result = "";

                bool loop = true;
                while (loop)
                {
                    //Downloads the HTML of a website
                    //Stores the user password into a global variable and converts to byte[]
                    //Saves the HTML into a file after being encrypted
                    //Returns the decrypted HTML code from the file
                    if (choice == 1)
                    {
                        while (loop)
                        {
                            Console.WriteLine("Please enter a website to download...");
                            website = Console.ReadLine();

                            if (ValidateWebsite(website))
                                loop = false;
                            else
                            {
                                Console.WriteLine("Not a valid website");
                                Console.Read();
                            }
                        }
                        
                        website = DownloadWebsite(website);
                        password = Encoding.UTF8.GetBytes(Password());
                        SaveFile(website, password);
                    }
                    //Opens the users file that they choose
                    //Asks the user for their original password
                    else if (choice == 2)
                    {
                        loop = false;
                        result = OpenFile("encrptedFile.txt");
                    }
                    else
                    {
                        Console.WriteLine("Please choose between 1 or 2");
                        choice = Convert.ToInt32(Console.ReadLine());
                    }
                }
                Console.WriteLine(result);
            }
        }

        //This is the method that displays the file explorer
        [DllImport("comdlg32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool GetOpenFileName(ref OpenFileName ofn);

        private static string ShowDialog()
        {
            var openFileName = new OpenFileName();
            openFileName.lStructSize = Marshal.SizeOf(openFileName);
            openFileName.lpstrFilter = "All Files (*.*)\0*.*\0";
            openFileName.lpstrFile = new string(new char[256]);
            openFileName.nMaxFile = openFileName.lpstrFile.Length;
            openFileName.lpstrFileTitle = new string(new char[64]);
            openFileName.lpstrInitialDir = Environment.CurrentDirectory + "\\";
            openFileName.nMaxFileTitle = openFileName.lpstrFileTitle.Length;
            openFileName.lpstrTitle = "Open File Dialog...";
            if (GetOpenFileName(ref openFileName))
                return openFileName.lpstrFile;
            return string.Empty;
        }

        public static bool ValidateWebsite(string webInput)
        {
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(webInput);
                webRequest.Method = "HEAD";

                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                if (webResponse.StatusCode == HttpStatusCode.OK)
                    return true;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return false;
        }

        public static void Menu()
        {
            string[] options = { "Download & encrypt website", "Open encrypted file" };

            for (int i = 0; i < options.Length; i++)
                Console.WriteLine($"{i+1}: {options[i]}");
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

            string passwordInput = "";

            bool loop = true;
            while (loop)
            {
                Console.WriteLine("Please enter a password");
                passwordInput = Console.ReadLine();

                if (passwordInput.Length == 32 || passwordInput.Length == 16)
                    loop = false;
                else
                {
                    Console.WriteLine($"Password length must be 32bits or 16bits long, your previous input was {passwordInput.Length}");
                    Console.Read();
                }
            }
            
            return passwordInput;
        }

        public static string OpenFile(string userFile)
        {
            Console.WriteLine("Please input the file password");

            string textFile = "";
            using (StreamReader file = new StreamReader(ShowDialog()))
            {
                string ln;

                while ((ln = file.ReadLine()) != null)
                {
                    textFile += ln;
                }

                file.Close();
            }

            string decrypted = "";

            using (Aes myAes = Aes.Create())
            {
                myAes.Key = Encoding.UTF8.GetBytes(Password());
                myAes.GenerateIV();

                decrypted = DecryptStringFromBytes(textFile, myAes.Key, myAes.IV);
            }

            return decrypted;


            return "";
        }

        public static void SaveFile(string file, byte[] password)
        {
            //Need to implement the custom password bit
            using (Aes myAes = Aes.Create())
            {
                myAes.Key = password;
                myAes.GenerateIV();
                ivNum = myAes.IV;

                string encrypted = EncryptStringToBytes(file, myAes.Key, myAes.IV);

                FileStream fs = new FileStream("encrptedFile.txt", FileMode.Create);
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
                aesAlg.Key = password;
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

        static string DecryptStringFromBytes(string file, byte[] Key, byte[] IV)
        {
            //Converts the string to a byte[]
            byte[] cipherByte = Convert.FromBase64String(file);

            string text;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Mode = CipherMode.CBC;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherByte))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                    {
                        csDecrypt.Write(cipherByte);
                        csDecrypt.Close();
                    }
                    text = Encoding.UTF8.GetString(msDecrypt.ToArray());
                }
            }

            return text;
        }
    }
}