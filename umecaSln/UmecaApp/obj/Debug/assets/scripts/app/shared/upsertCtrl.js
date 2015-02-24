app.controller('upsertController', function ($scope, $rootScope, $sce) {
    $scope.WaitFor = false;
    $scope.MsgError = "";
    $scope.Model = {};


    $scope.submit = function (formId, urlToPost, hasReturnId) {

        if ($(formId).valid() == false) {
            $scope.MsgError = $sce.trustAsHtml("Debe proporcionar toda la informaci&oacute;n para guardar");
            $scope.Invalid = true;
            return false;
        }

        $scope.WaitFor = true;

        if (hasReturnId === true) {
            $.post(urlToPost, $(formId).serialize())
                .success($scope.handleSuccessWithId)
                .error($scope.handleError);
        }
        else {
            $.post(urlToPost, $(formId).serialize())
                .success($scope.handleSuccess)
                .error($scope.handleError);
        }
        return true;
    };


    $scope.submitRedirect = function (formId, urlToPost, hasReturnId, validate) {

        var stVal = true;

        if (validate != undefined)
            stVal = validate();

        if ($(formId).valid() == false || stVal == false) {
            $scope.Invalid = true;
            return false;
        }

        $scope.WaitFor = true;

        if (hasReturnId === true) {
            $.post(urlToPost, $(formId).serialize())
                .success($scope.handleSuccessWithId)
                .error($scope.handleError);
        }
        else {
            $.post(urlToPost, $(formId).serialize())
                .success($scope.handleSuccessRedirect)
                .error($scope.handleError);
        }
        return true;
    };

    $scope.returnUrl = function (urlToGo) {
        window.goToUrlMvcUrl(urlToGo, "");
    };


    $scope.handleSuccessRedirect = function (resp) {
        $scope.WaitFor = false;

        try {
            if (resp.hasError === undefined) {
                resp = resp.responseMessage;
            }
            if (resp.hasError === false) {
                window.goToUrlMvcUrl(resp.urlToGo, "");
                $scope.WaitFor = false;
                $scope.$apply();
                return;
            }

            $scope.MsgError = $sce.trustAsHtml(resp.message);

            $scope.$apply();

        } catch (e) {
            $scope.MsgError = $sce.trustAsHtml("Error inesperado de datos. Por favor intente más tarde.");
        }
    };

    $scope.handleSuccessWithId = function (resp) {
        $scope.WaitFor = false;

        try {
            if (resp.hasError === undefined) {
                resp = resp.responseMessage;
            }
            if (resp.hasError === false) {
                $rootScope.$broadcast("onLastId", resp.Id);
                $scope.Model.dlg.modal('hide');
                $scope.Model.def.resolve({ isCancel: false });
                return;
            }

            $scope.MsgError = $sce.trustAsHtml(resp.message);
            $scope.$apply();

        } catch (e) {
            $scope.MsgError = $sce.trustAsHtml("Error inesperado de datos. Por favor intente más tarde.");
        }
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

    $scope.setReason = function (opc) {
        if (opc.isFinal && $scope.Model.reason != "") {
            $scope.Model.reason = opc.reason;
        }
    }


    $scope.formatHtml = function (sHtml) {
        return $sce.trustAsHtml(sHtml);
    };

});
