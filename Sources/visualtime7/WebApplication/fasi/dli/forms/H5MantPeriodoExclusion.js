var H5MantPeriodoExclusionSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5MantPeriodoExclusionFormId').val(),
            TabExclusionPeriodType_Grid_TabExclusionPeriodType_Item: generalSupport.NormalizeProperties($('#TabExclusionPeriodType_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate'),
            TabExclusionPeriodTypeTranslator_Grid_TabExclusionPeriodType_Item: generalSupport.NormalizeProperties($('#TabExclusionPeriodTypeTranslator_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#H5MantPeriodoExclusionFormId').val(data.InstanceFormId);

        H5MantPeriodoExclusionSupport.LookUpForRecordStatus(source);
        H5MantPeriodoExclusionSupport.LookUpForLanguageIdTranslator(source);

        H5MantPeriodoExclusionSupport.TabExclusionPeriodType_GridTblRequest();
        if (data.TabExclusionPeriodType_Grid_TabExclusionPeriodType_Item !== null)
            $('#TabExclusionPeriodType_GridTbl').bootstrapTable('load', data.TabExclusionPeriodType_Grid_TabExclusionPeriodType_Item);
        H5MantPeriodoExclusionSupport.TabExclusionPeriodTypeTranslator_GridTblRequest();
        if (data.TabExclusionPeriodTypeTranslator_Grid_TabExclusionPeriodType_Item !== null)
            $('#TabExclusionPeriodTypeTranslator_GridTbl').bootstrapTable('load', data.TabExclusionPeriodTypeTranslator_Grid_TabExclusionPeriodType_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#ExclusionPeriodType', {
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
      new AutoNumeric('#ExclusionPeriodTypeTranslator', {
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
					         H5MantPeriodoExclusionSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5MantPeriodoExclusionSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.TabExclusionPeriodType_Grid_insert = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPeriodoExclusionActions.aspx/TabExclusionPeriodType_Grid1InsertCommandActionTabExclusionPeriodType", false,
               JSON.stringify({ EXCLUSIONPERIODTYPE1: row.ExclusionPeriodType, RECORDSTATUS2: row.RecordStatus, CREATORUSERCODE2: generalSupport.UserContext().userId, UPDATEUSERCODE4: generalSupport.UserContext().userId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPeriodoExclusionActions.aspx/TabExclusionPeriodType_Grid3InsertCommandActionTransExclusionPeriodType", false,
               JSON.stringify({ EXCLUSIONPERIODTYPE1: row.ExclusionPeriodType, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabExclusionPeriodType_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message7 = 'Se agregó correctamente el registro';
            notification.toastr.success('Agregar registro', message7);
                    }                    
                    else {
            var message8 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message8);

                        }

    };
    this.TabExclusionPeriodType_Grid_update = function (row, $modal) {
          var data;
            var recordCount;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPeriodoExclusionActions.aspx/TabExclusionPeriodType_Grid1UpdateCommandActionTabExclusionPeriodType", false,
               JSON.stringify({ RECORDSTATUS1: row.RecordStatus, UPDATEUSERCODE1: generalSupport.UserContext().userId, TabExclusionPeriodTypeExclusionPeriodType3: row.ExclusionPeriodType }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPeriodoExclusionActions.aspx/TabExclusionPeriodType_Grid3SelectCommandActionTransExclusionPeriodType", false,
               JSON.stringify({                 TransExclusionPeriodTypeExclusionPeriodType1: row.ExclusionPeriodType,
                TransExclusionPeriodTypeLanguageId2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPeriodoExclusionActions.aspx/TabExclusionPeriodType_Grid5InsertCommandActionTransExclusionPeriodType", false,
               JSON.stringify({ EXCLUSIONPERIODTYPE1: row.ExclusionPeriodType, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPeriodoExclusionActions.aspx/TabExclusionPeriodType_Grid6UpdateCommandActionTransExclusionPeriodType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransExclusionPeriodTypeExclusionPeriodType4: row.ExclusionPeriodType, TransExclusionPeriodTypeLanguageId5: generalSupport.SessionContext().languageId }));
               


                        }

                    }
        if (data.d.Success === true){
            $('#TabExclusionPeriodType_GridTbl').bootstrapTable('updateByUniqueId', { id: row.ExclusionPeriodType, row: row });
            $modal.modal('hide');
            var message9 = 'Se actualizó correctamente el registro';
            notification.toastr.success('Actualizar registro', message9);
                        }                        
                        else {
            var message10 = 'No se pudo actualizar el registro';
            notification.swal.error('Actualizar registro', message10);

                            }

    };
    this.TabExclusionPeriodType_Grid_delete = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPeriodoExclusionActions.aspx/TabExclusionPeriodType_Grid1DeleteCommandActionTransExclusionPeriodType", false,
               JSON.stringify({ TransExclusionPeriodTypeExclusionPeriodType1: row.ExclusionPeriodType }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPeriodoExclusionActions.aspx/TabExclusionPeriodType_Grid3DeleteCommandActionTabExclusionPeriodType", false,
               JSON.stringify({ TabExclusionPeriodTypeExclusionPeriodType1: row.ExclusionPeriodType }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabExclusionPeriodType_GridTbl').bootstrapTable('remove', {field: 'ExclusionPeriodType', values: [generalSupport.NumericValue('#ExclusionPeriodType', -99999, 99999)]});
            var message7 = 'Se eliminó correctamente el registro';
            notification.toastr.success('Eliminar registro', message7);
                    }                    
                    else {
            var message8 = 'No se puede eliminar el registro';
            notification.toastr.error('Eliminar registro', message8);

                        }

    };
    this.TabExclusionPeriodType_Grid_BeforeShowPopup = function (row, $modal) {
          var data;
            var nextId;
        if (row.ExclusionPeriodType === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPeriodoExclusionActions.aspx/TabExclusionPeriodType_Grid2SelectCommandActionTabExclusionPeriodType", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#ExclusionPeriodType', nextId);

            }

    };
    this.TabExclusionPeriodTypeTranslator_Grid_update = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPeriodoExclusionActions.aspx/TabExclusionPeriodTypeTranslator_Grid1UpdateCommandActionTransExclusionPeriodType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransExclusionPeriodTypeExclusionPeriodType4: row.ExclusionPeriodType, TransExclusionPeriodTypeLanguageId5: row.LanguageId }));
               

        if (data.d.Success === true){
            $('#TabExclusionPeriodTypeTranslator_GridTbl').bootstrapTable('updateByUniqueId', { id: row.ExclusionPeriodType, row: row });
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
            var formInstance = $("#H5MantPeriodoExclusionMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                H5MantPeriodoExclusionSupport.TabExclusionPeriodType_GridTblRequest();
                $('#TabExclusionPeriodType_GridContainer').toggleClass('hidden', false);
                $('#TabExclusionPeriodTypeTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#H5MantPeriodoExclusionMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                H5MantPeriodoExclusionSupport.TabExclusionPeriodTypeTranslator_GridTblRequest();
                $('#TabExclusionPeriodTypeTranslator_GridContainer').toggleClass('hidden', false);
                $('#TabExclusionPeriodType_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5MantPeriodoExclusionMainForm").validate({
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
        $("#TabExclusionPeriodType_GridEditForm").validate({
            rules: {
                ExclusionPeriodType: {
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
                ExclusionPeriodType: {
                    AutoNumericMinValue: $.i18n.t('app.validation.ExclusionPeriodType.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.ExclusionPeriodType.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.ExclusionPeriodType.required')
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
        $("#TabExclusionPeriodTypeTranslator_GridEditForm").validate({
            rules: {
                ExclusionPeriodTypeTranslator: {
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
                ExclusionPeriodTypeTranslator: {
                    AutoNumericMinValue: $.i18n.t('app.validation.ExclusionPeriodTypeTranslator.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.ExclusionPeriodTypeTranslator.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.ExclusionPeriodTypeTranslator.required')
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantPeriodoExclusionActions.aspx/LookUpForRecordStatus", false,
                JSON.stringify({ id: $('#H5MantPeriodoExclusionFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantPeriodoExclusionActions.aspx/LookUpForLanguageIdTranslator", false,
                JSON.stringify({ id: $('#H5MantPeriodoExclusionFormId').val() }),
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

    this.TabExclusionPeriodType_GridTblSetup = function (table) {
        H5MantPeriodoExclusionSupport.LookUpForRecordStatus('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'ExclusionPeriodType',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabExclusionPeriodType_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'ExclusionPeriodType',
                title: 'Período de exclusión',
                events: 'TabExclusionPeriodType_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'RecordStatus',
                title: 'Estado del registro',
                formatter: 'H5MantPeriodoExclusionSupport.LookUpForRecordStatusFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5MantPeriodoExclusionSupport.CreatorUserCode_FormatterMaskData',
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
                formatter: 'H5MantPeriodoExclusionSupport.UpdateUserCode_FormatterMaskData',
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


        $('#TabExclusionPeriodType_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabExclusionPeriodType_GridTbl');
            $('#TabExclusionPeriodType_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabExclusionPeriodType_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabExclusionPeriodType_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantPeriodoExclusionSupport.TabExclusionPeriodType_GridRowToInput(row);
                H5MantPeriodoExclusionSupport.TabExclusionPeriodType_Grid_delete(row, null);
                
                return row.ExclusionPeriodType;
            });

            $('#TabExclusionPeriodType_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabExclusionPeriodType_GridCreateBtn').click(function () {
            var formInstance = $("#TabExclusionPeriodType_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantPeriodoExclusionSupport.TabExclusionPeriodType_GridShowModal($('#TabExclusionPeriodType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabExclusionPeriodType_GridPopup').find('#TabExclusionPeriodType_GridSaveBtn').click(function () {
            var formInstance = $("#TabExclusionPeriodType_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabExclusionPeriodType_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabExclusionPeriodType_GridSaveBtn').html();
                $('#TabExclusionPeriodType_GridSaveBtn').html('Procesando...');
                $('#TabExclusionPeriodType_GridSaveBtn').prop('disabled', true);

                H5MantPeriodoExclusionSupport.currentRow.ExclusionPeriodType = generalSupport.NumericValue('#ExclusionPeriodType', -99999, 99999);
                H5MantPeriodoExclusionSupport.currentRow.RecordStatus = $('#RecordStatus').val();
                H5MantPeriodoExclusionSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                H5MantPeriodoExclusionSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                H5MantPeriodoExclusionSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);
                H5MantPeriodoExclusionSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';
                H5MantPeriodoExclusionSupport.currentRow.Description = $('#Description').val();
                H5MantPeriodoExclusionSupport.currentRow.ShortDescription = $('#ShortDescription').val();

                $('#TabExclusionPeriodType_GridSaveBtn').prop('disabled', false);
                $('#TabExclusionPeriodType_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantPeriodoExclusionSupport.TabExclusionPeriodType_Grid_update(H5MantPeriodoExclusionSupport.currentRow, $modal);
                }
                else {                    
                    H5MantPeriodoExclusionSupport.TabExclusionPeriodType_Grid_insert(H5MantPeriodoExclusionSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabExclusionPeriodType_GridShowModal = function (md, title, row) {
        row = row || { ExclusionPeriodType: 0, RecordStatus: 0, CreatorUserCode: 0, CreationDate: null, UpdateUserCode: 0, UpdateDate: null, Description: null, ShortDescription: null };

        md.data('id', row.ExclusionPeriodType);
        md.find('.modal-title').text(title);

        H5MantPeriodoExclusionSupport.TabExclusionPeriodType_GridRowToInput(row);
        $('#ExclusionPeriodType').prop('disabled', (row.ExclusionPeriodType !== 0));
        $('#CreatorUserCode').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        H5MantPeriodoExclusionSupport.TabExclusionPeriodType_Grid_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.TabExclusionPeriodType_GridRowToInput = function (row) {
        H5MantPeriodoExclusionSupport.currentRow = row;
        AutoNumeric.set('#ExclusionPeriodType', row.ExclusionPeriodType);
        H5MantPeriodoExclusionSupport.LookUpForRecordStatus(row.RecordStatus, '');
        $('#RecordStatus').trigger('change');
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#Description').val(row.Description);
        $('#ShortDescription').val(row.ShortDescription);

    };
    this.TabExclusionPeriodType_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantPeriodoExclusionActions.aspx/TabExclusionPeriodType_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                TransExclusionPeriodTypeLanguageId1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#TabExclusionPeriodType_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.TabExclusionPeriodTypeTranslator_GridTblSetup = function (table) {
        H5MantPeriodoExclusionSupport.LookUpForLanguageIdTranslator('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'ExclusionPeriodType',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabExclusionPeriodTypeTranslator_Gridtoolbar',
            columns: [{
                field: 'ExclusionPeriodType',
                title: 'Período de exclusión',
                formatter: 'H5MantPeriodoExclusionSupport.ExclusionPeriodTypeTranslator_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LanguageId',
                title: 'Idioma',
                formatter: 'H5MantPeriodoExclusionSupport.LookUpForLanguageIdTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Description',
                title: 'Descripción',
                events: 'TabExclusionPeriodTypeTranslator_GridActionEvents',
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


        $('#TabExclusionPeriodTypeTranslator_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabExclusionPeriodTypeTranslator_GridTbl');
            $('#TabExclusionPeriodTypeTranslator_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabExclusionPeriodTypeTranslator_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabExclusionPeriodTypeTranslator_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantPeriodoExclusionSupport.TabExclusionPeriodTypeTranslator_GridRowToInput(row);
                
                
                return row.ExclusionPeriodType;
            });
            
          $('#TabExclusionPeriodTypeTranslator_GridTbl').bootstrapTable('remove', {
                field: 'ExclusionPeriodType',
                values: ids
           });

            $('#TabExclusionPeriodTypeTranslator_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabExclusionPeriodTypeTranslator_GridCreateBtn').click(function () {
            var formInstance = $("#TabExclusionPeriodTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantPeriodoExclusionSupport.TabExclusionPeriodTypeTranslator_GridShowModal($('#TabExclusionPeriodTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabExclusionPeriodTypeTranslator_GridPopup').find('#TabExclusionPeriodTypeTranslator_GridSaveBtn').click(function () {
            var formInstance = $("#TabExclusionPeriodTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabExclusionPeriodTypeTranslator_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabExclusionPeriodTypeTranslator_GridSaveBtn').html();
                $('#TabExclusionPeriodTypeTranslator_GridSaveBtn').html('Procesando...');
                $('#TabExclusionPeriodTypeTranslator_GridSaveBtn').prop('disabled', true);

                H5MantPeriodoExclusionSupport.currentRow.ExclusionPeriodType = generalSupport.NumericValue('#ExclusionPeriodTypeTranslator', -99999, 99999);
                H5MantPeriodoExclusionSupport.currentRow.LanguageId = parseInt(0 + $('#LanguageIdTranslator').val(), 10);
                H5MantPeriodoExclusionSupport.currentRow.Description = $('#DescriptionTranslator').val();
                H5MantPeriodoExclusionSupport.currentRow.ShortDescription = $('#ShortDescriptionTranslator').val();

                $('#TabExclusionPeriodTypeTranslator_GridSaveBtn').prop('disabled', false);
                $('#TabExclusionPeriodTypeTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantPeriodoExclusionSupport.TabExclusionPeriodTypeTranslator_Grid_update(H5MantPeriodoExclusionSupport.currentRow, $modal);
                }
                else {                    
                    $('#TabExclusionPeriodTypeTranslator_GridTbl').bootstrapTable('append', H5MantPeriodoExclusionSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabExclusionPeriodTypeTranslator_GridShowModal = function (md, title, row) {
        row = row || { ExclusionPeriodType: 0, LanguageId: 0, Description: null, ShortDescription: null };

        md.data('id', row.ExclusionPeriodType);
        md.find('.modal-title').text(title);

        H5MantPeriodoExclusionSupport.TabExclusionPeriodTypeTranslator_GridRowToInput(row);
        $('#ExclusionPeriodTypeTranslator').prop('disabled', true);
        $('#LanguageIdTranslator').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabExclusionPeriodTypeTranslator_GridRowToInput = function (row) {
        H5MantPeriodoExclusionSupport.currentRow = row;
        AutoNumeric.set('#ExclusionPeriodTypeTranslator', row.ExclusionPeriodType);
        H5MantPeriodoExclusionSupport.LookUpForLanguageIdTranslator(row.LanguageId, '');
        $('#LanguageIdTranslator').trigger('change');
        $('#DescriptionTranslator').val(row.Description);
        $('#ShortDescriptionTranslator').val(row.ShortDescription);

    };
    this.TabExclusionPeriodTypeTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantPeriodoExclusionActions.aspx/TabExclusionPeriodTypeTranslator_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#TabExclusionPeriodTypeTranslator_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.ExclusionPeriodType_FormatterMaskData = function (value, row, index) {          
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
    this.ExclusionPeriodTypeTranslator_FormatterMaskData = function (value, row, index) {          
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
        
        H5MantPeriodoExclusionSupport.ValidateSetup();
        tableHelperSupport.Translate('#TabExclusionPeriodType_GridTbl', '#TabExclusionPeriodType_GridTbl');
tableHelperSupport.Translate('#TabExclusionPeriodTypeTranslator_GridTbl', '#TabExclusionPeriodTypeTranslator_GridTbl');

    });
        

    H5MantPeriodoExclusionSupport.ControlBehaviour();
    H5MantPeriodoExclusionSupport.ControlActions();
    

    $("#TabExclusionPeriodType_GridTblPlaceHolder").replaceWith('<table id="TabExclusionPeriodType_GridTbl"></table>');
    H5MantPeriodoExclusionSupport.TabExclusionPeriodType_GridTblSetup($('#TabExclusionPeriodType_GridTbl'));
    $("#TabExclusionPeriodTypeTranslator_GridTblPlaceHolder").replaceWith('<table id="TabExclusionPeriodTypeTranslator_GridTbl"></table>');
    H5MantPeriodoExclusionSupport.TabExclusionPeriodTypeTranslator_GridTblSetup($('#TabExclusionPeriodTypeTranslator_GridTbl'));

        H5MantPeriodoExclusionSupport.TabExclusionPeriodType_GridTblRequest();
        H5MantPeriodoExclusionSupport.TabExclusionPeriodTypeTranslator_GridTblRequest();




  };
};

$(document).ready(function () {
   H5MantPeriodoExclusionSupport.Init();
});

window.TabExclusionPeriodType_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantPeriodoExclusionSupport.TabExclusionPeriodType_GridShowModal($('#TabExclusionPeriodType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.TabExclusionPeriodTypeTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantPeriodoExclusionSupport.TabExclusionPeriodTypeTranslator_GridShowModal($('#TabExclusionPeriodTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
