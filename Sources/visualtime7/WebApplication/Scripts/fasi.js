var $loading;

var ProxyAsyncRoleInCase = "";
var ProxySyncRoleInCase = "";
var ProxyAsyncLookUps = "";
var ProxySyncLookUps = "";
var ProxyAsyncRestrictions = "";
var ProxySyncRestrictions = "";
var ProxyAsyncRequirement = "";
var ProxySyncRequirement = "";
var ProxyAsyncUnderwritingCase = "";
var ProxySyncUnderwritingCase = "";
var ProxyAsyncUnderwritingRule = "";
var ProxySyncUnderwritingRule = "";
var ProxyAsyncTabUnderwritingRule = "";
var ProxySyncTabUnderwritingRule = "";
var ProxyAsyncUnderwritingPanel = "";
var ProxySyncUnderwritingPanel = "";
var ProxyAsyncHistory = "";
var ProxySyncHistory = "";
var ProxyAsyncDecision = "";
var ProxySyncDecision = "";
var ProxyAsyncRulesInCase = "";
var ProxySyncRulesInCase = "";
var ProxyAsyncAttachmentsInCase = "";
var ProxySyncAttachmentsInCase = "";
var GeneralGridOptions;
var GeneralAddSettings;
var GeneralDelSettings;
var ChildTab;
var ChildTabViewPolicy;
var isChildTabOpened = false;
var isChildTabViewPolicyOpened = false;
var timer = setInterval(checkChildReloaded, 1000);
var isReloaded = false;
var ua = window.navigator.userAgent;
var msie = ua.indexOf("MSIE ");

function checkChildOpened() {

    if (isChildTabOpened &&
            ChildTab.closed) {
        isChildTabOpened = false;
        localStorage.removeItem("editingPolicy");
        if (isExplorer) {
            isEditMode = GetEditModeStatus();
            ReloadCaseInformationTab(isEditMode);
            ReloadHistoryPremiumGrid(isEditMode);
            $loading.hide();
        }
        else
            window.location.reload(true);
    }
}

function checkChildReloaded() {

    if (isChildTabViewPolicyOpened && ChildTabViewPolicy.closed) {
        isChildTabViewPolicyOpened = false;
    }

    else if (isChildTabViewPolicyOpened && ChildTabViewPolicy.isReloaded) {
        isChildTabViewPolicyOpened = false;
        ChildTabViewPolicy.close();
    }

    if (isChildTabOpened && ChildTab.closed) {
        isChildTabOpened = false;
        if (isExplorer) {
            isEditMode = GetEditModeStatus();
            ReloadCaseInformationTab(isEditMode);
            ReloadHistoryPremiumGrid(isEditMode);
            ReloadGeneralInformationGrid(isEditMode);
            $loading.hide();
        }
        else
            window.location.reload(true);
    }
    else if (isChildTabOpened && ChildTab.isReloaded) {
        isChildTabOpened = false;
        ChildTab.close();
        if (isExplorer) {
            isEditMode = GetEditModeStatus();
            ReloadCaseInformationTab(isEditMode);
            ReloadHistoryPremiumGrid(isEditMode);
            ReloadGeneralInformationGrid(isEditMode);
            $loading.hide();
        }
        else
            window.location.reload(true);
    }
}

