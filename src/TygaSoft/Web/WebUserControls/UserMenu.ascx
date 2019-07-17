<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserMenu.ascx.cs" Inherits="TygaSoft.Web.WebUserControls.UserMenu" %>

<div id="menuLeft" class="easyui-accordion" data-options="fit:true">
    <div title="采购" style="overflow:auto;padding:10px;">
                     
    </div>
    <div title="入库，收货，上架" data-options="selected:true" style="padding:10px;">
        <ul class="aMenu">
            <li>
                <a href="/u/takeorder.html">收货单</a>
            </li>
            <li>
                <a href="/u/tginstore.html">收货记录</a>
            </li>
        </ul>
    </div>
    <div title="出库，分配，发货" style="padding:10px;">
        <ul class="aMenu">
            <li>
                <a href="#">出库订单</a>
            </li>
            <li>
                <a href="#">拣货明细</a>
            </li>
            <li>
                <a href="#">拣货任务</a>
            </li>
            <li>
                <a href="#">波次</a>
            </li>
        </ul>   
    </div>
    <div title="费用管理" style="padding:10px;">
        <ul class="aMenu">
            <li><a href="/wms/a/tcost.html">仓储费用</a></li>
            <li><a href="#">增值费用</a></li>
        </ul>           
    </div>
    <div title="管理，查询，调整" style="padding:10px;">
        <ul class="aMenu">
            <li>
                <a href="/wms/a/tstore.html">库存查询</a>
            </li>
            <li>
                <a href="#">任务中心</a>
            </li>
            <li>
                <a href="#">库存调整</a>
            </li>
            <li>
                <a href="#">库存移动</a>
            </li>
            <li>
                <a href="#">货品转移</a>
            </li>
            <li>
                <a href="#">库存交易</a>
            </li>
            <li>
                <a href="#">库存平面图</a>
            </li>
        </ul>       
    </div>
    <div title="业务，规则，策略" style="padding:10px;">
                          
    </div>
    <div title="售后管理" style="padding:10px;">
         <ul class="aMenu">
             <li><a href="#">保养计划</a></li>
             <li><a href="#">保养记录</a></li>
             <li><a href="#">维修记录</a></li>
         </ul>           
    </div>
    <div title="设备管理" style="padding:10px;">
         <ul class="aMenu">
             <li><a href="/wms/a/tdevice.html">设备分布图</a></li>
             <li><a href="#">设备运行报表</a></li>
             <li><a href="#">设备故障和告警</a></li>
             <li><a href="#">零部件库存</a></li>
         </ul>           
    </div>
    <div title="基础数据" style="padding:10px;">
        <ul class="aMenu">
            <li><a href="/wms/a/abase.html">货主</a></li>
            <li><a href="/wms/a/ygbase.html">供应商</a></li>
            <li><a href="/wms/a/gybase.html">库位</a></li>
            <li><a href="#">区域</a></li>
            <li><a href="/wms/a/tabase.html">货品</a></li>
            <li><a href="/wms/a/tybase.html">库区</a></li>
            <li><a href="/wms/a/gbase.html">包装</a></li>
         </ul>                 
    </div>
    <div title="配置" style="padding:10px;">
                    
    </div>
</div>