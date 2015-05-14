using System;

using SQLite.Net.Attributes;

namespace UmecaApp
{
	[Table("cat_status_meeting")]
	public class StatusMeeting
	{
		public StatusMeeting ()
		{
		}
		[PrimaryKey, AutoIncrement,Column("id_status")]
		public int Id{ get; set; }

		[Column("status"),MaxLength(255)]
		public String Status{ get; set;}

		[Column("description"),MaxLength(255)]
		public String Description{ get; set;}
	}

}