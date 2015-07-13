using System;

namespace UmecaApp
{
	public class TabletElectionDto
	{
		public TabletElectionDto ()
		{
		}

		public TabletElectionDto(int? id, String name) {
			this.id = id;
			this.name = name;
		}

		public int? id{ get; set ; }
		public String name{ get; set ; }

	}
}

