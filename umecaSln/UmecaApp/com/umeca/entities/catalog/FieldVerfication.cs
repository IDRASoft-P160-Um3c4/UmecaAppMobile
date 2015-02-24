using System;
using SQLite.Net.Attributes;
namespace UmecaApp
{
	[Table("cat_field_verification")]
	public class FieldVerfication
	{
		public FieldVerfication ()
		{
		}

		[AutoIncrement,PrimaryKey]
		public int Id{get;set;}

		[Column("code"), MaxLength(255)]
		public String Code{get;set;}

		[Column("section_name"), MaxLength(255)]
		public String SectionName{get;set;}

		[Column("section_code")]
		public int SectionCode{get;set;}

		[Column("field_name"), MaxLength(255)]
		public String FieldName{get;set;}

		[Column("index_field")]
		public int IndexField{get;set;}

		[Column("is_obosolete")]
		public Boolean IsObsolete{get;set;}

		[Column("id_subsection")]
		public int IdSubsection{get;set;}

		[Column("type"), MaxLength(255)]
		public String Type{get;set;}


	}
}

