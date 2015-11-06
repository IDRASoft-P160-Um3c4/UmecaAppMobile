using System;
using SQLite;

namespace Umeca.Data
{
	[Table("cat_district")]
	public class District
	{
		public District ()
		{
		}

		[PrimaryKey, AutoIncrement,Column("id_district")]
		public int Id{ get; set; }

		[Column("name"),MaxLength(255)]
		public String Name{ get; set;}

		[Column("is_obsolete")]
		public bool IsObsolete{ get; set;}

	}
}

