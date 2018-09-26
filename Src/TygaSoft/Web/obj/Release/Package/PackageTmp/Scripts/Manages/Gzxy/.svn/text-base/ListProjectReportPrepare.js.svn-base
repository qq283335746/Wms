var ListProjectReportPrepare = {
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
        var keyword = $("#txtKeywordProject").textbox('getValue');
        var jData = { "ReqName": "GetInfoneProjectReportPrepareList", "PageIndex": "" + pageIndex + "", "PageSize": "" + pageSize + "", "Keyword": "" + keyword + "" };

        Common.AjaxPost("/wms/h/users.html", jData, function (result) {
            //console.log('result--' + result.Data);
            $("#dgProject").datagrid('loadData', JSON.parse(result.Data));
        })
    },
    OnAdd: function () {
        ListProjectReportPrepare.SelectRow = null;
        var w = $(window).width();
        var h = $(window).height();
        if (w > 750) w = 750;
        else w = w * 0.9;
        if (h > 700) h = 700;
        else h = h * 0.9;
        if ($("body").find("#dlgAddProjectReportPrepare").length == 0) {
            $("body").append("<div id=\"dlgAddProjectReportPrepare\" style=\"padding:10px;\"></div>");
        }
        $("#dlgAddProjectReportPrepare").dialog({
            title: '填写大项目信息',
            width: w,
            height: h,
            closed: false,
            href: '/wms/u/ty.html',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddProjectReportPrepare.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddProjectReportPrepare').dialog('close');
                }
            }]
        })
        return false;
    },
    OnEdit: function () {
        var rows = $("#dgProject").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        ListProjectReportPrepare.SelectRow = rows[0];
        var w = $(window).width();
        var h = $(window).height();
        if (w > 750) w = 750;
        else w = w * 0.9;
        if (h > 700) h = 700;
        else h = h * 0.9;
        if ($("body").find("#dlgAddProjectReportPrepare").length == 0) {
            $("body").append("<div id=\"dlgAddProjectReportPrepare\" style=\"padding:10px;\"></div>");
        }
        $("#dlgAddProjectReportPrepare").dialog({
            title: '编辑大项目信息',
            width: w,
            height: h,
            closed: false,
            href: '/wms/u/ty.html',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddProjectReportPrepare.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddProjectReportPrepare').dialog('close');
                }
            }]
        })
        return false;
    },
    OnDel: function () {
        var rows = $("#dgProject").datagrid('getSelections');
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
                var postData = { "ReqName": "DeleteInfoneProjectReportPrepare", "ItemAppend": "" + itemAppend + "" };
                Common.AjaxPost("/wms/h/content.html", postData, function (result) {
                    jeasyuiFun.show("温馨提示", "操作成功！");
                    setTimeout(function () {
                        ListProjectReportPrepare.LoadDg(1, 10);
                    }, 700);
                })
            }
        });
    },
    OnSearch: function () {
        ListProjectReportPrepare.LoadDg(1, 10);
    },
    OnView: function () {
        var rows = $("#dgProject").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        window.open("/wms/u/yprint.html?key=Project&Id=" + rows[0].Id + "");
    }
}