// See https://aka.ms/new-console-template for more information
using System;
using XMLLINK;

namespace XMLLINK
{
    internal class Programe
    {
        static void Main(string[] args)
        {
            IEnumerable<Products> products = new List<Products>();
            products = Products.GetProducts();
            Products.QueryProducts(products);
        }

        
    }
}


