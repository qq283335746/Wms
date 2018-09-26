var AddDeviceRepairRecord = {
    Init: function () {
        //this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {
        $("#btnSave").click(function () {
            AddDeviceRepairRecord.Save();
        })
    },
    InitData:function(){
        $("#txtRecordDate").datebox({ required: true });
    },
    Save: function () {
        
        var isValid = $('#dlgFm').form('validate');
        if (!isValid) return false;
        var sId = $.trim($("#hId").val());
        var whetherFix = $.trim($("#ddlWhetherFix>option:selected").text());
        var isBack = $.trim($("#ddlIsBack").val());
        var sRecordDate = $.trim($("#txtRecordDate").datebox('getValue'));
        var sCustomer = $.trim($("#txtCustomer").val());
        var sSerialNumber = $.trim($("#txtSerialNumber").val());
        if (sSerialNumber != '') sSerialNumber = encodeURIComponent(sSerialNumber);
        var sDeviceModel = $.trim($("#txtDeviceModel").val());
        var sFaultCause = $.trim($("#txtFaultCause").val());
        var sSolveMethod = $.trim($("#txtSolveMethod").val());
        var sCustomerProblem = $.trim($("#txtCustomerProblem").val());
        var sDevicePart = $.trim($("#txtDevicePart").val());
        var sTreatmentSituation = $.trim($("#txtTreatmentSituation").val());
        var sHandoverPerson = $.trim($("#txtHandoverPerson").val());
        var sBackDate = $.trim($("#txtBackDate").datebox('getValue'));
        var sRegisteredPerson = $.trim($("#txtRegisteredPerson").val());
        var sRemark = $.trim($("#txtRemark").val());

        var postData = JSON.parse('{"ReqName": "SaveInfoneDeviceRepairRecord","Id":"' + sId + '","RecordDate":"' + sRecordDate + '","Customer":"' + sCustomer + '","SerialNumber":"' + sSerialNumber + '","DeviceModel":"' + sDeviceModel + '","FaultCause":"' + sFaultCause + '","SolveMethod":"' + sSolveMethod + '","CustomerProblem":"' + sCustomerProblem + '","DevicePart":"' + sDevicePart + '","TreatmentSituation":"' + sTreatmentSituation + '","WhetherFix":"' + whetherFix + '","HandoverPerson":"' + sHandoverPerson + '","IsBack":"' + isBack + '","BackDate":"' + sBackDate + '","RegisteredPerson":"' + sRegisteredPerson + '","Remark":"' + sRemark + '"}');
        Common.AjaxPost("/wms/h/content.html", postData, function (result) {
            $("#dlgAddDeviceRepairRecord").dialog('close');
            jeasyuiFun.show("温馨提示", "操作成功！");
            setTimeout(function () {
                ListDeviceRepairRecord.LoadDg(1, 10);
            }, 700);
        })
    }
}