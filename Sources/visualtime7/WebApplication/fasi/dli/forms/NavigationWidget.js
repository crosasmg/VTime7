var NavigationWidgetSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#NavigationWidgetFormId').val(),
            NAVIGATIONDIRECTORY_Grid_NAVIGATIONDIRECTORY_Item: generalSupport.NormalizeProperties($('#NAVIGATIONDIRECTORY_GridTbl').bootstrapTable('getData'), ''),
            NAVIGATIONDIRECTORYTranslator_Grid_NAVIGATIONDIRECTORY_Item: generalSupport.NormalizeProperties($('#NAVIGATIONDIRECTORYTranslator_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#NavigationWidgetFormId').val(data.InstanceFormId);

        NavigationWidgetSupport.LookUpForCATEGORYCODE(source);
        NavigationWidgetSupport.LookUpForSTATUS(source);
        NavigationWidgetSupport.LookUpForCATEGORYCODETranslator(source);
        NavigationWidgetSupport.LookUpForSTATUSTranslator(source);
        NavigationWidgetSupport.LookUpForLANGUAGEIDTranslator(source);

        NavigationWidgetSupport.NAVIGATIONDIRECTORY_GridTblRequest();
        if (data.NAVIGATIONDIRECTORY_Grid_NAVIGATIONDIRECTORY_Item !== null)
            $('#NAVIGATIONDIRECTORY_GridTbl').bootstrapTable('load', data.NAVIGATIONDIRECTORY_Grid_NAVIGATIONDIRECTORY_Item);
        NavigationWidgetSupport.NAVIGATIONDIRECTORYTranslator_GridTblRequest();
        if (data.NAVIGATIONDIRECTORYTranslator_Grid_NAVIGATIONDIRECTORY_Item !== null)
            $('#NAVIGATIONDIRECTORYTranslator_GridTbl').bootstrapTable('load', data.NAVIGATIONDIRECTORYTranslator_Grid_NAVIGATIONDIRECTORY_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#ID', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
        });
      new AutoNumeric('#IDTranslator', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
        });






    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               NavigationWidgetSupport.ObjectToInput(data.d.Data.Instance, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };


    this.CallRenderLookUps = function (data) {
          if (data.d.Success === true && data.d.Data.LookUps) {

              data.d.Data.LookUps.forEach(function (elementSource) {
              generalSupport.RenderLookUp(elementSource.Key, data.d.Data.Instance[elementSource.Key], 'Initialization', elementSource.Items, false);
 
              });
          }
     };



    this.NAVIGATIONDIRECTORY_Grid_insert = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#NAVIGATIONDIRECTORY_GridSaveBtn'));          
            var errors;
        data = app.core.SyncWebMethod("/fasi/dli/forms/NavigationWidgetActions.aspx/NAVIGATIONDIRECTORY_Grid1InsertCommandActionNAVIGATIONDIRECTORY", false,
               JSON.stringify({ ID1: row.ID, NAME2: row.NAME, URLPATH3: row.URLPATH, CATEGORYCODE4: row.CATEGORYCODE, IMAGEFILE5: row.IMAGEFILE, STATUS7: row.STATUS, CREATORUSERCODE7: app.user.userId, UPDATEUSERCODE9: app.user.userId, MODELID12: row.MODELID }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/NavigationWidgetActions.aspx/NAVIGATIONDIRECTORY_Grid3InsertCommandActionNAVIGATIONDIRECTORYDESC", false,
               JSON.stringify({ ID1: row.ID, LANGUAGEID1: generalSupport.SessionContext().languageId, TITLE3: row.TITLE, DESCRIPTION4: row.DESCRIPTION, CREATORUSERCODE5: app.user.userId, UPDATEUSERCODE6: app.user.userId }));
               

            }            
            else {
            var message4 = $.i18n.t('app.form.NAVIGATIONDIRECTORY_Grid_Message_Notify_insert4');
            notification.swal.error($.i18n.t('app.form.NAVIGATIONDIRECTORY_Grid_Title_Notify_insert4'), message4);

                }
        if (data.d.Success === true){
            $('#NAVIGATIONDIRECTORY_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message7 = $.i18n.t('app.form.NAVIGATIONDIRECTORY_Grid_Message_Notify_insert7');
            notification.toastr.success($.i18n.t('app.form.NAVIGATIONDIRECTORY_Grid_Title_Notify_insert7'), message7);
                    }                    
                    else {
            var message8 = $.i18n.t('app.form.NAVIGATIONDIRECTORY_Grid_Message_Notify_insert8');
            notification.swal.error($.i18n.t('app.form.NAVIGATIONDIRECTORY_Grid_Title_Notify_insert8'), message8);

                        }

    };
    this.NAVIGATIONDIRECTORY_Grid_update = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#NAVIGATIONDIRECTORY_GridSaveBtn'));          
            var recordCount;
            var errors;
        data = app.core.SyncWebMethod("/fasi/dli/forms/NavigationWidgetActions.aspx/NAVIGATIONDIRECTORY_Grid1UpdateCommandActionNAVIGATIONDIRECTORY", false,
               JSON.stringify({ NAME1: row.NAME, URLPATH2: row.URLPATH, CATEGORYCODE3: row.CATEGORYCODE, IMAGEFILE4: row.IMAGEFILE, STATUS6: row.STATUS, UPDATEUSERCODE6: app.user.userId, MODELID9: row.MODELID, NAVIGATIONDIRECTORYID10: row.ID }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/NavigationWidgetActions.aspx/NAVIGATIONDIRECTORY_Grid3SelectCommandActionNAVIGATIONDIRECTORYDESC", false,
               JSON.stringify({                 NAVIGATIONDIRECTORYDESCID1: row.ID,
                NAVIGATIONDIRECTORYDESCLANGUAGEID2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/NavigationWidgetActions.aspx/NAVIGATIONDIRECTORY_Grid5InsertCommandActionNAVIGATIONDIRECTORYDESC", false,
               JSON.stringify({ ID1: row.ID, LANGUAGEID1: generalSupport.SessionContext().languageId, TITLE3: row.TITLE, DESCRIPTION4: row.DESCRIPTION, CREATORUSERCODE4: app.user.userId, UPDATEUSERCODE6: app.user.userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/NavigationWidgetActions.aspx/NAVIGATIONDIRECTORY_Grid6UpdateCommandActionNAVIGATIONDIRECTORYDESC", false,
               JSON.stringify({ TITLE1: row.TITLE, DESCRIPTION2: row.DESCRIPTION, UPDATEUSERCODE2: app.user.userId, NAVIGATIONDIRECTORYDESCID5: row.ID, NAVIGATIONDIRECTORYDESCLANGUAGEID6: generalSupport.SessionContext().languageId }));
               


                        }

                    }
        if (data.d.Success === true){
            $('#NAVIGATIONDIRECTORY_GridTbl').bootstrapTable('updateByUniqueId', { id: row.ID, row: row });
            $modal.modal('hide');
            var message9 = $.i18n.t('app.form.NAVIGATIONDIRECTORY_Grid_Message_Notify_update9');
            notification.toastr.success($.i18n.t('app.form.NAVIGATIONDIRECTORY_Grid_Title_Notify_update9'), message9);
                        }                        
                        else {
            var message10 = $.i18n.t('app.form.NAVIGATIONDIRECTORY_Grid_Message_Notify_update10');
            notification.swal.error($.i18n.t('app.form.NAVIGATIONDIRECTORY_Grid_Title_Notify_update10'), message10);

                            }

    };
    this.NAVIGATIONDIRECTORY_Grid_delete = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#NAVIGATIONDIRECTORY_GridSaveBtn'));          
            var errors;
        data = app.core.SyncWebMethod("/fasi/dli/forms/NavigationWidgetActions.aspx/NAVIGATIONDIRECTORY_Grid1DeleteCommandActionNAVIGATIONDIRECTORYDESC", false,
               JSON.stringify({ NAVIGATIONDIRECTORYDESCID1: row.ID }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/NavigationWidgetActions.aspx/NAVIGATIONDIRECTORY_Grid3DeleteCommandActionNAVIGATIONDIRECTORY", false,
               JSON.stringify({ NAVIGATIONDIRECTORYID1: row.ID }));
               

            }            
            else {
            var message4 = $.i18n.t('app.form.NAVIGATIONDIRECTORY_Grid_Message_Notify_delete4');
            notification.swal.error($.i18n.t('app.form.NAVIGATIONDIRECTORY_Grid_Title_Notify_delete4'), message4);

                }
        if (data.d.Success === true){
            $('#NAVIGATIONDIRECTORY_GridTbl').bootstrapTable('remove', {field: 'ID', values: [generalSupport.NumericValue('#ID', -999999999, 999999999)]});
            var message7 = $.i18n.t('app.form.NAVIGATIONDIRECTORY_Grid_Message_Notify_delete7');
            notification.toastr.success($.i18n.t('app.form.NAVIGATIONDIRECTORY_Grid_Title_Notify_delete7'), message7);
                    }                    
                    else {
            var message8 = $.i18n.t('app.form.NAVIGATIONDIRECTORY_Grid_Message_Notify_delete8');
            notification.toastr.error($.i18n.t('app.form.NAVIGATIONDIRECTORY_Grid_Title_Notify_delete8'), message8);

                        }

    };
    this.NAVIGATIONDIRECTORY_Grid_BeforeShowPopup = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#NAVIGATIONDIRECTORY_GridSaveBtn'));          
            var nextId;
        if (row.ID === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/NavigationWidgetActions.aspx/NAVIGATIONDIRECTORY_Grid2SelectCommandActionNAVIGATIONDIRECTORY", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#ID', nextId);

            }

    };
    this.NAVIGATIONDIRECTORYTranslator_Grid_update = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#NAVIGATIONDIRECTORYTranslator_GridSaveBtn'));          
            var errors;
        data = app.core.SyncWebMethod("/fasi/dli/forms/NavigationWidgetActions.aspx/NAVIGATIONDIRECTORYTranslator_Grid1UpdateCommandActionNAVIGATIONDIRECTORYDESC", false,
               JSON.stringify({ TITLE1: row.TITLE, DESCRIPTION2: row.DESCRIPTION, UPDATEUSERCODE2: app.user.userId, NAVIGATIONDIRECTORYDESCID5: row.ID, NAVIGATIONDIRECTORYDESCLANGUAGEID6: row.LANGUAGEID }));
               

        if (data.d.Success === true){
            $('#NAVIGATIONDIRECTORYTranslator_GridTbl').bootstrapTable('updateByUniqueId', { id: row.ID, row: row });
            $modal.modal('hide');
            var message4 = $.i18n.t('app.form.NAVIGATIONDIRECTORYTranslator_Grid_Message_Notify_update4');
            notification.toastr.success($.i18n.t('app.form.NAVIGATIONDIRECTORYTranslator_Grid_Title_Notify_update4'), message4);
            }            
            else {
            var message5 = $.i18n.t('app.form.NAVIGATIONDIRECTORYTranslator_Grid_Message_Notify_update5');
            notification.swal.error($.i18n.t('app.form.NAVIGATIONDIRECTORYTranslator_Grid_Title_Notify_update5'), message5);

                }

    };

    this.ControlActions =   function () {

        $('#ShowStandardGrid').click(function (event) {
            var formInstance = $("#NavigationWidgetMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                NavigationWidgetSupport.NAVIGATIONDIRECTORY_GridTblRequest();
                $('#NAVIGATIONDIRECTORY_GridContainer').toggleClass('hidden', false);
                $('#NAVIGATIONDIRECTORYTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#NavigationWidgetMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                NavigationWidgetSupport.NAVIGATIONDIRECTORYTranslator_GridTblRequest();
                $('#NAVIGATIONDIRECTORYTranslator_GridContainer').toggleClass('hidden', false);
                $('#NAVIGATIONDIRECTORY_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#NavigationWidgetMainForm").validate({
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
        $("#NAVIGATIONDIRECTORY_GridEditForm").validate().destroy();
        $("#NAVIGATIONDIRECTORY_GridEditForm").validate({
            rules: {
                ID: {
                    AutoNumericMinValue: -999999999,
                    AutoNumericMaxValue: 999999999,
                    required: true
                },
                NAME: {
                    required: true,
                    maxlength: 255
                },
                TITLE: {
                    maxlength: 120
                },
                DESCRIPTION: {
                    maxlength: 255
                },
                URLPATH: {
                    required: true,
                    maxlength: 255
                },
                CATEGORYCODE: {
                },
                IMAGEFILE: {
                    maxlength: 50
                },
                STATUS: {
                    required: true                },
                MODELID: {
                    maxlength: 36
                }

            },
            messages: {
                ID: {
                    AutoNumericMinValue: $.i18n.t('app.validation.ID.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.ID.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.ID.required')
                },
                NAME: {
                    required: $.i18n.t('app.validation.NAME.required'),
                    maxlength: $.i18n.t('app.validation.NAME.maxlength')
                },
                TITLE: {
                    maxlength: $.i18n.t('app.validation.TITLE.maxlength')
                },
                DESCRIPTION: {
                    maxlength: $.i18n.t('app.validation.DESCRIPTION.maxlength')
                },
                URLPATH: {
                    required: $.i18n.t('app.validation.URLPATH.required'),
                    maxlength: $.i18n.t('app.validation.URLPATH.maxlength')
                },
                CATEGORYCODE: {
                },
                IMAGEFILE: {
                    maxlength: $.i18n.t('app.validation.IMAGEFILE.maxlength')
                },
                STATUS: {
                    required: $.i18n.t('app.validation.STATUS.required')                },
                MODELID: {
                    maxlength: $.i18n.t('app.validation.MODELID.maxlength')
                }

            }
        });
        $("#NAVIGATIONDIRECTORYTranslator_GridEditForm").validate().destroy();
        $("#NAVIGATIONDIRECTORYTranslator_GridEditForm").validate({
            rules: {
                IDTranslator: {
                    AutoNumericMinValue: -999999999,
                    AutoNumericMaxValue: 999999999,
                    required: true
                },
                NAMETranslator: {
                    required: true,
                    maxlength: 255
                },
                TITLETranslator: {
                    maxlength: 120
                },
                DESCRIPTIONTranslator: {
                    maxlength: 255
                },
                URLPATHTranslator: {
                    required: true,
                    maxlength: 255
                },
                CATEGORYCODETranslator: {
                },
                IMAGEFILETranslator: {
                    maxlength: 50
                },
                STATUSTranslator: {
                    required: true                },
                LANGUAGEIDTranslator: {
                    required: true                },
                MODELIDTranslator: {
                    maxlength: 36
                }

            },
            messages: {
                IDTranslator: {
                    AutoNumericMinValue: $.i18n.t('app.validation.IDTranslator.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.IDTranslator.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.IDTranslator.required')
                },
                NAMETranslator: {
                    required: $.i18n.t('app.validation.NAMETranslator.required'),
                    maxlength: $.i18n.t('app.validation.NAMETranslator.maxlength')
                },
                TITLETranslator: {
                    maxlength: $.i18n.t('app.validation.TITLETranslator.maxlength')
                },
                DESCRIPTIONTranslator: {
                    maxlength: $.i18n.t('app.validation.DESCRIPTIONTranslator.maxlength')
                },
                URLPATHTranslator: {
                    required: $.i18n.t('app.validation.URLPATHTranslator.required'),
                    maxlength: $.i18n.t('app.validation.URLPATHTranslator.maxlength')
                },
                CATEGORYCODETranslator: {
                },
                IMAGEFILETranslator: {
                    maxlength: $.i18n.t('app.validation.IMAGEFILETranslator.maxlength')
                },
                STATUSTranslator: {
                    required: $.i18n.t('app.validation.STATUSTranslator.required')                },
                LANGUAGEIDTranslator: {
                    required: $.i18n.t('app.validation.LANGUAGEIDTranslator.required')                },
                MODELIDTranslator: {
                    maxlength: $.i18n.t('app.validation.MODELIDTranslator.maxlength')
                }

            }
        });

    };
    this.LookUpForCATEGORYCODEFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#CATEGORYCODE>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCATEGORYCODE = function (defaultValue, source) {
        var ctrol = $('#CATEGORYCODE');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/NavigationWidgetActions.aspx/LookUpForCATEGORYCODE", false,
                JSON.stringify({ id: $('#NavigationWidgetFormId').val() }),
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
    this.LookUpForSTATUSFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#STATUS>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForSTATUS = function (defaultValue, source) {
        var ctrol = $('#STATUS');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/NavigationWidgetActions.aspx/LookUpForSTATUS", false,
                JSON.stringify({ id: $('#NavigationWidgetFormId').val() }),
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
    this.LookUpForCATEGORYCODETranslatorFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#CATEGORYCODETranslator>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCATEGORYCODETranslator = function (defaultValue, source) {
        var ctrol = $('#CATEGORYCODETranslator');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/NavigationWidgetActions.aspx/LookUpForCATEGORYCODETranslator", false,
                JSON.stringify({ id: $('#NavigationWidgetFormId').val() }),
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
    this.LookUpForSTATUSTranslatorFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#STATUSTranslator>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForSTATUSTranslator = function (defaultValue, source) {
        var ctrol = $('#STATUSTranslator');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/NavigationWidgetActions.aspx/LookUpForSTATUSTranslator", false,
                JSON.stringify({ id: $('#NavigationWidgetFormId').val() }),
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
    this.LookUpForLANGUAGEIDTranslatorFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#LANGUAGEIDTranslator>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForLANGUAGEIDTranslator = function (defaultValue, source) {
        var ctrol = $('#LANGUAGEIDTranslator');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/NavigationWidgetActions.aspx/LookUpForLANGUAGEIDTranslator", false,
                JSON.stringify({ id: $('#NavigationWidgetFormId').val() }),
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

    this.NAVIGATIONDIRECTORY_GridTblSetup = function (table) {
        NavigationWidgetSupport.LookUpForCATEGORYCODE('');
        NavigationWidgetSupport.LookUpForSTATUS('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'ID',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showColumns: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
            onCellHtmlData: function(cell, row, col, data) {
					var result = "";
					if (data != "") {
					  var html = $.parseHTML(data);

					  $.each(html, function() {
						  if (typeof $(this).html() === 'undefined')
							  result += $(this).text();
						  else if (typeof $(this).attr('class') === 'undefined' || $(this).hasClass('th-inner') === true)
							  result += $(this).html();
              else if($(this).hasClass('update edit') === true)
                result += $(this).html();
              else if (typeof $(this).attr('class') === 'undefined' || $(this).hasClass('row-fluid') === true)
                  if (this.children.length !== 0) {
                      $.each(this.children, function () {
                          if ($(this).attr('class') === 'undefined' || $(this).hasClass('control-label') === true) {
                             result += $(this).text();
                          }
                      });
                  }
					  });
					}
					 return result;
				},
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#NAVIGATIONDIRECTORY_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true,
                formatter: 'NavigationWidgetSupport.selected_Formatter'
            }, {
                field: 'ID',
                title: $.i18n.t('app.form.NAVIGATIONDIRECTORY_GridTbl_ID_Title'),
                events: 'NAVIGATIONDIRECTORY_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NAME',
                title: $.i18n.t('app.form.NAVIGATIONDIRECTORY_GridTbl_NAME_Title'),
                sortable: true,
                halign: 'center'
            }, {
                field: 'TITLE',
                title: $.i18n.t('app.form.NAVIGATIONDIRECTORY_GridTbl_TITLE_Title'),
                sortable: true,
                halign: 'center'
            }, {
                field: 'DESCRIPTION',
                title: $.i18n.t('app.form.NAVIGATIONDIRECTORY_GridTbl_DESCRIPTION_Title'),
                sortable: true,
                halign: 'center'
            }, {
                field: 'URLPATH',
                title: $.i18n.t('app.form.NAVIGATIONDIRECTORY_GridTbl_URLPATH_Title'),
                sortable: true,
                halign: 'center'
            }, {
                field: 'CATEGORYCODE',
                title: $.i18n.t('app.form.NAVIGATIONDIRECTORY_GridTbl_CATEGORYCODE_Title'),
                formatter: 'NavigationWidgetSupport.LookUpForCATEGORYCODEFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'IMAGEFILE',
                title: $.i18n.t('app.form.NAVIGATIONDIRECTORY_GridTbl_IMAGEFILE_Title'),
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'STATUS',
                title: $.i18n.t('app.form.NAVIGATIONDIRECTORY_GridTbl_STATUS_Title'),
                formatter: 'NavigationWidgetSupport.LookUpForSTATUSFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'MODELID',
                title: $.i18n.t('app.form.NAVIGATIONDIRECTORY_GridTbl_MODELID_Title'),
                sortable: true,
                halign: 'center',
                visible: false
            }]
        });


        $('#NAVIGATIONDIRECTORY_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#NAVIGATIONDIRECTORY_GridTbl');
            $('#NAVIGATIONDIRECTORY_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#NAVIGATIONDIRECTORY_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#NAVIGATIONDIRECTORY_GridTbl').bootstrapTable('getSelections'), function (row) {		
                NavigationWidgetSupport.NAVIGATIONDIRECTORY_GridRowToInput(row);
                NavigationWidgetSupport.NAVIGATIONDIRECTORY_Grid_delete(row, null);
                
                return row.ID;
            });

            $('#NAVIGATIONDIRECTORY_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#NAVIGATIONDIRECTORY_GridCreateBtn').click(function () {
            var formInstance = $("#NAVIGATIONDIRECTORY_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            NavigationWidgetSupport.NAVIGATIONDIRECTORY_GridShowModal($('#NAVIGATIONDIRECTORY_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#NAVIGATIONDIRECTORY_GridPopup').find('#NAVIGATIONDIRECTORY_GridSaveBtn').click(function () {
            var formInstance = $("#NAVIGATIONDIRECTORY_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#NAVIGATIONDIRECTORY_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#NAVIGATIONDIRECTORY_GridSaveBtn').html();
                $('#NAVIGATIONDIRECTORY_GridSaveBtn').html('Procesando...');
                $('#NAVIGATIONDIRECTORY_GridSaveBtn').prop('disabled', true);

                NavigationWidgetSupport.currentRow.ID = generalSupport.NumericValue('#ID', -999999999, 999999999);
                NavigationWidgetSupport.currentRow.NAME = $('#NAME').val();
                NavigationWidgetSupport.currentRow.TITLE = $('#TITLE').val();
                NavigationWidgetSupport.currentRow.DESCRIPTION = $('#DESCRIPTION').val();
                NavigationWidgetSupport.currentRow.URLPATH = $('#URLPATH').val();
                NavigationWidgetSupport.currentRow.CATEGORYCODE = parseInt(0 + $('#CATEGORYCODE').val(), 10);
                NavigationWidgetSupport.currentRow.IMAGEFILE = $('#IMAGEFILE').val();
                NavigationWidgetSupport.currentRow.STATUS = parseInt(0 + $('#STATUS').val(), 10);
                NavigationWidgetSupport.currentRow.MODELID = $('#MODELID').val();

                $('#NAVIGATIONDIRECTORY_GridSaveBtn').prop('disabled', false);
                $('#NAVIGATIONDIRECTORY_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    NavigationWidgetSupport.NAVIGATIONDIRECTORY_Grid_update(NavigationWidgetSupport.currentRow, $modal);
                }
                else {                    
                    NavigationWidgetSupport.NAVIGATIONDIRECTORY_Grid_insert(NavigationWidgetSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.NAVIGATIONDIRECTORY_GridShowModal = function (md, title, row) {
        var formInstance = $("#NAVIGATIONDIRECTORY_GridEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { ID: 0, NAME: '', TITLE: '', DESCRIPTION: '', URLPATH: '', CATEGORYCODE: 0, IMAGEFILE: '', STATUS: 0, MODELID: '' };

        md.data('id', row.ID);
        md.find('.modal-title').text(title);

        NavigationWidgetSupport.NAVIGATIONDIRECTORY_GridRowToInput(row);
        $('#ID').prop('disabled', (row.ID !== 0));
        $('#MODELID').prop('disabled', true);
        NavigationWidgetSupport.NAVIGATIONDIRECTORY_Grid_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.NAVIGATIONDIRECTORY_GridRowToInput = function (row) {
        NavigationWidgetSupport.currentRow = row;
        AutoNumeric.set('#ID', row.ID);
        $('#NAME').val(row.NAME);
        $('#TITLE').val(row.TITLE);
        $('#DESCRIPTION').val(row.DESCRIPTION);
        $('#URLPATH').val(row.URLPATH);
        NavigationWidgetSupport.LookUpForCATEGORYCODE(row.CATEGORYCODE, '');
        $('#CATEGORYCODE').trigger('change');
        $('#IMAGEFILE').val(row.IMAGEFILE);
        NavigationWidgetSupport.LookUpForSTATUS(row.STATUS, '');
        $('#STATUS').trigger('change');
        $('#MODELID').val(row.MODELID);

    };
    this.NAVIGATIONDIRECTORY_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/NavigationWidgetActions.aspx/NAVIGATIONDIRECTORY_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                NAVIGATIONDIRECTORYDESCLANGUAGEID1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#NAVIGATIONDIRECTORY_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.NAVIGATIONDIRECTORYTranslator_GridTblSetup = function (table) {
        NavigationWidgetSupport.LookUpForCATEGORYCODETranslator('');
        NavigationWidgetSupport.LookUpForSTATUSTranslator('');
        NavigationWidgetSupport.LookUpForLANGUAGEIDTranslator('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'ID',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showColumns: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
            onCellHtmlData: function(cell, row, col, data) {
					var result = "";
					if (data != "") {
					  var html = $.parseHTML(data);

					  $.each(html, function() {
						  if (typeof $(this).html() === 'undefined')
							  result += $(this).text();
						  else if (typeof $(this).attr('class') === 'undefined' || $(this).hasClass('th-inner') === true)
							  result += $(this).html();
              else if($(this).hasClass('update edit') === true)
                result += $(this).html();
              else if (typeof $(this).attr('class') === 'undefined' || $(this).hasClass('row-fluid') === true)
                  if (this.children.length !== 0) {
                      $.each(this.children, function () {
                          if ($(this).attr('class') === 'undefined' || $(this).hasClass('control-label') === true) {
                             result += $(this).text();
                          }
                      });
                  }
					  });
					}
					 return result;
				},
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#NAVIGATIONDIRECTORYTranslator_Gridtoolbar',
            columns: [{
                field: 'ID',
                title: $.i18n.t('app.form.NAVIGATIONDIRECTORYTranslator_GridTbl_ID_Title'),
                events: 'NAVIGATIONDIRECTORYTranslator_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NAME',
                title: $.i18n.t('app.form.NAVIGATIONDIRECTORYTranslator_GridTbl_NAME_Title'),
                sortable: true,
                halign: 'center'
            }, {
                field: 'TITLE',
                title: $.i18n.t('app.form.NAVIGATIONDIRECTORYTranslator_GridTbl_TITLE_Title'),
                sortable: true,
                halign: 'center'
            }, {
                field: 'DESCRIPTION',
                title: $.i18n.t('app.form.NAVIGATIONDIRECTORYTranslator_GridTbl_DESCRIPTION_Title'),
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'URLPATH',
                title: $.i18n.t('app.form.NAVIGATIONDIRECTORYTranslator_GridTbl_URLPATH_Title'),
                sortable: true,
                halign: 'center'
            }, {
                field: 'CATEGORYCODE',
                title: $.i18n.t('app.form.NAVIGATIONDIRECTORYTranslator_GridTbl_CATEGORYCODE_Title'),
                formatter: 'NavigationWidgetSupport.LookUpForCATEGORYCODETranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'IMAGEFILE',
                title: $.i18n.t('app.form.NAVIGATIONDIRECTORYTranslator_GridTbl_IMAGEFILE_Title'),
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'STATUS',
                title: $.i18n.t('app.form.NAVIGATIONDIRECTORYTranslator_GridTbl_STATUS_Title'),
                formatter: 'NavigationWidgetSupport.LookUpForSTATUSTranslatorFormatter',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'LANGUAGEID',
                title: $.i18n.t('app.form.NAVIGATIONDIRECTORYTranslator_GridTbl_LANGUAGEID_Title'),
                formatter: 'NavigationWidgetSupport.LookUpForLANGUAGEIDTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'MODELID',
                title: $.i18n.t('app.form.NAVIGATIONDIRECTORYTranslator_GridTbl_MODELID_Title'),
                sortable: true,
                halign: 'center',
                visible: false
            }]
        });


        $('#NAVIGATIONDIRECTORYTranslator_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#NAVIGATIONDIRECTORYTranslator_GridTbl');
            $('#NAVIGATIONDIRECTORYTranslator_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#NAVIGATIONDIRECTORYTranslator_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#NAVIGATIONDIRECTORYTranslator_GridTbl').bootstrapTable('getSelections'), function (row) {		
                NavigationWidgetSupport.NAVIGATIONDIRECTORYTranslator_GridRowToInput(row);
                
                
                return row.ID;
            });
            
          $('#NAVIGATIONDIRECTORYTranslator_GridTbl').bootstrapTable('remove', {
                field: 'ID',
                values: ids
           });

            $('#NAVIGATIONDIRECTORYTranslator_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#NAVIGATIONDIRECTORYTranslator_GridCreateBtn').click(function () {
            var formInstance = $("#NAVIGATIONDIRECTORYTranslator_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            NavigationWidgetSupport.NAVIGATIONDIRECTORYTranslator_GridShowModal($('#NAVIGATIONDIRECTORYTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#NAVIGATIONDIRECTORYTranslator_GridPopup').find('#NAVIGATIONDIRECTORYTranslator_GridSaveBtn').click(function () {
            var formInstance = $("#NAVIGATIONDIRECTORYTranslator_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#NAVIGATIONDIRECTORYTranslator_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#NAVIGATIONDIRECTORYTranslator_GridSaveBtn').html();
                $('#NAVIGATIONDIRECTORYTranslator_GridSaveBtn').html('Procesando...');
                $('#NAVIGATIONDIRECTORYTranslator_GridSaveBtn').prop('disabled', true);

                NavigationWidgetSupport.currentRow.ID = generalSupport.NumericValue('#IDTranslator', -999999999, 999999999);
                NavigationWidgetSupport.currentRow.NAME = $('#NAMETranslator').val();
                NavigationWidgetSupport.currentRow.TITLE = $('#TITLETranslator').val();
                NavigationWidgetSupport.currentRow.DESCRIPTION = $('#DESCRIPTIONTranslator').val();
                NavigationWidgetSupport.currentRow.URLPATH = $('#URLPATHTranslator').val();
                NavigationWidgetSupport.currentRow.CATEGORYCODE = parseInt(0 + $('#CATEGORYCODETranslator').val(), 10);
                NavigationWidgetSupport.currentRow.IMAGEFILE = $('#IMAGEFILETranslator').val();
                NavigationWidgetSupport.currentRow.STATUS = parseInt(0 + $('#STATUSTranslator').val(), 10);
                NavigationWidgetSupport.currentRow.LANGUAGEID = parseInt(0 + $('#LANGUAGEIDTranslator').val(), 10);
                NavigationWidgetSupport.currentRow.MODELID = $('#MODELIDTranslator').val();

                $('#NAVIGATIONDIRECTORYTranslator_GridSaveBtn').prop('disabled', false);
                $('#NAVIGATIONDIRECTORYTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    NavigationWidgetSupport.NAVIGATIONDIRECTORYTranslator_Grid_update(NavigationWidgetSupport.currentRow, $modal);
                }
                else {                    
                    $('#NAVIGATIONDIRECTORYTranslator_GridTbl').bootstrapTable('append', NavigationWidgetSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.NAVIGATIONDIRECTORYTranslator_GridShowModal = function (md, title, row) {
        var formInstance = $("#NAVIGATIONDIRECTORYTranslator_GridEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { ID: 0, NAME: '', TITLE: '', DESCRIPTION: '', URLPATH: '', CATEGORYCODE: 0, IMAGEFILE: '', STATUS: 0, LANGUAGEID: 0, MODELID: '' };

        md.data('id', row.ID);
        md.find('.modal-title').text(title);

        NavigationWidgetSupport.NAVIGATIONDIRECTORYTranslator_GridRowToInput(row);
        $('#IDTranslator').prop('disabled', true);
        $('#NAMETranslator').prop('disabled', true);
        $('#URLPATHTranslator').prop('disabled', true);
        $('#CATEGORYCODETranslator').prop('disabled', (row.ID !== 0));
        $('#IMAGEFILETranslator').prop('disabled', (row.ID !== 0));
        $('#STATUSTranslator').prop('disabled', true);
        $('#LANGUAGEIDTranslator').prop('disabled', true);
        $('#MODELIDTranslator').prop('disabled', (row.ID !== 0));

        md.appendTo("body");
        md.modal('show');
    };

    this.NAVIGATIONDIRECTORYTranslator_GridRowToInput = function (row) {
        NavigationWidgetSupport.currentRow = row;
        AutoNumeric.set('#IDTranslator', row.ID);
        $('#NAMETranslator').val(row.NAME);
        $('#TITLETranslator').val(row.TITLE);
        $('#DESCRIPTIONTranslator').val(row.DESCRIPTION);
        $('#URLPATHTranslator').val(row.URLPATH);
        NavigationWidgetSupport.LookUpForCATEGORYCODETranslator(row.CATEGORYCODE, '');
        $('#CATEGORYCODETranslator').trigger('change');
        $('#IMAGEFILETranslator').val(row.IMAGEFILE);
        NavigationWidgetSupport.LookUpForSTATUSTranslator(row.STATUS, '');
        $('#STATUSTranslator').trigger('change');
        NavigationWidgetSupport.LookUpForLANGUAGEIDTranslator(row.LANGUAGEID, '');
        $('#LANGUAGEIDTranslator').trigger('change');
        $('#MODELIDTranslator').val(row.MODELID);

    };
    this.NAVIGATIONDIRECTORYTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/NavigationWidgetActions.aspx/NAVIGATIONDIRECTORYTranslator_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#NAVIGATIONDIRECTORYTranslator_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.ID_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
        });
      };
    this.IDTranslator_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
        });
      };


	    this.selected_Formatter = function (value, row, index) {          
          return {
                  disabled: $('#NAVIGATIONDIRECTORY_GridTbl *').prop('disabled'),
                  checked: value
                 }
      };



  this.Init = function(){
    
    moment.locale(app.user.languageName);
    
   generalSupport.TranslateInit('NavigationWidget', function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        NavigationWidgetSupport.ValidateSetup();
        
        

    NavigationWidgetSupport.ControlBehaviour();
    NavigationWidgetSupport.ControlActions();
    

    app.core.AsyncWebMethod("/fasi/dli/forms/NavigationWidgetActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), false,
            JSON.stringify({
                id: $('#NavigationWidgetFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
               if (data.d.Success === true) {
                  NavigationWidgetSupport.CallRenderLookUps(data);
                
            
                $("#NAVIGATIONDIRECTORY_GridTblPlaceHolder").replaceWith('<table id="NAVIGATIONDIRECTORY_GridTbl"></table>');
    NavigationWidgetSupport.NAVIGATIONDIRECTORY_GridTblSetup($('#NAVIGATIONDIRECTORY_GridTbl'));
    $("#NAVIGATIONDIRECTORYTranslator_GridTblPlaceHolder").replaceWith('<table id="NAVIGATIONDIRECTORYTranslator_GridTbl"></table>');
    NavigationWidgetSupport.NAVIGATIONDIRECTORYTranslator_GridTblSetup($('#NAVIGATIONDIRECTORYTranslator_GridTbl'));

                    NavigationWidgetSupport.NAVIGATIONDIRECTORY_GridTblRequest();
        NavigationWidgetSupport.NAVIGATIONDIRECTORYTranslator_GridTblRequest();

            
            
            
             }
});

   }, 'dli/forms');
  };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: ['Administrador'],
        Element: $("#NavigationWidgetMainForm"),
        CallBack: NavigationWidgetSupport.Init
    });
});

window.NAVIGATIONDIRECTORY_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        NavigationWidgetSupport.NAVIGATIONDIRECTORY_GridShowModal($('#NAVIGATIONDIRECTORY_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.NAVIGATIONDIRECTORYTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        NavigationWidgetSupport.NAVIGATIONDIRECTORYTranslator_GridShowModal($('#NAVIGATIONDIRECTORYTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
