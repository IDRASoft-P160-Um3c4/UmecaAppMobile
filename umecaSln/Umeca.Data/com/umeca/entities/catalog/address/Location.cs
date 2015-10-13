using System;

 using SQLite;

namespace UmecaApp
{

	[Table("cat_location")]
	public class Location
	{
		public Location ()
		{
		}

		public Location (int id_location, long id_municipality, String name, String abbreviation, String description, String zip_code)
		{
			this.Id = id_location;
			this.MunicipalityId = id_municipality;
			this.Name = name;
			this.Abbreviation = abbreviation;
			this.Description = description;
			this.ZipCode = zip_code;
		}

		[PrimaryKey, Column("id_location")]//was also AutoIncrement
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