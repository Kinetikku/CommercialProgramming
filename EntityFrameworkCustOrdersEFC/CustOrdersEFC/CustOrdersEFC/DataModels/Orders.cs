using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustOrdersEFC.DataModels
{
    internal class Orders
    {
        public int ID { get; set; }
        public DateOnly DateOfOrder { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal VAT { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public float AmtDue { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal TotalAmtDue { get; set; }

        //foreign keys
        public int SalesPersonID { get; set; }
        //Reference Navigation Property
        public Salesperson? Salesperson { get; set; }
        //Foreign Key
        public int CustomerID { get; set; }
        //Reference Navigation Property
        public Customer? Customer { get; set; }
    }
}
