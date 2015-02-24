app.controller('userController', function($scope, $timeout) {
    $scope.m = {};

    $scope.lstRoles = [];
    $scope.m.role = 0;

    $scope.init = function(){

        if($scope.lstRoles === undefined || $scope.lstRoles.length <= 0)
            return;

        if($scope.m.roleId === undefined){
            $scope.m.role = $scope.lstRoles[0];
            $scope.m.roleId = $scope.m.role.id;
        }
        else{
            for(var i=0; i < $scope.lstRoles.length; i++){
                var role = $scope.lstRoles[i];

                if(role.id === $scope.m.roleId){
                    $scope.m.role = role;
                    break;
                }
            }
        }




    };


    $timeout(function() {
        $scope.init();
    }, 0);
});