var AddCustomer = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        if (ListCustomer.SelectRow) {
            AddCustomer.InitEdit(ListCustomer.SelectRow);
        }
    },
    Container: $("#dlgAddCustomer"),
    InitEdit: function (data) {
        //console.log('data--' + JSON.stringify(data));
        var contarner = this.Container;
        contarner.find('#hCustomerId').val(data.Id);
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
        contarner.find('#txtRecordDate').val(data.RecordDate);

    },
    OnSave: function () {
        var isValid = $('#dlgCustomerFm').form('validate');
        if (!isValid) return false;
        var sId = $.trim($("#hCustomerId").val());
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

        var postData = { "ReqName": "SaveInfoneCustomer", "Id": "" + sId + "", "Coded": "" + sCoded + "", "Named": "" + sNamed + "", "ShortName": "" + sShortName + "", "InCompany": "" + sInCompany + "", "ContactMan": "" + sContactMan + "", "ContactPhone": "" + sContactPhone + "", "TelPhone": "" + sTelPhone + "", "Fax": "" + sFax + "", "PostCode": "" + sPostCode + "", "Address": "" + sAddress + "", "CompanyAbout": "" + encodeURIComponent(sCompanyAbout) + "" };
        Common.AjaxPost("/wms/h/content.html", postData, function (result) {
            AddCustomer.Container.dialog('close');
            jeasyuiFun.show("温馨提示", "保存成功！");
            setTimeout(function () {
                ListCustomer.LoadDg(1, 10);
            }, 1000);
        })
    }
}