using System;

namespace UmecaApp
{
	public class TabletCrimeDto
	{
		public TabletCrimeDto ()
		{
		}


		public TabletCrimeDto(int id, String comment, String article,
			int idE, String nameE,
			int idC, String nameC, String descriptionC, Boolean obsoleteC) {
			this.id = id;
			this.comment = comment;
			this.article = article;

			if (idE != null) {
				this.federal = new TabletElectionDto(idE, nameE);
			}

			if (idC != null) {
				this.crime = new TabletCrimeCatalogDto(idC, nameC, descriptionC, obsoleteC);
			}
		}

		public int id{ get; set; }
		public String comment{ get; set; }
		public String article{ get; set; }
		public TabletElectionDto federal{ get; set; }
		public TabletCrimeCatalogDto crime{ get; set; }

	}
}

