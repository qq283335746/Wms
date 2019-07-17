var ListRFID = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        this.LoadDg(1, 100);
    },
	LoadDg: function (pageIndex, pageSize) {
	    var keyword = $("#txtKeyword").textbox('getValue');
	    var postData = { "ReqName": "GetRFIDList", "PageIndex": "" + pageIndex + "", "PageSize": "" + pageSize + "", "Keyword": "" + keyword + "" };
	    Common.AjaxPost("/wms/h/users.html", postData, function (result) {
	        //console.log('result--' + result.Data);
	        var jData = JSON.parse(result.Data);
	        $("#dg").datagrid('loadData', jData);
	    })
	},
	OnSearch: function () {
	    ListRFID.LoadDg(1, 100);
	},
    OnDel: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) itemAppend += ",";
            itemAppend += rows[i].TID + '|' + rows[i].EPC;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                var postData = { "ReqName": "DeleteRFID", "ItemAppend": "" + itemAppend + "" };
                Common.AjaxPost("/wms/h/content.html", postData, function (result) {
                    jeasyuiFun.show("温馨提示", "操作成功！");
                    setTimeout(function () {
                        ListRFID.LoadDg(1, 100);
                    }, 700);
                })
            }
        });
    },
    Itv:null,
    OnScan: function () {
        var currText = $('#lbtnScan').find('.l-btn-text').text();
        if ($.trim(currText) == '开始扫描') $('#lbtnScan').find('.l-btn-text').text('正在扫描...');
        else {
            $('#lbtnScan').find('.l-btn-text').text('开始扫描');
            clearInterval(ListRFID.Itv);
            return false;
        }
        var timeOut = 0;
        ListRFID.Itv = setInterval(function () {
            ListRFID.LoadDg(1, 100);
            timeOut++;
            console.log('timeOut--' + timeOut);
            if (timeOut > 3600) {
                clearInterval(ListRFID.Itv);
                $('#lbtnScan').find('.l-btn-text').text('开始扫描');
            }
        }, 1000);
    }
}