ProxyAsyncRoleInCase = new serviceProxy("services/RoleInCase.aspx/", true);
ProxySyncRoleInCase = new serviceProxy("services/RoleInCase.aspx/", false);
ProxyAsyncLookUps = new serviceProxy("services/LookUps.aspx/", true);
ProxySyncLookUps = new serviceProxy("services/LookUps.aspx/", false);
ProxyAsyncRestrictions = new serviceProxy("services/Restriction.aspx/", true);
ProxySyncRestrictions = new serviceProxy("services/Restriction.aspx/", false);
ProxyAsyncUnderwritingCase = new serviceProxy("services/UnderwritingCase.aspx/", true);
ProxySyncUnderwritingCase = new serviceProxy("services/UnderwritingCase.aspx/", false);
ProxyAsyncUnderwritingRule = new serviceProxy("services/UnderwritingRule.aspx/", true);
ProxySyncUnderwritingRule = new serviceProxy("services/UnderwritingRule.aspx/", false);
ProxyAsyncTabUnderwritingRules = new serviceProxy("services/TabUnderwritingRules.aspx/", true);
ProxySyncTabUnderwritingRules = new serviceProxy("services/TabUnderwritingRules.aspx/", false);
ProxyAsyncUnderwritingPanel = new serviceProxy("UnderwritingPanel.aspx/", true);
ProxySyncUnderwritingPanel = new serviceProxy("UnderwritingPanel.aspx/", false);
ProxyAsyncRequirement = new serviceProxy("services/Requirement.aspx/", true);
ProxySyncRequirement = new serviceProxy("services/Requirement.aspx/", false);
ProxyAsyncHistory = new serviceProxy("services/CaseHistory.aspx/", true);
ProxySyncHistory = new serviceProxy("services/CaseHistory.aspx/", false);
ProxyAsyncDecision = new serviceProxy("services/Decision.aspx/", true);
ProxySyncDecision = new serviceProxy("services/Decision.aspx/", false);
ProxyAsyncAttachmentsInCase = new serviceProxy("services/Attachment.aspx/", true);
ProxySyncAttachmentsInCase = new serviceProxy("services/Attachment.aspx/", false);
ProxyAsyncPolicyHistory = new serviceProxy("services/PolicyHistory.aspx/", true);
ProxySynPolicyHistory = new serviceProxy("services/PolicyHistory.aspx/", false);

GeneralGridOptions = {
    loadonce: true,
    sortable: false,
    gridview: true,
    rownumbers: false,
    autoencode: false,
    pgbuttons: true,
    pgtext: null,
    gridview: true,
    hidegrid: false,
    autowidth: true,
    reloadAfterSubmit: false,
    sortname: 'id',
    sortorder: "desc",
    height: "auto !important",
    width: null,
    shrinkToFit: false,
    multiselect: false,
    editurl: "",
    mtype: "POST",
    datatype: 'json',
    postData: {},
    guiStyle: "bootstrap",
    iconSet: "glyph",
    ajaxGridOptions: { contentType: "application/json", timeout: 30000 },
    pgbuttons: false,
    navOptions: {
        reloadGridOptions: { fromServer: true }
    },
    serializeGridData: function (data) {
        return {};
        data = {};
    },
    jsonReader: {
        repeatitems: false,
        root: function (obj) { return obj.d; }
    },
    loadError: function (jqXHR, textStatus, errorThrown) {
        if (textStatus === "timeout") {
            alert("Ha ocurrido un error con el servicio, por favor, contacte a su administrador");
        }

        if (jqXHR.status === 401) {
            popupExpired.Show();
        }
    },
    loadComplete: function (data) {
        if ($(this).getGridParam("reccount") == 0) {
            if ($.trim($("#dpeCaseId_I").val()).length > 0)
                $("#textNoDataAvailableDiv").show();
            //$(this).parents(".ui-jqgrid").hide();
        }
        else {
            $("#textNoDataAvailableDiv").hide();
            $(this).parents(".ui-jqgrid").show();
        }
        $loading.hide();
    }



}

GeneralAddSettings = {
    width: 500,
    jqModal: false,
    recreateForm: true,
    reloadAfterSubmit: true,
    savekey: [true, 13],
    closeOnEscape: true,
    closeAfterAdd: true,
    checkOnSubmit: true,
    resize: false,
    serializeEditData: function (postData) {
        return JSON.stringify(postData);
    },
    afterSubmit: function (response, postdata) {
        $(this).jqGrid("setGridParam", { datatype: 'json' }).trigger("reloadGrid");
        return [true, "", ""]
    },
    afterShowForm: function (form) {
        var modal = form.parents(".ui-jqdialog");
        var grilla = form.parents(".ui-jqgrid");

        modal.css("position", "fixed");
        modal.css("top", 70);
        modal.css("left", (window.innerWidth - modal.width()) / 2);
    },
    ajaxEditOptions: { contentType: "application/json; charset=utf-8" }
}

