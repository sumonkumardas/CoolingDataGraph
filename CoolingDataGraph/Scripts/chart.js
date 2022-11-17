
$(function () { 


        var firstPointToRed = function(datas) {
            datas[0] = {
                y: datas[0],
                color: 'red'
            };
        };

        var LastPointToRed = function(datas) {
            datas[datas.length - 1] = {
                y: datas[datas.length - 1],
                color: 'red'
            };
        };

            var RealDataTitleShown = false, anamalyDataTitleShown = false;
            $.ajax({
                url: "/Home/GetGraphValues",
                success: function(result) {
                    var series = [];
                    var i = 0;
                    for (i = 0; i < result.GraphModel.SeriesDatas.length; i++) {
                        var anomanlyData = result.GraphModel.SeriesDatas[i].Annomaly;

                        var datas = result.GraphModel.SeriesDatas[i].Data;

                        if (!anomanlyData) {
                            LastPointToRed(datas);
                            if (i > 0)
                                firstPointToRed(datas);
                        } else {
                            firstPointToRed(datas);
                        }

                        var obj = {
                            name: result.GraphModel.SeriesDatas[i].Name,
                            data: datas,
                            color: result.GraphModel.SeriesDatas[i].Color,
                            pointStart: result.GraphModel.SeriesDatas[i].PointStart,
                            showInLegend: anomanlyData ? !anamalyDataTitleShown : !RealDataTitleShown
                        };

                        series.push(obj);
                        if (i < 2) {
                            if (anomanlyData)
                                anamalyDataTitleShown = true;
                            else
                                RealDataTitleShown = true;
                        }
                    }

                    $('#chart').highcharts({
                        title: {
                            text: 'Cooling Data Information Per Hour',
                            x: -20 //center
                        },
                        subtitle: {
                            text: 'Green Building',
                            x: -20
                        },
                        xAxis: {
                            categories: result.GraphModel.Categories
                        },
                        yAxis: {
                            title: {
                                text: 'Value'
                            },
                            plotLines: [
                                {
                                    value: 0,
                                    width: 1,
                                    color: '#808080'
                                }
                            ]
                        },
                        tooltip: {
                            valueSuffix: 'kw'
                        },
                        legend: {
                            layout: 'vertical',
                            align: 'right',
                            verticalAlign: 'middle',
                            borderWidth: 0
                        },
                        series: series

                    });
                }
            });





});