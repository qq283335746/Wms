angular.module('ngTygaSoft.services.Sys', [])
.factory('$tygasoftSys', function ($ionicPopup, $tygasoftMC) {
    var ts = {};

    ts.Bind = function ($scope) {
        $scope.ListData = [{ Name: '设置' }, { Name: '检测更新' }, { Name: '退出系统' }];

        $scope.onTo = function (name) {
            switch (name) {
                case "设置":
                    window.location = '#/tab/SysSet';
                    break;
                case "检测更新":
                    window.location = '#/tab/AppPackage';
                    break;
                case "退出系统":
                    ts.ExitApp();
                    break;
                default:
                    break;
            }
        };
    };

    ts.ExitApp = function () {
        $ionicPopup.confirm({
            title: $tygasoftMC.MC.Alert_Title,
            template: $tygasoftMC.MC.M_ExitApp_Content,
            cancelText: $tygasoftMC.MC.Btn_CancelText,
            okText: $tygasoftMC.MC.Btn_OkText
        }).then(function (res) {
            if (res) {
                RfidScan.onClose(function (result) {
                    $tygasoftLocalStorage.Set("UhfOnOff", 0);
                });
                ionic.Platform.exitApp();
            }
        })
    }

    return ts;
});