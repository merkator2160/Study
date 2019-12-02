using System;
using System.Threading.Tasks;

namespace CodeForces.Units.Interview.Models
{
    public class TryCatchTask
    {
        public async Task DoTestAsync()
        {
            throw new ArgumentOutOfRangeException();
        }
    }
}