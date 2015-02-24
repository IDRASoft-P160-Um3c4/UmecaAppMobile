using System;
using SQLite.Net.Attributes;

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

		[Column("election"), MaxLength(255)]
		public String Name{get;set;}

	}
}

