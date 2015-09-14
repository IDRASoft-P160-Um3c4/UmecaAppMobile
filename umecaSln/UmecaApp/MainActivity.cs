using Android.App;
using Android.Webkit;
using Android.OS;
using SQLiteNetExtensions;
using SQLite.Net;
using SQLite.Net.Platform;
using System;

namespace UmecaApp
{
	[Activity (Label = "Umeca", MainLauncher = true, Icon = "@drawable/icon", ConfigurationChanges=Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate(bundle);
			string condition = null;
			try{
				InsertCatalogs CatalogInserter = new InsertCatalogs ();
				CatalogInserter.insertAllCatalogs (this);
			}catch(Exception e){
				Console.WriteLine ("catched error at Main activity, InsertCatalogs.insertAllCatalogs() ");
				Console.WriteLine ("Error ::> "+e.Message);
				condition = "Se detectó un error al crear las tablas y catálogos de la base de datos, la información está corrupta por favor instale de nuevo esta aplicación o contacte a soporte técnico.";
			}
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

			var loginController = new LoginController (new HybridWebView (webView, this), db);
			RouteHandler.RegisterController ("Login", loginController);
			var meetingController = new MeetingController (new HybridWebView (webView, this), db);
			RouteHandler.RegisterController ("Meeting", meetingController);
			var verificationController = new VerificationController (new HybridWebView (webView, this), db);
			RouteHandler.RegisterController ("Verification", verificationController);
			var supervisionController = new SupervisionController (new HybridWebView (webView, this), db);
			RouteHandler.RegisterController ("Supervision", supervisionController);

			var syncController = new SyncController (new HybridWebView (webView, this), db);
			RouteHandler.RegisterController ("Sync", syncController);

			loginController.Index (condition);
		}
	}
}