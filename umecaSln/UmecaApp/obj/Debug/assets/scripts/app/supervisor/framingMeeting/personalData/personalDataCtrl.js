app.controller('personalDataFMController', function ($scope, $timeout, $http, $q, $sce) {

        $scope.pd = {};

        $scope.pdSuccessMsg = "";
        $scope.pdErrorMsg = "";
        $scope.lstStates = [];

        $scope.changeCountry = function () {

            if ($scope.pd.birthCountryId == 1) {

                $scope.pd.isMexico = true;

                $.post($scope.urlGetStates)
                    .success(function (resp) {
                        if (resp.hasError == true) {
                            $scope.MsgError = resp.message;
                            $scope.$apply();
                        } else {
                            resp = resp.responseMessage;
                            $scope.pd.lstStates = $.parseJSON(resp.message);
                            $scope.fillSelState();
                            $scope.$apply();
                        }
                    }
                )
                    .error(function () {
                        $scope.MsgError = "Error de red, intente mas tarde.";
                        $scope.$apply();
                    });
            } else {
                $scope.pd.isMexico = false;
            }

        };

        $scope.fillCountry = function () {

            if ($scope.lstCountry != undefined && $scope.lstCountry.length > 0)

                if ($scope.pd.birthCountryId === undefined || $scope.pd.birthCountryId === null) {

                    for (var i = 0; i < $scope.lstCountry.length; i++) {


                        if ($scope.lstCountry[i].id == 1) {//para seleccionar a mexico por defecto
                            $scope.pd.birthCountry = $scope.lstCountry[i];
                            $scope.pd.isMexico == true;
                            $scope.changeCountry();
                            break;
                        }
                    }

                    $scope.pd.birthCountryId = $scope.pd.birthCountry.id;
                }
                else {
                    for (var i = 0; i < $scope.lstCountry.length; i++) {
                        var rel = $scope.lstCountry[i];
                        if (rel.id === $scope.pd.birthCountryId) {
                            $scope.pd.birthCountry = rel;
                            break;
                        }
                    }
                }
        };

        $scope.loadPersonalData = function () {

            var currentTimeout = null;
            var ajaxConf;

            var url = $('#hidUrlPD').attr("value");
            var idCase = $('#hidIdCasePD').attr("value");

            ajaxConf = {
                method: "POST",
                params: {idCase: idCase}
            };

            ajaxConf.url = url;

            if (currentTimeout) {
                $timeout.cancel(currentTimeout);
            }

            currentTimeout = $timeout(function () {
                $http(ajaxConf)
                    .success(function (data) {
                        $scope.fillPersonalData(data);
                    });
            }, 200);
        };

        $scope.fillPersonalData = function (data) {
            $scope.pd.name = data.name;
            $scope.pd.lastNameP = data.lastNameP;
            $scope.pd.lastNameM = data.lastNameM;
            $scope.pd.gender = data.gender;
            $scope.pd.maritalStatus = data.maritalStatus;
            $scope.pd.maritalStatusYears = data.maritalStatusYears;
            $scope.pd.birthCountryId = data.birthCountryId;
            $scope.fillCountry();
            $scope.pd.birthState = data.birthState;
            $scope.pd.birthDate = $scope.myFormatDate(data.birthDate);

            $scope.pd.phone = data.phone;
            $scope.pd.celPhone = data.celPhone;
            $scope.pd.email = data.email;
            $scope.pd.socialNetworking = data.socialNetworking;
            $scope.pd.comments = data.comments;


            if ($scope.pd.birthDate != undefined && $scope.pd.birthDate != "")
                $scope.calcAge();

            $scope.pd.physicalCondition = data.physicalCondition;

            $scope.pd.isMexico = data.isMexico;

            if ($scope.pd.isMexico == true)
                $scope.pd.birthStateCmbId = data.birthStateId;

            $scope.changeCountry();

        };

        $scope.fillSelState = function () {

            if ($scope.pd.lstStates === undefined || $scope.pd.lstStates.length <= 0)
                return;

            if ($scope.pd.birthStateCmbId === undefined) {
                $scope.pd.birthStateCmb = $scope.pd.lstStates[0];
                $scope.pd.birthStateCmbId = $scope.pd.birthStateCmb.id;
            }
            else {
                for (var i = 0; i < $scope.pd.lstStates.length; i++) {
                    var sta = $scope.pd.lstStates[i];

                    if (sta.id === $scope.pd.birthStateCmbId) {
                        $scope.pd.birthStateCmb = sta;
                        break;
                    }
                }
            }
        };

        $scope.calcAge = function () {

            if ($scope.pd.birthDate != null && $scope.pd.birthDate != "") {
                var arrBth = [];
                arrBth = $scope.pd.birthDate.split('/');

                var dtBthObj;

                if (arrBth.length > 0)
                    dtBthObj = new Date(parseInt(arrBth[0]), parseInt(arrBth[1]) - 1, parseInt(arrBth[2]));
                else
                    dtBthObj = new Date($scope.pd.birthDate);

                var ageDifMs = Date.now() - dtBthObj.getTime();
                var ageDate = new Date(ageDifMs);

                $scope.pd.age = Math.abs(ageDate.getUTCFullYear() - 1970);
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


        $scope.init = function () {
            $scope.loadPersonalData();
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
                    $scope.pdSuccessMsg = $sce.trustAsHtml(resp.message);
                    $scope.pdErrorMsg = "";
                    $scope.$apply();
                    return;
                }

                $scope.pdErrorMsg = $sce.trustAsHtml(resp.message);
                $scope.pdSuccessMsg = "";
                $scope.$apply();

            } catch (e) {
                $scope.pdErrorMsg = "Error inesperado de datos. Por favor intente más tarde.";
            }
        };

        $scope.handleError = function () {
            $scope.WaitFor = false;
            $scope.pdErrorMsg = "Error de red. Por favor intente más tarde.";
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