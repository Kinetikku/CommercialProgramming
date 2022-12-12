using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustOrdersEFC.DataModels
{
    internal class Salesperson : Person
    {
        [Column(TypeName = "decimal(6, 2)")]
        public decimal SalesTotal { get; set; }

        //Foreign key
        public int DepartmentID { get; set; }

        //Reference Navigation Property
        public Department? Department { get; set; }
    }
}
