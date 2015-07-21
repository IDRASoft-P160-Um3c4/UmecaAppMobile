using System;

namespace UmecaApp
{
	public class TabletArrangementDto
	{
		public TabletArrangementDto ()
		{
		}

		public TabletArrangementDto(int id, String description, int type, Boolean? isNational, int index, Boolean? isObsolete, Boolean? isDefault, Boolean? isExclusive) {
			this.id = id;
			this.description = description;
			this.type = type;
			this.isNational = isNational;
			this.index = index;
			this.isObsolete = isObsolete;
			this.isDefault = isDefault;
			this.isExclusive = isExclusive;
		}

		public int id{ get; set ; }
		public String description{ get; set ; }
		public int type{ get; set ; }
		public Boolean? isNational{ get; set ; }
		public int index{ get; set ; }
		public Boolean? isObsolete{ get; set ; }
		public Boolean? isDefault{ get; set ; }
		public Boolean? isExclusive{ get; set ; }

	}
}

