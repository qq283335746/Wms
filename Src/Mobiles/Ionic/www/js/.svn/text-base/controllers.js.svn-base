angular.module('starter.controllers', [])

.controller('ShelfMissionCtrl', function ($scope, $tygasoftShelfMission) {
    $tygasoftShelfMission.Bind($scope);
})

.controller('ShelfMissionProductCtrl', function ($scope, $stateParams, $timeout, $ionicLoading, $tygasoftShelfMission) {

    $scope.ModelData = { "Barcode": "" };
    $scope.ShelfMissionInfo = JSON.parse($stateParams.item);
    $scope.SelectItem = {};

    $scope.$on('$ionicView.enter', function (e) {
        $ionicLoading.show();
        $timeout(function () {
            $tygasoftShelfMission.GetShelfMissionProduct($scope);
        }, 500).then(function () {
            $ionicLoading.hide();
        });
    });

    $scope.btnTabIndex = 0;
    $scope.onTabSelected = function (index) {
        $scope.btnTabIndex = index;
    };
    $scope.onBarcodeChanged = function () {
        if ($scope.btnTabIndex == 0) {
            $tygasoftShelfMission.GetBarcode($scope);
        }
    };
    $scope.onSure = function () {
        $tygasoftShelfMission.GetBarcode($scope);
    };
    $scope.onGoTo = function (item) {
        item.StockLocations = JSON.parse(item.StockLocations);
        window.location = "#/tab/StockLocationProduct/" + JSON.stringify(item) + "/ShelfMissionProduct";
    };
    $scope.toggleGroup = function (group) {
        if ($scope.isGroupShown(group)) {
            $scope.shownGroup = null;
        } else {
            $scope.shownGroup = group;
        }
    };
    $scope.isGroupShown = function (group) {
        return $scope.shownGroup === group;
    };
})

.controller('StockLocationProductCtrl', function ($scope, $stateParams, $tygasoftStockLocationProduct) {
    $scope.keyName = $stateParams.key;
    $scope.ItemInfo = JSON.parse($stateParams.item);
    $scope.ModelData = { "StayQtyTitle": "准备数量", "TotalBest": 0, "TotalOther": 0 };
    $scope.SelectItem = null;

    $tygasoftStockLocationProduct.Bind($scope);

    $scope.toggleGroup = function (group) {
        if ($scope.isGroupShown(group)) {
            $scope.shownGroup = null;
        } else {
            $scope.shownGroup = group;
        }
    };
    $scope.isGroupShown = function (group) {
        return $scope.shownGroup === group;
    };

    $scope.slideIndex = 0;
    $scope.activeSlide = function (index) {
        $scope.slideIndex = index;
    }
})

.controller('OrderPickedCtrl', function ($scope, $tygasoftOrderPicked) {
    $tygasoftOrderPicked.Bind($scope);
})

.controller('OrderPickProductCtrl', function ($scope, $stateParams, $timeout, $ionicLoading, $tygasoftCommon, $tygasoftOrderPicked) {

    $scope.ModelData = { "Barcode": "" };
    $scope.OrderPickInfo = JSON.parse($stateParams.item);
    $scope.$on('$ionicView.enter', function (e) {
        $ionicLoading.show();
        $timeout(function () {
            $tygasoftOrderPicked.GetOrderPickProduct($scope, $tygasoftCommon.PageIndex, $tygasoftCommon.PageSize, $scope.OrderPickInfo.Id);
        }, 500).then(function () {
            $ionicLoading.hide();
        });
    });

    $scope.btnTabIndex = 0;
    $scope.onTabSelected = function (index) {
        $scope.btnTabIndex = index;
    };
    $scope.onBarcodeChanged = function () {
        if ($scope.btnTabIndex == 0) {
            $tygasoftOrderPicked.GetBarcode($scope);
        }
    };
    $scope.onSure = function () {
        $tygasoftOrderPicked.GetBarcode($scope);
    };
    $scope.toHref = function (item) {
        window.location = '#/tab/StockLocationProduct/' + JSON.stringify(item) + '/OrderPickProduct';
    };
    $scope.toggleGroup = function (group) {
        if ($scope.isGroupShown(group)) {
            $scope.shownGroup = null;
        } else {
            $scope.shownGroup = group;
        }
    };
    $scope.isGroupShown = function (group) {
        return $scope.shownGroup === group;
    };
})

.controller('StockProductCtrl', function ($scope, $tygasoftStockProduct) {
    $scope.ModelData = { "StartDate": "请选择", "EndDate": "请选择", "Keyword": "" };
    $tygasoftStockProduct.Bind($scope);

    $scope.toggleGroup = function (group) {
        if ($scope.isGroupShown(group)) {
            $scope.shownGroup = null;
        } else {
            $scope.shownGroup = group;
        }
    };
    $scope.isGroupShown = function (group) {
        return $scope.shownGroup === group;
    };
})

.controller('PandianCtrl', function ($scope, $tygasoftPandian) {
    $tygasoftPandian.Bind($scope);
})

