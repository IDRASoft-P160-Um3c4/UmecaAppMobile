app.controller("framingMeetingLogController", function ($scope, $timeout, $sce) {

    $scope.b = {};
    $scope.lstAlterLog = [];
    $scope.lstDayChanges = [];
    $scope.lstFinalLog = [];
    $scope.lstTerminateLog = [];

    $scope.init = function () {
        $scope.doTerminateLstLog();
        $scope.doChangesLstLog();

    };

    $scope.toTrustHtml = function (cad) {
        return $sce.trustAsHtml(cad);
    }

    $timeout(function () {
        $scope.init();
    }, 0);

    $scope.idToObject = function (id, lstCat) {
        for (var i = 0; i < lstCat.length; i++) {
            var cat = lstCat[i];
            if (cat.id == id) {
                return cat;
            }
        }
        return undefined;
    };

    $scope.doChangesLstLog = function () {


        for (var i = 0; i < $scope.lstAlterLog.length; i++) {
            $scope.lstAlterLog[i].lstElements = $.parseJSON($scope.lstAlterLog[i].description);

            for (var j = 0; j < $scope.lstAlterLog[i].lstElements.length; j++) {
                if ($scope.lstAlterLog[i].lstElements[j].fieldName == "Disponibilidad") {
                    var arrSchedule = $.parseJSON($scope.lstAlterLog[i].lstElements[j].value);
                    var strSchedule = "";
                    for (var k = 0; k < arrSchedule.length; k++) {
                        if (strSchedule != "")
                            strSchedule += ", ";
                        strSchedule += arrSchedule[k].day + " de " + arrSchedule[k].start + " a " + arrSchedule[k].end;
                    }
                    $scope.lstAlterLog[i].lstElements[j].value = strSchedule;
                }
            }
        }

        for (var a = 0; a < $scope.lstDayChanges.length; a++) {
            var obj = {date: $scope.lstDayChanges[a], lstAlterLog: []};

            for (var b = 0; b < $scope.lstAlterLog.length; b++) {
                if ($scope.lstAlterLog[b].strDate === $scope.lstDayChanges[a]) {
                    obj.lstAlterLog.push($scope.lstAlterLog[b]);
                }
            }
            $scope.lstFinalLog.push(obj);
        }
    };

    $scope.doTerminateLstLog = function () {

        for (var i = 0; i < $scope.lstTerminateLog.length; i++) {
            $scope.lstTerminateLog[i].lstElements = $.parseJSON($scope.lstTerminateLog[i].description);

            for (var j = 0; j < $scope.lstTerminateLog[i].lstElements.length; j++) {
                if ($scope.lstTerminateLog[i].lstElements[j].fieldName == "Disponibilidad") {
                    var arrSchedule = $.parseJSON($scope.lstTerminateLog[i].lstElements[j].value);
                    var strSchedule = "";
                    for (var k = 0; k < arrSchedule.length; k++) {
                        if (strSchedule != "")
                            strSchedule += ", ";
                        strSchedule += arrSchedule[k].day + " de " + arrSchedule[k].start + " a " + arrSchedule[k].end;
                    }
                    $scope.lstTerminateLog[i].lstElements[j].value = strSchedule;
                }
            }
        }
    };


    $scope.setStatus = function (status) {
        switch (status) {
            case -1:
                return "No definido";
            case 0:
                return "Incumplido";
            case 1:
                return "Cumplido";
            default:
                return "NA";
        }
    };

    $scope.formatHtml = function (sHtml) {

        return $sce.trustAsHtml(sHtml);
    };

    $scope.getLogType = function (cad) {
        switch (cad) {
            case "ADDED":
                return "Agregado"
                break;
            case "MODIFIED":
                return "Modificado"
                break;
            case "DELETED":
                return "Eliminado"
                break;
        }
    };


})
;