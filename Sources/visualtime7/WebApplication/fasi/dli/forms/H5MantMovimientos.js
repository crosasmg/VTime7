var H5MantMovimientosSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5MantMovimientosFormId').val(),
            TabEntryType_Grid_TabEntryType_Item: generalSupport.NormalizeProperties($('#TabEntryType_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate'),
            TabEntryTypeTranslator_Grid_TabEntryType_Item: generalSupport.NormalizeProperties($('#TabEntryTypeTranslator_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#H5MantMovimientosFormId').val(data.InstanceFormId);

        H5MantMovimientosSupport.LookUpForRecordStatus(source);
        H5MantMovimientosSupport.LookUpForLanguageIdTranslator(source);

        H5MantMovimientosSupport.TabEntryType_GridTblRequest();
        if (data.TabEntryType_Grid_TabEntryType_Item !== null)
            $('#TabEntryType_GridTbl').bootstrapTable('load', data.TabEntryType_Grid_TabEntryType_Item);
        H5MantMovimientosSupport.TabEntryTypeTranslator_GridTblRequest();
        if (data.TabEntryTypeTranslator_Grid_TabEntryType_Item !== null)
            $('#TabEntryTypeTranslator_GridTbl').bootstrapTable('load', data.TabEntryTypeTranslator_Grid_TabEntryType_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#EntryType', {
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
      new AutoNumeric('#EntryTypeTranslator', {
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
					         H5MantMovimientosSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5MantMovimientosSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.TabEntryType_Grid_insert = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMovimientosActions.aspx/TabEntryType_Grid1InsertCommandActionTabEntryType", false,
               JSON.stringify({ ENTRYTYPE1: row.EntryType, RECORDSTATUS2: row.RecordStatus, CREATORUSERCODE2: generalSupport.UserContext().userId, UPDATEUSERCODE4: generalSupport.UserContext().userId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMovimientosActions.aspx/TabEntryType_Grid3InsertCommandActionTransEntryType", false,
               JSON.stringify({ ENTRYTYPE1: row.EntryType, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabEntryType_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message7 = 'Se agregó correctamente el registro';
            notification.toastr.success('Agregar registro', message7);
                    }                    
                    else {
            var message8 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message8);

                        }

    };
    this.TabEntryType_Grid_update = function (row, $modal) {
          var data;
            var recordCount;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMovimientosActions.aspx/TabEntryType_Grid1UpdateCommandActionTabEntryType", false,
               JSON.stringify({ RECORDSTATUS1: row.RecordStatus, UPDATEUSERCODE1: generalSupport.UserContext().userId, TabEntryTypeEntryType3: row.EntryType }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMovimientosActions.aspx/TabEntryType_Grid3SelectCommandActionTransEntryType", false,
               JSON.stringify({                 TransEntryTypeEntryType1: row.EntryType,
                TransEntryTypeLanguageId2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMovimientosActions.aspx/TabEntryType_Grid5InsertCommandActionTransEntryType", false,
               JSON.stringify({ ENTRYTYPE1: row.EntryType, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMovimientosActions.aspx/TabEntryType_Grid6UpdateCommandActionTransEntryType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransEntryTypeEntryType4: row.EntryType, TransEntryTypeLanguageId5: generalSupport.SessionContext().languageId }));
               


                        }

                    }
        if (data.d.Success === true){
            $('#TabEntryType_GridTbl').bootstrapTable('updateByUniqueId', { id: row.EntryType, row: row });
            $modal.modal('hide');
            var message9 = 'Se actualizó correctamente el registro';
            notification.toastr.success('Actualizar registro', message9);
                        }                        
                        else {
            var message10 = 'No se pudo actualizar el registro';
            notification.swal.error('Actualizar registro', message10);

                            }

    };
    this.TabEntryType_Grid_delete = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMovimientosActions.aspx/TabEntryType_Grid1DeleteCommandActionTransEntryType", false,
               JSON.stringify({ TransEntryTypeEntryType1: row.EntryType }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMovimientosActions.aspx/TabEntryType_Grid3DeleteCommandActionTabEntryType", false,
               JSON.stringify({ TabEntryTypeEntryType1: row.EntryType }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabEntryType_GridTbl').bootstrapTable('remove', {field: 'EntryType', values: [generalSupport.NumericValue('#EntryType', -99999, 99999)]});
            var message7 = 'Se eliminó correctamente el registro';
            notification.toastr.success('Eliminar registro', message7);
                    }                    
                    else {
            var message8 = 'No se puede eliminar el registro';
            notification.toastr.error('Eliminar registro', message8);

                        }

    };
    this.TabEntryType_Grid_BeforeShowPopup = function (row, $modal) {
          var data;
            var nextId;
        if (row.EntryType === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMovimientosActions.aspx/TabEntryType_Grid2SelectCommandActionTabEntryType", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#EntryType', nextId);

            }

    };
    this.TabEntryTypeTranslator_Grid_update = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMovimientosActions.aspx/TabEntryTypeTranslator_Grid1UpdateCommandActionTransEntryType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransEntryTypeEntryType4: row.EntryType, TransEntryTypeLanguageId5: row.LanguageId }));
               

        if (data.d.Success === true){
            $('#TabEntryTypeTranslator_GridTbl').bootstrapTable('updateByUniqueId', { id: row.EntryType, row: row });
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
            var formInstance = $("#H5MantMovimientosMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                H5MantMovimientosSupport.TabEntryType_GridTblRequest();
                $('#TabEntryType_GridContainer').toggleClass('hidden', false);
                $('#TabEntryTypeTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#H5MantMovimientosMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                H5MantMovimientosSupport.TabEntryTypeTranslator_GridTblRequest();
                $('#TabEntryTypeTranslator_GridContainer').toggleClass('hidden', false);
                $('#TabEntryType_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5MantMovimientosMainForm").validate({
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
        $("#TabEntryType_GridEditForm").validate({
            rules: {
                EntryType: {
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
                EntryType: {
                    AutoNumericMinValue: $.i18n.t('app.validation.EntryType.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.EntryType.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.EntryType.required')
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
        $("#TabEntryTypeTranslator_GridEditForm").validate({
            rules: {
                EntryTypeTranslator: {
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
                EntryTypeTranslator: {
                    AutoNumericMinValue: $.i18n.t('app.validation.EntryTypeTranslator.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.EntryTypeTranslator.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.EntryTypeTranslator.required')
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantMovimientosActions.aspx/LookUpForRecordStatus", false,
                JSON.stringify({ id: $('#H5MantMovimientosFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantMovimientosActions.aspx/LookUpForLanguageIdTranslator", false,
                JSON.stringify({ id: $('#H5MantMovimientosFormId').val() }),
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

    this.TabEntryType_GridTblSetup = function (table) {
        H5MantMovimientosSupport.LookUpForRecordStatus('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'EntryType',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabEntryType_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'EntryType',
                title: 'Movimiento',
                events: 'TabEntryType_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'RecordStatus',
                title: 'Estado del registro',
                formatter: 'H5MantMovimientosSupport.LookUpForRecordStatusFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5MantMovimientosSupport.CreatorUserCode_FormatterMaskData',
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
                formatter: 'H5MantMovimientosSupport.UpdateUserCode_FormatterMaskData',
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


        $('#TabEntryType_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabEntryType_GridTbl');
            $('#TabEntryType_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabEntryType_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabEntryType_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantMovimientosSupport.TabEntryType_GridRowToInput(row);
                H5MantMovimientosSupport.TabEntryType_Grid_delete(row, null);
                
                return row.EntryType;
            });

            $('#TabEntryType_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabEntryType_GridCreateBtn').click(function () {
            var formInstance = $("#TabEntryType_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantMovimientosSupport.TabEntryType_GridShowModal($('#TabEntryType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabEntryType_GridPopup').find('#TabEntryType_GridSaveBtn').click(function () {
            var formInstance = $("#TabEntryType_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabEntryType_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabEntryType_GridSaveBtn').html();
                $('#TabEntryType_GridSaveBtn').html('Procesando...');
                $('#TabEntryType_GridSaveBtn').prop('disabled', true);

                H5MantMovimientosSupport.currentRow.EntryType = generalSupport.NumericValue('#EntryType', -99999, 99999);
                H5MantMovimientosSupport.currentRow.RecordStatus = $('#RecordStatus').val();
                H5MantMovimientosSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                H5MantMovimientosSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                H5MantMovimientosSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);
                H5MantMovimientosSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';
                H5MantMovimientosSupport.currentRow.Description = $('#Description').val();
                H5MantMovimientosSupport.currentRow.ShortDescription = $('#ShortDescription').val();

                $('#TabEntryType_GridSaveBtn').prop('disabled', false);
                $('#TabEntryType_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantMovimientosSupport.TabEntryType_Grid_update(H5MantMovimientosSupport.currentRow, $modal);
                }
                else {                    
                    H5MantMovimientosSupport.TabEntryType_Grid_insert(H5MantMovimientosSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabEntryType_GridShowModal = function (md, title, row) {
        row = row || { EntryType: 0, RecordStatus: 0, CreatorUserCode: 0, CreationDate: null, UpdateUserCode: 0, UpdateDate: null, Description: null, ShortDescription: null };

        md.data('id', row.EntryType);
        md.find('.modal-title').text(title);

        H5MantMovimientosSupport.TabEntryType_GridRowToInput(row);
        $('#EntryType').prop('disabled', (row.EntryType !== 0));
        $('#CreatorUserCode').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        H5MantMovimientosSupport.TabEntryType_Grid_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.TabEntryType_GridRowToInput = function (row) {
        H5MantMovimientosSupport.currentRow = row;
        AutoNumeric.set('#EntryType', row.EntryType);
        H5MantMovimientosSupport.LookUpForRecordStatus(row.RecordStatus, '');
        $('#RecordStatus').trigger('change');
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#Description').val(row.Description);
        $('#ShortDescription').val(row.ShortDescription);

    };
    this.TabEntryType_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantMovimientosActions.aspx/TabEntryType_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                TransEntryTypeLanguageId1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#TabEntryType_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.TabEntryTypeTranslator_GridTblSetup = function (table) {
        H5MantMovimientosSupport.LookUpForLanguageIdTranslator('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'EntryType',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabEntryTypeTranslator_Gridtoolbar',
            columns: [{
                field: 'EntryType',
                title: 'Movimiento',
                formatter: 'H5MantMovimientosSupport.EntryTypeTranslator_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LanguageId',
                title: 'Idioma',
                formatter: 'H5MantMovimientosSupport.LookUpForLanguageIdTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Description',
                title: 'Descripción',
                events: 'TabEntryTypeTranslator_GridActionEvents',
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


        $('#TabEntryTypeTranslator_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabEntryTypeTranslator_GridTbl');
            $('#TabEntryTypeTranslator_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabEntryTypeTranslator_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabEntryTypeTranslator_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantMovimientosSupport.TabEntryTypeTranslator_GridRowToInput(row);
                
                
                return row.EntryType;
            });
            
          $('#TabEntryTypeTranslator_GridTbl').bootstrapTable('remove', {
                field: 'EntryType',
                values: ids
           });

            $('#TabEntryTypeTranslator_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabEntryTypeTranslator_GridCreateBtn').click(function () {
            var formInstance = $("#TabEntryTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantMovimientosSupport.TabEntryTypeTranslator_GridShowModal($('#TabEntryTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabEntryTypeTranslator_GridPopup').find('#TabEntryTypeTranslator_GridSaveBtn').click(function () {
            var formInstance = $("#TabEntryTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabEntryTypeTranslator_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabEntryTypeTranslator_GridSaveBtn').html();
                $('#TabEntryTypeTranslator_GridSaveBtn').html('Procesando...');
                $('#TabEntryTypeTranslator_GridSaveBtn').prop('disabled', true);

                H5MantMovimientosSupport.currentRow.EntryType = generalSupport.NumericValue('#EntryTypeTranslator', -99999, 99999);
                H5MantMovimientosSupport.currentRow.LanguageId = parseInt(0 + $('#LanguageIdTranslator').val(), 10);
                H5MantMovimientosSupport.currentRow.Description = $('#DescriptionTranslator').val();
                H5MantMovimientosSupport.currentRow.ShortDescription = $('#ShortDescriptionTranslator').val();

                $('#TabEntryTypeTranslator_GridSaveBtn').prop('disabled', false);
                $('#TabEntryTypeTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantMovimientosSupport.TabEntryTypeTranslator_Grid_update(H5MantMovimientosSupport.currentRow, $modal);
                }
                else {                    
                    $('#TabEntryTypeTranslator_GridTbl').bootstrapTable('append', H5MantMovimientosSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabEntryTypeTranslator_GridShowModal = function (md, title, row) {
        row = row || { EntryType: 0, LanguageId: 0, Description: null, ShortDescription: null };

        md.data('id', row.EntryType);
        md.find('.modal-title').text(title);

        H5MantMovimientosSupport.TabEntryTypeTranslator_GridRowToInput(row);
        $('#EntryTypeTranslator').prop('disabled', true);
        $('#LanguageIdTranslator').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabEntryTypeTranslator_GridRowToInput = function (row) {
        H5MantMovimientosSupport.currentRow = row;
        AutoNumeric.set('#EntryTypeTranslator', row.EntryType);
        H5MantMovimientosSupport.LookUpForLanguageIdTranslator(row.LanguageId, '');
        $('#LanguageIdTranslator').trigger('change');
        $('#DescriptionTranslator').val(row.Description);
        $('#ShortDescriptionTranslator').val(row.ShortDescription);

    };
    this.TabEntryTypeTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantMovimientosActions.aspx/TabEntryTypeTranslator_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#TabEntryTypeTranslator_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.EntryType_FormatterMaskData = function (value, row, index) {          
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
    this.EntryTypeTranslator_FormatterMaskData = function (value, row, index) {          
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
        
        H5MantMovimientosSupport.ValidateSetup();
        tableHelperSupport.Translate('#TabEntryType_GridTbl', '#TabEntryType_GridTbl');
tableHelperSupport.Translate('#TabEntryTypeTranslator_GridTbl', '#TabEntryTypeTranslator_GridTbl');

    });
        

    H5MantMovimientosSupport.ControlBehaviour();
    H5MantMovimientosSupport.ControlActions();
    

    $("#TabEntryType_GridTblPlaceHolder").replaceWith('<table id="TabEntryType_GridTbl"></table>');
    H5MantMovimientosSupport.TabEntryType_GridTblSetup($('#TabEntryType_GridTbl'));
    $("#TabEntryTypeTranslator_GridTblPlaceHolder").replaceWith('<table id="TabEntryTypeTranslator_GridTbl"></table>');
    H5MantMovimientosSupport.TabEntryTypeTranslator_GridTblSetup($('#TabEntryTypeTranslator_GridTbl'));

        H5MantMovimientosSupport.TabEntryType_GridTblRequest();
        H5MantMovimientosSupport.TabEntryTypeTranslator_GridTblRequest();




  };
};

$(document).ready(function () {
   H5MantMovimientosSupport.Init();
});

window.TabEntryType_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantMovimientosSupport.TabEntryType_GridShowModal($('#TabEntryType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.TabEntryTypeTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantMovimientosSupport.TabEntryTypeTranslator_GridShowModal($('#TabEntryTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
