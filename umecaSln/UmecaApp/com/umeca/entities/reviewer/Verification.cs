using System;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;

namespace UmecaApp
{
	[Table("verification")]
	public class Verification
	{

		public Verification ()
		{
		}


		[PrimaryKey, AutoIncrement, Column("id_verification")]
		public int Id{ get; set; }

		[ForeignKey(typeof(User)),Column("id_reviewer")]
		public int? ReviewerId{ get; set; }

		[ManyToOne]
		public User Reviewer { get; set; }

		[ForeignKey(typeof(Case)),Column("id_case")]
		public int? CaseDetentionId{ get; set; }

		[OneToOne]
		public Case CaseDetention { get; set; }

		[ForeignKey(typeof(StatusVerification)),Column("id_status")]
		public int StatusVerificationId { get; set; }

		[ManyToOne]
		public StatusVerification StatusVerification { get; set; }

		[Column("date_complete")]
		public DateTime? DateComplete{ get; set; }

		//meeting verified
		[ForeignKey(typeof(Meeting)),Column("id_meeting")]
		public int? MeetingId{ get; set; }

		[OneToOne]
		public Meeting Meeting { get; set; }

		[Column("date_create")]
		public DateTime? DateCreate{ get; set; }

		//tiene una lista de Source verification(fuentes de verificacion) asociados al id List<SourceVerification>
		//tiene una lista de FinalField supongo que es ya para el termino de supervicion quiero creer jaja XD List<FinalField>

	}
}

