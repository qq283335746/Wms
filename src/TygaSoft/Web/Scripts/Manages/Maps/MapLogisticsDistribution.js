var MapLogisticsDistribution = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        MapLogisticsDistribution.InitOrderMap("mapContainer");
        //MapLogisticsDistribution.InitMap("mapContainer");
        //MapLogisticsDistribution.GetMapStaticImage();
    },
    InitOrderMap: function (container) {
        var map = new BMap.Map(container);
        var postData = { "ReqName": "GetOrderMapList" };
        Common.AjaxPost("/wms/h/map.html", postData, function (result) {
            var jData = result.Data;
            for (var i = 0; i < jData.length; i++) {
                var item = jData[i];
                if (item.Lnglat != '') {
                    //console.log('item.Lnglat--' + i + '--' + item.Lnglat);
                    var lnglatArr = item.Lnglat.split(',');
                    if(i == 0) map.centerAndZoom(new BMap.Point(lnglatArr[0], lnglatArr[1]), 11);
                    var mk = new BMap.Marker(new BMap.Point(lnglatArr[0], lnglatArr[1]));
                    map.addOverlay(mk);
                }
            }
        })
    },
    InitMap: function (container) {
        var map = new BMap.Map(container);
        var postData = { "ReqName": "GetLocation" };
        Common.AjaxPost("/wms/h/map.html", postData, function (result) {
            console.log('result--' + JSON.stringify(result));
            var jData = JSON.parse(result.Data);
            map.centerAndZoom(new BMap.Point(jData.content.point.x, jData.content.point.y), 11);
            var mk = new BMap.Marker(new BMap.Point(jData.content.point.x, jData.content.point.y));
            map.addOverlay(mk);
        })
    },
    GetMapStaticImage: function () {
        var wh = Common.GetMainWh(-1, -1);
        var h = parseInt(wh[1] * 0.83);
        var postData = { "ReqName": "GetMapStaticImage", "Width": parseInt(wh[0]), "Height": h, "Center": "", "Markers": "广州市|万宁市|深圳市", "MarkerStyles": "" };
        Common.AjaxPost("/wms/h/users.html", postData, function (result) {
            console.log('GetMapStaticImage--result--' + JSON.stringify(result));
            $('#mapContainer').html('<img src="' + result.Data + '" style="width:100%;" />');
        })
    }
}