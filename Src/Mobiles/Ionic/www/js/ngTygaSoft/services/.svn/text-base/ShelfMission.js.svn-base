angular.module('ngTygaSoft.services.ShelfMission', [])

.factory('$tygasoftShelfMission', function ($http, $timeout, $ionicModal, $ionicPopup, $ionicLoading, $ionicActionSheet, $cordovaToast, $tygasoftDbHelper, $tygasoftCommon, $tygasoftMC, $tygasoftLogin) {

    var ts = {};

    ts.Bind = function ($scope) {
        $scope.$on('$ionicView.enter', function (e) {
            ts.GetShelfMissionList($scope);
        });
        $scope.getStyle = function (item) {
            if (item.Status != '已完成') return 'badge badge-positive fr mgr40';
            return 'fr';
        };
        
    };

    ts.OnToggleMenu = function ($scope) {
        var btnMenus = [
            { text: '<i class="icon ion-plus-circled positive"></i> 上传到服务器' },
            { text: '<i class="icon ion-minus-circled assertive"></i> 取消' }
        ];
        $ionicActionSheet.show({
            buttons: btnMenus,
            buttonClicked: function (index) {
                switch (index) {
                    case 0:
                        ts.DoSaveToServer();
                        return true;
                    default:
                        return true;
                }
                return true;
            }
        });
    };

    ts.ToStockLocationAppend = function ($scope, StockLocationList) {
        var sAppend = '';
        for (var i = 0; i < StockLocationList.length; i++) {
            if (i > 0) sAppend += ',';
            sAppend += StockLocationList[i].StockLocationCode;
        }
        $scope.ModelData.StockLocationAppend = sAppend;
    };

    ts.GetShelfMissionList = function ($scope) {
        try {
            var url = "" + $tygasoftCommon.ServerUrl() + "/Services/PdaService.svc/GetShelfMissionList";
            var sData = '{"model":{"PageIndex":"' + $tygasoftCommon.PageIndex + '","PageSize":"' + $tygasoftCommon.PageSize + '"}}';

            $ionicLoading.show();
            $http.defaults.headers.post['Content-Type'] = 'application/json; charset=utf-8';
            $http({
                method: 'POST',
                url: url,
                data: sData
            }).then(function (res) {
                $ionicLoading.hide();
                var result = res.data;
                //console.log('GetShelfMissionList--result--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    $ionicPopup.alert({
                        title: $tygasoftMC.MC.Alert_Title,
                        template: result.Msg,
                        okText: $tygasoftMC.MC.Btn_OkText
                    });
                    return false;
                }
                $scope.ListData = JSON.parse(result.Data);

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

    ts.GetShelfMissionProduct = function ($scope) {

        var url = "" + $tygasoftCommon.ServerUrl() + "/Services/PdaService.svc/GetShelfMissionProductList";
        var sData = '{"model":{"PageIndex":"' + $tygasoftCommon.PageIndex + '","PageSize":"' + $tygasoftCommon.PageSize + '","ShelfMissionId":"' + $scope.ShelfMissionInfo.Id + '"}}';
        $http.defaults.headers.post['Content-Type'] = 'application/json; charset=utf-8';
        $http({
            method: 'POST',
            url: url,
            data: sData
        }).then(function (res) {
            var result = res.data;
            //console.log('GetShelfMissionProduct--result--' + JSON.stringify(result));
            if (result.ResCode != 1000) {
                $ionicPopup.alert({title: $tygasoftMC.MC.Alert_Title,template: result.Msg,okText: $tygasoftMC.MC.Btn_OkText});
                return false;
            }
            var jData = JSON.parse(result.Data);
            for (var i = 0; i < jData.length; i++) {
                jData[i].StockLocationsName = ts.GetStockLocationsName(jData[i].StockLocations);
            }
            $scope.ShelfMissionProductData = jData;

        }, function (err) {
            alert($tygasoftMC.MC.Http_Err);
        });

        //var jData = [];
        //for (var i = 0; i < 10; i++) {
        //    var item = { "Id": "" + i + "", "OrderCode": "2016071100" + i + "", "OrderType": "类型" + i + "", "OrderDate": "2016-07-11", "OutStore": "发货仓库" + i + "", "ReceiptStore": "收货仓库" + i + "", "Qty": "" + i + "", "OutQty": "" + i + "", "OutStorePerson": "出库人" + i + "", "OutStoreDate": "2016-07-11", "Remark": "备注" + i + "" };
        //    jData.push(item);
        //}
        //$scope.ListData = jData;
        //return false;

        //$tygasoftDbHelper.GetAll("ShelfMissionProduct").then(function (res) {
        //    if (res) {
        //        for (var i = 0; i < res.length; i++) {
        //            alert(res.item(i).KeyName + res.item(i).ContentValue);
        //        }
        //    }
        //})

        //ts.GetShelfMissionProductByScanning($scope, $scope.ShelfMissionInfo.Id);
        //ts.GetShelfMissionProductByScanned($scope, $scope.ShelfMissionInfo.Id);

        //$scope.onInputByBarcode = function () {
        //    var barcode = $scope.ModelData.Barcode;
        //    if (barcode && barcode.Trim() != '') {
        //        for (var i = 0; i < $scope.ListScanning.length; i++) {
        //            var item = $scope.ListScanning[i];
        //            if (item.ProductCode == barcode) {
        //                $scope.ModelData.Qty = item.Qty;
        //                ts.ToStockLocationAppend($scope, item.StockLocationList);
        //            }
        //        }
        //    }
        //};
    };

    ts.GetStockLocationsName = function (stockLocations) {
        var s = '';
        var jStockLocations = JSON.parse(stockLocations);
        for (var i = 0; i < jStockLocations.length; i++) {
            if (i > 0) s += '，';
            s += jStockLocations[i].StockLocationCode;
        }
        return s;
    };

    ts.GetBarcode = function ($scope) {
        var barcode = $scope.ModelData.Barcode;
        if (!barcode || barcode == '') {
            return false;
        }
        
        $scope.ModelData.Barcode = '';
        $scope.SelectItem = {};
        for (var i = 0; i < $scope.ShelfMissionProductData.length; i++) {
            var item = $scope.ShelfMissionProductData[i];
            if (item.ProductCode == barcode) {
                $scope.SelectItem = item;
            }
        }
        if (!$scope.SelectItem.ProductCode) {
            $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.GetString('Params_NotExist', barcode), okText: $tygasoftMC.MC.Btn_OkText });
            return false;
        }
        $scope.SelectItem.StockLocations = JSON.parse($scope.SelectItem.StockLocations);
        //console.log(JSON.stringify($scope.SelectItem));
        window.location = '#/tab/StockLocationProduct/' + JSON.stringify($scope.SelectItem) + '/ShelfMissionProduct';
    };

    ts.GetById = function ($scope, Id) {
        $tygasoftDbHelper.GetValueByKey("ShelfMission", Id).then(function (res) {
            if (res) {
                $scope.ShelfMissionInfo = JSON.parse(res);
                //alert('$scope.ShelfMissionInfo--res--' + $scope.ShelfMissionInfo.ReceiptOrderNo);
            }
        })
    };

    ts.GetShelfMissionProductByScanning = function ($scope,shelfMissionId) {

        var sqlWhere = "and KeyName like '%" + shelfMissionId + "%' and ContentValue like '%\"Status\":2%'";
        //alert('Scanning--sqlWhere--' + sqlWhere);
        $tygasoftDbHelper.ExecuteReader("ShelfMissionProduct", sqlWhere).then(function (res) {
            var list = [];
            if (res) {
                for (var i = 0; i < res.length; i++) {
                    //alert('res.item(i).ContentValue99--' + res.item(i).ContentValue);
                    list.push(JSON.parse(res.item(i).ContentValue));
                }
            }
            $scope.ListScanning = list;
            $scope.ModelData.TotalScanning = $scope.ListScanning.length;
        })
    };

    ts.GetShelfMissionProductByScanned = function ($scope, shelfMissionId) {
        var sqlWhere = "and KeyName like '%" + shelfMissionId + "%' and ContentValue like '%\"Status\":3%'";
        //alert('Scanned--sqlWhere--' + sqlWhere);
        $tygasoftDbHelper.ExecuteReader("ShelfMissionProduct", sqlWhere).then(function (res) {
            if (res) {
                var list = [];
                for (var i = 0; i < res.length; i++) {
                    //alert('res.item(i).ContentValue--'+res.item(i).ContentValue);
                    list.push(JSON.parse(res.item(i).ContentValue));
                }
                $scope.ListScanned = list;
                $scope.ModelData.TotalScanned = $scope.ListScanned.length;
            }
        })
    };

    ts.DoScan = function ($scope) {

        var barcode = $scope.ModelData.Barcode;
        if (!barcode || barcode.Trim() == '') {
            $cordovaToast.showLongCenter($tygasoftMC.MC.M_Required_Error);
            return false;
        }
        var qty = $scope.ModelData.Qty;
        if (!qty || qty < 1) {
            $cordovaToast.showLongCenter($tygasoftMC.MC.M_Required_Error);
            return false;
        }
        qty = parseInt(qty);
        var sStockLocation = $scope.ModelData.StockLocation;
        if (!sStockLocation || sStockLocation == '') {
            sStockLocation = $scope.ModelData.StockLocationAppend;
        }
        if (!sStockLocation || sStockLocation == '') {
            $cordovaToast.showLongCenter($tygasoftMC.MC.M_StockLocation_EmptyError);
            return false;
        }

        var sqlWhere = "and KeyName like '%" + $scope.ShelfMissionInfo.Id + "%' and ContentValue like '%\"ProductCode\":\"" + barcode + "\"%' and ContentValue like '%\"Status\":2%'";
        $tygasoftDbHelper.ExecuteReader("ShelfMissionProduct", sqlWhere).then(function (res) {
            if (!res) {
                $cordovaToast.showLongCenter($tygasoftMC.GetString('Params_NotExist', barcode));
                return false;
            }
            var scanningRow = JSON.parse(res.item(0).ContentValue);
            scanningRow.Qty = parseInt(scanningRow.Qty) - qty;
            if (scanningRow.Qty < 0) {
                $cordovaToast.showLongCenter('上架数量超出了范围！');
                return false;
            }
            var scanningKey = res.item(0).KeyName;
            var rowId = res.item(0).Id;
            if (scanningRow.Qty == 0) {
                $tygasoftDbHelper.DeleteById('ShelfMissionProduct', rowId);
            }
            else $tygasoftDbHelper.Update('ShelfMissionProduct', '', $tygasoftMC.DataStatus.Update, scanningKey, JSON.stringify(scanningRow));

            var scannedSqlWhere = "and KeyName like '%" + scanningKey + "%' and ContentValue like '%\"ProductCode\":\"" + barcode + "\"%' and ContentValue like '%\"Status\":3%'";
            $tygasoftDbHelper.ExecuteReader("ShelfMissionProduct", scannedSqlWhere).then(function (res2) {
                var scannedRow = null;
                if (res2) {
                    scannedRow = JSON.parse(res2.item(0).ContentValue);
                    scannedRow.Qty = parseInt(scannedRow.Qty) + qty;
                    if (scannedRow.StockLocationList.length < 1) scannedRow.StockLocationList.push({ "StockLocationCode": sStockLocation, "Qty": qty });
                    else {
                        var isExist = false;
                        for (var i = 0; i < scannedRow.StockLocationList.length; i++) {
                            if (scannedRow.StockLocationList[i].StockLocationCode == sStockLocation) {
                                isExist = true;
                                scannedRow.StockLocationList[i].Qty = parseInt(scannedRow.StockLocationList[i].Qty) + qty;
                            }
                        }
                        if (!isExist) scannedRow.StockLocationList.push({ "StockLocationCode": sStockLocation, "Qty": qty });
                    }
                }
                else {
                    scannedRow = scanningRow;
                    scannedRow.Qty = qty;
                    scannedRow.Status = 3;
                    if (scannedRow.StockLocationList.length < 1) scannedRow.StockLocationList.push({ "StockLocationCode": sStockLocation, "Qty": qty });
                    else {
                        var isExist = false;
                        for (var i = 0; i < scannedRow.StockLocationList.length; i++) {
                            if (scannedRow.StockLocationList[i].StockLocationCode == sStockLocation) {
                                isExist = true;
                                scannedRow.StockLocationList[i].Qty = qty;
                            }
                        }
                        if (!isExist) scannedRow.StockLocationList.push({ "StockLocationCode": sStockLocation, "Qty": qty });
                    }
                }

                var scannedKey = scanningKey + '|' + rowId;
                $tygasoftDbHelper.DoInsert('ShelfMissionProduct', '', $tygasoftMC.DataStatus.Scanned, scannedKey, JSON.stringify(scannedRow), true).then(function () {
                    ts.BindShelfMissionProduct($scope);
                    ts.ResetShelfMissionProduct($scope);
                    $cordovaToast.showLongCenter($tygasoftMC.MC.Response_Ok);
                });
            })
        })
    };

    ts.DoSaveToServer = function () {
        $ionicPopup.confirm({
            title: $tygasoftMC.MC.Alert_Title,
            template: $tygasoftMC.MC.Confirm_SaveToServer,
            cancelText: $tygasoftMC.MC.Btn_CancelText,
            okText: $tygasoftMC.MC.Btn_OkText
        }).then(function (isOk) {
            if (isOk) {

                var sqlWhere = 'and Status = "' + $tygasoftMC.DataStatus.Scanned + '"';
                $tygasoftDbHelper.ExecuteReader("ShelfMissionProduct", sqlWhere).then(function (res) {
                    if (!res) {
                        $cordovaToast.showLongCenter($tygasoftMC.MC.Data_ToServer_EmptyError);
                        return false;
                    }
                    var list = [];
                    for (var i = 0; i < res.length; i++) {
                        var jRow = JSON.parse(res.item(i).ContentValue);
                        var keys = res.item(i).KeyName.split('|');
                        var item = { "Id": res.item(i).Id, "ShelfMissionId": "" + keys[0] + "", "ProductId": "" + keys[1] + "", "ProductCode": "", "ProductName": "", "Qty": jRow.Qty, "StockLocationList": jRow.StockLocationList };
                        list.push(item);
                    }
                    $ionicLoading.show();
                    ts.SaveToServer(list, 0);
                })
            }
        })
    };

    ts.SaveToServer = function (list, okCount) {
        if (!list || list.length == 0) {
            $ionicLoading.hide();
            $cordovaToast.showLongCenter($tygasoftMC.MC.Response_Ok);
            return false;
        }
        var maxLen = 20;
        if (list.length < maxLen) maxLen = list.length;
        var newList = [];
        for (var i = 0; i < maxLen; i++) {
            newList.push(list.shift());
            okCount++;
        }

        var url = "" + $tygasoftCommon.ServerUrl() + "/Services/PdaService.svc/SaveBarcodeScanQueue";
        var sData = '{"model":{"From":"上架","ItemBody":"' + encodeURIComponent(JSON.stringify(newList)) + '"}}';
        $http.defaults.headers.post['Content-Type'] = 'application/json; charset=utf-8';
        $http({
            method: 'POST',
            url: url,
            data: sData
        }).then(function (res) {
            var result = res.data;
            if (result.ResCode != 1000) {
                $ionicLoading.hide();
                $cordovaToast.showLongCenter(result.Msg);
                return false;
            }
            var jData = JSON.parse(result.Data);
            if (jData && jData.length > 0) {
                for (var i in jData) {
                    $tygasoftDbHelper.DeleteById('ShelfMissionProduct', jData[i].Id);
                }
                ts.SaveToServer(list, okCount);
            }
        }, function (err) {
            $ionicLoading.hide();
            alert($tygasoftMC.MC.Http_Err);
        });
    };

    return ts;
});