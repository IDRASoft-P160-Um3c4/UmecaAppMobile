app.controller("supervisionLogController", function($scope, $timeout,$sce, sharedSvc){

    $scope.reconstructedLstActMonPlan = [];
    $scope.assignedArrangementFilter = [];
    $scope.activitiesTypeFilter = [];
    $scope.b = {};
    $scope.m = {};
    $scope.m.lstMonActPlanSel = {};

    $scope.init = function () {
        var element = {};
        element.id = 0;
        element.name = "Todas";
        element.description = "";

        $scope.assignedArrangementFilter.push(element);
        $scope.assignedArrangementFilter = $scope.assignedArrangementFilter.concat($scope.lstHfAssignedArrangement);
        $scope.b.filterAssArr = $scope.assignedArrangementFilter[0];

        $scope.activitiesTypeFilter.push(element);
        $scope.activitiesTypeFilter = $scope.activitiesTypeFilter.concat($scope.lstActivities);
        $scope.b.filterActTyp = $scope.activitiesTypeFilter[0];


    };

    $scope.passToJson = function(str){
        str = "{\"sch\":"+str+"}";
        return str;
//        var a =str.split(',');
//        var b = [];
//        for(var i  = 0 ; i< a.length; i++){
//            b.push(JSON.stringify(a[i]));
//        }
        //return JSON.parse(str);
    };

    $scope.fillByFilter = function(){
        $scope.WaitFor = true;
        $scope.MsgError = "";
        var data = {};
        data.id = $scope.mpId;
        data.assignedArrangementId = $scope.b.filterAssArr.id;
        data.activityId = $scope.b.filterActTyp.id;
        var settings = {
            dataType: "json",
            type: "POST",
            url: $scope.urlToFill,
            data: data,
            success: function (resp) {
                $scope.WaitFor = false;
                if (resp.hasError === undefined) {
                    resp = resp.responseMessage;
                }
                if (resp.hasError === true) {
                    $scope.MsgError = "Ha ocurrido un error al filtrar las actividades.";
                    $scope.$apply();
                }
                else {
                    $scope.lstActMonPlan = JSON.parse(resp.returnData);
                    $scope.constructActMonPlan();
                    $scope.$apply();
                }
            },
            error: function () {
                $scope.MsgError = "No se ha podido conectar al servidor";
                $scope.$apply();
            }
        };

        $.ajax(settings);
    };

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
    }

    $scope.generateAssignedArrangements = function (actMonPlanId) {
        var bFound = false;
        var lstAssignedArrangements = [];
        for (var i = $scope.lstActMonPlanArrangement.length - 1; i >= 0; i--) {
            var mpArr = $scope.lstActMonPlanArrangement[i];
            if (mpArr.actMonPlanId === actMonPlanId) {
                bFound = true;
                var assArr = $scope.idToObject(mpArr.assignedArrangementId, $scope.lstHfAssignedArrangement);
                lstAssignedArrangements.push({
                    name: assArr.name,
                    status: $scope.setStatus(mpArr.status)
                });

                //$scope.lstActMonPlanArrangement.splice(i,1);   Se comenta para poder filtrar las actividades
            }

            if (bFound === true && mpArr.actMonPlanId !== actMonPlanId)
                break;
        }

        return lstAssignedArrangements;
    };
    
    $scope.formatHtml = function(sHtml){
        return $sce.trustAsHtml(sHtml);
    };


    $scope.constructActMonPlan = function(){

        $scope.reconstructedLstActMonPlan = [];

        for (var i = 0; i < $scope.lstActMonPlan.length; i++) {
            var act = $scope.lstActMonPlan[i];

            var actSup = $scope.idToObject(act.actSupervisionId, $scope.lstActivities);
            var aidSource = $scope.idToObject(act.aidSourceId, $scope.lstSources);

            //id
            var lstAssignedArrangements = $scope.generateAssignedArrangements(act.id);
            var comments = (act.comments === undefined || act.comments === null) ? "NA" : act.comments;

            var descAux = "";

            if (aidSource.description == undefined)
                descAux = "NA";
            else
                descAux = aidSource.description;

            var actRec = {
                id: act.id,
                start: act.start,
                end: act.end,
                supActivity: (actSup === undefined ? "NA" : actSup.name),
                aidSource: (aidSource === undefined ? "NA" : aidSource.name + " / " + descAux),
                lstAssignedArrangements: lstAssignedArrangements,
                status: act.status,
                comments: comments,
                user: act.user
            };

            $scope.reconstructedLstActMonPlan.push(actRec);

        }

    }

    $scope.formatHtml = function(sHtml){
        return $sce.trustAsHtml(sHtml);
    };



    $scope.splitAssignedArrangements = function (actMonPlanId) {
        var bFound = false;
        var lstAssignedArrangementsOk = [];
        var lstAssignedArrangementsFailed = [];

        for (var i = $scope.lstActMonPlanArrangement.length - 1; i >= 0; i--) {
            var mpArr = $scope.lstActMonPlanArrangement[i];
            if (mpArr.actMonPlanId === actMonPlanId) {
                bFound = true;
                var assArr = $scope.idToObject(mpArr.assignedArrangementId, $scope.lstHfAssignedArrangement);

                switch (mpArr.status) {
                    case 1:
                        lstAssignedArrangementsOk.push({
                            name: assArr.name,
                            status: $scope.setStatus(mpArr.status),
                            description: assArr.description
                        });
                        break;
                    case 0:
                        lstAssignedArrangementsFailed.push({
                            name: assArr.name,
                            status: $scope.setStatus(mpArr.status),
                            description: assArr.description
                        });
                        break;
                    default:
                        break;
                }

                $scope.lstActMonPlanArrangement.splice(i, 1);
            }

            if (bFound === true && mpArr.actMonPlanId !== actMonPlanId)
                break;
        }

        return {lstOk: lstAssignedArrangementsOk, hasOk: (lstAssignedArrangementsOk.length > 0),
            lstFailed: lstAssignedArrangementsFailed, hasFailed: (lstAssignedArrangementsFailed.length > 0)};
    }

    $scope.constructActMonPlanAccomplishment = function () {
        $scope.reconstructedLstActMonPlanOk = [];
        $scope.reconstructedLstActMonPlanFailed = [];

        for (var i = 0; i < $scope.lstActMonPlan.length; i++) {
            var act = $scope.lstActMonPlan[i];

            var actSup = $scope.idToObject(act.actSupervisionId, $scope.lstActivities);
            var aidSource = $scope.idToObject(act.aidSourceId, $scope.lstSources);

            //id
            var splitInfo = $scope.splitAssignedArrangements(act.id);
            var comments = (act.comments === undefined || act.comments === null) ? "NA" : act.comments;

            if (splitInfo.hasOk) {
                var actRec = {
                    start: act.start,
                    end: act.end,
                    supActivity: (actSup === undefined ? "NA" : actSup.name),
                    aidSource: (aidSource === undefined ? "NA" : aidSource.name + " / " + aidSource.description),
                    lstAssignedArrangements: splitInfo.lstOk,
                    status: act.status,
                    comments: comments
                };

                $scope.reconstructedLstActMonPlanOk.push(actRec);
            }

            if (splitInfo.hasFailed) {
                var actRec = {
                    start: act.start,
                    end: act.end,
                    supActivity: (actSup === undefined ? "NA" : actSup.name),
                    aidSource: (aidSource === undefined ? "NA" : aidSource.name + " / " + aidSource.description),
                    lstAssignedArrangements: splitInfo.lstFailed,
                    status: act.status,
                    comments: comments
                };

                $scope.reconstructedLstActMonPlanFailed.push(actRec);
            }

        }
    }

    $scope.createLabel = function (status) {
        return 'label ' + window.colorActMonPlan(status);
    };

    $scope.selectedAll = function(){
        var bSelection = false;
        if($scope.m.isSelectedAll === true){
            bSelection = true;
        }
        for (var key in $scope.m.lstMonActPlanSel) {
            if($scope.m.lstMonActPlanSel.hasOwnProperty(key))
                $scope.m.lstMonActPlanSel[key] = bSelection;
        }
    };

    $scope.deleteActivities = function(urlToGo){

        var lstActivitiesDel=[];
        for (var key in $scope.m.lstMonActPlanSel) {
            if($scope.m.lstMonActPlanSel.hasOwnProperty(key))
                if($scope.m.lstMonActPlanSel[key]) lstActivitiesDel.push(key);
        }

        if(lstActivitiesDel.length === 0){
            sharedSvc.showMsg(
                {
                    title: "Eliminar actividades",
                    message: "Al menos debe seleccionar una actividad para eliminar",
                    type: "danger"
                });
            return;
        }


        sharedSvc.showConfPass({ title: "Eliminar actividades", message: "&iquest;Est&aacute; seguro de que desea elimnar la(s) " +
            lstActivitiesDel.length + " actividad(es) del plan de monitoreo?", type: "warning", agreeCheck: "Estoy de acuerdo con eliminar la(s) actividad(es)" })
            .then(function (respMsg) {
                sharedSvc.doPost({lstActivitiesDel:lstActivitiesDel, caseId:$scope.caseId, monitoringPlanId:$scope.mpId, password:respMsg.m.password}, urlToGo)
                    .then($scope.onSuccessDel);
            });
    };

    $scope.onSuccessDel = function(resp){
        debugger;
        sharedSvc.showMsg(
            {
                title: "Eliminar actividades",
                type: "info",
                message: resp
            });

        $scope.fillByFilter();
    };
});