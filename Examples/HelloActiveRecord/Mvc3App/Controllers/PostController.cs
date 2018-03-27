using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace Mvc3App.Controllers
{
    public class PostController : Controller
    {
        //
        // GET: /Post/

        public ActionResult Index(int blogId)
        {
            ViewBag.BlogId = blogId;
            Blog blog = Blog.Find(blogId);  
            return View(blog.Posts);
        }

        public ActionResult Details(int id, int blogId)
        {
            ViewBag.BlogId = blogId;
            return View(Post.Find(id));
        }

        public ActionResult Create(int blogId)
        {
            ViewBag.BlogId = blogId;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Post post, int blogId)
        {
            post.SaveAndFlush();
            return RedirectToAction("Index", new { blogId = blogId });
        }

        public ActionResult Delete(int id, int blogId)
        {
            ViewBag.BlogId = blogId;
            return View(Post.Find(id));
        }

        [HttpPost]
        public ActionResult Delete(Post post, int blogId)
        {
            post.DeleteAndFlush();
            return RedirectToAction("Index", new { blogId = blogId });
        }

        public ActionResult Edit(int id, int blogId)
        {
            ViewBag.BlogId = blogId;
            return View(Post.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Post post, int blogId)
        {
            post.SaveAndFlush();
            return RedirectToAction("Index", new { blogId = blogId });
        }

        
    }
}
