using System;

namespace UmecaApp
{
	public class TabletFieldVerificationDto
	{
		public TabletFieldVerificationDto ()
		{
		}

		public TabletFieldVerificationDto(int? id, String code, String section, int? sectionCode, String fieldName, int? indexField, Boolean? isObsolete, int? idSubsection, String type) {
			this.id = id;
			this.code = code;
			this.section = section;
			this.sectionCode = sectionCode;
			this.fieldName = fieldName;
			this.indexField = indexField;
			this.isObsolete = isObsolete;
			this.idSubsection = idSubsection;
			this.type = type;
		}

		public int? id{ get; set ; }
		public String code{ get; set ; }
		public String section{ get; set ; }
		public int? sectionCode{ get; set ; }
		public String fieldName{ get; set ; }
		public int? indexField{ get; set ; }
		public Boolean? isObsolete{ get; set ; }
		public int? idSubsection{ get; set ; }
		public String type{ get; set ; }

	}
}

