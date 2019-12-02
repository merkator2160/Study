using DataBase;
using System;

namespace CodeForces.Units
{
    public static class EfNorthwindUnit
    {
        public static void Run()
        {
            using (var db = new DataContext())
            {
                var employees = db.Employees.AsNoTracking();
                foreach (var x in employees)
                {
                    Console.WriteLine(x.FirstName);
                }
            }
        }
    }
}