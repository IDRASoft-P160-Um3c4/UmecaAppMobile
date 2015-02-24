app.controller('scheduleHearingsIndexController', function ($scope, $sce, $timeout) {
    $scope.m = {};

    $scope.onSearchField = function(gridId){
        $scope.MsgError = "";
        if($scope.m.searchField === undefined || $scope.m.searchField === "" || $scope.m.searchField.trim() === ""){
            $scope.MsgError = $sce.trustAsHtml("Debe definir la carpeta judicial");
            return;
        }

        $(gridId).setGridParam({ postData: {searchField: $scope.m.searchField} });
        $(gridId).trigger("reloadGrid");
        $scope.m.isSearchDone = true;
    };

    $scope.onScheduleHearings = function(gridId, lstChks){
        $scope.MsgError = "";
        var selected = [];
        $('td input:checked').each(function() {
            selected.push($(this).attr('value'));
        });

        if(selected.length <= 0){
            $scope.MsgError = $sce.trustAsHtml("Al menos debe elegir un caso para agendar la audiencia");
            return;
        }

        $timeout(function(){
            window.scheduleNewHearing(selected);
        }, 0);
    }
});

