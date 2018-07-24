using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TaskOne.Models;

namespace TaskOne.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<SomeTestClass> _items;


        static HomeController()
        {
            _items = new List<SomeTestClass>
            {
                new SomeTestClass()
                {
                    Id = 1,
                    Title = "Title 1",
                    Data1 = "Data1 1",
                    Data2 = "Data2 1",
                    Data3 = "Data3 1",
                    Data4 = "Data4 1",
                    OtherTypeValue = new OtherType() {ID = 1, Title = "Ot1"}
                },
                new SomeTestClass()
                {
                    Id = 2,
                    Title = "Title 2",
                    Data1 = "Data1 2",
                    Data2 = "Data2 2",
                    Data3 = "Data3 2",
                    Data4 = "Data4 2",
                    OtherTypeValue = new OtherType() {ID = 2, Title = "Ot2"}
                },
                new SomeTestClass()
                {
                    Id = 3,
                    Title = "Title 3",
                    Data1 = "Data1 3",
                    Data2 = "Data2 3",
                    Data3 = "Data3 3",
                    Data4 = "Data4 3",
                    OtherTypeValue = new OtherType() {ID = 3, Title = "Ot3"}
                },
                new SomeTestClass()
                {
                    Id = 4,
                    Title = "Title 4",
                    Data1 = "Data1 4",
                    Data2 = "Data2 4",
                    Data3 = "Data3 4",
                    Data4 = "Data4 4",
                    OtherTypeValue = new OtherType() {ID = 4, Title = "Ot4"}
                },
                new SomeTestClass()
                {
                    Id = 5,
                    Title = "Title 5",
                    Data1 = "Data1 5",
                    Data2 = "Data2 5",
                    Data3 = "Data3 5",
                    Data4 = "Data4 5",
                    OtherTypeValue = new OtherType() {ID = 5, Title = "Ot5"}
                }
            };
        }


        public ActionResult Index()
        {
            return View(_items);
        }
        public ActionResult GetById(Int32 id)
        {
            return PartialView("_GetById", _items[id]);
        }
    }
}