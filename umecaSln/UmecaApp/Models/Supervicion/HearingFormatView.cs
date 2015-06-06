using System;

namespace UmecaApp
{
	public class HearingFormatView
	{
		public HearingFormatView ()
		{
		}

		public int? IdCase{ get; set; }

		public Boolean IsView{ get; set; }

		public int? IdFormat{ get; set; }

		public Boolean CanSave{ get; set; }

		public Boolean CanEdit{ get; set; }

		public Boolean DisableAll{ get; set; }

		public String IdFolder{ get; set; }

		public String IdJudicial{ get; set; }

		public DateTime AppointmentDate{ get; set; }

		public String Room{ get; set; }

		public DateTime InitTime{ get; set; }

		public String InitTimeStr{ get; set; }

		public DateTime EndTime{ get; set; }

		public String EndTimeStr{ get; set; }

		public String JudgeName{ get; set; }

		public String MpName{ get; set; }

		public String DefenderName{ get; set; }

		public String ImputedName{ get; set; }

		public String ImputedFLastName{ get; set; }

		public String ImputedSLastName{ get; set; }

		public DateTime ImputedBirthDate{ get; set; }

		public String ImputedBirthDateStr{ get; set; }

		public String ImputedTel{ get; set; }

		public int? ControlDetention{ get; set; }

		public int? Extension{ get; set; }

		public int? ImpForm{ get; set; }

		public DateTime ImputationDate{ get; set; }

		public String ImputationDateStr{ get; set; }

		public DateTime ExtDate{ get; set; }

		public String ExtDateStr{ get; set; }

		public int? VincProcess{ get; set; }

		public String LinkageRoom{ get; set; }

		public DateTime LinkageDate{ get; set; }

		public String LinkageDateStr{ get; set; }

		public DateTime LinkageTime{ get; set; }

		public String LinkageTimeStr{ get; set; }

		public int? ArrangementType{ get; set; }

		public Boolean NationalArrangement{ get; set; }

		public String AdditionalData{ get; set; }

		public String ListCrime{ get; set; }

		public String Terms{ get; set; }

		public String LstArrangement{ get; set; }

		public String Street{ get; set; }

		public String OutNum{ get; set; }

		public String InnNum{ get; set; }

		public LocationDto Location{ get; set; }

		public int? IdAddres{ get; set; }

		public String LstContactData{ get; set; }

		public String HearingFormatType{ get; set; }

		public String UserName{ get; set; }

		public String ConfirmComment{ get; set; }

		public String CredPass{ get; set; }

		public String Lat{ get; set; }

		public String Lng{ get; set; }

		public Boolean IsFinished{ get; set; }

		public Boolean HasPrevHF{ get; set; }

		public Boolean IsFirstFormat{ get; set; }

		public String Ccomments{ get; set; }

		public DateTime UmecaDate{ get; set; }

		public String UmecaDateStr{ get; set; }

		public DateTime UmecaTime{ get; set; }

		public String UmecaTimeStr{ get; set; }

		public int? UmecaSupervisorId{ get; set; }

		public int? HearingTypeId{ get; set; }

		public String HearingTypeSpecification{ get; set; }

		public int? ImputedPresence{ get; set; }

		public String HearingResult{ get; set; }

		public int? PreviousHearing{ get; set; }

	}
}

