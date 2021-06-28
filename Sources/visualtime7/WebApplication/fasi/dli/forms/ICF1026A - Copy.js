var ICF1026ASupport2 = new function () {

    this.currentRow = {};
    this.Parameter2Lkp = [];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#ICF1026AFormId').val(),
            Parameter1: $('#Parameter1').val(),
            Parameter2: $('#Parameter2').val()
        };
        return data;
    };

    this.ObjectToInput = function (data) {
        $('#ICF1026AFormId').val(data.InstanceFormId);

        ICF1026ASupport.LookUpForParameter1(data.Parameter1);


    };

    this.ControlBehaviour = function () {
                 $('#Parameter1').select2({  
	        placeholder: '',
	        ajax: {
	            type: "POST",
	            url: '/fasi/dli/forms/ICF1026AActions.aspx/LookUpForParameter1ByFilter',
	            contentType: "application/json; charset=utf-8",
	            dataType: 'json',
	            delay: 250,
	            data: function (params) {
                    // Se formatan los datos que se envía por parámetro
	                var query = {
                        id: $('#ICF1026AFormId').val(),
	                    filter: params.term ? params.term : '',
	                    pageLength: 10,
                        currentPage: params.page ? params.page - 1 : 0
	                }
	                return JSON.stringify(query);
	            },
	            processResults: function (response) {
                    // Se formatea los datos que recibe el componente
	                var data = $.map(response.d.Data, function (obj) {
	                    obj.id = obj.Code;
	                    obj.text = obj.Description;

	                    return obj;
	                });

	                return {
	                    results: data,
	                    pagination: {
	                        more: data.length >= 10
	                    }
	                };
	            }
	        },
	        templateResult: function (item) {
	            if (item.id) return item.id + ' | ' + item.text;
	            return item.text;
	        },
	        templateSelection: function (item) {
	            if (item.id) return item.id + ' | ' + item.text;
	            return item.text;
	        }
	    });









    };

    this.ActionProcess = function (data) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                ICF1026ASupport.ObjectToInput(data.d.Data);
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
            url: "/fasi/dli/forms/ICF1026AActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                id: $('#ICF1026AFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            success: function (data) {
                $.LoadingOverlay("hide");

                ICF1026ASupport.ActionProcess(data);
                
                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/ICF1026A.aspx?id=' + $('#ICF1026AFormId').val());
              
          

            },
            error: function (qXHR, textStatus, errorThrown) {
                $.LoadingOverlay("hide");
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };




    this.ControlActions = function () {


    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();
    
        $("#ICF1026AMainForm").validate({
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

    };
   this.LookUpForParameter1 = function (defaultValue) {
        if (defaultValue) {
            var select = $('#Parameter1');

            $.ajax({
                type: 'GET',
                url: '/fasi/dli/forms/ICF1026AActions.aspx/LookUpForParameter1ByValue?id=' + $('#ICF1026AFormId').val() + '&value=' + defaultValue,
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
            }).then(function (response) {

                if (response.d.Data.length > 0) {
                    // Se crea el "option" y lo agrega
                    var option = new Option(response.d.Data[0].Description, response.d.Data[0].Code, true, true);
                    select.append(option).trigger('change');

                    // Se llama de forma manual el evento de selección
                    select.trigger({
                        type: 'select2:select',
                        params: {
                            data: response.d.Data[0]
                        }
                    });
                }
            });
        }
    };
   this.LookUpForParameter2 = function (defaultValue) {
        var ctrol = $('#Parameter2');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "GET",
                url: constants.fasiApi.base + 'BackOffice/v1/ClientsLkpPagination?startIndex=0&' + 'endIndex=20&' + 'filter=*',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({  }),
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
                    },
                success: function (data) {
                    ctrol.children().remove();
                    if (data.Successfully === true) {
                        
                        data.Data.forEach(function (element) {
                            ctrol.append($('<option />').val(element.Code).text(element.Description));
                        });

                        if (defaultValue !== null)
                            ctrol.val(defaultValue).change();
                        else
                            ctrol.val(0).change();
                            
                            
                    }
                    else
                        generalSupport.NotifyFail(data.d.Reason, data.d.Code);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString())
                    ctrol.val(defaultValue).change();
    };





};

$(document).ready(function () {
    moment.locale('es');
    generalSupport.getUser();

    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('Combo con BigData Isaac');
        

    ICF1026ASupport.ControlBehaviour();
    ICF1026ASupport.ControlActions();
    ICF1026ASupport.ValidateSetup();
    ICF1026ASupport.Initialization();





});

