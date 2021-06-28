var HT5OnLineServiceUnderwriterIISupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5OnLineServiceUnderwriterIIFormId').val(),
            ActionType: $('input:radio[name=ActionType]:checked').val(),
            StartDate: generalSupport.DatePickerValueInputToObject('#StartDate'),
            EndDate: generalSupport.DatePickerValueInputToObject('#EndDate'),
            CaseToQuery: parseInt(0 + $('#CaseToQuery').val(), 10)
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#HT5OnLineServiceUnderwriterIIFormId').val(data.InstanceFormId);
        if($('input:radio[name=ActionType][value=' + data.ActionType +']').length===0)
           $('input:radio[name=ActionType]').prop('checked', false);
        else
           $($('input:radio[name=ActionType][value=' + data.ActionType +']')).prop('checked', true);
        $('#ActionType').data('oldValue', data.ActionType);
        $('#ActionType').val(data.ActionType);

        $('#StartDate').val(generalSupport.ToJavaScriptDateCustom(data.StartDate, generalSupport.DateFormat()));
        $('#EndDate').val(generalSupport.ToJavaScriptDateCustom(data.EndDate, generalSupport.DateFormat()));

        HT5OnLineServiceUnderwriterIISupport.LookUpForLineOfBusiness(source);
        HT5OnLineServiceUnderwriterIISupport.LookUpForDecision(source);
        HT5OnLineServiceUnderwriterIISupport.LookUpForStatus(source);
        HT5OnLineServiceUnderwriterIISupport.LookUpForCaseToQuery(data.CaseToQuery, source);
        HT5OnLineServiceUnderwriterIISupport.LookUpForProduct(data.UnderwritingCaseCollectionProduct, data.UnderwritingCaseCollectionLineOfBusiness, source);

        if (data.UnderwritingCase_UnderwritingCase !== null)
            $('#UnderwritingCaseTbl').bootstrapTable('load', data.UnderwritingCase_UnderwritingCase);

    };

    this.ControlBehaviour = function () {






        $('#LineOfBusiness').on('change', function () {
            var value = $('#LineOfBusiness').val();

            if (value !== null && value !== '0') {
                var skipData = $('#LineOfBusiness').data("skip");

                if (skipData !== undefined && skipData === true)
                    $('#LineOfBusiness').data("skip", false);
                else
                    HT5OnLineServiceUnderwriterIISupport.LookUpForProduct(null, parseInt(0 + $('#LineOfBusiness').val(), 10));
            }
            else
                if($('#LineOfBusiness').val() !== $('#Product').data("parentId1"))
                   $('#Product').children().remove();
        });
  this.LookUpForCaseToQuery = function (defaultValue, source) {
        var ctrol = $('#CaseToQuery');
        var oldvalue = ctrol.val();
     
        if (oldvalue === null)
            oldvalue = 0;
            
        ctrol.data('oldValue', oldvalue );
		
        if (defaultValue === null)
            defaultValue = 0;
         
        ctrol.children().remove();
        ctrol.append($('<option />').val('0').text(' Cargando...'));

       app.core.AsyncWebMethod("/fasi/dli/forms/HT5OnLineServiceUnderwriterIIActions.aspx/LookUpForCaseToQuery", false,
           JSON.stringify({
               formId: $('#HT5OnLineServiceUnderwriterIIFormId').val()
           }),
           function (data) {
               ctrol.children().remove();
               $.each(data.d.Data, function () {
                   ctrol.append($('<option />').val(this['Code']).text(this['Description']));
               });

               ctrol.val(defaultValue);

               if (defaultValue.toString() !== oldvalue.toString())
                   ctrol.change();
           });
  
    };

        $('#StartDate_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        $('#EndDate_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                HT5OnLineServiceUnderwriterIISupport.ObjectToInput(data.d.Data, source);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        app.core.AsyncWebMethod("/fasi/dli/forms/HT5OnLineServiceUnderwriterIIActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), true,
            JSON.stringify({
                id: $('#HT5OnLineServiceUnderwriterIIFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {

                HT5OnLineServiceUnderwriterIISupport.ActionProcess(data, 'Initialization');

                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/HT5OnLineServiceUnderwriterII.aspx?id=' + $('#HT5OnLineServiceUnderwriterIIFormId').val());
              
          

            });
    };




    this.ControlActions = function () {

        $('input:radio[name=ActionType]').change(function () {
         if ($('input:radio[name=ActionType]:checked').val() !== null) {
           app.core.SyncWebMethod("/fasi/dli/forms/HT5OnLineServiceUnderwriterIIActions.aspx/ActionTypeChange", false,
                 JSON.stringify({
                     instance: HT5OnLineServiceUnderwriterIISupport.InputToObject()
                 }),
                 function (data) {
                     HT5OnLineServiceUnderwriterIISupport.ActionProcess(data, 'ActionTypeChange');
             });
      }          
    });
        $('#button4').click(function (event) {
                var formInstance = $("#HT5OnLineServiceUnderwriterIIMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#button4'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/HT5OnLineServiceUnderwriterIIActions.aspx/button4Click", false,
                          JSON.stringify({
                                        instance: HT5OnLineServiceUnderwriterIISupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    HT5OnLineServiceUnderwriterIISupport.ActionProcess(data, 'button4Click');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('#button7').click(function (event) {
                var formInstance = $("#HT5OnLineServiceUnderwriterIIMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#button7'));
                    btnLoading.start();

                    app.core.SyncWebMethod("/fasi/dli/forms/HT5OnLineServiceUnderwriterIIActions.aspx/button7Click", false,
                          JSON.stringify({
                                        instance: HT5OnLineServiceUnderwriterIISupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    HT5OnLineServiceUnderwriterIISupport.ActionProcess(data, 'button7Click');
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
    
        $("#HT5OnLineServiceUnderwriterIIMainForm").validate({
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
                StartDate: {
                    required: true,
                    DatePicker: true
                },
                EndDate: {
                    required: true,
                    DatePicker: true
                }
            },
            messages: {
                StartDate: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                EndDate: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                }
            }
        });

    };
    this.LookUpForLineOfBusinessFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#LineOfBusiness>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForLineOfBusiness = function (defaultValue, source) {
        var ctrol = $('#LineOfBusiness');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/HT5OnLineServiceUnderwriterIIActions.aspx/LookUpForLineOfBusiness", false,
                JSON.stringify({ id: $('#HT5OnLineServiceUnderwriterIIFormId').val() }),
                function (data) {
                    ctrol.children().remove();
                        ctrol.append($('<option />').val(0).text(''));
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
    this.LookUpForProductFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            HT5OnLineServiceUnderwriterIISupport.LookUpForProduct(null, row.LineOfBusiness);
            result = $("#Product>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForProduct = function (defaultValue, value1, source) {
        var ctrol = $('#Product');
        var parentId1 = ctrol.data("parentId1");
        
        if ((typeof parentId1 == 'undefined' && typeof value1 !== 'undefined') || parentId1 !== value1) {
            ctrol.data("parentId1", value1);

            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));            
            
            app.core.SyncWebMethod("/fasi/dli/forms/HT5OnLineServiceUnderwriterIIActions.aspx/LookUpForProduct", false,
                JSON.stringify({
                                        id: $('#HT5OnLineServiceUnderwriterIIFormId').val(),
                    UnderwritingCaseCollectionLineOfBusiness: value1
                }),
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
					      if(source !== 'Initialization')
                    ctrol.change();
            }
    };
    this.LookUpForDecisionFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#Decision>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForDecision = function (defaultValue, source) {
        var ctrol = $('#Decision');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/HT5OnLineServiceUnderwriterIIActions.aspx/LookUpForDecision", false,
                JSON.stringify({ id: $('#HT5OnLineServiceUnderwriterIIFormId').val() }),
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
    this.LookUpForStatusFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#Status>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForStatus = function (defaultValue, source) {
        var ctrol = $('#Status');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/HT5OnLineServiceUnderwriterIIActions.aspx/LookUpForStatus", false,
                JSON.stringify({ id: $('#HT5OnLineServiceUnderwriterIIFormId').val() }),
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

    this.UnderwritingCaseTblSetup = function (table) {
        HT5OnLineServiceUnderwriterIISupport.LookUpForLineOfBusiness('');
        HT5OnLineServiceUnderwriterIISupport.LookUpForDecision('');
        HT5OnLineServiceUnderwriterIISupport.LookUpForStatus('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'UnderwritingCaseID',
            search: true,
            columns: [{
                field: 'UnderwritingCaseID',
                title: 'Caso',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'ReasonDescription',
                title: 'Contratante',
                sortable: false,
                halign: 'center'
            }, {
                field: 'OpenDate',
                title: 'Registro',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'LineOfBusiness',
                title: 'Ramo',
                formatter: 'HT5OnLineServiceUnderwriterIISupport.LookUpForLineOfBusinessFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'Product',
                title: 'Producto',
                formatter: 'HT5OnLineServiceUnderwriterIISupport.LookUpForProductFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'Decision',
                title: 'Decisión',
                formatter: 'HT5OnLineServiceUnderwriterIISupport.LookUpForDecisionFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'Status',
                title: 'Estado',
                formatter: 'HT5OnLineServiceUnderwriterIISupport.LookUpForStatusFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'OpenDate',
                title: 'F.reación',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }]
        });



    };


    this.UnderwritingCaseRowToInput = function (row) {
        HT5OnLineServiceUnderwriterIISupport.currentRow = row;
        $('#UnderwritingCaseID').val(row.UnderwritingCaseID);
        $('#ReasonDescription').val(row.ReasonDescription);
        $('#OpenDate').val(generalSupport.ToJavaScriptDateCustom(row.OpenDate, generalSupport.DateFormat()));
        HT5OnLineServiceUnderwriterIISupport.LookUpForLineOfBusiness(row.LineOfBusiness, '');
        HT5OnLineServiceUnderwriterIISupport.LookUpForProduct(row.Product, row.LineOfBusiness, '');
        HT5OnLineServiceUnderwriterIISupport.LookUpForDecision(row.Decision, '');
        HT5OnLineServiceUnderwriterIISupport.LookUpForStatus(row.Status, '');
        $('#OpenDateG').val(generalSupport.ToJavaScriptDateCustom(row.OpenDate, generalSupport.DateFormat()));

    };




};
$(function ($)
 {
    securitySupport.ValidateAccessRoles(['Suscriptor']);
});
$(document).ready(function () {
    moment.locale(generalSupport.UserContext().languageName);
    
   generalSupport.TranslateInit(generalSupport.GetCurrentName(), function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        tableHelperSupport.Translate('#UnderwritingCaseTbl', '#UnderwritingCaseTbl');

    });
        

    HT5OnLineServiceUnderwriterIISupport.ControlBehaviour();
    HT5OnLineServiceUnderwriterIISupport.ControlActions();
    HT5OnLineServiceUnderwriterIISupport.ValidateSetup();
    HT5OnLineServiceUnderwriterIISupport.Initialization();

    $("#UnderwritingCaseTblPlaceHolder").replaceWith('<table id="UnderwritingCaseTbl"></table>');
    HT5OnLineServiceUnderwriterIISupport.UnderwritingCaseTblSetup($('#UnderwritingCaseTbl'));





});

