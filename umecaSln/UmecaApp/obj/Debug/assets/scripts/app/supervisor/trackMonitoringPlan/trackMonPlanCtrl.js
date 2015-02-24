app.controller('trackMonPlanController', function($scope, $timeout, sharedSvc){

    $scope.m = {};
    $scope.yearStart = -1;
    $scope.monthStart = -1;
    $scope.activityIdCurrent = -1;
    $scope.caseIdCurrent = -1;
    $scope.supIdCurrent = -1;
    $scope.yearEnd = -1;
    $scope.monthEnd = -1;
    $scope.mainMonPlanId = -1;

    $scope.returnToCases = function(url){
        $scope.waitFor = true;
        window.goToUrlMvcUrl(url);
    }

    $scope.loadActivities = function(monPlanId, dateStart, dateEnd, urlToPost){
        if($scope.m.activity === undefined){
            $scope.m.activity = {id: 0};
        }
        if($scope.m.case === undefined){
            $scope.m.case = {id:0};
        }
        if($scope.m.user === undefined){
            $scope.m.user = {id:0};
        }

        var yearStart = dateStart.getFullYear();
        var monthStart = dateStart.getMonth();
        var yearEnd = dateEnd.getFullYear();
        var monthEnd = dateEnd.getMonth();
        $scope.mainMonPlanId = monPlanId;

        if($scope.filtersAreEqual() &&
            yearStart === yearEnd && monthStart === monthEnd){
            if($scope.yearStart === yearStart && $scope.monthStart === monthStart ||
                $scope.yearEnd === yearEnd && $scope.monthEnd === monthEnd){
                return;
            }
        }
        else{
            if($scope.filtersAreEqual() &&
                $scope.yearStart === yearStart && $scope.monthStart === monthStart &&
                $scope.yearEnd === yearEnd && $scope.monthEnd === monthEnd){
                return;
            }
        }


        $scope.activityIdCurrent = $scope.m.activity.id;
        $scope.caseIdCurrent = $scope.m.case.id;
        $scope.supIdCurrent = $scope.m.user.id;
        $scope.yearStart = dateStart.getFullYear();
        $scope.monthStart = dateStart.getMonth();

        $scope.yearEnd = dateEnd.getFullYear();
        $scope.monthEnd = dateEnd.getMonth();

        $scope.workingTrack = true;
        $scope.$apply();

        $.ajax({
            url: urlToPost,
            type: "POST",
            data: JSON.stringify({monPlanId: monPlanId, yearStart: $scope.yearStart, monthStart: ($scope.monthStart+1),
                yearEnd: $scope.yearEnd, monthEnd: ($scope.monthEnd+1), activityId: $scope.m.activity.id, caseFilterId: $scope.m.case.id,
                userFilterId: $scope.m.user.id}),
            success: $scope.handleSuccess,
            error: $scope.handleError,
            dataType: "json",
            contentType: "application/json"
        });
    }

    $scope.filtersAreEqual = function(){
        if($scope.activityIdCurrent == $scope.m.activity.id && $scope.caseIdCurrent == $scope.m.case.id &&$scope.supIdCurrent == $scope.m.user.id)
            return true;
        return false;
    }

    $scope.idToName = function(id, lstCat){
        for(var i=0; i<lstCat.length; i++){
            var dat = lstCat[i];
            if(id === dat.id){
                return dat.name;
            }
        }

        return "";
    }

    $scope.processActivities = function(data){
        var lstGoals = data.lstGoals;
        var lstActivities = data.lstActivities;
        var lstMonPlanActivities = data.lstMonPlanActivities;
        var lstEvents = [];
        var today = window.stringToDate(data.today);
        today.setHours(0,0,0,0);

        for(var i=0; i<lstMonPlanActivities.length; i++){
            var act = lstMonPlanActivities[i];
            var className = 'label-info';
            var end = window.stringToDate(act.end);

            className = window.colorActMonPlan(act.status, end, today, undefined, act.suspended);

            if(act.monitoringPlanId === $scope.mainMonPlanId){
                className = className + ' border-event-main';
            }

            var event = {
                title: "Caso " + act.caseId + "  (" + act.mpId + ") Imputado: "
                        + act.personName + " / " + $scope.idToName(act.activityId, lstActivities) + " / " + $scope.idToName(act.goalId, lstGoals),
                idActivity: act.activityMonId,
                start: window.stringToDate(act.start),
                end: end,
                allDay: false,
                className: className,
                suspended: act.suspended
            };
            lstEvents.push(event);
        }
        return lstEvents;
    };

    $scope.handleSuccess = function(data){
        try{
            $scope.workingTrack = false;
            $scope.$apply();
            if(data.hasError === true){
                sharedSvc.showMsg({title: "Planes de seguimiento",message: data.message,type: "danger"});
                return;
            }

            var info = $scope.processActivities(data);
            $scope.m.calendar.fullCalendar('removeEvents');

            for(i=0; i<info.length; i++){
                $scope.m.calendar.fullCalendar('renderEvent', info[i], true);
            }
            $scope.m.calendar.fullCalendar('unselect');
        }catch(ex){
            sharedSvc.showMsg({
                title: "Error de red",
                message: "<strong>No fue posible conectarse al servidor</strong> <br/><br/>Por favor intente más tarde",
                type: "danger"
            });
        }
    }

    $scope.handleError = function(data){
        $scope.workingTrack = false;
        sharedSvc.showMsg({
            title: "Error de red",
            message: "<strong>No fue posible conectarse al servidor</strong> <br/><br/>Por favor intente más tarde",
            type: "danger"
        });
    }

    $scope.initActivitySelect = function(){
        if($scope.lstActivities !== undefined && $scope.lstActivities.length > 0){
            $timeout(function(){
                $scope.m.activity = $scope.lstActivities[0];
            }, 1);
        }
    }


    $scope.initCaseSelect = function(){
        if($scope.lstCases !== undefined && $scope.lstCases.length > 0){
            $timeout(function(){
                $scope.m.case = $scope.lstCases[0];
            }, 1);
        }
    }

   $scope.initUserSelect = function(){
        if($scope.lstUser !== undefined && $scope.lstUser.length > 0){
            $timeout(function(){
                $scope.m.user = $scope.lstUser[0];
            }, 1);
        }
    }

    $scope.onChangeSelect = function(){
        $timeout(function(){
            window.calendar.fullCalendar( 'render' );
        }, 1);
    }
});