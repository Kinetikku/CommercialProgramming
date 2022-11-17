using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqJoin
{
    internal class Transactions
    {
        public string TransactionID { get; set; } = string.Empty;
        public long CreditCardNO { get; set; }
        public int VAT { get; set; }
        public  int TotalexVAT { get; set; }
        public int NoItems  { get; set; }

        internal static IEnumerable<Transactions> GetTransactions()
        {
            XDocument transactions = XDocument.Load("Transactions.xml");

            IEnumerable<XElement> allTransactions = transactions.Descendants("order").Select(e => e);

            var transactionObjects = allTransactions.Select(e => new Transactions
            {
                TransactionID = (string)e.Element("Transaction_ID"),
                CreditCardNO = (long)e.Element("Credit_Card_no"),
                VAT = (int)e.Element("VAT"),
                TotalexVAT = (int)e.Element("Total_Ex_VAT"),
                NoItems = (int)e.Element("Number_of_items")
            });

            return transactionObjects;
        }

        internal static void QueryTransactions(IEnumerable<Transactions> allTransactions)
        {
            var task1 = allTransactions.OrderBy(e => e.CreditCardNO).Select(e => e);

            Console.WriteLine("Task 1:-------------------");

            foreach (var t in task1)
            {
                Console.WriteLine($"Transaction ID: {t.TransactionID}\nCredit Card Number: {t.CreditCardNO}\nVAT: {t.VAT}\nTotal Ex VAT: {t.TotalexVAT}\nNumber of Items: {t.NoItems}\n");
            }

            var task2 = allTransactions.Where(e => e.TotalexVAT > 2000 && e.NoItems < 3).Select(e => e);

            Console.WriteLine("Task 2:-------------------");

            foreach (var t in task2)
            {
                Console.WriteLine($"Transaction ID: {t.TransactionID}\nCredit Card Number: {t.CreditCardNO}\nVAT: {t.VAT}\nTotal Ex VAT: {t.TotalexVAT}\nNumber of Items: {t.NoItems}\n");
            }

            var task3 = allTransactions.Select(e => new { TotalexVAT = e.TotalexVAT, VAT = e.VAT});

            Console.WriteLine("Task 3:-------------------");

            foreach (var t in task3)
            {
                Console.WriteLine($"VAT: {t.VAT}\nTotal Ex VAT: {t.TotalexVAT}");
            }

            var task4 = allTransactions.Select(e => new { TotalexVAT = e.TotalexVAT, VAT = e.VAT });
        }

        internal static void CreateNewFile()
        {
            string path = Environment.CurrentDirectory;

            using (StreamWriter sw = File.CreateText(path + ".xml")) ;
        }
    }
}
