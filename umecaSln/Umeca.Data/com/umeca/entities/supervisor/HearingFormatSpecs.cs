using System;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;

namespace Umeca.Data
{
	[Table("hearing_format_specs")]
	public class HearingFormatSpecs
	{
		public HearingFormatSpecs ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id_format_specs")]
		public int Id{ get; set; }

		[Column("control_detention")]
		public int ControlDetention{ get; set; }

		[Column("extension")]
		public int Extension{ get; set; }

		[Column("imputation_formulation")]
		public int ImputationFormulation{ get; set; }

		[Column("imputation_date")]
		public DateTime? ImputationDate{ get; set; }

		[Column("linkage_process")]
		public int LinkageProcess{ get; set; }

		[Column("linkage_room")]
		public String LinkageRoom{ get; set; }

		[Column("linkage_date")]
		public DateTime? LinkageDate{ get; set; }

		[Column("ext_date")]
		public DateTime? ExtDate{ get; set; }

		[Column("linkage_time")]
		public DateTime? LinkageTime{ get; set; }

		[Column("arrangement_type")]
		public int ArrangementType{ get; set; }

		[Column("national_arrangement")]
		public Boolean NationalArrangement{ get; set; }

	}
}