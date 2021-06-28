var H5MAntTipoEnfermedadSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5MAntTipoEnfermedadFormId').val(),
            TabIllnessType_Grid_TabIllnessType_Item: generalSupport.NormalizeProperties($('#TabIllnessType_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate'),
            TabIllnessTypeTranslator_Grid_TabIllnessType_Item: generalSupport.NormalizeProperties($('#TabIllnessTypeTranslator_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#H5MAntTipoEnfermedadFormId').val(data.InstanceFormId);

        H5MAntTipoEnfermedadSupport.LookUpForRecordStatus(source);
        H5MAntTipoEnfermedadSupport.LookUpForLanguageIdTranslator(source);

        H5MAntTipoEnfermedadSupport.TabIllnessType_GridTblRequest();
        if (data.TabIllnessType_Grid_TabIllnessType_Item !== null)
            $('#TabIllnessType_GridTbl').bootstrapTable('load', data.TabIllnessType_Grid_TabIllnessType_Item);
        H5MAntTipoEnfermedadSupport.TabIllnessTypeTranslator_GridTblRequest();
        if (data.TabIllnessTypeTranslator_Grid_TabIllnessType_Item !== null)
            $('#TabIllnessTypeTranslator_GridTbl').bootstrapTable('load', data.TabIllnessTypeTranslator_Grid_TabIllnessType_Item);

    };

    this.ControlBehaviour = function () {



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
					         H5MAntTipoEnfermedadSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5MAntTipoEnfermedadSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.TabIllnessType_Grid_insert = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MAntTipoEnfermedadActions.aspx/TabIllnessType_Grid1InsertCommandActionTabIllnessType", false,
               JSON.stringify({ IMPAIRMENTCODE1: row.ImpairmentCode, RECORDSTATUS2: row.RecordStatus, CREATORUSERCODE2: generalSupport.UserContext().userId, UPDATEUSERCODE4: generalSupport.UserContext().userId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MAntTipoEnfermedadActions.aspx/TabIllnessType_Grid3InsertCommandActionTransIllnessType", false,
               JSON.stringify({ IMPAIRMENTCODE1: row.ImpairmentCode, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabIllnessType_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message7 = 'Se agregó correctamente el registro';
            notification.toastr.success('Agregar registro', message7);
                    }                    
                    else {
            var message8 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message8);

                        }

    };
    this.TabIllnessType_Grid_update = function (row, $modal) {
          var data;
            var recordCount;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MAntTipoEnfermedadActions.aspx/TabIllnessType_Grid1UpdateCommandActionTabIllnessType", false,
               JSON.stringify({ RECORDSTATUS1: row.RecordStatus, UPDATEUSERCODE1: generalSupport.UserContext().userId, TabIllnessTypeImpairmentCode3: row.ImpairmentCode }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MAntTipoEnfermedadActions.aspx/TabIllnessType_Grid3SelectCommandActionTransIllnessType", false,
               JSON.stringify({                 TransIllnessTypeImpairmentCode1: row.ImpairmentCode,
                TransIllnessTypeLanguageId2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MAntTipoEnfermedadActions.aspx/TabIllnessType_Grid5InsertCommandActionTransIllnessType", false,
               JSON.stringify({ IMPAIRMENTCODE1: row.ImpairmentCode, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MAntTipoEnfermedadActions.aspx/TabIllnessType_Grid6UpdateCommandActionTransIllnessType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransIllnessTypeImpairmentCode4: row.ImpairmentCode, TransIllnessTypeLanguageId5: generalSupport.SessionContext().languageId }));
               


                        }

                    }
        if (data.d.Success === true){
            $('#TabIllnessType_GridTbl').bootstrapTable('updateByUniqueId', { id: row.ImpairmentCode, row: row });
            $modal.modal('hide');
            var message9 = 'Se actualizó correctamente el registro';
            notification.toastr.success('Actualizar registro', message9);
                        }                        
                        else {
            var message10 = 'No se pudo actualizar el registro';
            notification.swal.error('Actualizar registro', message10);

                            }

    };
    this.TabIllnessType_Grid_delete = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MAntTipoEnfermedadActions.aspx/TabIllnessType_Grid1DeleteCommandActionTransIllnessType", false,
               JSON.stringify({ TransIllnessTypeImpairmentCode1: row.ImpairmentCode }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MAntTipoEnfermedadActions.aspx/TabIllnessType_Grid3DeleteCommandActionTabIllnessType", false,
               JSON.stringify({ TabIllnessTypeImpairmentCode1: row.ImpairmentCode }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabIllnessType_GridTbl').bootstrapTable('remove', {field: 'ImpairmentCode', values: [$('#ImpairmentCode').val()]});
            var message7 = 'Se eliminó correctamente el registro';
            notification.toastr.success('Eliminar registro', message7);
                    }                    
                    else {
            var message8 = 'No se puede eliminar el registro';
            notification.toastr.error('Eliminar registro', message8);

                        }

    };
    this.TabIllnessTypeTranslator_Grid_update = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MAntTipoEnfermedadActions.aspx/TabIllnessTypeTranslator_Grid1UpdateCommandActionTransIllnessType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransIllnessTypeImpairmentCode4: row.ImpairmentCode, TransIllnessTypeLanguageId5: row.LanguageId }));
               

        if (data.d.Success === true){
            $('#TabIllnessTypeTranslator_GridTbl').bootstrapTable('updateByUniqueId', { id: row.ImpairmentCode, row: row });
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
            var formInstance = $("#H5MAntTipoEnfermedadMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                H5MAntTipoEnfermedadSupport.TabIllnessType_GridTblRequest();
                $('#TabIllnessType_GridContainer').toggleClass('hidden', false);
                $('#TabIllnessTypeTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#H5MAntTipoEnfermedadMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                H5MAntTipoEnfermedadSupport.TabIllnessTypeTranslator_GridTblRequest();
                $('#TabIllnessTypeTranslator_GridContainer').toggleClass('hidden', false);
                $('#TabIllnessType_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5MAntTipoEnfermedadMainForm").validate({
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
        $("#TabIllnessType_GridEditForm").validate({
            rules: {
                ImpairmentCode: {
                    required: true,
                    maxlength: 5
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
                    maxlength: 256
                },
                ShortDescription: {
                    required: true,
                    maxlength: 20
                }

            },
            messages: {
                ImpairmentCode: {
                    required: $.i18n.t('app.validation.ImpairmentCode.required'),
                    maxlength: $.i18n.t('app.validation.ImpairmentCode.maxlength')
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
        $("#TabIllnessTypeTranslator_GridEditForm").validate({
            rules: {
                ImpairmentCodeTranslator: {
                    required: true,
                    maxlength: 5
                },
                LanguageIdTranslator: {
                    required: true,
                },
                DescriptionTranslator: {
                    required: true,
                    maxlength: 256
                },
                ShortDescriptionTranslator: {
                    required: true,
                    maxlength: 20
                }

            },
            messages: {
                ImpairmentCodeTranslator: {
                    required: $.i18n.t('app.validation.ImpairmentCodeTranslator.required'),
                    maxlength: $.i18n.t('app.validation.ImpairmentCodeTranslator.maxlength')
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MAntTipoEnfermedadActions.aspx/LookUpForRecordStatus", false,
                JSON.stringify({ id: $('#H5MAntTipoEnfermedadFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MAntTipoEnfermedadActions.aspx/LookUpForLanguageIdTranslator", false,
                JSON.stringify({ id: $('#H5MAntTipoEnfermedadFormId').val() }),
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

    this.TabIllnessType_GridTblSetup = function (table) {
        H5MAntTipoEnfermedadSupport.LookUpForRecordStatus('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'ImpairmentCode',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabIllnessType_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'ImpairmentCode',
                title: 'Enfermedad',
                events: 'TabIllnessType_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'RecordStatus',
                title: 'Estado del Registro',
                formatter: 'H5MAntTipoEnfermedadSupport.LookUpForRecordStatusFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5MAntTipoEnfermedadSupport.CreatorUserCode_FormatterMaskData',
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
                formatter: 'H5MAntTipoEnfermedadSupport.UpdateUserCode_FormatterMaskData',
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


        $('#TabIllnessType_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabIllnessType_GridTbl');
            $('#TabIllnessType_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabIllnessType_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabIllnessType_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MAntTipoEnfermedadSupport.TabIllnessType_GridRowToInput(row);
                H5MAntTipoEnfermedadSupport.TabIllnessType_Grid_delete(row, null);
                
                return row.ImpairmentCode;
            });

            $('#TabIllnessType_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabIllnessType_GridCreateBtn').click(function () {
            var formInstance = $("#TabIllnessType_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MAntTipoEnfermedadSupport.TabIllnessType_GridShowModal($('#TabIllnessType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabIllnessType_GridPopup').find('#TabIllnessType_GridSaveBtn').click(function () {
            var formInstance = $("#TabIllnessType_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabIllnessType_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabIllnessType_GridSaveBtn').html();
                $('#TabIllnessType_GridSaveBtn').html('Procesando...');
                $('#TabIllnessType_GridSaveBtn').prop('disabled', true);

                H5MAntTipoEnfermedadSupport.currentRow.ImpairmentCode = $('#ImpairmentCode').val();
                H5MAntTipoEnfermedadSupport.currentRow.RecordStatus = $('#RecordStatus').val();
                H5MAntTipoEnfermedadSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                H5MAntTipoEnfermedadSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                H5MAntTipoEnfermedadSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);
                H5MAntTipoEnfermedadSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';
                H5MAntTipoEnfermedadSupport.currentRow.Description = $('#Description').val();
                H5MAntTipoEnfermedadSupport.currentRow.ShortDescription = $('#ShortDescription').val();

                $('#TabIllnessType_GridSaveBtn').prop('disabled', false);
                $('#TabIllnessType_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MAntTipoEnfermedadSupport.TabIllnessType_Grid_update(H5MAntTipoEnfermedadSupport.currentRow, $modal);
                }
                else {                    
                    H5MAntTipoEnfermedadSupport.TabIllnessType_Grid_insert(H5MAntTipoEnfermedadSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabIllnessType_GridShowModal = function (md, title, row) {
        row = row || { ImpairmentCode: null, RecordStatus: 0, CreatorUserCode: 0, CreationDate: null, UpdateUserCode: 0, UpdateDate: null, Description: null, ShortDescription: null };

        md.data('id', row.ImpairmentCode);
        md.find('.modal-title').text(title);

        H5MAntTipoEnfermedadSupport.TabIllnessType_GridRowToInput(row);
        $('#ImpairmentCode').prop('disabled', (row.ImpairmentCode !== ''));
        $('#CreatorUserCode').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabIllnessType_GridRowToInput = function (row) {
        H5MAntTipoEnfermedadSupport.currentRow = row;
        $('#ImpairmentCode').val(row.ImpairmentCode);
        H5MAntTipoEnfermedadSupport.LookUpForRecordStatus(row.RecordStatus, '');
        $('#RecordStatus').trigger('change');
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#Description').val(row.Description);
        $('#ShortDescription').val(row.ShortDescription);

    };
    this.TabIllnessType_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MAntTipoEnfermedadActions.aspx/TabIllnessType_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                TransIllnessTypeLanguageId1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#TabIllnessType_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.TabIllnessTypeTranslator_GridTblSetup = function (table) {
        H5MAntTipoEnfermedadSupport.LookUpForLanguageIdTranslator('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'ImpairmentCode',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabIllnessTypeTranslator_Gridtoolbar',
            columns: [{
                field: 'ImpairmentCode',
                title: 'Enfermedad',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LanguageId',
                title: 'Idioma',
                formatter: 'H5MAntTipoEnfermedadSupport.LookUpForLanguageIdTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Description',
                title: 'Descripción',
                events: 'TabIllnessTypeTranslator_GridActionEvents',
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


        $('#TabIllnessTypeTranslator_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabIllnessTypeTranslator_GridTbl');
            $('#TabIllnessTypeTranslator_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabIllnessTypeTranslator_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabIllnessTypeTranslator_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MAntTipoEnfermedadSupport.TabIllnessTypeTranslator_GridRowToInput(row);
                
                
                return row.ImpairmentCode;
            });
            
          $('#TabIllnessTypeTranslator_GridTbl').bootstrapTable('remove', {
                field: 'ImpairmentCode',
                values: ids
           });

            $('#TabIllnessTypeTranslator_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabIllnessTypeTranslator_GridCreateBtn').click(function () {
            var formInstance = $("#TabIllnessTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MAntTipoEnfermedadSupport.TabIllnessTypeTranslator_GridShowModal($('#TabIllnessTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabIllnessTypeTranslator_GridPopup').find('#TabIllnessTypeTranslator_GridSaveBtn').click(function () {
            var formInstance = $("#TabIllnessTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabIllnessTypeTranslator_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabIllnessTypeTranslator_GridSaveBtn').html();
                $('#TabIllnessTypeTranslator_GridSaveBtn').html('Procesando...');
                $('#TabIllnessTypeTranslator_GridSaveBtn').prop('disabled', true);

                H5MAntTipoEnfermedadSupport.currentRow.ImpairmentCode = $('#ImpairmentCodeTranslator').val();
                H5MAntTipoEnfermedadSupport.currentRow.LanguageId = parseInt(0 + $('#LanguageIdTranslator').val(), 10);
                H5MAntTipoEnfermedadSupport.currentRow.Description = $('#DescriptionTranslator').val();
                H5MAntTipoEnfermedadSupport.currentRow.ShortDescription = $('#ShortDescriptionTranslator').val();

                $('#TabIllnessTypeTranslator_GridSaveBtn').prop('disabled', false);
                $('#TabIllnessTypeTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MAntTipoEnfermedadSupport.TabIllnessTypeTranslator_Grid_update(H5MAntTipoEnfermedadSupport.currentRow, $modal);
                }
                else {                    
                    $('#TabIllnessTypeTranslator_GridTbl').bootstrapTable('append', H5MAntTipoEnfermedadSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabIllnessTypeTranslator_GridShowModal = function (md, title, row) {
        row = row || { ImpairmentCode: null, LanguageId: 0, Description: null, ShortDescription: null };

        md.data('id', row.ImpairmentCode);
        md.find('.modal-title').text(title);

        H5MAntTipoEnfermedadSupport.TabIllnessTypeTranslator_GridRowToInput(row);
        $('#ImpairmentCodeTranslator').prop('disabled', true);
        $('#LanguageIdTranslator').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabIllnessTypeTranslator_GridRowToInput = function (row) {
        H5MAntTipoEnfermedadSupport.currentRow = row;
        $('#ImpairmentCodeTranslator').val(row.ImpairmentCode);
        H5MAntTipoEnfermedadSupport.LookUpForLanguageIdTranslator(row.LanguageId, '');
        $('#LanguageIdTranslator').trigger('change');
        $('#DescriptionTranslator').val(row.Description);
        $('#ShortDescriptionTranslator').val(row.ShortDescription);

    };
    this.TabIllnessTypeTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MAntTipoEnfermedadActions.aspx/TabIllnessTypeTranslator_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#TabIllnessTypeTranslator_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

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





  this.Init = function(){
    
    moment.locale(generalSupport.UserContext().languageName);
    
   generalSupport.TranslateInit(generalSupport.GetCurrentName(), function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        H5MAntTipoEnfermedadSupport.ValidateSetup();
        tableHelperSupport.Translate('#TabIllnessType_GridTbl', '#TabIllnessType_GridTbl');
tableHelperSupport.Translate('#TabIllnessTypeTranslator_GridTbl', '#TabIllnessTypeTranslator_GridTbl');

    });
        

    H5MAntTipoEnfermedadSupport.ControlBehaviour();
    H5MAntTipoEnfermedadSupport.ControlActions();
    

    $("#TabIllnessType_GridTblPlaceHolder").replaceWith('<table id="TabIllnessType_GridTbl"></table>');
    H5MAntTipoEnfermedadSupport.TabIllnessType_GridTblSetup($('#TabIllnessType_GridTbl'));
    $("#TabIllnessTypeTranslator_GridTblPlaceHolder").replaceWith('<table id="TabIllnessTypeTranslator_GridTbl"></table>');
    H5MAntTipoEnfermedadSupport.TabIllnessTypeTranslator_GridTblSetup($('#TabIllnessTypeTranslator_GridTbl'));

        H5MAntTipoEnfermedadSupport.TabIllnessType_GridTblRequest();
        H5MAntTipoEnfermedadSupport.TabIllnessTypeTranslator_GridTblRequest();




  };
};

$(document).ready(function () {
   H5MAntTipoEnfermedadSupport.Init();
});

window.TabIllnessType_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MAntTipoEnfermedadSupport.TabIllnessType_GridShowModal($('#TabIllnessType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.TabIllnessTypeTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MAntTipoEnfermedadSupport.TabIllnessTypeTranslator_GridShowModal($('#TabIllnessTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
