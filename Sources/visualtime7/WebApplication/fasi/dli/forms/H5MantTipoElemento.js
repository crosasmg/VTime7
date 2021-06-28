var H5MantTipoElementoSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5MantTipoElementoFormId').val(),
            TabDiscountOrExtraPremium_Grid_TabDiscountOrExtraPremium_Item: generalSupport.NormalizeProperties($('#TabDiscountOrExtraPremium_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate'),
            TabDiscountOrExtraPremiumTranslator_Grid_TabDiscountOrExtraPremium_Item: generalSupport.NormalizeProperties($('#TabDiscountOrExtraPremiumTranslator_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#H5MantTipoElementoFormId').val(data.InstanceFormId);

        H5MantTipoElementoSupport.LookUpForRecordStatus(source);
        H5MantTipoElementoSupport.LookUpForLanguageIdTranslator(source);

        H5MantTipoElementoSupport.TabDiscountOrExtraPremium_GridTblRequest();
        if (data.TabDiscountOrExtraPremium_Grid_TabDiscountOrExtraPremium_Item !== null)
            $('#TabDiscountOrExtraPremium_GridTbl').bootstrapTable('load', data.TabDiscountOrExtraPremium_Grid_TabDiscountOrExtraPremium_Item);
        H5MantTipoElementoSupport.TabDiscountOrExtraPremiumTranslator_GridTblRequest();
        if (data.TabDiscountOrExtraPremiumTranslator_Grid_TabDiscountOrExtraPremium_Item !== null)
            $('#TabDiscountOrExtraPremiumTranslator_GridTbl').bootstrapTable('load', data.TabDiscountOrExtraPremiumTranslator_Grid_TabDiscountOrExtraPremium_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#DiscountOrExtraPremiumType', {
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
      new AutoNumeric('#DiscountOrExtraPremiumTypeTranslator', {
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
					         H5MantTipoElementoSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5MantTipoElementoSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.TabDiscountOrExtraPremium_Grid_insert = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoElementoActions.aspx/TabDiscountOrExtraPremium_Grid1InsertCommandActionTabDiscountOrExtraPremium", false,
               JSON.stringify({ DISCOUNTOREXTRAPREMIUMTYPE1: row.DiscountOrExtraPremiumType, RECORDSTATUS2: row.RecordStatus, CREATORUSERCODE2: generalSupport.UserContext().userId, UPDATEUSERCODE4: generalSupport.UserContext().userId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoElementoActions.aspx/TabDiscountOrExtraPremium_Grid3InsertCommandActionTransDiscountOrExtraPremium", false,
               JSON.stringify({ DISCOUNTOREXTRAPREMIUMTYPE1: row.DiscountOrExtraPremiumType, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabDiscountOrExtraPremium_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message7 = 'Se agregó correctamente el registro';
            notification.toastr.success('Agregar registro', message7);
                    }                    
                    else {
            var message8 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message8);

                        }

    };
    this.TabDiscountOrExtraPremium_Grid_update = function (row, $modal) {
          var data;
            var recordCount;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoElementoActions.aspx/TabDiscountOrExtraPremium_Grid1UpdateCommandActionTabDiscountOrExtraPremium", false,
               JSON.stringify({ RECORDSTATUS1: row.RecordStatus, UPDATEUSERCODE1: generalSupport.UserContext().userId, TabDiscountOrExtraPremiumDiscountOrExtraPremiumType3: row.DiscountOrExtraPremiumType }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoElementoActions.aspx/TabDiscountOrExtraPremium_Grid3SelectCommandActionTransDiscountOrExtraPremium", false,
               JSON.stringify({                 TransDiscountOrExtraPremiumDiscountOrExtraPremiumType1: row.DiscountOrExtraPremiumType,
                TransDiscountOrExtraPremiumLanguageId2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoElementoActions.aspx/TabDiscountOrExtraPremium_Grid5InsertCommandActionTransDiscountOrExtraPremium", false,
               JSON.stringify({ DISCOUNTOREXTRAPREMIUMTYPE1: row.DiscountOrExtraPremiumType, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoElementoActions.aspx/TabDiscountOrExtraPremium_Grid6UpdateCommandActionTransDiscountOrExtraPremium", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransDiscountOrExtraPremiumDiscountOrExtraPremiumType4: row.DiscountOrExtraPremiumType, TransDiscountOrExtraPremiumLanguageId5: generalSupport.SessionContext().languageId }));
               


                        }

                    }
        if (data.d.Success === true){
            $('#TabDiscountOrExtraPremium_GridTbl').bootstrapTable('updateByUniqueId', { id: row.DiscountOrExtraPremiumType, row: row });
            $modal.modal('hide');
            var message9 = 'Se actualizó correctamente el registro';
            notification.toastr.success('Actualizar registro', message9);
                        }                        
                        else {
            var message10 = 'No se pudo actualizar el registro';
            notification.swal.error('Actualizar registro', message10);

                            }

    };
    this.TabDiscountOrExtraPremium_Grid_delete = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoElementoActions.aspx/TabDiscountOrExtraPremium_Grid1DeleteCommandActionTransDiscountOrExtraPremium", false,
               JSON.stringify({ TransDiscountOrExtraPremiumDiscountOrExtraPremiumType1: row.DiscountOrExtraPremiumType }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoElementoActions.aspx/TabDiscountOrExtraPremium_Grid3DeleteCommandActionTabDiscountOrExtraPremium", false,
               JSON.stringify({ TabDiscountOrExtraPremiumDiscountOrExtraPremiumType1: row.DiscountOrExtraPremiumType }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabDiscountOrExtraPremium_GridTbl').bootstrapTable('remove', {field: 'DiscountOrExtraPremiumType', values: [generalSupport.NumericValue('#DiscountOrExtraPremiumType', -99999, 99999)]});
            var message7 = 'Se eliminó correctamente el registro';
            notification.toastr.success('Eliminar registro', message7);
                    }                    
                    else {
            var message8 = 'No se puede eliminar el registro';
            notification.toastr.error('Eliminar registro', message8);

                        }

    };
    this.TabDiscountOrExtraPremium_Grid_BeforeShowPopup = function (row, $modal) {
          var data;
            var nextId;
        if (row.DiscountOrExtraPremiumType === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoElementoActions.aspx/TabDiscountOrExtraPremium_Grid2SelectCommandActionTabDiscountOrExtraPremium", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#DiscountOrExtraPremiumType', nextId);

            }

    };
    this.TabDiscountOrExtraPremiumTranslator_Grid_update = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoElementoActions.aspx/TabDiscountOrExtraPremiumTranslator_Grid1UpdateCommandActionTransDiscountOrExtraPremium", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransDiscountOrExtraPremiumDiscountOrExtraPremiumType4: row.DiscountOrExtraPremiumType, TransDiscountOrExtraPremiumLanguageId5: row.LanguageId }));
               

        if (data.d.Success === true){
            $('#TabDiscountOrExtraPremiumTranslator_GridTbl').bootstrapTable('updateByUniqueId', { id: row.DiscountOrExtraPremiumType, row: row });
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
            var formInstance = $("#H5MantTipoElementoMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                H5MantTipoElementoSupport.TabDiscountOrExtraPremium_GridTblRequest();
                $('#TabDiscountOrExtraPremium_GridContainer').toggleClass('hidden', false);
                $('#TabDiscountOrExtraPremiumTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#H5MantTipoElementoMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                H5MantTipoElementoSupport.TabDiscountOrExtraPremiumTranslator_GridTblRequest();
                $('#TabDiscountOrExtraPremiumTranslator_GridContainer').toggleClass('hidden', false);
                $('#TabDiscountOrExtraPremium_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5MantTipoElementoMainForm").validate({
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
        $("#TabDiscountOrExtraPremium_GridEditForm").validate({
            rules: {
                DiscountOrExtraPremiumType: {
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
                DiscountOrExtraPremiumType: {
                    AutoNumericMinValue: $.i18n.t('app.validation.DiscountOrExtraPremiumType.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.DiscountOrExtraPremiumType.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.DiscountOrExtraPremiumType.required')
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
        $("#TabDiscountOrExtraPremiumTranslator_GridEditForm").validate({
            rules: {
                DiscountOrExtraPremiumTypeTranslator: {
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
                DiscountOrExtraPremiumTypeTranslator: {
                    AutoNumericMinValue: $.i18n.t('app.validation.DiscountOrExtraPremiumTypeTranslator.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.DiscountOrExtraPremiumTypeTranslator.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.DiscountOrExtraPremiumTypeTranslator.required')
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTipoElementoActions.aspx/LookUpForRecordStatus", false,
                JSON.stringify({ id: $('#H5MantTipoElementoFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTipoElementoActions.aspx/LookUpForLanguageIdTranslator", false,
                JSON.stringify({ id: $('#H5MantTipoElementoFormId').val() }),
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

    this.TabDiscountOrExtraPremium_GridTblSetup = function (table) {
        H5MantTipoElementoSupport.LookUpForRecordStatus('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'DiscountOrExtraPremiumType',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabDiscountOrExtraPremium_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'DiscountOrExtraPremiumType',
                title: 'Tipo',
                events: 'TabDiscountOrExtraPremium_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'RecordStatus',
                title: 'Estado del Registro',
                formatter: 'H5MantTipoElementoSupport.LookUpForRecordStatusFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5MantTipoElementoSupport.CreatorUserCode_FormatterMaskData',
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
                formatter: 'H5MantTipoElementoSupport.UpdateUserCode_FormatterMaskData',
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


        $('#TabDiscountOrExtraPremium_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabDiscountOrExtraPremium_GridTbl');
            $('#TabDiscountOrExtraPremium_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabDiscountOrExtraPremium_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabDiscountOrExtraPremium_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantTipoElementoSupport.TabDiscountOrExtraPremium_GridRowToInput(row);
                H5MantTipoElementoSupport.TabDiscountOrExtraPremium_Grid_delete(row, null);
                
                return row.DiscountOrExtraPremiumType;
            });

            $('#TabDiscountOrExtraPremium_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabDiscountOrExtraPremium_GridCreateBtn').click(function () {
            var formInstance = $("#TabDiscountOrExtraPremium_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantTipoElementoSupport.TabDiscountOrExtraPremium_GridShowModal($('#TabDiscountOrExtraPremium_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabDiscountOrExtraPremium_GridPopup').find('#TabDiscountOrExtraPremium_GridSaveBtn').click(function () {
            var formInstance = $("#TabDiscountOrExtraPremium_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabDiscountOrExtraPremium_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabDiscountOrExtraPremium_GridSaveBtn').html();
                $('#TabDiscountOrExtraPremium_GridSaveBtn').html('Procesando...');
                $('#TabDiscountOrExtraPremium_GridSaveBtn').prop('disabled', true);

                H5MantTipoElementoSupport.currentRow.DiscountOrExtraPremiumType = generalSupport.NumericValue('#DiscountOrExtraPremiumType', -99999, 99999);
                H5MantTipoElementoSupport.currentRow.RecordStatus = $('#RecordStatus').val();
                H5MantTipoElementoSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                H5MantTipoElementoSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                H5MantTipoElementoSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);
                H5MantTipoElementoSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';
                H5MantTipoElementoSupport.currentRow.Description = $('#Description').val();
                H5MantTipoElementoSupport.currentRow.ShortDescription = $('#ShortDescription').val();

                $('#TabDiscountOrExtraPremium_GridSaveBtn').prop('disabled', false);
                $('#TabDiscountOrExtraPremium_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantTipoElementoSupport.TabDiscountOrExtraPremium_Grid_update(H5MantTipoElementoSupport.currentRow, $modal);
                }
                else {                    
                    H5MantTipoElementoSupport.TabDiscountOrExtraPremium_Grid_insert(H5MantTipoElementoSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabDiscountOrExtraPremium_GridShowModal = function (md, title, row) {
        row = row || { DiscountOrExtraPremiumType: 0, RecordStatus: 0, CreatorUserCode: 0, CreationDate: null, UpdateUserCode: 0, UpdateDate: null, Description: null, ShortDescription: null };

        md.data('id', row.DiscountOrExtraPremiumType);
        md.find('.modal-title').text(title);

        H5MantTipoElementoSupport.TabDiscountOrExtraPremium_GridRowToInput(row);
        $('#DiscountOrExtraPremiumType').prop('disabled', (row.DiscountOrExtraPremiumType !== 0));
        $('#CreatorUserCode').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        H5MantTipoElementoSupport.TabDiscountOrExtraPremium_Grid_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.TabDiscountOrExtraPremium_GridRowToInput = function (row) {
        H5MantTipoElementoSupport.currentRow = row;
        AutoNumeric.set('#DiscountOrExtraPremiumType', row.DiscountOrExtraPremiumType);
        H5MantTipoElementoSupport.LookUpForRecordStatus(row.RecordStatus, '');
        $('#RecordStatus').trigger('change');
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#Description').val(row.Description);
        $('#ShortDescription').val(row.ShortDescription);

    };
    this.TabDiscountOrExtraPremium_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTipoElementoActions.aspx/TabDiscountOrExtraPremium_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                TransDiscountOrExtraPremiumLanguageId1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#TabDiscountOrExtraPremium_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.TabDiscountOrExtraPremiumTranslator_GridTblSetup = function (table) {
        H5MantTipoElementoSupport.LookUpForLanguageIdTranslator('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'DiscountOrExtraPremiumType',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabDiscountOrExtraPremiumTranslator_Gridtoolbar',
            columns: [{
                field: 'DiscountOrExtraPremiumType',
                title: 'Tipo',
                formatter: 'H5MantTipoElementoSupport.DiscountOrExtraPremiumTypeTranslator_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LanguageId',
                title: 'Idioma',
                formatter: 'H5MantTipoElementoSupport.LookUpForLanguageIdTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Description',
                title: 'Descripción',
                events: 'TabDiscountOrExtraPremiumTranslator_GridActionEvents',
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


        $('#TabDiscountOrExtraPremiumTranslator_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabDiscountOrExtraPremiumTranslator_GridTbl');
            $('#TabDiscountOrExtraPremiumTranslator_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabDiscountOrExtraPremiumTranslator_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabDiscountOrExtraPremiumTranslator_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantTipoElementoSupport.TabDiscountOrExtraPremiumTranslator_GridRowToInput(row);
                
                
                return row.DiscountOrExtraPremiumType;
            });
            
          $('#TabDiscountOrExtraPremiumTranslator_GridTbl').bootstrapTable('remove', {
                field: 'DiscountOrExtraPremiumType',
                values: ids
           });

            $('#TabDiscountOrExtraPremiumTranslator_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabDiscountOrExtraPremiumTranslator_GridCreateBtn').click(function () {
            var formInstance = $("#TabDiscountOrExtraPremiumTranslator_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantTipoElementoSupport.TabDiscountOrExtraPremiumTranslator_GridShowModal($('#TabDiscountOrExtraPremiumTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabDiscountOrExtraPremiumTranslator_GridPopup').find('#TabDiscountOrExtraPremiumTranslator_GridSaveBtn').click(function () {
            var formInstance = $("#TabDiscountOrExtraPremiumTranslator_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabDiscountOrExtraPremiumTranslator_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabDiscountOrExtraPremiumTranslator_GridSaveBtn').html();
                $('#TabDiscountOrExtraPremiumTranslator_GridSaveBtn').html('Procesando...');
                $('#TabDiscountOrExtraPremiumTranslator_GridSaveBtn').prop('disabled', true);

                H5MantTipoElementoSupport.currentRow.DiscountOrExtraPremiumType = generalSupport.NumericValue('#DiscountOrExtraPremiumTypeTranslator', -99999, 99999);
                H5MantTipoElementoSupport.currentRow.LanguageId = parseInt(0 + $('#LanguageIdTranslator').val(), 10);
                H5MantTipoElementoSupport.currentRow.Description = $('#DescriptionTranslator').val();
                H5MantTipoElementoSupport.currentRow.ShortDescription = $('#ShortDescriptionTranslator').val();

                $('#TabDiscountOrExtraPremiumTranslator_GridSaveBtn').prop('disabled', false);
                $('#TabDiscountOrExtraPremiumTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantTipoElementoSupport.TabDiscountOrExtraPremiumTranslator_Grid_update(H5MantTipoElementoSupport.currentRow, $modal);
                }
                else {                    
                    $('#TabDiscountOrExtraPremiumTranslator_GridTbl').bootstrapTable('append', H5MantTipoElementoSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabDiscountOrExtraPremiumTranslator_GridShowModal = function (md, title, row) {
        row = row || { DiscountOrExtraPremiumType: 0, LanguageId: 0, Description: null, ShortDescription: null };

        md.data('id', row.DiscountOrExtraPremiumType);
        md.find('.modal-title').text(title);

        H5MantTipoElementoSupport.TabDiscountOrExtraPremiumTranslator_GridRowToInput(row);
        $('#DiscountOrExtraPremiumTypeTranslator').prop('disabled', true);
        $('#LanguageIdTranslator').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabDiscountOrExtraPremiumTranslator_GridRowToInput = function (row) {
        H5MantTipoElementoSupport.currentRow = row;
        AutoNumeric.set('#DiscountOrExtraPremiumTypeTranslator', row.DiscountOrExtraPremiumType);
        H5MantTipoElementoSupport.LookUpForLanguageIdTranslator(row.LanguageId, '');
        $('#LanguageIdTranslator').trigger('change');
        $('#DescriptionTranslator').val(row.Description);
        $('#ShortDescriptionTranslator').val(row.ShortDescription);

    };
    this.TabDiscountOrExtraPremiumTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTipoElementoActions.aspx/TabDiscountOrExtraPremiumTranslator_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#TabDiscountOrExtraPremiumTranslator_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.DiscountOrExtraPremiumType_FormatterMaskData = function (value, row, index) {          
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
    this.DiscountOrExtraPremiumTypeTranslator_FormatterMaskData = function (value, row, index) {          
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
        
        H5MantTipoElementoSupport.ValidateSetup();
        tableHelperSupport.Translate('#TabDiscountOrExtraPremium_GridTbl', '#TabDiscountOrExtraPremium_GridTbl');
tableHelperSupport.Translate('#TabDiscountOrExtraPremiumTranslator_GridTbl', '#TabDiscountOrExtraPremiumTranslator_GridTbl');

    });
        

    H5MantTipoElementoSupport.ControlBehaviour();
    H5MantTipoElementoSupport.ControlActions();
    

    $("#TabDiscountOrExtraPremium_GridTblPlaceHolder").replaceWith('<table id="TabDiscountOrExtraPremium_GridTbl"></table>');
    H5MantTipoElementoSupport.TabDiscountOrExtraPremium_GridTblSetup($('#TabDiscountOrExtraPremium_GridTbl'));
    $("#TabDiscountOrExtraPremiumTranslator_GridTblPlaceHolder").replaceWith('<table id="TabDiscountOrExtraPremiumTranslator_GridTbl"></table>');
    H5MantTipoElementoSupport.TabDiscountOrExtraPremiumTranslator_GridTblSetup($('#TabDiscountOrExtraPremiumTranslator_GridTbl'));

        H5MantTipoElementoSupport.TabDiscountOrExtraPremium_GridTblRequest();
        H5MantTipoElementoSupport.TabDiscountOrExtraPremiumTranslator_GridTblRequest();




  };
};

$(document).ready(function () {
   H5MantTipoElementoSupport.Init();
});

window.TabDiscountOrExtraPremium_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantTipoElementoSupport.TabDiscountOrExtraPremium_GridShowModal($('#TabDiscountOrExtraPremium_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.TabDiscountOrExtraPremiumTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantTipoElementoSupport.TabDiscountOrExtraPremiumTranslator_GridShowModal($('#TabDiscountOrExtraPremiumTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
