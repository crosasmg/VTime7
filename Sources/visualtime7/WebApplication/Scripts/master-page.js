function getParameterByName(name, url) {
    name = name.toLowerCase();
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    url = url.toLowerCase();
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

function showPreview(docid) {
    window.open('ViewTemplate.aspx?id=' + docid, 't', 'scrollbars=yes,menubar=no,height=600,width=730,resizable=yes,toolbar=no,location=no,status=no');
}

function showWorkflow(docid, actid) {
    window.open('/docmanager/Admin/ViewWorkflow.aspx?id=' + docid + '&ac=' + actid, 'w', 'scrollbars=yes,menubar=no,height=600,width=710,resizable=yes,toolbar=no,location=no,status=no');
}

function showpopup(page) {
    window.open(page, 'p', 'scrollbars=yes,menubar=no,height=600,width=500,resizable=no,toolbar=no,location=no,status=no');
}

function changeStatusGO(statusActive) {
    if (statusActive === true) {
        $("#btnGO").removeAttr('disabled');
        $("#btnGO").button("refresh");
    } else {
        $("#btnGO").prop("disabled", true);
        $("#btnGO").button("refresh");
    }
}

$(function () {
    $("#btnGO").button({
        icons: {
            primary: "ui-icon-navigation"
        },
        label: $("#hfContentBtnGo").val()
    });

    var isEmployee = $("#hfIsEmployee").val();
    if (isEmployee === "1") {
        $('#txtSearchTransacction').show();
        $('#btnGO').show();
        $("#btnGO").button("refresh");
    }
    else {
        $('#txtSearchTransacction').hide();
        $('#btnGO').hide();
    }
    changeStatusGO(false);

    var watermark = $("#hfContentWatermark").val();

    $('#txtSearchTransacction').val(watermark).addClass('watermark');

    $('#txtSearchTransacction').blur(function () {
        if ($(this).val().length === 0) {
            $(this).val(watermark).addClass('watermark');
        }
    });

    //if focus and text is watermrk, set it to empty and remove the watermark class
    $('#txtSearchTransacction').focus(function () {
        if ($(this).val() === watermark) {
            $(this).val('').removeClass('watermark');
        }
    });

    $("#btnGO").click(function () {
        var ddd = $("#hfTransactionId").val();
        var param = { windowLogicalCode: $("#hfTransactionId").val() };
        var urlBase = window.location.protocol + '//' + window.location.host + '/dropthings/default.aspx/GetUrlTransaction';
        $.ajax({
            url: urlBase,
            data: JSON.stringify(param),
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataFilter: function (data) { return data; },
            success: function (data) {
                if (data.d.IsAllowed.length !== 0) {
                    alert(data.d.IsAllowed);
                }
                else {
                    insGoToInternal(data.d.URL);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(textStatus);
            }
        });
    });

    $("#txtSearchTransacction").autocomplete({
        source: function (request, response) {
            var param = { prefix: $('#txtSearchTransacction').val() };
            var urlBase = window.location.protocol + '//' + window.location.host + '/dropthings/default.aspx/GetTransaction';
            changeStatusGO(false);
            $.ajax({
                url: urlBase,
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    if (data.d.length > 0) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item, id: $.trim(item.split('-')[0])
                            }
                        }))
                    } else {
                        changeStatusGO(false);
                        response([{ label: 'No results found.', id: -1 }]);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    changeStatusGO(false);
                    alert(textStatus);
                }
            });
        },
        select: function (event, ui) {
            // prevent autocomplete from updating the textbox
            event.preventDefault();
            $(this).val(ui.item.label);
            $("#hfTransactionId").val(ui.item.id);
            if (ui.item.id === -1) {
                changeStatusGO(false);
            } else {
                changeStatusGO(true);
            }
        },
        minLength: 2    //minLength as 2, it means when ever user enter 2 character in TextBox the AutoComplete method will fire and get its source data.
    });
});

