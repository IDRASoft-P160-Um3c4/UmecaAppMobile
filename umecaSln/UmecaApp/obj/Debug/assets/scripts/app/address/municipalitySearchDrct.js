app.directive('findMunicipality', function ($http, $timeout) {
    return function (scope, elem, attr) {

        var currentTimeout = null;

        var urlRequest = attr['urlRequest'];

        var ajaxConf = {
            method: 'POST',
            url: urlRequest
        };

        elem.on('change blur', function () {
            if (scope.stateId == undefined)
                return;

            ajaxConf.params = {idState : scope.stateId};

            if (currentTimeout) {
                $timeout.cancel(currentTimeout);
            }

            currentTimeout = $timeout(function() {
                $http(ajaxConf)
                    .success(function (data) {
                        data.data=jQuery.parseJSON(data.data);
                        if (data.data == undefined || data.data.length === 0) {
                          //  cat={};
                            return;
                        }
                        scope.listMunicipality = data.data;
                       scope.municipality =scope.listMunicipality[0];
                        scope.municipalityId = scope.municipality.id;
                        scope.setLocation();
                    });
            }, 200);
        });
    };
});