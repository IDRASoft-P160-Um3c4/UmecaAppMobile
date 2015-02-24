app.directive('zipSearch', function ($http, $timeout) {
    return function (scope, elem, attr) {

        var currentTimeout = null;

        var urlRequest = attr['urlRequest'];

        var ajaxConf = {
            method: 'POST',
            url: urlRequest
        };

        elem.on('keyup change blur', function () {
            if (scope.zipCode == undefined || scope.zipCode.length != 5)
                return;

            ajaxConf.params = {zipCode : scope.zipCode};

            if (currentTimeout) {
                $timeout.cancel( currentTimeout);
            }

            currentTimeout = $timeout(function() {
                $http(ajaxConf)
                    .success(function (data) {
                        data.data=jQuery.parseJSON(data.data);
                        if (data.data == undefined || data.data.length === 0) {
                           scope.clear();
                            return;
                        }
                        scope.msgZipCode="";
                        var firstLocation = data.data[0];
                        for(var i=0; i<scope.listState.length; i++){
                            if(firstLocation.stateId == scope.listState[i].id){
                                scope.state = scope.listState[i];
                                scope.stateId = scope.state.id;
                            }
                        }
                            var ajaxConf = {
                                method: 'POST',
                                url: scope.urlMunicipality
                            };
                            ajaxConf.params = {idState : scope.stateId};
                            $http(ajaxConf)
                                .success(function (data) {
                                    data.data=jQuery.parseJSON(data.data);
                                    if (data.data == undefined || data.data.length === 0) {
                                        //  cat={};
                                        return;
                                    }
                                    scope.listMunicipality = data.data;
                                    for(var i=0; i<scope.listMunicipality.length; i++){
                                        if(firstLocation.munId == scope.listMunicipality[i].id){
                                            scope.municipality = scope.listMunicipality[i];
                                            scope.municipalityId = scope.municipality.id;
                                        }
                                    }
                                    ajaxConf = {
                                        method: 'POST',
                                        url: scope.urlLocation
                                    };
                                    ajaxConf.params = {idMun : scope.municipalityId};
                                    $http(ajaxConf)
                                        .success(function (data) {
                                            data.data=jQuery.parseJSON(data.data);
                                            if (data.data == undefined || data.data.length === 0) {
                                                // scope.clear();
                                                return;
                                            }
                                            scope.listLocation.concat(data.data);
                                            for(var i =0 ; i<scope.listLocation.length;i++){
                                                if(firstLocation.id == scope.listLocation[i].id){
                                                    scope.location =scope.listLocation[i];
                                                    scope.locationId = scope.location.id;
                                                    scope.zipCode = scope.location.zipCode;
                                                    scope.refreshMap();
                                                }
                                            }
                                        });
                                });

                            scope.listLocation = data.data;
                            scope.location =scope.listLocation[0];
                            scope.locationId = scope.location.id;
                    });

            }, 200);
        });
    };
});