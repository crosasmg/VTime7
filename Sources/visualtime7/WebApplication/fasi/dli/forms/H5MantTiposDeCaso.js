var H5MantTiposDeCasoSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5MantTiposDeCasoFormId').val(),
            TabUnderwritingCaseType_Grid_TabUnderwritingCaseType_Item: generalSupport.NormalizeProperties($('#TabUnderwritingCaseType_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate'),
            TabUnderwritingCaseTypeTranslator_Grid_TabUnderwritingCaseType_Item: generalSupport.NormalizeProperties($('#TabUnderwritingCaseTypeTranslator_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#H5MantTiposDeCasoFormId').val(data.InstanceFormId);

        H5MantTiposDeCasoSupport.LookUpForRecordStatus(source);
        H5MantTiposDeCasoSupport.LookUpForLanguageIdTranslator(source);

        H5MantTiposDeCasoSupport.TabUnderwritingCaseType_GridTblRequest();
        if (data.TabUnderwritingCaseType_Grid_TabUnderwritingCaseType_Item !== null)
            $('#TabUnderwritingCaseType_GridTbl').bootstrapTable('load', data.TabUnderwritingCaseType_Grid_TabUnderwritingCaseType_Item);
        H5MantTiposDeCasoSupport.TabUnderwritingCaseTypeTranslator_GridTblRequest();
        if (data.TabUnderwritingCaseTypeTranslator_Grid_TabUnderwritingCaseType_Item !== null)
            $('#TabUnderwritingCaseTypeTranslator_GridTbl').bootstrapTable('load', data.TabUnderwritingCaseTypeTranslator_Grid_TabUnderwritingCaseType_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#UnderwritingCaseType', {
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
      new AutoNumeric('#UnderwritingCaseTypeTranslator', {
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
					         H5MantTiposDeCasoSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5MantTiposDeCasoSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.TabUnderwritingCaseType_Grid_insert = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDeCasoActions.aspx/TabUnderwritingCaseType_Grid1InsertCommandActionTabUnderwritingCaseType", false,
               JSON.stringify({ UNDERWRITINGCASETYPE1: row.UnderwritingCaseType, RECORDSTATUS2: row.RecordStatus, CREATORUSERCODE2: generalSupport.UserContext().userId, UPDATEUSERCODE4: generalSupport.UserContext().userId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDeCasoActions.aspx/TabUnderwritingCaseType_Grid3InsertCommandActionTransUnderwritingCaseType", false,
               JSON.stringify({ UNDERWRITINGCASETYPE1: row.UnderwritingCaseType, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabUnderwritingCaseType_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message7 = 'Se agregó correctamente el registro';
            notification.toastr.success('Agregar registro', message7);
                    }                    
                    else {
            var message8 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message8);

                        }

    };
    this.TabUnderwritingCaseType_Grid_update = function (row, $modal) {
          var data;
            var recordCount;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDeCasoActions.aspx/TabUnderwritingCaseType_Grid1UpdateCommandActionTabUnderwritingCaseType", false,
               JSON.stringify({ RECORDSTATUS1: row.RecordStatus, UPDATEUSERCODE1: generalSupport.UserContext().userId, TabUnderwritingCaseTypeUnderwritingCaseType4: row.UnderwritingCaseType }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDeCasoActions.aspx/TabUnderwritingCaseType_Grid3SelectCommandActionTransUnderwritingCaseType", false,
               JSON.stringify({                 TransUnderwritingCaseTypeUnderwritingCaseType1: row.UnderwritingCaseType,
                TransUnderwritingCaseTypeLanguageId2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDeCasoActions.aspx/TabUnderwritingCaseType_Grid5InsertCommandActionTransUnderwritingCaseType", false,
               JSON.stringify({ UNDERWRITINGCASETYPE1: row.UnderwritingCaseType, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDeCasoActions.aspx/TabUnderwritingCaseType_Grid6UpdateCommandActionTransUnderwritingCaseType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransUnderwritingCaseTypeUnderwritingCaseType4: row.UnderwritingCaseType, TransUnderwritingCaseTypeLanguageId5: generalSupport.SessionContext().languageId }));
               


                        }

                    }
        if (data.d.Success === true){
            $('#TabUnderwritingCaseType_GridTbl').bootstrapTable('updateByUniqueId', { id: row.UnderwritingCaseType, row: row });
            $modal.modal('hide');
            var message9 = 'Se actualizó correctamente el registro';
            notification.toastr.success('Actualizar registro', message9);
                        }                        
                        else {
            var message10 = 'No se pudo actualizar el registro';
            notification.swal.error('Actualizar registro', message10);

                            }

    };
    this.TabUnderwritingCaseType_Grid_delete = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDeCasoActions.aspx/TabUnderwritingCaseType_Grid1DeleteCommandActionTransUnderwritingCaseType", false,
               JSON.stringify({ TransUnderwritingCaseTypeUnderwritingCaseType1: row.UnderwritingCaseType }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDeCasoActions.aspx/TabUnderwritingCaseType_Grid3DeleteCommandActionTabUnderwritingCaseType", false,
               JSON.stringify({ TabUnderwritingCaseTypeUnderwritingCaseType1: row.UnderwritingCaseType }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabUnderwritingCaseType_GridTbl').bootstrapTable('remove', {field: 'UnderwritingCaseType', values: [generalSupport.NumericValue('#UnderwritingCaseType', -99999, 99999)]});
            var message7 = 'Se eliminó correctamente el registro';
            notification.toastr.success('Eliminar registro', message7);
                    }                    
                    else {
            var message8 = 'No se puede eliminar el registro';
            notification.toastr.error('Eliminar registro', message8);

                        }

    };
    this.TabUnderwritingCaseType_Grid_BeforeShowPopup = function (row, $modal) {
          var data;
            var nextId;
        if (row.UnderwritingCaseType === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDeCasoActions.aspx/TabUnderwritingCaseType_Grid2SelectCommandActionTabUnderwritingCaseType", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#UnderwritingCaseType', nextId);

            }

    };
    this.TabUnderwritingCaseTypeTranslator_Grid_update = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDeCasoActions.aspx/TabUnderwritingCaseTypeTranslator_Grid1UpdateCommandActionTransUnderwritingCaseType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransUnderwritingCaseTypeUnderwritingCaseType4: row.UnderwritingCaseType, TransUnderwritingCaseTypeLanguageId5: row.LanguageId }));
               

        if (data.d.Success === true){
            $('#TabUnderwritingCaseTypeTranslator_GridTbl').bootstrapTable('updateByUniqueId', { id: row.UnderwritingCaseType, row: row });
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
            var formInstance = $("#H5MantTiposDeCasoMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                H5MantTiposDeCasoSupport.TabUnderwritingCaseType_GridTblRequest();
                $('#TabUnderwritingCaseType_GridContainer').toggleClass('hidden', false);
                $('#TabUnderwritingCaseTypeTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#H5MantTiposDeCasoMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                H5MantTiposDeCasoSupport.TabUnderwritingCaseTypeTranslator_GridTblRequest();
                $('#TabUnderwritingCaseTypeTranslator_GridContainer').toggleClass('hidden', false);
                $('#TabUnderwritingCaseType_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5MantTiposDeCasoMainForm").validate({
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
        $("#TabUnderwritingCaseType_GridEditForm").validate({
            rules: {
                UnderwritingCaseType: {
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
                UnderwritingCaseType: {
                    AutoNumericMinValue: $.i18n.t('app.validation.UnderwritingCaseType.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.UnderwritingCaseType.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.UnderwritingCaseType.required')
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
        $("#TabUnderwritingCaseTypeTranslator_GridEditForm").validate({
            rules: {
                UnderwritingCaseTypeTranslator: {
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
                UnderwritingCaseTypeTranslator: {
                    AutoNumericMinValue: $.i18n.t('app.validation.UnderwritingCaseTypeTranslator.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.UnderwritingCaseTypeTranslator.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.UnderwritingCaseTypeTranslator.required')
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTiposDeCasoActions.aspx/LookUpForRecordStatus", false,
                JSON.stringify({ id: $('#H5MantTiposDeCasoFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTiposDeCasoActions.aspx/LookUpForLanguageIdTranslator", false,
                JSON.stringify({ id: $('#H5MantTiposDeCasoFormId').val() }),
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

    this.TabUnderwritingCaseType_GridTblSetup = function (table) {
        H5MantTiposDeCasoSupport.LookUpForRecordStatus('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'UnderwritingCaseType',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabUnderwritingCaseType_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'UnderwritingCaseType',
                title: 'Tipo de suscripción',
                events: 'TabUnderwritingCaseType_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5MantTiposDeCasoSupport.CreatorUserCode_FormatterMaskData',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'CreationDate',
                title: 'Fecha de Actualización del Registro',
                formatter: 'tableHelperSupport.OnlyDateFormatterWithHoursMinutesSeconds',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'UpdateUserCode',
                title: 'Última actualización por',
                formatter: 'H5MantTiposDeCasoSupport.UpdateUserCode_FormatterMaskData',
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
                title: 'Estado del Registro',
                formatter: 'H5MantTiposDeCasoSupport.LookUpForRecordStatusFormatter',
                sortable: true,
                halign: 'center'
            }]
        });


        $('#TabUnderwritingCaseType_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabUnderwritingCaseType_GridTbl');
            $('#TabUnderwritingCaseType_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabUnderwritingCaseType_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabUnderwritingCaseType_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantTiposDeCasoSupport.TabUnderwritingCaseType_GridRowToInput(row);
                H5MantTiposDeCasoSupport.TabUnderwritingCaseType_Grid_delete(row, null);
                
                return row.UnderwritingCaseType;
            });

            $('#TabUnderwritingCaseType_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabUnderwritingCaseType_GridCreateBtn').click(function () {
            var formInstance = $("#TabUnderwritingCaseType_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantTiposDeCasoSupport.TabUnderwritingCaseType_GridShowModal($('#TabUnderwritingCaseType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabUnderwritingCaseType_GridPopup').find('#TabUnderwritingCaseType_GridSaveBtn').click(function () {
            var formInstance = $("#TabUnderwritingCaseType_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabUnderwritingCaseType_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabUnderwritingCaseType_GridSaveBtn').html();
                $('#TabUnderwritingCaseType_GridSaveBtn').html('Procesando...');
                $('#TabUnderwritingCaseType_GridSaveBtn').prop('disabled', true);

                H5MantTiposDeCasoSupport.currentRow.UnderwritingCaseType = generalSupport.NumericValue('#UnderwritingCaseType', -99999, 99999);
                H5MantTiposDeCasoSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                H5MantTiposDeCasoSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                H5MantTiposDeCasoSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);
                H5MantTiposDeCasoSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';
                H5MantTiposDeCasoSupport.currentRow.Description = $('#Description').val();
                H5MantTiposDeCasoSupport.currentRow.ShortDescription = $('#ShortDescription').val();
                H5MantTiposDeCasoSupport.currentRow.RecordStatus = $('#RecordStatus').val();

                $('#TabUnderwritingCaseType_GridSaveBtn').prop('disabled', false);
                $('#TabUnderwritingCaseType_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantTiposDeCasoSupport.TabUnderwritingCaseType_Grid_update(H5MantTiposDeCasoSupport.currentRow, $modal);
                }
                else {                    
                    H5MantTiposDeCasoSupport.TabUnderwritingCaseType_Grid_insert(H5MantTiposDeCasoSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabUnderwritingCaseType_GridShowModal = function (md, title, row) {
        row = row || { UnderwritingCaseType: 0, CreatorUserCode: 0, CreationDate: null, UpdateUserCode: 0, UpdateDate: null, Description: null, ShortDescription: null, RecordStatus: 0 };

        md.data('id', row.UnderwritingCaseType);
        md.find('.modal-title').text(title);

        H5MantTiposDeCasoSupport.TabUnderwritingCaseType_GridRowToInput(row);
        $('#UnderwritingCaseType').prop('disabled', (row.UnderwritingCaseType !== 0));
        $('#CreatorUserCode').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        H5MantTiposDeCasoSupport.TabUnderwritingCaseType_Grid_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.TabUnderwritingCaseType_GridRowToInput = function (row) {
        H5MantTiposDeCasoSupport.currentRow = row;
        AutoNumeric.set('#UnderwritingCaseType', row.UnderwritingCaseType);
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#Description').val(row.Description);
        $('#ShortDescription').val(row.ShortDescription);
        H5MantTiposDeCasoSupport.LookUpForRecordStatus(row.RecordStatus, '');
        $('#RecordStatus').trigger('change');

    };
    this.TabUnderwritingCaseType_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTiposDeCasoActions.aspx/TabUnderwritingCaseType_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                TransUnderwritingCaseTypeLanguageId1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#TabUnderwritingCaseType_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.TabUnderwritingCaseTypeTranslator_GridTblSetup = function (table) {
        H5MantTiposDeCasoSupport.LookUpForLanguageIdTranslator('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'UnderwritingCaseType',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabUnderwritingCaseTypeTranslator_Gridtoolbar',
            columns: [{
                field: 'UnderwritingCaseType',
                title: 'Tipo de suscripción',
                formatter: 'H5MantTiposDeCasoSupport.UnderwritingCaseTypeTranslator_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LanguageId',
                title: 'Idioma',
                formatter: 'H5MantTiposDeCasoSupport.LookUpForLanguageIdTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Description',
                title: 'Descripción',
                events: 'TabUnderwritingCaseTypeTranslator_GridActionEvents',
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


        $('#TabUnderwritingCaseTypeTranslator_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabUnderwritingCaseTypeTranslator_GridTbl');
            $('#TabUnderwritingCaseTypeTranslator_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabUnderwritingCaseTypeTranslator_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabUnderwritingCaseTypeTranslator_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantTiposDeCasoSupport.TabUnderwritingCaseTypeTranslator_GridRowToInput(row);
                
                
                return row.UnderwritingCaseType;
            });
            
          $('#TabUnderwritingCaseTypeTranslator_GridTbl').bootstrapTable('remove', {
                field: 'UnderwritingCaseType',
                values: ids
           });

            $('#TabUnderwritingCaseTypeTranslator_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabUnderwritingCaseTypeTranslator_GridCreateBtn').click(function () {
            var formInstance = $("#TabUnderwritingCaseTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantTiposDeCasoSupport.TabUnderwritingCaseTypeTranslator_GridShowModal($('#TabUnderwritingCaseTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabUnderwritingCaseTypeTranslator_GridPopup').find('#TabUnderwritingCaseTypeTranslator_GridSaveBtn').click(function () {
            var formInstance = $("#TabUnderwritingCaseTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabUnderwritingCaseTypeTranslator_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabUnderwritingCaseTypeTranslator_GridSaveBtn').html();
                $('#TabUnderwritingCaseTypeTranslator_GridSaveBtn').html('Procesando...');
                $('#TabUnderwritingCaseTypeTranslator_GridSaveBtn').prop('disabled', true);

                H5MantTiposDeCasoSupport.currentRow.UnderwritingCaseType = generalSupport.NumericValue('#UnderwritingCaseTypeTranslator', -99999, 99999);
                H5MantTiposDeCasoSupport.currentRow.LanguageId = parseInt(0 + $('#LanguageIdTranslator').val(), 10);
                H5MantTiposDeCasoSupport.currentRow.Description = $('#DescriptionTranslator').val();
                H5MantTiposDeCasoSupport.currentRow.ShortDescription = $('#ShortDescriptionTranslator').val();

                $('#TabUnderwritingCaseTypeTranslator_GridSaveBtn').prop('disabled', false);
                $('#TabUnderwritingCaseTypeTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantTiposDeCasoSupport.TabUnderwritingCaseTypeTranslator_Grid_update(H5MantTiposDeCasoSupport.currentRow, $modal);
                }
                else {                    
                    $('#TabUnderwritingCaseTypeTranslator_GridTbl').bootstrapTable('append', H5MantTiposDeCasoSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabUnderwritingCaseTypeTranslator_GridShowModal = function (md, title, row) {
        row = row || { UnderwritingCaseType: 0, LanguageId: 0, Description: null, ShortDescription: null };

        md.data('id', row.UnderwritingCaseType);
        md.find('.modal-title').text(title);

        H5MantTiposDeCasoSupport.TabUnderwritingCaseTypeTranslator_GridRowToInput(row);
        $('#UnderwritingCaseTypeTranslator').prop('disabled', true);
        $('#LanguageIdTranslator').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabUnderwritingCaseTypeTranslator_GridRowToInput = function (row) {
        H5MantTiposDeCasoSupport.currentRow = row;
        AutoNumeric.set('#UnderwritingCaseTypeTranslator', row.UnderwritingCaseType);
        H5MantTiposDeCasoSupport.LookUpForLanguageIdTranslator(row.LanguageId, '');
        $('#LanguageIdTranslator').trigger('change');
        $('#DescriptionTranslator').val(row.Description);
        $('#ShortDescriptionTranslator').val(row.ShortDescription);

    };
    this.TabUnderwritingCaseTypeTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTiposDeCasoActions.aspx/TabUnderwritingCaseTypeTranslator_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#TabUnderwritingCaseTypeTranslator_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.UnderwritingCaseType_FormatterMaskData = function (value, row, index) {          
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
    this.UnderwritingCaseTypeTranslator_FormatterMaskData = function (value, row, index) {          
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
        
        H5MantTiposDeCasoSupport.ValidateSetup();
        tableHelperSupport.Translate('#TabUnderwritingCaseType_GridTbl', '#TabUnderwritingCaseType_GridTbl');
tableHelperSupport.Translate('#TabUnderwritingCaseTypeTranslator_GridTbl', '#TabUnderwritingCaseTypeTranslator_GridTbl');

    });
        

    H5MantTiposDeCasoSupport.ControlBehaviour();
    H5MantTiposDeCasoSupport.ControlActions();
    

    $("#TabUnderwritingCaseType_GridTblPlaceHolder").replaceWith('<table id="TabUnderwritingCaseType_GridTbl"></table>');
    H5MantTiposDeCasoSupport.TabUnderwritingCaseType_GridTblSetup($('#TabUnderwritingCaseType_GridTbl'));
    $("#TabUnderwritingCaseTypeTranslator_GridTblPlaceHolder").replaceWith('<table id="TabUnderwritingCaseTypeTranslator_GridTbl"></table>');
    H5MantTiposDeCasoSupport.TabUnderwritingCaseTypeTranslator_GridTblSetup($('#TabUnderwritingCaseTypeTranslator_GridTbl'));

        H5MantTiposDeCasoSupport.TabUnderwritingCaseType_GridTblRequest();
        H5MantTiposDeCasoSupport.TabUnderwritingCaseTypeTranslator_GridTblRequest();




  };
};

$(document).ready(function () {
   H5MantTiposDeCasoSupport.Init();
});

window.TabUnderwritingCaseType_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantTiposDeCasoSupport.TabUnderwritingCaseType_GridShowModal($('#TabUnderwritingCaseType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.TabUnderwritingCaseTypeTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantTiposDeCasoSupport.TabUnderwritingCaseTypeTranslator_GridShowModal($('#TabUnderwritingCaseTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
