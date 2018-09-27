
angular.module('ngTygaSoft.services.Menu', [])

.factory('$tygasoftMenu', function ($ionicHistory,$location, $ionicSideMenuDelegate, $tygasoftCommon, $tygasoftMC, $tygasoftLogin) {

    var ts = {};

    ts.Bind = function ($scope) {
        $scope.listMenu = ts.GetMenus();
    };

    ts.GetMenus = function () {
        var data = [{ "id": "1", "text": "通讯录", "icon": "ion-ios-home-outline", "url": "app.Contacts", "children": [] }, { "id": "2", "text": "个人中心", "icon": "ion-ios-paper-outline","children": [{ "id": "21", "text": "通讯录申请", "icon": "ion-ios-paper-outline", "url": "#/app/ContactApply", "children": [] }]}, { "id": "3", "text": "数据管理", "icon": "ion-soup-can-outline", "url": "app.listImportData", "children": [] }, { "id": "4", "text": "登录", "icon": "ion-ios-person-outline", "url": "onLogin()", "children": [] }, { "id": "5", "text": "关于我们", "icon": "ion-ios-information-outline", "url": "app.AboutUs", "children": [] }, { "id": "6", "text": "退出", "icon": "ion-power", "url": "exitApp()", "children": [] }];
        return data;
    };

    ts.ToggleMenu = function ($scope,$state, menu) {
        if (menu.url != undefined && menu.url != "") {
            $ionicHistory.nextViewOptions({
                disableAnimate: true,
                disableBack: true,
                historyRoot: true
            });
            if (menu.url.indexOf('()') > -1) {
                switch (menu.url) {
                    case 'exitApp()':
                        $tygasoftLogin.ExitApp();
                        break;
                    case 'onLogin()':
                        $scope.onLogin();
                        break;
                    default:
                        break;
                }
            }
            else if (menu.url == "app.Contacts") {
                if ($location.path() != '/app/Contacts') {
                    $state.go(menu.url);
                }
            }
            else {
                $state.go(menu.url);
            }
            $ionicSideMenuDelegate.toggleLeft();
            return false;
        }
        menu.show = !menu.show;
    };

    ts.IsMenuShown = function (menu) {
        return menu.show;
    };

    ts.SetMenuClass = function (menu) {
        if (menu.children.length == 0) return '';
        else {
            return menu.show ? 'ion-ios-arrow-down' : 'ion-ios-arrow-right';
        }
    };

    return ts;
});