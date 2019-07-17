var AddOrderPicked = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        var Id = $('[id$=hId]').val();
        if (Id != '') {
            AddOrderPicked.GetOrderPickedInfo(Id);
        }
    },
    InitEdit: function (data) {
        $('#lbOrderCode').text(data.OrderCode);
        $('#hCustomerId').val(data.CustomerId);
        $('#txtStatus').val(data.Status);
        $('#txtCustomer').textbox('setText', data.CustomerCode + data.CustomerName);
    },
    GetOrderPickedInfo: function (Id) {
        var sData = {Id:Id};
        //console.log('sData--' + sData);
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetOrderPickedInfo",
            type: "get",
            data: sData,
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
            },
            complete: function () {
            },
            success: function (result) {
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }
                //console.log('result.Data--' + result.Data);
                AddOrderPicked.InitEdit(JSON.parse(result.Data));
            }
        });
    },
    OnSave: function () {
        var sId = $.trim($("#hId").val());
        var sOrderCode = $.trim($("#lbOrderCode").text());
        var sCustomerId = $("#hCustomerId").val();
        var sStatus = $.trim($("#txtStatus").val());
        if (sCustomerId == '') {
            $.messager.alert('错误提示', "带有*号的为必须项", 'error');
            return false;
        }

        var sData = '{"Id":"' + sId + '","OrderCode":"' + sOrderCode + '","CustomerId":"' + sCustomerId + '"}';
        //console.log('sData--' + sData);
        $.ajax({
            url: "/wms/Services/WmsService.svc/SaveOrderPicked",
            type: "post",
            data: '{"model":' + sData + '}',
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
                jeasyuiFun.show("温馨提示", "保存成功！");
                setTimeout(function () {
                    window.location = "/wms/a/gsend.html?Id=" + result.Data + "";
                }, 1000);
            }
        });
    }
}