using System;

namespace UmecaApp
{
	public class TabletMaritalStatusDto
	{
		public int id{ get; set; }
		public String name{ get; set; }

		public TabletMaritalStatusDto() {}

		public TabletMaritalStatusDto(int id, String name) {
			this.id = id;
			this.name = name;
		}
	}
}

