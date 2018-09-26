var AddSupplier = {
    Init: function () {
        //this.InitEvent();
    },
    InitEvent: function () {
        $("#btnSave").click(function () {
            AddSupplier.OnSave();
        })
    },
    Container: $("#dlgAddSupplier"),
    OnSave: function () {
        var isValid = $('#dlgFm').form('validate');
        if (!isValid) return false;
        var sId = $.trim($("#hId").val());
        var sCoded = $.trim($("#txtSupplierCode").val());
        var sNamed = $.trim($("#txtSupplierName").val());
        var sSquare = $.trim($("#txtSquare").val());
        var sDescr = $.trim($("#txtDescr").val());
        var sShortName = $.trim($("#txtShortName").val());
        var sContactMan = $.trim($("#txtContactMan").val());
        var sEmail = $.trim($("#txtEmail").val());
        var sPhone = $.trim($("#txtPhone").val());
        var sTelPhone = $.trim($("#txtTelPhone").val());
        var sFax = $.trim($("#txtFax").val());
        var sPostcode = $.trim($("#txtPostcode").val());
        var sAddress = $.trim($("#txtAddress").val());
        var sRemark = $.trim($("#txtRemark").val());

        var postData = { "ReqName": "SaveSupplier", "Id": "" + sId + "", "Coded": "" + sCoded + "", "Named": "" + sNamed + "","ShortName":"" + sShortName + "","ContactMan":"" + sContactMan + "","Email":"" + sEmail + "","Phone":"" + sPhone + "","TelPhone":"" + sTelPhone + "","Fax":"" + sFax + "","Postcode":"" + sPostcode + "","Address":"" + sAddress + "","Remark":"" + sRemark + "" };
        Common.AjaxPost("/wms/h/content.html", postData, function (result) {
            AddSupplier.Container.dialog('close');
            jeasyuiFun.show("温馨提示", "保存成功！");
            setTimeout(function () {
                ListSupplier.LoadDg(1, 10);
            }, 1000);
        })
    }
}