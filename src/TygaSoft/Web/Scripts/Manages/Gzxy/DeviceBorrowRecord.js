var DeviceBorrowRecord = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        this.InitForm();
        setTimeout(function () {
            DeviceBorrowRecord.Load(1, 10);
        })
    },
    InitForm:function(){
        DeviceBorrowRecord.CbbIsBack('cbbSIsBack', '');
        var pager = $("#dg").datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                DeviceBorrowRecord.Load(pageNumber, pageSize);
            }
        });
    },
    SelectRow: null,
    Load: function (pageIndex, pageSize) {
        var searchItem = DeviceBorrowRecord.GetSearchItem();
        var postData = { "ReqName": "GetInfoneDeviceBorrowRecordList", "PageIndex": "" + pageIndex + "", "PageSize": "" + pageSize + "", "Keyword": "" + searchItem.Keyword + "", "TypeName": "" + Common.GetQueryString("funType") + "", "StartDate": "" + searchItem.StartDate + "", "EndDate": "" + searchItem.EndDate + "", "BackDate": "" + searchItem.BackDate + "", "IsBack": "" + searchItem.IsBack + "" };
        Common.AjaxPost("/wms/h/users.html", postData, function (result) {
            //console.log('result--' + JSON.stringify(result));
            $("#dg").datagrid('loadData', JSON.parse(result.Data));
        })
    },
    OnSearch: function () {
        var pager = $("#dg").datagrid('getPager');
        DeviceBorrowRecord.Load(1, pager.pagination('options').pageSize);
    },
    GetSearchItem: function () {
        var sKeyword = $("#txtKeyword").textbox('getValue');
        var sStartDate = $('#txtSStartDate').datebox('getValue');
        var sEndDate = $('#txtSEndDate').datebox('getValue');
        var sBackDate = $('#txtSBackDate').datebox('getValue');
        var sIsBack = $.trim($('#cbbSIsBack').combobox('getValue')).replace('请选择', "");
        if (sIsBack != '') {
            if (sIsBack == '是') sIsBack = true;
            else sIsBack = false;
        }
        return { Keyword: sKeyword, StartDate: sStartDate, EndDate: sEndDate, BackDate: sBackDate, IsBack: sIsBack };
    },
    Add: function () {
        DeviceBorrowRecord.SelectRow = null;
        if ($("body").find("#dlgDeviceBorrowRecord").length == 0) {
            $("body").append("<div id=\"dlgDeviceBorrowRecord\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(720, 520);
        $("#dlgDeviceBorrowRecord").dialog({
            title: '填写信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    DeviceBorrowRecord.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgDeviceBorrowRecord').dialog('close');
                }
            }],
            href: '/wms/u/yinfone.html',
            onLoad: function () {
                DeviceBorrowRecord.CbbIsBack('cbbIsBack', '');
                DeviceBorrowRecord.CbbPartStatus('cbbPartStatus', '');
            }
        })
        return false;
    },
    Edit: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        DeviceBorrowRecord.SelectRow = rows[0];
        if ($("body").find("#dlgDeviceBorrowRecord").length == 0) {
            $("body").append("<div id=\"dlgDeviceBorrowRecord\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(720, 520);
        $("#dlgDeviceBorrowRecord").dialog({
            title: '编辑信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    DeviceBorrowRecord.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgDeviceBorrowRecord').dialog('close');
                }
            }],
            href: '/wms/u/yinfone.html',
            onLoad: function () {
                DeviceBorrowRecord.SetEdit(rows[0]);
            }
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
                var postData = { "ReqName": "DeleteInfoneDeviceBorrowRecord", "ItemAppend": "" + itemAppend + "" };
                Common.AjaxPost("/wms/h/content.html", postData, function (result) {
                    jeasyuiFun.show("温馨提示", "操作成功！");
                    setTimeout(function () {
                        DeviceBorrowRecord.Load(1, 10);
                    }, 700);
                });
            }
        });
    },
    Save: function () {
        var isValid = $('#dlgFm').form('validate');
        if (!isValid) return false;
        var id = $.trim($("#hId").val());
        var recordDate = $.trim($("#txtRecordDate").datebox('getValue'));
        var customer = $.trim($("#txtCustomer").val());
        var customerContact = $.trim($("#txtCustomerContact").val());
        var serialNumber = $.trim($("#txtSerialNumber").val());
        var deviceModel = $.trim($("#txtDeviceModel").val());
        var devicePart = $.trim($("#txtDevicePart").val());
        var partStatus = $.trim($("#txtPartStatus").val());
        var sPartStatus = $.trim($("#cbbPartStatus").combobox('getValue'));
        if (sPartStatus == '齐全') partStatus = sPartStatus;
        else if (partStatus == '') partStatus = sPartStatus;
        var projectAbout = $.trim($("#txtProjectAbout").val());
        var saleMan = $.trim($("#txtSaleMan").val());
        var sendOrderCode = $.trim($("#txtSendOrderCode").val());
        var isBack = $.trim($("#cbbIsBack").combobox('getValue')) == '是';
        var backDate = $.trim($("#txtBackDate").datebox('getValue'));
        var register = $.trim($("#txtRegister").val());
        var remark = $.trim($("#txtRemark").val());
        var funType = Common.GetQueryString('funType');

        var postData = { "ReqName": "SaveInfoneDeviceBorrowRecord", "Id": "" + id + "", "Customer": "" + customer + "", "CustomerContact": "" + customerContact + "", "SerialNumber": "" + serialNumber + "", "DeviceModel": "" + deviceModel + "", "DevicePart": "" + devicePart + "", "PartStatus": "" + partStatus + "", "ProjectAbout": "" + projectAbout + "", "SaleMan": "" + saleMan + "", "SendOrderCode": "" + sendOrderCode + "", "IsBack": "" + isBack + "", "BackDate": "" + backDate + "", "Register": "" + register + "", "Remark": "" + remark + "", "FunType": "" + funType + "", "RecordDate": "" + recordDate + "" };
        //console.log('postData--' + JSON.stringify(postData));
        //return false;
        Common.AjaxPost("/wms/h/content.html", postData, function (result) {
            $("#dlgDeviceBorrowRecord").dialog('close');
            jeasyuiFun.show("温馨提示", "保存成功！");
            setTimeout(function () {
                DeviceBorrowRecord.Load(1, 10);
            }, 700);
        })
    },
    SetEdit: function (data) {
        var contarner = $('#dlgDeviceBorrowRecord');
        contarner.find('#hId').val(data.Id);
        contarner.find('#txtCustomer').val(data.Customer);
        contarner.find('#txtCustomerContact').val(data.CustomerContact);
        contarner.find('#txtSerialNumber').val(data.SerialNumber);
        contarner.find('#txtDeviceModel').val(data.DeviceModel);
        contarner.find('#txtDevicePart').val(data.DevicePart);
        contarner.find('#txtPartStatus').val(data.PartStatus);
        contarner.find('#txtProjectAbout').val(data.ProjectAbout);
        contarner.find('#txtSaleMan').val(data.SaleMan);
        contarner.find('#txtSendOrderCode').val(data.SendOrderCode);
        contarner.find('#txtBackDate').datebox('setValue', data.SBackDate);
        contarner.find('#txtRegister').val(data.Register);
        contarner.find('#txtRemark').val(data.Remark);
        contarner.find('#txtRecordDate').datebox('setValue', data.SRecordDate);
        DeviceBorrowRecord.CbbIsBack('cbbIsBack', data.IsBack);
        DeviceBorrowRecord.CbbPartStatus('cbbPartStatus', data.PartStatus);
    },
    OnExport: function () {
        var searchItem = this.GetSearchItem();
        var queryStr = '&TypeName=' + Common.GetQueryString("funType") + '&Keyword=' + searchItem.Keyword + '&StartDate=' + searchItem.StartDate + '&EndDate=' + searchItem.EndDate + '&BackDate=' + searchItem.BackDate + '&IsBack=' + searchItem.IsBack + '';
        $.messager.confirm('提示', '确定要导出数据吗？', function (r) {
            if (r) {
                window.open("/wms/h/upload.html?ReqName=ExportDeviceBorrowRecord" + queryStr + "");
            }
        })
    },
    CbbPartStatus: function (cbbId, v) {
        var cbb = $('#' + cbbId + '');
        var jData = [{ "Name": "齐全" }, { "Name": "不齐全" }];

        cbb.combobox({
            valueField: 'Name',
            textField: 'Name',
            data: jData,
            onLoadSuccess: function () {
                if (v != "") {
                    if (v == jData[0].Name) cbb.combobox('setValue', v);
                    else {
                        cbb.combobox('setValue', jData[1].Name);
                        $('#txtPartStatus').val(v);
                    }
                }
                else {
                    cbb.combobox('select', jData[0].Name);
                }
            },
            onChange: function (newValue, oldValue) {
                if (newValue == '不齐全') {
                    $('#txtPartStatus').parent().show();
                    $('#panelPartStatus').removeAttr('style');
                }
                else {
                    $('#txtPartStatus').parent().hide();
                    $('#panelPartStatus').css('width','207px');
                }
            }
        });
    },
    CbbIsBack: function (cbbId, v) {
        var cbb = $('#' + cbbId + '');
        var jData = [{ "Name": "是" }, { "Name": "否" }];
        if (cbbId == 'cbbSIsBack') jData.splice(0, 0, { "Name": "请选择" });

        cbb.combobox({
            valueField: 'Name',
            textField: 'Name',
            data: jData,
            onLoadSuccess: function () {
                if (v != "") {
                    if (v == 'True' || v) v = '是';
                    cbb.combobox('setValue', v);
                }
                else {
                    if (cbbId == 'cbbSIsBack') cbb.combobox('select', jData[0].Name);
                    else cbb.combobox('select', jData[1].Name);
                }
            }
        });
    }
}