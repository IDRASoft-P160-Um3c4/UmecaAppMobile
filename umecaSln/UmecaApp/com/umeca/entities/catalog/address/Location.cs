using System;

using SQLite.Net.Attributes;

namespace UmecaApp
{

	[Table("cat_location")]
	public class Location
	{
		public Location ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int Id{ get; set;}

		[Column("id_municipality")]
		public long MunicipalityId{ get; set;}

		[Column("name"),MaxLength(100)]
		public String Name{ get; set;}

		[Column("abbreviation"),MaxLength(100)]
		public String Abbreviation{ get; set;}

		[Column("description"),MaxLength(100)]
		public String Description{ get; set;}

		[Column("zip_code"),MaxLength(10)]
		public String ZipCode{ get; set;}
	}
}

