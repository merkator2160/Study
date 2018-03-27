using CodeForces.Units.Protobuf.Models;
using ProtoBuf;
using System;
using System.IO;
using System.IO.Compression;

namespace CodeForces.Units.Protobuf
{
    public static class ProtobufSerializationUnite
    {
        public static void Run()
        {
            var filePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/data.zip";
            var person = new Person
            {
                Id = Int32.MaxValue,
                Name = "Person name",
                Phones = new String[] { "84628374563", "84763542319", "8567835132" },
                Address = new Address
                {
                    Line1 = "Value of line 1",
                    Line2 = "Value of line 2",
                    Line3 = "Value of line 3"
                }
            };

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                using (var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Create, true))
                {
                    var zipArchiveEntry = zipArchive.CreateEntry("data.bin", CompressionLevel.Optimal);
                    var zipStream = zipArchiveEntry.Open();
                    Serializer.Serialize(zipStream, person);
                }
            }
            using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Read, true))
                {
                    var zipArchiveEntry = zipArchive.Entries[0];
                    var zipStream = zipArchiveEntry.Open();
                    var deserializedPerson = Serializer.Deserialize<Person>(zipStream);
                    Console.WriteLine(deserializedPerson);
                }
            }
        }
    }
}