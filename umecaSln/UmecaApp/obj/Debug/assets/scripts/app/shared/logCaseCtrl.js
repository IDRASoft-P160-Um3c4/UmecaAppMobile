app.controller("logCaseController", function($scope, $timeout,$sce,$rootScope){

    $scope.init = function () {

    };

    $timeout(function () {
        $scope.init();
    }, 0);

    $scope.formatHtml = function(sHtml){
        return $sce.trustAsHtml(sHtml);
    };

    $rootScope.$on('RefreshListLog', function (event,list) {
        $scope.listLog=jQuery.parseJSON(list);
    });
});

app.controller("upsertLogCaseController", function($scope, $timeout,$sce,$rootScope){

    $scope.WaitFor = false;
    $scope.MsgError = "";
    $scope.Model = {};


    $scope.init = function () {

    };

    $timeout(function () {
        $scope.init();
    }, 0);

    $scope.formatHtml = function(sHtml){
        return $sce.trustAsHtml(sHtml);
    };


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
                $scope.Model.dlg.modal('hide');
                $scope.Model.def.resolve({ isCancel: false, resp: resp });
                $rootScope.$broadcast('RefreshListLog',resp.message);
                return;
            }

            $scope.MsgError = $sce.trustAsHtml(resp.message);
            $scope.$apply();

        } catch (e) {
            $scope.MsgError = $sce.trustAsHtml("Error inesperado de datos. Por favor intente más tarde.");
        }
    };

    $scope.handleError = function () {
        $scope.WaitFor = false;
        $scope.MsgError = $sce.trustAsHtml("Error de red. Por favor intente más tarde.");
        $scope.$apply();
    };



    $scope.cancel = function (isOk) {
        $scope.Model.dlg.modal('hide');

        if (isOk === true)
            $scope.Model.def.resolve({ isCancel: false });
        else
            $scope.Model.def.reject({ isCancel: true });
    };

    $scope.setDlg = function (dlg, urlToSubmit) {
        $scope.Model.dlg = dlg;
        $scope.Model.url = urlToSubmit;

        dlg.on('hidden.bs.modal', function () {
            dlg.data('modal', null);
            dlg.replaceWith("");
        });
    };


});