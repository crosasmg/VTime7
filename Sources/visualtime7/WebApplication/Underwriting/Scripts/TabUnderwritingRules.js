

/* ************************** Variables auxiliares para alarmas, exclusiones y restricciones ******************************* */
var isErrorAlarm = false;
var ddlBranchSelected = 0;
var ddlBranchAux = 0;
var validateExclusion = false;
var validateDescoexprem = false;
var validateMaxInsuredSum = false;
var alarmTypeBuffer = 0;
var reloadGridAlarm = false;
var illnessarray = [];
var roleCodesAccepted = [];
var illness = [];
var selectedRule = { UnderwritingRuleId: "", EffectiveDate: "" };
var numLocate = 1000.1;
var emptyDate= '01/01/0001';
var isProductLife = false;
/* **************************************** VARIABLES PARA ALARMAS ************************ */

var ProductList = {
    ProductList: "",
    findByProductCode: function (pcode, nBranch) {
        var i = 0;
        var found = false;
        for (i = 0; i < $(this.ProductList).length; i++)
            if ($(this.ProductList)[i].ProductCode == pcode && $(this.ProductList)[i].LineOfBusiness == nBranch) {
                found = true;
                break;
            }
        if (!found) return { ProductCode: "-1", ProductDescription: "" };
        return $(this.ProductList)[i];
    },
    arrayToJqgrid: function () {
        var i = 0;
        var ret = "";
        for (i = 0; i < $(ProductList.ProductList).length; i++)
            if ($(ProductList.ProductList)[i].LineOfBusiness == $("#ddlBranch").val())
                ret += $(ProductList.ProductList)[i].ProductCode + ":" + $(ProductList.ProductList)[i].ProductDescription + ";";

        ret = "-1:;" + ret;
        return ret.slice(0, -1);
    }
};
var AlarmTypeList = {
    AlarmTypeList: "",
    findByAlarmCode: function (pcode) {
        var i = 0;
        for (i = 0; i < $(this.AlarmTypeList).length; i++)
            if ($(this.AlarmTypeList)[i].Code == pcode)
                break;
        return $(this.AlarmTypeList)[i].Description;
    },

};


var stageList = {
    arr: "",
    FindByCode: FindByCode
};

var StatusList = {
    StatusList: "",
    findByStatusCode: function (pcode) {
        var i = 0;
        var found = false;
        for (i = 0; i < $(this.StatusList).length; i++)
            if ($(this.StatusList)[i].Code == pcode) {
                found = true;
                break;
            }
        if (!found) return {
            Code: "-1", Description: ""
        };
        return $(this.StatusList)[i];
    }
};

var DecisionList = {
    DecisionList: "",
    findByStageCode: function (pcode) {
        var i = 0;
        var found = false;
        for (i = 0; i < $(this.DecisionList).length; i++)
            if ($(this.DecisionList)[i].Code == pcode) {
                found = true;
                break;
            }
        if (!found) return {
            Code: "-1", Description: ""
        };
        return $(this.DecisionList)[i];
    }
};

/* ******************************** VARIABLES PARA EXCLUSIONES ***************************** */

var ExclusionTypeList = {
    ExclusionTypeList: "",
    findByExclusionCode: function (pcode) {
        var i = 0;
        for (i = 0; i < $(this.ExclusionTypeList).length; i++)
            if ($(this.ExclusionTypeList)[i].Code == pcode)
                break;
        return $(this.ExclusionTypeList)[i];
    }
};
var RatingTableList = {
    RatingTableList: "",
    findByRatingTableCode: function (pcode) {
        var i = 0;
        for (i = 0; i < $(this.RatingTableList).length; i++)
            if ($(this.RatingTableList)[i].RatingTable == pcode)
                break;
        return $(this.RatingTableList)[i];
    }
};
var ModuleList = {
    arr: "",
    findByCode: FindByCode
}
var RolesList = {
    arr: "",
    findByCode: FindByCode
}
var CoverageList = {
    arr: "",
    findByCode: FindByCode
}
var ModulesByProductList = {
    arr: "",
    findByCode: FindByCode
}
var CauseList = {
    arr: "",
    findByCode: FindByCode
}
var ExclusionPeriodList = {
    arr: "",
    findByCode: FindByCode
}
var IllnessWithExclusionList = {};

/* ************************* VARIABLES PARA RECARGOS Y DESCUENTOS ***************************** */

var DiscoexpremTypeList = {
    arr: "",
    findByCode: FindByCode
}
var RestrictionTypeList = {
    arr: "",
    findByCode: FindByCode
}
var CurrencyList = {
    arr: "",
    findByCode: FindByCode
}
var DiscountOrExtraPremiumTypeList = {
    arr: "",
    findByCode: FindByCode
}
var DiscoExPremTaxByProduc = {
    arr: "",
    findByCode: FindByCode
}
var RoleAllowedByProductCoverage = {
    arr: "",
    findByCode: FindByCode
}

/* ************************************ OTRAS VARIABLES ******************************** */
var gridtabAlarmList;
var isEditMode = false; /* TODO: buscar una alternativa a la asignacion en ubicaciones particulares */
var isNewRule = false;
var DialogButtonsText = {};
var btnLanguageText = "";

var currdecimalSeparator = "";
var currthousandsSeparator = "";
var dateTimeFormat = "";
var dateTimeFormatShort = "";
var ruleList = [];
/* **************************  Funciones Auxiliares **************************** */

function getCultureName() {
    resultFunction = "";
    ProxySyncTabUnderwritingRules.invoke("GetCultureInfoName", "", function (data) {
        resultFunction = data.d;
    });
    return resultFunction;
}

function compareMinorDate(date1, date2) {
    resultadoCompare = ""
    var params = JSON.stringify({
        date1: date1,
        date2: date2
    });

    ProxySyncTabUnderwritingRules.invoke("CompareMinorDate", params, function (data) {
        resultadoCompare = data.d;
    });
    return resultadoCompare;
}


function formatDateEffective(date) {
    resultFunction = "";
    var params = JSON.stringify({
        DateToFormat: date,
    });

    ProxySyncTabUnderwritingRules.invoke("formatDate", params, function (data) {
        resultFunction = data.d;
    });
    return resultFunction;
}

function getDateTimeFormat() {
    resultFunction = "";
    ProxySyncTabUnderwritingRules.invoke("GetDateTimeFormat", "", function (data) {
        resultFunction = data.d;
    });
    return resultFunction;
}

function getDateTimeFormatShortPattern() {
    resultFunction = "";
    ProxySyncTabUnderwritingRules.invoke("getDateTimeFormatShortPattern", "", function (data) {
        resultFunction = data.d;
    });
    return resultFunction;
}

function arrContainRoleKey(code) {
    roleCodesAccepted = [2, 3, 8, 20, 21, 22, 23, 24, 27, 28, 29, 30, 60, 67, 68]
    finded = false;
    for (i = 0; i < roleCodesAccepted.length; i++) {
        if (roleCodesAccepted[i] == code) {
            finded = true;
            break;
        }
    }
    return finded;
}

function getRuleOnLoadPage(idRule) {
    effectDateRule = "";
    ProxySyncTabUnderwritingRules.invoke("CleanListAlarms", "", function (data) { })
    cleanValidationsAlarms();
    unloadChildGrids();
    if ($("#EffectDateByRequest").val().length > 0) {
        effectDateRule = $("#EffectDateByRequest").val();
    } else {
        //effectDateRuleAux = formattedDateValue(new Date());
        //effectDateRule = new Date(effectDateRuleAux).format(window.__cultureInfo.dateTimeFormat.ShortDatePattern);
        $("#ModalRules").modal('toggle');
        LoadGridRules(idRule);
    }
    selectedRule = { UnderwritingRuleId: idRule, EffectiveDate: effectDateRule };
    GetTabUnderwritingRulesByIdRule(idRule, effectDateRule);
    LoadUnderwritingRuleAlarms(idRule);
}

function loadAutocompleteUwRule() {
    $('#ddlUwRule').autocomplete({
        source: function (request, response) {
            ruleList = [];
            var paramData = JSON.stringify({
                filter: request.term
            });
            $.when(
                ProxySyncLookUps.invoke("GetAllTabUnderwritingRuleFilterLkp", paramData, function (data) {
                    $.each(data.d, function (index, item) {
                        dateToParse = new Date(eval(item.EffectiveDate.slice(6, -2)));
                        ruleList.push(item.UnderwritingRuleId + " | " + formatDateEffective(dateToParse) + " | " + item.Description);
                    })
                })
            ).then(function () {
                $("#ddlUwRule_progressbar").prop("hidden", true);
                response(ruleList)
            });
        },
        minLength: 1,
        scroll: true,
        autoFocus: true,
        cache: false,
        select: function (event, ui) {
            ProxySyncTabUnderwritingRules.invoke("CleanListAlarms", "", function (data) { })
            cleanValidationsAlarms();
            unloadChildGrids();
            var params = ui.item.value.split("|");
            var seekIdRule = $.isNumeric(params[0]) ? params[0].trim() : -1;
            var seekDateRule = params[1] != undefined ? params[1].trim() : "";
            selectedRule = { UnderwritingRuleId: seekIdRule, EffectiveDate: seekDateRule };
            GetTabUnderwritingRulesByIdRule(seekIdRule, seekDateRule);
            LoadUnderwritingRuleAlarms(seekIdRule);
            return false;
        }
    }).focus(function () {
        $(this).autocomplete("search", "");
    });
}

function cleanValidationsAlarms() {
    validateExclusion = false;
    validateDescoexprem = false;
    validateMaxInsuredSum = false;

    if (sessionStorage.getItem('cantExclusionesAValidar') != null) {
        sessionStorage.setItem("cantExclusionesAValidar", 0);
    }
    if (sessionStorage.getItem('cantDescuentosAValidar') != null) {
        sessionStorage.setItem("cantDescuentosAValidar", 0);
    }
    if (sessionStorage.getItem('cantMaxAseguradoAValidar') != null) {
        sessionStorage.setItem("cantMaxAseguradoAValidar", 0);
    }
}

function CleanListAlarm(modal) {
    ProxySyncTabUnderwritingRules.invoke("CleanListAlarms", "", function (data) { });

    cleanValidationsAlarms();

    $('#' + modal).modal('toggle');
}

function CloseModal(modal) {
    $("#ddlBranch").val(ddlBranchSelected);
    $('#' + modal).modal('toggle');
}

function FindByCode(pcode) {
    var i = 0;
    var found = false;
    for (i = 0; i < $(this.arr).length; i++)
        if ($(this.arr)[i].Code == pcode) {
            found = true;
            break;
        }
    if (!found) return {
        Code: "-1", Description: ""
    };
    return $(this.arr)[i];
}

function arrayToJqgrid(listarr) {
    var i = 0;
    var ret = "";
    for (i = 0; i < $(listarr).length; i++)
        ret += $(listarr)[i].Code + ":" + $(listarr)[i].Description + ";";

    ret = "-1:;" + ret;
    return ret.slice(0, -1);
}

function arrayRolesToJqgrid(listarr) {
    var i = 0;
    var ret = "";
    for (i = 0; i < $(listarr).length; i++)
        if (arrContainRoleKey($(listarr)[i].Code)) {
            ret += $(listarr)[i].Code + ":" + $(listarr)[i].Description + ";";
        }
    ret = "-1:;" + ret;
    return ret.slice(0, -1);
}

function arrayRatingTableToJqgrid(listarr) {
    var i = 0;
    var ret = "";
    for (i = 0; i < $(listarr).length; i++)
        ret += $(listarr)[i].RatingTable + ":" + $(listarr)[i].RateDescription + ";";

    ret = "-1:;" + ret;
    return ret.slice(0, -1);
}

function getNullDateIfEmpty(value) {
    dateNull = new Date(value)
    patternDate = getDateTimeFormatShortPattern();
    switch (patternDate) {
        case "dd/MM/yyyy":
            return (value == "" ? new Date(0) : new Date(Date.parse(formattedDate(value))).yyyymmdd())//new Date(Date.parse(formattedDate(value))).yyyymmdd());
        case "MM/dd/yyyy":
            return (value == "" ? new Date(0) : formatDateEffective(new Date(value)))//new Date(value).format(getDateTimeFormatShortPattern()))
        default:
            return new Date(0);
    }
}

Number.prototype.format = function (n, x, s, c) {
    var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\D' : '$') + ')',
        num = this.toFixed(Math.max(0, ~~n));

    return (c ? num.replace('.', c) : num).replace(new RegExp(re, 'g'), '$&' + (s || ','));
};

Date.prototype.yyyymmdd = function () {
    var mm = this.getMonth() + 1;
    var dd = this.getDate();

    return [this.getFullYear(),
    (mm > 9 ? '' : '0') + mm,
    (dd > 9 ? '' : '0') + dd
    ].join('/');
};

function showAlert(msg) {
    $("#dialog-alert>p").html(msg);
    $("#dialog-alert").dialog("open");
}

function LoadRuleByKey(idRule, atDate) {
    selectedRule = { UnderwritingRuleId: idRule, EffectiveDate: atDate };
    GetTabUnderwritingRulesByIdRule(idRule, atDate);
    LoadUnderwritingRuleAlarms(idRule);
}

function GetTabUnderwritingRulesByIdRule(uwIdRule, uwDateRule) {
    var params = { IdRule: uwIdRule == "" ? 0 : uwIdRule, effectDate: getNullDateIfEmpty(uwDateRule), languageID: $("#btnLanguage").children(".btn.dropdown").attr("data-value") };
    var paramData = JSON.stringify(params);
    var disableControl = false;
    var disableEdit = false;
    isEditMode = false; /* TODO: buscar una alternativa a la asignacion en ubicaciones particulares */
    ProxySyncTabUnderwritingRules.invoke("GetTabUnderwritingRulesByIdRule", paramData, function (data) {
        cancellationDate = new Date(eval(data.d.CancellationDate.slice(6, -2)));
        if (formatDateEffective(cancellationDate) != emptyDate)
            UWRules_Buttons_Enable.whenSearching[0] = 0;
        else
            UWRules_Buttons_Enable.whenSearching[0] = 1;

        if (data.d.UnderwritingRuleId > 0) {
            $("#ddlUwRuleId").val(data.d.UnderwritingRuleId);
            disableControl = true;
        }
        if ($("#ddlUwRule").is(":visible"))
            $("#ddlUwRule").val(data.d.Description);
        else
            $("#ddlUwRuleDescription").val(data.d.Description);
        var vEfectDate = new Date(eval(data.d.EffectiveDate.slice(6, -2)));//.format(getDateTimeFormatShortPattern());
        vEfectDate = formatDateEffective(vEfectDate);
        var illnessDescription = "";

        if (data.d.ImpairmentCode != null) {
            var paramData = JSON.stringify({ illness: "" + data.d.ImpairmentCode + "" });
            ProxySyncTabUnderwritingRules.invoke("GetIllnessDescription", paramData, function (data) { illnessDescription = data.d });
        }
        $("#txtEfecDate").val(vEfectDate);
        $("#ddlUwStatus").val(data.d.UnderwritingRuleStatus);
        $("#txtEnfermedad").val(data.d.ImpairmentCode == null ? "" : (data.d.ImpairmentCode + " | " + illnessDescription));
        $("#txtExplanation").val(data.d.Explanation);
        $("#ddlNivelEnfermedad").val(data.d.DegreeId);
        $("#ddlUwArea").val(data.d.UnderwritingArea);
        $("#txtPoints").val(data.d.MortalityDebits).change();
        $("#ddlBranch").val(data.d.LineOfBusiness);
        $("#ddlCaseType").val(data.d.UnderwritingCaseType);
        $("#ddlReqType").val(data.d.RequirementType);
        $("#ddlReqStatus").val(data.d.RequirementStatus);
        $("#ddlReqQuestion").val(data.d.QuestionId);
        if (data.d.CancellationDate != null && data.d.CancellationDate != "") {
            var vCancelDate = new Date(eval(data.d.CancellationDate.slice(6, -2)));//.format("dd/MM/yyyy");
            vCancelDate = formatDateEffective(vCancelDate);
            if (vCancelDate != emptyDate && vCancelDate != "31/12/0000" && vCancelDate != "31/12/0001")
                disableEdit = true;
        }
    });
    if (disableControl) {
        enableUWRulesButtons(UWRules_Buttons_Enable.whenSearching);
        enableControlList(Control_List_Enable.whenSearching);
    } else {
        $("#ddlUwStatus").val(1);
    }
    if (disableEdit) {
        $("#EditRule").addClass("disabled");
        $("#DeleRule").addClass("disabled");
    } else if (uwIdRule != 0) {
        $("#EditRule").removeClass("disabled");
        $("#DeleRule").removeClass("disabled");
    }

    if (uwIdRule != 0) {
        $("#AddRule").addClass("disabled");
    }
}

function getModulesList(idProduct) {
    var params = JSON.stringify({
        LineOfBusiness: $("#ddlBranch").val(),
        productCode: idProduct,
        languageID: $("#btnLanguage").children(".btn.dropdown").attr("data-value")
    });
    if (idProduct != "-1")
        ProxyAsyncLookUps.invoke("GetModulesLkp", params, function (data) {
            ModuleList.arr = data.d;
        });
    else {
        ModuleList.arr = [];
    }
    isModularProduct = ModuleList.arr.length > 0;
}

function getCoverageList(idProduct, coverageModule) {
    var params = JSON.stringify({
        LineOfBusiness: $("#ddlBranch").val(),
        productCode: idProduct,
        coverageModule: coverageModule,
        languageID: $("#btnLanguage").children(".btn.dropdown").attr("data-value")
    });
    if (idProduct != "-1")
        ProxySyncLookUps.invoke("GetCoverageLkp", params, function (data) { CoverageList.arr = data.d; });
    else
        CoverageList.arr = [];
}

function getModulesByProductList(idBranch, idProduct) {
    try {
        var params = JSON.stringify({
            LineOfBusiness: idBranch,
            productCode: idProduct,
            languageID: $("#btnLanguage").children(".btn.dropdown").attr("data-value")
        });
        if (idProduct != "-1")
            ProxySyncLookUps.invoke("GetModuleByProductLkp", params, function (data) {
                ModulesByProductList.arr = data.d;
            });
        else {
            ModulesByProductList.arr = [];
        }
    } catch (e) {
        console.log("Error en la carga de la función getModulesByProductList Message [" + e.message + "]")
    }
}

