using System;
using SQLite.Net.Attributes;

namespace UmecaApp
{
	[Table("cat_periodicity")]
	public class Periodicity
	{
		public Periodicity ()
		{
		}

		[AutoIncrement,PrimaryKey]
		public int Id{get;set;}

		[Column("periodicity"), MaxLength(255)]
		public String Name{get;set;}

		[Column("specification")]
		public Boolean? Specification{get;set;}

		[Column("is_obsolete")]
		public Boolean? IsObsolete{get;set;}

	}
}

