var DlgStockLocation = {
    Init: function () {
        this.InitEvent()
    },
    InitEvent: function () {

    },
    InitData: function () {

    },
    CbgStockLocation: function (cbg, data, values) {
        //console.log('CbgStockLocation--' + JSON.stringify(data));
        cbg.combogrid({
            data: data,
            value: values,
            columns: [[
                { field: 'Id', checkbox: true },
                { field: 'Code', title: '库位代码', width: 100 },
                { field: 'Named', title: '库位名称', width: 200 }
            ]],
            filter: function (q, row) {
                var opts = $(this).combogrid('options');
                if (row[opts.textField].indexOf(q) > -1) return true;
                return false;
            }
        });
    },
    OnSelect: function (index, row) {
        var dg = $('#cbgStockLocation').combogrid('grid');
        var rows = dg.datagrid('getSelections');
        $('#cbgStockLocation').combogrid('setValues', rows);
    },
    GetStockLocationList: function (pageIndex, pageSize, dataList, callBackFun, zoneIds, values) {
        try {
            if (!zoneIds || zoneIds == "") {
                if (typeof (eval(callBackFun)) == 'function') {
                    callBackFun($('#cbgStockLocation'), dataList, values);
                }
                return false;
            }
            var sData = '{"model":{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","ZoneIds":"' + zoneIds + '"}}';
            $.ajax({
                url: "/wms/Services/WmsService.svc/GetStockLocationList",
                type: "post",
                data: sData,
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    //console.log('GetStockLocationList--result--' + JSON.stringify(result))
                    if (result.ResCode != 1000) {
                        if (result.Msg != "") {
                            $.messager.alert('系统提示', result.Msg, 'info');
                        }
                        return false;
                    }
                    var jData = JSON.parse(result.Data);
                    if (!dataList) {
                        dataList = jData.rows;
                    }
                    else {
                        for (var i = 0; i < jData.rows.length; i++) {
                            dataList.push(jData.rows[i]);
                        }
                    }
                    if (jData.rows.length > 0) {
                        pageIndex++;
                        DlgStockLocation.GetStockLocationList(pageIndex, pageSize, dataList, callBackFun, zoneIds, values);
                    }
                    else {
                        if (typeof (eval(callBackFun)) == 'function') {
                            callBackFun($('#cbgStockLocation'), dataList, values);
                        }
                    }
                }
            });
        }
        catch (e) {
        }
    }
}