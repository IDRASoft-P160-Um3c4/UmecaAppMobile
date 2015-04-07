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
public partial class AddressUpsert : WebViewTemplate
{

#line hidden

#line 1 "AddressUpsert.cshtml"
public UmecaApp.ImputedHome Model { get; set; }

#line default
#line hidden


public override void Execute()
{

#line 3 "AddressUpsert.cshtml"
  
Layout = "UmecaApp.headUm";


#line default
#line hidden
WriteLiteral("\r\n\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    app.controller('AddressUpsertControler', function($scope, $http, $timeout, $sce){

    $timeout(function () {
    		$scope.init();
		}, 0);

		$scope.init = function(){
		    try{
		    $scope.propiedades=true;
		    console.log(""inicia AddressUpsertControler"");
		    }
		    catch(err){
		    	alert(""error catched Iniciate angular function erro==>""+err.message);
		    }
		};
		    
		//regresa la pantalla de seleccion de meeting
		$scope.cancel = function(){
			window.location.replace('hybrid:Meeting/Index');
		};

});

</script>

<div");

WriteLiteral(" ng-app=\"umecaMobile\"");

WriteLiteral(" ng-controller=\"AddressUpsertControler\"");

WriteLiteral(">\r\n    <div >\r\n        <div");

WriteLiteral(" style=\"width:95%\"");

WriteLiteral(">\r\n        \t\t<div");

WriteLiteral(" class=\"modal-header\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"alert alert-info \"");

WriteLiteral(">\r\n                        <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"close\"");

WriteLiteral(" data-dismiss=\"modal\"");

WriteLiteral(" aria-hidden=\"true\"");

WriteLiteral(">&times;</button>\r\n                        <h4");

WriteLiteral(" class=\"element-center\"");

WriteLiteral("><i");

WriteLiteral(" class=\"glyphicon glyphicon-home \"");

WriteLiteral("></i>&nbsp;&nbsp;Domicilio</h4>\r\n                    </div>\r\n                </di" +
"v>\r\n                <!--componente adress by cp-->\r\n                <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n\t\t\t\t  <div");

WriteLiteral(" class=\"col-xs-5\"");

WriteLiteral(">\r\n\t\t\t\t    <label");

WriteLiteral(" for=\"zipCode\"");

WriteLiteral(">C&oacute;digo postal:</label>\r\n\t\t\t\t    <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"zipCode\"");

WriteLiteral(" name=\"zipCode\"");

WriteLiteral(" class=\"input-xxlarge\"");

WriteLiteral(" ng-model=\"zipCode\"");

WriteLiteral("\r\n\t\t\t\t    data-val=\"true\"");

WriteLiteral(" data-val-required=\"El c&oacute;digo postal es un campo requerido\"");

WriteLiteral("\r\n\t\t\t\t    placeholder=\"Escriba su C.P. para buscar..\"");

WriteLiteral("\r\n\t\t\t\t    data-val-length-max=\"6\"");

WriteLiteral(" data-val-length-min=\"1\"");

WriteLiteral("\r\n\t\t\t\t    data-val-length=\"Debe tener al menos 1 y m&aacute;ximo 6 caracteres.\"");

WriteLiteral("/>\r\n\t\t\t\t    <br>\r\n\t\t\t\t    <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"zipCode\"");

WriteLiteral(" data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n\t\t\t\t  </div>\r\n\t\t\t\t  <div");

WriteLiteral(" class=\"col-xs-7\"");

WriteLiteral(">\r\n\t\t\t\t    <br/>\r\n\t\t\t\t    <div");

WriteLiteral(" ng-show=\"msgZipCode\"");

WriteLiteral(" class=\"alert-danger element-center\"");

WriteLiteral(">\r\n\t\t\t\t      {{msgZipCode}}\r\n\t\t\t\t    </div>\r\n\t\t\t\t  </div>\r\n\t\t\t\t</div>\r\n\r\n\t\t\t\t<div" +
"");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n\t\t\t\t  <div");

WriteLiteral(" class=\"col-xs-6\"");

WriteLiteral(">\r\n\t\t\t\t    <label>Estado:</label>\r\n\t\t\t\t    <select");

WriteLiteral(" class=\"form-control element-center\"");

WriteLiteral(" ng-model=\"state\"");

WriteLiteral(" \r\n\t\t\t\t    ng-options=\"e.name for e in listState\"");

WriteLiteral("\r\n\t\t\t\t    ng-change=\"stateId = state.id;\"");

WriteLiteral("></select><br/>\r\n\r\n\t\t\t\t  </div>\r\n\t\t\t\t  <div");

WriteLiteral(" class=\"col-xs-6\"");

WriteLiteral(">\r\n\t\t\t\t    <label>Municipio:</label>\r\n\t\t\t\t    <select");

WriteLiteral(" class=\"form-control element-center\"");

WriteLiteral(" ng-model=\"municipality\"");

WriteLiteral("\r\n\t\t\t\t    ng-options=\"e.name for e in listMunicipality\"");

WriteLiteral(" ng-init=\'listMunicipaliti = \"\"\'");

WriteLiteral("\r\n\t\t\t\t    ng-change=\"municipalityId = municipality.id;\"");

WriteLiteral("></select><br/>\r\n\t\t\t\t  </div>\r\n\t\t\t\t</div>\r\n\t\t\t\t<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n\t\t\t\t  <div");

WriteLiteral(" class=\"col-xs-12\"");

WriteLiteral(">\r\n\t\t\t\t    <label>Localidad/Colonia:</label>\r\n\t\t\t\t    <br/>\r\n\t\t\t\t    <select");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" ng-model=\"location\"");

