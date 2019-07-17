
angular.module('ngTygaSoft.services.Login', [])

.factory('$tygasoftLogin', function ($q, $http, $ionicLoading, $cordovaToast, $ionicPopup, $tygasoftDbHelper, $tygasoftLocalStorage, $tygasoftCommon, $tygasoftMC) {

    var ts = {};

    ts.IsLogin = function () {
        var q = $q.defer();

        $tygasoftDbHelper.GetValueByKey('KeyValue', 'DeviceInfo').then(function (result) {
            if (result) {
                return q.resolve(!JSON.parse(result).UserName.IsNullOrEmpty());
            }
            return q.resolve(false);
        })

        return q.promise;
    };

    ts.GetLoginInfo = function () {
        var q = $q.defer();

        $tygasoftDbHelper.GetValueByKey('KeyValue', 'DeviceInfo').then(function (result) {
            if (result) return q.resolve(JSON.parse(result));
            return q.resolve(null);
        })

        return q.promise;
    };

    ts.LoginByOAuth = function () {
        var jDeviceInfo = this.GetLoginInfo();
        var url = $tygasoftCommon.ServerUrl() + "/services/MobileService.svc/LoginByOAuth";
        var sData = '{"model":{"AppKey":"' + $tygasoftCommon.AppKey + '","DeviceId":"' + jDeviceInfo.UUID + '","Latlng":"' + jDeviceInfo.Latlng + '","UserId":"' + jDeviceInfo.UserName + '","AccessToken":"' + jDeviceInfo.AccessToken + '"}}';

        $http.defaults.headers.post['Content-Type'] = 'application/json; charset=utf-8';
        $http({
            method: 'POST',
            url: url,
            data: sData
        }).then(function (res) {
            //console.log(res);
            var result = res.data;
            if (result.ResCode != 1000) {
                $ionicPopup.alert({
                    title: $tygasoftMC.MC.Alert_Title,
                    template: result.Msg,
                    okText: '确定'
                })
                return false;
            }
            console.log(result);
        });
    };

    ts.Login = function () {
        var jDeviceInfo = this.GetLoginInfo();
        var url = $tygasoftCommon.ServerUrl() + "/services/MobileService.svc/Login";
        var sData = '{"model":{"UUID":"' + jDeviceInfo.UUID + '","Latlng":"' + jDeviceInfo.Latlng + '","UserName":"' + jDeviceInfo.UserName + '","AccessToken":"' + jDeviceInfo.AccessToken + '"}}';

        $http.defaults.headers.post['Content-Type'] = 'application/json; charset=utf-8';
        $http({
            method: 'POST',
            url: url,
            data: sData,

        }).then(function (res) {
            //console.log(res);
            var result = res.data;
            console.log(result);
        });
    };

    ts.DoLogin = function ($scope) {
        var userName = $scope.loginData.UserName;
        if (!userName) {
            $cordovaToast.showShortCenter($tygasoftMC.MC.Required_Phone);
            return false;
        }
        var password = $scope.loginData.Password;
        if (!password) {
            $cordovaToast.showShortCenter($tygasoftMC.MC.Required_Password);
            return false;
        }

        ts.GetLoginInfo().then(function (jDeviceInfo) {
            if (!jDeviceInfo) {
                $cordovaToast.showShortCenter($tygasoftMC.MC.Data_Error);
                return false;
            }
            jDeviceInfo.UserName = userName;

            var url = $tygasoftCommon.ServerUrl() + "/services/MobileService.svc/Login";
            var sData = '{"model":{"AppKey":"' + $tygasoftCommon.AppKey + '","UserName":"' + userName + '","DeviceId":"' + jDeviceInfo.UUID + '","Latlng":"' + jDeviceInfo.Latlng + '","Password":"' + password + '"}}';

            $ionicLoading.show();
            $http.defaults.headers.post['Content-Type'] = 'application/json; charset=utf-8';
            $http({
                method: 'POST',
                url: url,
                data: sData
            }).then(function (res) {
                $ionicLoading.hide();
                var result = res.data;
                if (result.ResCode != 1000) {
                    $cordovaToast.showShortCenter(result.Msg);
                    return false;
                }
                ts.SaveLoginInfo(jDeviceInfo);
                $scope.onCloseLogin();
                $cordovaToast.showShortCenter($tygasoftMC.MC.Response_Login_Ok);
            }, function (err) {
                $ionicLoading.hide();
                $cordovaToast.showShortCenter($tygasoftMC.MC.Http_Err);
            });
        })
    };

    ts.DoRegister = function ($scope) {
        var userName = $scope.registerData.UserName;
        if (!userName) {
            $cordovaToast.showShortCenter($tygasoftMC.MC.Required_Phone);
            return false;
        }
        var password = $scope.registerData.Password;
        if (!password) {
            $cordovaToast.showShortCenter($tygasoftMC.MC.Required_Password);
            return false;
        }
        var sCfmPassword = $scope.registerData.CfmPassword;
        if (password != sCfmPassword) {
            $cordovaToast.showShortCenter($tygasoftMC.MC.Required_CfmPassword);
            return false;
        }

        ts.GetLoginInfo().then(function (jDeviceInfo) {
            if (!jDeviceInfo) {
                $cordovaToast.showShortCenter($tygasoftMC.MC.Data_Error);
                return false;
            }
            jDeviceInfo.UserName = userName;

            var url = $tygasoftCommon.ServerUrl() + "/services/MobileService.svc/Register";
            var sData = '{"model":{"AppKey":"' + $tygasoftCommon.AppKey + '","UserName":"' + userName + '","DeviceId":"' + jDeviceInfo.UUID + '","Latlng":"' + jDeviceInfo.Latlng + '","Password":"' + password + '"}}';

            $ionicLoading.show();
            $http.defaults.headers.post['Content-Type'] = 'application/json; charset=utf-8';
            $http({
                method: 'POST',
                url: url,
                data: sData
            }).then(function (res) {
                $ionicLoading.hide();
                var result = res.data;
                if (result.ResCode != 1000) {
                    $cordovaToast.showShortCenter(result.Msg);
                    return false;
                }
                ts.SaveLoginInfo(jDeviceInfo);
                $scope.onCloseRegister();
                $cordovaToast.showShortCenter($tygasoftMC.MC.Response_Register_Ok);
            }, function (err) {
                $ionicLoading.hide();
                $cordovaToast.showShortCenter($tygasoftMC.MC.Http_Err);
            });
        })
    };

    ts.SaveLoginInfo = function (jDeviceInfo) {
        $tygasoftDbHelper.DoInsert('KeyValue', jDeviceInfo.UserName, $tygasoftMC.DataStatus.Update, 'DeviceInfo', JSON.stringify(jDeviceInfo), true);
    };

    ts.ExitApp = function () {
        $ionicPopup.confirm({
            title: $tygasoftMC.MC.Alert_Title,
            template: $tygasoftMC.MC.M_ExitApp_Content,
            cancelText: $tygasoftMC.MC.Btn_CancelText,
            okText: $tygasoftMC.MC.Btn_OkText
        }).then(function (res) {
            if (res) {
                ionic.Platform.exitApp();
            }
        })
    };

    return ts;
});