app.controller('modalDlgController', function ($scope, $q, sharedSvc) {

    $scope.showInnScope = function(data, urlToGo, divToAppendId, dlgUpsertId) {
        return $scope.show(data, urlToGo, divToAppendId, dlgUpsertId, true);
    };

    $scope.show = function (data, urlToGo, divToAppendId, dlgUpsertId, innerScp) {
        if (innerScp === true) { $scope.working = true; } else { $scope.$apply(function () { $scope.working = true; }); }
        var def = $q.defer();
        divToAppendId = divToAppendId || "#dlgUpsert";
        dlgUpsertId = dlgUpsertId || "#dlgUpModalId";

        var settings = {
            dataType: "html",
            type: "POST",
            url: urlToGo,
            data: data,
            success: function (resp) {
                $(divToAppendId).empty().append(resp);
                if(dlgUpsertId != undefined){
                    var scope = angular.element($(dlgUpsertId)).scope();
                    scope.Model.def = def;
                    scope.$apply();
                }
                $scope.$apply(function () { $scope.working = false; });
            },
            error: function() {
                sharedSvc.showMsg(
                    {
                        title: "Error de red",
                        message: "<strong>No fue posible conectarse al servidor</strong> <br/><br/>Por favor intente más tarde",
                        type: "danger"
                    }).then(function () { def.reject({ isError: true }); });
                $scope.$apply(function () { $scope.working = false; });
            }
        };
        $.ajax(settings);
        return def.promise;
    };

    $scope.doConfirm = function (data, urlToGo) {
        var def = $q.defer();
        sharedSvc.showConf({ title: "Confirmación de Servicio", message: "¿Está seguro que desea de confirmar el uso del servicio?", type: "warning" }).
            then(function () {
                $scope.doPost(data, urlToGo, def);
            }, def.reject);
        return def.promise;
    };


    $scope.doConfirmFull = function (data, urlToGo, title, message, type, choiceA) {
        var def = $q.defer();
        sharedSvc.showConf({ title: title, message: message, type: type, choiceA: choiceA }).
            then(function (res) {
                var dataToSend = data;
                if(choiceA !== undefined){
                    try{
                        dataToSend = choiceA.prepareData(data, res);
                    }
                    catch(e){
                        dataToSend = data;
                    }
                }
                $scope.doPost(dataToSend, urlToGo, def);
            }, def.reject);
        return def.promise;
    };
    
    $scope.doCancelDocument = function (data, urlToGo, folio) {
        var def = $q.defer();
        sharedSvc.showConf({ title: "Confirmación de cancelación de documento", message: "¿Está seguro que desea cancelar el documento con folio "+folio+"?", type: "warning" }).
            then(function () {
                $scope.doPost(data, urlToGo, def);
            }, def.reject);
        return def.promise;
    };

    $scope.doObsolete = function(data, urlToGo) {
        var def = $q.defer();
        sharedSvc.showConf({ title: "Eliminar registro", message: "¿Está seguro de que desea eliminar el registro?", type: "danger" }).
            then(function() {
                $scope.doPost(data, urlToGo, def);
            }, def.reject);
        return def.promise;
    };


    $scope.doAction = function(data, urlToGo, title, message, type) {
        if(type == undefined)
            type = "danger";

        var def = $q.defer();
        sharedSvc.showConf({ title: title, message: message, type: type }).
            then(function() {
                $scope.doPost(data, urlToGo, def);
            }, def.reject);
        return def.promise;
    };

    $scope.doPost = function (data, urlToGo, def) {
        var settings = {
            dataType: "json",
            type: "POST",
            url: urlToGo,
            data: data,
            success: function (resp) {
                if (resp.hasError === true) {
                    sharedSvc.showMsg(
                        {
                            title: resp.title,
                            message: resp.message,
                            type: "danger"
                        }).then(function () { def.reject({ isError: true }); });
                }
                else {
                    def.resolve();
                }
            },
            error: function () {
                sharedSvc.showMsg(
                    {
                        title: "Error de red",
                        message: "<strong>No fue posible conectarse al servidor</strong> <br/><br/>Por favor intente más tarde",
                        type: "danger"
                    }).then(function () { def.reject({ isError: true }); });
            }
        };

        $.ajax(settings);
    };

});

