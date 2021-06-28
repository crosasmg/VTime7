var WidgetsByRoleManagerSupport = new function () {

    this.currentRow = {};
    this.newIndex = -1;
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#WidgetsByRoleManagerFormId').val(),
            RoleWidget_RoleWidget: generalSupport.NormalizeProperties($('#RoleWidgetTbl').bootstrapTable('getData'), ''),
            languageId: generalSupport.NumericValue('#languageId', -99999, 99999),
            RolFilter: parseInt(0 + $('#Filter').val(), 10)
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#WidgetsByRoleManagerFormId').val(data.InstanceFormId);
        AutoNumeric.set('#languageId', data.languageId);

        WidgetsByRoleManagerSupport.LookUpForWidgetId(source);
        WidgetsByRoleManagerSupport.LookUpForFilter(data.RolFilter, source);

        if (data.RoleWidget_RoleWidget !== null)
            $('#RoleWidgetTbl').bootstrapTable('load', data.RoleWidget_RoleWidget);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#Id', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
        });
      new AutoNumeric('#RoleId', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
        });
      new AutoNumeric('#Secuense', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: -999
        });
      new AutoNumeric('#languageId', {
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
               WidgetsByRoleManagerSupport.ObjectToInput(data.d.Data.Instance, source);
            
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



    this.RoleWidget_BeforeShowPopup = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#RoleWidgetSaveBtn'));          
            var LanguageIdValue;
        $('#RoleId').toggleClass('hidden', true);
        $('#RoleIdLabel').toggleClass('hidden', true);
        $('#RoleIdRequired').toggleClass('hidden', true);
        $('#Id').toggleClass('hidden', true);
        $('#IdLabel').toggleClass('hidden', true);
        $('#IdRequired').toggleClass('hidden', true);
        if (row.Id === 0){
                    AutoNumeric.set('#RoleId', $('#Filter').val());

               $.ajax({
                    type: "GET",
                    url: constants.fasiApi.base + 'fasi/v1/RoleWidgetsIndex',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({  }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                                     if (data.Successfully === true) {
                   AutoNumeric.set('#Id', data.Data);

           
            }
            else
           generalSupport.NotifyFail(data.Reason, data.Code);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
            }

    };
    this.RoleWidget_update = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#RoleWidgetSaveBtn'));          
            var UpdateResult;
            var errors;
            var ResultClean;
            var reason;
            var stringEmpty;
        stringEmpty = "";
               $.ajax({
                    type: "PUT",
                    url: constants.fasiApi.base + 'fasi/v1/RoleWidgetsUpdate',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({ Id: generalSupport.NumericValue('#Id', -999999999, 999999999), WidgetId: parseInt(0 + $('#WidgetId').val(), 10), IsDefault: $('#IsDefault').is(':checked'), IsEditAllow: $('#IsEditAllow').is(':checked'), IsEditAlowTitle: $('#IsEditAlowTitle').is(':checked'), Secuense: generalSupport.NumericValue('#Secuense', -999, 999), RoleId: parseInt(0 + $('#Filter').val(), 10) }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                         UpdateResult = data.Successfully;
reason = data.Reason;

            if (UpdateResult === true && reason === stringEmpty){
                var message4 = $.i18n.t('app.form.RoleWidget_Message_Notify_update4');
                notification.toastr.success($.i18n.t('app.form.RoleWidget_Title_Notify_update4'), message4);
                $('#RoleWidgetTbl').bootstrapTable('updateByUniqueId', { id: row.Id, row: row });
                $modal.modal('hide');
                }                
                else {
                var message6 = $.i18n.t('app.form.RoleWidget_Message_Notify_update6');
                message6 = message6.replace(/{{reason}}/g, reason);
                notification.swal.error($.i18n.t('app.form.RoleWidget_Title_Notify_update6'), message6);

                    }
        data = app.core.SyncWebMethod("/fasi/dli/forms/WidgetsByRoleManagerActions.aspx/Removec71871ebc4cd4fe2a856d1cf959d07eb", false,
               JSON.stringify({ KEY: 'UserAnonymous_UserId' }));
               

               $.ajax({
                    type: "GET",
                    url: constants.fasiApi.base + 'fasi/v1/CacheCleanByKey?Key=FASI_UserAnonymous',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({  }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                                     if (data.Successfully === true) {
           ResultClean = data.Successfully;

           
            }
            else
           generalSupport.NotifyFail(data.Reason, data.Code);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
    };
    this.RoleWidget_insert = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#RoleWidgetSaveBtn'));          
            var AddResult;
            var errors;
            var ResultClean;
            var reason;
            var stringEmpty;
        stringEmpty = "";
               $.ajax({
                    type: "POST",
                    url: constants.fasiApi.base + 'fasi/v1/RoleWidgetsAdd',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({ Id: generalSupport.NumericValue('#Id', -999999999, 999999999), WidgetId: parseInt(0 + $('#WidgetId').val(), 10), IsDefault: $('#IsDefault').is(':checked'), IsEditAllow: $('#IsEditAllow').is(':checked'), Secuense: generalSupport.NumericValue('#Secuense', -999, 999), RoleId: parseInt(0 + $('#Filter').val(), 10), IsEditAlowTitle: $('#IsEditAlowTitle').is(':checked') }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                         reason = data.Reason;
AddResult = data.Successfully;

            if (AddResult === true && reason === stringEmpty){
                $('#RoleWidgetTbl').bootstrapTable('append', row);
                $modal.modal('hide');
                var message5 = $.i18n.t('app.form.RoleWidget_Message_Notify_insert5');
                notification.toastr.success($.i18n.t('app.form.RoleWidget_Title_Notify_insert5'), message5);
                }                
                else {
                var message6 = $.i18n.t('app.form.RoleWidget_Message_Notify_insert6');
                message6 = message6.replace(/{{reason}}/g, reason);
                notification.swal.error($.i18n.t('app.form.RoleWidget_Title_Notify_insert6'), message6);

                    }
        data = app.core.SyncWebMethod("/fasi/dli/forms/WidgetsByRoleManagerActions.aspx/Remove6c95e9703e9c4056b98e9d27a73720b2", false,
               JSON.stringify({ KEY: 'UserAnonymous_UserId' }));
               

               $.ajax({
                    type: "GET",
                    url: constants.fasiApi.base + 'fasi/v1/CacheCleanByKey?Key=FASI_UserAnonymous',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({  }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                                     if (data.Successfully === true) {
           ResultClean = data.Successfully;

           
            }
            else
           generalSupport.NotifyFail(data.Reason, data.Code);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
    };
    this.RoleWidget_delete = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#RoleWidgetSaveBtn'));          
            var DeleteResult;
            var errors;
            var ResultClean;
               $.ajax({
                    type: "DELETE",
                    url: constants.fasiApi.base + 'fasi/v1/RoleWidgetDelete?Id=' + generalSupport.NumericValue('#Id', -999999999, 999999999),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({  }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                         DeleteResult = data.Successfully;

            if (DeleteResult === true){
                $('#RoleWidgetTbl').bootstrapTable('remove', {field: 'Id', values: [generalSupport.NumericValue('#Id', -999999999, 999999999)]});
                $('#RoleWidgetTbl').bootstrapTable('refresh');
                var message4 = $.i18n.t('app.form.RoleWidget_Message_Notify_delete4');
                notification.toastr.success($.i18n.t('app.form.RoleWidget_Title_Notify_delete4'), message4);
                }                
                else {
                var message5 = $.i18n.t('app.form.RoleWidget_Message_Notify_delete5');
                notification.toastr.error($.i18n.t('app.form.RoleWidget_Title_Notify_delete5'), message5);

                    }
        data = app.core.SyncWebMethod("/fasi/dli/forms/WidgetsByRoleManagerActions.aspx/Remove8da251281f784319a3cbc3dcada0755e", false,
               JSON.stringify({ KEY: 'UserAnonymous_UserId' }));
               

               $.ajax({
                    type: "GET",
                    url: constants.fasiApi.base + 'fasi/v1/CacheCleanByKey?Key=FASI_UserAnonymous',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({  }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                                     if (data.Successfully === true) {
           ResultClean = data.Successfully;

           
            }
            else
           generalSupport.NotifyFail(data.Reason, data.Code, true);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown, true);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown, true);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
    };

    this.ControlActions =   function () {

        $('#WidgetId').change(function () {
                      $(this).valid()


        });
        $('#Filter').change(function () {
                      var data;
                $('#RoleWidgetCreateBtn').prop('disabled', true);
                $('#RoleWidgetTbl').bootstrapTable('load', []);


        });
        $('#button1').click(function (event) {
            var formInstance = $("#WidgetsByRoleManagerMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
            var errors;
                var btnLoading = Ladda.create(document.querySelector('#button1'));
                btnLoading.start();
                if (parseInt(0 + $('#Filter').val(), 10) === 0){
                    var message2 = $.i18n.t('app.form.button1_Message_Notify_click2');
                    notification.swal.error($.i18n.t('app.form.button1_Title_Notify_click2'), message2);
                    }                    
                    else {
               $.ajax({
                    type: "GET",
                    url: constants.fasiApi.base + 'fasi/v1/WidgetByRole?RoleId=' + parseInt(0 + $('#Filter').val(), 10) + '&languageId=' + generalSupport.NumericValue('#languageId', -99999, 99999),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({  }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                                     if (data.Successfully === true) {
           $('#RoleWidgetTbl').bootstrapTable('load', data.Data.Items);

           
            }
            else
           generalSupport.NotifyFail(data.Reason, data.Code);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });                    $('#RoleWidgetCreateBtn').prop('disabled', false);

                        }
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#WidgetsByRoleManagerMainForm").validate({
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
                languageId: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999
                },
                Filter: {
                }
            },
            messages: {
                languageId: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -99999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999'
                },
                Filter: {
                }
            }
        });
        $("#RoleWidgetEditForm").validate().destroy();
        $("#RoleWidgetEditForm").validate({
            rules: {
                Id: {
                    AutoNumericMinValue: -999999999,
                    AutoNumericMaxValue: 999999999,
                    required: true
                },
                RoleId: {
                    AutoNumericMinValue: -999999999,
                    AutoNumericMaxValue: 999999999,
                    required: true
                },
                WidgetId: {
                    required: true                },
                Secuense: {
                    AutoNumericMinValue: -999,
                    AutoNumericMaxValue: 999,
                    required: true
                }

            },
            messages: {
                Id: {
                    AutoNumericMinValue: $.i18n.t('app.validation.Id.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.Id.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.Id.required')
                },
                RoleId: {
                    AutoNumericMinValue: $.i18n.t('app.validation.RoleId.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.RoleId.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.RoleId.required')
                },
                WidgetId: {
                    required: $.i18n.t('app.validation.WidgetId.required')                },
                Secuense: {
                    AutoNumericMinValue: $.i18n.t('app.validation.Secuense.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.Secuense.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.Secuense.required')
                }

            }
        });

    };
    this.LookUpForWidgetIdFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#WidgetId>option[value='" + value + "']").text();
        }
        return '<a href="javascript:void(0)" class="update edit" data-modal-title="Editar" title="Haga click para editar">' + result + '</a>';
    };
   this.LookUpForWidgetId = function (defaultValue, source) {
        var ctrol = $('#WidgetId');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "GET",
                url: constants.fasiApi.base + 'fasi/v1/WidgetsLkp?widgetIds=*&' + 'languageId=-1',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: JSON.stringify({  }),
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                success: function (data) {
                    ctrol.children().remove();
                    if (data.Successfully === true) {
                        
                        data.Data.forEach(function (element) {
                            ctrol.append($('<option />').val(element.Code).text(element.Description));
                        });

                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
						                if(source !== 'Initialization')
                              ctrol.change();
                              
                            
                            
                    }
                    else
                        generalSupport.NotifyFail(data.d.Reason, data.d.Code);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
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
   this.LookUpForFilter = function (defaultValue, source) {
        var ctrol = $('#Filter');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "GET",
                url: constants.fasiApi.base + 'Members/v1/RolesLkp',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                success: function (data) {
                    ctrol.children().remove();
                    if (data.Successfully === true) {
                        ctrol.append($('<option />').val(0).text('Indique un rol'));
                        data.Data.forEach(function (element) {
                            ctrol.append($('<option />').val(element.Code).text(element.Description));
                        });

                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
						                if(source !== 'Initialization')
                              ctrol.change();
                              
                            
                            
                    }
                    else
                        generalSupport.NotifyFail(data.d.Reason, data.d.Code);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
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

    this.RoleWidgetTblSetup = function (table) {
        WidgetsByRoleManagerSupport.LookUpForWidgetId('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'Id',
            sortable: true,
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
                },
                date: {
                    html: generalSupport.DateFormat()
                },
                mso: {
                    xslx: {
                        fileFormat: 'xlsx',
                        formatId: {
                            date: 14,
                            numbers: 0
                        }
                    }
                },
                numbers: {
                    html: {
                        decimalMark: generalSupport.DecimalCharacter(),
                        thousandsSeparator: generalSupport.DigitGroupSeparator()
                    }
                }
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'pdf', 'xlsx'],
            toolbar: '#RoleWidgettoolbar',
            columns: [{
                field: 'selected',
                checkbox: true,
                formatter: 'WidgetsByRoleManagerSupport.selected_Formatter'
            }, {
                field: 'Id',
                title: $.i18n.t('app.form.RoleWidgetTbl_Id_Title'),
                formatter: 'WidgetsByRoleManagerSupport.Id_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'RoleId',
                title: $.i18n.t('app.form.RoleWidgetTbl_RoleId_Title'),
                formatter: 'WidgetsByRoleManagerSupport.RoleId_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'WidgetId',
                title: $.i18n.t('app.form.RoleWidgetTbl_WidgetId_Title'),
                events: 'RoleWidgetActionEvents',
                formatter: 'WidgetsByRoleManagerSupport.LookUpForWidgetIdFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Secuense',
                title: $.i18n.t('app.form.RoleWidgetTbl_Secuense_Title'),
                formatter: 'WidgetsByRoleManagerSupport.Secuense_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'IsDefault',
                title: $.i18n.t('app.form.RoleWidgetTbl_IsDefault_Title'),
                formatter: 'WidgetsByRoleManagerSupport.IsDefault_IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'IsEditAllow',
                title: $.i18n.t('app.form.RoleWidgetTbl_IsEditAllow_Title'),
                formatter: 'WidgetsByRoleManagerSupport.IsEditAllow_IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'IsEditAlowTitle',
                title: $.i18n.t('app.form.RoleWidgetTbl_IsEditAlowTitle_Title'),
                formatter: 'WidgetsByRoleManagerSupport.IsEditAlowTitle_IsCheck',
                sortable: true,
                halign: 'center'
            }]
        });


        $('#RoleWidgetTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#RoleWidgetTbl');
            $('#RoleWidgetRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#RoleWidgetRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#RoleWidgetTbl').bootstrapTable('getSelections'), function (row) {		
                WidgetsByRoleManagerSupport.RoleWidgetRowToInput(row);
                WidgetsByRoleManagerSupport.RoleWidget_delete(row, null);
                
                return row.Id;
            });

            $('#RoleWidgetRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#RoleWidgetCreateBtn').click(function () {
            var formInstance = $("#RoleWidgetEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            WidgetsByRoleManagerSupport.RoleWidgetShowModal($('#RoleWidgetPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#RoleWidgetPopup').find('#RoleWidgetSaveBtn').click(function () {
            var formInstance = $("#RoleWidgetEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#RoleWidgetPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                {
                    wm = 'Update';
                }    
                else
                {
                   WidgetsByRoleManagerSupport.newIndex = WidgetsByRoleManagerSupport.newIndex - 1;
                   
                }
                
                var caption = $('#RoleWidgetSaveBtn').html();
                $('#RoleWidgetSaveBtn').html('Procesando...');
                $('#RoleWidgetSaveBtn').prop('disabled', true);

                WidgetsByRoleManagerSupport.currentRow.Id = generalSupport.NumericValue('#Id', -999999999, 999999999);
                WidgetsByRoleManagerSupport.currentRow.RoleId = generalSupport.NumericValue('#RoleId', -999999999, 999999999);
                WidgetsByRoleManagerSupport.currentRow.WidgetId = parseInt(0 + $('#WidgetId').val(), 10);
                WidgetsByRoleManagerSupport.currentRow.Secuense = generalSupport.NumericValue('#Secuense', -999, 999);
                WidgetsByRoleManagerSupport.currentRow.IsDefault = $('#IsDefault').is(':checked');
                WidgetsByRoleManagerSupport.currentRow.IsEditAllow = $('#IsEditAllow').is(':checked');
                WidgetsByRoleManagerSupport.currentRow.IsEditAlowTitle = $('#IsEditAlowTitle').is(':checked');

                $('#RoleWidgetSaveBtn').prop('disabled', false);
                $('#RoleWidgetSaveBtn').html(caption);

                if (wm === 'Update') {
                    WidgetsByRoleManagerSupport.RoleWidget_update(WidgetsByRoleManagerSupport.currentRow, $modal);
                }
                else {                    
                    WidgetsByRoleManagerSupport.RoleWidget_insert(WidgetsByRoleManagerSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.RoleWidgetShowModal = function (md, title, row) {
        var formInstance = $("#RoleWidgetEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { Id: 0, RoleId: 0, WidgetId: 0, Secuense: 1, IsDefault: null, IsEditAllow: null, IsEditAlowTitle: null };

        md.data('id', row.Id);
        md.find('.modal-title').text(title);

        WidgetsByRoleManagerSupport.RoleWidgetRowToInput(row);
        $('#Id').prop('disabled', true);
        $('#RoleId').prop('disabled', true);
        WidgetsByRoleManagerSupport.RoleWidget_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.RoleWidgetRowToInput = function (row) {
        WidgetsByRoleManagerSupport.currentRow = row;
        AutoNumeric.set('#Id', row.Id);
        AutoNumeric.set('#RoleId', row.RoleId);
        WidgetsByRoleManagerSupport.LookUpForWidgetId(row.WidgetId, '');
        $('#WidgetId').trigger('change');
        AutoNumeric.set('#Secuense', row.Secuense);
        $('#IsDefault').prop("checked", row.IsDefault);
        $('#IsEditAllow').prop("checked", row.IsEditAllow);
        $('#IsEditAlowTitle').prop("checked", row.IsEditAlowTitle);

    };





    this.Id_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
        });
      };
    this.RoleId_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
        });
      };
    this.Secuense_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: -999
        });
      };


	    this.selected_Formatter = function (value, row, index) {          
          return {
                  disabled: $('#RoleWidgetTbl *').prop('disabled'),
                  checked: value
                 }
      };
	    this.IsDefault_IsCheck = function (value, row, index) {
        var icon = "";
        
        if (value === false) {
            icon = "glyphicon glyphicon-unchecked";
        } else {
            icon = "glyphicon glyphicon-check";
        }
       
        return '<div class="text-center" >' +
            '<i class="' + icon + '"></i>' +
            '</div>';
      };
	    this.IsEditAllow_IsCheck = function (value, row, index) {
        var icon = "";
        
        if (value === false) {
            icon = "glyphicon glyphicon-unchecked";
        } else {
            icon = "glyphicon glyphicon-check";
        }
       
        return '<div class="text-center" >' +
            '<i class="' + icon + '"></i>' +
            '</div>';
      };
	    this.IsEditAlowTitle_IsCheck = function (value, row, index) {
        var icon = "";
        
        if (value === false) {
            icon = "glyphicon glyphicon-unchecked";
        } else {
            icon = "glyphicon glyphicon-check";
        }
       
        return '<div class="text-center" >' +
            '<i class="' + icon + '"></i>' +
            '</div>';
      };



  this.Init = function(){
    
    moment.locale(app.user.languageName);
    
   generalSupport.TranslateInit('WidgetsByRoleManager', function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        WidgetsByRoleManagerSupport.ValidateSetup();
        
        

    WidgetsByRoleManagerSupport.ControlBehaviour();
    WidgetsByRoleManagerSupport.ControlActions();
    

    app.core.AsyncWebMethod("/fasi/dli/forms/WidgetsByRoleManagerActions.aspx/Initialization", false,
            JSON.stringify({
                id: $('#WidgetsByRoleManagerFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
               if (data.d.Success === true) {
                  WidgetsByRoleManagerSupport.CallRenderLookUps(data);
                
                AutoNumeric.set('#languageId', generalSupport.URLNumericValue('languageId'));
        WidgetsByRoleManagerSupport.LookUpForFilter(generalSupport.URLNumericValue('RolFilter'));

                $("#RoleWidgetTblPlaceHolder").replaceWith('<table id="RoleWidgetTbl"></table>');
    WidgetsByRoleManagerSupport.RoleWidgetTblSetup($('#RoleWidgetTbl'));

            
            
            
                        AutoNumeric.set('#languageId', constants.defaultLanguageId);


             }
});

   }, 'dli/forms');
  };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: [],
        Element: $("#WidgetsByRoleManagerMainForm"),
        CallBack: WidgetsByRoleManagerSupport.Init
    });
});

window.RoleWidgetActionEvents = {
    'click .update': function (e, value, row, index) {
        WidgetsByRoleManagerSupport.RoleWidgetShowModal($('#RoleWidgetPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
