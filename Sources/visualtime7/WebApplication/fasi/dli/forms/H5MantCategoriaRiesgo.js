var H5MantCategoriaRiesgoSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5MantCategoriaRiesgoFormId').val(),
            TabRiskClassType_Grid_TabRiskClassType_Item: generalSupport.NormalizeProperties($('#TabRiskClassType_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate'),
            TabRiskClassTypeTranslator_Grid_TabRiskClassType_Item: generalSupport.NormalizeProperties($('#TabRiskClassTypeTranslator_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#H5MantCategoriaRiesgoFormId').val(data.InstanceFormId);

        H5MantCategoriaRiesgoSupport.LookUpForRecordStatus(source);
        H5MantCategoriaRiesgoSupport.LookUpForLanguageIdTranslator(source);

        H5MantCategoriaRiesgoSupport.TabRiskClassType_GridTblRequest();
        if (data.TabRiskClassType_Grid_TabRiskClassType_Item !== null)
            $('#TabRiskClassType_GridTbl').bootstrapTable('load', data.TabRiskClassType_Grid_TabRiskClassType_Item);
        H5MantCategoriaRiesgoSupport.TabRiskClassTypeTranslator_GridTblRequest();
        if (data.TabRiskClassTypeTranslator_Grid_TabRiskClassType_Item !== null)
            $('#TabRiskClassTypeTranslator_GridTbl').bootstrapTable('load', data.TabRiskClassTypeTranslator_Grid_TabRiskClassType_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#RiskClass', {
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
      new AutoNumeric('#RiskClassTranslator', {
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
					         H5MantCategoriaRiesgoSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5MantCategoriaRiesgoSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.TabRiskClassType_Grid_insert = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantCategoriaRiesgoActions.aspx/TabRiskClassType_Grid1InsertCommandActionTabRiskClassType", false,
               JSON.stringify({ RISKCLASS1: row.RiskClass, RECORDSTATUS2: row.RecordStatus, CREATORUSERCODE2: generalSupport.UserContext().userId, UPDATEUSERCODE4: generalSupport.UserContext().userId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantCategoriaRiesgoActions.aspx/TabRiskClassType_Grid3InsertCommandActionTransRiskClassType", false,
               JSON.stringify({ RISKCLASS1: row.RiskClass, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabRiskClassType_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message7 = 'Se agregó correctamente el registro';
            notification.toastr.success('Agregar registro', message7);
                    }                    
                    else {
            var message8 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message8);

                        }

    };
    this.TabRiskClassType_Grid_update = function (row, $modal) {
          var data;
            var recordCount;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantCategoriaRiesgoActions.aspx/TabRiskClassType_Grid1UpdateCommandActionTabRiskClassType", false,
               JSON.stringify({ RECORDSTATUS1: row.RecordStatus, UPDATEUSERCODE1: generalSupport.UserContext().userId, TabRiskClassTypeRiskClass3: row.RiskClass }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantCategoriaRiesgoActions.aspx/TabRiskClassType_Grid3SelectCommandActionTransRiskClassType", false,
               JSON.stringify({                 TransRiskClassTypeRiskClass1: row.RiskClass,
                TransRiskClassTypeLanguageId2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantCategoriaRiesgoActions.aspx/TabRiskClassType_Grid5InsertCommandActionTransRiskClassType", false,
               JSON.stringify({ RISKCLASS1: row.RiskClass, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantCategoriaRiesgoActions.aspx/TabRiskClassType_Grid6UpdateCommandActionTransRiskClassType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransRiskClassTypeRiskClass4: row.RiskClass, TransRiskClassTypeLanguageId5: generalSupport.SessionContext().languageId }));
               


                        }

                    }
        if (data.d.Success === true){
            $('#TabRiskClassType_GridTbl').bootstrapTable('updateByUniqueId', { id: row.RiskClass, row: row });
            $modal.modal('hide');
            var message9 = 'Se actualizó correctamente el registro';
            notification.toastr.success('Actualizar registro', message9);
                        }                        
                        else {
            var message10 = 'No se pudo actualizar el registro';
            notification.swal.error('Actualizar registro', message10);

                            }

    };
    this.TabRiskClassType_Grid_delete = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantCategoriaRiesgoActions.aspx/TabRiskClassType_Grid1DeleteCommandActionTransRiskClassType", false,
               JSON.stringify({ TransRiskClassTypeRiskClass1: row.RiskClass }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantCategoriaRiesgoActions.aspx/TabRiskClassType_Grid3DeleteCommandActionTabRiskClassType", false,
               JSON.stringify({ TabRiskClassTypeRiskClass1: row.RiskClass }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabRiskClassType_GridTbl').bootstrapTable('remove', {field: 'RiskClass', values: [generalSupport.NumericValue('#RiskClass', -99999, 99999)]});
            var message7 = 'Se eliminó correctamente el registro';
            notification.toastr.success('Eliminar registro', message7);
                    }                    
                    else {
            var message8 = 'No se puede eliminar el registro';
            notification.toastr.error('Eliminar registro', message8);

                        }

    };
    this.TabRiskClassType_Grid_BeforeShowPopup = function (row, $modal) {
          var data;
            var nextId;
        if (row.RiskClass === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantCategoriaRiesgoActions.aspx/TabRiskClassType_Grid2SelectCommandActionTabRiskClassType", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#RiskClass', nextId);

            }

    };
    this.TabRiskClassTypeTranslator_Grid_update = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantCategoriaRiesgoActions.aspx/TabRiskClassTypeTranslator_Grid1UpdateCommandActionTransRiskClassType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransRiskClassTypeRiskClass4: row.RiskClass, TransRiskClassTypeLanguageId5: row.LanguageId }));
               

        if (data.d.Success === true){
            $('#TabRiskClassTypeTranslator_GridTbl').bootstrapTable('updateByUniqueId', { id: row.RiskClass, row: row });
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
            var formInstance = $("#H5MantCategoriaRiesgoMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                H5MantCategoriaRiesgoSupport.TabRiskClassType_GridTblRequest();
                $('#TabRiskClassType_GridContainer').toggleClass('hidden', false);
                $('#TabRiskClassTypeTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#H5MantCategoriaRiesgoMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                H5MantCategoriaRiesgoSupport.TabRiskClassTypeTranslator_GridTblRequest();
                $('#TabRiskClassTypeTranslator_GridContainer').toggleClass('hidden', false);
                $('#TabRiskClassType_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5MantCategoriaRiesgoMainForm").validate({
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
        $("#TabRiskClassType_GridEditForm").validate({
            rules: {
                RiskClass: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999,
                    required: true
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
                }

            },
            messages: {
                RiskClass: {
                    AutoNumericMinValue: $.i18n.t('app.validation.RiskClass.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.RiskClass.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.RiskClass.required')
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
                }

            }
        });
        $("#TabRiskClassTypeTranslator_GridEditForm").validate({
            rules: {
                RiskClassTranslator: {
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
                RiskClassTranslator: {
                    AutoNumericMinValue: $.i18n.t('app.validation.RiskClassTranslator.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.RiskClassTranslator.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.RiskClassTranslator.required')
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantCategoriaRiesgoActions.aspx/LookUpForRecordStatus", false,
                JSON.stringify({ id: $('#H5MantCategoriaRiesgoFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantCategoriaRiesgoActions.aspx/LookUpForLanguageIdTranslator", false,
                JSON.stringify({ id: $('#H5MantCategoriaRiesgoFormId').val() }),
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

    this.TabRiskClassType_GridTblSetup = function (table) {
        H5MantCategoriaRiesgoSupport.LookUpForRecordStatus('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'RiskClass',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabRiskClassType_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'RiskClass',
                title: 'Clasificación de riesgo',
                events: 'TabRiskClassType_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
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
                formatter: 'H5MantCategoriaRiesgoSupport.LookUpForRecordStatusFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5MantCategoriaRiesgoSupport.CreatorUserCode_FormatterMaskData',
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
                formatter: 'H5MantCategoriaRiesgoSupport.UpdateUserCode_FormatterMaskData',
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
            }]
        });


        $('#TabRiskClassType_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabRiskClassType_GridTbl');
            $('#TabRiskClassType_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabRiskClassType_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabRiskClassType_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantCategoriaRiesgoSupport.TabRiskClassType_GridRowToInput(row);
                H5MantCategoriaRiesgoSupport.TabRiskClassType_Grid_delete(row, null);
                
                return row.RiskClass;
            });

            $('#TabRiskClassType_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabRiskClassType_GridCreateBtn').click(function () {
            var formInstance = $("#TabRiskClassType_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantCategoriaRiesgoSupport.TabRiskClassType_GridShowModal($('#TabRiskClassType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabRiskClassType_GridPopup').find('#TabRiskClassType_GridSaveBtn').click(function () {
            var formInstance = $("#TabRiskClassType_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabRiskClassType_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabRiskClassType_GridSaveBtn').html();
                $('#TabRiskClassType_GridSaveBtn').html('Procesando...');
                $('#TabRiskClassType_GridSaveBtn').prop('disabled', true);

                H5MantCategoriaRiesgoSupport.currentRow.RiskClass = generalSupport.NumericValue('#RiskClass', -99999, 99999);
                H5MantCategoriaRiesgoSupport.currentRow.Description = $('#Description').val();
                H5MantCategoriaRiesgoSupport.currentRow.ShortDescription = $('#ShortDescription').val();
                H5MantCategoriaRiesgoSupport.currentRow.RecordStatus = $('#RecordStatus').val();
                H5MantCategoriaRiesgoSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                H5MantCategoriaRiesgoSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                H5MantCategoriaRiesgoSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);
                H5MantCategoriaRiesgoSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';

                $('#TabRiskClassType_GridSaveBtn').prop('disabled', false);
                $('#TabRiskClassType_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantCategoriaRiesgoSupport.TabRiskClassType_Grid_update(H5MantCategoriaRiesgoSupport.currentRow, $modal);
                }
                else {                    
                    H5MantCategoriaRiesgoSupport.TabRiskClassType_Grid_insert(H5MantCategoriaRiesgoSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabRiskClassType_GridShowModal = function (md, title, row) {
        row = row || { RiskClass: 0, Description: null, ShortDescription: null, RecordStatus: 0, CreatorUserCode: 0, CreationDate: null, UpdateUserCode: 0, UpdateDate: null };

        md.data('id', row.RiskClass);
        md.find('.modal-title').text(title);

        H5MantCategoriaRiesgoSupport.TabRiskClassType_GridRowToInput(row);
        $('#RiskClass').prop('disabled', (row.RiskClass !== 0));
        $('#CreatorUserCode').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        H5MantCategoriaRiesgoSupport.TabRiskClassType_Grid_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.TabRiskClassType_GridRowToInput = function (row) {
        H5MantCategoriaRiesgoSupport.currentRow = row;
        AutoNumeric.set('#RiskClass', row.RiskClass);
        $('#Description').val(row.Description);
        $('#ShortDescription').val(row.ShortDescription);
        H5MantCategoriaRiesgoSupport.LookUpForRecordStatus(row.RecordStatus, '');
        $('#RecordStatus').trigger('change');
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));

    };
    this.TabRiskClassType_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantCategoriaRiesgoActions.aspx/TabRiskClassType_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                TransRiskClassTypeLanguageId1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#TabRiskClassType_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.TabRiskClassTypeTranslator_GridTblSetup = function (table) {
        H5MantCategoriaRiesgoSupport.LookUpForLanguageIdTranslator('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'RiskClass',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabRiskClassTypeTranslator_Gridtoolbar',
            columns: [{
                field: 'RiskClass',
                title: 'Clasificación de riesgo',
                formatter: 'H5MantCategoriaRiesgoSupport.RiskClassTranslator_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LanguageId',
                title: 'Idioma',
                formatter: 'H5MantCategoriaRiesgoSupport.LookUpForLanguageIdTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Description',
                title: 'Descripción',
                events: 'TabRiskClassTypeTranslator_GridActionEvents',
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


        $('#TabRiskClassTypeTranslator_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabRiskClassTypeTranslator_GridTbl');
            $('#TabRiskClassTypeTranslator_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabRiskClassTypeTranslator_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabRiskClassTypeTranslator_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantCategoriaRiesgoSupport.TabRiskClassTypeTranslator_GridRowToInput(row);
                
                
                return row.RiskClass;
            });
            
          $('#TabRiskClassTypeTranslator_GridTbl').bootstrapTable('remove', {
                field: 'RiskClass',
                values: ids
           });

            $('#TabRiskClassTypeTranslator_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabRiskClassTypeTranslator_GridCreateBtn').click(function () {
            var formInstance = $("#TabRiskClassTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantCategoriaRiesgoSupport.TabRiskClassTypeTranslator_GridShowModal($('#TabRiskClassTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabRiskClassTypeTranslator_GridPopup').find('#TabRiskClassTypeTranslator_GridSaveBtn').click(function () {
            var formInstance = $("#TabRiskClassTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabRiskClassTypeTranslator_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabRiskClassTypeTranslator_GridSaveBtn').html();
                $('#TabRiskClassTypeTranslator_GridSaveBtn').html('Procesando...');
                $('#TabRiskClassTypeTranslator_GridSaveBtn').prop('disabled', true);

                H5MantCategoriaRiesgoSupport.currentRow.RiskClass = generalSupport.NumericValue('#RiskClassTranslator', -99999, 99999);
                H5MantCategoriaRiesgoSupport.currentRow.LanguageId = parseInt(0 + $('#LanguageIdTranslator').val(), 10);
                H5MantCategoriaRiesgoSupport.currentRow.Description = $('#DescriptionTranslator').val();
                H5MantCategoriaRiesgoSupport.currentRow.ShortDescription = $('#ShortDescriptionTranslator').val();

                $('#TabRiskClassTypeTranslator_GridSaveBtn').prop('disabled', false);
                $('#TabRiskClassTypeTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantCategoriaRiesgoSupport.TabRiskClassTypeTranslator_Grid_update(H5MantCategoriaRiesgoSupport.currentRow, $modal);
                }
                else {                    
                    $('#TabRiskClassTypeTranslator_GridTbl').bootstrapTable('append', H5MantCategoriaRiesgoSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabRiskClassTypeTranslator_GridShowModal = function (md, title, row) {
        row = row || { RiskClass: 0, LanguageId: 0, Description: null, ShortDescription: null };

        md.data('id', row.RiskClass);
        md.find('.modal-title').text(title);

        H5MantCategoriaRiesgoSupport.TabRiskClassTypeTranslator_GridRowToInput(row);
        $('#RiskClassTranslator').prop('disabled', true);
        $('#LanguageIdTranslator').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabRiskClassTypeTranslator_GridRowToInput = function (row) {
        H5MantCategoriaRiesgoSupport.currentRow = row;
        AutoNumeric.set('#RiskClassTranslator', row.RiskClass);
        H5MantCategoriaRiesgoSupport.LookUpForLanguageIdTranslator(row.LanguageId, '');
        $('#LanguageIdTranslator').trigger('change');
        $('#DescriptionTranslator').val(row.Description);
        $('#ShortDescriptionTranslator').val(row.ShortDescription);

    };
    this.TabRiskClassTypeTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantCategoriaRiesgoActions.aspx/TabRiskClassTypeTranslator_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#TabRiskClassTypeTranslator_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.RiskClass_FormatterMaskData = function (value, row, index) {          
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
    this.RiskClassTranslator_FormatterMaskData = function (value, row, index) {          
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
        
        H5MantCategoriaRiesgoSupport.ValidateSetup();
        tableHelperSupport.Translate('#TabRiskClassType_GridTbl', '#TabRiskClassType_GridTbl');
tableHelperSupport.Translate('#TabRiskClassTypeTranslator_GridTbl', '#TabRiskClassTypeTranslator_GridTbl');

    });
        

    H5MantCategoriaRiesgoSupport.ControlBehaviour();
    H5MantCategoriaRiesgoSupport.ControlActions();
    

    $("#TabRiskClassType_GridTblPlaceHolder").replaceWith('<table id="TabRiskClassType_GridTbl"></table>');
    H5MantCategoriaRiesgoSupport.TabRiskClassType_GridTblSetup($('#TabRiskClassType_GridTbl'));
    $("#TabRiskClassTypeTranslator_GridTblPlaceHolder").replaceWith('<table id="TabRiskClassTypeTranslator_GridTbl"></table>');
    H5MantCategoriaRiesgoSupport.TabRiskClassTypeTranslator_GridTblSetup($('#TabRiskClassTypeTranslator_GridTbl'));

        H5MantCategoriaRiesgoSupport.TabRiskClassType_GridTblRequest();
        H5MantCategoriaRiesgoSupport.TabRiskClassTypeTranslator_GridTblRequest();




  };
};

$(document).ready(function () {
   H5MantCategoriaRiesgoSupport.Init();
});

window.TabRiskClassType_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantCategoriaRiesgoSupport.TabRiskClassType_GridShowModal($('#TabRiskClassType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.TabRiskClassTypeTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantCategoriaRiesgoSupport.TabRiskClassTypeTranslator_GridShowModal($('#TabRiskClassTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
