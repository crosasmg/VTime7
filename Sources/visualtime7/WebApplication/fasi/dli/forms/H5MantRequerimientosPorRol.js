var H5MantRequerimientosPorRolSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5MantRequerimientosPorRolFormId').val(),
            TabRequirementTypeByRole_Grid_TabRequirementTypeByRole_Item: generalSupport.NormalizeProperties($('#TabRequirementTypeByRole_GridTbl').bootstrapTable('getData'), 'CreationDate,UpdateDate')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#H5MantRequerimientosPorRolFormId').val(data.InstanceFormId);

        H5MantRequerimientosPorRolSupport.LookUpForRequirementType(source);
        H5MantRequerimientosPorRolSupport.LookUpForRoleCode(source);

        H5MantRequerimientosPorRolSupport.TabRequirementTypeByRole_GridTblRequest();
        if (data.TabRequirementTypeByRole_Grid_TabRequirementTypeByRole_Item !== null)
            $('#TabRequirementTypeByRole_GridTbl').bootstrapTable('load', data.TabRequirementTypeByRole_Grid_TabRequirementTypeByRole_Item);

    };

    this.ControlBehaviour = function () {



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
               if (source == 'Initialization')
					         H5MantRequerimientosPorRolSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5MantRequerimientosPorRolSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.TabRequirementTypeByRole_Grid_insert = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantRequerimientosPorRolActions.aspx/TabRequirementTypeByRole_Grid1InsertCommandActionTabRequirementTypeByRole", false,
               JSON.stringify({ REQUIREMENTTYPE1: row.RequirementType, ROLECODE2: row.RoleCode, CREATORUSERCODE3: generalSupport.UserContext().userId, UPDATEUSERCODE5: generalSupport.UserContext().userId }));
               

        if (data.d.Success === true){
            $('#TabRequirementTypeByRole_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message4 = 'Se agregó correctamente el registro';
            notification.toastr.success('Agregar registro', message4);
            }            
            else {
            var message5 = 'No se pudo agregar el registro';
            notification.swal.error('Agregar registro', message5);

                }

    };
    this.TabRequirementTypeByRole_Grid_update = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantRequerimientosPorRolActions.aspx/TabRequirementTypeByRole_Grid1UpdateCommandActionTabRequirementTypeByRole", false,
               JSON.stringify({ UPDATEUSERCODE1: generalSupport.UserContext().userId, TabRequirementTypeByRoleRequirementType3: row.RequirementType, TabRequirementTypeByRoleRoleCode4: row.RoleCode }));
               

        if (data.d.Success === true){
            $('#TabRequirementTypeByRole_GridTbl').bootstrapTable('updateByUniqueId', { id: row.RequirementType, row: row });
            $modal.modal('hide');
            var message4 = 'Se actualizó correctamente el registro';
            notification.toastr.success('Actualizar registro', message4);
            }            
            else {
            var message5 = 'No se pudo actualizar el registro';
            notification.swal.error('Actualizar registro', message5);

                }

    };
    this.TabRequirementTypeByRole_Grid_delete = function (row, $modal) {
          var data;
        data = app.core.SyncWebMethod("/fasi/dli/forms/H5MantRequerimientosPorRolActions.aspx/TabRequirementTypeByRole_Grid1DeleteCommandActionTabRequirementTypeByRole", false,
               JSON.stringify({ TabRequirementTypeByRoleRequirementType1: row.RequirementType, TabRequirementTypeByRoleRoleCode2: row.RoleCode }));
               

        if (data.d.Success === true){
            $('#TabRequirementTypeByRole_GridTbl').bootstrapTable('remove', {field: 'RequirementType', values: [parseInt(0 + $('#RequirementType').val(), 10)]});
            var message4 = 'Se eliminó correctamente el registro';
            notification.toastr.success('Eliminar registro', message4);
            }            
            else {
            var message5 = 'No se puede eliminar el registro';
            notification.toastr.error('Eliminar registro', message5);

                }

    };

    this.ControlActions =   function () {


    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5MantRequerimientosPorRolMainForm").validate({
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
        $("#TabRequirementTypeByRole_GridEditForm").validate({
            rules: {
                RequirementType: {
                    required: true,
                },
                RoleCode: {
                    required: true,
                },
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
                RequirementType: {
                    required: 'El campo es requerido.',
                },
                RoleCode: {
                    required: 'El campo es requerido.',
                },
                CreationDate: {
                    required: 'El campo es requerido.',
                    DatePicker: 'La fecha indicada no es válida'
                },
                CreatorUserCode: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -999999999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999',
                    required: 'El campo es requerido.'
                },
                UpdateDate: {
                    required: 'El campo es requerido.',
                    DatePicker: 'La fecha indicada no es válida'
                },
                UpdateUserCode: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -999999999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999',
                    required: 'El campo es requerido.'
                }

            }
        });

    };
    this.LookUpForRequirementTypeFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#RequirementType>option[value='" + value + "']").text();
        }
        return '<a href="javascript:void(0)" class="update edit" data-modal-title="Editar" title="Haga click para editar">' + result + '</a>';
    };
    this.LookUpForRequirementType = function (defaultValue, source) {
        var ctrol = $('#RequirementType');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantRequerimientosPorRolActions.aspx/LookUpForRequirementType", false,
                JSON.stringify({ id: $('#H5MantRequerimientosPorRolFormId').val() }),
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
    this.LookUpForRoleCodeFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#RoleCode>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForRoleCode = function (defaultValue, source) {
        var ctrol = $('#RoleCode');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantRequerimientosPorRolActions.aspx/LookUpForRoleCode", false,
                JSON.stringify({ id: $('#H5MantRequerimientosPorRolFormId').val() }),
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

    this.TabRequirementTypeByRole_GridTblSetup = function (table) {
        H5MantRequerimientosPorRolSupport.LookUpForRequirementType('');
        H5MantRequerimientosPorRolSupport.LookUpForRoleCode('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'RequirementType',
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                maxNestedTables: 0,
                pdfmake:{ 
                    enabled : true,
                    docDefinition: {
                        pageOrientation: 'landscape',
                        content : [{
                            layout: {
                                hLineWidth: function (i, node) {
                                    return (i === 0 || i === 1) ? 1 : 0;
                                },
                                vLineWidth: function (i, node) {
                                    return (i === 0 || i === node.table.widths.length) ? 2 : 0;
                                },
                                hLineColor: function (i, node) {
                                    return (i === 0 || i === 1) ? 'black' : 'gray';
                                },
                                vLineColor: function (i, node) {
                                    return (i === 0 || i === node.table.widths.length) ? 'white' : 'gray';
                                },
                                fillColor: function (rowIndex, node, columnIndex) {
                                    return (rowIndex % 2 === 0) ? '#DDEBF7' : null;
                                }
                            }
                        }]
                    } 
                }
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel', 'pdf'],
            toolbar: '#TabRequirementTypeByRole_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'RequirementType',
                title: 'Requisito o requerimiento',
                events: 'TabRequirementTypeByRole_GridActionEvents',
                formatter: 'H5MantRequerimientosPorRolSupport.LookUpForRequirementTypeFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'RoleCode',
                title: 'Rol o Figura',
                formatter: 'H5MantRequerimientosPorRolSupport.LookUpForRoleCodeFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CreationDate',
                title: 'Creado en',
                formatter: 'tableHelperSupport.OnlyDateFormatterWithHoursMinutesSeconds',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'CreatorUserCode',
                title: 'Creado por',
                formatter: 'H5MantRequerimientosPorRolSupport.CreatorUserCode_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'UpdateDate',
                title: 'Última actualización por',
                formatter: 'tableHelperSupport.OnlyDateFormatterWithHoursMinutesSeconds',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'UpdateUserCode',
                title: 'Última actualización en',
                formatter: 'H5MantRequerimientosPorRolSupport.UpdateUserCode_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }]
        });


        $('#TabRequirementTypeByRole_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TabRequirementTypeByRole_GridTbl');
            $('#TabRequirementTypeByRole_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TabRequirementTypeByRole_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TabRequirementTypeByRole_GridTbl').bootstrapTable('getSelections'), function (row) {		
                H5MantRequerimientosPorRolSupport.TabRequirementTypeByRole_GridRowToInput(row);
                H5MantRequerimientosPorRolSupport.TabRequirementTypeByRole_Grid_delete(row, null);
                
                return row.RequirementType;
            });

            $('#TabRequirementTypeByRole_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TabRequirementTypeByRole_GridCreateBtn').click(function () {
            var formInstance = $("#TabRequirementTypeByRole_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5MantRequerimientosPorRolSupport.TabRequirementTypeByRole_GridShowModal($('#TabRequirementTypeByRole_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TabRequirementTypeByRole_GridPopup').find('#TabRequirementTypeByRole_GridSaveBtn').click(function () {
            var formInstance = $("#TabRequirementTypeByRole_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TabRequirementTypeByRole_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TabRequirementTypeByRole_GridSaveBtn').html();
                $('#TabRequirementTypeByRole_GridSaveBtn').html('Procesando...');
                $('#TabRequirementTypeByRole_GridSaveBtn').prop('disabled', true);

                H5MantRequerimientosPorRolSupport.currentRow.RequirementType = parseInt(0 + $('#RequirementType').val(), 10);
                H5MantRequerimientosPorRolSupport.currentRow.RoleCode = parseInt(0 + $('#RoleCode').val(), 10);
                H5MantRequerimientosPorRolSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate') + ' HH:mm:ss';
                H5MantRequerimientosPorRolSupport.currentRow.CreatorUserCode = generalSupport.NumericValue('#CreatorUserCode', -999999999, 999999999);
                H5MantRequerimientosPorRolSupport.currentRow.UpdateDate = generalSupport.DatePickerValue('#UpdateDate') + ' HH:mm:ss';
                H5MantRequerimientosPorRolSupport.currentRow.UpdateUserCode = generalSupport.NumericValue('#UpdateUserCode', -999999999, 999999999);

                $('#TabRequirementTypeByRole_GridSaveBtn').prop('disabled', false);
                $('#TabRequirementTypeByRole_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    H5MantRequerimientosPorRolSupport.TabRequirementTypeByRole_Grid_update(H5MantRequerimientosPorRolSupport.currentRow, $modal);
                }
                else {                    
                    H5MantRequerimientosPorRolSupport.TabRequirementTypeByRole_Grid_insert(H5MantRequerimientosPorRolSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TabRequirementTypeByRole_GridShowModal = function (md, title, row) {
        row = row || { RequirementType: 0, RoleCode: 0, CreationDate: null, CreatorUserCode: 0, UpdateDate: null, UpdateUserCode: 0 };

        md.data('id', row.RequirementType);
        md.find('.modal-title').text(title);

        H5MantRequerimientosPorRolSupport.TabRequirementTypeByRole_GridRowToInput(row);
        $('#RequirementType').prop('disabled', (row.RequirementType !== 0));
        $('#RoleCode').prop('disabled', (row.RequirementType !== 0));
        $('#CreationDate').prop('disabled', true);
        $('#CreatorUserCode').prop('disabled', true);
        $('#UpdateDate').prop('disabled', true);
        $('#UpdateUserCode').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.TabRequirementTypeByRole_GridRowToInput = function (row) {
        H5MantRequerimientosPorRolSupport.currentRow = row;
        H5MantRequerimientosPorRolSupport.LookUpForRequirementType(row.RequirementType, '');
        $('#RequirementType').trigger('change');
        H5MantRequerimientosPorRolSupport.LookUpForRoleCode(row.RoleCode, '');
        $('#RoleCode').trigger('change');
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#CreatorUserCode', row.CreatorUserCode);
        $('#UpdateDate').val(generalSupport.ToJavaScriptDateCustom(row.UpdateDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        AutoNumeric.set('#UpdateUserCode', row.UpdateUserCode);

    };
    this.TabRequirementTypeByRole_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/H5MantRequerimientosPorRolActions.aspx/TabRequirementTypeByRole_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#TabRequirementTypeByRole_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

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





  this.Init = function(){
    securitySupport.ValidateAccessRoles(['EASE1', 'Suscriptor']);
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('Tipos de requerimiento por rol');
        

    H5MantRequerimientosPorRolSupport.ControlBehaviour();
    H5MantRequerimientosPorRolSupport.ControlActions();
    H5MantRequerimientosPorRolSupport.ValidateSetup();

    $("#TabRequirementTypeByRole_GridTblPlaceHolder").replaceWith('<table id="TabRequirementTypeByRole_GridTbl"></table>');
    H5MantRequerimientosPorRolSupport.TabRequirementTypeByRole_GridTblSetup($('#TabRequirementTypeByRole_GridTbl'));

        H5MantRequerimientosPorRolSupport.TabRequirementTypeByRole_GridTblRequest();




  };
};

$(document).ready(function () {
   H5MantRequerimientosPorRolSupport.Init();
});

window.TabRequirementTypeByRole_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        H5MantRequerimientosPorRolSupport.TabRequirementTypeByRole_GridShowModal($('#TabRequirementTypeByRole_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
