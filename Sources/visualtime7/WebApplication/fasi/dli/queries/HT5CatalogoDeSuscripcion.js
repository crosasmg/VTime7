var HT5CatalogoDeSuscripcionSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5CatalogoDeSuscripcionFormId').val(),
            RecordEffectiveDate: generalSupport.DatePickerValueInputToObject('#RecordEffectiveDate'),
            _AllowHistoryInfo: $('#_AllowHistoryInfo').is(':checked')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#HT5CatalogoDeSuscripcionFormId').val(data.InstanceFormId);
        $('#RecordEffectiveDate').val(generalSupport.ToJavaScriptDateCustom(data.RecordEffectiveDate, generalSupport.DateFormat()));
        $('#_AllowHistoryInfo').prop("checked", data._AllowHistoryInfo);

        HT5CatalogoDeSuscripcionSupport.LookUpForTabUnderwritingRuleRecordStatus(source);

        HT5CatalogoDeSuscripcionSupport.ItemsTblRequest();
        if (data.Items_Item !== null)
            $('#ItemsTbl').bootstrapTable('load', data.Items_Item);

    };

    this.ControlBehaviour = function () {

       this.Items_TabUnderwritingRuleUnderwritingRuleIdDesc_Item1 = function (row) {
           generalSupport.CallBackOfficePage('', '&ID='+ row.UnderwritingRuleId +'&EffectDate='+ generalSupport.ToJavaScriptDateCustom(row.EffectiveDate, generalSupport.DateFormat())  +'');

            return true;
        };






        $('#RecordEffectiveDate_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                HT5CatalogoDeSuscripcionSupport.ObjectToInput(data.d.Data, source);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };




    this.ControlActions = function () {

        $('#btnOk').click(function (event) {
            var formInstance = $("#HT5CatalogoDeSuscripcionMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#btnOk'));
                btnLoading.start();
                HT5CatalogoDeSuscripcionSupport.ItemsTblRequest();
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#TabUnderwritingRuleUnderwritingRuleIdDesc_MenuGroup').on('show.bs.dropdown', function () {
                    if (generalSupport.NumericValue('#TabUnderwritingRuleUnderwritingRuleId', -99999, 99999) !== null)
                        $('#TabUnderwritingRuleUnderwritingRuleIdDesc_DropDownMenu').find('[id="Items_TabUnderwritingRuleUnderwritingRuleIdDesc_Item1"]').show();
                    else
                        $('#TabUnderwritingRuleUnderwritingRuleIdDesc_DropDownMenu').find('[id="Items_TabUnderwritingRuleUnderwritingRuleIdDesc_Item1"]').hide();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();
    
        $("#HT5CatalogoDeSuscripcionMainForm").validate({
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
                RecordEffectiveDate: {
                    required: true,
                    DatePicker: true
                }
            },
            messages: {
                RecordEffectiveDate: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                }
            }
        });

    };
    this.LookUpForTabUnderwritingRuleRecordStatusFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#TabUnderwritingRuleRecordStatus>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForTabUnderwritingRuleRecordStatus = function (defaultValue, source) {
        var ctrol = $('#TabUnderwritingRuleRecordStatus');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/queries/HT5CatalogoDeSuscripcionActions.aspx/LookUpForTabUnderwritingRuleRecordStatus", false,
                JSON.stringify({ id: $('#HT5CatalogoDeSuscripcionFormId').val() }),
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
        HT5CatalogoDeSuscripcionSupport.LookUpForTabUnderwritingRuleRecordStatus('');
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            columns: [{
                field: 'UnderwritingRuleId',
                title: 'Regla de suscripción',
                formatter: 'HT5CatalogoDeSuscripcionSupport.TabUnderwritingRuleUnderwritingRuleId_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'UnderwritingRuleIdDesc',
                title: 'Regla de suscripción',
                formatter: 'tableHelperSupport.IsContextMenu',
                sortable: true,
                halign: 'center'
            }, {
                field: 'UnderwritingCaseType',
                title: 'Tipo de suscripción',
                formatter: 'HT5CatalogoDeSuscripcionSupport.TabUnderwritingRuleUnderwritingCaseType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'UnderwritingCaseTypeDesc',
                title: 'Tipo de suscripción',
                sortable: true,
                halign: 'center'
            }, {
                field: 'UnderwritingRuleStatus',
                title: 'Estado',
                formatter: 'HT5CatalogoDeSuscripcionSupport.TabUnderwritingRuleUnderwritingRuleStatus_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'UnderwritingRuleStatusDesc',
                title: 'Estado',
                sortable: true,
                halign: 'center'
            }, {
                field: 'EffectiveDate',
                title: 'Fecha de validez',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'CancellationDate',
                title: 'Fecha de anulación',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'UnderwritingArea',
                title: 'Área de suscripción',
                formatter: 'HT5CatalogoDeSuscripcionSupport.TabUnderwritingRuleUnderwritingArea_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'UnderwritingAreaDesc',
                title: 'Área de suscripción',
                sortable: true,
                halign: 'center'
            }, {
                field: 'RequirementType',
                title: 'Requerimiento',
                formatter: 'HT5CatalogoDeSuscripcionSupport.TabUnderwritingRuleRequirementType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'RequirementTypeDesc',
                title: 'Requerimiento',
                sortable: true,
                halign: 'center'
            }, {
                field: 'QuestionId',
                title: 'Pregunta',
                formatter: 'HT5CatalogoDeSuscripcionSupport.TabUnderwritingRuleQuestionId_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'QuestionIdDesc',
                title: 'Pregunta',
                sortable: true,
                halign: 'center'
            }, {
                field: 'RecordStatus',
                title: 'Estado del registro.',
                formatter: 'HT5CatalogoDeSuscripcionSupport.LookUpForTabUnderwritingRuleRecordStatusFormatter',
                sortable: true,
                halign: 'center'
            }]
        });

        table.bootstrapTable('refreshOptions', {
            contextMenu: '#ItemsContextMenu',
            contextMenuButton: '.menu-UnderwritingRuleIdDesc',
            beforeContextMenuRow: function (e, row, buttonElement) {
                HT5CatalogoDeSuscripcionSupport.ItemsRowToInput(row);
                if (buttonElement && $(buttonElement).hasClass('menu-UnderwritingRuleIdDesc')) {
                    if (row.UnderwritingRuleId !== null)
                        $('#Items_TabUnderwritingRuleUnderwritingRuleIdDescContextMenu').find('[data-item="Items_TabUnderwritingRuleUnderwritingRuleIdDesc_Item1"]').show();
                    else
                        $('#Items_TabUnderwritingRuleUnderwritingRuleIdDescContextMenu').find('[data-item="Items_TabUnderwritingRuleUnderwritingRuleIdDesc_Item1"]').hide();

                    $('#ItemsTbl').bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#Items_TabUnderwritingRuleUnderwritingRuleIdDescContextMenu',
                        buttonElement: buttonElement
                    });
                }
                return false;
            },
            onContextMenuItem: function (row, $el) {
                HT5CatalogoDeSuscripcionSupport.ItemsRowToInput(row);
                switch ($el.data("item")) {
                    case 'Items_TabUnderwritingRuleUnderwritingRuleIdDesc_Item1':
                        HT5CatalogoDeSuscripcionSupport.Items_TabUnderwritingRuleUnderwritingRuleIdDesc_Item1(row);
                        break;
                }
            }
        });


    };


    this.ItemsRowToInput = function (row) {
        HT5CatalogoDeSuscripcionSupport.currentRow = row;
        AutoNumeric.set('#TabUnderwritingRuleUnderwritingRuleId', row.UnderwritingRuleId);
        $('#TabUnderwritingRuleUnderwritingRuleIdDesc').val(row.UnderwritingRuleIdDesc);
        AutoNumeric.set('#TabUnderwritingRuleUnderwritingCaseType', row.UnderwritingCaseType);
        $('#TabUnderwritingRuleUnderwritingCaseTypeDesc').val(row.UnderwritingCaseTypeDesc);
        AutoNumeric.set('#TabUnderwritingRuleUnderwritingRuleStatus', row.UnderwritingRuleStatus);
        $('#TabUnderwritingRuleUnderwritingRuleStatusDesc').val(row.UnderwritingRuleStatusDesc);
        $('#TabUnderwritingRuleEffectiveDate').val(generalSupport.ToJavaScriptDateCustom(row.EffectiveDate, generalSupport.DateFormat()));
        $('#TabUnderwritingRuleCancellationDate').val(generalSupport.ToJavaScriptDateCustom(row.CancellationDate, generalSupport.DateFormat()));
        AutoNumeric.set('#TabUnderwritingRuleUnderwritingArea', row.UnderwritingArea);
        $('#TabUnderwritingRuleUnderwritingAreaDesc').val(row.UnderwritingAreaDesc);
        AutoNumeric.set('#TabUnderwritingRuleRequirementType', row.RequirementType);
        $('#TabUnderwritingRuleRequirementTypeDesc').val(row.RequirementTypeDesc);
        AutoNumeric.set('#TabUnderwritingRuleQuestionId', row.QuestionId);
        $('#TabUnderwritingRuleQuestionIdDesc').val(row.QuestionIdDesc);
        HT5CatalogoDeSuscripcionSupport.LookUpForTabUnderwritingRuleRecordStatus(row.RecordStatus, '');

    };
    this.ItemsTblRequest = function (params) {
        app.core.AsyncWebMethod("/fasi/dli/queries/HT5CatalogoDeSuscripcionActions.aspx/ItemsTblDataLoad", false,
            JSON.stringify({
                
            }),
            function (data) {
                    $('#ItemsTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);


            });
    };


    this.TabUnderwritingRuleUnderwritingRuleId_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.TabUnderwritingRuleUnderwritingCaseType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.TabUnderwritingRuleUnderwritingRuleStatus_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.TabUnderwritingRuleUnderwritingArea_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.TabUnderwritingRuleRequirementType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.TabUnderwritingRuleQuestionId_FormatterMaskData = function (value, row, index) {          
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
        masterSupport.setPageTitle('HT5Catálogo de Reglas de Suscripción.');
        

    HT5CatalogoDeSuscripcionSupport.ControlBehaviour();
    HT5CatalogoDeSuscripcionSupport.ControlActions();
    HT5CatalogoDeSuscripcionSupport.ValidateSetup();

    $('#RecordEffectiveDate').val(generalSupport.URLDateValue('RecordEffectiveDate'));

    $("#ItemsTblPlaceHolder").replaceWith('<table id="ItemsTbl"><caption >Trans Underwriting Rules</caption></table>');
    HT5CatalogoDeSuscripcionSupport.ItemsTblSetup($('#ItemsTbl'));

    $('#RecordEffectiveDate').val(moment().format(generalSupport.DateFormat()));
        HT5CatalogoDeSuscripcionSupport.ItemsTblRequest();



});

