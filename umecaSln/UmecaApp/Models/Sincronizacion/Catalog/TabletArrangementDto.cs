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

		private int id{ get; set ; }
		private String description{ get; set ; }
		private int type{ get; set ; }
		private Boolean? isNational{ get; set ; }
		private int index{ get; set ; }
		private Boolean? isObsolete{ get; set ; }
		private Boolean? isDefault{ get; set ; }
		private Boolean? isExclusive{ get; set ; }

	}
}

