using System;
using Castle.ActiveRecord;
using System.ComponentModel.DataAnnotations;

namespace Model
{
	[ActiveRecord]
	public class Post : ActiveRecordBase<Post>
	{
		private int id;
		private String title;
		private String contents;
		private String category;
		private DateTime created;
		private bool published;
		private Blog blog;

		public Post()
		{
			created = DateTime.Now;
		}

		public Post(Blog blog, String title, String contents, String category) : this()
		{
			this.blog = blog;
			this.title = title;
			this.contents = contents;
			this.category = category;
		}

		[PrimaryKey]
		public int Id
		{
			get { return id; }
			set { id = value; }
		}

        [Property]
        [Required]
		public String Title
		{
			get { return title; }
			set { title = value; }
		}

		[Property(ColumnType="StringClob")]
		public String Contents
		{
			get { return contents; }
			set { contents = value; }
		}

		[Property]
		public String Category
		{
			get { return category; }
			set { category = value; }
		}

		[BelongsTo("blogid")]
		public Blog Blog
		{
			get { return blog; }
			set { blog = value; }
		}

		[Property("created")]
		public DateTime Created
		{
			get { return created; }
			set { created = value; }
		}

		[Property("published")]
		public bool Published
		{
			get { return published; }
			set { published = value; }
		}
	}
}
