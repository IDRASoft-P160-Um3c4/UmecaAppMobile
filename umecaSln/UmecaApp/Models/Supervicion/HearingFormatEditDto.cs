using System;

namespace UmecaApp
{
	public class HearingFormatEditDto
	{
		public HearingFormatEditDto ()
		{
		}

		public int IdCase{ get; set; }

		public String MsgError{ get; set; }

		public String hfView{ get; set; }

		public Boolean hasPrevHF{ get; set; }

		public String listCrime{ get; set; }

		public Boolean readonlyBand{ get; set; }

		public String lstHearingType{ get; set; }

		public String lstSupervisor{ get; set; }
		//crime
		public String optionsCrime{ get; set; }

		public String listElection{ get; set; }
		//address

		public String listState{ get; set; }

		public String address{ get; set; }

	}
}

