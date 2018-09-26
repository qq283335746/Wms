DlgCustomer = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        
    },
    GetCustomerList: function (pageIndex, pageSize, dataList, callBackFun, values) {
        try {
            var jData = { "ReqName": "GetInfoneCustomerList", "PageIndex": "" + pageIndex + "", "PageSize": "" + pageSize + "" };
            Common.AjaxPost("/wms/h/users.html", jData, function (result) {
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
                    DlgCustomer.GetCustomerList(pageIndex, pageSize, dataList, callBackFun, values);
                }
                else {
                    Common.OnProgressStop();
                    if (typeof (eval(callBackFun)) == 'function') {
                        callBackFun($('#cbgCustomer'), dataList, values);
                    }
                }
            })
        }
        catch (e) {
            Common.OnProgressStop();
        }
    },
    CbgCustomer: function (cbg, data, values) {
        //console.log('data--' + JSON.stringify(data));
        cbg.combogrid({
            data: data,
            value: values,
            columns: [[
                { field: 'Id', checkbox: true },
                { field: 'Coded', title: '客户编码', width: 80 },
                { field: 'Named', title: '客户简称', width: 300 }
            ]],
            filter: function (q, row) {
                var opts = $(this).combogrid('options');
                if (row[opts.textField].indexOf(q) > -1) return true;
                return false;
            }
        });
    },
    OnSelect: function (index, row) {
        var dg = $('#cbgCustomer').combogrid('grid');
        var rows = dg.datagrid('getSelections');
        $('#cbgCustomer').combogrid('setValues', rows);
    }
}