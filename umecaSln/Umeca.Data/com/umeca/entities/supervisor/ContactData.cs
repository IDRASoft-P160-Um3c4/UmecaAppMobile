using System;

 using SQLite;

namespace Umeca.Data
{
	[Table("contact_data")]
	public class ContactData
	{
		public ContactData ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id_contact_data")]
		public int Id{ get; set; }

		[Column("name")]
		public String NameTxt{ get; set; }

		[Column("phone")]
		public String PhoneTxt{ get; set; }

		[Column("address")]
		public String AddressTxt{ get; set; }

		[Column("id_hearing_format")]
		public int HearingFormat{ get; set; }

	}
}