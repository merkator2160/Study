using System;

namespace CodeForces.Units.Interview.Models
{
    public class Task3Class
    {
        public class Task3A
        {
            public virtual void Method()
            {
                Console.WriteLine("Method of class A");
            }
        }
        public class Task3B : Task3A
        {
            public override void Method()
            {
                Console.WriteLine("Method of class B");
            }
        }

        public static void TestMethod(Task3A input)
        {
            input.Method();
        }
    }
}