using System;

namespace UmecaApp
{
	public class NewMeetingDto
	{
		public NewMeetingDto ()
		{
		}

		public Int64 Id{ get; set; }

		public String IdFolder{ get; set; }

		public String Name{ get; set; }

		public String LastNameP{ get; set; }

		public String LastNameM{ get; set; }

		public String Fullname{ get; set; }

		public DateTime? DateBirth{ get; set; }

		public bool? Gender{ get; set; }

		public String Description{ get; set; }

		public String GenderString{ get; set; }

		public String DateBirthString{ get; set; }

		public Int64 ReviewerId{ get; set; }

		public String StatusCase{ get; set; }

		public String StatusCode{ get; set; }

		public String ResponseMessage{ get; set; }
	}
}

