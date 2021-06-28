var HT5CambioCorreoClientesDemoSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5CambioCorreoClientesDemoFormId').val(),
            AddressDLI_Address: generalSupport.NormalizeProperties($('#AddressDLITbl').bootstrapTable('getData'), 'RecordEffectiveDate'),
            DireccionABorrar: generalSupport.NumericValue('#DireccionABorrar', -99999, 99999)
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#HT5CambioCorreoClientesDemoFormId').val(data.InstanceFormId);
        AutoNumeric.set('#DireccionABorrar', data.DireccionABorrar);


        if (data.AddressDLI_Address !== null)
            $('#AddressDLITbl').bootstrapTable('load', data.AddressDLI_Address);
        if (data.Client_Client !== null)
            $('#ClientTbl').bootstrapTable('load', data.Client_Client);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#DireccionABorrar', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });




        $('#RecordEffectiveDate_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#RecordEffectiveDate_group');


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               if (source == 'Initialization')
					         HT5CambioCorreoClientesDemoSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   HT5CambioCorreoClientesDemoSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        app.core.AsyncWebMethod("/fasi/dli/forms/HT5CambioCorreoClientesDemoActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), true,
            JSON.stringify({
                id: $('#HT5CambioCorreoClientesDemoFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
                if (data.d.Success)
                    $('#HT5CambioCorreoClientesDemoFormId').val(data.d.Data.Instance.InstanceFormId);
                    
                if (data.d.Success === true && data.d.Data.LookUps) {
                    data.d.Data.LookUps.forEach(function (elementSource) {
                        generalSupport.RenderLookUp(elementSource.Key, data.d.Data.Instance[elementSource.Key], 'Initialization', elementSource.Items);
                    });
                }
                


    $("#AddressDLITblPlaceHolder").replaceWith('<table id="AddressDLITbl"><caption>Correo electrónico de clientes utilizados</caption></table>');
    HT5CambioCorreoClientesDemoSupport.AddressDLITblSetup($('#AddressDLITbl'));
    $("#ClientTblPlaceHolder").replaceWith('<table id="ClientTbl"><caption>Client</caption></table>');
    HT5CambioCorreoClientesDemoSupport.ClientTblSetup($('#ClientTbl'));





                HT5CambioCorreoClientesDemoSupport.ActionProcess(data, 'Initialization');

                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)                    
                    window.history.replaceState({}, null, '/fasi/dli/forms/' + location.pathname.substring(location.pathname.lastIndexOf("/") + 1) + '?id=' + $('#HT5CambioCorreoClientesDemoFormId').val());
 
              
          

            });
    };




    this.ControlActions =   function () {

        $('#button4').click(function (event) {
                var formInstance = $("#HT5CambioCorreoClientesDemoMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#button4'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/HT5CambioCorreoClientesDemoActions.aspx/button4Click", false,
                          JSON.stringify({
                                        instance: HT5CambioCorreoClientesDemoSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    HT5CambioCorreoClientesDemoSupport.ActionProcess(data, 'button4Click');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('#button1').click(function (event) {
                var formInstance = $("#HT5CambioCorreoClientesDemoMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#button1'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/HT5CambioCorreoClientesDemoActions.aspx/button1Click", false,
                          JSON.stringify({
                                        instance: HT5CambioCorreoClientesDemoSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    HT5CambioCorreoClientesDemoSupport.ActionProcess(data, 'button1Click');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#HT5CambioCorreoClientesDemoMainForm").validate({
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
                DireccionABorrar: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999
                }
            },
            messages: {
                DireccionABorrar: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -99999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999'
                }
            }
        });
        $("#AddressDLIEditForm").validate().destroy();
        $("#AddressDLIEditForm").validate({
            rules: {
                RecordEffectiveDate: {
                    required: true,
                    DatePicker: true
                },
                Email: {
                    maxlength: 60
                },
                RecordOwner: {
                    required: true,
                    maxlength: 50
                },
                CustomString: {
                    maxlength: 50
                },
                KeyToAddressRecord: {
                    required: true,
                    maxlength: 50
                }

            },
            messages: {
                RecordEffectiveDate: {
                    required: 'El campo es requerido.',
                    DatePicker: 'La fecha indicada no es válida'
                },
                Email: {
                    maxlength: 'El campo permite 60 caracteres máximo'
                },
                RecordOwner: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 50 caracteres máximo'
                },
                CustomString: {
                    maxlength: 'El campo permite 50 caracteres máximo'
                },
                KeyToAddressRecord: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 50 caracteres máximo'
                }

            }
        });

    };

    this.AddressDLITblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'KeyToAddressRecord',
            toolbar: '#AddressDLItoolbar',
            columns: [{
                field: 'selected',
                checkbox: true,
                formatter: 'HT5CambioCorreoClientesDemoSupport.selected_Formatter'
            }, {
                field: 'RecordEffectiveDate',
                title: 'Fecha de Efecto',
                events: 'AddressDLIActionEvents',
                formatter: 'tableHelperSupport.EditCommandOnlyDateFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'Email',
                title: 'Correo electrónico',
                sortable: false,
                halign: 'center'
            }, {
                field: 'ClientID',
                title: 'Propietario',
                sortable: false,
                halign: 'center'
            }, {
                field: 'CustomString',
                title: 'Nombre',
                sortable: false,
                halign: 'center'
            }, {
                field: 'KeyToAddressRecord',
                title: 'Clave de Acceso a Dirección',
                sortable: false,
                halign: 'center'
            }]
        });


        $('#AddressDLITbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#AddressDLITbl');
            $('#AddressDLIRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#AddressDLIRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#AddressDLITbl').bootstrapTable('getSelections'), function (row) {		
                HT5CambioCorreoClientesDemoSupport.AddressDLIRowToInput(row);
                
                
                return row.KeyToAddressRecord;
            });
            
          $('#AddressDLITbl').bootstrapTable('remove', {
                field: 'KeyToAddressRecord',
                values: ids
           });

            $('#AddressDLIRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#AddressDLICreateBtn').click(function () {
            var formInstance = $("#AddressDLIEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            HT5CambioCorreoClientesDemoSupport.AddressDLIShowModal($('#AddressDLIPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#AddressDLIPopup').find('#AddressDLISaveBtn').click(function () {
            var formInstance = $("#AddressDLIEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#AddressDLIPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#AddressDLISaveBtn').html();
                $('#AddressDLISaveBtn').html('Procesando...');
                $('#AddressDLISaveBtn').prop('disabled', true);

                HT5CambioCorreoClientesDemoSupport.currentRow.RecordEffectiveDate = generalSupport.DatePickerValue('#RecordEffectiveDate');
                HT5CambioCorreoClientesDemoSupport.currentRow.Email = $('#Email').val();
                HT5CambioCorreoClientesDemoSupport.currentRow.ClientID = $('#RecordOwner').val();
                HT5CambioCorreoClientesDemoSupport.currentRow.CustomString = $('#CustomString').val();
                HT5CambioCorreoClientesDemoSupport.currentRow.KeyToAddressRecord = $('#KeyToAddressRecord').val();

                $('#AddressDLISaveBtn').prop('disabled', false);
                $('#AddressDLISaveBtn').html(caption);

                if (wm === 'Update') {
                    $('#AddressDLITbl').bootstrapTable('updateByUniqueId', { id: HT5CambioCorreoClientesDemoSupport.currentRow.KeyToAddressRecord, row: HT5CambioCorreoClientesDemoSupport.currentRow });
                    $modal.modal('hide');
                }
                else {                    
                    $('#AddressDLITbl').bootstrapTable('append', HT5CambioCorreoClientesDemoSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.AddressDLIShowModal = function (md, title, row) {
        var formInstance = $("#AddressDLIEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { RecordEffectiveDate: null, Email: '', ClientID: '', CustomString: '', KeyToAddressRecord: '' };

        md.data('id', row.KeyToAddressRecord);
        md.find('.modal-title').text(title);

        HT5CambioCorreoClientesDemoSupport.AddressDLIRowToInput(row);
        $('#RecordEffectiveDate').prop('disabled', (row.KeyToAddressRecord !== ''));
        $('#RecordOwner').prop('disabled', (row.KeyToAddressRecord !== ''));
        $('#CustomString').prop('disabled', (row.KeyToAddressRecord !== ''));
        $('#KeyToAddressRecord').prop('disabled', (row.KeyToAddressRecord !== ''));

        md.appendTo("body");
        md.modal('show');
    };

    this.AddressDLIRowToInput = function (row) {
        HT5CambioCorreoClientesDemoSupport.currentRow = row;
        $('#RecordEffectiveDate').val(generalSupport.ToJavaScriptDateCustom(row.RecordEffectiveDate, generalSupport.DateFormat()));
        $('#Email').val(row.Email);
        $('#RecordOwner').val(row.ClientID);
        $('#CustomString').val(row.CustomString);
        $('#KeyToAddressRecord').val(row.KeyToAddressRecord);

    };
    this.ClientTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'ClientID',
            columns: [{
                field: 'ClientID',
                title: 'Código',
                sortable: false,
                halign: 'center'
            }, {
                field: 'CompleteClientName',
                title: 'Nombre',
                sortable: false,
                halign: 'center'
            }, {
                field: 'NADDRESSID',
                title: 'NADDRESSID',
                formatter: 'HT5CambioCorreoClientesDemoSupport.NADDRESSID_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });




    };







    this.NADDRESSID_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };


	    this.selected_Formatter = function (value, row, index) {          
          return {
                  disabled: $('#AddressDLITbl *').prop('disabled'),
                  checked: value
                 }
      };



  this.Init = function(){
    securitySupport.ValidateAccessRoles(['EASE1']);
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('HT5Cambio de correo de clientes - demo');
        

    HT5CambioCorreoClientesDemoSupport.ControlBehaviour();
    HT5CambioCorreoClientesDemoSupport.ControlActions();
    HT5CambioCorreoClientesDemoSupport.ValidateSetup();
    HT5CambioCorreoClientesDemoSupport.Initialization();


  };
};

$(document).ready(function () {
   HT5CambioCorreoClientesDemoSupport.Init();
});

window.AddressDLIActionEvents = {
    'click .update': function (e, value, row, index) {
        HT5CambioCorreoClientesDemoSupport.AddressDLIShowModal($('#AddressDLIPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
