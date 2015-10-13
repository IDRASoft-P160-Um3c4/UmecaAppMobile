using System;

 using SQLite;


namespace UmecaApp
{
	[Table("case_detention")]
	public class Case
	{
		public Case ()
		{
		}
		[PrimaryKey, AutoIncrement, Column("id_case")]
		public int Id{ get; set; }

		[Column("id_web")]
		public long? webId{ get; set; }

		[Column("tac")]
		public long? tac{ get; set; }

		[Column("id_folder"),MaxLength(35)]
		public String IdFolder{ get; set; }

		[Column("id_mp"),MaxLength(35)]
		public String IdMP{ get; set; }

		[Column("recidivist")]
		public Boolean Recidivist{ get; set; }

		//[OneToOne]
		//public Meeting Meeting { get; set; }

		[Column("date_not_prosecute")]
		public DateTime? DateNotProsecute{ get; set; }

		[Column("date_obsolete")]
		public DateTime? DateObsolete{ get; set; }

		[Column("id_status")]
		public int StatusCaseId { get; set; }

		//[ManyToOne]
		//public StatusCase Status { get; set; }

		[Column("date_create")]
		public DateTime? DateCreate{ get; set; }

	}
}