function getRoleAllowedByProductCoverageList(idProduct) {
    var params = JSON.stringify({
        LineOfBusiness: $("#ddlBranch").val(),
        productCode: idProduct,
        languageID: $("#btnLanguage").children(".btn.dropdown").attr("data-value")
    });
    if (idProduct != "-1")
        ProxyAsyncLookUps.invoke("GetRoleAllowedByProductCoverage", params, function (data) {
            RoleAllowedByProductCoverage.arr = data.d;
        });
    else {
        RoleAllowedByProductCoverage.arr = [];
        RoleAllowedByProductCoverage.arr.unshift({
            Code: "-1", Description: ""
        });
    }
}

function LoadCoverageByModule(e) {
    $('#CoverageCode').empty();
    $('#CoverageCode').append($('<option>', {
        value: -1, text: ''
    }));
    var value = $.grep(CoverageList.arr, function (n, i) {
        return n.Module == $("#ProductModule option:selected").val();
    });
    $.each(value, function (i, item) {
        $('#CoverageCode').append($('<option>', {
            value: item.Code,
            text: item.Description
        }));
    });
}

function LoadCoverageByRole(e) {
    $('#CoverageCode').empty();
    $('#CoverageCode').append($('<option>', {
        value: -1, text: ''
    }));
    var value = $.grep(RolesList.arr, function (n, i) {
        return n.Module == e;
    });
    $.each(value, function (i, item) {
        $('#CoverageCode').append($('<option>', {
            value: item.Code,
            text: item.Description
        }));
    });
}

function getIllnessWithExclusionList(idBranch, idProduct, langId) {
    var res = [];
    var regini = 0;
    var regend = 3000; /* regend: Establece el tamaño deseado de carga bloque de datos que el servicio solicitará internamente */
    var jump = regend;
    var fetchmore = true;

    var params = JSON.stringify({
        LineOfBusiness: idBranch, productCode: idProduct, languageID: langId, filter: "", regini: regini, regend: regend
    });
    //ProxySyncLookUps.invoke("GetIllnessByProductFilterLkp", params, function (data) {
    ProxySyncLookUps.invoke("GetAllIllnessTypeLkp", "", function (data) {
        IllnessWithExclusionList = {};
        if (data.d.length > 0) {
            $.each(data.d, function (index, item) {
                IllnessWithExclusionList[item.Code] = item.Description

            });
        }
        else fetchmore = false;
    });
}

function unloadChildGrids() {
    /*
    $.jgrid.gridUnload("tblExclusion");
    $.jgrid.gridUnload("tblDiscoexprem");
    $.jgrid.gridUnload("tblMaxInsuredSum");
    */
    $("#tblExclusion").jqGrid("GridUnload");
    $("#tblDiscoexprem").jqGrid("GridUnload");
    $("#tblMaxInsuredSum").jqGrid("GridUnload");

}

function ValidateRequiredControls() {
    var res = false;
    var msg = "";
    $("#form-uwrule [required]").each(function () {
        var elemvalue;
        switch (this.nodeName.toLowerCase()) {
            case "select":
                elemvalue = $("#" + this.id + " option:selected").text().trim();
                break;
            default:
                elemvalue = $(this).val();
                break;
        }

        if (elemvalue == "")
            msg += $($("label[for='" + $(this)[0].id + "']")[0]).text() + ", ";
    });
    msg = msg.slice(0, -2);
    if (msg.length > 0) {
        $(".divError").html(TUWRMessages.requiredFieldsText + msg);
        $(".divError").show();
        setTimeout(function () {
            $(".divError").hide();
        }, 10000);
        return false;
    }
    $(".divError").hide();
    return true;
}


function validateGridAlarm() {
    message = "";
    cantExclusionesAValidar = sessionStorage.getItem("cantExclusionesAValidar") != null ? sessionStorage.getItem("cantExclusionesAValidar") : 0;
    cantDescuentosAValidar = sessionStorage.getItem("cantDescuentosAValidar") != null ? sessionStorage.getItem("cantDescuentosAValidar") : 0;
    cantMaxAseguradoAValidar = sessionStorage.getItem("cantMaxAseguradoAValidar") != null ? sessionStorage.getItem("cantMaxAseguradoAValidar") : 0;

    if (typeof gridtabAlarmList === "undefined" || gridtabAlarmList.getGridParam("reccount") == 0) {
        message = AlarmMessages.msgFillAlarm;
    }
    if (cantExclusionesAValidar > 0) {
        message = AlarmMessages.msgFillExclusion;
    }
    if (cantDescuentosAValidar > 0) {
        message = AlarmMessages.msgFillDescoexprem;
    }
    if (cantMaxAseguradoAValidar > 0) {
        message = AlarmMessages.msgFillMaxInsuredSum;
    }
    if (message != "") {
        $(".divError").html(message);
        $(".divError").show();
        setTimeout(function () {
            $(".divError").hide();
        }, 10000);
        isErrorAlarm = true;
        return false;
    } else
        return true;
}


function mapObjectToArray(dObj) {
    var dObjarray = [];
    var list = Object.keys(dObj);
    var mapped = list.map(function (el, i) {
        return { index: i, value: el };
    });
    mapped.sort(function (a, b) {
        return +(dObj[a.value] > dObj[b.value]) || +(dObj[a.value] === dObj[b.value]) - 1;
    });
    var dObjarray = mapped.map(function (el) {
        return list[el.index] + " | " + dObj[list[el.index]];
    });
    return dObjarray;
}

function filtReset() {

    gridModalRule = $("#gvwUWRules");
    gridModalRule.jqGrid('setGridParam', { search: false });
    var postData = gridModalRule.jqGrid('getGridParam', 'postData');
    $.extend(postData, { filters: "" });

    for (k in postData) {
        if (k == "_search")
            postData._search = false;
        else if ($.inArray(k, ["nd", "sidx", "rows", "sord", "page", "filters"]) < 0) {
            try {
                delete postData[k];
            } catch (e) { }
        }
    }
    $(':input[id*="gs_"]').val("");
    // for singe search you should replace the line with
    // $.extend(postData,{searchField:"",searchString:"",searchOper:""});
    gridModalRule.trigger("reloadGrid", [{ page: 1 }]);
    // for singe search you should replace the line with
    // $.extend(postData,{searchField:"",searchString:"",searchOper:""});
}


function controlRestrictionTabs(tabname) {
    if ("Li" + tabname == "LiExclusionsTab") {
        $("#LiExclusionsTab").show();
    } else {
        $("#LiExclusionsTab").hide();
    }

    if ("Li" + tabname == "LiSurchargeDiscountTab") {
        $("#LiSurchargeDiscountTab").show();
    } else {
        $("#LiSurchargeDiscountTab").hide();
    }

    if ("Li" + tabname == "LiMaxInsuredSumTab") {
        $("#LiMaxInsuredSumTab").show();
    } else {
        $("#LiMaxInsuredSumTab").hide();
    }

}

/* ***************************** Eventos ******************************************* */

$("#AddRule").on('click', function () {
    if (jQuery.grep(this.classList, function (n, i) { return n == "disabled"; }) == "disabled") return false;
    enableNewRule();
    isNewRule = true;
    enableUWRulesButtons(UWRules_Buttons_Enable.whenAdding);
    enableControlList(Control_List_Enable.whenAdding);
    clearControlList(Control_List_Clear.whenAdding);
    SetRequiredControls(Control_List_Required.whenAdding);
    $("#ddlUwRuleId").val("")
    GetTabUnderwritingRulesByIdRule(0, "");
    setControlDefaultValues(Control_List_Default.whenAdding);
    $("#txtEfecDate").val(jsonToDate("/Date" + Date.now() + "/"))
    isEditMode = true;
    unloadChildGrids();
    LoadUnderwritingRuleAlarms(0);

});

$("#DeleRule").on('click', function () {
    if (jQuery.grep(this.classList, function (n, i) { return n == "disabled"; }) == "disabled") return false;
    var response = "";
    var paramData = JSON.stringify({ underwritingRuleId: "" + $("#ddlUwRuleId").val() + "" });
    ProxySyncTabUnderwritingRules.invoke("ValidateRecordHistories", paramData, function (data) { response = data.d });
    $("#confirm_delete_rule_message").html(response);
    $('#confirm_delete_rule').modal('show');
});

$("#EditRule").on('click', function () {
    if (jQuery.grep(this.classList, function (n, i) { return n == "disabled"; }) == "disabled") return false;
    isNewRule = false;
    enableUWRulesButtons(UWRules_Buttons_Enable.whenEditing);
    enableControlList(Control_List_Enable.whenEditing);
    SetRequiredControls(Control_List_Required.whenUpdating);
    isEditMode = true; /* TODO: buscar una alternativa a la asignacion en ubicaciones particulares */
    var seekIdRule = $.isNumeric($("#ddlUwRuleId").val()) ? $("#ddlUwRuleId").val() : -1;
    //$("#ddlUwRuleDescription").attr("required", false);
    unloadChildGrids();
    LoadUnderwritingRuleAlarms(seekIdRule);
    setControlDefaultValues(Control_List_Default.whenUpdating);
    $("#ddlUwRule").prop('disabled', true);
    $("#ddlUwRule").addClass("disabled");
    if ($("#btnLanguage").children(".btn.dropdown").attr("data-value") == 1) {
        enableUpdateRule($("#ddlUwRule").val());
    } else {
        $("#ddlUwRuleDescription").attr("required", false);
    }
});

$("#CancelRule").on('click', function () {
    if (jQuery.grep(this.classList, function (n, i) { return n == "disabled"; }) == "disabled") return false;
    cancelRule();
});

$("#SaveRule").on('click', function () {
    if (jQuery.grep(this.classList, function (n, i) { return n == "disabled"; }) == "disabled") return false;
    if (compareMinorDate($("#txtEfecDate").val(), formatDateEffective(new Date())) == "True") {// (Date.parse($("#txtEfecDate").val()) < Date.parse(formatDateEffective(new Date()))) {
        $(".divError").html(TUWRMessages.greaterOrEqualThanToday);
        $(".divError").show();
        return false;
    }
    if (ValidateRequiredControls() && validateGridAlarm())

        if ($("#ddlUwRuleId").val() != 0 && $("#ddlUwRuleId").val() != "") {
            if (compareMinorDate($("#txtEfecDate").val(), selectedRule.EffectiveDate) == "True")//(Date.parse($("#txtEfecDate").val()) < Date.parse(selectedRule.EffectiveDate))
            {
                $(".divError").html(TUWRMessages.invalidEffectDate + selectedRule.EffectiveDate);
                $(".divError").show();
                return false;
            }
            $("#confirm_update_rule").modal('show');
        }
        else {
            SaveRule();
        }
});

$("#seekddlUwRule").on('click', function () {
    if (jQuery.grep(this.classList, function (n, i) { return n == "disabled"; }) == "disabled") return false;
    filtReset();
    $("#ModalRules").modal('toggle');
    LoadGridRules("");
});

$("#btn-cancelar-regla").on('click', function () {
    $(".modal-backdrop").hide();
});

$("#ddlBranch").on('change', function () {

    if (typeof ($("#ddlBranch option:selected").val()) != "undefined" && $.trim($("#ddlBranch option:selected").val()) != "") {
        ddlBranchAux = $("#ddlBranch option:selected").val();
        unloadChildGrids();
        if (isNewRule) {
            if (typeof gridtabAlarmList != "undefined" && gridtabAlarmList.getGridParam("reccount") > 0) {
                $("#confirm_accion").modal("show");
            } else {
                ddlBranchSelected = $("#ddlBranch option:selected").val();
                LoadUnderwritingRuleAlarms(0);
            }
        }
        else
            LoadUnderwritingRuleAlarms(seekIdRule);
    } else {
        unloadChildGrids();
        if (isNewRule) {
            LoadUnderwritingRuleAlarms(0);
        }
        else
            LoadUnderwritingRuleAlarms(seekIdRule);
    }
});


$("#ddlUwRule").on('input', function (e) {
    if ($(this).val().length >= 1) {
        $("#ddlUwRule_progressbar").prop("hidden", false);
        //whenSearchingFromInput = [1, 0, 0, 0, 0];
        //Enable_UWRules_Buttons(whenSearchingFromInput);
        //var paramData = JSON.stringify({
        //    filter: $(this).val()
        //});
        ////var rules = [];
        //$.when(
        //    ProxySyncLookUps.invoke("GetAllTabUnderwritingRuleFilterLkp", paramData, function (data) {
        //        $.each(data.d, function (index, item) {
        //            ruleList.push(item.UnderwritingRuleId + " | " + new Date(eval(item.EffectiveDate.slice(6, -2))).format(window.__cultureInfo.dateTimeFormat.ShortDatePattern) + " | " + item.Description);
        //        })
        //    })
        //).then(function () {
        //    $("#ddlUwRule_progressbar").prop("hidden", true);            
        //});
    }
});


$("#txtEnfermedad").on('input', function (e) {
    if ($(this).val().length > 0) {
        var paramData = JSON.stringify({ filter: "%" + $(this).val() + "%" });
        var illness = [];

        ProxySyncLookUps.invoke("GetAllIllnessTypeLkp", paramData, function (data) {
            $.each(data.d, function (index, item) {
                illness.push(item.Code + " | " + item.Description);
            });
        });
        $('#txtEnfermedad').autocomplete({
            source: illness,
            minLength: 0,
            scroll: true
        }).focus(function () {
            $(this).autocomplete("search", "");
        });
        $("#ddlNivelEnfermedad").removeProp('disabled');
    } else {
        $("#ddlNivelEnfermedad").val(-1);
        $("#ddlNivelEnfermedad").prop('disabled', true);
    }
});

$(function () {
    var dateToday = new Date();
    $("#txtEfecDate").datepicker(
        {
            minDate: dateToday
        });

    $("#ddlUwRule_progressbar").progressbar({
        value: false
    });

    /* INI */
    $("#txtPoints").on("change", function () {
        $("#slider").val(this.value);
    });
    if (isExplorer())
        $("#slider").on("change", function () {
            $('#txtPoints').val($(this).val());
        });
    else
        $("#slider").on("input", function () {
            $('#txtPoints').val($(this).val());
        });
    /* END - eventos para control input type range */
});


var seekIdRule = $.isNumeric($("#ddlUwRuleId").val()) ? $("#ddlUwRuleId").val() : -1;
$("#tabsGeneral ul.nav.nav-tabs > li > a[href$='#ExclusionsTab']").on("click", function () {
    LoadUnderwritingRuleAlarmsExclusions($("#ddlBranch").val(), selectedAlarm.Product, seekIdRule);
});
$("#tabsGeneral ul.nav.nav-tabs > li > a[href$='#SurchargeDiscountTab']").on("click", function () {
    LoadUnderwritingRuleAlarmsDiscoexprem($("#ddlBranch").val(), selectedAlarm.Product, seekIdRule);
});
$("#tabsGeneral ul.nav.nav-tabs > li > a[href$='#MaxInsuredSumTab']").on("click", function () {
    LoadUnderwritingRuleAlarmsMaxInsuredSum($("#ddlBranch").val(), selectedAlarm.Product, seekIdRule);
});

/********************************** Función principal para carga de reglas *************************************************/


function MessageEnabling() {
    if (validateDescoexprem) {
        $("messageInformation").html(AlarmMessages.msgInfoRestriction)
        //gridtabAlarmList.append('<td colspan="5" class=" subgrid-data"><div> H' + AlarmMessages.msgInfoRestriction + '</div></td>');
    }
    else if (validateExclusion) {
        $("messageInformation").html(AlarmMessages.msgInfoRestriction)
        //gridtabAlarmList.append('<td colspan="5" class=" subgrid-data"><div> H' + AlarmMessages.msgInfoRestriction + '</div></td>');
    }
    else if (validateMaxInsuredSum) {
        $("messageInformation").html(AlarmMessages.msgInfoRestriction)
        //gridtabAlarmList.append('<td colspan="5" class=" subgrid-data"><div> H' + AlarmMessages.msgInfoRestriction + '</div></td>');
        //gridtabAlarmList.jqGrid("footerData", "set", AlarmMessages.msgInfoRestriction, true)
    }
}
function LoadGridRules(uwRuleId) {
    //"use strict";
    $.jgrid.defaults.styleUI = 'Bootstrap';

    var gridColNamesRules = "";
    var gridColModelRules = "";
    ProxySyncTabUnderwritingRules.invoke("GetTabHeaderRulesValues", "", function (data) {
        gridColNamesRules = data.d;
    });

    var jqgridShortDateFormat = getDateTimeFormat();
    var gridColModelRules = [
        {
            name: "UnderwritingRuleId", index: "UnderwritingRuleId", resizable: true, editable: false, hidden: false, search: true, searchoptions: { defaultValue: uwRuleId }, sorttype: 'number',
        },
        {
            name: "UnderwritingRuleIdDescription", index: "UnderwritingRuleIdDescription", resizable: true, editable: false, hidden: false, search: true, formatter: function (cellvalue, options, rowObject) {
                rowObject.UnderwritingRuleIdDescription = rowObject.Description; return rowObject.UnderwritingRuleIdDescription;
            }, stype: 'text',
        },
        {
            name: "EffectiveDate", index: "EffectiveDate", resizable: true, editable: false, width: 140, sorttype: 'date', formatter: 'date', formatoptions: {
                newformat: jqgridShortDateFormat
            }, editrules: {
                custom: true, custom_func: ValidateDateField, required: true, edithidden: true
            }, editoptions: {
                dataInit: function (element) {
                    ConvertToCalendar(element); $(element).datepicker("disable");
                }
            }, formoptions: {
                rowpos: 5, colpos: 2
            }, formatter: function (cellvalue, options, rowObject) {
            	if (rowObject.EffectiveDateString == emptyDate){
            		rowObject.EffectiveDateString = '';
            	}
                return rowObject.EffectiveDateString;
            }
        },
        {
            name: "UnderwritingRuleStatusDescription", index: "UnderwritingRuleStatusDescription", resizable: true, editable: false, hidden: false, search: true, formatter: function (cellvalue, options, rowObject) {
                rowObject.UnderwritingRuleStatusDescription = $("#ddlUwStatus [value=" + rowObject.UnderwritingRuleStatus + "]").text(); return rowObject.UnderwritingRuleStatusDescription;
            }
        },
        {
            name: "ImpairmentCodeDescription", index: "ImpairmentCodeDescription", resizable: true, editable: false, hidden: false, search: true, formatter: function (cellvalue, options, rowObject) {
                return rowObject.ImpairmentCodeDescription != null ? rowObject.ImpairmentCodeDescription : '';
            }
        },
        {
            name: "UnderwritingCaseTypeDescription", index: "UnderwritingCaseTypeDescription", resizable: true, editable: false, hidden: false, search: true, formatter: function (cellvalue, options, rowObject) {
                rowObject.UnderwritingCaseTypeDescription = $("#ddlCaseType [value=" + rowObject.UnderwritingCaseType + "]").text(); return rowObject.UnderwritingCaseTypeDescription;
            }
        },
        {
            name: "RequirementTypeDescription", index: "RequirementTypeDescription", resizable: true, editable: false, hidden: false, search: true, formatter: function (cellvalue, options, rowObject) {
                rowObject.RequirementTypeDescription = $("#ddlReqType [value=" + rowObject.RequirementType + "]").text(); return rowObject.RequirementTypeDescription;
            }
        },
        {
            name: "UnderwritingAreaDescription", index: "UnderwritingAreaDescription", resizable: true, editable: false, hidden: false, search: true, formatter: function (cellvalue, options, rowObject) {
                rowObject.UnderwritingAreaDescription = $("#ddlUwArea [value=" + rowObject.UnderwritingArea + "]").text(); return rowObject.UnderwritingAreaDescription;
            }
        },
        {
            name: "LineOfBusinessDescription", index: "LineOfBusinessDescription", resizable: true, editable: false, hidden: false, search: true, formatter: function (cellvalue, options, rowObject) {
                rowObject.LineOfBusinessDescription = $("#ddlBranch [value=" + rowObject.LineOfBusiness + "]").text(); return rowObject.LineOfBusinessDescription;
            }
        },
        {
            name: "EffectiveDateString", index: "EffectiveDateString", resizable: true, editable: false, hidden: true, search: true
        }
    ];

    var resdatagridrules = "";
    ProxySyncTabUnderwritingRules.invoke("GetAllTabUnderwritingRules", "", function (data) {
        resdatagridrules = data.d;
    });

    var gridRules = $("#gvwUWRules");
    gridRules.jqGrid($.extend({}, GeneralGridOptions, {
        data: resdatagridrules,
        datatype: "local",
        colNames: gridColNamesRules,
        colModel: gridColModelRules,
        pager: '#pager-Rules',
        subGrid: false,
        viewrecords: false,
        pgbuttons: true,
        loadonce: true,
        onSelectRow: function (id) {
            var dataFromTheRow = gridRules.jqGrid('getRowData', id);
            selectedRule = dataFromTheRow;
            ProxySyncTabUnderwritingRules.invoke("CleanListAlarms", "", function (data) { })
            cleanValidationsAlarms();
            unloadChildGrids();
            var temp = window.alert;
            try {
                window.alert = function () { };
                GetTabUnderwritingRulesByIdRule(dataFromTheRow.UnderwritingRuleId, dataFromTheRow.EffectiveDate);
                LoadUnderwritingRuleAlarms(dataFromTheRow.UnderwritingRuleId);
            } catch (ex) {
                window.alert = temp;
                window.location.reload();
            }
            window.alert = temp;
            $("#ModalRules").modal('hide');
        }
    }));
    gridRules.jqGrid("filterToolbar", {
        stringResult: true,
        searchOnEnter: false,
        enableClear: false,
        searchOperators: false,
        defaultSearch: 'cn',
        autosearch: true
        //, recreateFilter: true
    });
    gridRules.jqGrid("navGrid", "#pager-Rules",
        {
            edit: false,
            add: false,
            del: false,
            search: false,
            refresh: true,
        });
    gridRules.trigger("reloadGrid");
    if (uwRuleId.length > 0) {
        gridRules[0].triggerToolbar();
    }
}

