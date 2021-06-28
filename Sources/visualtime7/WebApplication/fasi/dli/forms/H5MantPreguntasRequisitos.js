var H5MantPreguntasRequisitosSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5MantPreguntasRequisitosFormId').val(),
            TabQuestionsFromRequirement_Grid_TabQuestionsFromRequirement_Item: generalSupport.NormalizeProperties($('#TabQuestionsFromRequirement_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate'),
            TabQuestionsFromRequirementTranslator_Grid_TabQuestionsFromRequirement_Item: generalSupport.NormalizeProperties($('#TabQuestionsFromRequirementTranslator_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#H5MantPreguntasRequisitosFormId').val(data.InstanceFormId);

        H5MantPreguntasRequisitosSupport.LookUpForRecordStatus(source);
        H5MantPreguntasRequisitosSupport.LookUpForLanguageIdTranslator(source);

        H5MantPreguntasRequisitosSupport.TabQuestionsFromRequirement_GridTblRequest();
        if (data.TabQuestionsFromRequirement_Grid_TabQuestionsFromRequirement_Item !== null)
            $('#TabQuestionsFromRequirement_GridTbl').bootstrapTable('load', data.TabQuestionsFromRequirement_Grid_TabQuestionsFromRequirement_Item);
        H5MantPreguntasRequisitosSupport.TabQuestionsFromRequirementTranslator_GridTblRequest();
        if (data.TabQuestionsFromRequirementTranslator_Grid_TabQuestionsFromRequirement_Item !== null)
            $('#TabQuestionsFromRequirementTranslator_GridTbl').bootstrapTable('load', data.TabQuestionsFromRequirementTranslator_Grid_TabQuestionsFromRequirement_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#QuestionId', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      new AutoNumeric('#RequirementType', {
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
      new AutoNumeric('#QuestionIdTranslator', {
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
					         H5MantPreguntasRequisitosSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5MantPreguntasRequisitosSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.TabQuestionsFromRequirement_Grid_insert = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPreguntasRequisitosActions.aspx/TabQuestionsFromRequirement_Grid1InsertCommandActionTabQuestionsFromRequirement", false,
               JSON.stringify({ QUESTIONID1: row.QuestionId, REQUIREMENTTYPE2: row.RequirementType, RECORDSTATUS3: row.RecordStatus, CREATORUSERCODE3: generalSupport.UserContext().userId, UPDATEUSERCODE5: generalSupport.UserContext().userId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPreguntasRequisitosActions.aspx/TabQuestionsFromRequirement_Grid3InsertCommandActionTransQuestionsFromRequirement", false,
               JSON.stringify({ QUESTIONID1: row.QuestionId, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabQuestionsFromRequirement_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message7 = 'Se agregó correctamente el registro';
            notification.toastr.success('Agregar registro', message7);
                    }                    
                    else {
            var message8 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message8);

                        }

    };
    this.TabQuestionsFromRequirement_Grid_update = function (row, $modal) {
          var data;
            var recordCount;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPreguntasRequisitosActions.aspx/TabQuestionsFromRequirement_Grid1UpdateCommandActionTabQuestionsFromRequirement", false,
               JSON.stringify({ REQUIREMENTTYPE1: row.RequirementType, RECORDSTATUS2: row.RecordStatus, UPDATEUSERCODE2: generalSupport.UserContext().userId, TabQuestionsFromRequirementQuestionId4: row.QuestionId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPreguntasRequisitosActions.aspx/TabQuestionsFromRequirement_Grid3SelectCommandActionTransQuestionsFromRequirement", false,
               JSON.stringify({                 TransQuestionsFromRequirementQuestionId1: row.QuestionId,
                TransQuestionsFromRequirementLanguageId2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPreguntasRequisitosActions.aspx/TabQuestionsFromRequirement_Grid5InsertCommandActionTransQuestionsFromRequirement", false,
               JSON.stringify({ QUESTIONID1: row.QuestionId, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPreguntasRequisitosActions.aspx/TabQuestionsFromRequirement_Grid6UpdateCommandActionTransQuestionsFromRequirement", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransQuestionsFromRequirementQuestionId4: row.QuestionId, TransQuestionsFromRequirementLanguageId5: generalSupport.SessionContext().languageId }));
               


                        }

                    }
        if (data.d.Success === true){
            $('#TabQuestionsFromRequirement_GridTbl').bootstrapTable('updateByUniqueId', { id: row.QuestionId, row: row });
            $modal.modal('hide');
            var message9 = 'Se actualizó correctamente el registro';
            notification.toastr.success('Actualizar registro', message9);
                        }                        
                        else {
            var message10 = 'No se pudo actualizar el registro';
            notification.swal.error('Actualizar registro', message10);

                            }

    };
    this.TabQuestionsFromRequirement_Grid_delete = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPreguntasRequisitosActions.aspx/TabQuestionsFromRequirement_Grid1DeleteCommandActionTransQuestionsFromRequirement", false,
               JSON.stringify({ TransQuestionsFromRequirementQuestionId1: row.QuestionId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPreguntasRequisitosActions.aspx/TabQuestionsFromRequirement_Grid3DeleteCommandActionTabQuestionsFromRequirement", false,
               JSON.stringify({ TabQuestionsFromRequirementQuestionId1: row.QuestionId }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabQuestionsFromRequirement_GridTbl').bootstrapTable('remove', {field: 'QuestionId', values: [generalSupport.NumericValue('#QuestionId', -99999, 99999)]});
            var message7 = 'Se eliminó correctamente el registro';
            notification.toastr.success('Eliminar registro', message7);
                    }                    
                    else {
            var message8 = 'No se puede eliminar el registro';
            notification.toastr.error('Eliminar registro', message8);

                        }

    };
    this.TabQuestionsFromRequirement_Grid_BeforeShowPopup = function (row, $modal) {
          var data;
            var nextId;
        if (row.QuestionId === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPreguntasRequisitosActions.aspx/TabQuestionsFromRequirement_Grid2SelectCommandActionTabQuestionsFromRequirement", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#QuestionId', nextId);

            }

    };
    this.TabQuestionsFromRequirementTranslator_Grid_update = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantPreguntasRequisitosActions.aspx/TabQuestionsFromRequirementTranslator_Grid1UpdateCommandActionTransQuestionsFromRequirement", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransQuestionsFromRequirementQuestionId4: row.QuestionId, TransQuestionsFromRequirementLanguageId5: row.LanguageId }));
               

        if (data.d.Success === true){
            $('#TabQuestionsFromRequirementTranslator_GridTbl').bootstrapTable('updateByUniqueId', { id: row.QuestionId, row: row });
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
            var formInstance = $("#H5MantPreguntasRequisitosMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                H5MantPreguntasRequisitosSupport.TabQuestionsFromRequirement_GridTblRequest();
                $('#TabQuestionsFromRequirement_GridContainer').toggleClass('hidden', false);
                $('#TabQuestionsFromRequirementTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#H5MantPreguntasRequisitosMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                H5MantPreguntasRequisitosSupport.TabQuestionsFromRequirementTranslator_GridTblRequest();
                $('#TabQuestionsFromRequirementTranslator_GridContainer').toggleClass('hidden', false);
                $('#TabQuestionsFromRequirement_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5MantPreguntasRequisitosMainForm").validate({
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
        $("#TabQuestionsFromRequirement_GridEditForm").validate({
            rules: {
                QuestionId: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999,
                    required: true
                },
                RequirementType: {
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
                    maxlength: 256
                },
                ShortDescription: {
                    required: true,
                    maxlength: 20
                }

            },
            messages: {
                QuestionId: {
                    AutoNumericMinValue: $.i18n.t('app.validation.QuestionId.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.QuestionId.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.QuestionId.required')
                },
                RequirementType: {
                    AutoNumericMinValue: $.i18n.t('app.validation.RequirementType.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.RequirementType.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.RequirementType.required')
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
        $("#TabQuestionsFromRequirementTranslator_GridEditForm").validate({
            rules: {
                QuestionIdTranslator: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999,
                    required: true
                },
                LanguageIdTranslator: {
                    required: true,
                },
                DescriptionTranslator: {
                    required: true,
                    maxlength: 256
                },
                ShortDescriptionTranslator: {
                    required: true,
                    maxlength: 20
                }

            },
            messages: {
                QuestionIdTranslator: {
                    AutoNumericMinValue: $.i18n.t('app.validation.QuestionIdTranslator.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.QuestionIdTranslator.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.QuestionIdTranslator.required')
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantPreguntasRequisitosActions.aspx/LookUpForRecordStatus", false,
                JSON.stringify({ id: $('#H5MantPreguntasRequisitosFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantPreguntasRequisitosActions.aspx/LookUpForLanguageIdTranslator", false,
                JSON.stringify({ id: $('#H5MantPreguntasRequisitosFormId').val() }),
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

    this.TabQuestionsFromRequirement_GridTblSetup = function (table) {
        H5MantPreguntasRequisitosSupport.LookUpForRecordStatus('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'QuestionId',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabQuestionsFromRequirement_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'QuestionId',
                title: 'Código de Pregunta',
                events: 'TabQuestionsFromRequirement_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'RequirementType',
                title: 'Tipo de requisito',
                formatter: 'H5MantPreguntasRequisitosSupport.RequirementType_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'RecordStatus',
                title: 'Estado del registro',
                formatter: 'H5MantPreguntasRequisitosSupport.LookUpForRecordStatusFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5MantPreguntasRequisitosSupport.CreatorUserCode_FormatterMaskData',
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
                formatter: 'H5MantPreguntasRequisitosSupport.UpdateUserCode_FormatterMaskData',
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


        $('#TabQuestionsFromRequirement_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabQuestionsFromRequirement_GridTbl');
            $('#TabQuestionsFromRequirement_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabQuestionsFromRequirement_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabQuestionsFromRequirement_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantPreguntasRequisitosSupport.TabQuestionsFromRequirement_GridRowToInput(row);
                H5MantPreguntasRequisitosSupport.TabQuestionsFromRequirement_Grid_delete(row, null);
                
                return row.QuestionId;
            });

            $('#TabQuestionsFromRequirement_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabQuestionsFromRequirement_GridCreateBtn').click(function () {
            var formInstance = $("#TabQuestionsFromRequirement_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantPreguntasRequisitosSupport.TabQuestionsFromRequirement_GridShowModal($('#TabQuestionsFromRequirement_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabQuestionsFromRequirement_GridPopup').find('#TabQuestionsFromRequirement_GridSaveBtn').click(function () {
            var formInstance = $("#TabQuestionsFromRequirement_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabQuestionsFromRequirement_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabQuestionsFromRequirement_GridSaveBtn').html();
                $('#TabQuestionsFromRequirement_GridSaveBtn').html('Procesando...');
                $('#TabQuestionsFromRequirement_GridSaveBtn').prop('disabled', true);

                H5MantPreguntasRequisitosSupport.currentRow.QuestionId = generalSupport.NumericValue('#QuestionId', -99999, 99999);
                H5MantPreguntasRequisitosSupport.currentRow.RequirementType = generalSupport.NumericValue('#RequirementType', -99999, 99999);
                H5MantPreguntasRequisitosSupport.currentRow.RecordStatus = $('#RecordStatus').val();
                H5MantPreguntasRequisitosSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                H5MantPreguntasRequisitosSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                H5MantPreguntasRequisitosSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);
                H5MantPreguntasRequisitosSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';
                H5MantPreguntasRequisitosSupport.currentRow.Description = $('#Description').val();
                H5MantPreguntasRequisitosSupport.currentRow.ShortDescription = $('#ShortDescription').val();

                $('#TabQuestionsFromRequirement_GridSaveBtn').prop('disabled', false);
                $('#TabQuestionsFromRequirement_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantPreguntasRequisitosSupport.TabQuestionsFromRequirement_Grid_update(H5MantPreguntasRequisitosSupport.currentRow, $modal);
                }
                else {                    
                    H5MantPreguntasRequisitosSupport.TabQuestionsFromRequirement_Grid_insert(H5MantPreguntasRequisitosSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabQuestionsFromRequirement_GridShowModal = function (md, title, row) {
        row = row || { QuestionId: 0, RequirementType: 0, RecordStatus: 0, CreatorUserCode: 0, CreationDate: null, UpdateUserCode: 0, UpdateDate: null, Description: null, ShortDescription: null };

        md.data('id', row.QuestionId);
        md.find('.modal-title').text(title);

        H5MantPreguntasRequisitosSupport.TabQuestionsFromRequirement_GridRowToInput(row);
        $('#QuestionId').prop('disabled', (row.QuestionId !== 0));
        $('#CreatorUserCode').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        H5MantPreguntasRequisitosSupport.TabQuestionsFromRequirement_Grid_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.TabQuestionsFromRequirement_GridRowToInput = function (row) {
        H5MantPreguntasRequisitosSupport.currentRow = row;
        AutoNumeric.set('#QuestionId', row.QuestionId);
        AutoNumeric.set('#RequirementType', row.RequirementType);
        H5MantPreguntasRequisitosSupport.LookUpForRecordStatus(row.RecordStatus, '');
        $('#RecordStatus').trigger('change');
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#Description').val(row.Description);
        $('#ShortDescription').val(row.ShortDescription);

    };
    this.TabQuestionsFromRequirement_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantPreguntasRequisitosActions.aspx/TabQuestionsFromRequirement_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                TransQuestionsFromRequirementLanguageId1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#TabQuestionsFromRequirement_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.TabQuestionsFromRequirementTranslator_GridTblSetup = function (table) {
        H5MantPreguntasRequisitosSupport.LookUpForLanguageIdTranslator('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'QuestionId',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabQuestionsFromRequirementTranslator_Gridtoolbar',
            columns: [{
                field: 'QuestionId',
                title: 'Pregunta',
                formatter: 'H5MantPreguntasRequisitosSupport.QuestionIdTranslator_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LanguageId',
                title: 'Idioma',
                formatter: 'H5MantPreguntasRequisitosSupport.LookUpForLanguageIdTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Description',
                title: 'Descripción',
                events: 'TabQuestionsFromRequirementTranslator_GridActionEvents',
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


        $('#TabQuestionsFromRequirementTranslator_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabQuestionsFromRequirementTranslator_GridTbl');
            $('#TabQuestionsFromRequirementTranslator_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabQuestionsFromRequirementTranslator_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabQuestionsFromRequirementTranslator_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantPreguntasRequisitosSupport.TabQuestionsFromRequirementTranslator_GridRowToInput(row);
                
                
                return row.QuestionId;
            });
            
          $('#TabQuestionsFromRequirementTranslator_GridTbl').bootstrapTable('remove', {
                field: 'QuestionId',
                values: ids
           });

            $('#TabQuestionsFromRequirementTranslator_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabQuestionsFromRequirementTranslator_GridCreateBtn').click(function () {
            var formInstance = $("#TabQuestionsFromRequirementTranslator_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantPreguntasRequisitosSupport.TabQuestionsFromRequirementTranslator_GridShowModal($('#TabQuestionsFromRequirementTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabQuestionsFromRequirementTranslator_GridPopup').find('#TabQuestionsFromRequirementTranslator_GridSaveBtn').click(function () {
            var formInstance = $("#TabQuestionsFromRequirementTranslator_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabQuestionsFromRequirementTranslator_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabQuestionsFromRequirementTranslator_GridSaveBtn').html();
                $('#TabQuestionsFromRequirementTranslator_GridSaveBtn').html('Procesando...');
                $('#TabQuestionsFromRequirementTranslator_GridSaveBtn').prop('disabled', true);

                H5MantPreguntasRequisitosSupport.currentRow.QuestionId = generalSupport.NumericValue('#QuestionIdTranslator', -99999, 99999);
                H5MantPreguntasRequisitosSupport.currentRow.LanguageId = parseInt(0 + $('#LanguageIdTranslator').val(), 10);
                H5MantPreguntasRequisitosSupport.currentRow.Description = $('#DescriptionTranslator').val();
                H5MantPreguntasRequisitosSupport.currentRow.ShortDescription = $('#ShortDescriptionTranslator').val();

                $('#TabQuestionsFromRequirementTranslator_GridSaveBtn').prop('disabled', false);
                $('#TabQuestionsFromRequirementTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantPreguntasRequisitosSupport.TabQuestionsFromRequirementTranslator_Grid_update(H5MantPreguntasRequisitosSupport.currentRow, $modal);
                }
                else {                    
                    $('#TabQuestionsFromRequirementTranslator_GridTbl').bootstrapTable('append', H5MantPreguntasRequisitosSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabQuestionsFromRequirementTranslator_GridShowModal = function (md, title, row) {
        row = row || { QuestionId: 0, LanguageId: 0, Description: null, ShortDescription: null };

        md.data('id', row.QuestionId);
        md.find('.modal-title').text(title);

        H5MantPreguntasRequisitosSupport.TabQuestionsFromRequirementTranslator_GridRowToInput(row);
        $('#QuestionIdTranslator').prop('disabled', true);
        $('#LanguageIdTranslator').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabQuestionsFromRequirementTranslator_GridRowToInput = function (row) {
        H5MantPreguntasRequisitosSupport.currentRow = row;
        AutoNumeric.set('#QuestionIdTranslator', row.QuestionId);
        H5MantPreguntasRequisitosSupport.LookUpForLanguageIdTranslator(row.LanguageId, '');
        $('#LanguageIdTranslator').trigger('change');
        $('#DescriptionTranslator').val(row.Description);
        $('#ShortDescriptionTranslator').val(row.ShortDescription);

    };
    this.TabQuestionsFromRequirementTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantPreguntasRequisitosActions.aspx/TabQuestionsFromRequirementTranslator_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#TabQuestionsFromRequirementTranslator_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.QuestionId_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.RequirementType_FormatterMaskData = function (value, row, index) {          
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
    this.QuestionIdTranslator_FormatterMaskData = function (value, row, index) {          
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
        
        H5MantPreguntasRequisitosSupport.ValidateSetup();
        tableHelperSupport.Translate('#TabQuestionsFromRequirement_GridTbl', '#TabQuestionsFromRequirement_GridTbl');
tableHelperSupport.Translate('#TabQuestionsFromRequirementTranslator_GridTbl', '#TabQuestionsFromRequirementTranslator_GridTbl');

    });
        

    H5MantPreguntasRequisitosSupport.ControlBehaviour();
    H5MantPreguntasRequisitosSupport.ControlActions();
    

    $("#TabQuestionsFromRequirement_GridTblPlaceHolder").replaceWith('<table id="TabQuestionsFromRequirement_GridTbl"></table>');
    H5MantPreguntasRequisitosSupport.TabQuestionsFromRequirement_GridTblSetup($('#TabQuestionsFromRequirement_GridTbl'));
    $("#TabQuestionsFromRequirementTranslator_GridTblPlaceHolder").replaceWith('<table id="TabQuestionsFromRequirementTranslator_GridTbl"></table>');
    H5MantPreguntasRequisitosSupport.TabQuestionsFromRequirementTranslator_GridTblSetup($('#TabQuestionsFromRequirementTranslator_GridTbl'));

        H5MantPreguntasRequisitosSupport.TabQuestionsFromRequirement_GridTblRequest();
        H5MantPreguntasRequisitosSupport.TabQuestionsFromRequirementTranslator_GridTblRequest();




  };
};

$(document).ready(function () {
   H5MantPreguntasRequisitosSupport.Init();
});

window.TabQuestionsFromRequirement_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantPreguntasRequisitosSupport.TabQuestionsFromRequirement_GridShowModal($('#TabQuestionsFromRequirement_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.TabQuestionsFromRequirementTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantPreguntasRequisitosSupport.TabQuestionsFromRequirementTranslator_GridShowModal($('#TabQuestionsFromRequirementTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
