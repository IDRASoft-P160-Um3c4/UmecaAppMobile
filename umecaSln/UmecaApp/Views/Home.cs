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
public partial class Home : PortableRazor.ViewBase
{

#line hidden

#line 1 "Home.cshtml"
public UmecaApp.Models.PageModel Model { get; set; }

#line default
#line hidden


public override void Execute()
{
WriteLiteral("<!DOCTYPE html>\r\n<html>\r\n<head>\r\n   <title>UMECA</title>\r\n</head>\r\n<body>\r\n\r\n\r\n  " +
"              <div");

WriteLiteral(" class=\"header\"");

WriteLiteral(">\r\n                    <h2");

WriteLiteral(" class=\"modal-title element-center\"");

WriteLiteral(">");


#line 12 "Home.cshtml"
                                                      Write(Model.Title);


#line default
#line hidden
WriteLiteral("</h2>\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"modal-body\"");

WriteLiteral(">\r\n                    <h4");

WriteLiteral(" class=\"element-center\"");

WriteLiteral(">Ingrese los datos para acceder al portal</h4>\r\n                    <hr>\r\n       " +
"             <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"col-xs-3\"");

WriteLiteral(">&nbsp;</div>\r\n                        <div");

WriteLiteral(" class=\"col-xs-6\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"input-group\"");

WriteLiteral(">\r\n                                <span");

WriteLiteral(" class=\"input-group-addon\"");

WriteLiteral("><i");

WriteLiteral(" class=\"glyphicon glyphicon-lock\"");

WriteLiteral("></i></span>\r\n                                <input");

WriteLiteral(" name=\"j_password\"");

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

WriteLiteral("></span>\r\n                            </div>\r\n                        </div>\r\n\t\t\t" +
"\t\t<div");

WriteLiteral(" class=\"col-xs-3\"");

WriteLiteral(">&nbsp;</div>\r\n                    </div>\r\n                    <br>\r\n            " +
"    </div>\r\n                <div");

WriteLiteral(" class=\"modal-footer\"");

WriteLiteral(">\r\n                <a");

WriteAttribute ("href", " href=\"", "\""

#line 33 "Home.cshtml"
, Tuple.Create<string,object,bool> ("", Url.Action("Index","Meeting")

#line default
#line hidden
, false)
);
WriteLiteral(">\r\n                    <button");

WriteLiteral(" class=\"btn btn-default btn-primary btn-sm\"");

WriteLiteral(" >Ingresar</button>\r\n                </a>\r\n                </div>\r\n<br />\r\n<a");

WriteAttribute ("href", " href=\"", "\""

#line 38 "Home.cshtml"
, Tuple.Create<string,object,bool> ("", Url.Action("Index","Meeting")

#line default
#line hidden
, false)
);
WriteLiteral(">Meetings</a>\r\n<br />\r\n<a");

WriteAttribute ("href", " href=\"", "\""

#line 40 "Home.cshtml"
, Tuple.Create<string,object,bool> ("", Url.Action("MeetingEditNew","Meeting")

#line default
#line hidden
, false)
);
WriteLiteral(">New Meeting</a>\r\n<br />\r\n<a");

WriteAttribute ("href", " href=\"", "\""

#line 42 "Home.cshtml"
, Tuple.Create<string,object,bool> ("", Url.Action("MeetingDatosPersonales","Meeting")

#line default
#line hidden
, false)
, Tuple.Create<string,object,bool> ("", "?idCase=1", true)
);
WriteLiteral(">Datos Personales</a>\r\n</body>\r\n</html>");

}
}
}
#pragma warning restore 1591