WriteLiteral("\r\n\t\t\t\t    ng-options=\"e.name for e in listLocation\"");

WriteLiteral(" ng-init=\'listLocation = \"\"\'");

WriteLiteral("\r\n\t\t\t\t    ng-change=\"locationId = location.id; zipCode = location.zipCode; refres" +
"hMap();\"");

WriteLiteral("></select>\r\n\t\t\t\t  </div>\r\n\t\t\t\t</div>\r\n\t\t\t\t<br/>\r\n\t\t\t\t<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n\t\t\t\t\t  \t<div");

WriteLiteral(" class=\"col-xs-6\"");

WriteLiteral(">\r\n\t\t\t\t\t\t    Calle <br/>\r\n\t\t\t\t\t\t    <input");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" data-val=\"true\"");

WriteLiteral(" data-val-required=\"La calle es un campo requerido\"");

WriteLiteral("\r\n\t\t\t\t\t\t    data-val-length-max=\"100\"");

WriteLiteral(" data-val-length-min=\"1\"");

WriteLiteral("\r\n\t\t\t\t\t\t    data-val-length=\"Debe tener al menos 1 y m&aacute;ximo 100 caracteres" +
".\"");

WriteLiteral("\r\n\t\t\t\t\t\t    type=\"text\"");

WriteLiteral(" value=\"\"");

WriteLiteral(" ng-model=\"street\"");

WriteLiteral(" name=\"streetComponent\"");

WriteLiteral(" id=\"streetComponent\"");

WriteLiteral(">\r\n\t\t\t\t\t\t    <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"streetComponent\"");

WriteLiteral("\r\n\t\t\t\t\t\t    data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n\t\t\t\t\t\t  </div>\r\n\t\t\t\t\t\t  <div");

WriteLiteral(" class=\"col-xs-3\"");

WriteLiteral(">\r\n\t\t\t\t\t\t    No Ext <br/>\r\n\t\t\t\t\t\t    <input");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" data-val=\"true\"");

WriteLiteral("\r\n\t\t\t\t\t\t    data-val-length=\"Debe tener al menos 1 y m&aacute;ximo 10 caracteres\"" +
"");

WriteLiteral("\r\n\t\t\t\t\t\t    data-val-length-max=\"10\"");

WriteLiteral(" data-val-length-min=\"1\"");

WriteLiteral("\r\n\t\t\t\t\t\t    data-val-required=\"El n&uacute;mero exterior es un campo requerido\"");

WriteLiteral("\r\n\t\t\t\t\t\t    type=\"text\"");

WriteLiteral(" ng-model=\"outNum\"");

WriteLiteral("\r\n\t\t\t\t\t\t    id=\"outNumComponent\"");

WriteLiteral("\r\n\t\t\t\t\t\t    name=\"outNumComponent\"");

WriteLiteral(">\r\n\t\t\t\t\t\t    <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"outNumComponent\"");

