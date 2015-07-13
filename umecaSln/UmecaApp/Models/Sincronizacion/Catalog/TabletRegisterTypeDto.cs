using System;

namespace UmecaApp
{
	public class TabletRegisterTypeDto
	{
		public TabletRegisterTypeDto ()
		{
		}

		public TabletRegisterTypeDto( int? id, String name) {
			this.id = id;
			this.name = name;
		}

		public  int? id{ get; set ; }
		public String name{ get; set ; }

	}
}

