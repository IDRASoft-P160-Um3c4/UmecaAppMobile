using Android.App;
using Android.Webkit;
using Android.OS;
using SQLiteNetExtensions;
using SQLite.Net;
using SQLite.Net.Platform;

namespace UmecaApp
{
	[Activity (Label = "Umeca", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate(bundle);


//			var db = new SQLiteConnection(ConstantsDB.DB_PATH);
			var db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			var webView = FindViewById<WebView>(Resource.Id.webView);

			var loginController = new LoginController (new HybridWebView (webView, this), null);
			RouteHandler.RegisterController ("Login", loginController);
			var meetingController = new MeetingController (new HybridWebView (webView, this), db);
			RouteHandler.RegisterController ("Meeting", meetingController);
			loginController.Index ();
		}
	}
}