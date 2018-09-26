var AddOrderReceipt = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        var Id = $('[id$=hId]').val();
        $('#txtCustomer').textbox();
        var orderType = parseInt($('[id$=hOrderType]').val());
        if (orderType == 1) {
            $('#lbtnCreate').remove();
        }
        if (Id != '') {
            AddOrderReceipt.GetOrderReceiptInfo(Id);
        }
        else {
            AddOrderReceipt.CreateBlankFm();
            if (orderType != 1) {
                AddOrderReceipt.CbgOrderReceipt('cbgPreOrderCode',true, 1, null);
            }
        }
    },
    InitEdit: function (data) {
        //console.log(JSON.stringify(data));
        var orderType = parseInt(data.OrderType);
        $('[id$=hId]').val(data.Id);
        $("#hCustomerId").val(data.CustomerId);
        $('#txtCustomer').textbox('setValue', data.CustomerCode + data.CustomerName);
        $("#hSupplierId").val(data.SupplierId);
        $('#lbOrderCode').text(data.OrderCode);
        if (orderType != 1) AddOrderReceipt.CbgOrderReceipt('cbgPreOrderCode',!data.IsStopNext, 1, { "Id": "", "Name": data.PreOrderCode });
        $('#txtPurchaseOrderCode').val(data.PurchaseOrderCode);
        $("#ddlOrderReceiptType").find("option[text='" + data.TypeName + "']").attr("selected", true);
        $('#txtSettlementDate').val(data.SSettlementDate);
        $('#txtRecordDate').val(data.SRecordDate);
        $('#txtaRemark').val(data.Remark);
        $('#hIsStopNext').val(data.IsStopNext);
        $("#ddlOrderReceiptStatus").find("option[text='" + data.Status + "']").attr("selected", true);
        $('#txtLastTakeDate').val(data.SLastTakeDate);
        $('#txtExpectTakeDate').val(data.SExpectTakeDate);
        $('#txtSendDate').val(data.SSendDate);
        $('#txtPlanSendDate').val(data.SPlanSendDate);
        $('#txtRMA').val(data.RMA);
        $('#txtExpectVolume').val(data.ExpectVolume);
        $('#txtGW').val(data.GW);
        var jsonCustomAttr = JSON.parse(data.CustomAttr);
        if (jsonCustomAttr.length > 0) {
            var sTr = '';
            $.each(jsonCustomAttr, function (i, item) {
                var keyName = 'udf' + i + '：';
                var value = '<input class="txt" value="' + item.Value + '" />';
                sTr += '<div class="row-fl"><span class="rl">' + keyName + '</span><div class="fl">' + value + '</div></div>';
            })
            sTr += '<span class="clr"></span>';
            $('#customInfoT').html(sTr);
        }
        
        if (data.IsStopNext) {
            $('#lbtnCreate').linkbutton('disable');
            $('#lbtnSave').linkbutton('disable');
            $('#lbtnAddProduct').linkbutton('disable');
            $('#lbtnEditProduct').linkbutton('disable');
            $('#lbtnDelProduct').linkbutton('disable');
        }
    },
    CreateBlankFm: function () {
        var sTr = '';
        for (var i = 0; i < 10; i++) {
            var keyName = 'udf' + i + '：';
            var value = '<input class="txt" />';
            sTr += '<div class="row-fl"><span class="rl">' + keyName + '</span><div class="fl">' + value + '</div></div>';
        }
        sTr += '<span class="clr"></span>';
        $('#customInfoT').html(sTr);
    },
    JData:null,
    GetOrderReceiptInfo: function (Id) {
        var sData = { Id: Id };
        //console.log('sData--' + sData);
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetOrderReceiptInfo",
            type: "get",
            data: sData,
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                //console.log('result--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }
                var jData = JSON.parse(result.Data);
                AddOrderReceipt.JData = jData;
                AddOrderReceipt.InitEdit(jData);
            }
        });
    },
    Add:function(){
        var orderType = $.trim($('[id$=hOrderType]').val());
        window.location = '/wms/a/atinstore.html?orderType=' + orderType + '';
    },
    OnSave: function () {
        var isValid = $('#form1').form('validate');
        if (!isValid) return false;
        var sId = $.trim($("[id$=hId]").val());
        var sCustomerId = $.trim($("#hCustomerId").val());
        var sSupplierId = $.trim($("#hSupplierId").val());
        var sOrderCode = $.trim($("#lbOrderCode").text()).replace("系统自动生成","");
        var sOrderType = $.trim($("[id$=hOrderType]").val());
        var sPreOrderCode = '';
        if (parseInt($('[id$=hOrderType]').val()) != 1) sPreOrderCode = $('#cbgPreOrderCode').combogrid('getText');
        var sPurchaseOrderCode = $.trim($("#txtPurchaseOrderCode").val());
        var sTypeName = $.trim($("#ddlOrderReceiptType>option:selected").text()).replace("请选择", "");
        var sSettlementDate = $.trim($("#txtSettlementDate").val());
        var sRecordDate = $.trim($("#txtRecordDate").val());
        var sIsStopNext = $.trim($("#txtIsStopNext").val());
        var sStatus = $.trim($("#ddlOrderReceiptStatus option:selected").text()).replace("请选择", "");
        var sSort = 0;
        var sRemark = $.trim($("#txtaRemark").val());

        var sLastTakeDate = $.trim($("#txtLastTakeDate").val());
        var sExpectTakeDate = $.trim($("#txtExpectTakeDate").val());
        var sSendDate = $.trim($("#txtSendDate").val());
        var sPlanSendDate = $.trim($("#txtPlanSendDate").val());
        var sRMA = $.trim($("#txtRMA").val());
        var expectVolume = $.trim($("#txtExpectVolume").val());
        if (expectVolume == "") expectVolume = 0;
        var gW = $.trim($("#txtGW").val());
        if (gW == "") gW = 0;
        var customAttr = "";
        var customAttrIndex = 0;
        $("#customInfoT input").each(function () {
            var value = $.trim($(this).val());
            var key = $.trim($(this).parent().prev().find("span").text());
            if (customAttrIndex > 0) customAttr += ',';
            customAttr += '{"Name":"' + key + '","Value":"' + value + '"}';
            customAttrIndex++;
        })
        customAttr = "[" + customAttr + "]";
        customAttr = encodeURIComponent(customAttr);

        var sData = '{"Id":"' + sId + '","CustomerId":"' + sCustomerId + '","SupplierId":"' + sSupplierId + '","OrderCode":"' + sOrderCode + '","OrderType":"' + sOrderType + '","PreOrderCode":"' + sPreOrderCode + '","PurchaseOrderCode":"' + sPurchaseOrderCode + '","TypeName":"' + sTypeName + '","SettlementDate":"' + sSettlementDate + '","RecordDate":"' + sRecordDate + '","IsStopNext":"' + sIsStopNext + '","Status":"' + sStatus + '","Sort":"' + sSort + '","Remark":"' + sRemark + '","LastTakeDate":"' + sLastTakeDate + '","ExpectTakeDate":"' + sExpectTakeDate + '","SendDate":"' + sSendDate + '","PlanSendDate":"' + sPlanSendDate + '","RMA":"' + sRMA + '","ExpectVolume":"' + expectVolume + '","GW":"' + gW + '","CustomAttr":"' + customAttr + '"}';
        //console.log('sData--' + sData);
        //return false;

        $.ajax({
            url: "/wms/Services/WmsService.svc/SaveOrderReceipt",
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
                jeasyuiFun.show("温馨提示", "保存成功！");
                setTimeout(function () {
                    window.location = '/wms/a/atinstore.html?Id=' + result.Data + '&orderType=' + sOrderType + '';
                }, 500);
            }
        });
    },
    OnCreate: function () {
        var Id = $.trim($('[id$=hId]').val());
        if (Id == '') {
            return false;
        }
        var rows = $('#dgOrderProduct').datagrid('getRows');
        if (!rows || rows.length == 0) {
            $.messager.alert('提示', '请添加明细后再执行此操作', 'error');
            return false;
        }
        var isRight = true;
        for (var i = 0; i < rows.length; i++) {
            var row = rows[i];
            if (parseInt(row.ReceiptQty) == 0) {
                isRight = false;
                break;
            }
        }
        if (!isRight) {
            $.messager.alert('提示', "明细中存在未处理的数据行，请正确操作！", 'error');
            return false;
        }

        $.messager.confirm('温馨提醒', '确定要生成上架任务吗？', function (r) {
            if (r) {
                $.ajax({
                    url: "/wms/Services/WmsService.svc/CreateShelfMission",
                    type: "post",
                    data: '{"itemAppend":"' + Id + '"}',
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
                        setTimeout(function () {
                            window.location.reload();
                        }, 700);
                    }
                });
            }
        });
    },
    CbgOrderReceipt: function (cbgId,isLoad, orderType, values) {
        var cbg = $('#' + cbgId + '');
        if (!isLoad) {
            cbg.combogrid({
                readonly: true
            });
            if (values) cbg.combogrid('setValue', values);
        }
        else {
            var sData = '{"model":{"PageIndex":"1","PageSize":"100","OrderType":"' + orderType + '"}}';
            $.ajax({
                url: "/wms/Services/WmsService.svc/GetCbgOrderReceipt",
                type: "POST",
                data: sData,
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    //console.log('result--' + JSON.stringify(result));
                    if (result.ResCode != 1000) {
                        $.messager.alert('系统提示', result.Msg, 'info');
                        return false;
                    }

                    cbg.combogrid({
                        panelWidth: 300,
                        fitColumns: true,
                        columns: [[
                            { field: 'Name', title: '订单号', width: 120 },
                            { field: 'SDate', title: '创建日期', width: 120 }
                        ]],
                        data: JSON.parse(result.Data),
                        onLoadSuccess: function (data) {
                            if (values) cbg.combogrid('setValue', values);
                        },
                        onSelect: function (index, row) {
                            $('#hCustomerId').val(row.CustomerId);
                            $('#txtCustomer').textbox('setValue', row.CustomerCode + row.CustomerName);
                        }
                    });
                }
            });
        }
    }
}