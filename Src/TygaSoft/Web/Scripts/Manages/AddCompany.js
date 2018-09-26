var AddCompany = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        if (ListCompany.SelectRow) {
            AddCompany.InitEdit(ListCompany.SelectRow);
        }
    },
    Container: $("#dlgAddCompany"),
    InitEdit: function (data) {
        //console.log('data--' + JSON.stringify(data));
        var contarner = this.Container;
        contarner.find('#hCompanyId').val(data.Id);
        contarner.find('#txtCoded').val(data.Coded);
        contarner.find('#txtNamed').val(data.Named);
        contarner.find('#txtShortName').val(data.ShortName);
        contarner.find('#txtInCompany').val(data.InCompany);
        contarner.find('#txtContactMan').val(data.ContactMan);
        contarner.find('#txtContactPhone').val(data.ContactPhone);
        contarner.find('#txtTelPhone').val(data.TelPhone);
        contarner.find('#txtFax').val(data.Fax);
        contarner.find('#txtPostCode').val(data.PostCode);
        contarner.find('#txtAddress').val(data.Address);
        contarner.find('#txtCompanyAbout').val(data.CompanyAbout);
    },
    OnSave: function () {
        var isValid = $('#dlgCompanyFm').form('validate');
        if (!isValid) return false;
        var sId = $.trim($("#hCompanyId").val());
        var sCoded = $.trim($("#txtCoded").val());
        var sNamed = $.trim($("#txtNamed").val());
        var sShortName = $.trim($("#txtShortName").val());
        var sInCompany = $.trim($("#txtInCompany").val());
        var sContactMan = $.trim($("#txtContactMan").val());
        var sContactPhone = $.trim($("#txtContactPhone").val());
        var sTelPhone = $.trim($("#txtTelPhone").val());
        var sFax = $.trim($("#txtFax").val());
        var sPostCode = $.trim($("#txtPostCode").val());
        var sAddress = $.trim($("#txtAddress").val());
        var sCompanyAbout = $.trim($("#txtCompanyAbout").val());

        var postData = { "ReqName": "SaveCompany", "Id": "" + sId + "", "Coded": "" + sCoded + "", "Named": "" + sNamed + "", "ShortName": "" + sShortName + "", "InCompany": "" + sInCompany + "", "ContactMan": "" + sContactMan + "", "ContactPhone": "" + sContactPhone + "", "TelPhone": "" + sTelPhone + "", "Fax": "" + sFax + "", "PostCode": "" + sPostCode + "", "Address": "" + sAddress + "", "CompanyAbout": "" + sCompanyAbout + "" };
        Common.AjaxPost("/wms/h/content.html", postData, function (result) {
            AddCompany.Container.dialog('close');
            jeasyuiFun.show("温馨提示", "保存成功！");
            setTimeout(function () {
                ListCompany.LoadDg(1, 10);
            }, 1000);
        })
    }
}