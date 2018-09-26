var ListTakeOrder = {
    Init: function () {
        this.InitData();
    },
    InitEvent:function(){
    
    },
    InitData: function () {
        this.CreateTabs();
    },
    CreateTabs: function () {
        var orderType = $.trim($('[id$=hOrderType]').val());
        var Id = $.trim($("#hId").val());
        var t = $("#tabTakeOrder");
        if (!t.tabs('exists', '常用')) {
            t.tabs('add', {
                selected:true,
                title: '常用',
                style: { paddingTop: 20 },
                href: '/wms/a/ginstore.html?Id=' + Id + '&orderType=' + orderType + ''
            });
        }
        if (!t.tabs('exists', '其他信息')) {
            t.tabs('add', {
                selected: false,
                title: '其他信息',
                style: { padding: 20 },
                href: '/wms/a/ttinstore.html?Id=' + Id + '&orderType=' + orderType + ''
            });
        }
        if (!t.tabs('exists', '自定义')) {
            t.tabs('add', {
                selected: false,
                title: '自定义',
                style: { padding: 20 },
                href: '/wms/a/tyinstore.html?Id=' + Id + '&orderType=' + orderType + ''
            });
        }
    },
    Add: function () {
        var orderType = $.trim($('[id$=hOrderType]').val());
        window.location = '/wms/a/yinstore.html?orderType=' + orderType + '';
    },
    SaveOrderReceipt: function () {
        var isValid = $('#addOrderReceiptBaseFm').form('validate');
        if (!isValid) return false;
        isValid = $('#addOrderReceiptAttrFm').form('validate');
        if (!isValid) return false;
        var orderType = $.trim($('[id$=hOrderType]').val());
        var Id = $.trim($("#hId").val());
        var sPreOrderCode = $.trim($('[id$=txtPreOrderCode]').val());
        var sPurchaseOrderCode = $.trim($("#txtPurchaseOrderCode").val());
        var status = $.trim($("#ddlOrderReceiptStatus option:selected").text());
        var type = $.trim($("#ddlOrderReceiptType>option:selected").text());
        var settlementDate = $.trim($("#txtSettlementDate").val());
        var expectVolume = $.trim($("#txtExpectVolume").val());
        if (expectVolume == "") expectVolume = 0;
        var gW = $.trim($("#txtGW").val());
        if (gW == "") gW = 0;
        var customerId = $.trim($("#hCustomerId").val());
        var customAttr = "";
        $("#customInfoT tr input").each(function () {
            var value = $.trim($(this).val());
            var key = $.trim($(this).parent().prev().find("span").text());
            customAttr += "<Add Key=\"" + key + "\"><![CDATA[" + value + "]]></Add>";
        })
        customAttr = "<Datas><Data>" + customAttr + "</Data></Datas>";
        customAttr = encodeURIComponent(customAttr);

        var url = "/wms/Services/WmsService.svc/SaveOrderReceipt";
        var sData = '{"OrderType":"' + orderType + '","Id":"' + Id + '","OrderCode":"' + $.trim($("#lbOrderNum").text()) + '","CustomerId":"' + customerId + '","PurchaseOrderCode":"' + sPurchaseOrderCode + '","TypeName":"' + type + '","Status":"' + status + '","RecordDate":"' + $.trim($("#txtRecordDate").val()) + '","SettlementDate":"' + settlementDate + '","Remark":"' + $.trim($("#txtaRemark").val()) + '","LastTakeDate":"' + $.trim($("#txtLastTakeDate").val()) + '","ExpectTakeDate":"' + $.trim($("#txtExpectTakeDate").val()) + '","SendDate":"' + $.trim($("#txtSendDate").val()) + '","PlanSendDate":"' + $.trim($("#txtPlanSendDate").val()) + '","RMA":"' + $.trim($("#txtRMA").val()) + '","ExpectVolume":"' + expectVolume + '","GW":"' + gW + '","CustomAttr":"' + customAttr + '"}';

        $.ajax({
            url: url,
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

                $("#hId").val(result.Data);
                jeasyuiFun.show("温馨提示", "保存成功！");
                setTimeout(function () {
                    window.location = '/wms/a/yinstore.html?Id=' + result.Data + '';
                }, 1000);
            }
        });
    }
}