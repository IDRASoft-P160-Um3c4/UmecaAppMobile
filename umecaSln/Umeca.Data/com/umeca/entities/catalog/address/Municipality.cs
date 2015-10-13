using System;
 using SQLite;

namespace UmecaApp
{
	[Table("cat_municipality")]
	public class Municipality
	{
		public Municipality ()
		{
		}

		public Municipality (int id, int state, String name,String abreviacion,String descripcion)
		{
			this.Abbreviation = abreviacion;
			this.Description = descripcion;
			this.Id = id;
			this.Name = name;
			this.StateId = state;
		}

		[PrimaryKey]
		public int Id{ get; set;}

		[Column("id_state")]
		public int StateId{get;set;}

		[Column("name"), MaxLength(50)]
		public String Name{ get; set;}

		[Column("abbreviation"), MaxLength(100)]
		public String Abbreviation{get; set;}


		[Column("description"), MaxLength(100)]
		public String Description{get; set;}

	}
}

