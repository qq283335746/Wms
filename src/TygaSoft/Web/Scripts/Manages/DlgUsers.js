var DlgUsers = {
    Init: function () {
        this.InitEvent()
    },
    InitEvent: function () {

    },
    InitData: function () {

    },
    OnDlg: function (values,callback) {
        if ($("body").find("#dlgUsers").length == 0) {
            $("body").append('<div id="dlgUsers" style="padding:10px;"></div>');
        }
        var s = '';
        s += '<div class="row"><span class="rl">关联用户：</span><div class="fl"><input id="cbgUser" name="cbgUser" data-options="required:true,idField:\'Id\',textField: \'Text\', fit:false, fitColumns:true,multiple:false,rownumbers:true,onSelect:DlgUsers.OnSelect,panelWidth:398,missingMessage:\'必需项\'" style="width:398px;" /></div></div>';
        s += '<span class="clr"></span>';
        var wh = Common.GetWh(600, 300);
        $("#dlgUsers").dialog({
            title: '绑定用户',
            width: wh[0],
            height: wh[1],
            closed: false,
            cache: false,
            modal: true,
            iconCls: 'icon-ok',
            buttons: [{
                id: 'btnSaveDlgUsers', text: '确定', iconCls: 'icon-ok', handler: function () {
                    var isValid = $('#dlgUsersFm').form('validate');
                    if (!isValid) return false;
                    var userName = $.trim($('#cbgUser').combogrid('getText'));
                    callback(userName);
                }
            }, {
                id: 'btnCancelDlgUsers', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgUsers').dialog('close');
                }
            }],
            content: '<form id="dlgUsersFm">'+s+'</form>',
            onOpen: function () {
                DlgUsers.GetUserList(1, 100, null, DlgUsers.CbgUser, values);
            }
        })
    },
    CbgUser: function (cbg, data, values) {
        //console.log('CbgUser--data--' + JSON.stringify(data));
        cbg.combogrid({
            data: data,
            value: values,
            columns: [[
                { field: 'Id', checkbox: true },
                { field: 'Text', title: '用户名', width: 300 }
            ]],
            filter: function (q, row) {
                var opts = $(this).combogrid('options');
                if (row[opts.textField].indexOf(q) > -1) return true;
                return false;
            }
        });
    },
    OnSelect: function (index, row) {
        var dg = $('#cbgUser').combogrid('grid');
        var rows = dg.datagrid('getSelections');
        $('#cbgUser').combogrid('setValues', rows);
    },
    GetUserList: function (pageIndex, pageSize, dataList, callBackFun, values) {
        try {
            var sData = '{"model":{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '"}}';
            $.ajax({
                url: "/wms/Services/SecurityService.svc/GetUserList",
                type: "post",
                data: sData,
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    if (result.ResCode != 1000) {
                        if (result.Msg != "") {
                            $.messager.alert('系统提示', result.Msg, 'info');
                        }
                        return false;
                    }
                    var jData = JSON.parse(result.Data);
                    if (!dataList) {
                        dataList = jData.rows;
                    }
                    else {
                        for (var i = 0; i < jData.rows.length; i++) {
                            dataList.push(jData.rows[i]);
                        }
                    }
                    if (jData.rows.length > 0) {
                        pageIndex++;
                        DlgUsers.GetUserList(pageIndex, pageSize, dataList, callBackFun, values);
                    }
                    else {
                        Common.OnProgressStop();
                        if (typeof (eval(callBackFun)) == 'function') {
                            callBackFun($('#cbgUser'), dataList, values);
                        }
                    }
                }
            });
        }
        catch (e) {
            Common.OnProgressStop();
        }
        
    },
    GetUsersInRole: function (roleName, values) {
        var sData = '{"roleName":"' + roleName + '"}';
        $.ajax({
            url: "/wms/Services/SecurityService.svc/GetUsersInRole",
            type: "post",
            data: sData,
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                if (result.ResCode != 1000) {
                    if (result.Msg != "") {
                        $.messager.alert('系统提示', result.Msg, 'info');
                    }
                    return false;
                }
                var jData = JSON.parse(result.Data);
                DlgUsers.CbgUser($('#cbgUser'), jData, values);
            }
        });
    }
}