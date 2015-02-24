app.directive('ngCleanError', function () {
    return function ($scope, $element, $attr) {
        $element.bind('click', function () {
            var msg = $attr.msg;
            $scope[msg] = "";
            $scope.$apply();
        })
    };
});