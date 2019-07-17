<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddOrderReceiptProduct.aspx.cs" Inherits="TygaSoft.Web.Admin.InStore.AddOrderReceiptProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新建/编辑收货单货品</title>
</head>
<body>
    <form id="addOrderReceiptProductFm" runat="server">
    <div>
        <div id="tabOrderReceiptProduct" class="easyui-tabs">
            <div title="基本信息" style="padding:20px;">
                <div class="row">
                    <span class="rl">货品：</span>
                    <div class="fl">
                        <input runat="server" id="txtProduct" class="easyui-textbox" data-options="required:true,icons:[{iconCls:'icon-search',
                                handler: function(e){
                                    DlgProduct.OnDlg();
                                }
                            }]" style="width:200px" />
                    </div>
                </div>
                <div class="row mt10">
                    <span class="rl">包装：</span>
                    <div class="fl">
                        <input runat="server" id="txtPackage" class="easyui-textbox" data-options="icons:[{iconCls:'icon-search',
                                handler: function(e){
                                    DlgPackage.OnDlg();
                                }
                            }]" style="width:200px" />
                    </div>
                </div>
                <div class="row mt10">
                    <span class="rl">单位：</span>
                    <div class="fl">
                        <asp:DropDownList runat="server" ID="ddlUnit"></asp:DropDownList>
                    </div>
                </div>
                <div class="row mt10">
                    <span class="rl">预期量：</span>
                    <div class="fl">
                        <input runat="server" id="txtExpectedQty" class="mtxt" data-options="validType:'int'" />
                    </div>
                </div>
                <div class="row mt10">
                    <span class="rl">已收货量：</span>
                    <div class="fl">
                        <input runat="server" ClientIDMode="Static" id="txtReceiptQty" class="mtxt" data-options="validType:'intNotZero'" />
                    </div>
                </div>
                <div class="row mt10">
                    <span class="rl">采购订单：</span>
                    <div class="fl">
                        <input runat="server" id="txtProductPurchaseOrderCode" class="easyui-validatebox mtxt" data-options="validType:'numberAndEnglish'" />
                    </div>
                </div>
                <div class="row mt10">
                    <span class="rl">状态：</span>
                    <div class="fl">
                        <asp:DropDownList runat="server" ID="ddlOrderReceiptStatus"></asp:DropDownList>
                    </div>
                </div>
                <div class="row mt10">
                    <span class="rl">排序：</span>
                    <div class="fl">
                        <input runat="server" id="txtSort" class="easyui-validatebox mtxt" data-options="validType:'int'" />
                    </div>
                </div>
                <div class="row mt10">
                    <span class="rl">备注：</span>
                    <div class="fl">
                        <textarea runat="server" id="txtaProductRemark" class="mtxt" rows="3" cols="80" style="height:60px;"></textarea>
                    </div>
                </div>
            </div>
            <div title="批属性" style="padding:20px;">
                <div class="row mt10">
                    <span class="rl">包装：</span>
                    <div class="fl">
                        <input runat="server" id="txtPackageName" class="easyui-validatebox mtxt" />
                    </div>
                </div>
                <div class="row mt10">
                    <span class="rl">供应商：</span>
                    <div class="fl">
                        <input runat="server" id="txtSupplierName" class="easyui-validatebox mtxt" />
                    </div>
                </div>
                <div class="row mt10">
                    <span class="rl">生产日期：</span>
                    <div class="fl">
                        <input runat="server" id="txtProduceDate" class="easyui-datebox mtxt" />
                    </div>
                </div>
                <div class="row mt10">
                    <span class="rl">质量状态：</span>
                    <div class="fl">
                        <input runat="server" id="txtQualityStatus" class="easyui-validatebox mtxt" />
                    </div>
                </div>
            </div>
            <div title="质检" style="padding:20px;">
                <div class="row mt10">
                    <span class="rl">检验：</span>
                    <div class="fl">
                        <input runat="server" id="txtCheckQuantity" class="easyui-validatebox mtxt" data-options="validType:'int'" />
                    </div>
                </div>
                <div class="row mt10">
                    <span class="rl">已拒绝：</span>
                    <div class="fl">
                        <input runat="server" id="txtRejectQuantity" class="easyui-validatebox mtxt" data-options="validType:'int'" />
                    </div>
                </div>
                <div class="row mt10">
                    <span class="rl">QC状态：</span>
                    <div class="fl">
                        <input runat="server" id="txtQCStatus" class="easyui-validatebox mtxt" />
                    </div>
                </div>
                <div class="row mt10">
                    <span class="rl">是否需要QC：</span>
                    <div class="fl">
                        <asp:DropDownList runat="server" ID="ddlIsOk"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

        <input type="hidden" runat="server" id="hOrderProductId" />
        <input type="hidden" runat="server" id="hProductId" />
        <input type="hidden" runat="server" id="hPackageId" />

    </div>
    </form>

    <script type="text/javascript" src="/wms/Scripts/Admin/InStore/AddOrderReceiptProduct.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Admin/Base/DlgProduct.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Admin/Base/DlgPackage.js"></script>
    <script type="text/javascript">
        $(function () {
            AddOrderReceiptProduct.Init();
        })
    </script>
</body>
</html>
