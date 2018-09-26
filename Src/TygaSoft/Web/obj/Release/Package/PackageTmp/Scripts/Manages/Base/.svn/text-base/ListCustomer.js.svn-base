var ListCustomer = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {
    },
    InitData: function () {
        ListCustomer.LoadDg(1, 10);
        $("#dgCustomer").datagrid();
        var pager = $("#dgCustomer").datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                ListCustomer.LoadDg(pageNumber, pageSize);
            }
        });
    },
    LoadDg: function (pageIndex, pageSize) {
        var keyword = $("#txtKeyword").textbox('getValue');
        var jData = { "ReqName": "GetCustomerList", "PageIndex": "" + pageIndex + "", "PageSize": "" + pageSize + "", "Keyword": "" + keyword + "" };
        Common.AjaxPost("/wms/h/users.html", jData, function (result) {
            //console.log('result--' + result.Data);
            $("#dgCustomer").datagrid('loadData', JSON.parse(result.Data));
        })
    },
    OnSearch: function () {
        ListCustomer.LoadDg(1, 10);
    },
    SelectRow: null,
    OnSelect: function (index, row) {
        ListCustomer.SelectRow = row;
    },
    OnAdd: function () {
        if ($("body").find("#dlgAddCustomer").length == 0) {
            $("body").append("<div id=\"dlgAddCustomer\" style=\"padding:10px;\"></div>");
        }
        $("#dlgAddCustomer").dialog({
            title: '新增货主信息',
            width: 720,
            height: 500,
            closed: false,
            href: '/wms/a/ttbase.html',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddCustomer.Save();
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
        if ($("body").find("#dlgAddCustomer").length == 0) {
            $("body").append("<div id=\"dlgAddCustomer\" style=\"padding:10px;\"></div>");
        }
        var rows = $("#dgCustomer").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        $("#dlgAddCustomer").dialog({
            title: '编辑货主信息',
            width: 720,
            height: 500,
            closed: false,
            href: '/wms/a/ttbase.html?Id=' + rows[0].Id + '',
            modal: true,
            iconCls: 'icon-edit',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddCustomer.Save();
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
                $.ajax({
                    url: "/wms/Services/WmsService.svc/DeleteCustomer",
                    type: "post",
                    data: '{"itemAppend":"' + itemAppend + '"}',
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
                        jeasyuiFun.show("温馨提示", "操作成功！");
                        setTimeout(function () {
                            ListCustomer.LoadDg(1, 10);
                        }, 700);
                    }
                });
            }
        });
    },
    OnFeatureUser: function () {
        var rows = $("#dgCustomer").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        DlgUsers.OnDlg(rows[0].FUserId, ListCustomer.OnSaveFeatureUser);
    },
    OnSaveFeatureUser: function (userName) {
        var featureId = ListCustomer.SelectRow.Id;
        var postData = { "ReqName": "SaveFeatureUser", "TypeName": "Customer", "UserName": userName, FeatureId: featureId };
        //console.log('postData--' + JSON.stringify(postData));
        //return false;
        Common.AjaxPost("/wms/h/content.html", postData, function (result) {
            $("#dlgUsers").dialog('close');
            jeasyuiFun.show("温馨提示", "操作成功！");
            setTimeout(function () {
                ListCustomer.LoadDg(1, 10);
            }, 1000);
        })
    },
    CallBackByProfile: function (data) {
        $('#imgSiteLogo').attr('src', data[0].Src);
    }
}