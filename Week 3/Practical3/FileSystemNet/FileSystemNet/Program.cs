using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace FileSystemNet
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");

            //DirectoryInfo currDir = new DirectoryInfo(@"C:\Work\College\Commercial Programming 2\Week 3\Practical3\IOFilesGenerated");
            //DirectoryInfo oldDir = new DirectoryInfo(@"C:\Work\College\Commercial Programming 2\Week 3\Practical3\IOFilesGenerated");

            //Console.WriteLine(currDir.FullName);
            //Console.WriteLine(oldDir.Exists);

            //Console.WriteLine(currDir.Parent);
            //ReadAndWrite(currDir);

            //await ReadFileAsync();
            await ReadXML();
            await Staff.GetJsonData();
            await Staff.GetJsonDataAsync();
        }

        public static void ReadAndWrite(DirectoryInfo currDir)
        {
            Console.WriteLine("Enter Directory Name:");
            string newDir = Console.ReadLine();

            if (!System.IO.Directory.Exists(currDir.FullName + newDir))
            {
                System.IO.Directory.CreateDirectory(currDir.FullName + @"\" + newDir);
                CreateFile(currDir);
            }
            else
                Console.WriteLine("Directory already exists.");
        }

        public async static void CreateFile(DirectoryInfo currDir)
        {
            Console.WriteLine("Enter File Name:");

            var filename = Console.ReadLine();

            try
            {
                if (filename != null)
                {
                    FileStream fs = new FileStream(currDir.FullName, FileMode.OpenOrCreate);

                    using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                    {
                        await writer.WriteLineAsync("this is my new file");
                        await writer.WriteLineAsync("These string characters will be encoded");
                        await writer.WriteLineAsync("The end of my file");
                    }
                }
                else
                    Console.WriteLine("File was not created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static async Task ReadFileAsync()
        {
            try
            {
                using (StreamReader rd = new StreamReader(new FileStream(@"C:\Work\College\Commercial Programming 2\Week 3\Practical3\IOFilesGenerated\demo.txt", FileMode.Open)))
                {
                    var line = "";

                    Console.WriteLine("This will read my file");

                    while ((line = await rd.ReadLineAsync()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async Task ReadXML()
        {
            try
            {
                FileStream stream = new FileStream("../../../Products.xml", FileMode.Open);

                var products = await XDocument.LoadAsync(stream, LoadOptions.None, CancellationToken.None);
                if (products != null)
                {
                    Console.WriteLine(products.Root.Name.ToString());
                }
                else
                    Console.WriteLine("File is empty.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async Task GetMyFileAsync()
        {
            using var client = new HttpClient();

            var url = "http://filesamples.com/samples/document/txt/sample3.txt";

            var fname = Path.GetFileName(url);

            //Send request via web server
            var resp = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

            //Get response code
            resp.EnsureSuccessStatusCode();
            Console.WriteLine("Prints the response code:" + resp.EnsureSuccessStatusCode());
            using Stream stream = await resp.Content.ReadAsStreamAsync();

            using FileStream fs = File.Create(fname);

            await stream.CopyToAsync(fs);

            Console.WriteLine("File download");
        }
    }
}
