angular.module('ngTygaSoft.services.StockProduct', [])

.factory('$tygasoftStockProduct', function ($http, $timeout, $ionicModal, $ionicPopup, $ionicLoading, $ionicActionSheet, ionicDatePicker, $cordovaToast, $tygasoftDbHelper, $tygasoftCommon, $tygasoftMC, $tygasoftLogin) {

    var ts = {};

    ts.Bind = function ($scope) {
        $scope.$on('$ionicView.enter', function (e) {
            ts.GetStockProductList($scope);
        });

        $scope.onDpDate = function (n) {
            ionicDatePicker.openDatePicker({
                callback: function (val) {
                    var sDate = new Date(val).Format("yyyy-MM-dd");
                    switch (n) {
                        case '1':
                            $scope.ModelData.StartDate = sDate;
                            break;
                        case '2':
                            $scope.ModelData.EndDate = sDate;
                            break;
                        default:
                            break;
                    }
                }
            });
        };

        $scope.onSearch = function () {
            ts.GetStockProductList($scope);
        };
    };

    ts.GetStockProductList = function ($scope) {
        try {
            var url = "" + $tygasoftCommon.ServerUrl() + "/Services/PdaService.svc/GetStockProductList";
            var sData = '{"model":{"PageIndex":"' + $tygasoftCommon.PageIndex + '","PageSize":"' + $tygasoftCommon.PageSize + '","StartDate":"' + $scope.ModelData.StartDate.replace('请选择', '') + '","EndDate":"' + $scope.ModelData.EndDate.replace('请选择', '') + '","Keyword":"' + $scope.ModelData.Keyword + '"}}';

            $ionicLoading.show();
            $http.defaults.headers.post['Content-Type'] = 'application/json; charset=utf-8';
            $http({
                method: 'POST',
                url: url,
                data: sData
            }).then(function (res) {
                $ionicLoading.hide();
                var result = res.data;
                //console.log('GetStockProductList--result--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: result.Msg, okText: $tygasoftMC.MC.Btn_OkText });
                    return false;
                }
                $scope.StockProductData = JSON.parse(result.Data);

            }, function (err) {
                $ionicLoading.hide();
                alert($tygasoftMC.MC.Http_Err);
            });
        }
        catch (e) {
            $ionicLoading.hide();
            alert($tygasoftMC.MC.Http_Err);
        }
    };

    return ts;
});