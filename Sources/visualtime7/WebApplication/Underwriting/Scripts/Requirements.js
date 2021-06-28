var token = "";

var gridRequirement;

var ProcessType = ""
var ReceptionMaxDateValidationMsg = ""
var ReceptionMinDateValidationMsg = ""
var RequirementType = ""
var AlarmType = ""
var statusType = ""
var Payer = ""
var UnderwritingArea = ""
var RequestedTo = ""
var RequirementStatus = "";

var gridColNamesRequirement;
var gridColModelRequirement;
var previousRowIdRequirement = 0;
var ProvidersList = "";
var caseId;
var numCaso;
var childTabReq;
var intervalReq;
var editChild;

function checkChildReloadTabReq() {
    if (childTabReq.closed) {
        $loading.hide();
        clearInterval(intervalReq);
        var dataJson = JSON.stringify({ caseId: $('#dpeCaseId_I').val() });
        ProxyAsyncRequirement.invoke("UpdateCaseInstanceAfterAddRequirement", dataJson, function (data) { return; });
        if (editChild) {
            ReloadRequirementGrid("true");
        } else {
            ReloadRequirementGrid("false");
        }
    }
}

function SetRequirementinCache(requirementId) {
	var dataJson = JSON.stringify({ caseId: $('#dpeCaseId_I').val(), requirementID: requirementId });
    ProxySyncUnderwritingCase.invoke("SetCurrentRequirementID", dataJson, function (result) {
        return true;
    });
}

function controlEditInput(control, isEditMode) {
    isEditMode ? control.remove("disabled") : control.attr("disabled", "disabled");
}

