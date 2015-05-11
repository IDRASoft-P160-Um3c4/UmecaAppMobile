using Android.App;
using Android.Webkit;
using Android.OS;
using SQLiteNetExtensions;
using SQLite.Net;
using SQLite.Net.Platform;

namespace UmecaApp
{
	[Activity (Label = "Umeca", MainLauncher = true, Icon = "@drawable/icon", ConfigurationChanges=Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate(bundle);
			InsertCatalogs CatalogInserter = new InsertCatalogs ();
			CatalogInserter.insertAllCatalogs (this);

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
			var verificationController = new VerificationController (new HybridWebView (webView, this), db);
			RouteHandler.RegisterController ("Verification", verificationController);
			var supervisionController = new SupervisionController (new HybridWebView (webView, this), db);
			RouteHandler.RegisterController ("Supervition", verificationController);
			loginController.Index ();
		}
	}
}