GeneralDelSettings = {
    width: 500,
    recreateForm: true,
    savekey: [true, 13],
    closeAfterEdit: true,
    closeOnEscape: true,
    reloadAfterSubmit: true,
    resize: false,
    ajaxDelOptions: { contentType: "application/json; charset=utf-8" },
    afterSubmit: function (response, postdata) {
        $loading.hide();
        return [true, "", ""]
    },
    afterShowForm: function (form) {
        var modal = form.parents(".ui-jqdialog");
        var grilla = form.parents(".ui-jqgrid");

        modal.css("position", "fixed");
        modal.css("width", "400px");
        modal.css("top", 70);
        modal.css("left", (window.innerWidth - modal.width()) / 2);
    },
}

jQuery.ajaxSetup({
    beforeSend: function (xhr) {
        try {
            $loading.show();
        }
        catch (ex)
        {
            $loading = $(".loadingDiv"); 
            $loading.show();
        }
        //xhr.setRequestHeader("Authorization", "Bearer " + token);
    },
    
    complete: function () {
        try {
            $loading.hide();
        }
          catch (ex) {
            $loading = $(".loadingDiv");
            $loading.hide();
        }
    },
    error: function () {
        try {
            $loading.hide();
        }
        catch (ex) {
            $loading = $(".loadingDiv");
            $loading.hide();
        }
    }
});

// Manages all requests to the server
function serviceProxy(serviceUrl, isAsync) {
    var _I = this;
    this.serviceUrl = serviceUrl;
    this.isAsync = isAsync;

    // *** Call a wrapped object
    this.invoke = function (method, data, callback) {
        // *** The service endpoint URL        
        var url = _I.serviceUrl + method;

        return $.ajax({
            url: url,
            data: data,
            async: isAsync,
            type: "POST",
            contentType: "application/json",
            dataType: "json",
            success: callback,
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(jqXHR);
                if (jqXHR.status === 401) {
                    popupExpired.Show();
                }
                $(".alert.alert-danger").show();
            },
            complete: function () {
                hideAlerts();
                $loading.hide();
            }
        });
    }
}

// Used to load the default values on the ddl
function LoadDefaultValues(ddlName, webMethod, proxy) {
    return proxy.invoke(webMethod, "",
		function (result) {
		    var selectedOptions = $("select#" + ddlName);
		    $.each(result.d, function () {
		        selectedOptions.append($("<option />").val(this.Code).text(this.Description));
		    });
		}
	);
}

function LoadDefaultValuesWithParam(ddlName, webMethod, proxy, param) {
	return proxy.invoke(webMethod, param,
		function (result) {
			var selectedOptions = $("select#" + ddlName);
			$.each(result.d, function () {
				selectedOptions.append($("<option />").val(this.Code).text(this.Description));
			});
		}
	);
}

// Centralized function that syncs the selected role (shows up a modal)
function syncRegisterDialog(parameters, operationContract, syncProxy, successAlert, addModal) {
    var panel = $("#panel-confirmation-modal");
    panel.modal('show');
    panel.find("#save-changes-confirmation").click(function () {
        $(this).unbind();
        panel.modal('hide');

        syncProxy.invoke(operationContract, parameters,
            function (result) {
                LoadControl($(".tab-pane.active").attr("id"));
                successAlert.show();
            }
        );
    });
}

// Loads the time zone according to the local time, for example,
// for Santiago or Buenos Aires, it returns -03:00
function getLocalTZ(nameTagHTML) {
    var localTZ = new Date().toString().match(/([-\+][0-9]+)\s/)[1];
    return localTZ.slice(0, 3) + ":" + localTZ.slice(3);
}

