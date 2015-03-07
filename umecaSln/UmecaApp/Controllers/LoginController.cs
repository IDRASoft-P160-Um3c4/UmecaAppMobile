using System;
using PortableRazor;
using Android.Content;
using UmecaApp.Models;
using System.Collections.Generic;

using Java.Interop;
using Newtonsoft.Json;

namespace UmecaApp
{
	public class LoginController //: ControllerBase
	{

		IHybridWebView webView;
		IDataAccess dataAccess;

		public LoginController(IHybridWebView webView, IDataAccess dataAccess)
		{
			this.webView = webView;
			this.dataAccess = dataAccess;
		}

//		public LoginController(IHybridWebView webView, IDataAccess dataAccess) : base(webView, dataAccess) 
//		{
//		}

		public void Index()
		{
			var template = new Home {
				Model = new PageModel {Title = "Título de la página"}
			};
			var page = template.GenerateString ();

			webView.LoadHtmlString (page);
		}

		public void MeetingIndex()
		{
//			Console.WriteLine ("Creating database, if it doesn't already exist");
//			string dbPath = Path.Combine (
//				                Environment.GetFolderPath (Environment.SpecialFolder.Personal),
//				                "umeca.sqlite");
//
//			var db = new SQLiteConnection (dbPath);
//			db.CreateTable<Periodicity> ();
//
//			db.CreateTable<DrugType> ();
//
//			db.CreateTable<Degree> ();
//
//			db.CreateTable<AcademicLevel> ();
//
//			db.CreateTable<RegisterType> ();
//
//			db.CreateTable<Election> ();
//
//			db.CreateTable<DocumentType> ();
//
//			db.CreateTable<Relationship> ();
//
//			db.CreateTable<HomeType> ();
//
//			//db.CreateTable<TipoPropiedad> (); //actual seccundario anterior -------> segun yo igual que register type
//
//			db.CreateTable<ActivityCatalog> ();
//
//			//location
//			db.CreateTable<Country> ();
//			db.CreateTable<Location> ();
//			db.CreateTable<State> ();


			//create new meeting
//			db.CreateTable<Case> ();
//			db.CreateTable<Imputed> ();
//			db.CreateTable<Meeting> ();
//			db.CreateTable<StatusMeeting> ();
//
//			if (db.Table<SocialEnvironment> ().Count() == 0) {
//				// only insert the data if it doesn't already exist
//				Console.WriteLine ("No hay registros de meetings");
//			}
//


			//el imputado
//			
//			db.CreateTable<SocialEnvironment> ();
//			db.CreateTable<Reference> ();
//			db.CreateTable<Job> ();
//			db.CreateTable<School> ();
//			db.CreateTable<LeaveCountry> ();
//			db.CreateTable<SocialNetwork> ();



			var temp = new MeetingList();
			var pagestring = "nada que ver";
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
		}

		public void  MeetingEditNew()
		{

			var temp = new NewMeeting{Model = new NewMeetingDto{Name="nombre" , DateBirthString=DateTime.Today.ToString("yyyy/mm/dd")} };
			var pagestring = "nada que ver";
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
		}

		public void AddMeeting([Bind]NewMeetingDto model) {
			Console.WriteLine ("AddMeeting");
			Console.WriteLine ("EntrevistaTabla....."+model.ResponseMessage);
			/*Console.WriteLine ("No hay registros de meetings");
			Console.WriteLine ("JsonMeeting"+model);

			////////DEPENDIENDO DEL RESULTADO ES LA VISTA QUE REGRESA
			var temp = new NewMeeting{Model = new EntrevistaTabla{JsonMeeting=model + " guardx"}};
			var pagestring = "nada que ver";
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);*/
		}
	}


	public class LoginAction : Java.Lang.Object
	{
		Context context;

		public LoginAction(Context context)
		{
			this.context = context;
		}

		[Export("example")]
		public Java.Lang.String Example(Java.Lang.String number){

			var n = int.Parse (number.ToString());
			var result = new List<int> ();

			for (var i = 0; i < 10; i++)
				result.Add (n * i);

			return new Java.Lang.String(JsonConvert.SerializeObject(result));
		}
	}



}