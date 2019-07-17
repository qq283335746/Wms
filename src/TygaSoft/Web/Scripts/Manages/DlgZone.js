var DlgZone = {
    Init: function () {
        this.InitEvent();
        this.InitData();
        this.InitForm();
    },
    InitEvent: function () {

    },
    InitData: function () {
        setTimeout(function () {
            DlgZone.LoadDg(1, 10);
        }, 100);
    },
    InitForm: function () {
        $("#dgDlgZone").datagrid();
        var pager = $("#dgDlgZone").datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                DlgZone.LoadDg(pageNumber, pageSize);
            }
        });
    },
    LoadDg: function (pageIndex,pageSize) {
        var dg = $("#dgDlgZone");
        var postData = { "ReqName": "GetZoneList", "PageIndex": "" + pageIndex + "", "PageSize": "" + pageSize + "" };

        Common.AjaxPost("/wms/h/users.html", postData, function (result) {
            dg.datagrid('loadData', JSON.parse(result.Data));
        });
    },
    OnDlg: function () {
        if ($("body").find("#dlgZone").length == 0) {
            $("body").append("<div id=\"dlgZone\"></div>");
        }
        $("#dlgZone").dialog({
            title: '库区列表',
            width: 780,
            height: 500,
            closed: false,
            cache: false,
            href: '/wms/t/Zone.html',
            modal: true,
            iconCls: 'icon-ok',
            buttons: [{
                id: 'btnSaveZone', text: '确定', iconCls: 'icon-ok', handler: function () {
                    var rows = $("#dgDlgZone").datagrid('getSelections');
                    if (!rows || rows.length != 1) {
                        $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
                        return false;
                    }
                    $("#hZoneId").val(rows[0].Id);
                    $('#dlgZone').dialog('close');
                    $("#txtZone").textbox('setValue', rows[0].ZoneCode);
                }
            }, {
                id: 'btnCancelSaveZone', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgZone').dialog('close');
                }
            }]
        })
    },
    CbgZone: function (cbg, data, values) {
        //console.log('data--' + JSON.stringify(data));
        cbg.combogrid({
            data: data,
            value: values,
            columns: [[
                { field: 'Id', checkbox: true },
                { field: 'ZoneCode', title: '库区代码', width: 100 },
                { field: 'ZoneName', title: '库区名称', width: 200 }
            ]],
            filter: function (q, row) {
                var opts = $(this).combogrid('options');
                if (row[opts.textField].indexOf(q) > -1) return true;
                return false;
            }
        });
    },
    OnClickRow: function (index, row) {
        var dg = $('#cbgZone').combogrid('grid');
        var rows = dg.datagrid('getSelections');
        if (rows.length > 5) {
            return false;
        }
        var zoneIds = '';
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) zoneIds += ',';
            zoneIds += rows[i].Id;
        }
        DlgStockLocation.GetStockLocationList(1, 100, null, DlgStockLocation.CbgStockLocation, zoneIds);
    },
    OnSelect: function (index, row) {
        var dg = $('#cbgZone').combogrid('grid');
        var rows = dg.datagrid('getSelections');
        if (rows.length > 5) {
            $.messager.alert('提示', '最多选择5个库区', 'error');
            dg.datagrid('unselectRow', index);
            return false;
        }
        $('#cbgZone').combogrid('setValues', rows);
    },
    OnSelectAll: function (rows) {
        if (rows.length > 5) {
            $.messager.alert('提示', '最多选择5个库区', 'error');
            for (var i = 0; i < rows.length; i++) {
                var dg = $('#cbgZone').combogrid('grid');
                dg.datagrid('unselectRow', i);
            }
            return false;
        }
        var zoneIds = '';
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) zoneIds += ',';
            zoneIds += rows[i].Id;
        }
        DlgStockLocation.GetStockLocationList(1, 100, null, DlgStockLocation.CbgStockLocation, zoneIds);
    },
    GetZoneList: function (pageIndex, pageSize, dataList, callBackFun, values) {
        try {

            var postData = { "ReqName": "GetZoneList", "PageIndex": "" + pageIndex + "", "PageSize": "" + pageSize + "" };

            Common.AjaxPost("/wms/h/users.html", postData, function (result) {
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
                    DlgZone.GetZoneList(pageIndex, pageSize, dataList, callBackFun, values);
                }
                else {
                    Common.OnProgressStop();
                    if (typeof (eval(callBackFun)) == 'function') {
                        callBackFun($('#cbgZone'), dataList, values);
                    }
                }
            });
        }
        catch (e) {
            Common.OnProgressStop();
        }

    }
}