/* ***************************** Función principal para carga de alarmas  ******************************************************* */
var selectedAlarm = "";
var isModularProduct = false;
var AlarmMessages = {};

function LoadUnderwritingRuleAlarms(idRule) {
    "use strict";
    $.jgrid.defaults.styleUI = 'Bootstrap';
    // $.jgrid.gridUnload("tabAlarmList");
    $("#tabAlarmList").jqGrid("GridUnload");
    var gridColNamesTbUWRAlarm = "";
    var gridColModelTbUWRAlarm = "";
    var tooltips = {};

    ProxySyncTabUnderwritingRules.invoke("GetAlarmMessages", "", function (data) { AlarmMessages = data.d; });

    ProxySyncTabUnderwritingRules.invoke("GetHeaderValuesRuleAlarms", "", function (data) {
        gridColNamesTbUWRAlarm = data.d;
    });
    try {
        gridColNamesTbUWRAlarm.push("IdProduct");/*For hidden linked column*/
    } catch (e) {
    }

    ProxySyncTabUnderwritingRules.invoke("GetAlarmToolTips", "", function (data) {
        tooltips = data.d;
    });

    gridColModelTbUWRAlarm = [
        {
            name: 'Product', index: 'Product', resizable: true, editable: true, hidden: false, search: false,
            formatter: function (cellvalue, options, rowObject) {
                var ret = ProductList.findByProductCode(rowObject.Product, $("#ddlBranch").val()).ProductDescription;
                return ret;
            },
            unformat: function (cellvalue, options, cell) {
                return selectedAlarm.Product.toString();
            },
            edittype: 'select',
            editoptions: {
                value: ProductList.arrayToJqgrid(),
                defaultValue: "-1", title: tooltips.ttipProduct
            }
        },
        {
            name: 'AlarmType', index: 'AlarmType', resizable: true, editable: true, hidden: false, search: false, formatter: function (cellvalue, options, rowObject) {
                if (rowObject != null && rowObject.AlarmType != null) {
                    try {

                        return AlarmTypeList.findByAlarmCode(rowObject.AlarmType);
                    }
                    catch (e) {
                        return ""
                    }
                } else
                    return "";
            }, edittype: 'select',
            editoptions: {
                value: arrayToJqgrid(AlarmTypeList.AlarmTypeList), defaultValue: "-1",
                dataEvents: [{
                    type: 'change', fn: function () {
                        var alarmType = $(this[this.selectedIndex]).val(); $("#DecisionComplement").attr("disabled", alarmType == 7 ? false : true); alarmType == 7 ? null : $("#DecisionComplement").val("");
                    }
                }], title: tooltips.ttipAlarmType
            }
        },
        {
            name: 'UpdateOnlyAssociatedRisk', index: 'UpdateOnlyAssociatedRisk', align: "center", width: "250", resizable: true, editable: false, hidden: true, search: false, formatoptions: { disabled: true }
        },
        {
            name: 'Stage', index: 'Stage', resizable: true, editable: true, hidden: false, search: false, formatter: function (cellvalue, options, rowObject) {
                return stageList.FindByCode(rowObject.Stage).Description;
            }, edittype: 'select',
            editoptions: {
                value: arrayToJqgrid(stageList.arr), defaultValue: "-1", title: tooltips.ttipStage
            }
        },
        {
            name: 'Status', index: 'Status', resizable: true, editable: true, hidden: false, search: false, formatter: function (cellvalue, options, rowObject) {
                return StatusList.findByStatusCode(rowObject.Status).Description;
            }, edittype: 'select',
            editoptions: {
                value: arrayToJqgrid(StatusList.StatusList), defaultValue: "-1", title: tooltips.ttipStatus
            }
        },
        {
            name: 'Decision', index: 'Decision', resizable: true, editable: true, hidden: false, search: false, formatter: function (cellvalue, options, rowObject) {
                return DecisionList.findByStageCode(rowObject.Decision).Description;
            }, edittype: 'select',
            editoptions: {
                value: arrayToJqgrid(DecisionList.DecisionList), defaultValue: "-1", title: tooltips.ttipDecision
            }
        },
        {
            name: 'DecisionComplement', index: 'DecisionComplement', resizable: true, editable: true, hidden: false, search: false, editoptions: { disabled: function (cellvalue, options, rowObject) { return $("#AlarmType").val() == "7" ? "false" : "true" }, title: tooltips.ttipDecisionComplement }
        },
        {
            name: 'IdProduct', index: 'IdProduct', resizable: false, editable: true, hidden: true, search: false, formatter: function (cellvalue, options, rowObject) {
                return rowObject.Product;
            }
        }
    ];

    var resdatagridrulealarms = [];
    var params = JSON.stringify({
        idRule: idRule, effectDate: getNullDateIfEmpty($("#txtEfecDate").val()), idLanguage: $("#btnLanguage").children(".btn.dropdown").attr("data-value")
    });
    if ($("#txtEfecDate").val() == emptyDate)
    	$("#txtEfecDate").val('');
    ProxySyncTabUnderwritingRules.invoke("GetTabUnderwritingRuleAlarms", params, function (data) {
        var indexDataGrid = 0;
        if (data != null) {
            $.each(data.d, function (index, item) {
                if (!item.IsDeletedMark) {
                    resdatagridrulealarms[indexDataGrid] = item;
                    indexDataGrid++;
                }
            });
        }
        //resdatagridrulealarms = data.d
    });
    MessageEnabling();
    gridtabAlarmList = $("#tabAlarmList");
    gridtabAlarmList.jqGrid($.extend({}, GeneralGridOptions, {
        data: resdatagridrulealarms,
        datatype: "local",
        colNames: gridColNamesTbUWRAlarm,
        colModel: gridColModelTbUWRAlarm,
        pager: '#pager-tabAlarmList',
        width: 390,
        subGrid: false,
        viewrecords: false,
        pgbuttons: true,
        onSelectRow: function (id) {
            $("#loadingFog").show();
            var AlarmsdataFromTheRow = gridtabAlarmList.jqGrid('getRowData', id);
            var currAlarm = $.grep(resdatagridrulealarms, function (n, i) {
                return n.id == id;
            })[0];
            selectedAlarm = {
                Product: currAlarm.Product,
                AlarmType: currAlarm.AlarmType,
                UpdateOnlyAssociatedRisk: false,//currAlarm.UpdateOnlyAssociatedRisk,
                Stage: currAlarm.Stage,
                Status: currAlarm.Status,
                Decision: currAlarm.Decision,
                DecisionComplement: currAlarm.DecisionComplement,
                IdProduct: currAlarm.IdProduct
            };
            try {
                isProductLife = ($.grep(ProductList.ProductList, function (n) {
                    return n.ProductCode == selectedAlarm.Product && n.LineOfBusiness == $("#ddlBranch").val();
                })[0].TypeOfLineOfBusiness == 1);
            } catch (e) {
                isProductLife = false
            }



            function TabsEnabling(tabname) {

                controlRestrictionTabs(tabname);

                $("#tabsGeneral ul.nav.nav-tabs > li > a").each(function (x) {
                    if (this.hash != "#" + tabname || tabname == "") {
                        //$(this).addClass("disable-links");
                        //$(this).closest("li").addClass("disabled");
                        //if (tabname == "") $(this).closest("li").removeClass("active");
                    } else {
                        $(this).click();
                        $(this).removeClass("disable-links");
                        $(this).closest("li").removeClass("disabled");
                    }
                });
            };

            var showtab = 1; /* TODO: basar 'showtab' en "AlarmType" */
            switch (currAlarm.AlarmType) {
                case 5:
                    showtab = 1;
                    TabsEnabling("SurchargeDiscountTab");
                    break;
                case 6:
                    showtab = 0;
                    TabsEnabling("ExclusionsTab");
                    break;
                case 8:
                    showtab = 2;
                    TabsEnabling("MaxInsuredSumTab");
                    break;
                default:
                    showtab = 0;
                    TabsEnabling("");
                    unloadChildGrids();
                    $("#loadingFog").hide();
                    break;
            }
        }
    }));
    var grid = gridtabAlarmList;
    var addSettingsAlarm = $.extend({}, GeneralAddSettings, {
        width: 500,
        serializeEditData: function (postData) {
            var newAlarm = {
                Product: postData.Product,
                AlarmType: postData.AlarmType,
                UpdateOnlyAssociatedRisk: false,//postData.UpdateOnlyAssociatedRisk,
                Decision: postData.Decision,
                Status: postData.Status,
                DecisionComplement: postData.DecisionComplement,
                IdProduct: postData.IdProduct
            };

            alarmTypeBuffer = postData.AlarmType

            if (postData.Stage > 0)
                newAlarm = $.extend({}, newAlarm, { Stage: postData.Stage });
            return JSON.stringify({ newAlarm: newAlarm });
        },
        afterShowForm: function (form) {
            var modal = form.parents(".ui-jqdialog");

            modal.css("position", "fixed");
            modal.css("top", 180);
            if (isExplorer()) {
                modal.css("left", (window.innerWidth - modal.width()) / 2);
            } else {
                modal.css("left", (window.innerWidth - modal.width()) / 2);
            }
        },
        beforeSubmit: function (postData, formid) {
            if (postData.Product < 1) {
                return [false, AlarmMessages.msgreqProduct];
            }
            if (postData.AlarmType < 1) {
                return [false, AlarmMessages.msgreqAlarmType];
            }
            if (postData.Decision < 0) {
                postData.Decision = 0;
            }
            return [true, ''];
        },
        afterSubmit: function (response, postdata) {
            if (response.success().statusText == "OK"
                && response.success().status == 200
                && response.success().readyState == 4) {
                // $.jgrid.gridUnload("tabAlarmList");
                $("#tabAlarmList").jqGrid("GridUnload");
                isErrorAlarm = false;
                if (alarmTypeBuffer == 5) {
                    validateDescoexprem = true;
                    if (sessionStorage.getItem('cantDescuentosAValidar') != null) {
                        var cantDescuentosAValidar = parseInt(sessionStorage.getItem('cantDescuentosAValidar')) + 1;
                        sessionStorage.setItem("cantDescuentosAValidar", cantDescuentosAValidar);
                    } else {
                        sessionStorage.setItem("cantDescuentosAValidar", 1);
                    }
                    gridtabAlarmList.append('<td colspan="5" class=" subgrid-data"><div> H' + AlarmMessages.msgInfoRestriction + '</div></td>');
                }
                else if (alarmTypeBuffer == 6) {
                    validateExclusion = true;
                    if (sessionStorage.getItem('cantExclusionesAValidar') != null) {
                        var cantExclusiones = parseInt(sessionStorage.getItem('cantExclusionesAValidar')) + 1;
                        sessionStorage.setItem("cantExclusionesAValidar", cantExclusiones);
                    } else {
                        sessionStorage.setItem("cantExclusionesAValidar", 1);
                    }
                    gridtabAlarmList.append('<td colspan="5" class=" subgrid-data"><div>' + AlarmMessages.msgInfoRestriction + '</div></td>');
                }
                else if (alarmTypeBuffer == 8) {
                    validateMaxInsuredSum = true;
                    if (sessionStorage.getItem('cantMaxAseguradoAValidar') != null) {
                        var cantMaxAseguradoAValidar = parseInt(sessionStorage.getItem('cantMaxAseguradoAValidar')) + 1;
                        sessionStorage.setItem("cantMaxAseguradoAValidar", cantMaxAseguradoAValidar);
                    } else {
                        sessionStorage.setItem("cantMaxAseguradoAValidar", 1);
                    }
                    gridtabAlarmList.append('<td colspan="5" class=" subgrid-data"><div>' + AlarmMessages.msgInfoRestriction + '</div></td>');
                }
                controlRestrictionTabs("");
                alarmTypeBuffer = 0;

                unloadChildGrids();
                LoadUnderwritingRuleAlarms(0);
                return [true, ''];

            } else {
                return [false, ''];
            }
        },
        url: "services/TabUnderwritingRules.aspx/AddAlarm"
    });
    var editSettingsAlarm = $.extend({}, GeneralAddSettings, {
        jqModal: false,
        closeOnEscape: true,
        savekey: [true, 13],
        closeAfterEdit: true,
        recreateForm: true,
        width: 600,
        serializeEditData: function (postData) {
            var newAlarm = {
                Product: postData.Product,
                AlarmType: postData.AlarmType,
                UpdateOnlyAssociatedRisk: false,//postData.UpdateOnlyAssociatedRisk,
                Decision: postData.Decision,
                Status: postData.Status,
                DecisionComplement: postData.DecisionComplement,
                IdProduct: postData.IdProduct
            };

            if (postData.Stage > 0)
                newAlarm = $.extend({}, newAlarm, { Stage: postData.Stage });
            return JSON.stringify({ newAlarm: newAlarm });
        },
        beforeShowForm: function (formid) {
            $("#pData").addClass('hide');
            $("#nData").addClass('hide');
            $("#Product").prop("disabled", true);
            $("#AlarmType").prop("disabled", true);
            $("#DecisionComplement").prop("disabled", selectedAlarm.AlarmType == 7 ? false : true);

            return [true, ''];
        },
        afterShowForm: function (form) {
            var modal = form.parents(".ui-jqdialog");

            modal.css("position", "fixed");
            modal.css("top", 180);
            if (isExplorer()) {
                modal.css("left", (window.innerWidth - modal.width()) / 2);
            } else {
                modal.css("left", (window.innerWidth - modal.width()) / 2);
            }
            return [true, ''];
        },
        beforeSubmit: function (postData, formid) {
            if (postData.Product < 1) {
                return [false, AlarmMessages.msgreqProduct];
            }
            if (postData.AlarmType < 1) {
                return [false, AlarmMessages.msgreqAlarmType];
            }
            if (postData.Decision < 0) {
                postData.Decision = 0;
            }
            return [true, ''];
        },
        afterSubmit: function (response, postdata) {
            if (response.success().statusText == "OK"
                && response.success().status == 200
                && response.success().readyState == 4) {
                // $.jgrid.gridUnload("tabAlarmList");
                $("#tabAlarmList").jqGrid("GridUnload");
                unloadChildGrids();
                LoadUnderwritingRuleAlarms(0);
                return [true, ''];
            } else {
                return [false, ''];
            }
        },
        url: "services/TabUnderwritingRules.aspx/UpdateAlarm"
    });
    var delSettingsAlarm = $.extend({}, GeneralDelSettings, {
        serializeDelData: function (postData) {
            return JSON.stringify({
                selectedAlarm: selectedAlarm
            });
            alarmTypeBuffer = selectedAlarm.AlarmType
        },
        afterSubmit: function (response, postdata) {
            var res = JSON.parse(response.success().responseText).d;
            if (res.length > 0) {
                $loading.hide();
                return [false, res.d];
            }
            if (selectedAlarm.AlarmType == 5) {
                validateDescoexprem = false;
                if (sessionStorage.getItem('cantDescuentosAValidar') != null) {
                    var cantDescuentosAValidar = parseInt(sessionStorage.getItem('cantDescuentosAValidar')) - 1;
                    sessionStorage.setItem("cantDescuentosAValidar", cantDescuentosAValidar);
                }
            }
            else if (selectedAlarm.AlarmType == 6) {
                validateExclusion = false;
                if (sessionStorage.getItem('cantExclusionesAValidar') != null) {
                    var cantExclusionesAValidar = parseInt(sessionStorage.getItem('cantExclusionesAValidar')) - 1;
                    sessionStorage.setItem("cantExclusionesAValidar", cantExclusionesAValidar);
                }
            }
            else if (selectedAlarm.AlarmType == 8) {
                validateMaxInsuredSum = false;
                if (sessionStorage.getItem('cantMaxAseguradoAValidar') != null) {
                    var cantMaxAseguradoAValidar = parseInt(sessionStorage.getItem('cantMaxAseguradoAValidar')) - 1;
                    sessionStorage.setItem("cantMaxAseguradoAValidar", cantMaxAseguradoAValidar);
                }
            }
            alarmTypeBuffer = 0;

            unloadChildGrids();
            gridtabAlarmList.delRowData(postdata.id);
            setTimeout(function () { LoadUnderwritingRuleAlarms(0); }, 2000);
            //LoadUnderwritingRuleAlarms(0);
            return [true, ''];
        },
        url: "services/TabUnderwritingRules.aspx/RemoveAlarm"
    });

    if (typeof ($("#ddlBranch option:selected").val()) != "undefined" && $.trim($("#ddlBranch option:selected").val()) != "") {
        gridtabAlarmList.jqGrid("navGrid", "#pager-tabAlarmList", {
            edit: isEditMode,
            add: isEditMode,
            del: isEditMode,
            search: false,
            rowList: [],
            pgbuttons: false,
            pgtext: null,
            viewrecords: false,
            refresh: false
        },
            editSettingsAlarm,
            addSettingsAlarm,
            delSettingsAlarm
        );
    }
    else {
        gridtabAlarmList.append('<td colspan="5" class=" subgrid-data"><div>' + AlarmMessages.msgSelectBranch + '</div></td>');
    }
    gridtabAlarmList.jqGrid("filterToolbar", {
        searchOnEnter: false,
        enableClear: false,
        searchOperators: false,
        defaultSearch: 'cn',
        autosearch: true
    });

    gridtabAlarmList.trigger("reloadGrid");
}

