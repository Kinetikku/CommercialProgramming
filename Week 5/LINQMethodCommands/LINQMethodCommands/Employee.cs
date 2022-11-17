using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LINQMethodCommands
{
    public class Employee
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("first_name")]
        public string First_Name { get; set; } = string.Empty;

        [JsonPropertyName("last_name")]
        public string Last_Name { get; set; } = string.Empty;

        [JsonPropertyName("gender")]
        public string Gender { get; set; } = string.Empty;

        [JsonPropertyName("job_Title")]
        public string Job_Title { get; set; } = string.Empty;

        [JsonPropertyName("salary")]
        public int Salary { get; set; }

        [JsonPropertyName("department")]
        public string Department { get; set; } = string.Empty;

        internal static async Task GetJsonRecordsAsync()
        {
            var record = await File.ReadAllTextAsync("Employees.json");
            Employee[]? allEmployees = JsonSerializer.Deserialize<Employee[]>(record);

            //Question 1
            Console.WriteLine("Question 1:");
            IEnumerable<Employee>? all = allEmployees?.Select(e => e);

            if (all != null)
            {
                foreach (Employee i in all)
                    Console.WriteLine(i.First_Name + " " + i.Last_Name + " " + i.Gender + " " + i.Department + " $" + i.Salary);
            }
            Console.WriteLine("--------------------------------------------------------------\n\n");

            //Question 2
            Console.WriteLine("Question 2:");
            IEnumerable<Employee>? all1 = allEmployees?.Where(e => e.Gender == "Male").Select(e => e);

            if (all1 != null)
            {
                foreach (Employee i in all1)
                    Console.WriteLine(i.First_Name + " " + i.Last_Name + " " + i.Gender);
            }
            Console.WriteLine("--------------------------------------------------------------\n\n");

            //Question 3
            Console.WriteLine("Question 3:");
            int empCount = (int)allEmployees?.Where(e => e.Gender == "Male").Count();

            if (empCount != null)
            {
                Console.WriteLine(empCount);
            }
            Console.WriteLine("--------------------------------------------------------------\n\n");

            //Question 4
            Console.WriteLine("Question 4:");
            IEnumerable<Employee>? all2 = allEmployees?.Where(e => e.Gender == "Female" && e.Job_Title.StartsWith("M")).Select(e => e);

            if (all2 != null)
            {
                foreach (Employee i in all2)
                    Console.WriteLine(i.First_Name + " " + i.Last_Name + " " + i.Job_Title);
            }
            Console.WriteLine("--------------------------------------------------------------\n\n");

            //Question 5
            Console.WriteLine("Question 5:");
            IEnumerable<string>? all3 = allEmployees?.Where(e => e.Salary < 42000).Select(e => $"{e.First_Name} {e.Last_Name} has a salary of {e.Salary}");

            if (all3 != null)
            {
                foreach (string i in all3)
                    Console.WriteLine(i);
            }
            Console.WriteLine("--------------------------------------------------------------\n\n");

            //Question 6
            Console.WriteLine("Question 6:");
            IEnumerable<Employee>? all4 = allEmployees?.Where(e => e.Salary < 42000).Select(e => e);

            if (all4 != null)
            {
                foreach (Employee i in all4)
                    Console.WriteLine(i.First_Name + " " + i.Last_Name + " has a salary of $" + i.Salary);
            }
            Console.WriteLine("--------------------------------------------------------------\n\n");

            //Question 7
            Console.WriteLine("Question 7:");
            IEnumerable<Employee>? all5 = allEmployees?.Where(e => e.Job_Title.StartsWith("Comp") && e.Salary < 50000).Select(e => e);

            if (all5 != null)
            {
                foreach (Employee i in all5)
                    Console.WriteLine(i.First_Name + " " + i.Last_Name + " " + i.Job_Title + i.Salary);
            }
            Console.WriteLine("--------------------------------------------------------------\n\n");

            //Question 8
            Console.WriteLine("Question 8:");
            IEnumerable<Employee>? all6 = allEmployees?.Where(e => e.Job_Title.StartsWith("E") || e.Job_Title.StartsWith("T")).Select(e => e);

            if (all6 != null)
            {
                foreach (Employee i in all6)
                    Console.WriteLine(i.First_Name + " " + i.Last_Name + " " + i.Job_Title + " $"+ i.Salary);
            }
            Console.WriteLine("--------------------------------------------------------------\n\n");

            //Question 9
            Console.WriteLine("Question 9:");
            IEnumerable<string>? all7 = allEmployees?.OrderBy(e => e.Salary).Take(100).Select(e => $"{e.First_Name} {e.Last_Name} {e.Salary} {e.Gender}");

            if (all7 != null)
            {
                foreach (string i in all7)
                    Console.WriteLine(i);
            }
            Console.WriteLine("--------------------------------------------------------------\n\n");

            //Question 10
            Console.WriteLine("Question 10:");
            var all8 = allEmployees?.Where( e => e.Gender == "Female").Average(e => e.Salary);

            if (all8 != null)
            {
                Console.WriteLine("Average Salary is $" + all8);
            }
            Console.WriteLine("--------------------------------------------------------------\n\n");

            //Section 2 - Question 1
            Console.WriteLine("Section 2 - Question 1:");
            var all9 = allEmployees?.Where(e => e.Department == "Sales").Sum(e => e.Salary);

            if (all9 != null)
            {
                Console.WriteLine("Sum of Salary in Sales is $" + all9);
            }
            Console.WriteLine("--------------------------------------------------------------\n\n");

            //Section 2 - Question 2
            Console.WriteLine("Section 2 - Question 2:");
            var all10 = allEmployees?.OrderByDescending(e => e.Salary).Select(e => new { last_Name = e.Last_Name, department = e.Department, salary = e.Salary});

            if (all10 != null)
            {
                foreach (var i in all10)
                {
                    Console.WriteLine(i.last_Name + " " + i.department + " " + i.salary);
                }
            }
            Console.WriteLine("--------------------------------------------------------------\n\n");

            //Section 2 - Question 3
            Console.WriteLine("Section 2 - Question 3:");
            var all11 = allEmployees?.GroupBy(e => e.Department).Select(e => new { department = e.Key, last_name = e });

            if (all11 != null)
            {
                foreach (var i in all11)
                {
                    Console.WriteLine(i.department);

                    foreach (var x in i.last_name)
                    {
                        Console.WriteLine(" " + x.Last_Name);
                    }
                }
            }
            Console.WriteLine("--------------------------------------------------------------\n\n");

            //Section 2 - Question 4
            Console.WriteLine("Section 2 - Question 4:");
            var all12 = allEmployees?.GroupBy(e => e.Department).Select(e => new {department = e.Key, emp = e.Count()});

            if (all12 != null)
            {
                foreach (var i in all12)
                {
                    Console.WriteLine(i);
                }
            }
            Console.WriteLine("--------------------------------------------------------------\n\n");

            //Section 2 - Question 5
            Console.WriteLine("Section 2 - Question 5:");
            var all13 = allEmployees?.GroupBy(e => e.Department).Select(e => new { department = e.Key, salary = e.Sum(e => e.Salary) });

            if (all13 != null)
            {
                foreach (var i in all13)
                {
                    Console.WriteLine(i);
                }
            }
            Console.WriteLine("--------------------------------------------------------------\n\n");

            //Section 2 - Question 6
            Console.WriteLine("Section 2 - Question 6:");
            var all14 = allEmployees?.GroupBy(e => e.Department).Select(e => new { department = e.Key, Max_salary = e.Max(e => e.Salary) });

            if (all14 != null)
            {
                foreach (var i in all14)
                {
                    Console.WriteLine(i);
                }
            }
            Console.WriteLine("--------------------------------------------------------------\n\n");
        }
    }
}
