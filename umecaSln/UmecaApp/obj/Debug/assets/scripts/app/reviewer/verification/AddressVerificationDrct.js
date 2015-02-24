app.directive('verifAddress', function ($http, $rootScope) {
    return function (scope, elem, attr) {

        var currentTimeout = null;

        elem.on('click', function () {
            var idList = scope.$eval(attr.idElement);
            $rootScope.$broadcast('SetIdListAddress',idList);
            var addressId =scope.$eval(attr.addressId);
            $rootScope.$broadcast('SetIdAddressVerif',addressId);
            var code = attr.idCode;
            $rootScope.$broadcast('SetCodeAddress',code);
            var divModValid ="#dlgUpModalIdAddress";
            var scopeNew = angular.element(divModValid).scope();
            scopeNew.Model.def = scope.def;
            scopeNew.Model.dlg=$(divModValid);
            scopeNew.Model.dlg.modal('show');
            scopeNew.verification = false;
            scopeNew.enableProperties();
            $.validator.unobtrusive.parse("#FormCatIdAddress");
            $(divModValid).injector().invoke(function ($compile, $rootScope) {
                $compile($("#divElementVerifAddress"))($rootScope); $rootScope.$apply();
            });
            scopeNew.setDlg(scopeNew.Model.dlg);
            ////eliminar valores y eliminar readonly

        });
    };
});