using System;
using SQLite;

namespace UmecaApp
{
	[Table("cat_satate")]
	public class State
	{
		public State ()
		{
		}

		[AutoIncrement, PrimaryKey]
		public long Id{ get; set;}

		[Column("country_id")]
		public long CountryId{get;set;}

		[Column("name"), MaxLength(50)]
		public String Name{ get; set;}

		[Column("abbreviation"), MaxLength(100)]
		public String Abbreviation{get; set;}


		[Column("description"), MaxLength(100)]
		public String Description{get; set;}

	}
}

