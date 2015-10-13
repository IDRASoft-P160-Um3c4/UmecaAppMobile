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

		CatalogServiceController services;

		public LoginController(IHybridWebView webView)
		{
			this.webView = webView;
			services = new CatalogServiceController ();	
		}

		public void Index()
		{
			services. tablesInit();
			//TODO:Remove test verification
			var template = new Home {
				Model = new PageModel {Title = "Umeca", StatusMsg=null}
			};
			var page = template.GenerateString();

			webView.LoadHtmlString (page);
		}

		public void Index(String msg)
		{
			services. tablesInit();
			//TODO:Remove test verification
			var template = new Home {
				Model = new PageModel {Title = "Umeca", StatusMsg=msg}
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