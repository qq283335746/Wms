var ListRoleMenu = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {
        $("[name=abtnSave]").click(function () {
            ListRoleMenu.OnSave();
        })
    },
    InitData: function () {
        this.InitForm();
        this.LoadTgMenu();
    },
    InitForm:function(){
        //var cbbSelect = $('#cbbSelect');
        //var data = [{ Text: "全选" }, { Text: "全不选" }, { Text: "反选" }];
        //cbbSelect.combobox({
        //    data: data,
        //    valueField: 'Text',
        //    textField: 'Text'
        //});
    },
    LoadTgMenu: function () {
        var tg = $("#tgMenu");
        var allowRole = $.trim($("#hAllowRole").val());
        var denyUser = $.trim($("#hDenyUser").val());
        $.ajax({
            url: Common.AppName + "/Services/SecurityService.svc/GetMenusTreeGrid",
            type: "POST",
            data: '{"model":{"AllowRole":"' + allowRole + '","DenyUser":"' + denyUser + '"}',
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                Common.OnProgressStart();
            },
            complete: function () {
                Common.OnProgressStop();
            },
            success: function (result) {
                //console.log('GetMenusTreeGrid--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return;
                }
                tg.treegrid('loadData', eval("(" + result.Data + ")"));
            }
        });
    },
    OnClickCellByTgMenu: function (field, row) {
        var tg = $("#tgMenu");
        //var allowRole = $.trim($("#hAllowRole").val());
        //var denyUser = $.trim($("#hDenyUser").val());

        switch (field) {
            case "IsView":
                tg.treegrid('update', {
                    id: row.Id,
                    row: {
                        IsView: row.IsView == '1' ? '0':'1'
                    }
                });
                break;
            case "IsAdd":
                tg.treegrid('update', {
                    id: row.Id,
                    row: {
                        IsAdd: row.IsAdd == '1' ? '0' : '1'
                    }
                });
                break;
            case "IsEdit":
                tg.treegrid('update', {
                    id: row.Id,
                    row: {
                        IsEdit: row.IsEdit == '1' ? '0' : '1'
                    }
                });
                break;
            case "IsDel":
                tg.treegrid('update', {
                    id: row.Id,
                    row: {
                        IsDel: row.IsDel == '1' ? '0' : '1'
                    }
                });
                break;
            default:
                break;
        }
    },
    FormatterCheckbox: function (value, row, index) {
        if (value == "1") {
            return '<input type="checkbox" checked="checked" />';
        }
        else {
            return '<input type="checkbox" />';
        }
    },
    TreeGridItems: null,
    GetChildren:function(tg,parentNode){
        if (parentNode && $.trim(parentNode.Id) != "") {
            var childNodes = tg.treegrid('getChildren', parentNode.Id);
            for (var i = 0; i < childNodes.length; i++) {
                var row = childNodes[i];
                ListRoleMenu.TreeGridItems.push("" + row.Id + "|" + row.IsView + "|" + row.IsAdd + "|" + row.IsEdit + "|" + row.IsDel + "");
            }
        }
    },
    OnSave: function () {
        var sRoleName = $.trim($("#hAllowRole").val());
        var sUserName = $.trim($("#hDenyUser").val());
        if (sRoleName == '' && sUserName == '')
        {
            $.messager.alert('错误提示', "参数值有误，请正确操作！", 'error');
            return false;
        }
        ListRoleMenu.TreeGridItems = new Array();
        var tg = $("#tgMenu");
        var roots = tg.treegrid('getRoots');
        for (var i = 0; i < roots.length; i++) {
            var row = roots[i];
            ListRoleMenu.TreeGridItems.push("" + row.Id + "|" + row.IsView + "|" + row.IsAdd + "|" + row.IsEdit + "|" + row.IsDel + "");
            ListRoleMenu.GetChildren(tg, row);
        }
        if (ListRoleMenu.TreeGridItems.length == 0) {
            $.messager.alert('错误提示', "无任何数据", 'error');
            return false;
        }
        var rows = ListRoleMenu.TreeGridItems;

        var sJson = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) sJson += ",";
            var arr = rows[i].split('|');
            var isView = arr[1] == '1';
            var isAdd = arr[2] == '1';
            var isEdit = arr[3] == '1';
            var isDel = arr[4] == '1';
            sJson += '{"MenuId":"' + arr[0] + '","IsView":' + isView + ',"IsAdd":' + isAdd + ',"IsEdit":' + isEdit + ',"IsDelete":' + isDel + '}';
        }
        sJson = "[" + sJson + "]";
        //console.log('sJson--' + sJson);

        var postData = { "ReqName": "SaveMenuAccess", "RoleName": "" + sRoleName + "", "UserName": "" + sUserName + "", "MenuItemJson": "" + encodeURIComponent(sJson) + "" };
        //console.log('postData--' + JSON.stringify(postData));
        //return false;
        Common.AjaxPost("/wms/h/content.html", postData, function (result) {
            jeasyuiFun.show("温馨提示", "保存成功！");
        })
    },
    OnSelectOption: function (n) {
        var tg = $("#tgMenu");
        var data = tg.treegrid('getData');
        ListRoleMenu.DataChanged(data, n);
        tg.treegrid('loadData', data);
    },
    DataChanged: function (data, n) {
        var status = "1";
        if (n == 2) status = "0";
        for (var i = 0; i < data.length; i++) {
            if (n == 3) {
                if (data[i].IsView == "1") data[i].IsView = "0";
                else data[i].IsView = "1";

                if (data[i].IsAdd == "1") data[i].IsAdd = "0";
                else data[i].IsAdd = "1";

                if (data[i].IsEdit == "1") data[i].IsEdit = "0";
                else data[i].IsEdit = "1";

                if (data[i].IsDel == "1") data[i].IsDel = "0";
                else data[i].IsDel = "1";
            }
            else {
                data[i].IsView = status;
                data[i].IsAdd = status;
                data[i].IsEdit = status;
                data[i].IsDel = status;
            }
            
            var childData = data[i].children;
            if (childData && childData.length > 0) {
                ListRoleMenu.DataChanged(childData, n);
            }
        }
    }
}