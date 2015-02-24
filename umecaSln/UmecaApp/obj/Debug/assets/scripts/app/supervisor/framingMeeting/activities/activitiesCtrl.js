app.controller('activitiesController', function ($scope, $timeout, $rootScope, $sce) {

        $scope.activity = {};
        $scope.activity.day = "";
        $scope.activity.start = "";
        $scope.activity.end = "";
        $scope.MsgErrorSchedule = "";
        $scope.MsgErrorActivity = "";

        $scope.init = function () {
            $scope.fillSelActivity();
        };

        $timeout(function () {
            $scope.init();
            if ($scope.activity.lstSchedule == undefined)
                $scope.activity.lstSchedule = [];
            else
                $scope.activity.lstSchedule = JSON.parse($scope.activity.lstSchedule);
        }, 0);

        $scope.WaitFor = false;
        $scope.Model = {};

        $scope.submitActivity = function (formId, urlToPost, id) {

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
                .success($scope.handleSuccessActivity)
                .error($scope.handleErrorActivity);

            return true;
        };

        $scope.handleSuccessActivity = function (resp) {
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

                $scope.MsgErrorActivity = $sce.trustAsHtml(resp.message);
                $scope.$apply();

            } catch (e) {
                $scope.MsgErrorActivity = $sce.trustAsHtml("Error inesperado de datos. Por favor intente más tarde.");
            }
        };

        $scope.handleErrorActivity = function () {
            $scope.WaitFor = false;
            $scope.MsgErrorActivity = $sce.trustAsHtml("Error de red. Por favor intente más tarde.");
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
            if ($scope.activity.day == "" || $scope.activity.start == "" || $scope.activity.end == "") {
                $scope.MsgErrorSchedule = "Debe proporcionar todos los campos para poder agregar una disponibilidad.";
                return;
            }
            $scope.MsgErrorSchedule = "";

            var newObj = {"day": $scope.activity.day, "start": $scope.activity.start, "end": $scope.activity.end}
            $scope.activity.lstSchedule.push(newObj);

            $scope.activity.day = "";
            $scope.activity.start = "";
            $scope.activity.end = "";

        };

        $scope.removeSchedule = function (idx) {
            $scope.activity.lstSchedule.splice(idx, 1);
        };

        $scope.validateSchedule = function () {
            if ($scope.activity.lstSchedule == null || $scope.activity.lstSchedule == undefined || !$scope.activity.lstSchedule.length > 0) {
                $scope.MsgErrorSchedule = "Debe agregar al menos una disponibilidad para la actividad.";
                $scope.hasError = true;
                return false;
            }
            $scope.MsgErrorSchedule = "";
            return true;
        };

        $scope.fillSelActivity = function () {

            if ($scope.lstActivities === undefined || $scope.lstActivities.length <= 0)
                return;

            if ($scope.activity.idActivity === undefined) {
                $scope.activitySel = $scope.lstActivities[0];
                $scope.activity.idActivity = $scope.activitySel.id;
            }
            else {
                for (var i = 0; i < $scope.lstActivities.length; i++) {
                    var act = $scope.lstActivities[i];

                    if (act.id === $scope.activity.idActivity) {
                        $scope.activitySel = act;
                        break;
                    }
                }
            }
        };
    }
)
;