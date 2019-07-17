var ListDeviceRepairRecord = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {
        //$(document).bind('keypress', function (e) {
        //    if (e.keyCode == "13") {
        //        ListDeviceRepairRecord.OnSearch();
        //    }
        //});
        $("#abtnAdd").click(function () {
            ListDeviceRepairRecord.Add();
        });
        $("#abtnEdit").click(function () {
            ListDeviceRepairRecord.Edit();
        });
        $("#abtnDel").click(function () {
            ListDeviceRepairRecord.Del();
        });
    },
    InitData: function () {
        this.LoadDg(1, 10);
        $("#dg").datagrid();
        var pager = $("#dg").datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                ListDeviceRepairRecord.LoadDg(pageNumber, pageSize);
            }
        });
    },
    LoadDg: function (pageIndex, pageSize) {
        var keyword = $("#txtKeyword").textbox('getValue');
        var whetherFix = $.trim($("#ddlSWhetherFix>option:selected").text()).replace('请选择', '');
        var isBack = $.trim($("#ddlSIsBack").val()).replace('-1', '');
        var startDate = $.trim($("#txtStartDate").datebox('getValue'));
        var endDate = $.trim($("#txtEndDate").datebox('getValue'));
        var backDate = $.trim($("#txtSBackDate").datebox('getValue'));
        var jData = { "ReqName": "GetInfoneDeviceRepairRecordList", "PageIndex": "" + pageIndex + "", "PageSize": "" + pageSize + "", "Keyword": "" + keyword + "", "BackDate": "" + backDate + "", "WhetherFix": "" + whetherFix + "", "IsBack": "" + isBack + "", "StartDate": "" + startDate + "", "EndDate": "" + endDate + "" };
        Common.AjaxPost("/wms/h/users.html", jData, function (result) {
            //console.log('result--' + result.Data);
            $("#dg").datagrid('loadData', JSON.parse(result.Data));
        })
    },
    OnSearch: function () {
        var pager = $("#dg").datagrid('getPager');
        ListDeviceRepairRecord.LoadDg(1, pager.pagination('options').pageSize);
    },
    GetSearchItem: function () {
        var keyword = $("#txtKeyword").textbox('getValue');
        var whetherFix = $.trim($("#ddlSWhetherFix>option:selected").text()).replace('请选择', '');
        var isBack = $.trim($("#ddlSIsBack").val()).replace('-1', '');
        var sStartDate = $.trim($("#txtStartDate").datebox('getValue'));
        var sEndDate = $.trim($("#txtEndDate").datebox('getValue'));
        var backDate = $.trim($("#txtSBackDate").datebox('getValue'));
        return "keyword=" + encodeURIComponent(keyword) + "&whetherFix=" + encodeURIComponent(whetherFix) + "&isBack=" + isBack + "&startDate=" + sStartDate + "&endDate=" + sEndDate + "&backDate=" + backDate + "";
    },
    Add: function () {
        var h = $(window).height();
        if (h > 600) h = 630;
        else h = h * 0.8;
        $("#dlgAddDeviceRepairRecord").dialog({
            title: '新增维修设备记录信息',
            width: 720,
            height: h,
            closed: false,
            href: '../u/g.html',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddDeviceRepairRecord.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddDeviceRepairRecord').dialog('close');
                }
            }]
        })
        return false;
    },
    Edit: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var h = $(window).height();
        if (h > 600) h = 630;
        else h = h * 0.8;
        $("#dlgAddDeviceRepairRecord").dialog({
            title: '编辑维修设备记录信息',
            width: 720,
            height: h,
            closed: false,
            href: '../u/g.html?Id=' + rows[0].Id + '',
            modal: true,
            iconCls: 'icon-edit',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddDeviceRepairRecord.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddDeviceRepairRecord').dialog('close');
                }
            }]
        })
        return false;
    },
    Del: function () {
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
                var postData = { "ReqName": "DeleteInfoneDeviceRepairRecord", "ItemAppend": "" + itemAppend + "" };
                Common.AjaxPost("/wms/h/content.html", postData, function (result) {
                    jeasyuiFun.show("温馨提示", "操作成功！");
                    setTimeout(function () {
                        ListDeviceRepairRecord.LoadDg(1, 10);
                    }, 700);
                })
            }
        });
    },
    OnImport: function () {
        DlgUpload.TableName = "ImportDeviceRepairRecord";
        DlgUpload.CallBack = "ListDeviceRepairRecord.OnImportCallBack()";
        DlgUpload.OnDlgUpload();
    },
    OnImportCallBack: function () {
        setTimeout(function () {
            window.location.reload();
        }, 1000);
    },
    OnExport: function () {
        var queryStr = this.GetSearchItem();
        $.messager.confirm('提示', '确定要导出数据吗？', function (r) {
            if (r) {
                window.open("/wms/h/upload.html?ReqName=ExportDeviceRepairRecord&" + queryStr + "");
            }
        })
    },
    RowStyler: function (index, row) {
        if (index % 2 != 0) {
            return 'background-color:#F5F5F5;';
        }
    }
}