// Finds duplicates in the parameter array
function findDuplicatesInArray(myArray, message) {
    var isArrayValid = true;
    var map = [];

    // objRiskZones
    myArray.each(function () {
        map.push($(this).val());
    });

    var uniq = map.reduce(function (a, b) {
        if (a.indexOf(b) < 0) {
            a.push(b);
        } else {
            isArrayValid = false;
        }
        return a;
    }, []);

    if (!isArrayValid) {
        alert(message);
        console.log("Duplicates found in array: " + message);
        // shows the unique values
        //console.log(uniq);
    }

    return isArrayValid;
}

// Transform the date to a format that WCF understands
function dateToWcf(input, isJsDate) {
    var dt;

    // It's not a javascript date
    if (!isJsDate)
        dt = new Date(formattedDate(input));
    else {
        dt = new Date(input);
        if (isNaN(dt)) return "";
    }
    // The date has to be sent to the server in UTC timezone to avoid different hours
    var dt1 = Date.UTC(dt.getFullYear(), dt.getMonth(), dt.getDate(), 0, 0, 0, 0);

    return '\/Date(' + dt1 + ')\/';
}

// Transform the JsonResult date to a valid date
function jsonToDate(jd) {
    var re = /-?\d+/;
    var a = re.exec(jd);
    var b = new Date(parseInt(a[0]));
    var c = b.getDate();
    var d = b.getMonth() + 1;
    var e = b.getFullYear();
    if (c < 10) {
        c = '0' + c;
    }
    if (d < 10) {
        d = '0' + d;
    }
    return c + '/' + d + '/' + e;
    //value = new Date(parseInt(value.replace("/Date(", "").replace(")/",""), 10));
}

// Sets the date to a js and json to wcf format (mm/dd/yy)
function formattedDate(inputDate) {
    inputDate = $.datepicker.parseDate("dd/mm/yy", inputDate);
    return convertDate(inputDate);
}

// Sets the date to a js and json to wcf format (mm/dd/yy)
function formattedDateValue(dateValue) {
    return convertDate(dateValue);
}

//Convert Date format(mm/dd/yy)
function convertDate(inputDate) {
    var d = new Date(inputDate || Date.now()),
    month = '' + (d.getMonth() + 1),
    day = '' + d.getDate(),
    year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [month, day, year].join('/');
}

// Hides all alerts on the page
function hideAlerts() {
    window.setTimeout(function () {
        $(".alert").fadeOut('slow');
    }, 3000);
}

//Get all lookup values from a given type
function GetLookUps(methodName) {
    var rolesTypes = "";
    $.ajax({
        url: "services/LookUps.aspx/" + methodName,
        contentType: "application/json; charset=utf-8",
        type: "POST",
        datatype: "application/json",
        async: false,
        success: function (response) {
            $.each(response.d, function (index, item) {
                rolesTypes = rolesTypes + item.Code + ":" + item.Description + ";";
            });
        }
    });
    return rolesTypes.slice(0, -1);
}

function GetLookUpsWithParam(methodName, param) {
	var rolesTypes = "";
	$.ajax({
		url: "services/LookUps.aspx/" + methodName,
		contentType: "application/json; charset=utf-8",
		type: "POST",
		datatype: "application/json",
		data: param,
		async: false,
		success: function (response) {
			$.each(response.d, function (index, item) {
				rolesTypes = rolesTypes + item.Code + ":" + item.Description + ";";
			});
		}
	});
	return rolesTypes.slice(0, -1);
}

function GetLookUpsWithInitValue(methodName) {
    var rolesTypes = "-1:;";
    $.ajax({
        url: "services/LookUps.aspx/" + methodName,
        contentType: "application/json; charset=utf-8",
        type: "POST",
        datatype: "application/json",
        async: false,
        success: function (response) {
            $.each(response.d, function (index, item) {
                rolesTypes = rolesTypes + item.Code + ":" + item.Description + ";";
            });
        }
    });
    return rolesTypes.slice(0, -1);
}

