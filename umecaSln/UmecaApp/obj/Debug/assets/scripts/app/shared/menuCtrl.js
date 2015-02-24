app.controller('menuController', function ($scope, $rootScope, $sce, sharedSvc, $timeout) {
    $scope.Title = $sce.trustAsHtml("Sesi&oacute;n a punto de terminar");
    $scope.templateMessage = "Su sesi&oacute;n se terminará en {time}, para prolongar su sesi&oacute;n de click en Continuar";
    $scope.Message = $sce.trustAsHtml("");
    $scope.Type = "warning";
    $scope.timeStartRedirect = 0;
    $scope.urlCheckSession;
    $scope.secondToMessage;
    $scope.initValueSession;
    $scope.counterReqAjax;
    $scope.bandMessageShow = false;
    $scope.linkLogin = function () {
        $rootScope.$broadcast('onLinkLogin');
    };

    $scope.init = function() {
        $scope.secondToMessage = $scope.initValueSession * 0.05;
        $scope.resetValues();
        if($scope.hasUser){
            setInterval( function(){
               checkState();
            }, 1000);
        }
    };

    $scope.resetValues = function (){
        $scope.counterReqAjax=$scope.initValueSession;
         $scope.timeStartRedirect = $scope.secondToMessage;
    }
    $timeout(function() {
        $scope.init();
    }, 0);

    $(document).ajaxStart(function() {
      if($scope.bandMessageShow == false){
            $scope.resetValues();
      }
    });

    $scope.calculateTimeShow = function (){
        if($scope.timeStartRedirect!=0){
            var result = "";
            var totalTime =  $scope.timeStartRedirect;
            var minutes = Math.floor($scope.timeStartRedirect / 60);
            if(minutes>0){
               result = minutes + "minutos y ";
            }
            totalTime = totalTime - (minutes * 60);
            result = result+totalTime+" segundos";
            $scope.timeStartRedirect--;
            return result;
        }
        return "0 segundos";
    };

    function checkState(){
        if($scope.hasUser && $scope.counterReqAjax <  $scope.secondToMessage && ! $scope.bandMessageShow){
            $scope.Message = $sce.trustAsHtml($scope.templateMessage.replace("{time}",$scope.calculateTimeShow()));
            $scope.$apply();
            $("#ConfirmBoxDialogSession").modal("show");
            $scope.bandMessageShow =true;
            setTimeout( function(){
                var settings = {
                    dataType: "json",
                    type: "POST",
                    url: $scope.urlCheckSession,
                    success: function (resp) {
                        if (resp.hasError === true) {
                            try {
                                window.location.replace($scope.urlHome);
                            } catch(e) {
                                window.location = $scope.urlHome;
                            }
                        }
                    },
                    error: function () {
                        sharedSvc.showMsg(
                            {
                                title: "Error de red",
                                message: "<strong>No fue posible conectarse al servidor</strong> <br/><br/>Por favor intente más tarde",
                                type: "danger"
                            });
                    }
                };

                $.ajax(settings);
            }, (($scope.secondToMessage * 1000)+1000));

            setInterval( function(){
                if($scope.bandMessageShow){
                    $scope.Message = $sce.trustAsHtml($scope.templateMessage.replace("{time}",$scope.calculateTimeShow()));
                    $scope.$apply();
                }
            },1000);
        }
        $scope.counterReqAjax--;
    }
    $scope.continueSession = function (){
        $scope.bandMessageShow = false;

        $("#ConfirmBoxDialogSession").modal("hide");
        var settings = {
            dataType: "json",
            type: "POST",
            url: $scope.urlCheckSession,
            success: function (resp) {
                if (resp.hasError === true) {
                    try {
                        window.location.replace($scope.urlHome);
                    } catch(e) {
                        window.location = $scope.urlHome;
                    }
                }
            },
            error: function () {
                sharedSvc.showMsg(
                    {
                        title: "Error de red",
                        message: "<strong>No fue posible conectarse al servidor</strong> <br/><br/>Por favor intente más tarde",
                        type: "danger"
                    });
            }
        };

        $.ajax(settings);
    };

});