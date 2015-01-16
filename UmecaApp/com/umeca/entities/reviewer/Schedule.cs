using System;
using SQLite;

namespace UmecaApp
{
	[Table("schedule")]
	public class Schedule
	{
		public Schedule ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id")]
		public int Id{ get; set; }

		[Column("imputed_home_id"),MaxLength(50)]
		public String Day{ get; set; }

		[Column("imputed_home_id"),MaxLength(5)]
		public String Start{ get; set; }

		[Column("imputed_home_id"),MaxLength(5)]
		public String End{ get; set; }
	}
}

