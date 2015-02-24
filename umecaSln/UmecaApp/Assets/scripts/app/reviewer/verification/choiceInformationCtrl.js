app.controller('choiceInformationController', function ($scope, $timeout, $q, sharedSvc, $sce, $rootScope) {
    $scope.def = $q.defer();
    $scope.selectSource = true;
    $scope.TypeV = "";
    $scope.MessageV = "";
    $scope.TitleV = "";
    $scope.init = function () {
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
        $("#commentVerifSection").removeAttr("disabled");
    };


    $timeout(function () {
        $scope.init();
    }, 0);

    $scope.showChoices = function (code, id) {
        window.showChoices(code, id);
    };

    $rootScope.$on('ShowChoicesBySection', function (x, arg) {
        var idSection, idList, idSource, sectionName, listView;
        $scope.idSection = arg[0];
        $scope.idList = arg[1]
        $scope.idSource = arg[2]
        sectionName = arg[3];
        listView = arg[4];
        if (listView == undefined) {
            listView = "";
        }
        $scope.commentVerifSection = "";
        $scope.TypeV = ($scope.idSource == -1 ) ? "inverse" : "purple";
        $scope.TitleV = $sce.trustAsHtml("Elecci&oacute;n de informaci&oacute;n para la secci&oacute;n de " + sectionName + " " + listView);
        var fuente = ($scope.idSource == -1) ? " como: <b>\"No se puede verificar la informaci&oacute;n\"<b/>" : " con <b>LA INFORMACI&Oacute;N QUE PROPORCION&Oacute; EL IMPUTADO.</b>";
        $scope.MessageV = $sce.trustAsHtml("Est&aacute; por establecer la secci&oacute;n de " + sectionName + " " + listView + fuente);
        $("#VerifBySectionDialog").modal("show");
    });


    $scope.sendVerifSection = function (urlToPost, idCase) {
        $scope.WaitForChoice=true;
        var data = {};
        data.idCase = idCase;
        data.id = $scope.idSection;
        if ($scope.idList != undefined) {
            data.idList = $scope.idList;
        }
        data.idSource = $scope.idSource;
        data.comment = $scope.commentVerifSection;
        var settings = {
            dataType: "json",
            type: "POST",
            url: urlToPost,
            data: data,
            success: function (resp) {
                $("#VerifBySectionDialog").modal("hide");
                if (resp.hasError === undefined) {
                    resp = resp.responseMessage;
                }
                if (resp.hasError === true) {
                    sharedSvc.showMsg(
                        {
                            title: resp.title,
                            message: resp.message,
                            type: "danger"
                        });
                }
                $scope.WaitForChoice=false;
            },
            error: function () {
                $("#VerifBySectionDialog").modal("hide");
                sharedSvc.showMsg(
                    {
                        title: "Error de red",
                        message: "<strong>No fue posible conectarse al servidor</strong> <br/><br/>Por favor intente más tarde",
                        type: "danger"
                    });
                $scope.WaitForChoice=false;
            }
        };

        $.ajax(settings);
    };

    $scope.pastToJson = function (string) {
        var result = jQuery.parseJSON(string);
        if (result == "") {
            return "[]";
        } else {
            return result;
        }
    };

    $scope.terminateVerification = function (urlTerminate) {
        $scope.WaitFor = true;

        $.post(urlTerminate)
            .success($scope.handleSuccess)
            .error($scope.handleError);
    };

    $scope.showMessageError = function (elementClick) {
        $("#divErrorMessage").show();
        var position = $(".tab-content").position();
        $("#divErrorMessage").css("left", position.left + 5);
        $("#divErrorMessage").addClass("errorMessageClass");

        $scope.entityError = elementClick;
    };


    $scope.hideMessageError = function () {
        $("#divErrorMessage").hide();
    };

    $scope.handleSuccess = function (resp) {
        $scope.WaitFor = false;

        $scope.listMsgError = {};
        try {
            if (resp.hasError === undefined) {
                resp = resp.responseMessage;
            }
            if (resp.hasError === false) {
                window.terminateVerification();
                return;
            }
            var obj = JSON.parse(resp.message);
            if (obj.groupMessage != undefined) {
                for (var i = 0; i < obj.groupMessage.length; i++) {
                    var g1 = obj.groupMessage[i];
                    $scope.listMsgError[g1.section] = $sce.trustAsHtml(g1.messages);
                }
            }
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


    $scope.setDlg = function (dlg, urlToSubmit) {
        $scope.Model.dlg = dlg;
        $scope.Model.url = urlToSubmit;

        dlg.on('hidden.bs.modal', function () {
            dlg.data('modal', null);
            dlg.replaceWith("");
        });
    };

});

