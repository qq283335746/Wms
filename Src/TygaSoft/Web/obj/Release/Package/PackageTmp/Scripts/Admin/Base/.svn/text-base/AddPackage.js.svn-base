var AddPackage = {
    Init: function () {
        this.InitForm();
    },
    InitEvent: function () {

    },
    InitData: function () {

    },
    InitForm: function () {
        
    },
    Save: function () {
        var isValid = $('#addPackageFm').form('validate');
        if (!isValid) return false;
        var Id = $.trim($("#hId").val());
        var customerId = $.trim($("#hCustomerId").val());
        var productId = $.trim($("#hProductId").val());
        var totalPiece = $.trim($("#txtTotalPiece").val());
        if (totalPiece == "") totalPiece = 0;
        var totalInsidePackage = $.trim($("#txtTotalInsidePackage").val());
        if (totalInsidePackage == "") totalInsidePackage = 0;
        var totalBox = $.trim($("#txtTotalBox").val());
        if (totalBox == "") totalBox = 0;
        var totalTray = $.trim($("#txtTotalTray").val());
        if (totalTray == "") totalTray = 0;
        var unitXml = "";

        $("table.infoT").each(function (index) {
            var currTable = $(this);
            var key = currTable.attr("code");
            if (key != undefined) {
                unitXml += "<" + key + ">";

                var sAppend = "";
                currTable.find("[id^=txt]").each(function () {
                    var value = $.trim($(this).val());
                    var kcode = $(this).attr("id").replace("txt", "");
                    var name = $.trim($(this).parent().prev().find("span").text());
                    sAppend += "<Data  Code=\"" + kcode + "\" Name=\"" + name + "\"><![CDATA[" + value + "]]></Data>";
                })

                unitXml += sAppend;

                unitXml += "</" + key + ">";
            }
        })
        unitXml = "<Root>" + unitXml + "</Root>";
        unitXml = encodeURIComponent(unitXml);

        var sData = '{"Id":"' + Id + '","CustomerId":"' + customerId + '","ProductId":"' + productId + '","PackageCode":"' + $.trim($("#txtPackageCode").val()) + '","TotalPiece":"' + totalPiece + '","TotalInsidePackage":"' + totalInsidePackage + '","TotalBox":"' + totalBox + '","TotalTray":"' + totalTray + '","Remark":"' + $.trim($("#txtaRemark").val()) + '","UnitXml":"' + unitXml + '"}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/SavePackage",
            type: "post",
            data: '{"model":' + sData + '}',
            contentType: "application/json; charset=utf-8",
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
                jeasyuiFun.show("温馨提示", "操作成功！");
                setTimeout(function () {
                    window.location = "/wms/a/gbase.html";
                },1000)
            }
        });
    }
}