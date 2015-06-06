using System;

namespace UmecaApp
{
	public class HearingFormatGrid
	{
		public HearingFormatGrid ()
		{
		}

		public HearingFormatGrid(int? id, Boolean isFinished, String idFolder, String idMP, String name, String lastNP, String lastNM, int? hType, int? ext, int? pVinc, DateTime registerTime, String userName,int idCase) {
			this.Id = id;
			this.IdFolder = idFolder;
			this.IdMP = idMP;
			this.CaseId = idCase;
			var sb = "";
			sb += name;
			sb += " ";
			sb += lastNP;
			sb += " ";
			sb += lastNM;

			this.Fullname = sb;

			this.UserName = userName;

			if (hType != null) {

				if (hType==Constants.HEARING_TYPE_MC)
					HearingType = "MC";
				if (hType==Constants.HEARING_TYPE_SCP)
					HearingType = "SCPP";
			} else {
				HearingType = "NA";
			}

			if (pVinc != null && pVinc==Constants.PROCESS_VINC_NO)
				ProcessVinc = "No";
			else if (pVinc != null && pVinc==Constants.PROCESS_VINC_YES)
				ProcessVinc = "Si";
			if (pVinc != null && pVinc==Constants.PROCESS_VINC_NO_REGISTER)
				ProcessVinc = "Sin registro";

			if (ext != null && ext==Constants.EXTENSION_144)
				Extension = "144 hrs";
			else if (ext != null && ext==Constants.EXTENSION_72)
				Extension = "72 hrs";
			else if (ext != null && ext==Constants.EXTENSION_NO)
				Extension = "No";
			else if (ext == null)
				Extension = "NA";
			this.RegisterTime = registerTime.ToString (Constants.FORMAT_CALENDAR_I);

			this.IsFinished = isFinished;

			if (IsFinished == true)
				this.FinishedStr = "Si";
			else if (isFinished == false)
				this.FinishedStr = "No";
		}

		public int? Id{ get; set; }

		public String IdFolder{ get; set; }

		public String IdMP{ get; set; }

		public String Fullname{ get; set; }

		public String HearingType{ get; set; }

		public String Extension{ get; set; }

		public String ProcessVinc{ get; set; }

		public String RegisterTime{ get; set; }

		public String UserName{ get; set; }

		public Boolean IsFinished{ get; set; }

		public String FinishedStr{ get; set; }

		public int CaseId{ get; set; }


	}
}

