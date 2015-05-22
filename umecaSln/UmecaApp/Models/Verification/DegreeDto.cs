
using System;

namespace UmecaApp
{
	public class DegreeDto
	{
		public int id{ get; set;}

		public String name{ get; set;}

		public DegreeDto ()
		{
		}

		public DegreeDto dtoGrade(int Id, String Name){
			this.id = Id;
			this.name= Name;
			return this;
		}

	}
}

