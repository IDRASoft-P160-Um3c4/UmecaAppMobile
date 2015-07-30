using System;

namespace UmecaApp
{
	public class TabletScheduleDto
	{
		public TabletScheduleDto ()
		{
		}

		public TabletScheduleDto(int id, long? webId, String day, String start, String end) {

			this.id = id;
			this.webId = webId;
			this.day = day;
			this.start = start;
			this.end = end;
		}

		public long? webId{ get; set; }
		public int id{ get; set; }
		public String day{ get; set; }
		public String start{ get; set; }
		public String end{ get; set; }

	}
}

