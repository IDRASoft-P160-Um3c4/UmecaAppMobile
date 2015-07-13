using System;

namespace UmecaApp
{
	public class ScheduleDto
	{
		public ScheduleDto ()
		{
		}

		public ScheduleDto (Schedule e)
		{
			this.Day = e.Day;
			this.End = e.End;
			this.FramingActivityId = e.FramingActivityId;
			this.FramingAddressId = e.FramingAddressId;
			this.Id = e.Id;
			this.ImputedHomeId = e.ImputedHomeId;
			this.JobId = e.JobId;
			this.SchoolId = e.SchoolId;
			this.Start = e.Start;
		}
		public int Id{ get; set; }

		public String Day{ get; set; }

		public String Start{ get; set; }

		public String End{ get; set; }

		public int? ImputedHomeId{ get; set; }

		public int? JobId{ get; set; }

		public int? SchoolId{ get; set; }

		public int? FramingActivityId{ get; set; }

		public int? FramingAddressId{ get; set; }
	}
}

