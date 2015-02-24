app.directive('rfcCalculated', function ($http, $timeout) {
    return function (scope, elem, attr) {

        var currentTimeout = null;


        elem.on('keyup change blur click', function () {
            if (scope.name == "" || scope.lastNameM == "" || scope.lastNameP == "" || scope.dateBirth=="")
                return;
            if (currentTimeout) {
                $timeout.cancel(currentTimeout);
            }
            currentTimeout = $timeout(function() {
                var rfc = "";
                var name=scope.name;
                var lastNameP = scope.lastNameP;
                var lastNameM = scope.lastNameM;
                var birthDate  = scope.dateBirth;
                var numbers ="";
                var from = birthDate.split("/");
                numbers = from[0].substring(2,4)+from[1]+from[2];
                rfc = name.substring(0,2)+lastNameP.substring(0,1)+lastNameM.substring(0,1)+numbers ;
                scope.rfc= rfc.toUpperCase();
            }, 200);
        });
    };
});