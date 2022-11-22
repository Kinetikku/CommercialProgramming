
using System;
using LinqJoin;

namespace LinqJoin
{
    internal class Program{

        public static async Task Main(string[] args)
        {
            IEnumerable<Transactions> transactions = new List<Transactions>();
            transactions = Transactions.GetTransactions();

            IEnumerable<Students> students = new List<Students>();
            students = await Students.GetStudentDataA();

            Transactions.QueryTransactions(transactions);

            Transactions.QueryJoin1(transactions, students);
            Transactions.QueryJoin2(transactions, students);

            Transactions.WriteToXMLFile(transactions);
        }
    }
}