// Obtiene el URL para editar la póliza
function GetEditPolicyUrl(caseId) {
    var postData = {
        caseId: caseId
    }

    ProxySyncUnderwritingPanel.invoke("GetEditPolicyUrl", JSON.stringify(postData),
        function (result) {
            if (result.d != "") {
                isChildTabOpened = true;
                ChildTab = window.open(result.d, '_blank');
            }
            else {
                alert("Link no disponible, recargue la página o verifique si la sesión ha caducado");
            }
        });
    $loading.show();
}

// Obtiene el URL para ver la póliza de acuerdo a la versión
function GetViewPolicyUrl(version) {
    nbEnableHtml5Setting = false;
    ProxySyncUnderwritingPanel.invoke("GetViewPolicyUrl", "",
        function (result) {
            if (result.d != "") {
                isChildTabViewPolicyOpened = true;
                ProxySyncUnderwritingPanel.invoke("IsEnableHtml5", "",
                    function (result) {
                        nbEnableHtml5Setting = result.d;
                    });
                if (nbEnableHtml5Setting)
                {
                    console.log("Ingrese aqui 1");
                    isChildTabViewPolicyOpened = true;
                    ChildTabViewPolicy = window.open(result.d + '&uwRelease=' + version, '_blank');
                }
                else
                {
                    console.log("Ingrese aqui")
                    var p = result.d.split('?');
                    var action = p[0];
                    var params = p[1].split('&');
                    ChildTabViewPolicy = $(document.createElement('form')).attr('action', action).attr('method', 'post').attr("target", "_blank");
                    $('body').append(ChildTabViewPolicy);
                    for (var i in params) {
                        bufferString = params[i];
                        var tmp = bufferString.toString().split('=');
                        var key = tmp[0], value = tmp[1];
                        $(document.createElement('input')).attr('type', 'hidden').attr('name', key).attr('value', value).appendTo(ChildTabViewPolicy);
                    }
                    $(document.createElement('input')).attr('type', 'hidden').attr('name', "uwRelease").attr('value', version).appendTo(ChildTabViewPolicy);

                    ChildTabViewPolicy.submit();
                }
            }
            else {
                alert("Información no disponible (formulario inexistente)");
            }
        });
}

function GetViewPolicyUrlByUWCaseID(uwCaseID,version) {
    var dataJson = JSON.stringify({ uwCaseID: uwCaseID })
    nbEnableHtml5Setting = false;
    ProxySyncUnderwritingPanel.invoke("GetViewPolicyUrlByUWCaseID", dataJson,
        function (result) {
            if (result.d != "") {
                isChildTabViewPolicyOpened = true;
                ProxySyncUnderwritingPanel.invoke("IsEnableHtml5", "",
                    function (result) {
                        nbEnableHtml5Setting = result.d;
                    });
                if (nbEnableHtml5Setting) {
                    console.log("Ingrese aqui 1");
                    isChildTabViewPolicyOpened = true;
                    ChildTabViewPolicy = window.open(result.d + '&uwRelease=' + version, '_blank');
                }
                else {
                    var p = result.d.split('?');
                    var action = p[0];
                    var params = p[1].split('&');
                    ChildTabViewPolicy = $(document.createElement('form')).attr('action', action).attr('method', 'post').attr("target", "_blank");
                    $('body').append(ChildTabViewPolicy);
                    for (var i in params) {
                        bufferString = params[i];
                        var tmp = bufferString.toString().split('=');
                        var key = tmp[0], value = tmp[1];
                        $(document.createElement('input')).attr('type', 'hidden').attr('name', key).attr('value', value).appendTo(ChildTabViewPolicy);
                    }
                    $(document.createElement('input')).attr('type', 'hidden').attr('name', "uwRelease").attr('value', version).appendTo(ChildTabViewPolicy);

                    ChildTabViewPolicy.submit();
                }
            }
            else {
                alert("Información no disponible (formulario inexistente)");
            }
        });
}


// Obtiene el URL de la información general 
function GetHostAndFormUrl() {
    var url;
    ProxySyncUnderwritingPanel.invoke("GetHostAndFormUrl", "",
        function (result) {
            url = result.d;
        });
    return url;
}


