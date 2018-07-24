using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Samples
{
	class Program
    {
        static void Main(string[] args)
        {
            var namesCount = 100;

            var names = new List<String>(namesCount);
            var baseNames = new[] { "Pete", "Harry", "Gracham" };
            var random = new Random();


            for (var i = 0; i < namesCount; i++)
            {
                var randomlySelectedName = baseNames[random.Next(2)];
                //var name = $"{randomlySelectedName}-{random.Next(1000)}";

                var name = $"{randomlySelectedName}-{random.Next(1000)}";

                names.Add(name);
            }

            foreach (var x in names)
            {
                Console.WriteLine(x);
            }

            var fileName = "result.txt";
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var path = Path.Combine(desktopPath, fileName);

            SaveNames(path, names);

            Console.ReadKey();
        }


        // SUPPORT FUNCTIONS //////////////////////////////////////////////////////////////////////
        private static void SaveNames(String path, IReadOnlyCollection<String> name)
        {
            var stringBuilder = new StringBuilder(100);
            foreach (var x in name)
            {
                stringBuilder.AppendLine(x);
            }

            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    writer.Write(stringBuilder);
                }
            }
        }
        private static String CreateRandomString()
        {
            var id = Guid.NewGuid();
            var idStr = id.ToString();
            var cleanId = idStr.Replace("-", "");
            return cleanId;
        }
    }
}
