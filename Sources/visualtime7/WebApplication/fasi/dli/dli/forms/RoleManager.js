var RoleManagerSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#RoleManagerFormId').val(),
            Role_Role: generalSupport.NormalizeProperties($('#RoleTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data) {
        $('#RoleManagerFormId').val(data.InstanceFormId);


        $('#RoleTbl').bootstrapTable('refreshOptions', { ajax: RoleManagerSupport.RoleTblRequest });
        if (data.Role_Role !== null)
            $('#RoleTbl').bootstrapTable('load', data.Role_Role);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#Id', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      new AutoNumeric('#SecurityLevel', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "9",
            decimalPlaces: 0,
            minimumValue: "0"
        });






    };

    this.ActionProcess = function (data) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                RoleManagerSupport.ObjectToInput(data.d.Data);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.Role_insert = function (row, $modal) {
            var ReleAddResult;
            var errors;
            var ReleAddMessage;
               $.ajax({
                    type: "POST",
                    url: constants.fasiApi.base + 'Members/v1/RoleAdd',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ Id: AutoNumeric.getNumber('#Id'), Name: $('#Name').val(), IsBackOfficeSource: $('#IsBackOfficeSource').is(':checked'), SecurityLevel: AutoNumeric.getNumber('#SecurityLevel') }),
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
                    },
                    success: function (data) {
ReleAddResult = data.Successfully;
ReleAddMessage = data.Reason;
        AutoNumeric.set('#Id', data.Id);

            if (ReleAddResult == true){
                $('#RoleTbl').bootstrapTable('append', row);
                $modal.modal('hide');
                notification.swal.success('Agregar rol', 'Se agregó correctamente el role');
                }                
                else {
                notification.swal.error('Agregar rol', 'No se agregó correctamente el role');

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
    };
    this.Role_update = function (row, $modal) {
            var ReleUpdateResult;
            var errors;
               $.ajax({
                    type: "PUT",
                    url: constants.fasiApi.base + 'Members/v1/RoleUpdate',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ Id: AutoNumeric.getNumber('#Id'), Name: $('#Name').val(), IsBackOfficeSource: $('#IsBackOfficeSource').is(':checked'), SecurityLevel: AutoNumeric.getNumber('#SecurityLevel') }),
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
                    },
                    success: function (data) {
ReleUpdateResult = data.Successfully;
        $('#IsBackOfficeSource').prop("checked", data.Data);

            if (ReleUpdateResult == true){
                notification.swal.success('Actualización de role', 'Se actualizó correctamente el role');
                }                
                else {
                notification.swal.error('Actualización de role', 'No se pudo actualizar correctamente el role');

                    }
            $('#RoleTbl').bootstrapTable('updateByUniqueId', { id: row.Id, row: row });
            $modal.modal('hide');

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
    };
    this.Role_delete = function (row, $modal) {
            var RoleRemoveResult;
            var errors;
               $.ajax({
                    type: "DELETE",
                    url: constants.fasiApi.base + 'Members/v1/RoleRemove?Id=' + AutoNumeric.getNumber('#Id'),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({  }),
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
                    },
                    success: function (data) {
            if (data.Successfully === true) {
           RoleRemoveResult = data.Data;

                       if (RoleRemoveResult == true){
                $('#RoleTbl').bootstrapTable('remove', {field: 'Id', values: [AutoNumeric.getNumber('#Id')]});
                notification.swal.success('Eliminación de role', 'Se eliminó el role');
                }                
                else {
                notification.swal.error('Eliminación de role', 'No se pudo eliminar el role');

                    }

            }
            else
           generalSupport.NotifyFail(data.Reason, data.Code);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
    };
    this.Role_BeforeShowPopup = function (row, $modal) {
        if (row.Id == 0){
               $.ajax({
                    type: "GET",
                    url: constants.fasiApi.base + 'Members/v1/RoleIndex',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({  }),
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
                    },
                    success: function (data) {
            if (data.Successfully === true) {
                   AutoNumeric.set('#Id', data.Data);

           
            }
            else
           generalSupport.NotifyFail(data.Reason, data.Code);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            }

    };

    this.ControlActions = function () {


    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();
    
        $("#RoleManagerMainForm").validate({
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
        $("#RoleEditForm").validate({
            rules: {
                Id: {

                },
                Name: {
                    required: true
                },
                SecurityLevel: {

                }

            },
            messages: {
                Id: {

                },
                Name: {
                    required: 'El campo es requerido'
                },
                SecurityLevel: {

                }

            }
        });

    };

    this.RoleTblRequest = function (params) {
        $.ajax({
             type: "GET",
             url: constants.fasiApi.base + 'Members/v1/Roles?startIndex=' + (((params.data.offset !== undefined) ? params.data.offset : 0)+1) + '&endIndex=' + (((params.data.offset !== undefined) ? params.data.offset : 0)+((params.data.limit !== undefined) ? params.data.limit : 0)) + '&filter=' + ((params.data.search !== undefined) ? params.data.search : ''),
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             data: JSON.stringify({  }),
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
                    },
                   success: function (data) {
                if (data.Successfully === true) {
                    params.success({
                        total: data.Data.Count,
                        rows: data.Data.Items
                    });
                }
                else
                    generalSupport.NotifyFail(data.Reason, data.Code);
              },
               error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
             });
    };
    this.RoleTblSetup = function (table) {
        table.bootstrapTable({
            ajax: RoleManagerSupport.RoleTblRequest,
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'Id',
            sidePagination: 'server',
            search: true,
            showColumns: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
        toolbar: '#Roletoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'Id',
                title: 'Identificador',
                formatter: 'RoleManagerSupport.Id_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'Name',
                title: 'Nombre',
                events: 'RoleActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'SecurityLevel',
                title: 'Nivel  de seguridad',
                formatter: 'RoleManagerSupport.SecurityLevel_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'IsBackOfficeSource',
                title: 'Backoffice',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: false,
                halign: 'center'
            }]
        });


        $('#RoleTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#RoleTbl');
            $('#RoleRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#RoleRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#RoleTbl').bootstrapTable('getSelections'), function (row) {		
                RoleManagerSupport.RoleRowToInput(row);
                RoleManagerSupport.Role_delete(row, null);
                
                return row.Id;
            });

            $('#RoleRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#RoleCreateBtn').click(function () {
            var formInstance = $("#RoleEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            RoleManagerSupport.RoleShowModal($('#RolePopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#RolePopup').find('#RoleSaveBtn').click(function () {
            var formInstance = $("#RoleEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#RolePopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#RoleSaveBtn').html();
                $('#RoleSaveBtn').html('Procesando...');
                $('#RoleSaveBtn').prop('disabled', true);

                RoleManagerSupport.currentRow.Id = AutoNumeric.getNumber('#Id');
                RoleManagerSupport.currentRow.Name = $('#Name').val();
                RoleManagerSupport.currentRow.SecurityLevel = AutoNumeric.getNumber('#SecurityLevel');
                RoleManagerSupport.currentRow.IsBackOfficeSource = $('#IsBackOfficeSource').is(':checked');

                $('#RoleSaveBtn').prop('disabled', false);
                $('#RoleSaveBtn').html(caption);

                if (wm === 'Update') {
                    RoleManagerSupport.Role_update(row, $modal);
                }
                else {                    
                    RoleManagerSupport.Role_insert(row, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });
    };

    this.RoleShowModal = function (md, title, row) {
        row = row || { Id: 0, Name: null, SecurityLevel: 0, IsBackOfficeSource: null };

        md.data('id', row.Id);
        md.find('.modal-title').text(title);

        RoleManagerSupport.RoleRowToInput(row);
        $('#IsBackOfficeSource').prop('disabled', true);
        RoleManagerSupport.Role_BeforeShowPopup(row, md);
        md.modal('show');
    };

    this.RoleRowToInput = function (row) {
        RoleManagerSupport.currentRow = row;
        AutoNumeric.set('#Id', row.Id);
        $('#Name').val(row.Name);
        AutoNumeric.set('#SecurityLevel', row.SecurityLevel);
        $('#IsBackOfficeSource').prop("checked", row.IsBackOfficeSource);

    };


    this.Id_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.SecurityLevel_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "9",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      };


};
$(function ($)
 {
    securitySupport.ValidateAccessRoles(['Administrador']);
});
$(document).ready(function () {
    moment.locale('es');
    generalSupport.getUser();

   generalSupport.TranslateInit(generalSupport.GetCurrentName(), function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
            masterSupport.setPageTitle($.i18n.t('app.title'));
    });
        

    RoleManagerSupport.ControlBehaviour();
    RoleManagerSupport.ControlActions();
    RoleManagerSupport.ValidateSetup();


    $("#RoleTblPlaceHolder").replaceWith('<table id="RoleTbl"></table>');
    RoleManagerSupport.RoleTblSetup($('#RoleTbl'));

        $('#RoleTbl').bootstrapTable('refreshOptions', { ajax: RoleManagerSupport.RoleTblRequest });



});

window.RoleActionEvents = {
    'click .update': function (e, value, row, index) {
        RoleManagerSupport.RoleShowModal($('#RolePopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
