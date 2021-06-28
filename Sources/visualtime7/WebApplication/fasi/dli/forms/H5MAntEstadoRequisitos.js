var H5MAntEstadoRequisitosSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5MAntEstadoRequisitosFormId').val(),
            TabRequirementStatusType_Grid_TabRequirementStatusType_Item: generalSupport.NormalizeProperties($('#TabRequirementStatusType_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate'),
            TabRequirementStatusTypeTranslator_Grid_TabRequirementStatusType_Item: generalSupport.NormalizeProperties($('#TabRequirementStatusTypeTranslator_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#H5MAntEstadoRequisitosFormId').val(data.InstanceFormId);

        H5MAntEstadoRequisitosSupport.LookUpForRecordStatus(source);
        H5MAntEstadoRequisitosSupport.LookUpForLanguageIdTranslator(source);

        H5MAntEstadoRequisitosSupport.TabRequirementStatusType_GridTblRequest();
        if (data.TabRequirementStatusType_Grid_TabRequirementStatusType_Item !== null)
            $('#TabRequirementStatusType_GridTbl').bootstrapTable('load', data.TabRequirementStatusType_Grid_TabRequirementStatusType_Item);
        H5MAntEstadoRequisitosSupport.TabRequirementStatusTypeTranslator_GridTblRequest();
        if (data.TabRequirementStatusTypeTranslator_Grid_TabRequirementStatusType_Item !== null)
            $('#TabRequirementStatusTypeTranslator_GridTbl').bootstrapTable('load', data.TabRequirementStatusTypeTranslator_Grid_TabRequirementStatusType_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#RequirementStatus', {
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
      new AutoNumeric('#RequirementStatusTranslator', {
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
					         H5MAntEstadoRequisitosSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5MAntEstadoRequisitosSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.TabRequirementStatusType_Grid_insert = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MAntEstadoRequisitosActions.aspx/TabRequirementStatusType_Grid1InsertCommandActionTabRequirementStatusType", false,
               JSON.stringify({ REQUIREMENTSTATUS1: row.RequirementStatus, RECORDSTATUS2: row.RecordStatus, CREATORUSERCODE2: generalSupport.UserContext().userId, UPDATEUSERCODE4: generalSupport.UserContext().userId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MAntEstadoRequisitosActions.aspx/TabRequirementStatusType_Grid3InsertCommandActionTransRequirementStatusType", false,
               JSON.stringify({ REQUIREMENTSTATUS1: row.RequirementStatus, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabRequirementStatusType_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message7 = 'Se agregó correctamente el registro';
            notification.toastr.success('Agregar registro', message7);
                    }                    
                    else {
            var message8 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message8);

                        }

    };
    this.TabRequirementStatusType_Grid_update = function (row, $modal) {
          var data;
            var recordCount;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MAntEstadoRequisitosActions.aspx/TabRequirementStatusType_Grid1UpdateCommandActionTabRequirementStatusType", false,
               JSON.stringify({ RECORDSTATUS1: row.RecordStatus, UPDATEUSERCODE1: generalSupport.UserContext().userId, TabRequirementStatusTypeRequirementStatus3: row.RequirementStatus }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MAntEstadoRequisitosActions.aspx/TabRequirementStatusType_Grid3SelectCommandActionTransRequirementStatusType", false,
               JSON.stringify({                 TransRequirementStatusTypeRequirementStatus1: row.RequirementStatus,
                TransRequirementStatusTypeLanguageId2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MAntEstadoRequisitosActions.aspx/TabRequirementStatusType_Grid5InsertCommandActionTransRequirementStatusType", false,
               JSON.stringify({ REQUIREMENTSTATUS1: row.RequirementStatus, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MAntEstadoRequisitosActions.aspx/TabRequirementStatusType_Grid6UpdateCommandActionTransRequirementStatusType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransRequirementStatusTypeRequirementStatus4: row.RequirementStatus, TransRequirementStatusTypeLanguageId5: generalSupport.SessionContext().languageId }));
               


                        }

                    }
        if (data.d.Success === true){
            $('#TabRequirementStatusType_GridTbl').bootstrapTable('updateByUniqueId', { id: row.RequirementStatus, row: row });
            $modal.modal('hide');
            var message9 = 'Se actualizó correctamente el registro';
            notification.toastr.success('Actualizar registro', message9);
                        }                        
                        else {
            var message10 = 'No se pudo actualizar el registro';
            notification.swal.error('Actualizar registro', message10);

                            }

    };
    this.TabRequirementStatusType_Grid_delete = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MAntEstadoRequisitosActions.aspx/TabRequirementStatusType_Grid1DeleteCommandActionTransRequirementStatusType", false,
               JSON.stringify({ TransRequirementStatusTypeRequirementStatus1: row.RequirementStatus }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MAntEstadoRequisitosActions.aspx/TabRequirementStatusType_Grid3DeleteCommandActionTabRequirementStatusType", false,
               JSON.stringify({ TabRequirementStatusTypeRequirementStatus1: row.RequirementStatus }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabRequirementStatusType_GridTbl').bootstrapTable('remove', {field: 'RequirementStatus', values: [generalSupport.NumericValue('#RequirementStatus', -99999, 99999)]});
            var message7 = 'Se eliminó correctamente el registro';
            notification.toastr.success('Eliminar registro', message7);
                    }                    
                    else {
            var message8 = 'No se puede eliminar el registro';
            notification.toastr.error('Eliminar registro', message8);

                        }

    };
    this.TabRequirementStatusType_Grid_BeforeShowPopup = function (row, $modal) {
          var data;
            var nextId;
        if (row.RequirementStatus === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MAntEstadoRequisitosActions.aspx/TabRequirementStatusType_Grid2SelectCommandActionTabRequirementStatusType", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#RequirementStatus', nextId);

            }

    };
    this.TabRequirementStatusTypeTranslator_Grid_update = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MAntEstadoRequisitosActions.aspx/TabRequirementStatusTypeTranslator_Grid1UpdateCommandActionTransRequirementStatusType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransRequirementStatusTypeRequirementStatus4: row.RequirementStatus, TransRequirementStatusTypeLanguageId5: row.LanguageId }));
               

        if (data.d.Success === true){
            $('#TabRequirementStatusTypeTranslator_GridTbl').bootstrapTable('updateByUniqueId', { id: row.RequirementStatus, row: row });
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
            var formInstance = $("#H5MAntEstadoRequisitosMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                H5MAntEstadoRequisitosSupport.TabRequirementStatusType_GridTblRequest();
                $('#TabRequirementStatusType_GridContainer').toggleClass('hidden', false);
                $('#TabRequirementStatusTypeTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#H5MAntEstadoRequisitosMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                H5MAntEstadoRequisitosSupport.TabRequirementStatusTypeTranslator_GridTblRequest();
                $('#TabRequirementStatusTypeTranslator_GridContainer').toggleClass('hidden', false);
                $('#TabRequirementStatusType_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5MAntEstadoRequisitosMainForm").validate({
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
        $("#TabRequirementStatusType_GridEditForm").validate({
            rules: {
                RequirementStatus: {
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
                RequirementStatus: {
                    AutoNumericMinValue: $.i18n.t('app.validation.RequirementStatus.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.RequirementStatus.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.RequirementStatus.required')
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
        $("#TabRequirementStatusTypeTranslator_GridEditForm").validate({
            rules: {
                RequirementStatusTranslator: {
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
                RequirementStatusTranslator: {
                    AutoNumericMinValue: $.i18n.t('app.validation.RequirementStatusTranslator.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.RequirementStatusTranslator.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.RequirementStatusTranslator.required')
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MAntEstadoRequisitosActions.aspx/LookUpForRecordStatus", false,
                JSON.stringify({ id: $('#H5MAntEstadoRequisitosFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MAntEstadoRequisitosActions.aspx/LookUpForLanguageIdTranslator", false,
                JSON.stringify({ id: $('#H5MAntEstadoRequisitosFormId').val() }),
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

    this.TabRequirementStatusType_GridTblSetup = function (table) {
        H5MAntEstadoRequisitosSupport.LookUpForRecordStatus('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'RequirementStatus',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabRequirementStatusType_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'RequirementStatus',
                title: 'Estado del requisito',
                events: 'TabRequirementStatusType_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'RecordStatus',
                title: 'Estado del registro',
                formatter: 'H5MAntEstadoRequisitosSupport.LookUpForRecordStatusFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5MAntEstadoRequisitosSupport.CreatorUserCode_FormatterMaskData',
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
                formatter: 'H5MAntEstadoRequisitosSupport.UpdateUserCode_FormatterMaskData',
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


        $('#TabRequirementStatusType_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabRequirementStatusType_GridTbl');
            $('#TabRequirementStatusType_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabRequirementStatusType_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabRequirementStatusType_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MAntEstadoRequisitosSupport.TabRequirementStatusType_GridRowToInput(row);
                H5MAntEstadoRequisitosSupport.TabRequirementStatusType_Grid_delete(row, null);
                
                return row.RequirementStatus;
            });

            $('#TabRequirementStatusType_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabRequirementStatusType_GridCreateBtn').click(function () {
            var formInstance = $("#TabRequirementStatusType_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MAntEstadoRequisitosSupport.TabRequirementStatusType_GridShowModal($('#TabRequirementStatusType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabRequirementStatusType_GridPopup').find('#TabRequirementStatusType_GridSaveBtn').click(function () {
            var formInstance = $("#TabRequirementStatusType_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabRequirementStatusType_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabRequirementStatusType_GridSaveBtn').html();
                $('#TabRequirementStatusType_GridSaveBtn').html('Procesando...');
                $('#TabRequirementStatusType_GridSaveBtn').prop('disabled', true);

                H5MAntEstadoRequisitosSupport.currentRow.RequirementStatus = generalSupport.NumericValue('#RequirementStatus', -99999, 99999);
                H5MAntEstadoRequisitosSupport.currentRow.RecordStatus = $('#RecordStatus').val();
                H5MAntEstadoRequisitosSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                H5MAntEstadoRequisitosSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                H5MAntEstadoRequisitosSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);
                H5MAntEstadoRequisitosSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';
                H5MAntEstadoRequisitosSupport.currentRow.Description = $('#Description').val();
                H5MAntEstadoRequisitosSupport.currentRow.ShortDescription = $('#ShortDescription').val();

                $('#TabRequirementStatusType_GridSaveBtn').prop('disabled', false);
                $('#TabRequirementStatusType_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MAntEstadoRequisitosSupport.TabRequirementStatusType_Grid_update(H5MAntEstadoRequisitosSupport.currentRow, $modal);
                }
                else {                    
                    H5MAntEstadoRequisitosSupport.TabRequirementStatusType_Grid_insert(H5MAntEstadoRequisitosSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabRequirementStatusType_GridShowModal = function (md, title, row) {
        row = row || { RequirementStatus: 0, RecordStatus: 0, CreatorUserCode: 0, CreationDate: null, UpdateUserCode: 0, UpdateDate: null, Description: null, ShortDescription: null };

        md.data('id', row.RequirementStatus);
        md.find('.modal-title').text(title);

        H5MAntEstadoRequisitosSupport.TabRequirementStatusType_GridRowToInput(row);
        $('#RequirementStatus').prop('disabled', (row.RequirementStatus !== 0));
        $('#CreatorUserCode').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        H5MAntEstadoRequisitosSupport.TabRequirementStatusType_Grid_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.TabRequirementStatusType_GridRowToInput = function (row) {
        H5MAntEstadoRequisitosSupport.currentRow = row;
        AutoNumeric.set('#RequirementStatus', row.RequirementStatus);
        H5MAntEstadoRequisitosSupport.LookUpForRecordStatus(row.RecordStatus, '');
        $('#RecordStatus').trigger('change');
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#Description').val(row.Description);
        $('#ShortDescription').val(row.ShortDescription);

    };
    this.TabRequirementStatusType_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MAntEstadoRequisitosActions.aspx/TabRequirementStatusType_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                TransRequirementStatusTypeLanguageId1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#TabRequirementStatusType_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.TabRequirementStatusTypeTranslator_GridTblSetup = function (table) {
        H5MAntEstadoRequisitosSupport.LookUpForLanguageIdTranslator('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'RequirementStatus',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabRequirementStatusTypeTranslator_Gridtoolbar',
            columns: [{
                field: 'RequirementStatus',
                title: 'Estado del requisito',
                formatter: 'H5MAntEstadoRequisitosSupport.RequirementStatusTranslator_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LanguageId',
                title: 'Idioma',
                formatter: 'H5MAntEstadoRequisitosSupport.LookUpForLanguageIdTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Description',
                title: 'Descripción',
                events: 'TabRequirementStatusTypeTranslator_GridActionEvents',
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


        $('#TabRequirementStatusTypeTranslator_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabRequirementStatusTypeTranslator_GridTbl');
            $('#TabRequirementStatusTypeTranslator_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabRequirementStatusTypeTranslator_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabRequirementStatusTypeTranslator_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MAntEstadoRequisitosSupport.TabRequirementStatusTypeTranslator_GridRowToInput(row);
                
                
                return row.RequirementStatus;
            });
            
          $('#TabRequirementStatusTypeTranslator_GridTbl').bootstrapTable('remove', {
                field: 'RequirementStatus',
                values: ids
           });

            $('#TabRequirementStatusTypeTranslator_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabRequirementStatusTypeTranslator_GridCreateBtn').click(function () {
            var formInstance = $("#TabRequirementStatusTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MAntEstadoRequisitosSupport.TabRequirementStatusTypeTranslator_GridShowModal($('#TabRequirementStatusTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabRequirementStatusTypeTranslator_GridPopup').find('#TabRequirementStatusTypeTranslator_GridSaveBtn').click(function () {
            var formInstance = $("#TabRequirementStatusTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabRequirementStatusTypeTranslator_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabRequirementStatusTypeTranslator_GridSaveBtn').html();
                $('#TabRequirementStatusTypeTranslator_GridSaveBtn').html('Procesando...');
                $('#TabRequirementStatusTypeTranslator_GridSaveBtn').prop('disabled', true);

                H5MAntEstadoRequisitosSupport.currentRow.RequirementStatus = generalSupport.NumericValue('#RequirementStatusTranslator', -99999, 99999);
                H5MAntEstadoRequisitosSupport.currentRow.LanguageId = parseInt(0 + $('#LanguageIdTranslator').val(), 10);
                H5MAntEstadoRequisitosSupport.currentRow.Description = $('#DescriptionTranslator').val();
                H5MAntEstadoRequisitosSupport.currentRow.ShortDescription = $('#ShortDescriptionTranslator').val();

                $('#TabRequirementStatusTypeTranslator_GridSaveBtn').prop('disabled', false);
                $('#TabRequirementStatusTypeTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MAntEstadoRequisitosSupport.TabRequirementStatusTypeTranslator_Grid_update(H5MAntEstadoRequisitosSupport.currentRow, $modal);
                }
                else {                    
                    $('#TabRequirementStatusTypeTranslator_GridTbl').bootstrapTable('append', H5MAntEstadoRequisitosSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabRequirementStatusTypeTranslator_GridShowModal = function (md, title, row) {
        row = row || { RequirementStatus: 0, LanguageId: 0, Description: null, ShortDescription: null };

        md.data('id', row.RequirementStatus);
        md.find('.modal-title').text(title);

        H5MAntEstadoRequisitosSupport.TabRequirementStatusTypeTranslator_GridRowToInput(row);
        $('#RequirementStatusTranslator').prop('disabled', true);
        $('#LanguageIdTranslator').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabRequirementStatusTypeTranslator_GridRowToInput = function (row) {
        H5MAntEstadoRequisitosSupport.currentRow = row;
        AutoNumeric.set('#RequirementStatusTranslator', row.RequirementStatus);
        H5MAntEstadoRequisitosSupport.LookUpForLanguageIdTranslator(row.LanguageId, '');
        $('#LanguageIdTranslator').trigger('change');
        $('#DescriptionTranslator').val(row.Description);
        $('#ShortDescriptionTranslator').val(row.ShortDescription);

    };
    this.TabRequirementStatusTypeTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MAntEstadoRequisitosActions.aspx/TabRequirementStatusTypeTranslator_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#TabRequirementStatusTypeTranslator_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.RequirementStatus_FormatterMaskData = function (value, row, index) {          
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
    this.RequirementStatusTranslator_FormatterMaskData = function (value, row, index) {          
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
        
        H5MAntEstadoRequisitosSupport.ValidateSetup();
        tableHelperSupport.Translate('#TabRequirementStatusType_GridTbl', '#TabRequirementStatusType_GridTbl');
tableHelperSupport.Translate('#TabRequirementStatusTypeTranslator_GridTbl', '#TabRequirementStatusTypeTranslator_GridTbl');

    });
        

    H5MAntEstadoRequisitosSupport.ControlBehaviour();
    H5MAntEstadoRequisitosSupport.ControlActions();
    

    $("#TabRequirementStatusType_GridTblPlaceHolder").replaceWith('<table id="TabRequirementStatusType_GridTbl"></table>');
    H5MAntEstadoRequisitosSupport.TabRequirementStatusType_GridTblSetup($('#TabRequirementStatusType_GridTbl'));
    $("#TabRequirementStatusTypeTranslator_GridTblPlaceHolder").replaceWith('<table id="TabRequirementStatusTypeTranslator_GridTbl"></table>');
    H5MAntEstadoRequisitosSupport.TabRequirementStatusTypeTranslator_GridTblSetup($('#TabRequirementStatusTypeTranslator_GridTbl'));

        H5MAntEstadoRequisitosSupport.TabRequirementStatusType_GridTblRequest();
        H5MAntEstadoRequisitosSupport.TabRequirementStatusTypeTranslator_GridTblRequest();




  };
};

$(document).ready(function () {
   H5MAntEstadoRequisitosSupport.Init();
});

window.TabRequirementStatusType_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MAntEstadoRequisitosSupport.TabRequirementStatusType_GridShowModal($('#TabRequirementStatusType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.TabRequirementStatusTypeTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MAntEstadoRequisitosSupport.TabRequirementStatusTypeTranslator_GridShowModal($('#TabRequirementStatusTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
