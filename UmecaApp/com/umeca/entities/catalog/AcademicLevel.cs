using System;
using SQLite;
using System.Collections.Generic;

namespace UmecaApp
{
	[Table("cat_academic_level")]
	public class AcademicLevel
	{
		public AcademicLevel ()
		{
		}

		[AutoIncrement,PrimaryKey]
		public int Id{ get; set;}

		[Column("name"), MaxLength(255)]
		public String Name{get; set;}

		[Column("is_obsolete")]
		public Boolean IsObsolete { get; set;}

		public List<Degree> Degrees {get; set;}

	}
}

