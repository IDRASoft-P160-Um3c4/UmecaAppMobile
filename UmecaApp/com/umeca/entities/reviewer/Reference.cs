using System;
using SQLite;

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
		private String FullName{ get; set; }

		[Column("age"),MaxLength(50)]
		private int Age{ get; set; }

		[Column("id_relationship")]
		private int RelationshipId{ get; set; }

		[Column("address"),MaxLength(250)]
		private String Address{ get; set; }

		[Column("phone"),MaxLength(200)]
		private String Phone{ get; set; }

		[Column("specification_document_type"),MaxLength(250)]
		private String SpecificationDocumentType{ get; set; }

		[Column("id_document_type")]
		private int DocumentTypeId{ get; set; }

		[Column("meeting_id")]
		private int MeetingId{ get; set; }

		[Column("is_accompaniment")]
		private Boolean IsAccompaniment{ get; set; }

		[Column("specification_relationship"),MaxLength(255)]
		private String SpecificationRelationship{ get; set; }

		[Column("block")]
		private Boolean Block{ get; set; }

	}
}

