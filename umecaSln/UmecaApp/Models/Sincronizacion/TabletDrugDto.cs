using System;

namespace UmecaApp
{
	public class TabletDrugDto
	{
		public TabletDrugDto ()
		{
		}

		public TabletDrugDto( int?  id, String quantity, DateTime? lastUse, Boolean? block, String specificationType, String specificationPeriodicity, String onsetAge,
			 int? idDT, String nameDT, Boolean specificationDT, Boolean isObsoleteDT,
			 int? idP, String nameP, Boolean isObsoleteP, Boolean specificationP) {
			this.id = id;
			this.webId = id;
			this.quantity = quantity;
			this.lastUse = lastUse == null ? null : String.Format("{0:yyyy/MM/dd}", lastUse);
			this.block = block;
			this.specificationType = specificationType;
			this.specificationPeriodicity = specificationPeriodicity;
			this.onsetAge = onsetAge;

			if (idDT != null) {
				this.drugType = new TabletDrugTypeDto(idDT, nameDT, specificationDT, isObsoleteDT);
			}

			if (idP != null) {
				this.periodicity = new TabletPeriodicityDto(idP, nameP, isObsoleteP, specificationP);
			}
		}

		public  long?  webId{ get; set; }
		public  int?  id{ get; set; }
		public String quantity{ get; set; }
		public String lastUse{ get; set; }
		public Boolean? block{ get; set; }
		public String specificationType{ get; set; }
		public String specificationPeriodicity{ get; set; }
		public String onsetAge{ get; set; }
		public TabletDrugTypeDto drugType{ get; set; }
		public TabletPeriodicityDto periodicity{ get; set; }

	}
}

