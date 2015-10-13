using System;
 using SQLite;

namespace UmecaApp
{
	[Table("cat_activity")]
	public class ActivityCatalog
	{
		public ActivityCatalog ()
		{
		}

		[AutoIncrement,PrimaryKey]
		public int Id{get;set;}

		[Column("activity"), MaxLength(255)]
		public String Name{get;set;}

		[Column("specification")]
		public Boolean? Specification{get;set;}

		[Column("is_obsolete")]
		public Boolean? IsObsolete{get;set;}

	}
}

