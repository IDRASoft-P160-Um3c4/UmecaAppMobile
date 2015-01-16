using System;
using SQLite;

namespace UmecaApp
{
	[Table("cat_register_type")]
	public class RegisterType
	{

		public RegisterType ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int Id{ get; set; }

		[Column("name"),MaxLength(255)]
		public String Name{ get; set;}

	}
}

