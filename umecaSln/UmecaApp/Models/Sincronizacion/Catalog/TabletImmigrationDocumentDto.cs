using System;

namespace UmecaApp
{
	public class TabletImmigrationDocumentDto
	{
		public TabletImmigrationDocumentDto ()
		{
		}

		public TabletImmigrationDocumentDto(int? id, String name, Boolean? specification, Boolean? obsolete) {
			this.id = id;
			this.name = name;
			this.specification = specification;
			this.obsolete = obsolete;
		}

		public int? id{ get; set ; }
		public String name{ get; set ; }
		public Boolean? specification{ get; set ; }
		public Boolean? obsolete{ get; set ; }

	}
}

