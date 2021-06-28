var NSF2501ASupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#NSF2501AFormId').val(),
            Grupo_Acceso_Grid_Grupo_Acceso_Item: generalSupport.NormalizeProperties($('#Grupo_Acceso_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#NSF2501AFormId').val(data.InstanceFormId);

        NSF2501ASupport.LookUpForEstado_Registro(source);

        NSF2501ASupport.Grupo_Acceso_GridTblRequest();
        if (data.Grupo_Acceso_Grid_Grupo_Acceso_Item !== null)
            $('#Grupo_Acceso_GridTbl').bootstrapTable('load', data.Grupo_Acceso_Grid_Grupo_Acceso_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#Id_Grupo_Acceso', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      new AutoNumeric('#CreatorUserCode', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999",
            decimalPlaces: 0,
            minimumValue: "-999999999"
        });
      new AutoNumeric('#UpdateUserCode', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999",
            decimalPlaces: 0,
            minimumValue: "-999999999"
        });




        $('#CreationDate_group').datetimepicker({
            format: generalSupport.DateFormat() + ' HH:mm:ss',
            locale: moment.locale()
        });
        $('#UpdateDate_group').datetimepicker({
            format: generalSupport.DateFormat() + ' HH:mm:ss',
            locale: moment.locale()
        });


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                NSF2501ASupport.ObjectToInput(data.d.Data, source);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.Grupo_Acceso_Grid_insert = function (row, $modal) {
        app.core.AsyncWebMethod("/fasi/dli/forms/NSF2501AActions.aspx/Grupo_Acceso_GridInsertCommandActionGrupo_Acceso", false,
            JSON.stringify({ GRUPO_ACCESOID_GRUPO_ACCESO1: AutoNumeric.getNumber('#Id_Grupo_Acceso'), GRUPO_ACCESODESCRIPCION2: $('#Descripcion').val(), GRUPO_ACCESODESCRIPCION_CORTA3: $('#Descripcion_Corta').val(), GRUPO_ACCESOESTADO_REGISTRO4: $('#Estado_Registro').val(), GRUPO_ACCESOCREATORUSERCODE5: generalSupport.UserContext().userId, GRUPO_ACCESOUPDATEUSERCODE7: generalSupport.UserContext().userId }),
            function (data) {
                

            if (data.d.Success == true){
                $('#Grupo_Acceso_GridTbl').bootstrapTable('append', row);
                $modal.modal('hide');
                notification.toastr.success('Agregar registro', 'Se agregó correctamente el registro');
                }                
                else {
                notification.swal.error('Agregar registro', 'No se pudo agregar el registro');

                    }

            });
    };
    this.Grupo_Acceso_Grid_update = function (row, $modal) {
        app.core.AsyncWebMethod("/fasi/dli/forms/NSF2501AActions.aspx/Grupo_Acceso_GridUpdateCommandActionGrupo_Acceso", false,
            JSON.stringify({ GRUPO_ACCESODESCRIPCION1: $('#Descripcion').val(), GRUPO_ACCESODESCRIPCION_CORTA2: $('#Descripcion_Corta').val(), GRUPO_ACCESOESTADO_REGISTRO3: $('#Estado_Registro').val(), GRUPO_ACCESOUPDATEUSERCODE4: generalSupport.UserContext().userId, GrupoAccesoIdGrupoAcceso6: AutoNumeric.getNumber('#Id_Grupo_Acceso') }),
            function (data) {
                

            if (data.d.Success == true){
                $('#Grupo_Acceso_GridTbl').bootstrapTable('updateByUniqueId', { id: row.Id_Grupo_Acceso, row: row });
                $modal.modal('hide');
                notification.toastr.success('Actualizar registro', 'Se actualizó correctamente el registro');
                }                
                else {
                notification.swal.error('Actualizar registro', 'No se pudo actualizar el registro');

                    }

            });
    };
    this.Grupo_Acceso_Grid_delete = function (row, $modal) {
        app.core.AsyncWebMethod("/fasi/dli/forms/NSF2501AActions.aspx/Grupo_Acceso_GridDeleteCommandActionGrupo_Acceso", false,
            JSON.stringify({ GrupoAccesoIdGrupoAcceso1: AutoNumeric.getNumber('#Id_Grupo_Acceso') }),
            function (data) {
                

            if (data.d.Success == true){
                $('#Grupo_Acceso_GridTbl').bootstrapTable('remove', {field: 'Id_Grupo_Acceso', values: [AutoNumeric.getNumber('#Id_Grupo_Acceso')]});
                notification.toastr.success('Eliminar registro', 'Se eliminó correctamente el registro');
                }                
                else {
                notification.toastr.error('Eliminar registro', 'No se puede eliminar el registro');

                    }

            });
    };
    this.Grupo_Acceso_Grid_BeforeShowPopup = function (row, $modal) {
            var nextId;
        if (row.Id_Grupo_Acceso == 0){
        app.core.AsyncWebMethod("/fasi/dli/forms/NSF2501AActions.aspx/Grupo_Acceso_GridSelectCommandActionGrupo_Acceso", false,
            JSON.stringify({  }),
            function (data) {
                
                 if (data.d.Count !== 0)
                                nextId = data.d.Data.Result; 
                        AutoNumeric.set('#Id_Grupo_Acceso', nextId + 1);


            });
            }

    };

    this.ControlActions = function () {


    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();
    
        $("#NSF2501AMainForm").validate({
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
        $("#Grupo_Acceso_GridEditForm").validate({
            rules: {
                Id_Grupo_Acceso: {
                    required: true
                },
                Descripcion: {
                    required: true,
                    maxlength: 75
                },
                Descripcion_Corta: {
                    required: true,
                    maxlength: 40
                },
                Estado_Registro: {
                    required: true
                },
                CreationDate: {
                    required: true,
                    DatePicker: true
                },
                CreatorUserCode: {
                    required: true
                },
                UpdateDate: {
                    required: true,
                    DatePicker: true
                },
                UpdateUserCode: {
                    required: true
                }

            },
            messages: {
                Id_Grupo_Acceso: {
                    required: 'El campo es requerido.'
                },
                Descripcion: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 75 caracteres máximo'
                },
                Descripcion_Corta: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 40 caracteres máximo'
                },
                Estado_Registro: {
                    required: 'El campo es requerido.'
                },
                CreationDate: {
                    required: 'El campo es requerido.',
                    DatePicker: 'La fecha indicada no es válida'
                },
                CreatorUserCode: {
                    required: 'El campo es requerido.'
                },
                UpdateDate: {
                    required: 'El campo es requerido.',
                    DatePicker: 'La fecha indicada no es válida'
                },
                UpdateUserCode: {
                    required: 'El campo es requerido.'
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

            app.core.AsyncWebMethod("/fasi/dli/forms/NSF2501AActions.aspx/LookUpForEstado_Registro", false,
                JSON.stringify({ id: $('#NSF2501AFormId').val() }),
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

    this.Grupo_Acceso_GridTblSetup = function (table) {
        NSF2501ASupport.LookUpForEstado_Registro('');
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'Id_Grupo_Acceso',
            sidePagination: 'client',
            search: true,
            showColumns: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
        toolbar: '#Grupo_Acceso_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'Id_Grupo_Acceso',
                title: 'Identificador',
                formatter: 'NSF2501ASupport.Id_Grupo_Acceso_FormatterMaskData',
                sortable: false,
                halign: 'center',
                visible: false
            }, {
                field: 'Descripcion',
                title: 'Descripción',
                events: 'Grupo_Acceso_GridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'Descripcion_Corta',
                title: 'Descripcion corta',
                sortable: false,
                halign: 'center'
            }, {
                field: 'Estado_Registro',
                title: 'Estado',
                formatter: 'NSF2501ASupport.LookUpForEstado_RegistroFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'CreationDate',
                title: 'Fecha de Creación',
                formatter: 'tableHelperSupport.OnlyDateFormatterWithHoursMinutesSeconds',
                sortable: false,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'CreatorUserCode',
                title: 'User Code Creador',
                formatter: 'NSF2501ASupport.CreatorUserCode_FormatterMaskData',
                sortable: false,
                halign: 'center',
                visible: false
            }, {
                field: 'UpdateDate',
                title: 'Fecha de Actualización del Registro',
                formatter: 'tableHelperSupport.OnlyDateFormatterWithHoursMinutesSeconds',
                sortable: false,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'UpdateUserCode',
                title: 'Código de Usuario Que Actualiza',
                formatter: 'NSF2501ASupport.UpdateUserCode_FormatterMaskData',
                sortable: false,
                halign: 'center',
                visible: false
            }]
        });


        $('#Grupo_Acceso_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#Grupo_Acceso_GridTbl');
            $('#Grupo_Acceso_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#Grupo_Acceso_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#Grupo_Acceso_GridTbl').bootstrapTable('getSelections'), function (row) {		
                NSF2501ASupport.Grupo_Acceso_GridRowToInput(row);
                NSF2501ASupport.Grupo_Acceso_Grid_delete(row, null);
                
                return row.Id_Grupo_Acceso;
            });

            $('#Grupo_Acceso_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#Grupo_Acceso_GridCreateBtn').click(function () {
            var formInstance = $("#Grupo_Acceso_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            NSF2501ASupport.Grupo_Acceso_GridShowModal($('#Grupo_Acceso_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#Grupo_Acceso_GridPopup').find('#Grupo_Acceso_GridSaveBtn').click(function () {
            var formInstance = $("#Grupo_Acceso_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#Grupo_Acceso_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#Grupo_Acceso_GridSaveBtn').html();
                $('#Grupo_Acceso_GridSaveBtn').html('Procesando...');
                $('#Grupo_Acceso_GridSaveBtn').prop('disabled', true);

                NSF2501ASupport.currentRow.Id_Grupo_Acceso = AutoNumeric.getNumber('#Id_Grupo_Acceso');
                NSF2501ASupport.currentRow.Descripcion = $('#Descripcion').val();
                NSF2501ASupport.currentRow.Descripcion_Corta = $('#Descripcion_Corta').val();
                NSF2501ASupport.currentRow.Estado_Registro = $('#Estado_Registro').val();
                NSF2501ASupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                NSF2501ASupport.currentRow.CreatorUserCode = AutoNumeric.getNumber('#CreatorUserCode');
                NSF2501ASupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';
                NSF2501ASupport.currentRow.UpdateUserCode = AutoNumeric.getNumber('#UpdateUserCode');

                $('#Grupo_Acceso_GridSaveBtn').prop('disabled', false);
                $('#Grupo_Acceso_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    NSF2501ASupport.Grupo_Acceso_Grid_update(NSF2501ASupport.currentRow, $modal);
                }
                else {                    
                    NSF2501ASupport.Grupo_Acceso_Grid_insert(NSF2501ASupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });
    };

    this.Grupo_Acceso_GridShowModal = function (md, title, row) {
        row = row || { Id_Grupo_Acceso: null, Descripcion: null, Descripcion_Corta: null, Estado_Registro: null, CreationDate: null, CreatorUserCode: null, UpdateDate: null, UpdateUserCode: null };

        md.data('id', row.Id_Grupo_Acceso);
        md.find('.modal-title').text(title);

        NSF2501ASupport.Grupo_Acceso_GridRowToInput(row);
        $('#Id_Grupo_Acceso').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#CreatorUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);
        NSF2501ASupport.Grupo_Acceso_Grid_BeforeShowPopup(row, md);
        md.modal('show');
    };

    this.Grupo_Acceso_GridRowToInput = function (row) {
        NSF2501ASupport.currentRow = row;
        AutoNumeric.set('#Id_Grupo_Acceso', row.Id_Grupo_Acceso);
        $('#Descripcion').val(row.Descripcion);
        $('#Descripcion_Corta').val(row.Descripcion_Corta);
        NSF2501ASupport.LookUpForEstado_Registro(row.Estado_Registro, '');
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);

    };
    this.Grupo_Acceso_GridTblRequest = function (params) {
        app.core.AsyncWebMethod("/fasi/dli/forms/NSF2501AActions.aspx/Grupo_Acceso_GridTblDataLoad", false,
            JSON.stringify({
                                filter: ''
            }),
            function (data) {
                    $('#Grupo_Acceso_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);


            });
    };


    this.Id_Grupo_Acceso_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.CreatorUserCode_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999",
            decimalPlaces: 0,
            minimumValue: "-999999999"
        });
      };
    this.UpdateUserCode_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999",
            decimalPlaces: 0,
            minimumValue: "-999999999"
        });
      };


};

$(document).ready(function () {
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('Grupos de acceso');
        

    NSF2501ASupport.ControlBehaviour();
    NSF2501ASupport.ControlActions();
    NSF2501ASupport.ValidateSetup();


    $("#Grupo_Acceso_GridTblPlaceHolder").replaceWith('<table id="Grupo_Acceso_GridTbl"></table>');
    NSF2501ASupport.Grupo_Acceso_GridTblSetup($('#Grupo_Acceso_GridTbl'));

        NSF2501ASupport.Grupo_Acceso_GridTblRequest();



});

window.Grupo_Acceso_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        NSF2501ASupport.Grupo_Acceso_GridShowModal($('#Grupo_Acceso_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
