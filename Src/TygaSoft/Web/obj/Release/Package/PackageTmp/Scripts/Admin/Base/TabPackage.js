var TabPackage = {
    Init: function () {
        this.InitForm();
    },
    InitEvent: function () {

    },
    InitData: function () {

    },
    InitForm: function () {
        var tabs = $("#tabPackage");
        if (!tabs.tabs('exists', '单位')) {
            tabs.tabs('add', {
                selected: true,
                title: '单位',
                style: { paddingTop: 20 },
                href: '/wms/a/ybase.html'
            });
        }
        if (!tabs.tabs('exists', '主计量单位')) {
            tabs.tabs('add', {
                selected: false,
                title: '主计量单位',
                style: { paddingTop: 20 },
                content: '主计量单位'
            });
        }
        if (!tabs.tabs('exists', '内包装')) {
            tabs.tabs('add', {
                selected: false,
                title: '内包装',
                style: { paddingTop: 20 },
                content: '内包装'
            });
        }
        if (!tabs.tabs('exists', '箱包装')) {
            tabs.tabs('add', {
                selected: false,
                title: '箱包装',
                style: { paddingTop: 20 },
                content: '箱包装'
            });
        }
        if (!tabs.tabs('exists', '托盘')) {
            tabs.tabs('add', {
                selected: false,
                title: '托盘',
                style: { paddingTop: 20 },
                content: '托盘'
            });
        }
    },
    OnTabSelect: function (title, index) {
        switch (title) {
            default:
                break;
        }
    }
}