angular.module('ngTygaSoft.services.SysSet', [])

.factory('$tygasoftSysSet', function ($http, $ionicLoading, $ionicPopup, $ionicModal, ionicDatePicker, $tygasoftLocalStorage, $tygasoftMC, $tygasoftCommon) {

    var ts = {};

    ts.Bind = function ($scope) {
        $scope.ModelData.UhfOnOff = parseInt($tygasoftLocalStorage.Get("UhfOnOff", "0")) == 1;
        $scope.onUhfChange = function () {
            $scope.ModelData.UhfOnOff ? ts.UhfOn() : ts.UhfOff();
        }
        $scope.onSave = function () {
            if (!$scope.ModelData.ServiceUrl || $scope.ModelData.ServiceUrl == '') {
                $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.M_EmptyError, okText: $tygasoftMC.MC.Btn_OkText });
                return false;
            }
            $ionicPopup.confirm({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.M_SaveConfirm, cancelText: $tygasoftMC.MC.Btn_CancelText, okText: $tygasoftMC.MC.Btn_OkText }).then(function (r) {
                if (r) {
                    $tygasoftLocalStorage.Set("ServiceUrl", $scope.ModelData.ServiceUrl);
                    $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.Response_Ok, okText: $tygasoftMC.MC.Btn_OkText });
                }
            })
        }
    };

    ts.UhfOn = function () {
        RfidScan.onPause(0);
        RfidScan.onScan();
        $tygasoftLocalStorage.Set("UhfOnOff", 1);
    };

    ts.UhfOff = function () {
        RfidScan.onPause(1);
        $tygasoftLocalStorage.Set("UhfOnOff", 0);
    };

    return ts;
});