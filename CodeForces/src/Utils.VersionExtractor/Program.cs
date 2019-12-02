using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Utils.VersionExtractor
{
    class Program
    {
        static void Main(String[] args)
        {
            var extractioTargetFileName = args[0];

            var currentDirectory = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(currentDirectory);
            var firstExecutible = files.FirstOrDefault(p => p.Contains(extractioTargetFileName));

            if (firstExecutible == null)
                return;

            var hydraVersion = FileVersionInfo.GetVersionInfo(firstExecutible).FileVersion;

            File.WriteAllText("version.txt", hydraVersion, Encoding.UTF8);
        }
    }
}
