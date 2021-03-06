﻿using System;
using SQLite;

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

		[Column("address_id")]
			public int AddressId{ get; set; }

		[Column("name")]
			public int RegisterType{ get; set; }

		[Column("time_live"),MaxLength(30)]
			public String TimeLive{ get; set; }

		[Column("id_beint")]
			public int HomeTypeId{ get; set; }

		[Column("reason_change"),MaxLength(500)]
			public String ReasonChange{ get; set; }

		[Column("description"),MaxLength(500)]
			public String Description{ get; set; }

		[Column("phone"),MaxLength(200)]
			public String Phone{ get; set; }

		[Column("id_meeting")]
			public int Meeting{ get; set; }

		[Column("specification"),MaxLength(50)]
			public String Specification{ get; set; }

		[Column("reasonSecondary"),MaxLength(500)]
			public String ReasonSecondary{ get; set; }

	}
}

