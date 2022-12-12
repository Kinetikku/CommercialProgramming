using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustOrdersEFC.DataModels
{
    internal class Person
    {
        public int ID { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = String.Empty;
        [MaxLength(200)]
        public string Address { get; set; } = String.Empty;
        [MaxLength(15)]
        public string PhoneNumber { get; set; } = String.Empty;
    }
}
