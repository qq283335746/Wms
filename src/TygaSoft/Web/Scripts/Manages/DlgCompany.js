var DlgCompany = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {

    },
    CbgCompany: function (cbg, data, values, readonly) {
        cbg.combogrid({
            readonly: readonly,
            data: data,
            value: values,
            columns: [[
                { field: 'Id', checkbox: true },
                { field: 'Coded', title: '公司编号', width: 100 },
                { field: 'Named', title: '公司名称', width: 300 }
            ]],
            filter: function (q, row) {
                var opts = $(this).combogrid('options');
                if (row[opts.textField].indexOf(q) > -1) return true;
                return false;
            }
        });
    },
    OnSelect: function (index, row) {
        var dg = $('#cbgCompany').combogrid('grid');
        var rows = dg.datagrid('getSelections');
        $('#cbgCompany').combogrid('setValues', rows);
    },
    GetCompanyList: function (pageIndex, pageSize, dataList, callBackFun, values, readonly) {
        var jData = { "ReqName": "GetCompanyList", "PageIndex": "" + pageIndex + "", "PageSize": "" + pageSize + "" };

        Common.AjaxPost("/wms/h/users.html", jData, function (result) {
            //console.log('GetCompanyList--result--' + result.Data);
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
                DlgCompany.GetCompanyList(pageIndex, pageSize, dataList, callBackFun, values);
            }
            else {
                if (typeof (eval(callBackFun)) == 'function') {
                    callBackFun($('#cbgCompany'), dataList, values, readonly);
                }
            }
        })
    }
}