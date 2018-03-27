using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RequestService.Common.Models;

namespace RequestService.Common.Interfaces
{
    public interface ICreateRequestService
    {
        string LastErrorText { get; }
        bool Create(RequestType requestType, int userId);
    }
}
