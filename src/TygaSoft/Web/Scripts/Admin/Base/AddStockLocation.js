var AddStockLocation = {
    Init: function () {
        this.InitEvent();
    },
    InitEvent: function () {
        //$("#btnSave").click(function () {
        //    AddStockLocation.Save();
        //})

        setInterval(AddStockLocation.AutoVolume, 100);
    },
    Container: $("#dlgAddStockLocation"),
    OnSave: function () {
        var isValid = $('#dlgFm').form('validate');
        if (!isValid) return false;
        var Id = $.trim($("#hId").val());
        var zoneId = $.trim($("#hZoneId").val());
        var sCode = $.trim($("#txtCode").val());
        var sName = $.trim($("#txtName").val());
        var width = $.trim($("#txtWidth").val());
        if (width == "") width = 0;
        var wide = $.trim($("#txtWide").val());
        if (wide == "") wide = 0;
        var high = $.trim($("#txtHigh").val());
        if (high == "") high = 0;
        var volume = $.trim($("#txtVolume").val());
        if (volume == "") volume = 0;
        var cubage = $.trim($("#txtCubage").val());
        if (cubage == "") cubage = 0;
        cubage = parseFloat(cubage).toFixed(5);
        var row = $.trim($("#txtRow").val());
        if (row == "") row = 0;
        var layer = $.trim($("#txtLayer").val());
        if (layer == "") layer = 0;
        var col = $.trim($("#txtCol").val());
        if (col == "") col = 0;
        var passway = $.trim($("#txtPassway").val());
        if (passway == "") passway = 0;
        var x = $.trim($("#txtX").val());
        if (x == "") x = 0;
        var y = $.trim($("#txtY").val());
        if (y == "") y = 0;
        var z = $.trim($("#txtZ").val());
        if (z == "") z = 0;
        var orientation = $.trim($("#txtOrientation").val());
        if (orientation == "") orientation = 0;
        var stackLimit = $.trim($("#txtStackLimit").val());
        if (stackLimit == "") stackLimit = 0;
        var groundTrayQty = $.trim($("#txtGroundTrayQty").val());
        if (groundTrayQty == "") groundTrayQty = 0;
        var carryWeight = $.trim($("#txtCarryWeight").val());
        if (carryWeight == "") carryWeight = 0;
        var levelNum = $.trim($("#txtLevelNum").val());
        if (levelNum == "") levelNum = 0;
        var insertTaskSeq = $.trim($("#txtInsertTaskSeq").val());
        if (insertTaskSeq == "") insertTaskSeq = 0;
        var inventoryGroupId = $.trim($("#txtInventoryGroupId").val());
        if (inventoryGroupId == "") inventoryGroupId = 0;
        var isMixPlace = $.trim($("#ddlIsMixPlace").val()) == "1" ? true : false;
        var isBatchNum = $.trim($("#ddlIsBatchNum").val()) == "1" ? true : false;
        var isLoseId = $.trim($("#ddlIsLoseId").val()) == "1" ? true : false;
        var ctrType = $("#ddlCtrType>option:selected").text();
        var abc = $("#ddlABC>option:selected").text();
        var stockLocationType = $("#ddlStockLocationType>option:selected").text();
        var stockLocationDeal = $("#ddlStockLocationDeal>option:selected").text();
        var useStatus = $("#ddlUseStatus>option:selected").text();
        var inventoryGroupId = $.trim($("#txtInventoryGroupId").val());
        var sRouteSeq = $.trim($("#txtRouteSeq").val());
        var sStatus = $.trim($("#txtStatus").val());
        var sWarehouse = $.trim($("#txtWarehouse").val());
        var sPickArea = $.trim($("#txtPickArea").val());
        var sPickMethod = $.trim($("#txtPickMethod").val());
        var sRemark = $.trim($("#txtRemark").val());

        var postData = { "ReqName": "SaveStockLocation", "Id": "" + Id + "", "ZoneId": "" + zoneId + "", "Coded": "" + sCode + "", "Named": "" + sName + "", "Row": "" + row + "", "Layer": "" + layer + "", "Col": "" + col + "", "Passway": "" + passway + "", "StockLocationDeal": "" + stockLocationDeal + "", "Width": "" + width + "", "Wide": "" + wide + "", "High": "" + high + "", "Volume": "" + volume + "", "Cubage": "" + cubage + "", "StackLimit": "" + stackLimit + "", "CarryWeight": "" + carryWeight + "", "Xc": "" + x + "", "Yc": "" + y + "", "Zc": "" + z + "", "Orientation": "" + orientation + "", "StockLocationType": "" + stockLocationType + "", "GroundTrayQty": "" + groundTrayQty + "", "UseStatus": "" + useStatus + "", "RouteSeq": "" + sRouteSeq + "", "IsMixPlace": "" + isMixPlace + "", "IsBatchNum": "" + isBatchNum + "", "IsLoseId": "" + isLoseId + "", "InsertTaskSeq": "" + insertTaskSeq + "", "Status": "" + sStatus + "", "Warehouse": "" + sWarehouse + "", "LevelNum": "" + levelNum + "", "PickArea": "" + sPickArea + "", "CtrType": "" + ctrType + "", "ABC": "" + abc + "", "InventoryGroupId": "" + inventoryGroupId + "", "PickMethod": "" + sPickMethod + "", "Remark": "" + sRemark + "" };
        Common.AjaxPost("/wms/h/content.html", postData, function (result) {
            AddStockLocation.Container.dialog('close');
            jeasyuiFun.show("温馨提示", "保存成功！");
            setTimeout(function () {
                ListStockLocation.LoadDg(1, 10);
            }, 700);
        })
        return false;
        var sData = '{"Id":"' + Id + '","ZoneId":"' + zoneId + '","Code":"' + sCode + '","Named":"' + sName + '","Row":"' + row + '","Layer":"' + layer + '","Col":"' + col + '","Passway":"' + passway + '","StockLocationDeal":"' + stockLocationDeal + '","Width":"' + width + '","Wide":"' + wide + '","High":"' + high + '","Volume":"' + volume + '","Cubage":"' + cubage + '","StackLimit":"' + stackLimit + '","CarryWeight":"' + carryWeight + '","Xc":"' + x + '","Yc":"' + y + '","Zc":"' + z + '","Orientation":"' + orientation + '","StockLocationType":"' + stockLocationType + '","GroundTrayQty":"' + groundTrayQty + '","UseStatus":"' + useStatus + '","RouteSeq":"' + $.trim($("#txtRouteSeq").val()) + '","IsMixPlace":"' + isMixPlace + '","IsBatchNum":"' + isBatchNum + '","IsLoseId":"' + isLoseId + '","InsertTaskSeq":"' + insertTaskSeq + '","Status":"' + $.trim($("#txtStatus").val()) + '","Warehouse":"' + $.trim($("#txtWarehouse").val()) + '","LevelNum":"' + levelNum + '","PickArea":"' + $.trim($("#txtPickArea").val()) + '","CtrType":"' + ctrType + '","ABC":"' + abc + '","InventoryGroupId":"' + inventoryGroupId + '","PickMethod":"' + $.trim($("#txtPickMethod").val()) + '","Remark":"' + $.trim($("#txtRemark").val()) + '"}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/SaveStockLocation",
            type: "post",
            data: '{"model":' + sData + '}',
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $.messager.progress({ title: '请稍等', msg: '正在执行...' });
            },
            complete: function () {
                $.messager.progress('close');
            },
            success: function (result) {
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }
                jeasyuiFun.show("温馨提示", "操作成功！");
                setTimeout(function () {
                    window.location.reload();
                }, 1500)
            }
        });
    },
    AutoVolume: function () {
        var width = $.trim($("#txtWidth").val());
        if (width == "") width = 0;
        var wide = $.trim($("#txtWide").val());
        if (wide == "") wide = 0;
        var high = $.trim($("#txtHigh").val());
        if (high == "") high = 0;
        var volume = parseFloat(width) * parseFloat(wide) * parseFloat(high);
        volume = volume.toFixed(5);
        $("#txtVolume").val(volume);
    }
}