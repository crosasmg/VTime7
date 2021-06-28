var H5MantRestriccionSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5MantRestriccionFormId').val(),
            TabRestrictionType_Grid_TabRestrictionType_Item: generalSupport.NormalizeProperties($('#TabRestrictionType_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate'),
            TabRestrictionTypeTranslator_Grid_TabRestrictionType_Item: generalSupport.NormalizeProperties($('#TabRestrictionTypeTranslator_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#H5MantRestriccionFormId').val(data.InstanceFormId);

        H5MantRestriccionSupport.LookUpForRecordStatus(source);
        H5MantRestriccionSupport.LookUpForLanguageIdTranslator(source);

        H5MantRestriccionSupport.TabRestrictionType_GridTblRequest();
        if (data.TabRestrictionType_Grid_TabRestrictionType_Item !== null)
            $('#TabRestrictionType_GridTbl').bootstrapTable('load', data.TabRestrictionType_Grid_TabRestrictionType_Item);
        H5MantRestriccionSupport.TabRestrictionTypeTranslator_GridTblRequest();
        if (data.TabRestrictionTypeTranslator_Grid_TabRestrictionType_Item !== null)
            $('#TabRestrictionTypeTranslator_GridTbl').bootstrapTable('load', data.TabRestrictionTypeTranslator_Grid_TabRestrictionType_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#RestrictionType', {
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
      new AutoNumeric('#RestrictionTypeTranslator', {
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
					         H5MantRestriccionSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5MantRestriccionSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.TabRestrictionType_Grid_insert = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantRestriccionActions.aspx/TabRestrictionType_Grid1InsertCommandActionTabRestrictionType", false,
               JSON.stringify({ RESTRICTIONTYPE1: row.RestrictionType, RECORDSTATUS2: row.RecordStatus, CREATORUSERCODE2: generalSupport.UserContext().userId, UPDATEUSERCODE4: generalSupport.UserContext().userId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantRestriccionActions.aspx/TabRestrictionType_Grid3InsertCommandActionTransRestrictionType", false,
               JSON.stringify({ RESTRICTIONTYPE1: row.RestrictionType, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabRestrictionType_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message7 = 'Se agregó correctamente el registro';
            notification.toastr.success('Agregar registro', message7);
                    }                    
                    else {
            var message8 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message8);

                        }

    };
    this.TabRestrictionType_Grid_update = function (row, $modal) {
          var data;
            var recordCount;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantRestriccionActions.aspx/TabRestrictionType_Grid1UpdateCommandActionTabRestrictionType", false,
               JSON.stringify({ RECORDSTATUS1: row.RecordStatus, UPDATEUSERCODE1: generalSupport.UserContext().userId, TabRestrictionTypeRestrictionType3: row.RestrictionType }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantRestriccionActions.aspx/TabRestrictionType_Grid3SelectCommandActionTransRestrictionType", false,
               JSON.stringify({                 TransRestrictionTypeRestrictionType1: row.RestrictionType,
                TransRestrictionTypeLanguageId2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantRestriccionActions.aspx/TabRestrictionType_Grid5InsertCommandActionTransRestrictionType", false,
               JSON.stringify({ RESTRICTIONTYPE1: row.RestrictionType, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantRestriccionActions.aspx/TabRestrictionType_Grid6UpdateCommandActionTransRestrictionType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransRestrictionTypeRestrictionType4: row.RestrictionType, TransRestrictionTypeLanguageId5: generalSupport.SessionContext().languageId }));
               


                        }

                    }
        if (data.d.Success === true){
            $('#TabRestrictionType_GridTbl').bootstrapTable('updateByUniqueId', { id: row.RestrictionType, row: row });
            $modal.modal('hide');
            var message9 = 'Se actualizó correctamente el registro';
            notification.toastr.success('Actualizar registro', message9);
                        }                        
                        else {
            var message10 = 'No se pudo actualizar el registro';
            notification.swal.error('Actualizar registro', message10);

                            }

    };
    this.TabRestrictionType_Grid_delete = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantRestriccionActions.aspx/TabRestrictionType_Grid1DeleteCommandActionTransRestrictionType", false,
               JSON.stringify({ TransRestrictionTypeRestrictionType1: row.RestrictionType }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantRestriccionActions.aspx/TabRestrictionType_Grid3DeleteCommandActionTabRestrictionType", false,
               JSON.stringify({ TabRestrictionTypeRestrictionType1: row.RestrictionType }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabRestrictionType_GridTbl').bootstrapTable('remove', {field: 'RestrictionType', values: [generalSupport.NumericValue('#RestrictionType', -99999, 99999)]});
            var message7 = 'Se eliminó correctamente el registro';
            notification.toastr.success('Eliminar registro', message7);
                    }                    
                    else {
            var message8 = 'No se puede eliminar el registro';
            notification.toastr.error('Eliminar registro', message8);

                        }

    };
    this.TabRestrictionType_Grid_BeforeShowPopup = function (row, $modal) {
          var data;
            var nextId;
        if (row.RestrictionType === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantRestriccionActions.aspx/TabRestrictionType_Grid2SelectCommandActionTabRestrictionType", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#RestrictionType', nextId);

            }

    };
    this.TabRestrictionTypeTranslator_Grid_update = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantRestriccionActions.aspx/TabRestrictionTypeTranslator_Grid1UpdateCommandActionTransRestrictionType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransRestrictionTypeRestrictionType4: row.RestrictionType, TransRestrictionTypeLanguageId5: row.LanguageId }));
               

        if (data.d.Success === true){
            $('#TabRestrictionTypeTranslator_GridTbl').bootstrapTable('updateByUniqueId', { id: row.RestrictionType, row: row });
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
            var formInstance = $("#H5MantRestriccionMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                H5MantRestriccionSupport.TabRestrictionType_GridTblRequest();
                $('#TabRestrictionType_GridContainer').toggleClass('hidden', false);
                $('#TabRestrictionTypeTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#H5MantRestriccionMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                H5MantRestriccionSupport.TabRestrictionTypeTranslator_GridTblRequest();
                $('#TabRestrictionTypeTranslator_GridContainer').toggleClass('hidden', false);
                $('#TabRestrictionType_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5MantRestriccionMainForm").validate({
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
        $("#TabRestrictionType_GridEditForm").validate({
            rules: {
                RestrictionType: {
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
                RestrictionType: {
                    AutoNumericMinValue: $.i18n.t('app.validation.RestrictionType.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.RestrictionType.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.RestrictionType.required')
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
        $("#TabRestrictionTypeTranslator_GridEditForm").validate({
            rules: {
                RestrictionTypeTranslator: {
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
                RestrictionTypeTranslator: {
                    AutoNumericMinValue: $.i18n.t('app.validation.RestrictionTypeTranslator.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.RestrictionTypeTranslator.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.RestrictionTypeTranslator.required')
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantRestriccionActions.aspx/LookUpForRecordStatus", false,
                JSON.stringify({ id: $('#H5MantRestriccionFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantRestriccionActions.aspx/LookUpForLanguageIdTranslator", false,
                JSON.stringify({ id: $('#H5MantRestriccionFormId').val() }),
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

    this.TabRestrictionType_GridTblSetup = function (table) {
        H5MantRestriccionSupport.LookUpForRecordStatus('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'RestrictionType',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabRestrictionType_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'RestrictionType',
                title: 'Tipo de restricción',
                events: 'TabRestrictionType_GridActionEvents',
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
                formatter: 'H5MantRestriccionSupport.LookUpForRecordStatusFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5MantRestriccionSupport.CreatorUserCode_FormatterMaskData',
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
                formatter: 'H5MantRestriccionSupport.UpdateUserCode_FormatterMaskData',
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


        $('#TabRestrictionType_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabRestrictionType_GridTbl');
            $('#TabRestrictionType_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabRestrictionType_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabRestrictionType_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantRestriccionSupport.TabRestrictionType_GridRowToInput(row);
                H5MantRestriccionSupport.TabRestrictionType_Grid_delete(row, null);
                
                return row.RestrictionType;
            });

            $('#TabRestrictionType_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabRestrictionType_GridCreateBtn').click(function () {
            var formInstance = $("#TabRestrictionType_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantRestriccionSupport.TabRestrictionType_GridShowModal($('#TabRestrictionType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabRestrictionType_GridPopup').find('#TabRestrictionType_GridSaveBtn').click(function () {
            var formInstance = $("#TabRestrictionType_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabRestrictionType_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabRestrictionType_GridSaveBtn').html();
                $('#TabRestrictionType_GridSaveBtn').html('Procesando...');
                $('#TabRestrictionType_GridSaveBtn').prop('disabled', true);

                H5MantRestriccionSupport.currentRow.RestrictionType = generalSupport.NumericValue('#RestrictionType', -99999, 99999);
                H5MantRestriccionSupport.currentRow.Description = $('#Description').val();
                H5MantRestriccionSupport.currentRow.ShortDescription = $('#ShortDescription').val();
                H5MantRestriccionSupport.currentRow.RecordStatus = $('#RecordStatus').val();
                H5MantRestriccionSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                H5MantRestriccionSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                H5MantRestriccionSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);
                H5MantRestriccionSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';

                $('#TabRestrictionType_GridSaveBtn').prop('disabled', false);
                $('#TabRestrictionType_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantRestriccionSupport.TabRestrictionType_Grid_update(H5MantRestriccionSupport.currentRow, $modal);
                }
                else {                    
                    H5MantRestriccionSupport.TabRestrictionType_Grid_insert(H5MantRestriccionSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabRestrictionType_GridShowModal = function (md, title, row) {
        row = row || { RestrictionType: 0, Description: null, ShortDescription: null, RecordStatus: 0, CreatorUserCode: 0, CreationDate: null, UpdateUserCode: 0, UpdateDate: null };

        md.data('id', row.RestrictionType);
        md.find('.modal-title').text(title);

        H5MantRestriccionSupport.TabRestrictionType_GridRowToInput(row);
        $('#RestrictionType').prop('disabled', (row.RestrictionType !== 0));
        $('#CreatorUserCode').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        H5MantRestriccionSupport.TabRestrictionType_Grid_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.TabRestrictionType_GridRowToInput = function (row) {
        H5MantRestriccionSupport.currentRow = row;
        AutoNumeric.set('#RestrictionType', row.RestrictionType);
        $('#Description').val(row.Description);
        $('#ShortDescription').val(row.ShortDescription);
        H5MantRestriccionSupport.LookUpForRecordStatus(row.RecordStatus, '');
        $('#RecordStatus').trigger('change');
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));

    };
    this.TabRestrictionType_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantRestriccionActions.aspx/TabRestrictionType_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                TransRestrictionTypeLanguageId1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#TabRestrictionType_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.TabRestrictionTypeTranslator_GridTblSetup = function (table) {
        H5MantRestriccionSupport.LookUpForLanguageIdTranslator('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'RestrictionType',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabRestrictionTypeTranslator_Gridtoolbar',
            columns: [{
                field: 'RestrictionType',
                title: 'Tipo de restricción',
                formatter: 'H5MantRestriccionSupport.RestrictionTypeTranslator_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LanguageId',
                title: 'Idioma',
                formatter: 'H5MantRestriccionSupport.LookUpForLanguageIdTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Description',
                title: 'Descripción',
                events: 'TabRestrictionTypeTranslator_GridActionEvents',
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


        $('#TabRestrictionTypeTranslator_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabRestrictionTypeTranslator_GridTbl');
            $('#TabRestrictionTypeTranslator_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabRestrictionTypeTranslator_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabRestrictionTypeTranslator_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantRestriccionSupport.TabRestrictionTypeTranslator_GridRowToInput(row);
                
                
                return row.RestrictionType;
            });
            
          $('#TabRestrictionTypeTranslator_GridTbl').bootstrapTable('remove', {
                field: 'RestrictionType',
                values: ids
           });

            $('#TabRestrictionTypeTranslator_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabRestrictionTypeTranslator_GridCreateBtn').click(function () {
            var formInstance = $("#TabRestrictionTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantRestriccionSupport.TabRestrictionTypeTranslator_GridShowModal($('#TabRestrictionTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabRestrictionTypeTranslator_GridPopup').find('#TabRestrictionTypeTranslator_GridSaveBtn').click(function () {
            var formInstance = $("#TabRestrictionTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabRestrictionTypeTranslator_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabRestrictionTypeTranslator_GridSaveBtn').html();
                $('#TabRestrictionTypeTranslator_GridSaveBtn').html('Procesando...');
                $('#TabRestrictionTypeTranslator_GridSaveBtn').prop('disabled', true);

                H5MantRestriccionSupport.currentRow.RestrictionType = generalSupport.NumericValue('#RestrictionTypeTranslator', -99999, 99999);
                H5MantRestriccionSupport.currentRow.LanguageId = parseInt(0 + $('#LanguageIdTranslator').val(), 10);
                H5MantRestriccionSupport.currentRow.Description = $('#DescriptionTranslator').val();
                H5MantRestriccionSupport.currentRow.ShortDescription = $('#ShortDescriptionTranslator').val();

                $('#TabRestrictionTypeTranslator_GridSaveBtn').prop('disabled', false);
                $('#TabRestrictionTypeTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantRestriccionSupport.TabRestrictionTypeTranslator_Grid_update(H5MantRestriccionSupport.currentRow, $modal);
                }
                else {                    
                    $('#TabRestrictionTypeTranslator_GridTbl').bootstrapTable('append', H5MantRestriccionSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabRestrictionTypeTranslator_GridShowModal = function (md, title, row) {
        row = row || { RestrictionType: 0, LanguageId: 0, Description: null, ShortDescription: null };

        md.data('id', row.RestrictionType);
        md.find('.modal-title').text(title);

        H5MantRestriccionSupport.TabRestrictionTypeTranslator_GridRowToInput(row);
        $('#RestrictionTypeTranslator').prop('disabled', true);
        $('#LanguageIdTranslator').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabRestrictionTypeTranslator_GridRowToInput = function (row) {
        H5MantRestriccionSupport.currentRow = row;
        AutoNumeric.set('#RestrictionTypeTranslator', row.RestrictionType);
        H5MantRestriccionSupport.LookUpForLanguageIdTranslator(row.LanguageId, '');
        $('#LanguageIdTranslator').trigger('change');
        $('#DescriptionTranslator').val(row.Description);
        $('#ShortDescriptionTranslator').val(row.ShortDescription);

    };
    this.TabRestrictionTypeTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantRestriccionActions.aspx/TabRestrictionTypeTranslator_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#TabRestrictionTypeTranslator_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.RestrictionType_FormatterMaskData = function (value, row, index) {          
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
    this.RestrictionTypeTranslator_FormatterMaskData = function (value, row, index) {          
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
        
        H5MantRestriccionSupport.ValidateSetup();
        tableHelperSupport.Translate('#TabRestrictionType_GridTbl', '#TabRestrictionType_GridTbl');
tableHelperSupport.Translate('#TabRestrictionTypeTranslator_GridTbl', '#TabRestrictionTypeTranslator_GridTbl');

    });
        

    H5MantRestriccionSupport.ControlBehaviour();
    H5MantRestriccionSupport.ControlActions();
    

    $("#TabRestrictionType_GridTblPlaceHolder").replaceWith('<table id="TabRestrictionType_GridTbl"></table>');
    H5MantRestriccionSupport.TabRestrictionType_GridTblSetup($('#TabRestrictionType_GridTbl'));
    $("#TabRestrictionTypeTranslator_GridTblPlaceHolder").replaceWith('<table id="TabRestrictionTypeTranslator_GridTbl"></table>');
    H5MantRestriccionSupport.TabRestrictionTypeTranslator_GridTblSetup($('#TabRestrictionTypeTranslator_GridTbl'));

        H5MantRestriccionSupport.TabRestrictionType_GridTblRequest();
        H5MantRestriccionSupport.TabRestrictionTypeTranslator_GridTblRequest();




  };
};

$(document).ready(function () {
   H5MantRestriccionSupport.Init();
});

window.TabRestrictionType_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantRestriccionSupport.TabRestrictionType_GridShowModal($('#TabRestrictionType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.TabRestrictionTypeTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantRestriccionSupport.TabRestrictionTypeTranslator_GridShowModal($('#TabRestrictionTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
