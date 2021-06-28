var ConsumidorSeguridadSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#ConsumidorSeguridadFormId').val(),
            Consumidor_Grid_Consumidor_Item: generalSupport.NormalizeProperties($('#Consumidor_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#ConsumidorSeguridadFormId').val(data.InstanceFormId);

        ConsumidorSeguridadSupport.LookUpForEstado_Registro(source);

        ConsumidorSeguridadSupport.Consumidor_GridTblRequest();
        if (data.Consumidor_Grid_Consumidor_Item !== null)
            $('#Consumidor_GridTbl').bootstrapTable('load', data.Consumidor_Grid_Consumidor_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#Id_Consumidor', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      new AutoNumeric('#Id_Compania', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      new AutoNumeric('#Vida_Token_Acceso', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999,
            decimalPlaces: 0,
            minimumValue: -999999
        });
      new AutoNumeric('#CreatorUserCode', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      new AutoNumeric('#UpdateUserCode', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
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
               ConsumidorSeguridadSupport.ObjectToInput(data.d.Data.Instance, source);
            
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



    this.Consumidor_Grid_insert = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#Consumidor_GridSaveBtn'));          
        data = app.core.SyncWebMethod("/fasi/dli/forms/ConsumidorSeguridadActions.aspx/Consumidor_Grid1InsertCommandActionConsumidor", false,
               JSON.stringify({ ID_CONSUMIDOR1: row.Id_Consumidor, ID_COMPANIA2: row.Id_Compania, CODIGO_SECRETO3: row.Codigo_Secreto, NOMBRE_CONSUMIDOR4: row.Nombre_Consumidor, IDENTIFICADOR_CONSUMIDOR5: row.Identificador_Consumidor, VIDA_TOKEN_ACCESO6: row.Vida_Token_Acceso, ESTADO_REGISTRO7: row.Estado_Registro, CREATORUSERCODE8: app.user.userId, UPDATEUSERCODE10: app.user.userId }));
               

        if (data.d.Success === true){
            $('#Consumidor_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message4 = $.i18n.t('app.form.Consumidor_Grid_Message_Notify_insert4');
            notification.toastr.success($.i18n.t('app.form.Consumidor_Grid_Title_Notify_insert4'), message4);
            }            
            else {
            var message5 = $.i18n.t('app.form.Consumidor_Grid_Message_Notify_insert5');
            notification.swal.error($.i18n.t('app.form.Consumidor_Grid_Title_Notify_insert5'), message5);

                }

    };
    this.Consumidor_Grid_update = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#Consumidor_GridSaveBtn'));          
        data = app.core.SyncWebMethod("/fasi/dli/forms/ConsumidorSeguridadActions.aspx/Consumidor_Grid1UpdateCommandActionConsumidor", false,
               JSON.stringify({ CODIGO_SECRETO1: row.Codigo_Secreto, NOMBRE_CONSUMIDOR2: row.Nombre_Consumidor, IDENTIFICADOR_CONSUMIDOR3: row.Identificador_Consumidor, VIDA_TOKEN_ACCESO4: row.Vida_Token_Acceso, ESTADO_REGISTRO5: row.Estado_Registro, UPDATEUSERCODE6: app.user.userId, ConsumidorIdConsumidor8: row.Id_Consumidor, ConsumidorIdCompania9: row.Id_Compania }));
               

        if (data.d.Success === true){
            $('#Consumidor_GridTbl').bootstrapTable('updateByUniqueId', { id: row.Id_Consumidor, row: row });
            $modal.modal('hide');
            var message4 = $.i18n.t('app.form.Consumidor_Grid_Message_Notify_update4');
            notification.toastr.success($.i18n.t('app.form.Consumidor_Grid_Title_Notify_update4'), message4);
            }            
            else {
            var message5 = $.i18n.t('app.form.Consumidor_Grid_Message_Notify_update5');
            notification.swal.error($.i18n.t('app.form.Consumidor_Grid_Title_Notify_update5'), message5);

                }

    };
    this.Consumidor_Grid_delete = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#Consumidor_GridSaveBtn'));          
        data = app.core.SyncWebMethod("/fasi/dli/forms/ConsumidorSeguridadActions.aspx/Consumidor_Grid1DeleteCommandActionConsumidor", false,
               JSON.stringify({ ConsumidorIdConsumidor1: row.Id_Consumidor, ConsumidorIdCompania2: row.Id_Compania }));
               

        if (data.d.Success === true){
            $('#Consumidor_GridTbl').bootstrapTable('remove', {field: 'Id_Consumidor', values: [generalSupport.NumericValue('#Id_Consumidor', -9999999999, 9999999999)]});
            var message4 = $.i18n.t('app.form.Consumidor_Grid_Message_Notify_delete4');
            notification.toastr.success($.i18n.t('app.form.Consumidor_Grid_Title_Notify_delete4'), message4);
            }            
            else {
            var message5 = $.i18n.t('app.form.Consumidor_Grid_Message_Notify_delete5');
            notification.toastr.error($.i18n.t('app.form.Consumidor_Grid_Title_Notify_delete5'), message5);

                }

    };

    this.ControlActions =   function () {


    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#ConsumidorSeguridadMainForm").validate({
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
        $("#Consumidor_GridEditForm").validate().destroy();
        $("#Consumidor_GridEditForm").validate({
            rules: {
                Id_Consumidor: {
                    AutoNumericMinValue: -9999999999,
                    AutoNumericMaxValue: 9999999999,
                    required: true
                },
                Id_Compania: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999,
                    required: true
                },
                Codigo_Secreto: {
                    required: true,
                    maxlength: 50
                },
                Nombre_Consumidor: {
                    required: true,
                    maxlength: 50
                },
                Identificador_Consumidor: {
                    required: true,
                    maxlength: 50
                },
                Vida_Token_Acceso: {
                    AutoNumericMinValue: -999999,
                    AutoNumericMaxValue: 999999,
                    required: true
                },
                Estado_Registro: {
                    required: true                },
                CreationDate: {
                    required: true,
                    DatePicker: true
                },
                CreatorUserCode: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999,
                    required: true
                },
                UpdateDate: {
                    required: true,
                    DatePicker: true
                },
                UpdateUserCode: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999,
                    required: true
                }

            },
            messages: {
                Id_Consumidor: {
                    AutoNumericMinValue: $.i18n.t('app.validation.Id_Consumidor.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.Id_Consumidor.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.Id_Consumidor.required')
                },
                Id_Compania: {
                    AutoNumericMinValue: $.i18n.t('app.validation.Id_Compania.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.Id_Compania.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.Id_Compania.required')
                },
                Codigo_Secreto: {
                    required: $.i18n.t('app.validation.Codigo_Secreto.required'),
                    maxlength: $.i18n.t('app.validation.Codigo_Secreto.maxlength')
                },
                Nombre_Consumidor: {
                    required: $.i18n.t('app.validation.Nombre_Consumidor.required'),
                    maxlength: $.i18n.t('app.validation.Nombre_Consumidor.maxlength')
                },
                Identificador_Consumidor: {
                    required: $.i18n.t('app.validation.Identificador_Consumidor.required'),
                    maxlength: $.i18n.t('app.validation.Identificador_Consumidor.maxlength')
                },
                Vida_Token_Acceso: {
                    AutoNumericMinValue: $.i18n.t('app.validation.Vida_Token_Acceso.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.Vida_Token_Acceso.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.Vida_Token_Acceso.required')
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

            app.core.AsyncWebMethod("/fasi/dli/forms/ConsumidorSeguridadActions.aspx/LookUpForEstado_Registro", false,
                JSON.stringify({ id: $('#ConsumidorSeguridadFormId').val() }),
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

    this.Consumidor_GridTblSetup = function (table) {
        ConsumidorSeguridadSupport.LookUpForEstado_Registro('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'Id_Consumidor',
            sortable: true,
            sidePagination: 'client',
            search: true,
            toolbar: '#Consumidor_Gridtoolbar',
            columns: [{
                field: 'Id_Consumidor',
                title: $.i18n.t('app.form.Consumidor_GridTbl_Id_Consumidor_Title'),
                formatter: 'ConsumidorSeguridadSupport.Id_Consumidor_FormatterMaskData',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'Id_Compania',
                title: $.i18n.t('app.form.Consumidor_GridTbl_Id_Compania_Title'),
                formatter: 'ConsumidorSeguridadSupport.Id_Compania_FormatterMaskData',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'Codigo_Secreto',
                title: $.i18n.t('app.form.Consumidor_GridTbl_Codigo_Secreto_Title'),
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'Nombre_Consumidor',
                title: $.i18n.t('app.form.Consumidor_GridTbl_Nombre_Consumidor_Title'),
                events: 'Consumidor_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Identificador_Consumidor',
                title: $.i18n.t('app.form.Consumidor_GridTbl_Identificador_Consumidor_Title'),
                sortable: true,
                halign: 'center'
            }, {
                field: 'Vida_Token_Acceso',
                title: $.i18n.t('app.form.Consumidor_GridTbl_Vida_Token_Acceso_Title'),
                formatter: 'ConsumidorSeguridadSupport.Vida_Token_Acceso_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Estado_Registro',
                title: $.i18n.t('app.form.Consumidor_GridTbl_Estado_Registro_Title'),
                formatter: 'ConsumidorSeguridadSupport.LookUpForEstado_RegistroFormatter',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'CreationDate',
                title: $.i18n.t('app.form.Consumidor_GridTbl_CreationDate_Title'),
                formatter: 'tableHelperSupport.OnlyDateFormatterWithHoursMinutesSeconds',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'CreatorUserCode',
                title: $.i18n.t('app.form.Consumidor_GridTbl_CreatorUserCode_Title'),
                formatter: 'ConsumidorSeguridadSupport.CreatorUserCode_FormatterMaskData',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'UpdateDate',
                title: $.i18n.t('app.form.Consumidor_GridTbl_UpdateDate_Title'),
                formatter: 'tableHelperSupport.OnlyDateFormatterWithHoursMinutesSeconds',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'UpdateUserCode',
                title: $.i18n.t('app.form.Consumidor_GridTbl_UpdateUserCode_Title'),
                formatter: 'ConsumidorSeguridadSupport.UpdateUserCode_FormatterMaskData',
                sortable: true,
                halign: 'center',
                visible: false
            }]
        });


        $('#Consumidor_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#Consumidor_GridTbl');
            $('#Consumidor_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#Consumidor_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#Consumidor_GridTbl').bootstrapTable('getSelections'), function (row) {		
                ConsumidorSeguridadSupport.Consumidor_GridRowToInput(row);
                ConsumidorSeguridadSupport.Consumidor_Grid_delete(row, null);
                
                return row.Id_Consumidor;
            });

            $('#Consumidor_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#Consumidor_GridCreateBtn').click(function () {
            var formInstance = $("#Consumidor_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            ConsumidorSeguridadSupport.Consumidor_GridShowModal($('#Consumidor_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#Consumidor_GridPopup').find('#Consumidor_GridSaveBtn').click(function () {
            var formInstance = $("#Consumidor_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#Consumidor_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#Consumidor_GridSaveBtn').html();
                $('#Consumidor_GridSaveBtn').html('Procesando...');
                $('#Consumidor_GridSaveBtn').prop('disabled', true);

                ConsumidorSeguridadSupport.currentRow.Id_Consumidor = generalSupport.NumericValue('#Id_Consumidor', -9999999999, 9999999999);
                ConsumidorSeguridadSupport.currentRow.Id_Compania = generalSupport.NumericValue('#Id_Compania', -99999, 99999);
                ConsumidorSeguridadSupport.currentRow.Codigo_Secreto = $('#Codigo_Secreto').val();
                ConsumidorSeguridadSupport.currentRow.Nombre_Consumidor = $('#Nombre_Consumidor').val();
                ConsumidorSeguridadSupport.currentRow.Identificador_Consumidor = $('#Identificador_Consumidor').val();
                ConsumidorSeguridadSupport.currentRow.Vida_Token_Acceso = generalSupport.NumericValue('#Vida_Token_Acceso', -999999, 999999);
                ConsumidorSeguridadSupport.currentRow.Estado_Registro = $('#Estado_Registro').val();
                ConsumidorSeguridadSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                ConsumidorSeguridadSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -99999, 99999);
                ConsumidorSeguridadSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';
                ConsumidorSeguridadSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -99999, 99999);

                $('#Consumidor_GridSaveBtn').prop('disabled', false);
                $('#Consumidor_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    ConsumidorSeguridadSupport.Consumidor_Grid_update(ConsumidorSeguridadSupport.currentRow, $modal);
                }
                else {                    
                    ConsumidorSeguridadSupport.Consumidor_Grid_insert(ConsumidorSeguridadSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.Consumidor_GridShowModal = function (md, title, row) {
        var formInstance = $("#Consumidor_GridEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { Id_Consumidor: 0, Id_Compania: 0, Codigo_Secreto: '', Nombre_Consumidor: '', Identificador_Consumidor: '', Vida_Token_Acceso: 0, Estado_Registro: '', CreationDate: null, CreatorUserCode: 0, UpdateDate: null, UpdateUserCode: 0 };

        md.data('id', row.Id_Consumidor);
        md.find('.modal-title').text(title);

        ConsumidorSeguridadSupport.Consumidor_GridRowToInput(row);
        $('#Id_Consumidor').prop('disabled', (row.Id_Consumidor !== 0));
        $('#Id_Compania').prop('disabled', (row.Id_Consumidor !== 0));
        $('#Codigo_Secreto').prop('disabled', (row.Id_Consumidor !== 0));
        $('#Nombre_Consumidor').prop('disabled', (row.Id_Consumidor !== 0));
        $('#Identificador_Consumidor').prop('disabled', (row.Id_Consumidor !== 0));
        $('#Estado_Registro').prop('disabled', (row.Id_Consumidor !== 0));
        $('#CreationDate').prop('disabled', true);
        $('#CreatorUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.Consumidor_GridRowToInput = function (row) {
        ConsumidorSeguridadSupport.currentRow = row;
        AutoNumeric.set('#Id_Consumidor', row.Id_Consumidor);
        AutoNumeric.set('#Id_Compania', row.Id_Compania);
        $('#Codigo_Secreto').val(row.Codigo_Secreto);
        $('#Nombre_Consumidor').val(row.Nombre_Consumidor);
        $('#Identificador_Consumidor').val(row.Identificador_Consumidor);
        AutoNumeric.set('#Vida_Token_Acceso', row.Vida_Token_Acceso);
        ConsumidorSeguridadSupport.LookUpForEstado_Registro(row.Estado_Registro, '');
        $('#Estado_Registro').trigger('change');
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);

    };
    this.Consumidor_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/ConsumidorSeguridadActions.aspx/Consumidor_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#Consumidor_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.Id_Consumidor_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.Id_Compania_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Vida_Token_Acceso_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999,
            decimalPlaces: 0,
            minimumValue: -999999
        });
      };
    this.CreatorUserCode_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.UpdateUserCode_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };





  this.Init = function(){
    
    moment.locale(app.user.languageName);
    
   generalSupport.TranslateInit('ConsumidorSeguridad', function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        ConsumidorSeguridadSupport.ValidateSetup();
        
        

    ConsumidorSeguridadSupport.ControlBehaviour();
    ConsumidorSeguridadSupport.ControlActions();
    

    app.core.AsyncWebMethod("/fasi/dli/forms/ConsumidorSeguridadActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), false,
            JSON.stringify({
                id: $('#ConsumidorSeguridadFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
               if (data.d.Success === true) {
                  ConsumidorSeguridadSupport.CallRenderLookUps(data);
                
            
                $("#Consumidor_GridTblPlaceHolder").replaceWith('<table id="Consumidor_GridTbl"></table>');
    ConsumidorSeguridadSupport.Consumidor_GridTblSetup($('#Consumidor_GridTbl'));

                    ConsumidorSeguridadSupport.Consumidor_GridTblRequest();

            
            
            
             }
});

   }, 'dli/forms');
  };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: ['Administrador'],
        Element: $("#ConsumidorSeguridadMainForm"),
        CallBack: ConsumidorSeguridadSupport.Init
    });
});

window.Consumidor_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        ConsumidorSeguridadSupport.Consumidor_GridShowModal($('#Consumidor_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
