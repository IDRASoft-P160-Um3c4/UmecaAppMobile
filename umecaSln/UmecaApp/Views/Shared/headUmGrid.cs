#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UmecaApp
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


[System.CodeDom.Compiler.GeneratedCodeAttribute("RazorTemplatePreprocessor", "2.6.0.0")]
public partial class headUmGrid : WebViewTemplate
{

#line hidden

public override void Execute()
{
WriteLiteral(@"<!DOCTYPE html>
<html>
	<head>
		<title>Umeca</title>

		<script>
		function DownloadVerification(){
		var respuesta = Sync.downloadVerificacion(""99630110"");
		alert(respuesta);
		}
		</script>


		<script>
var askPasswordKeySync  = {};

askPasswordKeySync.show = function (caso){
	var dlgMsgBox = $('#askPasswordKeySyncBoxDlgId');
	dlgMsgBox.show();
}; 


askPasswordKeySync.hide = function (){
	var dlgMsgBox = $('#askPasswordKeySyncBoxDlgId');
	dlgMsgBox.hide();
};

askPasswordKeySync.yes = function (){
	var password = $(""#askPasswordKeySynccontainerPassword"").val();
	$(""#askPasswordKeySynccontainerPassword"").val("""");
	var rep = Sync.downloadVerificacion(password);
	var respuesta = $.parseJSON( rep );
			if(respuesta.error==""true""){
				alert(respuesta.response);
			}else{
				location.reload();
			}
};
    	</script>
<div");

WriteLiteral(" class=\"modal-dialog\"");

WriteLiteral(" style=\"display:none; width:60%; position: relative;top: 15%;left: 50%;margin: 0 " +
"0 0 -30%;\"");

WriteLiteral(" id=\"askPasswordKeySyncBoxDlgId\"");

WriteLiteral(" >\r\n        <div");

WriteLiteral(" class=\"modal-content\"");

WriteLiteral(" style=\"z-index: 1000;\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"modal-header\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"alert alert-info\"");

WriteLiteral(">\r\n                    <button");

WriteLiteral(" id=\"askPasswordKeySyncBoxDlgXclose\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"close\"");

WriteLiteral(" onclick=\"javascript:askPasswordKeySync.hide();\"");

WriteLiteral(">×</button>\r\n                    <h4");

WriteLiteral(" class=\"modal-title element-center ng-binding\"");

WriteLiteral(" ng-bind-html=\"Title\"");

WriteLiteral(">Confirmación para Descargar información</h4>\r\n                </div>\r\n          " +
"  </div>\r\n            <div");

WriteLiteral(" class=\"modal-body\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"element-center ng-binding\"");

WriteLiteral(" ng-bind-html=\"Message\"");

WriteLiteral(">Por favor ingrese la contraseña para continuar:<br><input");

WriteLiteral(" type=\"password\"");

WriteLiteral(" name=\"usrPassword\"");

WriteLiteral(" id=\"askPasswordKeySynccontainerPassword\"");

WriteLiteral("/>\r\n                <br /><b>Al continuar descargaran los casos asignados al usua" +
"rio.</b></div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"modal-footer\"");

WriteLiteral(">\r\n                <button");

WriteLiteral(" id=\"MessageBoxDlgYes\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default btn-primary\"");

WriteLiteral(" onclick=\"javascript:askPasswordKeySync.yes();\"");

WriteLiteral(" >Si</button>\r\n                <button");

WriteLiteral(" id=\"MessageBoxDlgNo\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral(" onclick=\"javascript:askPasswordKeySync.hide();\"");

WriteLiteral(" >No</button>\r\n            </div>\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"blocker\"");

WriteLiteral(" style=\"z-index:999;\"");

WriteLiteral(">\r\n\t\t    <div>\r\n\t\t        Cargando...<img");

WriteLiteral(" src=\"content/images/ajax_loader.gif\"");

WriteLiteral(" alt=\"no content detected\"");

WriteLiteral(" />\r\n\t\t    </div>\r\n\t\t</div>\r\n    </div>\r\n\r\n\r\n\t\t<!--LAOYUT para meeting index solo" +
" muestra el bostrap table -->\r\n    <link");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" href=\"content/themes/umeca/bootstrap-timepicker.css\"");

WriteLiteral("/>\r\n    <link");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" href=\"content/themes/umeca/datepicker.css\"");

WriteLiteral("/>\r\n    <link");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" href=\"content/themes/umeca/font-awesome.min.css\"");

WriteLiteral("/>\r\n    <link");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" href=\"content/themes/umeca/jquery-ui-1.10.3.full.min.css\"");

WriteLiteral("/>\r\n    <link");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" href=\"content/themes/umeca/ace-fonts.css\"");

