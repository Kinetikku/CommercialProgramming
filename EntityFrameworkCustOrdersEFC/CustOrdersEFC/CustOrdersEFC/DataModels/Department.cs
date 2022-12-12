using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustOrdersEFC.DataModels
{
    internal class Department
    {
        public int ID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        //Collection Navigation Property
        public List<Salesperson> Salespersons { get; set; }
    }
}
