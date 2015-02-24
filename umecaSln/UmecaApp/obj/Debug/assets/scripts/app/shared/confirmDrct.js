app.directive('ngConfirmAction',function() {
    return function($scope, $element, $attr) {
        var clickAction = $attr.confirmedClickAction
        $element.bind('click',function(){
            var scopeModal = angular.element($("#ConfirmationDlgId")).scope();
            var msg = $attr.confirmMessage || "¿Está seguro que desea continuar?";
            var type = $attr.confirmType || "danger";
            var title = $attr.confirmTitle || "Confirmar acci&oacute;n";
            scopeModal.setParametersModal(msg,type,title);
            scopeModal.$apply();
            $("#ConfirmationDlgId").modal('show');
            scopeModal.yes = function() {
                $scope.$eval(clickAction);
                $("#ConfirmationDlgId").modal('hide');
            };

            scopeModal.no = function() {
                $("#ConfirmationDlgId").modal('hide');
            };
        })
    };
})