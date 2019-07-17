var AddStockWarning = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        if (ListStockWarning.SelectRow != null) {
            AddStockWarning.InitEdit(ListStockWarning.SelectRow);
        }
        else {
            AddStockWarning.CbbIsDisable("");
            AddStockWarning.CbbZoneProperty("");
            AddStockWarning.CbbZone("");
            AddStockWarning.CbbStockLocation("",null,1,10000);
            AddStockWarning.CbbStockLocationProperty("");
        }
    },
    Container: $("#dlgAddStockWarning"),
    InitEdit: function (data) {
        //console.log('data--' + JSON.stringify(data));
        var contarner = this.Container;
        contarner.find('#hId').val(data.Id);
        contarner.find('#txtCoded').val(data.Coded);
        contarner.find('#txtStockAmount').val(data.StockAmount);
        contarner.find('#txtOverdueDay').val(data.OverdueDay);
        contarner.find('#txtMinQty').val(data.MinQty);
        contarner.find('#txtMaxQty').val(data.MaxQty);
        contarner.find('#txtRemark').val(data.Remark);
        contarner.find('#txtSort').val(data.Sort);
        AddStockWarning.CbbIsDisable(data.IsDisableName);
        AddStockWarning.CbbZoneProperty(data.ZoneProperty);
        AddStockWarning.CbbZone(data.ZoneId);
        AddStockWarning.CbbStockLocation(data.StockLocationId, data.ZoneId,1,10000);
        AddStockWarning.CbbStockLocationProperty(data.StockLocationProperty);
    },
    CbbIsDisable: function (v) {
        v = v == '是' ? '禁用' : '启用';
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
    CbbZoneProperty: function (v) {
        var jData = [{ "Id": "0", "Text": "启用" }, { "Id": "1", "Text": "不可用" }];
        var cbb = $('#cbbZoneProperty');
        cbb.combobox({
            valueField: 'Id',
            textField:'Text',
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
    CbbStockLocationProperty: function (v) {
        var jData = [{ "Id": "-1", "Text": "请选择" }, { "Id": "0", "Text": "流利架" }, { "Id": "1", "Text": "可乐库位" }, { "Id": "2", "Text": "可用" }];
        var cbb = $('#cbbStockLocationProperty');
        cbb.combobox({
            valueField: 'Id',
            textField: 'Text',
            data: jData,
            onLoadSuccess: function () {
                if (v != "") {
                    $.each(jData, function (i, item) {
                        if (item.Text == v) {
                            cbb.combobox('select', item.Id);
                            return false;
                        }
                    })
                }
                else {
                    cbb.combobox('select', jData[0].Id);
                }
            }
        });
    },
    CbbZone: function (v) {
        var cbb = $('#cbbZone');

        $.ajax({
            url: "/wms/Services/WmsService.svc/GetCbbZone",
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
    OnZoneChange: function (newValue, oldValue) {
        if (oldValue && oldValue != '') {
            AddStockWarning.CbbStockLocation("", newValue, 1, 10000);
        }
    },
    CbbStockLocation: function (v, zoneId, pageIndex, pageSize) {
        var cbb = $('#cbbStockLocation');
        var jData = [{ "Id": "-1", "Named": "请选择", "selected": true }];
        if (v && v != '') {
            jData[0].selected = false;
        }
        if (!zoneId || zoneId == -1) {
            cbb.combobox({
                valueField: 'Id',
                textField: 'Named',
                data: jData
            });
            return false;
        }
        else {
            var postData = { "ReqName": "GetStockLocationList", "PageIndex": "" + pageIndex + "", "PageSize": "" + pageSize + "", "Keyword": "", "ZoneId": "" + zoneId + "" };
            Common.AjaxPost("/wms/h/users.html", postData, function (result) {
                //console.log('result--' + result.Data);
                var rData = JSON.parse(result.Data);
                for (var i = 0; i < rData.rows.length; i++) {
                    var item = rData.rows[i];
                    if ((v && v != '') && v == item.Id) item.selected = true;
                    jData.push(item);
                }
                cbb.combobox({
                    valueField: 'Id',
                    textField: 'Named',
                    data: jData
                });
            });
        }
    },
    OnSave: function () {
        var isValid = $('#dlgFm').form('validate');
        if (!isValid) return false;
        var Id = $('#hId').val();
        var sCoded = $.trim($('#txtCoded').val());
        var sZoneProperty = $('#cbbZoneProperty').combobox('getText');
        var sZoneId = $('#cbbZone').combobox('getValue');
        var sStockLocationId = $('#cbbStockLocation').combobox('getValue');
        var sStockLocationProperty = $('#cbbStockLocationProperty').combobox('getText');
        var sStockAmount = $.trim($('#txtStockAmount').val());
        var sOverdueDay = $.trim($('#txtOverdueDay').val());
        var sMinQty = $.trim($('#txtMinQty').val());
        var sMaxQty = $.trim($('#txtMaxQty').val());
        var sRemark = $.trim($('#txtRemark').val());
        var sSort = $.trim($('#txtSort').val());
        if (sSort == '') sSort = 0;
        var sIsDisable = $('#cbbIsDisable').combobox('getValue') == "1" ? true : false;

        var sData = '{"Id":"' + Id + '","Coded":"' + sCoded + '","ZoneProperty":"' + sZoneProperty + '","ZoneId":"' + sZoneId + '","StockLocationId":"' + sStockLocationId + '","StockLocationProperty":"' + sStockLocationProperty + '","StockAmount":"' + sStockAmount + '","OverdueDay":"' + sOverdueDay + '","MinQty":"' + sMinQty + '","MaxQty":"' + sMaxQty + '","Remark":"' + sRemark + '","Sort":"' + sSort + '","IsDisable":"' + sIsDisable + '"}';
        //console.log('sData--' + sData);

        $.ajax({
            url: "/wms/Services/WmsService.svc/SaveStockWarning",
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
                //console.log('result--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }
                AddStockWarning.Container.dialog('close');
                ListStockWarning.LoadDg(1, 10);
                jeasyuiFun.show("温馨提示", "保存成功！");
            }
        });
    }
}