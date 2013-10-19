var CharityCtrl = ['$scope', '$routeParams', '$location', 'charityService',
    function ($scope, $routeParams, $location, charityService) {
    //$scope.getTodos = function () {
    //    todoService.query({ format: 'json' }, function (todos) {
    //        $scope.todos = [];
    //        $scope.todos = $scope.todos.concat(todos);
    //    });
    //};

    $scope.addCharity = function () {
        charityService.save(null, $scope.charity, function () {
            $scope.charityForm.$setPristine();
            $scope.charity.Task = '';
        });
    };

}];