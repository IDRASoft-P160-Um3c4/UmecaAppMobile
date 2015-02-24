app.controller('referencesController', function ($scope, $timeout, $rootScope) {


    $scope.refe = {};

    $scope.init = function () {
        $scope.fillSelRelationship();
        $scope.disableFields($scope.refe.hasVictimWitnessInfo);
    };

    $timeout(function () {
        $scope.init();
    }, 0);

    $scope.WaitFor = false;
    $scope.MsgError = "";
    $scope.Model = {};

    $scope.fillSelRelationship = function () {

        if ($scope.lstRelationship === undefined || $scope.lstRelationship.length <= 0)
            return;

        if ($scope.refe.relationshipId === undefined) {
            $scope.refe.relationship = $scope.lstRelationship[0];
            $scope.refe.relationshipId = $scope.refe.relationship.id;
        }
        else {
            for (var i = 0; i < $scope.lstRelationship.length; i++) {
                var rel = $scope.lstRelationship[i];

                if (rel.id === $scope.refe.relationshipId) {
                    $scope.refe.relationship = rel;
                    break;
                }
            }
        }

    };

    $scope.existHousemate = function (val) {

        if (val == false) {
            $scope.refe.name = "NO APLICA";
            $scope.refe.phone = "NO APLICA";
            $scope.refe.address = "NO APLICA";
            $scope.refe.timeAgo = "NO APLICA";
        } else {
            $scope.refe.name = "";
            $scope.refe.phone = "";
            $scope.refe.address = "";
            $scope.refe.timeAgo = "";
        }
        $scope.disableFields(val);
    };

    $scope.disableFields = function (val) {
        if (val == false) {
            $("#divRefe :input").attr("disabled", true);
            $("#divHiddenRefe :input").attr("disabled", false);
            $scope.selRelationshipNone();
        } else {
            $("#divRefe :input").attr("disabled", false);
            $("#divHiddenRefe :input").attr("disabled", true);
        }
    };

    $scope.selRelationshipNone = function () {
        for (var i = 0; i < $scope.lstRelationship.length; i++) {
            var rel = $scope.lstRelationship[i];
            if (rel.name.toLowerCase() == "ninguno") {
                $scope.refe.relationship = rel;
                break;
            }
        }
    };

    $scope.submitIdCaseParam = function (formId, urlToPost, id) {

        $(formId).validate();

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

            $scope.MsgError = resp.message;
            $scope.$apply();

        } catch (e) {
            $scope.MsgError = "Error inesperado de datos. Por favor intente más tarde.";
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
                $scope.Model.def.resolve({ isCancel: false });
                $rootScope.$broadcast("reloadEnvironment");
                return;
            }

            $scope.MsgError = resp.message;


            $scope.$apply();

        } catch (e) {
            $scope.MsgError = "Error inesperado de datos. Por favor intente más tarde.";
        }
    };

    $scope.handleError = function () {
        $scope.WaitFor = false;
        $scope.MsgError = "Error de red. Por favor intente más tarde.";
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