app.controller('addressFMController', function ($scope, $timeout, $http, $rootScope) {

    $scope.fa = {};
    $scope.a = {};
    $scope.MsgErrorSchedule = "";
    $scope.day = "";
    $scope.start = "";
    $scope.end = "";


    $scope.loadAddress = function () {

        var currentTimeout = null;
        var url = $('#urlGridAddress').attr("value");
        var idCase = $('#hidIdCaseAQ').attr("value");
        var ajaxConf;

        ajaxConf = {
            method: "POST",
            params: {idCase: idCase}
        };

        ajaxConf.url = url;

        if (currentTimeout) {
            $timeout.cancel(currentTimeout);
        }

        currentTimeout = $timeout(function () {
            $http(ajaxConf)
                .success(function (data) {
                    $scope.lstRelativeAbroad = data;
                });
        }, 200);
    };

    $scope.init = function () {
        $scope.fillRegisterType();
        $scope.fillHomeType();

        if ($scope.fa.schedule == undefined || $scope.fa.schedule == null)
            $scope.fa.schedule = [];
        else
            $scope.fa.schedule = JSON.parse($scope.fa.schedule);

        $scope.clrFields();
    };

    $scope.fillRegisterType = function () {
        if ($scope.lstRegisterType === undefined || $scope.lstRegisterType.length <= 0)
            return;

        if ($scope.fa.registerTypeId === undefined) {
            $scope.fa.registerType = $scope.lstRegisterType[0];
            $scope.fa.registerTypeId = $scope.fa.registerType.id;
        }
        else {
            for (var i = 0; i < $scope.lstRegisterType.length; i++) {
                var type = $scope.lstRegisterType[i];

                if (type.id === $scope.fa.registerTypeId) {
                    $scope.fa.registerType = type;
                    break;
                }
            }
        }
    };

    $scope.fillHomeType = function () {
        if ($scope.lstHomeType === undefined || $scope.lstHomeType.length <= 0)
            return;

        if ($scope.fa.homeTypeId === undefined) {
            $scope.fa.homeType = $scope.lstHomeType[0];
            $scope.fa.homeTypeId = $scope.fa.homeType.id;
        }
        else {
            for (var i = 0; i < $scope.lstHomeType.length; i++) {
                var bel = $scope.lstHomeType[i];

                if (bel.id === $scope.fa.homeTypeId) {
                    $scope.fa.homeType = bel;
                    break;
                }
            }
        }
    };

    $scope.addSchedule = function () {
        if ($scope.day == "" || $scope.start == "" || $scope.end == "") {
            $scope.MsgErrorSchedule = "Debe proporcionar todos los campos para poder agregar una disponibilidad.";
            return;
        }
        $scope.MsgErrorSchedule = "";

        var newObj = {"day": $scope.day, "start": $scope.start, "end": $scope.end}
        $scope.fa.schedule.push(newObj);

        $scope.day = "";
        $scope.start = "";
        $scope.end = "";

    };

    $scope.removeSchedule = function (idx) {
        $scope.activity.lstSchedule.splice(idx, 1);
    };

    $scope.validateSchedule = function () {
        if ($scope.fa.schedule == null || $scope.fa.schedule == undefined || !$scope.fa.schedule.length > 0) {
            $scope.MsgErrorSchedule = "Debe agregar al menos una disponibilidad para el domicilio.";
            $scope.hasError = true;
            return false;
        }
        $scope.MsgErrorSchedule = "";
        return true;
    };

    $scope.clrFields = function () {
        if ($scope.fa.registerType.id == 3) {
            $scope.fa.timeAgo = "";
            $scope.fa.addressRef = "";
            $scope.fa.reasonAnother = "";
            $scope.fa.schedule = [];
        } else {
            $scope.fa.timeLive = "";
            $scope.fa.reasonChange = "";
        }
    };


    $timeout(function () {
        $scope.init();
    }, 0);

    $scope.WaitFor = false;
    $scope.Model = {};

    $scope.submitIdCaseParam = function (formId, urlToPost, id) {

        $scope.Invalid = false;

        if ($(formId).valid() == false) {
            $scope.Invalid = true;
        }

        if ($scope.validateSchedule() == false) {
            $scope.Invalid = true;
        }

        if ($scope.Invalid == true)
            return false;

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
                $scope.Model.dlg.modal('hide');
                $scope.Model.def.resolve({ isCancel: false, resp: resp });
                return;
            }

            $scope.aqErrorMsg = resp.message;
            $scope.$apply();

        } catch (e) {
            $scope.aqErrorMsg = "Error inesperado de datos. Por favor intente más tarde.";
        }
    };

    $scope.handleError = function () {
        $scope.WaitFor = false;
        $scope.aqErrorMsg = "Error de red. Por favor intente más tarde.";
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