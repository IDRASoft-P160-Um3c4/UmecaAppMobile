﻿using System;

 using SQLite;

namespace UmecaApp
{
	[Table("schedule")]
	public class Schedule
	{
		public Schedule ()
		{
			this.FramingActivityId = null;
			this.FramingAddressId = null;
			this.ImputedHomeId = null;
			this.ImputedHomeId = null;
			this.JobId = null;
			this.SchoolId = null;
			this.webId = null;
		}

		[PrimaryKey, AutoIncrement, Column("id_schedule")]
		public int Id{ get; set; }

		[Column("id_web")]
		public long? webId{ get; set; }

		[Column("day"),MaxLength(50)]
		public String Day{ get; set; }

		[Column("start"),MaxLength(5)]
		public String Start{ get; set; }

		[Column("end"),MaxLength(5)]
		public String End{ get; set; }

		[Column("id_imputed_home")]
		public int? ImputedHomeId{ get; set; }

		[Column("id_job")]
		public int? JobId{ get; set; }

		[Column("id_school")]
		public int? SchoolId{ get; set; }

		[Column("id_framing_activity")]
		public int? FramingActivityId{ get; set; }

		[Column("id_framing_address")]
		public int? FramingAddressId{ get; set; }
	}
}

