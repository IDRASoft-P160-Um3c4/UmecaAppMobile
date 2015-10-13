using System;

 using SQLite;
using Umeca.Data;

namespace UmecaApp
{

	public class AddressDto
	{
		public AddressDto ()
		{
		}

		public AddressDto(Address address ){
			this.id = address.Id;
			this.street = address.Street;
			this.outNum = address.OutNum;
			this.innNum = address.InnNum;
			this.lat = address.Lat;
			this.lng = address.Lng;
		}

		public AddressDto(Address address,Location location) {
			this.id = address.Id;
			this.street = address.Street;
			this.outNum = address.OutNum;
			this.innNum = address.InnNum;
			this.lat = address.Lat;
			this.lng = address.Lng;
			this.locationId = location.Id;
			this.zipCode = location.ZipCode;
		}

		public int id{ get; set; }
		public String street{ get; set; }
		public String outNum{ get; set; }
		public String innNum{ get; set; }
		public String zipCode{ get; set; }
		public String lat{ get; set; }
		public String lng{ get; set; }
		public int idCase{ get; set; }
		public int locationId{ get; set; }
		public LocationDto location{ get; set; }
		public String addressRef{ get; set; }
	}
}