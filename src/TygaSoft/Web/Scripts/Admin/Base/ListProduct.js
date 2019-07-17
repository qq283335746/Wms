var ListProduct = {
    Init: function () {
        this.InitEvent();
        this.InitData();
        this.InitForm();
    },
    InitEvent: function () {
        $("#dgProductToolbar").find("[name=abtnAdd]").click(function () {
            ListProduct.Add();
        })
        $("#dgProductToolbar").find("[name=abtnEdit]").click(function () {
            ListProduct.Edit();
        })
        $("#dgProductToolbar").find("[name=abtnDel]").click(function () {
            ListProduct.Del();
        })
    },
    InitData: function () {
        setTimeout(function () {
            ListProduct.LoadDg(1, 10);
        }, 100);
    },
    InitForm:function(){
        var pager = $("#dgProduct").datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                ListProduct.LoadDg(pageNumber, pageSize);
            }
        });
    },
    GetCategoryId:function(){
        var t = $("#treeCt");
        var node = t.tree('getSelected');
        if (node) {
            return node.id;
        }
        if ($.trim($('#hId').val()) != "") return $.trim($('#hCategoryId').val());
        return "";
    },
    LoadDg: function (pageIndex, pageSize) {
        var categoryId = ListProduct.GetCategoryId();
        var sKeyword = $.trim($('#txtKeyword').textbox('getValue'));
        var sData = '{"model":{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","CategoryId":"' + categoryId + '","Keyword":"' + sKeyword + '"}}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetProductList",
            type: "post",
            data: sData,
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                //console.log('GetProductList--result--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    if (result.Msg != "") {
                        $.messager.alert('系统提示', result.Msg, 'info');
                    }
                    return false;
                }
                $("#dgProduct").datagrid('loadData', eval("(" + result.Data + ")"));
            }
        });
    },
    OnSearch: function () {
        ListProduct.LoadDg(1, 10);
    },
    Add:function(){
        var t = $("#treeCt");
        var node = t.tree('getSelected');
        if (!node) {
            $.messager.alert('错误提示', '请选中一个物料分类节点再进行此操作', 'error');
            return false;
        }
        if ($("body").find("#dlgAddProduct").length == 0) {
            $("body").append("<div id=\"dlgAddProduct\" style=\"padding:10px;\"></div>");
        }
        var w = $(window).width();
        var h = $(window).height();
        if (w > 960) w = 960;
        else w = w * 0.96;
        if (h > 370) h = 370;
        else h = h * 0.8;
        $("#dlgAddProduct").dialog({
            title: '新建物料',
            width: w,
            height: h,
            closed: false,
            cache: false,
            href: '/wms/a/yybase.html?categoryId=' + node.id + '',
            modal: true,
            iconCls: 'icon-save',
            buttons: [{
                id: 'btnSaveProduct', text: '保存', iconCls: 'icon-ok', handler: function () {
                    AddProduct.Save();
                }
            }, {
                id: 'btnCancelSaveProduct', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddProduct').dialog('close');
                }
            }]
        })
    },
    Edit:function(){
        var rows = $("#dgProduct").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行物料数据进行操作", 'error');
            return false;
        }
        if ($("body").find("#dlgAddProduct").length == 0) {
            $("body").append("<div id=\"dlgAddProduct\" style=\"padding:10px;\"></div>");
        }
        var w = $(window).width();
        var h = $(window).height();
        if (w > 960) w = 960;
        else w = w * 0.96;
        if (h > 370) h = 370;
        else h = h * 0.8;
        $("#dlgAddProduct").dialog({
            title: '编辑物料',
            width: w,
            height: h,
            closed: false,
            cache: false,
            href: '/wms/a/yybase.html?Id=' + rows[0].Id + '',
            modal: true,
            iconCls: 'icon-save',
            buttons: [{
                id: 'btnSaveProduct', text: '保存', iconCls: 'icon-ok', handler: function () {
                    AddProduct.Save();
                }
            }, {
                id: 'btnCancelSaveProduct', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddProduct').dialog('close');
                }
            }]
        })
    },
    Del: function () {
        var rows = $("#dgProduct").datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行物料数据进行操作", 'error');
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
                    url: "/wms/Services/WmsService.svc/DeleteProduct",
                    type: "post",
                    data: '{"itemAppend":"' + itemAppend + '"}',
                    contentType: "application/json; charset=utf-8",
                    beforeSend: function () {
                        $.messager.progress({ title: '请稍等', msg: '正在执行...' });
                    },
                    complete: function () {
                        $.messager.progress('close');
                    },
                    success: function (result) {
                        if (result.ResCode != 1000) {
                            $.messager.alert('系统提示', result.Msg, 'info');
                            return false;
                        }
                        ListProduct.LoadDg(1, 10);
                        jeasyuiFun.show("温馨提示", "操作成功！");
                    }
                });
            }
        });
    },
    OnImport: function () {
        DlgUpload.TableName = "ImportProduct";
        DlgUpload.CallBack = "ListProduct.OnImportCallBack()";
        DlgUpload.OnDlgUpload();
    },
    OnImportCallBack: function () {
        setTimeout(function () {
            window.location.reload();
        }, 1000);
    },
    OnExport: function () {
    }
}