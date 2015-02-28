using System;
//listas
using System.Collections.Generic;

namespace UmecaApp
{
	public class MeetingDatosPersonalesDto
	{
		public MeetingDatosPersonalesDto ()
		{
		}
		public int caseId { get; set; }

		public String idFolder { get; set; }

		public int imputedId { get; set; }

		public String Name{ get; set; }

		public String LastNameP{ get; set; }

		public String LastNameM{ get; set; }

		public String FoneticString{ get; set; }

		public Boolean? Gender{ get; set; }

		public DateTime BirthDate{ get; set; }

		public int ageString{ get; set; }

		public String CelPhone{ get; set; }

		public int? YearsMaritalStatus{ get; set; }

		public int? MaritalStatusId { get; set; }

		public MaritalStatus MaritalStatus { get; set; }

		public int? Boys{ get; set; }

		public int? DependentBoys{ get; set; }

		public int? BirthCountry{ get; set; }

		public String BirthMunicipality{ get; set; }

		public String BirthState{ get; set; }

		public String BirthLocation{ get; set; }

		public String Nickname{ get; set; }

		public int? LocationId { get; set; }

		public Location Location { get; set; }

		public int? MeetingId { get; set; }

		public Meeting Meeting { get; set; }


		public int? ReviewerId{ get; set; }


		public User Reviewer { get; set; }


		public int? StatusMeetingId { get; set; }


		public StatusMeeting StatusMeeting { get; set; }


		public int MeetingType{ get; set; } 


		public String CommentReference{ get; set; }


		public String CommentJob{ get; set; }


		public String CommentSchool{ get; set; }


		public String CommentCountry{ get; set; }

//		[Column("comment_home"),MaxLength(500)]
		public String CommentHome{ get; set; }

//		[Column("comment_drug"),MaxLength(500)]
		public String CommentDrug{ get; set; }

//		[Column("date_create")]
		public DateTime? DateCreate{ get; set; }

//		[Column("date_terminate")]
		public DateTime? DateTerminate{ get; set; }

		public String JsonMeeting { get; set; }

		public String JsonCountrys { get; set; }

		public String JsonActivities { get; set; }
	}
}

