var H5MantEtapasSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5MantEtapasFormId').val(),
            TabStageCase_Grid_TabStageCase_Item: generalSupport.NormalizeProperties($('#TabStageCase_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate'),
            TabStageCaseTranslator_Grid_TabStageCase_Item: generalSupport.NormalizeProperties($('#TabStageCaseTranslator_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#H5MantEtapasFormId').val(data.InstanceFormId);

        H5MantEtapasSupport.LookUpForLineOfBusiness(source);
        H5MantEtapasSupport.LookUpForUnderwritingCaseType(source);
        H5MantEtapasSupport.LookUpForRecordStatus(source);
        H5MantEtapasSupport.LookUpForLanguageIdTranslator(source);
        H5MantEtapasSupport.LookUpForProduct(data.TabStageCaseCollectionProduct, data.TabStageCaseCollectionLineOfBusiness, source);

        H5MantEtapasSupport.TabStageCase_GridTblRequest();
        if (data.TabStageCase_Grid_TabStageCase_Item !== null)
            $('#TabStageCase_GridTbl').bootstrapTable('load', data.TabStageCase_Grid_TabStageCase_Item);
        H5MantEtapasSupport.TabStageCaseTranslator_GridTblRequest();
        if (data.TabStageCaseTranslator_Grid_TabStageCase_Item !== null)
            $('#TabStageCaseTranslator_GridTbl').bootstrapTable('load', data.TabStageCaseTranslator_Grid_TabStageCase_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#Stage', {
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
      new AutoNumeric('#StageTranslator', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });



        $('#LineOfBusiness').on('change', function () {
            var value = $('#LineOfBusiness').val();

            if (value !== null && value !== '0') {
                var skipData = $('#LineOfBusiness').data("skip");

                if (skipData !== undefined && skipData === true)
                    $('#LineOfBusiness').data("skip", false);
                else
                    H5MantEtapasSupport.LookUpForProduct(null, parseInt(0 + $('#LineOfBusiness').val(), 10));
            }
            else
                if($('#LineOfBusiness').val() !== $('#Product').data("parentId1"))
                   $('#Product').children().remove();
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
					         H5MantEtapasSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5MantEtapasSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.TabStageCase_Grid_insert = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEtapasActions.aspx/TabStageCase_Grid1InsertCommandActionTabStageCase", false,
               JSON.stringify({ STAGE1: row.Stage, LINEOFBUSINESS2: row.LineOfBusiness, PRODUCT3: row.Product, UNDERWRITINGCASETYPE4: row.UnderwritingCaseType, RECORDSTATUS5: row.RecordStatus, CREATORUSERCODE5: generalSupport.UserContext().userId, UPDATEUSERCODE7: generalSupport.UserContext().userId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEtapasActions.aspx/TabStageCase_Grid3InsertCommandActionTransStageCase", false,
               JSON.stringify({ STAGE1: row.Stage, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabStageCase_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message7 = 'Se agregó correctamente el registro';
            notification.toastr.success('Agregar registro', message7);
                    }                    
                    else {
            var message8 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message8);

                        }

    };
    this.TabStageCase_Grid_update = function (row, $modal) {
          var data;
            var recordCount;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEtapasActions.aspx/TabStageCase_Grid1UpdateCommandActionTabStageCase", false,
               JSON.stringify({ LINEOFBUSINESS1: row.LineOfBusiness, PRODUCT2: row.Product, UNDERWRITINGCASETYPE3: row.UnderwritingCaseType, RECORDSTATUS4: row.RecordStatus, UPDATEUSERCODE4: generalSupport.UserContext().userId, TabStageCaseStage6: row.Stage }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEtapasActions.aspx/TabStageCase_Grid3SelectCommandActionTransStageCase", false,
               JSON.stringify({                 TransStageCaseStage1: row.Stage,
                TransStageCaseLanguageId2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEtapasActions.aspx/TabStageCase_Grid5InsertCommandActionTransStageCase", false,
               JSON.stringify({ STAGE1: row.Stage, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEtapasActions.aspx/TabStageCase_Grid6UpdateCommandActionTransStageCase", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransStageCaseStage4: row.Stage, TransStageCaseLanguageId5: generalSupport.SessionContext().languageId }));
               


                        }

                    }
        if (data.d.Success === true){
            $('#TabStageCase_GridTbl').bootstrapTable('updateByUniqueId', { id: row.Stage, row: row });
            $modal.modal('hide');
            var message9 = 'Se actualizó correctamente el registro';
            notification.toastr.success('Actualizar registro', message9);
                        }                        
                        else {
            var message10 = 'No se pudo actualizar el registro';
            notification.swal.error('Actualizar registro', message10);

                            }

    };
    this.TabStageCase_Grid_delete = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEtapasActions.aspx/TabStageCase_Grid1DeleteCommandActionTransStageCase", false,
               JSON.stringify({ TransStageCaseStage1: row.Stage }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEtapasActions.aspx/TabStageCase_Grid3DeleteCommandActionTabStageCase", false,
               JSON.stringify({ TabStageCaseStage1: row.Stage }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabStageCase_GridTbl').bootstrapTable('remove', {field: 'Stage', values: [generalSupport.NumericValue('#Stage', -99999, 99999)]});
            var message7 = 'Se eliminó correctamente el registro';
            notification.toastr.success('Eliminar registro', message7);
                    }                    
                    else {
            var message8 = 'No se puede eliminar el registro';
            notification.toastr.error('Eliminar registro', message8);

                        }

    };
    this.TabStageCase_Grid_BeforeShowPopup = function (row, $modal) {
          var data;
            var nextId;
        if (row.Stage === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEtapasActions.aspx/TabStageCase_Grid2SelectCommandActionTabStageCase", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#Stage', nextId);

            }

    };
    this.TabStageCaseTranslator_Grid_update = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantEtapasActions.aspx/TabStageCaseTranslator_Grid1UpdateCommandActionTransStageCase", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransStageCaseStage4: row.Stage, TransStageCaseLanguageId5: row.LanguageId }));
               

        if (data.d.Success === true){
            $('#TabStageCaseTranslator_GridTbl').bootstrapTable('updateByUniqueId', { id: row.Stage, row: row });
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
            var formInstance = $("#H5MantEtapasMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                H5MantEtapasSupport.TabStageCase_GridTblRequest();
                $('#TabStageCase_GridContainer').toggleClass('hidden', false);
                $('#TabStageCaseTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#H5MantEtapasMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                H5MantEtapasSupport.TabStageCaseTranslator_GridTblRequest();
                $('#TabStageCaseTranslator_GridContainer').toggleClass('hidden', false);
                $('#TabStageCase_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5MantEtapasMainForm").validate({
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
        $("#TabStageCase_GridEditForm").validate({
            rules: {
                Stage: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999,
                    required: true
                },
                LineOfBusiness: {
                },
                Product: {
                },
                UnderwritingCaseType: {
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
                },
                RecordStatus: {
                    required: true,
                }

            },
            messages: {
                Stage: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -99999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999',
                    required: 'El campo es requerido.'
                },
                LineOfBusiness: {
                },
                Product: {
                },
                UnderwritingCaseType: {
                    required: 'El campo es requerido.',
                },
                CreatorUserCode: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -999999999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999',
                    required: 'El campo es requerido.'
                },
                CreationDate: {
                    required: 'El campo es requerido.',
                    DatePicker: 'La fecha indicada no es válida'
                },
                UpdateUserCode: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -999999999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999',
                    required: 'El campo es requerido.'
                },
                UpdateDate: {
                    required: 'El campo es requerido.',
                    DatePicker: 'La fecha indicada no es válida'
                },
                Description: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 60 caracteres máximo'
                },
                ShortDescription: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 20 caracteres máximo'
                },
                RecordStatus: {
                    required: 'El campo es requerido.',
                }

            }
        });
        $("#TabStageCaseTranslator_GridEditForm").validate({
            rules: {
                StageTranslator: {
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
                StageTranslator: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -99999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999',
                    required: 'El campo es requerido.'
                },
                LanguageIdTranslator: {
                    required: 'El campo es requerido.',
                },
                DescriptionTranslator: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 60 caracteres máximo'
                },
                ShortDescriptionTranslator: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 20 caracteres máximo'
                }

            }
        });

    };
    this.LookUpForLineOfBusinessFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#LineOfBusiness>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForLineOfBusiness = function (defaultValue, source) {
        var ctrol = $('#LineOfBusiness');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/H5MantEtapasActions.aspx/LookUpForLineOfBusiness", false,
                JSON.stringify({ id: $('#H5MantEtapasFormId').val() }),
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
    this.LookUpForProductFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            H5MantEtapasSupport.LookUpForProduct(null, row.LineOfBusiness);
            result = $("#Product>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForProduct = function (defaultValue, value1, source) {
        var ctrol = $('#Product');
        var parentId1 = ctrol.data("parentId1");
        
        if ((typeof parentId1 == 'undefined' && typeof value1 !== 'undefined') || parentId1 !== value1) {
            ctrol.data("parentId1", value1);

            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));            
            
            app.core.SyncWebMethod("/fasi/dli/forms/H5MantEtapasActions.aspx/LookUpForProduct", false,
                JSON.stringify({
                                        id: $('#H5MantEtapasFormId').val(),
                    TabStageCaseCollectionLineOfBusiness: value1
                }),
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
					      if(source !== 'Initialization')
                    ctrol.change();
            }
    };
    this.LookUpForUnderwritingCaseTypeFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#UnderwritingCaseType>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForUnderwritingCaseType = function (defaultValue, source) {
        var ctrol = $('#UnderwritingCaseType');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantEtapasActions.aspx/LookUpForUnderwritingCaseType", false,
                JSON.stringify({ id: $('#H5MantEtapasFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantEtapasActions.aspx/LookUpForRecordStatus", false,
                JSON.stringify({ id: $('#H5MantEtapasFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantEtapasActions.aspx/LookUpForLanguageIdTranslator", false,
                JSON.stringify({ id: $('#H5MantEtapasFormId').val() }),
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

    this.TabStageCase_GridTblSetup = function (table) {
        H5MantEtapasSupport.LookUpForLineOfBusiness('');
        H5MantEtapasSupport.LookUpForUnderwritingCaseType('');
        H5MantEtapasSupport.LookUpForRecordStatus('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'Stage',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabStageCase_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'Stage',
                title: 'Etapa del caso',
                events: 'TabStageCase_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LineOfBusiness',
                title: 'Ramo',
                formatter: 'H5MantEtapasSupport.LookUpForLineOfBusinessFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Product',
                title: 'Producto',
                formatter: 'H5MantEtapasSupport.LookUpForProductFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'UnderwritingCaseType',
                title: 'Tipo de suscripción',
                formatter: 'H5MantEtapasSupport.LookUpForUnderwritingCaseTypeFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5MantEtapasSupport.CreatorUserCode_FormatterMaskData',
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
                formatter: 'H5MantEtapasSupport.UpdateUserCode_FormatterMaskData',
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
            }, {
                field: 'RecordStatus',
                title: 'Estado del Registro',
                formatter: 'H5MantEtapasSupport.LookUpForRecordStatusFormatter',
                sortable: true,
                halign: 'center'
            }]
        });


        $('#TabStageCase_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabStageCase_GridTbl');
            $('#TabStageCase_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabStageCase_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabStageCase_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantEtapasSupport.TabStageCase_GridRowToInput(row);
                H5MantEtapasSupport.TabStageCase_Grid_delete(row, null);
                
                return row.Stage;
            });

            $('#TabStageCase_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabStageCase_GridCreateBtn').click(function () {
            var formInstance = $("#TabStageCase_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantEtapasSupport.TabStageCase_GridShowModal($('#TabStageCase_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabStageCase_GridPopup').find('#TabStageCase_GridSaveBtn').click(function () {
            var formInstance = $("#TabStageCase_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabStageCase_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabStageCase_GridSaveBtn').html();
                $('#TabStageCase_GridSaveBtn').html('Procesando...');
                $('#TabStageCase_GridSaveBtn').prop('disabled', true);

                H5MantEtapasSupport.currentRow.Stage = generalSupport.NumericValue('#Stage', -99999, 99999);
                H5MantEtapasSupport.currentRow.LineOfBusiness = parseInt(0 + $('#LineOfBusiness').val(), 10);
                H5MantEtapasSupport.currentRow.Product = parseInt(0 + $('#Product').val(), 10);
                H5MantEtapasSupport.currentRow.UnderwritingCaseType = parseInt(0 + $('#UnderwritingCaseType').val(), 10);
                H5MantEtapasSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                H5MantEtapasSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                H5MantEtapasSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);
                H5MantEtapasSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';
                H5MantEtapasSupport.currentRow.Description = $('#Description').val();
                H5MantEtapasSupport.currentRow.ShortDescription = $('#ShortDescription').val();
                H5MantEtapasSupport.currentRow.RecordStatus = $('#RecordStatus').val();

                $('#TabStageCase_GridSaveBtn').prop('disabled', false);
                $('#TabStageCase_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantEtapasSupport.TabStageCase_Grid_update(H5MantEtapasSupport.currentRow, $modal);
                }
                else {                    
                    H5MantEtapasSupport.TabStageCase_Grid_insert(H5MantEtapasSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabStageCase_GridShowModal = function (md, title, row) {
        row = row || { Stage: 0, LineOfBusiness: 0, Product: 0, UnderwritingCaseType: 0, CreatorUserCode: 0, CreationDate: null, UpdateUserCode: 0, UpdateDate: null, Description: null, ShortDescription: null, RecordStatus: 0 };

        md.data('id', row.Stage);
        md.find('.modal-title').text(title);

        H5MantEtapasSupport.TabStageCase_GridRowToInput(row);
        $('#Stage').prop('disabled', (row.Stage !== 0));
        $('#CreatorUserCode').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        H5MantEtapasSupport.TabStageCase_Grid_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.TabStageCase_GridRowToInput = function (row) {
        H5MantEtapasSupport.currentRow = row;
        AutoNumeric.set('#Stage', row.Stage);
        H5MantEtapasSupport.LookUpForLineOfBusiness(row.LineOfBusiness, '');
        $('#LineOfBusiness').trigger('change');
        H5MantEtapasSupport.LookUpForProduct(row.Product, row.LineOfBusiness, '');
        $('#Product').trigger('change');
        H5MantEtapasSupport.LookUpForUnderwritingCaseType(row.UnderwritingCaseType, '');
        $('#UnderwritingCaseType').trigger('change');
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#Description').val(row.Description);
        $('#ShortDescription').val(row.ShortDescription);
        H5MantEtapasSupport.LookUpForRecordStatus(row.RecordStatus, '');
        $('#RecordStatus').trigger('change');

    };
    this.TabStageCase_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantEtapasActions.aspx/TabStageCase_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                TransStageCaseLanguageId1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#TabStageCase_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.TabStageCaseTranslator_GridTblSetup = function (table) {
        H5MantEtapasSupport.LookUpForLanguageIdTranslator('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'Stage',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#TabStageCaseTranslator_Gridtoolbar',
            columns: [{
                field: 'Stage',
                title: 'Etapa del caso',
                formatter: 'H5MantEtapasSupport.StageTranslator_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LanguageId',
                title: 'Idioma',
                formatter: 'H5MantEtapasSupport.LookUpForLanguageIdTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Description',
                title: 'Descripción',
                events: 'TabStageCaseTranslator_GridActionEvents',
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


        $('#TabStageCaseTranslator_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabStageCaseTranslator_GridTbl');
            $('#TabStageCaseTranslator_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabStageCaseTranslator_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabStageCaseTranslator_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantEtapasSupport.TabStageCaseTranslator_GridRowToInput(row);
                
                
                return row.Stage;
            });
            
          $('#TabStageCaseTranslator_GridTbl').bootstrapTable('remove', {
                field: 'Stage',
                values: ids
           });

            $('#TabStageCaseTranslator_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabStageCaseTranslator_GridCreateBtn').click(function () {
            var formInstance = $("#TabStageCaseTranslator_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantEtapasSupport.TabStageCaseTranslator_GridShowModal($('#TabStageCaseTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabStageCaseTranslator_GridPopup').find('#TabStageCaseTranslator_GridSaveBtn').click(function () {
            var formInstance = $("#TabStageCaseTranslator_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabStageCaseTranslator_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabStageCaseTranslator_GridSaveBtn').html();
                $('#TabStageCaseTranslator_GridSaveBtn').html('Procesando...');
                $('#TabStageCaseTranslator_GridSaveBtn').prop('disabled', true);

                H5MantEtapasSupport.currentRow.Stage = generalSupport.NumericValue('#StageTranslator', -99999, 99999);
                H5MantEtapasSupport.currentRow.LanguageId = parseInt(0 + $('#LanguageIdTranslator').val(), 10);
                H5MantEtapasSupport.currentRow.Description = $('#DescriptionTranslator').val();
                H5MantEtapasSupport.currentRow.ShortDescription = $('#ShortDescriptionTranslator').val();

                $('#TabStageCaseTranslator_GridSaveBtn').prop('disabled', false);
                $('#TabStageCaseTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantEtapasSupport.TabStageCaseTranslator_Grid_update(H5MantEtapasSupport.currentRow, $modal);
                }
                else {                    
                    $('#TabStageCaseTranslator_GridTbl').bootstrapTable('append', H5MantEtapasSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabStageCaseTranslator_GridShowModal = function (md, title, row) {
        row = row || { Stage: 0, LanguageId: 0, Description: null, ShortDescription: null };

        md.data('id', row.Stage);
        md.find('.modal-title').text(title);

        H5MantEtapasSupport.TabStageCaseTranslator_GridRowToInput(row);
        $('#StageTranslator').prop('disabled', true);
        $('#LanguageIdTranslator').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabStageCaseTranslator_GridRowToInput = function (row) {
        H5MantEtapasSupport.currentRow = row;
        AutoNumeric.set('#StageTranslator', row.Stage);
        H5MantEtapasSupport.LookUpForLanguageIdTranslator(row.LanguageId, '');
        $('#LanguageIdTranslator').trigger('change');
        $('#DescriptionTranslator').val(row.Description);
        $('#ShortDescriptionTranslator').val(row.ShortDescription);

    };
    this.TabStageCaseTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantEtapasActions.aspx/TabStageCaseTranslator_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#TabStageCaseTranslator_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.Stage_FormatterMaskData = function (value, row, index) {          
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
    this.StageTranslator_FormatterMaskData = function (value, row, index) {          
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
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('Etapas del caso de suscripción');
        

    H5MantEtapasSupport.ControlBehaviour();
    H5MantEtapasSupport.ControlActions();
    H5MantEtapasSupport.ValidateSetup();

    $("#TabStageCase_GridTblPlaceHolder").replaceWith('<table id="TabStageCase_GridTbl"></table>');
    H5MantEtapasSupport.TabStageCase_GridTblSetup($('#TabStageCase_GridTbl'));
    $("#TabStageCaseTranslator_GridTblPlaceHolder").replaceWith('<table id="TabStageCaseTranslator_GridTbl"></table>');
    H5MantEtapasSupport.TabStageCaseTranslator_GridTblSetup($('#TabStageCaseTranslator_GridTbl'));

        H5MantEtapasSupport.TabStageCase_GridTblRequest();
        H5MantEtapasSupport.TabStageCaseTranslator_GridTblRequest();




  };
};

$(document).ready(function () {
   H5MantEtapasSupport.Init();
});

window.TabStageCase_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantEtapasSupport.TabStageCase_GridShowModal($('#TabStageCase_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.TabStageCaseTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantEtapasSupport.TabStageCaseTranslator_GridShowModal($('#TabStageCaseTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
