using System;
using SQLite.Net.Attributes;


namespace UmecaApp
{
	[Table("cat_crime")]
	public class CrimeCatalog
	{
		public CrimeCatalog ()
		{
		}

		[AutoIncrement,PrimaryKey]
		public int Id{ get; set;}

		[Column("name"),MaxLength(255)]
		public String Name{ get; set;}

		[Column("descritpion"),MaxLength(255)]
		public String Description{ get; set;}

		[Column("is_obsolete")]
		public Boolean IsObsolete{ get; set;}

		[Column("group_crime_id")]
		public int GroupCrimeId{ get; set;}
	}
}

