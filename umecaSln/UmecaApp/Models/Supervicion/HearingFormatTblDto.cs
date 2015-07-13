using System;
//listas
using System.Collections.Generic;

namespace UmecaApp
{
	public class HearingFormatTblDto
	{
		public HearingFormatTblDto ()
		{
		}

		public int CaseId{ get; set; }

		public string message { get; set; }

		public List<HearingFormatGrid> rows{ get; set; }
	}
}

