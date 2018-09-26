<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddStockLocation.aspx.cs" Inherits="TygaSoft.Web.Admin.Base.AddStockLocation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新建库位</title>
</head>
<body>
    <form id="dlgFm" runat="server">
        <div class="easyui-tabs" data-options="border:false">
            <div title="基本信息" style="padding:20px;">
                <ul class="h_ul">
                    <li>
                        <div class="row">
                            <span class="rl">库位代码：</span>
                            <div class="fl">
                                <input runat="server" id="txtCode" class="easyui-validatebox txt100" data-options="required:true" />
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">库位名称：</span>
                            <div class="fl">
                                <input runat="server" id="txtName" class="easyui-validatebox txt100" data-options="required:true" />
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">库区：</span>
                            <div class="fl">
                                <input runat="server" id="txtZone" class="easyui-textbox txt100" data-options="required:true,missingMessage:'请选择库区',invalidMessage:'请选择库区', icons:[{iconCls:'icon-search',
                                    handler: function(e){
                                        DlgZone.OnDlg();
                                    }
                                }]" />
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">排：</span>
                            <div class="fl">
                                <input runat="server" id="txtRow" class="txt100" />
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">层：</span>
                            <div class="fl">
                                <input runat="server" id="txtLayer" class="txt100" />
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">列：</span>
                            <div class="fl">
                                <input runat="server" id="txtCol" class="txt100" />
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">通道：</span>
                            <div class="fl">
                                <input runat="server" id="txtPassway" class="txt100" />
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">库位处理：</span>
                            <div class="fl">
                                <asp:DropDownList runat="server" ID="ddlStockLocationDeal"></asp:DropDownList>
                            </div>
                            <span class="clr"></span>
                        </div>
                    </li>
                    <li class="ml20">
                        <div class="row">
                            <span class="rl">长度：</span>
                            <div class="fl">
                                <input runat="server" id="txtWidth" class="txt100" />
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">宽度：</span>
                            <div class="fl">
                                <input runat="server" id="txtWide" class="txt100" />
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">高度：</span>
                            <div class="fl">
                                <input runat="server" id="txtHigh" class="txt100" />
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">体积：</span>
                            <div class="fl">
                                <input runat="server" id="txtVolume" class="txt100" />
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">容积：</span>
                            <div class="fl">
                                <input runat="server" id="txtCubage" class="txt100" />
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">堆栈限制：</span>
                            <div class="fl">
                                <input runat="server" id="txtStackLimit" class="txt100" />
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">载重量：</span>
                            <div class="fl">
                                <input runat="server" id="txtCarryWeight" class="txt100" />
                            </div>
                            <span class="clr"></span>
                        </div>
                    </li>
                    <li class="ml20">
                        <div class="row">
                            <span class="rl">X坐标：</span>
                            <div class="fl">
                                <input runat="server" id="txtX" class="txt100" />
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">Y坐标：</span>
                            <div class="fl">
                                <input runat="server" id="txtY" class="txt100" />
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">Z坐标：</span>
                            <div class="fl">
                                <input runat="server" id="txtZ" class="txt100" />
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">定方位：</span>
                            <div class="fl">
                                <input runat="server" id="txtOrientation" class="txt100" />
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">库位类型：</span>
                            <div class="fl">
                                <asp:DropDownList runat="server" ID="ddlStockLocationType"></asp:DropDownList>
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">地面托盘数：</span>
                            <div class="fl">
                                <input runat="server" id="txtGroundTrayQty" class="txt100" />
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">标志：</span>
                            <div class="fl">
                                <asp:DropDownList runat="server" ID="ddlUseStatus"></asp:DropDownList>
                            </div>
                            <span class="clr"></span>
                        </div>
                    </li>
                </ul>
            </div>
            <div title="控制" style="padding:20px;">
                <ul class="h_ul">
                    <li>
                        <div class="row">
                            <span class="rl140">路线顺序：</span>
                            <div class="fl">
                                <input runat="server" id="txtRouteSeq" class="txt" />
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl140">混放货品：</span>
                            <div class="fl">
                                <asp:DropDownList runat="server" ID="ddlIsMixPlace"></asp:DropDownList>
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl140">混放批号：</span>
                            <div class="fl">
                                <asp:DropDownList runat="server" ID="ddlIsBatchNum"></asp:DropDownList>
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl140">丢失ID：</span>
                            <div class="fl">
                                <asp:DropDownList runat="server" ID="ddlIsLoseId"></asp:DropDownList>
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl140">插入其它任务的顺序：</span>
                            <div class="fl">
                                <input runat="server" id="txtInsertTaskSeq" class="txt" />
                            </div>
                            <span class="clr"></span>
                        </div>
                    </li>
                    <li class="ml20">
                        <div class="row">
                            <span class="rl">状态：</span>
                            <div class="fl">
                                <input runat="server" id="txtStatus" class="txt" />
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">仓库：</span>
                            <div class="fl">
                                <input runat="server" id="txtWarehouse" class="txt" />
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">层级：</span>
                            <div class="fl">
                                <input runat="server" id="txtLevelNum" class="txt" />
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">拣货区：</span>
                            <div class="fl">
                                <input runat="server" id="txtPickArea" class="txt" />
                            </div>
                            <span class="clr"></span>
                        </div>
                    </li>
                    <li class="ml20">
                        <div class="row mt10">
                            <span class="rl">种类：</span>
                            <div class="fl">
                                <asp:DropDownList runat="server" ID="ddlCtrType"></asp:DropDownList>
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">ABC：</span>
                            <div class="fl">
                                <asp:DropDownList runat="server" ID="ddlABC"></asp:DropDownList>
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">库存组ID：</span>
                            <div class="fl">
                                <input runat="server" id="txtInventoryGroupId" class="txt" />
                            </div>
                            <span class="clr"></span>
                        </div>
                        <div class="row mt10">
                            <span class="rl">拣货方法：</span>
                            <div class="fl">
                                <input runat="server" id="txtPickMethod" class="txt" />
                            </div>
                            <span class="clr"></span>
                        </div>
                    </li>
                </ul>
            </div>
        </div>

        <input type="hidden" id="hId" runat="server" clientidmode="Static" />
        <input type="hidden" id="hZoneId" runat="server" clientidmode="Static" />
        
    </form>

    <script type="text/javascript" src="/wms/Scripts/Admin/Base/AddStockLocation.js"></script>
     <script type="text/javascript" src="/wms/Scripts/Manages/DlgZone.js"></script>
    <script type="text/javascript">
        $(function () {
            AddStockLocation.Init();
        })
    </script>
</body>
</html>
