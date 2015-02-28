using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using System.Linq;

namespace UmecaApp
{
	public static class RouteHandler
	{
		private static string defaultControllerName;

		public static Dictionary<string, object> Controllers { get; private set; }

		static RouteHandler ()
		{
			Controllers = new Dictionary<string, object> (StringComparer.OrdinalIgnoreCase);
			defaultControllerName = String.Empty;
		}

		public static void RegisterController(string controllerName, object controller) {
			if (!Controllers.ContainsKey (controllerName)) {
				Controllers.Add (controllerName, controller);
			}
			if (defaultControllerName == String.Empty) 
				defaultControllerName = controllerName;
		}

		public static void SetDefaultController(string controllerName) {
			defaultControllerName = controllerName;
		}

		public static bool HandleRequest(string url) {

			// If the URL is not our own custom scheme, just let the webView load the URL as usual
			var scheme = PortableRazor.ViewBase.UrlScheme;

			if (!url.StartsWith(scheme))
				return false;

			if (url == scheme)
				return false;

			// This handler will treat everything between the protocol and "?"
			// as the method name.  The querystring has all of the parameters.
			var resources = url.Substring(scheme.Length).Split('?');
			var actionName = resources [0];
			var controllerName = defaultControllerName;
			if (actionName.Contains ("/")) {
				var parts = actionName.Split ('/');
				controllerName = parts [0];
				actionName = parts [1];
			}

			var controller = Controllers [controllerName];
			var method = controller.GetType ().GetRuntimeMethod (actionName);

			var parameters = resources.Length > 1 ? PortableRazor.Web.HttpUtility.ParseQueryString (resources [1]) : null;

			var methodParams = method.GetParameters ();
			var paramsIn = new object[methodParams.Length];

			foreach (var p in methodParams) {
				var hasBindable = Attribute.IsDefined (p,typeof(UmecaApp.Bind));

				if (hasBindable) {
					object result;
					try {
						var mi = typeof(JsonConvert).GetMethods (BindingFlags.Public | BindingFlags.Static).FirstOrDefault(m => m.IsGenericMethod);
						mi = mi.MakeGenericMethod( new [] { p.ParameterType } );
						object[] args = { parameters [p.Name] };
						result = mi.Invoke (null, args);
					}
					catch {
						result = null;
					}
					paramsIn [Array.IndexOf (methodParams, p)] = result == null ? null : Convert.ChangeType(result, p.ParameterType);

				} else {
					paramsIn [Array.IndexOf (methodParams, p)] = parameters [p.Name] != null ? 
						Convert.ChangeType (parameters [p.Name], p.ParameterType) : null;
				}
			}

			method.Invoke (controller, paramsIn);

			return true;
		}

		private static MethodInfo GetRuntimeMethod(this Type type, string name) {
			var methods = type.GetRuntimeMethods ();
			foreach (var method in methods)
				if (method.Name == name)
					return method;
			return null;
		}
	}
}