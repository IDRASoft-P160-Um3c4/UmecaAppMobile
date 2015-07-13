using System;

//listas
using System.Collections.Generic;

namespace UmecaApp
{
	public class DomiciliosVerificationDto
	{
		public DomiciliosVerificationDto ()
		{
		}

		public DomiciliosVerificationDto (ImputedHome nuevo)
		{
			this.AddressId = nuevo.AddressId;	
			this.addressString = nuevo.addressString;
			this.Description = nuevo.Description;
			this.HomeTypeId = nuevo.HomeTypeId;
			this.Id = nuevo.Id;
			this.InnNum = nuevo.InnNum;
			this.Lat = nuevo.Lat;
			this.Lng = nuevo.Lng;
			this.LocationId = nuevo.LocationId;
			this.MeetingId = nuevo.MeetingId;
			this.OutNum = nuevo.OutNum;
			this.Phone = nuevo.Phone;
			this.ReasonChange = nuevo.ReasonChange;
			this.ReasonSecondary = nuevo.ReasonSecondary;
			this.RegisterTypeId = nuevo.RegisterTypeId;
			this.Specification = nuevo.Specification;
			this.Street = nuevo.Street;
			this.TimeLive = nuevo.TimeLive;
		}

		public int Id{ get; set; }

		public int? AddressId{ get; set; }
	
		public String Street{ get; set; }

		public String OutNum{ get; set; }

		public String InnNum{ get; set; }

		public int? LocationId{ get; set; }

		public String Lat{ get; set; }

		public String Lng{ get; set; }

		public String addressString{ get; set; }

		public int? RegisterTypeId{ get; set; }

		public String TimeLive{ get; set; }

		public int? HomeTypeId{ get; set; }

		public String ReasonChange{ get; set; }

		public String Description{ get; set; }

		public String Phone{ get; set; }

		public int? MeetingId{ get; set; }

		public String Specification{ get; set; }

		public String ReasonSecondary{ get; set; }

		public List<Schedule> ScheduleList{ get; set; }

		public String Schedule{ get; set; }
	}
}

