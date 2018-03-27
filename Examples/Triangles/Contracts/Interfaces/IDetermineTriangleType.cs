using Contracts.Models;

namespace Contracts.Interfaces
{
    public interface IDetermineTriangleType
    {
        void DetermineType(ref Triangle triangle);
    }
}