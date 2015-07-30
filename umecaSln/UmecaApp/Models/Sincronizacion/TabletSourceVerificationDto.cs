using System;
using System.Collections.Generic;

namespace UmecaApp
{
	public class TabletSourceVerificationDto
	{
		public TabletSourceVerificationDto ()
		{
		}

		public TabletSourceVerificationDto( int?  id, String fullName,  int?  age, String address, String phone, Boolean? isAuthorized,DateTime?dateComplete,DateTime?dateAuthorized, String specification, Boolean? visible,
			int?  idVM, String nameVM, Boolean? isObsoleteVM,
			int?  idR, String nameR, Boolean? isObsoleteR, Boolean? specificationR) {

//			SimpleDateFormat sdf = new SimpleDateFormat("yy yy/MM/dd");
			this.webId = id;
			this.id = id;
			this.fullName = fullName;
			this.age = age;
			this.address = address;
			this.phone = phone;
			this.isAuthorized = isAuthorized;
			this.dateComplete = dateComplete == null ? null : String.Format("{0:yyyy/MM/dd}", dateComplete);
			this.dateAuthorized = dateAuthorized == null ? null : String.Format("{0:yyyy/MM/dd}", dateAuthorized);
			this.specification = specification;
			this.visible = visible;

			if (idVM != null) {
				this.verificationMethod = new TabletVerificationMethodDto(idVM, nameVM, isObsoleteVM);
			}

			if (idR != null) {
				this.relationship = new TabletRelationshipDto(idR, nameR, isObsoleteR, specificationR);
			}
		}

		public  long?  webId{ get; set; }
		public  int?  id{ get; set; }
		public String fullName{ get; set; }
		public  int?  age{ get; set; }
		public String address{ get; set; }
		public String phone{ get; set; }
		public Boolean? isAuthorized{ get; set; }
		public String dateComplete{ get; set; }
		public String dateAuthorized{ get; set; }
		public String specification{ get; set; }
		public Boolean? visible{ get; set; }
		public TabletVerificationMethodDto verificationMethod{ get; set; }
		public TabletRelationshipDto relationship{ get; set; }

		public List<TabletFieldMeetingSourceDto> fieldMeetingSourceList{ get; set; }

	}
}

