using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LinqExample1
{
    public class Staff
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("first_name")]
        public string? First_Name { get; set; }

        [JsonPropertyName("last_name")]
        public string? Last_Name { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("gender")]
        public string? Gender { get; set; }

        [JsonPropertyName("ip_address")]
        public string? Ip_Address { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        internal static async Task GetJsonRecordsAsync()
        {
            var file = await File.ReadAllTextAsync("staff.json");
            Staff[]? allStaff = JsonSerializer.Deserialize<Staff[]>(file);

            //IEnumerable<Staff>? all = allStaff?.Select(s => s);

            IEnumerable<Staff>? all = allStaff?.Where(s => s.Country == "Greece" && s.Gender == "Male").Select(s => s);

            if(all != null)
            {
                foreach(Staff i in all)
                    Console.WriteLine("The staff member " + i.First_Name + " is from " + i.Country + " and gender is " + i.Gender);
            }

            Console.WriteLine("\n--------------------------------------\n");

            //List<string>? listStaff = allStaff?.Where(s => s.Gender == "Male" && s.Country == "Greece").Select(s => $"{s.Gender} {s.Last_Name}").ToList();

            //if (listStaff != null)
            //{
            //    foreach (string i in listStaff)
            //    {
            //        Console.WriteLine("{0}", i);
            //    }
            //}

            allStaff?.Where(s => s.Country != null && s.Country.StartsWith("C"))
                .OrderBy(s => s.Country)
                .Skip(240)
                .Select(s => $"{s.Last_Name} is from {s.Country}").ToList()
                .ForEach(s => Console.WriteLine(s));

            Console.WriteLine("\n--------------------------------------\n");

            var countOfStaff = allStaff?.Where(s => s.Country != null && s.Country.StartsWith("C")).Count();

            Console.WriteLine("The count of staff from countries beginning with 'C' are {0}", countOfStaff);
        }

    }
}
