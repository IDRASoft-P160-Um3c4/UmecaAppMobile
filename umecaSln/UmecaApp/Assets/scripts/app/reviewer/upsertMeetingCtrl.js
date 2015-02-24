app.controller('upsertMeetingController', function ($scope, $timeout, $sce) {
    $scope.verification=false;
    $scope.submit = function (formId, urlToPost) {

        if ($(formId).valid() == false) {
            $scope.Invalid = true;
            return false;
        }
        $scope.WaitFor = true;
        $.post(urlToPost, $(formId).serialize())
            .success($scope.handleSuccess)
            .error($scope.handleError);
        return true;

    };

    $scope.handleSuccess = function (resp) {
        $scope.WaitFor = false;

        try {
            if (resp.hasError === undefined) {
                resp = resp.responseMessage;
            }
            if (resp.hasError === false) {
                $scope.msgExito = $sce.trustAsHtml(resp.message);
                $scope.$apply();
                return;
            }
            $scope.msgError =$sce.trustAsHtml( resp.message);
            $scope.$apply();

        } catch (e) {
            $scope.msgError = $sce.trustAsHtml("Error inesperado de datos. Por favor intente m&aacute;s tarde.");
        }
    };

    $scope.handleError = function () {
        $scope.WaitFor = false;
        $scope.MsgError = $sce.trustAsHtml("Error de red. Por favor intente m&aacute;s tarde.");
        $scope.$apply();
    };

});