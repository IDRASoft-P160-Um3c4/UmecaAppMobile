using System;
using System.Collections.Generic;

namespace UmecaApp
{
	public class TabletCaseDto
	{
		public TabletCaseDto ()
		{
		}


		public TabletCaseDto(int id, String idFolder, String idMP, Boolean recidivist, DateTime? dateNotProsecute, DateTime? dateObsolete, DateTime? dateCreate) {
			this.id = id;
			this.webId = id;
			this.idFolder = idFolder;
			this.idMP = idMP;
			this.recidivist = recidivist;
			this.dateNotProsecute = dateNotProsecute == null ? null : String.Format("{0:yyyy/MM/dd}", dateNotProsecute);
			this.dateObsolete = dateObsolete == null ? null : String.Format("{0:yyyy/MM/dd}", dateObsolete);
			this.dateCreate = dateCreate == null ? null : String.Format("{0:yyyy/MM/dd}", dateCreate);

		}

		public int webId{ get; set; }
		public int id{ get; set; }
		public String idFolder{ get; set; }
		public String idMP{ get; set; }
		public Boolean recidivist{ get; set; }
		public String dateNotProsecute;
		public String dateObsolete{ get; set; }
		public String dateCreate{ get; set; }
		public TabletStatusCaseDto status{ get; set; }
		public TabletMeetingDto meeting{ get; set; }
		public TabletVerificationDto verification{ get; set; }
		public List<TabletHearingFormatDto> hearingFormats{ get; set; }
		public List<TabletLogCaseDto> logCase{ get; set; }

	}
}

