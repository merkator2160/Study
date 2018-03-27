using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RequestService.Common.Models;


namespace RequestService.Common.Interfaces
{
    public interface IDataProvider
    {
        void AddUser(String firstName, String lastName, Guid userId);
        void AddRequest(RequestType type, String text, DateTime creationDateTime, Guid userId);
    }
}
