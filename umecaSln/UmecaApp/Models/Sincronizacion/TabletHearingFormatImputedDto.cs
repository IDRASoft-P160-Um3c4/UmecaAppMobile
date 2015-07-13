using System;

namespace UmecaApp
{
	public class TabletHearingFormatImputedDto
	{
		public TabletHearingFormatImputedDto ()
		{
		}

		public TabletHearingFormatImputedDto(int? id, String name, String lastNameP, String lastNameM, DateTime? birthDate, String imputeTel,
			int? idA, String streetA, String outNumA, String innNumA, String latA, String lngA, String addressStringA,
			int? idL, String nameL, String abbreviationL, String descriptionL, String zipCodeL) {

//			SimpleDateFormat sdf = new SimpleDateFormat("yyyy/MM/dd");
			this.id = id;
			this.name = name;
			this.lastNameP = lastNameP;
			this.lastNameM = lastNameM;
			this.birthDate = birthDate == null ? null : String.Format("{0:yyyy/MM/dd}", birthDate);
			this.imputeTel = imputeTel;
			if (idA != null) {
				this.address = new TabletAddressDto(idA, streetA, outNumA, innNumA, latA, lngA, addressStringA, idL, nameL, abbreviationL, descriptionL, zipCodeL);
			}
		}

		public int? id{ get; set; }
		public String name{ get; set; }
		public String lastNameP{ get; set; }
		public String lastNameM{ get; set; }
		public String birthDate{ get; set; }
		public String imputeTel{ get; set; }
		public TabletAddressDto address{ get; set; }

	}
}

