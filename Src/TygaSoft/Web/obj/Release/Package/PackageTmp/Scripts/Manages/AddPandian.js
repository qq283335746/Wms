var AddPandian = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        if ($('[id$=hId]').val() == '') {
            Common.OnProgressStart();
            DlgUsers.GetUserList(1, 100, null, DlgUsers.CbgUser);
            DlgCustomer.GetCustomerList(1, 100, null, DlgCustomer.CbgCustomer);
            DlgZone.GetZoneList(1, 100, null, DlgZone.CbgZone);
            DlgStockLocation.GetStockLocationList(1, 100, null, DlgStockLocation.CbgStockLocation, null, null);
        }
        else {
            AddPandian.GetPandianInfo();
        }
    },
    GetPandianInfo:function(){
        var sData = { Id: $('[id$=hId]').val() };
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetPandianInfo",
            type: "get",
            data: sData,
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                //console.log('GetPandianInfo--result--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }
                AddPandian.InitEdit(JSON.parse(result.Data));
            }
        });
    },
    InitEdit: function (data) {
        if (data.Status != '新建') {
            $('#lbtnSave').linkbutton('disable');
        }
        $('#lbOrderCode').text(data.OrderCode);
        $('#txtNamed').val(data.Named);
        $('#txtAllowUsers').val(data.AllowUsers);
        $('#txtRemark').val(data.Remark);
        $('#txtStockStartDate').datebox('setValue', new Date(data.StockStartDate).Format("yyyy-MM-dd"));
        $('#txtStockEndDate').datebox('setValue', new Date(data.StockEndDate).Format("yyyy-MM-dd"));
        $('#txtCustomers').val(data.Customers);
        Common.OnProgressStart();
        DlgUsers.GetUserList(1, 100, null, DlgUsers.CbgUser, data.AllowUsers);
        DlgCustomer.GetCustomerList(1, 100, null, DlgCustomer.CbgCustomer, data.Customers);
        DlgZone.GetZoneList(1, 100, null, DlgZone.CbgZone, data.Zones);
        DlgStockLocation.GetStockLocationList(1, 100, null, DlgStockLocation.CbgStockLocation, data.Zones, data.StockLocations);
    },
    OnSave: function () {
        var isValid = $('#form1').form('validate');
        if (!isValid) return false;
        var sId = $.trim($('[id$=hId]').val());
        var sOrderCode = $.trim($("#lbOrderCode").text().replace('系统自动生成',''));
        var sNamed = $.trim($("#txtNamed").val());
        var sAllowUsers = $.trim($("#cbgUser").combogrid('getValues'));
        var sRemark = $.trim($("#txtRemark").val());
        var sStockStartDate = $.trim($("#txtStockStartDate").datebox('getValue'));
        var sStockEndDate = $.trim($("#txtStockEndDate").datebox('getValue'));
        var sCustomers = $.trim($("#cbgCustomer").combogrid('getValues'));
        var sZones = $.trim($("#cbgZone").combogrid('getValues'));
        var sStockLocations = $.trim($("#cbgStockLocation").combogrid('getValues'));

        var sData = '{"model":{"Id":"' + sId + '","OrderCode":"' + sOrderCode + '","Named":"' + sNamed + '","AllowUsers":"' + sAllowUsers + '","Remark":"' + sRemark + '","StockStartDate":"' + sStockStartDate + '","StockEndDate":"' + sStockEndDate + '","Customers":"' + sCustomers + '","Zones":"' + sZones + '","StockLocations":"' + sStockLocations + '"}}';
        //console.log('sData--' + sData);
        //return false;

        $.ajax({
            url: "/wms/Services/WmsService.svc/SavePandian",
            type: "post",
            data: sData,
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $.messager.progress({ title: '请稍等', msg: '正在执行...' });
            },
            complete: function () {
                $.messager.progress('close');
            },
            success: function (result) {
                //console.log('SavePandian--result--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }
                jeasyuiFun.show("温馨提示", "保存成功！");
                setTimeout(function () {
                    var sHref = window.location.href;
                    if (sHref.indexOf('?Id=') == -1) sHref += '?Id=' + result.Data + '';
                    window.location = sHref;
                }, 700);
            }
        });
    }
}