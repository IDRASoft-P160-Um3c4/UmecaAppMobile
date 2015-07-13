using System;
using System.Collections.Generic;

namespace UmecaApp
{
	public class TabletVerificationDto
	{
		public TabletVerificationDto ()
		{
		}

		public TabletVerificationDto( int?  id, DateTime? dateComplete, DateTime? dateCreate,
			int?  idUsr, String fullnameUsr,
			int?  idStV, String nameStV, String descriptionStV) {
//			SimpleDateFormat sdf = new SimpleDateFormat("yyyy/MM/dd");
			this.id = id;
			this.dateComplete = dateComplete == null ? null : String.Format("{0:yyyy/MM/dd}", dateComplete);
			this.dateCreate = dateCreate == null ? null : String.Format("{0:yyyy/MM/dd}", dateCreate);;

			if (idUsr != null) {
				this.reviewer = new TabletUserDto(idUsr, fullnameUsr);
			}

			if (idStV != null) {
				this.status = new TabletStatusVerificationDto(idStV, nameStV, descriptionStV);
			}
		}

		public  int?  id{ get; set; }
		public String dateComplete{ get; set; }
		public String dateCreate{ get; set; }
		public TabletUserDto reviewer{ get; set; }
		public TabletStatusVerificationDto status{ get; set; }
		public List<TabletSourceVerificationDto> sourceVerifications{ get; set; }

	}
}

