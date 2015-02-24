app.controller('coDependantController', function($scope, $timeout) {
    $scope.c = {};
    $scope.listRelationship = [];
    $scope.c.rel = 0;
    $scope.listCoDependant = [];


    $scope.init = function(){
        if($scope.listCoDependant == undefined){
            $scope.listCoDependant = [];
        }
        if($scope.listRelationship === undefined || $scope.listRelationship.length <= 0)
            return;

        if($scope.c.relId === undefined){
            $scope.c.rel = $scope.listRelationship[0];
            $scope.c.relId = $scope.c.rel.id;
        }
        $scope.cleanArray();
    };


    $timeout(function() {
        $scope.init();
    }, 0);


    $scope.validateCoDefendant = function(){
        $scope.msgError= "";
        var valid = true;
        if ($scope.c.fullName == undefined || $scope.c.fullName == "" ) {
            $scope.msgError = "Para agregar un coimputado debe ingresar el nombre completo.";
            return false;
        }
        var strName= $scope.c.fullName+"";
        if (strName.length > 300 || strName.length < 5){
            $scope.msgError = "La longitud del nombre debe ser entre 5 y 300 caracteres";
            valid = false;
        }
        return valid;

    }
   $scope.addCoDependant = function(){
          if($scope.validateCoDefendant() == false){
              return false;
          }
          var a ={};
          a.relationship = {};
          a.relationship.id = $scope.c.rel.id;
          a.relationship.name = $scope.c.rel.name;
          a.fullName = $scope.c.fullName;
          $scope.listCoDependant.push(a);
          $scope.c.federal = $scope.listRelationship[0];
          $scope.c.fullName = undefined;
          $scope.cleanArray();

   };

    $scope.deleteCoDependant= function(index){
      $scope.listCoDependant.splice(index,1);
        $scope.cleanArray();

    };

    $scope.cleanArray  = function (){
        var abc = $scope.listCoDependant;
        $scope.coDependantString = JSON.stringify(abc);
        /*for(var item in $scope.listSchedule){
            delete item[$$hashKey];
        } */
    }
});