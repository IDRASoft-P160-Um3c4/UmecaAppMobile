app.controller('framingMeetingController', function ($scope, $timeout, $http, $rootScope, $sce) {

    $scope.fm = {}

    $scope.FMsuccessMsg = "";
    $scope.FMerrorMsg = "";

    $scope.FMerrorMsgLst = "";
    $scope.listMsgError = {};

    $scope.addressSuccessMsg = "";
    $scope.housemateSuccessMsg = "";
    $scope.referencesSuccessMsg = "";
    $scope.drugsSuccessMsg = "";
    $scope.activitiesSuccessMsg = "";
    $scope.jobSuccessMsg = "";
    $scope.victimSuccessMsg = "";


    $scope.addressErrorMsg = "";
    $scope.housemateErrorMsg = "";
    $scope.referencesErrorMsg = "";
    $scope.drugsErrorMsg = "";
    $scope.activitiesErrorMsg = "";
    $scope.jobErrorMsg = "";
    $scope.victimErrorMsg = "";
    $scope.fm.WaitFor = false;


    $scope.disableView = function () {
        /*if ($scope.fm.objView.canTerminate == false) {
         $("#divFM :input").attr("disabled", true);
         }
         else {
         $("#divFM :input").attr("disabled", false);
         }*/
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

    $scope.returnFM = function () {

        if ($scope.returnId && $scope.returnId != null && $scope.returnId != undefined && $scope.returnId > 0) {
            window.goToUrlMvcUrl($scope.urlManagerSup);
        }
        else
            window.goToUrlMvcUrl($scope.urlIndex);
    };

    $scope.init = function () {
        //$scope.disableView();
        var colorbox_params = {
            reposition: true,
            scalePhotos: true,
            scrolling: false,
            previous: '<i class="icon-arrow-left"></i>',
            next: '<i class="icon-arrow-right"></i>',
            close: '&times;',
            current: '{current} of {total}',
            maxWidth: '100%',
            maxHeight: '100%',
            onOpen: function () {
                document.body.style.overflow = 'hidden';
            },
            onClosed: function () {
                document.body.style.overflow = 'auto';
            },
            onComplete: function () {
                $.colorbox.resize();
            }
        };

        $('.ace-thumbnails [data-rel="colorbox"]').colorbox(colorbox_params);


    };

    $timeout(function () {
        $scope.init();
    }, 0);


    $scope.resizeMap = function () {
        $timeout(function () {
            $rootScope.$broadcast("resizeMap");
        }, 10);

    };

    $scope.doTerminate = function () {
        var currentTimeout = null;
        var url = "doTerminate.json";
        var idCase = $scope.fm.objView.idCase;
        var ajaxConf;

        ajaxConf = {
            method: "GET",
            params: {idCase: idCase}
        };

        ajaxConf.url = url;

        if (currentTimeout) {
            $timeout.cancel(currentTimeout);
        }

        $scope.fm.WaitFor = true;

        currentTimeout = $timeout(function () {
            $http(ajaxConf)
                .success(function (data) {
                    $scope.fm.WaitFor = false;

                    if (data.hasError == undefined) {
                        data = data.responseMesage;
                    }

                    if (data.hasError == true) {
                        $scope.listMsgError = [];
                        var obj = JSON.parse(data.message);
                        if (obj.groupMessage != undefined) {
                            for (var i = 0; i < obj.groupMessage.length; i++) {
                                var g1 = obj.groupMessage[i];
                                $scope.listMsgError[g1.section] = $sce.trustAsHtml(g1.messages);
                            }
                        }
                    }
                    else
                        $scope.returnFM();
                })
                .error(function () {
                    $scope.fm.WaitFor = false;
                    $scope.listMsgError['general'] = $sce.trustAsHtml("Error de red, intente m&aacute;s tarde.");
                });
        }, 200);
    };

    $scope.openNewPage = function (url, id) {
        var params = [];
        params["idParam"] = id;
        window.goToNewUrl(url, params, {opts: "fullscreen=no, top=0, left=0, width=500, height=300"});
    };

    $scope.submitComments = function (formId, urlToPost, id) {
        $(formId).validate();

        if ($(formId).valid() == false) {
            $scope.Invalid = true;
            return false;
        }
        $scope.fm.WaitFor = true;

        var url = urlToPost + id;

        $scope.actSuccessMsg = "";
        $scope.actErrorMsg = "";

        $.post(url, $(formId).serialize())
            .success(function (resp) {
                $scope.handleSuccessComment(resp, formId);
            })
            .error(function () {
                $scope.handleErrorComments(formId)
            });

        return true;
    };

    $scope.handleSuccessComment = function (resp, formId) {
        $scope.fm.WaitFor = false;

        try {
            if (resp.hasError === undefined) {
                resp = resp.responseMessage;
            }
            if (resp.hasError === false) {
                var arrMsg = resp.message.split('|');
                var idMsg = arrMsg[0];
                var msg = arrMsg[1];

                switch (idMsg) {
                    case "1":
                        $scope.addressSuccessMsg = $sce.trustAsHtml(msg);
                        break;
                    case "2":
                        $scope.housemateSuccessMsg = $sce.trustAsHtml(msg);
                        break;
                    case "3":
                        $scope.referencesSuccessMsg = $sce.trustAsHtml(msg);
                        break;
                    case "4":
                        $scope.drugsSuccessMsg = $sce.trustAsHtml(msg);
                    case "5":
                        $scope.activitiesSuccessMsg = $sce.trustAsHtml(msg);
                        break;
                    case "6":
                        $scope.jobSuccessMsg = $sce.trustAsHtml(msg);
                        break;
                    case "7":
                        $scope.victimSuccessMsg = $sce.trustAsHtml(msg);
                        break;
                }
            }
        } catch (e) {
            switch (formId) {
                case "FormCommentAddress":
                    $scope.addressErrorMsg = $sce.trustAsHtml("Error inesperado de datos. Por favor intente más tarde.");
                    break;
                case "FormCommentHousemate":
                    $scope.housemateErrorMsg = $sce.trustAsHtml("Error inesperado de datos. Por favor intente más tarde.");
                    break;
                case "FormCommentReferences":
                    $scope.referencesErrorMsg = $sce.trustAsHtml("Error inesperado de datos. Por favor intente más tarde.");
                    break;
                case "FormCommentDrugs":
                    $scope.drugsErrorMsg = $sce.trustAsHtml("Error inesperado de datos. Por favor intente más tarde.");
                    break;
                case "FormCommentActivities":
                    $scope.activitiesErrorMsg = $sce.trustAsHtml("Error inesperado de datos. Por favor intente más tarde.");
                    break;
                case "FormCommentJob":
                    $scope.jobErrorMsg = $sce.trustAsHtml("Error inesperado de datos. Por favor intente más tarde.");
                    break;
                case "FormCommentVictim":
                    $scope.victimErrorMsg = $sce.trustAsHtml("Error inesperado de datos. Por favor intente más tarde.");
                    break;
            }
        }
        $scope.$apply();
        return;
    };

    $scope.handleErrorComments = function (formId) {
        $scope.fm.WaitFor = false;

        switch (formId) {
            case "FormCommentAddress":
                $scope.addressErrorMsg = $sce.trustAsHtml("Error inesperado de datos. Por favor intente más tarde.");
                break;
            case "FormCommentHousemate":
                $scope.housemateErrorMsg = $sce.trustAsHtml("Error inesperado de datos. Por favor intente más tarde.");
                break;
            case "FormCommentReferences":
                $scope.referencesErrorMsg = $sce.trustAsHtml("Error inesperado de datos. Por favor intente más tarde.");
                break;
            case "FormCommentDrugs":
                $scope.drugsErrorMsg = $sce.trustAsHtml("Error inesperado de datos. Por favor intente más tarde.");
                break;
        }
    };

//    $scope.framingMeetingController = function (url){
//        var idCase = $scope.fm.objView.idCase;
//        var ajaxConf = {
//            method: "GET",
//            params: {id: idCase}
//        };
//
//        ajaxConf.url = url;
//        $http(ajaxConf)
//            .success(function (data) {
//
//                if (data.hasError == undefined) {
//                    data = data.responseMesage;
//                }
//
//                if (data.hasError == false) {
//                    var obj = JSON.parse(data.message);
//                    if (obj.groupMessage != undefined) {
//                        for (var i = 0; i < obj.groupMessage.length; i++) {
//                            var g1 = obj.groupMessage[i];
//                            $scope.listMsgError[g1.section] = $sce.trustAsHtml(g1.messages);
//                        }
//                    }
//                }
//                else
//                    $scope.listMsgError['general'] = $sce.trustAsHtml("Error de red, intente m&aacute;s tarde.");
//            });
//    }

})
;