using System;
using System.Collections.Generic;

namespace UmecaApp
{
	public class TabletAcademicLevelDto
	{
		public TabletAcademicLevelDto ()
		{
		}

		public int id{ get; set ; }
		public String name{ get; set ; }
		public Boolean isObsolete{ get; set ; }
		public List<TabletDegreeDto> degrees{ get; set ; }

	}
}