/* *************************************************** Función principal Grid de Restricciones (se dejó practicamente igual) ********************************** */

var selectedRestriction = ""; /* Es global para todas las restricciones. Se usa segun convenga en cada tab dependiente de las alarmas. */
var RatingTable;
var enuExclusionType = {
    impairment: 1,
    coverage: 2,
    impairmentUnderAnSpecificCoverage: 3,
    impairmentByTariff: 4
};
var regConfig = {};
var AlarmExclusionsMessages = {};

function LoadUnderwritingRuleAlarmsExclusions(idBranch, idProduct, idRule) {
    $.jgrid.defaults.styleUI = 'Bootstrap';
    "use strict";
    // $.jgrid.gridUnload("tblExclusion");
    $("#tblExclusion").jqGrid("GridUnload");
    var gridColNamesTblExclusions = "";
    var gridColModelTblExclusions = "";
    var resdatagridrulealarmexclusions = "";
    var gridtblExclusion = $("#tblExclusion");
    var params = JSON.stringify({
        LineOfBusiness: $("#ddlBranch").val(),
        productCode: idProduct,
        languageID: $("#btnLanguage").children(".btn.dropdown").attr("data-value")
    });
    var params2 = JSON.stringify({
        selectedAlarm: selectedAlarm,
        languageId: $("#btnLanguage").children(".btn.dropdown").attr("data-value")
    });

    var tooltips = {};

    $.when(
        ProxyAsyncTabUnderwritingRules.invoke("GetHeaderValuesRuleAlarmsExclusions", "", function (data) { gridColNamesTblExclusions = data.d; })
        , ProxyAsyncLookUps.invoke("GetAllBasicRatingByProductLkp", params, function (data) { RatingTableList.arr = data.d; })
        , ProxyAsyncLookUps.invoke("GetAllRolesByProductLkp", params, function (data) { RolesList.arr = data.d; })
        , ProxyAsyncLookUps.invoke("GetAllExclusionType", "", function (data) { ExclusionTypeList.ExclusionTypeList = data.d; })
        , ProxyAsyncLookUps.invoke("GetAllExclusionPeriodType", "", function (data) { ExclusionPeriodList.arr = data.d; })
        , getModulesList(idProduct)
        , getModulesByProductList(idBranch, idProduct)
        , ProxyAsyncLookUps.invoke("GetReasonForExclusionOfIllnessLkp", "", function (data) { CauseList.arr = data.d; })
        //, getIllnessWithExclusionList(idBranch, idProduct, $("#btnLanguage").children(".btn.dropdown").attr("data-value"))
        , getCoverageList(idProduct, 0) /*TODO: validar este llamado con cero */
        , getRoleAllowedByProductCoverageList(idProduct)
        , ProxyAsyncTabUnderwritingRules.invoke("GetAlarmExclusionsMessages", params2, function (data) { AlarmExclusionsMessages = data.d; })
        , ProxyAsyncTabUnderwritingRules.invoke("RetrieveTabUnderwritingRuleAlarm", params2, function (data) { resdatagridrulealarmexclusions = data.d; })
        , ProxyAsyncTabUnderwritingRules.invoke("GetAlarmExclusionToolTips", "", function (data) { tooltips = data.d; })
    ).then(function () {
        isModularProduct = ModuleList.arr.length > 0;
        isRatingTable = RatingTableList.length > 0 ? 1 : 0;
        //IllnessWithExclusionArray = mapObjectToArray(IllnessWithExclusionList);
        var fieldBehavior = [
            /*ExclusionType, field, modular, [mandatory, enabled, defval, hidden], ![mandatory, enabled, defval, hidden]*/
            [1, "RestrictionId", 0, [null, 0, 0, 1], [null, 0, 0, 1]],
            [1, "RatingTable", 0, [isRatingTable, isRatingTable, -1, isRatingTable], [isRatingTable, isRatingTable, -1, isRatingTable]],
            [1, "ProductModule", 0, [null, 0, 0, 0], [null, 0, 0, 0]],
            [1, "CoverageCode", 0, [null, 0, 0, 0], [null, 0, 0, 0]],
            [1, "ImpairmentCode", 0, [1, 1, "", 1], [1, 1, "", 1]],
            [1, "Cause", 0, [null, 1, -1, 1], [null, 1, -1, 1]],
            [1, "ExclusionPeriodType", 0, [1, 1, 0, 1], [1, 1, 0, 1]],
            [2, "RestrictionId", 0, [null, 0, 0, 1], [null, 0, 0, 1]],
            [2, "RatingTable", 0, [null, 0, -1, 0], [null, 0, -1, 0]],
            [2, "ProductModule", isModularProduct, [1, 1, -1, 1], [1, 1, -1, 1]],
            [2, "CoverageCode", isModularProduct, [1, 1, -1, 1], [1, 1, -1, 1]],
            [2, "ImpairmentCode", 0, [null, 0, "", 0], [null, 0, "", 0]],
            [2, "Cause", 0, [null, 0, 0, 0], [null, 0, 0, 0]],
            [2, "ExclusionPeriodType", 0, [null, 0, 0, 0], [null, 0, 0, 0]],
            [3, "RestrictionId", 0, [null, 0, 0, 1], [null, 0, 0, 1]],
            [3, "RatingTable", 0, , [null, 0, 0, 0]],
            [3, "ProductModule", isModularProduct, [1, 1, -1, 1], [0, 0, -1, 0]],
            [3, "CoverageCode", isModularProduct, [1, 1, -1, 1], [1, 1, -1, 1]],
            [3, "ImpairmentCode", 0, , [1, 1, "", 1]],
            [3, "Cause", 0, , [null, 1, -1, 1]],
            [3, "ExclusionPeriodType", 0, , [1, 1, "", 1], [1, 1, "", 1]],
            [4, "RestrictionId", 0, [null, 0, 0, 1], [null, 0, 0, 1]],
            [4, "RatingTable", 0, [isRatingTable, isRatingTable, -1, isRatingTable], [isRatingTable, isRatingTable, -1, isRatingTable]],
            [4, "ProductModule", isModularProduct, [1, 1, -1, 1], [1, 1, -1, 1]],
            [4, "CoverageCode", isModularProduct, [1, 1, -1, 1], [1, 1, -1, 1]],
            [4, "ImpairmentCode", 0, [1, 1, "", 1], [1, 1, "", 1]],
            [4, "Cause", 0, [1, 1, -1, 1], [1, 1, -1, 1]],
            [4, "ExclusionPeriodType", 0, [1, 1, "", 1], [1, 1, "", 1]],
            [5, "RestrictionId", 0, [null, 0, 0, 1], [null, 0, 0, 1]],
            [5, "RatingTable", 0, [null, 0, -1, 0], [null, 0, -1, 0]],
            [5, "ProductModule", false, [0, 0, -1, 0], [0, 0, -1, 0]],
            [5, "CoverageCode", false, [0, 0, -1, 0], [0, 0, -1, 0]],
            [5, "ImpairmentCode", 0, , [null, 0, "", 0], [null, 0, "", 0]],
            [5, "Cause", 0, , [null, 0, 0, 0]],
            [5, "ExclusionPeriodType", 0, , [null, 0, 0, 0]],
            [5, "WaitingPeriodDays", 0, , [null, 0, 0, 0]],
            [5, "WaitingPeriodMonths", 0, , [null, 0, 0, 0]],
            [5, "WaitingPeriodYears", 0, , [null, 0, 0, 0]]
        ];
        function ProcessBehavior(ExclusionType) {
            regConfig = {};

            if (ExclusionType == enuExclusionType.impairmentByTariff) {
                $("#dialog-message").dialog("open");
            }
            var processBehavior = $.grep(fieldBehavior, function (n) {
                return n[0] == parseInt(ExclusionType);
            });
            for (i = 0; i < processBehavior.length; i++) {
                var field = processBehavior[i][1];
                var modular = processBehavior[i][2];
                var mandatory;
                var enabled;
                var defval;
                var hidden;

                if (modular) {
                    mandatory = processBehavior[i][3][0];
                    enabled = processBehavior[i][3][1];
                    defval = processBehavior[i][3][2];
                    hidden = processBehavior[i][3][3];
                } else {
                    mandatory = processBehavior[i][4][0];
                    enabled = processBehavior[i][4][1];
                    defval = processBehavior[i][4][2];
                    hidden = processBehavior[i][4][3];
                }

                regConfig[field] = {
                    modular: modular,
                    mandatory: mandatory,
                    enabled: enabled,
                    defval: defval,
                    hidden: hidden
                }

                $("#" + field).attr("disabled", !enabled);
                //$("#tr_" + field).css("visibility", hidden == 0 ? "visible" : "hidden");
                $("#" + field).val(!defval ? "" : defval);
            }
        }

        gridColModelTblExclusions = [
            {
                name: 'RestrictionId', index: 'RestrictionId', resizable: false, editable: false, hidden: true, search: false,
            },
            {
                name: 'ExclusionType', index: 'ExclusionType', resizable: true, editable: true, hidden: false, search: false,
                formatter: function (cellvalue, options, rowObject) {
                    return ExclusionTypeList.findByExclusionCode(cellvalue).Description;
                },
                edittype: 'select',
                editoptions: {
                    value: arrayToJqgrid(ExclusionTypeList.ExclusionTypeList),
                    defaultValue: "-1",
                    dataEvents: [{
                        type: 'change', fn: function () {
                            var ExclusionType = $(this[this.selectedIndex]).val();
                            /*
                            value="2">Excluir cobertura
                            value="1">Excluir discapacidad
                            value="3">Excluir discapacidad bajo cobertura específica
                            value="4">Excluir discapacidad por tarifa
                            */
                            regConfig = {};
                            ProcessBehavior(ExclusionType);
                        }
                    }],
                    disabled: function () {
                        if (!isProductLife) $(this).val(enuExclusionType.coverage); ProcessBehavior(enuExclusionType.coverage); return !isProductLife;
                    }, title: tooltips.ttipExclusionType
                }
            },
            {
                name: 'RatingTable', index: 'RatingTable', resizable: true, editable: true, hidden: false, search: false,
                edittype: 'select',
                formatter: function (cellvalue, options, rowObject) {
                    try {
                        ratingDescription = RatingTableList.findByRatingTableCode(cellvalue).RateDescription
                    } catch (e) {
                        ratingDescription = "";
                    }
                    return ratingDescription == -1 || "" ? "" : ratingDescription;
                },
                editoptions: {
                    disabled: true,
                    value: arrayRatingTableToJqgrid(RatingTableList.arr),
                    defaultValue: "-1",
                    title: tooltips.ttipRatingTable
                }, align: 'right'
            },
            {
                /* TODO: VALIDAR SI 'ProductModule' ES DESABILITADO SI EL PRODUCTO NO ES MODULAR */
                name: 'ProductModule', index: 'ProductModule', resizable: true, editable: true, hidden: false, search: false,
                formatter: function (cellvalue, options, rowObject) {
                    return isModularProduct ? ModuleList.findByCode(cellvalue).Description : "";
                },
                edittype: 'select',
                editoptions: {
                    value: arrayToJqgrid(ModuleList.arr),
                    defaultValue: "-1",
                    disabled: !isModularProduct, title: tooltips.ttipProductModule
                }
            },
            {
                name: 'CoverageCode', index: 'CoverageCode', resizable: true, editable: true, hidden: false, search: false,
                formatter: function (cellvalue, options, rowObject) {
                    return CoverageList.findByCode(cellvalue).Description;
                },
                edittype: 'select', editoptions: {
                    value: arrayToJqgrid(CoverageList.arr), defaultValue: "-1", title: tooltips.ttipCoverageCode
                }
            },
            /* TODO: Validar el codigo/descripcion de la enfermedad porque no se esta escribiendo desde el ApplyRule */
            {
                name: 'ImpairmentCode', index: 'ImpairmentCode', resizable: true, editable: true, hidden: false, search: false,
                formatter: function (cellvalue, options, rowObject) {
                    if (rowObject.ImpairmentCode == null) rowObject.ImpairmentCodeDescription = "";
                    else {
                        if (rowObject.ImpairmentCode.split("|").length > 0)
                            try {
                                rowObject.ImpairmentCodeDescription = rowObject.ImpairmentCode.split("|")[1].trim()
                            } catch (e) {
                                rowObject.ImpairmentCodeDescription = rowObject.ImpairmentCode;
                            }
                    }
                    return rowObject.ImpairmentCodeDescription;
                },
                editoptions: {
                    dataEvents: [{
                        type: 'keyup', fn: function (e) {
                            if ($(this).val().length > 0) {
                                var paramFilter = JSON.stringify({ filter: "%" + $(this).val() + "%" });
                                var impairmentArray = [];
                                ProxySyncLookUps.invoke("GetAllIllnessTypeLkp", paramFilter, function (data) {
                                    $.each(data.d, function (index, item) {
                                        impairmentArray.push(item.Code + " | " + item.Description);
                                    });
                                });
                                $('#ImpairmentCode').autocomplete({
                                    source: impairmentArray,
                                    minLength: 0,
                                    scroll: true
                                }).focus(function () {
                                    $(this).autocomplete("search", "");
                                });
                            } else {
                                $('#ImpairmentCode').autocomplete("search", "");
                            }
                        }
                    }],
                    disabled: true, title: tooltips.ttipImpairmentCode
                }
            },
            {
                name: 'Cause', index: 'Cause', resizable: true, editable: true, hidden: false, search: false,
                formatter: function (cellvalue, options, rowObject) {
                    return CauseList.findByCode(cellvalue).Description;
                },
                edittype: 'select', editoptions: {
                    value: arrayToJqgrid(CauseList.arr), defaultValue: "-1", disabled: true, title: tooltips.ttipCause
                }
            },
            {
                name: 'ExclusionPeriodType', index: 'ExclusionPeriodType', resizable: true, editable: true, hidden: false, search: false,
                formatter: function (cellvalue, options, rowObject) {
                    return ExclusionPeriodList.findByCode(cellvalue).Description;
                },
                edittype: 'select',
                editoptions: {
                    value: arrayToJqgrid(ExclusionPeriodList.arr), defaultValue: "-1",
                    dataEvents: [{
                        type: 'change', fn: function () {
                            var ExclusionPeriodType = $(this[this.selectedIndex]).val();
                            var controls = ["WaitingPeriodDays", "WaitingPeriodMonths", "WaitingPeriodYears"];
                            for (i = 0; i < controls.length; i++) {
                                $("#" + controls[i]).attr("disabled", ExclusionPeriodType == 2 ? false : true);
                                ExclusionPeriodType == 2 ? null : $("#" + controls[i]).val("");
                            }
                        }
                    }], title: tooltips.ttipExclusionPeriodType_ex,
                    disabled: true
                }
            },
            /* TODO: Averiguar por que al crear una alerta de tipo exclusion desde el ApplyRule se esta guardando los siguientes datos en las columnas DOFFxyz */
            {
                name: 'WaitingPeriodDays', index: 'WaitingPeriodDays', resizable: true, editable: true, hidden: false, search: false, editoptions: {
                    disabled: true,
                    dataInit: function (e) {
                        e.style.textAlign = 'right';
                    }, title: tooltips.ttipWaitingPeriodDays
                }, align: 'right',
                formatter: 'integer'
            },
            {
                name: 'WaitingPeriodMonths', index: 'WaitingPeriodMonths', resizable: true, editable: true, hidden: false, search: false, editoptions: {
                    disabled: true,
                    dataInit: function (e) {
                        e.style.textAlign = 'right';
                    }, title: tooltips.ttipWaitingPeriodMonths
                }, align: 'right',
                formatter: 'integer'
            },
            {
                name: 'WaitingPeriodYears', index: 'WaitingPeriodYears', resizable: true, editable: true, hidden: false, search: false, editoptions: {
                    disabled: true,
                    dataInit: function (e) {
                        e.style.textAlign = 'right';
                    }, title: tooltips.ttipWaitingPeriodYears
                }, align: 'right',
                formatter: 'integer'
            },
        ];
        /*
       ProxySyncUnderwritingRule.invoke("GetAllRestriction", "{ AlarmType: " + typeAlarm + " }", function (data) { gridColModelTblExclusions = data.d; });
        */
        gridtblExclusion.jqGrid($.extend({}, GeneralGridOptions, {
            autowidth: true,
            height: '100%',
            shrinkToFit: true,
            width: 390,
            data: resdatagridrulealarmexclusions,
            datatype: "local",
            colNames: gridColNamesTblExclusions,
            colModel: gridColModelTblExclusions,
            pager: '#pager-tblExclusion',
            subGrid: false,
            viewrecords: false,
            pgbuttons: true,
            onSelectRow: function (id) {
                var RestrictiondataFromTheRow = gridtblExclusion.jqGrid('getRowData', id);
                var currRestriction = $.grep(resdatagridrulealarmexclusions, function (n, i) {
                    return n.id == id;
                })[0];
                selectedRestriction = {
                    ExclusionType: currRestriction.ExclusionType,
                    RatingTable: currRestriction.RatingTable,
                    ProductModule: currRestriction.ProductModule,
                    CoverageCode: currRestriction.CoverageCode,
                    ImpairmentCode: currRestriction.ImpairmentCode,
                    Cause: currRestriction.Cause,
                    ExclusionPeriodType: currRestriction.ExclusionPeriodType,
                    WaitingPeriodDays: currRestriction.WaitingPeriodDays,
                    WaitingPeriodMonths: currRestriction.WaitingPeriodMonths,
                    WaitingPeriodYears: currRestriction.WaitingPeriodYears,
                    RestrictionId: currRestriction.RestrictionId
                };
            }
        }));

        var addSettingsAlarmExclusions = $.extend({}, GeneralAddSettings, {
            width: 400,
            serializeEditData: function (postData) {
                var ret = JSON.stringify({
                    selectedAlarm: selectedAlarm,
                    dataRestriction: {
                        RestrictionId: selectedRestriction.RestrictionId == "-1" ? 0 : selectedRestriction.RestrictionId,
                        ExclusionType: postData.ExclusionType == "-1" ? 0 : postData.ExclusionType,
                        RatingTable: postData.RatingTable == "" ? 0 : postData.RatingTable,
                        ProductModule: postData.ProductModule == "-1" ? 0 : postData.ProductModule,
                        CoverageCode: postData.CoverageCode == "-1" ? 0 : postData.CoverageCode,
                        ImpairmentCode: postData.ImpairmentCode == "" ? "" : postData.ImpairmentCode,
                        Cause: postData.Cause == "-1" ? 0 : postData.Cause,
                        ExclusionPeriodType: postData.ExclusionPeriodType == "-1" ? 0 : postData.ExclusionPeriodType,
                        WaitingPeriodDays: postData.WaitingPeriodDays == "" ? 0 : postData.WaitingPeriodDays,
                        WaitingPeriodMonths: postData.WaitingPeriodMonths == "" ? 0 : postData.WaitingPeriodMonths,
                        WaitingPeriodYears: postData.WaitingPeriodYears == "" ? 0 : postData.WaitingPeriodYears,
                        RestrictionType: 1
                    }
                });
                return ret;
            },
            afterShowForm: function (form) {
                var modal = form.parents(".ui-jqdialog");

                modal.css("position", "fixed");
                modal.css("top", 180);
                if (isExplorer()) {
                    modal.css("left", (window.innerWidth - modal.width()) / 2);
                } else {
                    modal.css("left", (window.innerWidth - modal.width()) / 2);
                }
            },
            beforeSubmit: function (postData, formid) {
                if (postData.ExclusionType < 1) {
                    return [false, AlarmExclusionsMessages.msgreqExclusionType];
                }
                if (regConfig.RatingTable.mandatory && postData.RatingTable < 1) {
                    return [false, AlarmExclusionsMessages.msgreqRatingTable];
                }
                if (regConfig.ProductModule.mandatory && isModularProduct && postData.ProductModule < 1) {
                    return [false, AlarmExclusionsMessages.msgreqProductModule];
                }
                if (regConfig.CoverageCode.mandatory && postData.CoverageCode < 1) {
                    return [false, AlarmExclusionsMessages.msgreqCoverageCode];
                }
                if (regConfig.ImpairmentCode.mandatory && postData.ImpairmentCode < 1) {
                    return [false, AlarmExclusionsMessages.msgreqImpairmentCode];
                }
                if (regConfig.ExclusionPeriodType.mandatory && postData.ExclusionPeriodType < 1) {
                    return [false, AlarmExclusionsMessages.msgreqExclusionPeriodType];
                }

                var periods = ['WaitingPeriodDays', 'WaitingPeriodMonths', 'WaitingPeriodYears'];
                var res = 0;
                if (postData.ExclusionPeriodType == enuExclusionType.coverage) {
                    for (i = 0; i < periods.length; i++) res += $("#" + periods[i]).val();
                    if (res < 1) {
                        return [false, AlarmExclusionsMessages.msgreqPeriod];
                    }
                }
                return [true, ''];
            },
            afterSubmit: function (response, postdata) {
                if (response.success().statusText == "OK"
                    && response.success().status == 200
                    && response.success().readyState == 4
                    && JSON.parse(response.success().responseText).d == "") {
                    var seekIdRule = $.isNumeric($("#ddlUwRuleId").val()) ? $("#ddlUwRuleId").val() : -1;
                    validateExclusion = false;
                    if (sessionStorage.getItem('cantExclusionesAValidar') != null) {
                        cantExclusiones = parseInt(sessionStorage.getItem('cantExclusionesAValidar')) - 1;
                        sessionStorage.setItem("cantExclusionesAValidar", cantExclusiones);
                    } else {
                        sessionStorage.setItem("cantExclusionesAValidar", 0);
                    }
                    LoadUnderwritingRuleAlarmsExclusions(idBranch, selectedAlarm.Product, seekIdRule);
                    return [true, ''];
                } else {
                    return [false, JSON.parse(response.success().responseText).d];
                } DeleteRestriction
            },
            url: "services/TabUnderwritingRules.aspx/AddRestriction"
        });
        var editSettingsAlarmExclusions = $.extend({}, GeneralAddSettings, {
            jqModal: false,
            closeOnEscape: true,
            savekey: [true, 13],
            closeAfterEdit: true,
            recreateForm: true,
            width: 400,
            serializeEditData: function (postData) {
                var ret = JSON.stringify({
                    selectedAlarm: selectedAlarm,
                    dataRestriction: {
                        RestrictionId: selectedRestriction.RestrictionId == "-1" ? 0 : selectedRestriction.RestrictionId,
                        ExclusionType: postData.ExclusionType == "-1" ? 0 : postData.ExclusionType,
                        RatingTable: postData.RatingTable == "" ? 0 : postData.RatingTable,
                        ProductModule: postData.ProductModule == "-1" ? 0 : postData.ProductModule,
                        CoverageCode: postData.CoverageCode == "-1" ? 0 : postData.CoverageCode,
                        ImpairmentCode: postData.ImpairmentCode == "" ? "" : postData.ImpairmentCode,
                        Cause: postData.Cause == "-1" ? 0 : postData.Cause,
                        ExclusionPeriodType: postData.ExclusionPeriodType == "-1" ? 0 : postData.ExclusionPeriodType,
                        WaitingPeriodDays: postData.WaitingPeriodDays == "" ? 0 : postData.WaitingPeriodDays,
                        WaitingPeriodMonths: postData.WaitingPeriodMonths == "" ? 0 : postData.WaitingPeriodMonths,
                        WaitingPeriodYears: postData.WaitingPeriodYears == "" ? 0 : postData.WaitingPeriodYears,
                        RestrictionType: 1
                    }
                });
                return ret;
            },
            beforeShowForm: function (formid) {
                $("#pData").addClass('hide');
                $("#nData").addClass('hide');
                $("#ExclusionType").prop("disabled", true);
                $("#ProductModule").prop("disabled", true);
                $("#CoverageCode").prop("disabled", true);
                return [true, ''];
            },
            afterShowForm: function (form) {
                var modal = form.parents(".ui-jqdialog");

                modal.css("position", "fixed");
                modal.css("top", 180);
                if (isExplorer()) {
                    modal.css("left", (window.innerWidth - modal.width()) / 2);
                } else {
                    modal.css("left", (window.innerWidth - modal.width()) / 2);
                }
            },
            beforeSubmit: function (postData, formid) {
                AlarmExclusionsMessages.msgreqExclusionType
                if (regConfig.RatingTable.mandatory && postData.RatingTable < 1) {
                    return [false, AlarmExclusionsMessages.msgreqRatingTable];
                }
                if (regConfig.ProductModule.mandatory && isModularProduct && postData.ProductModule < 1) {
                    return [false, AlarmExclusionsMessages.msgreqProductModule];
                }
                if (regConfig.CoverageCode.mandatory && postData.CoverageCode < 1) {
                    return [false, AlarmExclusionsMessages.msgreqCoverageCode];
                }
                if (regConfig.ImpairmentCode.mandatory && postData.ImpairmentCode < 1) {
                    return [false, AlarmExclusionsMessages.msgreqImpairmentCode];
                }
                if (regConfig.ExclusionPeriodType.mandatory && postData.ExclusionPeriodType < 1) {
                    return [false, AlarmExclusionsMessages.msgreqExclusionPeriodType];
                }
                var periods = ['WaitingPeriodDays', 'WaitingPeriodMonths', 'WaitingPeriodYears'];
                var res = 0;
                if (postData.ExclusionPeriodType == enuExclusionType.coverage) {
                    for (i = 0; i < periods.length; i++) res += $("#" + periods[i]).val();
                    if (res < 1) {
                        return [false, AlarmExclusionsMessages.msgreqPeriod];
                    }
                }
                return [true, ''];
            },
            afterSubmit: function (response, postdata) {
                if (response.success().statusText == "OK"
                    && response.success().status == 200
                    && response.success().readyState == 4) {
                    unloadChildGrids();
                    var seekIdRule = $.isNumeric($("#ddlUwRuleId").val()) ? $("#ddlUwRuleId").val() : -1;
                    LoadUnderwritingRuleAlarmsExclusions(idBranch, selectedAlarm.Product, seekIdRule);
                    return [true, ''];
                } else {
                    return [false, ''];
                }
            },
            url: "services/TabUnderwritingRules.aspx/EditRestriction"
        });
        var delSettingsAlarmExclusions = $.extend({}, GeneralDelSettings, {
            serializeDelData: function (postData) {
                var ret = JSON.stringify({
                    selectedAlarm: selectedAlarm,
                    dataRestriction: {
                        RestrictionId: selectedRestriction.RestrictionId,
                        ExclusionType: postData.ExclusionType == "-1" ? 0 : postData.ExclusionType,
                        RatingTable: postData.RatingTable == "" ? 0 : postData.RatingTable,
                        ProductModule: postData.ProductModule == "-1" ? 0 : postData.ProductModule,
                        CoverageCode: postData.CoverageCode == "-1" ? 0 : postData.CoverageCode,
                        ImpairmentCode: postData.ImpairmentCode == "" ? "" : postData.ImpairmentCode,
                        Cause: postData.Cause == "-1" ? 0 : postData.Cause,
                        ExclusionPeriodType: postData.ExclusionPeriodType == "-1" ? 0 : postData.ExclusionPeriodType,
                        WaitingPeriodDays: postData.WaitingPeriodDays == "" ? 0 : postData.WaitingPeriodDays,
                        WaitingPeriodMonths: postData.WaitingPeriodMonths == "" ? 0 : postData.WaitingPeriodMonths,
                        WaitingPeriodYears: postData.WaitingPeriodYears == "" ? 0 : postData.WaitingPeriodYears,
                        RestrictionType: 1
                    }
                });
                return ret;
            },
            afterSubmit: function (response, postdata) {
                var res = JSON.parse(response.success().responseText).d;
                if (res.length > 0) {
                    $loading.hide();
                    return [false, res.d];
                }
                gridtblExclusion.delRowData(postdata.id);
                validateExclusion = true;
                if (sessionStorage.getItem('cantExclusionesAValidar') != null) {
                    cantExclusionesAValidar = parseInt(sessionStorage.getItem('cantExclusionesAValidar')) + 1;
                    sessionStorage.setItem("cantExclusionesAValidar", cantExclusionesAValidar);
                } else {
                    sessionStorage.setItem("cantExclusionesAValidar", 1);
                }
                LoadUnderwritingRuleAlarms(0);
                var seekIdRule = $.isNumeric($("#ddlUwRuleId").val()) ? $("#ddlUwRuleId").val() : -1;
                // unloadChildGrids();
                //LoadUnderwritingRuleAlarmsExclusions(idBranch, idProduct, seekIdRule);
                return [true, ''];
            },
            url: "services/TabUnderwritingRules.aspx/DeleteRestriction"
        });

        gridtblExclusion.jqGrid("navGrid", "#pager-tblExclusion", {
            edit: isEditMode,
            add: isEditMode,
            del: isEditMode,
            search: false,
            rowList: [],
            pgbuttons: false,
            pgtext: null,
            viewrecords: false,
            refresh: false
        },
            editSettingsAlarmExclusions,
            addSettingsAlarmExclusions,
            delSettingsAlarmExclusions
        );

        gridtblExclusion.jqGrid("filterToolbar", {
            searchOnEnter: false,
            enableClear: false,
            searchOperators: false,
            defaultSearch: 'cn',
            autosearch: true
        });
        gridtblExclusion.trigger("reloadGrid");
        $("#loadingFog").hide();
    }, null, function () { $("#loadingFog").hide(); });
}


