using System;
 using SQLite;
namespace Umeca.Data
{

	public class DocumentType
	{
		[PrimaryKey, AutoIncrement]
		public int Id{get;set;}

		[MaxLength(255)]
		public String Name{get;set;}

		public Boolean? Specification{get;set;}

		public Boolean? IsObsolete{get;set;}

	}
}

