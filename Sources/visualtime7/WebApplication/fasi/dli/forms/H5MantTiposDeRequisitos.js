var H5MantTiposDeRequisitosSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5MantTiposDeRequisitosFormId').val(),
            TabRequirementType_Grid_TabRequirementType_Item: generalSupport.NormalizeProperties($('#TabRequirementType_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate'),
            TabRequirementTypeTranslator_Grid_TabRequirementType_Item: generalSupport.NormalizeProperties($('#TabRequirementTypeTranslator_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#H5MantTiposDeRequisitosFormId').val(data.InstanceFormId);

        H5MantTiposDeRequisitosSupport.LookUpForLineOfBusiness(source);
        H5MantTiposDeRequisitosSupport.LookUpForRecordStatus(source);
        H5MantTiposDeRequisitosSupport.LookUpForProcessType(source);
        H5MantTiposDeRequisitosSupport.LookUpForUnderwritingArea(source);
        H5MantTiposDeRequisitosSupport.LookUpForPayer(source);
        H5MantTiposDeRequisitosSupport.LookUpForLanguageIdTranslator(source);
        H5MantTiposDeRequisitosSupport.LookUpForProduct(data.TabRequirementTypeCollectionProduct, data.TabRequirementTypeCollectionLineOfBusiness, source);

        H5MantTiposDeRequisitosSupport.TabRequirementType_GridTblRequest();
        if (data.TabRequirementType_Grid_TabRequirementType_Item !== null)
            $('#TabRequirementType_GridTbl').bootstrapTable('load', data.TabRequirementType_Grid_TabRequirementType_Item);
        H5MantTiposDeRequisitosSupport.TabRequirementTypeTranslator_GridTblRequest();
        if (data.TabRequirementTypeTranslator_Grid_TabRequirementType_Item !== null)
            $('#TabRequirementTypeTranslator_GridTbl').bootstrapTable('load', data.TabRequirementTypeTranslator_Grid_TabRequirementType_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#RequirementType', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      new AutoNumeric('#Cost', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 0,
            minimumValue: -999999999999999999
        });
      new AutoNumeric('#AcordRequirementCode', {
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
      new AutoNumeric('#RequirementTypeTranslator', {
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
                    H5MantTiposDeRequisitosSupport.LookUpForProduct(null, parseInt(0 + $('#LineOfBusiness').val(), 10));
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
					         H5MantTiposDeRequisitosSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5MantTiposDeRequisitosSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.TabRequirementType_Grid_insert = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDeRequisitosActions.aspx/TabRequirementType_Grid1InsertCommandActionTabRequirementType", false,
               JSON.stringify({ REQUIREMENTTYPE1: row.RequirementType, PROCESSTYPE2: row.ProcessType, UNDERWRITINGAREA3: row.UnderwritingArea, PAYER4: row.Payer, COST5: row.Cost, LINK6: row.Link, ACORDREQUIREMENTCODE7: row.AcordRequirementCode, LINEOFBUSINESS8: row.LineOfBusiness, PRODUCT9: row.Product, RECORDSTATUS10: row.RecordStatus, CREATORUSERCODE10: generalSupport.UserContext().userId, UPDATEUSERCODE12: generalSupport.UserContext().userId, ALLOWVIEWREQUIREMENT15: row.AllowViewRequirement, ALLOWLOADREQUIREMENT16: row.AllowLoadRequirement }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDeRequisitosActions.aspx/TabRequirementType_Grid3InsertCommandActionTransRequirementType", false,
               JSON.stringify({ REQUIREMENTTYPE1: row.RequirementType, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabRequirementType_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message7 = 'Se agregó correctamente el registro';
            notification.toastr.success('Agregar registro', message7);
                    }                    
                    else {
            var message8 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message8);

                        }

    };
    this.TabRequirementType_Grid_update = function (row, $modal) {
          var data;
            var recordCount;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDeRequisitosActions.aspx/TabRequirementType_Grid1UpdateCommandActionTabRequirementType", false,
               JSON.stringify({ PROCESSTYPE1: row.ProcessType, UNDERWRITINGAREA2: row.UnderwritingArea, PAYER3: row.Payer, COST4: row.Cost, LINK5: row.Link, ACORDREQUIREMENTCODE6: row.AcordRequirementCode, LINEOFBUSINESS7: row.LineOfBusiness, PRODUCT8: row.Product, RECORDSTATUS9: row.RecordStatus, UPDATEUSERCODE9: generalSupport.UserContext().userId, ALLOWVIEWREQUIREMENT11: row.AllowViewRequirement, ALLOWLOADREQUIREMENT12: row.AllowLoadRequirement, TabRequirementTypeRequirementType13: row.RequirementType }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDeRequisitosActions.aspx/TabRequirementType_Grid3SelectCommandActionTransRequirementType", false,
               JSON.stringify({                 TransRequirementTypeRequirementType1: row.RequirementType,
                TransRequirementTypeLanguageId2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDeRequisitosActions.aspx/TabRequirementType_Grid5InsertCommandActionTransRequirementType", false,
               JSON.stringify({ REQUIREMENTTYPE1: row.RequirementType, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDeRequisitosActions.aspx/TabRequirementType_Grid6UpdateCommandActionTransRequirementType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransRequirementTypeRequirementType4: row.RequirementType, TransRequirementTypeLanguageId5: generalSupport.SessionContext().languageId }));
               


                        }

                    }
        if (data.d.Success === true){
            $('#TabRequirementType_GridTbl').bootstrapTable('updateByUniqueId', { id: row.RequirementType, row: row });
            $modal.modal('hide');
            var message9 = 'Se actualizó correctamente el registro';
            notification.toastr.success('Actualizar registro', message9);
                        }                        
                        else {
            var message10 = 'No se pudo actualizar el registro';
            notification.swal.error('Actualizar registro', message10);

                            }

    };
    this.TabRequirementType_Grid_delete = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDeRequisitosActions.aspx/TabRequirementType_Grid1DeleteCommandActionTransRequirementType", false,
               JSON.stringify({ TransRequirementTypeRequirementType1: row.RequirementType }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDeRequisitosActions.aspx/TabRequirementType_Grid3DeleteCommandActionTabRequirementType", false,
               JSON.stringify({ TabRequirementTypeRequirementType1: row.RequirementType }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            $('#TabRequirementType_GridTbl').bootstrapTable('remove', {field: 'RequirementType', values: [generalSupport.NumericValue('#RequirementType', -99999, 99999)]});
            var message7 = 'Se eliminó correctamente el registro';
            notification.toastr.success('Eliminar registro', message7);
                    }                    
                    else {
            var message8 = 'No se puede eliminar el registro';
            notification.toastr.error('Eliminar registro', message8);

                        }

    };
    this.TabRequirementType_Grid_BeforeShowPopup = function (row, $modal) {
          var data;
            var nextId;
        if (row.RequirementType === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDeRequisitosActions.aspx/TabRequirementType_Grid2SelectCommandActionTabRequirementType", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#RequirementType', nextId);

            }

    };
    this.TabRequirementTypeTranslator_Grid_update = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDeRequisitosActions.aspx/TabRequirementTypeTranslator_Grid1UpdateCommandActionTransRequirementType", false,
               JSON.stringify({ DESCRIPTION1: row.Description, SHORTDESCRIPTION2: row.ShortDescription, UPDATEUSERCODE2: generalSupport.UserContext().userId, TransRequirementTypeRequirementType4: row.RequirementType, TransRequirementTypeLanguageId5: row.LanguageId }));
               

        if (data.d.Success === true){
            $('#TabRequirementTypeTranslator_GridTbl').bootstrapTable('updateByUniqueId', { id: row.RequirementType, row: row });
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
            var formInstance = $("#H5MantTiposDeRequisitosMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                H5MantTiposDeRequisitosSupport.TabRequirementType_GridTblRequest();
                $('#TabRequirementType_GridContainer').toggleClass('hidden', false);
                $('#TabRequirementTypeTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#H5MantTiposDeRequisitosMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                H5MantTiposDeRequisitosSupport.TabRequirementTypeTranslator_GridTblRequest();
                $('#TabRequirementTypeTranslator_GridContainer').toggleClass('hidden', false);
                $('#TabRequirementType_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5MantTiposDeRequisitosMainForm").validate({
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
        $("#TabRequirementType_GridEditForm").validate({
            rules: {
                RequirementType: {
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
                LineOfBusiness: {
                },
                Product: {
                },
                RecordStatus: {
                    required: true,
                },
                ProcessType: {
                    required: true,
                },
                UnderwritingArea: {
                    required: true,
                },
                Payer: {
                    required: true,
                },
                Cost: {
                    AutoNumericMinValue: -999999999999999999,
                    AutoNumericMaxValue: 999999999999999999
                },
                Link: {
                    maxlength: 256
                },
                AcordRequirementCode: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999
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
                RequirementType: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -99999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999',
                    required: 'El campo es requerido.'
                },
                Description: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 60 caracteres máximo'
                },
                ShortDescription: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 20 caracteres máximo'
                },
                LineOfBusiness: {
                },
                Product: {
                },
                RecordStatus: {
                    required: 'El campo es requerido.',
                },
                ProcessType: {
                    required: 'El campo es requerido.',
                },
                UnderwritingArea: {
                    required: 'El campo es requerido.',
                },
                Payer: {
                    required: 'El campo es requerido.',
                },
                Cost: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -999999999999999999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999999'
                },
                Link: {
                    maxlength: 'El campo permite 256 caracteres máximo'
                },
                AcordRequirementCode: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -99999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999'
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
                }

            }
        });
        $("#TabRequirementTypeTranslator_GridEditForm").validate({
            rules: {
                RequirementTypeTranslator: {
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
                RequirementTypeTranslator: {
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

            app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDeRequisitosActions.aspx/LookUpForLineOfBusiness", false,
                JSON.stringify({ id: $('#H5MantTiposDeRequisitosFormId').val() }),
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
            H5MantTiposDeRequisitosSupport.LookUpForProduct(null, row.LineOfBusiness);
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
            
            app.core.SyncWebMethod("/fasi/dli/forms/H5MantTiposDeRequisitosActions.aspx/LookUpForProduct", false,
                JSON.stringify({
                                        id: $('#H5MantTiposDeRequisitosFormId').val(),
                    TabRequirementTypeCollectionLineOfBusiness: value1
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTiposDeRequisitosActions.aspx/LookUpForRecordStatus", false,
                JSON.stringify({ id: $('#H5MantTiposDeRequisitosFormId').val() }),
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
    this.LookUpForProcessTypeFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#ProcessType>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForProcessType = function (defaultValue, source) {
        var ctrol = $('#ProcessType');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTiposDeRequisitosActions.aspx/LookUpForProcessType", false,
                JSON.stringify({ id: $('#H5MantTiposDeRequisitosFormId').val() }),
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
    this.LookUpForUnderwritingAreaFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#UnderwritingArea>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForUnderwritingArea = function (defaultValue, source) {
        var ctrol = $('#UnderwritingArea');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTiposDeRequisitosActions.aspx/LookUpForUnderwritingArea", false,
                JSON.stringify({ id: $('#H5MantTiposDeRequisitosFormId').val() }),
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
    this.LookUpForPayerFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#Payer>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForPayer = function (defaultValue, source) {
        var ctrol = $('#Payer');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTiposDeRequisitosActions.aspx/LookUpForPayer", false,
                JSON.stringify({ id: $('#H5MantTiposDeRequisitosFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTiposDeRequisitosActions.aspx/LookUpForLanguageIdTranslator", false,
                JSON.stringify({ id: $('#H5MantTiposDeRequisitosFormId').val() }),
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

    this.TabRequirementType_GridTblSetup = function (table) {
        H5MantTiposDeRequisitosSupport.LookUpForLineOfBusiness('');
        H5MantTiposDeRequisitosSupport.LookUpForRecordStatus('');
        H5MantTiposDeRequisitosSupport.LookUpForProcessType('');
        H5MantTiposDeRequisitosSupport.LookUpForUnderwritingArea('');
        H5MantTiposDeRequisitosSupport.LookUpForPayer('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'RequirementType',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0,
                pdfmake:{ 
                    enabled : true,
                    docDefinition: {
                        pageOrientation: 'landscape',
                        content : [{
                            layout: {
                                hLineWidth: function (i, node) {
                                    return (i === 0 || i === 1) ? 1 : 0;
                                },
                                vLineWidth: function (i, node) {
                                    return (i === 0 || i === node.table.widths.length) ? 2 : 0;
                                },
                                hLineColor: function (i, node) {
                                    return (i === 0 || i === 1) ? 'black' : 'gray';
                                },
                                vLineColor: function (i, node) {
                                    return (i === 0 || i === node.table.widths.length) ? 'white' : 'gray';
                                },
                                fillColor: function (rowIndex, node, columnIndex) {
                                    return (rowIndex % 2 === 0) ? '#DDEBF7' : null;
                                }
                            }
                        }]
                    } 
                }
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel', 'pdf'],
            toolbar: '#TabRequirementType_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'RequirementType',
                title: 'ID',
                events: 'TabRequirementType_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Description',
                title: 'Nombre',
                sortable: true,
                halign: 'center'
            }, {
                field: 'ShortDescription',
                title: 'Descripción breve',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LineOfBusiness',
                title: 'Ramo',
                formatter: 'H5MantTiposDeRequisitosSupport.LookUpForLineOfBusinessFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Product',
                title: 'Producto',
                formatter: 'H5MantTiposDeRequisitosSupport.LookUpForProductFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'RecordStatus',
                title: 'Estado del registro',
                formatter: 'H5MantTiposDeRequisitosSupport.LookUpForRecordStatusFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'ProcessType',
                title: 'Procesado por',
                formatter: 'H5MantTiposDeRequisitosSupport.LookUpForProcessTypeFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'UnderwritingArea',
                title: 'Área de suscripción',
                formatter: 'H5MantTiposDeRequisitosSupport.LookUpForUnderwritingAreaFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Payer',
                title: 'Pagador',
                formatter: 'H5MantTiposDeRequisitosSupport.LookUpForPayerFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Cost',
                title: 'Costo',
                formatter: 'H5MantTiposDeRequisitosSupport.Cost_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Link',
                title: 'Url',
                sortable: true,
                halign: 'center'
            }, {
                field: 'AcordRequirementCode',
                title: 'Código Acord',
                formatter: 'H5MantTiposDeRequisitosSupport.AcordRequirementCode_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5MantTiposDeRequisitosSupport.CreatorUserCode_FormatterMaskData',
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
                formatter: 'H5MantTiposDeRequisitosSupport.UpdateUserCode_FormatterMaskData',
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
                field: 'AllowViewRequirement',
                title: 'Mostrar botón para visualizar un documento',
                formatter: 'H5MantTiposDeRequisitosSupport.AllowViewRequirement_IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'AllowLoadRequirement',
                title: 'Mostrar botón para cargar un documento',
                formatter: 'H5MantTiposDeRequisitosSupport.AllowLoadRequirement_IsCheck',
                sortable: true,
                halign: 'center'
            }]
        });


        $('#TabRequirementType_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabRequirementType_GridTbl');
            $('#TabRequirementType_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabRequirementType_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabRequirementType_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantTiposDeRequisitosSupport.TabRequirementType_GridRowToInput(row);
                H5MantTiposDeRequisitosSupport.TabRequirementType_Grid_delete(row, null);
                
                return row.RequirementType;
            });

            $('#TabRequirementType_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabRequirementType_GridCreateBtn').click(function () {
            var formInstance = $("#TabRequirementType_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantTiposDeRequisitosSupport.TabRequirementType_GridShowModal($('#TabRequirementType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabRequirementType_GridPopup').find('#TabRequirementType_GridSaveBtn').click(function () {
            var formInstance = $("#TabRequirementType_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabRequirementType_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabRequirementType_GridSaveBtn').html();
                $('#TabRequirementType_GridSaveBtn').html('Procesando...');
                $('#TabRequirementType_GridSaveBtn').prop('disabled', true);

                H5MantTiposDeRequisitosSupport.currentRow.RequirementType = generalSupport.NumericValue('#RequirementType', -99999, 99999);
                H5MantTiposDeRequisitosSupport.currentRow.Description = $('#Description').val();
                H5MantTiposDeRequisitosSupport.currentRow.ShortDescription = $('#ShortDescription').val();
                H5MantTiposDeRequisitosSupport.currentRow.LineOfBusiness = parseInt(0 + $('#LineOfBusiness').val(), 10);
                H5MantTiposDeRequisitosSupport.currentRow.Product = parseInt(0 + $('#Product').val(), 10);
                H5MantTiposDeRequisitosSupport.currentRow.RecordStatus = $('#RecordStatus').val();
                H5MantTiposDeRequisitosSupport.currentRow.ProcessType = parseInt(0 + $('#ProcessType').val(), 10);
                H5MantTiposDeRequisitosSupport.currentRow.UnderwritingArea = parseInt(0 + $('#UnderwritingArea').val(), 10);
                H5MantTiposDeRequisitosSupport.currentRow.Payer = parseInt(0 + $('#Payer').val(), 10);
                H5MantTiposDeRequisitosSupport.currentRow.Cost = generalSupport.NumericValue('#Cost', -999999999999999999, 999999999999999999);
                H5MantTiposDeRequisitosSupport.currentRow.Link = $('#Link').val();
                H5MantTiposDeRequisitosSupport.currentRow.AcordRequirementCode = generalSupport.NumericValue('#AcordRequirementCode', -99999, 99999);
                H5MantTiposDeRequisitosSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                H5MantTiposDeRequisitosSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                H5MantTiposDeRequisitosSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);
                H5MantTiposDeRequisitosSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';
                H5MantTiposDeRequisitosSupport.currentRow.AllowViewRequirement = $('#AllowViewRequirement').is(':checked') ? '1' : '0';
                H5MantTiposDeRequisitosSupport.currentRow.AllowLoadRequirement = $('#AllowLoadRequirement').is(':checked') ? '1' : '0';

                $('#TabRequirementType_GridSaveBtn').prop('disabled', false);
                $('#TabRequirementType_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantTiposDeRequisitosSupport.TabRequirementType_Grid_update(H5MantTiposDeRequisitosSupport.currentRow, $modal);
                }
                else {                    
                    H5MantTiposDeRequisitosSupport.TabRequirementType_Grid_insert(H5MantTiposDeRequisitosSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabRequirementType_GridShowModal = function (md, title, row) {
        row = row || { RequirementType: 0, Description: null, ShortDescription: null, LineOfBusiness: 0, Product: 0, RecordStatus: '', ProcessType: 0, UnderwritingArea: 0, Payer: 0, Cost: 0, Link: null, AcordRequirementCode: 0, CreatorUserCode: 0, CreationDate: null, UpdateUserCode: 0, UpdateDate: null, AllowViewRequirement: 0, AllowLoadRequirement: 0 };

        md.data('id', row.RequirementType);
        md.find('.modal-title').text(title);

        H5MantTiposDeRequisitosSupport.TabRequirementType_GridRowToInput(row);
        $('#RequirementType').prop('disabled', (row.RequirementType !== 0));
        $('#CreatorUserCode').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        H5MantTiposDeRequisitosSupport.TabRequirementType_Grid_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.TabRequirementType_GridRowToInput = function (row) {
        H5MantTiposDeRequisitosSupport.currentRow = row;
        AutoNumeric.set('#RequirementType', row.RequirementType);
        $('#Description').val(row.Description);
        $('#ShortDescription').val(row.ShortDescription);
        H5MantTiposDeRequisitosSupport.LookUpForLineOfBusiness(row.LineOfBusiness, '');
        $('#LineOfBusiness').trigger('change');
        H5MantTiposDeRequisitosSupport.LookUpForProduct(row.Product, row.LineOfBusiness, '');
        $('#Product').trigger('change');
        H5MantTiposDeRequisitosSupport.LookUpForRecordStatus(row.RecordStatus, '');
        $('#RecordStatus').trigger('change');
        H5MantTiposDeRequisitosSupport.LookUpForProcessType(row.ProcessType, '');
        $('#ProcessType').trigger('change');
        H5MantTiposDeRequisitosSupport.LookUpForUnderwritingArea(row.UnderwritingArea, '');
        $('#UnderwritingArea').trigger('change');
        H5MantTiposDeRequisitosSupport.LookUpForPayer(row.Payer, '');
        $('#Payer').trigger('change');
        AutoNumeric.set('#Cost', row.Cost);
        $('#Link').val(row.Link);
        AutoNumeric.set('#AcordRequirementCode', row.AcordRequirementCode);
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#AllowViewRequirement').prop('checked', row.AllowViewRequirement === '1' ? true : false);
        $('#AllowLoadRequirement').prop('checked', row.AllowLoadRequirement === '1' ? true : false);

    };
    this.TabRequirementType_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTiposDeRequisitosActions.aspx/TabRequirementType_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                TransRequirementTypeLanguageId1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#TabRequirementType_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.TabRequirementTypeTranslator_GridTblSetup = function (table) {
        H5MantTiposDeRequisitosSupport.LookUpForLanguageIdTranslator('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'RequirementType',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0,
                pdfmake:{ 
                    enabled : true,
                    docDefinition: {
                        pageOrientation: 'landscape',
                        content : [{
                            layout: {
                                hLineWidth: function (i, node) {
                                    return (i === 0 || i === 1) ? 1 : 0;
                                },
                                vLineWidth: function (i, node) {
                                    return (i === 0 || i === node.table.widths.length) ? 2 : 0;
                                },
                                hLineColor: function (i, node) {
                                    return (i === 0 || i === 1) ? 'black' : 'gray';
                                },
                                vLineColor: function (i, node) {
                                    return (i === 0 || i === node.table.widths.length) ? 'white' : 'gray';
                                },
                                fillColor: function (rowIndex, node, columnIndex) {
                                    return (rowIndex % 2 === 0) ? '#DDEBF7' : null;
                                }
                            }
                        }]
                    } 
                }
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel', 'pdf'],
            toolbar: '#TabRequirementTypeTranslator_Gridtoolbar',
            columns: [{
                field: 'RequirementType',
                title: 'Tipo requisito',
                formatter: 'H5MantTiposDeRequisitosSupport.RequirementTypeTranslator_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LanguageId',
                title: 'Idioma',
                formatter: 'H5MantTiposDeRequisitosSupport.LookUpForLanguageIdTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Description',
                title: 'Descripción',
                events: 'TabRequirementTypeTranslator_GridActionEvents',
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


        $('#TabRequirementTypeTranslator_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabRequirementTypeTranslator_GridTbl');
            $('#TabRequirementTypeTranslator_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabRequirementTypeTranslator_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabRequirementTypeTranslator_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantTiposDeRequisitosSupport.TabRequirementTypeTranslator_GridRowToInput(row);
                
                
                return row.RequirementType;
            });
            
          $('#TabRequirementTypeTranslator_GridTbl').bootstrapTable('remove', {
                field: 'RequirementType',
                values: ids
           });

            $('#TabRequirementTypeTranslator_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabRequirementTypeTranslator_GridCreateBtn').click(function () {
            var formInstance = $("#TabRequirementTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantTiposDeRequisitosSupport.TabRequirementTypeTranslator_GridShowModal($('#TabRequirementTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabRequirementTypeTranslator_GridPopup').find('#TabRequirementTypeTranslator_GridSaveBtn').click(function () {
            var formInstance = $("#TabRequirementTypeTranslator_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabRequirementTypeTranslator_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabRequirementTypeTranslator_GridSaveBtn').html();
                $('#TabRequirementTypeTranslator_GridSaveBtn').html('Procesando...');
                $('#TabRequirementTypeTranslator_GridSaveBtn').prop('disabled', true);

                H5MantTiposDeRequisitosSupport.currentRow.RequirementType = generalSupport.NumericValue('#RequirementTypeTranslator', -99999, 99999);
                H5MantTiposDeRequisitosSupport.currentRow.LanguageId = parseInt(0 + $('#LanguageIdTranslator').val(), 10);
                H5MantTiposDeRequisitosSupport.currentRow.Description = $('#DescriptionTranslator').val();
                H5MantTiposDeRequisitosSupport.currentRow.ShortDescription = $('#ShortDescriptionTranslator').val();

                $('#TabRequirementTypeTranslator_GridSaveBtn').prop('disabled', false);
                $('#TabRequirementTypeTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantTiposDeRequisitosSupport.TabRequirementTypeTranslator_Grid_update(H5MantTiposDeRequisitosSupport.currentRow, $modal);
                }
                else {                    
                    $('#TabRequirementTypeTranslator_GridTbl').bootstrapTable('append', H5MantTiposDeRequisitosSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabRequirementTypeTranslator_GridShowModal = function (md, title, row) {
        row = row || { RequirementType: 0, LanguageId: 0, Description: null, ShortDescription: null };

        md.data('id', row.RequirementType);
        md.find('.modal-title').text(title);

        H5MantTiposDeRequisitosSupport.TabRequirementTypeTranslator_GridRowToInput(row);
        $('#RequirementTypeTranslator').prop('disabled', true);
        $('#LanguageIdTranslator').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabRequirementTypeTranslator_GridRowToInput = function (row) {
        H5MantTiposDeRequisitosSupport.currentRow = row;
        AutoNumeric.set('#RequirementTypeTranslator', row.RequirementType);
        H5MantTiposDeRequisitosSupport.LookUpForLanguageIdTranslator(row.LanguageId, '');
        $('#LanguageIdTranslator').trigger('change');
        $('#DescriptionTranslator').val(row.Description);
        $('#ShortDescriptionTranslator').val(row.ShortDescription);

    };
    this.TabRequirementTypeTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTiposDeRequisitosActions.aspx/TabRequirementTypeTranslator_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#TabRequirementTypeTranslator_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

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
    this.Cost_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 0,
            minimumValue: -999999999999999999
        });
      };
    this.AcordRequirementCode_FormatterMaskData = function (value, row, index) {          
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
    this.RequirementTypeTranslator_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };


	    this.AllowViewRequirement_IsCheck = function (value, row, index) {
        var icon = "";
        
        if (value === '0') {
            icon = "glyphicon glyphicon-unchecked";
        } else {
            icon = "glyphicon glyphicon-check";
        }
       
        return '<div class="text-center" >' +
            '<i class="' + icon + '"></i>' +
            '</div>';
      };
	    this.AllowLoadRequirement_IsCheck = function (value, row, index) {
        var icon = "";
        
        if (value === '0') {
            icon = "glyphicon glyphicon-unchecked";
        } else {
            icon = "glyphicon glyphicon-check";
        }
       
        return '<div class="text-center" >' +
            '<i class="' + icon + '"></i>' +
            '</div>';
      };



  this.Init = function(){
    securitySupport.ValidateAccessRoles(['EASE1', 'Suscriptor']);
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('Tipo de requisito: cuestionarios, examenes, etc');
        

    H5MantTiposDeRequisitosSupport.ControlBehaviour();
    H5MantTiposDeRequisitosSupport.ControlActions();
    H5MantTiposDeRequisitosSupport.ValidateSetup();

    $("#TabRequirementType_GridTblPlaceHolder").replaceWith('<table id="TabRequirementType_GridTbl"></table>');
    H5MantTiposDeRequisitosSupport.TabRequirementType_GridTblSetup($('#TabRequirementType_GridTbl'));
    $("#TabRequirementTypeTranslator_GridTblPlaceHolder").replaceWith('<table id="TabRequirementTypeTranslator_GridTbl"></table>');
    H5MantTiposDeRequisitosSupport.TabRequirementTypeTranslator_GridTblSetup($('#TabRequirementTypeTranslator_GridTbl'));

        H5MantTiposDeRequisitosSupport.TabRequirementType_GridTblRequest();
        H5MantTiposDeRequisitosSupport.TabRequirementTypeTranslator_GridTblRequest();




  };
};

$(document).ready(function () {
   H5MantTiposDeRequisitosSupport.Init();
});

window.TabRequirementType_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantTiposDeRequisitosSupport.TabRequirementType_GridShowModal($('#TabRequirementType_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.TabRequirementTypeTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantTiposDeRequisitosSupport.TabRequirementTypeTranslator_GridShowModal($('#TabRequirementTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