WriteLiteral("\r\n\t\t\t\t\t\t    data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n\t\t\t\t\t\t  </div>\r\n\t\t\t\t\t\t  <div");

WriteLiteral(" class=\"col-xs-3\"");

WriteLiteral(">\r\n\t\t\t\t\t\t    No Int <br/>\r\n\t\t\t\t\t\t    <input");

WriteLiteral(" class=\"with-100\"");

WriteLiteral(" data-val=\"true\"");

WriteLiteral("\r\n\t\t\t\t\t\t    data-val-length=\"Debe tener al menos 1 y m&aacute;ximo 10 caracteres\"" +
"");

WriteLiteral("\r\n\t\t\t\t\t\t    data-val-length-max=\"10\"");

WriteLiteral(" data-val-length-min=\"1\"");

WriteLiteral("\r\n\t\t\t\t\t\t    type=\"text\"");

WriteLiteral(" ng-model=\"innNum\"");

WriteLiteral("\r\n\t\t\t\t\t\t    id=\"innNumComponent\"");

WriteLiteral("\r\n\t\t\t\t\t\t    name=\"innNumComponent\"");

WriteLiteral(">\r\n\t\t\t\t\t\t    <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"innNumComponent\"");

WriteLiteral("\r\n\t\t\t\t\t\t    data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n\t\t\t\t\t\t  </div>\r\n\t\t\t\t</div>\r\n\t\t\t\t<br/>\r\n\t\t\t\t<!--END componente adress by" +
" cp-->\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n  <div");

WriteLiteral(" class=\"col-xs-3 element-left col-xs-offset-1\"");

WriteLiteral(">\r\n    Tel&eacute;fono:\r\n  </div>\r\n  <div");

WriteLiteral(" class=\"col-xs-9\"");

WriteLiteral(">\r\n    <textarea");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"width-100\"");

WriteLiteral(" ng-model=\"a.phone\"");

WriteLiteral(" name=\"phone\"");

WriteLiteral("\r\n    id=\"phone\"");

WriteLiteral("\r\n    data-val=\"true\"");

WriteLiteral("\r\n    data-val-length=\"Debe tener al menos 5 y m&aacute;ximo 200 caracteres\"");

WriteLiteral("\r\n    data-val-length-max=\"200\"");

WriteLiteral(" data-val-length-min=\"5\"");

WriteLiteral("\r\n    data-val-required=\"El tel&eacute;efono es un campo requerido\"");

WriteLiteral(">{{a.phone}}</textarea>\r\n    <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"phone\"");

WriteLiteral("\r\n    data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n  </div>\r\n</div>\r\n<br/>\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"col-xs-2 element-left col-xs-offset-1\"");

WriteLiteral(">\r\n        Tipo de propiedad:\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"col-xs-2\"");

WriteLiteral(">\r\n        <select");

WriteLiteral(" class=\"form-control element-center\"");

WriteLiteral(" ng-model=\"a.homeType\"");

WriteLiteral("\r\n                ng-options=\"e.name for e in lstHomeType\"");

WriteLiteral("\r\n                ng-change=\"a.homeTypeId = a.homeType.id;\"");

WriteLiteral(" ></select>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"col-xs-3 element-right\"");

WriteLiteral(">\r\n        Tipo de domiclio:\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"col-xs-3\"");

WriteLiteral(">\r\n        <select");

WriteLiteral(" class=\"form-control element-center\"");

WriteLiteral(" ng-model=\"a.type\"");

WriteLiteral("\r\n                ng-options=\"e.name for e in listType\"");

WriteLiteral("\r\n                ng-change=\"a.typeId = a.type.id;\"");

WriteLiteral(" ></select>\r\n    </div>\r\n</div>\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(" ng-show=\"a.homeType.specification == true\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"col-xs-3 element-right\"");

WriteLiteral(">\r\n        Especif&iacute;que tipo de propiedad:\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"col-xs-8\"");

WriteLiteral(">\r\n        <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" ng-model=\"a.specification\"");

WriteLiteral(" name=\"specification\"");

WriteLiteral("\r\n               id=\"specification\"");

WriteLiteral("\r\n               data-val=\"true\"");

WriteLiteral("\r\n               data-val-length=\"Debe tener al menos 1 y m&aacute;ximo 50 caract" +
"eres\"");

