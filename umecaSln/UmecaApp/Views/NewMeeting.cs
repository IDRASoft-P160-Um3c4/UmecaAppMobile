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
public partial class NewMeeting : WebViewTemplate
{

#line hidden

#line 1 "NewMeeting.cshtml"
public UmecaApp.NewMeetingDto Model { get; set; }

#line default
#line hidden


public override void Execute()
{

#line 3 "NewMeeting.cshtml"
  
	Layout = "UmecaApp.headUm";


#line default
#line hidden
WriteLiteral(@"
      <script>
    		 
    	app.controller('newMeettingController', function($scope, $http){
			$scope.m = {};
			$scope.Wait = false;
			$scope.isAccepted = {};

			$scope.save = function(){
				$scope.Wait = true;
				//todos los key del json deben ser igual al modelo cs
				if($(""#frmSubmitValuesMeeting"").valid()==false){
					$scope.Wait = false;
					return false;
				}
				var jsonData = JSON.stringify($scope.m);
				window.location.replace('hybrid:Meeting/AddMeeting?model=' + encodeURIComponent(jsonData));
				$scope.Wait = false;
			};

			$scope.cancel = function(){
				$scope.saving = false;
				//todos los key del json deben ser igual al modelo cs
				window.location.replace('hybrid:Meeting/Index');
			};

		});

    </script>
<form");

WriteLiteral(" id=\"frmSubmitValuesMeeting\"");

WriteLiteral(" ng-app=\"umecaMobile\"");

WriteLiteral(" ng-controller=\"newMeettingController\"");

WriteLiteral(">\r\n");


#line 35 "NewMeeting.cshtml"
 if(Model.ResponseMessage!=null){


#line default
#line hidden
WriteLiteral("\t<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n\t    <div");

WriteLiteral(" class=\"col-xs-12\"");

WriteLiteral(">\r\n\t        <div");

WriteLiteral(" class=\"alert alert-danger element-center error-font\"");

WriteLiteral(">\r\n\t            <span>");


#line 39 "NewMeeting.cshtml"
                 Write(Model.ResponseMessage);


#line default
#line hidden
WriteLiteral("\r\n\t            </span>\r\n\t        </div>\r\n\t    </div>\r\n\t</div>\r\n");


#line 44 "NewMeeting.cshtml"
}


#line default
#line hidden
WriteLiteral("    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-xs-12\"");

WriteLiteral(">\r\n            <label>Ingrese la información requerida para poder generar un nuev" +
"o número de expediente:</label>\r\n        </div>\r\n    </div>\r\n    <br>\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-xs-5 element-left\"");

WriteLiteral(">\r\n            Nombre:\r\n        </div>\r\n        <div");

WriteLiteral(" id=\"nombreA\"");

WriteLiteral(" class=\"col-xs-7\"");

WriteLiteral(@">
            <!--<input class=""form-control ng-pristine n2g-valid"" data-val=""true"" data-val-length=""Debe tener al menos 3 y máximo 50 caracteres"" data-val-required=""El nombre es un campo requerido"" data-val-length-max=""50"" data-val-length-min=""3"" id=""name"" name=""name"" type=""text"">-->
");

WriteLiteral("            ");


#line 57 "NewMeeting.cshtml"
       Write(Html.TextBox("name", @Model.Name, new {@ng_model="m.Name", @data_val="true", @data_val_length="Debe tener al menos 3 y máximo 50 caracteres", @data_val_required="El nombre es un campo requerido", @data_val_length_max="50", @data_val_length_min="3",  @ng_init="m.Name='"+@Model.Name+"'", @class="form-control"}));


#line default
#line hidden
WriteLiteral("\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"col-xs-9 col-xs-offset-3\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"name\"");

WriteLiteral(" data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n        </div>\r\n    </div>\r\n    <br>\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-xs-5 element-left\"");

WriteLiteral(">\r\n            Apellido paterno:\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"col-xs-7\"");

WriteLiteral(@">
            <!--<input class=""form-control ng-pristine ng-valid"" data-val=""true"" data-val-length=""Debe tener al menos 3 y máximo 50 caracteres"" data-val-length-max=""50"" data-val-length-min=""3"" data-val-required=""El apellido paterno es un campo requerido"" id=""lastNameP"" name=""lastNameP"" type=""text"">-->
");

WriteLiteral("            ");


#line 70 "NewMeeting.cshtml"
       Write(Html.TextBox("lastNameP", @Model.LastNameP, new {@ng_model="m.LastNameP", @data_val="true", @data_val_length="Debe tener al menos 3 y máximo 50 caracteres", @data_val_length_max="50", @data_val_length_min="3", @data_val_required="El apellido paterno es un campo requerido", @ng_init="m.LastNameP='"+@Model.LastNameP+"'", @class="form-control"}));


#line default
#line hidden
WriteLiteral("\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"col-xs-9 col-xs-offset-3\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"lastNameP\"");

WriteLiteral(" data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n        </div>\r\n    </div>\r\n\r\n    <br>\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-xs-5 element-left\"");

WriteLiteral(">\r\n            Apellido materno:\r\n        </div> \r\n        <div");

WriteLiteral(" class=\"col-xs-7\"");

WriteLiteral(@">
            <!--<input class=""form-control ng-pristine ng-valid"" data-val=""true"" data-val-length=""Debe tener al menos 3 y máximo 50 caracteres"" data-val-length-max=""50"" data-val-length-min=""3"" data-val-required=""El apellido materno es un campo requerido"" id=""lastNameM"" name=""lastNameM"" type=""text"">-->
");

WriteLiteral("            ");


#line 84 "NewMeeting.cshtml"
       Write(Html.TextBox("lastNameM", @Model.LastNameM, new {@ng_model="m.LastNameM", @data_val="true", @data_val_length="Debe tener al menos 3 y máximo 50 caracteres", @data_val_length_max="50", @data_val_length_min="3", @data_val_required="El apellido materno es un campo requerido", @ng_init="m.LastNameM='"+@Model.LastNameM+"'",  @class="form-control ng-pristine ng-valid"}));


#line default
#line hidden
WriteLiteral("\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"col-xs-9 col-xs-offset-3\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"lastNameM\"");

WriteLiteral(" data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n        </div>\r\n    </div>\r\n    <br>\r\n                        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-xs-5 element-left\"");

WriteLiteral(">\r\n                                Fecha de nacimiento:\r\n                        " +
"    </div>\r\n                            <div");

WriteLiteral(" class=\"col-xs-7\"");

WriteLiteral(">\r\n                                <div");

WriteLiteral(" class=\"input-group\"");

WriteLiteral(@">
                                    <!--<input class=""form-control date-picker"" readonly=""readonly"" type=""text"" data-date-format=""yyyy/mm/dd""
                                            data-val=""true"" data-val-required=""La fecha de nacimiento es un campo requerido"" ng-init=""birthDate=''""
                                            id=""birthDate"" name=""birthDate"" ng-model=""birthDate""/>
                                            <span class=""input-group-addon"">
                                                        <i class=""icon-calendar bigger-110"" ></i>
                                            </span>-->
");

WriteLiteral("                                    ");


#line 103 "NewMeeting.cshtml"
                               Write(Html.TextBox("DateBirth", @Model.DateBirth,
                                     new {@ng_model="m.DateBirth", @data_val="true", @data_date_format="yyyy/mm/dd",
                                      @data_val_required="La fecha de nacimiento es un campo requerido", @ng_init="m.DateBirth='"+@Model.DateBirthString+"'",
                                       @class="form-control date-picker", @readonly="readonly"}));


#line default
#line hidden
WriteLiteral("\r\n                                            <span");

WriteLiteral(" class=\"input-group-addon\"");

WriteLiteral(">\r\n                                                        <i");

WriteLiteral(" class=\"icon-calendar bigger-110\"");

WriteLiteral(" ></i>\r\n                                            </span>\r\n                    " +
"            </div>\r\n                            </div>\r\n                        " +
"    <div");

WriteLiteral(" class=\"col-xs-9 col-xs-offset-3\"");

WriteLiteral(">\r\n                                <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"DateBirth\"");

WriteLiteral(" data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n                            </div>\r\n                        </div>aaa\r\n" +
"    <br>\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-xs-5 element-left\"");

WriteLiteral(">\r\n            Género:\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"col-xs-7\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(" ng-init=\"m.Gender=false\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"col-xs-6\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"radio\"");

WriteLiteral(@">
                        <label>
                            <!--<input class=""ace"" type=""radio"" ng-checked=""gen==true"" name=""gender""
                                                       data-val-required=""El g?nero es un campo requerido"" id=""genero"" value=""true""
                                                       ng-model=""gen"">-->
");

WriteLiteral("                            ");


#line 129 "NewMeeting.cshtml"
                       Write(Html.RadioButton("gender", true, @Model.Gender.GetValueOrDefault(), new {@ng_model="m.Gender",  @ng_checked="m.Gender==true", @data_val_required="El g?nero es un campo requerido"	, @class="ace ng-pristine ng-valid"}));


#line default
#line hidden
WriteLiteral("\r\n                            <span");

WriteLiteral(" class=\"lbl\"");

WriteLiteral(">Femenino</span>\r\n                        </label>\r\n                    </div>\r\n " +
"               </div>\r\n                <div");

WriteLiteral(" class=\"col-xs-6\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"radio\"");

WriteLiteral(">\r\n                        <label>\r\n                            <!--<input class=" +
"\"ace ng-pristine ng-valid\" type=\"radio\" value=\"false\" ng-model=\"gen\" ng-checked=" +
"\"gen==false\" name=\"gender\" checked=\"checked\">-->\r\n");

WriteLiteral("                            ");


#line 138 "NewMeeting.cshtml"
                       Write(Html.RadioButton("gender", false, !@Model.Gender.GetValueOrDefault(), new {@ng_model="m.Gender", @ng_checked="m.Gender==false", @class="ace ng-pristine ng-valid"}));


#line default
#line hidden
WriteLiteral("\r\n                            <span");

WriteLiteral(" class=\"lbl\"");

WriteLiteral(">Masculino</span>\r\n                        </label>\r\n                    </div>\r\n" +
"                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <br>" +
"\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-xs-5 element-left\"");

WriteLiteral(">\r\n           Carpeta de investigación:\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"col-xs-7\"");

WriteLiteral(@">
                <!--<input class=""form-control ng-pristine ng-valid"" type=""text"" data-val-length=""Debe tener al menos 1 y máximo 35 caracteres"" data-val-length-max=""35"" data-val-length-min=""1"" data-val=""true"" data-val-required=""La carpeta de investigación es un campo requerido"" ng-init=""m.idFolder=''"" id=""meeting.caseDetention.idFolder"" name=""meeting.caseDetention.idFolder"" ng-model=""m.idFolder"">-->
");

WriteLiteral("                ");


#line 153 "NewMeeting.cshtml"
           Write(Html.TextBox("meeting.caseDetention.idFolder", @Model.IdFolder, new {@ng_model="m.IdFolder", @data_val="true", @data_val_length="Debe tener al menos 1 y máximo 35 caracteres", @data_val_length_max="35", @data_val_length_min="1", @data_val_required="La carpeta de investigación es un campo requerido", @class="form-control ng-pristine ng-valid"}));


#line default
#line hidden
WriteLiteral("\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"col-xs-9 col-xs-offset-3\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"meeting.caseDetention.idFolder\"");

WriteLiteral(" data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n        </div>\r\n    </div>\r\n    <br>\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-xs-12 text-danger\"");

WriteLiteral(">\r\n                <i");

WriteLiteral(" class=\"icon-warning-sign icon-animated-wrench bigger-120\"");

WriteLiteral("></i>&nbsp;\r\n               La carpeta de investigación no podrá ser modificada d" +
"urante la entrevista.\r\n        </div>\r\n    </div>\r\n    <br>\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-xs-12 element-center\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" id=\"isAccepted\"");

WriteLiteral(" ng-model=\"isAccepted\"");

WriteLiteral(" ng-init=\"isAccepted=false\"");

WriteLiteral(" class=\"ng-pristine ng-valid\"");

WriteLiteral(">\r\n            <label");

WriteLiteral(" class=\"info-note\"");

WriteLiteral(" for=\"isAccepted\"");

WriteLiteral(">¿El imputado acepta que se realice la entrevista de riesgos procesales?</label>\r" +
"\n        </div>\r\n    </div>\r\n\r\n<br>\r\n                <div");

WriteLiteral(" class=\"modal-footer\"");

WriteLiteral(">\r\n                    <span");

WriteLiteral(" class=\"btn btn-default btn-sm\"");

WriteLiteral(" ng-click=\"cancel()\"");

WriteLiteral(">\r\n                        Cancelar\r\n                    </span>\r\n               " +
"     <span");

WriteLiteral(" class=\"btn btn-default btn-primary btn-sm\"");

WriteLiteral(" ng-disabled=\"Wait==true || isAccepted == false\"");

WriteLiteral("\r\n                          ng-click=\"save();\"");

WriteLiteral(@">
                          Continuar
                    </span>
                </div>
                </form>
    <script>
        var date=new Date();
        date.setFullYear(date.getFullYear()-18);
        $('.date-picker').datepicker({autoclose:true, endDate:date}).next().on(ace.click_event, function(){
            $(this).prev().focus();
        });
    </script>");

}
}
}
#pragma warning restore 1591
