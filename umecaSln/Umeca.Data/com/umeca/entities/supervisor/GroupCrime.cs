using System;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;

namespace Umeca.Data
{
	[Table("cat_group_crime")]
	public class GroupCrime
	{
		public GroupCrime ()
		{
		}

		[PrimaryKey, Column("id_group")]
		public int Id{ get; set; }

		[Column("name"), MaxLength(255)]
		public String Name{ get; set; }

		[Column("description"), MaxLength(255)]
		public String Description{ get; set; }

		[Column("is_obsolete")]
		public Boolean IsObsolete{ get; set; }

	}
}