var ListCompany = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        $("#txtKeyword").textbox();
        $("#dgCompany").datagrid();
        var pager = $("#dgCompany").datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                ListCompany.LoadDg(pageNumber, pageSize);
            }
        });
        setTimeout(function () {
            ListCompany.LoadDg(1, 10);
        })
    },
    SelectRow: null,
    LoadDg: function (pageIndex, pageSize) {
        var keyword = $("#txtKeyword").textbox('getValue');
        var jData = { "ReqName": "GetCompanyList", "PageIndex": "" + pageIndex + "", "PageSize": "" + pageSize + "", "Keyword": "" + keyword + "" };
        Common.AjaxPost("/wms/h/users.html", jData, function (result) {
            //console.log('result--' + result.Data);
            $("#dgCompany").datagrid('loadData', JSON.parse(result.Data));
        })
    },
    OnSearch: function () {
        ListCompany.LoadDg(1, 10);
    },
    OnSelect: function (index, row) {
        ListCompany.SelectRow = row;
    },
    OnAdd: function () {
        ListCompany.SelectRow = null;
        var wh = Common.GetWh(750, 610);
        if ($("body").find("#dlgAddCompany").length == 0) {
            $("body").append("<div id=\"dlgAddCompany\" style=\"padding:10px;\"></div>");
        }
        $("#dlgAddCompany").dialog({
            title: '填写公司信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            href: '/wms/u/tcompany.html',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddCompany.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddCompany').dialog('close');
                }
            }]
        })
        return false;
    },
    OnEdit: function () {
        var rows = $("#dgCompany").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        ListCompany.SelectRow = rows[0];
        var wh = Common.GetWh(750, 610);
        if ($("body").find("#dlgAddCompany").length == 0) {
            $("body").append("<div id=\"dlgAddCompany\" style=\"padding:10px;\"></div>");
        }
        $("#dlgAddCompany").dialog({
            title: '编辑公司信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            href: '/wms/u/tcompany.html',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddCompany.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddCompany').dialog('close');
                }
            }]
        })
        return false;
    },
    OnDel: function () {
        var rows = $("#dgCompany").datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请至少选择一行数据再进行操作", 'error');
            return false;
        }
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) itemAppend += ",";
            itemAppend += rows[i].Id;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                var jData = { "ReqName": "DeleteCompany", "ItemAppend": "" + itemAppend + "" };
                Common.AjaxPost("/wms/h/content.html", jData, function (result) {
                    jeasyuiFun.show("温馨提示", "操作成功！");
                    setTimeout(function () {
                        ListCompany.LoadDg(1, 10);
                    }, 1000);
                })
            }
        });
    },
    OnFeatureUser: function () {
        var rows = $("#dgCompany").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        DlgUsers.OnDlg(rows[0].FUserId, ListCompany.OnSaveFeatureUser);
    },
    OnSaveFeatureUser: function (userName) {
        var featureId = ListCompany.SelectRow.Id;
        var postData = { "ReqName": "SaveFeatureUser", "TypeName": "Company", "UserName": userName, FeatureId: featureId };
        Common.AjaxPost("/wms/h/content.html", postData, function (result) {
            $("#dlgUsers").dialog('close');
            jeasyuiFun.show("温馨提示", "操作成功！");
            setTimeout(function () {
                ListCompany.LoadDg(1, 10);
            }, 1000);
        })
    }
}