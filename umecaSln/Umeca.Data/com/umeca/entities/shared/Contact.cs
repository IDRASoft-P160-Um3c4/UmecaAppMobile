using System;

 using SQLite;


namespace UmecaApp
{

	[Table("contact")]
	public class Contact
	{
		public Contact ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id")]
		public int Id
		{
			get;
			set;
		}

		[Indexed, Column("personId")]
		public int PersonId
		{
			get;
			set;
		}

		[Column("text")]
		public string Text {
			get;
			set;
		}

		public override string ToString()
		{
			return string.Format("[Contact: ID={0}, PersonId={1}, Text={2}]", this.Id, PersonId,Text);
		}

			
		public Contact (int id, int personId, string text)
		{
			this.Id = id;
			this.PersonId = personId;
			this.Text = text;
		}
		
	}
}

