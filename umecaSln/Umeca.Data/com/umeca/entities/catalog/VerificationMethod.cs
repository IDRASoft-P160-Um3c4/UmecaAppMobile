using System;

 using SQLite;

namespace UmecaApp
{
	[Table("cat_status_case")]
	public class VerificationMethod
	{
	
		public VerificationMethod ()
		{
		}

		[PrimaryKey, AutoIncrement,Column("id_status")]
		public int Id{ get; set; }

		[Column("name"),MaxLength(255)]
		public String Name{ get; set;}

		[Column("is_obsolete")]
		public bool IsObsolete{ get; set;}
	}
}

