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
public partial class VerificationList : WebViewTemplate
{

#line hidden

#line 1 "VerificationList.cshtml"
public System.Collections.Generic.List<UmecaApp.MeetingTblDto> Model { get; set; }

#line default
#line hidden


public override void Execute()
{

#line 3 "VerificationList.cshtml"
  
	Layout = "UmecaApp.headUmGrid";


#line default
#line hidden
WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    function legal(err){
    	alert(""legal>>""+err);
    }
    function editMeeting(idMeet){
    	window.location.replace('hybrid:Meeting/MeetingDatosPersonales?idCase='+idMeet);
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

#line 20 "VerificationList.cshtml"
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

WriteLiteral("></i>\r\n    </button></a>\r\n    <!--<button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral(" id=\"btnRefresh\"");

WriteLiteral(">\r\n        <i");

WriteLiteral(" class=\"glyphicon glyphicon-refresh\"");

WriteLiteral("></i>\r\n    </button>-->\r\n</div>\r\n    <table");

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

WriteLiteral(@">Carpeta de Investigaci&oacute;n</th>
        <th>Nombre completo</th>
        <th>Fecha de nacimiento</th>
        <th>G&eacute;nero</th>
        <th>Estatus</th>
        <!--<th>Id estatus</th>
        <th>Status case</th>-->
        <th>Acci&oacute;n</th>
    </tr>
    </thead>
    <tbody>
");


#line 46 "VerificationList.cshtml"
    

#line default
#line hidden

#line 46 "VerificationList.cshtml"
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


#line 49 "VerificationList.cshtml"
       Write(Meeting.CaseId);


#line default
#line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("        \t");


#line 52 "VerificationList.cshtml"
    Write(Meeting.IdFolder);


#line default
#line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("        \t");


#line 55 "VerificationList.cshtml"
     Write(Meeting.Name+" "+Meeting.LastNameP+" "+Meeting.LastNameM);


#line default
#line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("        \t");


#line 58 "VerificationList.cshtml"
     Write(((DateTime) Meeting.DateBirth).ToString("yyyy/MM/dd"));


#line default
#line hidden
WriteLiteral("\r\n        </td>\r\n        <td");

WriteLiteral(" id=\"td-id-1\"");

WriteLiteral(" class=\"td-class-1\"");

WriteLiteral(">\r\n");


#line 61 "VerificationList.cshtml"
            

#line default
#line hidden

#line 61 "VerificationList.cshtml"
             if (Meeting.Gender != null) {
                        if (Meeting.Gender.Equals(true)){//true is female


#line default
#line hidden
WriteLiteral("                        \t<label>Femenino</label>\r\n");


#line 64 "VerificationList.cshtml"
                        }else{


#line default
#line hidden
WriteLiteral("                            <label>Masculino</label>\r\n");


#line 66 "VerificationList.cshtml"
                        }
                    } else {


#line default
#line hidden
WriteLiteral("                        <label>Sin proporcionar</label>\r\n");


#line 69 "VerificationList.cshtml"
                    }


#line default
#line hidden
WriteLiteral("        </td>\r\n        <td>\r\n");

WriteLiteral("        \t");


#line 72 "VerificationList.cshtml"
    Write(Meeting.Description);


#line default
#line hidden
WriteLiteral("\r\n        </td>\r\n        <!--<td>\r\n");

WriteLiteral("       \t\t");


#line 75 "VerificationList.cshtml"
    Write(Meeting.StatusCode);


#line default
#line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("        \t");


#line 78 "VerificationList.cshtml"
    Write(Meeting.ReviewerId);


#line default
#line hidden
WriteLiteral("\r\n        </td>-->\r\n        <td>\r\n");


#line 81 "VerificationList.cshtml"
         

#line default
#line hidden

#line 81 "VerificationList.cshtml"
          if(Meeting.StatusCode=="INCOMPLETE_LEGAL") {


#line default
#line hidden
WriteLiteral("         \t<a");

WriteLiteral(" href=\"javascript:;\"");

WriteLiteral(" style=\"display:inline-block;\"");

WriteLiteral(" title=\"Procesos legales usuario\"");

WriteAttribute ("onclick", " onclick=\"", "\""
, Tuple.Create<string,object,bool> ("", "legal(", true)

#line 82 "VerificationList.cshtml"
                                                                        , Tuple.Create<string,object,bool> ("", Meeting.CaseId

#line default
#line hidden
, false)
, Tuple.Create<string,object,bool> ("", ");", true)
);
WriteLiteral("><i");

WriteLiteral(" class=\"icon-legal\"");

WriteLiteral("></i></a>\r\n");


#line 83 "VerificationList.cshtml"
         }
         else if(Meeting.StatusCode=="INCOMPLETE") {


#line default
#line hidden
WriteLiteral("         \t<a");

WriteLiteral(" href=\"javascript:;\"");

WriteLiteral(" style=\"display:inline-block;\"");

WriteLiteral(" title=\"Continuar entrevista\"");

WriteAttribute ("onclick", " onclick=\"", "\""
, Tuple.Create<string,object,bool> ("", "editMeeting(", true)

#line 85 "VerificationList.cshtml"
                                                                          , Tuple.Create<string,object,bool> ("", Meeting.CaseId

#line default
#line hidden
, false)
, Tuple.Create<string,object,bool> ("", ");", true)
);
WriteLiteral("><span");

WriteLiteral(" class=\"glyphicon glyphicon-pencil\"");

WriteLiteral("></span></a>\r\n");


#line 86 "VerificationList.cshtml"
         }


#line default
#line hidden
WriteLiteral("        </td>\r\n    </tr>\r\n");


#line 89 "VerificationList.cshtml"
}


#line default
#line hidden
WriteLiteral("    </tbody>\r\n</table>\r\n\t<!--<span");

WriteLiteral(" class=\"btn btn-default btn-sm\"");

WriteLiteral(" onclick=\"cancelari();\"");

WriteLiteral(">\r\n        Cancelar\r\n    </span>-->");

}
}
}
#pragma warning restore 1591
