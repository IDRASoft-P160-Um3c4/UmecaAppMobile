using System;
using System.Collections.Generic;

namespace UmecaApp
{
	public class TabletHearingFormatDto
	{
		public TabletHearingFormatDto ()
		{
		}

		public TabletHearingFormatDto(int id, DateTime? registerTime, String idFolder, String idJudicial, String room, DateTime? appointmentDateTime, DateTime? initTime, DateTime? endTime,
			String judgeName, String mpName, String defenderName, String terms, String confirmComment, Boolean? isFinished, String comments, DateTime? umecaDate, DateTime? umecaTime, String hearingTypeSpecification, int imputedPresence, String hearingResult, int previousHearing, Boolean? showNotification,
			int idHT, String descriptionHT, Boolean? isObsoleteHT, Boolean? lockHT, Boolean? specificationHT,
			int idHFS, int controlDetentionHFS, int extensionHFS, int imputationFormulationHFS, DateTime? imputationDateTimeHFS, int linkageProcessHFS, String linkageRoomHFS, DateTime? linkageDateTimeHFS, DateTime? extDateTimeHFS, DateTime? linkageTimeHFS, int arrangementTypeHFS, Boolean? nationalArrangementHFS,
			int idI, String nameI, String lastNamePI, String lastNameMI, DateTime? birthDateTimeI, String imputeTelI,
			int idA, String streetA, String outNumA, String innNumA, String latA, String lngA, String addressStringA,
			int idL, String nameL, String abbreviationL, String descriptionL, String zipCodeL) {

			this.id = id;
			this.registerTime = registerTime == null ? null : String.Format("{0:yyyy/MM/dd HH:mm:ss}", registerTime);
			this.idFolder = idFolder;
			this.idJudicial = idJudicial;
			this.room = room;
			this.appointmentDateTime = appointmentDateTime == null ? null : String.Format("{0:yyyy/MM/dd}", appointmentDateTime);
			this.initTime = initTime == null ? null : String.Format("{0:HH:mm:ss}", initTime);
			this.endTime = endTime == null ? null : String.Format("{0:HH:mm:ss}", endTime);
			this.judgeName = judgeName;
			this.mpName = mpName;
			this.defenderName = defenderName;
			this.terms = terms;
			this.confirmComment = confirmComment;
			this.isFinished = isFinished;
			this.comments = comments;
			this.umecaDate = umecaDate == null ? null : String.Format("{0:yyyy/MM/dd}", umecaDate);
			this.umecaTime = umecaTime == null ? null : String.Format("{0:HH:mm:ss}", umecaTime);
			this.hearingTypeSpecification = hearingTypeSpecification;
			this.imputedPresence = imputedPresence;
			this.hearingResult = hearingResult;
			this.previousHearing = previousHearing;
			this.showNotification = showNotification;

			if (idHT != null) {
				this.hearingType = new TabletHearingTypeDto(idHT, descriptionHT, isObsoleteHT, lockHT, specificationHT);
			}

			if (idHFS != null) {
				this.hearingFormatSpecs = new TabletHearingFormatSpecsDto(idHFS, controlDetentionHFS, extensionHFS, imputationFormulationHFS, imputationDateTimeHFS, linkageProcessHFS, linkageRoomHFS, linkageDateTimeHFS, extDateTimeHFS, linkageTimeHFS, arrangementTypeHFS, nationalArrangementHFS??false);
			}

			if (idI != null) {
				this.hearingImputed = new TabletHearingFormatImputedDto(idI, nameI, lastNamePI, lastNameMI, birthDateTimeI, imputeTelI,
					idA, streetA, outNumA, innNumA, latA, lngA, addressStringA,
					idL, nameL, abbreviationL, descriptionL, zipCodeL);
			}

		}

		public TabletHearingFormatDto(HearingFormatView hfV) {

			this.idFolder = hfV.idFolder;
			this.idJudicial = hfV.idJudicial;

			TabletHearingFormatImputedDto hfImp = new TabletHearingFormatImputedDto();
			hfImp.name = hfV.imputedName;
			hfImp.lastNameP = hfV.imputedFLastName;
			hfImp.lastNameM = hfV.imputedSLastName;
			hfImp.birthDate = hfV.imputedBirthDate == null ? null : String.Format("{0:yyyy/MM/dd}", hfV.imputedBirthDate);
			this.hearingImputed = hfImp;
		}

		public int id{ get; set; }
		public String registerTime{ get; set; }
		public String idFolder{ get; set; }
		public String idJudicial{ get; set; }
		public String room{ get; set; }
		public String appointmentDateTime{ get; set; }
		public String initTime{ get; set; }
		public String endTime{ get; set; }
		public String judgeName{ get; set; }
		public String mpName{ get; set; }
		public String defenderName{ get; set; }
		public String terms{ get; set; }
		public String confirmComment{ get; set; }
		public Boolean? isFinished{ get; set; }
		public String comments{ get; set; }
		public String umecaDate{ get; set; }
		public String umecaTime{ get; set; }
		public String hearingTypeSpecification{ get; set; }
		public int imputedPresence{ get; set; }
		public String hearingResult{ get; set; }
		public int previousHearing{ get; set; }
		public Boolean? showNotification{ get; set; }
		public TabletHearingTypeDto hearingType{ get; set; }
		public TabletHearingFormatSpecsDto hearingFormatSpecs{ get; set; }
		public TabletHearingFormatImputedDto hearingImputed{ get; set; }
		public List<TabletAssignedArrangementDto> assignedArrangements{ get; set; }
		public List<TabletContactDataDto> contacts{ get; set; }
		public List<TabletCrimeDto> crimeList{ get; set; }
		public TabletUserDto umecaSupervisor{ get; set; }
		public TabletUserDto supervisor{ get; set; }

	}
}

