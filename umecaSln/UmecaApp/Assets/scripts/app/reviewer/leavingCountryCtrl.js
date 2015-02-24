app.controller('leavingController', function($scope, $timeout,$q,$rootScope) {
    $scope.l = {};
    $scope.listElection = [];
    $scope.listCountry = [];
    $scope.l.oc= 0;
    $scope.l.country= 0;
    $scope.l.doc = 0;
    $scope.l.cf = 0;
    $scope.country=0;
    $scope.state=0;
    $scope.m = {};
    $scope.Model = {};
    $scope.def= $q.defer();

    $scope.showChoicesSection = function(idSection, idList, idSource, sectionName, listView){
        var arg = [idSection, idList, idSource, sectionName, listView]
        $rootScope.$broadcast('ShowChoicesBySection',arg);
    };

    $scope.init = function(){
        if($scope.listCountry === undefined || $scope.listCountry.length <= 0)
            return;

        if($scope.l.countryId === undefined){

            for (var i = 0; i < $scope.listCountry.length; i++) {
                if ($scope.listCountry[i].id == 1) {//para seleccionar Mexico por default
                    $scope.l.country = $scope.listCountry[i];
                    break;
                }
            }

            $scope.l.countryId = $scope.l.country.id;
        }
        else{
            for(var i=0; i < $scope.listCountry.length; i++){
                var country = $scope.listCountry[i];

                if(country.id === $scope.l.countryId){
                    $scope.l.country = country;
                    break;
                }
            }
        }
        if($scope.listElection === undefined || $scope.listElection.length <= 0)
            return;

        if($scope.l.ocId === undefined){
            $scope.l.oc = $scope.listElection[0];
            $scope.l.ocId = $scope.l.oc.id;
        }
        else{
            for(var i=0; i < $scope.listElection.length; i++){
                var oc = $scope.listElection[i];

                if(oc.id === $scope.l.ocId){
                    $scope.l.oc = oc;
                    break;
                }
            }
        }

        if($scope.l.docId === undefined){
            $scope.l.doc = $scope.listElection[0];
            $scope.l.docId = $scope.l.doc.id;
        }
        else{
            for(var i=0; i < $scope.listElection.length; i++){
                var doc = $scope.listElection[i];

                if(doc.id === $scope.l.docId){
                    $scope.l.doc = doc;
                    break;
                }
            }
        }

        if($scope.l.cfId === undefined){
            $scope.l.cf = $scope.listElection[0];
            $scope.l.cfId = $scope.l.cf.id;
        }
        else{
            for(var i=0; i < $scope.listElection.length; i++){
                var cf = $scope.listElection[i];

                if(cf.id === $scope.l.cfId){
                    $scope.l.cf = cf;
                    break;
                }
            }
        }

        if($scope.l.facId === undefined){
            $scope.l.fac = $scope.listElection[0];
            $scope.l.facId = $scope.l.fac.id;
        }
        else{
            for(var i=0; i < $scope.listElection.length; i++){
                var fac = $scope.listElection[i];

                if(fac.id === $scope.l.facId){
                    $scope.l.fac = fac;
                    break;
                }
            }
        }
        if($scope.l.immigrationDocId == undefined){
            $scope.l.immigrationDoc =$scope.listImmigrationDoc[0];
            $scope.l.immigrationDocId = $scope.l.immigrationDoc.id;
        } else{
            for(var i=0; i < $scope.listImmigrationDoc.length; i++){
                var fac = $scope.listImmigrationDoc[i];

                if(fac.id === $scope.l.immigrationDocId){
                    $scope.l.immigrationDoc = fac;
                    break;
                }
            }
        }
        if($scope.l.relId == undefined){
            $scope.l.rel =$scope.listRel[0];
            $scope.l.relId = $scope.l.rel.id;
        } else{
            for(var i=0; i < $scope.listRel.length; i++){
                var fac = $scope.listRel[i];

                if(fac.id === $scope.l.relId){
                    $scope.l.rel = fac;
                    break;
                }
            }
        }
    };


    $timeout(function() {
        $scope.init();
    }, 0);

});