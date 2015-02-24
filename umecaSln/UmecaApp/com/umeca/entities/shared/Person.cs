using System;

using SQLite.Net.Attributes;

namespace UmecaApp
{
	[Table("person")]
	public class Person
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		[Column("firstName")]
		public string FirstName { get; set; }

		[Column("lastName")]
		public string LastName { get; set; }

		public override string ToString()
		{
			return string.Format("[Person: ID={0}, FirstName={1}, LastName={2}]", Id, FirstName, LastName);
		}

		public Person (int id, string firstName, string lastName)
		{
			this.Id = id;
			this.FirstName = firstName;
			this.LastName = lastName;
		}

		public Person(){
		}
	}
}

