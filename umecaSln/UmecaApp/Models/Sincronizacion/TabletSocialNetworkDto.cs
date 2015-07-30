using System;
using System.Collections.Generic;

namespace UmecaApp
{
	public class TabletSocialNetworkDto
	{
		public TabletSocialNetworkDto ()
		{
		}

		public TabletSocialNetworkDto(int id, String comment) {
			this.id = id;
			this.webId = id;
			this.comment = comment;
		}

		public long? webId{ get; set; }
		public int? id{ get; set; }
		public String comment{ get; set; }
		public List<TabletPersonSocialNetworkDto> peopleSocialNetwork{ get; set; }

	}
}

