using System;
using SQLite;

namespace UmecaApp
{
	[Table("school")]
	public class School
	{
		public School ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id")]
		public long Id{ get; set; }

		[Column("name"),MaxLength(200)]
		public String Name{ get; set; }

		[Column("phone"),MaxLength(30)]
		public String Phone{ get; set; }

		[Column("address"),MaxLength(255)]
		public String Address{ get; set; }

		[Column("id_grade")]
		public long DegreeId{ get; set; }

		[Column("specification"),MaxLength(300)]
		public String Specification{ get; set; }

		[Column("block")]
		public Boolean block{ get; set; }

		[Column("id_meeting")]
		public long MeetingId{ get; set; }

	}
}

