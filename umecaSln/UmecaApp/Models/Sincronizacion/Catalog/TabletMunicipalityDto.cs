using System;

namespace UmecaApp
{
	public class TabletMunicipalityDto
	{
		public TabletMunicipalityDto ()
		{
		}

		public int id{ get; set; }
		public TabletStateDto state{ get; set; }
		public String name{ get; set; }
		public String abbreviation{ get; set; }
		public String description{ get; set; }

	}
}

