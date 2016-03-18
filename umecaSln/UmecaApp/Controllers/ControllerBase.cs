using PortableRazor;

namespace UmecaApp
{
	public class ControllerBase
	{
		protected IHybridWebView webView;
		protected IDataAccess dataAccess;

		public ControllerBase (IHybridWebView webView, IDataAccess dataAccess)
		{
			this.webView = webView;
			this.dataAccess = dataAccess;
		}

		public void Index ()
		{
			webView.LoadHtmlString ("Index");
		}
	}
}

