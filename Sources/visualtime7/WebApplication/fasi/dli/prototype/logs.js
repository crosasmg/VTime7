$(function () {

    var sales;

    //$.ajax({
    //    type: "POST",
    //    url: "/fasi/utils/logs.aspx/test2",
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    async: false,
    //    data: JSON.stringify({})
    //}).done(function (result) {
    //    sales = eval(result.d);
    //});

    //$("#dataGridContainer").dxPivotGrid({
    //    allowSortingBySummary: true,
    //    allowSorting: true,
    //    allowFiltering: true,
    //    allowExpandAll: true,
    //    height: 440,
    //    showBorders: true,
    //    fieldChooser: {
    //        enabled: true,
    //        applyChangesMode: "instantly",
    //        allowSearch: true
    //    },
    //    "export": {
    //        enabled: true,
    //        fileName: "Sales"
    //    },
    //    dataSource: {
    //        fields: [{
    //            dataField: "EFFECTDATE",
    //            dataType: "date",
    //            groupInterval: "month",
    //            area: "column"
    //        }, {
    //                dataField: "EFFECTDATE",
    //                dataType: "date",
    //                groupInterval: "day",
    //                area: "column"
    //            }, {
    //            dataField: "DESCRIPTION",
    //            area: "row"
    //        }, {
    //            caption: "Sales",
    //            dataField: "STATE",
    //            dataType: "number",
    //            summaryType: "count",
    //            area: "data"
    //        }],
    //        store: sales
    //    }
    //});

    //    columns: ["EFFECTDATE", "IPADDRESS", "EMAIL", "STATE", "DESCRIPTION"]

    $("#dataGridContainer").dxDataGrid({
        dataSource: new DevExpress.data.DataSource({
            load: function () {
                var d = $.Deferred();
                $.ajax({
                    type: "POST",
                    url: "/fasi/utils/logs.aspx/test2",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({})
                }).done(function (result) {
                    d.resolve(eval(result.d));
                });
                return d.promise();
            }
        }),
        columns: ["EFFECTDATE", "IPADDRESS", "EMAIL", "STATE", "DESCRIPTION"]
    });

    //$("#dataGridContainer").dxDataGrid({
    //    dataSource: new DevExpress.data.DataSource({
    //        load: function () {
    //            var d = $.Deferred();
    //            $.ajax({
    //                type: "POST",
    //                url: "/fasi/utils/logs.aspx/test",
    //                contentType: "application/json; charset=utf-8",
    //                dataType: "json",
    //                data: JSON.stringify({})
    //            }).done(function (result) {
    //                d.resolve(result.d);
    //            });
    //            return d.promise();
    //        }
    //    }),
    //    columns: ["CompanyName", "City", "State", "Phone", "Fax"]
    //});

    var xhr = new XMLHttpRequest();
    xhr.open("GET", "https://platform.clickatell.com/messages/http/send?apiKey=Pf3OOk_AQtS0jlpVIKyf2w==&to=50672155569&content=Test+message+text", true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            console.log('success');
        }
    };
    xhr.send();
});