using System;

namespace UmecaApp
{
	public class TabletRelSocialEnvironmentActivityDto
	{
		public TabletRelSocialEnvironmentActivityDto ()
		{
		}

		public TabletRelSocialEnvironmentActivityDto(int id, String specification,int idAct, String nameAct, Boolean specificationAct, Boolean isObsoleteAct) {
			this.id = id;
			this.specification = specification;
			activity = new TabletActivityDto(idAct,nameAct,specificationAct,isObsoleteAct);
		}

		public int id{ get; set; }
		public TabletActivityDto activity{ get; set; }
		public String specification{ get; set; }

	}
}

