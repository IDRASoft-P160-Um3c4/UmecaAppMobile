using System;

namespace UmecaApp
{
	public class NewLogCaseDto
	{
		public NewLogCaseDto ()
		{
		}

		public int id{ get; set; }

		public DateTime date{ get; set; }

		public String activity{ get; set; }

		public String resume{ get; set; }

		public String title{ get; set; }

		public int caseDetentionId{ get; set; }

		public int userId{ get; set; }

	}
}
