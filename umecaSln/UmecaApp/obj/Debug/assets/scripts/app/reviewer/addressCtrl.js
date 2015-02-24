app.controller('addressController', function($scope, $timeout, $http,$rootScope) {
    $scope.a = {};
    $scope.listLocation = [];
    $scope.lstHomeType = [];
    $scope.listType = [];
    $scope.a.homeType = 0;
    $scope.a.type=0;
    $scope.content = "Address";
    $scope.nameAddress = "address.";

    $scope.init = function(){
        if($scope.listType === undefined || $scope.listType.length <= 0)
            return;

        if($scope.a.typeId === undefined){
            $scope.a.type = $scope.listType[0];
            $scope.a.typeId = $scope.a.type.id;
        }
        else{
            for(var i=0; i < $scope.listType.length; i++){
                var type = $scope.listType[i];

                if(type.id === $scope.a.typeId){
                    $scope.a.type =type;
                    break;
                }
            }
        }


        if($scope.lstHomeType === undefined || $scope.lstHomeType.length <= 0)
            return;

        if($scope.a.homeTypeId === undefined){
            $scope.a.homeType = $scope.lstHomeType[0];
            $scope.a.homeTypeId = $scope.a.homeType.id;
        }
        else{
            for(var i=0; i < $scope.lstHomeType.length; i++){
                var bel = $scope.lstHomeType[i];

                if(bel.id === $scope.a.homeTypeId){
                    $scope.a.homeType = bel;
                    break;
                }
            }
        }

    };

    $scope.showChoicesSection = function(idSection, idList, idSource, sectionName, listView){
        var arg = [idSection, idList, idSource, sectionName, listView]
        $rootScope.$broadcast('ShowChoicesBySection',arg);
    };

    $timeout(function() {
        $scope.init();
    }, 0);
    $scope.WaitFor = false;
    $scope.MsgError = "";
    $scope.Model = {};

    $scope.submit = function (formId, urlToPost, hasReturnId) {
         if ($(formId).valid() == false) {
            $scope.Invalid = true;

            return false;
        }
        $scope.WaitFor = true;

        if (hasReturnId === true) {
            $.post(urlToPost, $(formId).serialize())
                .success($scope.handleSuccessWithId)
                .error($scope.handleError);
        }
        else {
            $.post(urlToPost, $(formId).serialize())
                .success($scope.handleSuccess)
                .error($scope.handleError);
        }
        return true;
    };

    $scope.handleSuccessWithId = function (resp) {
        $scope.WaitFor = false;

        try {
            if(resp.hasError===undefined){
                resp=resp.responseMessage;
            }
            if (resp.hasError === false) {
                $rootScope.$broadcast("onLastId", resp.Id);
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


    $scope.handleSuccess = function (resp) {
        $scope.WaitFor = false;

        try {
            if(resp.hasError===undefined){
                resp=resp.responseMessage;}
            if (resp.hasError === false) {
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
        $scope.Model.dlg.modal('hide');
        $scope.Model.def.reject({ isCancel: true });
    };

    $scope.setDlg = function (dlg, urlToSubmit) {
        $scope.Model.dlg = dlg;
        $scope.Model.url = urlToSubmit;

        dlg.on('hidden.bs.modal', function () {
            dlg.data('modal', null);
            dlg.replaceWith("");
        });
    };
});