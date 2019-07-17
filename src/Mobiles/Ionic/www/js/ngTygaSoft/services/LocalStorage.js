angular.module('ngTygaSoft.services.LocalStorage', [])

//本地存储数据===================================
.factory('$tygasoftLocalStorage', ['$window', function ($window) {
    return {
        //存储单个属性
        Set: function (key, value) {
            $window.localStorage[key] = value;
        },
        //读取单个属性
        Get: function (key, defaultValue) {
            return $window.localStorage[key] || defaultValue;
        },
        //存储对象，以JSON格式存储
        SetObject: function (key, value) {
            $window.localStorage[key] = JSON.stringify(value);
        },
        //读取对象
        GetObject: function (key) {
            return JSON.parse($window.localStorage[key] || '{}');
        }
    }
}]);