var MailTrackingSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#MailTrackingFormId').val(),
            Start: generalSupport.DatePickerValueInputToObject('#StartDP', true),
            Finish: generalSupport.DatePickerValueInputToObject('#FinishDP', true)
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#MailTrackingFormId').val(data.InstanceFormId);
        $('#StartDP').val(generalSupport.ToJavaScriptDateCustom(data.Start, generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#FinishDP').val(generalSupport.ToJavaScriptDateCustom(data.Finish, generalSupport.DateFormat() + ' HH:mm:ss'));


        $('#ClientTbl').bootstrapTable('refreshOptions', { ajax: MailTrackingSupport.ClientTblRequest });
        if (data.Client_Client !== null)
            $('#ClientTbl').bootstrapTable('load', data.Client_Client);

    };

    this.ControlBehaviour = function () {







        $('#StartDP_group').datetimepicker({
            format: generalSupport.DateFormat() + ' HH:mm:ss',
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#StartDP_group');
        $('#FinishDP_group').datetimepicker({
            format: generalSupport.DateFormat() + ' HH:mm:ss',
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#FinishDP_group');


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               MailTrackingSupport.ObjectToInput(data.d.Data.Instance, source);
            
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
            var formInstance = $("#MailTrackingMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#OkButton'));
                btnLoading.start();
                $('#ClientTbl').bootstrapTable('refresh');
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();

        $.validator.addMethod("FinishDP_Validate1", function (value, element) {
            var result = true;
            if (!this.optional(element)) {
                if (generalSupport.DatePickerValue('#FinishDP', true) < generalSupport.DatePickerValue('#StartDP', true)){
                    result = false;
            }

            }
            return result;
        });

        $("#MailTrackingMainForm").validate({
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
                    DatePicker: true
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
                    DatePicker: $.i18n.t('app.form.StartDP_DatePicker')
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
            sidePagination: 'server',
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
            columns: [{
                field: 'ClientID',
                title: $.i18n.t('app.form.ClientTbl_ClientID_Title'),
                sortable: false,
                halign: 'center',
                visible: false
            }, {
                field: 'DateOfIngress',
                title: $.i18n.t('app.form.ClientTbl_DateOfIngress_Title'),
                formatter: 'tableHelperSupport.OnlyDateFormatterWithHoursMinutesSeconds',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'CustomDate',
                title: $.i18n.t('app.form.ClientTbl_CustomDate_Title'),
                formatter: 'tableHelperSupport.OnlyDateFormatterWithHoursMinutesSeconds',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'eMailAddressDefault',
                title: $.i18n.t('app.form.ClientTbl_eMailAddressDefault_Title'),
                sortable: false,
                halign: 'center'
            }, {
                field: 'CustomString',
                title: $.i18n.t('app.form.ClientTbl_CustomString_Title'),
                sortable: false,
                halign: 'center'
            }, {
                field: 'LanguageDescription',
                title: $.i18n.t('app.form.ClientTbl_LanguageDescription_Title'),
                sortable: false,
                halign: 'center'
            }, {
                field: 'CustomStringEx',
                title: $.i18n.t('app.form.ClientTbl_CustomStringEx_Title'),
                sortable: false,
                halign: 'center'
            }]
        });




    };


    this.ClientTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/MailTrackingActions.aspx/ClientTblDataLoad", false,
              JSON.stringify({
                                               beginIndex: ((params.data.offset !== undefined) ? params.data.offset : 0)+1,
                endIndex: ((params.data.offset !== undefined) ? params.data.offset : 0)+((params.data.limit !== undefined) ? params.data.limit : 0),
                filter: (params.data.search !== undefined) ? params.data.search : '',
                JOBSLASTUPDATEDON3: generalSupport.DatePickerValueInputToObject('#StartDP', true),
                JOBSLASTUPDATEDON4: generalSupport.DatePickerValueInputToObject('#FinishDP', true)
              }),
              function (data) {
                    params.success({
                        total: data.d.Count,
                        rows: data.d.Data
                    });

              });
        
    };










  this.Init = function(){
    
    moment.locale(app.user.languageName);
    
   generalSupport.TranslateInit('MailTracking', function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        MailTrackingSupport.ValidateSetup();
        
        

    MailTrackingSupport.ControlBehaviour();
    MailTrackingSupport.ControlActions();
    

    app.core.AsyncWebMethod("/fasi/dli/forms/MailTrackingActions.aspx/Initialization", false,
            JSON.stringify({
                id: $('#MailTrackingFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
               if (data.d.Success === true) {
                  MailTrackingSupport.CallRenderLookUps(data);
                
                $('#StartDP').val(generalSupport.URLDateValue('Start'));
    $('#FinishDP').val(generalSupport.URLDateValue('Finish'));

                $("#ClientTblPlaceHolder").replaceWith('<table id="ClientTbl"></table>');
    MailTrackingSupport.ClientTblSetup($('#ClientTbl'));

                if ($('#StartDP').val() === '')
        $('#StartDP').val(moment().format(generalSupport.DateFormat() + ' HH:mm:ss'));
    if ($('#FinishDP').val() === '')
    $('#FinishDP').val(moment().add(1, 'days').format(generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#ClientTbl').bootstrapTable('refreshOptions', { ajax: MailTrackingSupport.ClientTblRequest });

            
            
            
             }
});

   }, 'dli/forms');
  };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: [],
        Element: $("#MailTrackingMainForm"),
        CallBack: MailTrackingSupport.Init
    });
});

