using System;

 using SQLite;

namespace Umeca.Data
{
	[Table("cat_hearing_type")]
	public class HearingType
	{
		public HearingType ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id_hearing_type")]
		public int Id{ get; set; }

		[Column("description")]
		public String Description{ get; set; }

		[Column("is_obsolete")]
		public Boolean IsObsolete{ get; set; }

		[Column("lock_arrangements")]
		public Boolean Lock{ get; set; }

		[Column("specification")]
		public Boolean Specification{ get; set; }

	}
}