app.directive('verifComp', function ($http, $timeout,$rootScope) {
    return function (scope, elem, attr) {

        elem.on('click', function () {
            var idList = scope.$eval(attr.idElement);
            var code = attr.code;
            $rootScope.$broadcast('SetIdList',idList);
            $rootScope.$broadcast('SetCodeVerif',code);
            var levelChild = scope.$eval(attr.levelChild);
            var finalElem=elem;
            for(var i=0; i<levelChild; i++){
              finalElem = finalElem.parent();
            }
            var codeVerif = finalElem.html();
            var divModValid ="#dlgUpModalId";
            var scopeNew = angular.element(divModValid).scope();
            scopeNew.Model.def = scope.def;
            $("#divElementVerif").empty().append(codeVerif);
            scopeNew.Model.dlg=$(divModValid);
            scopeNew.Model.dlg.modal('show');
            scopeNew.enableProperties();
            $.validator.unobtrusive.parse("#divElementVerif");
            $(divModValid).injector().invoke(function ($compile, $rootScope) {
                $compile($("#divContentVerifId"))($rootScope);
                $rootScope.$apply();
            });

            scopeNew.setDlg(scopeNew.Model.dlg);
            ////eliminar valores y eliminar readonly

        });
    };
});
