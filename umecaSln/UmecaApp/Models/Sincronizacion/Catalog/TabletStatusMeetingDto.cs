using System;

namespace UmecaApp
{
	public class TabletStatusMeetingDto
	{
		public TabletStatusMeetingDto ()
		{
		}

		public TabletStatusMeetingDto(int id, String name, String description) {
			this.id = id;
			this.name = name;
			this.description = description;
		}

		public int id{ get; set; }
		public String name{ get; set; }
		public String description{ get; set; }

	}
}

