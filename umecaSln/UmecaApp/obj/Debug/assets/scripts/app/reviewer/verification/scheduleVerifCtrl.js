app.controller('scheduleVerifController', function($scope, $timeout, $rootScope) {
    $scope.s = {};
    $scope.listSchedule = [];


    $scope.init = function(){
        $scope.listSchedule = [];
    };


    $timeout(function() {
        $scope.init();
    }, 0);

    $scope.addSchedule = function(){
        if($scope.s.day==undefined|| $scope.s.day.trim() == "" ||  $scope.s.start == undefined || $scope.s.end == undefined){
            $scope.msgError="Favor de ingresar un día, una hora de inicio y una hora de fin";
        }else{
            $scope.msgError="";
            var a ={};
            a.day = $scope.s.day;
            a.start = $scope.s.start;
            a.end = $scope.s.end;
            $scope.listSchedule.push(a);
            $scope.s.day = "";
            $scope.s.start = "12:00";
            $scope.s.end = "12:00";
            $scope.matchSchedule();
        }
    };

    $scope.deleteSchedule = function(index){
        $scope.listSchedule.splice(index,1);
        $scope.matchSchedule();

    };

    $rootScope.$on('CleanScheduleList', function () {
        $scope.listSchedule=[];
    });

    $scope.matchSchedule  = function (){
        var abc = $scope.listSchedule;

        $scope.schString = JSON.stringify(abc);
        $rootScope.$broadcast('MatchScheduleList',$scope.schString);

    }
});