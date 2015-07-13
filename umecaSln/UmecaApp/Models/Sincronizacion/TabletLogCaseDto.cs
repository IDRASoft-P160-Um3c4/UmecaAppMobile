using System;

namespace UmecaApp
{
	public class TabletLogCaseDto
	{
		public TabletLogCaseDto ()
		{
		}

		public TabletLogCaseDto(DateTime date, String activity, String userName, String title, String resume) {
			this.date = date;
			this.dateString = String.Format ("{0:dd/MM/yyyy hh:mm}", date).ToString ();
			this.activity = activity;
			this.userName = userName;
			this.title = title;
			this.resume = resume;
		}

		public int id{ get; set; }

		public DateTime date{ get; set; }

		public String activity{ get; set; }

		public String resume{ get; set; }

		public String title{ get; set; }

		public int caseDetentionId{ get; set; }

		public int userId{ get; set; }

		public String activityString{ get; set; }

		public String userName{ get; set; }

		public String dateString{ get; set; }

	}
}

