var WidgetManagerSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#WidgetManagerFormId').val(),
            Widget_Widget: generalSupport.NormalizeProperties($('#WidgetTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#WidgetManagerFormId').val(data.InstanceFormId);

        WidgetManagerSupport.LookUpForIcon(source);

        if (data.Widget_Widget !== null)
            $('#WidgetTbl').bootstrapTable('load', data.Widget_Widget);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#Id', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      new AutoNumeric('#LanguageId', {
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
               WidgetManagerSupport.ObjectToInput(data.d.Data.Instance, source);
            
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



    this.Widget_BeforeShowPopup = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#WidgetSaveBtn'));          
        if (row.Id === 0){
               $.ajax({
                    type: "GET",
                    url: constants.fasiApi.base + 'fasi/v1/WidgetIndex',
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

                                   AutoNumeric.set('#LanguageId', constants.defaultLanguageId);


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
    this.Widget_insert = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#WidgetSaveBtn'));          
            var WidgetAddResult;
            var errors;
               $.ajax({
                    type: "POST",
                    url: constants.fasiApi.base + 'fasi/v1/WidgetAdd',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({ Id: generalSupport.NumericValue('#Id', -99999, 99999), Description: $('#Description').val(), Url: $('#Url').val(), Icon: $('#Icon').val(), Title: $('#Title').val(), DefaultState: $('#DefaultState').val(), LanguageId: generalSupport.NumericValue('#LanguageId', -99999, 99999) }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                                     if (data.Successfully === true) {
           WidgetAddResult = data.Successfully;

                       if (WidgetAddResult === true){
                $('#WidgetTbl').bootstrapTable('append', row);
                $modal.modal('hide');
                var message4 = $.i18n.t('app.form.Widget_Message_Notify_insert4');
                notification.toastr.success($.i18n.t('app.form.Widget_Title_Notify_insert4'), message4);
                }                
                else {
                var message5 = $.i18n.t('app.form.Widget_Message_Notify_insert5');
                notification.toastr.error($.i18n.t('app.form.Widget_Title_Notify_insert5'), message5);

                    }

            }
            else
           generalSupport.NotifyFail(data.Reason, data.Code);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
    };
    this.Widget_update = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#WidgetSaveBtn'));          
            var WidgetUpdateResult;
            var errors;
               $.ajax({
                    type: "PUT",
                    url: constants.fasiApi.base + 'fasi/v1/WidgetUpdate',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({ Id: generalSupport.NumericValue('#Id', -99999, 99999), Description: $('#Description').val(), LanguageId: generalSupport.NumericValue('#LanguageId', -99999, 99999), Url: $('#Url').val(), Icon: $('#Icon').val(), Title: $('#Title').val(), DefaultState: $('#DefaultState').val() }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                                     if (data.Successfully === true) {
           WidgetUpdateResult = data.Successfully;

                       if (WidgetUpdateResult === true){
                var message3 = $.i18n.t('app.form.Widget_Message_Notify_update3');
                notification.toastr.success($.i18n.t('app.form.Widget_Title_Notify_update3'), message3);
                $('#WidgetTbl').bootstrapTable('updateByUniqueId', { id: row.Id, row: row });
                $modal.modal('hide');
                }                
                else {
                var message5 = $.i18n.t('app.form.Widget_Message_Notify_update5');
                notification.toastr.error($.i18n.t('app.form.Widget_Title_Notify_update5'), message5);

                    }

            }
            else
           generalSupport.NotifyFail(data.Reason, data.Code);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
    };
    this.Widget_delete = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#WidgetSaveBtn'));          
            var WidgetDeleteResult;
            var errors;
            var WidgetRolesResult;
               $.ajax({
                    type: "POST",
                    url: constants.fasiApi.base + 'fasi/v1/RolesByWidgetId?WidgetId=' + generalSupport.NumericValue('#Id', -99999, 99999),
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
                         WidgetRolesResult = data.Data;

            if (WidgetRolesResult === ''){
               $.ajax({
                    type: "DELETE",
                    url: constants.fasiApi.base + 'fasi/v1/WidgetDelete?Id=' + generalSupport.NumericValue('#Id', -99999, 99999),
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
           WidgetDeleteResult = data.Successfully;

                               if (WidgetDeleteResult === true){
                        var message5 = $.i18n.t('app.form.Widget_Message_Notify_delete5');
                        notification.toastr.success($.i18n.t('app.form.Widget_Title_Notify_delete5'), message5);
                        $('#WidgetTbl').bootstrapTable('remove', {field: 'Id', values: [generalSupport.NumericValue('#Id', -99999, 99999)]});
                            }                            
                            else {
                        var message7 = $.i18n.t('app.form.Widget_Message_Notify_delete7');
                        notification.toastr.error($.i18n.t('app.form.Widget_Title_Notify_delete7'), message7);

                                }

            }
            else
           generalSupport.NotifyFail(data.Reason, data.Code, true);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown, true);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });                        }                        
                        else {
                var message9 = $.i18n.t('app.form.Widget_Message_Notify_delete9');
                message9 = message9.replace(/{{WidgetRolesResult}}/g, WidgetRolesResult);
                notification.toastr.error($.i18n.t('app.form.Widget_Title_Notify_delete9'), message9);

                            }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown, true);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
    };

    this.ControlActions =   function () {

        $('#Icon').change(function () {
                      $(this).valid()


        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#WidgetManagerMainForm").validate({
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
        $("#WidgetEditForm").validate().destroy();
        $("#WidgetEditForm").validate({
            rules: {
                Id: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999,
                    required: true
                },
                Title: {
                    required: true,
                    maxlength: 255
                },
                Description: {
                    maxlength: 255
                },
                Url: {
                    required: true,
                    maxlength: 255
                },
                Icon: {
                    required: true                },
                LanguageId: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999
                }

            },
            messages: {
                Id: {
                    AutoNumericMinValue: $.i18n.t('app.validation.Id.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.Id.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.Id.required')
                },
                Title: {
                    required: $.i18n.t('app.validation.Title.required'),
                    maxlength: $.i18n.t('app.validation.Title.maxlength')
                },
                Description: {
                    maxlength: $.i18n.t('app.validation.Description.maxlength')
                },
                Url: {
                    required: $.i18n.t('app.validation.Url.required'),
                    maxlength: $.i18n.t('app.validation.Url.maxlength')
                },
                Icon: {
                    required: $.i18n.t('app.validation.Icon.required')                },
                LanguageId: {
                    AutoNumericMinValue: $.i18n.t('app.validation.LanguageId.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.LanguageId.AutoNumericMaxValue')
                }

            }
        });

    };
    this.LookUpForIconFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#Icon>option[value='" + value + "']").text();
        }
        return result;
    };
   this.LookUpForIcon = function (defaultValue, source) {
        var ctrol = $('#Icon');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "GET",
                url: constants.fasiApi.base + 'fasi/v1/LookUps?Key=WigetIcon&languageId=1',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                success: function (data) {
                    ctrol.children().remove();
                    if (data.Successfully === true) {
                        
                        data.Data.forEach(function (element) {
                            ctrol.append($('<option />').val(element.Description).text(element.Description));
                        });

                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
						                if(source !== 'Initialization')
                              ctrol.change();
                              
                            $('#Icon').select2({templateResult:WidgetIconTemplate, templateSelection:WidgetIconTemplate});
                            
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

    this.WidgetTblSetup = function (table) {
        WidgetManagerSupport.LookUpForIcon('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'Id',
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
            toolbar: '#Widgettoolbar',
            columns: [{
                field: 'selected',
                checkbox: true,
                formatter: 'WidgetManagerSupport.selected_Formatter'
            }, {
                field: 'Id',
                title: $.i18n.t('app.form.WidgetTbl_Id_Title'),
                formatter: 'WidgetManagerSupport.Id_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'Title',
                title: $.i18n.t('app.form.WidgetTbl_Title_Title'),
                events: 'WidgetActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'Description',
                title: $.i18n.t('app.form.WidgetTbl_Description_Title'),
                sortable: false,
                halign: 'center'
            }, {
                field: 'Url',
                title: $.i18n.t('app.form.WidgetTbl_Url_Title'),
                sortable: false,
                halign: 'center'
            }, {
                field: 'Icon',
                title: $.i18n.t('app.form.WidgetTbl_Icon_Title'),
                formatter: 'WidgetIconFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'DefaultState',
                title: $.i18n.t('app.form.WidgetTbl_DefaultState_Title'),
                sortable: false,
                halign: 'center',
                visible: false
            }, {
                field: 'LanguageId',
                title: $.i18n.t('app.form.WidgetTbl_LanguageId_Title'),
                formatter: 'WidgetManagerSupport.LanguageId_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right',
                visible: false
            }]
        });


        $('#WidgetTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#WidgetTbl');
            $('#WidgetRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#WidgetRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#WidgetTbl').bootstrapTable('getSelections'), function (row) {		
                WidgetManagerSupport.WidgetRowToInput(row);
                WidgetManagerSupport.Widget_delete(row, null);
                
                return row.Id;
            });

            $('#WidgetRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#WidgetCreateBtn').click(function () {
            var formInstance = $("#WidgetEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            WidgetManagerSupport.WidgetShowModal($('#WidgetPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#WidgetPopup').find('#WidgetSaveBtn').click(function () {
            var formInstance = $("#WidgetEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#WidgetPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#WidgetSaveBtn').html();
                $('#WidgetSaveBtn').html('Procesando...');
                $('#WidgetSaveBtn').prop('disabled', true);

                WidgetManagerSupport.currentRow.Id = generalSupport.NumericValue('#Id', -99999, 99999);
                WidgetManagerSupport.currentRow.Title = $('#Title').val();
                WidgetManagerSupport.currentRow.Description = $('#Description').val();
                WidgetManagerSupport.currentRow.Url = $('#Url').val();
                WidgetManagerSupport.currentRow.Icon = $('#Icon').val();
                WidgetManagerSupport.currentRow.DefaultState = $('#DefaultState').val();
                WidgetManagerSupport.currentRow.LanguageId = generalSupport.NumericValue('#LanguageId', -99999, 99999);

                $('#WidgetSaveBtn').prop('disabled', false);
                $('#WidgetSaveBtn').html(caption);

                if (wm === 'Update') {
                    WidgetManagerSupport.Widget_update(WidgetManagerSupport.currentRow, $modal);
                }
                else {                    
                    WidgetManagerSupport.Widget_insert(WidgetManagerSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.WidgetShowModal = function (md, title, row) {
        var formInstance = $("#WidgetEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { Id: 0, Title: '', Description: '', Url: '', Icon: '', DefaultState: '', LanguageId: 0 };

        md.data('id', row.Id);
        md.find('.modal-title').text(title);

        WidgetManagerSupport.WidgetRowToInput(row);
        $('#Id').prop('disabled', true);
        $('#LanguageId').prop('disabled', true);
        WidgetManagerSupport.Widget_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.WidgetRowToInput = function (row) {
        WidgetManagerSupport.currentRow = row;
        AutoNumeric.set('#Id', row.Id);
        $('#Title').val(row.Title);
        $('#Description').val(row.Description);
        $('#Url').val(row.Url);
        WidgetManagerSupport.LookUpForIcon(row.Icon, '');
        $('#Icon').trigger('change');
        $('#DefaultState').val(row.DefaultState);
        AutoNumeric.set('#LanguageId', row.LanguageId);

    };





    this.Id_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.LanguageId_FormatterMaskData = function (value, row, index) {          
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
                  disabled: $('#WidgetTbl *').prop('disabled'),
                  checked: value
                 }
      };



  this.Init = function(){
    
    moment.locale(app.user.languageName);
    
   generalSupport.TranslateInit('WidgetManager', function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        WidgetManagerSupport.ValidateSetup();
        
        

    WidgetManagerSupport.ControlBehaviour();
    WidgetManagerSupport.ControlActions();
    

    app.core.AsyncWebMethod("/fasi/dli/forms/WidgetManagerActions.aspx/Initialization", false,
            JSON.stringify({
                id: $('#WidgetManagerFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
               if (data.d.Success === true) {
                  WidgetManagerSupport.CallRenderLookUps(data);
                
            
                $("#WidgetTblPlaceHolder").replaceWith('<table id="WidgetTbl"></table>');
    WidgetManagerSupport.WidgetTblSetup($('#WidgetTbl'));

            
            
            
                var valueLanguageId = constants.defaultLanguageId;
            AutoNumeric.set('#LanguageId', valueLanguageId);

               $.ajax({
                    type: "GET",
                    url: constants.fasiApi.base + 'fasi/v1/Widgets?languageId=' + generalSupport.NumericValue('#LanguageId', -99999, 99999) + '&startIndex=0&' + 'endIndex=0',
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
                         $('#WidgetTbl').bootstrapTable('load', data.Data.Items);


                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
             }
});

   }, 'dli/forms');
  };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: ['Administrador'],
        Element: $("#WidgetManagerMainForm"),
        CallBack: WidgetManagerSupport.Init
    });
});

window.WidgetActionEvents = {
    'click .update': function (e, value, row, index) {
        WidgetManagerSupport.WidgetShowModal($('#WidgetPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
