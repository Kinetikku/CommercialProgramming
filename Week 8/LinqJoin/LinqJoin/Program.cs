
using System;
using LinqJoin;

namespace LinqJoin
{
    internal class Program{

        public static void Main(string[] args)
        {
            IEnumerable<Transactions> transactions = new List<Transactions>();
            transactions = Transactions.GetTransactions();
            Transactions.QueryTransactions(transactions);
            Transactions.CreateNewFile();
        }
    }
}