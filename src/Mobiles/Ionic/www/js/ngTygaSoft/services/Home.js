angular.module('ngTygaSoft.services.Home', [])
.factory('$tygasoftHome', function () {
    var ts = {};

    ts.Bind = function ($scope) {
        $scope.HomeMenuItems = ts.GetNavList();
        //$scope.ListNav = ts.GetNavList();
    };

    ts.GetNavList = function () {
        //return [{ Name: 'Delivery Task', Url: '#/tab/OrderPicked', Src: 'img/icons/home-jh.png', IsBorder: false }, { Name: 'Review', Url: '#/tab/StockProduct', Src: 'img/icons/home-kccx.png', IsBorder: true }];
        return [{ "Name": "智能出入库", "Src": "img/icons/home-rfid.png", "Url": "#/tab/Rfid" }, { Name: '上架', Url: '#/tab/ShelfMission', Src: 'img/icons/home-sj.png', IsBorder: false }, { Name: '拣货', Url: '#/tab/OrderPicked', Src: 'img/icons/home-jh.png', IsBorder: false }, { Name: '盘点', Url: '#/tab/Pandian', Src: 'img/icons/home-pd.png', IsBorder: false }, { Name: '查询', Url: '#/tab/StockProduct', Src: 'img/icons/home-kccx.png', IsBorder: true }];
        //var navData = [{ Name: '上架', Url: '#/tab/ShelfMission', Src: 'img/home-sj.png', IsBorder: false }, { Name: '库存查询', Url: '#/tab/StockProduct', Src: 'img/home-kccx.png', IsBorder: true }, { Name: '拣货', Url: '#/tab/OrderPicked', Src: 'img/home-jh.png', IsBorder: false }, { Name: '盘点', Url: '#/tab/Pandian', Src: 'img/home-pd.png', IsBorder: false }];
        ////var navData = [{ Name: '收货', Url: '#', Src: 'img/home-sh.png', IsBorder: false }, { Name: '无单收货', Url: '#', Src: 'img/home-wdsh.png', IsBorder: true }, { Name: '上架', Url: '#/tab/ShelfMission', Src: 'img/home-sj.png', IsBorder: false }, { Name: '移动', Url: '#', Src: 'img/home-yd.png', IsBorder: false }, { Name: '库存查询', Url: '#/tab/StockProduct', Src: 'img/home-kccx.png', IsBorder: true }, { Name: '拣货筛选', Url: '#', Src: 'img/home-jhsx.png', IsBorder: false }, { Name: '拣货', Url: '#/tab/OrderPicked', Src: 'img/home-jh.png', IsBorder: false }, { Name: '动态拣货', Url: '#', Src: 'img/home-dtjh.png', IsBorder: true }, { Name: '盘点', Url: '#/tab/Pandian', Src: 'img/home-pd.png', IsBorder: false }];

        //var listNav = [];
        //for (var i = 0; i < navData.length; i++) {
        //    if (i % 3 == 0) {
        //        var item = {};
        //        item.Name1 = navData[i].Name;
        //        item.Name2 = navData[i + 1] == undefined ? "" : navData[i + 1].Name;
        //        item.Name3 = navData[i + 2] == undefined ? "" : navData[i + 2].Name;
        //        item.Url1 = navData[i].Url;
        //        item.Url2 = navData[i + 1] == undefined ? "" : navData[i + 1].Url;
        //        item.Url3 = navData[i + 2] == undefined ? "" : navData[i + 2].Url;
        //        item.Src1 = navData[i].Src;
        //        item.Src2 = navData[i + 1] == undefined ? "" : navData[i + 1].Src;
        //        item.Src3 = navData[i + 2] == undefined ? "" : navData[i + 2].Src;
        //        item.IsBorder1 = navData[i].IsBorder ? ' border-lr' : '';
        //        item.IsBorder2 = navData[i + 1] == undefined ? " border-lr" : navData[i + 1].IsBorder ? ' border-lr' : '';
        //        item.IsBorder3 = navData[i + 2] == undefined ? "" : navData[i + 2].IsBorder ? ' border-lr' : '';
        //        listNav.push(item);
        //    }
        //}
        //return listNav;
    };

    return ts;
});