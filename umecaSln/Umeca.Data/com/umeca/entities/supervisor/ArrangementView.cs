using System;

namespace Umeca.Data
{
	
	public class ArrangementView
	{
		public ArrangementView ()
		{
		}

		public int Id{ get; set; }

		public String Name{ get; set; }

		public String Description{ get; set; }

		public Boolean SelVal{ get; set; }

		public Boolean IsDefault{ get; set; }

		public Boolean IsExclusive{ get; set; }

		public Boolean IsNational{ get; set; }

		public int Type{ get; set; }

	}
}