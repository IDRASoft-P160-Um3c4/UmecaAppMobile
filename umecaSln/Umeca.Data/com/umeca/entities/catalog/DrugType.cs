using System;
using SQLite.Net.Attributes;

namespace UmecaApp
{
	[Table("cat_drug_type")]
	public class DrugType
	{
		public DrugType ()
		{
		}

		[AutoIncrement,PrimaryKey]
		public int Id{get;set;}

		[Column("drug"), MaxLength(255)]
		public String Name{get;set;}

		[Column("specification")]
		public Boolean? Specification{get;set;}

		[Column("is_obsolete")]
		public Boolean? IsObsolete{get;set;}

	}
}

