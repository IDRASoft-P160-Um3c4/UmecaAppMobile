using System;
using SQLite.Net.Attributes;

namespace UmecaApp
{
	[Table("cat_marita_status")]
	public class MaritalStatus
	{
		public MaritalStatus ()
		{
		}

		[AutoIncrement,PrimaryKey]
		public int Id{get;set;}

		[Column("name"), MaxLength(255)]
		public String Name{get;set;}

	}
}

