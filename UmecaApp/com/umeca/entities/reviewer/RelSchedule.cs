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
		public long Id{ get; set; }

		[Column("imputed_home_id")]
		public long ImputedHomeId{ get; set; }

		[Column("job_id")]
		public long JobId{ get; set; }

		[Column("school_id")]
		public long SchoolId{ get; set; }

		[Column("schedule_id")]
		public long ScheduleId{ get; set; }
	}
}

