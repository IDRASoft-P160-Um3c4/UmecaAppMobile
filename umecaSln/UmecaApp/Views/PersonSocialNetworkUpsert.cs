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
public partial class PersonSocialNetworkUpsert : WebViewTemplate
{

#line hidden

#line 1 "PersonSocialNetworkUpsert.cshtml"
public UmecaApp.ModelContainer Model { get; set; }

#line default
#line hidden


public override void Execute()
{

#line 3 "PersonSocialNetworkUpsert.cshtml"
  
Layout = "UmecaApp.headUm";


#line default
#line hidden
WriteLiteral("\r\n<script>\r\napp.controller(\'socialNetworkController\', function($scope, $timeout) " +
"{\r\n\t$scope.m = {};\r\n\t$scope.p = {};\r\n    $scope.lstRel = [];\r\n    $scope.lstDoc " +
"= [];\r\n    $scope.lstLiv = [];\r\n    $scope.lstDep = [];\r\n    $scope.rel = {};\r\n " +
"   $scope.doc = {};\r\n    $scope.m.RelationshipId = {};\r\n    $scope.m.DocumentTyp" +
"eId = {};\r\n    $scope.liv = {};\r\n    $scope.dep = {};\r\n    $scope.MsgError = \"\";" +
"\r\n\r\n    $scope.init = function(){\r\n\t\t    \r\n\r\n\t\t\tvar relaciones = MeetingService." +
"findAllRelationship();\r\n\t\t    console.log(\"relaciones\"+relaciones);\r\n\t\t    $scop" +
"e.lstRel = JSON.parse(relaciones);\r\n\t\t    var documentos = MeetingService.findAl" +
"lDocumentType();\r\n\t\t    console.log(\"documentos\"+documentos);\r\n\t\t    $scope.lstD" +
"oc = JSON.parse(documentos);\r\n\t\t\tvar election = MeetingService.findAllElection()" +
";\r\n\t\t    console.log(\"election\"+election);\r\n\t\t    $scope.lstLiv = JSON.parse(ele" +
"ction);\r\n\t\t    $scope.lstDep = JSON.parse(election);\r\n\r\n\t\t    try{\r\n\t\t    consol" +
"e.log(\"modelContainer\"+$(\"#hdnJsonModelContainer\").val());\r\n\t\t    \tvar js = JSON" +
".parse($(\"#hdnJsonModelContainer\").val());\r\n\t\t\t    $scope.m = js;\r\n\t\t\t    } catc" +
"h(err){\r\n\t\t    \talert(\"Init erro==>\"+err.message);\r\n\t\t    }\r\n\r\n\t\t    if($scope.m" +
".block == undefined ||$scope.m.block == false ){\r\n\t\t    \t$scope.fillModel();\r\n\t\t" +
"    }\r\n\t\t    else{\r\n\t\t    \t$scope.m.block = true;\r\n\t\t    }\r\n\t\t    if($scope.m.Re" +
"lationshipId!=undefined){\r\n\t\t    \tfor(var i= 0; i < $scope.lstRel.length ; i++){" +
"\r\n\t                if($scope.lstRel[i].Id == $scope.m.RelationshipId){\r\n\t    \t\t\t" +
"\t$scope.rel = $scope.lstRel[i];\r\n\t                    break;\r\n\t                }" +
"\r\n\t            }\r\n\t\t    }\r\n\t\t    if($scope.m.DocumentTypeId!=undefined){\r\n\t\t    " +
"\tfor(var i= 0; i < $scope.lstDoc.length ; i++){\r\n\t                if($scope.lstD" +
"oc[i].Id == $scope.m.DocumentTypeId){\r\n\t    \t\t\t\t$scope.doc = $scope.lstDoc[i];\r\n" +
"\t                    break;\r\n\t                }\r\n\t            }\r\n\t\t    }\r\n\t\t    " +
"if($scope.m.LivingWithIde!=undefined){\r\n\t\t    \tfor(var i= 0; i < $scope.lstLiv.l" +
"ength ; i++){\r\n\t                if($scope.lstLiv[i].Id == $scope.m.LivingWithIde" +
"){\r\n\t    \t\t\t\t$scope.liv = $scope.lstLiv[i];\r\n\t                    break;\r\n\t     " +
"           }\r\n\t            }\r\n\t\t    }\r\n\t\t    if($scope.m.DependentId!=undefined)" +
"{\r\n\t\t    \tfor(var i= 0; i < $scope.lstDep.length ; i++){\r\n\t                if($s" +
"cope.lstDep[i].Id == $scope.m.DependentId){\r\n\t    \t\t\t\t$scope.dep = $scope.lstDep" +
"[i];\r\n\t                    break;\r\n\t                }\r\n\t            }\r\n\t\t    }\r\n" +
"    };\r\n\r\n    $timeout(function() {\r\n        $scope.init();\r\n    }, 0);\r\n\r\n    $" +
"scope.save = function(){\r\n    if($(\"#FormSocialNetworkId\").valid() == false){\r\n " +
"        return false;\r\n         }else{\r\n    \t\tconsole.log(\"save\"+$(\"#hdnMeetingC" +
"aseReference\").val());\r\n\t   \t\tvar jsonData = JSON.stringify($scope.m);\r\n\t\t\t$scop" +
"e.msgError = MeetingService.upsertPersonaRedSocial(JSON.stringify($scope.m));\r\n\t" +
"\t\tif($scope.msgError==undefined||$scope.msgError==null||$scope.msgError==\"\"){\r\n\t" +
"\t\t\t$scope.cancel($(\"#hdnMeetingCaseReference\").val());\r\n\t\t\t}\r\n\t\t}\r\n\t   };\r\n\r\n   " +
" $scope.cancel = function (idCase) {\r\n    console.log(\"cancel\"+idCase);\r\n       " +
" window.location.replace(\'hybrid:Meeting/MeetingDatosPersonales?idCase=\'+idCase)" +
";\r\n    };\r\n\r\n    $scope.fillModel = function(){\r\n        var template= \"NO TIENE" +
"\";\r\n        var template2 = \"Ninguno\";\r\n        if($scope.m.block === false){\r\n " +
"           $scope.m.Name = template;\r\n            for(var i= 0; i < $scope.lstRe" +
"l.length ; i++){\r\n                if($scope.lstRel[i].Name == template2){\r\n    \t" +
"\t\t\t$scope.rel = $scope.lstRel[i];\r\n                    $scope.m.RelationshipId =" +
" $scope.lstRel[i].Id;\r\n                    break;\r\n                }\r\n          " +
"  }\r\n            $scope.m.Phone = template;\r\n            for(var i= 0; i < $scop" +
"e.lstDoc.length ; i++){\r\n                if($scope.lstDoc[i].Name == template2){" +
"\r\n                \t$scope.doc = $scope.lstDoc[i];\r\n                    $scope.m." +
"DocumentTypeId = $scope.lstDoc[i].Id;\r\n                    break;\r\n             " +
"   }\r\n            }\r\n\r\n            $scope.m.Age = 0;\r\n            $scope.m.isAcc" +
"ompaniment = false;\r\n            for(var i= 0; i < $scope.lstDep.length ; i++){\r" +
"\n                if($scope.lstDep[i].Name == \"No\"){\r\n                    $scope." +
"dep = $scope.lstDep[i];\r\n                    $scope.m.DependentId = $scope.lstDe" +
"p[i].Id;\r\n                    break;\r\n                }\r\n            }\r\n        " +
"    for(var i= 0; i < $scope.lstLiv.length ; i++){\r\n                if($scope.ls" +
"tLiv[i].Name == \"Si\"){\r\n                    $scope.liv = $scope.lstLiv[i];\r\n    " +
"                $scope.m.LivingWithIde = $scope.lstLiv[i].Id;\r\n                 " +
"   break;\r\n                }\r\n            }\r\n        }else{\r\n            $scope." +
"m.Name = \"\";\r\n            \t\t$scope.rel = $scope.lstRel[0];\r\n                    " +
"$scope.m.RelationshipId = $scope.lstRel[0].Id;\r\n            $scope.m.Phone = tem" +
"plate;\r\n            \t\t$scope.doc = $scope.lstDoc[0];\r\n                    $scope" +
".m.DocumentTypeId = $scope.lstDoc[0].Id;\r\n            $scope.m.Age = \"\";\r\n      " +
"      $scope.m.Phone = \"\";\r\n            $scope.m.isAccompaniment = false;\r\n     " +
"               $scope.dep = $scope.lstDep[0];\r\n                    $scope.m.Depe" +
"ndentId = $scope.lstDep[0].Id;\r\n                    $scope.liv = $scope.lstLiv[0" +
"];\r\n                    $scope.m.LivingWithIde = $scope.lstLiv[0].Id;\r\n         " +
"   $scope.m.Address=\"\";\r\n        }\r\n    };\r\n});\r\n</script>\r\n<div>\r\n    <div");

WriteLiteral(" id=\"dlgUpModalId\"");

WriteLiteral(" ng-app=\"umecaMobile\"");

WriteLiteral(" ng-controller=\"socialNetworkController\"");

WriteLiteral(" >\r\n    <div>\r\n        <div");

WriteLiteral(" class=\"alert alert-info \"");

WriteLiteral(">\r\n            <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"close\"");

WriteLiteral(" data-dismiss=\"modal\"");

WriteLiteral(" aria-hidden=\"true\"");

WriteLiteral(">&times;</button>\r\n            <h4");

WriteLiteral(" class=\"element-center\"");

WriteLiteral("><i");

WriteLiteral(" class=\"icon-group \"");

WriteLiteral("></i>&nbsp;&nbsp;Persona de su red social</h4>\r\n        </div>\r\n    </div>\r\n     " +
"      \r\n<div>\r\n\t<div>\r\n\t    <input");

WriteLiteral(" id=\"hdnJsonModelContainer\"");

WriteLiteral(" type=\"hidden\"");

WriteAttribute ("value", " value=\"", "\""

#line 173 "PersonSocialNetworkUpsert.cshtml"
                     , Tuple.Create<string,object,bool> ("", Model.JsonModel

#line default
#line hidden
, false)
);
WriteLiteral(" name=\"jsonString\"");

WriteLiteral(" />\r\n\t    <input");

WriteLiteral(" id=\"hdnMeetingCaseReference\"");

WriteLiteral(" type=\"hidden\"");

WriteAttribute ("value", " value=\"", "\""

#line 174 "PersonSocialNetworkUpsert.cshtml"
                       , Tuple.Create<string,object,bool> ("", Model.Reference

#line default
#line hidden
, false)
);
WriteLiteral(" name=\"wasd\"");

WriteLiteral("/>\r\n\t</div>\r\n\t<form");

WriteLiteral(" id=\"FormSocialNetworkId\"");

WriteLiteral(" name=\"FormSocialNetworkId\"");

WriteLiteral(" >\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-xs-12 element-center\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-xs-6 element-right\"");

WriteLiteral(" >\r\n                &iquest;El imputado cuenta con personas en su red social?\r\n  " +
"          </div>\r\n            <div");

WriteLiteral(" class=\"col-xs-2\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" type=\"radio\"");

WriteLiteral(" name=\"block\"");

WriteLiteral("\r\n                       id=\"blockYes\"");

WriteLiteral(" ng-value=\"true\"");

WriteLiteral(" ng-model=\"m.block\"");

WriteLiteral(" ng-change=\"fillModel()\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" for=\"blockYes\"");

WriteLiteral(">Si</label> &nbsp;&nbsp;&nbsp;\r\n                <input");

WriteLiteral(" type=\"radio\"");

WriteLiteral("  name=\"block\"");

WriteLiteral("\r\n                       id=\"blockNo\"");

WriteLiteral(" ng-value=\"false\"");

WriteLiteral(" ng-model=\"m.block\"");

WriteLiteral(" ng-change=\"fillModel()\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" for=\"blockNo\"");

WriteLiteral(">No</label>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"hr hr-3\"");

WriteLiteral("></div>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-xs-10 col-xs-offset-1\"");

WriteLiteral(" ng-show=\"m.block\"");

WriteLiteral(">\r\n\r\n            <!--<input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" ng-update-hidden");

WriteLiteral(" name=\"isAccompaniment\"");

WriteLiteral(" ng-model=\"m.isAccompaniment\"");

WriteLiteral(">-->\r\n            <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" ng-model=\"m.isAccompaniment\"");

WriteLiteral(" id=\"isAccompaniment\"");

WriteLiteral(" ng-checked=\"m.isAccompaniment\"");

WriteLiteral("\r\n            \tng-disabled=\"!m.block\"");

WriteLiteral(">\r\n\r\n           <label");

WriteLiteral(" for=\"isAccompaniment\"");

WriteLiteral("> &iquest;Esta persona acompa&ntilde;ar&aacute; al imputado durante el proceso?</" +
"label>\r\n        </div>\r\n    </div>\r\n    <br/>\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n\r\n        <div");

WriteLiteral(" class=\"col-xs-12\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-xs-2 element-left\"");

WriteLiteral(">\r\n                Nombre:\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"col-xs-10\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" class=\"width-100\"");

WriteLiteral(" type=\"text\"");

WriteLiteral("\r\n                       data-val=\"true\"");

WriteLiteral(" data-val-length=\"Debe tener al menos 3 y m&aacute;ximo 150 caracteres\"");

WriteLiteral("\r\n                       data-val-length-max=\"150\"");

WriteLiteral(" data-val-length-min=\"3\"");

WriteLiteral("   ng-readonly=\"!m.block\"");

WriteLiteral("\r\n                       data-val-required=\"El Nombre es un campo requerido\"");

WriteLiteral("\r\n                       name=\"name\"");

WriteLiteral(" id=\"name\"");

WriteLiteral(" ng-model=\"m.Name\"");

WriteLiteral(">\r\n                <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"name\"");

WriteLiteral(" data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n        </div>\r\n    </div>\r\n    </div>\r\n    <br/>\r\n\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-xs-6\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-xs-4 element-left\"");

WriteLiteral(">\r\n                Relaci&oacute;n:\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"col-xs-8\"");

WriteLiteral(">\r\n                <select");

WriteLiteral(" class=\"form-control element-center\"");

WriteLiteral(" ng-model=\"rel\"");

WriteLiteral("\r\n                        ng-options=\"e.Name for e in lstRel\"");

WriteLiteral("\r\n                        ng-change=\"m.RelationshipId = rel[\'Id\']\"");

WriteLiteral("   ng-readonly=\"!m.block\"");

WriteLiteral("\r\n                       ></select>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"col-xs-9 col-xs-offset-3\"");

WriteLiteral(">\r\n                <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"lastName1\"");

WriteLiteral(" data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n            </div>\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"col-xs-6\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-xs-4 element-left\"");

WriteLiteral(">\r\n                Tel&eacute;fono:\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"col-xs-8\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" type=\"text\"");

WriteLiteral("  ng-readonly=\"!m.block\"");

WriteLiteral("\r\n                       data-val=\"true\"");

WriteLiteral(" data-val-length=\"Debe tener al menos 5 y m&aacute;ximo 200 caracteres\"");

WriteLiteral("\r\n                       data-val-length-max=\"200\"");

WriteLiteral(" data-val-length-min=\"5\"");

WriteLiteral("\r\n                       data-val-required=\"El tel&eacute;fono es un campo requer" +
"ido\"");

WriteLiteral("\r\n                       name=\"Phone\"");

WriteLiteral(" id=\"Phone\"");

WriteLiteral(" ng-model=\"m.Phone\"");

WriteLiteral(" ></textarea>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"col-xs-12\"");

WriteLiteral(">\r\n                <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"Phone\"");

WriteLiteral(" data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <br/>\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(" ng-show=\"m.RelationshipId[\'Specification\'] == true\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-xs-6\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-xs-4 element-left\"");

WriteLiteral(">\r\n                Especif&iacute;que<br/>relaci&oacute;n:\r\n            </div>\r\n " +
"           <div");

WriteLiteral(" class=\"col-xs-8\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" data-val=\"true\"");

WriteLiteral("\r\n                       data-val-length=\"Debe tener al menos 2 y m&aacute;ximo 2" +
"55 caracteres\"");

WriteLiteral("\r\n                       data-val-length-max=\"255\"");

WriteLiteral(" data-val-length-min=\"2\"");

WriteLiteral("\r\n                       data-val-required=\"La especificaci&oacute;n es un campo " +
"requerido\"");

WriteLiteral("\r\n                       type=\"text\"");

WriteLiteral(" ng-model=\"m.specificationRelationship\"");

WriteLiteral(" \r\n                       id=\"specificationRelationship\"");

WriteLiteral(" name=\"specificationRelationship\"");

WriteLiteral(">\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"col-xs-9 col-xs-offset-3\"");

WriteLiteral(">\r\n                <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"specificationRelationship\"");

WriteLiteral(" data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n            </div>\r\n        </div>\r\n\r\n    </div>\r\n    <br/>\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-xs-6\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-xs-4 element-left\"");

WriteLiteral(">\r\n                Identificaci&oacute;n prensentada:\r\n            </div>\r\n      " +
"      <div");

WriteLiteral(" class=\"col-xs-8\"");

WriteLiteral(">\r\n                <select");

WriteLiteral(" class=\"form-control element-center\"");

WriteLiteral(" ng-model=\"doc\"");

WriteLiteral("\r\n                        ng-options=\"e.Name for e in lstDoc\"");

WriteLiteral("\r\n                        ng-change=\"m.DocumentTypeId = doc[\'Id\']\"");

WriteLiteral("  ng-readonly=\"!m.block\"");

WriteLiteral("\r\n                        ></select>\r\n            </div>\r\n        </div>\r\n       " +
" <div");

WriteLiteral(" class=\"col-xs-6\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-xs-4 element-left\"");

WriteLiteral(">\r\n                Edad:\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"col-xs-8\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" data-val=\"true\"");

WriteLiteral("\r\n                       data-val-length=\"Debe tener al menos 1 y m&aacute;ximo 2" +
" n&uacute;mero\"");

WriteLiteral("\r\n                       data-val-regex-pattern=\"([0-9]+)\"");

WriteLiteral("    ng-readonly=\"!m.block\"");

WriteLiteral("\r\n                       data-val-regex=\"La edad s&oacute;lo puede contener n&uac" +
"ute;meros\"");

WriteLiteral("\r\n                       data-val-length-max=\"2\"");

WriteLiteral(" data-val-length-min=\"1\"");

WriteLiteral(" data-val-required=\"La edad es un campo requerido\"");

WriteLiteral("\r\n                       type=\"text\"");

WriteLiteral(" name=\"age\"");

WriteLiteral(" ng-model=\"m.Age\"");

WriteLiteral(" >\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"col-xs-12\"");

WriteLiteral(">\r\n                <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"age\"");

WriteLiteral(" data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <br/>\r\n\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(" ng-show=\"m.DocumentTypeId[\'Specification\'] == true\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-xs-6\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-xs-4 element-left\"");

WriteLiteral(">\r\n                Especif&iacute;que:\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"col-xs-8\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" data-val=\"true\"");

WriteLiteral("\r\n                       data-val-length=\"Debe tener al menos 2 y m&aacute;ximo 2" +
"55 caracteres\"");

WriteLiteral("\r\n                       data-val-length-max=\"255\"");

WriteLiteral(" data-val-length-min=\"2\"");

WriteLiteral("\r\n                       data-val-required=\"La especificaci&oacute;n es un campo " +
"requerido\"");

WriteLiteral("\r\n                       type=\"text\"");

WriteLiteral(" ng-model=\"m.SpecificationDocumentType\"");

WriteLiteral("\r\n                       id=\"specification\"");

WriteLiteral(" name=\"specification\"");

WriteLiteral(">\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"col-xs-9 col-xs-offset-3\"");

WriteLiteral(">\r\n                <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"specification\"");

WriteLiteral(" data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <br/>\r\n\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-xs-6\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-xs-4 element-left\"");

WriteLiteral(">\r\n                Vive con el:\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"col-xs-8\"");

WriteLiteral(">\r\n                <select");

WriteLiteral(" class=\"form-control element-center\"");

WriteLiteral(" ng-model=\"liv\"");

WriteLiteral("\r\n                        ng-options=\"e.Name for e in lstLiv\"");

WriteLiteral("\r\n                        ng-change=\"m.LivingWithIde = liv[\'Id\']\"");

WriteLiteral("  ng-readonly=\"!m.block\"");

WriteLiteral("\r\n                        ></select>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"col-xs-9 col-xs-offset-3\"");

WriteLiteral(">\r\n                <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"lastName1\"");

WriteLiteral(" data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n            </div>\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"col-xs-6\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-xs-4 element-left\"");

WriteLiteral(">\r\n                Dependiente econ&oacute;mico:\r\n            </div>\r\n           " +
" <div");

WriteLiteral(" class=\"col-xs-8\"");

WriteLiteral(">\r\n                <select");

WriteLiteral(" class=\"form-control element-center\"");

WriteLiteral(" ng-model=\"dep\"");

WriteLiteral("\r\n                        ng-options=\"e.Name for e in lstDep\"");

WriteLiteral("\r\n                        ng-change=\"m.DependentId = dep[\'Id\']\"");

WriteLiteral("   ng-readonly=\"!m.block\"");

WriteLiteral("\r\n                        ></select>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"col-xs-9 col-xs-offset-3\"");

WriteLiteral(">\r\n                <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"lastName1\"");

WriteLiteral(" data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <br/>\r\n\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(" ng-show=\"m.LivingWithIde == 2\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-xs-12\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-xs-2 element-left\"");

WriteLiteral(">Direcci&oacute;n:</div>\r\n            <div");

WriteLiteral(" class=\"col-xs-10\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" id=\"address\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" name=\"address\"");

WriteLiteral(" ng-model=\"m.Address\"");

WriteLiteral("\r\n                          data-val=\"true\"");

WriteLiteral(" data-val-required=\"La direcci&oacute;n es un campo requerido\"");

WriteLiteral("\r\n                          data-val-length=\"Debe tener al menos 6 y m&aacute;xim" +
"o 500 caracteres\"");

WriteLiteral("\r\n                          data-val-length-max=\"500\"");

WriteLiteral(" data-val-length-min=\"6\"");

WriteLiteral("></textarea>\r\n            </div>\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"col-xs-10 col-xs-offset-2\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" class=\"field-validation-valid\"");

WriteLiteral(" data-valmsg-for=\"address\"");

WriteLiteral(" data-valmsg-replace=\"true\"");

WriteLiteral("></span>\r\n        </div>\r\n    </div>\r\n    </form>\r\n</div>\r\n\r\n                    " +
"<br />\r\n                    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"col-xs-12\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" ng-show=\"MsgError\"");

WriteLiteral(" class=\"alert alert-danger element-center\"");

WriteLiteral(">\r\n                                {{MsgError}}\r\n                            </di" +
"v>\r\n                        </div>\r\n                    </div>\r\n                " +
"\r\n                <div");

WriteLiteral(" class=\"modal-footer\"");

WriteLiteral(">\r\n                    <span");

WriteLiteral(" class=\"btn btn-default btn-sm\"");

WriteAttribute ("ng-click", "  ng-click=\"", "\""
, Tuple.Create<string,object,bool> ("", "cancel(\'", true)

#line 386 "PersonSocialNetworkUpsert.cshtml"
                                     , Tuple.Create<string,object,bool> ("", Model.Reference

#line default
#line hidden
, false)
, Tuple.Create<string,object,bool> ("", "\')", true)
);
WriteLiteral(">\r\n                        Cancelar\r\n                    </span>\r\n               " +
"     <span");

WriteLiteral(" class=\"btn btn-default btn-primary btn-sm\"");

WriteLiteral("\r\n                           ng-click=\"save();\"");

WriteLiteral(">\r\n                          Guardar\r\n                    </span>\r\n              " +
"  </div>\r\n                </div>\r\n            </div>\r\n\r\n");

}
}
}
#pragma warning restore 1591