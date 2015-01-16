using System;
using SQLite;

namespace UmecaApp
{
	[Table("cat_perioficity")]
	public class Periodicity
	{
		public Periodicity ()
		{
		}

		[AutoIncrement,PrimaryKey]
		public int Id{get;set;}

		[Column("name"), MaxLength(255)]
		public String Name{get;set;}

		[Column("specification")]
		public Boolean Specification{get;set;}

		[Column("is_obsolete")]
		public Boolean IsObsolete{get;set;}

	}
}

