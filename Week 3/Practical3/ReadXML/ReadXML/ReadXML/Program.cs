using System.Xml.Linq;

namespace ReadXMLFile{
    class ReadXML
    {
        static async Task Main(string[] Args)
        {
            await ReadInXML();
        }

        static async Task ReadInXML()
        {
            try
            {
                FileStream stream = new FileStream("../../../Products.xml", FileMode.Open);

                var products = await XDocument.LoadAsync(stream, LoadOptions.None, CancellationToken.None);
                if (products != null)
                {
                    Console.WriteLine(products);
                }
                else
                    Console.WriteLine("File is empty.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}