var AddProjectReportPrepare = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        if (ListProjectReportPrepare.SelectRow) {
            AddProjectReportPrepare.InitEdit(ListProjectReportPrepare.SelectRow);
        }
        else {
            DlgCustomer.GetCustomerList(1, 50, null, DlgCustomer.CbgCustomer, null);
        }
    },
    Container: $("#dlgAddProjectReportPrepare"),
    InitEdit: function (data) {
        var contarner = this.Container;
        contarner.find('#hId').val(data.Id);
        DlgCustomer.GetCustomerList(1, 50, null, DlgCustomer.CbgCustomer, data.CustomerId);
        contarner.find('#txtProjectName').val(data.ProjectName);
        contarner.find('#txtProjectSource').val(data.ProjectSource);
        contarner.find('#txtCustomerOfficial').val(data.CustomerOfficial);
        contarner.find('#txtContactMan').val(data.ContactMan);
        contarner.find('#txtContactPhone').val(data.ContactPhone);
        contarner.find('#txtSpecsModel').val(data.SpecsModel);
        contarner.find('#txtPreQty').val(data.PreQty);
        contarner.find('#txtPreAmount').val(data.PreAmount);
        contarner.find('#txtProjectAbout').val(data.ProjectAbout);
        contarner.find('#txtStatus').val(data.Status);
        contarner.find('#txtRemark').val(data.Remark);
    },
    OnSave: function () {
        var isValid = $('#dlgProjectFm').form('validate');
        if (!isValid) return false;
        var contarner = this.Container;
        var sId = $.trim(contarner.find("#hId").val());
        var sCustomerId = $.trim($("#cbgCustomer").combogrid('getValues'));
        var sProjectName = $.trim($("#txtProjectName").val());
        var sProjectSource = $.trim($("#txtProjectSource").val());
        var sCustomerOfficial = $.trim($("#txtCustomerOfficial").val());
        var sContactMan = $.trim($("#txtContactMan").val());
        var sContactPhone = $.trim($("#txtContactPhone").val());
        var sSpecsModel = $.trim($("#txtSpecsModel").val());
        var sPreQty = $.trim($("#txtPreQty").val());
        var sPreAmount = $.trim($("#txtPreAmount").val());
        var sProjectAbout = $.trim($("#txtProjectAbout").val());
        var sStatus = $.trim($("#txtStatus").val());
        var sRemark = $.trim($("#txtRemark").val());

        var sData = '{"ReqName":"SaveInfoneProject","Id":"' + sId + '","CustomerId":"' + sCustomerId + '","ProjectName":"' + sProjectName + '","ProjectSource":"' + sProjectSource + '","CustomerOfficial":"' + sCustomerOfficial + '","ContactMan":"' + sContactMan + '","ContactPhone":"' + sContactPhone + '","SpecsModel":"' + sSpecsModel + '","PreQty":"' + sPreQty + '","PreAmount":"' + sPreAmount + '","ProjectAbout":"' + encodeURIComponent(sProjectAbout) + '","Status":"' + sStatus + '","Remark":"' + encodeURIComponent(sRemark) + '"}';

        $.post("/wms/h/content.html", JSON.parse(sData), function (result) {
            if (result.ResCode != 1000) {
                $.messager.alert('系统提示', result.Msg, 'info');
                return false;
            }
            AddProjectReportPrepare.Container.dialog('close');
            jeasyuiFun.show("温馨提示", "保存成功！");
            setTimeout(function () {
                ListProjectReportPrepare.LoadDg(1, 10);
            }, 1000);
        })
    }
}