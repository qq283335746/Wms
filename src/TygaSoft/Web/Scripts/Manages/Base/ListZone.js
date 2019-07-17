var ListZone = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        this.LoadDg(1, 10);
        var pager = $("#dg").datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                ListZone.LoadDg(pageNumber, pageSize);
            }
        });
    },
	SelectRow: null,
	LoadDg: function (pageIndex, pageSize) {
	    var keyword = $("#txtKeyword").textbox('getValue');
	    var postData = { "ReqName": "GetZoneList", "PageIndex": "" + pageIndex + "", "PageSize": "" + pageSize + "", "Keyword": "" + keyword + "" };

	    Common.AjaxPost("/wms/h/users.html", postData, function (result) {
	        //console.log('result--' + result.Data);
	        $("#dg").datagrid('loadData', JSON.parse(result.Data));
	    });
	},
	OnSearch: function () {
	    ListZone.LoadDg(1, 10);
	},
    OnAdd: function () {
        ListZone.SelectRow = null;
        var wh = Common.GetWh(560, 320);
        if ($("body").find("#dlgAddZone").length == 0) {
            $("body").append("<div id=\"dlgAddZone\" style=\"padding:10px;\"></div>");
        }
        $("#dlgAddZone").dialog({
            title: '填写信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            href: '/wms/a/tgbase.html',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddZone.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddZone').dialog('close');
                }
            }]
        })
        return false;
    },
    OnEdit: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        ListZone.SelectRow = rows[0];
        var w = $(window).width();
        var h = $(window).height();
        if (w > 750) w = 750;
        else w = w * 0.9;
        if (h > 340) h = 340;
        else h = h * 0.9;
        if ($("body").find("#dlgAddZone").length == 0) {
            $("body").append("<div id=\"dlgAddZone\" style=\"padding:10px;\"></div>");
        }
        $("#dlgAddZone").dialog({
            title: '编辑信息',
            width: w,
            height: h,
            closed: false,
            href: '/wms/a/tgbase.html?Id=' + rows[0].Id + '',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddZone.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddZone').dialog('close');
                }
            }]
        })
        return false;
    },
    OnDel: function () {
        var rows = $("#dg").datagrid('getSelections');
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
                var postData = { "ReqName": "DeleteZone", "ItemAppend": "" + itemAppend + "" };
                Common.AjaxPost("/wms/h/content.html", postData, function (result) {
                    jeasyuiFun.show("温馨提示", "操作成功！");
                    setTimeout(function () {
                        ListZone.LoadDg(1, 10);
                    }, 700);
                })
            }
        });
    }
}