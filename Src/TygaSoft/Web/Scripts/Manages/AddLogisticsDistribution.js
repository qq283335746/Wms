var AddLogisticsDistribution = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        if ($('[id$=hId]').val() == '') {
            DlgOrder.GetOrderList(1, 100, null, DlgOrder.CbgOrder);
            DlgCompany.GetCompanyList(1, 100, null, DlgCompany.CbgCompany);
            DlgVehicle.GetVehicleList(1, 100, null, DlgVehicle.CbgVehicle);
        }
        else {
            AddLogisticsDistribution.GetInfo();
        }
    },
    GetInfo: function () {
        var jData = { ReqName: 'GetLogisticsDistributionInfo', Id: $('[id$=hId]').val() };
        Common.AjaxPost("/wms/h/users.html", jData, function (result) {
            AddLogisticsDistribution.InitEdit(JSON.parse(result.Data));
        })
    },
    InitEdit: function (data) {
        console.log('data--' + JSON.stringify(data));
        $('#lbtnSave').linkbutton('disable');
        DlgOrder.GetOrderList(1, 100, null, DlgOrder.CbgOrder, data.RefOrders, true);
        DlgCompany.GetCompanyList(1, 100, null, DlgCompany.CbgCompany, data.CompanyId, true);
        DlgVehicle.GetVehicleList(1, 100, null, DlgVehicle.CbgVehicle, data.Vehicles, true);
        $('#lbOrderCode').text(data.OrderCode);
        $('#lbTotalPackage').text(data.TotalPackage);
        $('#lbTotalVolume').text(data.TotalVolume);
        $('#lbTotalWeight').text(data.TotalWeight);
        $('#txtToAddress').val(data.ToAddress);
        $('#txtRemark').val(data.Remark);
        if (data.TypeName == '物流公司') {
            $('#tabsOne').tabs('close', 1);
        }
        else {
            $('#tabsOne').tabs('close', 2);
        }
    },
    OnSave: function () {
        var isValid = $('#form1').form('validate');
        if (!isValid) return false;
        var sOrderCode = $.trim($("#lbOrderCode").text()).replace("系统自动生成","");
        var sRefOrders = $.trim($("#cbgOrder").combogrid('getValues'));
        var sCompanyId = $.trim($("#cbgCompany").combogrid('getValues'));
        var sVehicles = $.trim($("#cbgVehicle").combogrid('getValues'));
        var sTotalPackage = $.trim($('#lbTotalPackage').text());
        var sTotalVolume = $.trim($('#lbTotalVolume').text());
        var sTotalWeight = $.trim($('#lbTotalWeight').text());
        var sToAddress = $.trim($("#txtToAddress").val());
        var sTypeName = sCompanyId != '' ? '物流公司' : '自配';
        var sRemark = $.trim($("#txtRemark").val());
        if (sCompanyId == '' && sVehicles == '') {
            $.messager.alert('错误提示', "物流公司和车辆必选其一！", 'error');
            return false;
        }
        var jData = { "ReqName": "SaveLogisticsDistribution", "OrderCode": "" + sOrderCode + "", "RefOrders": "" + sRefOrders + "", "CompanyId": "" + sCompanyId + "", "Vehicles": "" + sVehicles + "", "TotalPackage": "" + sTotalPackage + "", "TotalVolume": "" + sTotalVolume + "", "TotalWeight": "" + sTotalWeight + "", "ToAddress": "" + sToAddress + "", "TypeName": "" + sTypeName + "", "Remark": "" + sRemark + "", "DeliveryVehicleID": "", "DriverName": "", "DriverPhone": "", "DeliveryStartTime": "", "Status": "" };
        //console.log('jData--' + JSON.stringify(jData));
        $.messager.confirm('温馨提醒', '保存后将不能再修改，确定要保存吗？', function (r) {
            if (r) {
                Common.AjaxPost("/wms/h/content.html", jData, function (result) {
                    //console.log('result--' + result.Data);
                    jeasyuiFun.show("温馨提示", "保存成功！");
                    setTimeout(function () {
                        window.location = '/wms/u/ta.html';
                    }, 700);
                });
            }
        });
    },
    OnTabsSelect: function (title, index) {
        if ($.trim($('[id$=hId]').val()) == '') {
            switch(index){
                case 1:
                    $('#cbgVehicle').combogrid('clear');
                    break;
                case 2:
                    $('#cbgCompany').combogrid('clear');
                    break;
                default:
                    break;
            }
        }
    },
    GetTotalOrderSendProduct: function (rows) {
        var Ids = '';
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) Ids += ',';
            Ids += rows[i].Id;
        }
        if (Ids != '') {
            var jData = { ReqName: 'GetTotalOrderSendProduct', OrderIds: Ids };
            Common.AjaxPost("/wms/h/users.html", jData, function (result) {
                var data = JSON.parse(result.Data)
                $('#lbTotalPackage').text(data.TotalPackage);
                $('#lbTotalVolume').text(data.TotalVolume);
                $('#lbTotalWeight').text(data.TotalWeight);
            })
        }
    }
}