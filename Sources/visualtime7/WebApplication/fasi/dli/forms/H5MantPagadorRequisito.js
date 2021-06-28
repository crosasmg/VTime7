var H5MantPagadorRequisitoSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5MantPagadorRequisitoFormId').val(),
            TabPayableByType_Grid_TabPayableByType_Item: generalSupport.NormalizeProperties($('#TabPayableByType_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate'),
            TabPayableByTypeTranslator_Grid_TabPayableByType_Item: generalSupport.NormalizeProperties($('#TabPayableByTypeTranslator_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#H5MantPagadorRequisitoFormId').val(data.InstanceFormId);

        H5MantPagadorRequisitoSupport.LookUpForRecordStatus(source);
        H5MantPagadorRequisitoSupport.LookUpForLanguageIdTranslator(source);

        H5MantPagadorRequisitoSupport.TabPayableByType_GridTblRequest();
        if (data.TabPayableByType_Grid_TabPayableByType_Item !== null)
            $('#TabPayableByType_GridTbl').bootstrapTable('load', data.TabPayableByType_Grid_TabPayableByType_Item);
        H5MantPagadorRequisitoSupport.TabPayableByTypeTranslator_GridTblRequest();
        if (data.TabPayableByTypeTranslator_Grid_TabPayableByType_Item !== null)
            $('#TabPayableByTypeTranslator_GridTbl').bootstrapTable('load', data.TabPayableByTypeTranslator_Grid_TabPayableByType_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#Payer', {
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
      new AutoNumeric('#PayerTranslator', {
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
					         H5MantPagadorRequisitoSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5MantPagadorRequisitoSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.TabPayableByType_Grid_insert = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPagadorRequisitoActions.aspx/TabPayableByType_Grid1InsertCommandActionTabPayableByType", false,
               JSON.stringify({ PAYER1: row.Payer, RECORDSTATUS2: row.RecordStatus, CREATORUSERCODE2: generalSupport.UserContext().userId, UPDATEUSERCODE4: generalSupport.UserContext().userId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPagadorRequisitoActions.aspx/TabPayableByType_Grid3InsertCommandActionTransPayableByType", false,
               JSON.stringify({ PAYER1: row.Payer, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabPayableByType_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message7 = 'Se agregó correctamente el registro';
            notification.toastr.success('Agregar registro', message7);
                    }                    
                    else {
            var message8 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message8);

                        }

    };
    this.TabPayableByType_Grid_update = function (row, $modal) {
          var data;
            var recordCount;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPagadorRequisitoActions.aspx/TabPayableByType_Grid1UpdateCommandActionTabPayableByType", false,
               JSON.stringify({ RECORDSTATUS1: row.RecordStatus, UPDATEUSERCODE1: generalSupport.UserContext().userId, TabPayableByTypePayer3: row.Payer }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPagadorRequisitoActions.aspx/TabPayableByType_Grid3SelectCommandActionTransPayableByType", false,
               JSON.stringify({                 TransPayableByTypePayer1: row.Payer,
                TransPayableByTypeLanguageId2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPagadorRequisitoActions.aspx/TabPayableByType_Grid5InsertCommandActionTransPayableByType", false,
               JSON.stringify({ PAYER1: row.Payer, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPagadorRequisitoActions.aspx/TabPayableByType_Grid6UpdateCommandActionTransPayableByType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransPayableByTypePayer4: row.Payer, TransPayableByTypeLanguageId5: generalSupport.SessionContext().languageId }));
               


                        }

                    }
        if (data.d.Success === true){
            $('#TabPayableByType_GridTbl').bootstrapTable('updateByUniqueId', { id: row.Payer, row: row });
            $modal.modal('hide');
            var message9 = 'Se actualizó correctamente el registro';
            notification.toastr.success('Actualizar registro', message9);
                        }                        
                        else {
            var message10 = 'No se pudo actualizar el registro';
            notification.swal.error('Actualizar registro', message10);

                            }

    };
    this.TabPayableByType_Grid_delete = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPagadorRequisitoActions.aspx/TabPayableByType_Grid1DeleteCommandActionTransPayableByType", false,
               JSON.stringify({ TransPayableByTypePayer1: row.Payer }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPagadorRequisitoActions.aspx/TabPayableByType_Grid3DeleteCommandActionTabPayableByType", false,
               JSON.stringify({ TabPayableByTypePayer1: row.Payer }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabPayableByType_GridTbl').bootstrapTable('remove', {field: 'Payer', values: [generalSupport.NumericValue('#Payer', -99999, 99999)]});
            var message7 = 'Se eliminó correctamente el registro';
            notification.toastr.success('Eliminar registro', message7);
                    }                    
                    else {
            var message8 = 'No se puede eliminar el registro';
            notification.toastr.error('Eliminar registro', message8);

                        }

    };
    this.TabPayableByType_Grid_BeforeShowPopup = function (row, $modal) {
          var data;
            var nextId;
        if (row.Payer === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPagadorRequisitoActions.aspx/TabPayableByType_Grid2SelectCommandActionTabPayableByType", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#Payer', nextId);

            }

    };
    this.TabPayableByTypeTranslator_Grid_update = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPagadorRequisitoActions.aspx/TabPayableByTypeTranslator_Grid1UpdateCommandActionTransPayableByType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransPayableByTypePayer4: row.Payer, TransPayableByTypeLanguageId5: row.LanguageId }));
               

        if (data.d.Success === true){
            $('#TabPayableByTypeTranslator_GridTbl').bootstrapTable('updateByUniqueId', { id: row.Payer, row: row });
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
            var formInstance = $("#H5MantPagadorRequisitoMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                H5MantPagadorRequisitoSupport.TabPayableByType_GridTblRequest();
                $('#TabPayableByType_GridContainer').toggleClass('hidden', false);
                $('#TabPayableByTypeTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#H5MantPagadorRequisitoMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                H5MantPagadorRequisitoSupport.TabPayableByTypeTranslator_GridTblRequest();
                $('#TabPayableByTypeTranslator_GridContainer').toggleClass('hidden', false);
                $('#TabPayableByType_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5MantPagadorRequisitoMainForm").validate({
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
        $("#TabPayableByType_GridEditForm").validate({
            rules: {
                Payer: {
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
                Payer: {
                    AutoNumericMinValue: $.i18n.t('app.validation.Payer.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.Payer.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.Payer.required')
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
        $("#TabPayableByTypeTranslator_GridEditForm").validate({
            rules: {
                PayerTranslator: {
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
                PayerTranslator: {
                    AutoNumericMinValue: $.i18n.t('app.validation.PayerTranslator.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.PayerTranslator.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.PayerTranslator.required')
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantPagadorRequisitoActions.aspx/LookUpForRecordStatus", false,
                JSON.stringify({ id: $('#H5MantPagadorRequisitoFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantPagadorRequisitoActions.aspx/LookUpForLanguageIdTranslator", false,
                JSON.stringify({ id: $('#H5MantPagadorRequisitoFormId').val() }),
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

    this.TabPayableByType_GridTblSetup = function (table) {
        H5MantPagadorRequisitoSupport.LookUpForRecordStatus('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'Payer',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabPayableByType_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'Payer',
                title: 'Pagador',
                events: 'TabPayableByType_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'RecordStatus',
                title: 'Estado del registro',
                formatter: 'H5MantPagadorRequisitoSupport.LookUpForRecordStatusFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5MantPagadorRequisitoSupport.CreatorUserCode_FormatterMaskData',
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
                formatter: 'H5MantPagadorRequisitoSupport.UpdateUserCode_FormatterMaskData',
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


        $('#TabPayableByType_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabPayableByType_GridTbl');
            $('#TabPayableByType_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabPayableByType_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabPayableByType_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantPagadorRequisitoSupport.TabPayableByType_GridRowToInput(row);
                H5MantPagadorRequisitoSupport.TabPayableByType_Grid_delete(row, null);
                
                return row.Payer;
            });

            $('#TabPayableByType_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabPayableByType_GridCreateBtn').click(function () {
            var formInstance = $("#TabPayableByType_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantPagadorRequisitoSupport.TabPayableByType_GridShowModal($('#TabPayableByType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabPayableByType_GridPopup').find('#TabPayableByType_GridSaveBtn').click(function () {
            var formInstance = $("#TabPayableByType_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabPayableByType_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabPayableByType_GridSaveBtn').html();
                $('#TabPayableByType_GridSaveBtn').html('Procesando...');
                $('#TabPayableByType_GridSaveBtn').prop('disabled', true);

                H5MantPagadorRequisitoSupport.currentRow.Payer = generalSupport.NumericValue('#Payer', -99999, 99999);
                H5MantPagadorRequisitoSupport.currentRow.RecordStatus = $('#RecordStatus').val();
                H5MantPagadorRequisitoSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                H5MantPagadorRequisitoSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                H5MantPagadorRequisitoSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);
                H5MantPagadorRequisitoSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';
                H5MantPagadorRequisitoSupport.currentRow.Description = $('#Description').val();
                H5MantPagadorRequisitoSupport.currentRow.ShortDescription = $('#ShortDescription').val();

                $('#TabPayableByType_GridSaveBtn').prop('disabled', false);
                $('#TabPayableByType_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantPagadorRequisitoSupport.TabPayableByType_Grid_update(H5MantPagadorRequisitoSupport.currentRow, $modal);
                }
                else {                    
                    H5MantPagadorRequisitoSupport.TabPayableByType_Grid_insert(H5MantPagadorRequisitoSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabPayableByType_GridShowModal = function (md, title, row) {
        row = row || { Payer: 0, RecordStatus: 0, CreatorUserCode: 0, CreationDate: null, UpdateUserCode: 0, UpdateDate: null, Description: null, ShortDescription: null };

        md.data('id', row.Payer);
        md.find('.modal-title').text(title);

        H5MantPagadorRequisitoSupport.TabPayableByType_GridRowToInput(row);
        $('#Payer').prop('disabled', (row.Payer !== 0));
        $('#CreatorUserCode').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        H5MantPagadorRequisitoSupport.TabPayableByType_Grid_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.TabPayableByType_GridRowToInput = function (row) {
        H5MantPagadorRequisitoSupport.currentRow = row;
        AutoNumeric.set('#Payer', row.Payer);
        H5MantPagadorRequisitoSupport.LookUpForRecordStatus(row.RecordStatus, '');
        $('#RecordStatus').trigger('change');
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#Description').val(row.Description);
        $('#ShortDescription').val(row.ShortDescription);

    };
    this.TabPayableByType_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantPagadorRequisitoActions.aspx/TabPayableByType_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                TransPayableByTypeLanguageId1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#TabPayableByType_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.TabPayableByTypeTranslator_GridTblSetup = function (table) {
        H5MantPagadorRequisitoSupport.LookUpForLanguageIdTranslator('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'Payer',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabPayableByTypeTranslator_Gridtoolbar',
            columns: [{
                field: 'Payer',
                title: 'Pagador',
                formatter: 'H5MantPagadorRequisitoSupport.PayerTranslator_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LanguageId',
                title: 'Idioma',
                formatter: 'H5MantPagadorRequisitoSupport.LookUpForLanguageIdTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Description',
                title: 'Descripción',
                events: 'TabPayableByTypeTranslator_GridActionEvents',
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


        $('#TabPayableByTypeTranslator_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabPayableByTypeTranslator_GridTbl');
            $('#TabPayableByTypeTranslator_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabPayableByTypeTranslator_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabPayableByTypeTranslator_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantPagadorRequisitoSupport.TabPayableByTypeTranslator_GridRowToInput(row);
                
                
                return row.Payer;
            });
            
          $('#TabPayableByTypeTranslator_GridTbl').bootstrapTable('remove', {
                field: 'Payer',
                values: ids
           });

            $('#TabPayableByTypeTranslator_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabPayableByTypeTranslator_GridCreateBtn').click(function () {
            var formInstance = $("#TabPayableByTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantPagadorRequisitoSupport.TabPayableByTypeTranslator_GridShowModal($('#TabPayableByTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabPayableByTypeTranslator_GridPopup').find('#TabPayableByTypeTranslator_GridSaveBtn').click(function () {
            var formInstance = $("#TabPayableByTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabPayableByTypeTranslator_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabPayableByTypeTranslator_GridSaveBtn').html();
                $('#TabPayableByTypeTranslator_GridSaveBtn').html('Procesando...');
                $('#TabPayableByTypeTranslator_GridSaveBtn').prop('disabled', true);

                H5MantPagadorRequisitoSupport.currentRow.Payer = generalSupport.NumericValue('#PayerTranslator', -99999, 99999);
                H5MantPagadorRequisitoSupport.currentRow.LanguageId = parseInt(0 + $('#LanguageIdTranslator').val(), 10);
                H5MantPagadorRequisitoSupport.currentRow.Description = $('#DescriptionTranslator').val();
                H5MantPagadorRequisitoSupport.currentRow.ShortDescription = $('#ShortDescriptionTranslator').val();

                $('#TabPayableByTypeTranslator_GridSaveBtn').prop('disabled', false);
                $('#TabPayableByTypeTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantPagadorRequisitoSupport.TabPayableByTypeTranslator_Grid_update(H5MantPagadorRequisitoSupport.currentRow, $modal);
                }
                else {                    
                    $('#TabPayableByTypeTranslator_GridTbl').bootstrapTable('append', H5MantPagadorRequisitoSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabPayableByTypeTranslator_GridShowModal = function (md, title, row) {
        row = row || { Payer: 0, LanguageId: 0, Description: null, ShortDescription: null };

        md.data('id', row.Payer);
        md.find('.modal-title').text(title);

        H5MantPagadorRequisitoSupport.TabPayableByTypeTranslator_GridRowToInput(row);
        $('#PayerTranslator').prop('disabled', true);
        $('#LanguageIdTranslator').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabPayableByTypeTranslator_GridRowToInput = function (row) {
        H5MantPagadorRequisitoSupport.currentRow = row;
        AutoNumeric.set('#PayerTranslator', row.Payer);
        H5MantPagadorRequisitoSupport.LookUpForLanguageIdTranslator(row.LanguageId, '');
        $('#LanguageIdTranslator').trigger('change');
        $('#DescriptionTranslator').val(row.Description);
        $('#ShortDescriptionTranslator').val(row.ShortDescription);

    };
    this.TabPayableByTypeTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantPagadorRequisitoActions.aspx/TabPayableByTypeTranslator_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#TabPayableByTypeTranslator_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.Payer_FormatterMaskData = function (value, row, index) {          
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
    this.PayerTranslator_FormatterMaskData = function (value, row, index) {          
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
        
        H5MantPagadorRequisitoSupport.ValidateSetup();
        tableHelperSupport.Translate('#TabPayableByType_GridTbl', '#TabPayableByType_GridTbl');
tableHelperSupport.Translate('#TabPayableByTypeTranslator_GridTbl', '#TabPayableByTypeTranslator_GridTbl');

    });
        

    H5MantPagadorRequisitoSupport.ControlBehaviour();
    H5MantPagadorRequisitoSupport.ControlActions();
    

    $("#TabPayableByType_GridTblPlaceHolder").replaceWith('<table id="TabPayableByType_GridTbl"></table>');
    H5MantPagadorRequisitoSupport.TabPayableByType_GridTblSetup($('#TabPayableByType_GridTbl'));
    $("#TabPayableByTypeTranslator_GridTblPlaceHolder").replaceWith('<table id="TabPayableByTypeTranslator_GridTbl"></table>');
    H5MantPagadorRequisitoSupport.TabPayableByTypeTranslator_GridTblSetup($('#TabPayableByTypeTranslator_GridTbl'));

        H5MantPagadorRequisitoSupport.TabPayableByType_GridTblRequest();
        H5MantPagadorRequisitoSupport.TabPayableByTypeTranslator_GridTblRequest();




  };
};

$(document).ready(function () {
   H5MantPagadorRequisitoSupport.Init();
});

window.TabPayableByType_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantPagadorRequisitoSupport.TabPayableByType_GridShowModal($('#TabPayableByType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.TabPayableByTypeTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantPagadorRequisitoSupport.TabPayableByTypeTranslator_GridShowModal($('#TabPayableByTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
