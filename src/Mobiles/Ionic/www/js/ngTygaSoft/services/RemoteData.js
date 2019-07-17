angular.module('ngTygaSoft.services.RemoteData', [])

.factory('$tygasoftRemoteData', function ($http, $timeout, $ionicModal, $ionicPopup, $ionicLoading, $tygasoftDbHelper, $tygasoftCommon, $tygasoftMC, $tygasoftLogin) {

    var ts = {};

    ts.Bind = function ($scope) {

        //var shelfMissionId = '7B0C5A79-F071-4DDF-AD35-F97B68B78E37';
        //var url = "" + $tygasoftCommon.ServerUrl() + "/Services/PdaService.svc/GetShelfMissionProductList";
        //var sData = '{"model":{"PageIndex":"' + 1 + '","PageSize":"' + 111 + '","ShelfMissionId":"' + shelfMissionId + '"}}';

        //$http.defaults.headers.post['Content-Type'] = 'application/json; charset=utf-8';
        //$http({
        //    method: 'POST',
        //    url: url,
        //    data: sData
        //}).then(function (res) {
        //    var result = res.data;
        //    console.log('GetShelfMissionList--result--' + JSON.stringify(result));
        //}, function (err) {
        //    $ionicLoading.hide();
        //    alert($tygasoftMC.MC.Http_Err);
        //});
        //return false;

        $scope.List = [];

        ts.GetList($scope,0);

        $scope.getTableName = function (index) {
            return ts.GetTableName(index);
        }

        $scope.objAddImportData = {};

        $ionicModal.fromTemplateUrl('templates/Sys/AddImportData.html', {
            scope: $scope
        }).then(function (modal) {
            $scope.addImportDataModal = modal;
        });

        $scope.onBack = function () {
            $scope.addImportDataModal.hide();
        }

        $scope.onDlgAddImportData = function (index) {
            $scope.objAddImportData = { "Index":index, "Title": ts.GetTableName(index), "DataType": "Import" };
            $scope.addImportDataModal.show();
        }

        $scope.doSave = function () {
            ts.DoSave($scope, $scope.objAddImportData);
        }
    }

    var dbTable = 'ShelfMission|ShelfMissionProduct';
    var dbTableName = '上架任务表|货物上架表';

    
    ts.GetList = function ($scope,index) {
        var tables = dbTable.split('|');
        if (index >= tables.length) return false;
        var tableName = tables[index];
        $tygasoftDbHelper.GetTotal(tableName).then(function (res) {
            $scope.List.push({ "TotalCount": res });
            index++
            ts.GetList($scope, index);
        });
    };

    ts.GetTableName = function (index) {
        var tables = dbTable.split('|');
        var tableNames = dbTableName.split('|');
        return '' + tableNames[index] + '（' + tables[index] + '）';
    }

    ts.DoSave = function ($scope, data) {
        var msg = '';
        if (data.DataType == 'Import') msg = $tygasoftMC.MC.Confirm_Msg_Import;
        else if (data.DataType == 'Delete') msg = $tygasoftMC.MC.Confirm_Msg_Delete;
        $ionicPopup.confirm({
            title: $tygasoftMC.MC.Alert_Title,
            template: msg,
            cancelText: $tygasoftMC.MC.Btn_CancelText,
            okText: $tygasoftMC.MC.Btn_OkText
        }).then(function (res) {
            if (res) {
                if (data.Index == 0) {
                    if (data.DataType == 'Import') {
                        $tygasoftDbHelper.GetValueByKey('KeyValue', 'DeviceInfo').then(function (res) {
                            var userName = '';
                            if (res && res != '') {
                                var jDeviceInfo = JSON.parse(res);
                                userName = jDeviceInfo.UserName;
                            }
                            $ionicLoading.show();
                            ts.ImportShelfMissionData($scope, userName, $tygasoftMC.DataStatus.Import, $tygasoftCommon.PageIndex, $tygasoftCommon.PageSize, 0);
                        })
                    }
                    else if (data.DataType == 'Delete') {
                        ts.DeleteShelfMissionData($scope);
                    }
                }
                else if (data.Index == 1) {
                    if (data.DataType == 'Import') {
                        $tygasoftDbHelper.GetValueByKey('KeyValue', 'DeviceInfo').then(function (res) {
                            var userName = '';
                            if (res && res != '') {
                                var jDeviceInfo = JSON.parse(res);
                                userName = jDeviceInfo.UserName;
                            }
                            ts.ImportShelfMissionProductData($scope, userName, $tygasoftMC.DataStatus.Import, $tygasoftCommon.PageIndex, $tygasoftCommon.PageSize, 0);
                        })
                    }
                    else if (data.DataType == 'Delete') {
                        ts.DeleteShelfMissionProductData($scope);
                    }
                }
            }
        })
    };

    ts.ImportShelfMissionData = function ($scope, userName, status, pageIndex, pageSize, okCount) {
        var url = "" + $tygasoftCommon.ServerUrl() + "/Services/PdaService.svc/GetShelfMissionList";
        var sData = '{"model":{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '"}}';

        $http.defaults.headers.post['Content-Type'] = 'application/json; charset=utf-8';
        $http({
            method: 'POST',
            url: url,
            data: sData
        }).then(function (res) {
            var result = res.data;
            //console.log('GetShelfMissionList--result--' + JSON.stringify(result));
            if (result.ResCode != 1000) {
                return false;
            }
            var jData = JSON.parse(result.Data);
            if (jData && jData.length > 0) {
                for (var i in jData) {
                    $tygasoftDbHelper.DoInsert('ShelfMission', userName, status, jData[i].Id, JSON.stringify(jData[i]), false);
                    okCount++;
                }
                pageIndex++;
                ts.ImportShelfMissionData($scope, userName, status, pageIndex, pageSize, okCount);
            }
            else {
                $ionicLoading.hide();
                $ionicPopup.alert({
                    title: $tygasoftMC.MC.Alert_Title,
                    template: $tygasoftMC.GetString('Server_Data_ImportCount', okCount),
                    okText: $tygasoftMC.MC.Btn_OkText
                }).then(function () {
                    $timeout(function () {
                        $scope.addImportDataModal.hide();
                        ts.Bind($scope);
                    }, 1000);
                })
            }
        }, function (err) {
            $ionicLoading.hide();
            alert($tygasoftMC.MC.Http_Err);
        });
    };

    ts.DeleteShelfMissionData = function ($scope) {
        $tygasoftDbHelper.DeleteAll('ShelfMission');
        $ionicPopup.alert({
            title: $tygasoftMC.MC.Alert_Title,
            template: $tygasoftMC.MC.Response_Ok,
            okText: $tygasoftMC.MC.Btn_OkText
        }).then(function () {
            $timeout(function () {
                $scope.addImportDataModal.hide();
                ts.Bind($scope);
            }, 1000);
        })
    };

    ts.ImportShelfMissionProductData = function ($scope, userName, status, pageIndex, pageSize, okCount) {
        $tygasoftDbHelper.GetAll('ShelfMission').then(function (res) {
            if (!res) {
                $ionicPopup.alert({
                    title: $tygasoftMC.MC.Alert_Title,
                    template: $tygasoftMC.MC.Response_Ok,
                    okText: $tygasoftMC.MC.Btn_OkText
                });
                return false;
            }
            var shelfMissionList = [];
            for (var i = 0; i < res.length; i++) {
                shelfMissionList.push(JSON.parse(res.item(i).ContentValue));
            }
            $ionicLoading.show();
            ts.ImportShelfMissionProductList($scope, userName, status, pageIndex, pageSize, okCount, shelfMissionList, 0);
        })
    };

    ts.ImportShelfMissionProductList = function ($scope, userName, status, pageIndex, pageSize, okCount, ShelfMissionList,index) {

        if (index >= ShelfMissionList.length) {
            $ionicLoading.hide();
            $ionicPopup.alert({
                title: $tygasoftMC.MC.Alert_Title,
                template: $tygasoftMC.GetString('Server_Data_ImportCount', okCount),
                okText: $tygasoftMC.MC.Btn_OkText
            }).then(function () {
                $timeout(function () {
                    $scope.addImportDataModal.hide();
                    ts.Bind($scope);
                }, 1000);
            })
            return false;
        }

        var shelfMissionId = ShelfMissionList[index].Id;
        var url = "" + $tygasoftCommon.ServerUrl() + "/Services/PdaService.svc/GetShelfMissionProductList";
        var sData = '{"model":{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","ShelfMissionId":"' + shelfMissionId + '"}}';

        $http.defaults.headers.post['Content-Type'] = 'application/json; charset=utf-8';
        $http({
            method: 'POST',
            url: url,
            data: sData
        }).then(function (res) {
            var result = res.data;
            //console.log('GetShelfMissionList--result--' + JSON.stringify(result));
            if (result.ResCode != 1000) {
                return false;
            }
            var jData = JSON.parse(result.Data);
            if (jData && jData.length > 0) {
                for (var i in jData) {
                    var key = shelfMissionId + '|' + jData[i].ProductId;
                    $tygasoftDbHelper.DoInsert('ShelfMissionProduct', userName, jData[i].Status, key, JSON.stringify(jData[i]), false);
                    okCount++;
                }
                pageIndex++;
                ts.ImportShelfMissionProductList($scope, userName, status, pageIndex, pageSize, okCount, ShelfMissionList, index);
            }
            else {
                index++;
                ts.ImportShelfMissionProductList($scope, userName, status, $tygasoftCommon.PageIndex, pageSize, okCount, ShelfMissionList, index);
            }
        }, function (err) {
            $ionicLoading.hide();
            alert($tygasoftMC.MC.Http_Err);
        });
    };

    ts.DeleteShelfMissionProductData = function ($scope) {
        $tygasoftDbHelper.DeleteAll('ShelfMissionProduct');
        $ionicPopup.alert({
            title: $tygasoftMC.MC.Alert_Title,
            template: $tygasoftMC.MC.Response_Ok,
            okText: $tygasoftMC.MC.Btn_OkText
        }).then(function () {
            $timeout(function () {
                $scope.addImportDataModal.hide();
                ts.Bind($scope);
            }, 1000);
        })
    };

    return ts;
});