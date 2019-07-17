var Wechat = {
    DlgStockProduct: function () {
        var rows = $("#dgStockProduct").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var s = Wechat.GetStockProductFm();
        var wh = Common.GetWh(600, 300);
        if ($("body").find("#dlgStockProductNotice").length == 0) {
            $("body").append("<div id=\"dlgStockProductNotice\" style=\"padding:10px;\"></div>");
        }
        $("#dlgStockProductNotice").dialog({
            title: '发送通知',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSaveStockProductNotice', text: '发送', iconCls: 'icon-save', handler: function () {
                    Wechat.SendStockProductNotice();
                }
            }, {
                id: 'btnCancelStockProductNotice', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgStockProductNotice').dialog('close');
                }
            }],
            content: s,
            onOpen: function () {
                var container = $('#dlgStockProductNoticeFm');
                container.find('#txtCode').val(rows[0].ProductCode);
                container.find('#txtName').val(rows[0].ProductName);
                container.find('#txtCustomerName').val(rows[0].CustomerName);
                container.find('#txtQty').val(rows[0].UnQty);

                $.ajax({
                    url: "/wms/Services/WmsService.svc/GetWechatUserList",
                    type: "get",
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    success: function (result) {
                        console.log(result.Data);
                        if (result.ResCode != 1000) {
                            $.messager.alert('系统提示', result.Msg, 'info');
                            return false;
                        }
                       
                        container.find('#cbbWechatUser').combobox({
                            valueField: 'openid',
                            textField: 'nickname',
                            data: JSON.parse(result.Data)
                        })
                    }
                });
            }
        })
    },
    GetStockProductFm: function () {
        var s = '';
        s += '<form id="dlgStockProductNoticeFm">';
        s += '<div class="row mt10"><span class="rl"><span class="cr">*</span> 接收人：</span><div class="fl"><input id ="cbbWechatUser" data-options="required:true" /></div></div> ';
        s += '<div class="row mt10"><span class="rl"><span class="cr">*</span> 货品编码：</span><div class="fl"><input id ="txtCode" class="easyui-validatebox txt200" data-options="required:true" /></div></div> ';
        s += '<div class="row mt10"><span class="rl"><span class="cr">*</span> 货品名称：</span><div class="fl"><input id ="txtName" class="easyui-validatebox txt200" data-options="required:true" /></div></div> ';
        s += '<div class="row mt10"><span class="rl"><span class="cr">*</span> 客户：</span><div class="fl"><input id ="txtCustomerName" class="easyui-validatebox txt200" data-options="required:true" /></div></div> ';
        s += '<div class="row mt10"><span class="rl"><span class="cr">*</span> 数量：</span><div class="fl"><input id ="txtQty" class="easyui-validatebox txt200" data-options="required:true" /></div></div> ';
        s += '</form>';
        
        return s;
    },
    SendStockProductNotice: function () {
        var container = $('#dlgStockProductNoticeFm');
        var to = $.trim(container.find('#cbbWechatUser').combobox('getValue'));
        var procode = $.trim(container.find('#txtCode').val());
        var proname = $.trim(container.find('#txtName').val());
        var customer = $.trim(container.find('#txtCustomerName').val());
        var quantity = $.trim(container.find('#txtQty').val());
        var sData = '{"model":{"OpenId":"' + to + '","Coded":"' + procode + '","Named":"' + proname + '","CustomerName":"' + customer + '","Qty":"' + quantity + '"}}';
        console.log('sData--' + sData);
        $.ajax({
            url: "/wms/Services/WmsService.svc/SendStockProductNotice",
            type: "post",
            data: sData,
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $.messager.progress({ title: '请稍等', msg: '正在执行...' });
            },
            complete: function () {
                $.messager.progress('close');
            },
            success: function (result) {
                console.log(JSON.stringify(result));
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }
                jeasyuiFun.show("温馨提示", "发送成功！");
                $("#dlgStockProductNotice").dialog('close');
            }
        });
    }
}