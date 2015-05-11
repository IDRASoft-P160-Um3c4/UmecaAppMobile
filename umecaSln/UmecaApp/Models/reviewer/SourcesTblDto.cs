using System;

namespace UmecaApp
{
	public class SourcesTblDto
	{
		public SourcesTblDto ()
		{
		}

		public int CaseId{ get; set; }

		public String IdFolder{ get; set; }

		public String FullnameImputed{ get; set; }

		public int? Age{ get; set; }

		public String reviewerFullname{ get; set; }

		public String tStart{ get; set; }

		public String tEnd{ get; set; }

		public String SourceListJson{ get; set; }
	}
}

