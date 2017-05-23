app.service('SpeciesService', ['$http', function ($http) {
    this.getSpecies = function () {
        return $http.get(zoo.urls.speciesController());
    };
}]);