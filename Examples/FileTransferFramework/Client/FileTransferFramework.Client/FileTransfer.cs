using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FileTransferFramework.Client
{
     [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Single)]
    public class FileTransfer : IFileTransfer
    {
        public FileTransferResponse Put(FileTransferRequest fileToPush)
        {
            var fileTransferResponse = CheckFileTransferRequest(fileToPush);
            if(fileTransferResponse.ResponseStatus == "FileIsValed")
            {
                try
                {
                    SaveFileStream(System.Configuration.ConfigurationManager.AppSettings["SavedLocation"] + "\\" + fileToPush.FileName, new MemoryStream(fileToPush.Content));
                    return new FileTransferResponse
                    {
                        CreateAt = DateTime.Now,
                        FileName = fileToPush.FileName,
                        Message = "File was transfered",
                        ResponseStatus = "Successful"
                    };
                }
                catch (Exception ex)
                {
                    return new FileTransferResponse
                    {
                        CreateAt = DateTime.Now,
                        FileName = fileToPush.FileName,
                        Message = ex.Message,
                        ResponseStatus = "Error"
                    };
                }
            }

            return fileTransferResponse;
        }
        private FileTransferResponse CheckFileTransferRequest(FileTransferRequest fileToPush)
        {
            if (fileToPush != null)
            {
                if (!string.IsNullOrEmpty(fileToPush.FileName))
                {
                    if (fileToPush.Content !=null)
                    {
                        return new FileTransferResponse
                        {
                            CreateAt = DateTime.Now,
                            FileName = fileToPush.FileName,
                            Message = string.Empty,
                            ResponseStatus = "FileIsValed"
                        };
                    }

                    return new FileTransferResponse
                    {
                        CreateAt = DateTime.Now,
                        FileName = "No Name",
                        Message = " File Content is null",
                        ResponseStatus = "Error"
                    };
                }

                return new FileTransferResponse
                {
                    CreateAt = DateTime.Now,
                    FileName = "No Name",
                    Message = " File Name Can't be Null",
                    ResponseStatus = "Error"
                };
            }

            return new FileTransferResponse
            {
                CreateAt = DateTime.Now,
                FileName = "No Name",
                Message = " File Can't be Null",
                ResponseStatus = "Error"
            };
        }
        private void SaveFileStream(String filePath, Stream stream)
        {
            try
            {
                if(File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                stream.CopyTo(fileStream);
                fileStream.Dispose();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
