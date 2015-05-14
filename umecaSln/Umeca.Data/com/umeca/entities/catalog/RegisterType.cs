using System;
using SQLite.Net.Attributes;

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

		[Column("register_type"),MaxLength(255)]
		public String Name{ get; set;}

	}
}

