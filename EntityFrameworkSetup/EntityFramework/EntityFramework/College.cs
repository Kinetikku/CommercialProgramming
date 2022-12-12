using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EntityFramework
{
    internal class College
    {
        public int ID { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }

        public List<Course> Courses { get; set; } = new();
    }
}
