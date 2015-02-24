app.controller('verificationAddressController', function($scope, $timeout, $q, $rootScope, $sce) {
    $scope.WaitFor = false;
    $scope.MsgError = "";
    $scope.Model = {};
    $scope.verification = false;
    $scope.generalComponent = false;
    $scope.fmsOfSource;
    $scope.addressId=0;
    $scope.countAuxAd = 0;

    $scope.findSourceBefore = function () {
        if ($scope.countAuxAd == 2) {
            $scope.fmsOfSource = $sce.trustAsHtml("Buscando informaci&oacute;n...");
            $scope.countAuxAd = 0;
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

    $scope.fillModel = function(){
            var data = {};
        if($scope.addressId != undefined && $scope.addressId !=0){
            data.idList = $scope.addressId;
            $.post($scope.urlFillModelAddress, data)
                .success($scope.fillModelSuccess)
                .error($scope.fillModelError);
        }

    };

    $scope.fillModelSuccess= function (data){
        var addressModel =data.responseMessage.message;
        $rootScope.$broadcast("setAddress", addressModel);
    };

    $scope.fillModelError = function (){
        $scope.fmsOfSource = $sce.trustAsHtml("No se ha podido cargar los datos del imputado");
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
//        $rootScope.$broadcast("resizeMap");
      };

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

    $timeout(function() {
        $scope.init();
    }, 0);


    $rootScope.$on('SetIdListAddress', function (event,idList) {
        $scope.countAuxAd++;
        $scope.findSourceBefore();
        $scope.idList=idList;
    });

     $rootScope.$on('SetIdAddressVerif', function (event,idAddress) {
        $scope.addressId = idAddress;
         $scope.fillModel();
    });

    $rootScope.$on('SetCodeAddress', function (event,code) {
        $scope.code=code;
        $scope.countAuxAd++;
        $scope.findSourceBefore();
    });

    $scope.submit = function (formId) {
        if ($(formId).valid() == false) {
            $scope.Invalid = true;
            return false;
        }

        $scope.WaitFor = true;

        var formSerialize = $(formId).serialize();
        var content = formSerialize + "&&idCase=" + $scope.idCase + "&&idSource=" + $scope.idSource + "&&code="+ $scope.code+"&&idList="+$scope.idList;
        $scope.WaitFor = true;
        $.post($scope.urlToGoSaveAddress, content)
            .success($scope.handleSuccess)
            .error($scope.handleError);
        return true;
    };


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
