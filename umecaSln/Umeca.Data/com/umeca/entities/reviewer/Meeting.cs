using System;

 using SQLite;

namespace UmecaApp
{
	[Table("meeting")]
	public class Meeting
	{

		public Meeting ()
		{
		}
		[PrimaryKey, AutoIncrement, Column("id_meeting")]
		public int Id{ get; set; }

		[Column("id_web")]
		public long? WebId{ get; set; }

		[Column("id_reviewer")]
		public int? ReviewerId{ get; set; }

		//[ManyToOne]
		//public User Reviewer { get; set; }

		[Column("id_case")]
		public int? CaseDetentionId{ get; set; }

		//[OneToOne]
//		public Case CaseDetention { get; set; }


		//[OneToOne]
//		public Imputed Imputed { get; set; }


		[Column("id_status")]
		public int? StatusMeetingId { get; set; }

		//[ManyToOne]
//		public StatusMeeting StatusMeeting { get; set; }

		[Column("meeting_type")]
		public int MeetingType{ get; set; } 

		[Column("comment_refernce"),MaxLength(500)]
		public String CommentReference{ get; set; }

		[Column("comment_job"),MaxLength(500)]
		public String CommentJob{ get; set; }

		[Column("comment_school"),MaxLength(500)]
		public String CommentSchool{ get; set; }
			
		[Column("comment_country"),MaxLength(500)]
		public String CommentCountry{ get; set; }

		[Column("comment_home"),MaxLength(500)]
		public String CommentHome{ get; set; }

		[Column("comment_drug"),MaxLength(500)]
		public String CommentDrug{ get; set; }

		[Column("date_create")]
		public DateTime? DateCreate{ get; set; }

		[Column("date_terminate")]
		public DateTime? DateTerminate{ get; set; }
	}
}

