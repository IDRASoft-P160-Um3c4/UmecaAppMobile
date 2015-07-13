using System;

namespace UmecaApp
{
	public class TabletActivityDto
	{
		public TabletActivityDto ()
		{
		}

		public TabletActivityDto(int id, String name, Boolean? specification, Boolean? isObsolete) {
			this.id = id;
			this.name = name;
			this.specification = specification;
			this.isObsolete = isObsolete;
		}

		public int id{ get; set ; }
		public String name{ get; set ; }
		public Boolean? specification{ get; set ; }
		public Boolean? isObsolete{ get; set ; }

	}
}

