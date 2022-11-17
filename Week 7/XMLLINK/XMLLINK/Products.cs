using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XMLLINK
{
    internal class Products
    {
        public string ID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public string Season { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        internal static IEnumerable<Products> GetProducts()
        {
            XDocument products = XDocument.Load("Products.xml");

            IEnumerable<XElement> allProducts = products.Descendants("Product").Select(e => e);

            var productObjects = allProducts.Select(e => new Products
            {
                ID = (string)e.Element("id"),
                Name = (string)e.Element("name"),
                Qty = (int)e.Element("qty"),
                Price = (decimal)e.Element("price"),
                Category = (string)e.Element("cat"),
                Season = (string)e.Attribute("Season")
            });

            //foreach (var p in productObjects)
            //{
            //    Console.WriteLine($"{p.ID} and {p.Name} {p.Qty} {p.Category} {p.Season}");
            //}

            Console.WriteLine("\n");

            return productObjects;
        }

        internal static void QueryProducts(IEnumerable<Products> allProducts)
        {
            var task2 = allProducts.Select(e => e);

            var task3 = allProducts.Where(e => e.Category == "Mobile").Select(e => e);

            var task4 = allProducts.Where(e => e.Season == "Winter").Select(e => e);

            var task5 = allProducts.Where(e => e.ID == "bk199").Select(e => e);

            var task6 = allProducts.Where(e => e.Price > 50).Select(e => e);

            var task7 = allProducts.OrderBy(e => e.Price).Select(e => e);

            var task8 = allProducts.OrderByDescending(e => e.Qty).Take(5).Select(e => e);

            var task9 = allProducts.GroupBy(s => s.Category).Select(s => new {
                Category = s.Key,
                TotalQty = s.Sum(s => s.Qty)
            });

            Console.WriteLine("Task 2");
            foreach (var i in task2)
            {
                Console.WriteLine($"{i.ID} and {i.Name} {i.Qty} {i.Category} {i.Price}");
            }
            Console.WriteLine("\n");

            Console.WriteLine("Task 3");
            foreach (var i in task3)
            {
                Console.WriteLine($"{i.ID} and {i.Name} {i.Qty} {i.Category} {i.Price}");
            }
            Console.WriteLine("\n");

            Console.WriteLine("Task 4");
            foreach (var i in task4)
            {
                Console.WriteLine($"{i.ID} and {i.Name} {i.Qty} {i.Category} {i.Price} from the {i.Season} Season");
            }
            Console.WriteLine("\n");

            Console.WriteLine("Task 5");
            foreach (var i in task5)
            {
                Console.WriteLine($"{i.ID} and {i.Name} {i.Qty} {i.Category} {i.Price}");
            }
            Console.WriteLine("\n");

            Console.WriteLine("Task 6");
            foreach (var i in task6)
            {
                Console.WriteLine($"{i.ID} and {i.Name} {i.Qty} {i.Category} {i.Price}");
            }
            Console.WriteLine("\n");

            Console.WriteLine("Task 7");
            foreach (var i in task7)
            {
                Console.WriteLine($"{i.ID} and {i.Name} {i.Qty} {i.Category} {i.Price}");
            }
            Console.WriteLine("\n");

            Console.WriteLine("Task 8");
            foreach (var i in task8)
            {
                Console.WriteLine($"{i.ID} and {i.Name}: Qty {i.Qty} {i.Category} {i.Price}");
            }
            Console.WriteLine("\n");

            Console.WriteLine("Task 9");
            foreach (var i in task9)
            {
                Console.WriteLine($"{i.Category} has a total qty of {i.TotalQty}");
            }
            Console.WriteLine("\n");
        }
    }
}
