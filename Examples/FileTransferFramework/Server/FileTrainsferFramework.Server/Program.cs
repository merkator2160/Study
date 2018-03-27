using System.ServiceModel;

namespace FileTrainsferFramework.Server
{    
    using System;
    using System.IO;
    using ClientFileTransferServiceReference;
    using System.Runtime.InteropServices;
    using System.Configuration;


    public class Program
    {
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("Kernel32")]
        private static extern IntPtr GetConsoleWindow();

        //const int SW_HIDE = 0;
        //const int SW_SHOW = 5;


        static void Main(string[] args)
        {
            try
            {
                //IntPtr hwnd;
                //hwnd = GetConsoleWindow();
                //ShowWindow(hwnd, SW_HIDE);

                var fileWatcher = new FileSystemWatcher(ConfigurationManager.AppSettings["FolderToWatch"]);
                fileWatcher.Created += FileWatcher_Created;
                fileWatcher.EnableRaisingEvents = true;

                Console.WriteLine("Watcher Started...");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                new Logger().Create("Service not started", DateTime.Now, "Error", ex.Message);
            }
            
            Console.ReadKey();
        }
        private static void FileWatcher_Created(Object sender, FileSystemEventArgs e)
        {
            FileTransferResponse response = null;
            try
            {
                if (!IsFileLocked(e.FullPath))
                {
                    var startAt = DateTime.Now;
                    var createdFile = new FileTransferRequest()
                    {
                        FileName = e.Name,
                        Content = File.ReadAllBytes(e.FullPath)
                    };

                    response = new FileTransferClient().Put(createdFile);

                    if (response.ResponseStatus != "Successful")
                    {
                        MoveToFailedFolder(e);
                    }
                    else
                    {
                        if (File.Exists(e.FullPath))
                        {
                            File.Delete(e.FullPath);
                        }
                    }

                    Console.WriteLine(response.ResponseStatus + " at: " + DateTime.Now.Subtract(startAt));
                    new Logger().Create(e.Name, DateTime.Now, response.ResponseStatus, response.Message);
                }
            }
            catch (CommunicationException ex)
            {
                MoveToFailedFolder(e);
                Console.WriteLine(ex.Message);
                new Logger().Create(e.Name, DateTime.Now, "Error", ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                if(response != null)
                {
                    new Logger().Create(e.Name, DateTime.Now, response.ResponseStatus, response.Message);
                }
                else
                {
                    new Logger().Create(e.Name, DateTime.Now, "Error", ex.Message);
                }
            }
        }
        private static void MoveToFailedFolder(FileSystemEventArgs e)
        {
            if (File.Exists(ConfigurationManager.AppSettings["FolderToWatch"] + "\\failed\\" + e.Name))
            {
                File.Delete(ConfigurationManager.AppSettings["FolderToWatch"] + "\\failed\\" + e.Name);
            }

            File.Move(e.FullPath, ConfigurationManager.AppSettings["FolderToWatch"] + "\\failed\\" + e.Name);
        }
        private static Boolean IsFileLocked(String filePath)
        {
            try
            {
                var numberOfTying = 1;
                while (numberOfTying <= 10)
                {
                    if (File.Exists(filePath))
                    {
                        FileStream stream = null;
                        try
                        {
                            Console.WriteLine("Try reading the: " + Path.GetFileName(filePath));
                            stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                            Console.WriteLine(" reading File succeeded");
                            return false;
                        }
                        catch
                        {
                            Console.WriteLine(" Wait for 1s ");
                            System.Threading.Thread.Sleep(1000);
                        }
                        finally
                        {
                            if (stream != null)
                            {
                                stream.Close();
                            }
                        }
                    }

                    numberOfTying++;
                }

                return true;
            }
            catch (Exception ex)
            {
                new Logger().Create(filePath, DateTime.Now, "Error", ex.Message);
                return true;
            }
        }
    }
}
