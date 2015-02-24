app.controller('victimController', function($scope, $timeout,$rootScope) {
    $scope.v = {};
    $scope.lstRel = [];
    $scope.v.rel = 0;
    $scope.nameAddress = "address."

    $scope.init = function(){
        if($scope.lstRel === undefined || $scope.lstRel.length <= 0)
            return;

        if($scope.v.relId === undefined){
            $scope.v.rel = $scope.lstRel[0];
            $scope.v.relId = $scope.v.rel.id;
        }
        else{
            for(var i=0; i < $scope.lstRel.length; i++){
                var rel = $scope.lstRel[i];

                if(rel.id === $scope.v.relId){
                    $scope.v.rel = rel;
                    break;
                }
            }
        }
    };


    $timeout(function() {
        $scope.init();
    }, 0);
});