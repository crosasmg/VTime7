var LookupDetailSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#LookupDetailFormId').val(),
            LOOKUP_Grid_LOOKUP_Item: generalSupport.NormalizeProperties($('#LOOKUP_GridTbl').bootstrapTable('getData'), 'LASTUPDATEDON'),
            filter: parseInt(0 + $('#filter').val(), 10),
            filterLng: parseInt(0 + $('#dropdownlist3').val(), 10)
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#LookupDetailFormId').val(data.InstanceFormId);

        LookupDetailSupport.LookUpForRECORDSTATUS(source);
        LookupDetailSupport.LookUpForfilter(data.filter, source);
        LookupDetailSupport.LookUpFordropdownlist3(data.filterLng, source);

        LookupDetailSupport.LOOKUP_GridTblRequest();
        if (data.LOOKUP_Grid_LOOKUP_Item !== null)
            $('#LOOKUP_GridTbl').bootstrapTable('load', data.LOOKUP_Grid_LOOKUP_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#LOOKUPID', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      new AutoNumeric('#LANGUAGEID', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      new AutoNumeric('#CODE', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      new AutoNumeric('#QUERYORDER', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999999,
            decimalPlaces: 0,
            minimumValue: -99999999
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
               LookupDetailSupport.ObjectToInput(data.d.Data.Instance, source);
            
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



    this.LOOKUP_Grid_insert = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#LOOKUP_GridSaveBtn'));          
            var errors;
        data = app.core.SyncWebMethod("/fasi/dli/forms/LookupDetailActions.aspx/LOOKUP_Grid1InsertCommandActionLOOKUP", false,
               JSON.stringify({ LOOKUPID1: parseInt(0 + $('#filter').val(), 10), LANGUAGEID2: parseInt(0 + $('#dropdownlist3').val(), 10), CODE3: row.CODE, DESCRIPTION4: row.DESCRIPTION, QUERYORDER5: row.QUERYORDER, RECORDSTATUS6: row.RECORDSTATUS, LASTUPDATEBY6: app.user.userId }));
               

        if (data.d.Success === true){
            $('#LOOKUP_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message4 = $.i18n.t('app.form.LOOKUP_Grid_Message_Notify_insert4');
            notification.toastr.success($.i18n.t('app.form.LOOKUP_Grid_Title_Notify_insert4'), message4);
            }            
            else {
            var message5 = $.i18n.t('app.form.LOOKUP_Grid_Message_Notify_insert5');
            notification.swal.error($.i18n.t('app.form.LOOKUP_Grid_Title_Notify_insert5'), message5);

                }

    };
    this.LOOKUP_Grid_update = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#LOOKUP_GridSaveBtn'));          
            var errors;
        data = app.core.SyncWebMethod("/fasi/dli/forms/LookupDetailActions.aspx/LOOKUP_Grid1UpdateCommandActionLOOKUP", false,
               JSON.stringify({ DESCRIPTION1: row.DESCRIPTION, QUERYORDER2: row.QUERYORDER, RECORDSTATUS3: row.RECORDSTATUS, LASTUPDATEBY3: app.user.userId, LOOKUPLOOKUPID6: row.LOOKUPID, LOOKUPLANGUAGEID7: parseInt(0 + $('#dropdownlist3').val(), 10), LOOKUPCODE8: row.CODE }));
               

        if (data.d.Success === true){
            $('#LOOKUP_GridTbl').bootstrapTable('updateByUniqueId', { id: row.CODE, row: row });
            $modal.modal('hide');
            var message4 = $.i18n.t('app.form.LOOKUP_Grid_Message_Notify_update4');
            notification.toastr.success($.i18n.t('app.form.LOOKUP_Grid_Title_Notify_update4'), message4);
            }            
            else {
            var message5 = $.i18n.t('app.form.LOOKUP_Grid_Message_Notify_update5');
            notification.swal.error($.i18n.t('app.form.LOOKUP_Grid_Title_Notify_update5'), message5);

                }

    };
    this.LOOKUP_Grid_delete = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#LOOKUP_GridSaveBtn'));          
            var errors;
        data = app.core.SyncWebMethod("/fasi/dli/forms/LookupDetailActions.aspx/LOOKUP_Grid1DeleteCommandActionLOOKUP", false,
               JSON.stringify({ LOOKUPLOOKUPID1: row.LOOKUPID, LOOKUPCODE2: row.CODE }));
               

        if (data.d.Success === true){
            $('#LOOKUP_GridTbl').bootstrapTable('remove', {field: 'CODE', values: [generalSupport.NumericValue('#CODE', -99999, 99999)]});
            var message4 = $.i18n.t('app.form.LOOKUP_Grid_Message_Notify_delete4');
            notification.toastr.success($.i18n.t('app.form.LOOKUP_Grid_Title_Notify_delete4'), message4);
            }            
            else {
            var message5 = $.i18n.t('app.form.LOOKUP_Grid_Message_Notify_delete5');
            notification.toastr.error($.i18n.t('app.form.LOOKUP_Grid_Title_Notify_delete5'), message5);

                }

    };
    this.LOOKUP_Grid_BeforeShowPopup = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#LOOKUP_GridSaveBtn'));          
            var nextId;
        if (row.CODE === 0){
        data = app.core.SyncWebMethod("/fasi/dli/forms/LookupDetailActions.aspx/LOOKUP_Grid2SelectCommandActionLOOKUP", false,
               JSON.stringify({  }));
               
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
            nextId = nextId + 1;
        AutoNumeric.set('#CODE', nextId);

            }

    };

    this.ControlActions =   function () {

        $('#SetFilter').click(function (event) {
            var formInstance = $("#LookupDetailMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#SetFilter'));
                btnLoading.start();
                LookupDetailSupport.LOOKUP_GridTblRequest();
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#LookupDetailMainForm").validate({
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
                filter: {
                },
                dropdownlist3: {
                }
            },
            messages: {
                filter: {
                },
                dropdownlist3: {
                }
            }
        });
        $("#LOOKUP_GridEditForm").validate().destroy();
        $("#LOOKUP_GridEditForm").validate({
            rules: {
                LOOKUPID: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999,
                    required: true
                },
                LANGUAGEID: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999,
                    required: true
                },
                CODE: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999
                },
                DESCRIPTION: {
                    required: true,
                    maxlength: 255
                },
                QUERYORDER: {
                    AutoNumericMinValue: -99999999,
                    AutoNumericMaxValue: 99999999
                },
                RECORDSTATUS: {
                    required: true                },
                LASTUPDATEBY: {
                    maxlength: 255
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
                LANGUAGEID: {
                    AutoNumericMinValue: $.i18n.t('app.validation.LANGUAGEID.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.LANGUAGEID.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.LANGUAGEID.required')
                },
                CODE: {
                    AutoNumericMinValue: $.i18n.t('app.validation.CODE.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.CODE.AutoNumericMaxValue')
                },
                DESCRIPTION: {
                    required: $.i18n.t('app.validation.DESCRIPTION.required'),
                    maxlength: $.i18n.t('app.validation.DESCRIPTION.maxlength')
                },
                QUERYORDER: {
                    AutoNumericMinValue: $.i18n.t('app.validation.QUERYORDER.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.QUERYORDER.AutoNumericMaxValue')
                },
                RECORDSTATUS: {
                    required: $.i18n.t('app.validation.RECORDSTATUS.required')                },
                LASTUPDATEBY: {
                    maxlength: $.i18n.t('app.validation.LASTUPDATEBY.maxlength')
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

            app.core.AsyncWebMethod("/fasi/dli/forms/LookupDetailActions.aspx/LookUpForRECORDSTATUS", false,
                JSON.stringify({ id: $('#LookupDetailFormId').val() }),
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
    this.LookUpForfilter = function (defaultValue, source) {
        var ctrol = $('#filter');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/LookupDetailActions.aspx/LookUpForfilter", false,
                JSON.stringify({ id: $('#LookupDetailFormId').val() }),
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
    this.LookUpFordropdownlist3 = function (defaultValue, source) {
        var ctrol = $('#dropdownlist3');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/LookupDetailActions.aspx/LookUpFordropdownlist3", false,
                JSON.stringify({ id: $('#LookupDetailFormId').val() }),
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

    this.LOOKUP_GridTblSetup = function (table) {
        LookupDetailSupport.LookUpForRECORDSTATUS('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'CODE',
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
            toolbar: '#LOOKUP_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true,
                formatter: 'LookupDetailSupport.selected_Formatter'
            }, {
                field: 'LOOKUPID',
                title: $.i18n.t('app.form.LOOKUP_GridTbl_LOOKUPID_Title'),
                formatter: 'LookupDetailSupport.LOOKUPID_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'LANGUAGEID',
                title: $.i18n.t('app.form.LOOKUP_GridTbl_LANGUAGEID_Title'),
                formatter: 'LookupDetailSupport.LANGUAGEID_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'CODE',
                title: $.i18n.t('app.form.LOOKUP_GridTbl_CODE_Title'),
                formatter: 'LookupDetailSupport.CODE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'DESCRIPTION',
                title: $.i18n.t('app.form.LOOKUP_GridTbl_DESCRIPTION_Title'),
                events: 'LOOKUP_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'QUERYORDER',
                title: $.i18n.t('app.form.LOOKUP_GridTbl_QUERYORDER_Title'),
                formatter: 'LookupDetailSupport.QUERYORDER_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'RECORDSTATUS',
                title: $.i18n.t('app.form.LOOKUP_GridTbl_RECORDSTATUS_Title'),
                formatter: 'LookupDetailSupport.LookUpForRECORDSTATUSFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'LASTUPDATEBY',
                title: $.i18n.t('app.form.LOOKUP_GridTbl_LASTUPDATEBY_Title'),
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'LASTUPDATEDON',
                title: $.i18n.t('app.form.LOOKUP_GridTbl_LASTUPDATEDON_Title'),
                formatter: 'tableHelperSupport.OnlyDateFormatterWithHoursMinutesSeconds',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }]
        });


        $('#LOOKUP_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#LOOKUP_GridTbl');
            $('#LOOKUP_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#LOOKUP_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#LOOKUP_GridTbl').bootstrapTable('getSelections'), function (row) {		
                LookupDetailSupport.LOOKUP_GridRowToInput(row);
                LookupDetailSupport.LOOKUP_Grid_delete(row, null);
                
                return row.CODE;
            });

            $('#LOOKUP_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#LOOKUP_GridCreateBtn').click(function () {
            var formInstance = $("#LOOKUP_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            LookupDetailSupport.LOOKUP_GridShowModal($('#LOOKUP_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#LOOKUP_GridPopup').find('#LOOKUP_GridSaveBtn').click(function () {
            var formInstance = $("#LOOKUP_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#LOOKUP_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#LOOKUP_GridSaveBtn').html();
                $('#LOOKUP_GridSaveBtn').html('Procesando...');
                $('#LOOKUP_GridSaveBtn').prop('disabled', true);

                LookupDetailSupport.currentRow.LOOKUPID = generalSupport.NumericValue('#LOOKUPID', -99999, 99999);
                LookupDetailSupport.currentRow.LANGUAGEID = generalSupport.NumericValue('#LANGUAGEID', -99999, 99999);
                LookupDetailSupport.currentRow.CODE = generalSupport.NumericValue('#CODE', -99999, 99999);
                LookupDetailSupport.currentRow.DESCRIPTION = $('#DESCRIPTION').val();
                LookupDetailSupport.currentRow.QUERYORDER = generalSupport.NumericValue('#QUERYORDER', -99999999, 99999999);
                LookupDetailSupport.currentRow.RECORDSTATUS = parseInt(0 + $('#RECORDSTATUS').val(), 10);
                LookupDetailSupport.currentRow.LASTUPDATEBY = $('#LASTUPDATEBY').val();
                LookupDetailSupport.currentRow.LASTUPDATEDON = generalSupport.DatePickerValue('#LASTUPDATEDON') + ' HH:mm:ss';

                $('#LOOKUP_GridSaveBtn').prop('disabled', false);
                $('#LOOKUP_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    LookupDetailSupport.LOOKUP_Grid_update(LookupDetailSupport.currentRow, $modal);
                }
                else {                    
                    LookupDetailSupport.LOOKUP_Grid_insert(LookupDetailSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.LOOKUP_GridShowModal = function (md, title, row) {
        var formInstance = $("#LOOKUP_GridEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { LOOKUPID: 0, LANGUAGEID: 0, CODE: 0, DESCRIPTION: '', QUERYORDER: 0, RECORDSTATUS: 0, LASTUPDATEBY: '', LASTUPDATEDON: null };

        md.data('id', row.CODE);
        md.find('.modal-title').text(title);

        LookupDetailSupport.LOOKUP_GridRowToInput(row);
        $('#LOOKUPID').prop('disabled', (row.CODE !== 0));
        $('#LANGUAGEID').prop('disabled', (row.CODE !== 0));
        $('#CODE').prop('disabled', (row.CODE !== 0));
        $('#LASTUPDATEBY').prop('disabled', true);
        $('#LASTUPDATEDON').prop('disabled', true);
        LookupDetailSupport.LOOKUP_Grid_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.LOOKUP_GridRowToInput = function (row) {
        LookupDetailSupport.currentRow = row;
        AutoNumeric.set('#LOOKUPID', row.LOOKUPID);
        AutoNumeric.set('#LANGUAGEID', row.LANGUAGEID);
        AutoNumeric.set('#CODE', row.CODE);
        $('#DESCRIPTION').val(row.DESCRIPTION);
        AutoNumeric.set('#QUERYORDER', row.QUERYORDER);
        LookupDetailSupport.LookUpForRECORDSTATUS(row.RECORDSTATUS, '');
        $('#RECORDSTATUS').trigger('change');
        $('#LASTUPDATEBY').val(row.LASTUPDATEBY);
        $('#LASTUPDATEDON').val(generalSupport.ToJavaScriptDateCustom(row.LASTUPDATEDON, generalSupport.DateFormat() + ' HH:mm:ss'));

    };
    this.LOOKUP_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/LookupDetailActions.aspx/LOOKUP_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                LOOKUPLOOKUPID1: parseInt(0 + $('#filter').val(), 10),
                LOOKUPLANGUAGEID2: parseInt(0 + $('#dropdownlist3').val(), 10)
              }),
              function (data) {
                  $('#LOOKUP_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

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
    this.LANGUAGEID_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CODE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.QUERYORDER_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999999,
            decimalPlaces: 0,
            minimumValue: -99999999
        });
      };


	    this.selected_Formatter = function (value, row, index) {          
          return {
                  disabled: $('#LOOKUP_GridTbl *').prop('disabled'),
                  checked: value
                 }
      };



  this.Init = function(){
    
    moment.locale(app.user.languageName);
    
   generalSupport.TranslateInit('LookupDetail', function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        LookupDetailSupport.ValidateSetup();
        
        

    LookupDetailSupport.ControlBehaviour();
    LookupDetailSupport.ControlActions();
    

    app.core.AsyncWebMethod("/fasi/dli/forms/LookupDetailActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), false,
            JSON.stringify({
                id: $('#LookupDetailFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
               if (data.d.Success === true) {
                  LookupDetailSupport.CallRenderLookUps(data);
                
                    LookupDetailSupport.LookUpForfilter(generalSupport.URLNumericValue('filter'));
        LookupDetailSupport.LookUpFordropdownlist3(generalSupport.URLNumericValue('filterLng'));

                $("#LOOKUP_GridTblPlaceHolder").replaceWith('<table id="LOOKUP_GridTbl"></table>');
    LookupDetailSupport.LOOKUP_GridTblSetup($('#LOOKUP_GridTbl'));

                    LookupDetailSupport.LOOKUP_GridTblRequest();

            
            
            
             }
});

   }, 'dli/forms');
  };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: ['Administrador'],
        Element: $("#LookupDetailMainForm"),
        CallBack: LookupDetailSupport.Init
    });
});

window.LOOKUP_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        LookupDetailSupport.LOOKUP_GridShowModal($('#LOOKUP_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
