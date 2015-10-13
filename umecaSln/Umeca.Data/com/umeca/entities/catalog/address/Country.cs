using System;

 using SQLite;

namespace UmecaApp
{

	[Table("cat_country")]
	public class Country
	{
		public Country ()
		{
		}

		public Country (int id, String name, String alpha2, String alpha3, Double lat, Double lon)
		{
			this.Id = id;
			this.Name = name;
			this.Alpha2 = alpha2;
			this.Alpha3 = alpha3;
			this.Latitude = lat;
			this.Longitude = lon;
		}

		[PrimaryKey]
		public int Id{get;set;}

		[Column("name"),MaxLength(50)]
		public String Name{get;set;}

		[Column("alpha2"),MaxLength(2)]
		public String Alpha2{get;set;}

		[Column("alpha3"),MaxLength(3)]
		public String Alpha3{get;set;}

		[Column("latitude")]
		public Double Latitude{get;set;}

		[Column("longitude")]
		public Double Longitude{get;set;}


	}
}

