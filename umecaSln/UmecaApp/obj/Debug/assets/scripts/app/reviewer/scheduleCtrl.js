app.controller('scheduleController', function($scope, $timeout) {
    $scope.s = {};
    $scope.listSchedule = [];


    $scope.init = function(){
        $('#timepickerEnd'+$scope.content).timepicker({
            minuteStep: 1,
            showSeconds: false,
            showMeridian: false
        }).next().on(ace.click_event, function(){
                $(this).prev().focus();
            });
        $('#timepickerStart'+$scope.content).timepicker({
            minuteStep: 1,
            showSeconds: false,
            showMeridian: false
        }).next().on(ace.click_event, function(){
                $(this).prev().focus();
            });
        if($scope.listSchedule == undefined){
            $scope.listSchedule = [];
        }else{
            try{
            if($scope.listSchedule[0].day== undefined){
                $scope.listSchedule = JSON.parse($scope.listSchedule);
            }
            }catch(e){}finally{
                $scope.matchSchedule();
            }

        }
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

    $scope.matchSchedule  = function (){
        var abc = $scope.listSchedule;

        $scope.schString = JSON.stringify(abc);
        /*for(var item in $scope.listSchedule){
            delete item[$$hashKey];
        } */
    }
});