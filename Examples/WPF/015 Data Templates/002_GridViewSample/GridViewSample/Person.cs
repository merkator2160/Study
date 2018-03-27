using System.Collections.Generic;

namespace GridViewSample
{
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }

        public Person(string firstName, string lastName, int age, string position)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Position = position;
        }
    }

    class PersonList : List<Person>
    {
        public PersonList()
        {
            this.Add(new Person("John", "Doe", 23, "Developer"));
            this.Add(new Person("Kent", "Elgas", 29, "Tester"));
            this.Add(new Person("Rea", "Ostrom", 31, "Team Lead"));
            this.Add(new Person("Lupe", "Campen", 42, "Project Manager"));
            this.Add(new Person("Alexander", "Heys", 35, "Developer"));
        }
    }
}
