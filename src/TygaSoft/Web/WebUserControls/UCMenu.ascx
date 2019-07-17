<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCMenu.ascx.cs" Inherits="TygaSoft.Web.WebUserControls.UCMenu" %>

<div id="accUcMenu" class="easyui-accordion acc" data-options="fit:true,onSelect:UCMenu.OnAccSelect,onAdd:UCMenu.OnAccAdd"></div>

<script type="text/javascript" src="/wms/Scripts/Manages/UCMenu.js"></script>
<script type="text/javascript">
    $(function () {
        UCMenu.Init();
    })
</script>