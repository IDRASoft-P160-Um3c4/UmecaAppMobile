using System;

namespace UmecaApp
{
	public class TabletLocationDto
	{
		public TabletLocationDto ()
		{
		}

		public TabletLocationDto( int? id, String name, String abbreviation, String description, String zipCode) {
			this.id = id;
			this.name = name;
			this.abbreviation = abbreviation;
			this.description = description;
			this.zipCode = zipCode;
		}

		public TabletLocationDto(Location location){
			this.id = id;
			this.name = location.Name;
			this.abbreviation = location.Abbreviation;
			this.description = location.Description;
			this.zipCode = location.ZipCode;
		}

		public  int? id{ get; set; }
		public TabletMunicipalityDto municipality{ get; set; }
		public String name{ get; set; }
		public String abbreviation{ get; set; }
		public String description{ get; set; }
		public String zipCode{ get; set; }

	}
}

