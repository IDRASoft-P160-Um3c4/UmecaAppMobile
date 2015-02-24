app.directive('loadStates', function ($http, $timeout) {
    return function (scope, elem, attr) {

        var currentTimeout = null;

        var urlRequest = attr['urlRequest'];

        var ajaxConf = {
            method: 'POST',
            url: urlRequest
        };

        elem.on('change blur', function () {
            if (scope.countryId == undefined || scope.countryId == "")
                return;

            ajaxConf.params = {countryId : scope.countryId};

            if (currentTimeout) {
                $timeout.cancel(currentTimeout);
            }

            currentTimeout = $timeout(function() {
                $http(ajaxConf)
                    .success(function (data) {
                        var states = jQuery.parseJSON(data.data);
                        if (states == undefined || states.length === 0) {
                            scope.clear();
                            return;
                        }
                        scope.listState = states;
                        scope.state =scope.listState[0];
                        scope.stateId = scope.state.id;
                    });
            }, 200);
        });
    };
});