var Common = {
    AppName: '/wms',
    GetWh:function(w,h){
        var winw = $(window).width();
        var winh = $(window).height();
        if (w > 0) {
            if (winw < w) w = winw * 0.9;
        }
        else w = winw;
        if (h > 0) {
            if (winh < h) h = winh * 0.9;
        }
        else h = winh;
        
        return new Array(w,h);
    },
    GetMainWh: function (w, h) {
        var winw = $('#pageMain').width();
        var winh = $('#pageMain').height();
        if (w > 0) {
            if (winw < w) w = winw * 0.9;
        }
        else w = winw;
        if (h > 0) {
            if (winh < h) h = winh * 0.9;
        }
        else h = winh;

        return new Array(w, h);
    },
    FDate: function (value, row, index) {
        value = value.replace('T', ' ');
        if (value.length > 18) value = value.substring(0, 18);
        return new Date(value).Format("yyyy-MM-dd");
    },
    FDateTime: function (value, row, index) {
        if (value == "") return "";
        return new Date(value.replace('T',' ')).Format("yyyy-MM-dd hh:mm:ss");
    },
    FIsYes: function (value, row, index) {
        if (value) return '是';
        return '否';
    },
    FStatus: function (value, row, index) {
        return '<span class="cy">' + value + '</span>';
    },
    FImg: function (value, row, index) {
        if (value && value != '') return '<img src="' + Common.AppName + '' + value + '" alt="图片" width="100" height="50" />';
        else return '<img src="' + Common.AppName + '/Images/nopic.gif" alt="图片" width="100" height="50" />';
    },
    GetQueryString: function (key) {
        var arr = Common.GetQueryStringItems();
        return arr[key];
    },
    GetQueryStringItems: function () {
        var hashMap = {};
        var href = window.location.href;
        var queryStr = href.substr(href.indexOf('?') + 1);
        var arr = queryStr.split("&");
        var len = arr.length;
        //var sJson = "";
        if (len > 0) {
            for (var i = 0; i < len; i++) {
                //if(i>0) sJson+=','
                var item = arr[i];
                var itemArr = item.split("=");
                if (itemArr.length > 1) hashMap[itemArr[0]] = $.trim(itemArr[1]);
                else hashMap[itemArr[0]] = "";
            }
        }
        return hashMap;
    },
    OnProgressStart: function () {
        $('#dlgWaiting').dialog({
            closed:false,
            content: '<div class="datagrid-mask-msg" style="display:block;"></div>'
        });
    },
    OnProgressStop: function () {
        $("#dlgWaiting").dialog('destroy');
    },
    EnumMenuName: {
        UCMenuParentName: '首页'
    },
    AjaxPost: function (url, data, callback) {
        try {
            Common.OnProgressStart();

            $.post(url, data, function (result) {
                Common.OnProgressStop();

                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }

                if (typeof (eval(callback)) == 'function') {
                    callback(result);
                }
            })
        }
        catch (e) {
            Common.OnProgressStop();
        }
    }
}