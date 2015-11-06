using System;

namespace UmecaApp
{
	public class HearingFormatView
	{
		public HearingFormatView ()
		{
		}

		public int? idCase{ get; set; }

		public Boolean? isView{ get; set; }

		public int? idFormat{ get; set; }

		public Boolean? canSave{ get; set; }

		public Boolean? canEdit{ get; set; }

		public Boolean? disableAll{ get; set; }

		public String idFolder{ get; set; }

		public String idJudicial{ get; set; }

		public DateTime? appointmentDate{ get; set; }

		public String appointmentDateStr{ get; set; }

		public String room{ get; set; }

		public DateTime? initTime{ get; set; }

		public String initTimeStr{ get; set; }

		public DateTime? endTime{ get; set; }

		public String endTimeStr{ get; set; }

		public String judgeName{ get; set; }

		public String mpName{ get; set; }

		public String defenderName{ get; set; }

		public String imputedName{ get; set; }

		public String imputedFLastName{ get; set; }

		public String imputedSLastName{ get; set; }

		public DateTime? imputedBirthDate{ get; set; }

		public String imputedBirthDateStr{ get; set; }

		public String imputedTel{ get; set; }

		public int? controlDetention{ get; set; }

		public int? extension{ get; set; }

		public int? impForm{ get; set; }

		public DateTime? imputationDate{ get; set; }

		public String imputationDateStr{ get; set; }

		public DateTime? extDate{ get; set; }

		public String extDateStr{ get; set; }

		public int? vincProcess{ get; set; }

		public String linkageRoom{ get; set; }

		public DateTime? linkageDate{ get; set; }

		public String linkageDateStr{ get; set; }

		public DateTime? linkageTime{ get; set; }

		public String linkageTimeStr{ get; set; }

		public int? arrangementType{ get; set; }

		public Boolean? nationalArrangement{ get; set; }

		public String additionalData{ get; set; }

		public String listCrime{ get; set; }

		public String terms{ get; set; }

		public String lstArrangement{ get; set; }

		public String street{ get; set; }

		public String outNum{ get; set; }

		public String innNum{ get; set; }

		public LocationDto location{ get; set; }

		public int? idAddres{ get; set; }

		public String lstContactData{ get; set; }

		public String hearingFormatType{ get; set; }

		public String userName{ get; set; }

		public String confirmComment{ get; set; }

		public String credPass{ get; set; }

		public String lat{ get; set; }

		public String lng{ get; set; }

		public Boolean? isFinished{ get; set; }

		public Boolean? hasPrevHF{ get; set; }

		public Boolean? isFirstFormat{ get; set; }

		public String comments{ get; set; }

		public DateTime? umecaDate{ get; set; }

		public String umecaDateStr{ get; set; }

		public DateTime? umecaTime{ get; set; }

		public String umecaTimeStr{ get; set; }

		public int? umecaSupervisorId{ get; set; }

		public int? hearingTypeId{ get; set; }

		public String hearingTypeSpecification{ get; set; }

		public int? imputedPresence{ get; set; }

		public String hearingResult{ get; set; }

		public int? previousHearing{ get; set; }


		public String TimeAgo{ get; set; }


		public String LocationPlace{ get; set; }

		public Boolean IsHomeless{ get; set; }

		public int? District{ get; set; }

		public Boolean? IsSubstracted{ get; set; }

	}
}

