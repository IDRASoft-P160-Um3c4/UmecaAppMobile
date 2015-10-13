using System;

 using SQLite;

namespace UmecaApp
{
	[Table("final_field")]
	public class FinalField
	{

		public FinalField ()
		{
		}


		[PrimaryKey, AutoIncrement, Column("id_final_field")]
		public int Id{ get; set; }

		[Column("reason"),MaxLength(500)]
		public String Reason{ get; set; }

		[Column("id_meeting")]
		public int? FieldMeetingSourceId{ get; set; }

		//[ManyToOne]
		public Meeting FieldMeetingSource { get; set; }

		[Column("id_verification")]
		public int? VerificationId{ get; set; }

		//[ManyToOne]
		public Verification Verification { get; set; }

		[Column("id_case")]
		public int? CaseDetentionId{ get; set; }
//
//		[OneToOne]
//		public Case CaseDetention { get; set; }
//
		[Column("id_meeting")]
		public int? MeetingId{ get; set; }
//
//		[OneToOne]
//		public Case Meeting { get; set; }


	}
}

