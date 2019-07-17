var ListLogisticsDistribution = {
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
        var keyword = $("#txtKeyword").textbox('getValue');
        var postData = { "ReqName": "GetLogisticsDistributionList", "PageIndex": "" + pageIndex + "", "PageSize": "" + pageSize + "", "Keyword": "" + keyword + "" };
        Common.AjaxPost("/wms/h/users.html", postData, function (result) {
            //console.log('GetLogisticsDistributionList--' + result.Data);
            var jData = JSON.parse(result.Data);
            $("#dg").datagrid('loadData', jData);
            if (jData.IsSelfView == 'True') {
                $('#dgToolbar').children().each(function () {
                    var lbtnText = $.trim($(this).find('.l-btn-text').text());
                    if (lbtnText == '新增' || lbtnText == '编辑' || lbtnText == '查看' || lbtnText == '删除') {
                        $(this).remove();
                    }
                })
                $("#dg").datagrid('hideColumn', 'Id');
            }
            else {
                $("#dg").datagrid('hideColumn', 'ToDo');
            }
        })
    },
    OnSearch: function () {
        ListLogisticsDistribution.LoadDg(1, 10);
    },
    OnAdd: function () {
        window.location = '/wms/u/yt.html';
    },
    OnEdit: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        window.location = '/wms/u/yt.html?Id=' + rows[0].Id + ''
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
                var postData = { "ReqName": "DeleteLogisticsDistribution", "ItemAppend": "" + itemAppend + "" };
                Common.AjaxPost("/wms/h/content.html", postData, function (result) {
                    jeasyuiFun.show("温馨提示", "操作成功！");
                    setTimeout(function () {
                        ListLogisticsDistribution.LoadDg(1, 10);
                    }, 700);
                })
            }
        });
    },
    FToDo: function (value, row, index) {
        var name = "";
        if (!row.Status || row.Status == "" || row.Status == "新建") name = "派送";
        else name = row.Status;
        return '<a class="abtn" Code="' + index + '" onclick="ListLogisticsDistribution.OnToDo(this)">' + name + '</a>';
    },
    OnToDo: function (t) {
        var $this = $(t);
        var rowIndex = parseInt($this.attr('Code'));
        //$('#dg').datagrid('unselectAll');
        //$('#dg').datagrid('acceptChanges');
        //$('#dg').datagrid('selectRow', rowIndex);
        var name = $.trim($this.text());
        ListLogisticsDistribution.DlgAccept(name, rowIndex);

        return false;
    },
    DlgAccept: function (name, rowIndex) {
        var rows = $('#dg').datagrid('getRows');
        var jRow = rows[rowIndex];
        if (!jRow) {
            $.messager.alert('错误提示', "数据存在异常，无法操作！", 'error');
            return false;
        }
        var dlgId = name == "派送" ? "dlgAddAccept" : "dlgAddReply";
        var s = '';
        s += '<div class="row-fl"><span class="rl">状态：</span><div class="fl" style="height:22px;"><input id="cbbDeliveryStatus" data-options="required:true,editable:false,panelHeight:100,onSelect:ListLogisticsDistribution.OnCbbStatusSelect,validType:\'select\'" class="txt200" style="height:22px;" /></div></div>';
        s += '<div id="agreeBox">';
        s += '<div class="row-fl"><span class="rl">车牌号：</span><div class="fl"><input id="txtDeliveryVehicleID" class="txt200" value="' + jRow.DeliveryVehicleID + '" /></div></div>';
        s += '<div class="row-fl"><span class="rl">司机姓名：</span><div class="fl"><input id="txtDriverName" class="txt200" value="' + jRow.DriverName + '" /></div></div>';
        s += '<div class="row-fl"><span class="rl">司机手机号：</span><div class="fl"><input id="txtDriverPhone" class="easyui-validatebox txt200" data-options="validType:\'phone\'" value="' + jRow.DriverPhone + '" /></div></div>';
        s += '<div class="row-fl"><span class="rl">出发时间：</span><div class="fl"><input id="txtDeliveryStartTime" class="easyui-datetimebox txt200" data-options="showSeconds:false,editable:false" value="' + Common.FDateTime(jRow.DeliveryStartTime) + '" /></div></div>';
        s += '</div>';
        s += '<div id="causeBox" style="display:none;">';
        s += '<span class="clr"></span>';
        s += '<div class="row mt10"><span class="rl">原因：</span><div class="fl"><input id="txtCauseAbout" class="easyui-textbox" data-options="required:true,multiline:true" value="' + jRow.CauseAbout + '" style="width:550px;height:100px;" /></div></div>';
        s += '</div>';
        s += '<span class="clr"></span>';
        var w = $(window).width();
        var h = $(window).height();
        if (w > 750) w = 750;
        else w = w * 0.9;
        if (h > 300) h = 300;
        else h = h * 0.9;
        if ($("body").find("#" + dlgId + "").length == 0) {
            $("body").append("<div id=\"" + dlgId + "\" style=\"padding:10px;\"></div>");
        }
        var dlg = $("#" + dlgId + "");
        dlg.dialog({
            title: '填写信息',
            width: w,
            height: h,
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            closable:false,
            buttons: [{
                text: '提交', iconCls: 'icon-save', disabled: name != "派送", handler: function () {
                    ListLogisticsDistribution.OnDlgSave(name, rowIndex);
                }
            }, {
                text: '取消', iconCls: 'icon-cancel', handler: function () {
                    dlg.dialog('destroy');
                }
            }],
            content: '<form id="' + dlgId + 'Fm">' + s + '</form>',
            onOpen: function () {
                var jStatusData = [{ "Text": "请选择", "selected": true }, { "Text": "同意" }, { "Text": "拒绝" }];
                //if (name == "已派送") {
                //    jStatusData.push({ "Id": 3, "Text": "已完成" });
                //}
                $('#cbbDeliveryStatus').combobox({
                    valueField: 'Text',
                    textField:'Text',
                    data: jStatusData
                });
                if (jRow.DeliveryStatus && jRow.DeliveryStatus != '') $('#cbbDeliveryStatus').combobox('setValue', jRow.DeliveryStatus);
            }
        })
    },
    OnDlgSave: function (name, rowIndex) {
        try {
            var dlgId = name == "派送" ? "dlgAddAccept" : "dlgAddReply";
            var dlg = $("#" + dlgId + "");
            var rows = $('#dg').datagrid('getRows');
            var jRow = rows[rowIndex];
            if (!jRow) {
                $.messager.alert('错误提示', "数据存在异常，无法操作！", 'error');
                return false;
            }
            jRow.DeliveryStatus = $('#cbbDeliveryStatus').combobox('getText');
            if (jRow.DeliveryStatus == "拒绝") jRow.Status = "已拒绝";
            else {
                var status = name == "派送" ? "已派送" : "已答复";
                jRow.Status = status;
            }
            var isValid = false;
            var isValid = $('#' + dlgId + 'Fm').form('validate');
            if (!isValid) return false;
            if (name == "派送") {
                var deliveryVehicleID = $.trim($("#txtDeliveryVehicleID").val());
                var driverName = $.trim($("#txtDriverName").val());
                var driverPhone = $.trim($("#txtDriverPhone").val());
                var deliveryStartTime = $.trim($("#txtDeliveryStartTime").datetimebox('getValue'));
                jRow.DeliveryVehicleID = deliveryVehicleID;
                jRow.DriverName = driverName;
                jRow.DriverPhone = driverPhone;
                jRow.DeliveryStartTime = deliveryStartTime;
                jRow.CauseAbout = $('#txtCauseAbout').textbox('getValue');
            }
            else {

            }

            jRow.ReqName = "SaveLogisticsDistribution";
            var postData = jRow;
            //console.log('postData--' + JSON.stringify(postData));
            //return false;
            Common.AjaxPost("/wms/h/content.html", postData, function (result) {
                dlg.dialog('destroy');
                jeasyuiFun.show("温馨提示", "保存成功！");
                $('#dg').datagrid('refreshRow', rowIndex);
            })
        }
        catch (e) {
            $.messager.alert('错误提示', "数据存在异常，无法操作！", 'error');
        }
    },
    OnCbbStatusSelect: function (record) {
        $('#dlgAddAccept').form('clear');
        if (record.Text == '拒绝') {
            $('#agreeBox').hide();
            $('#causeBox').show();
            $('#txtCauseAbout').textbox('enableValidation');
        }
        else {
            $('#agreeBox').show();
            $('#causeBox').hide();
            $('#txtCauseAbout').textbox('disableValidation');
        }
    }
}