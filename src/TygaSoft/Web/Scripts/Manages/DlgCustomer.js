var DlgCustomer = {
    Init: function () {
        this.InitEvent();
        this.InitData();
        this.InitForm();
    },
    InitEvent: function () {

    },
    InitData: function () {
        setTimeout(function () {
            DlgCustomer.LoadDg(1, 10);
        }, 100);
    },
    InitForm: function () {
        $("#dgDlgCustomer").datagrid();
        var pager = $("#dgDlgCustomer").datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                DlgCustomer.LoadDg(pageNumber, pageSize);
            }
        });
    },
    LoadDg: function (pageIndex,pageSize) {
        var dg = $("#dgDlgCustomer");
        var sData = '{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '"}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetCustomerList",
            type: "post",
            data: '{"model":' + sData + '}',
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                Common.OnProgressStart();
            },
            complete: function () {
                Common.OnProgressStop();
            },
            success: function (result) {
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }
                dg.datagrid('loadData', eval("(" + result.Data + ")"));
            }
        });
    },
    OnDlg: function () {
        if ($("body").find("#dlgCustomer").length == 0) {
            $("body").append("<div id=\"dlgCustomer\"></div>");
        }
        var w = $(window).width();
        if (w > 780) w = 780;
        else w = w * 0.8;
        var h = $(window).height();
        if (h > 500) h = 500;
        else h = h * 0.95;

        $("#dlgCustomer").dialog({
            title: '客户列表',
            width: w,
            height: h,
            closed: false,
            cache: false,
            href: '/wms/t/customer.html',
            modal: true,
            iconCls: 'icon-ok',
            buttons: [{
                id: 'btnSaveCustomer', text: '确定', iconCls: 'icon-ok', handler: function () {
                    var rows = $("#dgDlgCustomer").datagrid('getSelections');
                    if (!rows || rows.length != 1) {
                        $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
                        return false;
                    }
                    $("#hCustomerId").val(rows[0].Id);
                    $("#txtCustomer").textbox('setValue', rows[0].Coded + rows[0].Named);
                    $('#dlgCustomer').dialog('close');
                }
            }, {
                id: 'btnCancelSaveCustomer', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgCustomer').dialog('close');
                }
            }]
        })
    },
    CbgCustomer: function (cbg, data, values) {
        //console.log('data--' + JSON.stringify(data));
        cbg.combogrid({
            data: data,
            value: values,
            columns: [[
                { field: 'Id', checkbox: true },
                { field: 'Coded', title: '客户代码', width: 100 },
                { field: 'Named', title: '客户名称', width: 200 }
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
    },
    GetCustomerList: function (pageIndex, pageSize, dataList, callBackFun, values) {
        try {
            var sData = '{"model":{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '"}}';
            $.ajax({
                url: "/wms/Services/WmsService.svc/GetCustomerList",
                type: "post",
                data: sData,
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    //console.log('GetCustomerList--result--' + JSON.stringify(result));
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
                        DlgCustomer.GetCustomerList(pageIndex, pageSize, dataList, callBackFun, values);
                    }
                    else {
                        Common.OnProgressStop();
                        if (typeof (eval(callBackFun)) == 'function') {
                            callBackFun($('#cbgCustomer'), dataList, values);
                        }
                    }
                }
            });
        }
        catch (e) {
            Common.OnProgressStop();
        }

    }
}