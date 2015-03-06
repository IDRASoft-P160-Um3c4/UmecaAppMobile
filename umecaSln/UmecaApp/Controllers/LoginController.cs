using System;
using PortableRazor;
using UmecaApp.Models;

//para el context
using Android.Content;
//para el export
using Java.Interop;
//json convert
using Newtonsoft.Json;
//para List<>
using System.Collections.Generic;


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
				Model = new PageModel {Title = "Registro de usuario"}
			};
			var page = template.GenerateString ();

			webView.LoadHtmlString (page);
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