.controller('PandianProductCtrl', function ($scope, $stateParams, $timeout, $ionicLoading, $tygasoftCommon, $tygasoftPandian) {

    //$scope.DataItem = '{"PandianId":"a0b825cb-b20d-4a91-b7f9-e55da14f4ae3","UserId":"0c5d2aae-264e-4398-ae51-28a44ce238a6","ProductId":"96376f26-1f69-4e81-832b-34be4c459672","CustomerId":"6ebfdcc6-1f13-4bc9-bba0-1ead409d5415","Zones":"FinishedZ","StockLocations":"[{\"StockLocationId\":\"25f53e2e-3b5f-4875-a22f-9dc0e928507e\",\"StockLocationCode\":\"A-01-003\",\"StockLocationName\":\"A-01-003\",\"Qty\":11.0}]","StayQty":11,"UpdatedZones":"FinishedZ","UpdatedStockLocations":"[{\"StockLocationId\":\"25f53e2e-3b5f-4875-a22f-9dc0e928507e\",\"StockLocationCode\":\"A-01-003\",\"StockLocationName\":\"A-01-003\",\"Qty\":\"3\"}]","Qty":3,"FailQty":8,"Status":"待完成","Remark":"","LastUpdatedDate":"2016-11-25T17:51:24.887","UserName":"wms","ProductCode":"003.01.002","ProductName":"6903447400805","CustomerCode":"005","CustomerName":"广州慧联信息科技有限公司","StockLocationCodes":"A-01-003"}';
    $scope.ModelData = { "Barcode": "" };
    $scope.PandianInfo = JSON.parse($stateParams.item);
    $scope.$on('$ionicView.enter', function (e) {
        $tygasoftPandian.GetPandianProductList($scope, $tygasoftCommon.PageIndex, $tygasoftCommon.PageSize, $scope.PandianInfo.Id);
    });

    $scope.btnTabIndex = 0;
    $scope.onTabSelected = function (index) {
        $scope.btnTabIndex = index;
    };
    $scope.onBarcodeChanged = function () {
        if ($scope.btnTabIndex == 0) {
            $tygasoftPandian.GetBarcode($scope);
        }
    };
    $scope.onSure = function () {
        $tygasoftPandian.GetBarcode($scope);
    };
    $scope.toHref = function (item) {
        if (item.StockLocations != '') item.StockLocations = JSON.parse(item.StockLocations);
        if (item.UpdatedStockLocations != '') item.UpdatedStockLocations = JSON.parse(item.UpdatedStockLocations);
        window.location = '#/tab/StockLocationProduct/' + JSON.stringify(item) + '/PandianProduct';
    };
    $scope.toggleGroup = function (group) {
        if ($scope.isGroupShown(group)) {
            $scope.shownGroup = null;
        } else {
            $scope.shownGroup = group;
        }
    };
    $scope.isGroupShown = function (group) {
        return $scope.shownGroup === group;
    };
})

.controller('RfidCtrl', function ($scope, $interval, $http, $tygasoftRfid, $tygasoftCommon) {
    var itvRfid = null;
    $scope.CanReadRfid = true;

    $scope.$on('$ionicView.beforeEnter', function (e) {
        $tygasoftRfid.Reset();
    });
    $scope.$on('$ionicView.enter', function (e) {
        $tygasoftRfid.SetResult();
        var rfidItems = [];
        itvRfid = $interval(function () {
            if (!$scope.CanReadRfid) return;
            $scope.CanReadRfid = false;
            var rfids = $tygasoftRfid.GetRfidItems();
            for (var i = 0; i < rfids.length; i++) {
                rfidItems.push(rfids[i]);
                $scope.Items = rfidItems;
                $scope.DoRfid(rfids[i].Name);
            }
            $scope.CanReadRfid = true;
        }, 1000);
    });
    $scope.$on('$ionicView.leave', function (e) {
        //clearInterval(itvRfid);
        $interval.cancel(itvRfid);
        $tygasoftRfid.Reset();
    });

    $scope.DoRfid = function (rfid) {
        var postData = '{"model":{"TID":"' + rfid + '","EPC":"' + rfid + '"}}';
        var sUrl = "" + $tygasoftCommon.ServerUrl() + "/Services/PdaService.svc/SaveRFIDQueue";
        $http.defaults.headers.post['Content-Type'] = 'application/json; charset=utf-8';
        $http({
            method: 'POST',
            url: sUrl,
            data: postData
        }).then(function (res) {
            var result = res.data;
        }, function (err) {
        });
    };
})

.controller('SysSetCtrl', function ($scope, $tygasoftLocalStorage, $tygasoftSysSet) {
    $scope.ModelData = { "ServiceUrl": "" + $tygasoftLocalStorage.Get("ServiceUrl", "") + "", "UhfOnOff": "checked" };
    $tygasoftSysSet.Bind($scope);
})

.controller('AppPackageCtrl', function ($scope) {
    $scope.ListData = [{ "Name": "wms-release-v142.apk", "Href": "templates/Download/wms-release-v142.apk", "Descr": "下载" }];
})

.controller('ListImportDataCtrl', function ($scope, $tygasoftRemoteData) {
    $tygasoftRemoteData.Bind($scope);
})

.controller('HomeCtrl', function ($scope, $tygasoftHome) {
    $tygasoftHome.Bind($scope);
})

.controller('FoundCtrl', function ($scope, $tygasoftFound) {
    $tygasoftFound.Bind($scope);
})

.controller('SysCtrl', function ($scope, $tygasoftSys) {
    $tygasoftSys.Bind($scope);
});
