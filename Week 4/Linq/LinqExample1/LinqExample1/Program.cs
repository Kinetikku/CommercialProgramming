namespace LinqExample1
{
    internal class Programme{

        public static async Task Main(String[] args)
        {
            Console.WriteLine("Main Method");
            //Programme.IterateNumbers();
            await Staff.GetJsonRecordsAsync();
        }

        internal static IEnumerable<int> GenerateNumbers(int maxValue)
        {
            var result = new List<int>();
            for (var i = 0; i <= maxValue; i++)
                result.Add(i);
            return result;
        }

        internal static void IterateNumbers()
        {
            Console.WriteLine("Please enter max number to iterate to:");
            var number = Convert.ToInt32(Console.ReadLine());

            var result = GenerateNumbers(number);

            //Query Syntax
            var numQuery = from num in result
                           where (num % 2) == 0
                           select num;
            //Method Syntax
            var numMethod = GenerateNumbers(number).Where(n => n % 2 == 0);

            Console.WriteLine("Iterate through IEnumerable");
            foreach(var item in result)
                Console.WriteLine(item);
        }

    }
}