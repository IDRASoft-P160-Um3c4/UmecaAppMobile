using System;

using SQLite.Net.Attributes;

namespace UmecaApp
{

	[Table("cat_country")]
	public class Country
	{
		public Country ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int Id{get;set;}

		[Column("name"),MaxLength(50)]
		public String Name{get;set;}

		[Column("alpha2"),MaxLength(2)]
		public String Alpha2{get;set;}

		[Column("alpha3"),MaxLength(3)]
		public String Alpha3{get;set;}

		[Column("latitude")]
		public long Latitude{get;set;}

		[Column("longitude")]
		public long Longitude{get;set;}


	}
}

