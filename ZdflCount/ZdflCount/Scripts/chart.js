$(".statistic-btn-query").click(function () {
    var startDate = document.getElementById("startDate").value;
    var endDate = document.getElementById("endDate").value;
    var horizon = document.getElementById("horizon").value;
    var vertical = document.getElementById("vertical").value;
    var chart = document.getElementById("chart").value;
    var userNumber = document.getElementById("userNumber").value;
    var userName = document.getElementById("userName").value;
    var machine = document.getElementById("machine").value;
    var order = document.getElementById("order").value;
    var room = document.getElementById("room").value;
    if (startDate == "" || endDate == "") {
        alert("起止日期为必选项"); return;
    }
    var dataParam = {
        startDate: startDate,
        endDate: endDate,
        horizon: horizon,
        vertical: vertical
    }
    if (userName != "") dataParam.userName = userName;
    if (userNumber != "") dataParam.userNumber = userNumber;
    if (machine != "") dataParam.machine = machine;
    if (order != "") dataParam.order = order;
    if (room != "") dataParam.room = room;

    $.ajax({
        url: "/Manage/Statistics",
        type: "POST",
        data: dataParam,
        success: function (data) {
            var myChart = echarts.init(document.getElementById('mainChartBlock'));
            var option = {};
            switch (chart) {
                case "line": option = lineChart(data); break;
                case "bar": option = barChart(data); break;
                case "pie": option = pieChart(data, vertical); break;
                default: break;
            }
            myChart.setOption(option);
            document.getElementsByClassName("statistic-btn-excel").value = data.fileName;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("统计数据读取失败：" + errorThrown);
        }
    });

});

$(".statistic-btn-excel").click(function () {
    var fileName = document.getElementsByClassName("statistic-btn-excel").value;
    if (fileName == null || fileName == undefined) {
        alert("请先查询再下载");
        return;
    }
    var urlPath = location.href;
    var excelPath = urlPath.substr(0, urlPath.indexOf("Statistics"));
    window.open(excelPath + "DownloadExcel?excel=" + fileName);
});

function barChart(data) {
    return {
        title: {
            text: data.chartTitle,
            x: "center"
        },
        tooltip: {
            trigger: 'item'
        },
        grid: {
            left: '3%',
            right: '4%',
            bottom: '3%',
            containLabel: true
        },
        xAxis: [
            {
                type: 'category',
                boundaryGap: true,
                data: data.dataKey,
                axisTick: {
                    alignWithLabel: true
                }
            }
        ],
        yAxis: [
            {
                type: 'value'
            }
        ],
        series: [
            {
                type: 'bar',
                data: data.dataValue
            }
        ]
    };
}

function lineChart(data) {
    return {
        title: {
            text: data.chartTitle,
            x: "center"
        },
        tooltip: {
            trigger: 'item'
        },
        grid: {
            left: '3%',
            right: '4%',
            bottom: '3%',
            containLabel: true
        },
        xAxis: {
            type: 'category',
            boundaryGap: false,
            data: data.dataKey
        },
        yAxis: {
            type: 'value'
        },
        series: [
            {
                type: 'line',
                data: data.dataValue
            }
        ]
    };
}

function pieChart(data,itemTitle) {
    var showValue = [];
    for(var i=0;i<data.dataKey.length;i++)
    {
        showValue.push({
            name: data.dataKey[i],
            value:data.dataValue[i]
        });
    }
    return {
        title: {
            text: data.chartTitle,
            x: "center"
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        legend: {
            type: 'scroll',
            orient: 'vertical',
            right: 10,
            top: 20,
            bottom: 20,
            data: data.dataKey
        },
        series: [{
            name: itemTitle,
            type: 'pie',
            radius: '55%',
            center: ['40%', '50%'],
            data: showValue,
            itemStyle: {
                emphasis: {
                    shadowBlur: 10,
                    shadowOffsetX: 0,
                    shadowColor: 'rgba(0, 0, 0, 0.5)'
                }
            }
        }]
    };
}