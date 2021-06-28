var LookupMasterSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#LookupMasterFormId').val(),
            LOOKUPMASTER_Grid_LOOKUPMASTER_Item: generalSupport.NormalizeProperties($('#LOOKUPMASTER_GridTbl').bootstrapTable('getData'), 'LASTUPDATEDON')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#LookupMasterFormId').val(data.InstanceFormId);


        LookupMasterSupport.LOOKUPMASTER_GridTblRequest();
        if (data.LOOKUPMASTER_Grid_LOOKUPMASTER_Item !== null)
            $('#LOOKUPMASTER_GridTbl').bootstrapTable('load', data.LOOKUPMASTER_Grid_LOOKUPMASTER_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#LOOKUPID', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });




        $('#LASTUPDATEDON_group').datetimepicker({
            format: generalSupport.DateFormat() + ' HH:mm:ss',
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#LASTUPDATEDON_group');


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               LookupMasterSupport.ObjectToInput(data.d.Data.Instance, source);
            
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



    this.LOOKUPMASTER_Grid_insert = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#LOOKUPMASTER_GridSaveBtn'));          
        data = app.core.SyncWebMethod("/fasi/dli/forms/LookupMasterActions.aspx/LOOKUPMASTER_Grid1InsertCommandActionLOOKUPMASTER", false,
               JSON.stringify({ LOOKUPID1: row.LOOKUPID, KEY2: row.KEY, DESCRIPTION3: row.DESCRIPTION, RECORDSTATUS4: row.RECORDSTATUS, LASTUPDATEDBY4: app.user.userId }));
               

        if (data.d.Success === true){
            $('#LOOKUPMASTER_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message4 = $.i18n.t('app.form.LOOKUPMASTER_Grid_Message_Notify_insert4');
            notification.toastr.success($.i18n.t('app.form.LOOKUPMASTER_Grid_Title_Notify_insert4'), message4);
            }            
            else {
            var message5 = $.i18n.t('app.form.LOOKUPMASTER_Grid_Message_Notify_insert5');
            notification.swal.error($.i18n.t('app.form.LOOKUPMASTER_Grid_Title_Notify_insert5'), message5);

                }

    };
    this.LOOKUPMASTER_Grid_update = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#LOOKUPMASTER_GridSaveBtn'));          
        data = app.core.SyncWebMethod("/fasi/dli/forms/LookupMasterActions.aspx/LOOKUPMASTER_Grid1UpdateCommandActionLOOKUPMASTER", false,
               JSON.stringify({ KEY1: row.KEY, DESCRIPTION2: row.DESCRIPTION, RECORDSTATUS3: row.RECORDSTATUS, LASTUPDATEDBY3: app.user.userId, LOOKUPMASTERLOOKUPID6: row.LOOKUPID }));
               

        if (data.d.Success === true){
            $('#LOOKUPMASTER_GridTbl').bootstrapTable('updateByUniqueId', { id: row.LOOKUPID, row: row });
            $modal.modal('hide');
            var message4 = $.i18n.t('app.form.LOOKUPMASTER_Grid_Message_Notify_update4');
            notification.toastr.success($.i18n.t('app.form.LOOKUPMASTER_Grid_Title_Notify_update4'), message4);
            }            
            else {
            var message5 = $.i18n.t('app.form.LOOKUPMASTER_Grid_Message_Notify_update5');
            notification.swal.error($.i18n.t('app.form.LOOKUPMASTER_Grid_Title_Notify_update5'), message5);

                }

    };
    this.LOOKUPMASTER_Grid_delete = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#LOOKUPMASTER_GridSaveBtn'));          
        data = app.core.SyncWebMethod("/fasi/dli/forms/LookupMasterActions.aspx/LOOKUPMASTER_Grid1DeleteCommandActionLOOKUPMASTER", false,
               JSON.stringify({ LOOKUPMASTERLOOKUPID1: row.LOOKUPID }));
               

        if (data.d.Success === true){
            $('#LOOKUPMASTER_GridTbl').bootstrapTable('remove', {field: 'LOOKUPID', values: [generalSupport.NumericValue('#LOOKUPID', -99999, 99999)]});
            var message4 = $.i18n.t('app.form.LOOKUPMASTER_Grid_Message_Notify_delete4');
            notification.toastr.success($.i18n.t('app.form.LOOKUPMASTER_Grid_Title_Notify_delete4'), message4);
            }            
            else {
            var message5 = $.i18n.t('app.form.LOOKUPMASTER_Grid_Message_Notify_delete5');
            notification.toastr.error($.i18n.t('app.form.LOOKUPMASTER_Grid_Title_Notify_delete5'), message5);

                }

    };
    this.LOOKUPMASTER_Grid_BeforeShowPopup = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#LOOKUPMASTER_GridSaveBtn'));          
            var nextId;
        if (row.LOOKUPID === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/LookupMasterActions.aspx/LOOKUPMASTER_Grid2SelectCommandActionLOOKUPMASTER", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#LOOKUPID', nextId);

            }

    };

    this.ControlActions =   function () {


    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#LookupMasterMainForm").validate({
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
        $("#LOOKUPMASTER_GridEditForm").validate().destroy();
        $("#LOOKUPMASTER_GridEditForm").validate({
            rules: {
                LOOKUPID: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999,
                    required: true
                },
                DESCRIPTION: {
                    required: true,
                    maxlength: 255
                },
                KEY: {
                    required: true,
                    maxlength: 50
                },
                RECORDSTATUS: {
                    required: true                },
                LASTUPDATEDBY: {
                    maxlength: 80
                },
                LASTUPDATEDON: {
                    DatePicker: true
                }

            },
            messages: {
                LOOKUPID: {
                    AutoNumericMinValue: $.i18n.t('app.validation.LOOKUPID.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.LOOKUPID.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.LOOKUPID.required')
                },
                DESCRIPTION: {
                    required: $.i18n.t('app.validation.DESCRIPTION.required'),
                    maxlength: $.i18n.t('app.validation.DESCRIPTION.maxlength')
                },
                KEY: {
                    required: $.i18n.t('app.validation.KEY.required'),
                    maxlength: $.i18n.t('app.validation.KEY.maxlength')
                },
                RECORDSTATUS: {
                    required: $.i18n.t('app.validation.RECORDSTATUS.required')                },
                LASTUPDATEDBY: {
                    maxlength: $.i18n.t('app.validation.LASTUPDATEDBY.maxlength')
                },
                LASTUPDATEDON: {
                    DatePicker: $.i18n.t('app.validation.LASTUPDATEDON.DatePicker')
                }

            }
        });

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

            app.core.AsyncWebMethod("/fasi/dli/forms/LookupMasterActions.aspx/LookUpForRECORDSTATUS", false,
                JSON.stringify({ id: $('#LookupMasterFormId').val() }),
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

    this.LOOKUPMASTER_GridTblSetup = function (table) {
        LookupMasterSupport.LookUpForRECORDSTATUS('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'LOOKUPID',
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
            toolbar: '#LOOKUPMASTER_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true,
                formatter: 'LookupMasterSupport.selected_Formatter'
            }, {
                field: 'LOOKUPID',
                title: $.i18n.t('app.form.LOOKUPMASTER_GridTbl_LOOKUPID_Title'),
                formatter: 'LookupMasterSupport.LOOKUPID_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DESCRIPTION',
                title: $.i18n.t('app.form.LOOKUPMASTER_GridTbl_DESCRIPTION_Title'),
                events: 'LOOKUPMASTER_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'KEY',
                title: $.i18n.t('app.form.LOOKUPMASTER_GridTbl_KEY_Title'),
                sortable: true,
                halign: 'center'
            }, {
                field: 'RECORDSTATUS',
                title: $.i18n.t('app.form.LOOKUPMASTER_GridTbl_RECORDSTATUS_Title'),
                formatter: 'LookupMasterSupport.LookUpForRECORDSTATUSFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LASTUPDATEDBY',
                title: $.i18n.t('app.form.LOOKUPMASTER_GridTbl_LASTUPDATEDBY_Title'),
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'LASTUPDATEDON',
                title: $.i18n.t('app.form.LOOKUPMASTER_GridTbl_LASTUPDATEDON_Title'),
                formatter: 'tableHelperSupport.OnlyDateFormatterWithHoursMinutesSeconds',
                sortable: true,
                halign: 'center',
                visible: false
            }]
        });


        $('#LOOKUPMASTER_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#LOOKUPMASTER_GridTbl');
            $('#LOOKUPMASTER_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#LOOKUPMASTER_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#LOOKUPMASTER_GridTbl').bootstrapTable('getSelections'), function (row) {		
                LookupMasterSupport.LOOKUPMASTER_GridRowToInput(row);
                LookupMasterSupport.LOOKUPMASTER_Grid_delete(row, null);
                
                return row.LOOKUPID;
            });

            $('#LOOKUPMASTER_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#LOOKUPMASTER_GridCreateBtn').click(function () {
            var formInstance = $("#LOOKUPMASTER_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            LookupMasterSupport.LOOKUPMASTER_GridShowModal($('#LOOKUPMASTER_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#LOOKUPMASTER_GridPopup').find('#LOOKUPMASTER_GridSaveBtn').click(function () {
            var formInstance = $("#LOOKUPMASTER_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#LOOKUPMASTER_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#LOOKUPMASTER_GridSaveBtn').html();
                $('#LOOKUPMASTER_GridSaveBtn').html('Procesando...');
                $('#LOOKUPMASTER_GridSaveBtn').prop('disabled', true);

                LookupMasterSupport.currentRow.LOOKUPID = generalSupport.NumericValue('#LOOKUPID', -99999, 99999);
                LookupMasterSupport.currentRow.DESCRIPTION = $('#DESCRIPTION').val();
                LookupMasterSupport.currentRow.KEY = $('#KEY').val();
                LookupMasterSupport.currentRow.RECORDSTATUS = parseInt(0 + $('#RECORDSTATUS').val(), 10);
                LookupMasterSupport.currentRow.LASTUPDATEDBY = $('#LASTUPDATEDBY').val();
                LookupMasterSupport.currentRow.LASTUPDATEDON = generalSupport.DatePickerValue('#LASTUPDATEDON') + ' HH:mm:ss';

                $('#LOOKUPMASTER_GridSaveBtn').prop('disabled', false);
                $('#LOOKUPMASTER_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    LookupMasterSupport.LOOKUPMASTER_Grid_update(LookupMasterSupport.currentRow, $modal);
                }
                else {                    
                    LookupMasterSupport.LOOKUPMASTER_Grid_insert(LookupMasterSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.LOOKUPMASTER_GridShowModal = function (md, title, row) {
        var formInstance = $("#LOOKUPMASTER_GridEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { LOOKUPID: 0, DESCRIPTION: null, KEY: '', RECORDSTATUS: 0, LASTUPDATEDBY: '', LASTUPDATEDON: null };

        md.data('id', row.LOOKUPID);
        md.find('.modal-title').text(title);

        LookupMasterSupport.LOOKUPMASTER_GridRowToInput(row);
        $('#LOOKUPID').prop('disabled', (row.LOOKUPID !== 0));
        $('#LASTUPDATEDBY').prop('disabled', true);
        $('#LASTUPDATEDON').prop('disabled', true);
        LookupMasterSupport.LOOKUPMASTER_Grid_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.LOOKUPMASTER_GridRowToInput = function (row) {
        LookupMasterSupport.currentRow = row;
        AutoNumeric.set('#LOOKUPID', row.LOOKUPID);
        $('#DESCRIPTION').val(row.DESCRIPTION);
        $('#KEY').val(row.KEY);
        LookupMasterSupport.LookUpForRECORDSTATUS(row.RECORDSTATUS, '');
        $('#RECORDSTATUS').trigger('change');
        $('#LASTUPDATEDBY').val(row.LASTUPDATEDBY);
        $('#LASTUPDATEDON').val(generalSupport.ToJavaScriptDateCustom(row.LASTUPDATEDON, generalSupport.DateFormat() + ' HH:mm:ss'));

    };
    this.LOOKUPMASTER_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/LookupMasterActions.aspx/LOOKUPMASTER_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#LOOKUPMASTER_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.LOOKUPID_FormatterMaskData = function (value, row, index) {          
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
                  disabled: $('#LOOKUPMASTER_GridTbl *').prop('disabled'),
                  checked: value
                 }
      };



  this.Init = function(){
    
    moment.locale(app.user.languageName);
    
   generalSupport.TranslateInit('LookupMaster', function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        LookupMasterSupport.ValidateSetup();
        
        

    LookupMasterSupport.ControlBehaviour();
    LookupMasterSupport.ControlActions();
    

    app.core.AsyncWebMethod("/fasi/dli/forms/LookupMasterActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), false,
            JSON.stringify({
                id: $('#LookupMasterFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
               if (data.d.Success === true) {
                  LookupMasterSupport.CallRenderLookUps(data);
                
            
                $("#LOOKUPMASTER_GridTblPlaceHolder").replaceWith('<table id="LOOKUPMASTER_GridTbl"></table>');
    LookupMasterSupport.LOOKUPMASTER_GridTblSetup($('#LOOKUPMASTER_GridTbl'));

                    LookupMasterSupport.LOOKUPMASTER_GridTblRequest();

            
            
            
             }
});

   }, 'dli/forms');
  };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: [],
        Element: $("#LookupMasterMainForm"),
        CallBack: LookupMasterSupport.Init
    });
});

window.LOOKUPMASTER_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        LookupMasterSupport.LOOKUPMASTER_GridShowModal($('#LOOKUPMASTER_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
