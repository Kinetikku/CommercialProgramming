using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace Week2_Tutorial1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Creating and starting a stopwatch
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Console.WriteLine("Hello, World");
            Console.WriteLine("Main Thread has started");
            Console.WriteLine("ThreadID: " + Environment.CurrentManagedThreadId);
            Thread.CurrentThread.Name = "Main Thread";

            //Create Task Objects to assign to the methods below
            Task<int> t1 = method1Async();
            Task<char> t2 = method2Async();
            Task<List<String>> t3 = method3Async();

            int method1Result = await t1;
            char method2Result = await t2;
            List<string> method3Result = await t3;

            Console.WriteLine($"Task 1 returned {method1Result}");
            Console.WriteLine($"Task 2 returned {method2Result}");
            Console.WriteLine($"Task 3 returned {method3Result[0]}");
            Console.WriteLine("Main Thread has complete");
            //Start the Tasks
            //t1.Start();
            //t2.Start();
            //t2.Wait();
            //t3.Start();            

            //Stopping the stopwatch
            stopWatch.Stop();

            Console.WriteLine("RunTime in MilliSecounds " + stopWatch.ElapsedMilliseconds);
            
        }

        static async Task<int> method1Async()
        {
            Console.WriteLine("This is method 1");
            Console.WriteLine("ThreadID:" + Environment.CurrentManagedThreadId);
          
            //for(int i = 0; i < 2000; i++)
            //    Console.WriteLine(i);

            int num1 = 5, num2 = 5;
            int num3 = num1 + num2;

            await Task.Delay(200);

            return num3;
        }

        static async Task<char> method2Async()
        {
            Console.WriteLine("This is method 2");
            Console.WriteLine("ThreadID:" + Environment.CurrentManagedThreadId);

            var name = "Your Name";
            char c = name.FirstOrDefault();

            await Task.Delay(200);

            return c;
        }

        static async Task<List<String>> method3Async()
        {
            Console.WriteLine("This is method 3");
            Console.WriteLine("ThreadID:" + Environment.CurrentManagedThreadId);
            var listOfNames = new List<string> { "Edwina", "John", "Anne" };

            await Task.Delay(200);

            return listOfNames;
        }
    }
}
