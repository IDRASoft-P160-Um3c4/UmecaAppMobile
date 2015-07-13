using System;

namespace Umeca.Data
{
	
	public class ArrangementView
	{
		public ArrangementView ()
		{
		}

		public int id{ get; set; }

		public String name{ get; set; }

		public String description{ get; set; }

		public Boolean selVal{ get; set; }

		public Boolean isDefault{ get; set; }

		public Boolean isExclusive{ get; set; }

		public Boolean isNational{ get; set; }

		public int type{ get; set; }

	}
}