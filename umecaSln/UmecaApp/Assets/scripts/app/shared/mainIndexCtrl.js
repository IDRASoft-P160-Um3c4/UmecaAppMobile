app.controller("mainIndexController", function ($scope, sharedSvc, $sce) {

    $scope.deleteMsg = function (id, urlToGo) {
        sharedSvc.showConf({ title: "Confirmar eliminación", message: "¿Desea eliminar el mensaje de confirmación?", type: "warning" })
            .then(function () {
                $scope.doDeleteMsg(id, urlToGo);
            });
    }

    $scope.doDeleteMsg = function (id, urlToPost) {

        $.ajax({
            url: urlToPost,
            type: "POST",
            data: JSON.stringify({id: id}),
            success: $scope.handleSuccess,
            error: $scope.handleError,
            dataType: "json",
            contentType: "application/json"
        });
    }

    $scope.deleteNotif = function (id, urlToGo) {
        sharedSvc.showConf({ title: "Confirmar eliminación", message: "¿Desea eliminar la notificación?", type: "warning" })
            .then(function () {
                $scope.doDeleteNotif(id, urlToGo);
            });
    }

    $scope.doDeleteNotif = function (id, urlToPost) {

        $.ajax({
            url: urlToPost+id,
            type: "GET",
            success: $scope.handleSuccess,
            error: $scope.handleError
        });
    }

    $scope.handleSuccess = function (data) {
        try {
            if (data.hasError === true) {
                sharedSvc.showMsg({
                    title: data.title,
                    message: "Se presentó el siguiente error: " + data.message,
                    type: "danger"
                });
                return;
            }

            for(var i=0; i<$scope.lstNotification.length; i++){
                var notif = $scope.lstNotification[i];

                if(notif.id == data.returnData){

                    $scope.lstNotification.splice(i, 1);
                    $scope.$apply();
                    return;
                }
            }

        } catch (ex) {
            sharedSvc.showMsg({
                title: "Error de red",
                message: "<strong>No fue posible conectarse al servidor</strong> <br/><br/>Por favor intente más tarde",
                type: "danger"
            });
        }
    }

    $scope.handleError = function (data) {
        $scope.workingTrack = false;
        sharedSvc.showMsg({
            title: "Error de red",
            message: "<strong>No fue posible conectarse al servidor</strong> <br/><br/>Por favor intente más tarde",
            type: "danger"
        });
    }

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


    $scope.formatHtml = function(sHtml){
        return $sce.trustAsHtml(sHtml);
    };

});
