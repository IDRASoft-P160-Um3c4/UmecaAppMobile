using System;

namespace UmecaApp
{
	public class TabletLeaveCountryDto
	{
		public TabletLeaveCountryDto ()
		{
		}

		public TabletLeaveCountryDto(int? id, String timeAgo, String reason, String state, String media, String address, String timeResidence, String specficationImmigranDoc, String specificationRelationship,
			int? idFAC, String nameFAC,
			int? idCF, String nameCF,
			int? idOD, String nameOD,
			int? idLC, String nameLC,
			int? idID, String nameID, Boolean? specificationID, Boolean? obsoleteID,
			int? idC, String nameC, String alpha2C, String alpha3C, int? latitudeC, int? longitudeC,
			int? idR, String nameR, Boolean? isObsoleteR, Boolean? specificationR) {
			this.id = id;
			this.webId = id;
			this.timeAgo = timeAgo;
			this.reason = reason;
			this.state = state;
			this.media = media;
			this.address = address;
			this.timeResidence = timeResidence;
			this.specficationImmigranDoc = specficationImmigranDoc;
			this.specificationRelationship = specificationRelationship;

			if(idFAC!=null){
				this.familyAnotherCountry = new TabletElectionDto(idFAC,nameFAC);
			}

			if(idCF!=null){
				this.communicationFamily= new TabletElectionDto(idCF,nameCF);
			}

			if(idOD!=null){
				this.officialDocumentation = new TabletElectionDto(idOD,nameOD);
			}

			if(idLC!=null){
				this.livedCountry = new TabletElectionDto(idLC,nameLC);
			}

			if(idID!=null){
				this.immigrationDocument = new TabletImmigrationDocumentDto(idID,nameID,specificationID,obsoleteID);
			}

			if(idC!=null) {
				this.country = new TabletCountryDto(idC,nameC,alpha2C,alpha3C,latitudeC,longitudeC);
			}

			if(idR!=null){
				this.relationship = new TabletRelationshipDto(idR,nameR,isObsoleteR,specificationR);
			}
		}

		public long? webId{ get; set; }
		public int? id{ get; set; }
		public String timeAgo{ get; set; }
		public String reason{ get; set; }
		public String state{ get; set; }
		public String media{ get; set; }
		public String address{ get; set; }
		public String timeResidence{ get; set; }
		public String specficationImmigranDoc{ get; set; }
		public String specificationRelationship{ get; set; }
		public TabletElectionDto familyAnotherCountry{ get; set; }
		public TabletElectionDto communicationFamily{ get; set; }
		public TabletElectionDto officialDocumentation{ get; set; }
		public TabletElectionDto livedCountry{ get; set; }
		public TabletImmigrationDocumentDto immigrationDocument{ get; set; }
		public TabletCountryDto country{ get; set; }
		public TabletRelationshipDto relationship{ get; set; }

	}
}

