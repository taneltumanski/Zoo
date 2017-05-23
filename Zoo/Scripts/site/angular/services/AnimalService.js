app.service('AnimalService', ['$http', function ($http) {
    this.getAnimals = function () {
        return $http.get(zoo.urls.animalsController());
    };

    this.addOrUpdateAnimal = function (animal) {
        return $http.post(zoo.urls.animalsController(), animal);
    };

    this.deleteAnimal = function (id) {
        return $http.delete(zoo.urls.animalsController() + "?id=" + id);
    };
}]);