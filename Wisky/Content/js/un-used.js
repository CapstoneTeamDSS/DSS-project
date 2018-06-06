var brand_home = function () {
    function init() {
        loadStatistic(null, null);
        setupDaterangepicker();
        window.start = moment().subtract(30, 'days');
        window.end = moment().subtract(1, 'days');
        refreshMap();
    }
    function selectAd(ListAdIdsAndNames) {
        $('#selectAd').html();
        $('#selectAd').append('<option value="-1">&lt;Select an advertising&gt;</option>');
        for (var i = 0; i < ListAdIdsAndNames.length ; i++) {
            $('#selectAd').append('<option value="' + ListAdIdsAndNames[i].AdId + '">' + ListAdIdsAndNames[i].AdName + '</option>');
        }
    }
    function loadReachChangeOfAdId(AdId) {
        if (AdId != -1) {
            $.ajax({
                url: '/BrandManager/Home/getReachChangeChartData',
                data: { AdId: AdId, start: window.start.toISOString(), end: window.end.toISOString() },
                type: 'post',
                success: function (result) {
                    drawChart2OfReachChanges(result.Chart2Data);
                },
                error: function (error) {

                },
            });
        }
    }
    function DrawSplineChart(data, containerId, chartName) {
        $('#' + containerId).highcharts({
            chart: {
                type: 'spline'
            },
            title: {
                text: chartName
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: []
            },
            yAxis: {
                title: {
                    text: 'OnlineCount'
                },
                labels: {
                    formatter: function () {
                        return this.value;
                    }
                }
            },
            tooltip: {
                crosshairs: true,
                shared: true
            },
            plotOptions: {
                spline: {
                    marker: {
                        radius: 4,
                        lineColor: '#666666',
                        lineWidth: 1
                    }
                }
            },
            series: [
            {
                name: 'Online',
                marker: {
                    symbol: 'square'
                },
                data: data
            },
            ]
        });
    }
    function DrawSplineChartDayOfWeek(data, containerId, chartName) {
        $('#' + containerId).highcharts({
            chart: {
                type: 'spline'
            },
            title: {
                text: chartName
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"]
            },
            yAxis: {
                title: {
                    text: 'OnlineCount'
                },
                labels: {
                    formatter: function () {
                        return this.value;
                    }
                }
            },
            tooltip: {
                crosshairs: true,
                shared: true
            },
            plotOptions: {
                spline: {
                    marker: {
                        radius: 4,
                        lineColor: '#666666',
                        lineWidth: 1
                    }
                }
            },
            series: [
            {
                name: 'Online',
                marker: {
                    symbol: 'square'
                },
                data: data
            },
            ]
        });
    }
    function DrawPieChart(chartData, containerId, chartName) {
        // Build the chart
        $('#' + containerId).highcharts({
            chart: {
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: false
            },
            title: {
                text: chartName
            },
            tooltip: {
                pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: false
                    },
                    showInLegend: true
                }
            },
            series: [{
                type: 'pie',
                name: 'Data',
                data: chartData
            }]
        });
    }
    function prepareLoginModePiechart(listLoginModeName, listLoginModeValue) {
        var series = [];
        for (var i = 0; i < listLoginModeName.length; i++) {
            series.push([listLoginModeName[i], listLoginModeValue[i]]);
        }
        DrawPieChart(series, "pie-chart-login-mode", "Login Mode Distribution");
    }
    function prepareDeviceTypePiechart(listDeviceTypeName, listDeviceTypeValue) {
        var series = [];
        for (var i = 0; i < listDeviceTypeName.length; i++) {
            series.push([listDeviceTypeName[i], listDeviceTypeValue[i]]);
        }
        DrawPieChart(series, "pie-chart-device-type", "Device Type Distribution");
    }
    function showImage(url) {
        $('#viewImageModal').modal('show');
        $('#imageContent').html("<img src='" + url + "' alt='' width='inherit' height='450px'>");
    }
    function drawChart2OfReachChanges(data) {
        var days = new Array();//categories (spline chart's x-Axis)
        $.each(data.categories, function (index, day) {
            days.push(day);
        });
        //console.log(days);
        $('#spline').highcharts({
            title: {
                text: 'Reach Changes of Advertising',
                x: -20
            },
            subtitle: {
                text: '',
                x: -20
            },
            xAxis: {
                categories: days
            },
            yAxis: {
                title: {
                    text: 'Reach',
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }],
                allowDecimals: false,
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'middle',
                borderWidth: 0
            },
            series: data.series
        });
    }
    function drawTableAdvertisingsExpiring(data) {
        $('#Expiring').dataTable({
            "bFilter": true,
            "bRetrieve": true,
            "bServerSide": true,
            "bScrollCollapse": true,
            "sAjaxSource": "/BrandManager/Home/LoadAdvertisingExpiringDatatable",
            "fnServerParams": function (aoData) {
                //aoData.push({ "name": "listAdIds", "value": data });
                for (x = 0; x < data.length; x++) {
                    aoData.push({ "name": "listAdIds[" + x + "]", "value": data[x] });
                }
            },
            "bProcessing": true,
            "aoColumnDefs": [
                {
                    "aTargets": [0, 1, 2, 3, 4, 5],
                    "bSortable": false,
                },
                {
                    "aTargets": [6],
                    "fnRender": function (o) {
                        var data = o.aData[6];
                        return "<a class='btn btn-sm btn-primary' onclick='showImage(\"" + data + "\")'>View image</i></a>";
                    },
                    "bSortable": false
                },
                {
                    "aTargets": [7],
                    "bSortable": false,
                    "fnRender": function (o) {
                        var data = o.aData[7];
                        return "<a class='btn btn-sm btn-primary' onclick='showStatisticModal(\"" + data + "\")'><i class='glyphicon glyphicon-stats'></i></a>";
                    }
                },
            ],
            "bAutoWidth": false,
        });
    }

    var setupDaterangepicker = function () {
        $('#reportrange span').html(moment().subtract(30, 'days').format('MMMM D, YYYY') + ' - ' + moment().subtract(1, 'days').format('MMMM D, YYYY'));
        $('#reportrange').daterangepicker({
            format: 'MM/DD/YYYY',
            startDate: moment().subtract(30, 'days'),
            endDate: moment().subtract(1, 'days'),
            minDate: '01/01/2012',
            maxDate: '12/31/2015',
            dateLimit: { days: 60 },
            showDropdowns: true,
            showWeekNumbers: true,
            timePicker: false,
            timePickerIncrement: 1,
            timePicker12Hour: true,
            ranges: {
                'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                'Last 7 Days': [moment().subtract(6, 'days'), moment().subtract(1, 'days')],
                'Last 30 Days': [moment().subtract(30, 'days'), moment().subtract(1, 'days')],
                'This Month': [moment().startOf('month'), moment().endOf('month')],
                'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
            },
            opens: 'left',
            drops: 'down',
            buttonClasses: ['btn', 'btn-sm'],
            applyClass: 'btn-primary',
            cancelClass: 'btn-default',
            separator: ' to ',
            locale: {
                applyLabel: 'Submit',
                cancelLabel: 'Cancel',
                fromLabel: 'From',
                toLabel: 'To',
                customRangeLabel: 'Custom',
                daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
                monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
                firstDay: 1
            }
        }, function (start, end, label) {
            console.log(start.toISOString(), end.toISOString(), label);
            $('#reportrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
        });
        $('#reportrange').on('apply.daterangepicker', function (ev, picker) {
            console.log(picker.startDate + "aaaaa");
            console.log(picker.endDate);
            var startTime = picker.startDate.toISOString();
            var endTime = picker.endDate.toISOString();
            loadStatistic(startTime, endTime);
        });
    }

    var setUpPieChart = function (chartData) {
        var data = [];
        for (var i = 0; i < chartData.length; i++) {
            data.push([chartData[i].name, chartData[i].amount]);
        }
        //console.log(data);
        $('#pie-chart-visitor-per-location').highcharts({
            chart: {
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: false
            },
            title: {
                text: 'Vitors Per Location'
            },
            tooltip: {
                pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: false
                    },
                    showInLegend: true
                }
            },
            series: [{
                type: 'pie',
                name: 'Login Mode:',
                data: data,
            }]
        });
    }

    function initTotalVisitorTable(data) {
        var trHTML = '';
        $.each(data, function (index, item) {
            trHTML += '<tr><td>' + item.Value + '</td><td>' + item.Key + '</td></tr>';
        });
        $('#TotalVisitorTable tbody').children().remove();
        $('#TotalVisitorTable tbody').append(trHTML);
    }

    function loadGenericInfo(data) {
        $('#displayAppName').html(data[0]['Item2']);
        $('#displayAppStatus').html(data[1]['Item2']);
    }

    function loadStatistic(startDate, endDate) {
        $.ajax({
            url: '/BrandManager/Home/LoadStatisticData',
            data: { APGId: getBrandId(), startDate: startDate, endDate: endDate },
            type: "POST",
            success: function (result) {
                // table of total vistor per location
                //initTotalVisitorTable(result.TableData);
                setUpPieChart(result.ChartData);
                loadGenericInfo(result.TableData);
                var listHourOfDay = result.listHourOfDay;
                DrawSplineChart(listHourOfDay, "line-chart-hour-of-day", "Online per Hour");
                var listDayOfWeek = result.listDayOfWeek;
                DrawSplineChartDayOfWeek(listDayOfWeek, "line-chart-day-of-week", "Online per day of week");
                prepareLoginModePiechart(result.listLoginModeName, result.listLoginModeValue);
                prepareDeviceTypePiechart(result.listDeviceTypeName, result.listDeviceTypeValue);
            },
            error: function (error) {
                alert("Error occured, please try again later");
            }
        });
    }


    window.IsAlreadyRefreshAdsReport = false;
    function refreshAdsReport() {
        if (!window.IsAlreadyRefreshAdsReport) {
            setupAdsDaterangepicker();
            loadAllReady(null, null);
            window.IsAlreadyRefreshAdsReport = true;
        }
    }


    function drawTableTopFiveReachAdvertising(data) {
        $('#TopAd').dataTable({
            "bFilter": true,
            "bRetrieve": true,
            "bServerSide": true,
            "bScrollCollapse": true,
            "sAjaxSource": "/BrandManager/Home/LoadTopFiveReachAdvertisingDatatable",
            "fnServerParams": function (aoData) {
                //aoData.push({ "name": "listAdIds", "value": data });
                for (x = 0; x < data.length; x++) {
                    aoData.push({ "name": "listAdIds[" + x + "]", "value": data[x] });
                }
            },
            "bProcessing": true,
            "aoColumnDefs": [
                {
                    "aTargets": [0, 1, 2, 3, 4, 5, 8],
                    "bSortable": false
                },
                {
                    "aTargets": [6],
                    "fnRender": function (o) {
                        var data = o.aData[6];
                        return "<a class='btn btn-sm btn-primary' onclick='showImage(\"" + data + "\")'>View image</i></a>";
                    },
                    "bSortable": false
                },
                {
                    "aTargets": [7],
                    "bSortable": false,
                    "fnRender": function (o) {
                        var data = o.aData[7];
                        return "<a class='btn btn-sm btn-primary' onclick='showStatisticModal(\"" + data + "\")'><i class='glyphicon glyphicon-stats'></i></a>";
                    }
                },
            ],
            "bAutoWidth": false,
        });
    }

    function drawTableAllAdvertising(data) {
        $('#AdvertisingDatatable').dataTable({
            "bFilter": true,
            "bRetrieve": true,
            "bServerSide": true,
            "bScrollCollapse": true,
            "sAjaxSource": "/BrandManager/Home/LoadAdvertisingDatatable",
            "fnServerParams": function (aoData) {
                for (x = 0; x < data.length; x++) {
                    aoData.push({ "name": "listAdIds[" + x + "]", "value": data[x] });
                }
            },
            "bProcessing": true,
            "aoColumnDefs": [
                {
                    "aTargets": [0, 1, 2, 3, 4, 5],
                    "bSortable": false,
                },
                {
                    "aTargets": [6],
                    "fnRender": function (o) {
                        var data = o.aData[6];
                        return "<a class='btn btn-sm btn-primary' onclick='showImage(\"" + data + "\")'>View image</i></a>";
                    },
                    "bSortable": false
                },
                {
                    "aTargets": [7],
                    "bSortable": false,
                    "fnRender": function (o) {
                        var data = o.aData[7];
                        return "<a class='btn btn-sm btn-primary' onclick='showStatisticModal(\"" + data + "\")'><i class='glyphicon glyphicon-stats'></i></a>";
                    }
                },
            ],
            "bAutoWidth": false,
        });
    }

    function showStatisticModal(adID) {
        var initData = function () {
            $.ajax({
                url: '/BrandManager/AdStatistic/Statistic',
                data: { id: adID },
                type: "POST",
                success: function (result) {
                    $('[data-role=impression-value]').html(result.basicdata.Impression);
                    $('[data-role=reach-value]').html(result.basicdata.Reach);
                    $('[data-role=click-value]').html(result.basicdata.Click);
                    $('[data-role=ctr-value]').html(result.basicdata.CTR);
                    $('[data-role=frequency-value]').html(result.basicdata.Frequency);
                    $('[data-role=advertising-name]').html(result.basicdata.AdName);
                    $('[data-role=start-date]').html(result.basicdata.StartDate);
                    $('[data-role=end-date]').html(result.basicdata.EndDate);
                    window.setupHighchart(result.ChartData);
                    $('#statisticModal').modal('show');
                },
                error: function (error) {

                }
            });
        }
        $.ajax({
            url: '/BrandManager/AdStatistic/GetStatisticModal',
            type: "POST",
            success: function (result) {
                $('#statisticModal').html(result);
                initData();
                setupAdsDaterangepicker();
            }
        });
    }

    window.IsAlreadyLoadAllAdvertisingOfBrand = false;
    function loadAllReady(startDateTime, endDateTime) {
        if (!IsAlreadyLoadAllAdvertisingOfBrand) {
            $.ajax({
                url: '/BrandManager/Home/loadAllAdvertisingOfBrand',
                data: { BrandId: getBrandId() },
                type: "POST",
                success: function (result) {
                    drawTableAllAdvertising(result.listAdIds);
                    $('[data-role=info1]').html(result.totalReach);
                    $('[data-role=info2]').html(result.totalImpression);
                    $('[data-role=info3]').html(result.totalClicks);
                    window.IsAlreadyLoadAllAdvertisingOfBrand = true;
                    drawTableTopFiveReachAdvertising(result.ListTop5ReachAdvertisingsIds);
                    drawTableAdvertisingsExpiring(result.ListAdvertisingExpringIds);
                    selectAd(result.ListAdIdsAndNames);
                },
                error: function (error) {

                },
            });
        }
    }

    function setupAdsDaterangepicker() {
        $('#Adsreportrange span').html(moment().subtract(30, 'days').format('MMMM D, YYYY') + ' - ' + moment().subtract(1, 'days').format('MMMM D, YYYY'));
        $('#Adsreportrange').daterangepicker({
            format: 'MM/DD/YYYY',
            startDate: moment().subtract(30, 'days'),
            endDate: moment().subtract(1, 'days'),
            minDate: '01/01/2012',
            maxDate: '12/31/2015',
            dateLimit: { days: 60 },
            showDropdowns: true,
            showWeekNumbers: true,
            timePicker: false,
            timePickerIncrement: 1,
            timePicker12Hour: true,
            ranges: {
                'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                'Last 7 Days': [moment().subtract(6, 'days'), moment().subtract(1, 'days')],
                'Last 30 Days': [moment().subtract(30, 'days'), moment().subtract(1, 'days')],
                'This Month': [moment().startOf('month'), moment().endOf('month')],
                'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
            },
            opens: 'left',
            drops: 'down',
            buttonClasses: ['btn', 'btn-sm'],
            applyClass: 'btn-primary',
            cancelClass: 'btn-default',
            separator: ' to ',
            locale: {
                applyLabel: 'Submit',
                cancelLabel: 'Cancel',
                fromLabel: 'From',
                toLabel: 'To',
                customRangeLabel: 'Custom',
                daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
                monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
                firstDay: 1
            }
        }, function (start, end, label) {
            console.log(start.toISOString(), end.toISOString(), label);
            $('#Adsreportrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
        });
        $('#Adsreportrange').on('apply.daterangepicker', function (ev, picker) {
            console.log(picker.startDate);
            console.log(picker.endDate);
            var startTime = picker.startDate.toISOString();
            var endTime = picker.endDate.toISOString();
            window.start = picker.startDate;
            window.end = picker.endDate;
            loadAllReady(startTime, endTime);
        });
    }
}

