using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreeViewSample
{
    class Category
    {
        public string Name { get; set; }
        public List<Traffic> trafficList { get; set; }
    }

    class Traffic
    {
        public string TrafficDescription { get; set; }
        public Traffic(string trafic)
        {
            TrafficDescription = trafic;
        }
    }

    class CategoryCreator
    {
        static public List<Category> GetCreatorList()
        {
            Category cA = new Category();
            cA.Name = "категория A";
            cA.trafficList = new List<Traffic>() 
            { 
                new Traffic("мотоцикл"),
                new Traffic("мотороллер"),
                new Traffic("другие мототранспортные средства") 
            };

            Category cB = new Category();
            cB.Name = "категория B";
            cB.trafficList = new List<Traffic>() 
            { 
                new Traffic("автомобили не тяжелей 3.5т и меньше 8 мест") 
            };

            Category cC = new Category();
            cC.Name = "категория С";
            cC.trafficList = new List<Traffic>() 
            { 
                new Traffic("автомобили тяжелей 3.5т") 
            };

            Category cD = new Category();
            cD.Name = "категория D";
            cD.trafficList = new List<Traffic>()
            { 
                new Traffic("автомобили для перевозки пассажиров")
            };

            Category cE = new Category();
            cE.Name = "категория E";
            cE.trafficList = new List<Traffic>() 
            { 
                new Traffic("составы транспортных средств с тягачом")
            };

            return new List<Category>() { cA, cB, cC, cD, cE };
        }
    }
}
