using System;
using System.Collections.Generic;

namespace UmecaApp
{
	public class TabletMeetingDto
	{
		public TabletMeetingDto ()
		{
		}

		public TabletMeetingDto(int id, int meetingType, String commentReference, String commentJob, String commentSchool, String commentCountry, String commentHome, String commentDrug, DateTime? dateCreate, DateTime? dateTerminate) {

			this.id = id;
			this.webId = id;
			this.meetingType = meetingType;
			this.commentReference = commentReference;
			this.commentJob = commentJob;
			this.commentSchool = commentSchool;
			this.commentCountry = commentCountry;
			this.commentHome = commentHome;
			this.commentDrug = commentDrug;
			this.dateCreate = dateCreate == null ? null : String.Format("{0:yyyy/MM/dd}", dateCreate);
			this.dateTerminate = dateTerminate == null ? null : String.Format("{0:yyyy/MM/dd}", dateTerminate);
		}

		public Meeting MeetingToObject(){
			Meeting me = new Meeting ();
			me.CommentCountry = this.commentCountry;
			me.CommentDrug = this.commentDrug;
			me.CommentHome = this.commentHome;
			me.CommentJob = this.commentJob;
			me.CommentReference = this.commentReference;
			me.CommentSchool = this.commentSchool;
			if(this.dateCreate!=null){
			me.DateCreate =  DateTime.ParseExact(this.dateCreate, "yyyy/MM/dd",
				System.Globalization.CultureInfo.InvariantCulture);
			}
			if(this.dateTerminate!=null){
				me.DateTerminate = DateTime.ParseExact(this.dateTerminate, "yyyy/MM/dd",
					System.Globalization.CultureInfo.InvariantCulture);
			}
			if (this.meetingType != null) {
				me.MeetingType = this.meetingType ?? 0;
			}
			me.StatusMeetingId = this.status.id;
			me.WebId = this.webId;
			return me;
		}


		public long? webId{ get; set; }
		public int? id{ get; set; }
		public int? meetingType{ get; set; }
		public String commentReference{ get; set; }
		public String commentJob{ get; set; }
		public String commentSchool{ get; set; }
		public String commentCountry{ get; set; }
		public String commentHome{ get; set; }
		public String commentDrug{ get; set; }
		public String dateCreate{ get; set; }
		public String dateTerminate{ get; set; }
		public TabletStatusMeetingDto status{ get; set; }
		public TabletUserDto reviewer{ get; set; }
		public TabletImputedDto imputed{ get; set; }
		public TabletSocialNetworkDto socialNetwork{ get; set; }
		public TabletSchoolDto school{ get; set; }
		public TabletSocialEnvironmentDto socialEnvironment{ get; set; }
		public TabletLeaveCountryDto leaveCountry{ get; set; }
		public List<TabletReferenceDto> references{ get; set; }
		public List<TabletImputedHomeDto> imputedHomes{ get; set; }
		public List<TabletJobDto> jobs{ get; set; }
		public List<TabletDrugDto> drugs{ get; set; }
	}
}

