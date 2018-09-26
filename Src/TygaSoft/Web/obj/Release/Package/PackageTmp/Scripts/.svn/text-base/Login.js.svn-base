
var Login = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent:function(){
        $("#form1").submit(function () {
            var sUserName = $.trim($("#txtUserName").val());
            if (sUserName == "") {
                $(".warning-con").html("用户名不能为空");
                $(".warning").show();
                return false;
            }
            var sPsw = $.trim($("#txtPsw").val());
            if (sPsw == "") {
                $(".warning-con").html("密码不能为空");
                $(".warning").show();
                return false;
            }
            var sVc = $.trim($("#txtVc").val());
            if (sVc == "") {
                $(".warning-con").html("验证码不能为空");
                $(".warning").show();
                return false;
            }
            if (sVc.length != 4) {
                $(".warning-con").html("请输入4位的验证码");
                $(".warning").show();
                return false;
            }
            $(".warning").hide();
            $("#btn-submit").val("正在登录...");
        });
        $(".placeholder").click(function () {
            $(this).next().focus();
        })
        $("#loginInfo .ui-input-h40").focusin(function () {
            $(this).prev().hide();
        })
        $("#loginInfo .ui-input-h40").focusout(function () {
            if ($.trim($(this).val()) == "") {
                $(this).prev().show();
            }
        })
        $("#cbRememberMe").click(function () {
            $("#btn-submit").focus();
        })
    },
    InitData: function () {
        var myData = $("#myData");
        if (myData) {
            var sMyData = $.trim(myData.html());
            if (sMyData != "") {
                var json = eval("(" + sMyData + ")");
                if (json.UserName != '') {
                    $("#txtUserName").val(json.UserName);
                    $(".placeholder:first").click();
                }

                $("#cbRememberMe").attr("checked", "checked");
                $("#cbRememberMe").val("1");
            }
        }
    }
}