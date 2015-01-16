using System;
using SQLite;

namespace UmecaApp
{
	[Table("meeting")]
	public class Meeting
	{

		public Meeting ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id")]
		private int Id{ get; set; }

		[Column("reviewer_id")]
		private int ReviewerId{ get; set; }

		[Column("case_id")]
		private int CaseId{ get; set; }

		[Column("status_id")]
		private int StatusId{ get; set; }

		[Column("comment_refernce"),MaxLength(500)]
		private String CommentReference{ get; set; }

		[Column("comment_job"),MaxLength(500)]
		private String commentJob{ get; set; }

		[Column("comment_school"),MaxLength(500)]
		private String commentSchool{ get; set; }

		[Column("comment_country"),MaxLength(500)]
		private String commentCountry{ get; set; }

		[Column("comment_home"),MaxLength(500)]
		private String commentHome{ get; set; }

		[Column("comment_drug"),MaxLength(500)]
		private String commentDrug{ get; set; }

		[Column("date_create")]
		private DateTime dateCreate{ get; set; }

		[Column("date_terminate")]
		private DateTime dateTerminate{ get; set; }
	}
}

