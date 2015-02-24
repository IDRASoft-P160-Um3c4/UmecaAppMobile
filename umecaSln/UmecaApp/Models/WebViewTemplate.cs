using System;
using System.IO;
using PortableRazor.Web;
using PortableRazor.Web.Mvc;

namespace UmecaApp
{
	public abstract class WebViewTemplate : PortableRazor.ViewBase
	{
		public string Layout { get; set; }
		private IHtmlString Body { get; set; }

		public new string GenerateString ()
		{
			using (var sw = new StringWriter ()) {
				Generate (sw);

				if (!string.IsNullOrEmpty(Layout)) {
					var n = (WebViewTemplate)Activator.CreateInstance(Type.GetType (Layout));
					n.Body = new HtmlString(sw.ToString());
					return n.GenerateString ();
				}
				return sw.ToString ();
			}
		}

		public IHtmlString RenderBody()
		{
			return Body;
		}
	}
}