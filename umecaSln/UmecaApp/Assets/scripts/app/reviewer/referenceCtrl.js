app.controller('referenceController', function($scope, $timeout,$rootScope) {
    $scope.r = {};
    $scope.lstRel = [];
    $scope.lstDoc = [];
    $scope.r.rel = 0;
    $scope.r.doc = 0;

    $scope.showChoicesSection = function(idSection, idList, idSource, sectionName, listView){
        var arg = [idSection, idList, idSource, sectionName, listView]
        $rootScope.$broadcast('ShowChoicesBySection',arg);
    };

    $scope.init = function(){
        if($scope.lstRel === undefined || $scope.lstRel.length <= 0)
            return;

        if($scope.r.relId === undefined){
            $scope.r.rel = $scope.lstRel[0];
            $scope.r.relId = $scope.r.rel.id;
        }
        else{
            for(var i=0; i < $scope.lstRel.length; i++){
                var rel = $scope.lstRel[i];

                if(rel.id === $scope.r.relId){
                    $scope.r.rel = rel;
                    break;
                }
            }
        }
        if($scope.lstDoc === undefined || $scope.lstDoc.length <= 0)
            return;

        if($scope.r.docId === undefined){
            $scope.r.doc = $scope.lstDoc[0];
            $scope.r.docId = $scope.r.doc.id;
        }
        else{
            for(var i=0; i < $scope.lstDoc.length; i++){
                var doc = $scope.lstDoc[i];

                if(doc.id === $scope.r.docId){
                    $scope.r.doc = doc;
                    break;
                }
            }
        }


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

    $scope.fillModel = function(){
        var template= "NO TIENE";
        var template2 = "Ninguno";
        //alert("en fill model con blcok "+$scope.block);
        if($scope.blockR === false){
            $scope.r.fullName = template;
            for(var i= 0; i < $scope.lstRel.length ; i++){
                if($scope.lstRel[i].name == template2){
                    $scope.r.rel = $scope.lstRel[i];
                    $scope.r.relId = $scope.lstRel[i].id;
                    break;
                }
            }
            $scope.r.phone = template;
            for(var i= 0; i < $scope.lstDoc.length ; i++){
                if($scope.lstDoc[i].name == template2){
                    $scope.r.doc = $scope.lstDoc[i];
                    $scope.r.docId = $scope.lstDoc[i].id;
                    break;
                }
            }

            $scope.r.age = 0;
            $scope.r.address = template;
            $scope.r.isAccompaniment = false;
        }else{
            $scope.r.fullName = "";
            $scope.r.rel = $scope.lstRel[0];
            $scope.r.relId = $scope.lstRel[0].id;
            $scope.r.phone = "";
            $scope.r.doc = $scope.lstDoc[0];
            $scope.r.docId = $scope.lstDoc[0].id;
            $scope.r.age = "";
            $scope.r.address = "";
            $scope.r.isAccompaniment = false;
        }
    };
});