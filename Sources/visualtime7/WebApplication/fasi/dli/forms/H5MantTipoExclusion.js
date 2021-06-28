var H5MantTipoExclusionSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5MantTipoExclusionFormId').val(),
            TabExclusionType_Grid_TabExclusionType_Item: generalSupport.NormalizeProperties($('#TabExclusionType_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate'),
            TabExclusionTypeTranslator_Grid_TabExclusionType_Item: generalSupport.NormalizeProperties($('#TabExclusionTypeTranslator_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#H5MantTipoExclusionFormId').val(data.InstanceFormId);

        H5MantTipoExclusionSupport.LookUpForRecordStatus(source);
        H5MantTipoExclusionSupport.LookUpForLanguageIdTranslator(source);

        H5MantTipoExclusionSupport.TabExclusionType_GridTblRequest();
        if (data.TabExclusionType_Grid_TabExclusionType_Item !== null)
            $('#TabExclusionType_GridTbl').bootstrapTable('load', data.TabExclusionType_Grid_TabExclusionType_Item);
        H5MantTipoExclusionSupport.TabExclusionTypeTranslator_GridTblRequest();
        if (data.TabExclusionTypeTranslator_Grid_TabExclusionType_Item !== null)
            $('#TabExclusionTypeTranslator_GridTbl').bootstrapTable('load', data.TabExclusionTypeTranslator_Grid_TabExclusionType_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#ExclusionType', {
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
      new AutoNumeric('#ExclusionTypeTranslator', {
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
					         H5MantTipoExclusionSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5MantTipoExclusionSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.TabExclusionType_Grid_insert = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoExclusionActions.aspx/TabExclusionType_Grid1InsertCommandActionTabExclusionType", false,
               JSON.stringify({ EXCLUSIONTYPE1: row.ExclusionType, RECORDSTATUS2: row.RecordStatus, CREATORUSERCODE2: generalSupport.UserContext().userId, UPDATEUSERCODE4: generalSupport.UserContext().userId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoExclusionActions.aspx/TabExclusionType_Grid3InsertCommandActionTransExclusionType", false,
               JSON.stringify({ EXCLUSIONTYPE1: row.ExclusionType, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabExclusionType_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message7 = 'Se agregó correctamente el registro';
            notification.toastr.success('Agregar registro', message7);
                    }                    
                    else {
            var message8 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message8);

                        }

    };
    this.TabExclusionType_Grid_update = function (row, $modal) {
          var data;
            var recordCount;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoExclusionActions.aspx/TabExclusionType_Grid1UpdateCommandActionTabExclusionType", false,
               JSON.stringify({ RECORDSTATUS1: row.RecordStatus, UPDATEUSERCODE1: generalSupport.UserContext().userId, TabExclusionTypeExclusionType3: row.ExclusionType }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoExclusionActions.aspx/TabExclusionType_Grid3SelectCommandActionTransExclusionType", false,
               JSON.stringify({                 TransExclusionTypeExclusionType1: row.ExclusionType,
                TransExclusionTypeLanguageId2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoExclusionActions.aspx/TabExclusionType_Grid5InsertCommandActionTransExclusionType", false,
               JSON.stringify({ EXCLUSIONTYPE1: row.ExclusionType, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoExclusionActions.aspx/TabExclusionType_Grid6UpdateCommandActionTransExclusionType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransExclusionTypeExclusionType4: row.ExclusionType, TransExclusionTypeLanguageId5: generalSupport.SessionContext().languageId }));
               


                        }

                    }
        if (data.d.Success === true){
            $('#TabExclusionType_GridTbl').bootstrapTable('updateByUniqueId', { id: row.ExclusionType, row: row });
            $modal.modal('hide');
            var message9 = 'Se actualizó correctamente el registro';
            notification.toastr.success('Actualizar registro', message9);
                        }                        
                        else {
            var message10 = 'No se pudo actualizar el registro';
            notification.swal.error('Actualizar registro', message10);

                            }

    };
    this.TabExclusionType_Grid_delete = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoExclusionActions.aspx/TabExclusionType_Grid1DeleteCommandActionTransExclusionType", false,
               JSON.stringify({ TransExclusionTypeExclusionType1: row.ExclusionType }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoExclusionActions.aspx/TabExclusionType_Grid3DeleteCommandActionTabExclusionType", false,
               JSON.stringify({ TabExclusionTypeExclusionType1: row.ExclusionType }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabExclusionType_GridTbl').bootstrapTable('remove', {field: 'ExclusionType', values: [generalSupport.NumericValue('#ExclusionType', -99999, 99999)]});
            var message7 = 'Se eliminó correctamente el registro';
            notification.toastr.success('Eliminar registro', message7);
                    }                    
                    else {
            var message8 = 'No se puede eliminar el registro';
            notification.toastr.error('Eliminar registro', message8);

                        }

    };
    this.TabExclusionType_Grid_BeforeShowPopup = function (row, $modal) {
          var data;
            var nextId;
        if (row.ExclusionType === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoExclusionActions.aspx/TabExclusionType_Grid2SelectCommandActionTabExclusionType", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#ExclusionType', nextId);

            }

    };
    this.TabExclusionTypeTranslator_Grid_update = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoExclusionActions.aspx/TabExclusionTypeTranslator_Grid1UpdateCommandActionTransExclusionType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransExclusionTypeExclusionType4: row.ExclusionType, TransExclusionTypeLanguageId5: row.LanguageId }));
               

        if (data.d.Success === true){
            $('#TabExclusionTypeTranslator_GridTbl').bootstrapTable('updateByUniqueId', { id: row.ExclusionType, row: row });
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
            var formInstance = $("#H5MantTipoExclusionMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                H5MantTipoExclusionSupport.TabExclusionType_GridTblRequest();
                $('#TabExclusionType_GridContainer').toggleClass('hidden', false);
                $('#TabExclusionTypeTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#H5MantTipoExclusionMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                H5MantTipoExclusionSupport.TabExclusionTypeTranslator_GridTblRequest();
                $('#TabExclusionTypeTranslator_GridContainer').toggleClass('hidden', false);
                $('#TabExclusionType_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5MantTipoExclusionMainForm").validate({
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
        $("#TabExclusionType_GridEditForm").validate({
            rules: {
                ExclusionType: {
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
                ExclusionType: {
                    AutoNumericMinValue: $.i18n.t('app.validation.ExclusionType.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.ExclusionType.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.ExclusionType.required')
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
        $("#TabExclusionTypeTranslator_GridEditForm").validate({
            rules: {
                ExclusionTypeTranslator: {
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
                ExclusionTypeTranslator: {
                    AutoNumericMinValue: $.i18n.t('app.validation.ExclusionTypeTranslator.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.ExclusionTypeTranslator.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.ExclusionTypeTranslator.required')
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTipoExclusionActions.aspx/LookUpForRecordStatus", false,
                JSON.stringify({ id: $('#H5MantTipoExclusionFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTipoExclusionActions.aspx/LookUpForLanguageIdTranslator", false,
                JSON.stringify({ id: $('#H5MantTipoExclusionFormId').val() }),
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

    this.TabExclusionType_GridTblSetup = function (table) {
        H5MantTipoExclusionSupport.LookUpForRecordStatus('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'ExclusionType',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabExclusionType_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'ExclusionType',
                title: 'Tipo de exclusión',
                events: 'TabExclusionType_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'RecordStatus',
                title: 'Estado del registro',
                formatter: 'H5MantTipoExclusionSupport.LookUpForRecordStatusFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5MantTipoExclusionSupport.CreatorUserCode_FormatterMaskData',
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
                formatter: 'H5MantTipoExclusionSupport.UpdateUserCode_FormatterMaskData',
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


        $('#TabExclusionType_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabExclusionType_GridTbl');
            $('#TabExclusionType_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabExclusionType_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabExclusionType_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantTipoExclusionSupport.TabExclusionType_GridRowToInput(row);
                H5MantTipoExclusionSupport.TabExclusionType_Grid_delete(row, null);
                
                return row.ExclusionType;
            });

            $('#TabExclusionType_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabExclusionType_GridCreateBtn').click(function () {
            var formInstance = $("#TabExclusionType_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantTipoExclusionSupport.TabExclusionType_GridShowModal($('#TabExclusionType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabExclusionType_GridPopup').find('#TabExclusionType_GridSaveBtn').click(function () {
            var formInstance = $("#TabExclusionType_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabExclusionType_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabExclusionType_GridSaveBtn').html();
                $('#TabExclusionType_GridSaveBtn').html('Procesando...');
                $('#TabExclusionType_GridSaveBtn').prop('disabled', true);

                H5MantTipoExclusionSupport.currentRow.ExclusionType = generalSupport.NumericValue('#ExclusionType', -99999, 99999);
                H5MantTipoExclusionSupport.currentRow.RecordStatus = $('#RecordStatus').val();
                H5MantTipoExclusionSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                H5MantTipoExclusionSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                H5MantTipoExclusionSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);
                H5MantTipoExclusionSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';
                H5MantTipoExclusionSupport.currentRow.Description = $('#Description').val();
                H5MantTipoExclusionSupport.currentRow.ShortDescription = $('#ShortDescription').val();

                $('#TabExclusionType_GridSaveBtn').prop('disabled', false);
                $('#TabExclusionType_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantTipoExclusionSupport.TabExclusionType_Grid_update(H5MantTipoExclusionSupport.currentRow, $modal);
                }
                else {                    
                    H5MantTipoExclusionSupport.TabExclusionType_Grid_insert(H5MantTipoExclusionSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabExclusionType_GridShowModal = function (md, title, row) {
        row = row || { ExclusionType: 0, RecordStatus: 0, CreatorUserCode: 0, CreationDate: null, UpdateUserCode: 0, UpdateDate: null, Description: null, ShortDescription: null };

        md.data('id', row.ExclusionType);
        md.find('.modal-title').text(title);

        H5MantTipoExclusionSupport.TabExclusionType_GridRowToInput(row);
        $('#ExclusionType').prop('disabled', (row.ExclusionType !== 0));
        $('#CreatorUserCode').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        H5MantTipoExclusionSupport.TabExclusionType_Grid_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.TabExclusionType_GridRowToInput = function (row) {
        H5MantTipoExclusionSupport.currentRow = row;
        AutoNumeric.set('#ExclusionType', row.ExclusionType);
        H5MantTipoExclusionSupport.LookUpForRecordStatus(row.RecordStatus, '');
        $('#RecordStatus').trigger('change');
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#Description').val(row.Description);
        $('#ShortDescription').val(row.ShortDescription);

    };
    this.TabExclusionType_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTipoExclusionActions.aspx/TabExclusionType_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                TransExclusionTypeLanguageId1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#TabExclusionType_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.TabExclusionTypeTranslator_GridTblSetup = function (table) {
        H5MantTipoExclusionSupport.LookUpForLanguageIdTranslator('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'ExclusionType',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabExclusionTypeTranslator_Gridtoolbar',
            columns: [{
                field: 'ExclusionType',
                title: 'Tipo de exclusión',
                formatter: 'H5MantTipoExclusionSupport.ExclusionTypeTranslator_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LanguageId',
                title: 'Idioma',
                formatter: 'H5MantTipoExclusionSupport.LookUpForLanguageIdTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Description',
                title: 'Descripción',
                events: 'TabExclusionTypeTranslator_GridActionEvents',
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


        $('#TabExclusionTypeTranslator_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabExclusionTypeTranslator_GridTbl');
            $('#TabExclusionTypeTranslator_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabExclusionTypeTranslator_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabExclusionTypeTranslator_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantTipoExclusionSupport.TabExclusionTypeTranslator_GridRowToInput(row);
                
                
                return row.ExclusionType;
            });
            
          $('#TabExclusionTypeTranslator_GridTbl').bootstrapTable('remove', {
                field: 'ExclusionType',
                values: ids
           });

            $('#TabExclusionTypeTranslator_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabExclusionTypeTranslator_GridCreateBtn').click(function () {
            var formInstance = $("#TabExclusionTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantTipoExclusionSupport.TabExclusionTypeTranslator_GridShowModal($('#TabExclusionTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabExclusionTypeTranslator_GridPopup').find('#TabExclusionTypeTranslator_GridSaveBtn').click(function () {
            var formInstance = $("#TabExclusionTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabExclusionTypeTranslator_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabExclusionTypeTranslator_GridSaveBtn').html();
                $('#TabExclusionTypeTranslator_GridSaveBtn').html('Procesando...');
                $('#TabExclusionTypeTranslator_GridSaveBtn').prop('disabled', true);

                H5MantTipoExclusionSupport.currentRow.ExclusionType = generalSupport.NumericValue('#ExclusionTypeTranslator', -99999, 99999);
                H5MantTipoExclusionSupport.currentRow.LanguageId = parseInt(0 + $('#LanguageIdTranslator').val(), 10);
                H5MantTipoExclusionSupport.currentRow.Description = $('#DescriptionTranslator').val();
                H5MantTipoExclusionSupport.currentRow.ShortDescription = $('#ShortDescriptionTranslator').val();

                $('#TabExclusionTypeTranslator_GridSaveBtn').prop('disabled', false);
                $('#TabExclusionTypeTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantTipoExclusionSupport.TabExclusionTypeTranslator_Grid_update(H5MantTipoExclusionSupport.currentRow, $modal);
                }
                else {                    
                    $('#TabExclusionTypeTranslator_GridTbl').bootstrapTable('append', H5MantTipoExclusionSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabExclusionTypeTranslator_GridShowModal = function (md, title, row) {
        row = row || { ExclusionType: 0, LanguageId: 0, Description: null, ShortDescription: null };

        md.data('id', row.ExclusionType);
        md.find('.modal-title').text(title);

        H5MantTipoExclusionSupport.TabExclusionTypeTranslator_GridRowToInput(row);
        $('#ExclusionTypeTranslator').prop('disabled', true);
        $('#LanguageIdTranslator').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabExclusionTypeTranslator_GridRowToInput = function (row) {
        H5MantTipoExclusionSupport.currentRow = row;
        AutoNumeric.set('#ExclusionTypeTranslator', row.ExclusionType);
        H5MantTipoExclusionSupport.LookUpForLanguageIdTranslator(row.LanguageId, '');
        $('#LanguageIdTranslator').trigger('change');
        $('#DescriptionTranslator').val(row.Description);
        $('#ShortDescriptionTranslator').val(row.ShortDescription);

    };
    this.TabExclusionTypeTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTipoExclusionActions.aspx/TabExclusionTypeTranslator_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#TabExclusionTypeTranslator_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.ExclusionType_FormatterMaskData = function (value, row, index) {          
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
    this.ExclusionTypeTranslator_FormatterMaskData = function (value, row, index) {          
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
        
        H5MantTipoExclusionSupport.ValidateSetup();
        tableHelperSupport.Translate('#TabExclusionType_GridTbl', '#TabExclusionType_GridTbl');
tableHelperSupport.Translate('#TabExclusionTypeTranslator_GridTbl', '#TabExclusionTypeTranslator_GridTbl');

    });
        

    H5MantTipoExclusionSupport.ControlBehaviour();
    H5MantTipoExclusionSupport.ControlActions();
    

    $("#TabExclusionType_GridTblPlaceHolder").replaceWith('<table id="TabExclusionType_GridTbl"></table>');
    H5MantTipoExclusionSupport.TabExclusionType_GridTblSetup($('#TabExclusionType_GridTbl'));
    $("#TabExclusionTypeTranslator_GridTblPlaceHolder").replaceWith('<table id="TabExclusionTypeTranslator_GridTbl"></table>');
    H5MantTipoExclusionSupport.TabExclusionTypeTranslator_GridTblSetup($('#TabExclusionTypeTranslator_GridTbl'));

        H5MantTipoExclusionSupport.TabExclusionType_GridTblRequest();
        H5MantTipoExclusionSupport.TabExclusionTypeTranslator_GridTblRequest();




  };
};

$(document).ready(function () {
   H5MantTipoExclusionSupport.Init();
});

window.TabExclusionType_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantTipoExclusionSupport.TabExclusionType_GridShowModal($('#TabExclusionType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.TabExclusionTypeTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantTipoExclusionSupport.TabExclusionTypeTranslator_GridShowModal($('#TabExclusionTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
