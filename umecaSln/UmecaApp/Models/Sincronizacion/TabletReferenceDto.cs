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

		private int? webId{ get; set; }
		private int? id{ get; set; }
		private String fullName{ get; set; }
		private int? age{ get; set; }
		private String address{ get; set; }
		private String phone{ get; set; }
		private String specification{ get; set; }
		private Boolean? isAccompaniment{ get; set; }
		private String specificationRelationship{ get; set; }
		private Boolean? block{ get; set; }
		private TabletDocumentTypeDto documentType{ get; set; }
		private TabletRelationshipDto relationship{ get; set; }

	}
}

