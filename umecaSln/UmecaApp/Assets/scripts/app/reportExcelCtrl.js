app.controller('reportExcelController', function ($scope, $timeout, $http) {

        $scope.lstStatusCase = [];
        $scope.lstStatusMeeting = [];
        $scope.lstStatusVerification = [];
        $scope.lstGender = [];
        $scope.lstMaritalSt = [];
        $scope.lstJob = [];
        $scope.lstAcademicLvl = [];
        $scope.lstDrugs = [];
        $scope.lstLvlRisk = [];
        $scope.lstHearingType = [];
        $scope.lstCrimes = [];
        $scope.lstArrangement = [];
        $scope.lstActivities = [];

        $scope.lstStCaseStr = [];
        $scope.lstGenderStr = [];
        $scope.lstMarStStr = [];
        $scope.lstJobStr = [];
        $scope.lstAcLvlStr = [];
        $scope.lstDrugsStr = [];
        $scope.lstLvlRkStr = [];
        $scope.lstHearingTpStr = [];
        $scope.lstCrimesSel = [];
        $scope.lstCrimesStr = [];
        $scope.lstArrangementSel = [];
        $scope.lstArrangementStr = [];
        $scope.lstActivitiesSel = [];
        $scope.lstActivitiesStr = [];

        $scope.lstStates = [];
        $scope.lstMuns = [];
        $scope.lstLoca = [];


        $scope.urlGetMun = "";
        $scope.urlGetLoc = "";
        $scope.homePlace = false;

        $scope.m = {};
        $scope.m.filtersModel = {};

        $scope.init = function () {
            $scope.fillSelState();
            $scope.changeState($scope.stateSel);
        };

        $timeout(function () {
            $scope.init();
        }, 0);


        $scope.addIdToList = function (listName, id) {

            var idx = $scope.findIdInList(listName, id);

            if (idx != null) {
                $scope[listName].splice(idx, 1);
            } else {
                $scope[listName].push(id);
            }

        }
        ;

        $scope.findIdInList = function (listName, id) {

            if ($scope[listName] && $scope[listName] != undefined) {
                for (var i = 0; i < $scope[listName].length; i++) {
                    if ($scope[listName][i] == id)
                        return i;
                }
            }

            return null;
        };

        $scope.submitFindCases = function (formId, urlToPost) {

            if ($(formId).valid() == false)
                return;

            $scope.WaitFor = true;

            $.post(urlToPost, $(formId).serialize())
                .success(function (resp) {

                    $scope.WaitFor = false;
                    $scope.$apply();

                    if (resp.hasError == true) {
                        $scope.MsgError = resp.message;
                        $scope.$apply();
                    } else {
                        resp = resp.responseMessage;

                        $scope.fillFiltersModel();

                        window.reloadExcelGrid(resp.message, $scope.m.filtersModel);


                    }

                }
            )
                .
                error(function () {
                    $scope.WaitFor = false;
                    $scope.MsgError = "Error de red. Por favor intente más tarde.";
                    $scope.$apply();
                });

            return true;
        };

        $scope.fillSelState = function () {

            if ($scope.lstStates === undefined || $scope.lstStates.length <= 0)
                return;

            if ($scope.stateSelId === undefined) {
                $scope.stateSel = $scope.lstStates[0];
                $scope.stateSelId = $scope.stateSel.id;
            }
            else {
                for (var i = 0; i < $scope.lsStates.length; i++) {
                    var sta = $scope.lstStates[i];

                    if (sta.id === $scope.stateSelId) {
                        $scope.stateSel = sta;
                        break;
                    }
                }
            }
        };

        $scope.fillSelMun = function () {
            if ($scope.lstMuns === undefined || $scope.lstMuns.length <= 0)
                return;

            if ($scope.munSelId === undefined) {
                $scope.munSel = $scope.lstMuns[0];
                $scope.munSelId = $scope.munSel.id;
            }
            else {
                for (var i = 0; i < $scope.lstMuns.length; i++) {
                    var mun = $scope.lstMuns[i];
                    if (mun.id === $scope.munSelId) {
                        $scope.munSel = mun;
                        break;
                    }
                }
            }
        };

        $scope.fillSelLoc = function () {

            if ($scope.lstLoca === undefined || $scope.lstLoca.length <= 0)
                return;

            if ($scope.locSelId === undefined) {
                $scope.locSel = $scope.lstLoca[0];
                $scope.locSelId = $scope.locSel.id;
            }
            else {
                for (var i = 0; i < $scope.lstLoca.length; i++) {
                    var mun = $scope.lstLoca[i];
                    if (mun.id === $scope.locSelId) {
                        $scope.locSel = mun;
                        break;
                    }
                }
            }
        };

        $scope.fillFiltersModel = function () {

            $scope.m.filtersModel["iDt"] = $scope.initDate;
            $scope.m.filtersModel["eDt"] = $scope.endDate;
            $scope.m.filtersModel["mP"] = $scope.hasMonP;
            $scope.m.filtersModel["hJ"] = $scope.hasJob;

//            $scope.m.filtersModel["l1"] = $scope.lstStatusCase;
//            $scope.m.filtersModel["l2"] = $scope.lstStatusMeeting;
//            $scope.m.filtersModel["l3"] = $scope.lstStatusVerification;
//            $scope.m.filtersModel["l4"] = $scope.lstGender;
//            $scope.m.filtersModel["l5"] = $scope.lstMaritalSt;
//            $scope.m.filtersModel["l6"] = $scope.lstJob;
//            $scope.m.filtersModel["l7"] = $scope.lstAcademicLvl;
//            $scope.m.filtersModel["l8"] = $scope.lstDrugs;
//            $scope.m.filtersModel["l9"] = $scope.lstLvlRisk;
//            $scope.m.filtersModel["l10"] = $scope.lstHearingType;
//            $scope.m.filtersModel["l11"] = $scope.lstCrimesSel;
//            $scope.m.filtersModel["l12"] = $scope.lstArrangementSel;
//            $scope.m.filtersModel["l13"] = $scope.lstActivitiesSel;

            $scope.m.filtersModel["lstStCaseStr"] = $scope.lstStCaseStr;
            $scope.m.filtersModel["lstGenderStr"] = $scope.lstGenderStr;
            $scope.m.filtersModel["lstMarStStr"] = $scope.lstMarStStr;
            $scope.m.filtersModel["lstAcLvlStr"] = $scope.lstAcLvlStr;
            $scope.m.filtersModel["lstDrugsStr"] = $scope.lstDrugsStr;
            $scope.m.filtersModel["lstLvlRkStr"] = $scope.lstLvlRkStr;
            $scope.m.filtersModel["lstHearingTpStr"] = $scope.lstHearingTpStr;
            $scope.m.filtersModel["lstCrimeStr"] = $scope.lstCrimesStr;
            $scope.m.filtersModel["lstArrangementStr"] = $scope.lstArrangementStr;
            $scope.m.filtersModel["lstActivitiesStr"] = $scope.lstActivitiesStr;

        };


        $scope.changeState = function (stateSel) {

            $.post($scope.urlGetMun, {idSt: stateSel.id})
                .success(function (resp) {

                    if (resp.hasError == true) {
                        $scope.MsgError = resp.message;
                        $scope.$apply();
                    } else {
                        resp = resp.responseMessage;
                        $scope.lstMuns = $.parseJSON(resp.message);
                        $scope.munSelId = undefined;
                        $scope.fillSelMun();
                        $scope.$apply();
                        $scope.changeMun($scope.munSel);
                    }
                })
                .error(function () {
                    $scope.WaitFor = false;
                    $scope.MsgError = "Error de red. Por favor intente más tarde.";
                    $scope.$apply();
                });
        };

        $scope.changeMun = function (munSel) {

            $.post($scope.urlGetLoc, {idMun: munSel.id})
                .success(function (resp) {

                    if (resp.hasError == true) {
                        $scope.MsgError = resp.message;
                        $scope.$apply();
                    } else {
                        resp = resp.responseMessage;
                        $scope.lstLoca = $.parseJSON(resp.message);
                        $scope.locSelId = undefined;
                        $scope.fillSelLoc();
                        $scope.$apply();
                    }
                })
                .error(function () {
                    $scope.WaitFor = false;
                    $scope.MsgError = "Error de red. Por favor intente más tarde.";
                    $scope.$apply();
                });
        };

    }
)
;