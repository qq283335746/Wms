var ListOrderReceiptProduct = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        var dg = $("#dgOrderProduct");
        var pager = dg.datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                ListOrderReceiptProduct.GetList(pageNumber, pageSize);
            }
        });
        ListOrderReceiptProduct.GetList(1, 10);
    },
    GetList: function (pageIndex, pageSize) {
        var keyword = $("#txtKeyword").textbox('getValue');
        var dg = $("#dgOrderProduct");
        var sData = '{"model":{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","Keyword":"' + keyword + '"}}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetOrderReceiptProductList",
            type: "post",
            data: sData,
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                //console.log('result--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }
                dg.datagrid('loadData', JSON.parse(result.Data));
            }
        });
    },
    OnSearch: function () {
        ListOrderReceiptProduct.GetList(1, 10);
    }
}