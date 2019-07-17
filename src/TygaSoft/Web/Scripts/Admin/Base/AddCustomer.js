var AddCustomer = {
    Init: function () {
        //this.InitEvent();
    },
    InitEvent: function () {
        $("#btnSave").click(function () {
            AddCustomer.Save();
        })
    },
    Container: $("#dlgAddCustomer"),
    Save: function () {
        var isValid = $('#dlgFm').form('validate');
        if (!isValid) return false;
        var Id = $.trim($("#hId").val());
        
        var sData = '{"Id":"' + Id + '","CustomerCode":"' + $.trim($("#txtCustomerCode").val()) + '","CustomerName":"' + $.trim($("#txtCustomerName").val()) + '","ShortName":"' + $.trim($("#txtShortName").val()) + '","ContactMan":"' + $.trim($("#txtContactMan").val()) + '","Email":"' + $.trim($("#txtEmail").val()) + '","Phone":"' + $.trim($("#txtPhone").val()) + '","TelPhone":"' + $.trim($("#txtTelPhone").val()) + '","Fax":"' + $.trim($("#txtFax").val()) + '","Postcode":"' + $.trim($("#txtPostcode").val()) + '","Address":"' + $.trim($("#txtAddress").val()) + '","Remark":"' + $.trim($("#txtRemark").val()) + '"}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/SaveCustomer",
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
                AddCustomer.Container.dialog('close');
                jeasyuiFun.show("温馨提示", "保存成功！");
                setTimeout(function () {
                    ListCustomer.LoadDg(1, 10);
                },700)
            }
        });
    }
}