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
public partial class SyncCaseList : WebViewTemplate
{

#line hidden

#line 1 "SyncCaseList.cshtml"
public System.Collections.Generic.List<UmecaApp.MeetingTblDto> Model { get; set; }

#line default
#line hidden


public override void Execute()
{

#line 3 "SyncCaseList.cshtml"
  
	Layout = "UmecaApp.headUmGrid";


#line default
#line hidden
WriteLiteral(@"



 <script>
var askSyncIncomplete  = {};
askSyncIncomplete.idCase = 0;

askSyncIncomplete.show = function (caso){
	var dlgMsgBox = $('#askSyncIncompleteBoxDlgId');
	askSyncIncomplete.idCase = caso;
	dlgMsgBox.show();
}; 


askSyncIncomplete.hide = function (){
	askSyncIncomplete.idCase = 0;
	var dlgMsgBox = $('#askSyncIncompleteBoxDlgId');
	dlgMsgBox.hide();
};

askSyncIncomplete.yes = function (){
	var password = $(""#askSyncIncompletecontainerPassword"").val();
	$(""#askSyncIncompletecontainerPassword"").val("""");
	var result = Sync.sincrinozeCase(""[""+askSyncIncomplete.idCase+""]"", password,""verificacion"");
	var respuesta = $.parseJSON( result );
	console.log(respuesta.response);
			if(respuesta.error){
				askSyncIncomplete.hide();
				$(""#Resp2Value"").text(respuesta.response);
				$(""#mesageResp2"").show();
			}else{
				window.location.replace('hybrid:Sync/Index');
			}
askSyncIncomplete.idCase = 0;
};
    	</script>
<div");

WriteLiteral(" class=\"modal-dialog\"");

WriteLiteral(" style=\"display:none; width:60%; position: relative;top: 15%;left: 50%;margin: 0 " +
"0 0 -30%;\"");

WriteLiteral(" id=\"askSyncIncompleteBoxDlgId\"");

WriteLiteral(" >\r\n        <div");

WriteLiteral(" class=\"modal-content\"");

WriteLiteral(" style=\"z-index: 1000;\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"modal-header\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"alert alert-info\"");

WriteLiteral(">\r\n                    <button");

WriteLiteral(" id=\"askSyncIncompleteBoxDlgXclose\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"close\"");

WriteLiteral(" onclick=\"javascript:askSyncIncomplete.hide();\"");

WriteLiteral(">×</button>\r\n                    <h4");

WriteLiteral(" class=\"modal-title element-center ng-binding\"");

WriteLiteral(" ng-bind-html=\"Title\"");

WriteLiteral(">Confirmación para Sincronizar</h4>\r\n                </div>\r\n            </div>\r\n" +
"            <div");

WriteLiteral(" class=\"modal-body\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"element-center ng-binding\"");

WriteLiteral(" ng-bind-html=\"Message\"");

WriteLiteral(">Por favor ingrese la contrasea:<br><input");

WriteLiteral(" type=\"password\"");

WriteLiteral(" name=\"usrPassword\"");

WriteLiteral(" id=\"askSyncIncompletecontainerPassword\"");

WriteLiteral("/>\r\n                <br><b> Una vez sincronizada se eliminaran del dispositivo la" +
"s fuentes que no se terminaron y el caso que las contiene.</b></div>\r\n          " +
"  </div>\r\n            <div");

WriteLiteral(" class=\"modal-footer\"");

WriteLiteral(">\r\n                <button");

WriteLiteral(" id=\"MessageBoxDlgYes\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default btn-primary\"");

WriteLiteral(" onclick=\"javascript:askSyncIncomplete.yes();\"");

WriteLiteral(" >Si</button>\r\n                <button");

WriteLiteral(" id=\"MessageBoxDlgNo\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral(" onclick=\"javascript:askSyncIncomplete.hide();\"");

WriteLiteral(" >No</button>\r\n            </div>\r\n        </div>\r\n        <div");

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
var askSyncMeeting  = {};
askSyncMeeting.idCase = 0;

askSyncMeeting.show = function (caso){
	var dlgMsgBox = $('#askSyncMeetingBoxDlgId');
	askSyncMeeting.idCase = caso;
	dlgMsgBox.show();
};

askSyncMeeting.hide = function (){
	askSyncMeeting.idCase = 0;
	var dlgMsgBox = $('#askSyncMeetingBoxDlgId');
	dlgMsgBox.hide();
};

askSyncMeeting.yes = function (){
	var password = $(""#askSyncMeetingcontainerPassword"").val();
	$(""#askSyncMeetingcontainerPassword"").val("""");
	var result = Sync.sincrinozeCase(""[""+askSyncMeeting.idCase+""]"", password,""meeting"");
	alert(result);
askSyncMeeting .idCase = 0;
};
    	</script>
<div");

WriteLiteral(" class=\"modal-dialog\"");

WriteLiteral(" style=\"display:none; width:60%; position: relative;top: 15%;left: 50%;margin: 0 " +
"0 0 -30%;\"");

WriteLiteral(" id=\"askSyncMeetingBoxDlgId\"");

WriteLiteral(" >\r\n        <div");

WriteLiteral(" class=\"modal-content\"");

WriteLiteral(" style=\"z-index: 1000;\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"modal-header\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"alert alert-info\"");

WriteLiteral(">\r\n                    <button");

WriteLiteral(" id=\"askSyncMeetingBoxDlgXclose\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"close\"");

WriteLiteral(" onclick=\"javascript:askSyncMeeting.hide();\"");

WriteLiteral(">×</button>\r\n                    <h4");

WriteLiteral(" class=\"modal-title element-center ng-binding\"");

WriteLiteral(" ng-bind-html=\"Title\"");

WriteLiteral(">Confirmación para Sincronizar</h4>\r\n                </div>\r\n            </div>\r\n" +
"            <div");

WriteLiteral(" class=\"modal-body\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"element-center ng-binding\"");

WriteLiteral(" ng-bind-html=\"Message\"");

WriteLiteral(">Por favor ingrese la contrasea:<br><input");

WriteLiteral(" type=\"password\"");

WriteLiteral(" name=\"usrPassword\"");

WriteLiteral(" id=\"askSyncMeetingcontainerPassword\"");

WriteLiteral("/>\r\n                <br><b> Una vez termine se eliminaran el caso del dispositivo" +
".</b></div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"modal-footer\"");

WriteLiteral(">\r\n                <button");

WriteLiteral(" id=\"MessageBoxDlgYes\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default btn-primary\"");

WriteLiteral(" onclick=\"javascript:askSyncMeeting.yes();\"");

WriteLiteral(" >Si</button>\r\n                <button");

WriteLiteral(" id=\"MessageBoxDlgNo\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral(" onclick=\"javascript:askSyncMeeting.hide();\"");

WriteLiteral(" >No</button>\r\n            </div>\r\n        </div>\r\n        <div");

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
var askSyncVerification  = {};
askSyncVerification.idCase = 0;

askSyncVerification.show = function (caso){
	var dlgMsgBox = $('#askSyncVerificationBoxDlgId');
	askSyncVerification.idCase = caso;
	dlgMsgBox.show();
};

askSyncVerification.hide = function (){
	askSyncVerification.idCase = 0;
	var dlgMsgBox = $('#askSyncVerificationBoxDlgId');
	dlgMsgBox.hide();
};

askSyncVerification.yes = function (){
	var password = $(""#askSyncVerificationcontainerPassword"").val();
	$(""#askSyncVerificationcontainerPassword"").val("""");
	var result = Sync.sincrinozeCase(""[""+askSyncVerification.idCase+""]"", password,""verificacion"");
	alert(result);
askSyncPAssword.idCase = 0;
};
    	</script>
<div");

WriteLiteral(" class=\"modal-dialog\"");

WriteLiteral(" style=\"display:none; width:60%; position: relative;top: 15%;left: 50%;margin: 0 " +
"0 0 -30%;\"");

WriteLiteral(" id=\"askSyncVerificationBoxDlgId\"");

WriteLiteral(" >\r\n        <div");

WriteLiteral(" class=\"modal-content\"");

WriteLiteral(" style=\"z-index: 1000;\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"modal-header\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"alert alert-info\"");

WriteLiteral(">\r\n                    <button");

WriteLiteral(" id=\"askSyncVerificationBoxDlgXclose\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"close\"");

WriteLiteral(" onclick=\"javascript:askSyncVerification.hide();\"");

WriteLiteral(">×</button>\r\n                    <h4");

WriteLiteral(" class=\"modal-title element-center ng-binding\"");

WriteLiteral(" ng-bind-html=\"Title\"");

WriteLiteral(">Confirmación para Sincronizar</h4>\r\n                </div>\r\n            </div>\r\n" +
"            <div");

WriteLiteral(" class=\"modal-body\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"element-center ng-binding\"");

WriteLiteral(" ng-bind-html=\"Message\"");

WriteLiteral(">Por favor ingrese la contrasea:<br><input");

WriteLiteral(" type=\"password\"");

WriteLiteral(" name=\"usrPassword\"");

WriteLiteral(" id=\"askSyncVerificationcontainerPassword\"");

WriteLiteral("/>\r\n                <br><b> Una vez termine se eliminaran el caso del dispositivo" +
".</b></div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"modal-footer\"");

WriteLiteral(">\r\n                <button");

WriteLiteral(" id=\"MessageBoxDlgYes\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default btn-primary\"");

WriteLiteral(" onclick=\"javascript:askSyncVerification.yes();\"");

WriteLiteral(" >Si</button>\r\n                <button");

WriteLiteral(" id=\"MessageBoxDlgNo\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral(" onclick=\"javascript:askSyncVerification.hide();\"");

WriteLiteral(" >No</button>\r\n            </div>\r\n        </div>\r\n        <div");

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
var askSyncHearingFormat  = {};
askSyncHearingFormat.idCase = 0;

askSyncHearingFormat.show = function (caso){
	var dlgMsgBox = $('#askSyncHearingFormatBoxDlgId');
	askSyncHearingFormat.idCase = caso;
	dlgMsgBox.show();
};

askSyncHearingFormat.hide = function (){
	askSyncHearingFormat.idCase = 0;
	var dlgMsgBox = $('#askSyncHearingFormatBoxDlgId');
	dlgMsgBox.hide();
};

askSyncHearingFormat.yes = function (){
	var password = $(""#askSyncHearingFormatcontainerPassword"").val();
	$(""#askSyncHearingFormatcontainerPassword"").val("""");
	var result = Sync.sincrinozeCase(""[""+askSyncHearingFormat.idCase+""]"", password,""hearing"");
	alert(result);
askSyncHearingFormat .idCase = 0;
};
    	</script>
<div");

WriteLiteral(" class=\"modal-dialog\"");

WriteLiteral(" style=\"display:none; width:60%; position: relative;top: 15%;left: 50%;margin: 0 " +
"0 0 -30%;\"");

WriteLiteral(" id=\"askSyncHearingFormatBoxDlgId\"");

WriteLiteral(" >\r\n        <div");

WriteLiteral(" class=\"modal-content\"");

WriteLiteral(" style=\"z-index: 1000;\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"modal-header\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"alert alert-info\"");

WriteLiteral(">\r\n                    <button");

WriteLiteral(" id=\"askSyncHearingFormatBoxDlgXclose\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"close\"");

WriteLiteral(" onclick=\"javascript:askSyncHearingFormat.hide();\"");

WriteLiteral(">×</button>\r\n                    <h4");

WriteLiteral(" class=\"modal-title element-center ng-binding\"");

WriteLiteral(" ng-bind-html=\"Title\"");

WriteLiteral(">Confirmación para Sincronizar</h4>\r\n                </div>\r\n            </div>\r\n" +
"            <div");

WriteLiteral(" class=\"modal-body\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"element-center ng-binding\"");

WriteLiteral(" ng-bind-html=\"Message\"");

WriteLiteral(">Por favor ingrese la contrasea:<br><input");

WriteLiteral(" type=\"password\"");

WriteLiteral(" name=\"usrPassword\"");

WriteLiteral(" id=\"askSyncHearingFormatcontainerPassword\"");

WriteLiteral("/>\r\n                <br><b> Una vez termine se eliminaran el caso del dispositivo" +
".</b></div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"modal-footer\"");

WriteLiteral(">\r\n                <button");

WriteLiteral(" id=\"MessageBoxDlgYes\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default btn-primary\"");

WriteLiteral(" onclick=\"javascript:askSyncHearingFormat.yes();\"");

WriteLiteral(" >Si</button>\r\n                <button");

WriteLiteral(" id=\"MessageBoxDlgNo\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral(" onclick=\"javascript:askSyncHearingFormat.hide();\"");

WriteLiteral(" >No</button>\r\n            </div>\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"blocker\"");

WriteLiteral(" style=\"z-index:999;\"");

WriteLiteral(">\r\n\t\t    <div>\r\n\t\t        Cargando...<img");

WriteLiteral(" src=\"content/images/ajax_loader.gif\"");

WriteLiteral(" alt=\"no content detected\"");

WriteLiteral(" />\r\n\t\t    </div>\r\n\t\t</div>\r\n    </div>\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n" +
"\r\n\r\n\r\n\r\n\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    function legal(err){
    	alert(""legal>>""+err);
    }
    function syncronizar(idMeet){
    var casos = ""[""+idMeet+""]"";
    	var result = Sync.sincrinozeCase(""[""+casos+""]"", ""99630110"");
    }
    function cancelari(){
		window.location.replace('hybrid:Login/Index');
    }
    </script>
<h2");

WriteLiteral(" class=\"element-center\"");

WriteLiteral("><i");

WriteLiteral(" class=\"icon-comments-alt \"");

WriteLiteral("></i>&nbsp;&nbsp;Entrevistas de evaluaci&oacute;n de riesgos procesales</h2>\r\n<br" +
" />\r\n<div");

WriteLiteral(" id=\"toolbar2\"");

WriteLiteral(" class=\"btn-group\"");

WriteLiteral(">\r\n    <a");

WriteAttribute ("href", " href=\"", "\""

#line 252 "SyncCaseList.cshtml"
, Tuple.Create<string,object,bool> ("", Url.Action("MeetingEditNew","Meeting")

#line default
#line hidden
, false)
);
WriteLiteral(">\r\n    <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral(" id=\"btnAdd\"");

WriteLiteral(">\r\n        <i");

WriteLiteral(" class=\"glyphicon glyphicon-plus\"");

WriteLiteral("></i>\r\n    </button></a>\r\n    <!--<button type=\"button\" class=\"btn btn-default\" i" +
"d=\"btnRefresh\">\r\n        <i class=\"glyphicon glyphicon-refresh\"></i>\r\n    </butt" +
"on>-->\r\n</div>\r\n    <table");

WriteLiteral(" data-toggle=\"table\"");

WriteLiteral(" data-page-size=\"10\"");

WriteLiteral(" data-page-list=\"[10, 20, 30]\"");

WriteLiteral("\r\n     data-pagination=\"true\"");

WriteLiteral(" data-height=\"250\"");

WriteLiteral("\r\n     data-striped=\"true\"");

WriteLiteral(" data-search=\"true\"");

WriteLiteral(" \r\n     data-search-align=\"right\"");

WriteLiteral(" data-toolbar=\"#toolbar2\"");

WriteLiteral(">\r\n    <thead>\r\n    <tr>\r\n        <th>ID</th>\r\n        <th");

WriteLiteral(" data-width=\"900\"");

WriteLiteral(">Carpeta de Investigaci&oacute;n</th>\r\n        <th>Nombre completo</th>\r\n        " +
"<th>Estatus</th>\r\n        <th>sincronization Type</th>\r\n        <th>Acci&oacute;" +
"n</th>\r\n    </tr>\r\n    </thead>\r\n    <tbody> \r\n");


#line 275 "SyncCaseList.cshtml"
    

#line default
#line hidden

#line 275 "SyncCaseList.cshtml"
     foreach(var Meeting in Model) {


#line default
#line hidden
WriteLiteral("    <tr");

WriteLiteral(" id=\"tr-id-1\"");

WriteLiteral(" class=\"tr-class-1\"");

WriteLiteral(">\r\n        <td");

WriteLiteral(" id=\"td-id-1\"");

WriteLiteral(" class=\"td-class-1\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");


#line 278 "SyncCaseList.cshtml"
       Write(Meeting.CaseId);


#line default
#line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("        \t");


#line 281 "SyncCaseList.cshtml"
       Write(Meeting.IdFolder);


#line default
#line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("        \t");


#line 284 "SyncCaseList.cshtml"
        Write(Meeting.Name+" "+Meeting.LastNameP+" "+Meeting.LastNameM);


#line default
#line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("       \t\t");


#line 287 "SyncCaseList.cshtml"
       Write(Meeting.StatusCode);


#line default
#line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("        \t");


#line 290 "SyncCaseList.cshtml"
       Write(Meeting.Action);


#line default
#line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");


#line 293 "SyncCaseList.cshtml"
        

#line default
#line hidden

#line 293 "SyncCaseList.cshtml"
         if(Meeting.Action=="verificationIncomplete") {


#line default
#line hidden
WriteLiteral("         \t<a");

WriteAttribute ("href", " href=\"", "\""
, Tuple.Create<string,object,bool> ("", "javascript:askSyncIncomplete.show(", true)

#line 294 "SyncCaseList.cshtml"
                , Tuple.Create<string,object,bool> ("", Meeting.CaseId

#line default
#line hidden
, false)
, Tuple.Create<string,object,bool> ("", ");", true)
);
WriteLiteral(" style=\"display:inline-block;\"");

WriteLiteral(" title=\"Sincronizar Caso\"");

WriteLiteral(" onclick=\"\"");

WriteLiteral("><i");

WriteLiteral(" class=\"icon-exchange\"");

WriteLiteral("></i></a>\r\n");


#line 295 "SyncCaseList.cshtml"
        }


#line default
#line hidden
WriteLiteral("        ");


#line 296 "SyncCaseList.cshtml"
         if(Meeting.Action=="verification") {


#line default
#line hidden
WriteLiteral("         \t<a");

WriteAttribute ("href", " href=\"", "\""
, Tuple.Create<string,object,bool> ("", "javascript:askSyncVerification.show(", true)

#line 297 "SyncCaseList.cshtml"
                  , Tuple.Create<string,object,bool> ("", Meeting.CaseId

#line default
#line hidden
, false)
, Tuple.Create<string,object,bool> ("", ");", true)
);
WriteLiteral(" style=\"display:inline-block;\"");

WriteLiteral(" title=\"Sincronizar Caso\"");

WriteLiteral(" onclick=\"\"");

WriteLiteral("><i");

WriteLiteral(" class=\"icon-exchange\"");

WriteLiteral("></i></a>\r\n");


#line 298 "SyncCaseList.cshtml"
        }


#line default
#line hidden
WriteLiteral("        ");


#line 299 "SyncCaseList.cshtml"
         if(Meeting.Action=="meeting") {


#line default
#line hidden
WriteLiteral("         \t<a");

WriteAttribute ("href", " href=\"", "\""
, Tuple.Create<string,object,bool> ("", "javascript:askSyncMeeting.show(", true)

#line 300 "SyncCaseList.cshtml"
             , Tuple.Create<string,object,bool> ("", Meeting.CaseId

#line default
#line hidden
, false)
, Tuple.Create<string,object,bool> ("", ");", true)
);
WriteLiteral(" style=\"display:inline-block;\"");

WriteLiteral(" title=\"Sincronizar Caso\"");

WriteLiteral(" onclick=\"\"");

WriteLiteral("><i");

WriteLiteral(" class=\"icon-exchange\"");

WriteLiteral("></i></a>\r\n");


#line 301 "SyncCaseList.cshtml"
        }


#line default
#line hidden
WriteLiteral("        ");


#line 302 "SyncCaseList.cshtml"
         if(Meeting.Action=="hearing") {


#line default
#line hidden
WriteLiteral("         \t<a");

WriteAttribute ("href", " href=\"", "\""
, Tuple.Create<string,object,bool> ("", "javascript:askSyncHearingFormat.show(", true)

#line 303 "SyncCaseList.cshtml"
                   , Tuple.Create<string,object,bool> ("", Meeting.CaseId

#line default
#line hidden
, false)
, Tuple.Create<string,object,bool> ("", ");", true)
);
WriteLiteral(" style=\"display:inline-block;\"");

WriteLiteral(" title=\"Sincronizar Caso\"");

WriteLiteral(" onclick=\"\"");

WriteLiteral("><i");

WriteLiteral(" class=\"icon-exchange\"");

WriteLiteral("></i></a>\r\n");


#line 304 "SyncCaseList.cshtml"
        }


#line default
#line hidden
WriteLiteral("        </td>\r\n    </tr>\r\n");


#line 307 "SyncCaseList.cshtml"
}


#line default
#line hidden
WriteLiteral("    </tbody>\r\n</table>\r\n\t<!--<span class=\"btn btn-default btn-sm\" onclick=\"cancel" +
"ari();\">\r\n        Cancelar\r\n    </span>-->\r\n\r\n\r\n\r\n");

}
}
}
#pragma warning restore 1591