WriteLiteral("\r\n               data-val-length-max=\"50\"");

WriteLiteral(" data-val-length-min=\"1\"");

WriteLiteral("\r\n               data-val-required=\"La especificaci&oacute;n del tipo de propieda" +
"d es un campo requerido\"");

WriteLiteral(" >\r\n         <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"specification\"");

WriteLiteral("\r\n               data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n    </div>\r\n</div>\r\n\r\n\r\n\r\n    <!--Seccion dependiente del tipo de domic" +
"ilio-->\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(" ng-show=\"a.typeId==3\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-xs-10 col-xs-offset-1\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"widget-box\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"widget-header\"");

WriteLiteral(">\r\n                    <h4>Domicilio {{a.type.name}}</h4>\r\n                </div>" +
"\r\n                <div");

WriteLiteral(" class=\"widget-body\"");

WriteLiteral(">\r\n                    <br/>\r\n\r\n                    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"col-xs-10 col-xs-offset-1\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                                <div");

WriteLiteral(" class=\"col-xs-5 element-left\"");

WriteLiteral(">\r\n                                    Tiempo de residencia:\r\n                   " +
"             </div>\r\n                                <div");

WriteLiteral(" class=\"col-xs-7\"");

WriteLiteral(">\r\n                                    <input");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" data-val=\"true\"");

WriteLiteral("\r\n                                           data-val-length=\"Debe tener al menos" +
" 1 y m&aacute;ximo 30 caracteres\"");

WriteLiteral("\r\n                                           data-val-length-max=\"30\"");

WriteLiteral(" data-val-length-min=\"1\"");

WriteLiteral("\r\n                                           data-val-required=\"El tiempo de resi" +
"dencia es un campo requerido\"");

WriteLiteral("\r\n                                           id=\"timeLiveD\"");

WriteLiteral("\r\n                                           ng-model=\"a.timeLive\"");

WriteLiteral("\r\n                                           name=\"timeLiveD\"");

WriteLiteral("\r\n                                           type=\"text\"");

WriteLiteral(">\r\n                                </div>\r\n                            </div>\r\n  " +
"                          <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                                <div");

WriteLiteral(" class=\"col-xs-8 col-xs-offset-4\"");

WriteLiteral(">\r\n                                    <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"timeLiveD\"");

WriteLiteral("\r\n                                          data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n                                </div>\r\n                            </d" +
"iv>\r\n                            <br/>\r\n\r\n                            <div");

WriteLiteral(" class=\"row element-left\"");

WriteLiteral(">\r\n                                <div");

WriteLiteral(" class=\"col-xs-3\"");

WriteLiteral(">\r\n                                    Motivo de la mudanza:\r\n                   " +
"             </div>\r\n                                <div");

WriteLiteral(" class=\"col-xs-9\"");

WriteLiteral(">\r\n                                    <textarea");

WriteLiteral(" id=\"reasonChange\"");

WriteLiteral(" name=\"reasonChange\"");

WriteLiteral(" ng-model=\"a.reasonChange\"");

WriteLiteral("\r\n                                              class=\"form-control\"");

WriteLiteral("\r\n                                              data-val=\"true\"");

WriteLiteral("\r\n                                              data-val-length=\"Debe tener al me" +
"nos 1 y m&aacute;ximo 500 caracteres\"");

WriteLiteral("\r\n                                              data-val-length-max=\"500\"");

WriteLiteral(" data-val-length-min=\"1\"");

WriteLiteral("\r\n                                              data-val-required=\"La raz&oacute;" +
"n de cambio es un campo requerido\"");

WriteLiteral("></textarea>\r\n                                    <br/>\r\n                        " +
"            <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"reasonChange\"");

WriteLiteral("\r\n                                          data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n                                </div>\r\n                            </d" +
"iv>\r\n                            <br/>\r\n\r\n                            <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                                <div");

WriteLiteral(" class=\"hr hr-8\"");

WriteLiteral("></div>\r\n                            </div>\r\n                        </div>\r\n    " +
"                </div>\r\n                </div>\r\n            </div>\r\n        </di" +
"v>\r\n    </div>\r\n\r\n\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(" ng-show=\"a.typeId ==1 || a.typeId ==2\"");

WriteLiteral(">\r\n  <div");

