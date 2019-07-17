var jeasyuiFun = {
    show: function (title, msg) {
        $.messager.show({
            title: title,
            msg: msg,
            showType: 'slide',
            style: {
                right: '',
                top: document.body.scrollTop + document.documentElement.scrollTop,
                bottom: ''
            }
        });
    }
}