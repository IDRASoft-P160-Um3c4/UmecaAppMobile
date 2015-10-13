using System;
 using SQLite;

namespace UmecaApp
{
	[Table("cat_relationship")]
	public class Relationship
	{
		public Relationship ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int Id{ get; set; }

		[Column("is_obsolete")]
		public Boolean? IsObsolete{ get; set; }

		[Column("relationship"),MaxLength(255)]
		public String Name{ get; set; }

		[Column("specification")]
		public Boolean? Specification{ get; set; }

	}
}

