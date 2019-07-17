var DlgUpload = {
    Init: function () {

    },
    InitEvent: function () {

    },
    InitData: function () {

    },
    DlgId: 'dlgUpload',
    DlgParentId: "",
    IsMutil: false,
    TableName: "",
    CallBack: "",
    OnDlgUpload: function () {
        var dlgId = this.DlgId;
        var dlgParentId = this.DlgParentId;
        var isMutil = this.IsMutil;
        var tableName = this.TableName;
        var callBack = this.CallBack;
        if (tableName == "") {
            $.messager.alert('错误提醒', '参数TableName值不能为空字符串，请检查', 'error');
            return false;
        }

        var h = $(window).height();
        if (h > 400) h = 400;
        else h = h * 0.95;
        var w = $(window).width();
        if (w > 606) w = 606;
        else w = w * 0.95;

        var $_dlg = $("#" + dlgId + "");
        $_dlg.dialog({
            title: '上传文件',
            width: w,
            height: h,
            closed: false,
            modal: true,
            href: '/wms/u/upload.html?dlgId=' + dlgId + '&dlgParentId=' + dlgParentId + '&funName=' + tableName + '&isMutil=' + isMutil + '&callBack=' + callBack + '&submitUrl=/wms/h/upload.html',
            buttons: [{
                id: 'btnUploadPicture', text: '上 传', iconCls: 'icon-ok',
                handler: function () {
                    dlgUpload.OnUpload();
                }
            }, {
                id: 'btnCancelUpload', text: '取 消', iconCls: 'icon-cancel',
                handler: function () {
                    $_dlg.dialog('close');
                }
            }],
            toolbar: [{
                id: 'btnAddTextbox', text: '添 加', iconCls: 'icon-add',
                handler: function () {
                    dlgUpload.OnToolbarAdd();
                }
            }]
        })
    }
}