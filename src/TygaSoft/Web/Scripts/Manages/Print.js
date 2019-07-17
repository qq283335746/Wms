var Print = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent:function(){
    
    },
    InitData: function () {
        if (window.location.href.indexOf('print=1') > -1) {
            $('#printBtns').remove();
        }
    },
    OnPrint: function () {
        var sHref = window.location.href;
        if (sHref.indexOf("&print=") == -1) window.open(window.location.href+"&print=1");
    },
    SetBarcode: function () {
        //DlgBarcodeTemplate.OnDlg(null);
    }
}