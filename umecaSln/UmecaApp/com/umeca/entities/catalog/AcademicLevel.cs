﻿using System;
using SQLite.Net.Attributes;
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

		[Column("is_obsolete")]
		public Boolean? IsObsolete { get; set;}

		[Column("academic_level"), MaxLength(255)]
		public String Name{get; set;}

	}
}
