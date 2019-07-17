var AddZone = {
    Init: function () {
        //this.InitEvent();
    },
    InitEvent: function () {
        $("#btnSave").click(function () {
            AddZone.OnSave();
        })
    },
    Container: $("#dlgAddZone"),
    OnSave: function () {
        var isValid = $('#dlgFm').form('validate');
        if (!isValid) return false;
        var sId = $.trim($("#hId").val());
        var sCoded = $.trim($("#txtZoneCode").val());
        var sNamed = $.trim($("#txtZoneName").val());
        var sSquare = $.trim($("#txtSquare").val());
        var sDescr = $.trim($("#txtDescr").val());
        
        var postData = { "ReqName": "SaveZone", "Id": "" + sId + "", "Coded": "" + sCoded + "", "Named": "" + sNamed + "", "Square": "" + sSquare + "", "Descr": "" + sDescr + "" };
        Common.AjaxPost("/wms/h/content.html", postData, function (result) {
            AddZone.Container.dialog('close');
            jeasyuiFun.show("温馨提示", "保存成功！");
            setTimeout(function () {
                ListZone.LoadDg(1, 10);
            }, 1000);
        })
    }
}