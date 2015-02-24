app.controller('rolSupervisionController', function ($scope, sharedSvc) {

    $scope.lstActivityDelIds = [];
    $scope.msgError = undefined;
    $scope.waitFor = false;


    $scope.addActivityToDelete = function(id){
        if(id === -1)
            return;
        $scope.lstActivityDelIds.push(id);
    }

    $scope.saveRolActivities = function(urlToPost){
        $scope.msgError = undefined;
        $scope.waitFor = true;

        try{
            var lstEvents = $scope.m.calendar.fullCalendar('clientEvents');

            var lstActivities = [];

            for(var i=0; i<lstEvents.length; i++){
                var event = lstEvents[i];

                if(event.isModified !== true)
                    continue;

                var infoAct = event.infoActivity;

                var start = window.formatDateTime(event.start);
                var end = window.formatDateTime(event.end);

                lstActivities.push({
                    rolActivityId: event.idActivity,
                    eventId : event._id,
                    end: end,
                    start: start,
                    supervisorId: infoAct.supervisor.id
                });
            }

            if(lstActivities.length === 0 && $scope.lstActivityDelIds.length === 0){
                sharedSvc.showMsg({title: "Rol de supervisión",message: "No existen actividades para agregar, actualizar o eliminar",type: "info"});
                $scope.waitFor = false;
                return false;
            }

            var activityUpsert = {lstActivitiesUpsert:lstActivities, lstActivitiesDel: $scope.lstActivityDelIds};

            $.ajax({
                url: urlToPost,
                type: "POST",
                data: JSON.stringify(activityUpsert),
                success: $scope.handleSuccess,
                error: $scope.handleError,
                dataType: "json",
                contentType: "application/json"
            });
        }catch(e){
            $scope.waitFor = false;
        }


    }

    $scope.handleSuccess = function(resp){
        try{
            $scope.waitFor = false;

            if (resp.hasError === undefined || resp.hasError === true) {
                $scope.msgError = resp.message;
                $scope.$apply();
                return;
            }
            else if(resp.hasError === false){
                $scope.lstActivityDelIds = [];
                try{
                    var lstEvents = JSON.parse(resp.returnData);
                    for(var i=0; i<lstEvents.length; i++){
                        var eventInfo = lstEvents[i];
                        var event = $scope.m.calendar.fullCalendar('clientEvents', eventInfo.eventId);

                        if(event !== undefined && event.length > 0){
                            var fstEvent = event[0];
                            fstEvent.idActivity = eventInfo.rolActivityId;
                            fstEvent.doTitle(false);
                            fstEvent.isModified = false;
                            $scope.m.calendar.fullCalendar('updateEvent', event);
                        }
                    }

                }catch(eIn){}
                sharedSvc.showMsg({title: "Rol de supervisión",message: resp.message,type: "success"}).then();
            }
            $scope.$apply();
        } catch (e) {
            $scope.msgError = "Error inesperado de datos. Por favor intente más tarde.";
            $scope.$apply();
        }
    };

    $scope.handleError = function(){
        $scope.waitFor = false;
        $scope.msgError = "Error de red. Por favor intente más tarde.";
        $scope.$apply();

    };

    $scope.loadActivities = function(dateStart, dateEnd, urlToPost){
        var yearStart = dateStart.getFullYear();
        var monthStart = dateStart.getMonth();
        var yearEnd = dateEnd.getFullYear();
        var monthEnd = dateEnd.getMonth();

        if(yearStart === yearEnd && monthStart === monthEnd){
            if($scope.yearStart === yearStart && $scope.monthStart === monthStart ||
                $scope.yearEnd === yearEnd && $scope.monthEnd === monthEnd){
                return;
            }
        }
        else{
            if($scope.yearStart === yearStart && $scope.monthStart === monthStart &&
                $scope.yearEnd === yearEnd && $scope.monthEnd === monthEnd){
                return;
            }
        }

        $scope.yearStart = dateStart.getFullYear();
        $scope.monthStart = dateStart.getMonth();

        $scope.yearEnd = dateEnd.getFullYear();
        $scope.monthEnd = dateEnd.getMonth();

        $scope.workingTrack = true;
        $scope.$apply();

        $.ajax({
            url: urlToPost,
            type: "POST",
            data: JSON.stringify({monPlanId:-1, yearStart: $scope.yearStart, monthStart: ($scope.monthStart+1),
                yearEnd: $scope.yearEnd, monthEnd: ($scope.monthEnd+1)}),
            success: $scope.handleSuccessLoad,
            error: $scope.handleErrorLoad,
            dataType: "json",
            contentType: "application/json"
        });
    }

    $scope.idToName = function(id, lstCat, position){
        for(var i=0; i<lstCat.length; i++){
            var dat = lstCat[i];
            if(id === dat.id){
                switch (position){
                    case 0:
                        return dat.name;
                    case 1:
                        return dat.description;
                }
            }
        }
        return "";
    }

    $scope.idToObject = function(id, lstCat){
        for(var i=0; i<lstCat.length; i++){
            var cat = lstCat[i];
            if(cat.id == id){
                return cat;
            }
        }
        return undefined;
    };

    $scope.processActivities = function(data){
        var lstSupervisor = data.lstSupervisor;
        var lstRolActivities = data.lstRolActivities;
        var lstEvents = [];
        var today = window.stringToDate(data.today);
        today.setHours(0,0,0,0);


        for(var i=0; i<lstRolActivities.length; i++){
            var act = lstRolActivities[i];
            var className = 'label-info';
            var end = window.stringToDate(act.end);

            if(act.status === "NUEVA" || act.status === "MODIFICADA"){
                if(end < today)
                    className = 'label-success';
            }

            var supervisor = $scope.idToObject(act.supervisorId, $scope.m.lstSupervisor);

            var event = {
                doTitle: function(isModified){
                    this.title = (isModified === true ? "*" : "") + "Usuario "
                        + this.infoActivity.supervisor.name + "\nNombre: "
                        + this.infoActivity.supervisor.description;
                },
                title: "Usuario "
                    + $scope.idToName(act.supervisorId, lstSupervisor, 0) + "\nNombre: "
                    + $scope.idToName(act.supervisorId, lstSupervisor, 1),
                idActivity: act.rolActivityId,
                start: window.stringToDate(act.start),
                end: end,
                allDay: false,
                className: className,
                infoActivity:{
                    supervisor: supervisor
                }
            };
            lstEvents.push(event);
        }

        return lstEvents;
    };

    $scope.handleSuccessLoad = function(data){
        try{
            $scope.workingTrack = false;
            $scope.$apply();
            if(data.hasError === true){
                sharedSvc.showMsg({title: "Rol de supervisión",message: data.message,type: "danger"});
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

    $scope.handleErrorLoad = function(data){
        $scope.workingTrack = false;
        sharedSvc.showMsg({
            title: "Error de red",
            message: "<strong>No fue posible conectarse al servidor</strong> <br/><br/>Por favor intente más tarde",
            type: "danger"
        });
    }

});