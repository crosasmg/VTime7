var H5MantEstadoDelCAsoSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5MantEstadoDelCAsoFormId').val(),
            TabUnderwritingCaseSType_Grid_TabUnderwritingCaseSType_Item: generalSupport.NormalizeProperties($('#TabUnderwritingCaseSType_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate'),
            TabUnderwritingCaseSTypeTranslator_Grid_TabUnderwritingCaseSType_Item: generalSupport.NormalizeProperties($('#TabUnderwritingCaseSTypeTranslator_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#H5MantEstadoDelCAsoFormId').val(data.InstanceFormId);

        H5MantEstadoDelCAsoSupport.LookUpForRecordStatus(source);
        H5MantEstadoDelCAsoSupport.LookUpForLanguageIdTranslator(source);

        H5MantEstadoDelCAsoSupport.TabUnderwritingCaseSType_GridTblRequest();
        if (data.TabUnderwritingCaseSType_Grid_TabUnderwritingCaseSType_Item !== null)
            $('#TabUnderwritingCaseSType_GridTbl').bootstrapTable('load', data.TabUnderwritingCaseSType_Grid_TabUnderwritingCaseSType_Item);
        H5MantEstadoDelCAsoSupport.TabUnderwritingCaseSTypeTranslator_GridTblRequest();
        if (data.TabUnderwritingCaseSTypeTranslator_Grid_TabUnderwritingCaseSType_Item !== null)
            $('#TabUnderwritingCaseSTypeTranslator_GridTbl').bootstrapTable('load', data.TabUnderwritingCaseSTypeTranslator_Grid_TabUnderwritingCaseSType_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#UnderwritingCaseStatus', {
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
      new AutoNumeric('#UnderwritingCaseStatusTranslator', {
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
					         H5MantEstadoDelCAsoSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5MantEstadoDelCAsoSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.TabUnderwritingCaseSType_Grid_insert = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEstadoDelCAsoActions.aspx/TabUnderwritingCaseSType_Grid1InsertCommandActionTabUnderwritingCaseSType", false,
               JSON.stringify({ UNDERWRITINGCASESTATUS1: row.UnderwritingCaseStatus, RECORDSTATUS2: row.RecordStatus, CREATORUSERCODE2: generalSupport.UserContext().userId, UPDATEUSERCODE4: generalSupport.UserContext().userId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEstadoDelCAsoActions.aspx/TabUnderwritingCaseSType_Grid3InsertCommandActionTransUnderwritingCaseSType", false,
               JSON.stringify({ UNDERWRITINGCASESTATUS1: row.UnderwritingCaseStatus, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabUnderwritingCaseSType_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message7 = 'Se agregó correctamente el registro';
            notification.toastr.success('Agregar registro', message7);
                    }                    
                    else {
            var message8 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message8);

                        }

    };
    this.TabUnderwritingCaseSType_Grid_update = function (row, $modal) {
          var data;
            var recordCount;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEstadoDelCAsoActions.aspx/TabUnderwritingCaseSType_Grid1UpdateCommandActionTabUnderwritingCaseSType", false,
               JSON.stringify({ RECORDSTATUS1: row.RecordStatus, UPDATEUSERCODE1: generalSupport.UserContext().userId, TabUnderwritingCaseSTypeUnderwritingCaseStatus3: row.UnderwritingCaseStatus }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEstadoDelCAsoActions.aspx/TabUnderwritingCaseSType_Grid3SelectCommandActionTransUnderwritingCaseSType", false,
               JSON.stringify({                 TransUnderwritingCaseSTypeUnderwritingCaseStatus1: row.UnderwritingCaseStatus,
                TransUnderwritingCaseSTypeLanguageId2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEstadoDelCAsoActions.aspx/TabUnderwritingCaseSType_Grid5InsertCommandActionTransUnderwritingCaseSType", false,
               JSON.stringify({ UNDERWRITINGCASESTATUS1: row.UnderwritingCaseStatus, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEstadoDelCAsoActions.aspx/TabUnderwritingCaseSType_Grid6UpdateCommandActionTransUnderwritingCaseSType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransUnderwritingCaseSTypeUnderwritingCaseStatus4: row.UnderwritingCaseStatus, TransUnderwritingCaseSTypeLanguageId5: generalSupport.SessionContext().languageId }));
               


                        }

                    }
        if (data.d.Success === true){
            $('#TabUnderwritingCaseSType_GridTbl').bootstrapTable('updateByUniqueId', { id: row.UnderwritingCaseStatus, row: row });
            $modal.modal('hide');
            var message9 = 'Se actualizó correctamente el registro';
            notification.toastr.success('Actualizar registro', message9);
                        }                        
                        else {
            var message10 = 'No se pudo actualizar el registro';
            notification.swal.error('Actualizar registro', message10);

                            }

    };
    this.TabUnderwritingCaseSType_Grid_delete = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEstadoDelCAsoActions.aspx/TabUnderwritingCaseSType_Grid1DeleteCommandActionTransUnderwritingCaseSType", false,
               JSON.stringify({ TransUnderwritingCaseSTypeUnderwritingCaseStatus1: row.UnderwritingCaseStatus }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEstadoDelCAsoActions.aspx/TabUnderwritingCaseSType_Grid3DeleteCommandActionTabUnderwritingCaseSType", false,
               JSON.stringify({ TabUnderwritingCaseSTypeUnderwritingCaseStatus1: row.UnderwritingCaseStatus }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabUnderwritingCaseSType_GridTbl').bootstrapTable('remove', {field: 'UnderwritingCaseStatus', values: [generalSupport.NumericValue('#UnderwritingCaseStatus', -99999, 99999)]});
            var message7 = 'Se eliminó correctamente el registro';
            notification.toastr.success('Eliminar registro', message7);
                    }                    
                    else {
            var message8 = 'No se puede eliminar el registro';
            notification.toastr.error('Eliminar registro', message8);

                        }

    };
    this.TabUnderwritingCaseSType_Grid_BeforeShowPopup = function (row, $modal) {
          var data;
            var nextId;
        if (row.UnderwritingCaseStatus === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEstadoDelCAsoActions.aspx/TabUnderwritingCaseSType_Grid2SelectCommandActionTabUnderwritingCaseSType", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#UnderwritingCaseStatus', nextId);

            }

    };
    this.TabUnderwritingCaseSTypeTranslator_Grid_update = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEstadoDelCAsoActions.aspx/TabUnderwritingCaseSTypeTranslator_Grid1UpdateCommandActionTransUnderwritingCaseSType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransUnderwritingCaseSTypeUnderwritingCaseStatus4: row.UnderwritingCaseStatus, TransUnderwritingCaseSTypeLanguageId5: row.LanguageId }));
               

        if (data.d.Success === true){
            $('#TabUnderwritingCaseSTypeTranslator_GridTbl').bootstrapTable('updateByUniqueId', { id: row.UnderwritingCaseStatus, row: row });
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
            var formInstance = $("#H5MantEstadoDelCAsoMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                H5MantEstadoDelCAsoSupport.TabUnderwritingCaseSType_GridTblRequest();
                $('#TabUnderwritingCaseSType_GridContainer').toggleClass('hidden', false);
                $('#TabUnderwritingCaseSTypeTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#H5MantEstadoDelCAsoMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                H5MantEstadoDelCAsoSupport.TabUnderwritingCaseSTypeTranslator_GridTblRequest();
                $('#TabUnderwritingCaseSTypeTranslator_GridContainer').toggleClass('hidden', false);
                $('#TabUnderwritingCaseSType_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5MantEstadoDelCAsoMainForm").validate({
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
        $("#TabUnderwritingCaseSType_GridEditForm").validate({
            rules: {
                UnderwritingCaseStatus: {
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
                UnderwritingCaseStatus: {
                    AutoNumericMinValue: $.i18n.t('app.validation.UnderwritingCaseStatus.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.UnderwritingCaseStatus.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.UnderwritingCaseStatus.required')
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
        $("#TabUnderwritingCaseSTypeTranslator_GridEditForm").validate({
            rules: {
                UnderwritingCaseStatusTranslator: {
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
                UnderwritingCaseStatusTranslator: {
                    AutoNumericMinValue: $.i18n.t('app.validation.UnderwritingCaseStatusTranslator.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.UnderwritingCaseStatusTranslator.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.UnderwritingCaseStatusTranslator.required')
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantEstadoDelCAsoActions.aspx/LookUpForRecordStatus", false,
                JSON.stringify({ id: $('#H5MantEstadoDelCAsoFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantEstadoDelCAsoActions.aspx/LookUpForLanguageIdTranslator", false,
                JSON.stringify({ id: $('#H5MantEstadoDelCAsoFormId').val() }),
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

    this.TabUnderwritingCaseSType_GridTblSetup = function (table) {
        H5MantEstadoDelCAsoSupport.LookUpForRecordStatus('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'UnderwritingCaseStatus',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabUnderwritingCaseSType_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'UnderwritingCaseStatus',
                title: 'Estado del caso',
                events: 'TabUnderwritingCaseSType_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5MantEstadoDelCAsoSupport.CreatorUserCode_FormatterMaskData',
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
                formatter: 'H5MantEstadoDelCAsoSupport.UpdateUserCode_FormatterMaskData',
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
                formatter: 'H5MantEstadoDelCAsoSupport.LookUpForRecordStatusFormatter',
                sortable: true,
                halign: 'center'
            }]
        });


        $('#TabUnderwritingCaseSType_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabUnderwritingCaseSType_GridTbl');
            $('#TabUnderwritingCaseSType_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabUnderwritingCaseSType_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabUnderwritingCaseSType_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantEstadoDelCAsoSupport.TabUnderwritingCaseSType_GridRowToInput(row);
                H5MantEstadoDelCAsoSupport.TabUnderwritingCaseSType_Grid_delete(row, null);
                
                return row.UnderwritingCaseStatus;
            });

            $('#TabUnderwritingCaseSType_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabUnderwritingCaseSType_GridCreateBtn').click(function () {
            var formInstance = $("#TabUnderwritingCaseSType_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantEstadoDelCAsoSupport.TabUnderwritingCaseSType_GridShowModal($('#TabUnderwritingCaseSType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabUnderwritingCaseSType_GridPopup').find('#TabUnderwritingCaseSType_GridSaveBtn').click(function () {
            var formInstance = $("#TabUnderwritingCaseSType_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabUnderwritingCaseSType_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabUnderwritingCaseSType_GridSaveBtn').html();
                $('#TabUnderwritingCaseSType_GridSaveBtn').html('Procesando...');
                $('#TabUnderwritingCaseSType_GridSaveBtn').prop('disabled', true);

                H5MantEstadoDelCAsoSupport.currentRow.UnderwritingCaseStatus = generalSupport.NumericValue('#UnderwritingCaseStatus', -99999, 99999);
                H5MantEstadoDelCAsoSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                H5MantEstadoDelCAsoSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                H5MantEstadoDelCAsoSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);
                H5MantEstadoDelCAsoSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';
                H5MantEstadoDelCAsoSupport.currentRow.Description = $('#Description').val();
                H5MantEstadoDelCAsoSupport.currentRow.ShortDescription = $('#ShortDescription').val();
                H5MantEstadoDelCAsoSupport.currentRow.RecordStatus = $('#RecordStatus').val();

                $('#TabUnderwritingCaseSType_GridSaveBtn').prop('disabled', false);
                $('#TabUnderwritingCaseSType_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantEstadoDelCAsoSupport.TabUnderwritingCaseSType_Grid_update(H5MantEstadoDelCAsoSupport.currentRow, $modal);
                }
                else {                    
                    H5MantEstadoDelCAsoSupport.TabUnderwritingCaseSType_Grid_insert(H5MantEstadoDelCAsoSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabUnderwritingCaseSType_GridShowModal = function (md, title, row) {
        row = row || { UnderwritingCaseStatus: 0, CreatorUserCode: 0, CreationDate: null, UpdateUserCode: 0, UpdateDate: null, Description: null, ShortDescription: null, RecordStatus: 0 };

        md.data('id', row.UnderwritingCaseStatus);
        md.find('.modal-title').text(title);

        H5MantEstadoDelCAsoSupport.TabUnderwritingCaseSType_GridRowToInput(row);
        $('#UnderwritingCaseStatus').prop('disabled', (row.UnderwritingCaseStatus !== 0));
        $('#CreatorUserCode').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        H5MantEstadoDelCAsoSupport.TabUnderwritingCaseSType_Grid_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.TabUnderwritingCaseSType_GridRowToInput = function (row) {
        H5MantEstadoDelCAsoSupport.currentRow = row;
        AutoNumeric.set('#UnderwritingCaseStatus', row.UnderwritingCaseStatus);
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#Description').val(row.Description);
        $('#ShortDescription').val(row.ShortDescription);
        H5MantEstadoDelCAsoSupport.LookUpForRecordStatus(row.RecordStatus, '');
        $('#RecordStatus').trigger('change');

    };
    this.TabUnderwritingCaseSType_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantEstadoDelCAsoActions.aspx/TabUnderwritingCaseSType_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                TransUnderwritingCaseSTypeLanguageId1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#TabUnderwritingCaseSType_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.TabUnderwritingCaseSTypeTranslator_GridTblSetup = function (table) {
        H5MantEstadoDelCAsoSupport.LookUpForLanguageIdTranslator('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'UnderwritingCaseStatus',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabUnderwritingCaseSTypeTranslator_Gridtoolbar',
            columns: [{
                field: 'UnderwritingCaseStatus',
                title: 'Estado',
                formatter: 'H5MantEstadoDelCAsoSupport.UnderwritingCaseStatusTranslator_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LanguageId',
                title: 'Idioma',
                formatter: 'H5MantEstadoDelCAsoSupport.LookUpForLanguageIdTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Description',
                title: 'Descripción',
                events: 'TabUnderwritingCaseSTypeTranslator_GridActionEvents',
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


        $('#TabUnderwritingCaseSTypeTranslator_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabUnderwritingCaseSTypeTranslator_GridTbl');
            $('#TabUnderwritingCaseSTypeTranslator_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabUnderwritingCaseSTypeTranslator_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabUnderwritingCaseSTypeTranslator_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantEstadoDelCAsoSupport.TabUnderwritingCaseSTypeTranslator_GridRowToInput(row);
                
                
                return row.UnderwritingCaseStatus;
            });
            
          $('#TabUnderwritingCaseSTypeTranslator_GridTbl').bootstrapTable('remove', {
                field: 'UnderwritingCaseStatus',
                values: ids
           });

            $('#TabUnderwritingCaseSTypeTranslator_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabUnderwritingCaseSTypeTranslator_GridCreateBtn').click(function () {
            var formInstance = $("#TabUnderwritingCaseSTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantEstadoDelCAsoSupport.TabUnderwritingCaseSTypeTranslator_GridShowModal($('#TabUnderwritingCaseSTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabUnderwritingCaseSTypeTranslator_GridPopup').find('#TabUnderwritingCaseSTypeTranslator_GridSaveBtn').click(function () {
            var formInstance = $("#TabUnderwritingCaseSTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabUnderwritingCaseSTypeTranslator_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabUnderwritingCaseSTypeTranslator_GridSaveBtn').html();
                $('#TabUnderwritingCaseSTypeTranslator_GridSaveBtn').html('Procesando...');
                $('#TabUnderwritingCaseSTypeTranslator_GridSaveBtn').prop('disabled', true);

                H5MantEstadoDelCAsoSupport.currentRow.UnderwritingCaseStatus = generalSupport.NumericValue('#UnderwritingCaseStatusTranslator', -99999, 99999);
                H5MantEstadoDelCAsoSupport.currentRow.LanguageId = parseInt(0 + $('#LanguageIdTranslator').val(), 10);
                H5MantEstadoDelCAsoSupport.currentRow.Description = $('#DescriptionTranslator').val();
                H5MantEstadoDelCAsoSupport.currentRow.ShortDescription = $('#ShortDescriptionTranslator').val();

                $('#TabUnderwritingCaseSTypeTranslator_GridSaveBtn').prop('disabled', false);
                $('#TabUnderwritingCaseSTypeTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantEstadoDelCAsoSupport.TabUnderwritingCaseSTypeTranslator_Grid_update(H5MantEstadoDelCAsoSupport.currentRow, $modal);
                }
                else {                    
                    $('#TabUnderwritingCaseSTypeTranslator_GridTbl').bootstrapTable('append', H5MantEstadoDelCAsoSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabUnderwritingCaseSTypeTranslator_GridShowModal = function (md, title, row) {
        row = row || { UnderwritingCaseStatus: 0, LanguageId: 0, Description: null, ShortDescription: null };

        md.data('id', row.UnderwritingCaseStatus);
        md.find('.modal-title').text(title);

        H5MantEstadoDelCAsoSupport.TabUnderwritingCaseSTypeTranslator_GridRowToInput(row);
        $('#UnderwritingCaseStatusTranslator').prop('disabled', true);
        $('#LanguageIdTranslator').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabUnderwritingCaseSTypeTranslator_GridRowToInput = function (row) {
        H5MantEstadoDelCAsoSupport.currentRow = row;
        AutoNumeric.set('#UnderwritingCaseStatusTranslator', row.UnderwritingCaseStatus);
        H5MantEstadoDelCAsoSupport.LookUpForLanguageIdTranslator(row.LanguageId, '');
        $('#LanguageIdTranslator').trigger('change');
        $('#DescriptionTranslator').val(row.Description);
        $('#ShortDescriptionTranslator').val(row.ShortDescription);

    };
    this.TabUnderwritingCaseSTypeTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantEstadoDelCAsoActions.aspx/TabUnderwritingCaseSTypeTranslator_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#TabUnderwritingCaseSTypeTranslator_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

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
    this.UnderwritingCaseStatusTranslator_FormatterMaskData = function (value, row, index) {          
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
        
        H5MantEstadoDelCAsoSupport.ValidateSetup();
        tableHelperSupport.Translate('#TabUnderwritingCaseSType_GridTbl', '#TabUnderwritingCaseSType_GridTbl');
tableHelperSupport.Translate('#TabUnderwritingCaseSTypeTranslator_GridTbl', '#TabUnderwritingCaseSTypeTranslator_GridTbl');

    });
        

    H5MantEstadoDelCAsoSupport.ControlBehaviour();
    H5MantEstadoDelCAsoSupport.ControlActions();
    

    $("#TabUnderwritingCaseSType_GridTblPlaceHolder").replaceWith('<table id="TabUnderwritingCaseSType_GridTbl"></table>');
    H5MantEstadoDelCAsoSupport.TabUnderwritingCaseSType_GridTblSetup($('#TabUnderwritingCaseSType_GridTbl'));
    $("#TabUnderwritingCaseSTypeTranslator_GridTblPlaceHolder").replaceWith('<table id="TabUnderwritingCaseSTypeTranslator_GridTbl"></table>');
    H5MantEstadoDelCAsoSupport.TabUnderwritingCaseSTypeTranslator_GridTblSetup($('#TabUnderwritingCaseSTypeTranslator_GridTbl'));

        H5MantEstadoDelCAsoSupport.TabUnderwritingCaseSType_GridTblRequest();
        H5MantEstadoDelCAsoSupport.TabUnderwritingCaseSTypeTranslator_GridTblRequest();




  };
};

$(document).ready(function () {
   H5MantEstadoDelCAsoSupport.Init();
});

window.TabUnderwritingCaseSType_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantEstadoDelCAsoSupport.TabUnderwritingCaseSType_GridShowModal($('#TabUnderwritingCaseSType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.TabUnderwritingCaseSTypeTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantEstadoDelCAsoSupport.TabUnderwritingCaseSTypeTranslator_GridShowModal($('#TabUnderwritingCaseSTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
