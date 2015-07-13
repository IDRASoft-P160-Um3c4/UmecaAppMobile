using System;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;

using UmecaApp;

namespace Umeca.Data
{
	public class LogCase
	{
		public LogCase ()
		{
		}

		public LogCase(DateTime date, String activity, String userName, String title, String resume) {
			this.date = date;
			this.dateString = String.Format ("{0:dd/MM/yyyy hh:mm}", date).ToString ();
			this.activity = activity;
			this.userName = userName;
			this.title = title;
			this.resume = resume;
		}

		[PrimaryKey,AutoIncrement, Column("id_log_case")]
		public int id{ get; set; }

		[Column("date")]
		public DateTime date{ get; set; }

		[Column("activity")]
		public String activity{ get; set; }

		[Column("resume"),MaxLength(1500)]
		public String resume{ get; set; }

		[Column("title"),MaxLength(500)]
		public String title{ get; set; }

		[Column("id_case")]
		public int caseDetentionId{ get; set; }

		[Column("id_user")]
		public int userId{ get; set; }

		public String activityString{ get; set; }

		public String userName{ get; set; }

		public String dateString{ get; set; }

	}
}