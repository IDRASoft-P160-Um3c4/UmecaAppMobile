using System;

using SQLite.Net.Attributes;

namespace UmecaApp
{
	[Table("cat_state")]
	public class State
	{
		public State ()
		{
		}

		[AutoIncrement, PrimaryKey]
		public int Id{ get; set;}

		[Column("id_country")]
		public long CountryId{get;set;}

		[Column("name"), MaxLength(50)]
		public String Name{ get; set;}

		[Column("abbreviation"), MaxLength(100)]
		public String Abbreviation{get; set;}


		[Column("description"), MaxLength(100)]
		public String Description{get; set;}

	}
}

