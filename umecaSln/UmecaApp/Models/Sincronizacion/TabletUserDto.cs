using System;

namespace UmecaApp
{
	public class TabletUserDto
	{
		public int? id{ get; set; }
		public String fullname{ get; set; }
		public String hPassword{ get; set; }
		public String roleCode{ get; set; }
		public String guid{ get; set; }

		public TabletUserDto() {

		}

		public TabletUserDto(int? id, String fullname) {
			this.id = id;
			this.fullname = fullname;
		}

		public TabletUserDto(int? id, String fullname, String hPassword) {
			this.id = id;
			this.fullname = fullname;
			this.hPassword = hPassword;
		}
	}
}

