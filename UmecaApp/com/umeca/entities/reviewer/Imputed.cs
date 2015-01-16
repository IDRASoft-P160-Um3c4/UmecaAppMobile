using System;
using SQLite;

namespace UmecaApp
{
	[Table("imputed")]
	public class Imputed
	{
		public Imputed ()
		{
		}
		[PrimaryKey, AutoIncrement, Column("id")]
		public int Id{ get; set; }

		[Column("name"),MaxLength(50)]
		public String Name{ get; set; }

		[Column("lastname_p"),MaxLength(50)]
		public String LastNameP{ get; set; }

		[Column("lastname_m"),MaxLength(50)]
		public String LastNameM{ get; set; }
	
		[Column("fonetic_string"),MaxLength(150)]
		public String FoneticString{ get; set; }

		[Column("gender")]
		public Boolean Gender{ get; set; }

		[Column("birth_date")]
		public DateTime BirthDate{ get; set; }

		[Column("cel_phone"),MaxLength(20)]
		public String CelPhone{ get; set; }

		[Column("years_marital_status")]
		public int YearsMaritalStatus{ get; set; }

		[Column("id_marital_status")]
		public int MaritalStatus{ get; set; }

		[Column("boys")]
		public int Boys{ get; set; }

		[Column("dependent_boys"),MaxLength(25)]
		public int DependentBoys{ get; set; }

		[Column("country_id")]
		public int BirthCountryId{ get; set; }

		[Column("birth_municipality"),MaxLength(500)]
		public String BirthMunicipality{ get; set; }

		[Column("birth_state"),MaxLength(500)]
		public String BirthState{ get; set; }

		[Column("birth_location"),MaxLength(500)]
		public String BirthLocation{ get; set; }

		[Column("nickname"),MaxLength(100)]
		public String Nickname{ get; set; }

		[Column("location_id")]
		public int LocationId{ get; set; }

		[Column("meeting_id")]
		public int MeetingId{ get; set; }
	}
}

