
angular.module('ngTygaSoft.services.Common', [])

.factory('$tygasoftCommon', function () {

    var ts = {};

    ts.AppKey = 'B48DE0AC-8F92-4F1A-A0C6-BEE1A7A56EAC';

    ts.ServerUrl = function () {
        //return "http://localhost/wms";
        return "http://www.tygaweb.com/wms";
    };

    ts.PageIndex = 1;
    ts.PageSize = 20;

    ts.IsMobilePhone = function (s) {
        var reg = /^0{0,1}(13[0-9]|15[0-9]|18[0-9])[0-9]{8}$/;

    };

    ts.String = {
        IsNullOrWhiteSpace: function (s) {
            if (s) {
                if (s.replace(/^\s+|\s+$/g, "") != "") return false;
            }
            return true;
        },
        Trim: function (s) {
            return s.replace(/^\s+|\s+$/g, "");
        }
    };

    ts.CreateListYear = function () {
        var list = [];
        var years = new Array();
        var date = new Date();
        var year = date.getFullYear();
        for (var i = 0; i < 100; i++) {
            years.push(year - i);
        }
        for (var i = 0; i <= years.length; i++) {
            var item = {};
            if (i % 3 == 0) {
                item.f1 = years[i - 3];
                item.f2 = years[i - 2];
                item.f3 = years[i - 1];
                item.f4 = years[i];

                list.push(item);
            }
        }
        return list;
    };

    ts.CreateListGrade = function () {
        var list = [{ "Named": "高一" }, { "Named": "高二" }, { "Named": "高三" }, { "Named": "高一至高三" }, { "Named": "初一" }, { "Named": "初二" }, { "Named": "初三" }, { "Named": "初一至初三" }, { "Named": "小学一年级" }, { "Named": "小学二年级" }, { "Named": "小学三年级" }, { "Named": "小学四年级" }, { "Named": "小学五年级" }, { "Named": "小学六年级" }, { "Named": "小学一年级至六年级" }];
        return list;
    };

    return ts;
});