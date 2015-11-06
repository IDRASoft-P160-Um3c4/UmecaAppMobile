using System;
using System.Collections.Generic;

namespace UmecaApp
{
	public class TabletImputedHomeDto
	{
		public TabletImputedHomeDto ()
		{
		}

		public TabletImputedHomeDto( int?  id, String timeLive, String reasonChange, String description, String phone, String specification, String reasonSecondary,
			int?  idA, String streetA, String outNumA, String innNumA, String latA, String lngA, String addressStringA,
			int?  idL, String nameL, String abbreviationL, String descriptionL, String zipCodeL,
			int?  idHT, String nameHT, Boolean specificationHT, Boolean obsoleteHT,
			int?  idRT, String nameRT) {
			this.id = id;
			this.webId = id;
			this.timeLive = timeLive;
			this.reasonChange = reasonChange;
			this.description = description;
			this.phone = phone;
			this.specification = specification;
			this.reasonSecondary = reasonSecondary;

			if (idA != null) {
				this.address = new TabletAddressDto(idA, streetA, outNumA, innNumA, latA, lngA, addressStringA, idL, nameL, abbreviationL, descriptionL, zipCodeL);
			}

			if (idHT != null) {
				this.homeType = new TabletHomeTypeDto(idHT, nameHT, specificationHT, obsoleteHT);
			}

			if (idRT != null) {
				this.registerType = new TabletRegisterTypeDto(idRT, nameRT);
			}
		}

		public  long?  webId{ get; set; }
		public  int?  id{ get; set; }
		public String timeLive{ get; set; }
		public String reasonChange{ get; set; }
		public String description{ get; set; }
		public String phone{ get; set; }
		public String specification{ get; set; }
		public String reasonSecondary{ get; set; }
		public TabletAddressDto address{ get; set; }
		public TabletHomeTypeDto homeType{ get; set; }
		public TabletRegisterTypeDto registerType{ get; set; }
		public List<TabletScheduleDto> schedule{ get; set; }

		public Boolean? isHomeless{ get; set; }

	}
}

