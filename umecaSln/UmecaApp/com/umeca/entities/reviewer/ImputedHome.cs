using System;

using SQLite.Net.Attributes;

namespace UmecaApp
{
	[Table("imputed_home")]
	public class ImputedHome
	{
		public ImputedHome ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id")]
		public int Id{ get; set; }

		[Column("id_address")]
			public int? AddressId{ get; set; }

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

//		@Override
//		public String toString() {
//			String result = "";
//			if (street != null && !street.equals("")) {
//				result = "Calle: " + street + " No Ext: " + outNum;
//			}
//			if (outNum != null && !outNum.equals("")) {
//
//			}
//			if (innNum != null && !innNum.equals("")) {
//				result = result + " No Int:" + innNum;
//			}
//			if (location != null) {
//				result = result + "," + location.getName() + ". CP: " + location.getZipCode() + ". " + location.getMunicipality().getName() + ", " + location.getMunicipality().getState().getName() + ".";
//			}
//			return result;
//		}

		[Column("id_register_type")]
			public int? RegisterTypeId{ get; set; }

		[Column("time_live"),MaxLength(30)]
			public String TimeLive{ get; set; }

		[Column("id_belong")]
			public int? HomeTypeId{ get; set; }

		[Column("reason_change"),MaxLength(500)]
			public String ReasonChange{ get; set; }

		[Column("description"),MaxLength(500)]
			public String Description{ get; set; }

		[Column("phone"),MaxLength(200)]
			public String Phone{ get; set; }

		[Column("id_meeting")]
			public int? MeetingId{ get; set; }

		[Column("specification"),MaxLength(50)]
			public String Specification{ get; set; }

		[Column("reasonSecondary"),MaxLength(500)]
			public String ReasonSecondary{ get; set; }

			public String Schedule{ get; set; }
	}
}

