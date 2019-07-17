var InStockDemandOrder = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {
    },
    InitData: function () {
        if ($('#hIsDoPrint').val() != '1') {
            var Id = Common.GetQueryString("Id");
            if (Id) {
                var sData = '{"Id":"' + Id + '"}';
                $.ajax({
                    url: "/wms/Services/WmsService.svc/GetPrintShelfMission",
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
                        //console.log('result--' + JSON.stringify(result));
                        //return false;
                        if (result.ResCode != 1000) {
                            $.messager.alert('系统提示', result.Msg, 'info');
                            return false;
                        }
                        var jData = JSON.parse(result.Data);
                        $('#lbOrderCode').text(jData.OrderCode);
                        $('#lbSupplierName').text(jData.SupplierName);
                        $('#lbPrintDate').text(jData.SPrintDate);
                        $('#lbPurchaseOrderCode').text(jData.PurchaseOrderCode);
                        $('#lbPlanArrivalTime').text(jData.SPlanArrivalTime);
                        $('#lbActualArrivalTime').text(jData.SActualArrivalTime);
                        $('#imgBarcodeImageUri').attr('src', jData.BarcodeImageUri);
                        
                        if (jData.CargoList && jData.CargoList != '') {
                            var cargoList = JSON.parse(jData.CargoList);
                            $.each(cargoList, function (i, item) {
                                var rowIndex = i + 1;
                                var sRow = '<tr><td>' + rowIndex + '</td><td>' + item.CargoCode + '</td><td>' + item.CargoName + '</td><td>' + item.PackageCode + '</td><td>' + item.UnitName + '</td><td>' + item.ExpectQty + '</td><td>' + item.ReceiptQty + '</td><td>' + item.StorageName + '</td></tr>';
                                $('#dgCargo').append(sRow);
                            })
                        }
                    }
                });
            }
        }
    },
    IsPrint:false,
    OnPrint: function () {
        $('#hIsDoPrint').val('1');

        var w = $(window).width();
        var h = $(window).height();
        if (w > 1000) w = 1000;
        else w = w * 0.96;
        
        var headElements = '<meta charset=\"utf-8\" />,<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"/>';
        var options = { "popTitle": "入库需求单", "popWd": w, "popHt": h, "mode": "popup", "popClose": true, "extraCss": "", "retainAttr": ["class", "id", "style", "on"], "extraHead": "" + headElements + "" };

        $('#printContent').printArea(options);
    }
}