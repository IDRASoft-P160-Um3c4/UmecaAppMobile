using System;

//listas
using System.Collections.Generic;

namespace UmecaApp
{
	public class PersonSocialNetworkVerificationDto
	{
		public PersonSocialNetworkVerificationDto ()
		{
		}

		public PersonSocialNetworkVerificationDto (PersonSocialNetwork nuevo)
		{
			this.Address = nuevo.Address;	
			this.Age = nuevo.Age;
			this.block = nuevo.block;
			this.DependentId = nuevo.DependentId;
			this.DocumentTypeId = nuevo.DocumentTypeId;
			this.Id = nuevo.Id;
			this.isAccompaniment = nuevo.isAccompaniment;
			this.LivingWithIde = nuevo.LivingWithIde;
			this.Name = nuevo.Name;
			this.Phone = nuevo.Phone;
			this.RelationshipId = nuevo.RelationshipId;
			this.SocialNetworkId = nuevo.SocialNetworkId;
			this.SpecificationDocumentType = nuevo.SpecificationDocumentType;
			this.specificationRelationship = nuevo.specificationRelationship;
		}


		public int? Id{ get; set; }


		public String Name{ get; set; }


		public int? RelationshipId{ get; set; }


		public String Age{ get; set; }


		public String Phone{ get; set; }


		public String Address{ get; set; }


		public int? DocumentTypeId{ get; set; }


		public String SpecificationDocumentType{ get; set; }


		public int? DependentId{ get; set; }


		public int? LivingWithIde{ get; set; }


		public int? SocialNetworkId{ get; set; }


		public Boolean? isAccompaniment{ get; set; }


		public String specificationRelationship{ get; set; }


		public Boolean? block{ get; set; }
	}
}

