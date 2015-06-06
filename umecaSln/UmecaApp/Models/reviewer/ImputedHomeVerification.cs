using System;

using SQLite.Net.Attributes;

//listas
using System.Collections.Generic;

namespace UmecaApp
{
	
	public class ImputedHomeVerification
	{
		public ImputedHomeVerification ()
		{
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

	}
}