WriteLiteral(" class=\"col-xs-10 col-xs-offset-1\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"widget-box\"");

WriteLiteral(">\r\n      <div");

WriteLiteral(" class=\"widget-header\"");

WriteLiteral(">\r\n        <h4>Domicilio {{a.type.name}}</h4>\r\n      </div>\r\n\r\n      <div");

WriteLiteral(" class=\"widget-body\"");

WriteLiteral(">\r\n        <br/>\r\n\r\n        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n          <div");

WriteLiteral(" class=\"col-xs-10 col-xs-offset-1\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n              <div");

WriteLiteral(" class=\"col-xs-5\"");

WriteLiteral(">\r\n                Tiempo de vivir en el domicilio:\r\n              </div>\r\n      " +
"        <div");

WriteLiteral(" class=\"col-xs-7\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" data-val=\"true\"");

WriteLiteral("\r\n                data-val-length=\"Debe tener al menos 1 y m&aacute;ximo 30 carac" +
"teres\"");

WriteLiteral("\r\n                data-val-length-max=\"30\"");

WriteLiteral(" data-val-length-min=\"1\"");

WriteLiteral("\r\n                data-val-required=\"El tiempo de residencia es un campo requerid" +
"o\"");

WriteLiteral("\r\n                id=\"timeLiveA\"");

WriteLiteral("\r\n                ng-model=\"a.timeLive\"");

WriteLiteral("\r\n                name=\"timeLiveA\"");

WriteLiteral("\r\n                type=\"text\"");

WriteLiteral(">\r\n              </div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n              <div");

WriteLiteral(" class=\"col-xs-8 col-xs-offset-4\"");

WriteLiteral(">\r\n                <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"timeLiveA\"");

WriteLiteral("\r\n                data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n              </div>\r\n            </div>\r\n            <br/>\r\n\r\n        " +
"    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n              <div");

WriteLiteral(" class=\"col-xs-3\"");

WriteLiteral(">\r\n                Descripci&oacute;n de c&oacute;mo llegar al domicilio:<br/>\r\n " +
"               <label");

WriteLiteral(" class=\"info-example\"");

WriteLiteral(">(color de casa, ruta, etc.)</label>\r\n              </div>\r\n              <div");

WriteLiteral(" class=\"col-xs-9\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" id=\"description\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" name=\"description\"");

WriteLiteral("\r\n                ng-model=\"a.description\"");

WriteLiteral("\r\n                data-val=\"true\"");

WriteLiteral("\r\n                data-val-length=\"Debe tener al menos 1 y m&aacute;ximo 500 cara" +
"cteres\"");

WriteLiteral("\r\n                data-val-length-max=\"500\"");

WriteLiteral(" data-val-length-min=\"1\"");

WriteLiteral("\r\n                data-val-required=\"La descripci&oacute;n es un campo requerido\"" +
"");

WriteLiteral("></textarea>\r\n                <br/>\r\n                <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"description\"");

WriteLiteral("\r\n                data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n              </div>\r\n            </div>\r\n            <br/>\r\n          " +
"  <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(" ng-show=\"a.typeId==2\"");

WriteLiteral(">\r\n              <div");

WriteLiteral(" class=\"col-xs-3\"");

WriteLiteral(">\r\n               Raz&oacute;n por la que tiene <br/>un domicilio secundario:\r\n  " +
"           </div>\r\n             <div");

WriteLiteral(" class=\"col-xs-9\"");

WriteLiteral(">\r\n              <textarea");

WriteLiteral(" id=\"reasonSecondary\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" name=\"reasonSecondary\"");

WriteLiteral("\r\n              ng-model=\"a.reasonSecondary\"");

WriteLiteral("\r\n              data-val=\"true\"");

WriteLiteral("\r\n              data-val-length=\"Debe tener al menos 1 y m&aacute;ximo 500 caract" +
"eres\"");

WriteLiteral("\r\n              data-val-length-max=\"500\"");

WriteLiteral(" data-val-length-min=\"1\"");

WriteLiteral("\r\n              data-val-required=\"La raz&oacute;n es un campo requerido\"");

WriteLiteral("></textarea>\r\n              <br/>\r\n              <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"reasonSecondary\"");

WriteLiteral("\r\n              data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n            </div>\r\n          </div>\r\n          <br/>\r\n\r\n          <div" +
"");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-xs-12\"");

