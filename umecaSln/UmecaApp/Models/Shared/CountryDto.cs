using System;

namespace UmecaApp
{
	public class CountryDto
	{
		public int Id{ get; set;}

		public String Name{ get; set;}

		public CountryDto ()
		{
		}

		public CountryDto dtoCountry(Country country){
			this.Id= country.Id;
			this.Name = country.Name;
			return  this;
		}
	}
}

