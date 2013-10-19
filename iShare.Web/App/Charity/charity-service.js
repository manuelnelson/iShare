//Todoservice
TodoApp.factory('charityService', ['$resource', function ($resource) {
    return $resource('/api/charities/:Id', { Id: '@Id' },
    {
        update: { method: 'PUT', params: { format: 'json' } },
        deleteAll: { method: 'DELETE', params: { format: 'json' } }
    });
}]);