WriteLiteral(">\r\n            <!--schedule component-->\r\n<div");

WriteLiteral(" id=\"divScheduleComponent\"");

WriteLiteral(">\r\n\r\n  <link");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" href=\"content/themes/umeca/bootstrap-timepicker.css\"");

WriteLiteral(" />\r\n  <script");

WriteLiteral(" src=\"scripts/umeca/date-time/bootstrap-datepicker.min.js\"");

WriteLiteral("></script>\r\n  <script");

WriteLiteral(" src=\"scripts/umeca/date-time/moment.min.js\"");

WriteLiteral("></script>\r\n  <script");

WriteLiteral(" src=\"scripts/app/shared/dateTimePickerCursor.js\"");

WriteLiteral("></script>\r\n  <script");

WriteLiteral(" src=\"scripts/app/reviewer/scheduleCtrl.js\"");

WriteLiteral("></script>\r\n\r\n  <div");

WriteLiteral(" class=\"row element-center\"");

WriteLiteral(" ng-controller=\"scheduleController\"");

WriteLiteral(" id=\"divComponentSchedule\"");

WriteLiteral(">\r\n    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" ng-model=\"schString\"");

WriteLiteral(" name=\"sch\"");

WriteLiteral(" id=\"hdnJsonScheduleHome\"");

WriteLiteral(" ng-update-hidden >\r\n    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" ng-model=\"content\"");

WriteLiteral(" ng-init=\"content =\'\'+\'Home\';\"");

WriteLiteral(" ng-update-hidden >\r\n    <div");

WriteLiteral(" class=\"col-xs-10 col-xs-offset-1\"");

WriteLiteral(">\r\n      <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-xs-4 element-center\"");

WriteLiteral(">\r\n         D&iacute;a(s)<br/>\r\n         <input");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" value=\"\"");

WriteLiteral("\r\n         ng-model=\"s.Day\"");

WriteLiteral(" ng-init=\'s.Day= \"\"\'");

WriteLiteral(">\r\n       </div>\r\n       <div");

WriteLiteral(" class=\"col-xs-3 element-center\"");

WriteLiteral(">\r\n        Inicio<br/>\r\n        <div");

WriteLiteral(" class=\"input-group bootstrap-timepicker\"");

WriteLiteral(">\r\n          <input");

WriteLiteral(" id=\"timepickerStart{{content}}\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control umeca-time-picker tp\"");

WriteLiteral(" ng-model=\"s.Start\"");

WriteLiteral("/>\r\n          <span");

WriteLiteral(" class=\"input-group-addon\"");

WriteLiteral(">\r\n            <i");

WriteLiteral(" class=\"icon-time bigger-60\"");

WriteLiteral("></i>\r\n          </span>\r\n        </div>\r\n      </div>\r\n      <div");

WriteLiteral(" class=\"col-xs-3 element-center\"");

WriteLiteral(">\r\n        Fin <br/>\r\n        <div");

WriteLiteral(" class=\"input-group bootstrap-timepicker\"");

WriteLiteral(">\r\n          <input");

WriteLiteral(" id=\"timepickerEnd{{content}}\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control umeca-time-picker tp\"");

WriteLiteral(" ng-model=\"s.End\"");

WriteLiteral("/>\r\n          <span");

WriteLiteral(" class=\"input-group-addon\"");

WriteLiteral(">\r\n            <i");

WriteLiteral(" class=\"icon-time  bigger-40\"");

WriteLiteral("></i>\r\n          </span>\r\n        </div>\r\n      </div>\r\n      <div");

WriteLiteral(" class=\"col-xs-2\"");

WriteLiteral(">\r\n        Acciones<br/><div");

WriteLiteral(" class=\"space-5\"");

WriteLiteral("></div>\r\n        <i");

WriteLiteral(" class=\"icon-plus-sign orange\"");

WriteLiteral(" style=\"cursor:pointer;\"");

WriteLiteral(" ng-click=\"addSchedule()\"");

WriteLiteral(">  </i>\r\n      </div>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n      <div");

WriteLiteral(" class=\"hr hr-6\"");

WriteLiteral("></div>\r\n    </div>\r\n    <div");

WriteLiteral(" ng-show=\"msgError\"");

