app.controller('hearingFormatController', function ($scope, $timeout, $http, $q, $sce, $rootScope) {

        $scope.m = {};
        $scope.a = {};

        $scope.m.errTime;
        $scope.hasError = false;
        $scope.MsgError;
        $scope.MsgErrorContact = "";
        $scope.lblTerms = "";

        $scope.MsgErrorContact = "";

        $scope.m.labelImpForm = "";

        $scope.chnLblFormImp = function (id) {
            if ($scope.m.hasPrevHF == false) {
                if (id == 1) {
                    $scope.m.labelImpForm = $sce.trustAsHtml("Fecha de imputaci&oacute;n");
                    //$scope.m.impDate = $scope.myFormatDate(new Date());
                    $scope.m.impDate = $scope.m.appointmentDate;
                }
                else if (id == 2) {
                    $scope.m.labelImpForm = $sce.trustAsHtml("Nueva fecha de audiencia");
                    $scope.m.impDate = "";
                }
            }

        };

        $scope.chnExtDate = function (id) {
            $scope.chngVincProcess(1);
            if (id == 1) {
                $scope.m.extDate = $scope.myFormatDate(((new Date()).getTime() + (86400000 * 3)));
            }
            else if (id == 2) {
                $scope.m.extDate = $scope.myFormatDate(((new Date()).getTime() + (86400000 * 6)));
            }
            else if (id == 3) {
                //$scope.m.extDate = "";
                $scope.m.extDate = $scope.m.appointmentDate;
            }

        };

        /***/

        var dlgConfirmAction = $('#divConfirm');

        $scope.showDlg = function () {
            var def = $q.defer();

            $timeout(function () {
                dlgConfirmAction.modal('show');
                dlgConfirmAction.on('hidden.bs.modal', function () {
                    if ($scope.IsOk === true) {

                    }
                    else {

                    }
                });
            }, 1);

            return def.promise;
        }

        $scope.hideDlg = function () {
            dlgConfirmAction.modal('hide');
        }

        $scope.validateSave = function () {

            $scope.hasError = false;

            //$scope.validateInitEnd();
            $scope.validateBthDay();
            if ($scope.hasError == true) {
                $scope.MsgError = "No es posible guardar. Debe proporcionar toda la informaci&aacute;n requerida.";
                return false;
            }
            $scope.validateArrangementSel();
            if ($scope.hasError == true) {
                $scope.MsgError = "No es posible guardar. Debe proporcionar toda la informaci&aacute;n requerida.";
                return false;
            }
            $scope.validateLstContact();

            if ($scope.hasError == true) {
                $scope.MsgError = "No es posible guardar. Debe proporcionar toda la informaci&aacute;n requerida.";
                return false;
            }

            return true;
        };

        $scope.validateBthDay = function () {

            if (!$scope.m.impBthDay) {
                $scope.hasError = true;
                $scope.m.errBth = "Fecha de nacimiento es un campo requerido";
            }
            else {
                $scope.m.errBth = "";
                if ($scope.m.impAge < 18) {
                    $scope.hasError = true;
                    $scope.m.errAge = "No puede registrar a un menor de edad";
                }
                else
                    $scope.m.errAge = "";
            }


        }

        $scope.validateInitEnd = function () {

            if (Date.parse('01/01/2014 ' + $scope.m.initTime) >= Date.parse('01/01/2014 ' + $scope.m.endTime)) {
                $scope.hasError = true;
                $scope.m.errTime = "La hora de termino debe ser mayor a la hora de inicio";
                return;
            }
            else
                $scope.m.errTime = "";

        };

        $scope.addContact = function () {

            if ($scope.validateContact())
                return;

            var jsonRow = {
                "name": $scope.m.contactName,
                "phone": $scope.m.contactPhone,
                "address": $scope.m.contactAddress
            };

            $scope.m.lstContactData.push(jsonRow);

            $scope.m.contactName = "";
            $scope.m.contactPhone = "";
            $scope.m.contactAddress = "";
            $scope.MsgErrorContact = "";

        };

        $scope.validateContact = function () {

            $scope.MsgErrorContact = "";

            if ($scope.m.contactName === "" || $scope.m.contactPhone === "" || $scope.m.contactAddress === "" || !($scope.m.contactName) || !($scope.m.contactPhone) || !($scope.m.contactAddress)) {
                $scope.MsgErrorContact = "Debe proporcionar todos los campos para agregar el contacto."
                return true;
            }

            return false
        };

        $scope.removeContact = function (index) {
            if ($scope.m.canSave == true)
                $scope.m.lstContactData.splice(index, 1);
        };

        $scope.validateLstContact = function () {
            if ($scope.m.lstContactData == undefined || $scope.m.lstContactData.length < 1 && $scope.m.vincProcess == 1) {
                $scope.hasError = true;
                $scope.MsgErrorContact = "Debe registrar al menos un dato de contacto."
                return;
            }
            $scope.hasError = false;
            $scope.MsgErrorContact = "";
        };

        $scope.validateLinkProc = function () {

            if (!$scope.m.vincProcess) {
                $scope.hasError = true;
                $scope.m.errLinkProc = "Debe seleccionar una opci�n";
            }
            else
                $scope.m.errLinkProc = "";

            if ($scope.m.vincProcess == 1) {

                if (!$scope.m.linkageRoom || $scope.m.linkageRoom === "") {
                    $scope.hasError = true;
                    $scope.m.errLnkRoom = "Sala es un campo requerido";
                }
                else {
                    $scope.m.errLnkRoom = "";
                }

                if (!$scope.m.linkageDate || $scope.m.linkageDate === "") {
                    $scope.hasError = true;
                    $scope.m.errLnkDt = "Fecha es un campo requerido";
                }
                else {
                    $scope.m.errLnkDt = "";
                }

                if (!$scope.m.linkageTime || $scope.m.linkageTime === "") {
                    $scope.hasError = true;
                    $scope.m.errLnkTm = "Hora es un campo requerido";
                }
                else {
                    $scope.m.errLnkTm = "";
                }

            }
        };

        $scope.disableView = function (val) {

            if (val) {

                $("#divAudiencia :input").attr("disabled", true);
                $("#divImputado :input").attr("disabled", true);
                $("#divDelitos :input").attr("disabled", true);
                $("#divCtrlDet :input").attr("disabled", true);
                $("#divFormImp :input").attr("disabled", true);
                $("#divExt :input").attr("disabled", true);
                $("#divVincProc :input").attr("disabled", true);
                $("#divMedidas :input").attr("disabled", true);
                $("#divContact :input").attr("disabled", true);
                $("#divCitaUmeca :input").attr("disabled", true);
                $("#divPreviousHearing :input").attr("disabled", true);
            }
            else {
                $("#divAudiencia :input").attr("disabled", false);
                $("#divImputado :input").attr("disabled", false);
                $("#divDelitos :input").attr("disabled", false);
                $("#divCtrlDet :input").attr("disabled", false);

                if ($scope.m.hasPrevHF == true) {
                    $("#idFolder").attr("disabled", true);
                    $("#idJudicial").attr("disabled", true);
                    //$("#divImputado :input").attr("disabled", true);
                    $("#divFormImp :input").attr("disabled", true);
                }
                else
                    $("#divFormImp :input").attr("disabled", false);

                $("#divExt :input").attr("disabled", false);
                $("#divVincProc :input").attr("disabled", false);
                $("#divMedidas :input").attr("disabled", false);
                $("#divContact :input").attr("disabled", false);
            }

        };

        $scope.myFormatDate = function (dateMil) {

            var strDt = "";
            var date;

            if (dateMil && dateMil != "null") {

                date = new Date(dateMil);

                var dd, mm, yyyy;

                dd = date.getDate() < 10 ? '0' + date.getDate() : date.getDate();
                mm = date.getMonth() < 9 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1);
                yyyy = date.getFullYear();

                strDt = yyyy + "/" + mm + "/" + dd;
            }

            return strDt;
        };

        $scope.fillFormat = function (data) {

            $scope.m.canSave = data.canSave;
            $scope.m.canEdit = data.canEdit;
            $scope.m.disableAll = data.disableAll;

            //audiencia
            $scope.m.idCase = data.idCase;
            $scope.m.idFormat = data.idFormat;
            $scope.m.isFinished = data.isFinished;
            $scope.m.hasPrevHF = data.hasPrevHF;
            $scope.m.idFolder = data.idFolder;
            $scope.m.idJudicial = data.idJudicial;
            $scope.m.room = data.room;
            $scope.m.appointmentDate = $scope.myFormatDate(data.appointmentDate);
            if ($scope.m.appointmentDate === "") {
                $scope.m.appointmentDate = $scope.myFormatDate(new Date());
            }
            $scope.m.initTime = data.initTime;
            $scope.m.endTime = data.endTime;
            $scope.m.judgeName = data.judgeName;
            $scope.m.mpName = data.mpName;
            $scope.m.defenderName = data.defenderName;

            //imputado
            $scope.m.imputedName = data.imputedName;
            $scope.m.imputedFLastName = data.imputedFLastName;
            $scope.m.imputedSLastName = data.imputedSLastName;
            $scope.m.impBthDay = $scope.myFormatDate(data.imputedBirthDate);
            $scope.calcAge();
            $scope.m.imputedTel = data.imputedTel;

            $scope.m.crimes = data.crimes;
            $scope.m.additionalData = data.additionalData;
            $scope.m.isView = data.isView;

            //radios
            $scope.m.ctrlDet = data.controlDetention;

            if ($scope.m.isFinished == true) {
                $scope.m.ext = data.extension;
                $scope.m.extDate = $scope.myFormatDate(data.extDate);
            } else {
                $scope.m.extDate = "";
            }

            $scope.m.formImp = data.impForm;

            if ($scope.m.formImp == 1)
                $scope.m.labelImpForm = $sce.trustAsHtml("Fecha de imputaci&oacute;n");
            else if ($scope.m.formImp == 2)
                $scope.m.labelImpForm = $sce.trustAsHtml("Nueva fecha de audiencia");

            $scope.m.impDate = $scope.myFormatDate(data.imputationDate);

            $scope.m.vincProcess = data.vincProcess;
            $scope.m.linkageRoom = data.linkageRoom;
            $scope.m.linkageDate = $scope.myFormatDate(data.linkageDate);
            $scope.m.linkageTime = data.linkageTime;
            $scope.m.arrType = data.arrangementType;
            $scope.m.nationalArrangement = data.nationalArrangement;
            $scope.m.terms = data.terms;
            $scope.m.comments = data.comments;

            $scope.m.userName = data.userName;

            $scope.m.hearingTypeId = data.hearingTypeId;
            $scope.m.imputedPresence = data.imputedPresence;
            $scope.m.hearingTypeSpecification = data.hearingTypeSpecification;
            $scope.m.hearingResult = data.hearingResult;
            //

            $scope.m.umecaDate = $scope.myFormatDate(data.umecaDate);
            $scope.m.umecaTime = data.umecaTime;
            $scope.m.umecaSupervisorId = data.umecaSupervisorId;

            if (data.lstArrangement != undefined)
                $scope.m.lstArrangementShow = $.parseJSON(data.lstArrangement);

            if (data.lstContactData != undefined)
                $scope.m.lstContactData = $.parseJSON(data.lstContactData);
            else
                $scope.m.lstContactData = [];

            $scope.m.previousHearing = data.previousHearing;

            $scope.chgLblTerms();

            $scope.$apply();
        };


        $scope.saveHF = function (formId, urlToPost, validate) {
            if (validate != undefined)
                var stVal = validate();

            $rootScope.$broadcast('valAddCrime');

            if ($(formId).valid() == false || stVal == false) {
                $scope.Invalid = true;
                $scope.MsgError = $sce.trustAsHtml("No es posible terminar. Debe proporcionar toda la informaci&oacute;n requerida.");
                return false;
            }

            if ($scope.m.vincProcess == 2)
                $scope.showDlg();
            else
                $scope.submitReloadHF(formId, urlToPost)

        };


        $scope.submitReloadHF = function (formId, urlToPost, validate) {

            var stVal = true;

            if (validate != undefined)
                stVal = validate();

            if (stVal == false) {
                $scope.Invalid = true;
                return false;
            }

            $scope.WaitFor = true;

            $scope.m.isFinished = true;
            $scope.$apply();


            $.post(urlToPost, $(formId).serialize())
                .success(function (resp) {
                    if (resp.hasError === undefined) {
                        resp = resp.responseMessage;
                    }

                    if (resp.hasError == false) {
                        window.goToUrlMvcUrl(resp.urlToGo);
                    }

                    if (resp.hasError == true) {
                        $scope.MsgError = resp.message;
                        $scope.$apply();
                    }
                })
                .error(function () {
                    $scope.WaitFor = false;
                    $scope.MsgError = "Error de red. Por favor intente m�s tarde.";
                    $scope.$apply();
                });

            return true;
        };


        /**/

        $scope.submitPartiaSaveHF = function (formId, urlToPost) {

            var stVal = true;

            $scope.WaitFor = true;

            $.post(urlToPost, $(formId).serialize())
                .success(function (resp) {
                    if (resp.hasError === undefined) {
                        resp = resp.responseMessage;
                    }

                    if (resp.hasError == true) {
                        //$scope.msgMapRequest=$sce.trustAsHtml("Google Maps no cuenta con cooredenadas para el C&oacute;digo Postal: "+$scope.zipCode);
                        $scope.MsgError = $sce.trustAsHtml(resp.message);
                        $scope.$apply();
                    } else {
                        $scope.WaitFor = false;

                        var listMsg = resp.message.split("|");

                        $scope.m.idFormat = listMsg[0];
                        $scope.MsgError = "";
                        $scope.MsgSuccess = $sce.trustAsHtml(listMsg[1]);

                        $scope.$apply();
                    }
                })
                .error(function () {
                    $scope.WaitFor = false;
                    $scope.MsgError = "Error de red. Por favor intente m�s tarde.";
                    $scope.$apply();
                });

            return true;
        };

        /**/

        $scope.loadArrangements = function () {

            if ($scope.m.arrType == undefined || $scope.m.arrType === '' || $scope.m.disableAll == true) {
                return;
            }
            if ($scope.m.nationalArrangement == undefined || ($scope.m.nationalArrangement === '') || $scope.m.disableAll == true) {
                return;
            }

            $scope.chgLblTerms();

            var currentTimeout = null;
            var urlType = $('#url3').attr("value");

            var ajaxConf;

            ajaxConf = {
                method: "POST",
                params: {national: $scope.m.nationalArrangement, idTipo: $scope.m.arrType},
                dataType: "json",
                contentType: "application/json"
            };

            ajaxConf.url = urlType;

            if (currentTimeout) {
                $timeout.cancel(currentTimeout);
            }

            currentTimeout = $timeout(function () {
                $http(ajaxConf)
                    .success(function (data) {
                        $scope.m.lstArrangementShow = data;
                    });
            }, 200);
        };

        $scope.calcAge = function () {

            //yyyy/mm/dd o en milisegundos
            if ($scope.m.impBthDay != null && $scope.m.impBthDay != "") {

                var arrBth = [];
                arrBth = $scope.m.impBthDay.split('/');

                var dtBthObj;

                if (arrBth.length > 0)
                    dtBthObj = new Date(parseInt(arrBth[0]), parseInt(arrBth[1]) - 1, parseInt(arrBth[2]));
                else
                    dtBthObj = new Date($scope.m.impBthDay);

                var ageDifMs = Date.now() - dtBthObj.getTime();
                var ageDate = new Date(ageDifMs);
                $scope.m.impAge = Math.abs(ageDate.getUTCFullYear() - 1970);

                if ($scope.m.impAge < 18) {
                    $scope.hasError = true;
                    $scope.m.errAge = "No puede registrar a un menor de edad";
                }
                else
                    $scope.m.errAge = "";

                $scope.m.errBth = "";
            }
        };

        $scope.exclusiveSelected = function (idArr) {
            for (var i = 0; i < $scope.m.lstArrangementShow.length; i++) {
                if ($scope.m.lstArrangementShow[i].id != idArr && $scope.m.lstArrangementShow[i].selVal == true && $scope.m.lstArrangementShow[i].isExclusive == true) {
                    return $scope.m.lstArrangementShow[i].id
                }
            }

            return -1;
        };

        $scope.clearOthers = function (idExclusive) {
            for (var i = 0; i < $scope.m.lstArrangementShow.length; i++) {
                if ($scope.m.lstArrangementShow[i].id == idExclusive);
                else {
                    $scope.m.lstArrangementShow[i].selVal = false;
                    $scope.m.lstArrangementShow[i].description = "";
                }
            }
        };

        $scope.selectDefaults = function () {
            for (var i = 0; i < $scope.m.lstArrangementShow.length; i++) {
                if ($scope.m.lstArrangementShow[i].isDefault == true) {
                    $scope.m.lstArrangementShow[i].selVal = true;
                    $scope.m.lstArrangementShow[i].description = "";
                }
            }
        };

        $scope.validateArrangementSel = function (idx, idArr) {

            var noSel = 0;
            var noDesc = 0;

            var idExclusive = -1;

            if ($scope.m.vincProcess != 2) {

                if (!$scope.m.lstArrangementShow || $scope.m.disableAll == true)
                    return;

                if (idArr != undefined)
                    idExclusive = $scope.exclusiveSelected(idArr);

                if (idExclusive > 0) {
                    $scope.clearOthers(idExclusive);
                    $scope.m.errArrmntSel = $sce.trustAsHtml("No puede seleccionar otra medida cautelar. Debe deseleccionar la medida cautelar exclusiva.");
                    return;
                }

                if (idArr != undefined && idx != undefined)
                    if ($scope.m.lstArrangementShow[idx].isExclusive == true) {
                        $scope.clearOthers(idArr);
                    }

                if (idArr != undefined && idx != undefined)
                    if ($scope.m.lstArrangementShow[idx].isExclusive == true && $scope.m.lstArrangementShow[idx].selVal == false) {
                        $scope.selectDefaults();
                    }

                for (var i = 0; i < $scope.m.lstArrangementShow.length; i++) {

                    if ($scope.m.lstArrangementShow[i].selVal == true) {
                        noSel++;
                        if ($scope.m.lstArrangementShow[i].description != "" && $scope.m.lstArrangementShow[i].description != undefined) {
                            noDesc++;
                        }
                    } else {
                        $scope.m.lstArrangementShow[i].description = "";
                    }

                }

                if (noSel < 1) {
                    $scope.hasError = true;
                    $scope.m.errArrmntSel = $sce.trustAsHtml("Debe seleccionar al menos una obligaci&oacute;n procesal");
                    return;
                } else if (noSel > noDesc) {
                    $scope.hasError = true;
                    $scope.m.errArrmntSel = $sce.trustAsHtml("Debe indicar una descripci&oacute;n para cada obligaci&oacute;n procesal seleccionada");
                    return;
                } else {
                    $scope.m.errArrmntSel = $sce.trustAsHtml("");
                }
            }
        };

        $scope.hasContacts = function (id) {

            if (id == 2) {
                $scope.m.contactName = "NO TIENE";
                $scope.m.contactPhone = "00000000";
                $scope.m.contactAddress = "NO TIENE";
            } else {
                $scope.m.contactName = "";
                $scope.m.contactPhone = "";
                $scope.m.contactAddress = "";
            }
        };

        $scope.chngVincProcess = function (id) {
            if (id == 1 && $scope.m.ext == 3) {
                $scope.m.linkageRoom = $scope.m.room;
                $scope.m.linkageDate = $scope.m.appointmentDate;
                $scope.m.linkageTime = $scope.m.initTime;
            } else {
                $scope.m.linkageRoom = "";
                $scope.m.linkageDate = "";
                $scope.m.linkageTime = "";
            }
        };

        $scope.fillSelSupervisor = function () {

            if ($scope.lstSupervisor === undefined || $scope.lstSupervisor.length <= 0)
                return;

            if ($scope.m.umecaSupervisorId === undefined) {
                $scope.m.umecaSupervisor = $scope.lstSupervisor[0];
                $scope.m.umecaSupervisorId = $scope.m.umecaSupervisor.id;
            }
            else {
                for (var i = 0; i < $scope.lstSupervisor.length; i++) {
                    var rel = $scope.lstSupervisor[i];

                    if (rel.id === $scope.m.umecaSupervisorId) {
                        $scope.m.umecaSupervisor = rel;
                        break;
                    }
                }
            }
        };

        $scope.fillSelHearingType = function () {

            if ($scope.m.hasPrevHF == true) {
                if ($scope.lstHearingType === undefined || $scope.lstHearingType.length <= 0)
                    return;

                if ($scope.m.hearingTypeId === undefined) {
                    $scope.m.hearingType = $scope.lstHearingType[0];
                    $scope.m.hearingTypeId = $scope.m.hearingType.id;
                }
                else {
                    for (var i = 0; i < $scope.lstHearingType.length; i++) {
                        var rel = $scope.lstHearingType[i];

                        if (rel.id === $scope.m.hearingTypeId) {
                            $scope.m.hearingType = rel;
                            break;
                        }
                    }
                }
            }
        };

        $scope.lockArrangements = function () {
            if ($scope.m.hearingType && $scope.m.hearingType.lock == true || $scope.m.isView == true) {
                $("#divMedidas :input").attr("disabled", true);
                $("#divMedidasHidden :input").attr("disabled", false);
            }
            else {
                $("#divMedidas :input").attr("disabled", false);
                $("#divMedidasHidden :input").attr("disabled", true);
            }
            $scope.lockDefaultArrangements();
        };

        $scope.lockDefaultArrangements = function () {

            if ($scope.m.lstArrangementShow != undefined) {
                for (var i = 0; i < $scope.m.lstArrangementShow.length; i++) {
                    if ($scope.m.lstArrangementShow[i].isDefault == true)
                        $("#arrangement" + $scope.m.lstArrangementShow[i].id).attr("disabled", true);
                }
            }
        };

        $scope.init = function () {
            $scope.fillFormat($scope.m);
            $scope.fillSelSupervisor();
            $scope.fillSelHearingType();
            $scope.disableView($scope.m.disableAll);
            $scope.lockArrangements();
        };

        $scope.chgLblTerms = function () {
            if ($scope.m.arrType == 2)
                $scope.lblTerms = $sce.trustAsHtml("Plazo");
            else if ($scope.m.arrType == 1)
                $scope.lblTerms = $sce.trustAsHtml("Plazo de investigaci&oacute;n");
        };

        $timeout(function () {
            $scope.init();
        }, 0);

        $scope.returnUrlId = function () {
            var urlRet = $('#urlRet').attr("value");
            window.goToUrlMvcUrl(urlRet);
        }
        ;
    }
)
;