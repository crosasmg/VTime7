var H5MantTiposDePuntosSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5MantTiposDePuntosFormId').val(),
            TabDebitOrCreditType_Grid_TabDebitOrCreditType_Item: generalSupport.NormalizeProperties($('#TabDebitOrCreditType_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate'),
            TabDebitOrCreditTypeTranslator_Grid_TabDebitOrCreditType_Item: generalSupport.NormalizeProperties($('#TabDebitOrCreditTypeTranslator_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#H5MantTiposDePuntosFormId').val(data.InstanceFormId);

        H5MantTiposDePuntosSupport.LookUpForRecordStatus(source);
        H5MantTiposDePuntosSupport.LookUpForLanguageIdTranslator(source);

        H5MantTiposDePuntosSupport.TabDebitOrCreditType_GridTblRequest();
        if (data.TabDebitOrCreditType_Grid_TabDebitOrCreditType_Item !== null)
            $('#TabDebitOrCreditType_GridTbl').bootstrapTable('load', data.TabDebitOrCreditType_Grid_TabDebitOrCreditType_Item);
        H5MantTiposDePuntosSupport.TabDebitOrCreditTypeTranslator_GridTblRequest();
        if (data.TabDebitOrCreditTypeTranslator_Grid_TabDebitOrCreditType_Item !== null)
            $('#TabDebitOrCreditTypeTranslator_GridTbl').bootstrapTable('load', data.TabDebitOrCreditTypeTranslator_Grid_TabDebitOrCreditType_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#DebitOrCredit', {
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
      new AutoNumeric('#DebitOrCreditTranslator', {
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
					         H5MantTiposDePuntosSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5MantTiposDePuntosSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.TabDebitOrCreditType_Grid_insert = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDePuntosActions.aspx/TabDebitOrCreditType_Grid1InsertCommandActionTabDebitOrCreditType", false,
               JSON.stringify({ DEBITORCREDIT1: row.DebitOrCredit, RECORDSTATUS2: row.RecordStatus, CREATORUSERCODE2: generalSupport.UserContext().userId, UPDATEUSERCODE4: generalSupport.UserContext().userId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDePuntosActions.aspx/TabDebitOrCreditType_Grid3InsertCommandActionTransDebitOrCreditType", false,
               JSON.stringify({ DEBITORCREDIT1: row.DebitOrCredit, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabDebitOrCreditType_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message7 = 'Se agregó correctamente el registro';
            notification.toastr.success('Agregar registro', message7);
                    }                    
                    else {
            var message8 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message8);

                        }

    };
    this.TabDebitOrCreditType_Grid_update = function (row, $modal) {
          var data;
            var recordCount;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDePuntosActions.aspx/TabDebitOrCreditType_Grid1UpdateCommandActionTabDebitOrCreditType", false,
               JSON.stringify({ RECORDSTATUS1: row.RecordStatus, UPDATEUSERCODE1: generalSupport.UserContext().userId, TabDebitOrCreditTypeDebitOrCredit3: row.DebitOrCredit }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDePuntosActions.aspx/TabDebitOrCreditType_Grid3SelectCommandActionTransDebitOrCreditType", false,
               JSON.stringify({                 TransDebitOrCreditTypeDebitOrCredit1: row.DebitOrCredit,
                TransDebitOrCreditTypeLanguageId2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDePuntosActions.aspx/TabDebitOrCreditType_Grid5InsertCommandActionTransDebitOrCreditType", false,
               JSON.stringify({ DEBITORCREDIT1: row.DebitOrCredit, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDePuntosActions.aspx/TabDebitOrCreditType_Grid6UpdateCommandActionTransDebitOrCreditType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransDebitOrCreditTypeDebitOrCredit4: row.DebitOrCredit, TransDebitOrCreditTypeLanguageId5: generalSupport.SessionContext().languageId }));
               


                        }

                    }
        if (data.d.Success === true){
            $('#TabDebitOrCreditType_GridTbl').bootstrapTable('updateByUniqueId', { id: row.DebitOrCredit, row: row });
            $modal.modal('hide');
            var message9 = 'Se actualizó correctamente el registro';
            notification.toastr.success('Actualizar registro', message9);
                        }                        
                        else {
            var message10 = 'No se pudo actualizar el registro';
            notification.swal.error('Actualizar registro', message10);

                            }

    };
    this.TabDebitOrCreditType_Grid_delete = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDePuntosActions.aspx/TabDebitOrCreditType_Grid1DeleteCommandActionTransDebitOrCreditType", false,
               JSON.stringify({ TransDebitOrCreditTypeDebitOrCredit1: row.DebitOrCredit }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDePuntosActions.aspx/TabDebitOrCreditType_Grid3DeleteCommandActionTabDebitOrCreditType", false,
               JSON.stringify({ TabDebitOrCreditTypeDebitOrCredit1: row.DebitOrCredit }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabDebitOrCreditType_GridTbl').bootstrapTable('remove', {field: 'DebitOrCredit', values: [generalSupport.NumericValue('#DebitOrCredit', -99999, 99999)]});
            var message7 = 'Se eliminó correctamente el registro';
            notification.toastr.success('Eliminar registro', message7);
                    }                    
                    else {
            var message8 = 'No se puede eliminar el registro';
            notification.toastr.error('Eliminar registro', message8);

                        }

    };
    this.TabDebitOrCreditType_Grid_BeforeShowPopup = function (row, $modal) {
          var data;
            var nextId;
        if (row.DebitOrCredit === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDePuntosActions.aspx/TabDebitOrCreditType_Grid2SelectCommandActionTabDebitOrCreditType", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#DebitOrCredit', nextId);

            }

    };
    this.TabDebitOrCreditTypeTranslator_Grid_update = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDePuntosActions.aspx/TabDebitOrCreditTypeTranslator_Grid1UpdateCommandActionTransDebitOrCreditType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransDebitOrCreditTypeDebitOrCredit4: row.DebitOrCredit, TransDebitOrCreditTypeLanguageId5: row.LanguageId }));
               

        if (data.d.Success === true){
            $('#TabDebitOrCreditTypeTranslator_GridTbl').bootstrapTable('updateByUniqueId', { id: row.DebitOrCredit, row: row });
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
            var formInstance = $("#H5MantTiposDePuntosMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                H5MantTiposDePuntosSupport.TabDebitOrCreditType_GridTblRequest();
                $('#TabDebitOrCreditType_GridContainer').toggleClass('hidden', false);
                $('#TabDebitOrCreditTypeTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#H5MantTiposDePuntosMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                H5MantTiposDePuntosSupport.TabDebitOrCreditTypeTranslator_GridTblRequest();
                $('#TabDebitOrCreditTypeTranslator_GridContainer').toggleClass('hidden', false);
                $('#TabDebitOrCreditType_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5MantTiposDePuntosMainForm").validate({
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
        $("#TabDebitOrCreditType_GridEditForm").validate({
            rules: {
                DebitOrCredit: {
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
                DebitOrCredit: {
                    AutoNumericMinValue: $.i18n.t('app.validation.DebitOrCredit.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.DebitOrCredit.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.DebitOrCredit.required')
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
        $("#TabDebitOrCreditTypeTranslator_GridEditForm").validate({
            rules: {
                DebitOrCreditTranslator: {
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
                DebitOrCreditTranslator: {
                    AutoNumericMinValue: $.i18n.t('app.validation.DebitOrCreditTranslator.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.DebitOrCreditTranslator.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.DebitOrCreditTranslator.required')
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTiposDePuntosActions.aspx/LookUpForRecordStatus", false,
                JSON.stringify({ id: $('#H5MantTiposDePuntosFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTiposDePuntosActions.aspx/LookUpForLanguageIdTranslator", false,
                JSON.stringify({ id: $('#H5MantTiposDePuntosFormId').val() }),
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

    this.TabDebitOrCreditType_GridTblSetup = function (table) {
        H5MantTiposDePuntosSupport.LookUpForRecordStatus('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'DebitOrCredit',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabDebitOrCreditType_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'DebitOrCredit',
                title: 'Tipo de Puntos',
                events: 'TabDebitOrCreditType_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'RecordStatus',
                title: 'Estado del registro',
                formatter: 'H5MantTiposDePuntosSupport.LookUpForRecordStatusFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5MantTiposDePuntosSupport.CreatorUserCode_FormatterMaskData',
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
                formatter: 'H5MantTiposDePuntosSupport.UpdateUserCode_FormatterMaskData',
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


        $('#TabDebitOrCreditType_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabDebitOrCreditType_GridTbl');
            $('#TabDebitOrCreditType_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabDebitOrCreditType_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabDebitOrCreditType_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantTiposDePuntosSupport.TabDebitOrCreditType_GridRowToInput(row);
                H5MantTiposDePuntosSupport.TabDebitOrCreditType_Grid_delete(row, null);
                
                return row.DebitOrCredit;
            });

            $('#TabDebitOrCreditType_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabDebitOrCreditType_GridCreateBtn').click(function () {
            var formInstance = $("#TabDebitOrCreditType_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantTiposDePuntosSupport.TabDebitOrCreditType_GridShowModal($('#TabDebitOrCreditType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabDebitOrCreditType_GridPopup').find('#TabDebitOrCreditType_GridSaveBtn').click(function () {
            var formInstance = $("#TabDebitOrCreditType_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabDebitOrCreditType_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabDebitOrCreditType_GridSaveBtn').html();
                $('#TabDebitOrCreditType_GridSaveBtn').html('Procesando...');
                $('#TabDebitOrCreditType_GridSaveBtn').prop('disabled', true);

                H5MantTiposDePuntosSupport.currentRow.DebitOrCredit = generalSupport.NumericValue('#DebitOrCredit', -99999, 99999);
                H5MantTiposDePuntosSupport.currentRow.RecordStatus = $('#RecordStatus').val();
                H5MantTiposDePuntosSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                H5MantTiposDePuntosSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                H5MantTiposDePuntosSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);
                H5MantTiposDePuntosSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';
                H5MantTiposDePuntosSupport.currentRow.Description = $('#Description').val();
                H5MantTiposDePuntosSupport.currentRow.ShortDescription = $('#ShortDescription').val();

                $('#TabDebitOrCreditType_GridSaveBtn').prop('disabled', false);
                $('#TabDebitOrCreditType_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantTiposDePuntosSupport.TabDebitOrCreditType_Grid_update(H5MantTiposDePuntosSupport.currentRow, $modal);
                }
                else {                    
                    H5MantTiposDePuntosSupport.TabDebitOrCreditType_Grid_insert(H5MantTiposDePuntosSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabDebitOrCreditType_GridShowModal = function (md, title, row) {
        row = row || { DebitOrCredit: 0, RecordStatus: 0, CreatorUserCode: 0, CreationDate: null, UpdateUserCode: 0, UpdateDate: null, Description: null, ShortDescription: null };

        md.data('id', row.DebitOrCredit);
        md.find('.modal-title').text(title);

        H5MantTiposDePuntosSupport.TabDebitOrCreditType_GridRowToInput(row);
        $('#DebitOrCredit').prop('disabled', (row.DebitOrCredit !== 0));
        $('#CreatorUserCode').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        H5MantTiposDePuntosSupport.TabDebitOrCreditType_Grid_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.TabDebitOrCreditType_GridRowToInput = function (row) {
        H5MantTiposDePuntosSupport.currentRow = row;
        AutoNumeric.set('#DebitOrCredit', row.DebitOrCredit);
        H5MantTiposDePuntosSupport.LookUpForRecordStatus(row.RecordStatus, '');
        $('#RecordStatus').trigger('change');
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#Description').val(row.Description);
        $('#ShortDescription').val(row.ShortDescription);

    };
    this.TabDebitOrCreditType_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTiposDePuntosActions.aspx/TabDebitOrCreditType_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                TransDebitOrCreditTypeLanguageId1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#TabDebitOrCreditType_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.TabDebitOrCreditTypeTranslator_GridTblSetup = function (table) {
        H5MantTiposDePuntosSupport.LookUpForLanguageIdTranslator('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'DebitOrCredit',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabDebitOrCreditTypeTranslator_Gridtoolbar',
            columns: [{
                field: 'DebitOrCredit',
                title: 'Tipo de Puntos',
                formatter: 'H5MantTiposDePuntosSupport.DebitOrCreditTranslator_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LanguageId',
                title: 'Idioma',
                formatter: 'H5MantTiposDePuntosSupport.LookUpForLanguageIdTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Description',
                title: 'Descripción',
                events: 'TabDebitOrCreditTypeTranslator_GridActionEvents',
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


        $('#TabDebitOrCreditTypeTranslator_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabDebitOrCreditTypeTranslator_GridTbl');
            $('#TabDebitOrCreditTypeTranslator_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabDebitOrCreditTypeTranslator_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabDebitOrCreditTypeTranslator_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantTiposDePuntosSupport.TabDebitOrCreditTypeTranslator_GridRowToInput(row);
                
                
                return row.DebitOrCredit;
            });
            
          $('#TabDebitOrCreditTypeTranslator_GridTbl').bootstrapTable('remove', {
                field: 'DebitOrCredit',
                values: ids
           });

            $('#TabDebitOrCreditTypeTranslator_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabDebitOrCreditTypeTranslator_GridCreateBtn').click(function () {
            var formInstance = $("#TabDebitOrCreditTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantTiposDePuntosSupport.TabDebitOrCreditTypeTranslator_GridShowModal($('#TabDebitOrCreditTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabDebitOrCreditTypeTranslator_GridPopup').find('#TabDebitOrCreditTypeTranslator_GridSaveBtn').click(function () {
            var formInstance = $("#TabDebitOrCreditTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabDebitOrCreditTypeTranslator_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabDebitOrCreditTypeTranslator_GridSaveBtn').html();
                $('#TabDebitOrCreditTypeTranslator_GridSaveBtn').html('Procesando...');
                $('#TabDebitOrCreditTypeTranslator_GridSaveBtn').prop('disabled', true);

                H5MantTiposDePuntosSupport.currentRow.DebitOrCredit = generalSupport.NumericValue('#DebitOrCreditTranslator', -99999, 99999);
                H5MantTiposDePuntosSupport.currentRow.LanguageId = parseInt(0 + $('#LanguageIdTranslator').val(), 10);
                H5MantTiposDePuntosSupport.currentRow.Description = $('#DescriptionTranslator').val();
                H5MantTiposDePuntosSupport.currentRow.ShortDescription = $('#ShortDescriptionTranslator').val();

                $('#TabDebitOrCreditTypeTranslator_GridSaveBtn').prop('disabled', false);
                $('#TabDebitOrCreditTypeTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantTiposDePuntosSupport.TabDebitOrCreditTypeTranslator_Grid_update(H5MantTiposDePuntosSupport.currentRow, $modal);
                }
                else {                    
                    $('#TabDebitOrCreditTypeTranslator_GridTbl').bootstrapTable('append', H5MantTiposDePuntosSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabDebitOrCreditTypeTranslator_GridShowModal = function (md, title, row) {
        row = row || { DebitOrCredit: 0, LanguageId: 0, Description: null, ShortDescription: null };

        md.data('id', row.DebitOrCredit);
        md.find('.modal-title').text(title);

        H5MantTiposDePuntosSupport.TabDebitOrCreditTypeTranslator_GridRowToInput(row);
        $('#DebitOrCreditTranslator').prop('disabled', true);
        $('#LanguageIdTranslator').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabDebitOrCreditTypeTranslator_GridRowToInput = function (row) {
        H5MantTiposDePuntosSupport.currentRow = row;
        AutoNumeric.set('#DebitOrCreditTranslator', row.DebitOrCredit);
        H5MantTiposDePuntosSupport.LookUpForLanguageIdTranslator(row.LanguageId, '');
        $('#LanguageIdTranslator').trigger('change');
        $('#DescriptionTranslator').val(row.Description);
        $('#ShortDescriptionTranslator').val(row.ShortDescription);

    };
    this.TabDebitOrCreditTypeTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTiposDePuntosActions.aspx/TabDebitOrCreditTypeTranslator_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#TabDebitOrCreditTypeTranslator_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.DebitOrCredit_FormatterMaskData = function (value, row, index) {          
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
    this.DebitOrCreditTranslator_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };





  this.Init = function(){
    securitySupport.ValidateAccessRoles(['EASE1', 'Empleado', 'Suscriptor']);
    moment.locale(generalSupport.UserContext().languageName);
    
   generalSupport.TranslateInit(generalSupport.GetCurrentName(), function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        H5MantTiposDePuntosSupport.ValidateSetup();
        tableHelperSupport.Translate('#TabDebitOrCreditType_GridTbl', '#TabDebitOrCreditType_GridTbl');
tableHelperSupport.Translate('#TabDebitOrCreditTypeTranslator_GridTbl', '#TabDebitOrCreditTypeTranslator_GridTbl');

    });
        

    H5MantTiposDePuntosSupport.ControlBehaviour();
    H5MantTiposDePuntosSupport.ControlActions();
    

    $("#TabDebitOrCreditType_GridTblPlaceHolder").replaceWith('<table id="TabDebitOrCreditType_GridTbl"></table>');
    H5MantTiposDePuntosSupport.TabDebitOrCreditType_GridTblSetup($('#TabDebitOrCreditType_GridTbl'));
    $("#TabDebitOrCreditTypeTranslator_GridTblPlaceHolder").replaceWith('<table id="TabDebitOrCreditTypeTranslator_GridTbl"></table>');
    H5MantTiposDePuntosSupport.TabDebitOrCreditTypeTranslator_GridTblSetup($('#TabDebitOrCreditTypeTranslator_GridTbl'));

        H5MantTiposDePuntosSupport.TabDebitOrCreditType_GridTblRequest();
        H5MantTiposDePuntosSupport.TabDebitOrCreditTypeTranslator_GridTblRequest();




  };
};

$(document).ready(function () {
   H5MantTiposDePuntosSupport.Init();
});

window.TabDebitOrCreditType_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantTiposDePuntosSupport.TabDebitOrCreditType_GridShowModal($('#TabDebitOrCreditType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.TabDebitOrCreditTypeTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantTiposDePuntosSupport.TabDebitOrCreditTypeTranslator_GridShowModal($('#TabDebitOrCreditTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
