using System;

namespace UmecaApp
{
	public class TabletAssignedArrangementDto
	{
		public TabletAssignedArrangementDto ()
		{
		}

		public TabletAssignedArrangementDto(int id, String description,
			int idA, String descriptionA, int typeA, Boolean isNationalA, int indexA, Boolean? isObsoleteA, Boolean? isDefaultA, Boolean? isExclusiveA) {
			this.id = id;
			this.description = description;

			if(idA!=0){
				this.arrangement = new TabletArrangementDto(idA, descriptionA, typeA, isNationalA, indexA, isObsoleteA, isDefaultA, isExclusiveA);
			}
		}

		public int id{ get; set; }
		public String description{ get; set; }
		public TabletArrangementDto arrangement{ get; set; }

	}
}

