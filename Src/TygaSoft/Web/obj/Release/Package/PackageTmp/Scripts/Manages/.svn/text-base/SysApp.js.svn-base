var SysApp = {
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
                ListSysApp.LoadDg(pageNumber, pageSize);
            }
        });
    },
    SelectRow: null,
    LoadDg: function (pageIndex, pageSize) {
        var keyword = $("#txtKeyword").textbox('getValue');
        var postData = { "ReqName": "GetSysAppList", "PageIndex": "" + pageIndex + "", "PageSize": "" + pageSize + "", "Keyword": "" + keyword + "" };
        var dg = $('#dgSysApp');
        Common.AjaxPost("/wms/h/users.html", postData, function (result) {
            dg.datagrid({
                data: JSON.parse(result.Data),
                columns: [[
                    { field: 'Id', checkbox: true },
                    { field: 'Coded', title: '应用程序编码', width: 100 },
                    { field: 'Named', title: '应用程序名称', width: 100 },
                    { field: 'ConnString', title: '数据库连接字符串', width: 100 }
                ]],
                onLoadSuccess: function (data) {
                    console.log('GetSysAppInfo--' + JSON.stringify(data));
                    var rows = $('#bindT').datagrid("getSelections");
                    if (rows && rows.length == 1) {
                        var postData = { "ReqName": "GetSysAppInfo", "UserName": "" + rows[0].f1 + ""};
                        Common.AjaxPost("/wms/h/users.html", postData, function (result) {
                            console.log('GetSysAppInfo--' + JSON.stringify(result));
                            if (result.Data != 'null') {
                                var appUserInfo = JSON.parse(result.Data);
                                for (var i = 0; i < data.rows.length; i++) {
                                    if (data.rows[i].Id == appUserInfo.AppId) {
                                        dg.datagrid('selectRow', i);
                                        break;
                                    }
                                }
                            }
                        })
                    }
                }
            });
        })
    },
    OnSearch: function () {
        SysApp.LoadDg(1, 10);
    },
    OnAdd: function () {
        SysApp.SelectRow = null;
        if ($("body").find("#dlgAddSysApp").length == 0) {
            $("body").append("<div id=\"dlgAddSysApp\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(700, 300);
        $("#dlgAddSysApp").dialog({
            title: '填写信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                text: '保存', iconCls: 'icon-save', handler: function () {
                    SysApp.OnSave();
                }
            }, {
                text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddSysApp').dialog('close');
                }
            }],
            content: SysApp.GetFormString()
        });
        return false;
    },
    OnEdit: function () {
        var rows = $("#dgSysApp").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        SysApp.SelectRow = rows[0];
        if ($("body").find("#dlgAddSysApp").length == 0) {
            $("body").append("<div id=\"dlgAddSysApp\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(700, 300);
        $("#dlgAddSysApp").dialog({
            title: '编辑信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    SysApp.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddSysApp').dialog('close');
                }
            }],
            content: SysApp.GetFormString(),
            onOpen: function () {
                SysApp.SetForm(rows[0]);
            }
        });
        return false;
    },
    SetForm: function (data) {
        var contarner = $('#dlgSysAppFm');
        contarner.find('#txtCoded').val(data.Coded); 
        contarner.find('#txtNamed').val(data.Named); 
        contarner.find('#txtConnString').val(data.ConnString); 
    },
    GetFormString:function(){
        var s = '';
        s += '<form id="dlgSysAppFm"><div class="row"><span class="rl" style="width:136px;"><span class="cr">*</span> 应用程序编号：</span><div class="fl"><input id ="txtCoded" class="easyui-validatebox txt200" data-options="required:true" /></div></div> <div class="row mt10"><span class="rl" style="width:136px;"><span class="cr">*</span> 应用程序名称：</span><div class="fl"><input id ="txtNamed" class="easyui-validatebox txt200" data-options="required:true" /></div></div> ';
        s += '<div class="row mt10"><span class="rl" style="width:136px;"><span class="cr">*</span> 数据库连接字符串：</span><div class="fl"> <textarea id="txtConnString" rows="3" cols="80" style="width:498px;height:80px;"></textarea></div></div> ';
        s += '<input type="hidden" id="hId" /><span class="clr"></span></form>';
        return s;
    },
    OnDel: function () {
        var rows = $("#dgSysApp").datagrid('getSelections');
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
                var postData = { "ReqName": "DeleteSysApp", "ItemAppend": "" + itemAppend + "" };
                Common.AjaxPost("/wms/h/content.html", postData, function (result) {
                    jeasyuiFun.show("温馨提示", "保存成功！");
                    setTimeout(function () {
                        SysApp.LoadDg(1, 10);
                    }, 700);
                });
            }
        });
    },
    OnSave: function () {
        var isValid = $('#dlgSysAppFm').form('validate');
        if (!isValid) return false;
        var coded = $.trim($("#txtCoded").val());
        var named = $.trim($("#txtNamed").val());
        var connString = $.trim($("#txtConnString").val());

        var postData = { "ReqName": "SaveSysApp", "Coded": "" + coded + "", "Named": "" + named + "", "ConnString": "" + connString + "" };
        Common.AjaxPost("/wms/h/content.html", postData, function (result) {
            $("#dlgAddSysApp").dialog('close');
            jeasyuiFun.show("温馨提示", "保存成功！");
            setTimeout(function () {
                SysApp.LoadDg(1, 10);
            }, 700);
        })
    },
    DlgList: function (isAdmin,callBackFun) {
        var rows = $("#bindT").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var wh = Common.GetWh(780, 500);
        if ($("body").find("#dlgSysApp").length == 0) {
            $("body").append('<div id="dlgSysApp"></div>');
        }
        var $dlg = $('#dlgSysApp');
        var s = '<div id="dgToolbar" style="padding:5px;">';
        if (isAdmin) {
            s += '<a class="easyui-linkbutton" data-options="plain:true,iconCls:\'icon-add\'" onclick="SysApp.OnAdd()"><span>新增</span></a>';
            s += '<a class="easyui-linkbutton" data-options="plain:true,iconCls:\'icon-edit\'" onclick="SysApp.OnEdit()"><span>编辑</span></a>';
            s += '<a class="easyui-linkbutton" data-options="plain:true,iconCls:\'icon-remove\'" onclick="SysApp.OnDel()"><span>删除</span></a>';
        }
        s += '<div class="fr"><input id="txtKeyword" class="easyui-textbox" data-options="prompt:\'关键字\',buttonText:\'查询\',buttonIcon:\'icon-search\',onClickButton:SysApp.OnSearch" style="width:250px;" /></div>';
        s += '<span class="clr"></span></div>';
        s += '<table id="dgSysApp" data-options="fit:true,fitColumns:true,pagination:true,rownumbers:true,singleSelect:true,border:false,striped:true,toolbar:\'#dgToolbar\'"></table>';
        $dlg.dialog({
            title: '选择应用程序',
            width: wh[0],
            height: wh[1],
            closed: false,
            cache: false,
            modal: true,
            iconCls: 'icon-ok',
            buttons: [{
                id: 'btnOkDlgSysApp', text: '确定', iconCls: 'icon-ok', handler: function () {
                    var selectRows = $('#dgSysApp').datagrid('getSelections');
                    if (!selectRows || selectRows.length != 1) {
                        $.messager.alert('错误提示', "请选择应用程序", 'error');
                        return false;
                    }
                    callBackFun(selectRows[0]);
                }
            }, {
                id: 'btnCancelDlgSysApp', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $dlg.dialog('close');
                }
            }],
            content: s,
            onOpen: function () {
                SysApp.LoadDg(1, 50);
            }
        })
    }
}