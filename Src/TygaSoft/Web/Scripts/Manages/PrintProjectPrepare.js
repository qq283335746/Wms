var PrintProjectPrepare = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        $('#loProject').layout({
            width: $(window).width(),
            height:$(window).height()
        });
        var sKey = $('[id$=hKey]').val();
        var sValue = $('[id$=hValue]').val();
        if (sKey.indexOf('Customer') > -1) {
            $('#projectBox').remove();
            $('#dgCustomer').remove();
            PrintProjectPrepare.GetCustomerInfo(sValue);
            $('#dgProject').datagrid();
            $('#dgProject').datagrid('hideColumn', 'CustomerCode');
            $('#dgProject').datagrid('hideColumn', 'CustomerName');
            setTimeout(function () {
                PrintProjectPrepare.GetProjectsByCustomerId(sValue);
            })
        }
        else if (sKey.indexOf('Project') > -1) {
            $('#customerBox').remove();
            $('#dgProject').remove();
            PrintProjectPrepare.GetProjectInfo(sValue);
            $('#dgCustomer').datagrid();
            setTimeout(function () {
                PrintProjectPrepare.GetCustomersByProjectId(sValue);
            })
        }
        if (sKey.indexOf('Print=1') > -1) {
            $('#dgToolbar').remove();
        }
    },
    GetCustomerInfo: function (Id) {
        var postData = { "ReqName": "GetInfoneCustomerInfo", "Id": "" + Id + "" };
        Common.AjaxPost("/wms/h/users.html", postData, function (result) {
            //console.log('result--' + result.Data);
            PrintProjectPrepare.SetCustomerInfo(JSON.parse(result.Data));
        })
    },
    SetCustomerInfo:function(data){
        $('#lbCode').text(data.Coded);
        $('#lbName').text(data.Named);
        $('#lbShortName').text(data.ShortName);
        $('#lbInCompany').text(data.InCompany);
        $('#lbContactMan').text(data.ContactMan);
        $('#lbContactPhone').text(data.ContactPhone);
        $('#lbTelPhone').text(data.TelPhone);
        $('#lbFax').text(data.Fax);
        $('#lbPostCode').text(data.PostCode);
        $('#lbAddress').text(data.Address);
        $('#lbCompanyAbout').text(data.CompanyAbout);
    },
    GetCustomersByProjectId: function (projectId) {
        var postData = { "ReqName": "GetInfoneCustomerList", "TypeName": "GetCustomersByProjectId", "ParentId": "" + projectId + "" };
        Common.AjaxPost("/wms/h/users.html", postData, function (result) {
            //console.log('result--' + result.Data);
            $("#dgCustomer").datagrid('loadData', JSON.parse(result.Data));
        })
    },
    GetProjectInfo: function (Id) {
        var postData = { "ReqName": "GetInfoneProjectReportPrepareInfo", "Id": "" + Id + "" };
        Common.AjaxPost("/wms/h/users.html", postData, function (result) {
            //console.log('result--' + result.Data);
            PrintProjectPrepare.SetProjectInfo(JSON.parse(result.Data));
        })
    },
    SetProjectInfo: function (data) {
        $('#lbProjectName').text(data.ProjectName);
        $('#lbProjectSource').text(data.ProjectSource);
        $('#lbCustomer').text(data.CustomerName);
        $('#lbCustomerOfficial').text(data.CustomerOfficial);
        $('#lbProjectContactMan').text(data.ContactMan);
        $('#lbProjectContactPhone').text(data.ContactPhone);
        $('#lbSpecsModel').text(data.SpecsModel);
        $('#lbPreQty').text(data.PreQty);
        $('#lbPreAmount').text(data.PreAmount);
        $('#lbRemark').text(data.Remark);
        $('#lbProjectAbout').text(data.ProjectAbout);
    },
    GetProjectsByCustomerId: function (customerId) {
        var jData = { "ReqName": "GetInfoneProjectReportPrepareList", "TypeName": "GetProjectsByCustomerId", "ParentId": "" + customerId + "" };
        Common.AjaxPost("/wms/h/users.html", jData, function (result) {
            //console.log('result--' + result.Data);
            $("#dgProject").datagrid('loadData', JSON.parse(result.Data));
        })
    },
    OnPrint: function () {
        var $t = $('#loProject');
        var width = parseInt($t.css("width"));
        var height = parseInt($t.css("height"));
        //if (width > 1024) width = 1024;
        height = 0;
        var sizeFWidth = width;
        var sizeFHeight = height;
        var marginTop = -1;
        var marginRight = -1;
        var marginBottom = -1;
        var marginLeft = -1;
        $.messager.confirm('温馨提醒', '确定要打印吗？', function (r) {
            if (r) {
                window.open(window.location.href + "&Print=1&w=" + width + "&h=" + height + "&sfw=" + sizeFWidth + "&sfh=" + sizeFHeight + "&mt=" + marginTop + "&mr=" + marginRight + "&mb=" + marginBottom + "&ml=" + marginLeft + "");
            }
        });
    }
}