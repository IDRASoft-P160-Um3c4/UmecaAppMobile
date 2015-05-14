using System;

using SQLite.Net.Attributes;
namespace UmecaApp
{
	[Table("rel_activity")]
	public class RelActivity
	{
		public RelActivity ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id")]
		public int Id{ get; set; }

		[Column("activity_id")]
		public int ActivityId{ get; set; }

		[Column("social_environment_id")]
		public int SocialEnvironmentId{ get; set; }

		[Column("specification"),MaxLength(255)]
		public String specification{ get; set; }
	}
}

