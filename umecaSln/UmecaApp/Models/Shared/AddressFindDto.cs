using System;
//listas
using System.Collections.Generic;

namespace UmecaApp
{
	public class AddressFindDto
	{

		public String zipCode{ get; set;}

		public String StateId{ get; set;}

		public String MunicipalityId{ get; set;}

		public String LocationId{ get; set;}

		public List<Municipality> Municipios{ get; set;}

		public List<Location> Locaciones{ get; set;}

		public AddressFindDto ()
		{
		}
	}
}