//Convert an input to calendar
function ConvertToCalendar(element) {
    $(element).datepicker({
        showOn: "button",
        buttonImage: "../images/16x16/General/Calendar.png",
        buttonImageOnly: true,
        buttonText: "",
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd/mm/yy",
        yearRange: "1920:" + (new Date().getFullYear() + 10),
        //dayNamesMin: $.datepicker._defaults.dayNamesShort,  // Name of days with 3 letters
        firstDay: 0, // Starts with Sunday
    });
    $(element).width(130).css('display', 'inline-block');

}

function ExtractDate(dateWihtHours) {
    //if (dateWihtHours != null && dateWihtHours != "") {
    //    var matches = dateWihtHours.match(/^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4} (\d{2}):(\d{2}) (AM|PM)$/) //TODO este match no llega cuando el usuario modifica la fecha en el formulario 
    //    if (matches != null) {
    //        day = dateWihtHours.substring(0, 2);
    //        month = dateWihtHours.substring(3, 5);
    //        year = dateWihtHours.substring(8, 10);
    //        return month + "/" + day + "/" + year;
    //    }        
    //}    
}

//Decimal mask
function DecimalMask(elem) {
    $(elem).numeric(",");
    $(elem).css("text-align", "right");
}

function LoadControl(activeTab) {
    var inEditMode = GetEditModeStatus();
    switch (activeTab) {
        case "CaseInformationTab":
            ReloadCaseInformationTab(inEditMode);
            break;
        case "GeneralInformationTab":
            ReloadGeneralInformationGrid(inEditMode);
            break;
        case "RequirementsTab":
            ReloadRequirementGrid(inEditMode);
            break;
        case "DecisionTab":
            ReloadDecicionGrid(inEditMode);
            break;
        case "HistoryTab":
            ReloadHistoryGrid(inEditMode);
            break;
        case "RestrictionsTab":
            ReloadRestrictionsGrid(inEditMode);
            break;
        case "PolicyHistoryTab":
            ReloadHistoryPremiumGrid(inEditMode);
            break;
        case "CaseAttachmentsTab":
            $(".loadingDiv").hide();
    		loadAttachment();
    		break;
        case "NotesTab":
            $(".loadingDiv").hide();
    		loadNotes();
			break;
        default:
            $("#textNoDataAvailableDiv").hide();
            break;
    }
}

function FormValidator(formObject) {
    var isValid = false;

    formObject.validate({
        ignoreTitle: true,
        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
        },
        errorElement: 'span',
        errorClass: 'help-block',
        errorPlacement: function (error, element) {
            if (element.parent('.input-group').length) {
                error.insertAfter(element.parent());
            } else {
                error.insertAfter(element);
            }
        }
    });

    isValid = formObject.valid();
    return isValid;
}

// Converts an array to a CSV format string.
function arrayToCSV(arr) {
    var columnNames = [];
    var rows = [];
    for (var i = 0, len = arr.length; i < len; i++) {
        // Each obj represents a row in the table
        var obj = arr[i];
        // row will collect data from obj
        var row = [];
        for (var key in obj) {
            // Don't iterate through prototype stuff
            if (!obj.hasOwnProperty(key)) continue;
            // Collect the column names only once
            if (i === 0) columnNames.push(prepareValueForCSV(key));
            // Collect the data
            row.push(prepareValueForCSV(obj[key]));
        }
        // Push each row to the main collection as csv string
        rows.push(row.join(','));
    }
    // Put the columnNames at the beginning of all the rows
    rows.unshift(columnNames.join(','));
    // Return the csv string
    return rows.join('\n');
}

