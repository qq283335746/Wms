<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddOrderSend.aspx.cs" Inherits="TygaSoft.Web.Admin.OutStore.AddOrderSend" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新建发货单</title>
</head>
<body>
    <form id="dlgFm" runat="server">
        <div class="row - fl">
            <span class="rl"><span class="cr">*</span> 订单号：</span>
            <div class="fl"><input id="txtOrderCode" class="txt200" readonly="readonly" value="系统自动生成" /></div>
        </div>
        <div class="row - fl">
            <span class="rl"><span class="cr">*</span> 客户：</span>
            <div class="fl">
                <input id="txtCustomer" class="easyui-textbox" data-options="required:true,missingMessage:'请选择货主',invalidMessage:'请选择货主', icons:[{iconCls:'icon-search',
                        handler: function(e){
                            DlgCustomer.OnDlg();
                        }
                    }]" style="width:220px" />
            </div>
        </div>
        <span class="clr"></span>
        <input type="hidden" id="hId" />
        <input type="hidden" id="hCustomerId" runat="server" />
        
    </form>

    <script type="text/javascript" src="/wms/Scripts/Manages/DlgCustomer.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/AddOrderSend.js"></script>
    <script type="text/javascript">
        $(function () {
            AddOrderSend.Init();
        })
    </script>
</body>
</html>
