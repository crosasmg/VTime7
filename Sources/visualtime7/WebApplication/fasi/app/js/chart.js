var chartSupport = new function () {
    this.VectorChars = [];

    this.randomScalingFactor = function () {
        return Math.round(Math.random() * 100);
    };

    this.Instance = function (name) {
        var result = null;
        chartSupport.VectorChars.forEach(function (element) {
            if (element.options.element === name) {
                result = element;
            }
        });
        return result;
    };

    this.Update = function (name) {
        var instance = chartSupport.Instance(name);
        if (instance !== null) {
            instance.redraw();
            $(window).trigger('resize');
        }
    };

    this.Initialization = function (element, options) {
        var yKeys = [];
        var labels = [];

        var itemChar = chartSupport.Instance(element);

        if (options.Series.Definitions !== null && options.Series.Definitions !== undefined) {
            options.Series.Definitions.forEach(function (elementSource) {
                yKeys.push(elementSource.argument);
            });
        }

        if (options.Series.Definitions !== null && options.Series.Definitions !== undefined) {
            options.Series.Definitions.forEach(function (elementSource) {
                labels.push(elementSource.label);
            });
        }

        if (options.Series.Data.length === 0) {
            var item = '{' + options.LabelsPropertiesName + ':"",';
            var ids = yKeys.join(" , ");

            yKeys.forEach(function (elementSource) {
                ids = ids.replace(elementSource, elementSource + ':""');
            });

            item = item + ids + '}';
            options.Series.Data.push(JSON.parse(JSON.stringify(item)));
        }

        if (itemChar === null) {
            switch (options.type) {
                case 'bar':
                    itemChar = Morris.Bar({
                        element: element,
                        data: options.Series.Data,
                        xkey: options.LabelsPropertiesName,
                        ykeys: yKeys,
                        labels: labels,
                        hideHover: 'auto',
                        resize: true,
                        barColors: ['#1ab394', '#cacaca']
                    });
                    chartSupport.VectorChars.push(itemChar);

                    break;

                case 'pie':
                    if (itemChar === null) {
                        itemChar = Morris.Donut({
                            element: element,
                            data: options.Series.Data,
                            xkey: options.LabelsPropertiesName,
                            ykeys: yKeys,
                            labels: labels,
                            hideHover: 'auto',
                            resize: true,
                            barColors: ['#1ab394', '#cacaca']
                        });
                        chartSupport.VectorChars.push(itemChar);
                    }
                    break;
                default:
            }
        } else {
            if (itemChar !== null) {
                itemChar.setData(options.Series.Data);
            }
        }
        window.dispatchEvent(new Event('resize'));
    };
};