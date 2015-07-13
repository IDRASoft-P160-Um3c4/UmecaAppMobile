using System;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;
using Umeca.Data;

namespace UmecaApp
{

	public class StateDto
	{
		public StateDto ()
		{
		}

		public StateDto (State state) {
			this.id = state.Id;
			this.name = state.Name;
		}

		public int id{ get; set; }

		public String name{ get; set; }

	}
}