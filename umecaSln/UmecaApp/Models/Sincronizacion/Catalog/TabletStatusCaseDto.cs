using System;

namespace UmecaApp
{
	public class TabletStatusCaseDto
	{
		public TabletStatusCaseDto ()
		{
		}

		public int id{ get; set; }
		public String name{ get; set; }
		public String description{ get; set; }

		public TabletStatusCaseDto(int id, String name, String description) {
			this.id = id;
			this.name = name;
			this.description = description;
		}

	}
}