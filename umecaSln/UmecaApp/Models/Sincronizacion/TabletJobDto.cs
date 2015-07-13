using System;
using System.Collections.Generic;

namespace UmecaApp
{
	public class TabletJobDto
	{
		public TabletJobDto ()
		{
		}

		public TabletJobDto(int id, String post, String nameHead, String company, String phone,DateTime?startPrev,DateTime?start, float salaryWeek,DateTime?end, String reasonChange, String address, Boolean? block,
			int idRT, String nameRT) {

//			SimpleDateFormat sdf = new SimpleDateFormat("yyyy/MM/dd");

			this.id = id;
			this.webId = id;
			this.post = post;
			this.nameHead = nameHead;
			this.company = company;
			this.phone = phone;
			this.startPrev = startPrev == null ? null : String.Format("{0:yyyy/MM/dd}", startPrev);
			this.start = start == null ? null : String.Format("{0:yyyy/MM/dd}", start);
			this.salaryWeek = salaryWeek;
			this.end = end == null ? null : String.Format("{0:yyyy/MM/dd}", end);
			this.reasonChange = reasonChange;
			this.address = address;
			this.block = block;

			if (idRT != null) {
				this.registerType = new TabletRegisterTypeDto(idRT, nameRT);
			}
		}

		public int? webId{ get; set; }
		public int id{ get; set; }
		public String post{ get; set; }
		public String nameHead{ get; set; }
		public String company{ get; set; }
		public String phone{ get; set; }
		public String startPrev{ get; set; }
		public String start{ get; set; }
		public float salaryWeek{ get; set; }
		public String end{ get; set; }
		public String reasonChange{ get; set; }
		public String address{ get; set; }
		public Boolean? block{ get; set; }
		public TabletRegisterTypeDto registerType{ get; set; }
		public List<TabletScheduleDto> schedule{ get; set; }

	}
}

