app.controller('tecRevController', function ($scope, $timeout, $sce) {

    $scope.m = {};
    $scope.m.isEval = false;
    $scope.m.totRisk;
    $scope.m.actTab = 0;
    $scope.m.secIdx = 0;
    $scope.m.extras = [];
    $scope.sectionList = [];
    $scope.subSectionList = [];
    $scope.lstTot = [];
    $scope.lstSubtotSrv = [];
    $scope.lstQuestSel = [];
    $scope.lstSelValidator = [];
    $scope.MsgErrorLst = [];
    $scope.MsgSuccessTecRev = "";
    $scope.MsgSectionErrorLst = [];


    $scope.submitTecRev = function (formId, urlToPost, validate) {

        $scope.Invalid = true;
        $scope.isFinished = false;

        if ($(formId).valid() == false) {
            $scope.Invalid = true;
            return false;
        }

        if (validate != undefined) {
            if (validate() == false) {
                $scope.Invalid = true;
                return false;
            } else {
                $scope.isFinished = true;
                $scope.$apply();
            }
        }

        $scope.WaitFor = true;
        $.post(urlToPost, $(formId).serialize())
            .success($scope.handleSuccessTR)
            .error($scope.handleErrorTR);
        return true;
    };

    $scope.handleSuccessTR = function (resp) {
        $scope.WaitFor = false;

        try {

            if (resp.hasError === undefined) {
                resp = resp.responseMessage;
            }

            if (resp.hasError === false) {
                if (resp.urlToGo && resp.urlToGo != "") {
                    window.goToUrlMvcUrl(resp.urlToGo, "");
                } else {
                    $scope.MsgError = $sce.trustAsHtml("");
                    $scope.MsgSuccessTecRev = $sce.trustAsHtml(resp.message);
                }
            } else {
                $scope.MsgError = $sce.trustAsHtml(resp.message);
            }

            $scope.$apply();
            return;
        }
        catch
            (e) {
            $scope.MsgSuccessTecRev = $sce.trustAsHtml("");
            $scope.MsgError = $sce.trustAsHtml("Error inesperado de datos. Por favor intente más tarde.");
        }
    }
    ;

    $scope.handleErrorTR = function () {
        $scope.WaitFor = false;
        $scope.MsgError = $sce.trustAsHtml("Error de red. Por favor intente más tarde.");
        $scope.$apply();
    };


    $scope.toObject = function (json) {
        return JSON.parse(json);
    }

    $scope.findQuestion = function (id) {
        for (var i = 0; i < $scope.lstQuestSel.length; i++) {
            if ($scope.lstQuestSel[i] == id)
                return i;
        }
        return null;
    }

    $scope.delRowVal = function (totId) {

        var indx;

        for (var i = 0; i < $scope.lstSelValidator.length; i++) {
            if ($scope.lstSelValidator[i] == totId) {
                indx = i;
                break;
            }
        }

        $scope.lstSelValidator.splice(indx, 1);
    };

    $scope.changeVal = function (type, totId, questionId, value) {

        var idx = null;
        idx = $scope.findQuestion(questionId);

        if (type == "checkbox") {

            if (idx != null) { //si ya lo habia seleccionado lo quito y resto el valor a su respectivo total
                $scope.lstQuestSel.splice(idx, 1);
                $scope.lstTot[totId] = parseInt($scope.lstTot[totId]) - parseInt(value);
                $scope.delRowVal(totId);
            } else {
                $scope.lstQuestSel.push(questionId);
                $scope.lstTot[totId] = parseInt($scope.lstTot[totId]) + parseInt(value);
                $scope.lstSelValidator.push(totId);
            }
        }
        else if (type == "radio") {

            if (idx == null) { //busco al conjunto al que pertenece y elimino las preguntas seleccionadas de esa seccion y agrego la nueva seleccionada

                var questByTotId = $scope.getQuestionsByTotId(totId);

                for (var d = 0; d < questByTotId.length; d++) {

                    for (var i = 0; i < $scope.lstQuestSel.length; i++) {
                        if ($scope.lstQuestSel[i] == questByTotId[d].questionId) {
                            $scope.lstQuestSel.splice(i, 1);
                            break;
                        }
                    }
                }

                $scope.lstQuestSel.push(questionId);
                $scope.lstSelValidator.push(totId);

            }

            $scope.lstTot[totId] = value;
        }
    };

    $scope.getQuestionsByTotId = function (totId) {

        var questArr = new Array();
        var band = false;

        for (var a = 0; a < $scope.sectionList.length; a++) {
            if (band == true)
                break;

            for (var b = 0; b < $scope.sectionList[a].childs.length; b++) {
                if ($scope.sectionList[a].childs[b].tabId == totId) {
                    questArr = $scope.sectionList[a].childs[b].questions;
                    band = true;
                    break;
                }
            }
        }

        return questArr;
    };

    $scope.validateSelectedSections = function () {

        var finalArr = [];
        $scope.MsgErrorLst = [];

        for (var i = 0; i < $scope.lstSelValidator.length; i++) {
            if (finalArr.indexOf($scope.lstSelValidator[i]) < 0) {
                finalArr.push($scope.lstSelValidator[i]);
            }
        }

        //finalArr contiene las secciones de las cuales se selecciono una pregunta
        //se compara con la lista completa de secciones

        $scope.MsgSectionErrorLst = [];
        for (var i = 0; i < $scope.subSectionList.length; i++) {
            if (finalArr.indexOf($scope.subSectionList[i].code) < 0) {
                var subsectNameArr = $scope.subSectionList[i].code.split("_");
                var obj = {sect: subsectNameArr[0] + "_" + subsectNameArr[1], msg: "Debe contestar la secci&oacute;n " + $scope.subSectionList[i].name + "."};
                $scope.MsgSectionErrorLst.push(obj);
            }
        }


    };

    $scope.sectionHasError = function (sect) {
        for (var a = 0; a < $scope.MsgSectionErrorLst.length; a++) {
            if ($scope.MsgSectionErrorLst[a].sect == sect) {
                return true;
                break;
            }
        }
        return false;
    };

//    $scope.changeZIndex = function (elementClick) {
//        for (var a = 0; a < $scope.sectionList.length; a++) {
//            $("#li" + $scope.sectionList[a].tabId).css("z-index", "0");
//        }
//        $("#li" + elementClick).css("z-index", "1");
//    };

    $scope.showMessageError = function(elementClick){
        $("#divErrorMessage").show();
        var position = $(".tab-content").position();
        $("#divErrorMessage").css("left",position.left+5);
        $("#divErrorMessage").addClass("errorMessageClass");
        $scope.entityError=elementClick;
    };

    $scope.hideMessageError = function(){
        $("#divErrorMessage").hide();
    };

    $scope.getSectionMsg = function (sect) {
        var errMsg = "";
        for (var a = 0; a < $scope.MsgSectionErrorLst.length; a++) {
            if ($scope.MsgSectionErrorLst[a].sect == sect) {
                errMsg += "- " + $scope.MsgSectionErrorLst[a].msg + '<br/>';
            }
        }
        return $sce.trustAsHtml(errMsg);
    };

    $scope.getSubSections = function (section) {

        for (var i = 0; i < section.childs.length; i++) {
            $scope.subSectionList.push({"code": section.childs[i].tabId, "name": section.childs[i].sectionName});
        }

    };

    $scope.calcRisk = function () {

        $scope.validateSelectedSections();

        if ($scope.MsgErrorLst.length > 0 || $scope.MsgSectionErrorLst.length > 0)
            return;

        $scope.totTecRev = 0;

        for (totSec in $scope.lstTot)
            $scope.totTecRev = parseInt($scope.totTecRev) + parseInt($scope.lstTot[totSec]);

        $scope.flgShowRisk = true;

        $scope.genLstSubtotSrv();

    };

    $scope.checkQuest = function (questId) {

        if ($scope.lstQuestSel.indexOf(parseInt(questId)) > -1)
            return true

        return false;
    };

    $scope.validateSave = function () {

        if ($scope.flgShowRisk != true) {
            $scope.MsgErrorLst = [];
            $scope.MsgErrorLst.push("Debe realizar el cálculo del riesgo.");
            return false;
        }

        return true;
    };

    $scope.genLstSubtotSrv = function () {

        for (sect in $scope.lstTot)
            $scope.lstSubtotSrv.push({"subCode": sect, "val": $scope.lstTot[sect]})

    };

    $scope.init = function () {

        for (var i = 0; i < $scope.sectionList.length; i++)
            $scope.getSubSections($scope.sectionList[i]);

        if ($scope.lstSubtotSrv != undefined) {
            for (sect in $scope.lstTot)
                for (var i = 0; i < $scope.lstSubtotSrv.length; i++) {
                    if ($scope.lstSubtotSrv[i].subCode == sect) {
                        $scope.lstTot[sect] = $scope.lstSubtotSrv[i].val;
                    }
                }
        }

    };

    $scope.genLstSelVal = function (totId, questionId) {

        var idx = null;
        idx = $scope.findQuestion(questionId);

        if (idx != null)
            $scope.lstSelValidator.push(totId);

    };

    $scope.returnUrl = function (id) {

        var url;

        switch (id) {
            case 1://regreso para evaluador y managereval
                url = $scope.urlManagereval;
                break;
            case 2://regreso para supervisor y managersup
                url = $scope.urlManagerSup;
                break;
            case 3:
                //todo falta la url para el director
                break;
            default ://regreso por default
                url = $scope.urlTecRev;
                break;
        }

        window.goToUrlMvcUrl(url, "")
    };


    $timeout(function () {
        $scope.init();

        if ($scope.canEdit == true) {
            for (var i = 0; i < $scope.sectionList.length; i++) {//secciones
                for (var j = 0; j < $scope.sectionList[i].childs.length; j++) {//subsecciones
                    for (var k = 0; k < $scope.sectionList[i].childs[j].questions.length; k++) {//preguntas
                        $scope.genLstSelVal($scope.sectionList[i].childs[j].tabId, $scope.sectionList[i].childs[j].questions[k].questionId);
                    }
                }
            }
        }

    }, 0);


})
;