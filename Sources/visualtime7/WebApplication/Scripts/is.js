function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

var urlBase = window.location.protocol + '//' + window.location.host + '/dropthings/is.aspx';

//Métodos de Data Factory

function PopulateEnableConnections() {
    var keyValue = getParameterByName('key');
    var param = JSON.stringify({ key: keyValue });
    var urlAction = urlBase + "/GetConnetionsEnable";
    $.ajax({
        url: urlAction,
        data: param,
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            $('#ddlConnectionEnable').children().remove();
            if (result.d.length !== 0) {
                $.each(result.d, function (i, item) {
                    $('#ddlConnectionEnable').append($('<option>', {
                        value: item.Name,
                        text: item.Name
                    }));
                });
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("fail: " + errorThrown);
        }
    });
}

function disableElements(el) {
    el
}

function enableElements(el) {
    for (var i = 0; i < el.length; i++) {
        el[i].disabled = false;

        enableElements(el[i].children);
    }
}


$("#btnSelect").click(function () {
    var rowSelected = $('#Files').bootstrapTable('getSelections')
    if (rowSelected.length !== 0) {
        $("#ModalViewer").modal();
    } else {
        alert("Sebe seleccionar una row para poder editarlo");
    }
});

//Data Factory

$("#btnDataFactoryRun").click(function () {
    $("#lblMessageErrorDataFactory").text("");
    var urlAction = urlBase + "/ExcuteQuery";
    var keyValue1 = $('#ddlConnectionEnable option:selected').text();
    var keyValue2 = $('#txtQureyText').val();
    var param = JSON.stringify({ ConnectionStrinName: keyValue1, Query: keyValue2 });
    $.ajax({
        url: urlAction,
        data: param,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            $("#loading").dialog('open').html("<p>Please Wait...</p>");
        },
        success: function (result) {
            if (result.d.HasError === false) {
                if (result.d.Count !== 0) {
                    $("#dataTableDataFactoryResult").bootstrapTable('destroy');
                    var columnsValues = JSON.stringify({ "Columns": [] });
                    var parse_objColums = JSON.parse(columnsValues);

                    var Rowsalues = JSON.stringify({ "Row": [] });
                    var parse_obj = JSON.parse(Rowsalues);

                    $.each(result.d.ColumnaNames, function (index, value) {
                        var row = { "field": index, "title": value };
                        parse_objColums['Columns'].push(row);
                    });

                    $.each(result.d.Values, function (index, item) {
                        var data = {};
                        var temporalValue = item.Value;
                        $.each(temporalValue, function (indexInternal, valueInternal) {
                            data[indexInternal] = valueInternal.Value;
                        });;
                        parse_obj['Row'].push(data);
                    });

                    $("#dataTableDataFactoryResult").bootstrapTable({
                        height: 500,
                        striped: true,
                        pagination: true,
                        pageSize: 50,
                        search: true,
                        showColumns: true,
                        columns: parse_objColums.Columns,
                        data: parse_obj.Row
                    });
                } else {
                    $("#dataTableDataFactoryResult").bootstrapTable('destroy');
                    $("#zoneMessageErrorDataFactory").show("slow");
                    $("#lblMessageErrorDataFactory").text(result.d.MessageError);
                    $('#Files').bootstrapTable('removeAll');
                }
            } else {
                $("#dataTableDataFactoryResult").bootstrapTable('destroy');
                $("#zoneMessageErrorDataFactory").show("slow");
                $("#lblMessageErrorDataFactory").text(result.d.MessageError);
                $('#Files').bootstrapTable('removeAll');
            }
            $("#loading").dialog("close");
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $("#dataTableDataFactoryResult").bootstrapTable('destroy');

            $("#loading").dialog("close");

            alert("fail: " + errorThrown);
        }
    });
});


