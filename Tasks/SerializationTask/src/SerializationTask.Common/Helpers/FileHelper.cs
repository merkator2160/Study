using Newtonsoft.Json;
using System.Text;

namespace SerializationTask.Common.Helpers
{
    public static class FileHelper
    {
        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public static String DesktopDirectory => Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


        // READING ////////////////////////////////////////////////////////////////////////////////
        public static Task<T> GetFromJsonFileAsync<T>(String filePath)
        {
            return Task.Run(() => GetFromJsonFile<T>(filePath, Encoding.UTF8));
        }
        public static Task<T> GetFromJsonFileAsync<T>(String filePath, Encoding encoding)
        {
            return Task.Run(() => GetFromJsonFile<T>(filePath, encoding));
        }

        public static T GetFromJsonFile<T>(String filePath)
        {
            return GetFromJsonFile<T>(filePath, Encoding.UTF8);
        }
        public static T GetFromJsonFile<T>(String filePath, Encoding encoding)
        {
            using (var streamReader = new StreamReader(filePath, encoding))
            {
                using (var textReader = new JsonTextReader(streamReader))
                {
                    return new JsonSerializer().Deserialize<T>(textReader);
                }
            }
        }

        private static Task<String> GetFileTextAsync(String filePath)
        {
            return Task.Run(() => GetFileText(filePath));
        }
        private static Task<String> GetFileTextAsync(String filePath, Encoding encoding)
        {
            return Task.Run(() => GetFileText(filePath, encoding));
        }

        private static String GetFileText(String filePath)
        {
            return GetFileText(filePath, Encoding.UTF8);
        }
        private static String GetFileText(String filePath, Encoding encoding)
        {
            using (var streamReader = new StreamReader(filePath, encoding))
            {
                return streamReader.ReadToEnd();
            }
        }
        private static IEnumerable<String> GetFileLines(String filePath, Encoding encoding)
        {
            using (var sr = new StreamReader(filePath, encoding))
                while (sr.ReadLine() is { } line)
                    yield return line;
        }


        // WRITING ////////////////////////////////////////////////////////////////////////////////
        public static Task SaveOnDiskAsJsonAsync<T>(this T items, String filePath, Encoding encoding)
        {
            return Task.Run(() => { SaveOnDiskAsJson(items, filePath, encoding); });
        }
        public static Task SaveOnDiskAsJsonAsync<T>(this T items, String filePath)
        {
            return Task.Run(() => { SaveOnDiskAsJson(items, filePath); });
        }

        public static void SaveOnDiskAsJson<T>(this T items, String filePath)
        {
            SaveOnDiskAsJson(items, filePath, Encoding.UTF8);
        }
        public static void SaveOnDiskAsJson<T>(this T items, String filePath, Encoding encoding)
        {
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                using (var streamWriter = new StreamWriter(stream, encoding))
                {
                    using (var textWriter = new JsonTextWriter(streamWriter))
                    {
                        new JsonSerializer().Serialize(textWriter, items, typeof(T));
                    }
                }
            }
        }

        public static async Task SaveOnDiskAsync(this String str, String filePath)
        {
            await SaveOnDiskAsync(str, filePath, Encoding.UTF8);
        }
        public static async Task SaveOnDiskAsync(this String str, String filePath, Encoding encoding)
        {
            await using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await using (var streamWriter = new StreamWriter(stream, encoding))
                {
                    await streamWriter.WriteAsync(str);
                }
            }
        }

        public static void SaveOnDisk(this String str, String filePath)
        {
            SaveOnDisk(str, filePath, Encoding.UTF8);
        }
        public static void SaveOnDisk(this String str, String filePath, Encoding encoding)
        {
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                using (var streamWriter = new StreamWriter(fs, encoding))
                {
                    streamWriter.Write(str);
                }
            }
        }

        public static void SaveOnDisk(this Byte[] data, String filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                using (var streamWriter = new BinaryWriter(fs))
                {
                    streamWriter.Write(data);
                }
            }
        }
        public static void SaveOnDisk(this Stream sourceStream, String filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                sourceStream.Seek(0, SeekOrigin.Begin);
                sourceStream.CopyTo(fs);
            }
        }
    }
}