<%@ Page Title="角色用户分配关系" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="AddRoleUser.aspx.cs" Inherits="TygaSoft.Web.Manages.Members.AddRoleUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div id="toolbar" style="padding:5px;">
    <div class="fl mr20">
        当前角色：<span runat="server" id="lbRole" clientidmode="Static"></span>
    </div>
    <div class="fr">
        <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'用户名',buttonText:'查询',onClickButton:currFun.Search" />
    </div>
    <span class="clr"></span>
</div>

<table id="bindT" class="easyui-datagrid" title="用户" data-options="singleSelect:true,rownumbers:true,pagination:true,fitColumns:true,fit:true,toolbar:'#toolbar'">
    <thead>
        <tr>
            <th data-options="field:'f1',width:200">用户名</th>
            <th data-options="field:'f2',width:200">用户属于角色</th>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater ID="rpData" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%#Eval("UserName")%></td>
                    <td><input type="checkbox" <%#Eval("IsInRole").ToString() == "True" ? "checked='checked'" : ""%> onclick="currFun.CbIsInRole(this)" value='<%#Eval("UserName")%>' /></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>

<asp:Literal runat="server" ID="ltrMyData"></asp:Literal>

<script type="text/javascript">
    var sPageIndex = 0;
    var sPageSize = 0;
    var sTotalRecord = 0;
    var sQueryStr = "";

    $(function () {
        var myData = currFun.GetMyData("myDataForPage");
        sPageIndex = parseInt(myData.PageIndex);
        sPageSize = parseInt(myData.PageSize);
        sTotalRecord = parseInt(myData.TotalRecord);
        sQueryStr = myData.QueryStr.replace(/&amp;/g, '&');

        currFun.Init();
    })

    var currFun = {
        Init: function () {
            currFun.Grid(sPageIndex, sPageSize);
            currFun.InitData();
        },
        InitData:function(){
            var myModelData = currFun.GetMyData("myDataForModel");
            $("#txtKeyword").textbox('setValue', myModelData.Keyword);
        },
        GetMyData: function (id) {
            var myData = $("#"+id+"").html();
            return eval("(" + myData + ")");
        },
        Grid: function (pageIndex, pageSize) {
            var pager = $('#bindT').datagrid('getPager');
            $(pager).pagination({
                total: sTotalRecord,
                pageNumber: sPageIndex,
                pageSize: sPageSize,
                onSelectPage: function (pageNumber, pageSize) {
                    if (sQueryStr.length == 0) {
                        window.location = "?pageIndex=" + pageNumber + "&pageSize=" + pageSize + "";
                    }
                    else {
                        window.location = "?" + sQueryStr + "&pageIndex=" + pageNumber + "&pageSize=" + pageSize + "";
                    }
                }
            });
        },
        Search: function () {
            var roleName = $.trim($("#lbRole").text());
            var userName = $.trim($("#txtKeyword").textbox('getValue'));
            window.location = "?rName=" + roleName + "&uName=" + userName + "";
        },
        CbIsInRole: function (obj) {
            var $_obj = $(obj);
            var roleName = $.trim($("#lbRole").text());
            var userName = $_obj.val();
            var isInRole = $_obj.is(":checked");

            $.ajax({
                url: Common.AppName + "/Services/SecurityService.svc/SaveUserInRole",
                type: "post",
                contentType: "application/json; charset=utf-8",
                data: '{"userName":"' + userName + '","roleName":"' + roleName + '","isInRole":"' + isInRole + '"}',
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

                    if (!isInRole) {
                        window.location.reload();
                    }
                }
            });
        }
    } 
</script>

</asp:Content>
