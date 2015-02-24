app.controller('verificationActivitiesController', function($scope, $timeout, $q, $rootScope,$sce) {
    $scope.WaitFor = false;
    $scope.MsgError = "";
    $scope.Model = {};
    $scope.verification = false;
    $scope.generalComponent = false;
    $scope.activities="";
    $scope.init = function(){

      };

    $scope.formatHtml = function(sHtml){
        return $sce.trustAsHtml(sHtml);
    };

    $rootScope.$on('refreshActivities', function (event,activities) {
        $scope.activities=activities;
    });


    $timeout(function() {
        $scope.init();
    }, 0);

    $scope.validateVerif = function () {
        $scope.MsgError="";
      if($scope.activities==""||$scope.activities==undefined){
          $scope.MsgError="Debe agregar una actividad"
          return null;
      }else if($scope.activities !=undefined && $scope.activities=="false"){
          $scope.MsgError="Debe agregar las especificaciones de cada actividad requerida"
          return null;
      }else{
         return $scope.activities;
      }
    };
    $scope.submit = function (formId) {
        var formSerialize = $(formId).serialize();
        var result = $scope.validateVerif(formSerialize);
        if (result == null) {
            return false;
        }

        var content= {};
        content.name="socialEnvironment.activities";
        content.value=result;
        var aux = [];
        aux[0] = content;
        var contentJson = JSON.stringify(aux);
        $scope.WaitFor = true;

        var formSerialize = $(formId).serialize();
        var content = "val=" + contentJson + "&&idCase=" + $scope.idCase + "&&idSource=" + $scope.idSource;
        $scope.WaitFor = true;
        $.post($scope.urlToGoSave, content)
            .success($scope.handleSuccess)
            .error($scope.handleError);
        return true;
    };

    $scope.cancel = function () {
        $scope.Model.dlg.modal('hide');
    };


    $scope.handleSuccess = function (resp) {
        $scope.WaitFor = false;
        $scope.MsgError = "";

        try {

            if (resp.hasError === undefined) {
                resp = resp.responseMessage;
            }

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


    $scope.setDlg = function (dlg, urlToSubmit) {
        if ($scope.Model == undefined || $scope.Model.dlg == undefined) {
            $scope.Model.dlg = dlg;
            $scope.Model.url = urlToSubmit;
        }
    };

});


app.controller('innerActivitiesController', function ($scope, $timeout, $q, $rootScope) {

    $scope.specification = {};
    $scope.lstActivity = [];
    $scope.activityModel = [];
    $scope.activityList = [];
    $scope.pCSelected = [];
    $scope.relActivities = [];

    $scope.init = function () {
        $("#slctActivityV").chosen();
    };

    $timeout(function () {
        $scope.init();
    }, 0);

    $scope.matchActivities = function () {
        $scope.relActivities = [];
        for (var i = 0; i < $scope.activityModel.length; i++) {
            var model = {};
            model.activity = {};
            model.activity.id = $scope.activityModel[i].id;
            if ($scope.activityModel[i].specification && ($scope.specification[$scope.activityModel[i].name] == undefined || $scope.specification[$scope.activityModel[i].name] == "")) {
                $scope.activities = "false";
                $rootScope.$broadcast('refreshActivities', $scope.activities);
                return;
            }
            if ($scope.specification[$scope.activityModel[i].name] != undefined) {
                model.specification = $scope.specification[$scope.activityModel[i].name];
            } else {
                model.specification = "";
            }
            $scope.relActivities.push(model);
        }
        $scope.activities = JSON.stringify($scope.relActivities);
        $rootScope.$broadcast('refreshActivities', $scope.activities);

        return true;
    };

    $scope.selectedActivities = function (lstActivity, lstActivitySelect) {

        for (var i = 0; i < lstActivitySelect.length; i++) {
            for (var j = 0; j < lstActivity.length; j++) {
                if (lstActivity[j].id === lstActivitySelect[i].id) {
                    $scope.activityModel.push(lstActivity[j]);
                    if (lstActivity[j].specification) {
                        $scope.specification[lstActivity[j].name] = lstActivitySelect[i].specification;
                    }
                }
            }
        }
        $scope.matchActivities();
    };


});