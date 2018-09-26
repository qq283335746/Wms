<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCAddPreOrderReceipt.ascx.cs" Inherits="TygaSoft.Web.WebUserControls.UCAddPreOrderReceipt" %>
<table class="infoT">
    <tr>
        <td><div class="tar">收货单号：</div></td>
        <td>
            <span id="lbOrderCode">系统自动生成</span>
        </td>
        <td><div class="tar">货主：</div></td>
        <td>
            <input id="txtCustomer" class="easyui-textbox" data-options="required:true,missingMessage:'请选择货主',invalidMessage:'请选择货主', icons:[{iconCls:'icon-search',
                handler: function(e){
                    DlgCustomer.OnDlg();
                }
            }]" style="width:200px" />
        </td>
        <td><div class="tar">订单号：</div></td>
        <td>
            <input id="txtPurchaseOrderCode" class="easyui-validatebox txt" data-options="validType:'numberAndEnglish'" style="width:120px" />
        </td>
    </tr>
    <tr>
        <td><div class="tar">状态：</div></td>
        <td>
            <asp:DropDownList runat="server" ID="ddlOrderReceiptStatus" ClientIDMode="Static" Enabled="false"></asp:DropDownList>
        </td>
        <td><div class="tar">记录日期：</div></td>
        <td><input id="txtRecordDate" class="txt" readonly="readonly" style="width:192px;" /></td>
        <td><div class="tar">类型：</div></td>
        <td>
            <asp:DropDownList runat="server" ID="ddlOrderReceiptType" ClientIDMode="Static"></asp:DropDownList>
        </td>
        <td><div class="tar">结算日期：</div></td>
        <td><input id="txtSettlementDate" class="txt" readonly="readonly" style="width:120px" /></td>
    </tr>
    <tr style="margin-top:10px;">
        <td><div class="tar">备注：</div></td>
        <td colspan="7">
            <textarea id="txtaRemark" rows="3" cols="60" style="width:453px;height:50px;"></textarea>
        </td>
    </tr>
</table>

<input type="hidden" id="hCustomerId" />
<input type="hidden" id="hSupplierId" />