using System;
using SQLite;

namespace UmecaApp
{
	[Table("cat_degree")]
	public class Degree
	{
		public Degree ()
		{
		}

		[AutoIncrement, PrimaryKey]
		public int Id{get; set;}

		[Column("name"), MaxLength(255)]
		public String Name{ get; set;}

		[Column("is_obsolete")]
		public Boolean IsObsolete{ get; set;}

		[Column("academic_level_id")]
		public int AcademicLevelId{ get; set;}
	}
}

