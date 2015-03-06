using System;

namespace UmecaApp
{
	public class IdNameDto
	{
		public int Id{ get; set;}

		public String Name{ get; set;}

		public Boolean Disabled { get; set;}

		public String Reference { get; set;}

		public IdNameDto ()
		{
			this.Disabled = false;
		}

		public IdNameDto (int id, String name)
		{
			this.Id = id;
			this.Name = name;
			this.Disabled = false;
		}

		public IdNameDto (int id, String name, Boolean disable)
		{
			this.Id = id;
			this.Name = name;
			this.Disabled = disable;
		}

	}
}

