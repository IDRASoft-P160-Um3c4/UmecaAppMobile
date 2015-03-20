using System;

using SQLite.Net.Attributes;
namespace UmecaApp
{
	[Table("social_environment")]
	public class SocialEnvironment
	{
		public SocialEnvironment ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id")]
		public int Id{ get; set; }

		[Column("physical_condition"),MaxLength(500)]
		public String physicalCondition{ get; set; }

		[Column("comment"),MaxLength(1000)]
		public String comment{ get; set; }

		[Column("id_meeting")]
		public int MeetingId{ get; set; }
	}
}