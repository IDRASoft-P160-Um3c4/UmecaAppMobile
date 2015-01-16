using System;
using SQLite;

namespace UmecaApp
{
	[Table("cat_status_meeting")]
	public class StatusMeeting
	{
		public StatusMeeting ()
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

