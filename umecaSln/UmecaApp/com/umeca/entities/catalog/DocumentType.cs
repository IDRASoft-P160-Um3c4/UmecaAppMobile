using System;
using SQLite.Net.Attributes;
namespace UmecaApp
{
	[Table("cat_document_type")]
	public class DocumentType
	{
		public DocumentType ()
		{
		}

		[AutoIncrement,PrimaryKey]
		public int Id{get;set;}

		[Column("document_type"), MaxLength(255)]
		public String Name{get;set;}

		[Column("specification")]
		public Boolean? Specification{get;set;}

		[Column("is_obsolete")]
		public Boolean? IsObsolete{get;set;}

		}
}

