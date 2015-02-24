app.directive('enableTime', function ($http, $timeout) {
    return function (scope, elem, attr) {
        scope.cleanMessage=function (elementWatch){
            scope[elementWatch] ="";
           clearTimeout(scope.timeoutMessageFunction);
        };

        var elementWatch = attr.elementWatch;
        scope.$watch(elementWatch, function(newValue, oldValue) {
            if (oldValue == "" && (newValue !== "" || newValue!== undefined)) {
                scope.timeoutMessageFunction = setTimeout(function ()
                {
                    scope.$apply( scope.cleanMessage(elementWatch));
                }, 10000);
            }else{

            }
        });
    };
});