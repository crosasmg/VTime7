var H5MantNivelEnfermedadSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5MantNivelEnfermedadFormId').val(),
            TabDegree_Grid_TabDegree_Item: generalSupport.NormalizeProperties($('#TabDegree_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate'),
            TabDegreeTranslator_Grid_TabDegree_Item: generalSupport.NormalizeProperties($('#TabDegreeTranslator_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#H5MantNivelEnfermedadFormId').val(data.InstanceFormId);

        H5MantNivelEnfermedadSupport.LookUpForRecordStatus(source);
        H5MantNivelEnfermedadSupport.LookUpForLanguageIdTranslator(source);

        H5MantNivelEnfermedadSupport.TabDegree_GridTblRequest();
        if (data.TabDegree_Grid_TabDegree_Item !== null)
            $('#TabDegree_GridTbl').bootstrapTable('load', data.TabDegree_Grid_TabDegree_Item);
        H5MantNivelEnfermedadSupport.TabDegreeTranslator_GridTblRequest();
        if (data.TabDegreeTranslator_Grid_TabDegree_Item !== null)
            $('#TabDegreeTranslator_GridTbl').bootstrapTable('load', data.TabDegreeTranslator_Grid_TabDegree_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#DegreeId', {
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
      new AutoNumeric('#DegreeIdTranslator', {
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
					         H5MantNivelEnfermedadSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5MantNivelEnfermedadSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.TabDegree_Grid_insert = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantNivelEnfermedadActions.aspx/TabDegree_Grid1InsertCommandActionTabDegree", false,
               JSON.stringify({ DEGREEID1: row.DegreeId, RECORDSTATUS2: row.RecordStatus, CREATORUSERCODE2: generalSupport.UserContext().userId, UPDATEUSERCODE4: generalSupport.UserContext().userId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantNivelEnfermedadActions.aspx/TabDegree_Grid3InsertCommandActionTransDegree", false,
               JSON.stringify({ DEGREEID1: row.DegreeId, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabDegree_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message7 = 'Se agregó correctamente el registro';
            notification.toastr.success('Agregar registro', message7);
                    }                    
                    else {
            var message8 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message8);

                        }

    };
    this.TabDegree_Grid_update = function (row, $modal) {
          var data;
            var recordCount;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantNivelEnfermedadActions.aspx/TabDegree_Grid1UpdateCommandActionTabDegree", false,
               JSON.stringify({ RECORDSTATUS1: row.RecordStatus, UPDATEUSERCODE1: generalSupport.UserContext().userId, TabDegreeDegreeId3: row.DegreeId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantNivelEnfermedadActions.aspx/TabDegree_Grid3SelectCommandActionTransDegree", false,
               JSON.stringify({                 TransDegreeDegreeId1: row.DegreeId,
                TransDegreeLanguageId2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantNivelEnfermedadActions.aspx/TabDegree_Grid5InsertCommandActionTransDegree", false,
               JSON.stringify({ DEGREEID1: row.DegreeId, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantNivelEnfermedadActions.aspx/TabDegree_Grid6UpdateCommandActionTransDegree", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransDegreeDegreeId4: row.DegreeId, TransDegreeLanguageId5: generalSupport.SessionContext().languageId }));
               


                        }

                    }
        if (data.d.Success === true){
            $('#TabDegree_GridTbl').bootstrapTable('updateByUniqueId', { id: row.DegreeId, row: row });
            $modal.modal('hide');
            var message9 = 'Se actualizó correctamente el registro';
            notification.toastr.success('Actualizar registro', message9);
                        }                        
                        else {
            var message10 = 'No se pudo actualizar el registro';
            notification.swal.error('Actualizar registro', message10);

                            }

    };
    this.TabDegree_Grid_delete = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantNivelEnfermedadActions.aspx/TabDegree_Grid1DeleteCommandActionTransDegree", false,
               JSON.stringify({ TransDegreeDegreeId1: row.DegreeId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantNivelEnfermedadActions.aspx/TabDegree_Grid3DeleteCommandActionTabDegree", false,
               JSON.stringify({ TabDegreeDegreeId1: row.DegreeId }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabDegree_GridTbl').bootstrapTable('remove', {field: 'DegreeId', values: [generalSupport.NumericValue('#DegreeId', -99999, 99999)]});
            var message7 = 'Se eliminó correctamente el registro';
            notification.toastr.success('Eliminar registro', message7);
                    }                    
                    else {
            var message8 = 'No se puede eliminar el registro';
            notification.toastr.error('Eliminar registro', message8);

                        }

    };
    this.TabDegree_Grid_BeforeShowPopup = function (row, $modal) {
          var data;
            var nextId;
        if (row.DegreeId === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantNivelEnfermedadActions.aspx/TabDegree_Grid2SelectCommandActionTabDegree", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#DegreeId', nextId);

            }

    };
    this.TabDegreeTranslator_Grid_update = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantNivelEnfermedadActions.aspx/TabDegreeTranslator_Grid1UpdateCommandActionTransDegree", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransDegreeDegreeId4: row.DegreeId, TransDegreeLanguageId5: row.LanguageId }));
               

        if (data.d.Success === true){
            $('#TabDegreeTranslator_GridTbl').bootstrapTable('updateByUniqueId', { id: row.DegreeId, row: row });
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
            var formInstance = $("#H5MantNivelEnfermedadMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                H5MantNivelEnfermedadSupport.TabDegree_GridTblRequest();
                $('#TabDegree_GridContainer').toggleClass('hidden', false);
                $('#TabDegreeTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#H5MantNivelEnfermedadMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                H5MantNivelEnfermedadSupport.TabDegreeTranslator_GridTblRequest();
                $('#TabDegreeTranslator_GridContainer').toggleClass('hidden', false);
                $('#TabDegree_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5MantNivelEnfermedadMainForm").validate({
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
        $("#TabDegree_GridEditForm").validate({
            rules: {
                DegreeId: {
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
                DegreeId: {
                    AutoNumericMinValue: $.i18n.t('app.validation.DegreeId.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.DegreeId.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.DegreeId.required')
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
        $("#TabDegreeTranslator_GridEditForm").validate({
            rules: {
                DegreeIdTranslator: {
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
                DegreeIdTranslator: {
                    AutoNumericMinValue: $.i18n.t('app.validation.DegreeIdTranslator.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.DegreeIdTranslator.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.DegreeIdTranslator.required')
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantNivelEnfermedadActions.aspx/LookUpForRecordStatus", false,
                JSON.stringify({ id: $('#H5MantNivelEnfermedadFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantNivelEnfermedadActions.aspx/LookUpForLanguageIdTranslator", false,
                JSON.stringify({ id: $('#H5MantNivelEnfermedadFormId').val() }),
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

    this.TabDegree_GridTblSetup = function (table) {
        H5MantNivelEnfermedadSupport.LookUpForRecordStatus('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'DegreeId',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabDegree_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'DegreeId',
                title: 'Nivel/ Grado de la enfermedad',
                events: 'TabDegree_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'RecordStatus',
                title: 'Estado del registro',
                formatter: 'H5MantNivelEnfermedadSupport.LookUpForRecordStatusFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5MantNivelEnfermedadSupport.CreatorUserCode_FormatterMaskData',
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
                formatter: 'H5MantNivelEnfermedadSupport.UpdateUserCode_FormatterMaskData',
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


        $('#TabDegree_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabDegree_GridTbl');
            $('#TabDegree_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabDegree_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabDegree_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantNivelEnfermedadSupport.TabDegree_GridRowToInput(row);
                H5MantNivelEnfermedadSupport.TabDegree_Grid_delete(row, null);
                
                return row.DegreeId;
            });

            $('#TabDegree_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabDegree_GridCreateBtn').click(function () {
            var formInstance = $("#TabDegree_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantNivelEnfermedadSupport.TabDegree_GridShowModal($('#TabDegree_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabDegree_GridPopup').find('#TabDegree_GridSaveBtn').click(function () {
            var formInstance = $("#TabDegree_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabDegree_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabDegree_GridSaveBtn').html();
                $('#TabDegree_GridSaveBtn').html('Procesando...');
                $('#TabDegree_GridSaveBtn').prop('disabled', true);

                H5MantNivelEnfermedadSupport.currentRow.DegreeId = generalSupport.NumericValue('#DegreeId', -99999, 99999);
                H5MantNivelEnfermedadSupport.currentRow.RecordStatus = $('#RecordStatus').val();
                H5MantNivelEnfermedadSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                H5MantNivelEnfermedadSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                H5MantNivelEnfermedadSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);
                H5MantNivelEnfermedadSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';
                H5MantNivelEnfermedadSupport.currentRow.Description = $('#Description').val();
                H5MantNivelEnfermedadSupport.currentRow.ShortDescription = $('#ShortDescription').val();

                $('#TabDegree_GridSaveBtn').prop('disabled', false);
                $('#TabDegree_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantNivelEnfermedadSupport.TabDegree_Grid_update(H5MantNivelEnfermedadSupport.currentRow, $modal);
                }
                else {                    
                    H5MantNivelEnfermedadSupport.TabDegree_Grid_insert(H5MantNivelEnfermedadSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabDegree_GridShowModal = function (md, title, row) {
        row = row || { DegreeId: 0, RecordStatus: 0, CreatorUserCode: 0, CreationDate: null, UpdateUserCode: 0, UpdateDate: null, Description: null, ShortDescription: null };

        md.data('id', row.DegreeId);
        md.find('.modal-title').text(title);

        H5MantNivelEnfermedadSupport.TabDegree_GridRowToInput(row);
        $('#DegreeId').prop('disabled', (row.DegreeId !== 0));
        $('#CreatorUserCode').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        H5MantNivelEnfermedadSupport.TabDegree_Grid_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.TabDegree_GridRowToInput = function (row) {
        H5MantNivelEnfermedadSupport.currentRow = row;
        AutoNumeric.set('#DegreeId', row.DegreeId);
        H5MantNivelEnfermedadSupport.LookUpForRecordStatus(row.RecordStatus, '');
        $('#RecordStatus').trigger('change');
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#Description').val(row.Description);
        $('#ShortDescription').val(row.ShortDescription);

    };
    this.TabDegree_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantNivelEnfermedadActions.aspx/TabDegree_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                TransDegreeLanguageId1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#TabDegree_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.TabDegreeTranslator_GridTblSetup = function (table) {
        H5MantNivelEnfermedadSupport.LookUpForLanguageIdTranslator('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'DegreeId',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabDegreeTranslator_Gridtoolbar',
            columns: [{
                field: 'DegreeId',
                title: 'Nivel o grado de la enfermedad',
                formatter: 'H5MantNivelEnfermedadSupport.DegreeIdTranslator_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LanguageId',
                title: 'Idioma',
                formatter: 'H5MantNivelEnfermedadSupport.LookUpForLanguageIdTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Description',
                title: 'Descripción',
                events: 'TabDegreeTranslator_GridActionEvents',
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


        $('#TabDegreeTranslator_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabDegreeTranslator_GridTbl');
            $('#TabDegreeTranslator_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabDegreeTranslator_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabDegreeTranslator_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantNivelEnfermedadSupport.TabDegreeTranslator_GridRowToInput(row);
                
                
                return row.DegreeId;
            });
            
          $('#TabDegreeTranslator_GridTbl').bootstrapTable('remove', {
                field: 'DegreeId',
                values: ids
           });

            $('#TabDegreeTranslator_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabDegreeTranslator_GridCreateBtn').click(function () {
            var formInstance = $("#TabDegreeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantNivelEnfermedadSupport.TabDegreeTranslator_GridShowModal($('#TabDegreeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabDegreeTranslator_GridPopup').find('#TabDegreeTranslator_GridSaveBtn').click(function () {
            var formInstance = $("#TabDegreeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabDegreeTranslator_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabDegreeTranslator_GridSaveBtn').html();
                $('#TabDegreeTranslator_GridSaveBtn').html('Procesando...');
                $('#TabDegreeTranslator_GridSaveBtn').prop('disabled', true);

                H5MantNivelEnfermedadSupport.currentRow.DegreeId = generalSupport.NumericValue('#DegreeIdTranslator', -99999, 99999);
                H5MantNivelEnfermedadSupport.currentRow.LanguageId = parseInt(0 + $('#LanguageIdTranslator').val(), 10);
                H5MantNivelEnfermedadSupport.currentRow.Description = $('#DescriptionTranslator').val();
                H5MantNivelEnfermedadSupport.currentRow.ShortDescription = $('#ShortDescriptionTranslator').val();

                $('#TabDegreeTranslator_GridSaveBtn').prop('disabled', false);
                $('#TabDegreeTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantNivelEnfermedadSupport.TabDegreeTranslator_Grid_update(H5MantNivelEnfermedadSupport.currentRow, $modal);
                }
                else {                    
                    $('#TabDegreeTranslator_GridTbl').bootstrapTable('append', H5MantNivelEnfermedadSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabDegreeTranslator_GridShowModal = function (md, title, row) {
        row = row || { DegreeId: 0, LanguageId: 0, Description: null, ShortDescription: null };

        md.data('id', row.DegreeId);
        md.find('.modal-title').text(title);

        H5MantNivelEnfermedadSupport.TabDegreeTranslator_GridRowToInput(row);
        $('#DegreeIdTranslator').prop('disabled', true);
        $('#LanguageIdTranslator').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabDegreeTranslator_GridRowToInput = function (row) {
        H5MantNivelEnfermedadSupport.currentRow = row;
        AutoNumeric.set('#DegreeIdTranslator', row.DegreeId);
        H5MantNivelEnfermedadSupport.LookUpForLanguageIdTranslator(row.LanguageId, '');
        $('#LanguageIdTranslator').trigger('change');
        $('#DescriptionTranslator').val(row.Description);
        $('#ShortDescriptionTranslator').val(row.ShortDescription);

    };
    this.TabDegreeTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantNivelEnfermedadActions.aspx/TabDegreeTranslator_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#TabDegreeTranslator_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.DegreeId_FormatterMaskData = function (value, row, index) {          
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
    this.DegreeIdTranslator_FormatterMaskData = function (value, row, index) {          
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
        
        H5MantNivelEnfermedadSupport.ValidateSetup();
        tableHelperSupport.Translate('#TabDegree_GridTbl', '#TabDegree_GridTbl');
tableHelperSupport.Translate('#TabDegreeTranslator_GridTbl', '#TabDegreeTranslator_GridTbl');

    });
        

    H5MantNivelEnfermedadSupport.ControlBehaviour();
    H5MantNivelEnfermedadSupport.ControlActions();
    

    $("#TabDegree_GridTblPlaceHolder").replaceWith('<table id="TabDegree_GridTbl"></table>');
    H5MantNivelEnfermedadSupport.TabDegree_GridTblSetup($('#TabDegree_GridTbl'));
    $("#TabDegreeTranslator_GridTblPlaceHolder").replaceWith('<table id="TabDegreeTranslator_GridTbl"></table>');
    H5MantNivelEnfermedadSupport.TabDegreeTranslator_GridTblSetup($('#TabDegreeTranslator_GridTbl'));

        H5MantNivelEnfermedadSupport.TabDegree_GridTblRequest();
        H5MantNivelEnfermedadSupport.TabDegreeTranslator_GridTblRequest();




  };
};

$(document).ready(function () {
   H5MantNivelEnfermedadSupport.Init();
});

window.TabDegree_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantNivelEnfermedadSupport.TabDegree_GridShowModal($('#TabDegree_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.TabDegreeTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantNivelEnfermedadSupport.TabDegreeTranslator_GridShowModal($('#TabDegreeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
