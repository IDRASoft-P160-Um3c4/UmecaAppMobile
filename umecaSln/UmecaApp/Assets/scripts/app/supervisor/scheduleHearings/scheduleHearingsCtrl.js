app.controller('scheduleHearingsController', function ($scope, $sce, $timeout) {
    $scope.m = {};

    $scope.validateSave = function(){
        $scope.MsgErrorIn = "";

        if(!$scope.m.hasReminder)
            return true;

        var dt = $($scope.m.dtHearing).data('datepicker').date;
        var dtRem = $($scope.m.dtHearingReminder).data('datepicker').date;
        var dtC = new Date(dt.getFullYear(), dt.getMonth(), dt.getDate());
        var dtRemC = new Date(dtRem.getFullYear(), dtRem.getMonth(), dtRem.getDate());

        if(dtC <= dtRemC){
            $scope.MsgErrorIn = $sce.trustAsHtml("La fecha del recordatorio no puede ser el mismo d&iacute;a o d&eacute;spues de la audiencia");
            return false;
        }

        return true;
    };

});

