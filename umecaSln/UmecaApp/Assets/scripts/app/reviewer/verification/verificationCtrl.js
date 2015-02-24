app.controller('verificationController', function ($scope, $timeout, $q, sharedSvc) {
    $scope.def = $q.defer();
    $scope.verification = true;
    $scope.init = function () {
        if ($scope.managereval != undefined && $scope.managereval == true) {
            $scope.verification = false;
            $scope.showSchedule = true;
        }
        $(".icon-ok-circle").each(function () {
            $(this).css("cursor", "pointer");
            $(this).attr("rel", "tooltip");
            $(this).attr("title", "Coincide la información");
        });
        $(".icon-remove-circle").each(function () {
            $(this).css("cursor", "pointer");
            $(this).attr("rel", "tooltip");
            $(this).attr("title", "No coincide la información");
        });

        $(".icon-ban-circle").each(function () {
            $(this).css("cursor", "pointer");
            $(this).attr("rel", "tooltip");
            $(this).attr("title", "No conoce la información");
        });
        $("input:text").each(function () {
            $(this).attr('disabled', 'disabled');
        });
        $("input:radio").each(function () {
            $(this).attr('disabled', 'disabled');
        });
        $("select").each(function () {
            $(this).prop("disabled", true);
            ;
        });
        $("textarea").each(function () {
            $(this).attr('disabled', 'disabled');
        });
    };


    $timeout(function () {
        $scope.init();
    }, 0);


    $scope.doConfirmVerifEqual = function (code, id) {
        var data = {
            code: code,
            idCase: $scope.idCase,
            idSource: $scope.idSource
        };
        if (id != undefined) {
            data.idList = id;
        }
        var urlToGo = $scope.urlVerifTrue;
        var def = $q.defer();
        sharedSvc.showConf({ title: "Confirmación de verificación", message: "Establecer que la respuesta de la fuente es igual a lo proporcionado por el imputado", type: "success" }).
            then(function () {
                $scope.doPost(data, urlToGo, def);
            }, def.reject);
        return def.promise;
    };

    $scope.doConfirmVerifNotKnow = function (code, id) {
        var data = {
            code: code,
            idCase: $scope.idCase,
            idSource: $scope.idSource
        };
        if (id != undefined) {
            data.idList = id;
        }
        var urlToGo = $scope.urlVerifNotKnow;
        var def = $q.defer();
        sharedSvc.showConf({ title: "Confirmación de verificación", message: "Establecer que la fuente no conoce la información proporcionada por el imputado", type: "inverse" }).
            then(function () {
                $scope.doPost(data, urlToGo, def);
            }, def.reject);
        return def.promise;
    };

    $scope.terminateMeetingSource = function (urlTerminate) {
        var data = {
            idCase: $scope.idCase,
            idSource: $scope.idSource
        };
        var urlToGo = urlTerminate;
        var def = $q.defer();
        sharedSvc.showConf({ title: "Confirmación para terminar entrevista", message: "Esta seguro que desea terminar la entrevista con la fuente.<br/><b> Una vez terminada la entrevista no podrá modificar la información proporcionada.</b>", type: "primary" }).
            then(function () {
                $scope.doPost(data, urlToGo, def, true);
            }, def.reject);
        return def.promise;
    };

    $scope.doPost = function (data, urlToGo, def, redirect) {
        var settings = {
            dataType: "json",
            type: "POST",
            url: urlToGo,
            data: data,
            success: function (resp) {
                if (resp.hasError === undefined) {
                    resp = resp.responseMessage;
                }
                if (resp.hasError === true) {
                    sharedSvc.showMsg(
                        {
                            title: resp.title,
                            message: resp.message,
                            type: "danger"
                        }).then(function () {
                            def.reject({ isError: true });
                        });
                }
                else {
                    def.resolve();
                    if (redirect != undefined && redirect == true) {
                        window.cancelMeetingSource();
                    }
                }
            },
            error: function () {
                sharedSvc.showMsg(
                    {
                        title: "Error de red",
                        message: "<strong>No fue posible conectarse al servidor</strong> <br/><br/>Por favor intente más tarde",
                        type: "danger"
                    }).then(function () {
                        def.reject({ isError: true });
                    });
            }
        };

        $.ajax(settings);
    };

    $scope.pastToJson = function (string) {
        var result = jQuery.parseJSON(string);
        if (result == "") {
            return  "[]";
        } else {
            return result;
        }
    };

});

