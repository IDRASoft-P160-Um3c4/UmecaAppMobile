  app.controller('personalDataController', function ($scope, $timeout, $q, $http, $rootScope, $sce) {
    $scope.m = {};
    $scope.specification = {};
    $scope.lstActivity = [];
    $scope.activityModel = [];
    $scope.activityList = [];
    $scope.phyModel = [];
    $scope.listCountry = [];
    $scope.m.country = 0;
    $scope.lstPhysicalCondition = [];
    $scope.pCSelected = [];
    $scope.relActivities = [];
    $scope.def = $q.defer();


      $scope.setLocation = function () {
          var ajaxConf = {
              method: 'POST',
              url: $scope.urlLocation
          };
          ajaxConf.params = {idMun: $scope.municipalityId};
          $http(ajaxConf)
              .success(function (data) {
                  data.data = jQuery.parseJSON(data.data);
                  if (data.data == undefined || data.data.length === 0) {
                      // $scope.clear();
                      return;
                  }
                  $scope.listLocation = data.data;
                  if ($scope.listLocation.length > 0) {
                      $scope.location = $scope.listLocation[0];
                      $scope.locationId = $scope.location.id;
                      $scope.zipCode = $scope.location.zipCode;
                  }
              });
      };

    $scope.showChoicesSection = function(idSection, idList, idSource, sectionName, listView){
        var arg = [idSection, idList, idSource, sectionName, listView]
        $rootScope.$broadcast('ShowChoicesBySection',arg);
    };

    $scope.init = function () {
        if ($scope.listState != undefined && $scope.listState.length > 0) {
            if($scope.locationId != undefined && $scope.locationId != 0){

                var ajaxConf = {
                    method: 'POST',
                    url: $scope.urlFindIds
                };
                ajaxConf.params = {id: $scope.locationId};
                $http(ajaxConf)
                    .success(function (data) {
                        if(data.hasError == false){
                             var idsLocation = jQuery.parseJSON(data.message);
                             for(var i = 0; i< $scope.listState.length; i++){
                                 var s = $scope.listState[i];
                                 if(s.id == idsLocation.stateId){
                                     $scope.state = s;
                                     $scope.stateId = s.id;
                                     var ajaxConf = {
                                         method: 'POST',
                                         url: $scope.urlMunicipality
                                     };
                                     ajaxConf.params = {idState: $scope.stateId};
                                     $http(ajaxConf)
                                         .success(function (data) {
                                             data.data = jQuery.parseJSON(data.data);
                                             if (data.data == undefined || data.data.length === 0) {
                                                 //  cat={};
                                                 return;
                                             }
                                             $scope.listMunicipality = data.data;
                                             for (var i = 0; i < $scope.listMunicipality.length; i++) {
                                                 if (idsLocation.munId == $scope.listMunicipality[i].id) {
                                                     $scope.municipality = $scope.listMunicipality[i];
                                                     $scope.municipalityId = $scope.municipality.id;
                                                 }
                                             }
                                             ajaxConf = {
                                                 method: 'POST',
                                                 url: $scope.urlLocation
                                             };
                                             ajaxConf.params = {idMun: $scope.municipalityId};
                                             $http(ajaxConf)
                                                 .success(function (data) {
                                                     data.data = jQuery.parseJSON(data.data);
                                                     if (data.data == undefined || data.data.length === 0) {
                                                         // $scope.clear();
                                                         return;
                                                     }
                                                     $scope.listLocation = data.data;
                                                     for (var i = 0; i < $scope.listLocation.length; i++) {
                                                         if (idsLocation.id == $scope.listLocation[i].id) {
                                                             $scope.location = $scope.listLocation[i];
                                                             $scope.locationId = $scope.location.id;
                                                             $scope.zipCode = $scope.location.zipCode;
                                                         }
                                                     }
                                                 });
                                         });
                                 }
                             }
                        }

                        if (data.data == undefined || data.data.length === 0) {
                            //  cat={};
                            return;
                        }
                        $scope.listMunicipality = data.data;
                        $scope.municipality = $scope.listMunicipality[0];
                        $scope.municipalityId = $scope.municipality.id;
                        ajaxConf = {
                            method: 'POST',
                            url: $scope.urlLocation
                        };
                        ajaxConf.params = {idMun: $scope.municipalityId};
                        $http(ajaxConf)
                            .success(function (data) {
                                data.data = jQuery.parseJSON(data.data);
                                if (data.data == undefined || data.data.length === 0) {
                                    return;
                                }
                                $scope.listLocation = data.data;
                                $scope.location = $scope.listLocation[0];
                                $scope.locationId = $scope.location.id;
                                $scope.zipCode = $scope.location.zipCode;
                            });

                    });
            }else{
                $scope.state = $scope.listState[0];
                $scope.stateId = $scope.state.id;
                var ajaxConf = {
                    method: 'POST',
                    url: $scope.urlMunicipality
                };
                ajaxConf.params = {idState: $scope.stateId};
                $http(ajaxConf)
                    .success(function (data) {
                        data.data = jQuery.parseJSON(data.data);
                        if (data.data == undefined || data.data.length === 0) {
                            //  cat={};
                            return;
                        }
                        $scope.listMunicipality = data.data;
                        $scope.municipality = $scope.listMunicipality[0];
                        $scope.municipalityId = $scope.municipality.id;
                        ajaxConf = {
                            method: 'POST',
                            url: $scope.urlLocation
                        };
                        ajaxConf.params = {idMun: $scope.municipalityId};
                        $http(ajaxConf)
                            .success(function (data) {
                                data.data = jQuery.parseJSON(data.data);
                                if (data.data == undefined || data.data.length === 0) {
                                    return;
                                }
                                $scope.listLocation = data.data;
                                $scope.location = $scope.listLocation[0];
                                $scope.locationId = $scope.location.id;
                                $scope.zipCode = $scope.location.zipCode;
                            });

                    });
                }

        }
        $(".chosen-select").chosen();
        var birthDate = $("#birthDate").val();
        $("#birthDate").val(birthDate.replace("00:00:00.0", ""));
        birthDate = $("#dateBirthV").val();
        $("#dateBirthV").val(birthDate.replace("00:00:00.0", ""));

        if ($scope.listCountry === undefined || $scope.listCountry.length <= 0)
            return;

        if ($scope.m.countryId === undefined || $scope.m.countryId === null) {

            for (var i = 0; i < $scope.listCountry.length; i++) {

                if ($scope.listCountry[i].id == 1) {//para seleccionar a mexico por defecto
                    $scope.m.country = $scope.listCountry[i];
                    break;
                }
            }

            $scope.m.countryId = $scope.m.country.id;

        }
        else {
            for (var i = 0; i < $scope.listCountry.length; i++) {
                var country = $scope.listCountry[i];

                if (country.id === $scope.m.countryId) {
                    $scope.m.country = country;
                    break;
                }
            }
        }
    };
    $timeout(function () {
        $scope.init();
    }, 0)

    $scope.selectedActivities = function (lstActivity, lstActivitySelect) {

        for (var i = 0; i < lstActivitySelect.length; i++) {
            for (var j = 0; j < lstActivity.length; j++) {
                if (lstActivity[j].id === lstActivitySelect[i].id) {
                    $scope.activityModel.push(lstActivity[j]);
                    if (lstActivity[j].specification) {
                        $scope.specification[lstActivity[j].name] = lstActivitySelect[i].specification;
                    }
                }
            }
        }
        $scope.matchActivities();
    };

    $scope.matchActivities = function () {
        $scope.relActivities = [];
        for (var i = 0; i < $scope.activityModel.length; i++) {
            var model = {};
            model.activity = {};
            model.activity.id = $scope.activityModel[i].id;
            if ($scope.specification[$scope.activityModel[i].name] != undefined) {
                model.specification = $scope.specification[$scope.activityModel[i].name];
            } else {
                model.specification = "";
            }
            $scope.relActivities.push(model);
        }
        $scope.activities = JSON.stringify($scope.relActivities);
        return true;
    };
});