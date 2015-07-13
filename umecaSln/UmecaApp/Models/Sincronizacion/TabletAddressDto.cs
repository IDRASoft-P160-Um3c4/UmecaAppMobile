using System;

namespace UmecaApp
{
	public class TabletAddressDto
	{
		public TabletAddressDto ()
		{
		}

		public TabletAddressDto( int? id, String street, String outNum, String innNum, String lat, String lng, String addressString,
			 int? idL, String nameL, String abbreviationL, String descriptionL, String zipCodeL){
			this.id = id;
			this.street = street;
			this.outNum = outNum;
			this.innNum = innNum;
			this.lat = lat;
			this.lng = lng;
			this.addressString = addressString;

			if(idL!=null){
				this.location = new TabletLocationDto(idL,nameL,abbreviationL,descriptionL,zipCodeL);
			}
		}

		public TabletAddressDto(Address address){
			this.id = address.Id;
			this.street = address.Street;
			this.outNum = address.OutNum;
			this.innNum = address.InnNum;
			this.lat = address.Lat;
			this.lng = address.Lng;
			this.addressString = address.addressString;
		}

		public  int? id{ get; set ; }
		public String street{ get; set ; }
		public String outNum{ get; set ; }
		public String innNum{ get; set ; }
		public String lat{ get; set ; }
		public String lng{ get; set ; }
		public String addressString{ get; set ; }
		public TabletLocationDto location{ get; set ; }

	}
}

