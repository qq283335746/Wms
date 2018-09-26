var DlgOrder = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {

    },
    CbgOrder: function (cbg, data, values, readonly) {
        cbg.combogrid({
            readonly: readonly,
            data: data,
            value: values,
            columns: [[
                { field: 'Id', checkbox: true },
                { field: 'OrderCode', title: '单号', width: 100 }
            ]],
            filter: function (q, row) {
                var opts = $(this).combogrid('options');
                if (row[opts.textField].indexOf(q) > -1) return true;
                return false;
            }
        });
    },
    OnSelect: function (index, row) {
        var dg = $('#cbgOrder').combogrid('grid');
        var rows = dg.datagrid('getSelections');
        AddLogisticsDistribution.GetTotalOrderSendProduct(rows);
        $('#cbgOrder').combogrid('setValues', rows);
    },
    GetOrderList: function (pageIndex, pageSize, dataList, callBackFun, values, readonly) {
        var jData = { "ReqName": "GetOrderSendList", "PageIndex": "" + pageIndex + "", "PageSize": "" + pageSize + "" };

        Common.AjaxPost("/wms/h/users.html", jData, function (result) {
            //console.log('GetOrderList--result--' + result.Data);
            var resultData = JSON.parse(result.Data);
            if (!dataList) {
                dataList = resultData.rows;
            }
            else {
                for (var i = 0; i < resultData.rows.length; i++) {
                    dataList.push(resultData.rows[i]);
                }
            }
            if ((resultData.rows.length > 0) && (resultData.total > resultData.rows.length)) {
                pageIndex++;
                DlgOrder.GetOrderList(pageIndex, pageSize, dataList, callBackFun, values);
            }
            else {
                if (typeof (eval(callBackFun)) == 'function') {
                    callBackFun($('#cbgOrder'), dataList, values, readonly);
                }
            }
        })
    }
}