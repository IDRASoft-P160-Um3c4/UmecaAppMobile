#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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
public partial class Home : WebViewTemplate
{

#line hidden

#line 1 "Home.cshtml"
public UmecaApp.Models.PageModel Model { get; set; }

#line default
#line hidden


public override void Execute()
{
WriteLiteral("<!DOCTYPE html>\r\n<html>\r\n<head>\r\n   <title>UMECA</title>\r\n</head>\r\n<body>\r\n    <l" +
"ink");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" href=\"content/Site.css\"");

WriteLiteral(@"/>

     
<script>
var urlConfig  = {};
urlConfig.reference = {};

urlConfig.show = function (){
var dlgMsgBox = $('#MessageBoxDlgId');
dlgMsgBox.show();
};

urlConfig.hide = function (){
var dlgMsgBox = $('#MessageBoxDlgId');
dlgMsgBox.hide();
 $(""#HomeUrl"").val("""");
};

urlConfig.yes = function (){
var nuevoContexto = $(""#HomeUrl"").val();
if(nuevoContexto != """"){
	Sync.updateAplicationUrl(nuevoContexto);
}
var dlgMsgBox = $('#MessageBoxDlgId');
dlgMsgBox.hide();
 $(""#HomeUrl"").val("""");
};

    	</script>
<div");

WriteLiteral(" class=\"modal-dialog\"");

WriteLiteral(" style=\"display:none; width:60%; position: relative;top: 5%;left: 50%;margin: 0 0" +
" 0 -30%;\"");

WriteLiteral(" id=\"MessageBoxDlgId\"");

WriteLiteral(" >\r\n        <div");

WriteLiteral(" class=\"modal-content\"");

WriteLiteral(" style=\"z-index: 1000;\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"modal-header\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"alert alert-info\"");

WriteLiteral(">\r\n                    <button");

WriteLiteral(" id=\"MessageBoxDlgXclose\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"close\"");

WriteLiteral(" onclick=\"javascript:urlConfig.hide();\"");

WriteLiteral(">×</button>\r\n                    <h4");

WriteLiteral(" class=\"modal-title element-center ng-binding\"");

WriteLiteral(" ng-bind-html=\"Title\"");

WriteLiteral(">Actialización de URL</h4>\r\n                </div>\r\n            </div>\r\n         " +
"   <div");

WriteLiteral(" class=\"modal-body\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"element-center ng-binding\"");

WriteLiteral(" ng-bind-html=\"Message\"");

WriteLiteral(">¿Cambiar URL de la aplicación?</div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"col-xs-10 col-xs-offset-1\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"input-group\"");

WriteLiteral(">\r\n                        <span");

WriteLiteral(" class=\"input-group-addon\"");

WriteLiteral("><i");

WriteLiteral(" class=\"glyphicon glyphicon-globe\"");

WriteLiteral("></i></span>\r\n                        <input");

WriteLiteral(" name=\"HomeUrl\"");

WriteLiteral(" class=\"form-control ng-valid ng-dirty\"");

WriteLiteral(" id=\"HomeUrl\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" placeholder=\"http(s)://www.dominio.com/contexto\"");

WriteLiteral(">\r\n                    </div>\r\n                    <div");

WriteLiteral(" class=\"input-group error-font\"");

WriteLiteral(">\r\n                        <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-replace=\"true\"");

WriteLiteral(" data-valmsg-for=\"HomeUrl\"");

WriteLiteral("></span>\r\n                    </div>\r\n            \t</div>\r\n            </div>\r\n  " +
"          <div");

WriteLiteral(" class=\"modal-footer\"");

WriteLiteral(">\r\n                <button");

WriteLiteral(" id=\"MessageBoxDlgYes\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default btn-danger\"");

WriteLiteral(" onclick=\"javascript:urlConfig.yes();\"");

WriteLiteral(" >Actualizar</button>\r\n                <button");

WriteLiteral(" id=\"MessageBoxDlgNo\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral(" onclick=\"javascript:urlConfig.hide();\"");

WriteLiteral(" >Cancelar</button>\r\n            </div>\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"blocker\"");

WriteLiteral(" style=\"z-index:999;\"");

WriteLiteral(">\r\n\t\t    <div>\r\n\t\t        Cargando...<img");

WriteLiteral(" src=\"content/images/ajax_loader.gif\"");

WriteLiteral(" alt=\"no content detected\"");

WriteLiteral(@" />
		    </div>
		</div>
    </div>

<script>
var blokedPleaseWait  = {};
blokedPleaseWait.show = function (caso){
	$(""#blokedPleaseWaitBoxDlgId"").show();
	signarse();
};

blokedPleaseWait.hide = function (){
	var dlgMsgBox = $('#blokedPleaseWaitBoxDlgId');
	dlgMsgBox.hide();
};
</script>
<div");

WriteLiteral(" class=\"modal-dialog initloaderImage\"");

WriteLiteral(" style=\"display:none; width:100%; position: relative;top: 0%;left: 0%;margin: 0 0" +
" 0 0;\"");

WriteLiteral(" id=\"blokedPleaseWaitBoxDlgId\"");

WriteLiteral(" >\r\n    <div");

WriteLiteral(" class=\"blocker\"");

WriteLiteral(" style=\"z-index:999;\"");

WriteLiteral(">\r\n\t    <div>\r\n\t        Cargando...<img");

WriteLiteral(" src=\"content/images/ajax_loader.gif\"");

WriteLiteral(" alt=\"por favor espere\"");

WriteLiteral(" />\r\n\t    </div>\r\n\t</div>\r\n</div>\r\n\r\n    <script");

WriteLiteral(" src=\"scripts/jquery-1.10.2.min.js\"");

WriteLiteral("></script>\r\n    <script");

WriteLiteral(" src=\"scripts/umeca/bootstrap.min.js\"");

WriteLiteral("></script>\r\n    <script");

WriteLiteral(" src=\"scripts/jquery.validate.min.js\"");

WriteLiteral("></script>\r\n    <script");

WriteLiteral(" src=\"scripts/jquery.validate.unobtrusive.min.js\"");

WriteLiteral("></script>\r\n    <link");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" href=\"content/bootstrap.css\"");

WriteLiteral(@"/>
    <script>
    $(""#divMsgError"").hide();

    var signarse = function (){
    $("".initloaderImage"").show();
    	$(""#divMsgError"").hide();
    	if($(""#lgnForm"").valid() == false){
    		blokedPleaseWait.hide();
        	return false;
        }else{
        	var user=$(""#j_username"").val();
        	var pass=$(""#j_password"").val();
        	var result = Sync.userUpsert(user, pass);
        	var response = JSON.parse(result);
        	console.log(""result->""+result);
        	if(response[""error""]){
        	$(""#divMsgError"").show();
        	$(""#divMsgError"").text(response[""response""]);
        	}else{
				window.location.replace('hybrid:'+response[""response""]+'/Index');
        	}
        	console.log(""response->""+response[""response""]);
        }
        blokedPleaseWait.hide();
    }
    </script>
                <div");

WriteLiteral(" class=\"modal-header\"");

WriteLiteral(">\r\n                    <h2");

WriteLiteral(" class=\"modal-title element-center\"");

WriteLiteral(">Ingresar al sistema</h2>\r\n                    <div");

WriteLiteral(" class=\"element-center\"");

WriteLiteral(">\r\n                        <h4>\r\n                            <i");

WriteLiteral(" class=\"glyphicon glyphicon-cog\"");

WriteLiteral("></i>\r\n                            &nbsp;&nbsp;&nbsp;<i");

WriteLiteral(" class=\"glyphicon glyphicon-cloud\"");

WriteLiteral("></i>\r\n                            &nbsp;&nbsp;&nbsp;<i");

WriteLiteral(" class=\"glyphicon glyphicon-home\"");

WriteLiteral("></i>\r\n                            &nbsp;&nbsp;&nbsp;<i");

WriteLiteral(" class=\"glyphicon glyphicon-phone\"");

WriteLiteral("></i>\r\n                            &nbsp;&nbsp;&nbsp;<i");

WriteLiteral(" class=\"glyphicon glyphicon-tasks\"");

WriteLiteral(" onclick=\"urlConfig.show();\"");

WriteLiteral("></i>&nbsp;&nbsp;&nbsp;\r\n                        </h4>\r\n                    </div" +
">\r\n                </div>\r\n<form");

WriteLiteral(" id=\"lgnForm\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"modal-body\"");

WriteLiteral(">\r\n                    <h4");

WriteLiteral(" class=\"element-center\"");

WriteLiteral(">Ingrese los datos para acceder al portal</h4>\r\n");


#line 138 "Home.cshtml"
                    

#line default
#line hidden

#line 138 "Home.cshtml"
                     if(Model.StatusMsg!= null){


#line default
#line hidden
WriteLiteral("                    \t<div");

WriteLiteral(" class=\"umeca-toast-error element-center\"");

WriteLiteral(">\r\n\t\t                    <p >");


#line 140 "Home.cshtml"
                           Write(Model.StatusMsg);


#line default
#line hidden
WriteLiteral("</p>\r\n\t\t                </div>\r\n");


#line 142 "Home.cshtml"
                    }


#line default
#line hidden
WriteLiteral("\r\n                    <hr>\r\n                    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                    \t<div");

WriteLiteral(" class=\"col-xs-6\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"input-group\"");

WriteLiteral(">\r\n                                <span");

WriteLiteral(" class=\"input-group-addon\"");

WriteLiteral("><i");

WriteLiteral(" class=\"glyphicon glyphicon-user\"");

WriteLiteral("></i></span>\r\n                                <input");

WriteLiteral(" name=\"UserName\"");

WriteLiteral(" class=\"form-control ng-valid ng-dirty\"");

WriteLiteral(" id=\"j_username\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" placeholder=\"Usuario\"");

WriteLiteral(" value=\"\"");

WriteLiteral(" ng-model=\"m.username\"");

WriteLiteral(" data-val-required=\"No ha ingresado el usuario\"");

WriteLiteral(" data-val=\"true\"");

WriteLiteral(">\r\n                            </div>\r\n                            <div");

WriteLiteral(" class=\"input-group error-font\"");

WriteLiteral(">\r\n                                <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-replace=\"true\"");

WriteLiteral(" data-valmsg-for=\"UserName\"");

WriteLiteral("></span>\r\n                            </div>\r\n                        </div>\r\n   " +
"                 </div>\r\n                    <br />\r\n                    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"col-xs-6\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"input-group\"");

WriteLiteral(">\r\n                                <span");

WriteLiteral(" class=\"input-group-addon\"");

WriteLiteral("><i");

WriteLiteral(" class=\"glyphicon glyphicon-asterisk\"");

WriteLiteral("></i></span>\r\n                                <input");

WriteLiteral(" name=\"Password\"");

WriteLiteral(" class=\"form-control ng-valid ng-dirty\"");

WriteLiteral(" id=\"j_password\"");

WriteLiteral(" type=\"password\"");

WriteLiteral(" placeholder=\"Contraseña\"");

WriteLiteral(" ng-model=\"m.password\"");

WriteLiteral(" data-val-required=\"No ha ingresado la contraseña\"");

WriteLiteral(" data-val=\"true\"");

WriteLiteral(">\r\n                            </div>\r\n                            <div");

WriteLiteral(" class=\"input-group error-font\"");

WriteLiteral(">\r\n                                <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-replace=\"true\"");

WriteLiteral(" data-valmsg-for=\"Password\"");

WriteLiteral("></span>\r\n                            </div>\r\n                    \t</div>\r\n      " +
"              </div>\r\n\r\n                    <br>\r\n                </div>\r\n\r\n    " +
"            <div");

WriteLiteral(" style=\"display:none\"");

WriteLiteral(" class=\"alert alert-danger element-center error-font ng-binding\"");

WriteLiteral(" id=\"divMsgError\"");

WriteLiteral(" onclick=\" $(\'#divMsgError\').hide();\"");

WriteLiteral(">\r\n\t\t            <div");

WriteLiteral(" class=\"col-xs-12\"");

WriteLiteral(">\r\n\t\t                <div");

WriteLiteral(" class=\"umeca-toast-error element-center\"");

WriteLiteral(">\r\n\t\t                    <p");

WriteLiteral(" id=\"MsgError\"");

WriteLiteral("></p>\r\n\t\t                </div>\r\n\t\t            </div>\r\n\t\t        </div>\r\n\r\n      " +
"          <div");

WriteLiteral(" class=\"modal-footer\"");

WriteLiteral(">\r\n                \t<a");

WriteLiteral(" href=\"javascript:;\"");

WriteLiteral(" onclick=\"blokedPleaseWait.show();\"");

WriteLiteral(" >\r\n                    \t<label");

WriteLiteral(" class=\"btn btn-default btn-primary btn-sm\"");

WriteLiteral(" >Ingresar</label>\r\n                    </a>\r\n         \t\t</div>\r\n</form>\r\n\r\n\r\n   " +
"   </body>\r\n</html>");

}
}
}
#pragma warning restore 1591
