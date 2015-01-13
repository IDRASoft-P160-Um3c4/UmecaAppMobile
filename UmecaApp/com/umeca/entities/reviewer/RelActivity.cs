using System;
using SQLite;
namespace UmecaApp
{
	[Table("rel_activity")]
	public class RelActivity
	{
		public RelActivity ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id")]
		public long Id{ get; set; }

		[Column("activity_id")]
		public long ActivityId{ get; set; }

		[Column("social_environment_id")]
		public long SocialEnvironmentId{ get; set; }

		[Column("specification"),MaxLength(255)]
		public String specification{ get; set; }
	}
}

