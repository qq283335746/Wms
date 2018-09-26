var AddOrderReceiptRecord = {
    Init: function () {
        //this.InitEvent();
    },
    InitEvent: function () {
        $("#btnSave").click(function () {
            AddOrderReceiptRecord.Save();
        })
    },
    Container: $("#dlgAddOrderReceiptRecord"),
    Save: function () {
        var isValid = $('#dlgFm').form('validate');
        if (!isValid) return false;
        var Id = $.trim($("#hId").val());
        
        var sData = '{"Id":"' + Id + '","OrderReceiptRecordCode":"' + $.trim($("#txtOrderReceiptRecordCode").val()) + '","OrderReceiptRecordName":"' + $.trim($("#txtOrderReceiptRecordName").val()) + '","ShortName":"' + $.trim($("#txtShortName").val()) + '","ContactMan":"' + $.trim($("#txtContactMan").val()) + '","Email":"' + $.trim($("#txtEmail").val()) + '","Phone":"' + $.trim($("#txtPhone").val()) + '","TelPhone":"' + $.trim($("#txtTelPhone").val()) + '","Fax":"' + $.trim($("#txtFax").val()) + '","Postcode":"' + $.trim($("#txtPostcode").val()) + '","Address":"' + $.trim($("#txtAddress").val()) + '","Remark":"' + $.trim($("#txtRemark").val()) + '"}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/SaveOrderReceiptRecord",
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
                AddOrderReceiptRecord.Container.dialog('close');
                jeasyuiFun.show("温馨提示", "保存成功！");
                setTimeout(function () {
                    window.location = "/wms/a/tginstore.html";
                }, 1000)
            }
        });
    }
}