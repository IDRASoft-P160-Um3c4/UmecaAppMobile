using System;

namespace UmecaApp
{
	public class TabletVerificationMethodDto
	{
		public TabletVerificationMethodDto ()
		{
		}

		public TabletVerificationMethodDto( int?  id, String name, Boolean?  isObsolete) {
			this.id = id;
			this.name = name;
			this.isObsolete = isObsolete;
		}

		public  int?  id{ get; set; }
		public String name{ get; set; }
		public Boolean?  isObsolete{ get; set; }

	}
}

