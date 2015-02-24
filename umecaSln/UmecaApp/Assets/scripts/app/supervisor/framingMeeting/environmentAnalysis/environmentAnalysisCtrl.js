app.controller('environmentAnalysisController', function ($scope, $timeout, $http, $rootScope, $sce) {

    $scope.envir = {};

    $scope.errorSources = "";
    $scope.errorRisks = "";
    $scope.errorThreats = "";

    $scope.lstSources = {};

    $scope.lstSelectedSources = [];

    $scope.lstDependentSources = [];

    $scope.lstRisk = {};

    $scope.lstSelectedRisk = [];

    $scope.lstThreat = {};

    $scope.lstSelectedThreat = [];

    $scope.errorMsgEnv = "";

    $scope.successMsgEnv = "";

    $scope.selectSource = function (id) {

        var idx = $scope.findSource(id);

        $scope.errorSources = "";

        if (idx != null) {
            $scope.lstSelectedSources.splice(idx, 1);
        } else {
            $scope.lstSelectedSources.push(id);
        }

    };

    $scope.selectRisk = function (id) {

        var idx = $scope.findRisk(id);

        if (idx != null) {
            $scope.lstSelectedRisk.splice(idx, 1);
        } else {
            $scope.lstSelectedRisk.push(id);
        }

    };

    $scope.selectThreat = function (id) {

        var idx = $scope.findThreat(id);

        if (idx != null) {
            $scope.lstSelectedThreat.splice(idx, 1);
        } else {
            $scope.lstSelectedThreat.push(id);
        }

    };

    $scope.findSource = function (id) {
        for (var i = 0; i < $scope.lstSelectedSources.length; i++) {
            if ($scope.lstSelectedSources[i] == id)
                return i;
        }
        return null;
    }

    $scope.findDependentSource = function (id) {
        for (var i = 0; i < $scope.lstDependentSources.length; i++) {
            if ($scope.lstDependentSources[i] == id)
                return true;
        }
        return false;
    }

    $scope.findRisk = function (id) {
        for (var i = 0; i < $scope.lstSelectedRisk.length; i++) {
            if ($scope.lstSelectedRisk[i] == id)
                return i;
        }
        return null;
    }

    $scope.findThreat = function (id) {
        for (var i = 0; i < $scope.lstSelectedThreat.length; i++) {
            if ($scope.lstSelectedThreat[i] == id)
                return i;
        }
        return null;
    }

    $rootScope.$on('reloadEnvironment', function () {
        $scope.loadEnvironmentAnalysis();
    });

    $scope.fillEnvironmentAnalysis = function (data) {

        $scope.lstSources = $.parseJSON(data.lstSources);
        $scope.lstRisk = $.parseJSON(data.lstRisk);
        $scope.lstThreat = $.parseJSON(data.lstThreat);

        $scope.lstSelectedSources = $.parseJSON(data.lstSelectedSources);
        $scope.lstDependentSources = $.parseJSON(data.lstDependentSources);
        $scope.lstSelectedArrangement = $.parseJSON(data.lstSelectedArrangement);
        $scope.lstSelectedRisk = $.parseJSON(data.lstSelectedRisk);
        $scope.lstSelectedThreat = $.parseJSON(data.lstSelectedThreat);
        $scope.environmentComments = data.environmentComments;
    };

    $scope.loadEnvironmentAnalysis = function () {
        var currentTimeout = null;
        var urlType = $('#loadEnvironmentAnalysis').attr("value");

        var ajaxConf;

        var idCase = $('#envIdCase').attr("value");
        ajaxConf = {
            method: "POST",
            params: {idCase: idCase},
            dataType: "json",
            contentType: "application/json"
        };

        ajaxConf.url = urlType;

        if (currentTimeout) {
            $timeout.cancel(currentTimeout);
        }

        currentTimeout = $timeout(function () {
            $http(ajaxConf)
                .success(function (data) {
                    $scope.fillEnvironmentAnalysis(data);

                });
        }, 200);
    };

    $scope.init = function () {
        $scope.loadEnvironmentAnalysis();
    };

    $timeout(function () {
        $scope.init();
    }, 0);

    $scope.WaitFor = false;
    $scope.Model = {};


    $scope.validateSel = function () {

        $scope.errorThreats = "";
        $scope.errorSources = "";
        $scope.errorRisks = "";
        $scope.errorComments = "";

//        if ($scope.lstSelectedSources == undefined || $scope.lstSelectedSources.length <= 0)
//            $scope.errorSources = "Debe seleccionar al menos un vinculo";

        if ($scope.lstSelectedRisk == undefined || $scope.lstSelectedRisk.length <= 0)
            $scope.errorRisks = "Debe seleccionar al menos un riesgo";

        if ($scope.lstSelectedThreat == undefined || $scope.lstSelectedThreat.length <= 0)
            $scope.errorThreats = "Debe seleccionar al menos una amenaza";

        if ($scope.environmentComments == undefined || $scope.environmentComments == "")
            $scope.errorComments = "Observaciones es un campo requerido";

        if ($scope.errorSources != "" || $scope.errorRisks != "" || $scope.errorThreats != "" || $scope.errorComments != "")
            return false;

        return true;

    }

    $scope.submitIdCaseParam = function (formId, urlToPost, id) {

        $(formId).validate();

        if ($scope.validateSel() == false) {
            $scope.Invalid = true;
            return false;
        }

        if ($(formId).valid() == false) {
            $scope.Invalid = true;
            return false;
        }
        $scope.WaitFor = true;

        var url = urlToPost + id;

        $.post(url, $(formId).serialize())
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
                $scope.successMsgEnv = $sce.trustAsHtml(resp.message);
                $scope.$apply();
                return;
            }

            $scope.errorMsgEnv = $sce.trustAsHtml(resp.message);
            $scope.$apply();

        } catch (e) {
            $scope.errorMsgEnv = $sce.trustAsHtml("Error inesperado de datos. Por favor intente más tarde.");
        }
    };

    $scope.handleError = function () {
        $scope.WaitFor = false;
        $scope.errorMsg = $sce.trustAsHtml("Error de red. Por favor intente más tarde.");
        $scope.$apply();
    };

    $scope.cancel = function () {
        $scope.Model.dlg.modal('hide');
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