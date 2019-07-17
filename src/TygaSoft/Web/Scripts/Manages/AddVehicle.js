var AddVehicle = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        if (ListVehicle.SelectRow) {
            AddVehicle.InitEdit(ListVehicle.SelectRow);
        }
    },
    Container: $("#dlgAddVehicle"),
    InitEdit: function (data) {
        console.log('data--'+JSON.stringify(data));
        var contarner = AddVehicle.Container;
        contarner.find('#hId').val(data.Id);
        contarner.find('#txtVehicleID').val(data.VehicleID);
        contarner.find('#txtVehicleModel').val(data.VehicleModel);
        contarner.find('#txtLicence').val(data.Licence);
        contarner.find('#imgLicPic').attr({ 'src': data.LicPicUrl, 'code': data.LicPic });
        contarner.find('#txtOffenceRecord').val(data.OffenceRecord);
        contarner.find('#txtDriverID').val(data.DriverID);
        contarner.find('#imgDriverIDPicture').attr({ 'src': data.DriverIDPictureUrl, 'code': data.DriverIDPicture });
        contarner.find('#txtRewardRecord').val(data.RewardRecord);
        contarner.find('#txtRemark').val(data.Remark);
        contarner.find('#txtSort').val(data.Sort);
        //contarner.find('#txtIsDisable').val(data.IsDisable);
    },
    OnSave: function () {
        var isValid = $('#dlgFm').form('validate');
        if (!isValid) return false;
        var sId = $.trim($("#hId").val());
        var sVehicleID = $.trim($("#txtVehicleID").val());
        var sVehicleModel = $.trim($("#txtVehicleModel").val());
        var sLicence = $.trim($("#txtLicence").val());
        var sLicPic = $.trim($("#imgLicPic").attr('code'));
        var sOffenceRecord = $.trim($("#txtOffenceRecord").val());
        var sDriverID = $.trim($("#txtDriverID").val());
        var sDriverIDPicture = $.trim($("#imgDriverIDPicture").attr('code'));
        var sRewardRecord = $.trim($("#txtRewardRecord").val());
        var sRemark = $.trim($("#txtRemark").val());
        var sSort = $.trim($("#txtSort").val());
        if (sSort == '') sSort = 0;
        var sIsDisable = $.trim($("#txtIsDisable").val()) == '1';

        var sData = '{"Id":"' + sId + '","VehicleID":"' + sVehicleID + '","VehicleModel":"' + sVehicleModel + '","Licence":"' + sLicence + '","LicPic":"' + sLicPic + '","OffenceRecord":"' + sOffenceRecord + '","DriverID":"' + sDriverID + '","DriverIDPicture":"' + sDriverIDPicture + '","RewardRecord":"' + sRewardRecord + '","Remark":"' + sRemark + '","Sort":"' + sSort + '","IsDisable":"' + sIsDisable + '"}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/SaveVehicle",
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
                AddVehicle.Container.dialog('close');
                jeasyuiFun.show("温馨提示", "保存成功！");
                ListVehicle.LoadDg(1, 10);
            }
        });
    },
    CallBackByLicPic: function (data) {
        var item = data[0];
        $('#imgLicPic').attr({ 'src': item.Src, 'code': item.Id});
    },
    CallBackByDriverIDPicture: function (data) {
        var item = data[0];
        $('#imgDriverIDPicture').attr({ 'src': item.Src, 'code': item.Id });
    }
}