//Todoservice
TodoApp.factory('todoService', ['$resource', function ($resource) {
    return $resource('/api/categories/:Id', { Id: '@Id' },
    {
        update: { method: 'PUT', params: { format: 'json' } },
        deleteAll: { method: 'DELETE', params: { format: 'json' } }
    });
}]);

