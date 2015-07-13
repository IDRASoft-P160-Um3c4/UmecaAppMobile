using System;
//listas
using System.Collections.Generic;
using Umeca.Data;


namespace UmecaApp
{
	public class  LogActivityListDto
	{
		public  LogActivityListDto ()
		{
		}

		public int CaseId{ get; set; }

		public string folder { get; set; }

		public string imputado { get; set; }

		public string message { get; set; }

		public List<LogCase> rows{ get; set; }
	}
}

