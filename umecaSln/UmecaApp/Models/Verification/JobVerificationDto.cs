using System;

//listas
using System.Collections.Generic;

namespace UmecaApp
{
	public class JobVerificationDto
	{
		public JobVerificationDto ()
		{
		}

		public JobVerificationDto (Job nuevo)
		{
			this.Address = nuevo.Address;
			this.block = nuevo.block;
			this.Company = nuevo.Company;
			this.End = nuevo.End;
			this.Id = nuevo.Id;
			this.MeetingId = nuevo.MeetingId;
			this.NameHead = nuevo.NameHead;
			this.Phone = nuevo.Phone;
			this.Post = nuevo.Post;
			this.ReasonChange = nuevo.ReasonChange;
			this.RegisterTypeId = nuevo.RegisterTypeId;
			this.SalaryWeek = nuevo.SalaryWeek;
			this.Start = nuevo.Start;
			this.StartPrev = nuevo.StartPrev;
		}

		public int? Id{ get; set; }

		public String Post{ get; set; }

		public String NameHead{ get; set; }

		public String Company{ get; set; }

		public String Phone{ get; set; }

		public DateTime? StartPrev{ get; set; }

		public DateTime? Start{ get; set; }

		public float? SalaryWeek{ get; set; }

		public DateTime? End{ get; set; }

		public String ReasonChange{ get; set; }

		public String Address{ get; set; }

		public int? RegisterTypeId{ get; set; }

		public int? MeetingId{ get; set; }

		public Boolean? block{ get; set; }

		public List<Schedule> ScheduleList{ get; set; }
	}
}

