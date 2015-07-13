using System;

namespace UmecaApp
{
	public class TabletImputedDto
	{
		public TabletImputedDto ()
		{
		}

		public TabletImputedDto(int id, String name, String lastNameP, String lastNameM, String foneticString, Boolean gender, DateTime? birthDate, String celPhone, int yearsMaritalStatus, int boys, int dependentBoys, String birthMunicipality, String birthState, String birthLocation, String nickname) {
			this.id = id;
			this.webId = id;
			this.name = name;
			this.lastNameP = lastNameP;
			this.lastNameM = lastNameM;
			this.foneticString = foneticString;
			this.gender = gender;
			this.birthDate = birthDate == null ? null : String.Format("{0:yyyy/MM/dd}", birthDate);
			this.celPhone = celPhone;
			this.yearsMaritalStatus = yearsMaritalStatus;
			this.boys = boys;
			this.dependentBoys = dependentBoys;
			this.birthMunicipality = birthMunicipality;
			this.birthState = birthState;
			this.birthLocation = birthLocation;
			this.nickname = nickname;
		}

		public int webId{ get; set; }
		public int id{ get; set; }
		public String name{ get; set; }
		public String lastNameP{ get; set; }
		public String lastNameM{ get; set; }
		public String foneticString{ get; set; }
		public Boolean gender{ get; set; }
		public String birthDate{ get; set; }
		public String celPhone{ get; set; }
		public int yearsMaritalStatus{ get; set; }
		public int boys{ get; set; }
		public int dependentBoys{ get; set; }
		public String birthMunicipality{ get; set; }
		public String birthState{ get; set; }
		public String birthLocation{ get; set; }
		public String nickname{ get; set; }
		public TabletMaritalStatusDto maritalStatus{ get; set; }
		public TabletCountryDto birthCountry{ get; set; }
		public TabletLocationDto location{ get; set; }

	}
}

