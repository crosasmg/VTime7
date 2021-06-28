var H5MantTipoDeDecisionSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5MantTipoDeDecisionFormId').val(),
            TabDecisionType_Grid_TabDecisionType_Item: generalSupport.NormalizeProperties($('#TabDecisionType_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate'),
            TabDecisionTypeTranslator_Grid_TabDecisionType_Item: generalSupport.NormalizeProperties($('#TabDecisionTypeTranslator_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#H5MantTipoDeDecisionFormId').val(data.InstanceFormId);

        H5MantTipoDeDecisionSupport.LookUpForRecordStatus(source);
        H5MantTipoDeDecisionSupport.LookUpForLanguageIdTranslator(source);

        H5MantTipoDeDecisionSupport.TabDecisionType_GridTblRequest();
        if (data.TabDecisionType_Grid_TabDecisionType_Item !== null)
            $('#TabDecisionType_GridTbl').bootstrapTable('load', data.TabDecisionType_Grid_TabDecisionType_Item);
        H5MantTipoDeDecisionSupport.TabDecisionTypeTranslator_GridTblRequest();
        if (data.TabDecisionTypeTranslator_Grid_TabDecisionType_Item !== null)
            $('#TabDecisionTypeTranslator_GridTbl').bootstrapTable('load', data.TabDecisionTypeTranslator_Grid_TabDecisionType_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#Decision', {
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
      new AutoNumeric('#DecisionTranslator', {
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
					         H5MantTipoDeDecisionSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5MantTipoDeDecisionSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.TabDecisionType_Grid_insert = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoDeDecisionActions.aspx/TabDecisionType_Grid1InsertCommandActionTabDecisionType", false,
               JSON.stringify({ DECISION1: row.Decision, RECORDSTATUS2: row.RecordStatus, CREATORUSERCODE2: generalSupport.UserContext().userId, UPDATEUSERCODE4: generalSupport.UserContext().userId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoDeDecisionActions.aspx/TabDecisionType_Grid3InsertCommandActionTransDecisionType", false,
               JSON.stringify({ DECISION1: row.Decision, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabDecisionType_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message7 = 'Se agregó correctamente el registro';
            notification.toastr.success('Agregar registro', message7);
                    }                    
                    else {
            var message8 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message8);

                        }

    };
    this.TabDecisionType_Grid_update = function (row, $modal) {
          var data;
            var recordCount;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoDeDecisionActions.aspx/TabDecisionType_Grid1UpdateCommandActionTabDecisionType", false,
               JSON.stringify({ RECORDSTATUS1: row.RecordStatus, UPDATEUSERCODE1: generalSupport.UserContext().userId, TabDecisionTypeDecision3: row.Decision }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoDeDecisionActions.aspx/TabDecisionType_Grid3SelectCommandActionTransDecisionType", false,
               JSON.stringify({                 TransDecisionTypeDecision1: row.Decision,
                TransDecisionTypeLanguageId2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoDeDecisionActions.aspx/TabDecisionType_Grid5InsertCommandActionTransDecisionType", false,
               JSON.stringify({ DECISION1: row.Decision, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoDeDecisionActions.aspx/TabDecisionType_Grid6UpdateCommandActionTransDecisionType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransDecisionTypeDecision4: row.Decision, TransDecisionTypeLanguageId5: generalSupport.SessionContext().languageId }));
               


                        }

                    }
        if (data.d.Success === true){
            $('#TabDecisionType_GridTbl').bootstrapTable('updateByUniqueId', { id: row.Decision, row: row });
            $modal.modal('hide');
            var message9 = 'Se actualizó correctamente el registro';
            notification.toastr.success('Actualizar registro', message9);
                        }                        
                        else {
            var message10 = 'No se pudo actualizar el registro';
            notification.swal.error('Actualizar registro', message10);

                            }

    };
    this.TabDecisionType_Grid_delete = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoDeDecisionActions.aspx/TabDecisionType_Grid1DeleteCommandActionTransDecisionType", false,
               JSON.stringify({ TransDecisionTypeDecision1: row.Decision }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoDeDecisionActions.aspx/TabDecisionType_Grid3DeleteCommandActionTabDecisionType", false,
               JSON.stringify({ TabDecisionTypeDecision1: row.Decision }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabDecisionType_GridTbl').bootstrapTable('remove', {field: 'Decision', values: [generalSupport.NumericValue('#Decision', -99999, 99999)]});
            var message7 = 'Se eliminó correctamente el registro';
            notification.toastr.success('Eliminar registro', message7);
                    }                    
                    else {
            var message8 = 'No se puede eliminar el registro';
            notification.toastr.error('Eliminar registro', message8);

                        }

    };
    this.TabDecisionType_Grid_BeforeShowPopup = function (row, $modal) {
          var data;
            var nextId;
        if (row.Decision === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoDeDecisionActions.aspx/TabDecisionType_Grid2SelectCommandActionTabDecisionType", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#Decision', nextId);

            }

    };
    this.TabDecisionTypeTranslator_Grid_update = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoDeDecisionActions.aspx/TabDecisionTypeTranslator_Grid1UpdateCommandActionTransDecisionType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransDecisionTypeDecision4: row.Decision, TransDecisionTypeLanguageId5: row.LanguageId }));
               

        if (data.d.Success === true){
            $('#TabDecisionTypeTranslator_GridTbl').bootstrapTable('updateByUniqueId', { id: row.Decision, row: row });
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
            var formInstance = $("#H5MantTipoDeDecisionMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                H5MantTipoDeDecisionSupport.TabDecisionType_GridTblRequest();
                $('#TabDecisionType_GridContainer').toggleClass('hidden', false);
                $('#TabDecisionTypeTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#H5MantTipoDeDecisionMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                H5MantTipoDeDecisionSupport.TabDecisionTypeTranslator_GridTblRequest();
                $('#TabDecisionTypeTranslator_GridContainer').toggleClass('hidden', false);
                $('#TabDecisionType_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5MantTipoDeDecisionMainForm").validate({
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
        $("#TabDecisionType_GridEditForm").validate({
            rules: {
                Decision: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999,
                    required: true
                },
                RecordStatus: {
                    required: true,
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
                }

            },
            messages: {
                Decision: {
                    AutoNumericMinValue: $.i18n.t('app.validation.Decision.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.Decision.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.Decision.required')
                },
                RecordStatus: {
                    required: $.i18n.t('app.validation.RecordStatus.required'),
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
                }

            }
        });
        $("#TabDecisionTypeTranslator_GridEditForm").validate({
            rules: {
                DecisionTranslator: {
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
                DecisionTranslator: {
                    AutoNumericMinValue: $.i18n.t('app.validation.DecisionTranslator.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.DecisionTranslator.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.DecisionTranslator.required')
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTipoDeDecisionActions.aspx/LookUpForRecordStatus", false,
                JSON.stringify({ id: $('#H5MantTipoDeDecisionFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTipoDeDecisionActions.aspx/LookUpForLanguageIdTranslator", false,
                JSON.stringify({ id: $('#H5MantTipoDeDecisionFormId').val() }),
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

    this.TabDecisionType_GridTblSetup = function (table) {
        H5MantTipoDeDecisionSupport.LookUpForRecordStatus('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'Decision',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabDecisionType_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'Decision',
                title: 'Decisión',
                events: 'TabDecisionType_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'RecordStatus',
                title: 'Estado del registro',
                formatter: 'H5MantTipoDeDecisionSupport.LookUpForRecordStatusFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5MantTipoDeDecisionSupport.CreatorUserCode_FormatterMaskData',
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
                formatter: 'H5MantTipoDeDecisionSupport.UpdateUserCode_FormatterMaskData',
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
            }]
        });


        $('#TabDecisionType_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabDecisionType_GridTbl');
            $('#TabDecisionType_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabDecisionType_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabDecisionType_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantTipoDeDecisionSupport.TabDecisionType_GridRowToInput(row);
                H5MantTipoDeDecisionSupport.TabDecisionType_Grid_delete(row, null);
                
                return row.Decision;
            });

            $('#TabDecisionType_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabDecisionType_GridCreateBtn').click(function () {
            var formInstance = $("#TabDecisionType_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantTipoDeDecisionSupport.TabDecisionType_GridShowModal($('#TabDecisionType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabDecisionType_GridPopup').find('#TabDecisionType_GridSaveBtn').click(function () {
            var formInstance = $("#TabDecisionType_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabDecisionType_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabDecisionType_GridSaveBtn').html();
                $('#TabDecisionType_GridSaveBtn').html('Procesando...');
                $('#TabDecisionType_GridSaveBtn').prop('disabled', true);

                H5MantTipoDeDecisionSupport.currentRow.Decision = generalSupport.NumericValue('#Decision', -99999, 99999);
                H5MantTipoDeDecisionSupport.currentRow.RecordStatus = $('#RecordStatus').val();
                H5MantTipoDeDecisionSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                H5MantTipoDeDecisionSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                H5MantTipoDeDecisionSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);
                H5MantTipoDeDecisionSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';
                H5MantTipoDeDecisionSupport.currentRow.Description = $('#Description').val();
                H5MantTipoDeDecisionSupport.currentRow.ShortDescription = $('#ShortDescription').val();

                $('#TabDecisionType_GridSaveBtn').prop('disabled', false);
                $('#TabDecisionType_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantTipoDeDecisionSupport.TabDecisionType_Grid_update(H5MantTipoDeDecisionSupport.currentRow, $modal);
                }
                else {                    
                    H5MantTipoDeDecisionSupport.TabDecisionType_Grid_insert(H5MantTipoDeDecisionSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabDecisionType_GridShowModal = function (md, title, row) {
        row = row || { Decision: 0, RecordStatus: 0, CreatorUserCode: 0, CreationDate: null, UpdateUserCode: 0, UpdateDate: null, Description: null, ShortDescription: null };

        md.data('id', row.Decision);
        md.find('.modal-title').text(title);

        H5MantTipoDeDecisionSupport.TabDecisionType_GridRowToInput(row);
        $('#Decision').prop('disabled', (row.Decision !== 0));
        $('#CreatorUserCode').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        H5MantTipoDeDecisionSupport.TabDecisionType_Grid_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.TabDecisionType_GridRowToInput = function (row) {
        H5MantTipoDeDecisionSupport.currentRow = row;
        AutoNumeric.set('#Decision', row.Decision);
        H5MantTipoDeDecisionSupport.LookUpForRecordStatus(row.RecordStatus, '');
        $('#RecordStatus').trigger('change');
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#Description').val(row.Description);
        $('#ShortDescription').val(row.ShortDescription);

    };
    this.TabDecisionType_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTipoDeDecisionActions.aspx/TabDecisionType_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                TransDecisionTypeLanguageId1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#TabDecisionType_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.TabDecisionTypeTranslator_GridTblSetup = function (table) {
        H5MantTipoDeDecisionSupport.LookUpForLanguageIdTranslator('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'Decision',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabDecisionTypeTranslator_Gridtoolbar',
            columns: [{
                field: 'Decision',
                title: 'Decisión',
                formatter: 'H5MantTipoDeDecisionSupport.DecisionTranslator_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LanguageId',
                title: 'Idioma',
                formatter: 'H5MantTipoDeDecisionSupport.LookUpForLanguageIdTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Description',
                title: 'Descripción',
                events: 'TabDecisionTypeTranslator_GridActionEvents',
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


        $('#TabDecisionTypeTranslator_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabDecisionTypeTranslator_GridTbl');
            $('#TabDecisionTypeTranslator_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabDecisionTypeTranslator_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabDecisionTypeTranslator_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantTipoDeDecisionSupport.TabDecisionTypeTranslator_GridRowToInput(row);
                
                
                return row.Decision;
            });
            
          $('#TabDecisionTypeTranslator_GridTbl').bootstrapTable('remove', {
                field: 'Decision',
                values: ids
           });

            $('#TabDecisionTypeTranslator_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabDecisionTypeTranslator_GridCreateBtn').click(function () {
            var formInstance = $("#TabDecisionTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantTipoDeDecisionSupport.TabDecisionTypeTranslator_GridShowModal($('#TabDecisionTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabDecisionTypeTranslator_GridPopup').find('#TabDecisionTypeTranslator_GridSaveBtn').click(function () {
            var formInstance = $("#TabDecisionTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabDecisionTypeTranslator_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabDecisionTypeTranslator_GridSaveBtn').html();
                $('#TabDecisionTypeTranslator_GridSaveBtn').html('Procesando...');
                $('#TabDecisionTypeTranslator_GridSaveBtn').prop('disabled', true);

                H5MantTipoDeDecisionSupport.currentRow.Decision = generalSupport.NumericValue('#DecisionTranslator', -99999, 99999);
                H5MantTipoDeDecisionSupport.currentRow.LanguageId = parseInt(0 + $('#LanguageIdTranslator').val(), 10);
                H5MantTipoDeDecisionSupport.currentRow.Description = $('#DescriptionTranslator').val();
                H5MantTipoDeDecisionSupport.currentRow.ShortDescription = $('#ShortDescriptionTranslator').val();

                $('#TabDecisionTypeTranslator_GridSaveBtn').prop('disabled', false);
                $('#TabDecisionTypeTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantTipoDeDecisionSupport.TabDecisionTypeTranslator_Grid_update(H5MantTipoDeDecisionSupport.currentRow, $modal);
                }
                else {                    
                    $('#TabDecisionTypeTranslator_GridTbl').bootstrapTable('append', H5MantTipoDeDecisionSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabDecisionTypeTranslator_GridShowModal = function (md, title, row) {
        row = row || { Decision: 0, LanguageId: 0, Description: null, ShortDescription: null };

        md.data('id', row.Decision);
        md.find('.modal-title').text(title);

        H5MantTipoDeDecisionSupport.TabDecisionTypeTranslator_GridRowToInput(row);
        $('#DecisionTranslator').prop('disabled', true);
        $('#LanguageIdTranslator').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabDecisionTypeTranslator_GridRowToInput = function (row) {
        H5MantTipoDeDecisionSupport.currentRow = row;
        AutoNumeric.set('#DecisionTranslator', row.Decision);
        H5MantTipoDeDecisionSupport.LookUpForLanguageIdTranslator(row.LanguageId, '');
        $('#LanguageIdTranslator').trigger('change');
        $('#DescriptionTranslator').val(row.Description);
        $('#ShortDescriptionTranslator').val(row.ShortDescription);

    };
    this.TabDecisionTypeTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTipoDeDecisionActions.aspx/TabDecisionTypeTranslator_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#TabDecisionTypeTranslator_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.Decision_FormatterMaskData = function (value, row, index) {          
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
    this.DecisionTranslator_FormatterMaskData = function (value, row, index) {          
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
        
        H5MantTipoDeDecisionSupport.ValidateSetup();
        tableHelperSupport.Translate('#TabDecisionType_GridTbl', '#TabDecisionType_GridTbl');
tableHelperSupport.Translate('#TabDecisionTypeTranslator_GridTbl', '#TabDecisionTypeTranslator_GridTbl');

    });
        

    H5MantTipoDeDecisionSupport.ControlBehaviour();
    H5MantTipoDeDecisionSupport.ControlActions();
    

    $("#TabDecisionType_GridTblPlaceHolder").replaceWith('<table id="TabDecisionType_GridTbl"></table>');
    H5MantTipoDeDecisionSupport.TabDecisionType_GridTblSetup($('#TabDecisionType_GridTbl'));
    $("#TabDecisionTypeTranslator_GridTblPlaceHolder").replaceWith('<table id="TabDecisionTypeTranslator_GridTbl"></table>');
    H5MantTipoDeDecisionSupport.TabDecisionTypeTranslator_GridTblSetup($('#TabDecisionTypeTranslator_GridTbl'));

        H5MantTipoDeDecisionSupport.TabDecisionType_GridTblRequest();
        H5MantTipoDeDecisionSupport.TabDecisionTypeTranslator_GridTblRequest();




  };
};

$(document).ready(function () {
   H5MantTipoDeDecisionSupport.Init();
});

window.TabDecisionType_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantTipoDeDecisionSupport.TabDecisionType_GridShowModal($('#TabDecisionType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.TabDecisionTypeTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantTipoDeDecisionSupport.TabDecisionTypeTranslator_GridShowModal($('#TabDecisionTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
