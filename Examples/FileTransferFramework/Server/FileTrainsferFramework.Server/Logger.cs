using GeniusesCode.Framework.Data.SQLServer;
using System;


namespace FileTrainsferFramework.Server
{
    public class Logger
    {
        public bool Create(string fileName, DateTime createDate, string requestStatus, string message)
        {
            return new Operations().ExecuteNoneQuery(
                "Logs_Create",
                Operations.CommandType.StoredProcedure,
                new Parameter("FileName", fileName),
                new Parameter("CreateDate", createDate),
                new Parameter("RequestStatus", requestStatus),                
                new Parameter("Message", message));
        }
    }
}