// This function allows us to have commas, line breaks, and double 
// quotes in our value without breaking CSV format.
function prepareValueForCSV(val) {
    val = '' + val;
    // Escape quotes to avoid ending the value prematurely.
    val = val.replace(/"/g, '""');
    return '"' + val + '"';
}

function showLoader() {
    $loading.show();
}

$(function () {
    $loading = $('.loadingDiv');
    $loading.hide();

    $('a[role="tab"]').bind('click', function (e) {
        $loading.show();
        LoadControl($(this).attr("aria-controls"));
    });
});

function validateNumber(e, point) {
    var key = window.Event ? e.which : e.keyCode
    if (point)
        return ((key >= 48 && key <= 57) || key == 46)
    else
        return (key >= 48 && key <= 57)
}

function isExplorer() {
    return (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))  // If Internet Explorer
}

var reload = function () {
    var regex = new RegExp("([?;&])reload[^&;]*[;&]?");
    var query = window.location.href.split('#')[0].replace(regex, "$1").replace(/&$/, '');
    window.location.href =
        (window.location.href.indexOf('?') < 0 ? "?" : query + (query.slice(-1) != "?" ? "&" : ""))
        + "reload=" + new Date().getTime() + window.location.hash;
};

function LookUpsByObjectWithInitValue(data) {
    var rolesTypes = "-1:;";
    $.each(data, function (index, item) {
        rolesTypes = rolesTypes + item.Code + ":" + item.Description + ";";
    });
    return rolesTypes.slice(0, -1);
}

function isValidDate(dateString) {
    // First check for the pattern
    if (!/^\d{1,2}\/\d{1,2}\/\d{4}$/.test(dateString))
        return false;

    // Parse the date parts to integers
    var parts = dateString.split("/");
    var day = parseInt(parts[1], 10);
    var month = parseInt(parts[0], 10);
    var year = parseInt(parts[2], 10);

    // Check the ranges of month and year
    if (year < 1000 || year > 3000 || month == 0 || month > 12)
        return false;

    var monthLength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

    // Adjust for leap years
    if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
        monthLength[1] = 29;

    // Check the range of the day
    return day > 0 && day <= monthLength[month - 1];
}

function ValidateDateField(value, colName) {
    var lang = "";
    for (i in $.jgrid.locales) { lang = i; break; };
    var res = [false, colName + ": " + $.jgrid.locales[lang].formatter.date.customvalidationdate.msginvaliddate];

    try {
        if (isValidDate(formattedDate(value))) {
            res[0] = true;
            res[1] = colName + ": " + $.jgrid.locales[lang].formatter.date.customvalidationdate.msgvaliddate;
        }
    } catch (e) {
        //In case of exception do nothing and it will be returned variable res default values
    }
    return res;
}


function dragElement(elmnt) {
    var pos1 = 0, pos2 = 0, pos3 = 0, pos4 = 0;
    if (document.getElementById(elmnt.id + "header")) {
        /* if present, the header is where you move the DIV from:*/
        document.getElementById(elmnt.id + "header").onmousedown = dragMouseDown;
    } else {
        /* otherwise, move the DIV from anywhere inside the DIV:*/
        //elmnt.onmousedown = dragMouseDown;
    }

    function dragMouseDown(e) {
        e = e || window.event;
        // get the mouse cursor position at startup:
        pos3 = e.clientX;
        pos4 = e.clientY;
        document.onmouseup = closeDragElement;
        // call a function whenever the cursor moves:
        document.onmousemove = elementDrag;
    }

    function elementDrag(e) {
        e = e || window.event;
        // calculate the new cursor position:
        pos1 = pos3 - e.clientX;
        pos2 = pos4 - e.clientY;
        pos3 = e.clientX;
        pos4 = e.clientY;
        // set the element's new position:
        elmnt.style.top = (elmnt.offsetTop - pos2) + "px";
        elmnt.style.left = (elmnt.offsetLeft - pos1) + "px";
    }

    function closeDragElement() {
        /* stop moving when mouse button is released:*/
        document.onmouseup = null;
        document.onmousemove = null;
    }
}

function loadNotes() {
	$("#case-notes-iframe").attr("src", "controls/partials/_notes.aspx?caseId=" +$('#dpeCaseId_I').val());
}