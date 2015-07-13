using System;
using System.Collections.Generic;

namespace UmecaApp
{
	public class TabletSchoolDto
	{
		public TabletSchoolDto ()
		{
		}

		public TabletSchoolDto( int?  id, String name, String phone, String address, String specification, Boolean block) {
			this.id = id;
			this.webId = id;
			this.name = name;
			this.phone = phone;
			this.address = address;
			this.specification = specification;
			this.block = block;
		}

		public  int?  webId{ get; set; }
		public  int?  id{ get; set; }
		public String name{ get; set; }
		public String phone{ get; set; }
		public String address{ get; set; }
		public String specification{ get; set; }
		public Boolean block{ get; set; }
		public TabletDegreeDto degree{ get; set; }
		public List<TabletScheduleDto> schedule{ get; set; }

	}
}

