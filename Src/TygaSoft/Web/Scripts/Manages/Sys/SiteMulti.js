var SiteMulti = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        this.Load(1, 10);
        var pager = $("#dg").datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                SiteMulti.Load(pageNumber, pageSize);
            }
        });
    },
    SelectRow: null,
    Load: function (pageIndex, pageSize) {
        var keyword = $("#txtKeyword").textbox('getValue');
        var postData = { "ReqName": "GetSiteMultiList", "PageIndex": "" + pageIndex + "", "PageSize": "" + pageSize + "", "Keyword": "" + keyword + "" };
        Common.AjaxPost("/wms/h/users.html", postData, function (result) {
            //console.log('result--' + JSON.stringify(result));
            $("#dg").datagrid('loadData', JSON.parse(result.Data));
        })
    },
    OnSearch: function () {
        SiteMulti.Load(1, 10);
    },
    Add: function () {
        SiteMulti.SelectRow = null;
        if ($("body").find("#dlgSiteMulti").length == 0) {
            $("body").append("<div id=\"dlgSiteMulti\" style=\"padding:10px;\"></div>");
        }
        var s = '';
        var wh = Common.GetWh(600, 400);
        $("#dlgSiteMulti").dialog({
            title: '填写信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            href: '/wms/u/tsitemulti.html',
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    SiteMulti.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgSiteMulti').dialog('close');
                }
            }],
            onLoad: function () {
                SiteMulti.CbbCulture("cbbCulture", "");
            }

        })
        return false;
    },
    Edit: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        SiteMulti.SelectRow = rows[0];
        if ($("body").find("#dlgSiteMulti").length == 0) {
            $("body").append("<div id=\"dlgSiteMulti\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(600, 400);
        $("#dlgSiteMulti").dialog({
            title: '编辑信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            href: '/wms/u/tsitemulti.html',
            iconCls: 'icon-edit',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    SiteMulti.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgSiteMulti').dialog('close');
                }
            }],
            onLoad: function () {
                SiteMulti.CbbCulture("cbbCulture", rows[0].CultureName);
                SiteMulti.SetSiteMultiFm(rows[0]);
            }
        })
        return false;
    },
    Del: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请至少选择一行数据进行操作", 'error');
            return false;
        }
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) itemAppend += ",";
            itemAppend += rows[i].Id;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                var postData = { "ReqName": "DeleteSiteMulti", "ItemAppend": "" + itemAppend + "" };
                Common.AjaxPost("/wms/h/content.html", postData, function (result) {
                    jeasyuiFun.show("温馨提示", "保存成功！");
                    setTimeout(function () {
                        SiteMulti.Load(1, 10);
                    }, 700);
                });
            }
        });
    },
    Save: function () {
        var isValid = $('#dlgFm').form('validate');
        if (!isValid) return false;
        var Id = $.trim($("#hId").val());
        var coded = $.trim($("#txtCoded").val());
        var named = $.trim($("#txtNamed").val());
        var siteTitle = $.trim($("#txtSiteTitle").textbox('getValue'));
        var siteLogo = $("#imgSiteLogo").attr('src').replace(Common.AppName, "");
        if (siteLogo.indexOf('nopic.gif') > -1) siteLogo = '';
        var sCultureName = $('#cbbCulture').combobox('getValue');

        var postData = { "ReqName": "SaveSiteMulti", "Id": "" + Id + "", "Coded": "" + coded + "", "Named": "" + named + "", "SiteTitle": "" + siteTitle + "", "SiteLogo": "" + siteLogo + "", "CultureName": "" + sCultureName + "" };
        Common.AjaxPost("/wms/h/content.html", postData, function (result) {
            $("#dlgSiteMulti").dialog('close');
            jeasyuiFun.show("温馨提示", "保存成功！");
            setTimeout(function () {
                SiteMulti.Load(1, 10);
            }, 700);
        })
    },
    SetSiteMultiFm: function (data) {
        //console.log(JSON.stringify(data));
        var contarner = $("#dlgSiteMulti");
        contarner.find('#hId').val(data.Id);
        contarner.find('#txtCoded').val(data.Coded);
        contarner.find('#txtNamed').val(data.Named);
        contarner.find('#txtSiteTitle').textbox('setValue', data.SiteTitle);
        if ($.trim(data.SiteLogo) != '') contarner.find('#imgSiteLogo').attr("src", Common.AppName + data.SiteLogo);
        contarner.find('#imgSiteLogo').attr("code", data.SiteLogoId);
    },
    OnSiteLogoCallBack: function (data) {
        $('#imgSiteLogo').attr('src', data[0].Src);
        $('#imgSiteLogo').attr('code', data[0].Id);
    },
    Cbg: function (cbgId, isLoad, values) {
        var cbg = $('#' + cbgId + '');
        if (!isLoad) {
            cbg.combogrid({
                readonly: true
            });
            if (values) cbg.combogrid('setValue', values);
        }
        else {
            var postData = { "ReqName": "GetSiteMultiList", "PageIndex": "1", "PageSize": "1000" };
            Common.AjaxPost("/wms/h/users.html", postData, function (result) {
                //console.log('GetSiteMultiList--' + JSON.stringify(result));
                cbg.combogrid({
                    panelWidth: 400,
                    fitColumns: true,
                    columns: [[
                        { field: 'Id',checkbox: true },
                        { field: 'Coded', title: '站点编号', width: 100 },
                        { field: 'Named', title: '站点名称', width: 200 }
                    ]],
                    data: JSON.parse(result.Data),
                    onLoadSuccess: function (data) {
                        if (values) cbg.combogrid('setValue', values);
                    },
                    onSelect: function (index, row) {
                        
                    }
                });
            })
        }
    },
    CbbCulture: function (cbbId, v) {
        var jData = [{ "Coded": "zh-cn", "Named": "中文" }, { "Coded": "en-us", "Named": "英文" }];
        var cbb = $('#' + cbbId + '');
        cbb.combobox({
            valueField: 'Coded',
            textField: 'Named',
            data: jData,
            onLoadSuccess: function () {
                if (v != "") {
                    cbb.combobox('setValue', v);
                }
                else {
                    cbb.combobox('select', jData[0].Coded);
                }
            }
        });
    }
}