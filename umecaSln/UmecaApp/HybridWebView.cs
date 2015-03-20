using System;
using Android.App;
using Android.Content;
using Android.Webkit;
using PortableRazor;

namespace UmecaApp
{
	public class HybridWebView : IHybridWebView
	{
		private const string url = "file:///android_asset/";
		WebView webView;

		public HybridWebView(WebView uiWebView, Context context) {
			webView = uiWebView;

			// Use subclassed WebViewClient to intercept hybrid native calls
			var webViewClient = new HybridWebViewClient ();

			webView.SetWebViewClient (webViewClient);
			webView.Settings.CacheMode = CacheModes.CacheElseNetwork;
			webView.Settings.JavaScriptEnabled = true;
			webView.AddJavascriptInterface (new LoginAction(context), "login");
			webView.AddJavascriptInterface (new MeetingService(context), "MeetingService");
			webView.SetWebChromeClient (new HybridWebChromeClient (webView.Context));
		}

		#region IHybridWebView implementation

		public void LoadHtmlString (string html)
		{
			webView.LoadDataWithBaseURL(url, html, "text/html", "UTF-8", null);
		}

		public string EvaluateJavascript (string script) 
		{
			webView.LoadUrl ("javascript:" + script);
			return "";
		}

		public string BasePath {
			get {
				return url;
			}
		}

		#endregion


		class HybridWebViewClient : WebViewClient {
			public override bool ShouldOverrideUrlLoading (WebView webView, string url) {
				return RouteHandler.HandleRequest (url);
			}
		}

		class HybridWebChromeClient : WebChromeClient {
			Context context;

			public HybridWebChromeClient (Context context) : base () {
				this.context = context;
			}

			public override bool OnJsAlert (WebView view, string url, string message, JsResult result) {
				var alertDialogBuilder = new AlertDialog.Builder (context)
					.SetMessage (message)
					.SetCancelable (false)
					.SetPositiveButton ("Acceptar", (sender, args) => {
						result.Confirm ();
					});

				alertDialogBuilder.Create().Show();

				return true;
			}

			public override bool OnJsConfirm (WebView view, string url, string message, JsResult result) {
				var alertDialogBuilder = new AlertDialog.Builder (context)
					.SetMessage (message)
					.SetCancelable (false)
					.SetPositiveButton ("Aceptar", (sender, args) => {
						result.Confirm();
					})
					.SetNegativeButton ("Cancelar", (sender, args) => {
						result.Cancel();
					});

				alertDialogBuilder.Create().Show();

				return true;
			}
		}
	}
}