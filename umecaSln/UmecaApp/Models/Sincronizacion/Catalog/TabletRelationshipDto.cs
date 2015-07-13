using System;

namespace UmecaApp
{
	public class TabletRelationshipDto
	{
		public TabletRelationshipDto ()
		{
		}

		public TabletRelationshipDto(int? id, String name, Boolean? isObsolete, Boolean? specification) {
			this.id = id;
			this.name = name;
			this.isObsolete = isObsolete;
			this.specification = specification;
		}

		public int? id;
		public String name;
		public Boolean? isObsolete;
		public Boolean? specification;

	}
}

