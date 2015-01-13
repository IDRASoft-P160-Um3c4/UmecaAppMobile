using System;
using SQLite;
namespace UmecaApp
{
	[Table("social_environment")]
	public class SocialEnvironment
	{
		public SocialEnvironment ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id")]
		public long Id{ get; set; }

		[Column("physical_condition"),MaxLength(500)]
		public String physicalCondition{ get; set; }

		[Column("comment"),MaxLength(1000)]
		public String comment{ get; set; }

		[Column("id_meeting")]
		public long MeetingId{ get; set; }
	}
}

