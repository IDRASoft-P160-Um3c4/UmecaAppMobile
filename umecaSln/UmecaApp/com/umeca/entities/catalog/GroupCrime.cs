using System;
using SQLite.Net.Attributes;

namespace UmecaApp
{
	[Table("cat_group_crime")]
	public class GroupCrime
	{
		public GroupCrime ()
		{
		}

		[AutoIncrement, PrimaryKey]
		public int Id{get;set;}

		[Column("name"), MaxLength(255)]
		public String Name{ get; set;}

		[Column("description"), MaxLength(255)]
		public String Description{get; set;}

		[Column("is_obsolete")]
		public Boolean IsObsolete{ get; set;}
	}
}

