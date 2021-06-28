var WorkflowMonitorSupport = new function () {

    this.currentRow = {};
    this.newIndex = -1;
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#WorkflowMonitorFormId').val(),
            Start: generalSupport.DatePickerValueInputToObject('#StartDP', true),
            Finish: generalSupport.DatePickerValueInputToObject('#FinishDP', true)
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#WorkflowMonitorFormId').val(data.InstanceFormId);
        $('#StartDP').val(generalSupport.ToJavaScriptDateCustom(data.Start, generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#FinishDP').val(generalSupport.ToJavaScriptDateCustom(data.Finish, generalSupport.DateFormat() + ' HH:mm:ss'));


        if (data.Detail_ClientFinantialInformation !== null)
            $('#DetailTbl').bootstrapTable('load', data.Detail_ClientFinantialInformation);
        WorkflowMonitorSupport.ClientTblRequest();
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
               WorkflowMonitorSupport.ObjectToInput(data.d.Data.Instance, source);
            
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



    this.Detail_ShowValidation = function (row) {
            var returnData;
            var countData;
        data = app.core.SyncWebMethod("/fasi/dli/forms/WorkflowMonitorActions.aspx/Detail1SelectCommandActionWORKFLOWTRACKING", false,
               JSON.stringify({                 WORKFLOWTRACKINGWORKFLOWINSTANCEID1: row.FirstName }));
               
                 if (data.d.Count !== 0)
                                countData = data.d.Data.Result; 
        if (countData >= 1){
            returnData = true;
            }            
            else {
            returnData = false;

                }

        return returnData;
    };

    this.ControlActions =   function () {

        $('#OkButton').click(function (event) {
            var formInstance = $("#WorkflowMonitorMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#OkButton'));
                btnLoading.start();
                WorkflowMonitorSupport.ClientTblRequest();
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

        $("#WorkflowMonitorMainForm").validate({
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
            formatRecordsPerPage: function () { return '' },
            formatShowingRows: function () { return '' },
            detailView: true,
            onExpandRow: WorkflowMonitorSupport.ClientTblExpandRow,
            columns: [{
                field: 'FirstName',
                title: $.i18n.t('app.form.ClientTbl_FirstName_Title'),
                sortable: false,
                halign: 'center',
                visible: false
            }, {
                field: 'LastName',
                title: $.i18n.t('app.form.ClientTbl_LastName_Title'),
                sortable: false,
                halign: 'center'
            }, {
                field: 'LastName2',
                title: $.i18n.t('app.form.ClientTbl_LastName2_Title'),
                formatter: 'WorkflowMonitorSupport.State_FormatterImagePathMask',
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
                field: 'CustomDateEx',
                title: $.i18n.t('app.form.ClientTbl_CustomDateEx_Title'),
                formatter: 'tableHelperSupport.OnlyDateFormatterWithHoursMinutesSeconds',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'CustomNumeric',
                title: $.i18n.t('app.form.ClientTbl_CustomNumeric_Title'),
                formatter: 'WorkflowMonitorSupport.Durate_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'CustomString',
                title: $.i18n.t('app.form.ClientTbl_CustomString_Title'),
                sortable: false,
                halign: 'center',
                visible: false
            }]
        });




    };


    this.ClientTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/WorkflowMonitorActions.aspx/ClientTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                WorkflowInstanceTIMECREATED1: generalSupport.DatePickerValueInputToObject('#StartDP', true),
                WorkflowInstanceTIMECREATED2: generalSupport.DatePickerValueInputToObject('#FinishDP', true)
              }),
              function (data) {
                  $('#ClientTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.DetailTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            sidePagination: 'client',

            columns: [{
                field: 'UserCode',
                title: $.i18n.t('app.form.DetailTbl_UserCode_Title'),
                formatter: 'WorkflowMonitorSupport.UserCode_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'ClientID',
                title: $.i18n.t('app.form.DetailTbl_ClientID_Title'),
                sortable: false,
                halign: 'center'
            }, {
                field: 'ConceptDescription',
                title: $.i18n.t('app.form.DetailTbl_ConceptDescription_Title'),
                sortable: false,
                halign: 'center',
                visible: false
            }, {
                field: 'ConceptStatusDescription',
                title: $.i18n.t('app.form.DetailTbl_ConceptStatusDescription_Title'),
                formatter: 'WorkflowMonitorSupport.DetailState_FormatterImagePathMask',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DateOfTheInformation',
                title: $.i18n.t('app.form.DetailTbl_DateOfTheInformation_Title'),
                formatter: 'tableHelperSupport.OnlyDateFormatterWithHoursMinutesSeconds',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'UpdateTimeStamp',
                title: $.i18n.t('app.form.DetailTbl_UpdateTimeStamp_Title'),
                formatter: 'tableHelperSupport.OnlyDateFormatterWithHoursMinutesSeconds',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'Units',
                title: $.i18n.t('app.form.DetailTbl_Units_Title'),
                formatter: 'WorkflowMonitorSupport.DetailDuration_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });
  

        WorkflowMonitorSupport.$el = table;
        WorkflowMonitorSupport.DetailTblRequest();

      };

    this.DetailTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];
        
        app.core.AsyncWebMethod('/fasi/dli/forms/WorkflowMonitorActions.aspx/DetailTblDataLoad', false,
             JSON.stringify({
					                  WORKFLOWTRACKINGWORKFLOWINSTANCEID1: row.FirstName
				       }),
				       function (data) { 
                  table.bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

               });
    };
    this.ClientTblExpandRow = function (index, row, $detail) {
       var tblparentid = $detail.parent().parent().parent()[0].id;
       var html = [];
        $('#ClientTbl').find('.detail-view').each(function () {
      	  if (!$(this).is($detail.parent())) {
        	$(this).prev().find('.detail-icon').click();
          }
	});
        var detailShowDetail = WorkflowMonitorSupport.Detail_ShowValidation(row);
        if (detailShowDetail)
        html.push('<table id="DetailTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"></table>');
        

        $detail.html(html.join(""));

        if (detailShowDetail)
        WorkflowMonitorSupport.DetailTblSetup($detail.find('#DetailTbl-' + index));


    };





    this.Durate_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 2,
            minimumValue: -99999
        });
      };
    this.UserCode_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.DetailDuration_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 2,
            minimumValue: -999999999999999999
        });
      };



    this.State_FormatterImagePathMask = function (value, row, index, field) {
        return "<img src='../../app/img/WorkflowState/" + value + ".png' class='img - responsive'>";
    };
    this.DetailState_FormatterImagePathMask = function (value, row, index, field) {
        return "<img src='../../app/img/WorkflowState/" + value + ".png' class='img - responsive'>";
    };


  this.Init = function(){
    
    moment.locale(app.user.languageName);
    
   generalSupport.TranslateInit('WorkflowMonitor', function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        WorkflowMonitorSupport.ValidateSetup();
        
        

    WorkflowMonitorSupport.ControlBehaviour();
    WorkflowMonitorSupport.ControlActions();
    

    app.core.AsyncWebMethod("/fasi/dli/forms/WorkflowMonitorActions.aspx/Initialization", false,
            JSON.stringify({
                id: $('#WorkflowMonitorFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
               if (data.d.Success === true) {
                  WorkflowMonitorSupport.CallRenderLookUps(data);
                
                $('#StartDP').val(generalSupport.URLDateValue('Start'));
    $('#FinishDP').val(generalSupport.URLDateValue('Finish'));

                $("#ClientTblPlaceHolder").replaceWith('<table id="ClientTbl"></table>');
    WorkflowMonitorSupport.ClientTblSetup($('#ClientTbl'));

                if ($('#StartDP').val() === '')
        $('#StartDP').val(moment().format(generalSupport.DateFormat() + ' HH:mm:ss'));
    if ($('#FinishDP').val() === '')
    $('#FinishDP').val(moment().add(1, 'days').format(generalSupport.DateFormat() + ' HH:mm:ss'));
        WorkflowMonitorSupport.ClientTblRequest();

            
            
            
             }
});

   }, 'dli/forms');
  };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: [],
        Element: $("#WorkflowMonitorMainForm"),
        CallBack: WorkflowMonitorSupport.Init
    });
});

