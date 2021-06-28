var HT5CounterGuarantorQuestionnaireUWSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5CounterGuarantorQuestionnaireUWFormId').val(),
            ClientName: $('#ClientName').val(),
            uwcaseid: AutoNumeric.getNumber('#uwcaseid'),
            BondRequirementsCounterGuarantorCedulaIdentidad: $('#FileGeneratedNameCedulaIdentidad').val(),
            BondRequirementsCounterGuarantorAutorizacionConyuge: $('#FileGeneratedNameAutorizacionConyuge').val(),
            BondRequirementsCounterGuarantorBalancePersonal: $('#FileGeneratedNameBalancePersonal').val(),
            BondRequirementsCounterGuarantorTitulosPropiedad: $('#FileGeneratedNameTitulosPropiedad').val(),
            BondRequirementsCounterGuarantorReciboServicioBasico: $('#FileGeneratedNameReciboServicioBasico').val(),
            BondRequirementsCounterGuarantorDeclaracionImpuesto: $('#FileGeneratedNameDeclaracionImpuesto').val(),
            BondRequirementsCounterGuarantorReferenciasComerciales: $('#FileGeneratedNameReferenciasComerciales').val(),
            BondRequirementsGeneralComments: $('#GeneralComments').val(),
            BondRequirementsDateReceived: $('#BondRequirementsDateReceived').val() !== '' ? moment($('#BondRequirementsDateReceived').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD')
        };
        return data;
    };

    this.ObjectToInput = function (data) {
        $('#HT5CounterGuarantorQuestionnaireUWFormId').val(data.InstanceFormId);
        $('#ClientName').val(data.ClientName);
        AutoNumeric.set('#uwcaseid', data.uwcaseid);
        $('#FileGeneratedNameCedulaIdentidad').val(data.BondRequirementsCounterGuarantorCedulaIdentidad);
        $('#FileGeneratedNameAutorizacionConyuge').val(data.BondRequirementsCounterGuarantorAutorizacionConyuge);
        $('#FileGeneratedNameBalancePersonal').val(data.BondRequirementsCounterGuarantorBalancePersonal);
        $('#FileGeneratedNameTitulosPropiedad').val(data.BondRequirementsCounterGuarantorTitulosPropiedad);
        $('#FileGeneratedNameReciboServicioBasico').val(data.BondRequirementsCounterGuarantorReciboServicioBasico);
        $('#FileGeneratedNameDeclaracionImpuesto').val(data.BondRequirementsCounterGuarantorDeclaracionImpuesto);
        $('#FileGeneratedNameReferenciasComerciales').val(data.BondRequirementsCounterGuarantorReferenciasComerciales);
        $('#GeneralComments').val(data.BondRequirementsGeneralComments);
        $('#BondRequirementsDateReceived').val(generalSupport.ToJavaScriptDateCustom(data.BondRequirementsDateReceived, 'DD/MM/YYYY'));



    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#uwcaseid', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "0"
        });




        $('#BondRequirementsDateReceived_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });


    };

    this.ActionProcess = function (data) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                HT5CounterGuarantorQuestionnaireUWSupport.ObjectToInput(data.d.Data);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        $.LoadingOverlay("show");
        $.ajax({
            type: "POST",
            url: "/fasi/dli/forms/HT5CounterGuarantorQuestionnaireUWActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                id: $('#HT5CounterGuarantorQuestionnaireUWFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            success: function (data) {
                $.LoadingOverlay("hide");

                HT5CounterGuarantorQuestionnaireUWSupport.ActionProcess(data);
                
                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/HT5CounterGuarantorQuestionnaireUW.aspx?id=' + $('#HT5CounterGuarantorQuestionnaireUWFormId').val());
              
          
                HT5CounterGuarantorQuestionnaireUWSupport.InitializationFileBoxCedulaIdentidad();
                HT5CounterGuarantorQuestionnaireUWSupport.InitializationFileBoxAutorizacionConyuge();
                HT5CounterGuarantorQuestionnaireUWSupport.InitializationFileBoxBalancePersonal();
                HT5CounterGuarantorQuestionnaireUWSupport.InitializationFileBoxTitulosPropiedad();
                HT5CounterGuarantorQuestionnaireUWSupport.InitializationFileBoxReciboServicioBasico();
                HT5CounterGuarantorQuestionnaireUWSupport.InitializationFileBoxDeclaracionImpuesto();
                HT5CounterGuarantorQuestionnaireUWSupport.InitializationFileBoxReferenciasComerciales();

            },
            error: function (qXHR, textStatus, errorThrown) {
                $.LoadingOverlay("hide");
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };

   this.InitializationFileBoxCedulaIdentidad = function () {
        var inputFileBox = $("#CedulaIdentidad");

        ///////    Cargar los archivos     //////////
        var initialPreview = new Array();
        var initialPreviewConfig = new Array();
        var initialPreviewAsData = false;        

        // Se contiene archivo
        if ($("#FileOriginalNameCedulaIdentidad").val() !== '' && $("#FileGeneratedNameCedulaIdentidad").val() !== '') {

            initialPreviewAsData = true;

            // Carga el archivo
            initialPreview.push('http://' + window.location.host + '/fasi/dli/Uploads/' + $("#FileGeneratedNameCedulaIdentidad").val());
            initialPreviewConfig.push({ caption: $("#FileOriginalNameCedulaIdentidad").val(), key: $("#FileGeneratedNameCedulaIdentidad").val(), url: false });
        }

        inputFileBox.fileinput(
            {                
                uploadUrl: '/fasi/handlers/UploadFileHandler.ashx',
                hideThumbnailContent: true,

                // se configura el idioma español (por defecto es inglés, así que sacando la linea abajo tiene el componente en inglés)
                language: "es",

                // se configura la carga inicial de archivos que ya existen
                overwriteInitial: false,
                initialPreviewDownloadUrl: 'http://' + window.location.host + '/fasi/dli/Uploads/{key}',
                initialPreview: initialPreview,
                initialPreviewConfig: initialPreviewConfig,

                // se remove los botones que no se quiere mostrar
                showRemove: false,
                showCancel: false,
                showClose: false,
                showUpload: false,

                // se configura el evento de click
                browseOnZoneClick: true,

                // se configura la cantidad máxima de archivos permitidos
                maxFileCount: 1,
                autoReplace: true,

                // se configura los botones a mostrar en cada archivo cargado
                fileActionSettings: {
                    showRemove: false,
                    showUpload: false,
                    showDownload: true,
                    showZoom: false,
                    showDrag: false,
                    indicatorNew: ''
                }

            }).on('change', function (event) {
                
                inputFileBox.fileinput("upload");                

            }).on('fileuploaded', function (event, data, previewId, index) {
                // Actualiza la referencia de id x nombres de los archivos.
                HT5CounterGuarantorQuestionnaireUWSupport.UpdateFileBoxListAfterUploadFileCedulaIdentidad(data.filenames[index], data.response.id);
                
            });
    };

    this.UpdateFileBoxListAfterUploadFileCedulaIdentidad = function (fileOriginalName, fileGeneratedName) {     
        $('#FileOriginalNameCedulaIdentidad').val(fileOriginalName);
        $('#FileGeneratedNameCedulaIdentidad').val(fileGeneratedName);
    };
   this.InitializationFileBoxAutorizacionConyuge = function () {
        var inputFileBox = $("#AutorizacionConyuge");

        ///////    Cargar los archivos     //////////
        var initialPreview = new Array();
        var initialPreviewConfig = new Array();
        var initialPreviewAsData = false;        

        // Se contiene archivo
        if ($("#FileOriginalNameAutorizacionConyuge").val() !== '' && $("#FileGeneratedNameAutorizacionConyuge").val() !== '') {

            initialPreviewAsData = true;

            // Carga el archivo
            initialPreview.push('http://' + window.location.host + '/fasi/dli/Uploads/' + $("#FileGeneratedNameAutorizacionConyuge").val());
            initialPreviewConfig.push({ caption: $("#FileOriginalNameAutorizacionConyuge").val(), key: $("#FileGeneratedNameAutorizacionConyuge").val(), url: false });
        }

        inputFileBox.fileinput(
            {                
                uploadUrl: '/fasi/handlers/UploadFileHandler.ashx',
                hideThumbnailContent: true,

                // se configura el idioma español (por defecto es inglés, así que sacando la linea abajo tiene el componente en inglés)
                language: "es",

                // se configura la carga inicial de archivos que ya existen
                overwriteInitial: false,
                initialPreviewDownloadUrl: 'http://' + window.location.host + '/fasi/dli/Uploads/{key}',
                initialPreview: initialPreview,
                initialPreviewConfig: initialPreviewConfig,

                // se remove los botones que no se quiere mostrar
                showRemove: false,
                showCancel: false,
                showClose: false,
                showUpload: false,

                // se configura el evento de click
                browseOnZoneClick: true,

                // se configura la cantidad máxima de archivos permitidos
                maxFileCount: 1,
                autoReplace: true,

                // se configura los botones a mostrar en cada archivo cargado
                fileActionSettings: {
                    showRemove: false,
                    showUpload: false,
                    showDownload: true,
                    showZoom: false,
                    showDrag: false,
                    indicatorNew: ''
                }

            }).on('change', function (event) {
                
                inputFileBox.fileinput("upload");                

            }).on('fileuploaded', function (event, data, previewId, index) {
                // Actualiza la referencia de id x nombres de los archivos.
                HT5CounterGuarantorQuestionnaireUWSupport.UpdateFileBoxListAfterUploadFileAutorizacionConyuge(data.filenames[index], data.response.id);
                
            });
    };

    this.UpdateFileBoxListAfterUploadFileAutorizacionConyuge = function (fileOriginalName, fileGeneratedName) {     
        $('#FileOriginalNameAutorizacionConyuge').val(fileOriginalName);
        $('#FileGeneratedNameAutorizacionConyuge').val(fileGeneratedName);
    };
   this.InitializationFileBoxBalancePersonal = function () {
        var inputFileBox = $("#BalancePersonal");

        ///////    Cargar los archivos     //////////
        var initialPreview = new Array();
        var initialPreviewConfig = new Array();
        var initialPreviewAsData = false;        

        // Se contiene archivo
        if ($("#FileOriginalNameBalancePersonal").val() !== '' && $("#FileGeneratedNameBalancePersonal").val() !== '') {

            initialPreviewAsData = true;

            // Carga el archivo
            initialPreview.push('http://' + window.location.host + '/fasi/dli/Uploads/' + $("#FileGeneratedNameBalancePersonal").val());
            initialPreviewConfig.push({ caption: $("#FileOriginalNameBalancePersonal").val(), key: $("#FileGeneratedNameBalancePersonal").val(), url: false });
        }

        inputFileBox.fileinput(
            {                
                uploadUrl: '/fasi/handlers/UploadFileHandler.ashx',
                hideThumbnailContent: true,

                // se configura el idioma español (por defecto es inglés, así que sacando la linea abajo tiene el componente en inglés)
                language: "es",

                // se configura la carga inicial de archivos que ya existen
                overwriteInitial: false,
                initialPreviewDownloadUrl: 'http://' + window.location.host + '/fasi/dli/Uploads/{key}',
                initialPreview: initialPreview,
                initialPreviewConfig: initialPreviewConfig,

                // se remove los botones que no se quiere mostrar
                showRemove: false,
                showCancel: false,
                showClose: false,
                showUpload: false,

                // se configura el evento de click
                browseOnZoneClick: true,

                // se configura la cantidad máxima de archivos permitidos
                maxFileCount: 1,
                autoReplace: true,

                // se configura los botones a mostrar en cada archivo cargado
                fileActionSettings: {
                    showRemove: false,
                    showUpload: false,
                    showDownload: true,
                    showZoom: false,
                    showDrag: false,
                    indicatorNew: ''
                }

            }).on('change', function (event) {
                
                inputFileBox.fileinput("upload");                

            }).on('fileuploaded', function (event, data, previewId, index) {
                // Actualiza la referencia de id x nombres de los archivos.
                HT5CounterGuarantorQuestionnaireUWSupport.UpdateFileBoxListAfterUploadFileBalancePersonal(data.filenames[index], data.response.id);
                
            });
    };

    this.UpdateFileBoxListAfterUploadFileBalancePersonal = function (fileOriginalName, fileGeneratedName) {     
        $('#FileOriginalNameBalancePersonal').val(fileOriginalName);
        $('#FileGeneratedNameBalancePersonal').val(fileGeneratedName);
    };
   this.InitializationFileBoxTitulosPropiedad = function () {
        var inputFileBox = $("#TitulosPropiedad");

        ///////    Cargar los archivos     //////////
        var initialPreview = new Array();
        var initialPreviewConfig = new Array();
        var initialPreviewAsData = false;        

        // Se contiene archivo
        if ($("#FileOriginalNameTitulosPropiedad").val() !== '' && $("#FileGeneratedNameTitulosPropiedad").val() !== '') {

            initialPreviewAsData = true;

            // Carga el archivo
            initialPreview.push('http://' + window.location.host + '/fasi/dli/Uploads/' + $("#FileGeneratedNameTitulosPropiedad").val());
            initialPreviewConfig.push({ caption: $("#FileOriginalNameTitulosPropiedad").val(), key: $("#FileGeneratedNameTitulosPropiedad").val(), url: false });
        }

        inputFileBox.fileinput(
            {                
                uploadUrl: '/fasi/handlers/UploadFileHandler.ashx',
                hideThumbnailContent: true,

                // se configura el idioma español (por defecto es inglés, así que sacando la linea abajo tiene el componente en inglés)
                language: "es",

                // se configura la carga inicial de archivos que ya existen
                overwriteInitial: false,
                initialPreviewDownloadUrl: 'http://' + window.location.host + '/fasi/dli/Uploads/{key}',
                initialPreview: initialPreview,
                initialPreviewConfig: initialPreviewConfig,

                // se remove los botones que no se quiere mostrar
                showRemove: false,
                showCancel: false,
                showClose: false,
                showUpload: false,

                // se configura el evento de click
                browseOnZoneClick: true,

                // se configura la cantidad máxima de archivos permitidos
                maxFileCount: 1,
                autoReplace: true,

                // se configura los botones a mostrar en cada archivo cargado
                fileActionSettings: {
                    showRemove: false,
                    showUpload: false,
                    showDownload: true,
                    showZoom: false,
                    showDrag: false,
                    indicatorNew: ''
                }

            }).on('change', function (event) {
                
                inputFileBox.fileinput("upload");                

            }).on('fileuploaded', function (event, data, previewId, index) {
                // Actualiza la referencia de id x nombres de los archivos.
                HT5CounterGuarantorQuestionnaireUWSupport.UpdateFileBoxListAfterUploadFileTitulosPropiedad(data.filenames[index], data.response.id);
                
            });
    };

    this.UpdateFileBoxListAfterUploadFileTitulosPropiedad = function (fileOriginalName, fileGeneratedName) {     
        $('#FileOriginalNameTitulosPropiedad').val(fileOriginalName);
        $('#FileGeneratedNameTitulosPropiedad').val(fileGeneratedName);
    };
   this.InitializationFileBoxReciboServicioBasico = function () {
        var inputFileBox = $("#ReciboServicioBasico");

        ///////    Cargar los archivos     //////////
        var initialPreview = new Array();
        var initialPreviewConfig = new Array();
        var initialPreviewAsData = false;        

        // Se contiene archivo
        if ($("#FileOriginalNameReciboServicioBasico").val() !== '' && $("#FileGeneratedNameReciboServicioBasico").val() !== '') {

            initialPreviewAsData = true;

            // Carga el archivo
            initialPreview.push('http://' + window.location.host + '/fasi/dli/Uploads/' + $("#FileGeneratedNameReciboServicioBasico").val());
            initialPreviewConfig.push({ caption: $("#FileOriginalNameReciboServicioBasico").val(), key: $("#FileGeneratedNameReciboServicioBasico").val(), url: false });
        }

        inputFileBox.fileinput(
            {                
                uploadUrl: '/fasi/handlers/UploadFileHandler.ashx',
                hideThumbnailContent: true,

                // se configura el idioma español (por defecto es inglés, así que sacando la linea abajo tiene el componente en inglés)
                language: "es",

                // se configura la carga inicial de archivos que ya existen
                overwriteInitial: false,
                initialPreviewDownloadUrl: 'http://' + window.location.host + '/fasi/dli/Uploads/{key}',
                initialPreview: initialPreview,
                initialPreviewConfig: initialPreviewConfig,

                // se remove los botones que no se quiere mostrar
                showRemove: false,
                showCancel: false,
                showClose: false,
                showUpload: false,

                // se configura el evento de click
                browseOnZoneClick: true,

                // se configura la cantidad máxima de archivos permitidos
                maxFileCount: 1,
                autoReplace: true,

                // se configura los botones a mostrar en cada archivo cargado
                fileActionSettings: {
                    showRemove: false,
                    showUpload: false,
                    showDownload: true,
                    showZoom: false,
                    showDrag: false,
                    indicatorNew: ''
                }

            }).on('change', function (event) {
                
                inputFileBox.fileinput("upload");                

            }).on('fileuploaded', function (event, data, previewId, index) {
                // Actualiza la referencia de id x nombres de los archivos.
                HT5CounterGuarantorQuestionnaireUWSupport.UpdateFileBoxListAfterUploadFileReciboServicioBasico(data.filenames[index], data.response.id);
                
            });
    };

    this.UpdateFileBoxListAfterUploadFileReciboServicioBasico = function (fileOriginalName, fileGeneratedName) {     
        $('#FileOriginalNameReciboServicioBasico').val(fileOriginalName);
        $('#FileGeneratedNameReciboServicioBasico').val(fileGeneratedName);
    };
   this.InitializationFileBoxDeclaracionImpuesto = function () {
        var inputFileBox = $("#DeclaracionImpuesto");

        ///////    Cargar los archivos     //////////
        var initialPreview = new Array();
        var initialPreviewConfig = new Array();
        var initialPreviewAsData = false;        

        // Se contiene archivo
        if ($("#FileOriginalNameDeclaracionImpuesto").val() !== '' && $("#FileGeneratedNameDeclaracionImpuesto").val() !== '') {

            initialPreviewAsData = true;

            // Carga el archivo
            initialPreview.push('http://' + window.location.host + '/fasi/dli/Uploads/' + $("#FileGeneratedNameDeclaracionImpuesto").val());
            initialPreviewConfig.push({ caption: $("#FileOriginalNameDeclaracionImpuesto").val(), key: $("#FileGeneratedNameDeclaracionImpuesto").val(), url: false });
        }

        inputFileBox.fileinput(
            {                
                uploadUrl: '/fasi/handlers/UploadFileHandler.ashx',
                hideThumbnailContent: true,

                // se configura el idioma español (por defecto es inglés, así que sacando la linea abajo tiene el componente en inglés)
                language: "es",

                // se configura la carga inicial de archivos que ya existen
                overwriteInitial: false,
                initialPreviewDownloadUrl: 'http://' + window.location.host + '/fasi/dli/Uploads/{key}',
                initialPreview: initialPreview,
                initialPreviewConfig: initialPreviewConfig,

                // se remove los botones que no se quiere mostrar
                showRemove: false,
                showCancel: false,
                showClose: false,
                showUpload: false,

                // se configura el evento de click
                browseOnZoneClick: true,

                // se configura la cantidad máxima de archivos permitidos
                maxFileCount: 1,
                autoReplace: true,

                // se configura los botones a mostrar en cada archivo cargado
                fileActionSettings: {
                    showRemove: false,
                    showUpload: false,
                    showDownload: true,
                    showZoom: false,
                    showDrag: false,
                    indicatorNew: ''
                }

            }).on('change', function (event) {
                
                inputFileBox.fileinput("upload");                

            }).on('fileuploaded', function (event, data, previewId, index) {
                // Actualiza la referencia de id x nombres de los archivos.
                HT5CounterGuarantorQuestionnaireUWSupport.UpdateFileBoxListAfterUploadFileDeclaracionImpuesto(data.filenames[index], data.response.id);
                
            });
    };

    this.UpdateFileBoxListAfterUploadFileDeclaracionImpuesto = function (fileOriginalName, fileGeneratedName) {     
        $('#FileOriginalNameDeclaracionImpuesto').val(fileOriginalName);
        $('#FileGeneratedNameDeclaracionImpuesto').val(fileGeneratedName);
    };
   this.InitializationFileBoxReferenciasComerciales = function () {
        var inputFileBox = $("#ReferenciasComerciales");

        ///////    Cargar los archivos     //////////
        var initialPreview = new Array();
        var initialPreviewConfig = new Array();
        var initialPreviewAsData = false;        

        // Se contiene archivo
        if ($("#FileOriginalNameReferenciasComerciales").val() !== '' && $("#FileGeneratedNameReferenciasComerciales").val() !== '') {

            initialPreviewAsData = true;

            // Carga el archivo
            initialPreview.push('http://' + window.location.host + '/fasi/dli/Uploads/' + $("#FileGeneratedNameReferenciasComerciales").val());
            initialPreviewConfig.push({ caption: $("#FileOriginalNameReferenciasComerciales").val(), key: $("#FileGeneratedNameReferenciasComerciales").val(), url: false });
        }

        inputFileBox.fileinput(
            {                
                uploadUrl: '/fasi/handlers/UploadFileHandler.ashx',
                hideThumbnailContent: true,

                // se configura el idioma español (por defecto es inglés, así que sacando la linea abajo tiene el componente en inglés)
                language: "es",

                // se configura la carga inicial de archivos que ya existen
                overwriteInitial: false,
                initialPreviewDownloadUrl: 'http://' + window.location.host + '/fasi/dli/Uploads/{key}',
                initialPreview: initialPreview,
                initialPreviewConfig: initialPreviewConfig,

                // se remove los botones que no se quiere mostrar
                showRemove: false,
                showCancel: false,
                showClose: false,
                showUpload: false,

                // se configura el evento de click
                browseOnZoneClick: true,

                // se configura la cantidad máxima de archivos permitidos
                maxFileCount: 1,
                autoReplace: true,

                // se configura los botones a mostrar en cada archivo cargado
                fileActionSettings: {
                    showRemove: false,
                    showUpload: false,
                    showDownload: true,
                    showZoom: false,
                    showDrag: false,
                    indicatorNew: ''
                }

            }).on('change', function (event) {
                
                inputFileBox.fileinput("upload");                

            }).on('fileuploaded', function (event, data, previewId, index) {
                // Actualiza la referencia de id x nombres de los archivos.
                HT5CounterGuarantorQuestionnaireUWSupport.UpdateFileBoxListAfterUploadFileReferenciasComerciales(data.filenames[index], data.response.id);
                
            });
    };

    this.UpdateFileBoxListAfterUploadFileReferenciasComerciales = function (fileOriginalName, fileGeneratedName) {     
        $('#FileOriginalNameReferenciasComerciales').val(fileOriginalName);
        $('#FileGeneratedNameReferenciasComerciales').val(fileGeneratedName);
    };



    this.ControlActions = function () {

        $('#save').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#save'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5CounterGuarantorQuestionnaireUWActions.aspx/saveClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5CounterGuarantorQuestionnaireUWSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5CounterGuarantorQuestionnaireUWSupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            event.preventDefault();
        });
        $('#submit').click(function (event) {
            var formInstance = $("#HT5CounterGuarantorQuestionnaireUWMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#submit'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5CounterGuarantorQuestionnaireUWActions.aspx/submitClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5CounterGuarantorQuestionnaireUWSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5CounterGuarantorQuestionnaireUWSupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();
    
        $("#HT5CounterGuarantorQuestionnaireUWMainForm").validate({
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
                uwcaseid: {

                }
            },
            messages: {
                uwcaseid: {

                }
            }
        });

    };





};

$(document).ready(function () {
    moment.locale('es');
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('HT5Documentos solicitados al contragarante');
        

    HT5CounterGuarantorQuestionnaireUWSupport.ControlBehaviour();
    HT5CounterGuarantorQuestionnaireUWSupport.ControlActions();
    HT5CounterGuarantorQuestionnaireUWSupport.ValidateSetup();
    HT5CounterGuarantorQuestionnaireUWSupport.Initialization();





});

