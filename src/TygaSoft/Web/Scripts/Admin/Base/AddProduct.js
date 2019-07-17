var AddProduct = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        AddProduct.CbbSupplier($('#cbbSupplier').val());
    },
    Container: $("#dlgAddProduct"),
    InitEdit: function (data) {
    
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
    Save: function () {
        var isValid = $('#dlgProductFm').form('validate');
        if (!isValid) return false;
        var Id = $.trim($("#hId").val());
        var sort = $.trim($("#txtSort").val());
        if (sort == "") sort = 0;

        var sSupplierId = $('#cbbSupplier').combobox('getValue');
        var sRemark = $.trim($("#txtRemark").val());
        var sProductCode = $.trim($("#txtProductCode").val());
        var sProductName = $.trim($("#txtProductName").val());
        var sFullName = $.trim($("#txtFullName").val());
        var sSpecs = $.trim($("#txtSpecs").val());
        var sPrice = $.trim($('#txtPrice').val());
        var sMaterialQuality = $.trim($('#txtMaterialQuality').val());
        var sWeight = $.trim($('#txtWeight').val());
        var sMaxStore = $.trim($('#txtMaxStore').val());
        var sMinStore = $.trim($('#txtMinStore').val());
        var sOutPackVolume = $.trim($('#txtOutPackVolume').val());
        var sOutPackWeight = $.trim($('#txtOutPackWeight').val());
        var sInPackVolume = $.trim($('#txtInPackVolume').val());
        var sInPackWeight = $.trim($('#txtInPackWeight').val());
        var sOutPackQty = $.trim($('#txtOutPackQty').val());
        var sInPackQty = $.trim($('#txtInPackQty').val());
        var sShelfLife = $.trim($('#txtShelfLife').val());
        var sIsDisable = $.trim($('#ddlIsDisable').val()) == "1" ? true : false;

        if (sPrice == "") sPrice = 0;
        if (sWeight == "") sWeight = 0;
        if (sMaxStore == "") sMaxStore = 0;
        if (sMinStore == "") sMinStore = 0;
        if (sOutPackVolume == "") sOutPackVolume = 0;
        if (sOutPackWeight == "") sOutPackWeight = 0;
        if (sInPackVolume == "") sInPackVolume = 0;
        if (sInPackWeight == "") sInPackWeight = 0;
        if (sOutPackQty == "") sOutPackQty = 0;
        if (sInPackQty == "") sInPackQty = 0;
        if (sShelfLife == "") sShelfLife = 0;

        var sData = '{"Id":"' + Id + '","CategoryId":"' + ListProduct.GetCategoryId() + '","SupplierId":"' + sSupplierId + '","ProductCode":"' + sProductCode + '","ProductName":"' + sProductName + '","FullName":"' + sFullName + '","Specs":"' + sSpecs + '","Price":"' + sPrice + '","MaterialQuality":"' + sMaterialQuality + '","Weight":"' + sWeight + '","MaxStore":"' + sMaxStore + '","MinStore":"' + sMinStore + '","OutPackVolume":"' + sOutPackVolume + '","OutPackWeight":"' + sOutPackWeight + '","InPackVolume":"' + sInPackVolume + '","InPackWeight":"' + sInPackWeight + '","OutPackQty":"' + sOutPackQty + '","InPackQty":"' + sInPackQty + '","ShelfLife":"' + sShelfLife + '","IsDisable":"' + sIsDisable + '","Sort":"' + sort + '","Remark":"' + sRemark + '"}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/SaveProduct",
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
                AddProduct.Container.dialog('close');
                ListProduct.LoadDg(1, 10);
                jeasyuiFun.show("温馨提示", "保存成功！");
            }
        });
    }
}