app.controller("changeSupervisorController", function($scope){
    $scope.m = {};
    $scope.m.isAccepted = false;
    $scope.m.hasPassword = undefined;

    $("input").keypress(function(event) {
        if (event.which == 13 && $scope.m.hasPassword!="") {
            event.preventDefault();
            $( "#bntSubmitChangeSup" ).trigger( "click" );
        }
    });
});
