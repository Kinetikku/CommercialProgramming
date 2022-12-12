using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustOrdersEFC.DataModels
{
    internal class Customer : Person
    {
        public int CustPoints { get; set; } = 0;

        //Collection Navigation Property
        public List<Orders>? Orders { get; set; }
    }
}
