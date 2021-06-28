var H5TipoAreaSuscripcionSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5TipoAreaSuscripcionFormId').val(),
            TabUnderwritingAreaType_Grid_TabUnderwritingAreaType_Item: generalSupport.NormalizeProperties($('#TabUnderwritingAreaType_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate'),
            TabUnderwritingAreaTypeTranslator_Grid_TabUnderwritingAreaType_Item: generalSupport.NormalizeProperties($('#TabUnderwritingAreaTypeTranslator_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#H5TipoAreaSuscripcionFormId').val(data.InstanceFormId);

        H5TipoAreaSuscripcionSupport.LookUpForRecordStatus(source);
        H5TipoAreaSuscripcionSupport.LookUpForRecordStatusTranslator(source);
        H5TipoAreaSuscripcionSupport.LookUpForLanguageIdTranslator(source);

        H5TipoAreaSuscripcionSupport.TabUnderwritingAreaType_GridTblRequest();
        if (data.TabUnderwritingAreaType_Grid_TabUnderwritingAreaType_Item !== null)
            $('#TabUnderwritingAreaType_GridTbl').bootstrapTable('load', data.TabUnderwritingAreaType_Grid_TabUnderwritingAreaType_Item);
        H5TipoAreaSuscripcionSupport.TabUnderwritingAreaTypeTranslator_GridTblRequest();
        if (data.TabUnderwritingAreaTypeTranslator_Grid_TabUnderwritingAreaType_Item !== null)
            $('#TabUnderwritingAreaTypeTranslator_GridTbl').bootstrapTable('load', data.TabUnderwritingAreaTypeTranslator_Grid_TabUnderwritingAreaType_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#UnderwritingArea', {
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
      new AutoNumeric('#UnderwritingAreaTranslator', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      new AutoNumeric('#CreatorUserCodeTranslator', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
        });
      new AutoNumeric('#UpdateUserCodeTranslator', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
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
        $('#CreationDateTranslator_group').datetimepicker({
            format: generalSupport.DateFormat() + ' HH:mm:ss',
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#CreationDateTranslator_group');
        $('#UpdateDateTranslator_group').datetimepicker({
            format: generalSupport.DateFormat() + ' HH:mm:ss',
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#UpdateDateTranslator_group');


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               if (source == 'Initialization')
					         H5TipoAreaSuscripcionSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5TipoAreaSuscripcionSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.TabUnderwritingAreaType_Grid_insert = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5TipoAreaSuscripcionActions.aspx/TabUnderwritingAreaType_Grid1InsertCommandActionTabUnderwritingAreaType", false,
               JSON.stringify({ UNDERWRITINGAREA1: row.UnderwritingArea, RECORDSTATUS2: row.RecordStatus, CREATORUSERCODE2: generalSupport.UserContext().userId, UPDATEUSERCODE4: generalSupport.UserContext().userId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5TipoAreaSuscripcionActions.aspx/TabUnderwritingAreaType_Grid3InsertCommandActionTransUnderwritingAreaType", false,
               JSON.stringify({ UNDERWRITINGAREA1: row.UnderwritingArea, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabUnderwritingAreaType_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message7 = 'Se agregó correctamente el registro';
            notification.toastr.success('Agregar registro', message7);
                    }                    
                    else {
            var message8 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message8);

                        }

    };
    this.TabUnderwritingAreaType_Grid_update = function (row, $modal) {
          var data;
            var recordCount;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5TipoAreaSuscripcionActions.aspx/TabUnderwritingAreaType_Grid1UpdateCommandActionTabUnderwritingAreaType", false,
               JSON.stringify({ RECORDSTATUS1: row.RecordStatus, UPDATEUSERCODE1: generalSupport.UserContext().userId, TabUnderwritingAreaTypeUnderwritingArea3: row.UnderwritingArea }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5TipoAreaSuscripcionActions.aspx/TabUnderwritingAreaType_Grid3SelectCommandActionTransUnderwritingAreaType", false,
               JSON.stringify({                 TransUnderwritingAreaTypeUnderwritingArea1: row.UnderwritingArea,
                TransUnderwritingAreaTypeLanguageId2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5TipoAreaSuscripcionActions.aspx/TabUnderwritingAreaType_Grid5InsertCommandActionTransUnderwritingAreaType", false,
               JSON.stringify({ UNDERWRITINGAREA1: row.UnderwritingArea, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5TipoAreaSuscripcionActions.aspx/TabUnderwritingAreaType_Grid6UpdateCommandActionTransUnderwritingAreaType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransUnderwritingAreaTypeUnderwritingArea4: row.UnderwritingArea, TransUnderwritingAreaTypeLanguageId5: generalSupport.SessionContext().languageId }));
               


                        }

                    }
        if (data.d.Success === true){
            $('#TabUnderwritingAreaType_GridTbl').bootstrapTable('updateByUniqueId', { id: row.UnderwritingArea, row: row });
            $modal.modal('hide');
            var message9 = 'Se actualizó correctamente el registro';
            notification.toastr.success('Actualizar registro', message9);
                        }                        
                        else {
            var message10 = 'No se pudo actualizar el registro';
            notification.swal.error('Actualizar registro', message10);

                            }

    };
    this.TabUnderwritingAreaType_Grid_delete = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5TipoAreaSuscripcionActions.aspx/TabUnderwritingAreaType_Grid1DeleteCommandActionTransUnderwritingAreaType", false,
               JSON.stringify({ TransUnderwritingAreaTypeUnderwritingArea1: row.UnderwritingArea }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5TipoAreaSuscripcionActions.aspx/TabUnderwritingAreaType_Grid3DeleteCommandActionTabUnderwritingAreaType", false,
               JSON.stringify({ TabUnderwritingAreaTypeUnderwritingArea1: row.UnderwritingArea }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabUnderwritingAreaType_GridTbl').bootstrapTable('remove', {field: 'UnderwritingArea', values: [generalSupport.NumericValue('#UnderwritingArea', -99999, 99999)]});
            var message7 = 'Se eliminó correctamente el registro';
            notification.toastr.success('Eliminar registro', message7);
                    }                    
                    else {
            var message8 = 'No se puede eliminar el registro';
            notification.toastr.error('Eliminar registro', message8);

                        }

    };
    this.TabUnderwritingAreaType_Grid_BeforeShowPopup = function (row, $modal) {
          var data;
            var nextId;
        if (row.UnderwritingArea === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5TipoAreaSuscripcionActions.aspx/TabUnderwritingAreaType_Grid2SelectCommandActionTabUnderwritingAreaType", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#UnderwritingArea', nextId);

            }

    };
    this.TabUnderwritingAreaTypeTranslator_Grid_update = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5TipoAreaSuscripcionActions.aspx/TabUnderwritingAreaTypeTranslator_Grid1UpdateCommandActionTransUnderwritingAreaType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransUnderwritingAreaTypeUnderwritingArea4: row.UnderwritingArea, TransUnderwritingAreaTypeLanguageId5: row.LanguageId }));
               

        if (data.d.Success === true){
            $('#TabUnderwritingAreaTypeTranslator_GridTbl').bootstrapTable('updateByUniqueId', { id: row.UnderwritingArea, row: row });
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
            var formInstance = $("#H5TipoAreaSuscripcionMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                H5TipoAreaSuscripcionSupport.TabUnderwritingAreaType_GridTblRequest();
                $('#TabUnderwritingAreaType_GridContainer').toggleClass('hidden', false);
                $('#TabUnderwritingAreaTypeTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#H5TipoAreaSuscripcionMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                H5TipoAreaSuscripcionSupport.TabUnderwritingAreaTypeTranslator_GridTblRequest();
                $('#TabUnderwritingAreaTypeTranslator_GridContainer').toggleClass('hidden', false);
                $('#TabUnderwritingAreaType_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5TipoAreaSuscripcionMainForm").validate({
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
        $("#TabUnderwritingAreaType_GridEditForm").validate({
            rules: {
                UnderwritingArea: {
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
                UnderwritingArea: {
                    AutoNumericMinValue: $.i18n.t('app.validation.UnderwritingArea.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.UnderwritingArea.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.UnderwritingArea.required')
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
        $("#TabUnderwritingAreaTypeTranslator_GridEditForm").validate({
            rules: {
                UnderwritingAreaTranslator: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999,
                    required: true
                },
                DescriptionTranslator: {
                    required: true,
                    maxlength: 60
                },
                ShortDescriptionTranslator: {
                    required: true,
                    maxlength: 20
                },
                RecordStatusTranslator: {
                    required: true,
                },
                CreatorUserCodeTranslator: {
                    AutoNumericMinValue: -999999999,
                    AutoNumericMaxValue: 999999999,
                    required: true
                },
                CreationDateTranslator: {
                    required: true,
                    DatePicker: true
                },
                UpdateUserCodeTranslator: {
                    AutoNumericMinValue: -999999999,
                    AutoNumericMaxValue: 999999999,
                    required: true
                },
                UpdateDateTranslator: {
                    required: true,
                    DatePicker: true
                },
                LanguageIdTranslator: {
                    required: true,
                }

            },
            messages: {
                UnderwritingAreaTranslator: {
                    AutoNumericMinValue: $.i18n.t('app.validation.UnderwritingAreaTranslator.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.UnderwritingAreaTranslator.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.UnderwritingAreaTranslator.required')
                },
                DescriptionTranslator: {
                    required: $.i18n.t('app.validation.DescriptionTranslator.required'),
                    maxlength: $.i18n.t('app.validation.DescriptionTranslator.maxlength')
                },
                ShortDescriptionTranslator: {
                    required: $.i18n.t('app.validation.ShortDescriptionTranslator.required'),
                    maxlength: $.i18n.t('app.validation.ShortDescriptionTranslator.maxlength')
                },
                RecordStatusTranslator: {
                    required: $.i18n.t('app.validation.RecordStatusTranslator.required'),
                },
                CreatorUserCodeTranslator: {
                    AutoNumericMinValue: $.i18n.t('app.validation.CreatorUserCodeTranslator.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.CreatorUserCodeTranslator.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.CreatorUserCodeTranslator.required')
                },
                CreationDateTranslator: {
                    required: $.i18n.t('app.validation.CreationDateTranslator.required'),
                    DatePicker: $.i18n.t('app.validation.CreationDateTranslator.DatePicker')
                },
                UpdateUserCodeTranslator: {
                    AutoNumericMinValue: $.i18n.t('app.validation.UpdateUserCodeTranslator.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.UpdateUserCodeTranslator.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.UpdateUserCodeTranslator.required')
                },
                UpdateDateTranslator: {
                    required: $.i18n.t('app.validation.UpdateDateTranslator.required'),
                    DatePicker: $.i18n.t('app.validation.UpdateDateTranslator.DatePicker')
                },
                LanguageIdTranslator: {
                    required: $.i18n.t('app.validation.LanguageIdTranslator.required'),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5TipoAreaSuscripcionActions.aspx/LookUpForRecordStatus", false,
                JSON.stringify({ id: $('#H5TipoAreaSuscripcionFormId').val() }),
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
    this.LookUpForRecordStatusTranslatorFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#RecordStatusTranslator>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForRecordStatusTranslator = function (defaultValue, source) {
        var ctrol = $('#RecordStatusTranslator');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/H5TipoAreaSuscripcionActions.aspx/LookUpForRecordStatusTranslator", false,
                JSON.stringify({ id: $('#H5TipoAreaSuscripcionFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5TipoAreaSuscripcionActions.aspx/LookUpForLanguageIdTranslator", false,
                JSON.stringify({ id: $('#H5TipoAreaSuscripcionFormId').val() }),
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

    this.TabUnderwritingAreaType_GridTblSetup = function (table) {
        H5TipoAreaSuscripcionSupport.LookUpForRecordStatus('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'UnderwritingArea',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabUnderwritingAreaType_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'UnderwritingArea',
                title: 'Área..',
                events: 'TabUnderwritingAreaType_GridActionEvents',
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
                formatter: 'H5TipoAreaSuscripcionSupport.LookUpForRecordStatusFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5TipoAreaSuscripcionSupport.CreatorUserCode_FormatterMaskData',
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
                formatter: 'H5TipoAreaSuscripcionSupport.UpdateUserCode_FormatterMaskData',
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


        $('#TabUnderwritingAreaType_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabUnderwritingAreaType_GridTbl');
            $('#TabUnderwritingAreaType_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabUnderwritingAreaType_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabUnderwritingAreaType_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5TipoAreaSuscripcionSupport.TabUnderwritingAreaType_GridRowToInput(row);
                H5TipoAreaSuscripcionSupport.TabUnderwritingAreaType_Grid_delete(row, null);
                
                return row.UnderwritingArea;
            });

            $('#TabUnderwritingAreaType_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabUnderwritingAreaType_GridCreateBtn').click(function () {
            var formInstance = $("#TabUnderwritingAreaType_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5TipoAreaSuscripcionSupport.TabUnderwritingAreaType_GridShowModal($('#TabUnderwritingAreaType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabUnderwritingAreaType_GridPopup').find('#TabUnderwritingAreaType_GridSaveBtn').click(function () {
            var formInstance = $("#TabUnderwritingAreaType_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabUnderwritingAreaType_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabUnderwritingAreaType_GridSaveBtn').html();
                $('#TabUnderwritingAreaType_GridSaveBtn').html('Procesando...');
                $('#TabUnderwritingAreaType_GridSaveBtn').prop('disabled', true);

                H5TipoAreaSuscripcionSupport.currentRow.UnderwritingArea = generalSupport.NumericValue('#UnderwritingArea', -99999, 99999);
                H5TipoAreaSuscripcionSupport.currentRow.Description = $('#Description').val();
                H5TipoAreaSuscripcionSupport.currentRow.ShortDescription = $('#ShortDescription').val();
                H5TipoAreaSuscripcionSupport.currentRow.RecordStatus = $('#RecordStatus').val();
                H5TipoAreaSuscripcionSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                H5TipoAreaSuscripcionSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                H5TipoAreaSuscripcionSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);
                H5TipoAreaSuscripcionSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';

                $('#TabUnderwritingAreaType_GridSaveBtn').prop('disabled', false);
                $('#TabUnderwritingAreaType_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5TipoAreaSuscripcionSupport.TabUnderwritingAreaType_Grid_update(H5TipoAreaSuscripcionSupport.currentRow, $modal);
                }
                else {                    
                    H5TipoAreaSuscripcionSupport.TabUnderwritingAreaType_Grid_insert(H5TipoAreaSuscripcionSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabUnderwritingAreaType_GridShowModal = function (md, title, row) {
        row = row || { UnderwritingArea: 0, Description: null, ShortDescription: null, RecordStatus: 0, CreatorUserCode: 0, CreationDate: null, UpdateUserCode: 0, UpdateDate: null };

        md.data('id', row.UnderwritingArea);
        md.find('.modal-title').text(title);

        H5TipoAreaSuscripcionSupport.TabUnderwritingAreaType_GridRowToInput(row);
        $('#UnderwritingArea').prop('disabled', (row.UnderwritingArea !== 0));
        $('#CreatorUserCode').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        H5TipoAreaSuscripcionSupport.TabUnderwritingAreaType_Grid_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.TabUnderwritingAreaType_GridRowToInput = function (row) {
        H5TipoAreaSuscripcionSupport.currentRow = row;
        AutoNumeric.set('#UnderwritingArea', row.UnderwritingArea);
        $('#Description').val(row.Description);
        $('#ShortDescription').val(row.ShortDescription);
        H5TipoAreaSuscripcionSupport.LookUpForRecordStatus(row.RecordStatus, '');
        $('#RecordStatus').trigger('change');
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));

    };
    this.TabUnderwritingAreaType_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5TipoAreaSuscripcionActions.aspx/TabUnderwritingAreaType_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                TransUnderwritingAreaTypeLanguageId1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#TabUnderwritingAreaType_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.TabUnderwritingAreaTypeTranslator_GridTblSetup = function (table) {
        H5TipoAreaSuscripcionSupport.LookUpForRecordStatusTranslator('');
        H5TipoAreaSuscripcionSupport.LookUpForLanguageIdTranslator('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'UnderwritingArea',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabUnderwritingAreaTypeTranslator_Gridtoolbar',
            columns: [{
                field: 'UnderwritingArea',
                title: 'Área de suscripción',
                events: 'TabUnderwritingAreaTypeTranslator_GridActionEvents',
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
                formatter: 'H5TipoAreaSuscripcionSupport.LookUpForRecordStatusTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5TipoAreaSuscripcionSupport.CreatorUserCodeTranslator_FormatterMaskData',
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
                formatter: 'H5TipoAreaSuscripcionSupport.UpdateUserCodeTranslator_FormatterMaskData',
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
                field: 'LanguageId',
                title: 'Idioma',
                formatter: 'H5TipoAreaSuscripcionSupport.LookUpForLanguageIdTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }]
        });


        $('#TabUnderwritingAreaTypeTranslator_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabUnderwritingAreaTypeTranslator_GridTbl');
            $('#TabUnderwritingAreaTypeTranslator_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabUnderwritingAreaTypeTranslator_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabUnderwritingAreaTypeTranslator_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5TipoAreaSuscripcionSupport.TabUnderwritingAreaTypeTranslator_GridRowToInput(row);
                
                
                return row.UnderwritingArea;
            });
            
          $('#TabUnderwritingAreaTypeTranslator_GridTbl').bootstrapTable('remove', {
                field: 'UnderwritingArea',
                values: ids
           });

            $('#TabUnderwritingAreaTypeTranslator_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabUnderwritingAreaTypeTranslator_GridCreateBtn').click(function () {
            var formInstance = $("#TabUnderwritingAreaTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5TipoAreaSuscripcionSupport.TabUnderwritingAreaTypeTranslator_GridShowModal($('#TabUnderwritingAreaTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabUnderwritingAreaTypeTranslator_GridPopup').find('#TabUnderwritingAreaTypeTranslator_GridSaveBtn').click(function () {
            var formInstance = $("#TabUnderwritingAreaTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabUnderwritingAreaTypeTranslator_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabUnderwritingAreaTypeTranslator_GridSaveBtn').html();
                $('#TabUnderwritingAreaTypeTranslator_GridSaveBtn').html('Procesando...');
                $('#TabUnderwritingAreaTypeTranslator_GridSaveBtn').prop('disabled', true);

                H5TipoAreaSuscripcionSupport.currentRow.UnderwritingArea = generalSupport.NumericValue('#UnderwritingAreaTranslator', -99999, 99999);
                H5TipoAreaSuscripcionSupport.currentRow.Description = $('#DescriptionTranslator').val();
                H5TipoAreaSuscripcionSupport.currentRow.ShortDescription = $('#ShortDescriptionTranslator').val();
                H5TipoAreaSuscripcionSupport.currentRow.RecordStatus = $('#RecordStatusTranslator').val();
                H5TipoAreaSuscripcionSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCodeTranslator', -999999999, 999999999);
                H5TipoAreaSuscripcionSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDateTranslator') + ' HH:mm:ss';
                H5TipoAreaSuscripcionSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCodeTranslator', -999999999, 999999999);
                H5TipoAreaSuscripcionSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDateTranslator') + ' HH:mm:ss';
                H5TipoAreaSuscripcionSupport.currentRow.LanguageId = parseInt(0 + $('#LanguageIdTranslator').val(), 10);

                $('#TabUnderwritingAreaTypeTranslator_GridSaveBtn').prop('disabled', false);
                $('#TabUnderwritingAreaTypeTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5TipoAreaSuscripcionSupport.TabUnderwritingAreaTypeTranslator_Grid_update(H5TipoAreaSuscripcionSupport.currentRow, $modal);
                }
                else {                    
                    $('#TabUnderwritingAreaTypeTranslator_GridTbl').bootstrapTable('append', H5TipoAreaSuscripcionSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabUnderwritingAreaTypeTranslator_GridShowModal = function (md, title, row) {
        row = row || { UnderwritingArea: 0, Description: null, ShortDescription: null, RecordStatus: 0, CreatorUserCode: 0, CreationDate: null, UpdateUserCode: 0, UpdateDate: null, LanguageId: 0 };

        md.data('id', row.UnderwritingArea);
        md.find('.modal-title').text(title);

        H5TipoAreaSuscripcionSupport.TabUnderwritingAreaTypeTranslator_GridRowToInput(row);
        $('#UnderwritingAreaTranslator').prop('disabled', true);
        $('#RecordStatusTranslator').prop('disabled', true);
        $('#CreatorUserCodeTranslator').prop('disabled', true);
        $('#CreationDateTranslator').prop('disabled', true);
        $('#UpdateUserCodeTranslator').prop('disabled', true);
        $('#UpdateDateTranslator').prop('disabled', true);
        $('#LanguageIdTranslator').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabUnderwritingAreaTypeTranslator_GridRowToInput = function (row) {
        H5TipoAreaSuscripcionSupport.currentRow = row;
        AutoNumeric.set('#UnderwritingAreaTranslator', row.UnderwritingArea);
        $('#DescriptionTranslator').val(row.Description);
        $('#ShortDescriptionTranslator').val(row.ShortDescription);
        H5TipoAreaSuscripcionSupport.LookUpForRecordStatusTranslator(row.RecordStatus, '');
        $('#RecordStatusTranslator').trigger('change');
        AutoNumeric.set('#CreatorUserCodeTranslator', row.CreatorUserCode);
        $('#CreationDateTranslator').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCodeTranslator', row.UpdateUserCode);
        $('#UpdateDateTranslator').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        H5TipoAreaSuscripcionSupport.LookUpForLanguageIdTranslator(row.LanguageId, '');
        $('#LanguageIdTranslator').trigger('change');

    };
    this.TabUnderwritingAreaTypeTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5TipoAreaSuscripcionActions.aspx/TabUnderwritingAreaTypeTranslator_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#TabUnderwritingAreaTypeTranslator_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.UnderwritingArea_FormatterMaskData = function (value, row, index) {          
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
    this.UnderwritingAreaTranslator_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CreatorUserCodeTranslator_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
        });
      };
    this.UpdateUserCodeTranslator_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
        });
      };





  this.Init = function(){
    securitySupport.ValidateAccessRoles(['EASE1', 'Suscriptor']);
    moment.locale(generalSupport.UserContext().languageName);
    
   generalSupport.TranslateInit(generalSupport.GetCurrentName(), function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        H5TipoAreaSuscripcionSupport.ValidateSetup();
        tableHelperSupport.Translate('#TabUnderwritingAreaType_GridTbl', '#TabUnderwritingAreaType_GridTbl');
tableHelperSupport.Translate('#TabUnderwritingAreaTypeTranslator_GridTbl', '#TabUnderwritingAreaTypeTranslator_GridTbl');

    });
        

    H5TipoAreaSuscripcionSupport.ControlBehaviour();
    H5TipoAreaSuscripcionSupport.ControlActions();
    

    $("#TabUnderwritingAreaType_GridTblPlaceHolder").replaceWith('<table id="TabUnderwritingAreaType_GridTbl"></table>');
    H5TipoAreaSuscripcionSupport.TabUnderwritingAreaType_GridTblSetup($('#TabUnderwritingAreaType_GridTbl'));
    $("#TabUnderwritingAreaTypeTranslator_GridTblPlaceHolder").replaceWith('<table id="TabUnderwritingAreaTypeTranslator_GridTbl"></table>');
    H5TipoAreaSuscripcionSupport.TabUnderwritingAreaTypeTranslator_GridTblSetup($('#TabUnderwritingAreaTypeTranslator_GridTbl'));

        H5TipoAreaSuscripcionSupport.TabUnderwritingAreaType_GridTblRequest();
        H5TipoAreaSuscripcionSupport.TabUnderwritingAreaTypeTranslator_GridTblRequest();




  };
};

$(document).ready(function () {
   H5TipoAreaSuscripcionSupport.Init();
});

window.TabUnderwritingAreaType_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5TipoAreaSuscripcionSupport.TabUnderwritingAreaType_GridShowModal($('#TabUnderwritingAreaType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.TabUnderwritingAreaTypeTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5TipoAreaSuscripcionSupport.TabUnderwritingAreaTypeTranslator_GridShowModal($('#TabUnderwritingAreaTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
