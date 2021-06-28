var token = "";
var gridRole;
var RolesType = ""
var GendersType = ""
var gridColNamesRole = "";
var gridColModelRole = "";

function getNumberLength(number) {
    return number.toString().length;
}

function ReloadGeneralInformationGrid(editProperty) {
    
    //$.jgrid.gridUnload("grid-role");
    gridRole = $("#grid-role");
    gridRole.jqGrid("GridUnload");

    gridRole.jqGrid($.extend({}, GeneralGridOptions, {
    	url: "services/RoleInCase.aspx/GetAllRolesInCase",
    	serializeGridData: function (data) {
    	    return JSON.stringify({ caseId: $('#dpeCaseId_I').val().length === 0 ? 0 : $('#dpeCaseId_I').val() });
    		data = {};
    	},
    	colNames: gridColNamesRole,
    	colModel: gridColModelRole,
    	pager: '#pager-role',
    	subGrid: false,
    	viewrecords: false,
    	pgbuttons: false
    }));
    
    gridRole.jqGrid("navGrid", "#pager-role", {
        edit: false,
        add: false,
        del: false,
        search: false,
        refresh: false
    });
    gridRole.jqGrid("filterToolbar", {
        searchOnEnter: false,
        enableClear: false,
        searchOperators: false,
        defaultSearch: 'cn',
        autosearch: true
    });
    gridRole.trigger("reloadGrid");

}

function setSmokerIndicatorChk(cellvalue) {
    if (cellvalue == '1')
        return ' checked="checked" ';
    else
        return '';
}

/*---------------------------------------------------- On Dom ready -----------------------------------------------------*/
$(function () {
    $.jgrid.defaults.styleUI = 'Bootstrap';
    "use strict";

    ProxySyncRoleInCase.invoke("GetHeaderValues", "", function (data) { gridColNamesRole = data.d; });

    gridColModelRole = [{ name: 'RoleNameByLanguage', index: 'RoleNameByLanguage', resizable: true, editable: false, width: 140 },
                        { name: 'ClientID', index: 'ClientID', resizable: true, editable: true, width: 130, editrules: { required: true } },
                        { name: 'ClientName', index: 'ClientName', resizable: true, editable: true, editrules: { required: true } },
						{ name: 'CompleteAddress', index: 'CompleteAddress', resizable: true, editable: true, editrules: { required: true }, width: 190 },
						{ name: 'PhoneNumber', index: 'PhoneNumber', resizable: true, editable: true, editrules: { required: true } },
                        { name: 'ActuarialAge', index: 'ActuarialAge', resizable: true, editable: true, formatter: function (cellvalue, options, rowobject) { return getNumberLength(cellvalue) == 4 || cellvalue == 1 ? "" : cellvalue; }, width: 100 },
                        { name: 'GenderByLanguage', index: 'GenderByLanguage', resizable: true, editable: false, width: 80 },
                        { name: 'Height', index: 'Height', resizable: true, editable: true, formatter: function (cellvalue, options, rowobject) { return cellvalue == 0 ? "" : Number(cellvalue).toFixed(2); }, width: 70 },
						{ name: 'Weight', index: 'Weight', resizable: true, editable: true, formatter: function (cellvalue, options, rowobject) { return cellvalue == 0 ? "" : cellvalue; }, width: 70 },
						{ name: 'SmokerIndicator', index: 'SmokerIndicator', resizable: true, editable: true, width: 70, align: 'center', edittype: 'checkbox', formatter: function (cellvalue, options, rowobject) { return '<input type="checkbox"' + setSmokerIndicatorChk(cellvalue) + 'offval="no" disabled="disabled">'; }, classes: 'checkboxGrid' },
						{
						    name: 'ExclusionDate', index: 'ExclusionDate', resizable: true, editable: true, width: 90, editrules: { required: true },
						    formatter: function (cellvalue, options, rowobject) {
						        var dateValue = cellvalue.substring(cellvalue.indexOf('(') + 1, cellvalue.indexOf(')'));
						        var newDate = new Date(parseFloat(dateValue));
						        if (newDate.getFullYear() == 1 || newDate.getFullYear() == 0)
						            return '';
						        else
						            return convertdate(newDate);
						    }
						}];
});