WriteLiteral(" class=\"alert-danger element-center\"");

WriteLiteral(">\r\n      {{msgError}}\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"col-xs-12\"");

WriteLiteral(" ng-show=\"listSchedule.length==0\"");

WriteLiteral(">\r\n      <br/>\r\n      <br/>\r\n      <br/>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"col-xs-9 col-xs-offset-1\"");

WriteLiteral(" ng-show =\"listSchedule.length > 0\">\r\n     <div");

WriteLiteral(" class=\"row center\"");

WriteLiteral(">\r\n       <div");

WriteLiteral(" class=\"col-xs-5\"");

WriteLiteral(">\r\n         <h5");

WriteLiteral(" class=\"smaller lighter blue\"");

WriteLiteral(">D&iacute;a</h5>\r\n         <div");

WriteLiteral(" class=\"hr hr-2\"");

WriteLiteral("></div>\r\n       </div>\r\n       <div");

WriteLiteral(" class=\"col-xs-2\"");

WriteLiteral(">\r\n         <h5");

WriteLiteral(" class=\"smaller lighter blue\"");

WriteLiteral(">Inicio</h5>\r\n         <div");

WriteLiteral(" class=\"hr hr-2\"");

WriteLiteral("></div>\r\n       </div>\r\n       <div");

WriteLiteral(" class=\"col-xs-2\"");

WriteLiteral(">\r\n         <h5");

WriteLiteral(" class=\"smaller lighter blue\"");

WriteLiteral(">Fin</h5>\r\n         <div");

WriteLiteral(" class=\"hr hr-2\"");

WriteLiteral("></div>\r\n       </div>\r\n       <div");

WriteLiteral(" class=\"col-xs-2\"");

WriteLiteral(">\r\n         <h5");

WriteLiteral(" class=\"smaller lighter blue\"");

WriteLiteral(">Acciones</h5>\r\n         <div");

WriteLiteral(" class=\"hr hr-2\"");

WriteLiteral("></div>\r\n       </div>\r\n     </div>\r\n     <div");

WriteLiteral(" class=\"row center\"");

WriteLiteral(" ng-repeat =\"sch in listSchedule\">\r\n      <div");

WriteLiteral(" class=\"col-xs-5\"");

WriteLiteral(">\r\n        {{sch.Day}}\r\n      </div>\r\n      <div");

WriteLiteral(" class=\"col-xs-2\"");

WriteLiteral(">\r\n       {{sch.Start}}\r\n     </div>\r\n     <div");

WriteLiteral(" class=\"col-xs-2\"");

WriteLiteral(">\r\n      {{sch.End}}\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"col-xs-2\"");

WriteLiteral(">\r\n      <i");

WriteLiteral(" class=\"icon-trash red\"");

WriteLiteral(" style=\"cursor:pointer;\"");

WriteLiteral(" ng-click=\"deleteSchedule($index)\"");

WriteLiteral(@"></i>
    </div>
  </div>
  <br/>
</div>
</div>
<br/>
</div>
</div>
            <!--schedule component-->
            </div>
          </div>
          <br/>
        </div>
      </div>
    </div>
  </div>
</div>
</div>

    <!--END Seccion dependiente del tipo de domicilio-->










                <div >
                    <br />
                    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"col-xs-12\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" ng-show=\"MsgError\"");

WriteLiteral(" class=\"alert alert-danger element-center\"");

WriteLiteral(">\r\n                                {{MsgError}}\r\n                            </di" +
"v>\r\n                        </div>\r\n                    </div>\r\n                " +
"</div>\r\n                <div");

WriteLiteral(" class=\"modal-footer\"");

WriteLiteral(">\r\n                    <span");

WriteLiteral(" class=\"btn btn-default btn-sm\"");

WriteLiteral(" ng-click=\"cancel()\"");

WriteLiteral(">\r\n                        Cancelar\r\n                    </span>\r\n               " +
"     <span");

WriteLiteral(" class=\"btn btn-default btn-primary btn-sm\"");

WriteLiteral(" ng-disabled=\"WaitFor==true\"");

WriteLiteral("\r\n                          ng-click=\"\"");

WriteLiteral(">\r\n                          Guardar\r\n                    </span>\r\n              " +
"  </div>\r\n\r\n\r\n\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");

}
}
}
#pragma warning restore 1591
