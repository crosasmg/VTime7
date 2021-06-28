var AccessTrackingSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#AccessTrackingFormId').val(),
            Start: generalSupport.DatePickerValueInputToObject('#StartDP'),
            Finish: generalSupport.DatePickerValueInputToObject('#FinishDP')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#AccessTrackingFormId').val(data.InstanceFormId);
        $('#StartDP').val(generalSupport.ToJavaScriptDateCustom(data.Start, generalSupport.DateFormat()));
        $('#FinishDP').val(generalSupport.ToJavaScriptDateCustom(data.Finish, generalSupport.DateFormat()));


        if (data.Client_Client !== null)
            $('#ClientTbl').bootstrapTable('load', data.Client_Client);

    };

    this.ControlBehaviour = function () {







        $('#StartDP_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#StartDP_group');
        $('#FinishDP_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#FinishDP_group');


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               AccessTrackingSupport.ObjectToInput(data.d.Data.Instance, source);
            
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




    this.ControlActions =   function () {

        $('#OkButton').click(function (event) {
            var formInstance = $("#AccessTrackingMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#OkButton'));
                btnLoading.start();
        data = app.core.SyncWebMethod("/fasi/dli/forms/AccessTrackingActions.aspx/OkButton1SelectCommandActionUSERSSECURITYTRACE", false,
               JSON.stringify({                 USERSSECURITYTRACEEFFECTDATE3: generalSupport.DatePickerValue('#StartDP'),
                USERSSECURITYTRACEEFFECTDATE4: generalSupport.DatePickerValue('#FinishDP') }));
               btnLoading.stop();
                        if (data.d.Count !== 0)
                                   {
                                      $('#ClientTbl').bootstrapTable('load', data.d.Data);
                                   }
                                   else 
                                   {
		                                  $('#ClientTbl').bootstrapTable('load', []); 
                                   }

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();

        $.validator.addMethod("StartDP_Validate1", function (value, element) {
            var result = true;
            if (!this.optional(element)) {
                if (generalSupport.DatePickerValue('#StartDP') > generalSupport.DatePickerValue('#FinishDP')){
                    result = false;
            }

            }
            return result;
        });
        $.validator.addMethod("FinishDP_Validate1", function (value, element) {
            var result = true;
            if (!this.optional(element)) {
                if (generalSupport.DatePickerValue('#FinishDP') < generalSupport.DatePickerValue('#StartDP')){
                    result = false;
            }

            }
            return result;
        });

        $("#AccessTrackingMainForm").validate({
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
                StartDP: {
                    required: true,
                    DatePicker: true,
                    StartDP_Validate1: true
                },
                FinishDP: {
                    required: true,
                    DatePicker: true,
                    FinishDP_Validate1: true
                }
            },
            messages: {
                StartDP: {
                    required: $.i18n.t('app.form.StartDP_RequiredMessage'),
                    DatePicker: $.i18n.t('app.form.StartDP_DatePicker'),
                    StartDP_Validate1: $.i18n.t('app.form.StartDP_Validate1')
                },
                FinishDP: {
                    required: $.i18n.t('app.form.FinishDP_RequiredMessage'),
                    DatePicker: $.i18n.t('app.form.FinishDP_DatePicker'),
                    FinishDP_Validate1: $.i18n.t('app.form.FinishDP_Validate1')
                }
            }
        });

    };

    this.ClientTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            sortable: true,
            search: true,
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
            columns: [{
                field: 'DateOfIngress',
                title: $.i18n.t('app.form.ClientTbl_DateOfIngress_Title'),
                formatter: 'AccessTrackingSupport.ClientDateOfIngress_ColumnFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'CustomString',
                title: $.i18n.t('app.form.ClientTbl_CustomString_Title'),
                sortable: true,
                halign: 'center'
            }, {
                field: 'eMailAddressDefault',
                title: $.i18n.t('app.form.ClientTbl_eMailAddressDefault_Title'),
                sortable: true,
                halign: 'center'
            }, {
                field: 'LanguageDescription',
                title: $.i18n.t('app.form.ClientTbl_LanguageDescription_Title'),
                sortable: true,
                halign: 'center'
            }, {
                field: 'CustomStringEx',
                title: $.i18n.t('app.form.ClientTbl_CustomStringEx_Title'),
                sortable: true,
                halign: 'center'
            }]
        });




    };










    this.ClientDateOfIngress_ColumnFormatter = function (value, row, index, field) {
        return tableHelperSupport.DateFormatter(value, row, index);
    };


  this.Init = function(){
    
    moment.locale(app.user.languageName);
    
   generalSupport.TranslateInit('AccessTracking', function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        AccessTrackingSupport.ValidateSetup();
        
        

    AccessTrackingSupport.ControlBehaviour();
    AccessTrackingSupport.ControlActions();
    

    app.core.AsyncWebMethod("/fasi/dli/forms/AccessTrackingActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), false,
            JSON.stringify({
                id: $('#AccessTrackingFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
               if (data.d.Success === true) {
                  AccessTrackingSupport.CallRenderLookUps(data);
                
                $('#StartDP').val(generalSupport.URLDateValue('Start'));
    $('#FinishDP').val(generalSupport.URLDateValue('Finish'));

                $("#ClientTblPlaceHolder").replaceWith('<table id="ClientTbl"></table>');
    AccessTrackingSupport.ClientTblSetup($('#ClientTbl'));

                if ($('#StartDP').val() === '')
        $('#StartDP').val(moment().format(generalSupport.DateFormat()));
    if ($('#FinishDP').val() === '')
        $('#FinishDP').val(moment().format(generalSupport.DateFormat()));

            
            
            
             }
});

   }, 'dli/forms');
  };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: ['Administrador'],
        Element: $("#AccessTrackingMainForm"),
        CallBack: AccessTrackingSupport.Init
    });
});

