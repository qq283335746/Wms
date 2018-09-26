var Default = {
    Init: function () {
        Default.InitData();
        Default.InitProcess();
    },
    InitData: function () {
        //$("#layoutBory").css("height", $(window).height());
        Default.Init3DPie();
    },
    Init3DPie: function () {
        $('#container').highcharts({
            chart: {
                type: 'pie',
                options3d: {
                    enabled: true,
                    alpha: 45,
                    beta: 0
                }
            },
            title: {
                text: '供应商分布图'
            },
            tooltip: {
                pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    depth: 35,
                    dataLabels: {
                        enabled: true,
                        format: '{point.name}'
                    }
                }
            },
            series: [{
                type: 'pie',
                name: '占比',
                data: [
                    ['深圳', 26],
                    {
                        name: 'PDA',
                        y: 12.8,
                        sliced: true,
                        selected: true
                    },
                    ['广州', 45],
                    ['北京', 43],
                    ['上海', 29],
                    ['海南', 11]
                ]
            }]
        });
    },
    InitProcess: function () {
        var childs = $('.process>a>div');
        childs.each(function (i,item) {
            if (i % 2 == 0) {
                $(item).css('margin-top', '100px');
            }
        })
    }
}