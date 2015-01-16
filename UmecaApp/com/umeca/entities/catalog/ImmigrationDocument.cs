using System;
using SQLite;
namespace UmecaApp
{
	[Table("cat_immigration_document")]
	public class ImmigrationDocument
	{
		public ImmigrationDocument ()
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

