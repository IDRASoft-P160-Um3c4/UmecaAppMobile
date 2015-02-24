using System;
using SQLite.Net.Attributes;

namespace UmecaApp
{
	[Table("cat_hearing_format_type")]
	public class HearingType
	{
		public HearingType ()
		{
		}

		[AutoIncrement, PrimaryKey]
		public int Id{ get; set;}

		[Column("name"), MaxLength(255)]
		public String Name{get; set;}

		[Column("description"), MaxLength(255)]
		public String Description{ get; set;}

		[Column("is_obsolete")]
		public Boolean IsObsolete{get; set;}

	}
}

