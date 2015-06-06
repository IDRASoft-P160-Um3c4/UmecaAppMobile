using System;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;

namespace Umeca.Data
{
	[Table("assigned_arrangement")]
	public class AssignedArrangement
	{
		public AssignedArrangement ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id_assigned_arrangement")]
		public int Id{ get; set; }

		[Column("description")]
		public String Description{ get; set; }

		[Column("id_arrangement")]
		public int Arrangement{ get; set; }

		[Column("id_hearing_format")]
		public int HearingFormat{ get; set; }

		//default 0 for false maybe?
		[Column("require_presence")]
		public int RequirePresence{ get; set; }

	}
}