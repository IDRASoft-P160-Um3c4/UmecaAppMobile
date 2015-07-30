using System;
using System.Collections.Generic;

namespace UmecaApp
{
	public class TabletSocialEnvironmentDto
	{
		public TabletSocialEnvironmentDto ()
		{
		}

		public TabletSocialEnvironmentDto( int?  id, String physicalCondition, String comment) {
			this.id = id;
			this.webId = id;
			this.physicalCondition = physicalCondition;
			this.comment = comment;
		}

		public  long?  webId{ get; set; }
		public  int?  id{ get; set; }
		public String physicalCondition{ get; set; }
		public String comment{ get; set; }
		public List<TabletRelSocialEnvironmentActivityDto> relSocialEnvironmentActivities{ get; set; }

	}
}

