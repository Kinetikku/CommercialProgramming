using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FileSystemNet
{
    class Staff
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

        public Staff()
        {

        }

        public static async Task GetJsonDataAsync()
        {
            Console.WriteLine("Reading Json Data");
            var jsonFile = await File.ReadAllTextAsync("../../../Staff.json");
            var allStaff = JsonSerializer.Deserialize<Staff[]>(jsonFile);

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Staff));
            System.IO.FileStream file = System.IO.File.Create("../../../Staff.xml");

            serializer.Serialize(file, allStaff);
            file.Close();
        }

        public static async Task GetJsonData()
        {
            Console.WriteLine("Reading JSON Data");
            var jsonFile = await File.ReadAllTextAsync("../../../Staff.json");

            var allStaff = JsonSerializer.Deserialize<Staff[]>(jsonFile);

            if(allStaff != null)
            {
                var all = allStaff.Select(s => s);

                foreach (var staff in all)
                    Console.WriteLine($"The staff member {staff.First_Name} {staff.Last_Name} is from {staff.Country}");
            }
            else
                Console.WriteLine("File is empty");
        }
    }
}
