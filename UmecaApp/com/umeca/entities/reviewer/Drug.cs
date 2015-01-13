using System;
using SQLite;

namespace UmecaApp
{
	[Table("drug")]
	public class Drug
	{
		public Drug ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id")]
		public long Id{ get; set; }

		[Column("name")]
		public long DrugTypeId{ get; set; }

		[Column("id_periodicity")]
		public long PeriodicityId{ get; set; }

		[Column("quantity"),MaxLength(25)]
		public String Quantity{ get; set; }

		[Column("last_use")]
		public DateTime LastUse{ get; set; }

		[Column("block")]
		public Boolean block{ get; set; }

		[Column("specification_type"),MaxLength(100)]
		public String Specification{ get; set; }

		[Column("specification_periodicity"),MaxLength(100)]
		public String SpecificationPeriodicity{ get; set; }

		[Column("onset_age")]
		public String OnsetAge{ get; set; }

		[Column("id_meeting")]
		public long MeetingId{ get; set; }

	}
}