WriteLiteral("/>\r\n    <link");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" href=\"content/themes/umeca/ace-rtl.min.css\"");

WriteLiteral("/>\r\n    <link");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" href=\"content/themes/umeca/ace-skins.min.css\"");

WriteLiteral("/>\r\n\r\n    <link");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" href=\"content/bootstrap.css\"");

WriteLiteral("/>\r\n    <link");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" href=\"content/Site.css\"");

WriteLiteral("/>\r\n    <link");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" href=\"content/themes/umeca/ace.min.css\"");

WriteLiteral("/>\r\n   \r\n    <script");

WriteLiteral(" src=\"scripts/jquery-1.10.2.min.js\"");

WriteLiteral("></script>\r\n    <script");

WriteLiteral(" src=\"scripts/umeca/bootstrap.min.js\"");

WriteLiteral("></script>\r\n    <script");

WriteLiteral(" src=\"scripts/common.js\"");

WriteLiteral("></script>\r\n    <script");

WriteLiteral(" src=\"scripts/modernizr-2.6.2.js\"");

WriteLiteral("></script>\r\n    <script");

WriteLiteral(" src=\"scripts/respond.min.js\"");

WriteLiteral("></script>\r\n\r\n    <link");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" href=\"bootstrap-table/bootstrap-table.css\"");

WriteLiteral(">\r\n    <script");

WriteLiteral(" src=\"bootstrap-table/bootstrap-table.js\"");

WriteLiteral("></script>\r\n    <script");

WriteLiteral(" src=\"bootstrap-table/bootstrap-table-es-AR.min.js\"");

