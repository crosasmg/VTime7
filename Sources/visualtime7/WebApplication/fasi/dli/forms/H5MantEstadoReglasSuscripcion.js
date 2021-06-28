var H5MantEstadoReglasSuscripcionSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5MantEstadoReglasSuscripcionFormId').val(),
            TabUnderwritingRuleSType_Grid_TabUnderwritingRuleSType_Item: generalSupport.NormalizeProperties($('#TabUnderwritingRuleSType_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate'),
            TabUnderwritingRuleSTypeTranslator_Grid_TabUnderwritingRuleSType_Item: generalSupport.NormalizeProperties($('#TabUnderwritingRuleSTypeTranslator_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#H5MantEstadoReglasSuscripcionFormId').val(data.InstanceFormId);

        H5MantEstadoReglasSuscripcionSupport.LookUpForRecordStatus(source);
        H5MantEstadoReglasSuscripcionSupport.LookUpForLanguageIdTranslator(source);

        H5MantEstadoReglasSuscripcionSupport.TabUnderwritingRuleSType_GridTblRequest();
        if (data.TabUnderwritingRuleSType_Grid_TabUnderwritingRuleSType_Item !== null)
            $('#TabUnderwritingRuleSType_GridTbl').bootstrapTable('load', data.TabUnderwritingRuleSType_Grid_TabUnderwritingRuleSType_Item);
        H5MantEstadoReglasSuscripcionSupport.TabUnderwritingRuleSTypeTranslator_GridTblRequest();
        if (data.TabUnderwritingRuleSTypeTranslator_Grid_TabUnderwritingRuleSType_Item !== null)
            $('#TabUnderwritingRuleSTypeTranslator_GridTbl').bootstrapTable('load', data.TabUnderwritingRuleSTypeTranslator_Grid_TabUnderwritingRuleSType_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#UnderwritingRuleStatus', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      new AutoNumeric('#CreatorUserCode', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
        });
      new AutoNumeric('#UpdateUserCode', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
        });
      new AutoNumeric('#UnderwritingRuleStatusTranslator', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });




        $('#CreationDate_group').datetimepicker({
            format: generalSupport.DateFormat() + ' HH:mm:ss',
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#CreationDate_group');
        $('#UpdateDate_group').datetimepicker({
            format: generalSupport.DateFormat() + ' HH:mm:ss',
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#UpdateDate_group');


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               if (source == 'Initialization')
					         H5MantEstadoReglasSuscripcionSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5MantEstadoReglasSuscripcionSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.TabUnderwritingRuleSType_Grid_insert = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEstadoReglasSuscripcionActions.aspx/TabUnderwritingRuleSType_Grid1InsertCommandActionTabUnderwritingRuleSType", false,
               JSON.stringify({ UNDERWRITINGRULESTATUS1: row.UnderwritingRuleStatus, RECORDSTATUS2: row.RecordStatus, CREATORUSERCODE2: generalSupport.UserContext().userId, UPDATEUSERCODE4: generalSupport.UserContext().userId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEstadoReglasSuscripcionActions.aspx/TabUnderwritingRuleSType_Grid3InsertCommandActionTransUnderwritingRuleSType", false,
               JSON.stringify({ UNDERWRITINGRULESTATUS1: row.UnderwritingRuleStatus, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabUnderwritingRuleSType_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message7 = 'Se agregó correctamente el registro';
            notification.toastr.success('Agregar registro', message7);
                    }                    
                    else {
            var message8 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message8);

                        }

    };
    this.TabUnderwritingRuleSType_Grid_update = function (row, $modal) {
          var data;
            var recordCount;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEstadoReglasSuscripcionActions.aspx/TabUnderwritingRuleSType_Grid1UpdateCommandActionTabUnderwritingRuleSType", false,
               JSON.stringify({ RECORDSTATUS1: row.RecordStatus, UPDATEUSERCODE1: generalSupport.UserContext().userId, TabUnderwritingRuleSTypeUnderwritingRuleStatus3: row.UnderwritingRuleStatus }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEstadoReglasSuscripcionActions.aspx/TabUnderwritingRuleSType_Grid3SelectCommandActionTransUnderwritingRuleSType", false,
               JSON.stringify({                 TransUnderwritingRuleSTypeUnderwritingRuleStatus1: row.UnderwritingRuleStatus,
                TransUnderwritingRuleSTypeLanguageId2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEstadoReglasSuscripcionActions.aspx/TabUnderwritingRuleSType_Grid5InsertCommandActionTransUnderwritingRuleSType", false,
               JSON.stringify({ UNDERWRITINGRULESTATUS1: row.UnderwritingRuleStatus, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEstadoReglasSuscripcionActions.aspx/TabUnderwritingRuleSType_Grid6UpdateCommandActionTransUnderwritingRuleSType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransUnderwritingRuleSTypeUnderwritingRuleStatus4: row.UnderwritingRuleStatus, TransUnderwritingRuleSTypeLanguageId5: generalSupport.SessionContext().languageId }));
               


                        }

                    }
        if (data.d.Success === true){
            $('#TabUnderwritingRuleSType_GridTbl').bootstrapTable('updateByUniqueId', { id: row.UnderwritingRuleStatus, row: row });
            $modal.modal('hide');
            var message9 = 'Se actualizó correctamente el registro';
            notification.toastr.success('Actualizar registro', message9);
                        }                        
                        else {
            var message10 = 'No se pudo actualizar el registro';
            notification.swal.error('Actualizar registro', message10);

                            }

    };
    this.TabUnderwritingRuleSType_Grid_delete = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEstadoReglasSuscripcionActions.aspx/TabUnderwritingRuleSType_Grid1DeleteCommandActionTransUnderwritingRuleSType", false,
               JSON.stringify({ TransUnderwritingRuleSTypeUnderwritingRuleStatus1: row.UnderwritingRuleStatus }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEstadoReglasSuscripcionActions.aspx/TabUnderwritingRuleSType_Grid3DeleteCommandActionTabUnderwritingRuleSType", false,
               JSON.stringify({ TabUnderwritingRuleSTypeUnderwritingRuleStatus1: row.UnderwritingRuleStatus }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabUnderwritingRuleSType_GridTbl').bootstrapTable('remove', {field: 'UnderwritingRuleStatus', values: [generalSupport.NumericValue('#UnderwritingRuleStatus', -99999, 99999)]});
            var message7 = 'Se eliminó correctamente el registro';
            notification.toastr.success('Eliminar registro', message7);
                    }                    
                    else {
            var message8 = 'No se puede eliminar el registro';
            notification.toastr.error('Eliminar registro', message8);

                        }

    };
    this.TabUnderwritingRuleSType_Grid_BeforeShowPopup = function (row, $modal) {
          var data;
            var nextId;
        if (row.UnderwritingRuleStatus === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEstadoReglasSuscripcionActions.aspx/TabUnderwritingRuleSType_Grid2SelectCommandActionTabUnderwritingRuleSType", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#UnderwritingRuleStatus', nextId);

            }

    };
    this.TabUnderwritingRuleSTypeTranslator_Grid_update = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEstadoReglasSuscripcionActions.aspx/TabUnderwritingRuleSTypeTranslator_Grid1UpdateCommandActionTransUnderwritingRuleSType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransUnderwritingRuleSTypeUnderwritingRuleStatus4: row.UnderwritingRuleStatus, TransUnderwritingRuleSTypeLanguageId5: row.LanguageId }));
               

        if (data.d.Success === true){
            $('#TabUnderwritingRuleSTypeTranslator_GridTbl').bootstrapTable('updateByUniqueId', { id: row.UnderwritingRuleStatus, row: row });
            $modal.modal('hide');
            var message4 = 'Se actualizó correctamente el registro';
            notification.toastr.success('Actualizar registro', message4);
            }            
            else {
            var message5 = 'No se pudo actualizar el registro';
            notification.swal.error('Actualizar registro', message5);

                }

    };

    this.ControlActions =   function () {

        $('#ShowStandardGrid').click(function (event) {
            var formInstance = $("#H5MantEstadoReglasSuscripcionMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                H5MantEstadoReglasSuscripcionSupport.TabUnderwritingRuleSType_GridTblRequest();
                $('#TabUnderwritingRuleSType_GridContainer').toggleClass('hidden', false);
                $('#TabUnderwritingRuleSTypeTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#H5MantEstadoReglasSuscripcionMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                H5MantEstadoReglasSuscripcionSupport.TabUnderwritingRuleSTypeTranslator_GridTblRequest();
                $('#TabUnderwritingRuleSTypeTranslator_GridContainer').toggleClass('hidden', false);
                $('#TabUnderwritingRuleSType_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5MantEstadoReglasSuscripcionMainForm").validate({
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
        $("#TabUnderwritingRuleSType_GridEditForm").validate({
            rules: {
                UnderwritingRuleStatus: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999,
                    required: true
                },
                CreatorUserCode: {
                    AutoNumericMinValue: -999999999,
                    AutoNumericMaxValue: 999999999,
                    required: true
                },
                CreationDate: {
                    required: true,
                    DatePicker: true
                },
                UpdateUserCode: {
                    AutoNumericMinValue: -999999999,
                    AutoNumericMaxValue: 999999999,
                    required: true
                },
                UpdateDate: {
                    required: true,
                    DatePicker: true
                },
                Description: {
                    required: true,
                    maxlength: 60
                },
                ShortDescription: {
                    required: true,
                    maxlength: 20
                },
                RecordStatus: {
                    required: true,
                }

            },
            messages: {
                UnderwritingRuleStatus: {
                    AutoNumericMinValue: $.i18n.t('app.validation.UnderwritingRuleStatus.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.UnderwritingRuleStatus.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.UnderwritingRuleStatus.required')
                },
                CreatorUserCode: {
                    AutoNumericMinValue: $.i18n.t('app.validation.CreatorUserCode.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.CreatorUserCode.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.CreatorUserCode.required')
                },
                CreationDate: {
                    required: $.i18n.t('app.validation.CreationDate.required'),
                    DatePicker: $.i18n.t('app.validation.CreationDate.DatePicker')
                },
                UpdateUserCode: {
                    AutoNumericMinValue: $.i18n.t('app.validation.UpdateUserCode.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.UpdateUserCode.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.UpdateUserCode.required')
                },
                UpdateDate: {
                    required: $.i18n.t('app.validation.UpdateDate.required'),
                    DatePicker: $.i18n.t('app.validation.UpdateDate.DatePicker')
                },
                Description: {
                    required: $.i18n.t('app.validation.Description.required'),
                    maxlength: $.i18n.t('app.validation.Description.maxlength')
                },
                ShortDescription: {
                    required: $.i18n.t('app.validation.ShortDescription.required'),
                    maxlength: $.i18n.t('app.validation.ShortDescription.maxlength')
                },
                RecordStatus: {
                    required: $.i18n.t('app.validation.RecordStatus.required'),
                }

            }
        });
        $("#TabUnderwritingRuleSTypeTranslator_GridEditForm").validate({
            rules: {
                UnderwritingRuleStatusTranslator: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999,
                    required: true
                },
                LanguageIdTranslator: {
                    required: true,
                },
                DescriptionTranslator: {
                    required: true,
                    maxlength: 60
                },
                ShortDescriptionTranslator: {
                    required: true,
                    maxlength: 20
                }

            },
            messages: {
                UnderwritingRuleStatusTranslator: {
                    AutoNumericMinValue: $.i18n.t('app.validation.UnderwritingRuleStatusTranslator.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.UnderwritingRuleStatusTranslator.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.UnderwritingRuleStatusTranslator.required')
                },
                LanguageIdTranslator: {
                    required: $.i18n.t('app.validation.LanguageIdTranslator.required'),
                },
                DescriptionTranslator: {
                    required: $.i18n.t('app.validation.DescriptionTranslator.required'),
                    maxlength: $.i18n.t('app.validation.DescriptionTranslator.maxlength')
                },
                ShortDescriptionTranslator: {
                    required: $.i18n.t('app.validation.ShortDescriptionTranslator.required'),
                    maxlength: $.i18n.t('app.validation.ShortDescriptionTranslator.maxlength')
                }

            }
        });

    };
    this.LookUpForRecordStatusFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#RecordStatus>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForRecordStatus = function (defaultValue, source) {
        var ctrol = $('#RecordStatus');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantEstadoReglasSuscripcionActions.aspx/LookUpForRecordStatus", false,
                JSON.stringify({ id: $('#H5MantEstadoReglasSuscripcionFormId').val() }),
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
    this.LookUpForLanguageIdTranslatorFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#LanguageIdTranslator>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForLanguageIdTranslator = function (defaultValue, source) {
        var ctrol = $('#LanguageIdTranslator');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantEstadoReglasSuscripcionActions.aspx/LookUpForLanguageIdTranslator", false,
                JSON.stringify({ id: $('#H5MantEstadoReglasSuscripcionFormId').val() }),
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

    this.TabUnderwritingRuleSType_GridTblSetup = function (table) {
        H5MantEstadoReglasSuscripcionSupport.LookUpForRecordStatus('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'UnderwritingRuleStatus',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabUnderwritingRuleSType_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'UnderwritingRuleStatus',
                title: 'Estado de la regla de suscripción',
                events: 'TabUnderwritingRuleSType_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5MantEstadoReglasSuscripcionSupport.CreatorUserCode_FormatterMaskData',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'CreationDate',
                title: 'Creado en',
                formatter: 'tableHelperSupport.OnlyDateFormatterWithHoursMinutesSeconds',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'UpdateUserCode',
                title: 'Última actualización por',
                formatter: 'H5MantEstadoReglasSuscripcionSupport.UpdateUserCode_FormatterMaskData',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'UpdateDate',
                title: 'Última actualización en',
                formatter: 'tableHelperSupport.OnlyDateFormatterWithHoursMinutesSeconds',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'Description',
                title: 'Descripción',
                sortable: true,
                halign: 'center'
            }, {
                field: 'ShortDescription',
                title: 'Descripción breve',
                sortable: true,
                halign: 'center'
            }, {
                field: 'RecordStatus',
                title: 'Estado del registro',
                formatter: 'H5MantEstadoReglasSuscripcionSupport.LookUpForRecordStatusFormatter',
                sortable: true,
                halign: 'center'
            }]
        });


        $('#TabUnderwritingRuleSType_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabUnderwritingRuleSType_GridTbl');
            $('#TabUnderwritingRuleSType_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabUnderwritingRuleSType_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabUnderwritingRuleSType_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantEstadoReglasSuscripcionSupport.TabUnderwritingRuleSType_GridRowToInput(row);
                H5MantEstadoReglasSuscripcionSupport.TabUnderwritingRuleSType_Grid_delete(row, null);
                
                return row.UnderwritingRuleStatus;
            });

            $('#TabUnderwritingRuleSType_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabUnderwritingRuleSType_GridCreateBtn').click(function () {
            var formInstance = $("#TabUnderwritingRuleSType_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantEstadoReglasSuscripcionSupport.TabUnderwritingRuleSType_GridShowModal($('#TabUnderwritingRuleSType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabUnderwritingRuleSType_GridPopup').find('#TabUnderwritingRuleSType_GridSaveBtn').click(function () {
            var formInstance = $("#TabUnderwritingRuleSType_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabUnderwritingRuleSType_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabUnderwritingRuleSType_GridSaveBtn').html();
                $('#TabUnderwritingRuleSType_GridSaveBtn').html('Procesando...');
                $('#TabUnderwritingRuleSType_GridSaveBtn').prop('disabled', true);

                H5MantEstadoReglasSuscripcionSupport.currentRow.UnderwritingRuleStatus = generalSupport.NumericValue('#UnderwritingRuleStatus', -99999, 99999);
                H5MantEstadoReglasSuscripcionSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                H5MantEstadoReglasSuscripcionSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                H5MantEstadoReglasSuscripcionSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);
                H5MantEstadoReglasSuscripcionSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';
                H5MantEstadoReglasSuscripcionSupport.currentRow.Description = $('#Description').val();
                H5MantEstadoReglasSuscripcionSupport.currentRow.ShortDescription = $('#ShortDescription').val();
                H5MantEstadoReglasSuscripcionSupport.currentRow.RecordStatus = $('#RecordStatus').val();

                $('#TabUnderwritingRuleSType_GridSaveBtn').prop('disabled', false);
                $('#TabUnderwritingRuleSType_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantEstadoReglasSuscripcionSupport.TabUnderwritingRuleSType_Grid_update(H5MantEstadoReglasSuscripcionSupport.currentRow, $modal);
                }
                else {                    
                    H5MantEstadoReglasSuscripcionSupport.TabUnderwritingRuleSType_Grid_insert(H5MantEstadoReglasSuscripcionSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabUnderwritingRuleSType_GridShowModal = function (md, title, row) {
        row = row || { UnderwritingRuleStatus: 0, CreatorUserCode: 0, CreationDate: null, UpdateUserCode: 0, UpdateDate: null, Description: null, ShortDescription: null, RecordStatus: 0 };

        md.data('id', row.UnderwritingRuleStatus);
        md.find('.modal-title').text(title);

        H5MantEstadoReglasSuscripcionSupport.TabUnderwritingRuleSType_GridRowToInput(row);
        $('#UnderwritingRuleStatus').prop('disabled', (row.UnderwritingRuleStatus !== 0));
        $('#CreatorUserCode').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        H5MantEstadoReglasSuscripcionSupport.TabUnderwritingRuleSType_Grid_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.TabUnderwritingRuleSType_GridRowToInput = function (row) {
        H5MantEstadoReglasSuscripcionSupport.currentRow = row;
        AutoNumeric.set('#UnderwritingRuleStatus', row.UnderwritingRuleStatus);
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#Description').val(row.Description);
        $('#ShortDescription').val(row.ShortDescription);
        H5MantEstadoReglasSuscripcionSupport.LookUpForRecordStatus(row.RecordStatus, '');
        $('#RecordStatus').trigger('change');

    };
    this.TabUnderwritingRuleSType_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantEstadoReglasSuscripcionActions.aspx/TabUnderwritingRuleSType_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                TransUnderwritingRuleSTypeLanguageId1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#TabUnderwritingRuleSType_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.TabUnderwritingRuleSTypeTranslator_GridTblSetup = function (table) {
        H5MantEstadoReglasSuscripcionSupport.LookUpForLanguageIdTranslator('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'UnderwritingRuleStatus',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabUnderwritingRuleSTypeTranslator_Gridtoolbar',
            columns: [{
                field: 'UnderwritingRuleStatus',
                title: 'Estado de la regla de suscripción',
                formatter: 'H5MantEstadoReglasSuscripcionSupport.UnderwritingRuleStatusTranslator_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LanguageId',
                title: 'Idioma',
                formatter: 'H5MantEstadoReglasSuscripcionSupport.LookUpForLanguageIdTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Description',
                title: 'Descripción',
                events: 'TabUnderwritingRuleSTypeTranslator_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'ShortDescription',
                title: 'Descripción breve',
                sortable: true,
                halign: 'center'
            }]
        });


        $('#TabUnderwritingRuleSTypeTranslator_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabUnderwritingRuleSTypeTranslator_GridTbl');
            $('#TabUnderwritingRuleSTypeTranslator_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabUnderwritingRuleSTypeTranslator_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabUnderwritingRuleSTypeTranslator_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantEstadoReglasSuscripcionSupport.TabUnderwritingRuleSTypeTranslator_GridRowToInput(row);
                
                
                return row.UnderwritingRuleStatus;
            });
            
          $('#TabUnderwritingRuleSTypeTranslator_GridTbl').bootstrapTable('remove', {
                field: 'UnderwritingRuleStatus',
                values: ids
           });

            $('#TabUnderwritingRuleSTypeTranslator_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabUnderwritingRuleSTypeTranslator_GridCreateBtn').click(function () {
            var formInstance = $("#TabUnderwritingRuleSTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantEstadoReglasSuscripcionSupport.TabUnderwritingRuleSTypeTranslator_GridShowModal($('#TabUnderwritingRuleSTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabUnderwritingRuleSTypeTranslator_GridPopup').find('#TabUnderwritingRuleSTypeTranslator_GridSaveBtn').click(function () {
            var formInstance = $("#TabUnderwritingRuleSTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabUnderwritingRuleSTypeTranslator_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabUnderwritingRuleSTypeTranslator_GridSaveBtn').html();
                $('#TabUnderwritingRuleSTypeTranslator_GridSaveBtn').html('Procesando...');
                $('#TabUnderwritingRuleSTypeTranslator_GridSaveBtn').prop('disabled', true);

                H5MantEstadoReglasSuscripcionSupport.currentRow.UnderwritingRuleStatus = generalSupport.NumericValue('#UnderwritingRuleStatusTranslator', -99999, 99999);
                H5MantEstadoReglasSuscripcionSupport.currentRow.LanguageId = parseInt(0 + $('#LanguageIdTranslator').val(), 10);
                H5MantEstadoReglasSuscripcionSupport.currentRow.Description = $('#DescriptionTranslator').val();
                H5MantEstadoReglasSuscripcionSupport.currentRow.ShortDescription = $('#ShortDescriptionTranslator').val();

                $('#TabUnderwritingRuleSTypeTranslator_GridSaveBtn').prop('disabled', false);
                $('#TabUnderwritingRuleSTypeTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantEstadoReglasSuscripcionSupport.TabUnderwritingRuleSTypeTranslator_Grid_update(H5MantEstadoReglasSuscripcionSupport.currentRow, $modal);
                }
                else {                    
                    $('#TabUnderwritingRuleSTypeTranslator_GridTbl').bootstrapTable('append', H5MantEstadoReglasSuscripcionSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabUnderwritingRuleSTypeTranslator_GridShowModal = function (md, title, row) {
        row = row || { UnderwritingRuleStatus: 0, LanguageId: 0, Description: null, ShortDescription: null };

        md.data('id', row.UnderwritingRuleStatus);
        md.find('.modal-title').text(title);

        H5MantEstadoReglasSuscripcionSupport.TabUnderwritingRuleSTypeTranslator_GridRowToInput(row);
        $('#UnderwritingRuleStatusTranslator').prop('disabled', true);
        $('#LanguageIdTranslator').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabUnderwritingRuleSTypeTranslator_GridRowToInput = function (row) {
        H5MantEstadoReglasSuscripcionSupport.currentRow = row;
        AutoNumeric.set('#UnderwritingRuleStatusTranslator', row.UnderwritingRuleStatus);
        H5MantEstadoReglasSuscripcionSupport.LookUpForLanguageIdTranslator(row.LanguageId, '');
        $('#LanguageIdTranslator').trigger('change');
        $('#DescriptionTranslator').val(row.Description);
        $('#ShortDescriptionTranslator').val(row.ShortDescription);

    };
    this.TabUnderwritingRuleSTypeTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantEstadoReglasSuscripcionActions.aspx/TabUnderwritingRuleSTypeTranslator_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#TabUnderwritingRuleSTypeTranslator_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.UnderwritingRuleStatus_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CreatorUserCode_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
        });
      };
    this.UpdateUserCode_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
        });
      };
    this.UnderwritingRuleStatusTranslator_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };





  this.Init = function(){
    securitySupport.ValidateAccessRoles(['EASE1', 'Suscriptor']);
    moment.locale(generalSupport.UserContext().languageName);
    
   generalSupport.TranslateInit(generalSupport.GetCurrentName(), function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        H5MantEstadoReglasSuscripcionSupport.ValidateSetup();
        tableHelperSupport.Translate('#TabUnderwritingRuleSType_GridTbl', '#TabUnderwritingRuleSType_GridTbl');
tableHelperSupport.Translate('#TabUnderwritingRuleSTypeTranslator_GridTbl', '#TabUnderwritingRuleSTypeTranslator_GridTbl');

    });
        

    H5MantEstadoReglasSuscripcionSupport.ControlBehaviour();
    H5MantEstadoReglasSuscripcionSupport.ControlActions();
    

    $("#TabUnderwritingRuleSType_GridTblPlaceHolder").replaceWith('<table id="TabUnderwritingRuleSType_GridTbl"></table>');
    H5MantEstadoReglasSuscripcionSupport.TabUnderwritingRuleSType_GridTblSetup($('#TabUnderwritingRuleSType_GridTbl'));
    $("#TabUnderwritingRuleSTypeTranslator_GridTblPlaceHolder").replaceWith('<table id="TabUnderwritingRuleSTypeTranslator_GridTbl"></table>');
    H5MantEstadoReglasSuscripcionSupport.TabUnderwritingRuleSTypeTranslator_GridTblSetup($('#TabUnderwritingRuleSTypeTranslator_GridTbl'));

        H5MantEstadoReglasSuscripcionSupport.TabUnderwritingRuleSType_GridTblRequest();
        H5MantEstadoReglasSuscripcionSupport.TabUnderwritingRuleSTypeTranslator_GridTblRequest();




  };
};

$(document).ready(function () {
   H5MantEstadoReglasSuscripcionSupport.Init();
});

window.TabUnderwritingRuleSType_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantEstadoReglasSuscripcionSupport.TabUnderwritingRuleSType_GridShowModal($('#TabUnderwritingRuleSType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.TabUnderwritingRuleSTypeTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantEstadoReglasSuscripcionSupport.TabUnderwritingRuleSTypeTranslator_GridShowModal($('#TabUnderwritingRuleSTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
