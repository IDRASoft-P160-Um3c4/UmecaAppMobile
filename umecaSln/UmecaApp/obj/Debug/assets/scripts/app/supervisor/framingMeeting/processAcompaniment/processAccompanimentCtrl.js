app.controller('processAcompanimentController', function ($scope, $rootScope, $timeout, $http, $q) {

        $scope.pa = {};
        $scope.a = {};

        $scope.paSuccessMsg = "";
        $scope.paErrorMsg = "";

        $scope.lstRelationship = {};

        $scope.fillRelationship = function () {

            if ($scope.lstRelationship != undefined && $scope.lstRelationship.length > 0)

                if ($scope.pa.relationshipId === undefined || $scope.pa.relationshipId === null ) {
                    $scope.pa.relationship = $scope.lstRelationship[0];
                    $scope.pa.relationshipId = $scope.pa.relationship.id;
                }
                else {

                    for (var i = 0; i < $scope.lstRelationship.length; i++) {
                        var rel = $scope.lstRelationship[i];
                        if (rel.id === $scope.pa.relationshipId) {
                            $scope.pa.relationship = rel;
                            break;
                        }
                    }
                }
        };


        $scope.loadProcessAccompaniment = function () {

            var currentTimeout = null;
            var ajaxConf;

            var url = $('#hidUrlPA').attr("value");
            var idCase = $('#hidIdCaseProc').attr("value");

            ajaxConf = {
                method: "POST",
                params: {idCase: idCase}/*,
                 dataType: "json",
                 contentType: "application/json"*/
            };

            ajaxConf.url = url;

            if (currentTimeout) {
                $timeout.cancel(currentTimeout);
            }

            currentTimeout = $timeout(function () {
                $http(ajaxConf)
                    .success(function (data) {
                        $scope.fillAccompaniment(data);
                    });
            }, 200);
        };

        $scope.auxMap="framing";
        $scope.fillAccompaniment = function (data) {

            $scope.pa.name=data.name;
            $scope.pa.lastNameP=data.lastNameP;
            $scope.pa.lastNameM=data.lastNameM;
            $scope.pa.gender=data.gender;
            $scope.pa.age=data.age;
            $scope.pa.phone=data.phone;
            $scope.pa.celphone=data.celphone;
            $scope.pa.occName=data.occName;
            $scope.pa.occPlace=data.occPlace;
            $scope.pa.occPhone=data.occPhone;
            $scope.pa.degree=data.degree;
            $scope.pa.relationshipId=data.relationshipId;
            $scope.fillRelationship();
            $rootScope.$broadcast("setAddress", data.address);

        };

        $scope.init=function(){

            $scope.loadProcessAccompaniment();
        };

        $timeout(function () {
            $scope.init();
        }, 0);

        $scope.WaitFor = false;
        $scope.Model = {};

        $scope.submitIdCaseParam = function (formId, urlToPost, id) {

            $(formId).validate();

            if ($(formId).valid() == false) {
                $scope.Invalid = true;
                return false;
            }
            $scope.WaitFor = true;

            var url = urlToPost + id;

            $.post(url, $(formId).serialize())
                .success($scope.handleSuccess)
                .error($scope.handleError);

            return true;
        };

        $scope.handleSuccess = function (resp) {
            $scope.WaitFor = false;

            try {
                if (resp.hasError === undefined) {
                    resp = resp.responseMessage;
                }
                if (resp.hasError === false) {
                    $scope.paSuccessMsg = resp.message;
                    $scope.$apply();
                    return;
                }

                $scope.paErrorMsg = resp.message;
                $scope.$apply();

            } catch (e) {
                $scope.paErrorMsg = "Error inesperado de datos. Por favor intente más tarde.";
            }
        };

        $scope.handleError = function () {
            $scope.WaitFor = false;
            $scope.paErrorMsg = "Error de red. Por favor intente más tarde.";
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

    }
)
;