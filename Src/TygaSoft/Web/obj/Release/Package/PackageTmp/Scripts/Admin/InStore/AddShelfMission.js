var AddShelfMission = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        var Id = $.trim($('[id$=hId]').val());
        if (Id != '') {
            AddShelfMission.GetShelfMissionInfo(Id);
        }
        else {
            AddShelfMission.CbbIsDisable("");
            ///AddShelfMission.CbbOrderReceipt("");
            AddShelfMission.CbbSupplier("");
        }
    },
    Container: $("#dlgAddShelfMission"),
    InitEdit: function (data) {
        //var contarner = this.Container;
        //contarner.find('#hId').val(data.Id);
        //contarner.find('#txtAppointmentDate').val(data.SAppointmentDate);
        //contarner.find('#txtPlanArrivalTime').val(data.SPlanArrivalTime);
        //contarner.find('#txtPurchaseOrderNo').val(data.PurchaseOrderNo);
        //contarner.find('#txtRemark').val(data.Remark);
        //AddShelfMission.CbbIsDisable(data.IsDisable);
        //AddShelfMission.CbbOrderReceipt(data.OrderId);
        //AddShelfMission.CbbSupplier(data.SupplierId);
    },
    CbbIsDisable: function (v) {
        var jData = [{ "Id": "0", "Text": "启用" }, { "Id": "1", "Text": "禁用" }];
        var cbb = $('#cbbIsDisable');
        cbb.combobox({
            valueField: 'Id',
            textField: 'Text',
            data: jData,
            onLoadSuccess: function () {
                if (v != "") {
                    $.each(jData, function (i, item) {
                        if (item.Text == v) {
                            cbb.combobox('select', item.Id);
                        }
                    })
                }
                else {
                    cbb.combobox('select', jData[0].Id);
                }
            }
        });
    },
    CbbOrderReceipt: function (v) {
        var cbb = $('#cbbOrderReceipt');

        $.ajax({
            url: "/wms/Services/WmsService.svc/GetCbbOrderReceipt",
            type: "post",
            data: '',
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $.messager.progress({ title: '请稍等', msg: '正在执行...' });
            },
            complete: function () {
                $.messager.progress('close');
            },
            success: function (result) {
                if (result.ResCode != 1000) {
                    if (result.Msg != "") {
                        $.messager.alert('系统提示', result.Msg, 'info');
                    }
                    return false;
                }
                var jData = eval("(" + result.Data + ")");
                cbb.combobox({
                    valueField: 'Id',
                    textField: 'Text',
                    data: jData,
                    onLoadSuccess: function () {
                        if (v != "") {
                            cbb.combobox('setValue', v);
                        }
                        else {
                            cbb.combobox('select', jData[0].Id);
                        }
                    }
                });
            }
        });
    },
    CbbSupplier: function (v) {
        var cbb = $('#cbbSupplier');

        $.ajax({
            url: "/wms/Services/WmsService.svc/GetCbbSupplier",
            type: "post",
            data: '',
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $.messager.progress({ title: '请稍等', msg: '正在执行...' });
            },
            complete: function () {
                $.messager.progress('close');
            },
            success: function (result) {
                if (result.ResCode != 1000) {
                    if (result.Msg != "") {
                        $.messager.alert('系统提示', result.Msg, 'info');
                    }
                    return false;
                }
                var jData = eval("(" + result.Data + ")");
                cbb.combobox({
                    valueField: 'Id',
                    textField: 'Text',
                    data: jData,
                    onLoadSuccess: function () {
                        if (v != "") {
                            cbb.combobox('setValue', v);
                        }
                        else {
                            cbb.combobox('select', jData[0].Id);
                        }
                    }
                });
            }
        });
    },
    GetShelfMissionInfo: function (Id) {
        var sData = { Id: Id }; 
        //console.log('sData--' + sData);
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetShelfMissionInfo",
            type: "get",
            data: sData,
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }
                //console.log('result.Data--' + result.Data);
                AddShelfMission.SetInfo(JSON.parse(result.Data));
            }
        });
    },
    SetInfo: function (data) {
        $('#lbOrderCode').text(data.OrderCode);
        $('#txtRemark').val(data.Remark);
    },
    Add: function () {
        var Id = $.trim($('[id$=hId]').val());
        window.location = '/wms/a/gainstore.html';
    },
    OnSave: function () {
        var sId = $.trim($("[id$=hId]").val());
        var sOrderCode = $('#lbOrderCode').text().replace('系统自动生成','');
        var sRemark = $.trim($("#txtRemark").val());
        var sSort = 0;
        var sIsDisable = 0;

        var sData = '{"Id":"' + sId + '","OrderCode":"' + sOrderCode + '","Remark":"' + sRemark + '","Sort":"' + sSort + '","IsDisable":"' + sIsDisable + '"}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/SaveShelfMission",
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
                    window.location = "/wms/a/gainstore.html?Id=" + result.Data + "";
                }, 1000)
            }
        });
    }
}