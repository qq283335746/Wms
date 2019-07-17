<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPackage.aspx.cs" Inherits="TygaSoft.Web.Admin.Base.AddPackage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新建/编辑包装</title>
</head>
<body>
    <form id="addPackageFm" runat="server">
        <div id="tabPackage" class="easyui-tabs" data-options="border:false">
            <div title="单位" style="padding:20px;">
                <div class="row">
                    <div class="fl">
                        <span class="rl">货主：</span>
                        <div class="fl">
                            <input runat="server" id="txtCustomer" class="easyui-textbox" data-options="icons:[{iconCls:'icon-search',
                                handler: function(e){
                                    DlgCustomer.OnDlg();
                                }
                            }]" style="width:154px" />
                        </div>
                        <span class="clr"></span>
                    </div>
                    <div class="fl">
                        <span class="rl">货品：</span>
                        <div class="fl">
                            <input runat="server" id="txtProduct" class="easyui-textbox" data-options="icons:[{iconCls:'icon-search',
                                handler: function(e){
                                    DlgProduct.OnDlg();
                                }
                            }]" />
                        </div>
                        <span class="clr"></span>
                    </div>
                    <span class="clr"></span>
                </div>
                <div class="row mt10">
                    <span class="rl">包装代码：</span>
                    <div class="fl">
                        <input runat="server" id="txtPackageCode" class="easyui-validatebox txt" data-options="required:true" />
                    </div>
                    <span class="clr"></span>
                </div>
                <div class="row mt10">
                    <span class="rl">&nbsp;</span>
                    <div class="fl">
                        <span>单位/包装层级</span>
                        <div>
                            <div class="h_r">
                                <asp:DropDownList runat="server" ID="ddlPiece" Enabled="false" CssClass="ddl_r" />
                            </div>
                            <div class="h_r">
                                <asp:DropDownList runat="server" ID="ddlInsidePackage" Enabled="false" CssClass="ddl_r" />
                            </div>
                            <div class="h_r">
                                <asp:DropDownList runat="server" ID="ddlBox" Enabled="false" CssClass="ddl_r" />
                            </div>
                            <div class="h_r">
                                <asp:DropDownList runat="server" ID="ddlTray" Enabled="false" CssClass="ddl_r" />
                            </div>
                        </div>
                        <span class="clr"></span>
                    </div>
                    <div class="fl">
                        <span class="rl" style="width:60px;">&nbsp;</span>
                        <div class="fl">
                            <span>单位数量</span>
                            <div class="h_r">
                                <input runat="server" id="txtTotalPiece" class="easyui-validatebox txt" data-options="validType:'float'" style="width:88px;" />
                            </div>
                            <div class="h_r">
                                <input runat="server" id="txtTotalInsidePackage" class="easyui-validatebox txt" data-options="validType:'float'" style="width:88px;" />
                            </div>
                            <div class="h_r">
                                <input runat="server" id="txtTotalBox" class="easyui-validatebox txt" data-options="validType:'float'" style="width:88px;" />
                            </div>
                            <div class="h_r">
                                <input runat="server" id="txtTotalTray" class="easyui-validatebox txt" data-options="validType:'float'" style="width:88px;" />
                            </div>
                        </div>
                    </div>
                    <div class="fl">
                        <span class="rl" style="width:60px;">&nbsp;</span>
                        <div class="fl">
                            <span>示例</span>
                            <div class="mt5">
                                主计量单位为件(EA)
                            </div>
                        </div>
                    </div>
                    <span class="clr"></span>
                </div>
                <div class="row mt10">
                    <span class="rl">说明：</span>
                    <div class="fl">
                        <textarea runat="server" id="txtaRemark" rows="3" cols="80" style="width:436px; height:80px;"></textarea>
                    </div>
                    <span class="clr"></span>
                </div>
            </div>
            <div title="主计量单位" style="padding:20px;">
                <table class="infoT" code="MainUnit">
                    <tr>
                        <td><div class="tar"><span>毛重</span>1：</div></td>
                        <td>
                            <input runat="server" id="txtGW" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                        <td><div class="tar"><span>净重</span>1：</div></td>
                        <td>
                            <input runat="server" id="txtNW" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                    </tr>
                    <tr>
                        <td><div class="tar"><span>长度</span>1：</div></td>
                        <td>
                            <input runat="server" id="txtWidth" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                        <td><div class="tar"><span>宽度</span>1：</div></td>
                        <td>
                            <input runat="server" id="txtWide" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                    </tr>
                    <tr>
                        <td><div class="tar"><span>高度</span>1：</div></td>
                        <td>
                            <input runat="server" id="txtHigh" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                        <td><div class="tar"><span>体积</span>1：</div></td>
                        <td>
                            <input runat="server" id="txtVolume" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                    </tr>
                </table>
            </div>
            <div title="内包装" style="padding:20px;">
                <table class="infoT" code="InsidePackage">
                    <tr>
                        <td><div class="tar"><span>毛重</span>2：</div></td>
                        <td>
                            <input runat="server" id="txtInsideGW" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                        <td><div class="tar"><span>净重</span>2：</div></td>
                        <td>
                            <input runat="server" id="txtInsideNW" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                    </tr>
                    <tr>
                        <td><div class="tar"><span>长度</span>2：</div></td>
                        <td>
                            <input runat="server" id="txtInsideWidth" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                        <td><div class="tar"><span>宽度</span>2：</div></td>
                        <td>
                            <input runat="server" id="txtInsideWide" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                    </tr>
                    <tr>
                        <td><div class="tar"><span>高度</span>2：</div></td>
                        <td>
                            <input runat="server" id="txtInsideHigh" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                        <td><div class="tar"><span>体积</span>2：</div></td>
                        <td>
                            <input runat="server" id="txtInsideVolume" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                    </tr>
                </table>
            </div>
            <div title="箱包装" style="padding:20px;">
                <table class="infoT" code="BoxPackage">
                    <tr>
                        <td><div class="tar"><span>毛重</span>3：</div></td>
                        <td>
                            <input runat="server" id="txtBoxGW" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                        <td><div class="tar"><span>净重</span>3：</div></td>
                        <td>
                            <input runat="server" id="txtBoxNW" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                    </tr>
                    <tr>
                        <td><div class="tar"><span>长度</span>3：</div></td>
                        <td>
                            <input runat="server" id="txtBoxWidth" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                        <td><div class="tar"><span>宽度</span>3：</div></td>
                        <td>
                            <input runat="server" id="txtBoxWide" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                    </tr>
                    <tr>
                        <td><div class="tar"><span>高度</span>3：</div></td>
                        <td>
                            <input runat="server" id="txtBoxHigh" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                        <td><div class="tar"><span>体积</span>3：</div></td>
                        <td>
                            <input runat="server" id="txtBoxVolume" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                    </tr>
                </table>
            </div>
            <div title="托盘" style="padding:20px;">
                <table class="infoT" code="Tray">
                    <tr>
                        <td><div class="tar"><span>毛重</span>4：</div></td>
                        <td>
                            <input runat="server" id="txtTrayGW" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                        <td><div class="tar"><span>净重</span>4：</div></td>
                        <td>
                            <input runat="server" id="txtTrayNW" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                    </tr>
                    <tr>
                        <td><div class="tar"><span>每层数</span>：</div></td>
                        <td>
                            <input runat="server" id="txtEachLayerQty" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                        <td><div class="tar"><span>层高数</span>：</div></td>
                        <td>
                            <input runat="server" id="txtLayerHighQty" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                    </tr>
                    <tr>
                        <td><div class="tar"><span>长度</span>4：</div></td>
                        <td>
                            <input runat="server" id="txtTrayWidth" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                        <td><div class="tar"><span>宽度</span>4：</div></td>
                        <td>
                            <input runat="server" id="txtTrayWide" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                    </tr>
                    <tr>
                        <td><div class="tar"><span>高度</span>4：</div></td>
                        <td>
                            <input runat="server" id="txtTrayHigh" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                        <td><div class="tar"><span>体积</span>4：</div></td>
                        <td>
                            <input runat="server" id="txtTrayVolume" class="easyui-validatebox txt" data-options="validType:'float'" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        
        <input type="hidden" runat="server" id="hId" />
        <input type="hidden" runat="server" id="hCustomerId" />
        <input type="hidden" runat="server" id="hProductId" />
    </form>

    <script type="text/javascript" src="/wms/Scripts/Admin/Base/AddPackage.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/DlgCustomer.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Admin/Base/DlgProduct.js"></script>

</body>
</html>
