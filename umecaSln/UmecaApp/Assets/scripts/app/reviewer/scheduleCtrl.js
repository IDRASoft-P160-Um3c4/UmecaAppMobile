	app.controller('scheduleController', function($scope, $timeout) {
    $scope.s = {};
    $scope.listSchedule = [];
    $scope.content = "School";


    $scope.init = function(){
    $scope.listSchedule = [];
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
            console.log("1 init schedule");
            if($scope.schString==undefined){$scope.schString=$("#hdnJsonScheduleSchool").val();}
//            console.log("$scope.schString-"+$scope.schString);
//            console.log("hdnJsonScheduleSchool-"+$("#hdnJsonScheduleSchool").val());
            if($scope.schString != undefined){
                $scope.listSchedule = JSON.parse($scope.schString);
                console.log("2 init schedule");
            }
            }catch(e){console.log("error init schedule err---->"+e.message);}finally{
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
          if($scope.listSchedule == undefined){
            $scope.listSchedule = [];
        	}
console.log("typeof listSchedule >>"+typeof $scope.listSchedule+"    ...and string==>"+JSON.stringify($scope.listSchedule) );
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