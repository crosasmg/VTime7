var H5MantMovimientoCasoSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5MantMovimientoCasoFormId').val(),
            TabManualOrAutomaticType_Grid_TabManualOrAutomaticType_Item: generalSupport.NormalizeProperties($('#TabManualOrAutomaticType_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate'),
            TabManualOrAutomaticTypeTranslator_Grid_TabManualOrAutomaticType_Item: generalSupport.NormalizeProperties($('#TabManualOrAutomaticTypeTranslator_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#H5MantMovimientoCasoFormId').val(data.InstanceFormId);

        H5MantMovimientoCasoSupport.LookUpForRecordStatus(source);
        H5MantMovimientoCasoSupport.LookUpForLanguageIdTranslator(source);

        H5MantMovimientoCasoSupport.TabManualOrAutomaticType_GridTblRequest();
        if (data.TabManualOrAutomaticType_Grid_TabManualOrAutomaticType_Item !== null)
            $('#TabManualOrAutomaticType_GridTbl').bootstrapTable('load', data.TabManualOrAutomaticType_Grid_TabManualOrAutomaticType_Item);
        H5MantMovimientoCasoSupport.TabManualOrAutomaticTypeTranslator_GridTblRequest();
        if (data.TabManualOrAutomaticTypeTranslator_Grid_TabManualOrAutomaticType_Item !== null)
            $('#TabManualOrAutomaticTypeTranslator_GridTbl').bootstrapTable('load', data.TabManualOrAutomaticTypeTranslator_Grid_TabManualOrAutomaticType_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#ManualOrAutomatic', {
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
      new AutoNumeric('#ManualOrAutomaticTranslator', {
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
					         H5MantMovimientoCasoSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5MantMovimientoCasoSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.TabManualOrAutomaticType_Grid_insert = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMovimientoCasoActions.aspx/TabManualOrAutomaticType_Grid1InsertCommandActionTabManualOrAutomaticType", false,
               JSON.stringify({ MANUALORAUTOMATIC1: row.ManualOrAutomatic, RECORDSTATUS2: row.RecordStatus, CREATORUSERCODE2: generalSupport.UserContext().userId, UPDATEUSERCODE4: generalSupport.UserContext().userId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMovimientoCasoActions.aspx/TabManualOrAutomaticType_Grid3InsertCommandActionTransManualOrAutomaticType", false,
               JSON.stringify({ MANUALORAUTOMATIC1: row.ManualOrAutomatic, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabManualOrAutomaticType_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message7 = 'Se agregó correctamente el registro';
            notification.toastr.success('Agregar registro', message7);
                    }                    
                    else {
            var message8 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message8);

                        }

    };
    this.TabManualOrAutomaticType_Grid_update = function (row, $modal) {
          var data;
            var recordCount;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMovimientoCasoActions.aspx/TabManualOrAutomaticType_Grid1UpdateCommandActionTabManualOrAutomaticType", false,
               JSON.stringify({ RECORDSTATUS1: row.RecordStatus, UPDATEUSERCODE1: generalSupport.UserContext().userId, TabManualOrAutomaticTypeManualOrAutomatic3: row.ManualOrAutomatic }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMovimientoCasoActions.aspx/TabManualOrAutomaticType_Grid3SelectCommandActionTransManualOrAutomaticType", false,
               JSON.stringify({                 TransManualOrAutomaticTypeManualOrAutomatic1: row.ManualOrAutomatic,
                TransManualOrAutomaticTypeLanguageId2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMovimientoCasoActions.aspx/TabManualOrAutomaticType_Grid5InsertCommandActionTransManualOrAutomaticType", false,
               JSON.stringify({ MANUALORAUTOMATIC1: row.ManualOrAutomatic, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMovimientoCasoActions.aspx/TabManualOrAutomaticType_Grid6UpdateCommandActionTransManualOrAutomaticType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransManualOrAutomaticTypeManualOrAutomatic4: row.ManualOrAutomatic, TransManualOrAutomaticTypeLanguageId5: generalSupport.SessionContext().languageId }));
               


                        }

                    }
        if (data.d.Success === true){
            $('#TabManualOrAutomaticType_GridTbl').bootstrapTable('updateByUniqueId', { id: row.ManualOrAutomatic, row: row });
            $modal.modal('hide');
            var message9 = 'Se actualizó correctamente el registro';
            notification.toastr.success('Actualizar registro', message9);
                        }                        
                        else {
            var message10 = 'No se pudo actualizar el registro';
            notification.swal.error('Actualizar registro', message10);

                            }

    };
    this.TabManualOrAutomaticType_Grid_delete = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMovimientoCasoActions.aspx/TabManualOrAutomaticType_Grid1DeleteCommandActionTransManualOrAutomaticType", false,
               JSON.stringify({ TransManualOrAutomaticTypeManualOrAutomatic1: row.ManualOrAutomatic }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMovimientoCasoActions.aspx/TabManualOrAutomaticType_Grid3DeleteCommandActionTabManualOrAutomaticType", false,
               JSON.stringify({ TabManualOrAutomaticTypeManualOrAutomatic1: row.ManualOrAutomatic }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabManualOrAutomaticType_GridTbl').bootstrapTable('remove', {field: 'ManualOrAutomatic', values: [generalSupport.NumericValue('#ManualOrAutomatic', -99999, 99999)]});
            var message7 = 'Se eliminó correctamente el registro';
            notification.toastr.success('Eliminar registro', message7);
                    }                    
                    else {
            var message8 = 'No se puede eliminar el registro';
            notification.toastr.error('Eliminar registro', message8);

                        }

    };
    this.TabManualOrAutomaticType_Grid_BeforeShowPopup = function (row, $modal) {
          var data;
            var nextId;
        if (row.ManualOrAutomatic === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMovimientoCasoActions.aspx/TabManualOrAutomaticType_Grid2SelectCommandActionTabManualOrAutomaticType", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#ManualOrAutomatic', nextId);

            }

    };
    this.TabManualOrAutomaticTypeTranslator_Grid_update = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMovimientoCasoActions.aspx/TabManualOrAutomaticTypeTranslator_Grid1UpdateCommandActionTransManualOrAutomaticType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransManualOrAutomaticTypeManualOrAutomatic4: row.ManualOrAutomatic, TransManualOrAutomaticTypeLanguageId5: row.LanguageId }));
               

        if (data.d.Success === true){
            $('#TabManualOrAutomaticTypeTranslator_GridTbl').bootstrapTable('updateByUniqueId', { id: row.ManualOrAutomatic, row: row });
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
            var formInstance = $("#H5MantMovimientoCasoMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                H5MantMovimientoCasoSupport.TabManualOrAutomaticType_GridTblRequest();
                $('#TabManualOrAutomaticType_GridContainer').toggleClass('hidden', false);
                $('#TabManualOrAutomaticTypeTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#H5MantMovimientoCasoMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                H5MantMovimientoCasoSupport.TabManualOrAutomaticTypeTranslator_GridTblRequest();
                $('#TabManualOrAutomaticTypeTranslator_GridContainer').toggleClass('hidden', false);
                $('#TabManualOrAutomaticType_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5MantMovimientoCasoMainForm").validate({
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
        $("#TabManualOrAutomaticType_GridEditForm").validate({
            rules: {
                ManualOrAutomatic: {
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
                ManualOrAutomatic: {
                    AutoNumericMinValue: $.i18n.t('app.validation.ManualOrAutomatic.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.ManualOrAutomatic.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.ManualOrAutomatic.required')
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
        $("#TabManualOrAutomaticTypeTranslator_GridEditForm").validate({
            rules: {
                ManualOrAutomaticTranslator: {
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
                ManualOrAutomaticTranslator: {
                    AutoNumericMinValue: $.i18n.t('app.validation.ManualOrAutomaticTranslator.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.ManualOrAutomaticTranslator.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.ManualOrAutomaticTranslator.required')
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantMovimientoCasoActions.aspx/LookUpForRecordStatus", false,
                JSON.stringify({ id: $('#H5MantMovimientoCasoFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantMovimientoCasoActions.aspx/LookUpForLanguageIdTranslator", false,
                JSON.stringify({ id: $('#H5MantMovimientoCasoFormId').val() }),
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

    this.TabManualOrAutomaticType_GridTblSetup = function (table) {
        H5MantMovimientoCasoSupport.LookUpForRecordStatus('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'ManualOrAutomatic',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabManualOrAutomaticType_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'ManualOrAutomatic',
                title: 'Manual o automático',
                events: 'TabManualOrAutomaticType_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'RecordStatus',
                title: 'Estado del registro',
                formatter: 'H5MantMovimientoCasoSupport.LookUpForRecordStatusFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5MantMovimientoCasoSupport.CreatorUserCode_FormatterMaskData',
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
                formatter: 'H5MantMovimientoCasoSupport.UpdateUserCode_FormatterMaskData',
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


        $('#TabManualOrAutomaticType_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabManualOrAutomaticType_GridTbl');
            $('#TabManualOrAutomaticType_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabManualOrAutomaticType_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabManualOrAutomaticType_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantMovimientoCasoSupport.TabManualOrAutomaticType_GridRowToInput(row);
                H5MantMovimientoCasoSupport.TabManualOrAutomaticType_Grid_delete(row, null);
                
                return row.ManualOrAutomatic;
            });

            $('#TabManualOrAutomaticType_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabManualOrAutomaticType_GridCreateBtn').click(function () {
            var formInstance = $("#TabManualOrAutomaticType_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantMovimientoCasoSupport.TabManualOrAutomaticType_GridShowModal($('#TabManualOrAutomaticType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabManualOrAutomaticType_GridPopup').find('#TabManualOrAutomaticType_GridSaveBtn').click(function () {
            var formInstance = $("#TabManualOrAutomaticType_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabManualOrAutomaticType_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabManualOrAutomaticType_GridSaveBtn').html();
                $('#TabManualOrAutomaticType_GridSaveBtn').html('Procesando...');
                $('#TabManualOrAutomaticType_GridSaveBtn').prop('disabled', true);

                H5MantMovimientoCasoSupport.currentRow.ManualOrAutomatic = generalSupport.NumericValue('#ManualOrAutomatic', -99999, 99999);
                H5MantMovimientoCasoSupport.currentRow.RecordStatus = $('#RecordStatus').val();
                H5MantMovimientoCasoSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                H5MantMovimientoCasoSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                H5MantMovimientoCasoSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);
                H5MantMovimientoCasoSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';
                H5MantMovimientoCasoSupport.currentRow.Description = $('#Description').val();
                H5MantMovimientoCasoSupport.currentRow.ShortDescription = $('#ShortDescription').val();

                $('#TabManualOrAutomaticType_GridSaveBtn').prop('disabled', false);
                $('#TabManualOrAutomaticType_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantMovimientoCasoSupport.TabManualOrAutomaticType_Grid_update(H5MantMovimientoCasoSupport.currentRow, $modal);
                }
                else {                    
                    H5MantMovimientoCasoSupport.TabManualOrAutomaticType_Grid_insert(H5MantMovimientoCasoSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabManualOrAutomaticType_GridShowModal = function (md, title, row) {
        row = row || { ManualOrAutomatic: 0, RecordStatus: 0, CreatorUserCode: 0, CreationDate: null, UpdateUserCode: 0, UpdateDate: null, Description: null, ShortDescription: null };

        md.data('id', row.ManualOrAutomatic);
        md.find('.modal-title').text(title);

        H5MantMovimientoCasoSupport.TabManualOrAutomaticType_GridRowToInput(row);
        $('#ManualOrAutomatic').prop('disabled', (row.ManualOrAutomatic !== 0));
        $('#CreatorUserCode').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        H5MantMovimientoCasoSupport.TabManualOrAutomaticType_Grid_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.TabManualOrAutomaticType_GridRowToInput = function (row) {
        H5MantMovimientoCasoSupport.currentRow = row;
        AutoNumeric.set('#ManualOrAutomatic', row.ManualOrAutomatic);
        H5MantMovimientoCasoSupport.LookUpForRecordStatus(row.RecordStatus, '');
        $('#RecordStatus').trigger('change');
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#Description').val(row.Description);
        $('#ShortDescription').val(row.ShortDescription);

    };
    this.TabManualOrAutomaticType_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantMovimientoCasoActions.aspx/TabManualOrAutomaticType_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                TransManualOrAutomaticTypeLanguageId1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#TabManualOrAutomaticType_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.TabManualOrAutomaticTypeTranslator_GridTblSetup = function (table) {
        H5MantMovimientoCasoSupport.LookUpForLanguageIdTranslator('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'ManualOrAutomatic',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabManualOrAutomaticTypeTranslator_Gridtoolbar',
            columns: [{
                field: 'ManualOrAutomatic',
                title: 'Movimiento',
                formatter: 'H5MantMovimientoCasoSupport.ManualOrAutomaticTranslator_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LanguageId',
                title: 'Lenguaje Id',
                formatter: 'H5MantMovimientoCasoSupport.LookUpForLanguageIdTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Description',
                title: 'Descripción',
                events: 'TabManualOrAutomaticTypeTranslator_GridActionEvents',
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


        $('#TabManualOrAutomaticTypeTranslator_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabManualOrAutomaticTypeTranslator_GridTbl');
            $('#TabManualOrAutomaticTypeTranslator_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabManualOrAutomaticTypeTranslator_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabManualOrAutomaticTypeTranslator_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantMovimientoCasoSupport.TabManualOrAutomaticTypeTranslator_GridRowToInput(row);
                
                
                return row.ManualOrAutomatic;
            });
            
          $('#TabManualOrAutomaticTypeTranslator_GridTbl').bootstrapTable('remove', {
                field: 'ManualOrAutomatic',
                values: ids
           });

            $('#TabManualOrAutomaticTypeTranslator_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabManualOrAutomaticTypeTranslator_GridCreateBtn').click(function () {
            var formInstance = $("#TabManualOrAutomaticTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantMovimientoCasoSupport.TabManualOrAutomaticTypeTranslator_GridShowModal($('#TabManualOrAutomaticTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabManualOrAutomaticTypeTranslator_GridPopup').find('#TabManualOrAutomaticTypeTranslator_GridSaveBtn').click(function () {
            var formInstance = $("#TabManualOrAutomaticTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabManualOrAutomaticTypeTranslator_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabManualOrAutomaticTypeTranslator_GridSaveBtn').html();
                $('#TabManualOrAutomaticTypeTranslator_GridSaveBtn').html('Procesando...');
                $('#TabManualOrAutomaticTypeTranslator_GridSaveBtn').prop('disabled', true);

                H5MantMovimientoCasoSupport.currentRow.ManualOrAutomatic = generalSupport.NumericValue('#ManualOrAutomaticTranslator', -99999, 99999);
                H5MantMovimientoCasoSupport.currentRow.LanguageId = parseInt(0 + $('#LanguageIdTranslator').val(), 10);
                H5MantMovimientoCasoSupport.currentRow.Description = $('#DescriptionTranslator').val();
                H5MantMovimientoCasoSupport.currentRow.ShortDescription = $('#ShortDescriptionTranslator').val();

                $('#TabManualOrAutomaticTypeTranslator_GridSaveBtn').prop('disabled', false);
                $('#TabManualOrAutomaticTypeTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantMovimientoCasoSupport.TabManualOrAutomaticTypeTranslator_Grid_update(H5MantMovimientoCasoSupport.currentRow, $modal);
                }
                else {                    
                    $('#TabManualOrAutomaticTypeTranslator_GridTbl').bootstrapTable('append', H5MantMovimientoCasoSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabManualOrAutomaticTypeTranslator_GridShowModal = function (md, title, row) {
        row = row || { ManualOrAutomatic: 0, LanguageId: 0, Description: null, ShortDescription: null };

        md.data('id', row.ManualOrAutomatic);
        md.find('.modal-title').text(title);

        H5MantMovimientoCasoSupport.TabManualOrAutomaticTypeTranslator_GridRowToInput(row);
        $('#ManualOrAutomaticTranslator').prop('disabled', true);
        $('#LanguageIdTranslator').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabManualOrAutomaticTypeTranslator_GridRowToInput = function (row) {
        H5MantMovimientoCasoSupport.currentRow = row;
        AutoNumeric.set('#ManualOrAutomaticTranslator', row.ManualOrAutomatic);
        H5MantMovimientoCasoSupport.LookUpForLanguageIdTranslator(row.LanguageId, '');
        $('#LanguageIdTranslator').trigger('change');
        $('#DescriptionTranslator').val(row.Description);
        $('#ShortDescriptionTranslator').val(row.ShortDescription);

    };
    this.TabManualOrAutomaticTypeTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantMovimientoCasoActions.aspx/TabManualOrAutomaticTypeTranslator_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#TabManualOrAutomaticTypeTranslator_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.ManualOrAutomatic_FormatterMaskData = function (value, row, index) {          
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
    this.ManualOrAutomaticTranslator_FormatterMaskData = function (value, row, index) {          
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
        
        H5MantMovimientoCasoSupport.ValidateSetup();
        tableHelperSupport.Translate('#TabManualOrAutomaticType_GridTbl', '#TabManualOrAutomaticType_GridTbl');
tableHelperSupport.Translate('#TabManualOrAutomaticTypeTranslator_GridTbl', '#TabManualOrAutomaticTypeTranslator_GridTbl');

    });
        

    H5MantMovimientoCasoSupport.ControlBehaviour();
    H5MantMovimientoCasoSupport.ControlActions();
    

    $("#TabManualOrAutomaticType_GridTblPlaceHolder").replaceWith('<table id="TabManualOrAutomaticType_GridTbl"></table>');
    H5MantMovimientoCasoSupport.TabManualOrAutomaticType_GridTblSetup($('#TabManualOrAutomaticType_GridTbl'));
    $("#TabManualOrAutomaticTypeTranslator_GridTblPlaceHolder").replaceWith('<table id="TabManualOrAutomaticTypeTranslator_GridTbl"></table>');
    H5MantMovimientoCasoSupport.TabManualOrAutomaticTypeTranslator_GridTblSetup($('#TabManualOrAutomaticTypeTranslator_GridTbl'));

        H5MantMovimientoCasoSupport.TabManualOrAutomaticType_GridTblRequest();
        H5MantMovimientoCasoSupport.TabManualOrAutomaticTypeTranslator_GridTblRequest();




  };
};

$(document).ready(function () {
   H5MantMovimientoCasoSupport.Init();
});

window.TabManualOrAutomaticType_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantMovimientoCasoSupport.TabManualOrAutomaticType_GridShowModal($('#TabManualOrAutomaticType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.TabManualOrAutomaticTypeTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantMovimientoCasoSupport.TabManualOrAutomaticTypeTranslator_GridShowModal($('#TabManualOrAutomaticTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