/* *************************************************** Función principal Grid de Restricciones Recargos y Descuentos (se dejó practicamente igual) ********************************** */

var DiscountTypeExtend;
var AlarmDiscoexpremMessages = {};

function LoadUnderwritingRuleAlarmsDiscoexprem(idBranch, idProduct, idRule) {
    $.jgrid.defaults.styleUI = 'Bootstrap';
    "use strict";
    // $.jgrid.gridUnload("tblDiscoexprem");
    $("#tblDiscoexprem").jqGrid("GridUnload");

    var gridColNamesTbUWRDiscoexprem = "";
    var gridColModelTbUWRDiscoexprem = "";
    var resdatagridruleAlarmDiscoexprems = "";
    var gridtblDiscoexprem = $("#tblDiscoexprem");
    var params = JSON.stringify({
        LineOfBusiness: $("#ddlBranch").val(),
        productCode: idProduct,
        languageID: $("#btnLanguage").children(".btn.dropdown").attr("data-value")
    });

    var params2 = JSON.stringify({
        selectedAlarm: selectedAlarm,
        languageId: $("#btnLanguage").children(".btn.dropdown").attr("data-value")
    });

    var tooltips = {};

    $.when(
        ProxyAsyncTabUnderwritingRules.invoke("GetHeaderValuesRuleAlarmsDiscoexprem", "", function (data) { gridColNamesTbUWRDiscoexprem = data.d; })
        , ProxyAsyncLookUps.invoke("GetAllDiscountOrExtraPremiumLkp", "", function (data) { DiscoexpremTypeList.arr = data.d; })
        , ProxyAsyncLookUps.invoke("GetAllBasicRatingByProductLkp", params, function (data) { RatingTable = data.d; })
        , ProxyAsyncLookUps.invoke("GetAllDiscoExPremByProductsLkp", params, function (data) { DiscountOrExtraPremiumTypeList.arr = data.d; })
        , ProxyAsyncLookUps.invoke("GetAllDiscoExPremTaxByProductLkp", params, function (data) { DiscoExPremTaxByProduc.arr = data.d; }) /* TODO: VALIDAR SI ESTO ES NECESARIO ya se maneja con DiscountOrExtraPremiumTypeList Y DiscoexpremTypeList */
        , ProxyAsyncLookUps.invoke("GetCurrencyLkp", "", function (data) { CurrencyList.arr = data.d; })
        , ProxyAsyncLookUps.invoke("GetAllExclusionPeriodType", "", function (data) { ExclusionPeriodList.arr = data.d; })
        , getModulesList(idProduct)
        , ProxyAsyncLookUps.invoke("GetReasonForExclusionOfIllnessLkp", "", function (data) { CauseList.arr = data.d; })
        , getCoverageList(idProduct, 0) /*TODO: validar este llamado con cero */
        , ProxyAsyncTabUnderwritingRules.invoke("GetAlarmDiscoexpremMessages", "", function (data) { AlarmDiscoexpremMessages = data.d; })
        , ProxyAsyncTabUnderwritingRules.invoke("RetrieveTabUnderwritingRuleAlarm", params2, function (data) { resdatagridruleAlarmDiscoexprems = data.d; })
        , ProxyAsyncTabUnderwritingRules.invoke("GetAlarmDiscoExpremToolTips", "", function (data) { tooltips = data.d; })
    ).then(function () {
        isModularProduct = ModuleList.arr.length > 0;

        gridColModelTbUWRDiscoexprem = [
            {
                name: 'Discountorextrapremiumcode', index: 'Discountorextrapremiumcode', resizable: true, editable: true, hidden: false, search: false,
                formatter: function (cellvalue, options, rowObject) {
                    return DiscountOrExtraPremiumTypeList.findByCode(cellvalue).Description;
                },
                edittype: 'select', editoptions: {
                    value: arrayToJqgrid(DiscountOrExtraPremiumTypeList.arr),
                    defaultValue: "-1",
                    dataEvents: [{
                        type: 'change', fn: function () {
                            var selectedDiscountorextrapremiumcode = $(this[this.selectedIndex]).val();
                            $("#DiscountorExtraPremiumType").val(selectedDiscountorextrapremiumcode > 0 ? DiscountOrExtraPremiumTypeList.arr[0].TypeOfItem : -1);
                            $("#CurrencyCode").val(selectedDiscountorextrapremiumcode > 0 ? DiscountOrExtraPremiumTypeList.arr[0].Currency : -1);
                            $("#ExtraPremiumPercentage").val(DiscountOrExtraPremiumTypeList.arr[0].ExtraPremiumPercentage > 0 ? DiscountOrExtraPremiumTypeList.arr[0].ExtraPremiumPercentage : "");
                            $("#FlatExtraPremium").val(DiscountOrExtraPremiumTypeList.arr[0].FixedAmount ? DiscountOrExtraPremiumTypeList.arr[0].FixedAmount : "");
                        }
                    }], title: tooltips.ttipDiscountorextrapremiumcode
                }
            },
            {
                name: 'DiscountorExtraPremiumType', index: 'DiscountorExtraPremiumType', resizable: true, editable: true, hidden: false, search: false,
                formatter: function (cellvalue, options, rowObject) {
                    return DiscoexpremTypeList.findByCode(cellvalue).Description;
                },
                edittype: 'select', editoptions: {
                    value: arrayToJqgrid(DiscoexpremTypeList.arr), defaultValue: "-1", disabled: true, title: tooltips.ttipDiscountorExtraPremiumType
                }
            },
            {
                name: 'ExtraPremiumPercentage', index: 'ExtraPremiumPercentage', resizable: true, editable: true, hidden: false, search: false,
                editoptions: {
                    dataInit: function (e) {
                        e.style.textAlign = 'right';
                        e.value = e.value.length > 0 ? parseFloat(e.value).format(2, 3, currthousandsSeparator, currdecimalSeparator) : "";
                    }, title: tooltips.ttipExtraPremiumPercentage
                }, align: 'right',
                formatter: 'number'
            },
            {
                name: 'FlatExtraPremium', index: 'FlatExtraPremium', resizable: true, editable: true, hidden: false, search: false,
                editoptions: {
                    dataInit: function (e) {
                        e.style.textAlign = 'right';
                        e.value = e.value.length > 0 ? parseFloat(e.value).format(2, 3, currthousandsSeparator, currdecimalSeparator) : "";
                    }, title: tooltips.ttipFlatExtraPremium,
                    maxlength: 16
                }, align: 'right',
                formatter: 'number'
            },
            {
                name: 'CurrencyCode', index: 'CurrencyCode', resizable: true, editable: true, hidden: false, search: false,
                formatter: function (cellvalue, options, rowObject) {
                    return CurrencyList.findByCode(cellvalue).Description;
                },
                edittype: 'select', editoptions: {
                    value: arrayToJqgrid(CurrencyList.arr), defaultValue: "-1", disabled: true,
                    dataInit: function (e) {
                        e.style.textAlign = 'right';
                    }, title: tooltips.ttipCurrencyCode
                }, align: 'right'
            },
            {
                name: 'XPremiumDiscountOnlyInsured', index: 'XPremiumDiscountOnlyInsured', resizable: true, editable: true, hidden: false, search: false, edittype: 'checkbox',
                editoptions: {
                    value: "True:False", title: tooltips.ttipXPremiumDiscountOnlyInsured,
                    dataEvents: [{
                        type: 'change', fn: function (e) {
                            if (this.checked) {
                                //$("#TypeofUnit").prop("disabled", false);
                                $("#ExclusionPeriodType").prop("disabled", false);
                            } else {
                                //$("#TypeofUnit").prop("disabled", true);
                                $("#ExclusionPeriodType").prop("disabled", true);
                                //$('#TypeofUnit').prop('checked', false);
                                $("#ExclusionPeriodType").val(-1);
                                var controls = ["DOfFlatExtraPremiumDays", "DOfFlatExtraPremiumMonths", "DOfFlatExtraPremiumYears"];
                                for (i = 0; i < controls.length; i++) {
                                    $("#" + controls[i]).attr("disabled", ExclusionPeriodType == 2 ? false : true);
                                    $("#" + controls[i]).val("");
                                }
                            }
                        }
                    }]
                },
                formatter: "checkbox", formatoptions: { disabled: true }
            },
            {
                name: 'TypeofUnit', index: 'TypeofUnit', resizable: true, editable: true, hidden: false, search: false, edittype: 'checkbox',
                editoptions: {
                    value: "True:False", title: tooltips.ttipTypeOfUnit
                },
                formatter: "checkbox", formatoptions: { disabled: true }
            },
            {
                name: 'ExclusionPeriodType', index: 'ExclusionPeriodType', resizable: true, editable: true, hidden: false, search: false,
                formatter: function (cellvalue, options, rowObject) {
                    return ExclusionPeriodList.findByCode(cellvalue).Description;
                },
                edittype: 'select',
                editoptions: {
                    value: arrayToJqgrid(ExclusionPeriodList.arr),
                    defaultValue: "-1",
                    dataEvents: [{
                        type: 'change', fn: function () {
                            var ExclusionPeriodType = $(this[this.selectedIndex]).val();
                            var controls = ["DOfFlatExtraPremiumDays", "DOfFlatExtraPremiumMonths", "DOfFlatExtraPremiumYears"];
                            for (i = 0; i < controls.length; i++) {
                                $("#" + controls[i]).attr("disabled", ExclusionPeriodType == 2 ? false : true);
                                ExclusionPeriodType == 2 ? null : $("#" + controls[i]).val("");
                            }
                        }
                    }], title: tooltips.ttipExclusionPeriodType
                }
            },
            {
                name: 'DOfFlatExtraPremiumDays', index: 'DOfFlatExtraPremiumDays', resizable: true, editable: true, hidden: false, search: false, editoptions: {
                    disabled: true,
                    dataInit: function (e) {
                        e.style.textAlign = 'right';
                    }, title: tooltips.ttipDOfFlatExtraPremiumDays
                }, align: 'right',
                formatter: 'integer'
            },
            {
                name: 'DOfFlatExtraPremiumMonths', index: 'DOfFlatExtraPremiumMonths', resizable: true, editable: true, hidden: false, search: false, editoptions: {
                    disabled: true,
                    dataInit: function (e) {
                        e.style.textAlign = 'right';
                    }, title: tooltips.ttipDOfFlatExtraPremiumMonths
                }, align: 'right',
                formatter: 'integer'
            },
            {
                name: 'DOfFlatExtraPremiumYears', index: 'DOfFlatExtraPremiumYears', resizable: true, editable: true, hidden: false, search: false, editoptions: {
                    disabled: true,
                    dataInit: function (e) {
                        e.style.textAlign = 'right';
                    }, title: tooltips.ttipDOfFlatExtraPremiumYears
                }, align: 'right',
                formatter: 'integer'
            }
        ];

        gridtblDiscoexprem.jqGrid($.extend({}, GeneralGridOptions, {
            autowidth: true,
            height: '100%',
            shrinkToFit: true,
            width: 390,
            colNames: gridColNamesTbUWRDiscoexprem,
            colModel: gridColModelTbUWRDiscoexprem,
            datatype: "local",
            data: resdatagridruleAlarmDiscoexprems,
            pager: '#pager-tblDiscoexprem',
            subGrid: false,
            viewrecords: false,
            pgbuttons: true,
            onSelectRow: function (id) {
                var RestrictiondataFromTheRow = gridtblDiscoexprem.jqGrid('getRowData', id);
                var currRestriction = $.grep(resdatagridruleAlarmDiscoexprems, function (n, i) {
                    return n.id == id;
                })[0];
                selectedRestriction = {
                    Discountorextrapremiumcode: currRestriction.Discountorextrapremiumcode,
                    DiscountorExtraPremiumType: currRestriction.DiscountorExtraPremiumType,
                    ExtraPremiumPercentage: currRestriction.ExtraPremiumPercentage,
                    FlatExtraPremium: currRestriction.FlatExtraPremium,
                    CurrencyCode: currRestriction.CurrencyCode,
                    XPremiumDiscountOnlyInsured: currRestriction.XPremiumDiscountOnlyInsured,
                    TypeofUnit: currRestriction.TypeofUnit,
                    ExclusionPeriodType: currRestriction.ExclusionPeriodType,
                    DOfFlatExtraPremiumDays: currRestriction.DOfFlatExtraPremiumDays,
                    DOfFlatExtraPremiumMonths: currRestriction.DOfFlatExtraPremiumMonths,
                    DOfFlatExtraPremiumYears: currRestriction.DOfFlatExtraPremiumYears,
                    RestrictionId: currRestriction.RestrictionId
                };
            }
        }));

        var addSettingsAlarmDiscoexprem = $.extend({}, GeneralAddSettings, {
            width: 400,
            serializeEditData: function (postData) {
                var ret = JSON.stringify({
                    selectedAlarm: selectedAlarm,
                    dataRestriction: {
                        RestrictionId: selectedRestriction.RestrictionId == "-1" ? 0 : selectedRestriction.RestrictionId,
                        Discountorextrapremiumcode: postData.Discountorextrapremiumcode == "-1" ? 0 : postData.Discountorextrapremiumcode,
                        DiscountorExtraPremiumType: postData.DiscountorExtraPremiumType == "-1" || postData.DiscountorExtraPremiumType == null ? 0 : postData.DiscountorExtraPremiumType.replace(/,/g, "."),
                        ExtraPremiumPercentage: postData.ExtraPremiumPercentage == "" ? 0 : postData.ExtraPremiumPercentage.replace(/[.]/g, "").replace(/,/g, "."),
                        FlatExtraPremium: postData.FlatExtraPremium == "" ? 0 : postData.FlatExtraPremium.replace(/[.]/g, "").replace(/,/g, "."),
                        CurrencyCode: postData.CurrencyCode == "-1" ? 0 : postData.CurrencyCode,
                        XPremiumDiscountOnlyInsured: postData.XPremiumDiscountOnlyInsured,
                        TypeofUnit: postData.TypeofUnit,
                        ExclusionPeriodType: postData.ExclusionPeriodType == "-1" ? 0 : postData.ExclusionPeriodType,
                        DOfFlatExtraPremiumDays: postData.DOfFlatExtraPremiumDays == "" ? 0 : postData.DOfFlatExtraPremiumDays,
                        DOfFlatExtraPremiumMonths: postData.DOfFlatExtraPremiumMonths == "" ? 0 : postData.DOfFlatExtraPremiumMonths,
                        DOfFlatExtraPremiumYears: postData.DOfFlatExtraPremiumYears == "" ? 0 : postData.DOfFlatExtraPremiumYears,
                        RestrictionType: 2
                    }
                });
                return ret;
            },
            afterShowForm: function (form) {
                var modal = form.parents(".ui-jqdialog");

                modal.css("position", "fixed");
                modal.css("top", 180);
                if (isExplorer()) {
                    modal.css("left", (window.innerWidth - modal.width()) / 2);
                } else {
                    modal.css("left", (window.innerWidth - modal.width()) / 2);
                }
            },
            beforeSubmit: function (postData, formid) {
                if (postData.Discountorextrapremiumcode < 1) {
                    return [false, AlarmDiscoexpremMessages.msgreqDiscountorextrapremiumcode];
                }
                if (postData.DiscountorExtraPremiumType < 1) {
                    return [false, AlarmDiscoexpremMessages.msgreqDiscountorExtraPremiumType];
                }
                if (postData.ExtraPremiumPercentage < 1 && postData.FlatExtraPremium < 1) {
                    return [false, AlarmDiscoexpremMessages.msgreqExtraPremiumPercentage];
                }
                if (isProductLife && postData.XPremiumDiscountOnlyInsured === "True") {
                    if (postData.ExclusionPeriodType < 1) {
                        return [false, AlarmDiscoexpremMessages.msgreqExclusionPeriodTypeDisco];
                    }
                    var periods = ['DOfFlatExtraPremiumDays', 'DOfFlatExtraPremiumMonths', 'DOfFlatExtraPremiumYears'];
                    var res = 0;
                    if (postData.ExclusionPeriodType == 2) {
                        for (i = 0; i < periods.length; i++) res += $("#" + periods[i]).val();
                        if (res < 1) {
                            return [false, AlarmDiscoexpremMessages.msgreqPeriod];
                        }
                    }
                }
                return [true, ''];
            },
            beforeShowForm: function (formid) {
                if (!isProductLife) {
                    $("#tr_XPremiumDiscountOnlyInsured").addClass('hide');
                    $("#tr_TypeofUnit").addClass('hide');
                    $("#tr_ExclusionPeriodType").addClass('hide');
                    $("#tr_DOfFlatExtraPremiumDays").addClass('hide');
                    $("#tr_DOfFlatExtraPremiumMonths").addClass('hide');
                    $("#tr_DOfFlatExtraPremiumYears").addClass('hide');
                }
                $("#TypeofUnit").prop("disabled", false);
                $("#ExclusionPeriodType").prop("disabled", true);
                return [true, ''];
            },
            afterSubmit: function (response, postdata) {
                if (response.success().statusText == "OK"
                    && response.success().status == 200
                    && response.success().readyState == 4
                    && JSON.parse(response.success().responseText).d == "") {
                    unloadChildGrids();
                    var seekIdRule = $.isNumeric($("#ddlUwRuleId").val()) ? $("#ddlUwRuleId").val() : -1;
                    LoadUnderwritingRuleAlarmsDiscoexprem(selectedAlarm.LineOfBusiness, selectedAlarm.Product, seekIdRule);
                    validateDescoexprem = false;
                    if (sessionStorage.getItem('cantDescuentosAValidar') != null) {
                        cantDescuentos = parseInt(sessionStorage.getItem('cantDescuentosAValidar')) - 1;
                        sessionStorage.setItem("cantDescuentosAValidar", cantDescuentos);
                    } else {
                        sessionStorage.setItem("cantDescuentosAValidar", 0);
                    }
                    return [true, ''];
                } else {
                    return [false, JSON.parse(response.success().responseText).d];
                }
            },
            url: "services/TabUnderwritingRules.aspx/AddRestriction"
        });
        var editSettingsAlarmDiscoexprem = $.extend({}, GeneralAddSettings, {
            width: 400,
            serializeEditData: function (postData) {
                var ret = JSON.stringify({
                    selectedAlarm: selectedAlarm,
                    dataRestriction: {
                        RestrictionId: selectedRestriction.RestrictionId == "-1" ? 0 : selectedRestriction.RestrictionId,
                        Discountorextrapremiumcode: postData.Discountorextrapremiumcode == "-1" ? 0 : postData.Discountorextrapremiumcode,
                        DiscountorExtraPremiumType: postData.DiscountorExtraPremiumType == "-1" ? 0 : postData.DiscountorExtraPremiumType.replace(/,/g, "."),
                        ExtraPremiumPercentage: postData.ExtraPremiumPercentage == "" ? 0 : postData.ExtraPremiumPercentage.replace(/[.]/g, "").replace(/,/g, "."),
                        FlatExtraPremium: postData.FlatExtraPremium == "" ? 0 : postData.FlatExtraPremium.replace(/[.]/g, "").replace(/,/g, "."),
                        CurrencyCode: postData.CurrencyCode == "-1" ? 0 : postData.CurrencyCode,
                        XPremiumDiscountOnlyInsured: postData.XPremiumDiscountOnlyInsured,
                        TypeofUnit: postData.TypeofUnit,
                        ExclusionPeriodType: postData.ExclusionPeriodType == "-1" ? 0 : postData.ExclusionPeriodType,
                        DOfFlatExtraPremiumDays: postData.DOfFlatExtraPremiumDays == "" ? 0 : postData.DOfFlatExtraPremiumDays,
                        DOfFlatExtraPremiumMonths: postData.DOfFlatExtraPremiumMonths == "" ? 0 : postData.DOfFlatExtraPremiumMonths,
                        DOfFlatExtraPremiumYears: postData.DOfFlatExtraPremiumYears == "" ? 0 : postData.DOfFlatExtraPremiumYears,
                        RestrictionType: 2
                    }
                });
                return ret;
            },
            beforeShowForm: function (formid) {
                $("#pData").addClass('hide');
                $("#nData").addClass('hide');
                $("#Discountorextrapremiumcode").prop("disabled", true);
                $("#TypeofUnit").prop("disabled", false);
                $("#ExclusionPeriodType").prop("disabled", true);
                if (!isProductLife) {
                    $("#tr_XPremiumDiscountOnlyInsured").addClass('hide');
                    $("#tr_TypeofUnit").addClass('hide');
                    $("#tr_ExclusionPeriodType").addClass('hide');
                    $("#tr_DOfFlatExtraPremiumDays").addClass('hide');
                    $("#tr_DOfFlatExtraPremiumMonths").addClass('hide');
                    $("#tr_DOfFlatExtraPremiumYears").addClass('hide');
                }
                return [true, ''];
            },
            afterShowForm: function (form) {
                var modal = form.parents(".ui-jqdialog");

                modal.css("position", "fixed");
                modal.css("top", 180);
                if (isExplorer()) {
                    modal.css("left", (window.innerWidth - modal.width()) / 2);
                } else {
                    modal.css("left", (window.innerWidth - modal.width()) / 2);
                }
            },
            beforeSubmit: function (postData, formid) {
                if (postData.Discountorextrapremiumcode < 1) {
                    return [false, AlarmDiscoexpremMessages.msgreqDiscountorextrapremiumcode];
                }
                if (postData.DiscountorExtraPremiumType < 1) {
                    return [false, AlarmDiscoexpremMessages.msgreqDiscountorExtraPremiumType];
                }
                if (postData.ExtraPremiumPercentage < 1 && postData.FlatExtraPremium < 1) {
                    return [false, AlarmDiscoexpremMessages.msgreqExtraPremiumPercentage];
                }
                if (isProductLife && postData.XPremiumDiscountOnlyInsured === "True") {
                    if (postData.ExclusionPeriodType < 1) {
                        return [false, AlarmDiscoexpremMessages.msgreqExclusionPeriodTypeDisco];
                    }
                    var periods = ['DOfFlatExtraPremiumDays', 'DOfFlatExtraPremiumMonths', 'DOfFlatExtraPremiumYears'];
                    var res = 0;
                    if (postData.ExclusionPeriodType == 2) {
                        for (i = 0; i < periods.length; i++) res += $("#" + periods[i]).val();
                        if (res < 1) {
                            return [false, AlarmDiscoexpremMessages.msgreqPeriod];
                        }
                    }
                }
                return [true, ''];
            },
            afterSubmit: function (response, postdata) {
                if (response.success().statusText == "OK"
                    && response.success().status == 200
                    && response.success().readyState == 4) {
                    unloadChildGrids();
                    var seekIdRule = $.isNumeric($("#ddlUwRuleId").val()) ? $("#ddlUwRuleId").val() : -1;
                    LoadUnderwritingRuleAlarmsDiscoexprem(selectedAlarm.LineOfBusiness, selectedAlarm.Product, seekIdRule);
                    return [true, ''];
                } else {
                    return [false, ''];
                }
            },
            url: "services/TabUnderwritingRules.aspx/EditRestriction"
        });
        var delSettingsAlarmDiscoexprem = $.extend({}, GeneralDelSettings, {
            serializeDelData: function (postData) {
                var ret = JSON.stringify({
                    selectedAlarm: selectedAlarm,
                    dataRestriction: selectedRestriction
                });
                return ret;
            },
            afterSubmit: function (response, postdata) {
                var res = JSON.parse(response.success().responseText).d;
                if (res.length > 0) {
                    $loading.hide();
                    return [false, res.d];
                }
                gridtblDiscoexprem.delRowData(postdata.id);
                validateDescoexprem = true;
                if (sessionStorage.getItem('cantDescuentosAValidar') != null) {
                    cantDescuentosAValidar = parseInt(sessionStorage.getItem('cantDescuentosAValidar')) + 1;
                    sessionStorage.setItem("cantDescuentosAValidar", cantDescuentosAValidar);
                } else {
                    sessionStorage.setItem("cantDescuentosAValidar", 1);
                }
                LoadUnderwritingRuleAlarms(0);
                return [true, ''];
            },
            url: "services/TabUnderwritingRules.aspx/DeleteRestriction"
        });

        gridtblDiscoexprem.jqGrid("navGrid", "#pager-tblDiscoexprem", {
            edit: isEditMode,
            add: isEditMode,
            del: isEditMode,
            search: false,
            rowList: [],
            pgbuttons: false,
            pgtext: null,
            viewrecords: false,
            refresh: false
        },
            editSettingsAlarmDiscoexprem,
            addSettingsAlarmDiscoexprem,
            delSettingsAlarmDiscoexprem
        );

        gridtblDiscoexprem.jqGrid("filterToolbar", {
            searchOnEnter: false,
            enableClear: false,
            searchOperators: false,
            defaultSearch: 'cn',
            autosearch: true
        });
        gridtblDiscoexprem.trigger("reloadGrid");
        $("#loadingFog").hide();
    }, null, function () {
        $("#loadingFog").hide();
    });
}

