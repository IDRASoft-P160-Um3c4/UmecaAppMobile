using System;

namespace UmecaApp
{
	public class TabletScheduleDto
	{
		public TabletScheduleDto ()
		{
		}

		public TabletScheduleDto(int id, String day, String start, String end) {

			this.id = id;
			this.webId = id;
			this.day = day;
			this.start = start;
			this.end = end;
		}

		public int webId{ get; set; }
		public int id{ get; set; }
		public String day{ get; set; }
		public String start{ get; set; }
		public String end{ get; set; }

	}
}

