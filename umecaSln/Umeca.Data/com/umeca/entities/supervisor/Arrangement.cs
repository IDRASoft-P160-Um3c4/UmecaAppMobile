using System;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;

namespace Umeca.Data
{
	[Table("cat_arrangement")]
	public class Arrangement
	{
		public Arrangement ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id_arrangement")]
		public int Id{ get; set; }

		[Column("description"), MaxLength(1500)]
		public String Description{ get; set; }

		[Column("type")]
		public int Type{ get; set; }

		[Column("is_national")]
		public Boolean IsNational{ get; set; }

		[Column("arrangement_index")]
		public int Index{ get; set; }

		[Column("is_obsolete")]
		public Boolean IsObsolete{ get; set; }

		[Column("is_default")]
		public Boolean IsDefault{ get; set; }

		[Column("is_exclusive")]
		public Boolean IsExclusive{ get; set; }

	}
}