function ReloadRequirementGrid(editProperty) {
    if ($('#dpeCaseId_I').val().length === 0) {
        numCaso = 0;
    } else {
        numCaso = $('#dpeCaseId_I').val();
    }
    InitControlRequirement(numCaso);
    // $.jgrid.gridUnload("grid-requirement");
    $("#grid-requirement").jqGrid("GridUnload");
    var isEditMode = false
    
    if (editProperty.toLowerCase() == "true") {
        isEditMode = true
    }
    var addSettings = $.extend({}, GeneralAddSettings, {
        width: 800,
        reloadAfterSubmit: true,
        serializeEditData: function (postData) {
        	typeof postData.ProviderId === "undefined" || postData.ProviderId === "" ? postData.ProviderId = 0 : postData.ProviderId;
        	postData.caseId = numCaso;
            return JSON.stringify(postData);
        },
        url: "services/Requirement.aspx/AddRequirementParameters"
    });

    var delSettings = $.extend({}, GeneralDelSettings, {
        serializeDelData: function (postData) {
            return JSON.stringify({  caseId: numCaso, requirementId: postData.id });
        },
        url: "services/Requirement.aspx/RemoveRequirement"
    });
    gridRequirement = $("#grid-requirement");
    gridRequirement.jqGrid($.extend({}, GeneralGridOptions, {
        url: "services/Requirement.aspx/GetAllRequirements",
        colNames: gridColNamesRequirement,
        colModel: gridColModelRequirement,
        serializeGridData: function (data) {
        	return JSON.stringify({ caseId: numCaso });
        	data = {};
        },
        pager: '#pager-requirement',
        subGrid: true,
        viewrecords: false,
        pgbuttons: false,
        subGridRowExpanded: function (subgridId, rowId) {
            // we pass two parameters
            // subgrid_id is a id of the div tag created within a table data
            // the id of this element is a combination of the "sg_" + id of the row
            // the row_id is the id of the row
            // If we wan to pass additional parameters to the URL we can use
            // a method getRowData(row_id) - which returns associative array in type name-value
            // here we can easy construct the flowing
        	SetRequirementinCache(rowId);
        	
            var subgridTableId, pagerId;
            subgridTableId = subgridId + "_t";
            pagerId = "p_" + subgridTableId;

            // Closes any other previous subGridRow
            if (previousRowIdRequirement !== 0) {
                $(this).collapseSubGridRow(previousRowIdRequirement);
            }
            // Saves the actual row_id
            previousRowIdRequirement = rowId;

            var selectedRequirementID = $("tr#" + rowId + " > td[aria-describedby=grid-requirement_RequirementID]").text();
            var selectedRequirementStatus = $("tr#" + rowId + " > td[aria-describedby=grid-requirement_Status]").text();
            var selectedRequirementTypeEnum = $("tr#" + rowId + " > td[aria-describedby=grid-requirement_RequirementType]").text();
            var selectedProcessTypeEnum = $("tr#" + rowId + " > td[aria-describedby=grid-requirement_ProcessTypeEnum]").text();
            var selectedAlarmTypeEnum = $("tr#" + rowId + " > td[aria-describedby=grid-requirement_AlarmTypeEnum]").text();
            var selectedPayerEnum = $("tr#" + rowId + " > td[aria-describedby=grid-requirement_PayerEnum]").text();
            var selectedUnderwritingAreaEnum = $("tr#" + rowId + " > td[aria-describedby=grid-requirement_UnderwritingAreaEnum]").text();
            var selectedRequestedTo = $("tr#" + rowId + " > td[aria-describedby=grid-requirement_RequestedTo]").text();
            var selectedClientID = $("tr#" + rowId + " > td[aria-describedby=grid-requirement_ClientId]").text();
            var selectedRequirementDate = $("tr#" + rowId + " > td[aria-describedby=grid-requirement_RequirementDate]").text();
            var selectedReceptionDate = $("tr#" + rowId + " > td[aria-describedby=grid-requirement_ReceptionDate]").text();
            var selectedTotalDebits = $("tr#" + rowId + " > td[aria-describedby=grid-requirement_TotalDebits]").text();
            var selectedTotalCredits = $("tr#" + rowId + " > td[aria-describedby=grid-requirement_TotalCredits]").text();
            var selectedBalance = $("tr#" + rowId + " > td[aria-describedby=grid-requirement_Balance]").text();
            var selectedCost = $("tr#" + rowId + " > td[aria-describedby=grid-requirement_Cost]").text();
            var selectedCostDueAmount = $("tr#" + rowId + " > td[aria-describedby=grid-requirement_CostDueAmount]").text();
            var selectedAcordRequirementCode = $("tr#" + rowId + " > td[aria-describedby=grid-requirement_AcordRequirementCode]").text();
            var selectedProviderId = $("tr#" + rowId + " > td[aria-describedby=grid-requirement_ProviderId]").text();
            var selectedAllowViewRequirement = $("tr#" + rowId + " > td[aria-describedby=grid-requirement_AllowViewRequirement]").text();
            var selectedAllowLoadRequirement = $("tr#" + rowId + " > td[aria-describedby=grid-requirement_AllowLoadRequirement]").text();
            
            if (selectedProviderId == "")
                selectedProviderId = 0;

            var selectedRequirementDateData = $.grep($("#grid-requirement").jqGrid('getGridParam', 'data'), function (obj) { return obj._id_ == rowId; })[0].RequirementDate;
            var selectedReceptionDateData = $.grep($("#grid-requirement").jqGrid('getGridParam', 'data'), function (obj) { return obj._id_ == rowId; })[0].ReceptionDate;

            // Renders all the HTML related to the requirement detail     
            $("#" + subgridId)
                .html("<table id='" + subgridTableId + "' class='scroll'></table><div id='" + pagerId + "' class='scroll'></div>")
                .load("controls/partials/_requirementsDetail.aspx", function () {
                    $(this).append();
                    $("#informacion-general-content").load("controls/partials/_informacionGeneral.aspx", function () {

                        if (!isEditMode)
                            $("#btn-actualizar-informacion-general").hide();

                        var param = JSON.stringify({ caseId: $('#dpeCaseId_I').val() });
                        LoadDefaultValuesWithParam("ddlProveedor", "GetAllProvidersInCaseLkp", ProxySyncLookUps, param).done(function () {
                            $("#ddlProveedor").val(selectedProviderId);
                            controlEditInput($("#ddlProveedor"), isEditMode);
                        });

                        LoadDefaultValues("ddlTipoDeRequerimiento", "GetRequirementType", ProxySyncLookUps).done(function () {
                            $("#ddlTipoDeRequerimiento").val(selectedRequirementTypeEnum);
                        });

                        LoadDefaultValues("ddlTipoDeProceso", "GetProcessType", ProxyAsyncLookUps).done(function () {
                            $("#ddlTipoDeProceso").val(selectedProcessTypeEnum);
                            controlEditInput($("#ddlTipoDeProceso"), isEditMode);
                        });

                        //LoadDefaultValues("ddlAlarma", "GetAlarmType", ProxyAsyncLookUps).done(function () {
                        //    $("#ddlAlarma").val(selectedAlarmTypeEnum);
                        //    controlEditInput($("#ddlAlarma"), isEditMode);
                        //});

                        LoadDefaultValues("ddlPagador", "GetPayableByType", ProxyAsyncLookUps).done(function () {
                            $("#ddlPagador").val(selectedPayerEnum);
                            controlEditInput($("#ddlPagador"), isEditMode);
                        });

                        LoadDefaultValues("ddlStatus", "GetRequirementStatusType", ProxyAsyncLookUps).done(function () {
                            $("#ddlStatus").val(selectedRequirementStatus);
                            controlEditInput($("#ddlStatus"), isEditMode);
                        });

                        LoadDefaultValues("ddlAreaDeSuscripcion", "GetUnderwritingAreaType", ProxyAsyncLookUps).done(function () {
                            $("#ddlAreaDeSuscripcion").val(selectedUnderwritingAreaEnum);
                            controlEditInput($("#ddlAreaDeSuscripcion"), false);
                        });

                        ProxySyncRoleInCase.invoke("GetAllRolesInCase", param, function (result) {
                            $.each(result.d, function (index, item) {
                                $("#ddlSolicitadoA").append($("<option />").val(this.ClientID).text(this.RoleNameByLanguage + " - " + this.ClientName));
                                controlEditInput($("#ddlSolicitadoA"), isEditMode);
                            });
                            $("#ddlSolicitadoA").val(selectedClientID);
                        });
                        
                        ProxySyncRequirement.invoke("GetReceptionMinDateValidation", "", function (data) { ReceptionMinDateValidationMsg = data.d });
                        ProxySyncRequirement.invoke("GetReceptionMaxDateValidation", "", function (data) { ReceptionMaxDateValidationMsg = data.d });

                        if (isEditMode) {
                        	ConvertToCalendar("#txtFechaDeSolicitud");
                        	ConvertToCalendar("#txtFechaDeRecepcion");
                        }
                        $('#ClientId').val(selectedClientID);

                        //Dates are retrieved from DB, dates that are null are converted to '01/01/1 12:00 AM' in vb as a date cannot be null. so, when we get a date with this value we show it as empty 
                        // la conversion de de la fecha nula no siempre la hace a 01/01/0001 hay casos donde la fecha viene 31/12/0000
                        if (new Date(parseInt( selectedRequirementDateData.substr(6))).getTime() > new Date(0001, 01, 01).getTime()) {
                            $("#txtFechaDeSolicitud").val(selectedRequirementDate);
                        }

                        if (new Date(parseInt(selectedReceptionDateData.substr(6))).getTime() > new Date(0001, 01, 01).getTime()) {
                            $("#txtFechaDeRecepcion").val(selectedReceptionDate);
                        }

                        $("#txtFechaDeSolicitud").datepicker("option", "disabled", true);
                        controlEditInput($("#txtFechaDeRecepcion"), isEditMode);
                        $("#txtRequirementID").val(selectedRequirementID);
                        $("#txtDebitos").val(selectedTotalDebits);
                        $("#txtCreditos").val(selectedTotalCredits);
                        $("#txtBalance").val(selectedBalance);
                        $("#txtCosto").val(selectedCost);
                        controlEditInput($("#txtCosto"), isEditMode);

                        $("#txtFaltaPorPagar").val(selectedCostDueAmount);
                        $("#txtCodigoAcord").val(selectedAcordRequirementCode);

                        var url = GetHostAndFormUrl();
                        var rowData = gridRequirement.getRowData(rowId);

                        //Botón Ver Documento       
                        if (selectedAllowViewRequirement == "1") {
                            link = rowData['Link'];
                            caselockedParam = "false";
                            //if (link.length > 0)
                            //    link = link.replace(/-/g, 'hi');
                            if (isEditMode){
                                caselockedParam = "true";
                            }
                            $("a#aVerDocumento").click(function () {
                                $("a#aVerDocumento").removeAttr("href");
                                window.open(url + link + "&frompanel=True&caselocked=" + caselockedParam);
                            });
                            //$("a#aVerDocumento").attr("href", url + link + "&frompanel=True&caselocked="+caselockedParam);
                        } else
                        {
                            $("a#aVerDocumento").attr("disabled", "disabled");
                            $("a#aVerDocumento").css("display", "none");
                            $('a#aVerDocumento').click(function () { return false; });
                        }

                            //Botón Completar Información Manual                        
                            if (isEditMode && selectedAllowLoadRequirement == "1") {
								caselockedParam = "false";
								 if (isEditMode){
									caselockedParam = "true";
								 }
								 $("a#aCargarInformacionManual").click(function () {
								     $("a#aCargarInformacionManual").removeAttr("href");
								     intervalReq = setInterval(checkChildReloadTabReq, 1000);
								     $loading.show();
								     editChild = isEditMode;
								     childTabReq = window.open(url + rowData['Link'] + "&frompanel=False&caselocked=" + caselockedParam);
								 });
                                //$("a#aCargarInformacionManual").attr("href", url + rowData['Link'] + "&frompanel=False&caselocked="+caselockedParam);
                            }
                            else {
                                $("a#aCargarInformacionManual").attr("disabled", "disabled");
                                $("a#aCargarInformacionManual").css("display", "none");
                                $('a#aCargarInformacionManual').click(function () { return false; });
                            }

                            if (selectedAllowViewRequirement != "1" && (selectedAllowLoadRequirement != "1" || ! isEditMode))
                            {
                                $("#txtEnlaces").css("display", "none");
                            }
                                
                            var requirementEdited = {};

                            //DateFormat dateformat = new SimpleDateFormat("dd/mm/yyyyy hh:mm:ss a");
                            //Date requirementDate = df.parse(selectedRequirementDate);
                            var requirementDateValues = selectedRequirementDate.split(" ");
                            var receptionDateValues = selectedReceptionDate.split(" ");

                            $("#btn-actualizar-informacion-general").click(function () {
                                var receptionDate = $("#txtFechaDeRecepcion").datepicker("getDate");
                                // Status: Recibido, no procesado aún - 3, Procesado - 4, Aceptado 9
                                if (($("#ddlStatus").val() == 3) || ($("#ddlStatus").val() == 9) || ($("#ddlStatus").val() == 4))
                                    $("#txtFechaDeRecepcion").attr("required", "required");
                                else
                                    $("#txtFechaDeRecepcion").removeAttr("required");

                                if (Date.parse(formattedDateValue(receptionDate)) > parseInt((new Date).getTime())) {
                                    $("#txtFechaDeRecepcion").attr("required", "required");
                                    $("#txtFechaDeRecepcion").val("");
                                }
                                if (Date.parse(formattedDateValue(receptionDate)) < Date.parse(formattedDateValue($("#txtFechaDeSolicitud").datepicker("getDate")))) {
                                    $("#txtFechaDeRecepcion").attr("required", "required");
                                    $("#txtFechaDeRecepcion").val("");
                                }
                                if (FormValidator($("#form-informacion-general"))) {
                                        requirementEdited = {
                                            RequirementId: rowData["RequirementID"],
                                            RequestedTo:  selectedRequestedTo,
                                            ProcessType: $("#ddlTipoDeProceso").val(), //selectedProcessTypeEnum,
                                            UnderwritingAreaEnum: $("#ddlAreaDeSuscripcion").val(), // selectedUnderwritingAreaEnum,
                                            RequirementDate: formattedDateValue($("#txtFechaDeSolicitud").datepicker("getDate")), //new Date(requirementDateWithoutHours), TODO, falta obtener las fechas, porque no lo esta tomando bien del txt. Las fechas las esta obteniendo vacío
                                            ReceptionDate: receptionDate != null ?formattedDateValue($("#txtFechaDeRecepcion").datepicker("getDate")):"", //new Date(receptionDateWithoutHours),
                                            //AlarmTypeEnum: $("#ddlAlarma").val(), //selectedAlarmTypeEnum,
                                            TotalDebits: $("#txtDebitos").val(), //selectedTotalDebits,
                                            TotalCredits: $("#txtCreditos").val(), //selectedTotalCredits,
                                            Balance: $("#txtBalance").val(), //selectedBalance,
                                            ProviderId: $("#ddlProveedor").val() == "" || $("#ddlProveedor").val() == null ? "0" : $("#ddlProveedor").val(), //selectedProviderId,
                                            PayerEnum: $("#ddlPagador").val(),// selectedPayerEnum,
                                            Cost: $("#txtCosto").val(), //selectedCost,
                                            CostDueAmount: $("#txtFaltaPorPagar").val(), //selectedCostDueAmount,
                                            AcordRequirementCode: $("#txtCodigoAcord").val(), //selectedAcordRequirementCode,
                                            Status: $("#ddlStatus").val(),
                                            ClientId: selectedClientID,
                                            UnderwritingCaseID: numCaso
                                        };

                                        var data = JSON.stringify({
                                            requirement: requirementEdited                                   
                                        });
                                        syncRegisterDialog(data, "EditRequirement", ProxyAsyncRequirement, $(".requirement-alert-success")); //TODO debuguear el proxy y verificar el porque no esta haciendo los cambios requeridos. Entra al método, pero no modifica nada
                                }
                                else {
                                    if (Date.parse(formattedDateValue(receptionDate)) > parseInt((new Date).getTime())) {
                                        $("span#txtFechaDeRecepcion-error.help-block").text(ReceptionMaxDateValidationMsg)
                                        $("#txtFechaDeRecepcion").removeAttr("required");
                                    }
                                    if (Date.parse(formattedDateValue(receptionDate)) < Date.parse(formattedDateValue($("#txtFechaDeSolicitud").datepicker("getDate")))) {
                                        $("span#txtFechaDeRecepcion-error.help-block").text(ReceptionMinDateValidationMsg)
                                        $("#txtFechaDeRecepcion").removeAttr("required");
                                    }
                                }
                            });

                        });

                    $("#reglas-de-suscripcion-content").load("controls/partials/_requirementSuscriptionRules.aspx", function () {
                        ReloadUnderwritingRulesGrid(isEditMode, selectedRequirementID, selectedRequirementStatus);
                    });

                    $("#anexos-content").html("<iframe id=\"case-attachment-requirement-iframe\" style=\"width:100%; min-height:500px; border:none\"></iframe>")

                    $("#case-attachment-requirement-iframe").attr("src", "controls/partials/_attachments.aspx?SentFromRequirementTab=Yes&caseId=" + $('#dpeCaseId_I').val());
                });
        },
    }));
    gridRequirement.jqGrid(
        "navGrid",
        "#pager-requirement",
        {
            edit: false,
            add: isEditMode,
            del: false,
            search: false,
            rowList: [],
            pgbuttons: false,
            pgtext: null,
            viewrecords: false,
            refresh: false
        },
        {},
        addSettings/*,
        delSettings*/
    );
    gridRequirement.jqGrid("filterToolbar", {        
        searchOnEnter: false,
        enableClear: false,
        searchOperators: false,
        defaultSearch: 'cn',
        autosearch: true        
    });
    gridRequirement.trigger("reloadGrid");
}

