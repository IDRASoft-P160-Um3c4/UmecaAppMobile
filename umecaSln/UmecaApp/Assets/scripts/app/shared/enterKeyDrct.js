app.directive('ngEnterKey',function() {
    return function(scope, element, attrs) {
        var idElement = attrs['forElementId'];

        if(idElement === undefined)
            idElement = "btn-def-ck";

        element.bind("keypress", function(event) {
            if(event.which === 13) {
                event.preventDefault();
                $( "#" + idElement).trigger( "click" );
            }
        });
    };
})