using System;

namespace UmecaApp
{
	public class TabletStateDto
	{
		public TabletStateDto ()
		{
		}

		public int id{ get; set ; }
		public TabletCountryDto country{ get; set ; }
		public String name{ get; set ; }
		public String abbreviation{ get; set ; }
		public String description{ get; set ; }

	}
}