/*---------------------------------------------------- On Dom ready -----------------------------------------------------*/
function InitControlRequirement(casoId) {
    $.jgrid.defaults.styleUI = 'Bootstrap';
    "use strict";
    var data = JSON.stringify({ caseId: casoId });
    ProcessType = GetLookUps("GetProcessType");
    RequirementType = GetLookUpsWithParam("GetRequirementTypeActivesByRolesLkp", data);
    AlarmType = GetLookUps("GetAlarmType");
    statusType = GetLookUps("GetRequirementStatusType");
    Payer = GetLookUps("GetPayableByType");
    UnderwritingArea = GetLookUps("GetUnderwritingAreaType");
    RequirementStatus = GetLookUps("GetRequirementStatusType");
    ProvidersList = GetLookUpsWithParam("GetAllProvidersInCaseLkp", data);
        
    //Esta generando un error porque el método en el archivo del servicio recibe un parámetro Integer.
    //RequirementTypeByType = GetLookUps("GetRequirementTypeByType");

    if (ProvidersList == "") {
        ProvidersList = "0:";
    };

    var paramData = JSON.stringify({ caseId: casoId, requirementType: 0 });
    ProxySyncRoleInCase.invoke("GetAllRolesInCaseByRequirementType", paramData, function (result) {
        $.each(result.d, function (index, item) {
            if (typeof item.ClientName != "undefined" && item.ClientName != null) {
                RequestedTo = RequestedTo + item.ClientID + ":" + item.RoleNameByLanguage + " - " + item.ClientName + ";";
            }
            else {               
                RequestedTo = RequestedTo + "" + ":" + "" + ";";
            }
        });
        RequestedTo = RequestedTo.slice(0, -1);
    });
    ProxySyncRequirement.invoke("GetHeaderValues", "", function (data) { gridColNamesRequirement = data.d; });
        
    ProxySyncRequirement.invoke("GetToolTipValues", "", function (data) { toolTipValues = data.d; });

    gridColModelRequirement = [{ name: 'RequirementID', index: 'Status', editable: false, hidden: true, width: 1, viewable: false, editrules: { edithidden: true }, key: true },
                              { name: 'RequirementType', index: 'RequirementType', editable: true, hidden: true, width: 1, edittype: 'select', editoptions: { value: RequirementType, defaultValue: "0", dataEvents: [{ type: 'change', fn: onChange_RequirementType }], title: toolTipValues.RequirementType_ToolTip }, editrules: { required: true, edithidden: true }, formoptions: { rowpos: 1, colpos: 1 } },
                              { name: 'ProcessTypeEnum', index: 'ProcessTypeEnum', editable: true, hidden: true, width: 1, edittype: 'select', editoptions: { value: ProcessType, defaultValue: "0", disabled: true, title: toolTipValues.ProcessTypeEnum_ToolTip }, editrules: { required: true, edithidden: true }, formoptions: { rowpos: 1, colpos: 2 } },
                              { name: 'AlarmTypeEnum', index: 'AlarmTypeEnum', editable: true, hidden: true, width: 1, edittype: 'select', editoptions: { value: AlarmType, defaultValue: "0", disabled: true, title: toolTipValues.AlarmTypeEnum_ToolTip }, editrules: { required: true, edithidden: true }, formoptions: { rowpos: 2, colpos: 1 } },
                              { name: 'PayerEnum', index: 'PayerEnum', editable: true, hidden: true, width: 1, edittype: 'select', editoptions: { value: UnderwritingArea, defaultValue: "0", disabled: true, title: toolTipValues.PayerEnum_ToolTip }, editrules: { required: true, edithidden: true }, formoptions: { rowpos: 2, colpos: 2 } },
                              { name: 'UnderwritingAreaEnum', index: 'UnderwritingAreaEnum', editable: true, hidden: true, width: 1, edittype: 'select', editoptions: { value: UnderwritingArea, defaultValue: "0", disabled: true, title: toolTipValues.UnderwritingAreaEnum_ToolTip }, editrules: { required: true, edithidden: true }, formoptions: { rowpos: 3, colpos: 1 } },
                              { name: 'RequestedTo', index: 'RequestedTo', resizable: true, hidden: true, editable: true, width: 1, editoptions: { disabled: true, title: toolTipValues.RequestedTo_ToolTip }, editrules: { edithidden: false }, formoptions: { rowpos: 9, colpos: 1 } },
                              { name: 'ClientId', index: 'ClientId', editable: true, hidden: true, width: 1, edittype: 'select', editoptions: { value: RequestedTo, defaultValue: "0", disabled: true, title: toolTipValues.RequestedTo_ToolTip }, editrules: { required: true, edithidden: true }, formoptions: { rowpos: 3, colpos: 2 } },
                              { name: 'Cost', index: 'Cost', editable: true, hidden: true, width: 1, viewable: false, editrules: { required: true, edithidden: true }, editoptions: { dataInit: function (element) { DecimalMask(element) }, disabled: true, title: toolTipValues.Cost_ToolTip }, formoptions: { rowpos: 4, colpos: 2 } },
                              { name: 'Status', index: 'Status', resizable: true, editable: true, width: 1, hidden: true, edittype: 'select', editoptions: { value: statusType, defaultValue: "0", disabled: true, title: toolTipValues.Status_ToolTip }, editrules: { required: true, edithidden: true }, formoptions: { rowpos: 5, colpos: 1 } },
                              { name: 'StatusByLanguage', index: 'StatusByLanguage', resizable: true, editable: false, width: 125, editoptions: { disabled: true } },
                              { name: 'RequirementTypeEnumText', index: 'RequirementTypeEnumText', resizable: true, editable: false, width: 1, hidden: true, editoptions: { disabled: true} },
                              { name: 'RequirementTypeByLanguage', index: 'RequirementTypeByLanguage', resizable: true, editable: false, width: 210, editoptions: { disabled: true } },
							  { name: 'RequestedToByLanguage', index: 'RequestedToByLanguage', resizable: true, editable: false, width: 230, editoptions: { disabled: true } },
                              { name: 'RequirementDate', index: 'RequirementDate', resizable: true, editable: true, width: 115, sorttype: 'date', formatter: 'date', formatoptions: { newformat: 'd/m/Y' }, editrules: { custom: true, custom_func: ValidateDateField, required: true, edithidden: true }, editoptions: { dataInit: function (element) { ConvertToCalendar(element); $(element).datepicker("disable"); }, title: toolTipValues.RequirementDate_ToolTip }, formoptions: { rowpos: 5, colpos: 2 } },
							  {
							      name: 'ReceptionDate', index: 'ReceptionDate', editable: true, sorttype: 'date', formatoptions: { newformat: 'd/m/Y' }, width: 125, viewable: false, editrules: { custom: true, custom_func: ValidateDateField, required: true, edithidden: true }, editoptions: { dataInit: function (element) { ConvertToCalendar(element), $(element).datepicker("disable"); }, title: toolTipValues.ReceptionDate_ToolTip }, formoptions: { rowpos: 4, colpos: 1 },
							      formatter: function (cellvalue, options, rowobject) {
							          var dateValue = cellvalue.substring(cellvalue.indexOf('(') + 1, cellvalue.indexOf(')'));
							          var newDate = new Date(parseFloat(dateValue));
							          if (newDate.getFullYear() == 1 || newDate.getFullYear() == 0)
							              return '';
							          else
							              return convertdate(newDate);
							      }
							  },
                              { name: 'AlarmTypeEnumText', index: 'AlarmTypeEnumText', resizable: true, editable: false, width: 1, hidden: true, editoptions: { disabled: true } },
                              { name: 'AlarmTypeByLanguage', index: 'AlarmTypeByLanguage', resizable: true, editable: false, width: 150, hidden:true, editoptions: { disabled: true } },
                              { name: 'TotalDebits', index: 'TotalDebits', resizable: true, editable: true, width: 60, editrules: { required: true }, editoptions: { dataInit: function (element) { DecimalMask(element) }, disabled: true, title: toolTipValues.TotalDebits_ToolTip }, formoptions: { rowpos: 6, colpos: 1 } },
                              { name: 'TotalCredits', index: 'TotalCredits', resizable: true, editable: true, width: 60, editrules: { required: true }, editoptions: { dataInit: function (element) { DecimalMask(element) }, disabled: true, title: toolTipValues.TotalCredits_ToolTip }, formoptions: { rowpos: 6, colpos: 2 } },
                              { name: 'Balance', index: 'Balance', resizable: true, editable: true, width: 60, editrules: { required: true, edithidden: true }, editoptions: { dataInit: function (element) { DecimalMask(element) }, disabled: true, title: toolTipValues.Balance_ToolTip }, formoptions: { rowpos: 7, colpos: 1 } },
                              { name: 'CostDueAmount', index: 'CostDueAmount', hidden: true, resizable: true, editable: true, width: 90, editrules: { required: true, edithidden: true }, editoptions: { dataInit: function (element) { DecimalMask(element) }, disabled: true, title: toolTipValues.CostDueAmount_ToolTip }, formoptions: { rowpos: 7, colpos: 2 } },
                              { name: 'AcordRequirementCode', index: 'AcordRequirementCode', resizable: true, hidden: true, editable: true, width: 90, editrules: { required: true, edithidden: true }, formoptions: { rowpos: 8, colpos: 1 }, editoptions: { dataInit: function (element) { DecimalMask(element) }, disabled: true, title: toolTipValues.AcordRequirementCode_ToolTip } },
                              { name: 'ProviderId', index: 'ProviderId', resizable: true, editable: true, hidden: true, width: 150, edittype: 'select', editrules: { required: true, edithidden: true }, formoptions: { rowpos: 8, colpos: 2 }, editoptions: { value: ProvidersList, defaultValue: "0", disabled: true, title: toolTipValues.ProviderId_ToolTip } },
                              { name: 'Link', index: 'Link', resizable: true, hidden: true, editable: false, editoptions: { disabled: true } },
                              { name: 'AllowViewRequirement', index: 'AllowViewRequirement', hidden: true, resizable: false, editable: false, width: 150, editoptions: { disabled: true } },
                              { name: 'AllowLoadRequirement', index: 'AllowLoadRequirement', hidden: true, resizable: false, editable: false, width: 150, editoptions: { disabled: true } }];

}


    //Evento onChange del DropDownList RequirementType
    function onChange_RequirementType() {
        var resultRequirementType = "";
        var selectedRequirementTypeValue = $('#RequirementType').val();
        if (selectedRequirementTypeValue.length > 0) {
            $("#ClientId").val("");
            $("#ClientId").empty();
            RequestedTo = "";
            var paramData = JSON.stringify({ caseId: $('#dpeCaseId_I').val().length === 0 ? 0 : $('#dpeCaseId_I').val(), requirementType: selectedRequirementTypeValue });
            ProxySyncRoleInCase.invoke("GetAllRolesInCaseByRequirementType", paramData, function (result) {
                $.each(result.d, function (index, item) {
                    if (typeof item.ClientName != "undefined" && item.ClientName != null) {
                        $("#ClientId").append($("<option />").val(item.ClientID).text(item.RoleNameByLanguage + " - " + item.ClientName));
                        RequestedTo = RequestedTo + item.ClientID + ":" + item.RoleNameByLanguage + " - " + item.ClientName + ";";
                    }
                    else {
                        $("#ClientId").append($("<option />").val("").text(""));
                        RequestedTo = RequestedTo + "" + ":" + "" + ";";
                    }
                });
                RequestedTo = RequestedTo.slice(0, -1);
            });

            gridRequirement.trigger("reloadGrid");
            //Si el Tipo de Proceso es "Procesado por un WF automáticamente" , los campos (alarma, créditos, débitos, balance, proveedor, estado y  falta por pagar)...no se habilitan y no deben traer valor
            var fieldsProcessByWorkFlow = [{ name: 'RequirementType', disabled: false, required: true },
                                            { name: 'ProcessTypeEnum', disabled: true, required: true },
                                            { name: 'AlarmTypeEnum', disabled: true, required: false },
                                            { name: 'PayerEnum', disabled: true, required: true },
                                            { name: 'UnderwritingAreaEnum', disabled: true, required: true },
                                            { name: 'RequestedTo', disabled: false, required: false },
                                            { name: 'ClientId', disabled: false, required: true },
                                            { name: 'ReceptionDate', disabled: true, required: false },
                                            { name: 'Cost', disabled: true, required: true },
                                            { name: 'Status', disabled: true, required: false },
                                            { name: 'StatusByLanguage', disabled: false, required: false },
                                            { name: 'RequirementTypeEnumText', disabled: false, required: false },
                                            { name: 'RequirementTypeByLanguage', disabled: false, required: false },
                                            { name: 'RequirementDate', disabled: true, required: true },
                                            { name: 'AlarmTypeEnumText', disabled: true, required: false },
                                            { name: 'AlarmTypeByLanguage', disabled: true, required: false },
                                            { name: 'TotalDebits', disabled: true, required: false },
                                            { name: 'TotalCredits', disabled: true, required: false },
                                            { name: 'Balance', disabled: true, required: false },
                                            { name: 'CostDueAmount', disabled: true, required: false },
                                            { name: 'AcordRequirementCode', disabled: true, required: true },
                                            { name: 'ProviderId', disabled: true, required: false },
                                            { name: 'Link', disabled: false, required: false }];

            var fieldsProcessByHuman = [{ name: 'RequirementType', disabled: false, required: true },
                                        { name: 'ProcessTypeEnum', disabled: true, required: true },
                                        { name: 'AlarmTypeEnum', disabled: false, required: true },
                                        { name: 'PayerEnum', disabled: true, required: true },
                                        { name: 'UnderwritingAreaEnum', disabled: true, required: true },
                                        { name: 'RequestedTo', disabled: false, required: false },
                                        { name: 'ClientId', disabled: false, required: true },
                                        { name: 'ReceptionDate', disabled: false, required: false },
                                        { name: 'Cost', disabled: true, required: true },
                                        { name: 'Status', disabled: false, required: true },
                                        { name: 'StatusByLanguage', disabled: false, required: false },
                                        { name: 'RequirementTypeEnumText', disabled: false, required: false },
                                        { name: 'RequirementTypeByLanguage', disabled: false, required: false },
                                        { name: 'RequirementDate', disabled: true, required: true },
                                        { name: 'AlarmTypeEnumText', disabled: false, required: false },
                                        { name: 'AlarmTypeByLanguage', disabled: false, required: false },
                                        { name: 'TotalDebits', disabled: true, required: false },
                                        { name: 'TotalCredits', disabled: true, required: false },
                                        { name: 'Balance', disabled: true, required: false },
                                        { name: 'CostDueAmount', disabled: true, required: false },
                                        { name: 'AcordRequirementCode', disabled: true, required: false },
                                        { name: 'ProviderId', disabled: false, required: false },
                                        { name: 'Link', disabled: false, required: false }];


            var fieldsProcessByWorkFlowHuman = [{ name: 'RequirementType', disabled: false, required: true },
                                            { name: 'ProcessTypeEnum', disabled: true, required: true },
                                            { name: 'AlarmTypeEnum', disabled: true, required: false },
                                            { name: 'PayerEnum', disabled: true, required: true },
                                            { name: 'UnderwritingAreaEnum', disabled: true, required: true },
                                            { name: 'RequestedTo', disabled: false, required: false },
                                            { name: 'ClientId', disabled: false, required: true },
                                            { name: 'ReceptionDate', disabled: true, required: false },
                                            { name: 'Cost', disabled: true, required: true },
                                            { name: 'Status', disabled: true, required: false },
                                            { name: 'StatusByLanguage', disabled: false, required: false },
                                            { name: 'RequirementTypeEnumText', disabled: false, required: false },
                                            { name: 'RequirementTypeByLanguage', disabled: false, required: false },
                                            { name: 'RequirementDate', disabled: true, required: true },
                                            { name: 'AlarmTypeEnumText', disabled: true, required: false },
                                            { name: 'AlarmTypeByLanguage', disabled: true, required: false },
                                            { name: 'TotalDebits', disabled: true, required: false },
                                            { name: 'TotalCredits', disabled: true, required: false },
                                            { name: 'Balance', disabled: true, required: false },
                                            { name: 'CostDueAmount', disabled: true, required: false },
                                            { name: 'AcordRequirementCode', disabled: true, required: true },
                                            { name: 'ProviderId', disabled: true, required: false },
                                            { name: 'Link', disabled: false, required: false }];


            //Se busca el tipo de requerimiento según lo escogido en el DropDownList RequirementType
            ProxySyncLookUps.invoke("GetRequirementTypeByType", "{requirementType: " + selectedRequirementTypeValue + " }", function (result) {
                resultRequirementType = result.d;
            });

            //Después de que el usuario indique el "Tipo de requerimiento", mostrar por defecto los valores que tiene ese requerimiento en la tabla TabRequirementType
            //  (Tipo de proceso, Área de suscripción,  PAgador, Costo, Código acord)
            $('#ProcessTypeEnum').val(resultRequirementType.ProcessType);
            $('#UnderwritingAreaEnum').val(resultRequirementType.UnderwritingArea);
            $('#PayerEnum').val(resultRequirementType.Payer);
            $('#Cost').val(resultRequirementType.Cost);
            $('#AcordRequirementCode').val(resultRequirementType.AcordRequirementCode);
            $('#AlarmTypeEnum').val("1")
            $('#TotalDebits').val("0")
            $('#TotalCredits').val("0")
            $('#Balance').val("0")
            $('#CostDueAmount').val("0")
            $('#RequirementDate').datepicker().datepicker('setDate', 'today');
            $("#Status").val("1")
            $("#ProviderId").prop('selectedIndex', 1);

            var fieldsToProcess;
            switch (resultRequirementType.ProcessType) {
                case 1:
                    fieldsToProcess = fieldsProcessByWorkFlow;
                    break;
                case 2:
                    fieldsToProcess = fieldsProcessByHuman;
                    break;
                case 3:
                    fieldsToProcess = fieldsProcessByWorkFlowHuman;
                    break;
            }

            $.each(fieldsToProcess, function (i, item) {
                setFieldDisable(item.name, item.disabled);
                setFieldRequired(item.name, item.required);
            });

        }
    }

    function setFieldDisable(fieldName, disabled) {
        var fieldElement = $('#' + fieldName);
        if (fieldElement.hasClass('hasDatepicker')) {
            if (disabled) {
                fieldElement.datepicker("disable");
            }
            else
            {
                fieldElement.datepicker("enable");
            }
        }
        else
        {
            fieldElement.attr('disabled', disabled);
        }
    }

    function myformatter(cellvalue) {
        var dateValue = cellvalue.substring(cellvalue.indexOf('(') + 1, cellvalue.indexOf(')'));
        var newDate = new Date(parseFloat(dateValue));
        if (newDate.getFullYear() == 1)
            return '';
        else
            return convertdate(newDate);
    }

    function convertdate(str) {
        var date = new Date(str),
            mnth = ("0" + (date.getMonth() + 1)).slice(-2),
            day = ("0" + date.getDate()).slice(-2);
        return [day, mnth, date.getFullYear()].join("/");
    }


    function setFieldRequired(fielName, requiredData)
    {
        $("#grid-requirement").jqGrid('setColProp', fielName, { editrules: { required: requiredData } });
    }

