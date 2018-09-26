var DlgBarcodeTemplate = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        this.GetBarcodeTemplateInfo();
        this.LoadDg(1, 100);
    },
    LoadDg:function(pageIndex,pageSize){
        var sData = '{"model":{"PageIndex":' + pageIndex + ',"PageSize":' + pageSize + ',"TypeName":"Barcode"}}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetBarcodeTemplateList",
            type: "post",
            data: sData,
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                Common.OnProgressStart();
            },
            complete: function () {
                Common.OnProgressStop();
            },
            success: function (result) {
                //console.log('GetBarcodeTemplateList--result--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    if (result.Msg != "") {
                        $.messager.alert('系统提示', result.Msg, 'info');
                    }
                    return false;
                }
                var jData = JSON.parse(result.Data);
                $('#dgBarcodeTemplate').datagrid('loadData', JSON.parse(result.Data));
            }
        });
    },
    SelectRow: null,
    OnSelect: function (index, row) {
        //console.log('OnSelect--row--' + JSON.stringify(row));
        DlgBarcodeTemplate.SelectRow = row;
        DlgBarcodeTemplate.SetFm(JSON.parse(row.JContent));
        $('#lbtnSaveAs').linkbutton('enable');
        if (row.IsDefault) $('#lbtnSetDefault').linkbutton('disable');
        else $('#lbtnSetDefault').linkbutton('enable');
    },
    OnDlg: function (callBackFun) {
        var w = $(window).width();
        var h = $(window).height();
        if (w > 1000) w = 1000;
        else w = w * 0.9;
        if (h > 700) h = 700;
        else h = h * 0.9;
        if ($("body").find("#dlgBarcodeTemplate").length == 0) {
            $("body").append("<div id=\"dlgBarcodeTemplate\"></div>");
        }
        var dlg = $("#dlgBarcodeTemplate");
        dlg.dialog({
            title: '条码模板管理',
            width: w,
            height: h,
            closed: false,
            href: '/wms/u/tbarcode.html',
            modal: true,
            iconCls: 'icon-ok',
            buttons: [{
                id: 'btnSave', text: '确定', iconCls: 'icon-save', handler: function () {
                    if (typeof (eval(callBackFun)) == 'function') {
                        callBackFun(DlgBarcodeTemplate.SelectRow);
                    }
                    dlg.dialog('close');
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    dlg.dialog('close');
                }
            }]
        })
    },
    GetBarcodeTemplateInfo: function () {
        var sData = '{"model":{"Id":""}}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetBarcodeTemplateInfo",
            type: "post",
            data: sData,
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                Common.OnProgressStart();
            },
            complete: function () {
                Common.OnProgressStop();
            },
            success: function (result) {
                //console.log('GetBarcodeTemplateInfo--result--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    if (result.Msg != "") {
                        $.messager.alert('系统提示', result.Msg, 'info');
                    }
                    return false;
                }
                var jData = JSON.parse(result.Data);
                DlgBarcodeTemplate.CbbBarcodeFormat(jData.BarcodeFormatList, jData.BarcodeFormat);
            }
        });
    },
    CbbBarcodeFormat: function (jData, v) {
        if (!v || v == '') v = 'CODE_128';
        var cbb = $('#cbbBarcodeFormat');
        cbb.combobox({
            valueField: 'Key',
            textField: 'Value',
            data: jData,
            onLoadSuccess: function () {
                if (v != "") {
                    for (var i = 0; i < jData.length; i++) {
                        if (jData[i].Value == v) cbb.combobox('select', jData[i].Key);
                    }
                }
                else {
                    cbb.combobox('select', jData[0].Key);
                }
            }
        });
    },
    GetFm: function () {
        var isValid = $('#dlgBarcodeTemplateFm').form('validate');
        if (!isValid) return null;
        var barcodeFormat = $('#cbbBarcodeFormat').combobox('getText');
        var width = $.trim($('#txtWidth').val()) == '' ? 238 : parseInt($('#txtWidth').val());
        var height = $.trim($('#txtHeight').val()) == '' ? 50 : parseInt($('#txtHeight').val());
        var barcode = $.trim($('#txtBarcode').val()) == '' ? '123456789' : $.trim($('#txtBarcode').val());
        var margin = $.trim($('#txtMargin').val()) == '' ? 0 : parseInt($('#txtMargin').val());
        return { BarcodeFormat: barcodeFormat, Width: width, Height: height, Barcode: barcode, Margin: margin, IsDefault:false };
    },
    SetFm: function (data) {
        $('#imgBarcodeBrowser').attr('src', data.ImageUrl);
        $('#cbbBarcodeFormat').combobox('setValue', data.BarcodeFormat);
        $('#txtWidth').val(data.Width);
        $('#txtHeight').val(data.Height);
        $('#txtBarcode').val(data.Barcode);
        $('#txtMargin').val(data.Margin);
    },
    OnBrowser: function () {
        var jData = DlgBarcodeTemplate.GetFm();
        console.log('jInfo--' + JSON.stringify(jData));
        var sData = '{"model":' + JSON.stringify(jData) + '}';
        console.log('GetBarcode--sData--' + sData);
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetBarcodeBrowser",
            type: "post",
            data: sData,
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                Common.OnProgressStart();
            },
            complete: function () {
                Common.OnProgressStop();
            },
            success: function (result) {
                //console.log('GetBarcode--result--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    if (result.Msg != "") {
                        $.messager.alert('系统提示', result.Msg, 'info');
                    }
                    return false;
                }
                $('#imgBarcodeBrowser').attr('src', result.Data);
            }
        });
    },
    OnSave: function () {
        //console.log('OnSave--' + DlgBarcodeTemplate.SelectRow);
        if (!DlgBarcodeTemplate.SelectRow) {
            DlgBarcodeTemplate.DlgTitle();
        }
        else {
            var jData = DlgBarcodeTemplate.GetFm();
            if (!jData) return false;
            jData.Id = DlgBarcodeTemplate.SelectRow.Id;
            jData.Title = DlgBarcodeTemplate.SelectRow.Title;
            jData.IsDefault = DlgBarcodeTemplate.SelectRow.IsDefault;
            DlgBarcodeTemplate.Save(null, jData);
        }
    },
    OnSaveAsTemplate: function () {
        DlgBarcodeTemplate.DlgTitle();
    },
    DlgTitle: function () {
        if ($("body").find("#dlgTitle").length == 0) {
            $("body").append('<div id="dlgTitle" style="padding:10px;"></div>');
        }
        var dlg = $("#dlgTitle");
        dlg.dialog({
            title: '保存模板',
            width: 400,
            height: 150,
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            content: '<form id="dlgTitleFm">标题：<input id="txtTitle" class="easyui-textbox" data-options="required:true" style="width:88%"></form>',
            buttons: [{
                id: 'btnSaveByDlgSave', text: '确定', iconCls: 'icon-save', handler: function () {
                    var isValid = $('#dlgTitleFm').form('validate');
                    if (!isValid) return false;

                    var jData = DlgBarcodeTemplate.GetFm();
                    if (!jData) return false;
                    jData.Title = $.trim($('#txtTitle').val());

                    DlgBarcodeTemplate.Save(dlg, jData);
                }
            }, {
                id: 'btnCancelByDlgSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    dlg.dialog('close');
                }
            }]
        })
    },
    Save: function (dlg, jData) {
        //console.log('sData--' + JSON.stringify(jData));
        //return false;
        jData.TypeName = 'Barcode';
        $.ajax({
            url: "/wms/Services/WmsService.svc/SaveBarcodeTemplate",
            type: "post",
            data: '{"model":' + JSON.stringify(jData) + '}',
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
                if(dlg) dlg.dialog('close');
                DlgBarcodeTemplate.LoadDg(1, 100);
                jeasyuiFun.show("温馨提示", "保存成功！");
            }
        });
    },
    OnDel: function () {
        var dg = $("#dgBarcodeTemplate");
        var rows = dg.datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请至少选择一行数据进行操作", 'error');
            return false;
        }
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) itemAppend += ",";
            itemAppend += rows[i].Id;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                $.ajax({
                    url: "/wms/Services/WmsService.svc/DeleteBarcodeTemplate",
                    type: "post",
                    data: '{"itemAppend":"' + itemAppend + '"}',
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
                        jeasyuiFun.show("温馨提示", "操作成功！");
                        DlgBarcodeTemplate.LoadDg(1, 100);
                    }
                });
            }
        });
    },
    OnSetDefault: function () {
        var dg = $("#dgBarcodeTemplate");
        var rows = dg.datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var jData = rows[0];
        jData.IsDefault = true;
        DlgBarcodeTemplate.Save(null, JSON.stringify(jData));
    }
}