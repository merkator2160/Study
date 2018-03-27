
namespace FileTransferFramework.Client
{
    using System.IO;
    using System.ServiceModel;

    [ServiceContract]
    public interface IFileTransfer
    {
        [OperationContract]
        FileTransferResponse Put(FileTransferRequest fileToPush);
    }
}
