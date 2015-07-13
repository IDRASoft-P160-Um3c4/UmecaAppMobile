using System;

namespace UmecaApp
{
	public class TabletStatusVerificationDto
	{
		public TabletStatusVerificationDto ()
		{
		}


		public TabletStatusVerificationDto(int? id, String name, String description) {
			this.id = id;
			this.name = name;
			this.description = description;
		}

		public int? id{ get; set ; }
		public String name{ get; set ; }
		public String description{ get; set ; }

	}
}