function showUsersEditor() {
    var hdnListID = 'ctl00_ContentPlaceholder1_WidgetPanelsLayout_WidgetContainer10056_Widget10056_hdnUserList';
    var hdnListCodes = 'ctl00_ContentPlaceholder1_WidgetPanelsLayout_WidgetContainer10056_Widget10056_hdnUserCodes';

    var str1 = document.getElementById(hdnListID).value;
    var str2 = document.getElementById(hdnListCodes).value;

    var Argumentos = new Array(str1, str2);

    var ConfiguracionPagina = 'center:yes;resizable:no;help:no;status:no;dialogWidth:550px;dialogHeight:400px';
    var Pagina = '\\dropthings\\widgets\\UsersEditor.aspx?list=' + str1;

    Argumentos = window.showModalDialog(Pagina, Argumentos, ConfiguracionPagina);

    if (Argumentos !== null) {
        document.getElementById(hdnListID).value = Argumentos[0];
        document.getElementById(hdnListCodes).value = Argumentos[1];
    }
}

function insGoToInternal(RefUrl) {
    var lstrURL = RefUrl.substr(RefUrl.indexOf('sCodispl=') + 9);
    var lintLength = lstrURL.indexOf('&');
    var lstrCodispl = lstrURL.substr(0, lintLength);
    var win = open(RefUrl, 'Transaccion' + lstrCodispl.replace('-', '_'), 'toolbar=no,resizable=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=750,height=450,left=20,top=20');
    win.moveTo(0, 0);
    win.resizeTo(window.screen.availWidth, window.screen.availHeight);
}

function insGoToDesignerForm(RefUrl) {
    var win = location(RefUrl);
}

var showPopup = true;
var iframe;

function OnPopupInit(s, e) {
    iframe = LogInPopupControl.GetContentIFrame();

    /* the "load" event is fired when the content has been already loaded */
    ASPxClientUtils.AttachEventToElement(iframe, 'load', OnContentLoaded);
}

function OnPopupShown(s, e) {
    if (showPopup)
        loadingPanel.ShowInElement(iframe);
}

function OnContentLoaded(e) {
    showPopup = false;
    loadingPanel.Hide();
}

function ShowPopupControl(s, e) {
    var urlBase = window.location.protocol + '//' + window.location.host + '/dropthings/default.aspx/Configurations';
    $.ajax({
        url: urlBase,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d['Security.Mode'] === 'ActiveDirectory') {
                ForgotPasswordHyperLink.SetVisible(false);
                RegisterHyperLink.SetVisible(false);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
    showPopup = true;
    LogInPopupControl.SetContentUrl('/Authentication/UserLogIn.aspx');  
    //LogInPopupControl.SetSize(370, 330);
    LogInPopupControl.Show();
}

function LogInPopupControlSetSize(width, high) {
    LogInPopupControl.SetSize(width, high);
}

function HidePopupControl(reload, url, time) {
    loadingPanel.Show();
    LogInPopupControl.Hide();
    window.location.href = url;
}

function ShowPopUpTime() {
    LogInPopupControl.Hide();
    window.location.reload();
}

function HideRegisterOption() {
    RegisterHyperLink.SetText("");
}

function ResizePopupWithCaptcha() {
    var height = LogInPopupControl.GetHeight();
    var width = LogInPopupControl.GetWidth();
    LogInPopupControl.SetSize(width + 100, height + 300);
}

function ResizePopupNormal() {
    var height = LogInPopupControl.GetHeight();
    var width = LogInPopupControl.GetWidth();
    LogInPopupControl.SetSize(width - 100, height - 300);
}

function ShowLoadingPanel(isShow) {
    if (isShow === true) {
        loadingPanel.Show();
    }
    else {
        loadingPanel.Hide();
    }
}

function ShowLogInPopup(isShow) {
    
    if (isShow === true) {
        LogInPopupControl.Show();
    }
    else {
        LogInPopupControl.Hide();
    }
}


var AppInfoValue;

function AppInfo() {
    var urlBase = window.location.protocol + '//' + window.location.host + '/dropthings/default.aspx/AppInfo';
    $.ajax({
        url: urlBase,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d != null) {
                var url = window.location.protocol + '//' + window.location.host + '/dropthings/download.ashx?path=' + data.d + '&IsFolder=False';
                window.open(url, '_blank');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
}

$(function () {
    AppInfoValue = getParameterByName("AppInfo");
   
    if (AppInfoValue != null) {
        AppInfo()
    }

    //$("#widget_area_wrapper").find("table").first();

    $(".mainColumn").each(function (index, value) {
        var columnStyle = $(value).attr("style");
        //console.log(columnStyle.indexOf("visibility: hidden"));
        if (columnStyle.indexOf("visibility: hidden") > -1) {
            $(value).css("display", "none");
        } else {
            $(value).css("display", "table-cell");
        }
    });
});