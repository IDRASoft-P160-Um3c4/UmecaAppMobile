app.controller('upsertVerificationController', function ($scope, $rootScope, $timeout, $sce) {
    $scope.WaitFor = false;
    $scope.MsgError = "";
    $scope.Model = {};
    $scope.verification = false;
    $scope.nameScope = "";
    $scope.generalComponent = false;
    $scope.fmsOfSource;
    $scope.countAux = 0;
    $scope.findSourceBefore = function () {
        if ($scope.countAux == 2) {
            $scope.fmsOfSource = $sce.trustAsHtml("Buscando informaci&oacute;n...");
            $scope.countAux = 0;
            var data = {};
            data.idCase = $scope.idCase;
            data.idSource = $scope.idSource;
            data.idList = $scope.idList;
            data.code = $scope.codeVerif;
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


    $rootScope.$on('SetIdList', function (event, idList) {
        $scope.idList = idList;
        $scope.countAux++;
        $scope.findSourceBefore();
    });

    $rootScope.$on('SetCodeVerif', function (event, codeVerif) {
        $scope.codeVerif = codeVerif;
        $scope.countAux++;
        $scope.findSourceBefore();
    });

    $scope.init = function () {

        $('.date-picker').datepicker({autoclose: true, endDate: new Date()}).next().on(ace.click_event, function () {
            $(this).prev().focus();
        });
        var allInput = $("input:text");
        var allTextArea = $("textarea");
        $("#divElementVerif").find(allInput).each(function () {
            $(this).val("");
        });
        $("#divElementVerif").find(allTextArea).each(function () {
            $(this).val("");
        });
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

    $timeout(function () {
        $scope.init();
    }, 0);

    $scope.validateVerif = function (formSerialize) {
        var hasError = false;
        var submitElement = [];
        var vars = formSerialize.split("&");
        for (var i = 0; i < vars.length; i++) {
            var psEqual = vars[i].split("=");
            var e = {};
            e.name = psEqual[0];
            if (psEqual[0] == "imputedHomes.timeLive1") {
                e.name = "imputedHomes.timeLive";
            }

            e.value = psEqual[1];

            var allElement = $("[name='" + psEqual[0] + "']");
            $("#divElementVerif").find(allElement).each(function () {
                var message = "";
                var adding = true;

                var elementAux = $(this);
                var isHide = false;
                var isParent = false;
                do {
                    isHide = elementAux.hasClass("ng-hide");
                    isParent = (elementAux.attr("id") == "divElementVerif");
                    elementAux = elementAux.parent();
                } while (!isHide && !isParent);
                if (isHide) {
                    adding = false;
                }
                if (e.value == undefined || e.value == "") {
                    if (!isHide && isParent) {
                        message = $(this).attr("data-val-required");
                    }
                } else {
                    var valMax = $(this).attr("data-val-length-max");
                    var valMin = $(this).attr("data-val-length-min");
                    var pattern = $(this).attr("data-val-regex-pattern");
                    if (valMax != undefined && valMin != undefined && ($(this).val().length > parseInt(valMax) || $(this).val().length < parseInt(valMin))) {
                        message = $(this).attr("data-val-length")
                    }
                    if (pattern != undefined && $(this).val().match("^[0-9]+$") == null) {
                        message = $(this).attr("data-val-regex");
                    }
                }
                var spanVal = $(this).siblings("span");
                if (spanVal.hasClass("input-group-addon")) {
                    spanVal = $(this).parent().siblings('span');
                }
                var c = $(this).is("input:text");
                if (message != "") {
                    spanVal.css("display", "block");
                    spanVal.addClass("field-validation-error");
                    spanVal.removeClass("field-validation-valid");
                    spanVal.text(message);
                    hasError = true;
                } else {
                    if ($(this).is("input:text") || $(this).is("textarea")) {
                        spanVal.css("display", "none");
                        spanVal.addClass("field-validation-valid");
                        spanVal.removeClass("field-validation-error");
                    }
                    if (e.value != "") {

                        if ($(this).is("input:radio")) {
                            for (var z = 0; z < submitElement.length; z++) {
                                if (submitElement[z].name == e.name) {
                                    adding = false;
                                }
                            }
                        }
                        if (adding) {
                            submitElement.push(e);
                        }
                    }
                }
            });

        }
        if (hasError)
            return null;
        return submitElement;
    };

    $scope.submit = function (formId) {
        var formSerialize = $(formId).serialize();
        var result = $scope.validateVerif(formSerialize);
        if (result == null) {
            return false;
        }
        if ($scope.idList == undefined) {
            $scope.idList = "";
        }
        if (result[0] != undefined && result[0].name == "imputed.birthCountry.id" && result[0].value != "1") {
            for (var i = 1; i < result.length; i++) {
                if (result[i].name == "imputed.location.id") {
                    result.splice(i, 1);
                }
            }
        }

        for (var x = 0; x < result.length; x++) {
            if (result[x] != undefined)
                result[x].value = result[x].value.replace(/%22/g, "'");
        }

        var content = JSON.stringify(result);
        content = "val=" + content + "&&idCase=" + $scope.idCase + "&&idSource=" + $scope.idSource + "&&idList=" + $scope.idList;
        $scope.WaitFor = true;
        $.post($scope.urlToGoSave, content)
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
                $scope.Model.def.resolve({isCancel: false});
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
            $scope.Model.def.reject({isCancel: true});
        }
    };

    $scope.setDlg = function (dlg, urlToSubmit) {
        if ($scope.Model == undefined || $scope.Model.dlg == undefined) {
            $scope.Model.dlg = dlg;
            $scope.Model.url = urlToSubmit;
        }
    };

});
