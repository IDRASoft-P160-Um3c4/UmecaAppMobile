using System;
using Umeca.Data;

//listas
using System.Collections.Generic;

namespace UmecaApp
{
	public class HFDtoSave
	{
		public HFDtoSave ()
		{
		}

		public HearingFormat hearingFormat{ get; set; }

		public HearingFormatImputed hearingFormatImputed{ get; set; }

		public Address addressImputado{ get; set; }

		public HearingFormatSpecs newHearingFormatSpecs{ get; set; }

		public List<AssignedArrangement> newArrangments{ get; set; }

		public List<ContactData> lstContactDataView{ get; set; }

		public List<Crime> crimeList{ get; set; }

		public Boolean? IsSubstracted{ get; set; }
	}
}

