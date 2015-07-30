using System;

namespace UmecaApp
{
	public class TabletReferenceDto
	{
		public TabletReferenceDto ()
		{
		}

		public TabletReferenceDto(int? id, String fullName, int? age, String address, String phone, String specification, Boolean? isAccompaniment, String specificationRelationship, Boolean? block,
			int? idDT, String nameDT, Boolean? isObsoleteDT, Boolean? specificationDT,
			int? idR, String nameR, Boolean? isObsoleteR, Boolean? specificationR) {
			this.id = id;
			this.webId = id;
			this.fullName = fullName;
			this.age = age;
			this.address = address;
			this.phone = phone;
			this.specification = specification;
			this.isAccompaniment = isAccompaniment;
			this.specificationRelationship = specificationRelationship;
			this.block = block;

			if (idDT != null) {
				this.documentType = new TabletDocumentTypeDto(idDT, nameDT, isObsoleteDT, specificationDT);
			}

			if (idR != null) {
				this.relationship = new TabletRelationshipDto(idR, nameR, isObsoleteR, specificationR);
			}
		}

		public long? webId{ get; set; }
		public int? id{ get; set; }
		public String fullName{ get; set; }
		public int? age{ get; set; }
		public String address{ get; set; }
		public String phone{ get; set; }
		public String specification{ get; set; }
		public Boolean? isAccompaniment{ get; set; }
		public String specificationRelationship{ get; set; }
		public Boolean? block{ get; set; }
		public TabletDocumentTypeDto documentType{ get; set; }
		public TabletRelationshipDto relationship{ get; set; }

	}
}

