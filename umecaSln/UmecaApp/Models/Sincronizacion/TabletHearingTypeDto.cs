using System;

namespace UmecaApp
{
	public class TabletHearingTypeDto
	{
		public TabletHearingTypeDto ()
		{
		}

		public TabletHearingTypeDto(int id, String description, Boolean? isObsolete, Boolean? Lock, Boolean? specification) {
			this.id = id;
			this.description = description;
			this.isObsolete = isObsolete;
			this.Lock = Lock;
					this.specification = specification;
		}

		public int id{ get; set; }
		public String description{ get; set; }
		public Boolean? isObsolete{ get; set; }
		public Boolean? Lock{ get; set; }
		public Boolean? specification{ get; set; }

	}
}

