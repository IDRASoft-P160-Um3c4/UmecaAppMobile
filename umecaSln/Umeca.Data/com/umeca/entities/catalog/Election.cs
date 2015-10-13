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
		[AutoIncrement,PrimaryKey, Column("id_election")]
		public int Id{get;set;}

		[Column("election"), MaxLength(255)]
		public String Name{get;set;}

	}
}

