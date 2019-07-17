var AddOrderSend = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        var Id = $('[id$=hId]').val();
        if (Id != '') {
            AddOrderSend.GetOrderSendInfo(Id);
        }
        else {
            $('#lbtnSave').linkbutton('enable');
        }
    },
    InitEdit: function (data) {
        if (data.StayQty == 0) $('#lbtnSave').linkbutton('enable');
        else $('#lbtnSave').linkbutton('disable');
        if (data.Status == 0) {
            $('#lbtnCreate').linkbutton('enable');
            $('#abtnAddProduct').linkbutton('enable');
        }
        else {
            $('#lbtnCreate').linkbutton('disable');
            $('#abtnAddProduct').linkbutton('disable');
        }
        $('#lbOrderCode').text(data.OrderCode);
        $('#hCustomerId').val(data.CustomerId);
        $('#txtStatus').val(data.Status);
        $('#txtCustomer').textbox('setText', data.CustomerCode + data.CustomerName);
    },
    JData:null,
    GetOrderSendInfo: function (Id) {
        var sData = {Id:Id};
        //console.log('sData--' + sData);
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetOrderSendInfo",
            type: "get",
            data: sData,
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                console.log('GetOrderSendInfo--result--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }
                //console.log('result.Data--' + result.Data);
                AddOrderSend.JData = JSON.parse(result.Data);
                AddOrderSend.InitEdit(AddOrderSend.JData);
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
        console.log('sData--' + sData);
        $.ajax({
            url: "/wms/Services/WmsService.svc/SaveOrderSend",
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
    },
    OnCreate: function () {
        var Id = $.trim($('[id$=hId]').val());
        if (Id == '') {
            return false;
        }
        var rows = $('#dgOrderProduct').datagrid('getRows');
        if (!rows || rows.length == 0) {
            $.messager.alert('提示', '请添加明细后再执行此操作', 'error');
            return false;
        }
        var isRight = true;
        for (var i = 0; i < rows.length; i++) {
            var row = rows[i];
            if (parseInt(row.Qty) == 0) {
                isRight = false;
                break;
            }
        }
        if (!isRight) {
            $.messager.alert('提示', "明细中存在未处理的数据行，请正确操作！", 'error');
            return false;
        }

        $.messager.confirm('温馨提醒', '确定要生成拣货任务吗？', function (r) {
            if (r) {
                $.ajax({
                    url: "/wms/Services/WmsService.svc/CreateOrderPicked",
                    type: "post",
                    data: '{"itemAppend":"' + Id + '"}',
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
                        $('#lbtnCreate').linkbutton('disable');
                        $('#abtnAddProduct').linkbutton('disable');
                        jeasyuiFun.show("温馨提示", "操作成功！");
                    }
                });
            }
        });
    }
}