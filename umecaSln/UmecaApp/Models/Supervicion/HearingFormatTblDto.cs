using System;

namespace UmecaApp
{
	public class HearingFormatTblDto
	{
		public HearingFormatTblDto ()
		{
		}

		public int CaseId{ get; set; }

		public String IdFolder{ get; set; }

		public String IdMP{ get; set; }

		public String Name{ get; set; }

		public String LastNameP{ get; set; }

		public String LastNameM{ get; set; }

		public String Fullname{ get; set; }

		public DateTime? DateBirth{ get; set; }

		public bool? Gender{ get; set; }

		public int MeetingStatusId{ get; set; }

		public String Description{ get; set; }

		public String GenderString{ get; set; }

		public String DateBirthString{ get; set; }

		public Int64 ReviewerId{ get; set; }

		public String StatusCase{ get; set; }

		public String StatusCode{ get; set; }

		public String Action{ get; set; }
	}
}

