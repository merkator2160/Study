using System;
using System.Runtime.InteropServices;

namespace CodeForces.Units.Guids
{
    [Guid("a515cbb2-120f-40db-aef9-925b1a25f2ac")]
    public class GuidAttributeClass
    {
        public GuidAttributeClass()
        {
            var id = typeof(GuidAttributeClass).GUID.ToString();

            Console.WriteLine(id);
        }
    }
}