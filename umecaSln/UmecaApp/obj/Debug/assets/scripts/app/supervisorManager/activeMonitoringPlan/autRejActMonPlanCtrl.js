app.controller("authRejActMonPlanController", function ($scope, $sce, $timeout, $anchorScroll, $location) {
    $scope.m = {};
    $scope.m.lstAutRejActMon = [];
    $scope.detail = '-';
    $scope.dtNew = {};
    $scope.dtOld = {};
    $scope.lstActivities = [];


    $scope.changeAll = function () {
        if ($scope.m.authAll != undefined) {
            for (var i = 0; i < $scope.m.lstAutRejActMon.length; i++) {
                if ($scope.activityShow($scope.m.lstAutRejActMon[i].id)) {
                    $scope.m.lstAutRejActMon[i].value = $scope.m.authAll;
                }
            }
        }
        $scope.onChangeRejAuth();
    }

    $scope.activityShow = function (idActivity) {
        for (var i = 0; i < $scope.lstActivities.length; i++) {
            if ($scope.lstActivities[i].activityMonId == idActivity && $scope.lstActivities[i].visible)
                return true;
        }
        return false;
    }

    $scope.filterActivities = function () {
        for (var i = 0; i < $scope.lstActivities.length; i++) {
            if ($scope.filterSelected != "ALL") {
                if ($scope.lstActivities[i].status == $scope.filterSelected) {
                    $scope.lstActivities[i].visible = true;
                } else {
                    $scope.lstActivities[i].visible = false;
                }
            } else {
                $scope.lstActivities[i].visible = true;
            }
        }
    }

    $scope.getColor = function (status) {
        return window.colorActMonPlan(status, undefined, undefined, 1);
    };

    $scope.createLabel = function (status) {
        return 'label ' + window.colorActMonPlan(status);
    };

    $scope.init = function (lstActivities) {
        $scope.filterSelected = "ALL";
        for (var i = 0, tot = lstActivities.length; i < tot; i++) {
            var act = lstActivities[i];
            $scope.m.lstAutRejActMon.push({id: act.activityMonId, value: '0'});
            act.visible = true;
        }
        $scope.onChangeRejAuth();
    };

    $scope.showActivity = function (actMonPlanId, iAct) {
        var settings = {
            dataType: "json",
            contentType: "application/json",
            type: "POST",
            url: $scope.urlToGo,
            data: actMonPlanId.toString(),
            success: $scope.onSuccess,
            error: $scope.onError
        };
        $scope.iAct = iAct;
        $.ajax(settings);
    };

    $scope.onSuccess = function (response) {
        if (response.hasError) {
            $scope.MsgError = $sce.trustAsHtml(response.message);
            $scope.$apply();
            return;
        }

        var retDt = response.returnData;
        $scope.dtNew = retDt.newActMonPlanInfo;
        $scope.dtOld = retDt.oldActMonPlanInfo;

        if ($scope.dtNew === undefined)
            $scope.dtNew = {};
        if ($scope.dtOld === undefined)
            $scope.dtOld = {};

        $scope.switchWindow();
        $scope.$apply();
    };

    $scope.onError = function () {
        $scope.MsgError = $sce.trustAsHtml("No fue posible conectarse al servidor. Por favor intente m&aacute;s tarde.");
        $scope.$apply();
    };

    $scope.switchWindow = function () {
        if ($scope.detail === '-')
            $scope.detail = false;
        $scope.detail = !$scope.detail;
    };

    $scope.onChangeRejAuth = function () {

        $scope.countRej = 0;
        $scope.countAuth = 0;

        for (var i = 0, tot = $scope.m.lstAutRejActMon.length; i < tot; i++) {
            var act = $scope.m.lstAutRejActMon[i];
            if (act.value === '1')
                $scope.countAuth++;
            else
                $scope.countRej++;
        }
    };

    $scope.scrollBottom = function () {
        $timeout(function () {
            $location.hash("btn-act-footer");
            $anchorScroll();
        }, 1);
    };

    $scope.doSave = function (urlToSave) {

        var settings = {
            dataType: "json",
            contentType: "application/json",
            type: "POST",
            url: urlToSave,
            data: JSON.stringify({lstAutRejActMon: $scope.m.lstAutRejActMon, ps: $scope.m.password, comments: $scope.m.comments}),
            success: $scope.onSuccessDoSave,
            error: $scope.onError
        };
        $.ajax(settings);
    };

    $scope.onSuccessDoSave = function (response) {
        if (response.hasError) {
            $scope.MsgError = $sce.trustAsHtml(response.message);
            $scope.$apply();
            return;
        }

        $scope.detail = 'resMsg';
        $scope.MsgResponse = $sce.trustAsHtml(response.message);

        $scope.$apply();
    };

});
