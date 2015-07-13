using System;

namespace UmecaApp
{
	public class TabletFieldMeetingSourceDto
	{
		public TabletFieldMeetingSourceDto ()
		{
		}

		public TabletFieldMeetingSourceDto(int id, String value, String jsonValue, Boolean isFinal, int idFieldList, String reason,
			int? idSFV, String nameSFV, String descriptionSFV,
			int? idFV, String codeFV, String sectionFV,  int?  sectionCodeFV, String fieldNameFV,  int?  indexFieldFV, Boolean isObsoleteFV,  int?  idSubsectionFV, String typeFV) {
			this.id = id;
			this.value = value;
			this.jsonValue = jsonValue;
			this.isFinal = isFinal;
			this.idFieldList = idFieldList;
			this.reason = reason;

			if(idSFV!=null){
				this.statusFieldVerification=new TabletStatusFieldVerificationDto(idSFV,nameSFV,descriptionSFV);
			}

			if(idFV!=null){
				this.fieldVerification=new TabletFieldVerificationDto(idFV,codeFV,sectionFV,sectionCodeFV,fieldNameFV,indexFieldFV,isObsoleteFV,idSubsectionFV,typeFV);
			}
		}

		public int id{ get; set; }
		public String value{ get; set; }
		public String jsonValue{ get; set; }
		public Boolean isFinal{ get; set; }
		public int idFieldList{ get; set; }
		public String reason{ get; set; }
		public TabletStatusFieldVerificationDto statusFieldVerification{ get; set; }
		public TabletFieldVerificationDto fieldVerification{ get; set; }

	}
}

