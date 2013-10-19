//Todoservice
TodoApp.factory('categoriesService', ['$resource', function ($resource) {
    return $resource('/api/categories/:Id', { Id: '@Id' },
    {
        getList: { method: 'GET', isArray: true, params: { format: 'json' } },
        update: { method: 'PUT', params: { format: 'json' } },
        deleteAll: { method: 'DELETE', params: { format: 'json' } }
    });
}]);

