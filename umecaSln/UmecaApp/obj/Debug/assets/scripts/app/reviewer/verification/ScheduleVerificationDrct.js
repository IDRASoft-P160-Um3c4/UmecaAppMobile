app.directive('verifSchedule', function ($http, $rootScope) {
    return function (scope, elem, attr) {

        var currentTimeout = null;

        elem.on('click', function () {
            var idList = scope.$eval(attr.idElement);
            $rootScope.$broadcast('SetIdListSchedule',idList);
            var code = attr.idCode;
            $rootScope.$broadcast('SetCodeSchedule',code);
            var divModValid ="#dlgUpModalIdSchedule";
            var scopeNew = angular.element(divModValid).scope();
            scopeNew.Model.def = scope.def;
            scopeNew.Model.dlg=$(divModValid);
            scopeNew.Model.dlg.modal('show');
            scopeNew.verification = false;
            scopeNew.listSchedule=[];
            scopeNew.enableProperties();
            $.validator.unobtrusive.parse("#FormCatIdSchedule");
           $(divModValid).injector().invoke(function ($compile, $rootScope) {
                $compile($("#divElementVerifSchedule"))($rootScope);
                $rootScope.$apply();
            });
            scopeNew.setDlg(scopeNew.Model.dlg);
            ////eliminar valores y eliminar readonly

        });
    };
});