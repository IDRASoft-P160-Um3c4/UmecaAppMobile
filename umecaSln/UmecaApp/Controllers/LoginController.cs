using System;
using PortableRazor;
using Android.Content;
using UmecaApp.Models;
using System.Collections.Generic;
using SQLite.Net;

using Java.Interop;
using Newtonsoft.Json;

namespace UmecaApp
{
	public class LoginController
	{
		IHybridWebView webView;
		SQLiteConnection dataAccess;
		CatalogServiceController services;

		public LoginController(IHybridWebView webView, SQLiteConnection dataAccess)
		{
			this.webView = webView;
			this.dataAccess = dataAccess;

			services = new CatalogServiceController (dataAccess);	
		}

		public void Index()
		{
//			services.createVerificationTest ();
			services. tablesInit();
			//TODO:Remove test verification
			var template = new Home {
				Model = new PageModel {Title = "Umeca"}
			};
			var page = template.GenerateString();

			webView.LoadHtmlString (page);
		}

		public void MeetingIndex()
		{
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