using System;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;

using UmecaApp;

namespace Umeca.Data
{
	[Table("crime")]
	public class Crime
	{
		public Crime ()
		{
		}

		[PrimaryKey,AutoIncrement, Column("id_crime")]
		public int Id{ get; set; }

		[Column("comment")]
		public String Comment{ get; set; }

		[Column("article")]
		public String Article{ get; set; }

		//		CrimeCatalog
		[Column("id_crime_cat")]
		public int IdCrimeCat{ get; set; }

		//		Election si/no
		[Column("id_federal")]
		public int Federal{ get; set; }


		//UN CRIMEN PUEDE PERTENECER A CRIMINAL PROCEDING O HEARING FORMAT
		//		CurrentCriminalProceeding 
		[Column("id_criminal_proceeding")]
		public int CriminalProceeding{ get; set; }

		//		HearingFormat
		[Column("hearingFormat")]
		public int HearingFormat{ get; set; }

		public String HashKey{ get; set; }

	}
}