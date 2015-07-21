using System;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;

namespace UmecaApp
{
	[Table("source_verification")]
	public class SourceVerification
	{

		public SourceVerification ()
		{
		}

		public SourceVerification(int id,String fullName, int age, String relationshipString, String address, String phone, Boolean isAuthorized, DateTime? dateComplete, int idCase, Boolean visible) {
			this.Id =  id;
			this.FullName = fullName;
			this.Age = age;
			this.RelationshipString = relationshipString;
			this.Address = address;
			this.Phone = phone;
			this.IsAuthorized = isAuthorized;
			this.DateComplete = dateComplete;
			if(dateComplete!=null){
				this.StatusString ="Entrevista de verificaci&oacute;n terminada";
			}else{
				this.StatusString = "Entrevista de verificaci&oacute;n incompleta";
			}
			this.IdCase = idCase;
			this.Visible = visible;
		}

		public SourceVerification(int id,String fullName, String relationshipString, String phone, String address, int idVerificationMethod, Boolean isAuthorized, int idCase) {
			this.Id =  id;
			this.FullName = fullName;
			this.RelationshipString = relationshipString;
			this.Address = address;
			this.Phone = phone;
			this.VerificationMethodId = idVerificationMethod;
			this.IsAuthorized = isAuthorized;
			this.IdCase = idCase;
		}

		[PrimaryKey, AutoIncrement, Column("id_source_verification")]
		public int Id{ get; set; }

		[Column("id_web")]
		public long? webId{ get; set; }

		[Column("name"),MaxLength(150)]
		public String FullName{ get; set; }

		[Column("age")]
		public int Age{ get; set; }

		[Column("id_relationship")]
		public int RelationshipId{ get; set; }

		public String RelationshipString{ get; set; }

		[Column("address"),MaxLength(250)]
		public String Address{ get; set; }

		[Column("phone"),MaxLength(200)]
		public String Phone{ get; set; }

		[Column("isAuthorized")]
		public Boolean IsAuthorized{ get; set; }

		[Column("date_complete")]
		public DateTime? DateComplete{ get; set; }

		public String StatusString{ get; set; }

		[Column("dateAuthorized")]
		public DateTime? DateAuthorized{ get; set; }

		[Column("specification"),MaxLength(250)]
		public String Specification{ get; set; }

		public int IdCase{ get; set; }

		[Column("id_verification_method")]
		public int? VerificationMethodId{ get; set; }

		[Column("id_request")]
		public int? CaseRequestId{ get; set; }

		[Column("visible")]
		public Boolean Visible{ get; set; }

		[Column("id_verification")]
		public int? VerificationId{ get; set; }

		//tiene un List<FieldMeetingSource>

	}
}

