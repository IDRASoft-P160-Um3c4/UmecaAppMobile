using System;

using SQLite.Net.Attributes;

namespace UmecaApp
{
	[Table("reference")]
	public class Reference
	{
		public Reference ()
		{
		}


		[PrimaryKey, AutoIncrement, Column("id")]
		public int Id{ get; set; }

		[Column("name"),MaxLength(150)]
		public String FullName{ get; set; }

		[Column("age"),MaxLength(50)]
		public int Age{ get; set; }

		[Column("id_relationship")]
		public int RelationshipId{ get; set; }

		[Column("address"),MaxLength(250)]
		public String Address{ get; set; }

		[Column("phone"),MaxLength(200)]
		public String Phone{ get; set; }

		[Column("specification_document_type"),MaxLength(250)]
		public String SpecificationDocumentType{ get; set; }

		[Column("id_document_type")]
		public int DocumentTypeId{ get; set; }

		[Column("meeting_id")]
		public int MeetingId{ get; set; }

		[Column("is_accompaniment")]
		public Boolean IsAccompaniment{ get; set; }

		[Column("specification_relationship"),MaxLength(255)]
		public String SpecificationRelationship{ get; set; }

		[Column("block")]
		public Boolean block{ get; set; }

	}
}

