using System;

using SQLite.Net.Attributes;

namespace UmecaApp
{
	[Table("perosn_social_netwok")]
	public class PersonSocialNetwork
	{
		public PersonSocialNetwork ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id")]
		public int Id{ get; set; }

		[Column("id_web")]
		public long? webId{ get; set; }

		[Column("name"),MaxLength(150)]
		public String Name{ get; set; }

		[Column("id_relationship")]
		public int? RelationshipId{ get; set; }

		[Column("age")]
		public int? Age{ get; set; }

		[Column("phone"),MaxLength(200)]
		public String Phone{ get; set; }

		[Column("address"),MaxLength(500)]
		public String Address{ get; set; }

		[Column("id_document_type")]
		public int? DocumentTypeId{ get; set; }

		[Column("specification_document_type"),MaxLength(250)]
		public String SpecificationDocumentType{ get; set; }

		[Column("id_dependent")]
		public int? DependentId{ get; set; }

		[Column("id_living_with")]
		public int? LivingWithIde{ get; set; }

		[Column("id_social_network")]
		public int? SocialNetworkId{ get; set; }
	
		[Column("is_accompaniment")]
		public Boolean? isAccompaniment{ get; set; }

		[Column("specification_relationship"),MaxLength(255)]
		public String specificationRelationship{ get; set; }

		[Column("block")]
		public Boolean? block{ get; set; }

	}
}

