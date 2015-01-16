using System;
using SQLite;



namespace UmecaApp
{
	[Table("case_detention")]
	public class Case
	{
		public Case ()
		{
		}
		[PrimaryKey, AutoIncrement]
		public int Id{ get; set; }

		[Column("id_folder"),MaxLength(25)]
		public String IdFolder{ get; set; }

		[Column("id_mp"),MaxLength(15)]
		public String IdMP{ get; set; }

		[Column("status_id")]
		public int StatusId{ get; set; }

		[Column("date_create")]
		public DateTime DateCreate{ get; set; }
	}
}

