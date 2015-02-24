(function(window, document) {

// Create all modules and define dependencies to make sure they exist
// and are loaded in the correct order to satisfy dependency injection
// before all nested files are concatenated by Grunt

// Config
    angular.module('ngCsv.config', []).
        value('ngCsv.config', {
            debug: true
        }).
        config(['$compileProvider', function($compileProvider){
            if (angular.isDefined($compileProvider.urlSanitizationWhitelist)) {
                $compileProvider.urlSanitizationWhitelist(/^\s*(https?|ftp|mailto|file|data):/);
            } else {
                $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|file|data):/);
            }
        }]);

// Modules
    angular.module('ngCsv.directives', []);
    angular.module('ngCsv',
        [
            'ngCsv.config',
            'ngCsv.directives',
            'ngSanitize'
        ]);
    /**
     * ng-csv module
     * Export Javascript's arrays to csv files from the browser
     *
     * Author: asafdav - https://github.com/asafdav
     */
    angular.module('ngCsv.directives', []).
        directive('ngCsv', ['$parse', function($parse) {
            return {
                restrict: 'AC',
                replace: true,
                transclude: true,
                scope: { data:'=ngCsv', filename:'@filename' },
                controller: function($scope, $element, $attrs, $transclude) {
                    $scope.csv = "";
                    $scope.$watch('data', function(newValue, oldValue) {
                        $scope.buildCsv(newValue);
                    });

                    $scope.buildCsv = function(data) {

                        var csvContent = 'data:application/msword,' +
                            encodeURIComponent(
                                    '<!DOCTYPE html>'+
                                    '<html lang="en">'+
                                    '<head><title>Embedded Window</title></head>'+
                                    '<body><h1>oiajsfioadjfoiasdjfoiasdofiaj</h1></body>'+
                                    '</html>'
                            );

                        $scope.csv=csvContent;

                        };

                    $scope.getFilename = function() {
                        return $scope.filename ? $scope.filename : "datosCaso.doc";
                    };
                },
                template: '<div class="csv-wrap">' +
                    '<div class="element" ng-transclude></div>' +
                    '<a class="hidden-link" ng-hide="true" ng-href="{{ csv }}" download="{{ getFilename() }}"></a>' +
                    '</div>',
                link: function(scope, element, attrs) {
                    var subject = angular.element(element.children()[0]),
                        link = angular.element(element.children()[1]);

                    subject.bind('click', function(e) {
                        link[0].click();
                    });
                }
            };
        }]);
})(window, document);