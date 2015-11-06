using System;

 using SQLite;

namespace UmecaApp
{

	[Table("job")]
	public class Job
	{
		public Job ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id")]
		public int Id{ get; set; }

		[Column("id_web")]
		public long? webId{ get; set; }

		[Column("post"),MaxLength(50)]
		public String Post{ get; set; }

		[Column("name_head"),MaxLength(150)]
		public String NameHead{ get; set; }

		[Column("company"),MaxLength(150)]
		public String Company{ get; set; }

		[Column("phone"),MaxLength(20)]
		public String Phone{ get; set; }

		[Column("start_prev")]
		public DateTime? StartPrev{ get; set; }

		[Column("start")]
		public DateTime? Start{ get; set; }

		[Column("salary_week")]
		public String SalaryWeek{ get; set; }

		[Column("end")]
		public DateTime? End{ get; set; }

		[Column("reason_change"),MaxLength(1000)]
		public String ReasonChange{ get; set; }

		[Column("address"),MaxLength(1000)]
		public String Address{ get; set; }

		[Column("id_register_type")]
		public int? RegisterTypeId{ get; set; }

		[Column("id_meeting")]
		public int? MeetingId{ get; set; }

		[Column("blcok")]
		public Boolean? block{ get; set; }

		public String Schedule{ get; set; }
	}
}

