using System;

 using SQLite;
using Umeca.Data;

namespace UmecaApp
{

	public class SelectList  
	{
		public SelectList ()
		{
		}

		public int id{ get; set; }
		public int idAux{ get; set; }
		public DateTime calendar{ get; set; }
		public String name{ get; set; }
		public String description{ get; set; }
		public int aux{ get; set; }
		public Boolean Lock{ get; set; }
		public Boolean specification{ get; set; }
		public String strDate{ get; set; }
		public String logType{ get; set; }


		public SelectList(int id, String name) {
			this.id = id;
			this.name = name;
		}

		public SelectList(int id, Boolean Lock) {
			this.id = id;
			this.Lock = Lock;
		}

		public SelectList(int id, String name, String description) {
			this.id = id;
			this.name = name;
			this.description = description;
		}

		public SelectList(int id, DateTime calendar) {
			this.idAux = id;
			this.calendar = calendar;
		}

		public SelectList(int id, String name, String description, String secDescription) {
			this.id = id;
			this.name = description + " / " + name;
			this.description = secDescription;
		}

		public SelectList(int id, String arrangement, String description, int typeArrangement) {
			this.id = id;
			this.description = description;
			this.name = arrangement;
			if (typeArrangement.Equals(Constants.HEARING_TYPE_SCP))
				this.name += " - SCP";
			else if (typeArrangement.Equals(Constants.HEARING_TYPE_MC))
				this.name += " - MC";

			this.name += " / " + description;
		}

		public SelectList(int id, String description, Boolean Lock, Boolean specification) {
			this.id = id;
			this.description = description;
			this.Lock = Lock;
					this.specification = specification;
		}

		public SelectList(int id, String name, Boolean specification) {
			this.id = id;
			this.name = name;
			this.specification = specification;
		}

		public SelectList(String name, String description, String strDate, String logType) {
			this.name = name;
			this.description = description;
			this.strDate = strDate;
			this.logType = logType;
		}

		public SelectList(int id, int idAux) {
			this.id = id;
			this.idAux = idAux;
		}

		public SelectList(int id, int aux, String code, String status) {
			this.id = id;
			this.aux = aux;
			this.description = code;
			this.name = status;
		}

	}
}