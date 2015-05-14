using System;
using PortableRazor;
using Android.Content;
using UmecaApp.Models;
using System.Collections.Generic;

using Java.Interop;
using Newtonsoft.Json;

namespace UmecaApp
{
	public class LoginController
	{
		IHybridWebView webView;
		IDataAccess dataAccess;

		public LoginController(IHybridWebView webView, IDataAccess dataAccess)
		{
			this.webView = webView;
			this.dataAccess = dataAccess;
		}

		public void Index()
		{
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