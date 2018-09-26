var AddOrderReceiptProduct = {
    Init: function () {
        this.InitEvent();
        this.InitForm();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        
    },
    InitForm:function(){
        $('#tabOrderReceiptProduct').tabs();
        var orderType = parseInt($('[id$=hOrderType]').val());
        var txtExpectedQty = $('#txtExpectedQty');
        var txtReceiptQty = $('#txtReceiptQty');
        
        txtExpectedQty.validatebox({
            required: orderType == 1,
            disabled: orderType != 1
        });
        txtReceiptQty.validatebox({
            required: orderType != 1,
            novalidate: orderType == 1
        });
        if (orderType == 1) {
            txtReceiptQty.attr('readonly', 'readonly');
        }
    },
    Container:$("#dlgAddOrderReceiptProduct"),
    Save: function () {
        var isValid = $('#addOrderReceiptProductFm').form('validate');
        if (!isValid) return false;
        var Id = $.trim($("#hOrderProductId").val());
        var orderId = $.trim($("[id$=hId]").val());
        if (orderId == "") {
            $.messager.alert('异常提示', "检测到收货单信息未保存，请检查", 'info');
            return false;
        }
        var productId = $.trim($("#hProductId").val());
        var packageId = $.trim($("#hPackageId").val());
        var sProductPurchaseOrderCode = $.trim($("#txtProductPurchaseOrderCode").val());
        var unit = $("#ddlUnit>option:selected").text();
        var expectedQty = $.trim($("#txtExpectedQty").val());
        if (expectedQty == "") expectedQty = 0;
        var receiptQty = $.trim($("#txtReceiptQty").val());
        if (receiptQty == "") receiptQty = 0;
        var status = $.trim($("#ddlOrderReceiptStatus>option:selected").text());
        var sort = $.trim($("#txtSort").val());
        if (sort == "") sort = 0;
        var checkQuantity = $.trim($("#txtCheckQuantity").val());
        if (checkQuantity == "") checkQuantity = 0;
        var rejectQuantity = $.trim($("#txtRejectQuantity").val());
        if (rejectQuantity == "") rejectQuantity = 0;
        var isQCNeed = $("#ddlIsOk").val();
        if (isQCNeed == "1") isQCNeed = true;
        else isQCNeed = false;

        var sData = '{"OrderId":"' + orderId + '","Id":"' + Id + '","ProductId":"' + productId + '","PackageId":"' + packageId + '","Unit":"' + unit + '","ExpectedQty":"' + expectedQty + '","ReceiptQty":"' + receiptQty + '","PurchaseOrderCode":"' + $.trim($("#txtProductPurchaseOrderNum").val()) + '","Status":"' + status + '","Sort":"' + sort + '","Remark":"' + $.trim($("#txtaProductRemark").val()) + '","PackageName":"' + $.trim($("#txtPackageName").val()) + '","SupplierName":"' + $.trim($("#txtSupplierName").val()) + '","ProduceDate":"' + $.trim($("#txtProduceDate").datebox('getValue')) + '","QualityStatus":"' + $.trim($("#txtQualityStatus").val()) + '","ProductAttrPurchaseOrderCode":"' + $.trim($("#txtProductAttrPurchaseOrderNum").val()) + '","CheckQuantity":"' + checkQuantity + '","RejectQuantity":"' + rejectQuantity + '","QCStatus":"' + $.trim($("#txtQCStatus").val()) + '","IsQCNeed":"' + isQCNeed + '"}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/SaveOrderReceiptProduct",
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
                
                $('#dlgAddOrderReceiptProduct').dialog('close');
                jeasyuiFun.show("温馨提示", "保存成功！");
                ListOrderReceiptProduct.LoadDg(1, 10);
            }
        });
    }
}