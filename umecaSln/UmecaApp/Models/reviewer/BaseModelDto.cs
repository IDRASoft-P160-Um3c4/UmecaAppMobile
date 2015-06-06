using System;
//listas
using System.Collections.Generic;

namespace UmecaApp
{
	public class BaseModelDto
	{
		public BaseModelDto ()
		{
		}

		public int Id{ get; set; }

		public String DatosGenerales{ get; set; }

		public List<ContainerModelDto> elementos{ get; set; }
	}
}

