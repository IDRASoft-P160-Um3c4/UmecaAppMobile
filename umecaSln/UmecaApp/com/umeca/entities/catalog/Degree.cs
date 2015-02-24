using System;
using SQLite.Net.Attributes;

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

		[Column("degree"), MaxLength(255)]
		public String Name{ get; set;}

		[Column("is_obsolete")]
		public Boolean? IsObsolete{ get; set;}

		[Column("id_academic_level")]
		public int AcademicLevelId{ get; set;}
	}
}

