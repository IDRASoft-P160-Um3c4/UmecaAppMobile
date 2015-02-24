app.directive('verifTrue', function ($http, $timeout) {
    return function (scope, elem, attr) {
        var currentTimeout = null;
        elem.on('click', function () {
            var codeVerification = attr.codeVerif;
            var divModValid ="#dlgUpModalId";
            var scopeNew = angular.element(divModValid).scope();
            scopeNew.doConfirmVerifTrue(codeVerification);

        });
    };
});