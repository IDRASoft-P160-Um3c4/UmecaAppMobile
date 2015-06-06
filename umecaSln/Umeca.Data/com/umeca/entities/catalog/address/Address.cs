using System;

using SQLite.Net.Attributes;

//listas
using System.Collections.Generic;

namespace UmecaApp
{
	[Table("address")]
	public class Address
	{
		public Address ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id_address")]
		public int Id{ get; set; }

		[Column("street"),MaxLength(100)]
		public String Street{ get; set; }

		[Column("no_outside"),MaxLength(10)]
		public String OutNum{ get; set; }

		[Column("no_inside"),MaxLength(10)]
		public String InnNum{ get; set; }

		[Column("id_location")]
		public int? LocationId{ get; set; }

		[Column("lat")]
		public String Lat{ get; set; }

		[Column("lng")]
		public String Lng{ get; set; }

		[Column("address_string"),MaxLength(500)]
		public String addressString{ get; set; }

	}
}

