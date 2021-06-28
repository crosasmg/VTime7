var H5CasosSuscripcionPendientesSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5CasosSuscripcionPendientesFormId').val()
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#H5CasosSuscripcionPendientesFormId').val(data.InstanceFormId);

        H5CasosSuscripcionPendientesSupport.LookUpForUnderwritingCaseCreatorUserCode(source);
        H5CasosSuscripcionPendientesSupport.LookUpForUnderwritingCaseUpdateUserCode(source);

        H5CasosSuscripcionPendientesSupport.ItemsTblRequest();
        if (data.Items_Item !== null)
            $('#ItemsTbl').bootstrapTable('load', data.Items_Item);

    };

    this.ControlBehaviour = function () {

       this.Items_UnderwritingCaseUnderwritingCaseID_Item1 = function (row) {
           window.location.href = '/fasi/dli/forms/UnderwritingPanel.aspx?uwCaseID='+ row.UnderwritingCaseID +'';

            return true;
        };








    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                H5CasosSuscripcionPendientesSupport.ObjectToInput(data.d.Data, source);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };




    this.ControlActions = function () {


    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();
    
        $("#H5CasosSuscripcionPendientesMainForm").validate({
            errorPlacement: function (error, element) {
                var name = $(element).attr("name");
                var $obj = $("#" + name + "_validate");
                if ($obj.length) {
                    error.appendTo($obj);
                }
                else {
                    error.insertAfter(element);
                }
            },

            rules: {

            },
            messages: {

            }
        });

    };
    this.LookUpForUnderwritingCaseCreatorUserCodeFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#UnderwritingCaseCreatorUserCode>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForUnderwritingCaseCreatorUserCode = function (defaultValue, source) {
        var ctrol = $('#UnderwritingCaseCreatorUserCode');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/queries/H5CasosSuscripcionPendientesActions.aspx/LookUpForUnderwritingCaseCreatorUserCode", false,
                JSON.stringify({ id: $('#H5CasosSuscripcionPendientesFormId').val() }),
                function (data) {
                    ctrol.children().remove();
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);

                        if (source !== 'Initialization')
                            ctrol.change();
                            
                            
                });

        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);

                    if (source !== 'Initialization')
                        ctrol.change();
                }
    };
    this.LookUpForUnderwritingCaseUpdateUserCodeFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#UnderwritingCaseUpdateUserCode>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForUnderwritingCaseUpdateUserCode = function (defaultValue, source) {
        var ctrol = $('#UnderwritingCaseUpdateUserCode');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/queries/H5CasosSuscripcionPendientesActions.aspx/LookUpForUnderwritingCaseUpdateUserCode", false,
                JSON.stringify({ id: $('#H5CasosSuscripcionPendientesFormId').val() }),
                function (data) {
                    ctrol.children().remove();
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);

                        if (source !== 'Initialization')
                            ctrol.change();
                            
                            
                });

        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);

                    if (source !== 'Initialization')
                        ctrol.change();
                }
    };

    this.ItemsTblSetup = function (table) {
        H5CasosSuscripcionPendientesSupport.LookUpForUnderwritingCaseCreatorUserCode('');
        H5CasosSuscripcionPendientesSupport.LookUpForUnderwritingCaseUpdateUserCode('');
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            search: true,
            columns: [{
                field: 'RecordType',
                title: 'Record Type',
                formatter: 'H5CasosSuscripcionPendientesSupport.UnderwritingCaseRecordType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'RecordTypeDesc',
                title: 'Record Type',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Status',
                title: 'Status',
                formatter: 'H5CasosSuscripcionPendientesSupport.UnderwritingCaseStatus_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'StatusDesc',
                title: 'Status',
                sortable: true,
                halign: 'center'
            }, {
                field: 'UnderwritingCaseID',
                title: 'Underwriting Case ID',
                formatter: 'tableHelperSupport.IsContextMenu',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'OpenDate',
                title: 'Open Date',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'Decision',
                title: 'Decision',
                formatter: 'H5CasosSuscripcionPendientesSupport.UnderwritingCaseDecision_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'DecisionDesc',
                title: 'Decision',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creator User Code',
                formatter: 'H5CasosSuscripcionPendientesSupport.LookUpForUnderwritingCaseCreatorUserCodeFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'UpdateUserCode',
                title: 'Update User Code',
                formatter: 'H5CasosSuscripcionPendientesSupport.LookUpForUnderwritingCaseUpdateUserCodeFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'UpdateDate',
                title: 'Update Date',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }]
        });

        table.bootstrapTable('refreshOptions', {
            contextMenu: '#ItemsContextMenu',
            contextMenuButton: '.menu-UnderwritingCaseID',
            beforeContextMenuRow: function (e, row, buttonElement) {
                H5CasosSuscripcionPendientesSupport.ItemsRowToInput(row);
                if (buttonElement && $(buttonElement).hasClass('menu-UnderwritingCaseID')) {

                    $('#ItemsTbl').bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#Items_UnderwritingCaseUnderwritingCaseIDContextMenu',
                        buttonElement: buttonElement
                    });
                }
                return false;
            },
            onContextMenuItem: function (row, $el) {
                H5CasosSuscripcionPendientesSupport.ItemsRowToInput(row);
                switch ($el.data("item")) {
                    case 'Items_UnderwritingCaseUnderwritingCaseID_Item1':
                        H5CasosSuscripcionPendientesSupport.Items_UnderwritingCaseUnderwritingCaseID_Item1(row);
                        break;
                }
            }
        });


    };


    this.ItemsRowToInput = function (row) {
        H5CasosSuscripcionPendientesSupport.currentRow = row;
        AutoNumeric.set('#UnderwritingCaseRecordType', row.RecordType);
        $('#UnderwritingCaseRecordTypeDesc').val(row.RecordTypeDesc);
        AutoNumeric.set('#UnderwritingCaseStatus', row.Status);
        $('#UnderwritingCaseStatusDesc').val(row.StatusDesc);
        $('#UnderwritingCaseUnderwritingCaseID').val(row.UnderwritingCaseID);
        $('#UnderwritingCaseOpenDate').val(generalSupport.ToJavaScriptDateCustom(row.OpenDate, generalSupport.DateFormat()));
        AutoNumeric.set('#UnderwritingCaseDecision', row.Decision);
        $('#UnderwritingCaseDecisionDesc').val(row.DecisionDesc);
        H5CasosSuscripcionPendientesSupport.LookUpForUnderwritingCaseCreatorUserCode(row.CreatorUserCode, '');
        H5CasosSuscripcionPendientesSupport.LookUpForUnderwritingCaseUpdateUserCode(row.UpdateUserCode, '');
        $('#UnderwritingCaseUpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat()));

    };
    this.ItemsTblRequest = function (params) {
        app.core.AsyncWebMethod("/fasi/dli/queries/H5CasosSuscripcionPendientesActions.aspx/ItemsTblDataLoad", false,
            JSON.stringify({
                                filter: ''
            }),
            function (data) {
                    $('#ItemsTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);


            });
    };


    this.UnderwritingCaseRecordType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.UnderwritingCaseStatus_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.UnderwritingCaseDecision_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };


};

$(document).ready(function () {
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('Casos pendientes');
        

    H5CasosSuscripcionPendientesSupport.ControlBehaviour();
    H5CasosSuscripcionPendientesSupport.ControlActions();
    H5CasosSuscripcionPendientesSupport.ValidateSetup();


    $("#ItemsTblPlaceHolder").replaceWith('<table id="ItemsTbl"><caption >Underwriting Cases</caption></table>');
    H5CasosSuscripcionPendientesSupport.ItemsTblSetup($('#ItemsTbl'));

        H5CasosSuscripcionPendientesSupport.ItemsTblRequest();



});

