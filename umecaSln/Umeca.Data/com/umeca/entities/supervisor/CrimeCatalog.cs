using System;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;

namespace Umeca.Data
{
	[Table("cat_crime")]
	public class CrimeCatalog
	{
		public CrimeCatalog ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id_crime_cat")]
		public int Id{ get; set; }

		//		GroupCrime
		[Column("id_group")]
		public int GroupCrime{ get; set; }

		[Column("name"),MaxLength(255)]
		public String Name{ get; set; }

		[Column("description"),MaxLength(255)]
		public String Description{ get; set; }

		[Column("is_obsolete")]
		public Boolean Obsolete{ get; set; }

	}
}