app.controller('monActivityController', function($scope, $location, $anchorScroll, $timeout){
    $scope.m = {};
    $scope.m.do = false;
    $scope.m.notDo = false;
    $scope.isReadOnly = true;
    $scope.m.lstArrangements = {};
    $scope.m.isAgree = false;

    $scope.do = function(){
        $scope.m.do = true;
        $scope.m.notDo = !($scope.m.do);
        $scope.m.commentsFail = "";
        $timeout(function(){
            $location.hash("btn-act-footer");
            $anchorScroll();
        },1);
    };

    $scope.notDo = function(){
        $scope.m.notDo = true;
        $scope.m.do = !($scope.m.notDo);
        $scope.m.commentsOk = "";

        $scope.setDefaultArrangements();

        $scope.updateArr();

        $timeout(function(){
            $location.hash("btn-act-footer");
            $anchorScroll();
        },1);
    };

    $scope.setDefaultArrangements = function(){
        for(var arr in $scope.m.lstArrangements){
            $scope.m.lstArrangements[arr] = -1;
        }
    }

    $scope.initArrangements = function(){
        for(var i=0; i<$scope.lstArrangements.length; i++){
            var element = $scope.lstArrangements[i];
            $scope.m.lstArrangements[element.id] = element.intValue;
        }
        $scope.updateArr();
    }

    $scope.updateArr = function(){
        var arrangements = [];
        for(var arr in $scope.m.lstArrangements){
            arrangements.push({id:arr, value:$scope.m.lstArrangements[arr]});
        }
        $scope.m.arrangementsValues = JSON.stringify(arrangements);
    }

});