// https://github.com/Eonasdan/bootstrap-datetimepicker/issues/314#issuecomment-42657927

app.directive("datepicker", function () {
    return {
        restrict: "A",
        require: "ngModel",
        link: function (scope, elem, attrs, ngModelCtrl) {
            var updateModel = function () {
                scope.$apply(function () {
                    ngModelCtrl.$modelValue = elem.val();
                });
            };
            elem.datepicker({
                format: "yyyy-mm-dd"
            });
            elem.on("change", function (e) {
                updateModel();
            });
        }
    }
});