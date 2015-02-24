package umecaapp;


public class HybridWebView_HybridWebChromeClient
	extends android.webkit.WebChromeClient
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onJsAlert:(Landroid/webkit/WebView;Ljava/lang/String;Ljava/lang/String;Landroid/webkit/JsResult;)Z:GetOnJsAlert_Landroid_webkit_WebView_Ljava_lang_String_Ljava_lang_String_Landroid_webkit_JsResult_Handler\n" +
			"n_onJsConfirm:(Landroid/webkit/WebView;Ljava/lang/String;Ljava/lang/String;Landroid/webkit/JsResult;)Z:GetOnJsConfirm_Landroid_webkit_WebView_Ljava_lang_String_Ljava_lang_String_Landroid_webkit_JsResult_Handler\n" +
			"";
		mono.android.Runtime.register ("UmecaApp.HybridWebView/HybridWebChromeClient, Umeca, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", HybridWebView_HybridWebChromeClient.class, __md_methods);
	}


	public HybridWebView_HybridWebChromeClient () throws java.lang.Throwable
	{
		super ();
		if (getClass () == HybridWebView_HybridWebChromeClient.class)
			mono.android.TypeManager.Activate ("UmecaApp.HybridWebView/HybridWebChromeClient, Umeca, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public HybridWebView_HybridWebChromeClient (android.content.Context p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == HybridWebView_HybridWebChromeClient.class)
			mono.android.TypeManager.Activate ("UmecaApp.HybridWebView/HybridWebChromeClient, Umeca, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public boolean onJsAlert (android.webkit.WebView p0, java.lang.String p1, java.lang.String p2, android.webkit.JsResult p3)
	{
		return n_onJsAlert (p0, p1, p2, p3);
	}

	private native boolean n_onJsAlert (android.webkit.WebView p0, java.lang.String p1, java.lang.String p2, android.webkit.JsResult p3);


	public boolean onJsConfirm (android.webkit.WebView p0, java.lang.String p1, java.lang.String p2, android.webkit.JsResult p3)
	{
		return n_onJsConfirm (p0, p1, p2, p3);
	}

	private native boolean n_onJsConfirm (android.webkit.WebView p0, java.lang.String p1, java.lang.String p2, android.webkit.JsResult p3);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
