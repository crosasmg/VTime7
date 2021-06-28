var AccesosSeguridadSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#AccesosSeguridadFormId').val(),
            Acceso_Grid_Acceso_Item: generalSupport.NormalizeProperties($('#Acceso_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#AccesosSeguridadFormId').val(data.InstanceFormId);

        AccesosSeguridadSupport.LookUpForEstado_Registro(source);

        AccesosSeguridadSupport.Acceso_GridTblRequest();
        if (data.Acceso_Grid_Acceso_Item !== null)
            $('#Acceso_GridTbl').bootstrapTable('load', data.Acceso_Grid_Acceso_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#Id_Acceso', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      new AutoNumeric('#Id_Tipo_Acceso', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
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
               AccesosSeguridadSupport.ObjectToInput(data.d.Data.Instance, source);
            
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



    this.Acceso_Grid_insert = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#Acceso_GridSaveBtn'));          
        data = app.core.SyncWebMethod("/fasi/dli/forms/AccesosSeguridadActions.aspx/Acceso_Grid1InsertCommandActionAcceso", false,
               JSON.stringify({ ID_ACCESO1: row.Id_Acceso, ID_TIPO_ACCESO2: row.Id_Tipo_Acceso, DESCRIPCION3: row.Descripcion, DESCRIPCION_CORTA4: row.Descripcion_Corta, ESTADO_REGISTRO5: row.Estado_Registro, CREATORUSERCODE6: app.user.userId, UPDATEUSERCODE8: app.user.userId }));
               

        if (data.d.Success === true){
            $('#Acceso_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            this.Acceso_GridTblRequest();
            var message5 = $.i18n.t('app.form.Acceso_Grid_Message_Notify_insert5');
            notification.toastr.success($.i18n.t('app.form.Acceso_Grid_Title_Notify_insert5'), message5);
        data = app.core.SyncWebMethod("/fasi/dli/forms/AccesosSeguridadActions.aspx/Clean7e9be84a64ab4caab20a80d4f374bd42", false,
               JSON.stringify({  }));
               

            }            
            else {
            var message7 = $.i18n.t('app.form.Acceso_Grid_Message_Notify_insert7');
            notification.swal.error($.i18n.t('app.form.Acceso_Grid_Title_Notify_insert7'), message7);

                }

    };
    this.Acceso_Grid_update = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#Acceso_GridSaveBtn'));          
        data = app.core.SyncWebMethod("/fasi/dli/forms/AccesosSeguridadActions.aspx/Acceso_Grid1UpdateCommandActionAcceso", false,
               JSON.stringify({ ID_TIPO_ACCESO1: row.Id_Tipo_Acceso, DESCRIPCION2: row.Descripcion, DESCRIPCION_CORTA3: row.Descripcion_Corta, ESTADO_REGISTRO4: row.Estado_Registro, UPDATEUSERCODE5: app.user.userId, AccesoIdAcceso7: row.Id_Acceso }));
               

        if (data.d.Success === true){
            $('#Acceso_GridTbl').bootstrapTable('updateByUniqueId', { id: row.Id_Acceso, row: row });
            $modal.modal('hide');
            var message4 = $.i18n.t('app.form.Acceso_Grid_Message_Notify_update4');
            notification.toastr.success($.i18n.t('app.form.Acceso_Grid_Title_Notify_update4'), message4);
        data = app.core.SyncWebMethod("/fasi/dli/forms/AccesosSeguridadActions.aspx/Cleanbefc1756dffc424aafff5f34f49f1aba", false,
               JSON.stringify({  }));
               

            }            
            else {
            var message6 = $.i18n.t('app.form.Acceso_Grid_Message_Notify_update6');
            notification.swal.error($.i18n.t('app.form.Acceso_Grid_Title_Notify_update6'), message6);

                }

    };
    this.Acceso_Grid_delete = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#Acceso_GridSaveBtn'));          
        data = app.core.SyncWebMethod("/fasi/dli/forms/AccesosSeguridadActions.aspx/Acceso_Grid1DeleteCommandActionAcceso", false,
               JSON.stringify({ AccesoIdAcceso1: row.Id_Acceso }));
               

        if (data.d.Success === true){
            $('#Acceso_GridTbl').bootstrapTable('remove', {field: 'Id_Acceso', values: [generalSupport.NumericValue('#Id_Acceso', -9999999999, 9999999999)]});
            var message4 = $.i18n.t('app.form.Acceso_Grid_Message_Notify_delete4');
            notification.toastr.success($.i18n.t('app.form.Acceso_Grid_Title_Notify_delete4'), message4);
        data = app.core.SyncWebMethod("/fasi/dli/forms/AccesosSeguridadActions.aspx/Cleanf82cb9a2735f43fa8b1c2e312ba67443", false,
               JSON.stringify({  }));
               

            }            
            else {
            var message6 = $.i18n.t('app.form.Acceso_Grid_Message_Notify_delete6');
            notification.toastr.error($.i18n.t('app.form.Acceso_Grid_Title_Notify_delete6'), message6);

                }

    };

    this.ControlActions =   function () {


    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#AccesosSeguridadMainForm").validate({
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
        $("#Acceso_GridEditForm").validate().destroy();
        $("#Acceso_GridEditForm").validate({
            rules: {
                Id_Acceso: {
                    AutoNumericMinValue: -9999999999,
                    AutoNumericMaxValue: 9999999999,
                    required: true
                },
                Id_Tipo_Acceso: {
                    AutoNumericMinValue: -9999999999,
                    AutoNumericMaxValue: 9999999999,
                    required: true
                },
                Descripcion: {
                    required: true,
                    maxlength: 200
                },
                Descripcion_Corta: {
                    required: true,
                    maxlength: 200
                },
                Estado_Registro: {
                    required: true                },
                CreationDate: {
                    required: true,
                    DatePicker: true
                },
                CreatorUserCode: {
                    AutoNumericMinValue: -999999999,
                    AutoNumericMaxValue: 999999999,
                    required: true
                },
                UpdateDate: {
                    required: true,
                    DatePicker: true
                },
                UpdateUserCode: {
                    AutoNumericMinValue: -999999999,
                    AutoNumericMaxValue: 999999999,
                    required: true
                }

            },
            messages: {
                Id_Acceso: {
                    AutoNumericMinValue: $.i18n.t('app.validation.Id_Acceso.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.Id_Acceso.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.Id_Acceso.required')
                },
                Id_Tipo_Acceso: {
                    AutoNumericMinValue: $.i18n.t('app.validation.Id_Tipo_Acceso.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.Id_Tipo_Acceso.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.Id_Tipo_Acceso.required')
                },
                Descripcion: {
                    required: $.i18n.t('app.validation.Descripcion.required'),
                    maxlength: $.i18n.t('app.validation.Descripcion.maxlength')
                },
                Descripcion_Corta: {
                    required: $.i18n.t('app.validation.Descripcion_Corta.required'),
                    maxlength: $.i18n.t('app.validation.Descripcion_Corta.maxlength')
                },
                Estado_Registro: {
                    required: $.i18n.t('app.validation.Estado_Registro.required')                },
                CreationDate: {
                    required: $.i18n.t('app.validation.CreationDate.required'),
                    DatePicker: $.i18n.t('app.validation.CreationDate.DatePicker')
                },
                CreatorUserCode: {
                    AutoNumericMinValue: $.i18n.t('app.validation.CreatorUserCode.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.CreatorUserCode.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.CreatorUserCode.required')
                },
                UpdateDate: {
                    required: $.i18n.t('app.validation.UpdateDate.required'),
                    DatePicker: $.i18n.t('app.validation.UpdateDate.DatePicker')
                },
                UpdateUserCode: {
                    AutoNumericMinValue: $.i18n.t('app.validation.UpdateUserCode.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.UpdateUserCode.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.UpdateUserCode.required')
                }

            }
        });

    };
    this.LookUpForEstado_RegistroFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#Estado_Registro>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForEstado_Registro = function (defaultValue, source) {
        var ctrol = $('#Estado_Registro');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/AccesosSeguridadActions.aspx/LookUpForEstado_Registro", false,
                JSON.stringify({ id: $('#AccesosSeguridadFormId').val() }),
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

    this.Acceso_GridTblSetup = function (table) {
        AccesosSeguridadSupport.LookUpForEstado_Registro('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 20,
            uniqueId: 'Id_Acceso',
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
            toolbar: '#Acceso_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true,
                formatter: 'AccesosSeguridadSupport.selected_Formatter'
            }, {
                field: 'Id_Acceso',
                title: $.i18n.t('app.form.Acceso_GridTbl_Id_Acceso_Title'),
                formatter: 'AccesosSeguridadSupport.Id_Acceso_FormatterMaskData',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'Id_Tipo_Acceso',
                title: $.i18n.t('app.form.Acceso_GridTbl_Id_Tipo_Acceso_Title'),
                formatter: 'AccesosSeguridadSupport.Id_Tipo_Acceso_FormatterMaskData',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'Descripcion',
                title: $.i18n.t('app.form.Acceso_GridTbl_Descripcion_Title'),
                events: 'Acceso_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Descripcion_Corta',
                title: $.i18n.t('app.form.Acceso_GridTbl_Descripcion_Corta_Title'),
                sortable: true,
                halign: 'center'
            }, {
                field: 'Estado_Registro',
                title: $.i18n.t('app.form.Acceso_GridTbl_Estado_Registro_Title'),
                formatter: 'AccesosSeguridadSupport.LookUpForEstado_RegistroFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreationDate',
                title: $.i18n.t('app.form.Acceso_GridTbl_CreationDate_Title'),
                formatter: 'tableHelperSupport.OnlyDateFormatterWithHoursMinutesSeconds',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'CreatorUserCode',
                title: $.i18n.t('app.form.Acceso_GridTbl_CreatorUserCode_Title'),
                formatter: 'AccesosSeguridadSupport.CreatorUserCode_FormatterMaskData',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'UpdateDate',
                title: $.i18n.t('app.form.Acceso_GridTbl_UpdateDate_Title'),
                formatter: 'tableHelperSupport.OnlyDateFormatterWithHoursMinutesSeconds',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'UpdateUserCode',
                title: $.i18n.t('app.form.Acceso_GridTbl_UpdateUserCode_Title'),
                formatter: 'AccesosSeguridadSupport.UpdateUserCode_FormatterMaskData',
                sortable: true,
                halign: 'center',
                visible: false
            }]
        });


        $('#Acceso_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#Acceso_GridTbl');
            $('#Acceso_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#Acceso_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#Acceso_GridTbl').bootstrapTable('getSelections'), function (row) {		
                AccesosSeguridadSupport.Acceso_GridRowToInput(row);
                AccesosSeguridadSupport.Acceso_Grid_delete(row, null);
                
                return row.Id_Acceso;
            });

            $('#Acceso_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#Acceso_GridCreateBtn').click(function () {
            var formInstance = $("#Acceso_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            AccesosSeguridadSupport.Acceso_GridShowModal($('#Acceso_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#Acceso_GridPopup').find('#Acceso_GridSaveBtn').click(function () {
            var formInstance = $("#Acceso_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#Acceso_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#Acceso_GridSaveBtn').html();
                $('#Acceso_GridSaveBtn').html('Procesando...');
                $('#Acceso_GridSaveBtn').prop('disabled', true);

                AccesosSeguridadSupport.currentRow.Id_Acceso = generalSupport.NumericValue('#Id_Acceso', -9999999999, 9999999999);
                AccesosSeguridadSupport.currentRow.Id_Tipo_Acceso = generalSupport.NumericValue('#Id_Tipo_Acceso', -9999999999, 9999999999);
                AccesosSeguridadSupport.currentRow.Descripcion = $('#Descripcion').val();
                AccesosSeguridadSupport.currentRow.Descripcion_Corta = $('#Descripcion_Corta').val();
                AccesosSeguridadSupport.currentRow.Estado_Registro = $('#Estado_Registro').val();
                AccesosSeguridadSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                AccesosSeguridadSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                AccesosSeguridadSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';
                AccesosSeguridadSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);

                $('#Acceso_GridSaveBtn').prop('disabled', false);
                $('#Acceso_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    AccesosSeguridadSupport.Acceso_Grid_update(AccesosSeguridadSupport.currentRow, $modal);
                }
                else {                    
                    AccesosSeguridadSupport.Acceso_Grid_insert(AccesosSeguridadSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.Acceso_GridShowModal = function (md, title, row) {
        var formInstance = $("#Acceso_GridEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { Id_Acceso: 0, Id_Tipo_Acceso: 1, Descripcion: '', Descripcion_Corta: '', Estado_Registro: '', CreationDate: null, CreatorUserCode: 0, UpdateDate: null, UpdateUserCode: 0 };

        md.data('id', row.Id_Acceso);
        md.find('.modal-title').text(title);

        AccesosSeguridadSupport.Acceso_GridRowToInput(row);
        $('#Id_Acceso').prop('disabled', true);
        $('#Id_Tipo_Acceso').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#CreatorUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.Acceso_GridRowToInput = function (row) {
        AccesosSeguridadSupport.currentRow = row;
        AutoNumeric.set('#Id_Acceso', row.Id_Acceso);
        AutoNumeric.set('#Id_Tipo_Acceso', row.Id_Tipo_Acceso);
        $('#Descripcion').val(row.Descripcion);
        $('#Descripcion_Corta').val(row.Descripcion_Corta);
        AccesosSeguridadSupport.LookUpForEstado_Registro(row.Estado_Registro, '');
        $('#Estado_Registro').trigger('change');
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);

    };
    this.Acceso_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/AccesosSeguridadActions.aspx/Acceso_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#Acceso_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.Id_Acceso_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.Id_Tipo_Acceso_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
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


	    this.selected_Formatter = function (value, row, index) {          
          return {
                  disabled: $('#Acceso_GridTbl *').prop('disabled'),
                  checked: value
                 }
      };



  this.Init = function(){
    
    moment.locale(app.user.languageName);
    
   generalSupport.TranslateInit('AccesosSeguridad', function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        AccesosSeguridadSupport.ValidateSetup();
        
        

    AccesosSeguridadSupport.ControlBehaviour();
    AccesosSeguridadSupport.ControlActions();
    

    app.core.AsyncWebMethod("/fasi/dli/forms/AccesosSeguridadActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), false,
            JSON.stringify({
                id: $('#AccesosSeguridadFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
               if (data.d.Success === true) {
                  AccesosSeguridadSupport.CallRenderLookUps(data);
                
            
                $("#Acceso_GridTblPlaceHolder").replaceWith('<table id="Acceso_GridTbl"></table>');
    AccesosSeguridadSupport.Acceso_GridTblSetup($('#Acceso_GridTbl'));

                    AccesosSeguridadSupport.Acceso_GridTblRequest();

            
            
            
             }
});

   }, 'dli/forms');
  };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: ['Administrador'],
        Element: $("#AccesosSeguridadMainForm"),
        CallBack: AccesosSeguridadSupport.Init
    });
});

window.Acceso_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        AccesosSeguridadSupport.Acceso_GridShowModal($('#Acceso_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
