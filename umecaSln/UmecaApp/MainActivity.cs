using Android.App;
using Android.Webkit;
using Android.OS;
using System;

namespace UmecaApp
{
	[Activity (Label = "Umeca", Theme = "@style/MyTheme", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			var condition = Intent.GetStringExtra ("condition");
			SetContentView (Resource.Layout.Main);
			var webView = FindViewById<WebView> (Resource.Id.webView);

			var loginController = new LoginController (new HybridWebView (webView, this));
			RouteHandler.RegisterController ("Login", loginController);

			var meetingController = new MeetingController (new HybridWebView (webView, this));
			RouteHandler.RegisterController ("Meeting", meetingController);

			var verificationController = new VerificationController (new HybridWebView (webView, this));
			RouteHandler.RegisterController ("Verification", verificationController);

			var supervisionController = new SupervisionController (new HybridWebView (webView, this));
			RouteHandler.RegisterController ("Supervision", supervisionController);

			var syncController = new SyncController (new HybridWebView (webView, this));
			RouteHandler.RegisterController ("Sync", syncController);

			loginController.Index (condition);

		}
	}
}