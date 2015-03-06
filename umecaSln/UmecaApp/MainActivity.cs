using Android.App;
using Android.Webkit;
using Android.OS;
using SQLiteNetExtensions;
using SQLite.Net;
using SQLite.Net.Platform;
using System.IO;

namespace UmecaApp
{
	[Activity (Label = "Umeca", MainLauncher = true, Icon = "@drawable/icon", ConfigurationChanges=Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
	public class MainActivity : Activity
	{	
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.Main);

			var webView = FindViewById<WebView>(Resource.Id.webView);

			if (File.Exists (ConstantsDB.DB_PATH)) {
				System.Console.WriteLine ("existe la base de datos");
			} else {
				System.Console.WriteLine ("NOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO existe la base de datos");
			}
			var db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			// Set our view from the "main" layout resource


			var loginController = new LoginController (new HybridWebView (webView, this), null);
			RouteHandler.RegisterController ("Login", loginController);
//			var meetingController = new MeetingController (new HybridWebView (webView, this), db);
//			RouteHandler.RegisterController ("Meeting", meetingController);
			loginController.Index ();
		}
	}
}