app.controller('framingJobController', function ($scope, $timeout, $rootScope, $sce) {

        $scope.job = {};
        $scope.day = "";
        $scope.timeStart = "";
        $scope.timeEnd = "";
        $scope.MsgErrorJob = "";
        $scope.MsgErrorSchedule = "";

        $scope.init = function () {
            $scope.fillSelRegisterType();
        };

        $timeout(function () {
            $scope.init();
            if ($scope.job.schedule == undefined)
                $scope.job.schedule = [];
            else
                $scope.job.schedule = JSON.parse($scope.job.schedule);

            $scope.hasActualJob = $scope.job.block;
            $scope.initDisable($scope.hasActualJob);
        }, 0);

        $scope.WaitFor = false;
        $scope.Model = {};

        $scope.submitJob = function (formId, urlToPost, id) {

            $(formId).validate();

            //$scope.Invalid = false;

            if ($(formId).valid() == false) {
                $scope.Invalid = true;
            }

            if ($scope.registerType.id != 3 && $scope.hasActualJob == true && $scope.validateSchedule() == false) {
                $scope.Invalid = true;
            }

            if ($scope.Invalid == true)
                return false;

            $scope.WaitFor = true;

            var url = urlToPost + id;

            $.post(url, $(formId).serialize())
                .success($scope.handleSuccessJob)
                .error($scope.handleErrorJob);

            return true;
        };

        $scope.handleSuccessJob = function (resp) {
            $scope.WaitFor = false;

            try {
                if (resp.hasError === undefined) {
                    resp = resp.responseMessage;
                }
                if (resp.hasError === false) {
                    $scope.Model.dlg.modal('hide');
                    $scope.Model.def.resolve({ isCancel: false });
                    return;
                }

                $scope.MsgErrorJob = $sce.trustAsHtml(resp.message);
                $scope.$apply();

            } catch (e) {
                $scope.MsgErrorJob = $sce.trustAsHtml("Error inesperado de datos. Por favor intente más tarde.");
            }
        };

        $scope.handleErrorJob = function () {
            $scope.WaitFor = false;
            $scope.MsgErrorJob = $sce.trustAsHtml("Error de red. Por favor intente más tarde.");
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

        $scope.addSchedule = function () {
            if ($scope.day && $scope.day != "" && $scope.timeStart && $scope.timeStart != "" && $scope.timeEnd && $scope.timeEnd != "") {
                $scope.MsgErrorSchedule = "";

                var newObj = {"day": $scope.day, "start": $scope.timeStart, "end": $scope.timeEnd}
                $scope.job.schedule.push(newObj);

                $scope.day = "";
                $scope.timeStart = "";
                $scope.timeEnd = "";
            } else {
                $scope.MsgErrorSchedule = "Debe proporcionar todos los campos para poder agregar una disponibilidad.";
            }
        };

        $scope.removeSchedule = function (idx) {
            $scope.job.schedule.splice(idx, 1);
        };

        $scope.validateSchedule = function () {
            if ($scope.job.schedule == null || $scope.job.schedule == undefined || !$scope.job.schedule.length > 0) {
                $scope.MsgErrorSchedule = "Debe agregar al menos una disponibilidad para el trabajo.";
                $scope.hasError = true;
                return false;
            }
            $scope.MsgErrorSchedule = "";
            return true;
        };

        $scope.fillSelRegisterType = function () {
            if ($scope.lstRegisterType === undefined || $scope.lstRegisterType.length <= 0)
                return;

            if ($scope.job.registerTypeId === undefined) {
                $scope.registerType = $scope.lstRegisterType[0];
                $scope.job.registerTypeId = $scope.registerType.id;
            }
            else {
                for (var i = 0; i < $scope.lstRegisterType.length; i++) {
                    var act = $scope.lstRegisterType[i];
                    if (act.id === $scope.job.registerTypeId) {
                        $scope.registerType = act;
                        break;
                    }
                }
            }
        };

        $scope.initDisable = function (value) {
            if (value == false) {
                $("#divJob :input").attr("disabled", true);
                $("#divSpecs :input").attr("disabled", true);
                $("#divHiddenJob :input").attr("disabled", false);
            } else {
                $("#divJob :input").attr("disabled", false);
                $("#divSpecs :input").attr("disabled", false);
                $("#divHiddenJob :input").attr("disabled", true);
            }

        };

        $scope.disableView = function (value, init) {

            if (value == false) {
                $scope.job.company = "NO TRABAJA";
                $scope.job.nameHead = "NO TRABAJA";
                $scope.job.phone = "NO TRABAJA";
                $scope.job.address = "NO TRABAJA";
                $scope.job.post = "NO TRABAJA";

                $("#divJob :input").attr("disabled", true);
                $("#divSpecs :input").attr("disabled", true);
                $("#divHiddenJob :input").attr("disabled", false);
            } else {

                $scope.job.company = "";
                $scope.job.nameHead = "";
                $scope.job.phone = "";
                $scope.job.address = "";
                $scope.job.post = "";

                $("#divJob :input").attr("disabled", false);
                $("#divSpecs :input").attr("disabled", false);
                $("#divHiddenJob :input").attr("disabled", true);
            }

        };

    }
)
;