function FolderAndFiles(path) {
    var urlAction = urlBase + "/FolderAndFiles";
    var param = JSON.stringify({ path: path });
    $.ajax({
        url: urlAction,
        data: param,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            var root = result.d;
            var data = new Array();
            data.push(Render(root));

            //$('#jstree1').on('changed.jstree', function (e, data) {
            //        var i, j, r = [];
            //        for (i = 0, j = data.selected.length; i < j; i++) {
            //            var rr = data.instance.get_node(data.selected[i]).PathFullName;
            //            r.push(data.instance.get_node(data.selected[i]).text);
            //        }
            //        $('#event_result').html('Selected: ' + r.join(', '));
            //})

            $('#jstree1').on('select_node.jstree', function (e, data) {
                $('#ContainerFile').empty();
                if (data.node.data.IsFolder === true) {
                    data.node.data.Childs.forEach(
                        function Formate(value) {
                            if (value.IsFolder === false) {
                                moment.locale('es');
                                var d = new moment(value.LastWrite).format('LLLL');
                                var PathFullName = Encrypted(value.PathFullName);
                                var item = ' <div id="' + value.Name + '" class="file-box">' +
                                    ' 	<div class="file">' +
                                    ' 		<a href="#">' +
                                    ' 			<span class="corner"></span>' +
                                    ' ' +
                                    ' 			<div class="icon">' +
                                    ' 				<i class="fa fa-file"></i>' +
                                    ' 			</div>' +
                                    ' 			<div class="file-name">' +
                                    ' 				' + value.Name +
                                    ' 				<br>' +
                                    ' 				<small>Write:' + d + '</small>' +
                                    ' 				<br>' +
                                    '               <a href="' + window.location.protocol + '//' + window.location.host + '/dropthings/download.ashx?path=' + PathFullName + '&IsFolder=False' + '"><i class="fa fa-download"></i></a>' +
                                    ' 			</div>' +
                                    ' 		</a>' +
                                    ' 	</div>' +
                                    ' </div>';
                                $('#ContainerFile').append(item);
                            }
                        }
                    );
                }
                //var i, j, r = [];
                //for (i = 0, j = data.selected.length; i < j; i++) {
                //    var rr = data.instance.get_node(data.selected[i]).PathFullName;
                //    r.push(data.instance.get_node(data.selected[i]).text);
                //}
                //$('#event_result').html('Selected: ' + r.join(', '));
            })

            $('#jstree1').jstree({
                'core': {
                    'check_callback': true,
                    'data': data,
                },
                'plugins': ['types', 'contextmenu', 'dnd'],
                "contextmenu": {
                    "items": function ($node) {
                        return {
                            "Download": {
                                "label": "Download",
                                'icon': 'fa fa-download',
                                "action": function (data) {
                                    var PathFullName = Encrypted($node.data.PathFullName);
                                    window.open(window.location.protocol + '//' + window.location.host + '/dropthings/download.ashx?path=' + PathFullName + '&IsFolder=' + $node.data.IsFolder, '_blank');
                                }
                            }
                            //,
                            //"Create": {
                            //    "label": "Create",
                            //    "action": function (data) {
                            //        var ref = $.jstree.reference(data.reference);
                            //        sel = ref.get_selected();
                            //        if (!sel.length) { return false; }
                            //        sel = sel[0];
                            //        sel = ref.create_node(sel, { "type": "file" });
                            //        if (sel) {
                            //            ref.edit(sel);
                            //        }
                            //    }
                            //},
                            //"Rename": {
                            //    "label": "Rename",
                            //    "action": function (data) {
                            //        var inst = $.jstree.reference(data.reference);
                            //        obj = inst.get_node(data.reference);
                            //        inst.edit(obj);
                            //    }
                            //},
                            //"Delete": {
                            //    "label": "Delete",
                            //    "action": function (data) {
                            //        var ref = $.jstree.reference(data.reference),
                            //            sel = ref.get_selected();
                            //        if (!sel.length) { return false; }
                            //        ref.delete_node(sel);
                            //    }
                            //}
                        };
                    }
                },
                'types': {
                    'default': {
                        'icon': 'fa fa-folder'
                    },
                    'html': {
                        'icon': 'fa fa-file-code-o'
                    },
                    'svg': {
                        'icon': 'fa fa-file-picture-o'
                    },
                    'css': {
                        'icon': 'fa fa-file-code-o'
                    },
                    'img': {
                        'icon': 'fa fa-file-image-o'
                    },
                    'js': {
                        'icon': 'fa fa-file-text-o'
                    }
                }
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
            Validator(false);
            alert("fail: " + errorThrown);
        }
    });
}

function Encrypted(keyValue) {
    var urlAction = urlBase + "/Encrypted";
    var result = "";
    var param = JSON.stringify({ value: keyValue });
    $.ajax({
        url: urlAction,
        async: false,
        data: param,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (item) {
            result = item.d;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $("#loading").dialog("close");
        }
    });
    return result;
};

function Render(item) {
    var result = {
        text: item.Name,
        data: item
    };
    if (item.IsFolder) {
        result.icon = 'fa fa-folder';
        if (item.Childs !== null || item.Childs.length !== 0) {
            result.children = new Array();
            item.Childs.forEach(
                function Formate(value) {
                    if (value.IsFolder) {
                        result.children.push(Render(value));
                    }
                }
            );
        }
    }// else {
    //    result.icon = 'fa fa-file-o';
    //}
    return result;
}

function Validator(state) {
    if (state === true) {
        // Populate controls
        PopulateEnableConnections();

        //Data factory
        $('#btnDataFactoryRun').attr('disabled', false);

        $($("#tabsEnable").find('#tabDataFactory')).show();

        $($("#tabsEnable").find('#tabFileManager')).show();
    } else {
        //Data factory
        $('#btnDataFactoryRun').attr('disabled', true);

        //Token Manager
        $('#btnTokenGenerate').attr('disabled', true);
    }
};

$(document).ready(function () {

    $('a[data-toggle="tab"]').on('shown.bs.tab', function () {
    });

    //General

    $("#loading").dialog({
        hide: 'slide',
        show: 'slide',
        autoOpen: false
    });


    //Data Factory
    $("#dataTableDataFactoryResult").bootstrapTable();

    $("#zoneMessageErrorDataFactory").hide();

    //File Manager
    // fileManager.SetHeight($(window).height());
    var path = getParameterByName('path');

    if (path === null) {
        path = '';
    }

    FolderAndFiles(path);

    //Validator
    var enable = false;
    var keyValue = getParameterByName('key');
    if (keyValue !== null) {
        var urlAction = urlBase + "/Validator";
        var param = JSON.stringify({ key: keyValue });
        $.ajax({
            url: urlAction,
            data: param,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                if (result.d) {
                    Validator(true);
                }
                else {
                    Validator(false);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                Validator(false);
                alert("fail: " + errorThrown);
            }
        });
    } else {
        Validator(false);
    }
});


$(function () {
    $($("#tabsEnable").find('#tabDataFactory')).hide();
    $($("#tabsEnable").find('#tabFileManager')).hide();
})