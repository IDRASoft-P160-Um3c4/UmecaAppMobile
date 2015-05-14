using System;

using SQLite.Net.Attributes;

namespace UmecaApp
{
	[Table("cat_state")]
	public class State
	{
		public State ()
		{
		}

		public State (int id, long countryId,  String name,String abreviacion,String descripcion)
		{
			this.Abbreviation = abreviacion;
			this.Description = descripcion;
			this.Id = id;
			this.Name = name;
			this.CountryId = countryId;
		}

		[PrimaryKey]
		public int Id{ get; set;}

		[Column("id_country")]
		public long CountryId{get;set;}

		[Column("name"), MaxLength(50)]
		public String Name{ get; set;}

		[Column("abbreviation"), MaxLength(100)]
		public String Abbreviation{get; set;}


		[Column("description"), MaxLength(100)]
		public String Description{get; set;}

	}
}

