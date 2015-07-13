using System;

namespace UmecaApp
{
	public class TabletHearingFormatSpecsDto
	{
		public TabletHearingFormatSpecsDto ()
		{
		}

		public TabletHearingFormatSpecsDto(int id, int? controlDetention, int? extension, int? imputationFormulation, DateTime? imputationDate, int? linkageProcess, String linkageRoom, DateTime? linkageDate, DateTime? extDate, DateTime? linkageTime, int? arrangementType, Boolean nationalArrangement) {
//			SimpleDateFormat sdfD = new SimpleDateFormat("yyyy/MM/dd");
//			SimpleDateFormat sdfT = new SimpleDateFormat("HH:mm:ss");
			this.id = id;
			this.controlDetention = controlDetention;
			this.extension = extension;
			this.imputationFormulation = imputationFormulation;
			this.imputationDate = imputationDate == null ? null : String.Format("{0:yyyy/MM/dd}", imputationDate);
			this.linkageProcess = linkageProcess;
			this.linkageRoom = linkageRoom;
			this.linkageDate = linkageDate == null ? null : String.Format("{0:yyyy/MM/dd}", linkageDate);
			this.extDate = extDate == null ? null : String.Format("{0:yyyy/MM/dd}", extDate);
			this.linkageTime = linkageTime == null ? null : String.Format("{0:HH:mm:ss}", linkageTime);
			this.arrangementType = arrangementType;
			this.nationalArrangement = nationalArrangement;
		}

		public int id{ get; set; }
		public int? controlDetention{ get; set; }
		public int? extension{ get; set; }
		public int? imputationFormulation{ get; set; }
		public String imputationDate{ get; set; }
		public int? linkageProcess{ get; set; }
		public String linkageRoom{ get; set; }
		public String linkageDate{ get; set; }
		public String extDate{ get; set; }
		public String linkageTime{ get; set; }
		public int? arrangementType{ get; set; }
		public Boolean nationalArrangement{ get; set; }

	}
}

