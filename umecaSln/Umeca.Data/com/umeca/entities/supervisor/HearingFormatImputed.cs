using System;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;

namespace Umeca.Data
{
	[Table("hearing_format_imputed")]
	public class HearingFormatImputed
	{
		public HearingFormatImputed ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id_hearing_format_imputed")]
		public int Id{ get; set; }

		[Column("name")]
		public String Name{ get; set; }

		[Column("lastname_p")]
		public String LastNameP{ get; set; }

		[Column("lastname_m")]
		public String LastNameM{ get; set; }

		[Column("birth_date")]
		public DateTime? BirthDate{ get; set; }

		[Column("imputed_tel")]
		public String ImputeTel{ get; set; }

		//		Address
		[Column("id_addres")]
		public int Address{ get; set; }

	}
}