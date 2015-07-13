using System;

namespace UmecaApp
{
	public class TabletDegreeDto
	{
		public TabletDegreeDto ()
		{
		}

		public TabletDegreeDto(int id, String name, Boolean? isObsolete) {
			this.id = id;
			this.name = name;
			this.isObsolete = isObsolete;
		}

		public int id{ get; set ; }
		public String name{ get; set ; }
		public Boolean? isObsolete{ get; set ; }

	}
}

