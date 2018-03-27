using System;
using System.Linq;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using Model;

namespace BaseBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            ActiveRecordStarter.Initialize(ActiveRecordSectionHandler.Instance, typeof(Blog), typeof(Post));
            ActiveRecordStarter.CreateSchema();
            Blog b1 = new Blog("blog1"){Author="Author1"};
            Blog b2 = new Blog("blog2") { Author = "Author2" };
            b1.Save();
            b2.Save();
            var post = new Post();
            post.Title = "Title";
            post.Published = true;
            post.Contents = "contents";
            post.Blog = b1;
            post.SaveAndFlush();
            b1.SaveAndFlush();
            b2.SaveAndFlush();

            var blog = Blog.FindByName("blog1");            
            Console.WriteLine("Blogs created: {0}", Blog.FindAll().Count());
            Console.WriteLine("Posts created: {0}", Post.FindAll().Count());
            Console.WriteLine("Done.");

        }
    }
}
