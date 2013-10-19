var CharityCtrl = ['$scope', '$routeParams', '$location', 'charityService', 'categoriesService',
    function ($scope, $routeParams, $location, charityService, categoriesService) {
        //$scope.getTodos = function () {
        //    todoService.query({ format: 'json' }, function (todos) {
        //        $scope.todos = [];
        //        $scope.todos = $scope.todos.concat(todos);
        //    });
        //};

        categoriesService.getList(null, function (categories) {
            $scope.categories = categories;
        });

        $scope.addCharity = function () {
            charityService.save(null, $scope.charity, function () {
                $scope.charityForm.$setPristine();
                $scope.charity = '';
            });
        };

    }];