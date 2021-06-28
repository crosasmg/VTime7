var HT5InspectionAutoQuestionnaireUWSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5InspectionAutoQuestionnaireUWFormId').val(),
            InspectionAutoMileage: generalSupport.NumericValue('#Mileage', 0, 9999999999),
            InspectionAutoCleanCarIndicator: $('#CleanCarIndicator').is(':checked'),
            InspectionAutoAirConditionOk: $('#AirConditionOk').is(':checked'),
            InspectionAutoAirConditionComments: $('#AirConditionComments').val(),
            InspectionAutoEngineOk: $('#EngineOk').is(':checked'),
            InspectionAutoEngineComments: $('#EngineComments').val(),
            InspectionAutoLightingSystemOK: $('#LightingSystemOK').is(':checked'),
            InspectionAutoLightingSystemComments: $('#LightingSystemComments').val(),
            InspectionAutoRubbersOk: $('#RubbersOk').is(':checked'),
            InspectionAutoRubbersComments: $('#RubbersComments').val(),
            InspectionAutoEngineSerialNumber: $('#EngineSerialNumber').val(),
            InspectionAutoChassis: $('#Chassis').val(),
            InspectionAutoSerialsOK: $('#SerialsOK').is(':checked'),
            InspectionAutoSerialsComments: $('#SerialsComments').val(),
            InspectionAutoAlarm: $('#Alarm').is(':checked'),
            InspectionAutoLockingBar: $('#LockingBar').is(':checked'),
            InspectionAutoSatelliteTrackingSystem: $('#SatelliteTrackingSystem').is(':checked'),
            InspectionAutoMulTLock: $('#MulTLock').is(':checked'),
            InspectionAutoTrabegas: $('#Trabegas').is(':checked'),
            InspectionAutoArmoredVehicle: $('#ArmoredVehicle').is(':checked'),
            InspectionAutoImageFrontPlace: $('#FileGeneratedNameImageFrontPlace').val(),
            InspectionAutoImageBackPlace: $('#FileGeneratedNameImageBackPlace').val(),
            InspectionAutoImageRightSidePlace: $('#FileGeneratedNameImageRightSidePlace').val(),
            InspectionAutoImageLeftSidePlace: $('#FileGeneratedNameImageLeftSidePlace').val(),
            InspectionAutoEvidencePreviousCollisions: $('#EvidencePreviousCollisions').is(':checked'),
            InspectionAutoImage1EvidencePreviousCollision: $('#FileGeneratedNameImage1EvidencePreviousCollision').val(),
            InspectionAutoImage2EvidencePreviousCollision: $('#FileGeneratedNameImage2EvidencePreviousCollision').val(),
            InspectionAutoImage3EvidencePreviousCollision: $('#FileGeneratedNameImage3EvidencePreviousCollision').val(),
            InspectionAutoImage4EvidencePreviousCollision: $('#FileGeneratedNameImage4EvidencePreviousCollision').val(),
            InspectionAutoGeneralPoints: generalSupport.NumericValue('#GeneralPoints', 0, 999),
            InspectionAutoGeneralComments: $('#GeneralComments').val(),
            InspectionAutoInspectionDate: generalSupport.DatePickerValueInputToObject('#InspectionDate'),
            InspectionAutoInspectionTime: $('#InspectionTime').val(),
            InspectionAutoInspectionPlace: $('#InspectionPlace').val(),
            InspectionAutoDateReceived: generalSupport.DatePickerValueInputToObject('#DateReceived')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#HT5InspectionAutoQuestionnaireUWFormId').val(data.InstanceFormId);
        $('#ClientName').html(data.ClientName);
        $('#uwcaseid').html(data.uwcaseid);
        AutoNumeric.set('#Mileage', data.InspectionAutoMileage);
        $('#CleanCarIndicator').prop("checked", data.InspectionAutoCleanCarIndicator);
        $('#AirConditionOk').prop("checked", data.InspectionAutoAirConditionOk);
        $('#AirConditionComments').val(data.InspectionAutoAirConditionComments);
        $('#EngineOk').prop("checked", data.InspectionAutoEngineOk);
        $('#EngineComments').val(data.InspectionAutoEngineComments);
        $('#LightingSystemOK').prop("checked", data.InspectionAutoLightingSystemOK);
        $('#LightingSystemComments').val(data.InspectionAutoLightingSystemComments);
        $('#RubbersOk').prop("checked", data.InspectionAutoRubbersOk);
        $('#RubbersComments').val(data.InspectionAutoRubbersComments);
        $('#EngineSerialNumber').val(data.InspectionAutoEngineSerialNumber);
        $('#Chassis').val(data.InspectionAutoChassis);
        $('#SerialsOK').prop("checked", data.InspectionAutoSerialsOK);
        $('#SerialsComments').val(data.InspectionAutoSerialsComments);
        $('#Alarm').prop("checked", data.InspectionAutoAlarm);
        $('#LockingBar').prop("checked", data.InspectionAutoLockingBar);
        $('#SatelliteTrackingSystem').prop("checked", data.InspectionAutoSatelliteTrackingSystem);
        $('#MulTLock').prop("checked", data.InspectionAutoMulTLock);
        $('#Trabegas').prop("checked", data.InspectionAutoTrabegas);
        $('#ArmoredVehicle').prop("checked", data.InspectionAutoArmoredVehicle);
        $('#FileGeneratedNameImageFrontPlace').val(data.InspectionAutoImageFrontPlace);
        $('#FileGeneratedNameImageBackPlace').val(data.InspectionAutoImageBackPlace);
        $('#FileGeneratedNameImageRightSidePlace').val(data.InspectionAutoImageRightSidePlace);
        $('#FileGeneratedNameImageLeftSidePlace').val(data.InspectionAutoImageLeftSidePlace);
        $('#EvidencePreviousCollisions').prop("checked", data.InspectionAutoEvidencePreviousCollisions);
        $('#FileGeneratedNameImage1EvidencePreviousCollision').val(data.InspectionAutoImage1EvidencePreviousCollision);
        $('#FileGeneratedNameImage2EvidencePreviousCollision').val(data.InspectionAutoImage2EvidencePreviousCollision);
        $('#FileGeneratedNameImage3EvidencePreviousCollision').val(data.InspectionAutoImage3EvidencePreviousCollision);
        $('#FileGeneratedNameImage4EvidencePreviousCollision').val(data.InspectionAutoImage4EvidencePreviousCollision);
        AutoNumeric.set('#GeneralPoints', data.InspectionAutoGeneralPoints);
        $('#GeneralComments').val(data.InspectionAutoGeneralComments);
        $('#InspectionDate').val(generalSupport.ToJavaScriptDateCustom(data.InspectionAutoInspectionDate, generalSupport.DateFormat()));
        $('#InspectionTime').val(data.InspectionAutoInspectionTime);
        $('#InspectionPlace').val(data.InspectionAutoInspectionPlace);
        $('#DateReceived').val(generalSupport.ToJavaScriptDateCustom(data.InspectionAutoDateReceived, generalSupport.DateFormat()));



    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#Mileage', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#GeneralPoints', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: 0
        });




        $('#InspectionDate_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        $('#DateReceived_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                HT5InspectionAutoQuestionnaireUWSupport.ObjectToInput(data.d.Data, source);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        app.core.AsyncWebMethod("/fasi/dli/forms/HT5InspectionAutoQuestionnaireUWActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), true,
            JSON.stringify({
                id: $('#HT5InspectionAutoQuestionnaireUWFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {

                HT5InspectionAutoQuestionnaireUWSupport.ActionProcess(data, 'Initialization');

                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/HT5InspectionAutoQuestionnaireUW.aspx?id=' + $('#HT5InspectionAutoQuestionnaireUWFormId').val());
              
          
                HT5InspectionAutoQuestionnaireUWSupport.InitializationFileBoxImageFrontPlace();
                HT5InspectionAutoQuestionnaireUWSupport.InitializationFileBoxImageBackPlace();
                HT5InspectionAutoQuestionnaireUWSupport.InitializationFileBoxImageRightSidePlace();
                HT5InspectionAutoQuestionnaireUWSupport.InitializationFileBoxImageLeftSidePlace();
                HT5InspectionAutoQuestionnaireUWSupport.InitializationFileBoxImage1EvidencePreviousCollision();
                HT5InspectionAutoQuestionnaireUWSupport.InitializationFileBoxImage2EvidencePreviousCollision();
                HT5InspectionAutoQuestionnaireUWSupport.InitializationFileBoxImage3EvidencePreviousCollision();
                HT5InspectionAutoQuestionnaireUWSupport.InitializationFileBoxImage4EvidencePreviousCollision();

            });
    };

   this.InitializationFileBoxImageFrontPlace = function () {
        var inputFileBox = $("#ImageFrontPlace");

        ///////    Cargar los archivos     //////////
        var initialPreview = new Array();
        var initialPreviewConfig = new Array();
        var initialPreviewAsData = false;        

        // Se contiene archivo
        if ($("#FileOriginalNameImageFrontPlace").val() !== '' && $("#FileGeneratedNameImageFrontPlace").val() !== '') {

            initialPreviewAsData = true;

            // Carga el archivo
            initialPreview.push('http://' + window.location.host + '/fasi/dli/Uploads/' + $("#FileGeneratedNameImageFrontPlace").val());
            initialPreviewConfig.push({ caption: $("#FileOriginalNameImageFrontPlace").val(), key: $("#FileGeneratedNameImageFrontPlace").val(), url: false });
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
                HT5InspectionAutoQuestionnaireUWSupport.UpdateFileBoxListAfterUploadFileImageFrontPlace(data.filenames[index], data.response.id);
                
            });
    };

    this.UpdateFileBoxListAfterUploadFileImageFrontPlace = function (fileOriginalName, fileGeneratedName) {     
        $('#FileOriginalNameImageFrontPlace').val(fileOriginalName);
        $('#FileGeneratedNameImageFrontPlace').val(fileGeneratedName);
    };
   this.InitializationFileBoxImageBackPlace = function () {
        var inputFileBox = $("#ImageBackPlace");

        ///////    Cargar los archivos     //////////
        var initialPreview = new Array();
        var initialPreviewConfig = new Array();
        var initialPreviewAsData = false;        

        // Se contiene archivo
        if ($("#FileOriginalNameImageBackPlace").val() !== '' && $("#FileGeneratedNameImageBackPlace").val() !== '') {

            initialPreviewAsData = true;

            // Carga el archivo
            initialPreview.push('http://' + window.location.host + '/fasi/dli/Uploads/' + $("#FileGeneratedNameImageBackPlace").val());
            initialPreviewConfig.push({ caption: $("#FileOriginalNameImageBackPlace").val(), key: $("#FileGeneratedNameImageBackPlace").val(), url: false });
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
                HT5InspectionAutoQuestionnaireUWSupport.UpdateFileBoxListAfterUploadFileImageBackPlace(data.filenames[index], data.response.id);
                
            });
    };

    this.UpdateFileBoxListAfterUploadFileImageBackPlace = function (fileOriginalName, fileGeneratedName) {     
        $('#FileOriginalNameImageBackPlace').val(fileOriginalName);
        $('#FileGeneratedNameImageBackPlace').val(fileGeneratedName);
    };
   this.InitializationFileBoxImageRightSidePlace = function () {
        var inputFileBox = $("#ImageRightSidePlace");

        ///////    Cargar los archivos     //////////
        var initialPreview = new Array();
        var initialPreviewConfig = new Array();
        var initialPreviewAsData = false;        

        // Se contiene archivo
        if ($("#FileOriginalNameImageRightSidePlace").val() !== '' && $("#FileGeneratedNameImageRightSidePlace").val() !== '') {

            initialPreviewAsData = true;

            // Carga el archivo
            initialPreview.push('http://' + window.location.host + '/fasi/dli/Uploads/' + $("#FileGeneratedNameImageRightSidePlace").val());
            initialPreviewConfig.push({ caption: $("#FileOriginalNameImageRightSidePlace").val(), key: $("#FileGeneratedNameImageRightSidePlace").val(), url: false });
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
                HT5InspectionAutoQuestionnaireUWSupport.UpdateFileBoxListAfterUploadFileImageRightSidePlace(data.filenames[index], data.response.id);
                
            });
    };

    this.UpdateFileBoxListAfterUploadFileImageRightSidePlace = function (fileOriginalName, fileGeneratedName) {     
        $('#FileOriginalNameImageRightSidePlace').val(fileOriginalName);
        $('#FileGeneratedNameImageRightSidePlace').val(fileGeneratedName);
    };
   this.InitializationFileBoxImageLeftSidePlace = function () {
        var inputFileBox = $("#ImageLeftSidePlace");

        ///////    Cargar los archivos     //////////
        var initialPreview = new Array();
        var initialPreviewConfig = new Array();
        var initialPreviewAsData = false;        

        // Se contiene archivo
        if ($("#FileOriginalNameImageLeftSidePlace").val() !== '' && $("#FileGeneratedNameImageLeftSidePlace").val() !== '') {

            initialPreviewAsData = true;

            // Carga el archivo
            initialPreview.push('http://' + window.location.host + '/fasi/dli/Uploads/' + $("#FileGeneratedNameImageLeftSidePlace").val());
            initialPreviewConfig.push({ caption: $("#FileOriginalNameImageLeftSidePlace").val(), key: $("#FileGeneratedNameImageLeftSidePlace").val(), url: false });
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
                HT5InspectionAutoQuestionnaireUWSupport.UpdateFileBoxListAfterUploadFileImageLeftSidePlace(data.filenames[index], data.response.id);
                
            });
    };

    this.UpdateFileBoxListAfterUploadFileImageLeftSidePlace = function (fileOriginalName, fileGeneratedName) {     
        $('#FileOriginalNameImageLeftSidePlace').val(fileOriginalName);
        $('#FileGeneratedNameImageLeftSidePlace').val(fileGeneratedName);
    };
   this.InitializationFileBoxImage1EvidencePreviousCollision = function () {
        var inputFileBox = $("#Image1EvidencePreviousCollision");

        ///////    Cargar los archivos     //////////
        var initialPreview = new Array();
        var initialPreviewConfig = new Array();
        var initialPreviewAsData = false;        

        // Se contiene archivo
        if ($("#FileOriginalNameImage1EvidencePreviousCollision").val() !== '' && $("#FileGeneratedNameImage1EvidencePreviousCollision").val() !== '') {

            initialPreviewAsData = true;

            // Carga el archivo
            initialPreview.push('http://' + window.location.host + '/fasi/dli/Uploads/' + $("#FileGeneratedNameImage1EvidencePreviousCollision").val());
            initialPreviewConfig.push({ caption: $("#FileOriginalNameImage1EvidencePreviousCollision").val(), key: $("#FileGeneratedNameImage1EvidencePreviousCollision").val(), url: false });
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
                HT5InspectionAutoQuestionnaireUWSupport.UpdateFileBoxListAfterUploadFileImage1EvidencePreviousCollision(data.filenames[index], data.response.id);
                
            });
    };

    this.UpdateFileBoxListAfterUploadFileImage1EvidencePreviousCollision = function (fileOriginalName, fileGeneratedName) {     
        $('#FileOriginalNameImage1EvidencePreviousCollision').val(fileOriginalName);
        $('#FileGeneratedNameImage1EvidencePreviousCollision').val(fileGeneratedName);
    };
   this.InitializationFileBoxImage2EvidencePreviousCollision = function () {
        var inputFileBox = $("#Image2EvidencePreviousCollision");

        ///////    Cargar los archivos     //////////
        var initialPreview = new Array();
        var initialPreviewConfig = new Array();
        var initialPreviewAsData = false;        

        // Se contiene archivo
        if ($("#FileOriginalNameImage2EvidencePreviousCollision").val() !== '' && $("#FileGeneratedNameImage2EvidencePreviousCollision").val() !== '') {

            initialPreviewAsData = true;

            // Carga el archivo
            initialPreview.push('http://' + window.location.host + '/fasi/dli/Uploads/' + $("#FileGeneratedNameImage2EvidencePreviousCollision").val());
            initialPreviewConfig.push({ caption: $("#FileOriginalNameImage2EvidencePreviousCollision").val(), key: $("#FileGeneratedNameImage2EvidencePreviousCollision").val(), url: false });
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
                HT5InspectionAutoQuestionnaireUWSupport.UpdateFileBoxListAfterUploadFileImage2EvidencePreviousCollision(data.filenames[index], data.response.id);
                
            });
    };

    this.UpdateFileBoxListAfterUploadFileImage2EvidencePreviousCollision = function (fileOriginalName, fileGeneratedName) {     
        $('#FileOriginalNameImage2EvidencePreviousCollision').val(fileOriginalName);
        $('#FileGeneratedNameImage2EvidencePreviousCollision').val(fileGeneratedName);
    };
   this.InitializationFileBoxImage3EvidencePreviousCollision = function () {
        var inputFileBox = $("#Image3EvidencePreviousCollision");

        ///////    Cargar los archivos     //////////
        var initialPreview = new Array();
        var initialPreviewConfig = new Array();
        var initialPreviewAsData = false;        

        // Se contiene archivo
        if ($("#FileOriginalNameImage3EvidencePreviousCollision").val() !== '' && $("#FileGeneratedNameImage3EvidencePreviousCollision").val() !== '') {

            initialPreviewAsData = true;

            // Carga el archivo
            initialPreview.push('http://' + window.location.host + '/fasi/dli/Uploads/' + $("#FileGeneratedNameImage3EvidencePreviousCollision").val());
            initialPreviewConfig.push({ caption: $("#FileOriginalNameImage3EvidencePreviousCollision").val(), key: $("#FileGeneratedNameImage3EvidencePreviousCollision").val(), url: false });
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
                HT5InspectionAutoQuestionnaireUWSupport.UpdateFileBoxListAfterUploadFileImage3EvidencePreviousCollision(data.filenames[index], data.response.id);
                
            });
    };

    this.UpdateFileBoxListAfterUploadFileImage3EvidencePreviousCollision = function (fileOriginalName, fileGeneratedName) {     
        $('#FileOriginalNameImage3EvidencePreviousCollision').val(fileOriginalName);
        $('#FileGeneratedNameImage3EvidencePreviousCollision').val(fileGeneratedName);
    };
   this.InitializationFileBoxImage4EvidencePreviousCollision = function () {
        var inputFileBox = $("#Image4EvidencePreviousCollision");

        ///////    Cargar los archivos     //////////
        var initialPreview = new Array();
        var initialPreviewConfig = new Array();
        var initialPreviewAsData = false;        

        // Se contiene archivo
        if ($("#FileOriginalNameImage4EvidencePreviousCollision").val() !== '' && $("#FileGeneratedNameImage4EvidencePreviousCollision").val() !== '') {

            initialPreviewAsData = true;

            // Carga el archivo
            initialPreview.push('http://' + window.location.host + '/fasi/dli/Uploads/' + $("#FileGeneratedNameImage4EvidencePreviousCollision").val());
            initialPreviewConfig.push({ caption: $("#FileOriginalNameImage4EvidencePreviousCollision").val(), key: $("#FileGeneratedNameImage4EvidencePreviousCollision").val(), url: false });
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
                HT5InspectionAutoQuestionnaireUWSupport.UpdateFileBoxListAfterUploadFileImage4EvidencePreviousCollision(data.filenames[index], data.response.id);
                
            });
    };

    this.UpdateFileBoxListAfterUploadFileImage4EvidencePreviousCollision = function (fileOriginalName, fileGeneratedName) {     
        $('#FileOriginalNameImage4EvidencePreviousCollision').val(fileOriginalName);
        $('#FileGeneratedNameImage4EvidencePreviousCollision').val(fileGeneratedName);
    };



    this.ControlActions = function () {

        $('#EvidencePreviousCollisions').change(function () {

                if ($('#EvidencePreviousCollisions').is(':checked') == true){
                    }                    
                    else {

                        }


        });
        $('#submit').click(function (event) {
                var formInstance = $("#HT5InspectionAutoQuestionnaireUWMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#submit'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/HT5InspectionAutoQuestionnaireUWActions.aspx/submitClick", false,
                          JSON.stringify({
                                        instance: HT5InspectionAutoQuestionnaireUWSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    HT5InspectionAutoQuestionnaireUWSupport.ActionProcess(data, 'submitClick');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('#save').click(function (event) {
                var formInstance = $("#HT5InspectionAutoQuestionnaireUWMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#save'));
                    btnLoading.start();

                    app.core.SyncWebMethod("/fasi/dli/forms/HT5InspectionAutoQuestionnaireUWActions.aspx/saveClick", false,
                          JSON.stringify({
                                        instance: HT5InspectionAutoQuestionnaireUWSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    HT5InspectionAutoQuestionnaireUWSupport.ActionProcess(data, 'saveClick');
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
    
        $("#HT5InspectionAutoQuestionnaireUWMainForm").validate({
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
                ClientName: {
                    maxlength: 15
                },
                uwcaseid: {
                    maxlength: 15
                },
                Mileage: {
                    AutoNumericRequired: true,
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 9999999999
                },
                EngineSerialNumber: {
                    required: true,
                    maxlength: 40
                },
                Chassis: {
                    required: true,
                    maxlength: 40
                },
                GeneralPoints: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999
                },
                InspectionDate: {
                    required: true,
                    DatePicker: true
                },
                InspectionTime: {
                    required: true,
                    maxlength: 5
                },
                InspectionPlace: {
                    required: true,
                    maxlength: 30
                },
                DateReceived: {
                    DatePicker: true
                }
            },
            messages: {
                ClientName: {
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                uwcaseid: {
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                Mileage: {
                    AutoNumericRequired: 'El campo es requerido',
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 9999999999'
                },
                EngineSerialNumber: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 40 caracteres máximo'
                },
                Chassis: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 40 caracteres máximo'
                },
                GeneralPoints: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999'
                },
                InspectionDate: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                InspectionTime: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 5 caracteres máximo'
                },
                InspectionPlace: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 30 caracteres máximo'
                },
                DateReceived: {
                    DatePicker: 'La fecha indicada no es válida'
                }
            }
        });

    };





};

$(document).ready(function () {
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('HT5Reporte de inspección del vehículo');
        

    HT5InspectionAutoQuestionnaireUWSupport.ControlBehaviour();
    HT5InspectionAutoQuestionnaireUWSupport.ControlActions();
    HT5InspectionAutoQuestionnaireUWSupport.ValidateSetup();
    HT5InspectionAutoQuestionnaireUWSupport.Initialization();





});

