using System;
using SQLite;

namespace Umeca.Data
{
	[Table("cat_information_availability")]
	public class InformationAviability
	{
		public InformationAviability ()
		{
		}

		[PrimaryKey, AutoIncrement,Column("information_availability")]
		public int Id{ get; set; }

		[Column("name"),MaxLength(255)]
		public String Name{ get; set;}

		[Column("is_obsolete")]
		public bool IsObsolete{ get; set;}

		[Column("specification")]
		public bool Specification{ get; set;}


	}
}
