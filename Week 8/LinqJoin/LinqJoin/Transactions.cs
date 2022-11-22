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

        //This method will read all the transactions from the XML file. It only reads child elements that are stored in the
        // <order? tags
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

        //This method outputs different querys to suit the required tasks listed on the worksheet
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
        }

        internal static void QueryJoin1(IEnumerable<Transactions> allTransactions, IEnumerable<Students> allStudents)
        {
            var join = allStudents.GroupJoin(allTransactions,
                studs => studs.Credit_Card_No,
                trans => trans.CreditCardNO,
                (studs, trans) => new
                {
                    TransactionID = trans.TransactionID,
                    First_Name = studs.First_Name,
                    Last_Name = studs.Last_Name
                }) ;

            foreach (var s in join)
            {
                Console.WriteLine("TransactionID: " + s.TransactionID + " Student : " + s.student_fname + s.student_lname
                    + "Number of transactions: " + s.List_Transactions);
            }
        }

        internal static void QueryJoin2(IEnumerable<Transactions> allTransactions, IEnumerable<Students> allStudents)
        {
            var join2 = allStudents.GroupJoin(allTransactions,
            studs => studs.Credit_Card_No,
            trans => trans.CreditCardNO,
            (studs, trans) => new
            {
                student_id = studs.Student_ID,
                student_fname = studs.First_Name,
                student_lname = studs.Last_Name,
                List_Transactions = trans.Count()
            });

            foreach (var s in join2)
            {
                Console.WriteLine(s.student_fname + "  " + s.student_lname + "  " + s.student_id
                    + "Number of transactions: " + s.List_Transactions);
            }
        }

        //This method will get the current directory, create a new file and if there is a
        //file already with the same name it will overwite it. It will then print out
        //the XML tags in the supposed correct manner while following XML syntax
        internal static void WriteToXMLFile(IEnumerable<Transactions> allTransactions)
        {
            string path = System.AppDomain.CurrentDomain?.BaseDirectory;

            using (StreamWriter sw = new StreamWriter($"{path}Transactions2.xml", false))
            {
                var results = allTransactions.Select(e => e);

                sw.WriteLine("<!-- Updated of Total Amt Due-->");
                sw.WriteLine("<stock>");

                //I added tabs to make the readability easier
                foreach (var t in results)
                {
                    sw.WriteLine("\t</order>");
                    sw.WriteLine("\t\t<Transaction_ID>" + t.TransactionID + "</Transaction_ID>");
                    sw.WriteLine("\t\t<Credit_Card_no>" + t.CreditCardNO + "</Credit_Card_no>");
                    sw.WriteLine("\t\t<VAT>" + t.VAT + "</VAT>");
                    sw.WriteLine("\t\t<Total_Ex_VAT>" + t.TotalexVAT + "</Total_Ex_VAT>");
                    sw.WriteLine("\t\t<Number_of_items>" + t.NoItems + "</Number_of_items>");
                    sw.WriteLine("\t\t<Total_Amt_Due>" + "</Total_Amt_Due>");
                    sw.WriteLine("\t</order>");
                }

                sw.WriteLine("</stock>");

            }
        }
    }
}
