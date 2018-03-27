using System;
using System.Collections.Generic;
using System.IO;

namespace CodeForces.Units
{
    public static class DiskDirectorySearchUnit
    {
        public static void Run()
        {
            CollectGsDirectories();
        }
        private static void GetNumberAllDirectoriesOnAllDrives()
        {
            var drives = DriveInfo.GetDrives();
            foreach (var x in drives)
            {
                Console.WriteLine();
                Console.WriteLine(x.Name);

                var driveDirectories = GetAllSubDirectories(x.RootDirectory);
                Console.WriteLine(driveDirectories.Length);
            }
        }
        private static void CollectGsDirectories()
        {
            var drives = DriveInfo.GetDrives();
            foreach (var x in drives)
            {
                Console.WriteLine();
                Console.WriteLine(x.Name);

                var driveDirectories = FindDirectories(x.RootDirectory, "_gsdata_");
                foreach (var y in driveDirectories)
                {
                    Console.WriteLine(y.FullName);
                }
            }
        }   // TODO: Insufficient permissions exception


        // SUPPORT FUNCTIONS //////////////////////////////////////////////////////////////////////
        private static DirectoryInfo[] GetAllSubDirectories(DirectoryInfo directory)
        {
            var subDirectoryList = new List<DirectoryInfo>();

            try
            {
                var subDirectories = directory.GetDirectories();
                subDirectoryList.AddRange(subDirectories);

                foreach (var x in subDirectories)
                {
                    var subsOfSubdirectory = GetAllSubDirectories(x);
                    subDirectoryList.AddRange(subsOfSubdirectory);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Issuficient permissions, just skip this directory
            }
            catch (DirectoryNotFoundException)
            {
                // TODO: This happening from time to time, maybe because there are temp directories
            }
            catch (IOException)
            {
                // TODO: Sometimes old HDD doesn't cope with numder of requests like my old Seagate
            }

            return subDirectoryList.ToArray();
        }
        private static DirectoryInfo[] FindDirectories(DirectoryInfo directory, String pattern)
        {
            return directory.GetDirectories(pattern, SearchOption.AllDirectories);
        }
    }
}