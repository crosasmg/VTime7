var H5CotizacionesTodosLosRamosIconosSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5CotizacionesTodosLosRamosIconosFormId').val(),
            RiskInformationPrimaryInsuredClientClientID: $('#ClientID').val(),
            ClienteProductor: $('#ClienteProductor').val(),
            ImagenSeleccionada: $('#image9').attr('src')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#H5CotizacionesTodosLosRamosIconosFormId').val(data.InstanceFormId);
        $('#CompleteClientNameCTLR').html(data.RiskInformationPrimaryInsuredClientCompleteClientName);
        $('#image9').attr('src', data.ImagenSeleccionada);

        H5CotizacionesTodosLosRamosIconosSupport.LookUpForClientID(data.RiskInformationPrimaryInsuredClientClientID, source);
        H5CotizacionesTodosLosRamosIconosSupport.LookUpForClienteProductor(data.ClienteProductor, source);


    };

    this.ControlBehaviour = function () {





       this.LookUpForClientID = function (defaultValue, source) {
        if (defaultValue) {
            var select = $('#ClientID');

            ajaxJsonHelper.get(constants.fasiApi.backoffice + 'CompleteClientName?clientID=' + defaultValue + '&withClientId=false', null,
                function (data) {
                    if (data.Successfully) {
                        var option = new Option(data.Data, defaultValue, true, true);
                        select.append(option).trigger('change');
                        select.trigger({ type: 'select2:select', params: { data: option } });
                    }
                }
            );
        }
    };




    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               if (source == 'Initialization')
					         H5CotizacionesTodosLosRamosIconosSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5CotizacionesTodosLosRamosIconosSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionesTodosLosRamosIconosActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), true,
            JSON.stringify({
                id: $('#H5CotizacionesTodosLosRamosIconosFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
                if (data.d.Success)
                    $('#H5CotizacionesTodosLosRamosIconosFormId').val(data.d.Data.Instance.InstanceFormId);
                    
                if (data.d.Success === true && data.d.Data.LookUps) {
                    data.d.Data.LookUps.forEach(function (elementSource) {
                        generalSupport.RenderLookUp(elementSource.Key, data.d.Data.Instance[elementSource.Key], 'Initialization', elementSource.Items);
                    });
                }
                







                H5CotizacionesTodosLosRamosIconosSupport.ActionProcess(data, 'Initialization');

                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)                    
                    window.history.replaceState({}, null, '/fasi/dli/forms/' + location.pathname.substring(location.pathname.lastIndexOf("/") + 1) + '?id=' + $('#H5CotizacionesTodosLosRamosIconosFormId').val());
 
              
          

            });
    };




    this.ControlActions =   function () {

        $('#btnCotizarFinal').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#btnCotizarFinal'));
                btnLoading.start();

            app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionesTodosLosRamosIconosActions.aspx/btnCotizarFinalClick", false,
                JSON.stringify({
                    instance: H5CotizacionesTodosLosRamosIconosSupport.InputToObject()
                }),
                function (data) {
                    btnLoading.stop();

                    H5CotizacionesTodosLosRamosIconosSupport.ActionProcess(data, 'btnCotizarFinalClick');
                },
                function () {
                    btnLoading.stop();
                });
            event.preventDefault();
        });
        $('#button3MVVNN').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#button3MVVNN'));
                btnLoading.start();

            app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionesTodosLosRamosIconosActions.aspx/button3MVVNNClick", false,
                JSON.stringify({
                    instance: H5CotizacionesTodosLosRamosIconosSupport.InputToObject()
                }),
                function (data) {
                    btnLoading.stop();

                    H5CotizacionesTodosLosRamosIconosSupport.ActionProcess(data, 'button3MVVNNClick');
                },
                function () {
                    btnLoading.stop();
                });
            event.preventDefault();
        });
        $('#buttonCotVI').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#buttonCotVI'));
                btnLoading.start();

            app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionesTodosLosRamosIconosActions.aspx/buttonCotVIClick", false,
                JSON.stringify({
                    instance: H5CotizacionesTodosLosRamosIconosSupport.InputToObject()
                }),
                function (data) {
                    btnLoading.stop();

                    H5CotizacionesTodosLosRamosIconosSupport.ActionProcess(data, 'buttonCotVIClick');
                },
                function () {
                    btnLoading.stop();
                });
            event.preventDefault();
        });
        $('#CotizaMAD').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#CotizaMAD'));
                btnLoading.start();

            app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionesTodosLosRamosIconosActions.aspx/CotizaMADClick", false,
                JSON.stringify({
                    instance: H5CotizacionesTodosLosRamosIconosSupport.InputToObject()
                }),
                function (data) {
                    btnLoading.stop();

                    H5CotizacionesTodosLosRamosIconosSupport.ActionProcess(data, 'CotizaMADClick');
                },
                function () {
                    btnLoading.stop();
                });
            event.preventDefault();
        });
        $('#CotizaHV').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#CotizaHV'));
                btnLoading.start();

            app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionesTodosLosRamosIconosActions.aspx/CotizaHVClick", false,
                JSON.stringify({
                    instance: H5CotizacionesTodosLosRamosIconosSupport.InputToObject()
                }),
                function (data) {
                    btnLoading.stop();

                    H5CotizacionesTodosLosRamosIconosSupport.ActionProcess(data, 'CotizaHVClick');
                },
                function () {
                    btnLoading.stop();
                });
            event.preventDefault();
        });
        $('#CotizaHV2').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#CotizaHV2'));
                btnLoading.start();

            app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionesTodosLosRamosIconosActions.aspx/CotizaHV2Click", false,
                JSON.stringify({
                    instance: H5CotizacionesTodosLosRamosIconosSupport.InputToObject()
                }),
                function (data) {
                    btnLoading.stop();

                    H5CotizacionesTodosLosRamosIconosSupport.ActionProcess(data, 'CotizaHV2Click');
                },
                function () {
                    btnLoading.stop();
                });
            event.preventDefault();
        });
        $('#button13').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#button13'));
                btnLoading.start();

            app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionesTodosLosRamosIconosActions.aspx/button13Click", false,
                JSON.stringify({
                    instance: H5CotizacionesTodosLosRamosIconosSupport.InputToObject()
                }),
                function (data) {
                    btnLoading.stop();

                    H5CotizacionesTodosLosRamosIconosSupport.ActionProcess(data, 'button13Click');
                },
                function () {
                    btnLoading.stop();
                });
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5CotizacionesTodosLosRamosIconosMainForm").validate({
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
                CompleteClientNameCTLR: {
                    maxlength: 63
                },
                ClienteProductor: {
                }
            },
            messages: {
                CompleteClientNameCTLR: {
                    maxlength: 'El campo permite 63 caracteres máximo'
                },
                ClienteProductor: {
                }
            }
        });

    };
     $('#ClientID').select2({
        placeholder: '',
        width: '100%',
        ajax: {
            url: constants.fasiApi.backoffice + 'LookupClient',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            delay: 250, 
            headers: {
                'Accept-Language': localStorage.getItem('languageName')
            },
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.UserContext().token);
            },
            data: function (params) {
                var page = 10;
                var current = params.page ? params.page - 1 : 0;
                var query = {
                    filter: params.term ? params.term : '',
                    beginIndex: page * current + 1,
                    endIndex: page * (current + 1)
                }
              return query;
            },
            processResults: function (response) {
                if (response.Successfully && response.Data != null) {
                    var data = $.map(response.Data, function (obj) {
                        obj.id = obj.ClientID;
                        obj.text = obj.CompleteClientName;
                        return obj;
                    });
                    return {
                        results: data,
                        pagination: {
                            more: data.length >= 10
                        }
                    };
                }
            }
        },
        templateResult: function (item) {
            if (item.id) return item.id + ' ' + item.text;
            return item.text;
        },
        templateSelection: function (item) {
            if (item.id) return item.id + ' ' + item.text;
            return item.text;
        }
    });
    this.LookUpForClienteProductor = function (defaultValue, source) {
        var ctrol = $('#ClienteProductor');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/H5CotizacionesTodosLosRamosIconosActions.aspx/LookUpForClienteProductor", false,
                JSON.stringify({ id: $('#H5CotizacionesTodosLosRamosIconosFormId').val() }),
                function (data) {
                    ctrol.children().remove();
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Code'] + ' - ' + this['Description']));
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











  this.Init = function(){
    
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('');
        

    H5CotizacionesTodosLosRamosIconosSupport.ControlBehaviour();
    H5CotizacionesTodosLosRamosIconosSupport.ControlActions();
    H5CotizacionesTodosLosRamosIconosSupport.ValidateSetup();
    H5CotizacionesTodosLosRamosIconosSupport.Initialization();


  };
};

$(document).ready(function () {
   H5CotizacionesTodosLosRamosIconosSupport.Init();
});

