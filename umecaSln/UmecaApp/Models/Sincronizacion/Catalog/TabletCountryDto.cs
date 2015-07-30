using System;

namespace UmecaApp
{
	public class TabletCountryDto
	{
		public TabletCountryDto ()
		{
		}

		public TabletCountryDto(int? id, String name, String alpha2, String alpha3, long? latitude, long? longitude) {
			this.id = id;
			this.name = name;
			this.alpha2 = alpha2;
			this.alpha3 = alpha3;
			this.latitude = latitude;
			this.longitude = longitude;
		}

		public int? id{ get; set; }
		public String name{ get; set; }
		public String alpha2{ get; set; }
		public String alpha3{ get; set; }
		public Double? latitude{ get; set; }
		public Double? longitude{ get; set; }


	}
}

