using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EntityFramework
{
    internal class Course
    {
        public int ID { get; set; }

        [MaxLength(200)]
        public string? Name { get; set; } = string.Empty;

        public int? Duration { get; set; } = 0;

        [ColumnAttribute(TypeName = "decimal(5, 2)")]
        public decimal Fees { get; set; }
        public int Points { get; set; } = 0;

        public College? College { get; set; }
        public int CollegeID { get; set; } = 0;
    }
}
