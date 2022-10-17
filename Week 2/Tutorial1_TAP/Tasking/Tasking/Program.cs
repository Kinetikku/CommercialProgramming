using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasking
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Task<int> t1 = DownloadPictureAsync();
            Task<bool> t2 = GetConnectionToDBAsync();
            Task<String> t3 = GetDataFromDBAsync();

            int GetPictureResult = await t1;
            bool GetConnectionResult = await t2;
            String GetDataResult = await t3;

            Console.WriteLine("Hello World");
            Console.WriteLine($"{t1.Result} has been downloaded");

            Console.WriteLine($"{t2.Result} - Database Connected");
            Console.WriteLine($"{t3.Result} from DB ");

            Console.WriteLine("This is the end of the main method.");
        }

        public async Task<bool> GetConnectionToDBAsync()
        {
            Console.WriteLine("Getting connection to database");
            await Task.Delay(2000);
            Console.WriteLine("Connection Up");
            return true;
        }

        public async Task<String> GetDataFromDBAsync()
        {
            Console.WriteLine("Getting data from database");
            await Task.Delay(2000);
            string data = "Data Received";
            Console.WriteLine(data);
            return data;
        }

        public async Task<int> DownloadPictureAsync()
        {
            Console.WriteLine("Downloading Picture");
            await Task.Delay(2000);
            Console.WriteLine("Picture Downloaded");
            return 100;
        }
    }
}
