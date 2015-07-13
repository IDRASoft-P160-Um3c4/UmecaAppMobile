using System;

namespace UmecaApp
{
	public class TabletPersonSocialNetworkDto
	{
		public TabletPersonSocialNetworkDto ()
		{
		}

		public TabletPersonSocialNetworkDto(int id, String name, int age, String phone, String address, String specification, Boolean? isAccompaniment, String specificationRelationship, Boolean? block) {
			this.id = id;
			this.webId = id;
			this.name = name;
			this.age = age;
			this.phone = phone;
			this.address = address;
			this.specification = specification;
			this.isAccompaniment = isAccompaniment;
			this.specificationRelationship = specificationRelationship;
			this.block = block;
		}

		public int webId{ get; set; }
		public int id{ get; set; }
		public String name{ get; set; }
		public int age{ get; set; }
		public String phone{ get; set; }
		public String address{ get; set; }
		public String specification{ get; set; }
		public Boolean? isAccompaniment{ get; set; }
		public String specificationRelationship{ get; set; }
		public Boolean? block{ get; set; }

		public TabletRelationshipDto relationship{ get; set; }
		public TabletDocumentTypeDto documentType{ get; set; }
		public TabletElectionDto dependent{ get; set; }
		public TabletElectionDto livingWith{ get; set; }

	}
}