/* *************************************************** Función principal Grid de Restricciones Recargos y Descuentos (se dejó practicamente igual) ********************************** */

var AlarmMaxInsuredSumMessages = {};

function LoadUnderwritingRuleAlarmsMaxInsuredSum(idBranch, idProduct, idRule) {
    $.jgrid.defaults.styleUI = 'Bootstrap';
    "use strict";
    // $.jgrid.gridUnload("tblMaxInsuredSum");
    $("#tblMaxInsuredSum").jqGrid("GridUnload");
    var gridColNamesTbUWRMaxInsuredSum = "";
    var gridColModelTbUWRMaxInsuredSum = "";
    var resdatagridruleAlarmMaxInsuredSum = "";
    var gridtblMaxInsuredSum = $("#tblMaxInsuredSum");

    var params = JSON.stringify({
        LineOfBusiness: $("#ddlBranch").val(),
        productCode: idProduct,
        languageID: $("#btnLanguage").children(".btn.dropdown").attr("data-value")
    });
    var params2 = {
        selectedAlarm: selectedAlarm,
        languageId: $("#btnLanguage").children(".btn.dropdown").attr("data-value")
    };

    var tooltips = {};

    $.when(
        getModulesList(idProduct)
        , getModulesByProductList(idBranch, idProduct)
        , ProxyAsyncTabUnderwritingRules.invoke("GetHeaderValuesRuleAlarmsMaxInsuredSum", "", function (data) { gridColNamesTbUWRMaxInsuredSum = data.d; })
        , ProxyAsyncLookUps.invoke("GetCurrencyLkp", "", function (data) { CurrencyList.arr = data.d; })
        , ProxyAsyncLookUps.invoke("GetAllBasicRatingByProductLkp", params, function (data) { RatingTable = data.d; })
        , getCoverageList(idProduct, 0) /*TODO: validar este llamado con cero */
        , ProxyAsyncTabUnderwritingRules.invoke("GetAlarmMaxInsuredSumMessages", "", function (data) { AlarmMaxInsuredSumMessages = data.d; })
        , ProxyAsyncTabUnderwritingRules.invoke("RetrieveTabUnderwritingRuleAlarm", JSON.stringify(params2), function (data) { resdatagridruleAlarmMaxInsuredSum = data.d; })
        , ProxyAsyncTabUnderwritingRules.invoke("GetAlarmMaxInsuredSumToolTips", "", function (data) { tooltips = data.d; })
    ).then(function () {
        isModularProduct = ModuleList.arr.length > 0;

        gridColModelTbUWRMaxInsuredSum = [
            {
                name: 'ProductModule', index: 'ProductModule', resizable: true, editable: true, hidden: false, search: false,
                formatter: function (cellvalue, options, rowObject) {
                    return ModuleList.findByCode(cellvalue).Description;
                },
                edittype: 'select',
                editoptions: {
                    value: arrayToJqgrid(ModuleList.arr),
                    defaultValue: "-1",
                    disabled: !isModularProduct,
                    dataEvents: [{
                        type: 'change', fn: function (e) {
                            LoadCoverageByModule(e);
                        }
                    }], title: tooltips.ttipProductModule_mis
                }
            },
            {
                name: 'CoverageCode', index: 'CoverageCode', resizable: true, editable: true, hidden: false, search: false,
                formatter: function (cellvalue, options, rowObject) {
                    return CoverageList.findByCode(cellvalue).Description;
                },
                unformat: function (cellvalue, options, cell) {
                    return CoverageList.findByCode(selectedRestriction.CoverageCode).Description;
                },
                edittype: 'select',
                editoptions: {
                    value: arrayToJqgrid(CoverageList.arr),
                    defaultValue: "-1",
                    dataEvents: [{
                        type: 'change', fn: function () {
                            var selectedCoverageCode = $(this[this.selectedIndex]).val();
                            $("#CurrencyCode").val(CoverageList.findByCode(selectedCoverageCode).Currency);
                        }
                    }], title: tooltips.ttipCoverageCode_mis
                }
            },
            {
                name: 'MaximumInsuredAmount', index: 'MaximumInsuredAmount', resizable: true, editable: true, hidden: false, search: false,
                editoptions: {
                    defaultValue: RatingTable.length > 0 ? RatingTable[0].Limit : "",
                    dataInit: function (e) {
                        e.style.textAlign = 'right';
                        e.value = e.value.length > 0 ? parseFloat(e.value).format(2, 3, currthousandsSeparator, currdecimalSeparator) : "";
                    }, title: tooltips.ttipMaximumInsuredAmount,
                    maxlength: 16
                }, align: 'right',
                formatter: 'number'
            },
            {
                name: 'CurrencyCode', index: 'CurrencyCode', resizable: true, editable: true, hidden: false, search: false,
                formatter: function (cellvalue, options, rowObject) {
                    return CurrencyList.findByCode(cellvalue).Description;
                },
                edittype: 'select',
                editoptions: {
                    value: arrayToJqgrid(CurrencyList.arr),
                    defaultValue: CoverageList.arr > 0 ? (CoverageList.arr[0].Currency == 0 ? "-1" : CoverageList.arr[0].Currency) : "",
                    disabled: true, title: tooltips.ttipCurrencyCode_mis
                }
            }
        ];

        gridtblMaxInsuredSum.jqGrid($.extend({}, GeneralGridOptions, {
            autowidth: true,
            height: '100%',
            shrinkToFit: true,
            width: 390,
            data: resdatagridruleAlarmMaxInsuredSum,
            datatype: "local",
            colNames: gridColNamesTbUWRMaxInsuredSum,
            colModel: gridColModelTbUWRMaxInsuredSum,
            pager: '#pager-tblMaxInsuredSum',
            subGrid: false,
            viewrecords: false,
            pgbuttons: true,
            onSelectRow: function (id) {
                var RestrictiondataFromTheRow = gridtblMaxInsuredSum.jqGrid('getRowData', id);
                var currRestriction = $.grep(resdatagridruleAlarmMaxInsuredSum, function (n, i) {
                    return n.id == id;
                })[0];
                selectedRestriction = {
                    ProductModule: currRestriction.ProductModule,
                    CoverageCode: currRestriction.CoverageCode,
                    MaximumInsuredAmount: currRestriction.MaximumInsuredAmount,
                    CurrencyCode: currRestriction.CurrencyCode,
                    RestrictionId: currRestriction.RestrictionId
                };
            }
        }));

        var addSettingsAlarmMaxInsuredSum = $.extend({}, GeneralAddSettings, {
            width: 400,
            serializeEditData: function (postData) {
                var ret = JSON.stringify({
                    selectedAlarm: selectedAlarm,
                    dataRestriction: {
                        RestrictionId: selectedRestriction.RestrictionId == "-1" ? 0 : selectedRestriction.RestrictionId,
                        ProductModule: postData.ProductModule == "-1" ? 0 : postData.ProductModule,
                        CoverageCode: postData.CoverageCode == "-1" ? 0 : postData.CoverageCode,
                        MaximumInsuredAmount: postData.MaximumInsuredAmount == "" ? 0 : postData.MaximumInsuredAmount.replace(/[.]/g, "").replace(/,/g, "."),
                        CurrencyCode: postData.CurrencyCode == "-1" ? 0 : postData.CurrencyCode,
                        RestrictionType: 3
                    }
                });
                return ret;
            },
            afterShowForm: function (form) {
                var modal = form.parents(".ui-jqdialog");

                modal.css("position", "fixed");
                modal.css("top", 180);
                if (isExplorer()) {
                    modal.css("left", (window.innerWidth - modal.width()) / 2);
                } else {
                    modal.css("left", (window.innerWidth - modal.width()) / 2);
                }
            },
            beforeSubmit: function (postData, formid) {
                if (isModularProduct && postData.ProductModule < 1) {
                    return [false, AlarmMaxInsuredSumMessages.msgreqProductModule];
                }
                if (postData.CoverageCode < 1) {
                    return [false, AlarmMaxInsuredSumMessages.msgreqCoverageCode];
                }
                if (postData.MaximumInsuredAmount < 1) {
                    return [false, AlarmMaxInsuredSumMessages.msgreqMaximumInsuredAmount];
                }
                return [true, ''];
            },
            afterSubmit: function (response, postdata) {
                if (response.success().statusText == "OK"
                    && response.success().status == 200
                    && response.success().readyState == 4
                    && JSON.parse(response.success().responseText).d == "") {
                    unloadChildGrids();
                    var seekIdRule = $.isNumeric($("#ddlUwRuleId").val()) ? $("#ddlUwRuleId").val() : -1;
                    LoadUnderwritingRuleAlarmsMaxInsuredSum(selectedAlarm.LineOfBusiness, selectedAlarm.Product, seekIdRule);
                    validateMaxInsuredSum = false;
                    if (sessionStorage.getItem('cantMaxAseguradoAValidar') != null) {
                        cantMaxAseguradoAValidar = parseInt(sessionStorage.getItem('cantMaxAseguradoAValidar')) - 1;
                        sessionStorage.setItem("cantMaxAseguradoAValidar", cantMaxAseguradoAValidar);
                    } else {
                        sessionStorage.setItem("cantMaxAseguradoAValidar", 0);
                    }
                    return [true, ''];
                } else {
                    return [false, JSON.parse(response.success().responseText).d];
                }
            },
            url: "services/TabUnderwritingRules.aspx/AddRestriction"
        });
        var editSettingsAlarmMaxInsuredSum = $.extend({}, GeneralAddSettings, {
            width: 400,
            serializeEditData: function (postData) {
                var ret = JSON.stringify({
                    selectedAlarm: selectedAlarm,
                    dataRestriction: {
                        RestrictionId: selectedRestriction.RestrictionId == "-1" ? 0 : selectedRestriction.RestrictionId,
                        ProductModule: postData.ProductModule == "-1" ? 0 : postData.ProductModule,
                        CoverageCode: postData.CoverageCode == "-1" ? 0 : postData.CoverageCode,
                        MaximumInsuredAmount: postData.MaximumInsuredAmount == "" ? 0 : postData.MaximumInsuredAmount.replace(/[.]/g, "").replace(/,/g, "."),
                        CurrencyCode: postData.CurrencyCode == "-1" ? 0 : postData.CurrencyCode,
                        RestrictionType: 3
                    }
                });
                return ret;
            },
            beforeShowForm: function (formid) {
                $("#pData").addClass('hide');
                $("#nData").addClass('hide');
                $("#ProductModule").prop("disabled", true);
                $("#CoverageCode").prop("disabled", true);
                return [true, ''];
            },
            afterShowForm: function (form) {
                var modal = form.parents(".ui-jqdialog");

                modal.css("position", "fixed");
                modal.css("top", 180);
                if (isExplorer()) {
                    modal.css("left", (window.innerWidth - modal.width()) / 2);
                } else {
                    modal.css("left", (window.innerWidth - modal.width()) / 2);
                }
            },
            beforeSubmit: function (postData, formid) {
                if (isModularProduct && postData.ProductModule < 1) {
                    return [false, AlarmMaxInsuredSumMessages.msgreqProductModule];
                }
                if (postData.CoverageCode < 1) {
                    return [false, AlarmMaxInsuredSumMessages.msgreqCoverageCode];
                }
                if (postData.MaximumInsuredAmount < 1) {
                    return [false, AlarmMaxInsuredSumMessages.msgreqMaximumInsuredAmount];
                }
                return [true, ''];
            },
            afterSubmit: function (response, postdata) {
                if (response.success().statusText == "OK"
                    && response.success().status == 200
                    && response.success().readyState == 4) {
                    unloadChildGrids();
                    var seekIdRule = $.isNumeric($("#ddlUwRuleId").val()) ? $("#ddlUwRuleId").val() : -1;
                    LoadUnderwritingRuleAlarmsMaxInsuredSum(selectedAlarm.LineOfBusiness, selectedAlarm.Product, seekIdRule);
                    return [true, ''];
                } else {
                    return [false, ''];
                }
            },
            url: "services/TabUnderwritingRules.aspx/EditRestriction"
        });
        var delSettingsAlarmMaxInsuredSum = $.extend({}, GeneralDelSettings, {
            serializeDelData: function (postData) {
                var ret = JSON.stringify({
                    selectedAlarm: selectedAlarm,
                    dataRestriction: selectedRestriction
                });
                return ret;
            },
            afterSubmit: function (response, postdata) {
                var res = JSON.parse(response.success().responseText).d;
                if (res.length > 0) {
                    $loading.hide();
                    return [false, res.d];
                }
                gridtblMaxInsuredSum.delRowData(postdata.id);
                validateMaxInsuredSum = true;
                if (sessionStorage.getItem('cantMaxAseguradoAValidar') != null) {
                    cantMaxAseguradoAValidar = parseInt(sessionStorage.getItem('cantMaxAseguradoAValidar')) + 1;
                    sessionStorage.setItem("cantMaxAseguradoAValidar", cantMaxAseguradoAValidar);
                } else {
                    sessionStorage.setItem("cantMaxAseguradoAValidar", 1);
                }
                LoadUnderwritingRuleAlarms(0);
                return [true, ''];
            },
            url: "services/TabUnderwritingRules.aspx/DeleteRestriction"
        });

        gridtblMaxInsuredSum.jqGrid("navGrid", "#pager-tblMaxInsuredSum", {
            edit: isEditMode,
            add: isEditMode,
            del: isEditMode,
            search: false,
            rowList: [],
            pgbuttons: false,
            pgtext: null,
            viewrecords: false,
            refresh: false
        },
            editSettingsAlarmMaxInsuredSum,
            addSettingsAlarmMaxInsuredSum,
            delSettingsAlarmMaxInsuredSum);

        gridtblMaxInsuredSum.jqGrid("filterToolbar", {
            searchOnEnter: false,
            enableClear: false,
            searchOperators: false,
            defaultSearch: 'cn',
            autosearch: true
        });
        gridtblMaxInsuredSum.trigger("reloadGrid");
        $("#loadingFog").hide();
    }, null, function () { $("#loadingFog").hide(); });
}



