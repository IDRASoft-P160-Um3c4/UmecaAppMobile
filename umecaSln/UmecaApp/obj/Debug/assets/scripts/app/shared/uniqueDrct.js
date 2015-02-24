app.directive('ngUnique', ['$http', function (async) {

    return {        
        require: 'ngModel',
        link: function(scope, elem, attrs, ctrl) {
            var options = scope.$eval(attrs.ngUnique);
            
            elem.on('blur', function() {
                scope.$apply(function() {
                    scope.errorUnique = "";
                    var val = elem.val();
                    if(val.length <= 0)
                        return;

                    var req = options.extraParam || {};
                    req[options.param] = val;

                    var ajaxConf = {
                        method: 'POST',
                        url: options.url,
                        data: req
                    };

                    async(ajaxConf)
                        .success(function (data) {
                            if(data.unique === false){
                                scope.errorUnique = options.msgError;
                            }
                            ctrl.$setValidity('unique', data.unique);
                        });
                });
            });
        }
    };
}]);


app.directive('uppercase', function() {
    return {
        require: 'ngModel',
        link: function(scope, element, attrs, modelCtrl) {
            var uppercase = function (inputValue) {

                if (inputValue === undefined || inputValue == null)
                    return undefined;

                var uppercased = inputValue.toUpperCase();
                if (uppercased !== inputValue) {
                    modelCtrl.$setViewValue(uppercased);
                    modelCtrl.$render();
                }
                return uppercased;
            };

            modelCtrl.$parsers.push(uppercase);
            uppercase(scope[attrs.ngModel]);
        }
    };
});

/*
app.directive('ngPrueba', function () {
    return {
        require: 'ngModel',
        //link: function postLink(scope, element, attrs) {
        //    var options = scope.$eval(attrs.ngPrueba);
        //    alert(options.valor);
        //    element.bind('blur', function () {
        //        scope.$apply(function () {
        //            scope.name = options.valor;
        //        });
        //    });
        //}
        link: function(scope, elem, attrs) {
            var options = scope.$eval(attrs.ngPrueba);
            options.valor = options.valor || '';
            elem.on('blur', function() {
                 scope.$apply(function() {
                     scope.StateName = options.valor;
                });
           });
        }
    };
});
*/