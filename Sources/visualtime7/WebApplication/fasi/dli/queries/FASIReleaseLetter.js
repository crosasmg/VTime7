var FASIReleaseLetterSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#FASIReleaseLetterFormId').val(),
            inicio: generalSupport.DatePickerValueInputToObject('#inicio'),
            fin: generalSupport.DatePickerValueInputToObject('#fin')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#FASIReleaseLetterFormId').val(data.InstanceFormId);
        $('#inicio').val(generalSupport.ToJavaScriptDateCustom(data.inicio, generalSupport.DateFormat()));
        $('#fin').val(generalSupport.ToJavaScriptDateCustom(data.fin, generalSupport.DateFormat()));


        FASIReleaseLetterSupport.ItemsTblRequest();
        if (data.Items_Item !== null)
            $('#ItemsTbl').bootstrapTable('load', data.Items_Item);

    };

    this.ControlBehaviour = function () {







        $('#inicio_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#inicio_group');
        $('#fin_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#fin_group');


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               FASIReleaseLetterSupport.ObjectToInput(data.d.Data.Instance, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };






    this.ControlActions =   function () {

        $('#btnOk').click(function (event) {
            var formInstance = $("#FASIReleaseLetterMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#btnOk'));
                btnLoading.start();
                FASIReleaseLetterSupport.ItemsTblRequest();
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#FASIReleaseLetterMainForm").validate({
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
                inicio: {
                    required: true,
                    DatePicker: true
                },
                fin: {
                    required: true,
                    DatePicker: true
                }
            },
            messages: {
                inicio: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                fin: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                }
            }
        });

    };

    this.ItemsTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            sortable: true,
            sidePagination: 'client',
            columns: [{
                field: 'PAQUETE',
                title: 'Paquete',
                formatter: 'FASIReleaseLetterSupport.BITACORAFORMPAQUETE_Formatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'FUENTE',
                title: 'Fuente',
                sortable: true,
                halign: 'center'
            }, {
                field: 'FECHA',
                title: 'Fecha',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'TFSIDS',
                title: 'TFS',
                sortable: true,
                halign: 'center'
            }, {
                field: 'QUIEN',
                title: 'Quien',
                sortable: true,
                halign: 'center'
            }, {
                field: 'PAIS',
                title: 'País',
                sortable: true,
                halign: 'center'
            }, {
                field: 'PROYECTO',
                title: 'Proyecto',
                sortable: true,
                halign: 'center'
            }, {
                field: 'MOTIVO',
                title: 'Motivo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CATEGORIA',
                title: 'Categoria',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DESCRIPCION',
                title: 'Descripción',
                sortable: true,
                halign: 'center'
            }]
        });




    };


    this.ItemsTblRequest = function (params) {
        if ($("#FASIReleaseLetterMainForm").validate().checkForm()) {
            app.core.AsyncWebMethod("/fasi/dli/queries/FASIReleaseLetterActions.aspx/ItemsTblDataLoad", false,
              JSON.stringify({
                                               BITACORAFORMFECHA2: generalSupport.DatePickerValueInputToObject('#inicio'),
                BITACORAFORMFECHA3: generalSupport.DatePickerValueInputToObject('#fin')
              }),
              function (data) {
                  $('#ItemsTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        }
    };




    this.BITACORAFORMPAQUETE_Formatter = function (value, row, index) {
        return '<a href="https://www.inmotiontools.com/ftp' + row.FUENTE + '/' + value + '">' + value + '</a>';
    };






  this.Init = function(){
    
    moment.locale(app.user.languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('FASI - Release Letter');
        

    FASIReleaseLetterSupport.ControlBehaviour();
    FASIReleaseLetterSupport.ControlActions();
    FASIReleaseLetterSupport.ValidateSetup();

    $('#inicio').val(generalSupport.URLDateValue('inicio'));
    $('#fin').val(generalSupport.URLDateValue('fin'));

    $("#ItemsTblPlaceHolder").replaceWith('<table id="ItemsTbl"></table>');
    FASIReleaseLetterSupport.ItemsTblSetup($('#ItemsTbl'));

    if ($('#inicio').val() === '')
    $('#inicio').val(moment().add(-90, 'days').format(generalSupport.DateFormat()));
    if ($('#fin').val() === '')
    $('#fin').val(moment().add(1, 'days').format(generalSupport.DateFormat()));
        FASIReleaseLetterSupport.ItemsTblRequest();



    if(generalSupport.URLStringValue('notheader') === 'y') $('#zoneHeader').toggleClass('hidden', true);



  };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: [],
        Element: $("#FASIReleaseLetterMainForm"),
        CallBack: FASIReleaseLetterSupport.Init
    });
});

