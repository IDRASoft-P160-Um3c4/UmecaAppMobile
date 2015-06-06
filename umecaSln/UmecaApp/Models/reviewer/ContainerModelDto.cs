using System;
//listas
using System.Collections.Generic;

namespace UmecaApp
{
	public class ContainerModelDto
	{
		public ContainerModelDto ()
		{
		}

		public int Id{ get; set; }

		public String Name{ get; set; }

		public List<InnerModelDto> disponibles{ get; set; }
	}
}

