app.controller('upsertScheduleVerifController', function($scope, $timeout, $q,$rootScope, $sce) {
    $scope.WaitFor = false;
    $scope.MsgError = "";
    $scope.Model = {};
    $scope.verification = false;
    $scope.generalComponent = false;
    $scope.fmsOfSource;
    $scope.countAuxSch = 0;

    $scope.findSourceBefore = function () {
        if ($scope.countAuxSch == 2) {
            $scope.fmsOfSource = $sce.trustAsHtml("Buscando informaci&oacute;n...");
            $scope.countAuxSch = 0;
            var data = {};
            data.idCase = $scope.idCase;
            data.idSource = $scope.idSource;
            data.idList = $scope.idList;
            data.code = $scope.code;
            $.post($scope.urlSearchInformation, data)
                .success($scope.handleSuccessFindPrevious)
                .error($scope.handleErrorFindPrevious);
        }
    };

    $scope.handleSuccessFindPrevious = function (resp) {
        $scope.fmsOfSource = $sce.trustAsHtml(resp.responseMessage.message);
        $scope.$apply();
    };

    $scope.handleErrorFindPrevious = function (resp) {
        $scope.fmsOfSource = $sce.trustAsHtml("No se ha podido obtener la informaci&oacute;n de la fuente");
        $scope.$apply();
    };


    $scope.init = function(){

    };


    $timeout(function() {
        $scope.init();
    }, 0);

    $scope.enableProperties = function () {

        $("input:text").each(function () {
            $(this).prop('disabled', false);
        });
        $("textarea").each(function () {
            $(this).prop('disabled', false);
        });
        $("select").each(function () {
            $(this).prop('disabled', false);
        });
        $("input:radio").each(function () {
            $(this).removeAttr('disabled');
        });
    };

    $scope.disableProperties = function () {
        $("input:text").each(function () {
            $(this).attr('disabled', 'disabled');
        });
        $("select").each(function () {
            $(this).prop("disabled", true);
            ;
        });
        $("textarea").each(function () {
            $(this).attr('disabled', 'disabled');
        });
        $("input:radio").each(function () {
            $(this).attr('disabled', 'disabled');
        });
    };
    $scope.submit = function (formId) {
        if ($scope.schString == "" || $scope.schString == undefined) {
            $scope.MsgError = "Debe agregar al menos una disponibilidad";
            return false;
        }

        $scope.WaitFor = true;
        if($scope.idList==undefined){
            $scope.idList = "";
        }
        var formSerialize = $(formId).serialize();
        var content = formSerialize + "&&idCase=" + $scope.idCase + "&&idSource=" + $scope.idSource+"&&schedule="+$scope.schString+"&&code="+$scope.code+"&&idList="+$scope.idList;
        $scope.WaitFor = true;
        $.post($scope.urlToGoSaveSchedule, content)
            .success($scope.handleSuccess)
            .error($scope.handleError);
        return true;
    };


    $rootScope.$on('SetIdListSchedule', function (event,idList) {
        $scope.idList=idList;
        $scope.countAuxSch++;
        $scope.findSourceBefore();
    });

    $rootScope.$on('SetCodeSchedule', function (event,code) {
        $scope.code=code;
        $scope.countAuxSch++;
        $scope.findSourceBefore();
    });
    $scope.handleSuccess = function (resp) {
        $scope.WaitFor = false;
        $scope.MsgError = "";

        try {

            if (resp.hasError === undefined) {
                resp = resp.responseMessage;
            }

            if (resp.hasError === false) {
                $scope.disableProperties();
                $scope.Model.dlg.modal('hide');
                $scope.Model.def.resolve({ isCancel: false });
                return;
            }

            $scope.MsgError = resp.message;
            $scope.$apply();

        } catch (e) {
            $scope.MsgError = "Error inesperado de datos. Por favor intente más tarde.";
        }
    };

    $scope.handleError = function () {
        $scope.WaitFor = false;
        $scope.MsgError = "Error de red. Por favor intente más tarde.";
        $scope.$apply();
    };

    $rootScope.$on('MatchScheduleList', function (event,list) {
        $scope.schString=list;
    });

    $scope.cancel = function () {
        $scope.MsgError = "";
        $scope.verification = true;
        $scope.disableProperties();
        if ($scope.Model.dlg != undefined) {
            $scope.Model.dlg.modal('hide');
        }
        if ($scope.Model.def != undefined) {
            $scope.Model.def.reject({ isCancel: true });
        }
    };

    $scope.setDlg = function (dlg, urlToSubmit) {
        if ($scope.Model == undefined || $scope.Model.dlg == undefined) {
            $scope.Model.dlg = dlg;
            $scope.Model.url = urlToSubmit;
        }
    };
});

app.controller('innerScheduleController', function($scope, $rootScope, $timeout, $q) {

    $scope.init = function(){
        $('.tp').each(function(){
            $(this).timepicker({
                minuteStep: 1,
                showSeconds: false,
                showMeridian: false
            }).next().on(ace.click_event, function(){
                    $(this).prev().focus();
                });
        });
        $rootScope.$broadcast('CleanScheduleList');
    };

    $timeout(function() {
        $scope.init();
    }, 0);


});