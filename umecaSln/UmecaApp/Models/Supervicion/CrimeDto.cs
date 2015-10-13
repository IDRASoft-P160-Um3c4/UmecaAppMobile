using System;

 using SQLite;
using Umeca.Data;

namespace UmecaApp
{

	public class CrimeDto
	{
		public CrimeDto ()
		{
		}

		public String article{ get; set; }

		public String comment{ get; set; }

		public CatalogDto crime{ get; set; }

		public CatalogDto federal{ get; set; }

		public Crime ToCrime(){
			var crimen = new Crime ();
			crimen.Article = this.article;
			crimen.Comment = this.comment;
			crimen.IdCrimeCat = this.crime.id;
			crimen.Federal = this.federal.id;
			return crimen;
		}
	}
}