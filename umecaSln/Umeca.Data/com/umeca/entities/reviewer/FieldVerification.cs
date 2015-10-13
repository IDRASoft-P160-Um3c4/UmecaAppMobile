using System;

 using SQLite;

namespace UmecaApp
{
	[Table("field_verification")]
	public class FieldVerification
	{

		public FieldVerification ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id_field_verification")]
		public int Id{ get; set; }

		[Column("code"), MaxLength(255),Unique()]
		public String Code{get;set;}

		[Column("section"), MaxLength(255)]
		public String Section{get;set;}

		[Column("section_code")]
		public int? SectionCode{get;set;}

		[Column("field_name"), MaxLength(255)]
		public String FieldName{get;set;}

		[Column("index_field")]
		public int? IndexField{get;set;}

		[Column("is_obosolete")]
		public Boolean IsObsolete{get;set;}

		[Column("id_subsection")]
		public int? IdSubsection{get;set;}

		[Column("type_field"), MaxLength(255)]
		public String Type{get;set;}

	}
}

