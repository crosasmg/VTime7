var MainNavigationSupport = new function () {

    this.currentRow = {};
    this.newIndex = -1;
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#MainNavigationFormId').val(),
            NAVIGATION_Grid_NAVIGATION_Item: generalSupport.NormalizeProperties($('#NAVIGATION_GridTbl').bootstrapTable('getData'), ''),
            NAVIGATIONTranslator_Grid_NAVIGATION_Item: generalSupport.NormalizeProperties($('#NAVIGATIONTranslator_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#MainNavigationFormId').val(data.InstanceFormId);

        MainNavigationSupport.LookUpForPARENTCODE(source);
        MainNavigationSupport.LookUpForRECORDSTATUS(source);
        MainNavigationSupport.LookUpForPARENTCODETranslator(source);
        MainNavigationSupport.LookUpForRECORDSTATUSTranslator(source);
        MainNavigationSupport.LookUpForLANGUAGEIDTranslator(source);

        MainNavigationSupport.NAVIGATION_GridTblRequest();
        if (data.NAVIGATION_Grid_NAVIGATION_Item !== null)
            $('#NAVIGATION_GridTbl').bootstrapTable('load', data.NAVIGATION_Grid_NAVIGATION_Item);
        MainNavigationSupport.NAVIGATIONTranslator_GridTblRequest();
        if (data.NAVIGATIONTranslator_Grid_NAVIGATION_Item !== null)
            $('#NAVIGATIONTranslator_GridTbl').bootstrapTable('load', data.NAVIGATIONTranslator_Grid_NAVIGATION_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#SEQUENCE', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });






    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               MainNavigationSupport.ObjectToInput(data.d.Data.Instance, source);
            
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



    this.NAVIGATION_Grid_insert = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#NAVIGATION_GridSaveBtn'));          
            var errors;
        data = app.core.SyncWebMethod("/fasi/dli/forms/MainNavigationActions.aspx/NAVIGATION_Grid1InsertCommandActionNAVIGATION", false,
               JSON.stringify({ CODE1: row.CODE, PARENTCODE2: row.PARENTCODE, TYPE3: row.TYPE, URLPATH4: row.URLPATH, SMALLIMAGE5: row.SMALLIMAGE, BIGIMAGE6: row.BIGIMAGE, SEQUENCE7: row.SEQUENCE, URLHELP8: row.URLHELP, MODELID9: row.MODELID, ICONCSSCLASS10: row.ICONCSSCLASS, RECORDSTATUS11: row.RECORDSTATUS, CREATORUSERCODE11: app.user.userId, UPDATEUSERCODE13: app.user.userId }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/MainNavigationActions.aspx/NAVIGATION_Grid3InsertCommandActionNAVIGATIONDESC", false,
               JSON.stringify({ CODE1: row.CODE, LANGUAGEID1: generalSupport.SessionContext().languageId, TITLE3: row.TITLE, DESCRIPTION4: row.DESCRIPTION, CREATORUSERCODE4: app.user.userId, UPDATEUSERCODE6: app.user.userId }));
               

            }            
            else {
            var message4 = $.i18n.t('app.form.NAVIGATION_Grid_Message_Notify_insert4');
            notification.swal.error($.i18n.t('app.form.NAVIGATION_Grid_Title_Notify_insert4'), message4);

                }
        if (data.d.Success === true){
            $('#NAVIGATION_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message7 = $.i18n.t('app.form.NAVIGATION_Grid_Message_Notify_insert7');
            notification.toastr.success($.i18n.t('app.form.NAVIGATION_Grid_Title_Notify_insert7'), message7);
                    }                    
                    else {
            var message8 = $.i18n.t('app.form.NAVIGATION_Grid_Message_Notify_insert8');
            notification.swal.error($.i18n.t('app.form.NAVIGATION_Grid_Title_Notify_insert8'), message8);

                        }

    };
    this.NAVIGATION_Grid_update = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#NAVIGATION_GridSaveBtn'));          
            var recordCount;
            var errors;
        data = app.core.SyncWebMethod("/fasi/dli/forms/MainNavigationActions.aspx/NAVIGATION_Grid1UpdateCommandActionNAVIGATION", false,
               JSON.stringify({ PARENTCODE1: row.PARENTCODE, TYPE2: row.TYPE, URLPATH3: row.URLPATH, SMALLIMAGE4: row.SMALLIMAGE, BIGIMAGE5: row.BIGIMAGE, SEQUENCE6: row.SEQUENCE, URLHELP7: row.URLHELP, MODELID8: row.MODELID, ICONCSSCLASS9: row.ICONCSSCLASS, RECORDSTATUS10: row.RECORDSTATUS, UPDATEUSERCODE10: app.user.userId, NAVIGATIONCODE13: row.CODE }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/MainNavigationActions.aspx/NAVIGATION_Grid3SelectCommandActionNAVIGATIONDESC", false,
               JSON.stringify({                 NAVIGATIONDESCCODE1: row.CODE,
                NAVIGATIONDESCLANGUAGEID2: generalSupport.SessionContext().languageId }));
               
                 if (data.d.Count !== 0)
                                recordCount = data.d.Data.Result; 
            if (recordCount === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/MainNavigationActions.aspx/NAVIGATION_Grid5InsertCommandActionNAVIGATIONDESC", false,
               JSON.stringify({ CODE1: row.CODE, LANGUAGEID1: generalSupport.SessionContext().languageId, TITLE3: row.TITLE, DESCRIPTION4: row.DESCRIPTION, CREATORUSERCODE4: app.user.userId, UPDATEUSERCODE6: app.user.userId }));
               

                    }                    
                    else {
        data = app.core.SyncWebMethod("/fasi/dli/forms/MainNavigationActions.aspx/NAVIGATION_Grid6UpdateCommandActionNAVIGATIONDESC", false,
               JSON.stringify({ TITLE1: row.TITLE, DESCRIPTION2: row.DESCRIPTION, UPDATEUSERCODE2: app.user.userId, NAVIGATIONDESCCODE5: row.CODE, NAVIGATIONDESCLANGUAGEID6: generalSupport.SessionContext().languageId }));
               


                        }

                    }
        if (data.d.Success === true){
            $('#NAVIGATION_GridTbl').bootstrapTable('updateByUniqueId', { id: row.Unique_Key, row: row });
            $modal.modal('hide');
            var message9 = $.i18n.t('app.form.NAVIGATION_Grid_Message_Notify_update9');
            notification.toastr.success($.i18n.t('app.form.NAVIGATION_Grid_Title_Notify_update9'), message9);
                        }                        
                        else {
            var message10 = $.i18n.t('app.form.NAVIGATION_Grid_Message_Notify_update10');
            notification.swal.error($.i18n.t('app.form.NAVIGATION_Grid_Title_Notify_update10'), message10);

                            }

    };
    this.NAVIGATION_Grid_delete = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#NAVIGATION_GridSaveBtn'));          
            var errors;
        data = app.core.SyncWebMethod("/fasi/dli/forms/MainNavigationActions.aspx/NAVIGATION_Grid1DeleteCommandActionNAVIGATIONDESC", false,
               JSON.stringify({ NAVIGATIONDESCCODE1: row.CODE }));
               

        if (data.d.Success === true){
        data = app.core.SyncWebMethod("/fasi/dli/forms/MainNavigationActions.aspx/NAVIGATION_Grid3DeleteCommandActionNAVIGATION", false,
               JSON.stringify({ NAVIGATIONCODE1: row.CODE }));
               

            }            
            else {
            var message4 = $.i18n.t('app.form.NAVIGATION_Grid_Message_Notify_delete4');
            notification.swal.error($.i18n.t('app.form.NAVIGATION_Grid_Title_Notify_delete4'), message4);

                }
        if (data.d.Success === true){
            $('#NAVIGATION_GridTbl').bootstrapTable('remove', {field: 'Unique_Key', values: [row.Unique_Key]});
            var message7 = $.i18n.t('app.form.NAVIGATION_Grid_Message_Notify_delete7');
            notification.toastr.success($.i18n.t('app.form.NAVIGATION_Grid_Title_Notify_delete7'), message7);
                    }                    
                    else {
            var message8 = $.i18n.t('app.form.NAVIGATION_Grid_Message_Notify_delete8');
            notification.toastr.error($.i18n.t('app.form.NAVIGATION_Grid_Title_Notify_delete8'), message8);

                        }

    };
    this.NAVIGATIONTranslator_Grid_update = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#NAVIGATIONTranslator_GridSaveBtn'));          
            var errors;
        data = app.core.SyncWebMethod("/fasi/dli/forms/MainNavigationActions.aspx/NAVIGATIONTranslator_Grid1UpdateCommandActionNAVIGATIONDESC", false,
               JSON.stringify({ TITLE1: row.TITLE, DESCRIPTION2: row.DESCRIPTION, UPDATEUSERCODE2: app.user.userId, NAVIGATIONDESCCODE5: row.CODE, NAVIGATIONDESCLANGUAGEID6: row.LANGUAGEID }));
               

        if (data.d.Success === true){
            $('#NAVIGATIONTranslator_GridTbl').bootstrapTable('updateByUniqueId', { id: row.Unique_Key, row: row });
            $modal.modal('hide');
            var message4 = $.i18n.t('app.form.NAVIGATIONTranslator_Grid_Message_Notify_update4');
            notification.toastr.success($.i18n.t('app.form.NAVIGATIONTranslator_Grid_Title_Notify_update4'), message4);
            }            
            else {
            var message5 = $.i18n.t('app.form.NAVIGATIONTranslator_Grid_Message_Notify_update5');
            notification.swal.error($.i18n.t('app.form.NAVIGATIONTranslator_Grid_Title_Notify_update5'), message5);

                }

    };

    this.ControlActions =   function () {

        $('#ShowStandardGrid').click(function (event) {
            var formInstance = $("#MainNavigationMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowStandardGrid'));
                btnLoading.start();
                MainNavigationSupport.NAVIGATION_GridTblRequest();
                $('#NAVIGATION_GridContainer').toggleClass('hidden', false);
                $('#NAVIGATIONTranslator_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ShowTranslatorGrid').click(function (event) {
            var formInstance = $("#MainNavigationMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#ShowTranslatorGrid'));
                btnLoading.start();
                MainNavigationSupport.NAVIGATIONTranslator_GridTblRequest();
                $('#NAVIGATIONTranslator_GridContainer').toggleClass('hidden', false);
                $('#NAVIGATION_GridContainer').toggleClass('hidden', true);
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#MainNavigationMainForm").validate({
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
        $("#NAVIGATION_GridEditForm").validate().destroy();
        $("#NAVIGATION_GridEditForm").validate({
            rules: {
                TYPE: {
                },
                CODE: {
                    required: true,
                    maxlength: 8
                },
                PARENTCODE: {
                },
                TITLE: {
                    required: true,
                    maxlength: 80
                },
                URLPATH: {
                    maxlength: 255
                },
                RECORDSTATUS: {
                    required: true                },
                SMALLIMAGE: {
                    maxlength: 80
                },
                BIGIMAGE: {
                    maxlength: 80
                },
                SEQUENCE: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999,
                    required: true
                },
                URLHELP: {
                    maxlength: 255
                },
                ICONCSSCLASS: {
                    maxlength: 30
                },
                MODELID: {
                    maxlength: 36
                }

            },
            messages: {
                TYPE: {
                },
                CODE: {
                    required: $.i18n.t('app.validation.CODE.required'),
                    maxlength: $.i18n.t('app.validation.CODE.maxlength')
                },
                PARENTCODE: {
                },
                TITLE: {
                    required: $.i18n.t('app.validation.TITLE.required'),
                    maxlength: $.i18n.t('app.validation.TITLE.maxlength')
                },
                URLPATH: {
                    maxlength: $.i18n.t('app.validation.URLPATH.maxlength')
                },
                RECORDSTATUS: {
                    required: $.i18n.t('app.validation.RECORDSTATUS.required')                },
                SMALLIMAGE: {
                    maxlength: $.i18n.t('app.validation.SMALLIMAGE.maxlength')
                },
                BIGIMAGE: {
                    maxlength: $.i18n.t('app.validation.BIGIMAGE.maxlength')
                },
                SEQUENCE: {
                    AutoNumericMinValue: $.i18n.t('app.validation.SEQUENCE.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.SEQUENCE.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.SEQUENCE.required')
                },
                URLHELP: {
                    maxlength: $.i18n.t('app.validation.URLHELP.maxlength')
                },
                ICONCSSCLASS: {
                    maxlength: $.i18n.t('app.validation.ICONCSSCLASS.maxlength')
                },
                MODELID: {
                    maxlength: $.i18n.t('app.validation.MODELID.maxlength')
                }

            }
        });
        $("#NAVIGATIONTranslator_GridEditForm").validate().destroy();
        $("#NAVIGATIONTranslator_GridEditForm").validate({
            rules: {
                TYPETranslator: {
                },
                CODETranslator: {
                    required: true,
                    maxlength: 8
                },
                PARENTCODETranslator: {
                },
                TITLETranslator: {
                    required: true,
                    maxlength: 80
                },
                URLPATHTranslator: {
                    maxlength: 255
                },
                RECORDSTATUSTranslator: {
                    required: true                },
                LANGUAGEIDTranslator: {
                    required: true                }

            },
            messages: {
                TYPETranslator: {
                },
                CODETranslator: {
                    required: $.i18n.t('app.validation.CODETranslator.required'),
                    maxlength: $.i18n.t('app.validation.CODETranslator.maxlength')
                },
                PARENTCODETranslator: {
                },
                TITLETranslator: {
                    required: $.i18n.t('app.validation.TITLETranslator.required'),
                    maxlength: $.i18n.t('app.validation.TITLETranslator.maxlength')
                },
                URLPATHTranslator: {
                    maxlength: $.i18n.t('app.validation.URLPATHTranslator.maxlength')
                },
                RECORDSTATUSTranslator: {
                    required: $.i18n.t('app.validation.RECORDSTATUSTranslator.required')                },
                LANGUAGEIDTranslator: {
                    required: $.i18n.t('app.validation.LANGUAGEIDTranslator.required')                }

            }
        });

    };
    this.LookUpForTYPEFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#TYPE>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForPARENTCODEFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#PARENTCODE>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForPARENTCODE = function (defaultValue, source) {
        var ctrol = $('#PARENTCODE');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/MainNavigationActions.aspx/LookUpForPARENTCODE", false,
                JSON.stringify({ id: $('#MainNavigationFormId').val() }),
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
    this.LookUpForRECORDSTATUSFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#RECORDSTATUS>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForRECORDSTATUS = function (defaultValue, source) {
        var ctrol = $('#RECORDSTATUS');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/MainNavigationActions.aspx/LookUpForRECORDSTATUS", false,
                JSON.stringify({ id: $('#MainNavigationFormId').val() }),
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
    this.LookUpForTYPETranslatorFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#TYPETranslator>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForPARENTCODETranslatorFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#PARENTCODETranslator>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForPARENTCODETranslator = function (defaultValue, source) {
        var ctrol = $('#PARENTCODETranslator');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/MainNavigationActions.aspx/LookUpForPARENTCODETranslator", false,
                JSON.stringify({ id: $('#MainNavigationFormId').val() }),
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
    this.LookUpForRECORDSTATUSTranslatorFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#RECORDSTATUSTranslator>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForRECORDSTATUSTranslator = function (defaultValue, source) {
        var ctrol = $('#RECORDSTATUSTranslator');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/MainNavigationActions.aspx/LookUpForRECORDSTATUSTranslator", false,
                JSON.stringify({ id: $('#MainNavigationFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/MainNavigationActions.aspx/LookUpForLANGUAGEIDTranslator", false,
                JSON.stringify({ id: $('#MainNavigationFormId').val() }),
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

    this.NAVIGATION_GridTblSetup = function (table) {
        MainNavigationSupport.LookUpForPARENTCODE('');
        MainNavigationSupport.LookUpForRECORDSTATUS('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'Unique_Key',
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
                maxNestedTables: 0,
                jspdf: {                          // jsPDF / jsPDF-AutoTable related options
                    orientation:      'l',
                    unit:             'mm',
                    format:           'a4',         // One of jsPDF page formats or 'bestfit' for automatic paper format selection
                    margins:          {left: 5, right: 5, top: 10, bottom: 10},
                    split: 10,
                    autotable: {
                      styles: {
                        fontSize:     9,
                        fillColor:    255,          // Color value or 'inherit' to use css background-color from html table
                        fontStyle:    'normal',     // 'normal', 'bold', 'italic', 'bolditalic' or 'inherit' to use css font-weight and font-style from html table
                        overflow:     'linebreak',  // 'visible', 'hidden', 'ellipsize' or 'linebreak'
                        cellWidth:    'auto',
                      }
                  }
                }
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'pdf', 'xlsx'],
            toolbar: '#NAVIGATION_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true,
                formatter: 'MainNavigationSupport.selected_Formatter'
            }, {
                field: 'TYPE',
                title: $.i18n.t('app.form.NAVIGATION_GridTbl_TYPE_Title'),
                formatter: 'MainNavigationSupport.LookUpForTYPEFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CODE',
                title: $.i18n.t('app.form.NAVIGATION_GridTbl_CODE_Title'),
                events: 'NAVIGATION_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'PARENTCODE',
                title: $.i18n.t('app.form.NAVIGATION_GridTbl_PARENTCODE_Title'),
                formatter: 'MainNavigationSupport.LookUpForPARENTCODEFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'TITLE',
                title: $.i18n.t('app.form.NAVIGATION_GridTbl_TITLE_Title'),
                sortable: true,
                halign: 'center'
            }, {
                field: 'DESCRIPTION',
                title: $.i18n.t('app.form.NAVIGATION_GridTbl_DESCRIPTION_Title'),
                sortable: true,
                halign: 'center'
            }, {
                field: 'URLPATH',
                title: $.i18n.t('app.form.NAVIGATION_GridTbl_URLPATH_Title'),
                sortable: true,
                halign: 'center'
            }, {
                field: 'RECORDSTATUS',
                title: $.i18n.t('app.form.NAVIGATION_GridTbl_RECORDSTATUS_Title'),
                formatter: 'MainNavigationSupport.LookUpForRECORDSTATUSFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SMALLIMAGE',
                title: $.i18n.t('app.form.NAVIGATION_GridTbl_SMALLIMAGE_Title'),
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'BIGIMAGE',
                title: $.i18n.t('app.form.NAVIGATION_GridTbl_BIGIMAGE_Title'),
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SEQUENCE',
                title: $.i18n.t('app.form.NAVIGATION_GridTbl_SEQUENCE_Title'),
                formatter: 'MainNavigationSupport.SEQUENCE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'URLHELP',
                title: $.i18n.t('app.form.NAVIGATION_GridTbl_URLHELP_Title'),
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'ICONCSSCLASS',
                title: $.i18n.t('app.form.NAVIGATION_GridTbl_ICONCSSCLASS_Title'),
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'MODELID',
                title: $.i18n.t('app.form.NAVIGATION_GridTbl_MODELID_Title'),
                sortable: true,
                halign: 'center',
                visible: false
            }]
        });


        $('#NAVIGATION_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#NAVIGATION_GridTbl');
            $('#NAVIGATION_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#NAVIGATION_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#NAVIGATION_GridTbl').bootstrapTable('getSelections'), function (row) {		
                MainNavigationSupport.NAVIGATION_GridRowToInput(row);
                MainNavigationSupport.NAVIGATION_Grid_delete(row, null);
                
                return row.Unique_Key;
            });

            $('#NAVIGATION_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#NAVIGATION_GridCreateBtn').click(function () {
            var formInstance = $("#NAVIGATION_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            MainNavigationSupport.NAVIGATION_GridShowModal($('#NAVIGATION_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#NAVIGATION_GridPopup').find('#NAVIGATION_GridSaveBtn').click(function () {
            var formInstance = $("#NAVIGATION_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#NAVIGATION_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';
                else
                   MainNavigationSupport.newIndex = MainNavigationSupport.newIndex - 1;
                   
                var caption = $('#NAVIGATION_GridSaveBtn').html();
                $('#NAVIGATION_GridSaveBtn').html('Procesando...');
                $('#NAVIGATION_GridSaveBtn').prop('disabled', true);

                MainNavigationSupport.currentRow.Unique_Key = MainNavigationSupport.newIndex;
                MainNavigationSupport.currentRow.TYPE = parseInt(0 + $('#TYPE').val(), 10);
                MainNavigationSupport.currentRow.CODE = $('#CODE').val();
                MainNavigationSupport.currentRow.PARENTCODE = $('#PARENTCODE').val();
                MainNavigationSupport.currentRow.TITLE = $('#TITLE').val();
                MainNavigationSupport.currentRow.DESCRIPTION = $('#DESCRIPTION').val();
                MainNavigationSupport.currentRow.URLPATH = $('#URLPATH').val();
                MainNavigationSupport.currentRow.RECORDSTATUS = parseInt(0 + $('#RECORDSTATUS').val(), 10);
                MainNavigationSupport.currentRow.SMALLIMAGE = $('#SMALLIMAGE').val();
                MainNavigationSupport.currentRow.BIGIMAGE = $('#BIGIMAGE').val();
                MainNavigationSupport.currentRow.SEQUENCE = generalSupport.NumericValue('#SEQUENCE', -99999, 99999);
                MainNavigationSupport.currentRow.URLHELP = $('#URLHELP').val();
                MainNavigationSupport.currentRow.ICONCSSCLASS = $('#ICONCSSCLASS').val();
                MainNavigationSupport.currentRow.MODELID = $('#MODELID').val();

                $('#NAVIGATION_GridSaveBtn').prop('disabled', false);
                $('#NAVIGATION_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    MainNavigationSupport.NAVIGATION_Grid_update(MainNavigationSupport.currentRow, $modal);
                }
                else {                    
                    MainNavigationSupport.NAVIGATION_Grid_insert(MainNavigationSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.NAVIGATION_GridShowModal = function (md, title, row) {
        var formInstance = $("#NAVIGATION_GridEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { TYPE: 0, CODE: '', PARENTCODE: '', TITLE: '', DESCRIPTION: '', URLPATH: '', RECORDSTATUS: 0, SMALLIMAGE: '', BIGIMAGE: '', SEQUENCE: 0, URLHELP: '', ICONCSSCLASS: '', MODELID: '' };

        md.data('id', row.Unique_Key);
        md.find('.modal-title').text(title);

        MainNavigationSupport.NAVIGATION_GridRowToInput(row);
        $('#CODE').prop('disabled', (row.CODE !== ''));
        $('#SMALLIMAGE').prop('disabled', true);
        $('#BIGIMAGE').prop('disabled', true);
        $('#SEQUENCE').prop('disabled', true);
        $('#URLHELP').prop('disabled', true);
        $('#ICONCSSCLASS').prop('disabled', true);
        $('#MODELID').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.NAVIGATION_GridRowToInput = function (row) {
        MainNavigationSupport.currentRow = row;
        $('#TYPE').val(row.TYPE);
        $('#TYPE').trigger('change');
        $('#CODE').val(row.CODE);
        MainNavigationSupport.LookUpForPARENTCODE(row.PARENTCODE, '');
        $('#PARENTCODE').trigger('change');
        $('#TITLE').val(row.TITLE);
        $('#DESCRIPTION').val(row.DESCRIPTION);
        $('#URLPATH').val(row.URLPATH);
        MainNavigationSupport.LookUpForRECORDSTATUS(row.RECORDSTATUS, '');
        $('#RECORDSTATUS').trigger('change');
        $('#SMALLIMAGE').val(row.SMALLIMAGE);
        $('#BIGIMAGE').val(row.BIGIMAGE);
        AutoNumeric.set('#SEQUENCE', row.SEQUENCE);
        $('#URLHELP').val(row.URLHELP);
        $('#ICONCSSCLASS').val(row.ICONCSSCLASS);
        $('#MODELID').val(row.MODELID);

    };
    this.NAVIGATION_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/MainNavigationActions.aspx/NAVIGATION_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                NAVIGATIONDESCLANGUAGEID1: generalSupport.SessionContext().languageId
              }),
              function (data) {
                  $('#NAVIGATION_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.NAVIGATIONTranslator_GridTblSetup = function (table) {
        MainNavigationSupport.LookUpForPARENTCODETranslator('');
        MainNavigationSupport.LookUpForRECORDSTATUSTranslator('');
        MainNavigationSupport.LookUpForLANGUAGEIDTranslator('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'Unique_Key',
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
                maxNestedTables: 0,
                jspdf: {                          // jsPDF / jsPDF-AutoTable related options
                    orientation:      'l',
                    unit:             'mm',
                    format:           'a4',         // One of jsPDF page formats or 'bestfit' for automatic paper format selection
                    margins:          {left: 5, right: 5, top: 10, bottom: 10},
                    split: 10,
                    autotable: {
                      styles: {
                        fontSize:     9,
                        fillColor:    255,          // Color value or 'inherit' to use css background-color from html table
                        fontStyle:    'normal',     // 'normal', 'bold', 'italic', 'bolditalic' or 'inherit' to use css font-weight and font-style from html table
                        overflow:     'linebreak',  // 'visible', 'hidden', 'ellipsize' or 'linebreak'
                        cellWidth:    'auto',
                      }
                  }
                }
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'pdf', 'xlsx'],
            toolbar: '#NAVIGATIONTranslator_Gridtoolbar',
            columns: [{
                field: 'TYPE',
                title: $.i18n.t('app.form.NAVIGATIONTranslator_GridTbl_TYPE_Title'),
                formatter: 'MainNavigationSupport.LookUpForTYPETranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CODE',
                title: $.i18n.t('app.form.NAVIGATIONTranslator_GridTbl_CODE_Title'),
                events: 'NAVIGATIONTranslator_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'PARENTCODE',
                title: $.i18n.t('app.form.NAVIGATIONTranslator_GridTbl_PARENTCODE_Title'),
                formatter: 'MainNavigationSupport.LookUpForPARENTCODETranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'TITLE',
                title: $.i18n.t('app.form.NAVIGATIONTranslator_GridTbl_TITLE_Title'),
                sortable: true,
                halign: 'center'
            }, {
                field: 'DESCRIPTION',
                title: $.i18n.t('app.form.NAVIGATIONTranslator_GridTbl_DESCRIPTION_Title'),
                sortable: true,
                halign: 'center'
            }, {
                field: 'URLPATH',
                title: $.i18n.t('app.form.NAVIGATIONTranslator_GridTbl_URLPATH_Title'),
                sortable: true,
                halign: 'center'
            }, {
                field: 'RECORDSTATUS',
                title: $.i18n.t('app.form.NAVIGATIONTranslator_GridTbl_RECORDSTATUS_Title'),
                formatter: 'MainNavigationSupport.LookUpForRECORDSTATUSTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LANGUAGEID',
                title: $.i18n.t('app.form.NAVIGATIONTranslator_GridTbl_LANGUAGEID_Title'),
                formatter: 'MainNavigationSupport.LookUpForLANGUAGEIDTranslatorFormatter',
                sortable: true,
                halign: 'center'
            }]
        });


        $('#NAVIGATIONTranslator_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#NAVIGATIONTranslator_GridTbl');
            $('#NAVIGATIONTranslator_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#NAVIGATIONTranslator_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#NAVIGATIONTranslator_GridTbl').bootstrapTable('getSelections'), function (row) {		
                MainNavigationSupport.NAVIGATIONTranslator_GridRowToInput(row);
                
                
                return row.Unique_Key;
            });
            
          $('#NAVIGATIONTranslator_GridTbl').bootstrapTable('remove', {
                field: 'Unique_Key',
                values: ids
           });

            $('#NAVIGATIONTranslator_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#NAVIGATIONTranslator_GridCreateBtn').click(function () {
            var formInstance = $("#NAVIGATIONTranslator_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            MainNavigationSupport.NAVIGATIONTranslator_GridShowModal($('#NAVIGATIONTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#NAVIGATIONTranslator_GridPopup').find('#NAVIGATIONTranslator_GridSaveBtn').click(function () {
            var formInstance = $("#NAVIGATIONTranslator_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#NAVIGATIONTranslator_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';
                else
                   MainNavigationSupport.newIndex = MainNavigationSupport.newIndex - 1;
                   
                var caption = $('#NAVIGATIONTranslator_GridSaveBtn').html();
                $('#NAVIGATIONTranslator_GridSaveBtn').html('Procesando...');
                $('#NAVIGATIONTranslator_GridSaveBtn').prop('disabled', true);

                MainNavigationSupport.currentRow.Unique_Key = MainNavigationSupport.newIndex;
                MainNavigationSupport.currentRow.TYPE = parseInt(0 + $('#TYPETranslator').val(), 10);
                MainNavigationSupport.currentRow.CODE = $('#CODETranslator').val();
                MainNavigationSupport.currentRow.PARENTCODE = $('#PARENTCODETranslator').val();
                MainNavigationSupport.currentRow.TITLE = $('#TITLETranslator').val();
                MainNavigationSupport.currentRow.DESCRIPTION = $('#DESCRIPTIONTranslator').val();
                MainNavigationSupport.currentRow.URLPATH = $('#URLPATHTranslator').val();
                MainNavigationSupport.currentRow.RECORDSTATUS = parseInt(0 + $('#RECORDSTATUSTranslator').val(), 10);
                MainNavigationSupport.currentRow.LANGUAGEID = parseInt(0 + $('#LANGUAGEIDTranslator').val(), 10);

                $('#NAVIGATIONTranslator_GridSaveBtn').prop('disabled', false);
                $('#NAVIGATIONTranslator_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    MainNavigationSupport.NAVIGATIONTranslator_Grid_update(MainNavigationSupport.currentRow, $modal);
                }
                else {                    
                    $('#NAVIGATIONTranslator_GridTbl').bootstrapTable('append', MainNavigationSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.NAVIGATIONTranslator_GridShowModal = function (md, title, row) {
        var formInstance = $("#NAVIGATIONTranslator_GridEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { TYPE: 0, CODE: '', PARENTCODE: '', TITLE: '', DESCRIPTION: '', URLPATH: '', RECORDSTATUS: 0, LANGUAGEID: 0 };

        md.data('id', row.Unique_Key);
        md.find('.modal-title').text(title);

        MainNavigationSupport.NAVIGATIONTranslator_GridRowToInput(row);
        $('#TYPETranslator').prop('disabled', (row.CODE !== ''));
        $('#CODETranslator').prop('disabled', true);
        $('#PARENTCODETranslator').prop('disabled', (row.CODE !== ''));
        $('#URLPATHTranslator').prop('disabled', (row.CODE !== ''));
        $('#RECORDSTATUSTranslator').prop('disabled', true);
        $('#LANGUAGEIDTranslator').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.NAVIGATIONTranslator_GridRowToInput = function (row) {
        MainNavigationSupport.currentRow = row;
        $('#TYPETranslator').val(row.TYPE);
        $('#TYPETranslator').trigger('change');
        $('#CODETranslator').val(row.CODE);
        MainNavigationSupport.LookUpForPARENTCODETranslator(row.PARENTCODE, '');
        $('#PARENTCODETranslator').trigger('change');
        $('#TITLETranslator').val(row.TITLE);
        $('#DESCRIPTIONTranslator').val(row.DESCRIPTION);
        $('#URLPATHTranslator').val(row.URLPATH);
        MainNavigationSupport.LookUpForRECORDSTATUSTranslator(row.RECORDSTATUS, '');
        $('#RECORDSTATUSTranslator').trigger('change');
        MainNavigationSupport.LookUpForLANGUAGEIDTranslator(row.LANGUAGEID, '');
        $('#LANGUAGEIDTranslator').trigger('change');

    };
    this.NAVIGATIONTranslator_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/MainNavigationActions.aspx/NAVIGATIONTranslator_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#NAVIGATIONTranslator_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.SEQUENCE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };


	    this.selected_Formatter = function (value, row, index) {          
          return {
                  disabled: $('#NAVIGATION_GridTbl *').prop('disabled'),
                  checked: value
                 }
      };



  this.Init = function(){
    
    moment.locale(app.user.languageName);
    
   generalSupport.TranslateInit('MainNavigation', function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        MainNavigationSupport.ValidateSetup();
        
        

    MainNavigationSupport.ControlBehaviour();
    MainNavigationSupport.ControlActions();
    

    app.core.AsyncWebMethod("/fasi/dli/forms/MainNavigationActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), false,
            JSON.stringify({
                id: $('#MainNavigationFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
               if (data.d.Success === true) {
                  MainNavigationSupport.CallRenderLookUps(data);
                
            
                $("#NAVIGATION_GridTblPlaceHolder").replaceWith('<table id="NAVIGATION_GridTbl"></table>');
    MainNavigationSupport.NAVIGATION_GridTblSetup($('#NAVIGATION_GridTbl'));
    $("#NAVIGATIONTranslator_GridTblPlaceHolder").replaceWith('<table id="NAVIGATIONTranslator_GridTbl"></table>');
    MainNavigationSupport.NAVIGATIONTranslator_GridTblSetup($('#NAVIGATIONTranslator_GridTbl'));

                    MainNavigationSupport.NAVIGATION_GridTblRequest();
        MainNavigationSupport.NAVIGATIONTranslator_GridTblRequest();

            
            
            
             }
});

   }, 'dli/forms');
  };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: ['Administrador'],
        Element: $("#MainNavigationMainForm"),
        CallBack: MainNavigationSupport.Init
    });
});

window.NAVIGATION_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        MainNavigationSupport.NAVIGATION_GridShowModal($('#NAVIGATION_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.NAVIGATIONTranslator_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        MainNavigationSupport.NAVIGATIONTranslator_GridShowModal($('#NAVIGATIONTranslator_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
