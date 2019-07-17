var ListMesOrder = {
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
                ListMesOrder.LoadDg(pageNumber, pageSize);
            }
        });
    },
	SelectRow: null,
    LoadDg: function (pageIndex, pageSize) {
        var keyword = $("#txtKeyword").textbox('getValue');
        var postData = { "ReqName": "GetMesOrderList", "PageIndex": "" + pageIndex + "", "PageSize": "" + pageSize + "", "Keyword": "" + keyword + "" };
        Common.AjaxPost("/wms/h/users.html", postData, function (result) {
            //console.log('result--' + JSON.stringify(result));
            $("#dg").datagrid('loadData', JSON.parse(result.Data));
        })
    },
    OnSearch: function () {
        ListMesOrder.LoadDg(1, 10);
    },
    OnAdd: function () {
        ListMesOrder.SelectRow = null;
        if ($("body").find("#dlgAddMesOrder").length == 0) {
            $("body").append("<div id=\"dlgAddMesOrder\" style=\"padding:10px;\"></div>");
        }
		var s = '';
		var wh = Common.GetWh(780, 500);
        $("#dlgAddMesOrder").dialog({
            title: '填写信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddMesOrder.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddMesOrder').dialog('close');
                }
            }],
			content:s
        })
        return false;
    },
    OnEdit: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        ListMesOrder.SelectRow = rows[0];
        if ($("body").find("#dlgAddMesOrder").length == 0) {
            $("body").append("<div id=\"dlgAddMesOrder\" style=\"padding:10px;\"></div>");
        }
		var s = '';
		var wh = Common.GetWh(780, 500);
        $("#dlgAddMesOrder").dialog({
            title: '编辑信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddMesOrder.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddMesOrder').dialog('close');
                }
            }],
			content:s
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
			    var postData = { "ReqName": "DeleteMesOrder", "ItemAppend": "" + itemAppend + "" };
			    Common.AjaxPost("/wms/h/content.html", postData, function (result) {
                    jeasyuiFun.show("温馨提示", "保存成功！");
                    setTimeout(function () {
                        ListMesOrder.LoadDg(1, 10);
                    }, 700);
                });
            }
        });
    },
	OnSave: function () {
        var isValid = $('#dlgFm').form('validate');
        if (!isValid) return false;
		var oBarcode = $.trim($("#txtOBarcode").val()); 
        var pBarcode = $.trim($("#txtPBarcode").val()); 
        var pdBarcode = $.trim($("#txtPdBarcode").val()); 
        var ptBarcode = $.trim($("#txtPtBarcode").val()); 
        var qty = $.trim($("#txtQty").val()); 
        var startDate = $.trim($("#txtStartDate").val()); 
        var endDate = $.trim($("#txtEndDate").val()); 
        var sort = $.trim($("#txtSort").val()); 
        var remark = $.trim($("#txtRemark").val()); 

        var postData = {"ReqName":"SaveMesOrder","OBarcode":"" + oBarcode + "","PBarcode":"" + pBarcode + "","PdBarcode":"" + pdBarcode + "","PtBarcode":"" + ptBarcode + "","Qty":"" + qty + "","StartDate":"" + startDate + "","EndDate":"" + endDate + "","Sort":"" + sort + "","Remark":"" + remark + ""};
        Common.AjaxPost("/wms/h/content.html", postData, function (result) {
            $("#dlgAddMesOrder").dialog('close');
            jeasyuiFun.show("温馨提示", "保存成功！");
            setTimeout(function () {
                ListMesOrder.LoadDg(1, 10);
            }, 700);
        })
    }
}