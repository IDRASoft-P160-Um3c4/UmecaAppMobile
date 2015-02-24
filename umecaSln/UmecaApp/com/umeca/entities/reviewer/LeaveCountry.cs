using System;

using SQLite.Net.Attributes;

namespace UmecaApp
{
	[Table("leave_country")]
	public class LeaveCountry
	{
		public LeaveCountry ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id")]
		public int Id{ get; set; }

		[Column("official_documentation_id")]
		public int OfficialDocumentationId{ get; set; }

		[Column("lived_countr_id")]
		public int LivedCountryId{ get; set; }

		[Column("time_ago"), MaxLength(250)]
		public String timeAgo{ get; set; }

		[Column("reason"), MaxLength(500)]
		public String reason{ get; set; }

		[Column("id_family_another_country")]
		public int FamilyAnotherCountryId{ get; set; }


		[Column("id_communication_family")]
		public int CommunicationFamilyId{ get; set; }

		[Column("id_country")]
		public int CountryId{ get; set; }

		[Column("state"), MaxLength(100)]
		public String State{ get; set; }

		[Column("media"), MaxLength(100)]
		public String Media{ get; set; }


		[Column("address"), MaxLength(500)]
		public String Address{ get; set; }


		[Column("id_immigration_document")]
		public int ImmigrationDocumentId{ get; set; }

		[Column("id_relationship")]
		public int RelationshipId{ get; set; }


		[Column("time_residence"), MaxLength(50)]
		public String TimeResidence{ get; set; }

		[Column("specification_immigrant_docountr_id"), MaxLength(255)]
		public String SpecficationImmigranDoc{ get; set; }

		[Column("specification_relationship"), MaxLength(255)]
		public String specificationRelationship{ get; set; }

		[Column("id_meeting")]
		public int MeetingId{ get; set; }
	}
}

