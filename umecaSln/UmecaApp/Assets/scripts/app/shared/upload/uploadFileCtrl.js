app.controller('uploadFileController', function($scope, $timeout, $sce) {
    $scope.m = {};
    $scope.listTypeName = [];


    $scope.setOutError = function(msg){
        $scope.$apply(function(){
            $scope.MsgError = $sce.trustAsHtml(msg);
            $timeout(function(){
                $scope.MsgError = $sce.trustAsHtml("");
            }, 10000)
        });
    }

    $scope.setSuccess = function(msg){
        $scope.$apply(function(){

            $scope.MsgSuccess = $sce.trustAsHtml(msg);
            $timeout(function(){
                $scope.MsgSuccess = $sce.trustAsHtml("");
            }, 10000)
        });
    }

    $scope.downloadAll = function(){
        window.downloadAll();
    }

    $timeout(function () {
        $scope.init();
    }, 0);

    $scope.init = function () {
         if($scope.listTypeName==undefined || $scope.listTypeName.length == 0){
             $scope.m.typeNameFileId = $scope.defaultType.id;
             return;
         }
         $scope.m.typeNameFile =$scope.listTypeName[0];
         $scope.m.typeNameFileId = $scope.m.typeNameFile.id;
    };

});