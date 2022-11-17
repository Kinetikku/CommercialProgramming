using System;

namespace LINQMethodCommands
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await Employee.GetJsonRecordsAsync();
        }
    }
}