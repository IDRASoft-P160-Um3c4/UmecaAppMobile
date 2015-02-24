using System;

using SQLite.Net.Attributes;

namespace UmecaApp
{
	[Table("cat_status_case")]
	public class StatusCase
	{
	
		public StatusCase ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int Id{ get; set; }

		[Column("name"),MaxLength(255)]
		public String Name{ get; set;}

		[Column("description"),MaxLength(255)]
		public String Description{ get; set;}
	}
}