WriteLiteral("></script>\r\n    <style");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(@">
        .pagination-info {
        float: right;!important;
        }
        div.pagination {
            width: 55%!important;
        float: left!important;
        }
        ul.pagination {
            display: inline-block!important;
            text-align: right!important;
            float: right!important;
        }
        .page-list {
        float: left!important;
        padding-left: 5px;
        }
        .pagination-detail{
            width: 45%!important;
            float: right!important;
            padding-top: 10px;
        }
    </style>

    <script");

WriteLiteral(" src=\"scripts/umeca/ace-elements.min.js\"");

WriteLiteral("></script>\r\n    \t<script");

WriteLiteral(" src=\"scripts/umeca/ace-extra.js\"");

WriteLiteral("></script>\r\n    <script");

WriteLiteral(" src=\"scripts/umeca/ace.min.js\"");

WriteLiteral("></script>\r\n    <script");

WriteLiteral(" src=\"scripts/umeca/typeahead-bs2.min.js\"");

WriteLiteral("></script>\r\n\t</head>\r\n\t<body>\r\n\r\n\r\n<!--<script src=\"scripts/app/shared/menuCtrl.j" +
"s\"></script>\r\n<div ng-controller=\"menuController\">-->\r\n<div>\r\n<div");

WriteLiteral(" class=\"navbar navbar-default navbar-static-top\"");

WriteLiteral(">\r\n<style>\r\n    .navbar > .container {\r\n        padding-left: 5px !important;\r\n  " +
"      padding-right: 5px !important;\r\n    }\r\n\r\n    .container {\r\n        max-wid" +
"th: 100% !important;\r\n    }\r\n</style>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    try {\r\n        ace.settings.check(\'navbar\', \'fixed\')\r\n    } catch (e) {\r\n " +
"   }\r\n</script>\r\n\r\n<div");

WriteLiteral(" class=\"container\"");

WriteLiteral(" id=\"navbar-container\"");

WriteLiteral(">\r\n<div");

WriteLiteral(" class=\"navbar-header pull-left\"");

WriteLiteral(">\r\n    <a");

WriteAttribute ("href", " href=\"", "\""

#line 143 "headUmGrid.cshtml"
, Tuple.Create<string,object,bool> ("", Url.Action("Index","Login")

#line default
#line hidden
, false)
);
WriteLiteral(" class=\"navbar-brand\"");

WriteLiteral(">\r\n        <i");

WriteLiteral(" class=\"glyphicon glyphicon-cloud\"");

WriteLiteral("></i>&nbsp;&nbsp;Inicio</a>\r\n</div>\r\n<!-- /.navbar-header -->\r\n\r\n<div");

WriteLiteral(" class=\"navbar-header pull-right\"");

WriteLiteral(" role=\"navigation\"");

WriteLiteral(">\r\n    <ul");

WriteLiteral(" class=\"nav ace-nav\"");

WriteLiteral(">\r\n        <li");

WriteLiteral(" class=\"nav-li-blue\"");

WriteLiteral("></li>\r\n            <li");

WriteLiteral(" class=\"nav-li-blue\"");

WriteLiteral("><a");

WriteAttribute ("href", " href=\"", "\""

#line 151 "headUmGrid.cshtml"
      , Tuple.Create<string,object,bool> ("", Url.Action("Index","Meeting")

#line default
#line hidden
, false)
);
WriteLiteral("><i\r\n                    class=\"icon-comments-alt\"></i>&nbsp;&nbsp;Entrevista</a>" +
"</li>\r\n            <li");

WriteLiteral(" class=\"nav-li-blue\"");

WriteLiteral("><a");

WriteAttribute ("href", " href=\"", "\""

#line 153 "headUmGrid.cshtml"
      , Tuple.Create<string,object,bool> ("", Url.Action("Index","Verification")

#line default
#line hidden
, false)
);
WriteLiteral("><i\r\n                    class=\"icon-check\"></i>&nbsp;&nbsp;Verificaci&oacute;n</" +
"a>\r\n            </li>\r\n            <li");

WriteLiteral(" class=\"nav-li-blue\"");

WriteLiteral("><a");

WriteLiteral(" href=\"javascript:askPasswordKeySync.show();\"");

WriteLiteral("><i\r\n                    class=\"icon-cloud-download\"></i>&nbsp;&nbsp;Descargar In" +
"formaci&oacute;n</a>\r\n            </li>\r\n            <li");

WriteLiteral(" class=\"nav-li-blue\"");

WriteLiteral("><a");

WriteAttribute ("href", " href=\"", "\""

#line 159 "headUmGrid.cshtml"
      , Tuple.Create<string,object,bool> ("", Url.Action("Index","Sync")

#line default
#line hidden
, false)
);
WriteLiteral("><i\r\n                    class=\"icon-exchange\"></i>&nbsp;&nbsp;Sincronizar</a></l" +
"i>\r\n    </ul>\r\n    <!-- /.ace-nav -->\r\n</div>\r\n<!-- /.navbar-header -->\r\n</div>\r" +
"\n<!-- /.container -->\r\n</div>\r\n</div>\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n");

WriteLiteral("\t\t");


#line 182 "headUmGrid.cshtml"
   Write(RenderBody());


#line default
#line hidden
WriteLiteral("\r\n\t</body>\r\n</html>");

}
}
}
#pragma warning restore 1591