app.controller('innerVerificationController', function ($scope, $timeout, $http) {
    $scope.init = function () {


        $('.date-picker').datepicker({autoclose: true, endDate: new Date()}).next().on(ace.click_event, function () {
            $(this).prev().focus();
        });
        if ($scope.generalComponent) {
            $scope.MsgError = "";
            var allSelectElements = $("select");
            $("#divElementVerif").find(allSelectElements).each(function () {
                var nameModel = $(this).attr("ng-model");
                var nameModelComplete = nameModel.split(".");
                var nameModelLast = nameModelComplete[nameModelComplete.length - 1];
                var toUpperName = nameModelLast.charAt(0).toUpperCase() + nameModelLast.substring(1);

                if (toUpperName == "Degree") {
                    for(var j=0; j<$scope.lstLevel.length; j++){
                        $scope.lstDegree = $scope.lstLevel[j].degrees;
                        for(var i= 0; i< $scope.lstDegree.length; i++){
                            if($(this).attr("value")==$scope.lstDegree[i].id){
                                $scope.school.level = $scope.lstLevel[j];
                                $scope.school.levelId = $scope.school.level.id;
                                $scope.school.degree = $scope.lstDegree[i];
                                $scope.school.degreeId = $scope.lstDegree[i].id;
                            }
                        }
                    }
                } else {
                    var nameList = "list" + toUpperName;
                    if ($scope[nameList] == undefined) {
                        nameList = "lst" + toUpperName;
                        if ($scope[nameList] == undefined) {
                            nameList = "listElection";
                        }
                    }
                    if ($scope[nameModelComplete[0]] == undefined) {
                        $scope[nameModelComplete[0]] = {};
                    }
                    if ($scope[nameList] != undefined && $scope[nameList][0] != undefined) {
                        if($(this).attr("value")!= undefined){
                        for(var i = 0; i<$scope[nameList].length; i++){
                            if($(this).attr("value") == $scope[nameList][i].id){
                                $scope[nameModelComplete[0]][nameModelComplete[1]] = $scope[nameList][i];
                                $scope[nameModelComplete[0]][nameModelComplete[1] + "Id"] = $scope[nameList][i].id;
                            }
                        }
                        }else{
                            $scope[nameModelComplete[0]][nameModelComplete[1]] = $scope[nameList][0];
                            $scope[nameModelComplete[0]][nameModelComplete[1] + "Id"] = $scope[nameList][0].id;
                        }
                    }
                }
            });
            var divTypeImputedHome = $(".removeClassHide")
            $("#divElementVerif").find(divTypeImputedHome).each(function () {
                $(this).removeClass("ng-hide");
            });
        }
        var allInput = $("input:text");
        var allTextArea = $("textarea");
        $("#divElementVerif").find(allInput).each(function () {
            if( $(this).val() == ""){
                var nameModel = $(this).attr("ng-model");
                var sections = nameModel.split(".");
                var actual = $scope;
                for(var i=0; i<sections.length; i++){
                    if(i < (sections.length -1) && actual[sections[i]] == undefined){
                        actual[sections[i]] = {};
                    }else if(i == (sections.length -1)){
                        actual[sections[i]]=$(this).attr("value");
                    }
                    actual = actual[sections[i]];
                }
            }
        });
        $("#divElementVerif").find(allTextArea).each(function () {
            if( $(this).val() == ""){
                var nameModel = $(this).attr("ng-model");
                var sections = nameModel.split(".");
                var actual = $scope;
                for(var i=0; i<sections.length; i++){
                    if(i < (sections.length -1) && actual[sections[i]] == undefined){
                        actual[sections[i]] = {};
                    }else if(i == (sections.length -1)){
                        actual[sections[i]]=$(this).attr("value");
                    }
                    actual = actual[sections[i]];
                }
            }
        });
        if($scope.locationId != undefined){
            $scope.setState();
        }
    };

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

    $scope.setState = function () {
        if ($scope.stateId == undefined) {
            if ($scope.listState != undefined && $scope.listState.length > 0) {
                $scope.state = $scope.listState[0];
                $scope.stateId = $scope.state.id;
            }
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
                    if ($scope.listMunicipality.length > 0) {
                        $scope.municipality = $scope.listMunicipality[0];
                        $scope.municipalityId = $scope.municipality.id;
                    }
                    $scope.setLocation();
                });
        }
    };
    $timeout(function () {
        $scope.init();
    }, 0);


});


