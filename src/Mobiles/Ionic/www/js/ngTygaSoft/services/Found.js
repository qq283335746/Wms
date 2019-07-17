angular.module('ngTygaSoft.services.Found', [])
.factory('$tygasoftFound', function () {
    var ts = {};

    ts.Bind = function ($scope) {
        $scope.ListData = [];
        //$scope.ListData = [{ Url: '#/tab/ListImportData', Name: '数据管理' }];
    };

    return ts;
});