/* **************************  Inicialización de controles correspondientes a campos del formulario **************************** */

var Control_List = /**/["txtEfecDate"
                      /**/, "ddlUwStatus"        /**/, "txtEnfermedad"        /**/, "txtExplanation"
                      /**/, "ddlNivelEnfermedad" /**/, "ddlUwArea"            /**/, "txtPoints"
                      /**/, "slider"             /**/, "ddlBranch"            /**/, "ddlCaseType"
                      /**/, "ddlReqType"         /**/, "ddlReqStatus"         /**/, "ddlReqQuestion"
                      /**/, "seekddlUwRule"];

var Control_List_Enable = {
    whenAdding:    /**/[1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0],
    whenDeleting:  /**/[0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1],
    whenEditing:   /**/[1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0],
    whenCanceling: /**/[0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1],
    whenSaving:    /**/[0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
    whenSearching: /**/[0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1],
    whenLoading:   /**/[0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1]
}

var Control_List_Clear = {
    whenAdding:    /**/[1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
    whenDeleting:  /**/[0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
    whenEditing:   /**/[0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
    whenCanceling: /**/[1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
    whenSaving:    /**/[0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
    whenSearching: /**/[0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
    whenLoading:   /**/[0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
}

var Control_List_Required = {
    whenAdding:    /**/[0, 1, 0, 1, 0, 1, 0, 0, 1, 0, 1, 1, 1],
    whenUpdating:  /**/[0, 1, 0, 1, 0, 1, 0, 0, 1, 0, 1, 1, 1],
    whenCanceling: /**/[0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
}

var Control_List_Default = {
    whenAdding:    /**/[null, null, null, null, null, null, null, null, null, null, null, null, null, null, null],
    whenUpdating:  /**/[null, null, null, null, null, null, null, null, null, null, null, null, null, null, null],
    //whenCanceling: /**/[0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
}

/* **************************  Inicialización de controles correspondientes a botones del formulario **************************** */
var UWRules_Buttons_Enable = {
    /*| AddRule | DeleRule | Edit | Cancel | Save | x | x | x | x | */
    whenAdding:    /**/[0, 0, 0, 1, 1],
    whenDeleting:  /**/[0, 0, 0, 0, 0],
    whenEditing:   /**/[0, 1, 0, 1, 1],
    whenCanceling: /**/[1, 0, 0, 0, 0],
    whenSaving:    /**/[0, 0, 0, 0, 0],
    whenSearching: /**/[0, 1, 1, 1, 0],
    whenLoading:   /**/[1, 0, 0, 0, 0]
}


/* **************************  Funciones básicas para manejo de controles correspondientes a campos del formulario **************************** */

function enableNewRule() {
    $("#newRuleLabel").show();
    $("#newRuleTextInput").show();
    $("#editRule").hide();
    $("#editRuleLabel").hide();
    $("#seekddlUwRule").hide();
    $("#ddlUwRuleId").val("");
    $("#ddlUwRuleDescription").val("");
    $("#ddlUwRuleDescription").attr("required", true);
}

function enableUpdateRule(ruleDescription) {
    $("#newRuleLabel").show();
    $("#newRuleTextInput").show();
    $("#editRule").hide();
    $("#editRuleLabel").hide();
    $("#seekddlUwRule").hide();
    $("#ddlUwRuleDescription").attr("required", true);
    $("#ddlUwRuleDescription").val(ruleDescription)
}

function enableEditRule() {
    $("#newRuleLabel").hide();
    $("#newRuleTextInput").hide();
    $("#editRule").show();
    $("#editRuleLabel").show();
    $("#seekddlUwRule").show();
    $("#ddlUwRule").val("");
    $("#ddlUwRule").removeClass("disabled");
    $("#ddlUwRule").prop('disabled', false);
    $("#ddlUwRuleDescription").attr("required", false);
}

function enableControlList(arr) {
    for (i = 0; i < arr.length; i++) {
        try {
            $("#" + Control_List[i])[0].disabled = !arr[i];
            if (arr[i])
                $("#" + Control_List[i]).removeClass("disabled");
            else
                $("#" + Control_List[i]).addClass("disabled");
        } catch (e) {
        }
    }
}

function clearControlList(arr) {
    for (i = 0; i < arr.length; i++) {
        arr[i] == 1 ? $("#" + Control_List[i])[0].value = "" : null;
    }
}

function SetRequiredControls(arr) {
    for (i = 0; i < arr.length; i++) {
        $("label[for='" + Control_List[i] + "']").removeClass("required");
        $("#" + Control_List[i]).removeAttr("required");
        if (arr[i] == 1) {
            $("#" + Control_List[i])[0].required = true;
            $("label[for='" + Control_List[i] + "']").addClass("required");
        }
    }
}

function setControlDefaultValues(arr) {
    for (i = 0; i < arr.length; i++) {
        arr[i] != null ? $("#" + Control_List[i])[0].value = arr[i] : null;
    }
}

/* **************************  Funciones básicas para manejo de controles correspondientes a botones del formulario **************************** */

function enableUWRulesButtons(arr) {
    var MainMenu = $(".btn", "#UWRules_ButtonSet");
    for (i = 0; i < arr.length; i++) {
        var src = $("img", "#" + MainMenu[i].id)[0].src;
        var pos = src.lastIndexOf('/');
        var path = src.substring(pos, 0);
        var imgname = src.substring(pos + 1).replace("disabled", "");
        var newsrc = "";
        if (arr[i] == 0) {
            newsrc = path + "/disabled" + imgname;
            $("#" + MainMenu[i].id).addClass("disabled");
        } else {
            newsrc = path + "/" + imgname;
            $("#" + MainMenu[i].id).removeClass("disabled");
        }
        $("img", "#" + MainMenu[i].id)[0].src = newsrc;
    }
}


function SaveRule() {
    var impairmentCode = 0;
    var newRule = {};
    newRule.UnderwritingRuleId = $("#ddlUwRuleId").val() == "" ? 0 : $("#ddlUwRuleId").val();
    newRule.Description = $("#ddlUwRuleDescription").val() == "-1" ? 0 : $("#ddlUwRuleDescription").val();
    if ($.grep(["", null, undefined, "-1"], function (n) { return n == eval($("#txtEfecDate").val()) }).length == 0)
        newRule.EffectiveDate = getNullDateIfEmpty($("#txtEfecDate").val());
    newRule.UnderwritingRuleStatus = $("#ddlUwStatus").val() == "-1" ? 0 : $("#ddlUwStatus").val();
    if ($("#txtEnfermedad").val() != "") newRule.ImpairmentCode = $("#txtEnfermedad").val().split('|')[0].trim();
    if ($("#txtExplanation").val() != "") newRule.Explanation = $("#txtExplanation").val();
    /* TODO: agregar la validacion de campo vacio para los dropown que corresponda */
    newRule.DegreeId = (($.grep(["", null, undefined], function (n) { return n == eval($("#ddlNivelEnfermedad").val()) }).length > 0) ? 0 : $("#ddlNivelEnfermedad").val());
    newRule.UnderwritingArea = $("#ddlUwArea").val() == "-1" ? 0 : $("#ddlUwArea").val();
    newRule.MortalityDebits = $("#txtPoints").val() == "" ? 0 : $("#txtPoints").val();
    newRule.LineOfBusiness = $("#ddlBranch").val() == "-1" ? 0 : $("#ddlBranch").val();
    newRule.UnderwritingCaseType = (($.grep(["", null, undefined], function (n) { return n == eval($("#ddlCaseType").val()) }).length > 0) ? 0 : $("#ddlCaseType").val());
    /*RULE-REQUIREMENT-SECTION-DATA*/
    newRule.RequirementType = (($.grep(["", null, undefined], function (n) { return n == eval($("#ddlReqType").val()) }).length > 0) ? 0 : $("#ddlReqType").val());
    newRule.RequirementStatus = (($.grep(["", null, undefined], function (n) { return n == eval($("#ddlReqStatus").val()) }).length > 0) ? 0 : $("#ddlReqStatus").val());
    newRule.QuestionId = (($.grep(["", null, undefined], function (n) { return n == eval($("#ddlReqQuestion").val()) }).length > 0) ? 0 : $("#ddlReqQuestion").val());

    var TRANSUNDERWRITINGRULE = {};
    TRANSUNDERWRITINGRULE.UnderwritingRuleId = $("#ddlUwRuleId").val() == "" ? 0 : $("#ddlUwRuleId").val();
    TRANSUNDERWRITINGRULE.EffectiveDate = formattedDateValue($("#txtEfecDate").datepicker("getDate"));
    TRANSUNDERWRITINGRULE.LanguageId = $("#btnLanguage").children(".btn.dropdown").attr("data-value");
    TRANSUNDERWRITINGRULE.Description = $("#ddlUwRuleDescription").val() == "-1" ? 0 : $("#ddlUwRuleDescription").val();
    TRANSUNDERWRITINGRULE.Explanation = $("#txtExplanation").val() == "-1" ? 0 : $("#txtExplanation").val();

    var TRANSUNDERWRITINGRULEs = [];
    TRANSUNDERWRITINGRULEs[0] = TRANSUNDERWRITINGRULE;

    var postData = JSON.stringify({
        newRule: newRule, lstTransRule: TRANSUNDERWRITINGRULEs
    });

    ProxySyncTabUnderwritingRules.invoke("CreateTabUnderwritingRule", postData, function (response) {
        if (response.d != "0") {
            $(".divSuccess").html(AlarmMessages.msgSuccess.replace(/@rule@/g, response.d));
            $(".divSuccess").show();
            setTimeout(function () {
                $(".divSuccess").hide();
            }, 4000);
            cancelRule();
        } else {
            $(".divError").html(AlarmMessages.msgFailed);
            $(".divError").show();
            setTimeout(function () {
                $(".divError").hide();
            }, 4000);
        }
    });
}



function UpdateRule() {
    var impairmentCode = 0;
    var updRule = {};
    updRule.UnderwritingRuleId = $("#ddlUwRuleId").val() == "" ? 0 : $("#ddlUwRuleId").val();
    updRule.Description = $("#ddlUwRuleDescription").val() == "-1" ? 0 : $("#ddlUwRuleDescription").val();
    /* TODO: validar la fecha que se pasa segun el idioma (tanto server como client). */
    if ($("#txtEfecDate").val() != "") updRule.EffectiveDate = getNullDateIfEmpty($("#txtEfecDate").val());
    updRule.UnderwritingRuleStatus = $("#ddlUwStatus").val() == "-1" ? 0 : $("#ddlUwStatus").val();
    if ($("#txtEnfermedad").val() != "") updRule.ImpairmentCode = $("#txtEnfermedad").val().split('|')[0].trim();
    if ($("#txtExplanation").val() != "") updRule.Explanation = $("#txtExplanation").val();
    /* TODO: agregar la validacion de campo vacio para los dropown que corresponda */
    if ($.grep(["", null, undefined, "-1"], function (n) { return n == $("#ddlNivelEnfermedad").val() }).length == 0) updRule.DegreeId = $("#ddlNivelEnfermedad").val();
    updRule.UnderwritingArea = $("#ddlUwArea").val() == "-1" ? 0 : $("#ddlUwArea").val();
    updRule.MortalityDebits = $("#txtPoints").val() == "" ? 0 : $("#txtPoints").val();
    updRule.LineOfBusiness = $("#ddlBranch").val() == "-1" ? 0 : $("#ddlBranch").val();
    updRule.UnderwritingCaseType = (($.grep(["", null, undefined], function (n) { return n == eval($("#ddlCaseType").val()) }).length > 0) ? 0 : $("#ddlCaseType").val());
    /*RULE-REQUIREMENT-SECTION-DATA*/
    updRule.RequirementType = (($.grep(["", null, undefined], function (n) { return n == eval($("#ddlReqType").val()) }).length > 0) ? 0 : $("#ddlReqType").val());
    updRule.RequirementStatus = (($.grep(["", null, undefined], function (n) { return n == eval($("#ddlReqStatus").val()) }).length > 0) ? 0 : $("#ddlReqStatus").val());
    updRule.QuestionId = (($.grep(["", null, undefined], function (n) { return n == eval($("#ddlReqQuestion").val()) }).length > 0) ? 0 : $("#ddlReqQuestion").val());

    var TRANSUNDERWRITINGRULE = {};
    TRANSUNDERWRITINGRULE.UnderwritingRuleId = $("#ddlUwRuleId").val() == "" ? 0 : $("#ddlUwRuleId").val();
    TRANSUNDERWRITINGRULE.EffectiveDate = formattedDateValue($("#txtEfecDate").datepicker("getDate"));
    TRANSUNDERWRITINGRULE.LanguageId = $("#btnLanguage").children(".btn.dropdown").attr("data-value");
    if (($("#ddlUwRule").is(":visible")))
        TRANSUNDERWRITINGRULE.Description = $("#ddlUwRule").val();
    else
        TRANSUNDERWRITINGRULE.Description = $("#ddlUwRuleDescription").val();
    TRANSUNDERWRITINGRULE.Explanation = $("#txtExplanation").val();

    var TRANSUNDERWRITINGRULEs = [];
    TRANSUNDERWRITINGRULEs[0] = TRANSUNDERWRITINGRULE;

    var postData = JSON.stringify({
        updRule: updRule, lstTransRule: TRANSUNDERWRITINGRULEs
    });

    var resultUnderwriting = "";
    ProxySyncTabUnderwritingRules.invoke("UpdateTabUnderwritingRule", postData, function (data) {
        if (data.d == "") {
            $(".divSuccess").html(AlarmMessages.msgSuccessfull.replace(/@rule@/g, ""));
            $(".divSuccess").show();
            setTimeout(function () {
                $(".divSuccess").hide();
            }, 4000);
            cancelRule();
        } else {
            $(".divError").html(data.d);
            $(".divError").show();
        }
    });
    $('#confirm_update_rule').modal('toggle');
}

function DeleteRule() {
    var delRule = {};
    delRule.UnderwritingRuleId = $("#ddlUwRuleId").val() == "" ? 0 : $("#ddlUwRuleId").val();
    delRule.Description = $("#ddlUwRuleDescription").val() == "-1" ? 0 : $("#ddlUwRuleDescription").val();
    if ($("#txtEfecDate").val() != "") /* TODO: validar la fecha que se pasa segun el idioma (tanto server como client). */
        delRule.EffectiveDate = getNullDateIfEmpty($("#txtEfecDate").val());

    var postData = JSON.stringify({ delRule: delRule });

    ProxySyncTabUnderwritingRules.invoke("DeleteTabUnderwritingRule", postData, function (data) {
        if (data.d == "") {
            $(".divSuccess").html(AlarmMessages.msgSuccessfull.replace(/@rule@/g, ""));
            $(".divSuccess").show();
            setTimeout(function () {
                $(".divSuccess").hide();
            }, 4000);
            cancelRule();
        } else {
            $(".divError").html(AlarmMessages.msgDeletedDenied);
            $(".divError").show();
        }
    });
    $('#confirm_delete_rule').modal('toggle');
}

function cancelRule() {
    selectedRule = { UnderwritingRuleId: "", EffectiveDate: "" };
    isNewRule = false;
    enableUWRulesButtons(UWRules_Buttons_Enable.whenCanceling);
    enableControlList(Control_List_Enable.whenCanceling);
    clearControlList(Control_List_Clear.whenCanceling);
    $("#ddlUwRuleId").val("");
    enableEditRule()
    SetRequiredControls(Control_List_Required.whenCanceling);
    ProxySyncTabUnderwritingRules.invoke("CleanListAlarms", "", function (data) { })
    cleanValidationsAlarms();
    // $.jgrid.gridUnload("tabAlarmList"); /* TODO: validar si esto se debe controlar aqui */
    $("#tabAlarmList").jqGrid("GridUnload");
    // $.jgrid.gridUnload("gvwUWRules");
    $("#gvwUWRules").jqGrid("GridUnload");
    unloadChildGrids();
    $(".divError").hide();
    validateExclusion = false;
    validateDescoexprem = false;
    validateMaxInsuredSum = false;
    filtReset();
}

function changeLanguage(obj) {
    var target = $(obj).parents(".btn-group").children(".btn.dropdown").children();
    var source = $(obj).children();
    target[0].src = source[0].src;
    target[1].innerHTML = btnLanguageText + source[1].innerHTML + " ";
    $($(target).parent()).attr("data-value", $(obj).attr("data-value"));
    GetTabUnderwritingRulesByIdRule(selectedRule.UnderwritingRuleId, selectedRule.EffectiveDate);
    LoadUnderwritingRuleAlarms(selectedRule.UnderwritingRuleId);
}

/* ***************************************************Carga Inicial********************************************* */

$(document).ready(function () {
    try {
        $.when(
            LoadDefaultValues("ddlUwStatus", "GetAllUnderwritingRuleStatusType", ProxyAsyncLookUps)
            , LoadDefaultValues("ddlNivelEnfermedad", "GetAllDegreeLkp", ProxyAsyncLookUps)
            , LoadDefaultValues("ddlUwArea", "GetUnderwritingAreaType", ProxyAsyncLookUps)
            , LoadDefaultValues("ddlBranch", "GetLineOfBussinesLkp", ProxyAsyncLookUps)
            , LoadDefaultValues("ddlCaseType", "GetUnderwritingCaseType", ProxyAsyncLookUps)
            , LoadDefaultValues("ddlReqType", "GetRequirementTypeActivesLkp", ProxyAsyncLookUps)
            , LoadDefaultValues("ddlReqStatus", "GetRequirementStatusType", ProxyAsyncLookUps)
            , LoadDefaultValues("ddlReqQuestion", "GetQuestionsFromRequirement", ProxyAsyncLookUps)
            //, ProxyAsyncLookUps.invoke("GetIllnessTypeLkp", "", function (data) {
            //    illness = [];
            //    $.each(data.d, function (index, item) {
            //        illness[item.Code] = item.Description;
            //    });
            //    illnessarray = mapObjectToArray(illness);
            //    $('#txtEnfermedad').autocomplete({
            //        source: illnessarray,
            //        minLength: 2,
            //        scroll: true,
            //        autoFocus: true,
            //    }).focus(function () {
            //        $(this).autocomplete("search", "");
            //    });
            //})
            , ProxyAsyncLookUps.invoke("GetProductLkp", "", function (data) {
                ProductList.ProductList = data.d; ProductList.ProductList.unshift({
                    ProductCode: "-1", ProductDescription: ""
                });
            })
            , ProxyAsyncLookUps.invoke("GetAlarmType", "", function (data) {
                AlarmTypeList.AlarmTypeList = data.d;
            })
            , ProxyAsyncLookUps.invoke("GetStageLkp", "", function (data) {
                stageList.arr = data.d;
            })
            , ProxyAsyncLookUps.invoke("GetDecisionTypeLkp", "", function (data) {
                DecisionList.DecisionList = data.d;
            })
            , ProxyAsyncLookUps.invoke("GetUnderwritingCaseStatusType", "", function (data) {
                StatusList.StatusList = data.d;
            })
            , ProxyAsyncLookUps.invoke("GetAllAllRestrictionTypeLkp", "", function (data) {
                RestrictionTypeList.arr = data.d;
            })
            , ProxyAsyncTabUnderwritingRules.invoke("GetDialogButtonsText", "", function (data) {
                DialogButtonsText = data.d;
            })
            , ProxyAsyncTabUnderwritingRules.invoke("GetTUWRMessages", "", function (data) { TUWRMessages = data.d; })
            , ProxyAsyncTabUnderwritingRules.invoke("getbtnLanguageText", "", function (data) { btnLanguageText = data.d; })
            , currdecimalSeparator = numLocate.toLocaleString().substring(5, 6)//$.jgrid.locales[getCultureName()].formatter.number.decimalSeparator
            , currthousandsSeparator = numLocate.toLocaleString().substring(1, 2)//$.jgrid.locales[getCultureName()].formatter.number.thousandsSeparator
            , dateTimeFormat = getDateTimeFormat()
            , dateTimeFormatShort = getDateTimeFormatShortPattern()

        ).then(function () {
            cleanValidationsAlarms();
            $("#loadingFog").hide();
            enableUWRulesButtons(UWRules_Buttons_Enable.whenLoading);
            enableControlList(Control_List_Enable.whenLoading);
            $("#loadingFog").hide();
            $("#dialog-confirm-save").dialog({
                autoOpen: false, resizable: false, height: "auto", width: 400, modal: true,
                buttons: [
                    {
                        text: DialogButtonsText.btnSave, icons: {
                            primary: "ui-icon-arrowthickstop-1-s"
                        }, click: function () {
                            UpdateRule(); $(this).dialog("close");
                        }
                    },
                    {
                        text: DialogButtonsText.btnCancel, icons: {
                            primary: "ui-icon-close"
                        }, click: function () {
                            $(this).dialog("close");
                        }
                    }
                ]
            });
            $("#dialog-confirm-delete").dialog({
                autoOpen: false, resizable: false, height: "auto", width: 400, modal: true,
                buttons: [
                    {
                        text: DialogButtonsText.btnDelete, icons: {
                            primary: "ui-icon-trash"
                        }, click: function () {
                            DeleteRule(); $(this).dialog("close");
                        }
                    },
                    {
                        text: DialogButtonsText.btnCancel, icons: {
                            primary: "ui-icon-close"
                        }, click: function () {
                            $(this).dialog("close");
                        }
                    }
                ]
            });
            $("#dialog-message").dialog({
                autoOpen: false, resizable: false, height: "auto", width: 400, modal: true,
                buttons: {
                    Ok: function () {
                        $(this).dialog("close");
                    }
                }
            });
            $("#dialog-alert").dialog({
                autoOpen: false, resizable: false, height: "auto", width: 400, modal: true,
                buttons: {
                    Ok: function () {
                        $(this).dialog("close");
                    }
                }
            });
            loadAutocompleteUwRule();
            if ($("#UnderRuleIDByRequest").val().length > 0) {
                getRuleOnLoadPage($("#UnderRuleIDByRequest").val());
            }

        });
    } catch (ex) {
        console.log("Excepciones varias " + ex);
        $("#loadingFog").hide();
    }

});


