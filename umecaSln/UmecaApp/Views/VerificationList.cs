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

WriteLiteral(">\r\n    function fuentes(idCase){\r\n    \twindow.location.replace(\'hybrid:Verificati" +
"on/IndexFuentes?idCase=\'+idCase);\r\n    }\r\n    </script>\r\n<h2");

WriteLiteral(" class=\"element-center\"");

WriteLiteral("><i");

WriteLiteral(" class=\"icon-chek \"");

WriteLiteral("></i>&nbsp;&nbsp;Procesos de verificaci&oacute;n</h2>\r\n<br />\r\n    <table");

WriteLiteral(" data-toggle=\"table\"");

WriteLiteral(" data-page-size=\"10\"");

WriteLiteral(" data-page-list=\"[10, 20, 30]\"");

WriteLiteral("\r\n     data-pagination=\"true\"");

WriteLiteral(" data-height=\"250\"");

WriteLiteral("\r\n     data-striped=\"true\"");

WriteLiteral(" data-search=\"true\"");

WriteLiteral(" \r\n     data-search-align=\"right\"");

WriteLiteral(" data-toolbar=\"#toolbar2\"");

WriteLiteral(@">
    <thead>
    <tr>
        <th>ID</th>
        <th>Carpeta de Investigaci&oacute;n</th>
        <th>Nombre completo</th>
        <th>G&eacute;nero</th>
        <th>Estatus</th>
        <th>Acci&oacute;n</th>
    </tr>
    </thead>
    <tbody>
");


#line 28 "VerificationList.cshtml"
    

#line default
#line hidden

#line 28 "VerificationList.cshtml"
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


#line 31 "VerificationList.cshtml"
       Write(Meeting.CaseId);


#line default
#line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("        \t");


#line 34 "VerificationList.cshtml"
       Write(Meeting.IdFolder);


#line default
#line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("        \t");


#line 37 "VerificationList.cshtml"
        Write(Meeting.Name+" "+Meeting.LastNameP+" "+Meeting.LastNameM);


#line default
#line hidden
WriteLiteral("\r\n        </td>\r\n        <td");

WriteLiteral(" id=\"td-id-1\"");

WriteLiteral(" class=\"td-class-1\"");

WriteLiteral(">\r\n");


#line 40 "VerificationList.cshtml"
            

#line default
#line hidden

#line 40 "VerificationList.cshtml"
             if (Meeting.Gender != null) {
                        if (Meeting.Gender.Equals(true)){//true is female


#line default
#line hidden
WriteLiteral("                        \t<label>Femenino</label>\r\n");


#line 43 "VerificationList.cshtml"
                        }else{


#line default
#line hidden
WriteLiteral("                            <label>Masculino</label>\r\n");


#line 45 "VerificationList.cshtml"
                        }
                    } else {


#line default
#line hidden
WriteLiteral("                        <label>Sin proporcionar</label>\r\n");


#line 48 "VerificationList.cshtml"
                    }


#line default
#line hidden
WriteLiteral("        </td>\r\n        <td>\r\n");

WriteLiteral("        ");


#line 51 "VerificationList.cshtml"
   Write(Meeting.StatusCode);


#line default
#line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n         \r\n         \t<a");

WriteLiteral(" href=\"javascript:;\"");

WriteLiteral(" style=\"display:inline-block;\"");

WriteLiteral(" title=\"Entrevistar fuentes\"");

WriteAttribute ("onclick", " onclick=\"", "\""
, Tuple.Create<string,object,bool> ("", "fuentes(", true)

#line 55 "VerificationList.cshtml"
                                                                       , Tuple.Create<string,object,bool> ("", Meeting.CaseId

#line default
#line hidden
, false)
, Tuple.Create<string,object,bool> ("", ");", true)
);
WriteLiteral("><i");

WriteLiteral(" class=\"icon-group blue\"");

WriteLiteral("></i></a>\r\n         \r\n        </td>\r\n    </tr>\r\n");


#line 59 "VerificationList.cshtml"
}


#line default
#line hidden
WriteLiteral("    </tbody>\r\n</table>\r\n\t<!--<span class=\"btn btn-default btn-sm\" onclick=\"cancel" +
"ari();\">\r\n        Cancelar\r\n    </span>-->");

}
}
}
#pragma warning restore 1591
