$.extend($.fn.validatebox.defaults.rules, {
    orderCode: {
        validator: function (value, param) {
            return /^([a-zA-Z]+)?\d+$/.test(value);
        },
        message: '单号格式不正确！'
    },
    select: {
        validator: function (value, param) {
            return value != "-1" && value != '请选择';
        },
        message: '必选项'
    },
    cfmPsw: {
        validator: function (value, param) {
            return value == $(param[0]).val();
        },
        message: '前后输入密码不正确，请检查'
    },
    psw: {
        validator: function (value, param) {
            var reg = /^[a-zA-Z0-9_\@\-\!\#\$\%\^\*\.\~]{6,30}$/;
            return reg.test(value);
        },
        message: '密码应由数字、字母、特殊字符（不包括“&”）组成，且是6-30位的字符串！'
    },
    phone: {
        validator: function (value, param) {
            return /^((\d+)|(-)?(\d+)){8,15}$/.test(value);
        },
        message: '请正确输入手机号或电话号码'
    },
    mobilePhone: {
        validator: function (value, param) {
            return /^((\d+)|(-)?(\d+)){8,15}$/.test(value);
        },
        message: '请正确输入手机号'
    },
    telPhone: {
        validator: function (value, param) {
            return /^(\+86)?((\d+)(-)?){8,15}$/.test(value);
        },
        message: '电话号码正确格式为数字'
    },
    QQ: {
        validator: function (value, param) {
            return /^(\d+){5,15}$/.test(value);
        },
        message: '请输入正确的QQ号码'
    },
    percentage: {
        validator: function (value, param) {
            return /^(\d{1,2})(0)?$|^((\d{1,2})\.(\d{1,2}))$/.test($(param[0]).val());
        },
        message: '请输入正确的百分比数值，保留两位小数'
    },
    price: {
        validator: function (value, param) {
            return /(^\d+$)|(^(\d+)\.(\d+){1,2}$)/.test(value);
        },
        message: '请输入正确的金额'
    },
    int: {
        validator: function (value, param) {
            return /^\d+$/.test(value);
        },
        message: '请输入数字'
    },
    float: {
        validator: function (value, param) {
            return /^\d+(\.\d+)?$/.test(value);
        },
        message: '请输入数字或浮点数'
    },
    intNotZero: {
        validator: function (value, param) {
            if (/^\d+$/.test(value)) {
                return value > 0;
            }
            return false;
        },
        message: '请输入大于0的整数'
    },
    numberlength: {
        validator: function (value, param) {
            if (value.length !== 15) {
                return true;
            } else {
                return false;
            }
        },
        message: '请输入15位的数字的IMEI号！'
    },
    rate: {
        validator: function (value, param) {
            if (/^\d+$/.test(value)) {
                return value >= 0 && value <= 100;
            } else {
                return false;
            }
        },
        message: '请输入0至100的数字.'
    },
    ratebase: {
        validator: function (value, param) {
            return value >= 0 && value <= 1;
        },
        message: '请输入0至1的之间的值.'
    },
    dateMaxCompare: {
        validator: function (value, param) {
            return Date.parse(value) >= Date.parse($(param[0]).datebox('getValue'));
        },
        message: '开始时间不能大于结束时间'
    },
    haschinese: {
        validator: function (value, param) {
            return !(/[^\x00-\xff]/g.test(value));
        },
        message: '不能包含中文'
    },
    numberAndEnglish: {
        validator: function (value, param) {
            return !(/[^\w\.\/]/.test(value));
        },
        message: '请输入数字、字母组成的字符串'
    }
});
