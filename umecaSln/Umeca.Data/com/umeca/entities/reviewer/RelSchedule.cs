using System;

 using SQLite;

namespace UmecaApp
{
	[Table("rel_schedule")]
	public class RelSchedule
	{
		public RelSchedule ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id")]
		public int Id{ get; set; }

		[Column("imputed_home_id")]
		public int ImputedHomeId{ get; set; }

		[Column("job_id")]
		public int JobId{ get; set; }

		[Column("school_id")]
		public int SchoolId{ get; set; }

		[Column("schedule_id")]
		public int ScheduleId{ get; set; }
	}
}

