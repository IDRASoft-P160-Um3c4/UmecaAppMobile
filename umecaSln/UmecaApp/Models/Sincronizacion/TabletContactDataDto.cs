using System;

namespace UmecaApp
{
	public class TabletContactDataDto
	{
		public TabletContactDataDto ()
		{
		}

		public TabletContactDataDto(int id, String nameTxt, String phoneTxt, String addressTxt) {
			this.id = id;
			this.nameTxt = nameTxt;
			this.phoneTxt = phoneTxt;
			this.addressTxt = addressTxt;
		}

		public int id{ get; set; }
		public String nameTxt{ get; set; }
		public String phoneTxt{ get; set; }
		public String addressTxt{ get; set; }
		public Boolean liveWith{ get; set; }

	}
}

