using System;
//listas
using System.Collections.Generic;

namespace UmecaApp
{
	public class GroupMessageMeetingDto
	{
		public GroupMessageMeetingDto ()
		{
		}

		public GroupMessageMeetingDto(String section) {
			this.section = section;
		}

		public GroupMessageMeetingDto(String section, List<String> listString) {
			this.section = section;
			this.listString = listString;
		}

		public String section{ get; set; }
		public List<String> listString{ get; set; }
		public String messages{ get; set; }
	}
}

