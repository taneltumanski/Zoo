var app = angular.module('app', []);

app.controller('ZooController', ['$scope', 'AnimalService', 'SpeciesService',
    function ZooController($scope, AnimalService, SpeciesService) {
        $scope.animals = [];
        $scope.species = [];
        $scope.currentAnimal = emptyAnimal();
        $scope.addUpdateErrors = [];

        init();

        function init() {
            AnimalService
                .getAnimals()
                .then(function (result) {
                    $scope.animals = result.data;
                });

            var zooHubProxy = $.connection.zooHub;
            zooHubProxy.client.message = function (data) {
                $scope.$apply(function () {
                    var animal = data.Data;

                    if (data.IsDeleted) {
                        for (var i = 0; i < $scope.animals.length; i++) {
                            if ($scope.animals[i].Id == animal.Id) {
                                $scope.animals.splice(i, 1);
                            }
                        }
                    } else {
                        var found = false;

                        for (var i = 0; i < $scope.animals.length; i++) {
                            if ($scope.animals[i].Id == animal.Id) {
                                $scope.animals[i] = animal;
                                found = true;
                            }
                        }

                        if (!found) {
                            $scope.animals.push(animal);
                        }
                    }
                });
            };

            $.connection.hub.start().done(function () {
                console.log("Connection started");
            });
        }

        function updateSpecies() {
            SpeciesService
                .getSpecies()
                .then(function (result) {
                    $scope.species = result.data;
                });
        }

        function emptyAnimal() {
            return {
                Id: 0,
                Name: "",
                BirthDate: "",
                Specie: {
                    Id: 0,
                    Name: ""
                }
            };
        }

        function cloneAnimal(animal) {
            return {
                Id: animal.Id,
                Name: animal.Name,
                BirthDate: animal.BirthDate,
                Specie: {
                    Id: animal.Specie.Id,
                    Name: animal.Specie.Name
                }
            };
        }

        $scope.addAnimal = function () {
            updateSpecies();
            $scope.currentAnimal = emptyAnimal();
            $scope.addUpdateErrors = [];

            $("#animalModal").modal()
        }

        $scope.updateAnimal = function (id) {
            var selectedAnimal;

            for (var i = 0; i < $scope.animals.length; i++) {
                if ($scope.animals[i].Id == id) {
                    selectedAnimal = $scope.animals[i];
                    break;
                }
            }

            if (selectedAnimal) {
                updateSpecies();
                $scope.addUpdateErrors = [];
                $scope.currentAnimal = cloneAnimal(selectedAnimal);

                $("#animalModal").modal();
            }  
        }

        $scope.saveAnimal = function () {
            AnimalService
                .addOrUpdateAnimal($scope.currentAnimal)
                .then(
                    function (result) {
                        $("#animalModal").modal("hide");
                    },
                    function (error) {
                        var errors = [];

                        for (var err in error.data.Errors) {
                            for (var i = 0; i < error.data.Errors[err].length; i++) {
                                errors.push(error.data.Errors[err][i]);
                            }
                        }
                        $scope.addUpdateErrors = errors;
                    });
        }

        $scope.deleteAnimal = function (id) {
            AnimalService.deleteAnimal(id);
        }

        $scope.hasAnimals = function () {
            return $scope.animals.length > 0;
        }

        $scope.getAverageAge = function () {
            if ($scope.animals.length == 0) {
                return 0;
            }

            var total = 0;
            for (var i = 0; i < $scope.animals.length; i++) {
                total += $scope.animals[i].Age;
            }

            return total / $scope.animals.length;
        }
    }
]);