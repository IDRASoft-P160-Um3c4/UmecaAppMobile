using System;
using SQLite;
namespace UmecaApp
{
	[Table("cat_election")]
	public class Election
	{
		public Election ()
		{
		}
		[AutoIncrement,PrimaryKey]
		public int Id{get;set;}

		[Column("name"), MaxLength(255)]
		public String Name{get;set;}

	}
}

