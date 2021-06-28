var H5MantMotivosRechazoSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5MantMotivosRechazoFormId').val(),
            TabRejectionReason_Grid_TabRejectionReason_Item: generalSupport.NormalizeProperties($('#TabRejectionReason_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate'),
            TabRejectionReasonTranslator_Grid_TabRejectionReason_Item: generalSupport.NormalizeProperties($('#TabRejectionReasonTranslator_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#H5MantMotivosRechazoFormId').val(data.InstanceFormId);

        H5MantMotivosRechazoSupport.LookUpForRecordStatus(source);
        H5MantMotivosRechazoSupport.LookUpForLanguageIdTranslator(source);

        H5MantMotivosRechazoSupport.TabRejectionReason_GridTblRequest();
        if (data.TabRejectionReason_Grid_TabRejectionReason_Item !== null)
            $('#TabRejectionReason_GridTbl').bootstrapTable('load', data.TabRejectionReason_Grid_TabRejectionReason_Item);
        H5MantMotivosRechazoSupport.TabRejectionReasonTranslator_GridTblRequest();
        if (data.TabRejectionReasonTranslator_Grid_TabRejectionReason_Item !== null)
            $('#TabRejectionReasonTranslator_GridTbl').bootstrapTable('load', data.TabRejectionReasonTranslator_Grid_TabRejectionReason_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#ReasonOfRejection', {
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
      new AutoNumeric('#ReasonOfRejectionTranslator', {
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
					         H5MantMotivosRechazoSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5MantMotivosRechazoSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.TabRejectionReason_Grid_insert = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMotivosRechazoActions.aspx/TabRejectionReason_Grid1InsertCommandActionTabRejectionReason", false,
               JSON.stringify({ REASONOFREJECTION1: row.ReasonOfRejection, RECORDSTATUS2: row.RecordStatus, CREATORUSERCODE2: generalSupport.UserContext().userId, UPDATEUSERCODE4: generalSupport.UserContext().userId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMotivosRechazoActions.aspx/TabRejectionReason_Grid3InsertCommandActionTransRejectionReason", false,
               JSON.stringify({ REASONOFREJECTION1: row.ReasonOfRejection, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabRejectionReason_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message7 = 'Se agregó correctamente el registro';
            notification.toastr.success('Agregar registro', message7);
                    }                    
                    else {
            var message8 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message8);

                        }

    };
    this.TabRejectionReason_Grid_update = function (row, $modal) {
          var data;
            var recordCount;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMotivosRechazoActions.aspx/TabRejectionReason_Grid1UpdateCommandActionTabRejectionReason", false,
               JSON.stringify({ RECORDSTATUS1: row.RecordStatus, UPDATEUSERCODE1: generalSupport.UserContext().userId, TabRejectionReasonReasonOfRejection3: row.ReasonOfRejection }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMotivosRechazoActions.aspx/TabRejectionReason_Grid3SelectCommandActionTransRejectionReason", false,
               JSON.stringify({                 TransRejectionReasonReasonOfRejection1: row.ReasonOfRejection,
                TransRejectionReasonLanguageId2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMotivosRechazoActions.aspx/TabRejectionReason_Grid5InsertCommandActionTransRejectionReason", false,
               JSON.stringify({ REASONOFREJECTION1: row.ReasonOfRejection, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMotivosRechazoActions.aspx/TabRejectionReason_Grid6UpdateCommandActionTransRejectionReason", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransRejectionReasonReasonOfRejection4: row.ReasonOfRejection, TransRejectionReasonLanguageId5: generalSupport.SessionContext().languageId }));
               


                        }

                    }
        if (data.d.Success === true){
            $('#TabRejectionReason_GridTbl').bootstrapTable('updateByUniqueId', { id: row.ReasonOfRejection, row: row });
            $modal.modal('hide');
            var message9 = 'Se actualizó correctamente el registro';
            notification.toastr.success('Actualizar registro', message9);
                        }                        
                        else {
            var message10 = 'No se pudo actualizar el registro';
            notification.swal.error('Actualizar registro', message10);

                            }

    };
    this.TabRejectionReason_Grid_delete = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMotivosRechazoActions.aspx/TabRejectionReason_Grid1DeleteCommandActionTransRejectionReason", false,
               JSON.stringify({ TransRejectionReasonReasonOfRejection1: row.ReasonOfRejection }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMotivosRechazoActions.aspx/TabRejectionReason_Grid3DeleteCommandActionTabRejectionReason", false,
               JSON.stringify({ TabRejectionReasonReasonOfRejection1: row.ReasonOfRejection }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabRejectionReason_GridTbl').bootstrapTable('remove', {field: 'ReasonOfRejection', values: [generalSupport.NumericValue('#ReasonOfRejection', -99999, 99999)]});
            var message7 = 'Se eliminó correctamente el registro';
            notification.toastr.success('Eliminar registro', message7);
                    }                    
                    else {
            var message8 = 'No se puede eliminar el registro';
            notification.toastr.error('Eliminar registro', message8);

                        }

    };
    this.TabRejectionReason_Grid_BeforeShowPopup = function (row, $modal) {
          var data;
            var nextId;
        if (row.ReasonOfRejection === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMotivosRechazoActions.aspx/TabRejectionReason_Grid2SelectCommandActionTabRejectionReason", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#ReasonOfRejection', nextId);

            }

    };
    this.TabRejectionReasonTranslator_Grid_update = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantMotivosRechazoActions.aspx/TabRejectionReasonTranslator_Grid1UpdateCommandActionTransRejectionReason", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransRejectionReasonReasonOfRejection4: row.ReasonOfRejection, TransRejectionReasonLanguageId5: row.LanguageId }));
               

        if (data.d.Success === true){
            $('#TabRejectionReasonTranslator_GridTbl').bootstrapTable('updateByUniqueId', { id: row.ReasonOfRejection, row: row });
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
            var formInstance = $("#H5MantMotivosRechazoMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                H5MantMotivosRechazoSupport.TabRejectionReason_GridTblRequest();
                $('#TabRejectionReason_GridContainer').toggleClass('hidden', false);
                $('#TabRejectionReasonTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#H5MantMotivosRechazoMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                H5MantMotivosRechazoSupport.TabRejectionReasonTranslator_GridTblRequest();
                $('#TabRejectionReasonTranslator_GridContainer').toggleClass('hidden', false);
                $('#TabRejectionReason_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5MantMotivosRechazoMainForm").validate({
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
        $("#TabRejectionReason_GridEditForm").validate({
            rules: {
                ReasonOfRejection: {
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
                ReasonOfRejection: {
                    AutoNumericMinValue: $.i18n.t('app.validation.ReasonOfRejection.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.ReasonOfRejection.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.ReasonOfRejection.required')
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
        $("#TabRejectionReasonTranslator_GridEditForm").validate({
            rules: {
                ReasonOfRejectionTranslator: {
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
                ReasonOfRejectionTranslator: {
                    AutoNumericMinValue: $.i18n.t('app.validation.ReasonOfRejectionTranslator.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.ReasonOfRejectionTranslator.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.ReasonOfRejectionTranslator.required')
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantMotivosRechazoActions.aspx/LookUpForRecordStatus", false,
                JSON.stringify({ id: $('#H5MantMotivosRechazoFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantMotivosRechazoActions.aspx/LookUpForLanguageIdTranslator", false,
                JSON.stringify({ id: $('#H5MantMotivosRechazoFormId').val() }),
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

    this.TabRejectionReason_GridTblSetup = function (table) {
        H5MantMotivosRechazoSupport.LookUpForRecordStatus('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'ReasonOfRejection',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabRejectionReason_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'ReasonOfRejection',
                title: 'Motivo',
                events: 'TabRejectionReason_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'RecordStatus',
                title: 'Estado del Registro',
                formatter: 'H5MantMotivosRechazoSupport.LookUpForRecordStatusFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5MantMotivosRechazoSupport.CreatorUserCode_FormatterMaskData',
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
                formatter: 'H5MantMotivosRechazoSupport.UpdateUserCode_FormatterMaskData',
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


        $('#TabRejectionReason_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabRejectionReason_GridTbl');
            $('#TabRejectionReason_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabRejectionReason_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabRejectionReason_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantMotivosRechazoSupport.TabRejectionReason_GridRowToInput(row);
                H5MantMotivosRechazoSupport.TabRejectionReason_Grid_delete(row, null);
                
                return row.ReasonOfRejection;
            });

            $('#TabRejectionReason_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabRejectionReason_GridCreateBtn').click(function () {
            var formInstance = $("#TabRejectionReason_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantMotivosRechazoSupport.TabRejectionReason_GridShowModal($('#TabRejectionReason_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabRejectionReason_GridPopup').find('#TabRejectionReason_GridSaveBtn').click(function () {
            var formInstance = $("#TabRejectionReason_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabRejectionReason_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabRejectionReason_GridSaveBtn').html();
                $('#TabRejectionReason_GridSaveBtn').html('Procesando...');
                $('#TabRejectionReason_GridSaveBtn').prop('disabled', true);

                H5MantMotivosRechazoSupport.currentRow.ReasonOfRejection = generalSupport.NumericValue('#ReasonOfRejection', -99999, 99999);
                H5MantMotivosRechazoSupport.currentRow.RecordStatus = $('#RecordStatus').val();
                H5MantMotivosRechazoSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                H5MantMotivosRechazoSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                H5MantMotivosRechazoSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);
                H5MantMotivosRechazoSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';
                H5MantMotivosRechazoSupport.currentRow.Description = $('#Description').val();
                H5MantMotivosRechazoSupport.currentRow.ShortDescription = $('#ShortDescription').val();

                $('#TabRejectionReason_GridSaveBtn').prop('disabled', false);
                $('#TabRejectionReason_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantMotivosRechazoSupport.TabRejectionReason_Grid_update(H5MantMotivosRechazoSupport.currentRow, $modal);
                }
                else {                    
                    H5MantMotivosRechazoSupport.TabRejectionReason_Grid_insert(H5MantMotivosRechazoSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabRejectionReason_GridShowModal = function (md, title, row) {
        row = row || { ReasonOfRejection: 0, RecordStatus: 0, CreatorUserCode: 0, CreationDate: null, UpdateUserCode: 0, UpdateDate: null, Description: null, ShortDescription: null };

        md.data('id', row.ReasonOfRejection);
        md.find('.modal-title').text(title);

        H5MantMotivosRechazoSupport.TabRejectionReason_GridRowToInput(row);
        $('#ReasonOfRejection').prop('disabled', (row.ReasonOfRejection !== 0));
        $('#CreatorUserCode').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        H5MantMotivosRechazoSupport.TabRejectionReason_Grid_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.TabRejectionReason_GridRowToInput = function (row) {
        H5MantMotivosRechazoSupport.currentRow = row;
        AutoNumeric.set('#ReasonOfRejection', row.ReasonOfRejection);
        H5MantMotivosRechazoSupport.LookUpForRecordStatus(row.RecordStatus, '');
        $('#RecordStatus').trigger('change');
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#Description').val(row.Description);
        $('#ShortDescription').val(row.ShortDescription);

    };
    this.TabRejectionReason_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantMotivosRechazoActions.aspx/TabRejectionReason_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                TransRejectionReasonLanguageId1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#TabRejectionReason_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.TabRejectionReasonTranslator_GridTblSetup = function (table) {
        H5MantMotivosRechazoSupport.LookUpForLanguageIdTranslator('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'ReasonOfRejection',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabRejectionReasonTranslator_Gridtoolbar',
            columns: [{
                field: 'ReasonOfRejection',
                title: 'Motivo',
                formatter: 'H5MantMotivosRechazoSupport.ReasonOfRejectionTranslator_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LanguageId',
                title: 'Código de idioma',
                formatter: 'H5MantMotivosRechazoSupport.LookUpForLanguageIdTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Description',
                title: 'Descripción',
                events: 'TabRejectionReasonTranslator_GridActionEvents',
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


        $('#TabRejectionReasonTranslator_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabRejectionReasonTranslator_GridTbl');
            $('#TabRejectionReasonTranslator_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabRejectionReasonTranslator_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabRejectionReasonTranslator_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantMotivosRechazoSupport.TabRejectionReasonTranslator_GridRowToInput(row);
                
                
                return row.ReasonOfRejection;
            });
            
          $('#TabRejectionReasonTranslator_GridTbl').bootstrapTable('remove', {
                field: 'ReasonOfRejection',
                values: ids
           });

            $('#TabRejectionReasonTranslator_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabRejectionReasonTranslator_GridCreateBtn').click(function () {
            var formInstance = $("#TabRejectionReasonTranslator_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantMotivosRechazoSupport.TabRejectionReasonTranslator_GridShowModal($('#TabRejectionReasonTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabRejectionReasonTranslator_GridPopup').find('#TabRejectionReasonTranslator_GridSaveBtn').click(function () {
            var formInstance = $("#TabRejectionReasonTranslator_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabRejectionReasonTranslator_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabRejectionReasonTranslator_GridSaveBtn').html();
                $('#TabRejectionReasonTranslator_GridSaveBtn').html('Procesando...');
                $('#TabRejectionReasonTranslator_GridSaveBtn').prop('disabled', true);

                H5MantMotivosRechazoSupport.currentRow.ReasonOfRejection = generalSupport.NumericValue('#ReasonOfRejectionTranslator', -99999, 99999);
                H5MantMotivosRechazoSupport.currentRow.LanguageId = parseInt(0 + $('#LanguageIdTranslator').val(), 10);
                H5MantMotivosRechazoSupport.currentRow.Description = $('#DescriptionTranslator').val();
                H5MantMotivosRechazoSupport.currentRow.ShortDescription = $('#ShortDescriptionTranslator').val();

                $('#TabRejectionReasonTranslator_GridSaveBtn').prop('disabled', false);
                $('#TabRejectionReasonTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantMotivosRechazoSupport.TabRejectionReasonTranslator_Grid_update(H5MantMotivosRechazoSupport.currentRow, $modal);
                }
                else {                    
                    $('#TabRejectionReasonTranslator_GridTbl').bootstrapTable('append', H5MantMotivosRechazoSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabRejectionReasonTranslator_GridShowModal = function (md, title, row) {
        row = row || { ReasonOfRejection: 0, LanguageId: 0, Description: null, ShortDescription: null };

        md.data('id', row.ReasonOfRejection);
        md.find('.modal-title').text(title);

        H5MantMotivosRechazoSupport.TabRejectionReasonTranslator_GridRowToInput(row);
        $('#ReasonOfRejectionTranslator').prop('disabled', true);
        $('#LanguageIdTranslator').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabRejectionReasonTranslator_GridRowToInput = function (row) {
        H5MantMotivosRechazoSupport.currentRow = row;
        AutoNumeric.set('#ReasonOfRejectionTranslator', row.ReasonOfRejection);
        H5MantMotivosRechazoSupport.LookUpForLanguageIdTranslator(row.LanguageId, '');
        $('#LanguageIdTranslator').trigger('change');
        $('#DescriptionTranslator').val(row.Description);
        $('#ShortDescriptionTranslator').val(row.ShortDescription);

    };
    this.TabRejectionReasonTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantMotivosRechazoActions.aspx/TabRejectionReasonTranslator_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#TabRejectionReasonTranslator_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.ReasonOfRejection_FormatterMaskData = function (value, row, index) {          
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
    this.ReasonOfRejectionTranslator_FormatterMaskData = function (value, row, index) {          
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
        
        H5MantMotivosRechazoSupport.ValidateSetup();
        tableHelperSupport.Translate('#TabRejectionReason_GridTbl', '#TabRejectionReason_GridTbl');
tableHelperSupport.Translate('#TabRejectionReasonTranslator_GridTbl', '#TabRejectionReasonTranslator_GridTbl');

    });
        

    H5MantMotivosRechazoSupport.ControlBehaviour();
    H5MantMotivosRechazoSupport.ControlActions();
    

    $("#TabRejectionReason_GridTblPlaceHolder").replaceWith('<table id="TabRejectionReason_GridTbl"></table>');
    H5MantMotivosRechazoSupport.TabRejectionReason_GridTblSetup($('#TabRejectionReason_GridTbl'));
    $("#TabRejectionReasonTranslator_GridTblPlaceHolder").replaceWith('<table id="TabRejectionReasonTranslator_GridTbl"></table>');
    H5MantMotivosRechazoSupport.TabRejectionReasonTranslator_GridTblSetup($('#TabRejectionReasonTranslator_GridTbl'));

        H5MantMotivosRechazoSupport.TabRejectionReason_GridTblRequest();
        H5MantMotivosRechazoSupport.TabRejectionReasonTranslator_GridTblRequest();




  };
};

$(document).ready(function () {
   H5MantMotivosRechazoSupport.Init();
});

window.TabRejectionReason_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantMotivosRechazoSupport.TabRejectionReason_GridShowModal($('#TabRejectionReason_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.TabRejectionReasonTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantMotivosRechazoSupport.TabRejectionReasonTranslator_GridShowModal($('#TabRejectionReasonTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
