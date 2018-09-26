
var MenusFun = {
    Init: function () {
        MenusFun.SelectCurrent();
        MenusFun.Hover();
    },
    Hover: function () {
        $(".nav a").hover(function () {
            $(this).addClass("hover").siblings().removeClass("hover");
        }, function () {
            $(this).removeClass("hover")
        })
    },
    SelectCurrent: function () {
        var currMenu = $("#SitePaths>span:last").text();
        $(".nav a").filter(":contains('" + currMenu + "')").addClass("curr").siblings().removeClass("curr");
    }
};

var UserMenus = {
    Init: function () {
        UserMenus.TreeLoad();
    },
    TreeLoad: function () {
        var t = $("#eastTree");
        $.ajax({
            url: "/ScriptServices/UsersService.asmx/GetTreeJsonForMenu",
            type: "post",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            success: function (json) {
                var jsonData = (new Function("", "return " + json.d))();
                t.tree({
                    data: jsonData,
                    formatter: function (node) {
                        if (node.id.length > 0) {
                            return "<a href=\"" + node.id + "\">" + node.text + "</a>";
                        }
                        return node.text;
                    },
                    animate: true
                })
                UserMenus.SelectCurrent();
                //t.children().children("div:first").hide();
            }
        });
    },
    SelectCurrent: function () {
        var navArr = $("#eastTree").find("a");
        var spanArr = $("#SitePaths>span:not(:contains('>'))");
        var spanArrLen = spanArr.length - 1;
        for (i = spanArrLen; i >= 0; i--) {

            var hlNav = navArr.filter(":contains('" + spanArr.eq(i).text() + "')");
            if (hlNav.length > 0) {
                hlNav.parent().parent().addClass("bg_curr");
                break;
            }
        }
    }
};

var SharesMenus = {
    Init: function () {
        SharesMenus.SelectedAccordion();
    },
    SelectCurrent: function () {
        var navArr = $("#eastTree").find("a");
        var spanArr = $("#SitePaths>span:not(:contains('>'))");
        var spanArrLen = spanArr.length - 1;
        for (i = spanArrLen; i >= 0; i--) {
            var hlNav = navArr.filter(":contains('" + spanArr.eq(i).text() + "')");
            if (hlNav.length > 0) {
                hlNav.parent().parent().addClass("bg_curr");
                break;
            }
        }
    },
    SelectedAccordion: function () {
        $("#menuLeft").accordion('select', $.trim($("#SitePaths>span:eq(2)").text()));
    }
};