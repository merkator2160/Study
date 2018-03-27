using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BindingToSQL.DB
{
    public class Book
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }

        public Book() { }
        public Book(string title, string description, string author)
        {
            Title = title;
            Description = description;
            Author = author;
        }
    }
}
