	app.controller('scheduleController', function($scope, $timeout) {
    $scope.s = {};
    $scope.listSchedule = [];


    $scope.init = function(){
    if( $scope.content == undefined || $scope.content=="" ){
    	
    }
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
            try{
            console.log("1 init schedule");
            if($scope.schString==undefined){$scope.schString=$("#hdnJsonSchedule"+$scope.content).val();}
            console.log("$scope.schString-"+$scope.schString);
            console.log("hdnJsonSchedule"+$scope.content+"-"+$("#hdnJsonSchedule"+$scope.content).val());
            if($scope.schString != undefined&&$scope.schString != null&&$scope.schString != ""){
                $scope.listSchedule = JSON.parse($scope.schString);
                console.log("2 init schedule");
            }else{
            	$scope.listSchedule = [];
            }
            }catch(e){console.log("error init schedule err---->"+e.message);}finally{
                $scope.matchSchedule();
            }
    };


    $timeout(function() {
        $scope.init();
    }, 0);

   $scope.addSchedule = function(){
      if($scope.s.Day==undefined|| $scope.s.Day.trim() == "" ||  $scope.s.Start == undefined || $scope.s.End == undefined){
          $scope.msgError="Favor de ingresar un día, una hora de inicio y una hora de fin";
      }else{
          $scope.msgError="";
          var a ={};
          a.Day = $scope.s.Day;
          a.Start = $scope.s.Start;
          a.End = $scope.s.End;
          if($scope.listSchedule == undefined){
            $scope.listSchedule = [];
        	}
console.log("typeof listSchedule >>"+typeof $scope.listSchedule+"    ...and string==>"+JSON.stringify($scope.listSchedule) );
          $scope.listSchedule.push(a);
          $scope.s.Day = "";
          $scope.s.Start = "12:00";
          $scope.s.End = "12:00";
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