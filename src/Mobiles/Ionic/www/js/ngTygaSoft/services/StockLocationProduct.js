angular.module('ngTygaSoft.services.StockLocationProduct', [])

.factory('$tygasoftStockLocationProduct', function ($http, $timeout, $ionicModal, $ionicPopup, $ionicLoading, $ionicActionSheet, $cordovaToast, $tygasoftDbHelper, $tygasoftCommon, $tygasoftMC, $tygasoftLogin) {

    var ts = {};

    ts.Bind = function ($scope) {
        $scope.$on('$ionicView.enter', function (e) {
            if ($scope.keyName == 'PandianProduct') {
                $scope.ModelData.StayQtyTitle = '账面数';
                $scope.ItemInfo.RemainQty = $scope.ItemInfo.StayQty;
                ts.GetStockLocationProductsByPdp($scope);
            }
            else {
                $scope.ItemInfo.RemainQty = $scope.ItemInfo.StayQty - $scope.ItemInfo.Qty;
                var customerId = !$scope.ItemInfo.CustomerId ? null : $scope.ItemInfo.CustomerId;
                var qty = $scope.ItemInfo.StayQty - $scope.ItemInfo.Qty;
                ts.GetStockLocationProductList($scope, $tygasoftCommon.PageIndex, $tygasoftCommon.PageSize, $scope.keyName, $scope.ItemInfo.ProductId, qty, customerId);
            }
        });

        $scope.onRowSelect = function (item) {
            $scope.SelectItem = item;
            $scope.ModelData.StockLocationCode = item.StockLocationCode;
            $scope.ModelData.Qty = item.Qty == 0 ? null : item.Qty;
        };

        $scope.onStockLocationCodeChanged = function () {
            ts.OnStockLocationCodeChanged($scope);
        };

        $scope.onSure = function () {
            ts.OnSure($scope);
        };

        $scope.onToggleMenu = function () {
            ts.OnToggleMenu($scope);
        };
    };

    ts.OnToggleMenu = function ($scope) {
        var btnMenus = [
            { text: '<i class="icon ion-plus-circled positive"></i> 提交' },
            { text: '<i class="icon ion-minus-circled assertive"></i> 取消' }
        ];
        $ionicActionSheet.show({
            buttons: btnMenus,
            buttonClicked: function (index) {
                switch (index) {
                    case 0:
                        ts.OnSave($scope);
                        return true;
                    default:
                        return true;
                }
                return true;
            }
        });
    };

    ts.BindSlpBySmp = function ($scope) {
        ts.GetStockLocationProductList($scope, $tygasoftCommon.PageIndex, $tygasoftCommon.PageSize, 'ShelfMissionProduct', $scope.ItemInfo.ProductId, $scope.ItemInfo.Qty);
    };

    ts.GetStockLocationProductList = function ($scope, pageIndex, pageSize, keyName, productId, qty, customerId) {
        $ionicLoading.show();
        var url = "" + $tygasoftCommon.ServerUrl() + "/Services/PdaService.svc/GetStockLocationProductList";
        var sData = '{"model":{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","KeyName":"' + keyName + '","ProductId":"' + productId + '","Qty":"' + qty + '","CustomerId":"' + customerId + '"}}';
        //console.log('GetStockLocationProductList--sData--' + sData);
        $http.defaults.headers.post['Content-Type'] = 'application/json; charset=utf-8';
        $http({
            method: 'POST',
            url: url,
            data: sData
        }).then(function (res) {
            $ionicLoading.hide();
            var result = res.data;
            //console.log('GetStockLocationProductList--result--' + JSON.stringify(result));
            if (result.ResCode != 1000) {
                $ionicPopup.alert({title: $tygasoftMC.MC.Alert_Title,template: result.Msg,okText: $tygasoftMC.MC.Btn_OkText});
                return false;
            }
            var slBestData = [];
            var slOtherData = [];

            if (keyName == 'ShelfMissionProduct' || 'OrderPickProduct') {
                var jData = JSON.parse(result.Data);
                for (var i = 0; i < jData.length; i++) {
                    var item = jData[i];
                    if (item.IsBest) slBestData.push(item);
                    else slOtherData.push(item);
                }
                $scope.SlBestData = slBestData;
                $scope.SlOtherData = slOtherData;
            }

            $scope.ModelData.TotalBest = slBestData.length;
            $scope.ModelData.TotalOther = slOtherData.length;

        }, function (err) {
            $ionicLoading.hide();
            alert($tygasoftMC.MC.Http_Err);
        });
    };

    ts.GetStockLocationProductsByPdp = function ($scope) {
        console.log('GetStockLocationProductsByPdp--');
        var jData = $scope.ItemInfo.StockLocations == '' ? [] : $scope.ItemInfo.StockLocations;
        $scope.SlBestData = $scope.ItemInfo.UpdatedStockLocations == '' ? jData : $scope.ItemInfo.UpdatedStockLocations;
    };

    ts.OnStockLocationCodeChanged = function ($scope) {
        var barcode = $scope.ModelData.StockLocationCode;
        if (barcode && barcode.Trim() != '') {
            $scope.SelectItem = null;
            for (var i = 0; i < $scope.SlBestData.length; i++) {
                var item = $scope.SlBestData[i];
                if (item.StockLocationCode == barcode) {
                    $scope.SelectItem = item;
                }
            }
            if ($scope.keyName != 'PandianProduct') {
                for (var i = 0; i < $scope.SlOtherData.length; i++) {
                    var item = $scope.SlOtherData[i];
                    if (item.StockLocationCode == barcode) {
                        $scope.SelectItem = item;
                    }
                }
            }
        }
    };

    ts.OnSure = function ($scope) {
        
        var sStockLocationCode = $scope.ModelData.StockLocationCode;
        if (!sStockLocationCode || sStockLocationCode == '') {
            $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.M_Required_Error, okText: $tygasoftMC.MC.Btn_OkText });
            return false;
        }
        if (!$scope.SelectItem) {
            $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.GetString('Params_NotExist', sStockLocationCode), okText: $tygasoftMC.MC.Btn_OkText });
            return false;
        }
        var qty = $scope.ModelData.Qty;
        if (!qty || qty == 0) {
            $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.M_Required_Error, okText: $tygasoftMC.MC.Btn_OkText });
            return false;
        }
        if ($scope.keyName != 'PandianProduct') {
            if (parseInt(qty) > parseInt($scope.ItemInfo.RemainQty)) {
                $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.M_QtyInvalidError, okText: $tygasoftMC.MC.Btn_OkText });
                return false;
            }
        }
        if ($scope.keyName == 'PandianProduct') {
            $scope.SelectItem.IsChanged = $scope.SelectItem.Qty != qty;
        }
        $scope.SelectItem.Qty = qty;

        if ($scope.keyName != 'PandianProduct') {
            ts.ResetRemainQty($scope);
            ts.ResetFm($scope);
        }
        //console.log('$scope.SelectItem--' + JSON.stringify($scope.SelectItem));
        var dlgShow = $ionicPopup.show({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.Response_Ok, okText: $tygasoftMC.MC.Btn_OkText });
        setTimeout(function () {
            dlgShow.close();
        }, 1000);
    };

    ts.OnSave = function ($scope) {
        if (!$scope.SlBestData || $scope.SlBestData.length == 0) return false;
        if ($scope.keyName == 'PandianProduct') {
            ts.SavePdp($scope);
        }
        else {
            var sAppend = '';
            var appendIndex = 0;
            for (var i = 0; i < $scope.SlBestData.length; i++) {
                var row = $scope.SlBestData[i];
                if (row.Qty && row.Qty > 0) {
                    if (appendIndex > 0) sAppend += '|';
                    sAppend += '' + row.StockLocationId + ',' + row.Qty + '';
                    appendIndex++;
                }
            }
            for (var i = 0; i < $scope.SlOtherData.length; i++) {
                var row = $scope.SlOtherData[i];
                if (row.Qty && row.Qty > 0) {
                    if (appendIndex > 0) sAppend += '|';
                    sAppend += '' + row.StockLocationId + ',' + row.Qty + '';
                    appendIndex++;
                }
            }
            if (sAppend == '') {
                $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.Data_SaveEmptyError, okText: $tygasoftMC.MC.Btn_OkText });
                return false;
            }
            switch ($scope.keyName) {
                case 'ShelfMissionProduct':
                    sAppend = '' + $scope.ItemInfo.ShelfMissionId + '$' + $scope.ItemInfo.OrderId + '$' + $scope.ItemInfo.ProductId + '$' + sAppend + '';
                    break;
                case 'OrderPickProduct':
                    sAppend = '' + $scope.ItemInfo.OrderPickId + '$' + $scope.ItemInfo.OrderId + '$' + $scope.ItemInfo.ProductId + '$' + $scope.ItemInfo.CustomerId + '$' + sAppend + '';
                    break;
                default:
                    break;
            }

            var sData = { "itemAppend": "" + sAppend + "" };
            //console.log('sAppend--' + sAppend);
            //return false;
            ts.Save($scope, sData);
        }
    };

    ts.Save = function ($scope, sData) {
        var saveMethod = '';
        switch ($scope.keyName) {
            case 'ShelfMissionProduct':
                saveMethod = 'SaveShelfMissionProduct';
                break;
            case 'OrderPickProduct':
                saveMethod = 'SaveOrderPickProduct';
            default:
                break;
        }
        var sUrl = "" + $tygasoftCommon.ServerUrl() + "/Services/PdaService.svc/" + saveMethod + "";

        $ionicLoading.show();
        $http.defaults.headers.post['Content-Type'] = 'application/json; charset=utf-8';
        $http({
            method: 'POST',
            url: sUrl,
            data: sData
        }).then(function (res) {
            $ionicLoading.hide();
            var result = res.data;
            //console.log('result--' + JSON.stringify(result));
            if (result.ResCode != 1000) {
                $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: result.Msg, okText: $tygasoftMC.MC.Btn_OkText });
                return false;
            }
            
            ts.ResetSave($scope);
            $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.Response_Ok, okText: $tygasoftMC.MC.Btn_OkText });
        }, function (err) {
            $ionicLoading.hide();
            alert($tygasoftMC.MC.Http_Err);
        });
    };

    ts.ResetSave = function ($scope) {
        var totalQty = 0;
        for (var i = 0; i < $scope.SlBestData.length; i++) {
            var row = $scope.SlBestData[i];
            if (row.Qty && row.Qty > 0) {
                totalQty += parseInt(row.Qty);
                row.Qty = 0;
            }
        }
        for (var i = 0; i < $scope.SlOtherData.length; i++) {
            var row = $scope.SlOtherData[i];
            if (row.Qty && row.Qty > 0) {
                totalQty += parseInt(row.Qty);
                row.Qty = 0;
            }
        }
        $scope.ItemInfo.Qty += totalQty;
    };

    ts.ResetFm = function ($scope) {
        $scope.ModelData.StockLocationCode = '';
        $scope.ModelData.Qty = '';
    };

    ts.ResetRemainQty = function ($scope) {
        var totalQty = 0;
        for (var i = 0; i < $scope.SlBestData.length; i++) {
            var row = $scope.SlBestData[i];
            if (row.Qty && row.Qty > 0) {
                totalQty += +row.Qty;
            }
        }
        for (var i = 0; i < $scope.SlOtherData.length; i++) {
            var row = $scope.SlOtherData[i];
            if (row.Qty && row.Qty > 0) {
                totalQty += +row.Qty;
            }
        }
        $scope.ItemInfo.RemainQty = ($scope.ItemInfo.StayQty - $scope.ItemInfo.Qty - totalQty);
    };

    ts.SavePdp = function ($scope) {
        var jData = [];
        var qty = 0;
        var isChanged = false;
        for (var i = 0; i < $scope.SlBestData.length; i++) {
            var row = $scope.SlBestData[i];
            qty += parseFloat(row.Qty);
            jData.push({ "StockLocationId": row.StockLocationId, "StockLocationCode": row.StockLocationCode, "StockLocationName": row.StockLocationName, "Qty": row.Qty });
            if (row.IsChanged) {
                isChanged = true;
            }
        }
        if (!isChanged && ($scope.ItemInfo.Qty == qty)) {
            $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.Data_SaveEmptyError, okText: $tygasoftMC.MC.Btn_OkText });
            return false;
        }
        //console.log('jData--' + JSON.stringify(jData));
        var sData = '{"model":{"PandianId":"' + $scope.ItemInfo.PandianId + '","ProductId":"' + $scope.ItemInfo.ProductId + '","CustomerId":"' + $scope.ItemInfo.CustomerId + '","Zones":"' + $scope.ItemInfo.Zones + '","StockLocations":"' + encodeURIComponent(JSON.stringify(jData)) + '","Qty":"' + qty + '"}}';
        var sUrl = "" + $tygasoftCommon.ServerUrl() + "/Services/PdaService.svc/SavePandianProduct";
        //console.log('sData--' + sData);
        $ionicLoading.show();
        $http.defaults.headers.post['Content-Type'] = 'application/json; charset=utf-8';
        $http({
            method: 'POST',
            url: sUrl,
            data: sData
        }).then(function (res) {
            $ionicLoading.hide();
            var result = res.data;
            //console.log('result--' + JSON.stringify(result));
            if (result.ResCode != 1000) {
                $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: result.Msg, okText: $tygasoftMC.MC.Btn_OkText });
                return false;
            }

            $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.Response_Ok, okText: $tygasoftMC.MC.Btn_OkText });
        }, function (err) {
            $ionicLoading.hide();
            alert($tygasoftMC.MC.Http_Err);
        });
    };

    return ts;
});