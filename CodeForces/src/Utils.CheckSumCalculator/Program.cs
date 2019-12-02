using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Utils.CheckSumCalculator
{
    class Program
    {
        // args[0] - required file extensions: exe dll

        static void Main(String[] args)
        {
            if (args.Length == 0)
                return;

            var sourceDirectory = Directory.GetCurrentDirectory();
            var targetDirectory = Directory.GetCurrentDirectory();

            var itselfName = Assembly.GetExecutingAssembly().GetName().Name;
            var requiredFilesWithoutItself = Directory.GetFiles(sourceDirectory).Where(x =>
            {
                var fileName = Path.GetFileName(x);
                return Array.Exists(args, fileName.Contains) && !fileName.Contains(itselfName);
            }).ToArray();

            var fileCheckSums = requiredFilesWithoutItself.Select(CalculateCheckSum);

            File.WriteAllText($"{targetDirectory}\\catalog.json", JsonConvert.SerializeObject(fileCheckSums), Encoding.UTF8);
        }
        private static FileCheckSum CalculateCheckSum(String filePath)
        {
            using (var stream = File.OpenRead(filePath))
            {
                using (var sha = new SHA256Managed())
                {
                    var hash = sha.ComputeHash(stream);
                    return new FileCheckSum()
                    {
                        CheckSum = BitConverter.ToString(hash).Replace("-", String.Empty),
                        FileName = Path.GetFileName(filePath)
                    };
                }
            }
        }
    }
}
