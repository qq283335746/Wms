var DlgPackage = {
    Init: function () {
        this.InitEvent();
        this.InitData();
        this.InitForm();
    },
    InitEvent: function () {

    },
    InitData: function () {
        setTimeout(function () {
            DlgPackage.LoadDg(1, 10);
        }, 100);
    },
    InitForm: function () {
        $("#dgDlgPackage").datagrid();
        var pager = $("#dgDlgPackage").datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                DlgPackage.LoadDg(pageNumber, pageSize);
            }
        });
    },
    LoadDg: function (pageIndex,pageSize) {
        var dg = $("#dgDlgPackage");
        var sData = '{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '"}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetPackageList",
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
        if ($("body").find("#dlgPackage").length == 0) {
            $("body").append("<div id=\"dlgPackage\"></div>");
        }
        $("#dlgPackage").dialog({
            title: '包装列表',
            width: 780,
            height: 500,
            closed: false,
            cache: false,
            href: '/wms/t/Package.html',
            modal: true,
            iconCls: 'icon-ok',
            buttons: [{
                id: 'btnSavePackage', text: '确定', iconCls: 'icon-ok', handler: function () {
                    var rows = $("#dgDlgPackage").datagrid('getSelections');
                    if (!rows || rows.length != 1) {
                        $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
                        return false;
                    }
                    $("#hPackageId").val(rows[0].Id);
                    $('#dlgPackage').dialog('close');
                    $("#txtPackage").textbox('setValue', rows[0].PackageCode);
                }
            }, {
                id: 'btnCancelSavePackage', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgPackage').dialog('close');
                }
            }]
        })
    }
}