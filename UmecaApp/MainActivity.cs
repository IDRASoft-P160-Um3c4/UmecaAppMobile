using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using SQLite;

using System.Linq;
using System.Threading.Tasks;
using Environment = System.Environment;
using System.Collections.Generic;



namespace UmecaApp
{
	[Activity (Label = "UmecaApp", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			TextView textV = FindViewById<TextView> (Resource.Id.myTextView);
			Console.WriteLine ("Creating database, if it doesn't already exist");
			string dbPath = Path.Combine (
				Environment.GetFolderPath (Environment.SpecialFolder.Personal),
				"demoDos.db3");
			var db = new SQLiteConnection (dbPath);
			db.CreateTable<Person> ();
			db.CreateTable<Contact> ();
			if (db.Table<Person> ().Count() == 0) {
				// only insert the data if it doesn't already exist
				var newPerson = new Person ();
				newPerson.FirstName = "nombre persona uno";
				newPerson.LastName = "segundo persona uno";
				db.Insert (newPerson); 
				var newContact = new Contact ();
				newContact.PersonId = newPerson.Id;
				newContact.Text = "contacto uno persona uno";
				db.Insert (newContact);
				var newPerson2 = new Person ();
				newPerson2.FirstName = "nombre persona dos";
				newPerson2.LastName = "segundo persona dos";
				db.Insert (newPerson2); 
				var newContact2 = new Contact ();
				newContact2.PersonId = newPerson2.Id;
				newContact2.Text = "contacto uno persona Dos";
				db.Insert (newContact2);
				var newContact3 = new Contact ();
				newContact3.PersonId = newPerson2.Id;
				newContact3.Text = "contacto dos persona Dos";
				db.Insert (newContact3);
			}
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
				Console.WriteLine("Reading data");
				var tableContact = db.Table<Contact>().ToList();
				Console.WriteLine("contactos_------------->");
				foreach(var c in tableContact){
					Console.WriteLine("id:"+c.Id+", personid"+c.PersonId+", text:"+c.Text);
				}
				Console.WriteLine("///////////////////////////////////////");

				var tablePerson = db.Table<Person> ().ToList();
				foreach (var p in tablePerson) {
					Console.WriteLine ("id: "+p.Id + " ,firstName: " + p.FirstName + " ,lastName:"+p.LastName);
					var contactByPerson = from c in db.Table<Contact>()
							where c.PersonId.Equals(p.Id)
						select c;
					var cbyp = contactByPerson.ToList();
					Console.WriteLine("numero contactos: "+cbyp.Count);
					foreach(var c in cbyp){
						Console.WriteLine("id: "+c.Id+" ,personId: "+c.PersonId+" ,text: "+c.Text);
					}
				}
			};


		}
	}
}