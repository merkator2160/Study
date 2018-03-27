using System;
using Castle.ActiveRecord;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NHibernate.Criterion;

namespace Model
{
	[ActiveRecord]
	public class Blog : ActiveRecordBase<Blog>
	{
		private int id;
		private String name;
		private String author;
        private IList<Post> posts = new List<Post>();

		public Blog()
		{
		}

		public Blog(String name)
		{
			this.name = name;
		}

		[PrimaryKey]
		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		[Property]
        [Required]
		public String Name
		{
			get { return name; }
			set { name = value; }
		}

		[Property]
        [Required]
		public String Author
		{
			get { return author; }
			set { author = value; }
		}

		[HasMany(typeof(Post), 
			Table="Posts", ColumnKey="blogid", 
			Inverse=true, Cascade=ManyRelationCascadeEnum.AllDeleteOrphan)]
		public IList<Post> Posts
		{
			get { return posts; }
			set { posts = value; }
		}

		public static Blog Find(int id)
		{
			return (Blog)FindByPrimaryKey(typeof(Blog), id);
		}

        public static Blog FindByName(string name)
        {

            return FindFirst((typeof (Blog)), Expression.Eq("Name", name)) as Blog;
        }
	}
}