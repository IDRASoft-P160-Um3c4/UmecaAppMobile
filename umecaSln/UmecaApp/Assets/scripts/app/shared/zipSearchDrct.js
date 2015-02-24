app.directive('zipSearch', function ($http, $timeout) {
    return function (scope, elem, attr) {

        var currentTimeout = null;

        var urlRequest = attr['urlRequest'];

        var ajaxConf = {
            method: 'POST',
            url: urlRequest
        };

        elem.on('keyup change blur', function () {
            if (scope.zipCode == undefined || scope.zipCode.length < 4 || scope.zipCode.length > 5)
                return;

            ajaxConf.params = {zipCode : scope.zipCode};

            if (currentTimeout) {
                $timeout.cancel(currentTimeout);
            }

            currentTimeout = $timeout(function() {

               $http(ajaxConf)
                    .success(function (data) {
                        data.data=jQuery.parseJSON(data.data);

                        if (data.data == undefined || data.data.length === 0) {
                            scope.clear();
                            return;
                        }

                        scope.listLocation = data.data;
                        scope.a.location =scope.listLocation[0];
                        scope.a.locationId = scope.a.location.id;

                    });
            }, 200);
        });
    };
});