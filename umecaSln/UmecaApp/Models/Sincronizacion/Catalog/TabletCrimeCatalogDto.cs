using System;

namespace UmecaApp
{
	public class TabletCrimeCatalogDto
	{
		public TabletCrimeCatalogDto ()
		{
		}

		public TabletCrimeCatalogDto(int id, String name, String description, Boolean obsolete) {
			this.id = id;
			this.name = name;
			this.description = description;
			this.obsolete = obsolete;
		}

		public int id{ get; set ; }
		public String name{ get; set ; }
		public String description{ get; set ; }
		public Boolean? obsolete{ get; set ; }

	}
}

