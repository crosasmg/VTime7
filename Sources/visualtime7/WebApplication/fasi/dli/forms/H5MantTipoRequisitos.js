var H5MantTipoRequisitosSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5MantTipoRequisitosFormId').val(),
            TabRequirementType_TabRequirementType: generalSupport.NormalizeProperties($('#TabRequirementTypeTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate'),
            TabRequirementTypeTranslator_Grid_TabRequirementType_Item: generalSupport.NormalizeProperties($('#TabRequirementTypeTranslator_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#H5MantTipoRequisitosFormId').val(data.InstanceFormId);

        H5MantTipoRequisitosSupport.LookUpForLineOfBusiness(source);

        H5MantTipoRequisitosSupport.TabRequirementTypeTblRequest();
        if (data.TabRequirementType_TabRequirementType !== null)
            $('#TabRequirementTypeTbl').bootstrapTable('load', data.TabRequirementType_TabRequirementType);
        H5MantTipoRequisitosSupport.TabRequirementTypeTranslator_GridTblRequest();
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
      new AutoNumeric('#ProcessType', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      new AutoNumeric('#UnderwritingArea', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      new AutoNumeric('#Payer', {
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
      new AutoNumeric('#VerDocumentInt', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9,
            decimalPlaces: 0,
            minimumValue: -9
        });
      new AutoNumeric('#CargaDocumentoInt', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9,
            decimalPlaces: 0,
            minimumValue: -9
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
                    H5MantTipoRequisitosSupport.LookUpForProduct(null, parseInt(0 + $('#LineOfBusiness').val(), 10));
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
                H5MantTipoRequisitosSupport.ObjectToInput(data.d.Data, source);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.TabRequirementType_insert = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoRequisitosActions.aspx/TabRequirementType1InsertCommandActionTABREQUIREMENTTYPE", false,
               JSON.stringify({ REQUIREMENTTYPE1: row.RequirementType, RECORDSTATUS2: row.RecordStatus, PROCESSTYPE3: row.ProcessType, UNDERWRITINGAREA4: row.UnderwritingArea, PAYER5: row.Payer, COST6: row.Cost, CREATORUSERCODE6: generalSupport.UserContext().userId, UPDATEUSERCODE8: generalSupport.UserContext().userId, ACORDREQUIREMENTCODE11: row.AcordRequirementCode, LINK12: row.Link, LINEOFBUSINESS13: row.LineOfBusiness, PRODUCT14: row.Product, ALLOWVIEWREQUIREMENT15: row.AllowViewRequirement, ALLOWLOADREQUIREMENT16: row.AllowLoadRequirement }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoRequisitosActions.aspx/TabRequirementType3InsertCommandActionTransRequirementType", false,
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
    this.TabRequirementType_update = function (row, $modal) {
          var data;
            var recordCount;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoRequisitosActions.aspx/TabRequirementType1UpdateCommandActionTABREQUIREMENTTYPE", false,
               JSON.stringify({ RECORDSTATUS1: row.RecordStatus, PROCESSTYPE2: row.ProcessType, UNDERWRITINGAREA3: row.UnderwritingArea, PAYER4: row.Payer, COST5: row.Cost, UPDATEUSERCODE5: generalSupport.UserContext().userId, ACORDREQUIREMENTCODE7: row.AcordRequirementCode, LINK8: row.Link, LINEOFBUSINESS9: row.LineOfBusiness, PRODUCT10: row.Product, ALLOWVIEWREQUIREMENT11: row.AllowViewRequirement, ALLOWLOADREQUIREMENT12: row.AllowLoadRequirement, TABREQUIREMENTTYPEREQUIREMENTTYPE13: row.RequirementType }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoRequisitosActions.aspx/TabRequirementType3SelectCommandActionTransRequirementType", false,
               JSON.stringify({                 TransRequirementTypeRequirementType1: row.RequirementType,
                TransRequirementTypeLanguageId2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoRequisitosActions.aspx/TabRequirementType5InsertCommandActionTransRequirementType", false,
               JSON.stringify({ REQUIREMENTTYPE1: row.RequirementType, LANGUAGEID1: generalSupport.SessionContext().languageId, DESCRIPTION3: row.Description, SHORTDESCRIPTION4: row.ShortDescription, CREATORUSERCODE4: generalSupport.UserContext().userId, UPDATEUSERCODE6: generalSupport.UserContext().userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoRequisitosActions.aspx/TabRequirementType6UpdateCommandActionTransRequirementType", false,
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
    this.TabRequirementType_delete = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoRequisitosActions.aspx/TabRequirementType1DeleteCommandActionTransRequirementType", false,
               JSON.stringify({ TransRequirementTypeRequirementType1: row.RequirementType }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoRequisitosActions.aspx/TabRequirementType3DeleteCommandActionTabRequirementType", false,
               JSON.stringify({ TabRequirementTypeRequirementType1: row.RequirementType }));
               

            }            
            else {
            var message4 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message4);

                }
        if (data.d.Success === true){
            var message7 = 'Se eliminó correctamente el registro';
            notification.toastr.success('Eliminar registro', message7);
                    }                    
                    else {
            var message8 = 'No se puede eliminar el registro';
            notification.toastr.error('Eliminar registro', message8);

                        }

    };
    this.TabRequirementType_BeforeShowPopup = function (row, $modal) {
          var data;
            var nextId;
        if (row.RequirementType === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoRequisitosActions.aspx/TabRequirementType2SelectCommandActionTabRequirementType", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#RequirementType', nextId);

            }

    };
    this.TabRequirementTypeTranslator_Grid_update = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoRequisitosActions.aspx/TabRequirementTypeTranslator_Grid1UpdateCommandActionTransRequirementType", false,
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

    this.ControlActions = function () {

        $('#ShowStandardGrid').click(function (event) {
            var formInstance = $("#H5MantTipoRequisitosMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                H5MantTipoRequisitosSupport.TabRequirementType_GridTblRequest();
                $('#TabRequirementType_GridContainer').toggleClass('hidden', false);
                $('#TabRequirementTypeTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#H5MantTipoRequisitosMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                H5MantTipoRequisitosSupport.TabRequirementTypeTranslator_GridTblRequest();
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


        $("#H5MantTipoRequisitosMainForm").validate({
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
        $("#TabRequirementTypeEditForm").validate({
            rules: {
                RequirementType: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999,
                    required: true
                },
                ProcessType: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999,
                    required: true
                },
                UnderwritingArea: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999,
                    required: true
                },
                Payer: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999,
                    required: true
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
                RecordStatus: {
                    required: true
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
                VerDocumentInt: {
                    AutoNumericMinValue: -9,
                    AutoNumericMaxValue: 9
                },
                CargaDocumentoInt: {
                    AutoNumericMinValue: -9,
                    AutoNumericMaxValue: 9
                }

            },
            messages: {
                RequirementType: {
                    AutoNumericMinValue: $.i18n.t('app.validation.RequirementType.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.RequirementType.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.RequirementType.required')
                },
                ProcessType: {
                    AutoNumericMinValue: $.i18n.t('app.validation.ProcessType.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.ProcessType.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.ProcessType.required')
                },
                UnderwritingArea: {
                    AutoNumericMinValue: $.i18n.t('app.validation.UnderwritingArea.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.UnderwritingArea.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.UnderwritingArea.required')
                },
                Payer: {
                    AutoNumericMinValue: $.i18n.t('app.validation.Payer.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.Payer.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.Payer.required')
                },
                Cost: {
                    AutoNumericMinValue: $.i18n.t('app.validation.Cost.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.Cost.AutoNumericMaxValue')
                },
                Link: {
                    maxlength: $.i18n.t('app.validation.Link.maxlength')
                },
                AcordRequirementCode: {
                    AutoNumericMinValue: $.i18n.t('app.validation.AcordRequirementCode.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.AcordRequirementCode.AutoNumericMaxValue')
                },
                RecordStatus: {
                    required: $.i18n.t('app.validation.RecordStatus.required')
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
                },
                VerDocumentInt: {
                    AutoNumericMinValue: $.i18n.t('app.validation.VerDocumentInt.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.VerDocumentInt.AutoNumericMaxValue')
                },
                CargaDocumentoInt: {
                    AutoNumericMinValue: $.i18n.t('app.validation.CargaDocumentoInt.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.CargaDocumentoInt.AutoNumericMaxValue')
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
                    required: true
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
                    AutoNumericMinValue: $.i18n.t('app.validation.RequirementTypeTranslator.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.RequirementTypeTranslator.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.RequirementTypeTranslator.required')
                },
                LanguageIdTranslator: {
                    required: $.i18n.t('app.validation.LanguageIdTranslator.required')
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
    this.LookUpForProductFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            H5MantTipoRequisitosSupport.LookUpForProduct(null, row.LineOfBusiness);
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
            
            app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoRequisitosActions.aspx/LookUpForProduct", false,
                JSON.stringify({
                                        id: $('#H5MantTipoRequisitosFormId').val(),
                    TabRequirementTypeCollection1LineOfBusiness: value1
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTipoRequisitosActions.aspx/LookUpForRecordStatus", false,
                JSON.stringify({ id: $('#H5MantTipoRequisitosFormId').val() }),
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
    this.LookUpForAllowLoadRequirementFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#AllowLoadRequirement>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForAllowViewRequirementFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#AllowViewRequirement>option[value='" + value + "']").text();
        }
        return result;
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

            app.core.SyncWebMethod("/fasi/dli/forms/H5MantTipoRequisitosActions.aspx/LookUpForLineOfBusiness", false,
                JSON.stringify({ id: $('#H5MantTipoRequisitosFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTipoRequisitosActions.aspx/LookUpForLanguageIdTranslator", false,
                JSON.stringify({ id: $('#H5MantTipoRequisitosFormId').val() }),
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

    this.TabRequirementTypeTblSetup = function (table) {
        H5MantTipoRequisitosSupport.LookUpForRecordStatus('');
        H5MantTipoRequisitosSupport.LookUpForLineOfBusiness('');
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
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
        toolbar: '#TabRequirementTypetoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'RequirementType',
                title: 'Tipo requisito',
                events: 'TabRequirementTypeActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'ProcessType',
                title: 'Procesado por',
                formatter: 'H5MantTipoRequisitosSupport.ProcessType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'UnderwritingArea',
                title: 'Área de suscripción',
                formatter: 'H5MantTipoRequisitosSupport.UnderwritingArea_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'Payer',
                title: 'Pagador',
                formatter: 'H5MantTipoRequisitosSupport.Payer_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'Cost',
                title: 'Costo',
                formatter: 'H5MantTipoRequisitosSupport.Cost_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'Link',
                title: 'Url',
                sortable: true,
                halign: 'center'
            }, {
                field: 'AcordRequirementCode',
                title: 'Código Acord',
                formatter: 'H5MantTipoRequisitosSupport.AcordRequirementCode_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'Product',
                title: 'Producto',
                formatter: 'H5MantTipoRequisitosSupport.LookUpForProductFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'RecordStatus',
                title: 'Estado del registro',
                formatter: 'H5MantTipoRequisitosSupport.LookUpForRecordStatusFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5MantTipoRequisitosSupport.CreatorUserCode_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
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
                formatter: 'H5MantTipoRequisitosSupport.UpdateUserCode_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
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
                field: 'RoleCode',
                title: 'VerDocumentInt',
                formatter: 'H5MantTipoRequisitosSupport.VerDocumentInt_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'Tag',
                title: 'CargaDocumentoInt',
                formatter: 'H5MantTipoRequisitosSupport.CargaDocumentoInt_FormatterMaskData',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'AllowLoadRequirement',
                title: 'Mostrar botón para cargar un documento',
                formatter: 'H5MantTipoRequisitosSupport.LookUpForAllowLoadRequirementFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'AllowViewRequirement',
                title: 'Mostrar botón para visualizar un documento',
                formatter: 'H5MantTipoRequisitosSupport.LookUpForAllowViewRequirementFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LineOfBusiness',
                title: 'Ramo',
                formatter: 'H5MantTipoRequisitosSupport.LookUpForLineOfBusinessFormatter',
                sortable: true,
                halign: 'center'
            }]
        });


        $('#TabRequirementTypeTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabRequirementTypeTbl');
            $('#TabRequirementTypeRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabRequirementTypeRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabRequirementTypeTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantTipoRequisitosSupport.TabRequirementTypeRowToInput(row);
                H5MantTipoRequisitosSupport.TabRequirementType_delete(row, null);
                
                return row.RequirementType;
            });

            $('#TabRequirementTypeRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabRequirementTypeCreateBtn').click(function () {
            var formInstance = $("#TabRequirementTypeEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantTipoRequisitosSupport.TabRequirementTypeShowModal($('#TabRequirementTypePopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabRequirementTypePopup').find('#TabRequirementTypeSaveBtn').click(function () {
            var formInstance = $("#TabRequirementTypeEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabRequirementTypePopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabRequirementTypeSaveBtn').html();
                $('#TabRequirementTypeSaveBtn').html('Procesando...');
                $('#TabRequirementTypeSaveBtn').prop('disabled', true);

                H5MantTipoRequisitosSupport.currentRow.RequirementType = generalSupport.NumericValue('#RequirementType', -99999, 99999);
                H5MantTipoRequisitosSupport.currentRow.ProcessType = generalSupport.NumericValue('#ProcessType', -99999, 99999);
                H5MantTipoRequisitosSupport.currentRow.UnderwritingArea = generalSupport.NumericValue('#UnderwritingArea', -99999, 99999);
                H5MantTipoRequisitosSupport.currentRow.Payer = generalSupport.NumericValue('#Payer', -99999, 99999);
                H5MantTipoRequisitosSupport.currentRow.Cost = generalSupport.NumericValue('#Cost', -999999999999999999, 999999999999999999);
                H5MantTipoRequisitosSupport.currentRow.Link = $('#Link').val();
                H5MantTipoRequisitosSupport.currentRow.AcordRequirementCode = generalSupport.NumericValue('#AcordRequirementCode', -99999, 99999);
                H5MantTipoRequisitosSupport.currentRow.Product = parseInt(0 + $('#Product').val(), 10);
                H5MantTipoRequisitosSupport.currentRow.RecordStatus = $('#RecordStatus').val();
                H5MantTipoRequisitosSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                H5MantTipoRequisitosSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                H5MantTipoRequisitosSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);
                H5MantTipoRequisitosSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';
                H5MantTipoRequisitosSupport.currentRow.Description = $('#Description').val();
                H5MantTipoRequisitosSupport.currentRow.ShortDescription = $('#ShortDescription').val();
                H5MantTipoRequisitosSupport.currentRow.RoleCode = generalSupport.NumericValue('#VerDocumentInt', -9, 9);
                H5MantTipoRequisitosSupport.currentRow.Tag = generalSupport.NumericValue('#CargaDocumentoInt', -9, 9);
                H5MantTipoRequisitosSupport.currentRow.AllowLoadRequirement = parseInt(0 + $('#AllowLoadRequirement').val(), 10);
                H5MantTipoRequisitosSupport.currentRow.AllowViewRequirement = parseInt(0 + $('#AllowViewRequirement').val(), 10);
                H5MantTipoRequisitosSupport.currentRow.LineOfBusiness = parseInt(0 + $('#LineOfBusiness').val(), 10);

                $('#TabRequirementTypeSaveBtn').prop('disabled', false);
                $('#TabRequirementTypeSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantTipoRequisitosSupport.TabRequirementType_update(H5MantTipoRequisitosSupport.currentRow, $modal);
                }
                else {                    
                    H5MantTipoRequisitosSupport.TabRequirementType_insert(H5MantTipoRequisitosSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });
    };

    this.TabRequirementTypeShowModal = function (md, title, row) {
        row = row || { RequirementType: 0, ProcessType: 0, UnderwritingArea: 0, Payer: 0, Cost: 0, Link: null, AcordRequirementCode: 0, Product: 0, RecordStatus: '', CreatorUserCode: 0, CreationDate: null, UpdateUserCode: 0, UpdateDate: null, Description: null, ShortDescription: null, RoleCode: 1, Tag: null, AllowLoadRequirement: 0, AllowViewRequirement: 0, LineOfBusiness: 0 };

        md.data('id', row.RequirementType);
        md.find('.modal-title').text(title);

        H5MantTipoRequisitosSupport.TabRequirementTypeRowToInput(row);
        $('#RequirementType').prop('disabled', (row.RequirementType !== 0));
        $('#CreatorUserCode').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        $('#VerDocumentInt').prop('disabled', true);
        $('#CargaDocumentoInt').prop('disabled', true);
        H5MantTipoRequisitosSupport.TabRequirementType_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.TabRequirementTypeRowToInput = function (row) {
        H5MantTipoRequisitosSupport.currentRow = row;
        AutoNumeric.set('#RequirementType', row.RequirementType);
        AutoNumeric.set('#ProcessType', row.ProcessType);
        AutoNumeric.set('#UnderwritingArea', row.UnderwritingArea);
        AutoNumeric.set('#Payer', row.Payer);
        AutoNumeric.set('#Cost', row.Cost);
        $('#Link').val(row.Link);
        AutoNumeric.set('#AcordRequirementCode', row.AcordRequirementCode);
        H5MantTipoRequisitosSupport.LookUpForProduct(row.Product, row.LineOfBusiness, '');
        $('#Product').trigger('change');
        H5MantTipoRequisitosSupport.LookUpForRecordStatus(row.RecordStatus, '');
        $('#RecordStatus').trigger('change');
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#Description').val(row.Description);
        $('#ShortDescription').val(row.ShortDescription);
        AutoNumeric.set('#VerDocumentInt', row.RoleCode);
        AutoNumeric.set('#CargaDocumentoInt', row.Tag);
        $('#AllowLoadRequirement').val(row.AllowLoadRequirement);
        $('#AllowLoadRequirement').trigger('change');
        $('#AllowViewRequirement').val(row.AllowViewRequirement);
        $('#AllowViewRequirement').trigger('change');
        H5MantTipoRequisitosSupport.LookUpForLineOfBusiness(row.LineOfBusiness, '');
        $('#LineOfBusiness').trigger('change');

    };
    this.TabRequirementTypeTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTipoRequisitosActions.aspx/TabRequirementTypeTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                TransRequirementTypeLanguageId1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#TabRequirementTypeTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.TabRequirementTypeTranslator_GridTblSetup = function (table) {
        H5MantTipoRequisitosSupport.LookUpForLanguageIdTranslator('');
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
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
        toolbar: '#TabRequirementTypeTranslator_Gridtoolbar',
            columns: [{
                field: 'RequirementType',
                title: 'Tipo requisito',
                formatter: 'H5MantTipoRequisitosSupport.RequirementTypeTranslator_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LanguageId',
                title: 'Idioma',
                formatter: 'H5MantTipoRequisitosSupport.LookUpForLanguageIdTranslatorFormatter',
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
                H5MantTipoRequisitosSupport.TabRequirementTypeTranslator_GridRowToInput(row);
                
                
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
            H5MantTipoRequisitosSupport.TabRequirementTypeTranslator_GridShowModal($('#TabRequirementTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
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

                H5MantTipoRequisitosSupport.currentRow.RequirementType = generalSupport.NumericValue('#RequirementTypeTranslator', -99999, 99999);
                H5MantTipoRequisitosSupport.currentRow.LanguageId = parseInt(0 + $('#LanguageIdTranslator').val(), 10);
                H5MantTipoRequisitosSupport.currentRow.Description = $('#DescriptionTranslator').val();
                H5MantTipoRequisitosSupport.currentRow.ShortDescription = $('#ShortDescriptionTranslator').val();

                $('#TabRequirementTypeTranslator_GridSaveBtn').prop('disabled', false);
                $('#TabRequirementTypeTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantTipoRequisitosSupport.TabRequirementTypeTranslator_Grid_update(H5MantTipoRequisitosSupport.currentRow, $modal);
                }
                else {                    
                    $('#TabRequirementTypeTranslator_GridTbl').bootstrapTable('append', H5MantTipoRequisitosSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });
    };

    this.TabRequirementTypeTranslator_GridShowModal = function (md, title, row) {
        row = row || { RequirementType: 0, LanguageId: null, Description: null, ShortDescription: null };

        md.data('id', row.RequirementType);
        md.find('.modal-title').text(title);

        H5MantTipoRequisitosSupport.TabRequirementTypeTranslator_GridRowToInput(row);
        $('#RequirementTypeTranslator').prop('disabled', true);
        $('#LanguageIdTranslator').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabRequirementTypeTranslator_GridRowToInput = function (row) {
        H5MantTipoRequisitosSupport.currentRow = row;
        AutoNumeric.set('#RequirementTypeTranslator', row.RequirementType);
        H5MantTipoRequisitosSupport.LookUpForLanguageIdTranslator(row.LanguageId, '');
        $('#LanguageIdTranslator').trigger('change');
        $('#DescriptionTranslator').val(row.Description);
        $('#ShortDescriptionTranslator').val(row.ShortDescription);

    };
    this.TabRequirementTypeTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantTipoRequisitosActions.aspx/TabRequirementTypeTranslator_GridTblDataLoad", false,
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
    this.ProcessType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
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
    this.Payer_FormatterMaskData = function (value, row, index) {          
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
    this.VerDocumentInt_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9,
            decimalPlaces: 0,
            minimumValue: -9
        });
      };
    this.CargaDocumentoInt_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9,
            decimalPlaces: 0,
            minimumValue: -9
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


};
$(function ($)
 {
    securitySupport.ValidateAccessRoles(['EASE1', 'Suscriptor']);
});
$(document).ready(function () {
    moment.locale(generalSupport.UserContext().languageName);
    
   generalSupport.TranslateInit(generalSupport.GetCurrentName(), function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        H5MantTipoRequisitosSupport.ValidateSetup();
        tableHelperSupport.Translate('#TabRequirementTypeTbl', '#TabRequirementTypeTbl');
tableHelperSupport.Translate('#TabRequirementTypeTranslator_GridTbl', '#TabRequirementTypeTranslator_GridTbl');

    });
        

    H5MantTipoRequisitosSupport.ControlBehaviour();
    H5MantTipoRequisitosSupport.ControlActions();
    


    $("#TabRequirementTypeTblPlaceHolder").replaceWith('<table id="TabRequirementTypeTbl"><caption data-i18n="app.form.TabRequirementType_Title" >TabRequirementType</caption></table>');
    H5MantTipoRequisitosSupport.TabRequirementTypeTblSetup($('#TabRequirementTypeTbl'));
    $("#TabRequirementTypeTranslator_GridTblPlaceHolder").replaceWith('<table id="TabRequirementTypeTranslator_GridTbl"></table>');
    H5MantTipoRequisitosSupport.TabRequirementTypeTranslator_GridTblSetup($('#TabRequirementTypeTranslator_GridTbl'));

        H5MantTipoRequisitosSupport.TabRequirementTypeTblRequest();
        H5MantTipoRequisitosSupport.TabRequirementTypeTranslator_GridTblRequest();




});

window.TabRequirementTypeActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantTipoRequisitosSupport.TabRequirementTypeShowModal($('#TabRequirementTypePopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.TabRequirementTypeTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantTipoRequisitosSupport.TabRequirementTypeTranslator_GridShowModal($('#TabRequirementTypeTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
