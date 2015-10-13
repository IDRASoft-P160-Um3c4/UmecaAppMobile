using System;

 using SQLite;

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

		[Column("id_reviewer")]
		public int? ReviewerId{ get; set; }

		//[ManyToOne]
//		public User Reviewer { get; set; }

		[Column("id_case")]
		public int? CaseDetentionId{ get; set; }

		//[OneToOne]
//		public Case CaseDetention { get; set; }

		[Column("id_status_verification")]
		public int StatusVerificationId { get; set; }

		//[ManyToOne]
//		public StatusVerification StatusVerification { get; set; }

		[Column("date_complete")]
		public DateTime? DateComplete{ get; set; }

		[Column("date_create")]
		public DateTime? DateCreate{ get; set; }

		//tiene una lista de Source verification(fuentes de verificacion) asociados al id List<SourceVerification>
		//tiene una lista de FinalField supongo que es ya para el termino de supervicion quiero creer jaja XD List<FinalField>

	}
}

