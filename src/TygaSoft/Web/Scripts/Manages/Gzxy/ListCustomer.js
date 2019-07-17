var ListCustomer = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        this.LoadDg(1, 10);
    },
    SelectRow: null,
    LoadDg: function (pageIndex, pageSize) {
        var keyword = $("#txtKeywordCustomer").textbox('getValue');
        var postData = { "ReqName": "GetInfoneCustomerList", "PageIndex": "" + pageIndex + "", "PageSize": "" + pageSize + "", "Keyword": "" + keyword + "" };

        Common.AjaxPost("/wms/h/users.html", postData, function (result) {
            //console.log('result--' + result.Data);
            $("#dgCustomer").datagrid('loadData', JSON.parse(result.Data));
        })
    },
    OnAdd: function () {
        ListCustomer.SelectRow = null;
        var w = $(window).width();
        var h = $(window).height();
        if (w > 750) w = 750;
        else w = w * 0.9;
        if (h > 700) h = 700;
        else h = h * 0.9;
        if ($("body").find("#dlgAddCustomer").length == 0) {
            $("body").append("<div id=\"dlgAddCustomer\" style=\"padding:10px;\"></div>");
        }
        $("#dlgAddCustomer").dialog({
            title: '填写客户信息',
            width: w,
            height: h,
            closed: false,
            href: '/wms/u/tt.html',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddCustomer.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddCustomer').dialog('close');
                }
            }]
        })
        return false;
    },
    OnEdit: function () {
        var rows = $("#dgCustomer").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        ListCustomer.SelectRow = rows[0];
        var w = $(window).width();
        var h = $(window).height();
        if (w > 750) w = 750;
        else w = w * 0.9;
        if (h > 700) h = 700;
        else h = h * 0.9;
        if ($("body").find("#dlgAddCustomer").length == 0) {
            $("body").append("<div id=\"dlgAddCustomer\" style=\"padding:10px;\"></div>");
        }
        $("#dlgAddCustomer").dialog({
            title: '编辑客户信息',
            width: w,
            height: h,
            closed: false,
            href: '/wms/u/tt.html',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddCustomer.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddCustomer').dialog('close');
                }
            }]
        })
        return false;
    },
    OnDel: function () {
        var rows = $("#dgCustomer").datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) itemAppend += ",";
            itemAppend += rows[i].Id;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                var postData = { "ReqName": "DeleteInfoneCustomer", "ItemAppend": "" + itemAppend + "" };
                Common.AjaxPost("/wms/h/content.html", postData, function (result) {
                    jeasyuiFun.show("温馨提示", "操作成功！");
                    setTimeout(function () {
                        ListCustomer.LoadDg(1, 10);
                    }, 700);
                })
            }
        });
    },
    OnSearch: function () {
        ListCustomer.LoadDg(1, 10);
    },
    OnView: function () {
        var rows = $("#dgCustomer").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        window.open("/wms/u/yprint.html?key=Customer&Id=" + rows[0].Id + "");
    }
}