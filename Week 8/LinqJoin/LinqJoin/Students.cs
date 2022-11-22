using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LinqJoin
{
    public class Students
    {

        [JsonPropertyName("Student_id")]
        public int Student_ID { get; set; }
        [JsonPropertyName("first_name")]
        public string First_Name { get; set; }
        [JsonPropertyName("last_name")]
        public string Last_Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("gender")]
        public string Gender { get; set; }
        [JsonPropertyName("Credit_Card_No")]
        public string Credit_Card_No { get; set; }
        public static async Task<IEnumerable<Students>> GetStudentDataA()
        {
            var file = await File.ReadAllTextAsync("students.json");
            var allStudents = JsonSerializer.Deserialize<Students[]>(file);
            IEnumerable<Students> students = allStudents.Select(s => s);

            foreach (var s in students)
                Console.WriteLine(s.Last_Name);

            return students;
        }

        public static void QueryData(IEnumerable<Students> allStudents)
        {
            Console.WriteLine("Query 1");
            var q1 = allStudents.Select(e => e);
            foreach (var s in q1)
                Console.WriteLine(s.First_Name);

            Console.WriteLine("Query 2");
            var q2 = allStudents.Where(e => e.Last_Name.StartsWith("W")).Select(e => e);
            foreach (var s in q2)
                Console.WriteLine(s.Last_Name);

            Console.WriteLine("Query 3");
            var q3 = allStudents.GroupBy(e => e.Gender).Select(e => e);
            foreach (var e in q3)
            {
                Console.WriteLine(e.Key);

                foreach (var e2 in e)
                    Console.WriteLine($"{e2.First_Name} {e2.Last_Name} {e2.Gender}");
            }

            Console.WriteLine("Query 4");
            var q4 = allStudents.OrderByDescending(e => e.Student_ID).Select(e => e);

            foreach (var e in q4)
                Console.WriteLine($"{e.Student_ID} {e.Last_Name}");

            Console.WriteLine("query 5");
            var q5 = allStudents.Where(e => e.Gender == "Male" && e.Last_Name == "Kinnard").Select(e => e);
            {
                foreach (var e in q5)
                    Console.WriteLine($"Name: {e.First_Name} {e.Last_Name}  Student ID: {e.Student_ID}  Gender:{e.Gender}");
            }
        }
    }



}
