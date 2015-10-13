using System;
 using SQLite;

namespace UmecaApp
{
	[Table("cat_home_type")]
	public class HomeType
	{
		public HomeType ()
		{
		}

		[AutoIncrement,PrimaryKey]
		public int Id{get;set;}

		[Column("home_type"), MaxLength(255)]
		public String Name{get;set;}

		[Column("specification")]
		public Boolean? Specification{get;set;}

		[Column("obsolete")]
		public Boolean? IsObsolete{get;set;}

	}
}

