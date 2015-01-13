using System;
using SQLite;

namespace UmecaApp
{
	[Table("social_network")]
	public class SocialNetwork
	{
		public SocialNetwork ()
		{
		}
		[PrimaryKey, AutoIncrement, Column("id")]
		public long Id{ get; set; }

		[Column("comment"),MaxLength(50)]
		public String Comment{ get; set; }

		[Column("meeting_id")]
		public long MeetingId{ get; set; }


	}
}

