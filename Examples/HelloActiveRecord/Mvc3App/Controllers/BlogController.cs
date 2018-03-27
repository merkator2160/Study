using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Framework.Config;
using Model;

namespace Mvc3App.Controllers
{
    public class BlogController : Controller
    {
        public ActionResult Index()
        {            
            return View(Blog.FindAll());
        }

        public ActionResult Edit(int id)
        {
            return View(Blog.Find(id)); 
        }

        [HttpPost]
        public ActionResult Edit(Blog blog)
        {            
            blog.SaveAndFlush();            
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            return View(Blog.Find(id));
        }

        public ActionResult Delete(int id)
        {
            return View(Blog.Find(id)); 
        }

        [HttpPost]
        public ActionResult Delete(Blog blog)
        {
            blog.DeleteAndFlush();
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Blog blog)
        {
            blog.SaveAndFlush();
            return RedirectToAction("Index");
        }
        
        
    }
}
