var HT5DiabetesQuestionnaireUWSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5DiabetesQuestionnaireUWFormId').val(),
            ClientName: $('#ClientName').val(),
            uwcaseid: generalSupport.NumericValue('#uwcaseid', 0, 99999),
            DiabetesAgeOnSetDiabetes: generalSupport.NumericValue('#AgeOnSetDiabetes', 0, 999),
            DiabetesDietMethod: $('#DietMethod').is(':checked'),
            DiabetesExerciseMethod: $('#ExerciseMethod').is(':checked'),
            DiabetesInsulinInjections: $('#InsulinInjections').is(':checked'),
            DiabetesInsulinPump: $('#InsulinPump').is(':checked'),
            DiabetesOtherMethod: $('#OtherMethod').val(),
            DiabetesEKGAbnormality: $('#EKGAbnormality').is(':checked'),
            DiabetesDiabeticComa: $('#DiabeticComa').is(':checked'),
            DiabetesEyeTrouble: $('#EyeTrouble').is(':checked'),
            DiabetesProteinInUrine: $('#ProteinInUrine').is(':checked'),
            DiabetesSkinUlceration: $('#SkinUlceration').is(':checked'),
            DiabetesAmputation: $('#Amputation').is(':checked'),
            DiabetesNeuropathy: $('#Neuropathy').is(':checked'),
            DiabetesInsulinReaction: $('#InsulinReaction').is(':checked'),
            DiabetesOther: $('#Other').val(),
            DiabetesFrenquencyMonitorBloodSugerLevel: parseInt(0 + $('#FrenquencyMonitorBloodSugerLevel').val(), 10),
            DiabetesMostRecentReadingSugarLevel: $('#MostRecentReadingSugarLevel').val(),
            DiabetesMostRecentReadingBloodPressureDiastolic: $('#MostRecentReadingBloodPressure').val(),
            DiabetesDateLastVisitPhysician: generalSupport.DatePickerValueInputToObject('#DateLastVisitPhysician'),
            DetailsOfMedicalPractitioners_DetailsOfMedicalPractitioners: generalSupport.NormalizeProperties($('#DetailsOfMedicalPractitionersTbl').bootstrapTable('getData'), ''),
            DiabetesCholesterolBelow200: $('input:radio[name=CholesterolBelow200]:checked').val(),
            DiabetesAdditionalInformation: $('#AdditionalInformation').val(),
            DiabetesDateReceived: generalSupport.DatePickerValueInputToObject('#DateReceived')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#HT5DiabetesQuestionnaireUWFormId').val(data.InstanceFormId);
        $('#ClientName').val(data.ClientName);
        AutoNumeric.set('#uwcaseid', data.uwcaseid);
        AutoNumeric.set('#AgeOnSetDiabetes', data.DiabetesAgeOnSetDiabetes);
        $('#DietMethod').prop("checked", data.DiabetesDietMethod);
        $('#ExerciseMethod').prop("checked", data.DiabetesExerciseMethod);
        $('#InsulinInjections').prop("checked", data.DiabetesInsulinInjections);
        $('#InsulinPump').prop("checked", data.DiabetesInsulinPump);
        $('#OtherMethod').val(data.DiabetesOtherMethod);
        $('#EKGAbnormality').prop("checked", data.DiabetesEKGAbnormality);
        $('#DiabeticComa').prop("checked", data.DiabetesDiabeticComa);
        $('#EyeTrouble').prop("checked", data.DiabetesEyeTrouble);
        $('#ProteinInUrine').prop("checked", data.DiabetesProteinInUrine);
        $('#SkinUlceration').prop("checked", data.DiabetesSkinUlceration);
        $('#Amputation').prop("checked", data.DiabetesAmputation);
        $('#Neuropathy').prop("checked", data.DiabetesNeuropathy);
        $('#InsulinReaction').prop("checked", data.DiabetesInsulinReaction);
        $('#Other').val(data.DiabetesOther);
        $('#FrenquencyMonitorBloodSugerLevel').data('oldValue', data.DiabetesFrenquencyMonitorBloodSugerLevel);
        $('#FrenquencyMonitorBloodSugerLevel').val(data.DiabetesFrenquencyMonitorBloodSugerLevel);
        $('#MostRecentReadingSugarLevel').val(data.DiabetesMostRecentReadingSugarLevel);
        $('#MostRecentReadingBloodPressure').val(data.DiabetesMostRecentReadingBloodPressureDiastolic);
        $('#DateLastVisitPhysician').val(generalSupport.ToJavaScriptDateCustom(data.DiabetesDateLastVisitPhysician, generalSupport.DateFormat()));
        if($('input:radio[name=CholesterolBelow200][value=' + data.DiabetesCholesterolBelow200 +']').length===0)
           $('input:radio[name=CholesterolBelow200]').prop('checked', false);
        else
           $($('input:radio[name=CholesterolBelow200][value=' + data.DiabetesCholesterolBelow200 +']')).prop('checked', true);
        $('#CholesterolBelow200').data('oldValue', data.DiabetesCholesterolBelow200);
        $('#CholesterolBelow200').val(data.DiabetesCholesterolBelow200);

        $('#AdditionalInformation').val(data.DiabetesAdditionalInformation);
        $('#DateReceived').val(generalSupport.ToJavaScriptDateCustom(data.DiabetesDateReceived, generalSupport.DateFormat()));


        if (data.DetailsOfMedicalPractitioners_DetailsOfMedicalPractitioners !== null)
            $('#DetailsOfMedicalPractitionersTbl').bootstrapTable('load', data.DetailsOfMedicalPractitioners_DetailsOfMedicalPractitioners);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#uwcaseid', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#AgeOnSetDiabetes', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#AddresPractitioner', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: 0
        });




        $('#DateLastVisitPhysician_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateLastVisitPhysician_group');
        $('#DateReceived_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateReceived_group');


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               if (source == 'Initialization')
					         HT5DiabetesQuestionnaireUWSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   HT5DiabetesQuestionnaireUWSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        app.core.AsyncWebMethod("/fasi/dli/forms/HT5DiabetesQuestionnaireUWActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), true,
            JSON.stringify({
                id: $('#HT5DiabetesQuestionnaireUWFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
                if (data.d.Success)
                    $('#HT5DiabetesQuestionnaireUWFormId').val(data.d.Data.Instance.InstanceFormId);
                    
                if (data.d.Success === true && data.d.Data.LookUps) {
                    data.d.Data.LookUps.forEach(function (elementSource) {
                        generalSupport.RenderLookUp(elementSource.Key, data.d.Data.Instance[elementSource.Key], 'Initialization', elementSource.Items);
                    });
                }
                


    $("#DetailsOfMedicalPractitionersTblPlaceHolder").replaceWith('<table id="DetailsOfMedicalPractitionersTbl"><caption>Detalles de los médicos</caption></table>');
    HT5DiabetesQuestionnaireUWSupport.DetailsOfMedicalPractitionersTblSetup($('#DetailsOfMedicalPractitionersTbl'));





                HT5DiabetesQuestionnaireUWSupport.ActionProcess(data, 'Initialization');

                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)                    
                    window.history.replaceState({}, null, '/fasi/dli/forms/' + location.pathname.substring(location.pathname.lastIndexOf("/") + 1) + '?id=' + $('#HT5DiabetesQuestionnaireUWFormId').val());
 
              
          

            });
    };




    this.ControlActions =   function () {

        $('#save').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#save'));
                btnLoading.start();

            app.core.AsyncWebMethod("/fasi/dli/forms/HT5DiabetesQuestionnaireUWActions.aspx/saveClick", false,
                JSON.stringify({
                    instance: HT5DiabetesQuestionnaireUWSupport.InputToObject()
                }),
                function (data) {
                    btnLoading.stop();

                    HT5DiabetesQuestionnaireUWSupport.ActionProcess(data, 'saveClick');
                },
                function () {
                    btnLoading.stop();
                });
            event.preventDefault();
        });
        $('#submit').click(function (event) {
                var formInstance = $("#HT5DiabetesQuestionnaireUWMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#submit'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/HT5DiabetesQuestionnaireUWActions.aspx/submitClick", false,
                          JSON.stringify({
                                        instance: HT5DiabetesQuestionnaireUWSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    HT5DiabetesQuestionnaireUWSupport.ActionProcess(data, 'submitClick');
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


        $("#HT5DiabetesQuestionnaireUWMainForm").validate({
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
                    maxlength: 30
                },
                uwcaseid: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 99999
                },
                AgeOnSetDiabetes: {
                    AutoNumericRequired: true,
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999
                },
                OtherMethod: {
                    maxlength: 30
                },
                Other: {
                    maxlength: 30
                },
                FrenquencyMonitorBloodSugerLevel: {
                    required: true                },
                MostRecentReadingSugarLevel: {
                    required: true,
                    maxlength: 15
                },
                MostRecentReadingBloodPressure: {
                    required: true,
                    maxlength: 15
                },
                DateLastVisitPhysician: {
                    required: true,
                    DatePicker: true
                },
                CholesterolBelow200: {
                },
                AdditionalInformation: {
                    maxlength: 0
                },
                DateReceived: {
                    required: true,
                    DatePicker: true
                }
            },
            messages: {
                ClientName: {
                    maxlength: 'El campo permite 30 caracteres máximo'
                },
                uwcaseid: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999'
                },
                AgeOnSetDiabetes: {
                    AutoNumericRequired: 'El campo es requerido.',
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999'
                },
                OtherMethod: {
                    maxlength: 'El campo permite 30 caracteres máximo'
                },
                Other: {
                    maxlength: 'El campo permite 30 caracteres máximo'
                },
                FrenquencyMonitorBloodSugerLevel: {
                    required: 'El campo es requerido.'                },
                MostRecentReadingSugarLevel: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                MostRecentReadingBloodPressure: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                DateLastVisitPhysician: {
                    required: 'El campo es requerido.',
                    DatePicker: 'La fecha indicada no es válida'
                },
                CholesterolBelow200: {
                },
                AdditionalInformation: {
                    maxlength: 'El campo permite 0 caracteres máximo'
                },
                DateReceived: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                }
            }
        });
        $("#DetailsOfMedicalPractitionersEditForm").validate().destroy();
        $("#DetailsOfMedicalPractitionersEditForm").validate({
            rules: {
                PractitionerName: {
                    maxlength: 35
                },
                PhonePractitioner: {
                    maxlength: 15
                },
                eMailPractitioner: {
                    maxlength: 50
                },
                AddresPractitioner: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 99999
                }

            },
            messages: {
                PractitionerName: {
                    maxlength: 'El campo permite 35 caracteres máximo'
                },
                PhonePractitioner: {
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                eMailPractitioner: {
                    maxlength: 'El campo permite 50 caracteres máximo'
                },
                AddresPractitioner: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999'
                }

            }
        });

    };

    this.DetailsOfMedicalPractitionersTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'IdDetails',
            toolbar: '#DetailsOfMedicalPractitionerstoolbar',
            columns: [{
                field: 'selected',
                checkbox: true,
                formatter: 'HT5DiabetesQuestionnaireUWSupport.selected_Formatter'
            }, {
                field: 'PractitionerName',
                title: 'Nombre',
                events: 'DetailsOfMedicalPractitionersActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'PhonePractitioner',
                title: 'Teléfono',
                sortable: false,
                halign: 'center'
            }, {
                field: 'eMailPractitioner',
                title: 'eMail',
                sortable: false,
                halign: 'center'
            }, {
                field: 'AddresPractitioner',
                title: 'Dirección',
                formatter: 'HT5DiabetesQuestionnaireUWSupport.AddresPractitioner_FormatterMaskData',
                sortable: false,
                halign: 'center'
            }]
        });


        $('#DetailsOfMedicalPractitionersTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#DetailsOfMedicalPractitionersTbl');
            $('#DetailsOfMedicalPractitionersRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#DetailsOfMedicalPractitionersRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#DetailsOfMedicalPractitionersTbl').bootstrapTable('getSelections'), function (row) {		
                HT5DiabetesQuestionnaireUWSupport.DetailsOfMedicalPractitionersRowToInput(row);
                
                
                return row.IdDetails;
            });
            
          $('#DetailsOfMedicalPractitionersTbl').bootstrapTable('remove', {
                field: 'IdDetails',
                values: ids
           });

            $('#DetailsOfMedicalPractitionersRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#DetailsOfMedicalPractitionersCreateBtn').click(function () {
            var formInstance = $("#DetailsOfMedicalPractitionersEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            HT5DiabetesQuestionnaireUWSupport.DetailsOfMedicalPractitionersShowModal($('#DetailsOfMedicalPractitionersPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#DetailsOfMedicalPractitionersPopup').find('#DetailsOfMedicalPractitionersSaveBtn').click(function () {
            var formInstance = $("#DetailsOfMedicalPractitionersEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#DetailsOfMedicalPractitionersPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#DetailsOfMedicalPractitionersSaveBtn').html();
                $('#DetailsOfMedicalPractitionersSaveBtn').html('Procesando...');
                $('#DetailsOfMedicalPractitionersSaveBtn').prop('disabled', true);

                HT5DiabetesQuestionnaireUWSupport.currentRow.PractitionerName = $('#PractitionerName').val();
                HT5DiabetesQuestionnaireUWSupport.currentRow.PhonePractitioner = $('#PhonePractitioner').val();
                HT5DiabetesQuestionnaireUWSupport.currentRow.eMailPractitioner = $('#eMailPractitioner').val();
                HT5DiabetesQuestionnaireUWSupport.currentRow.AddresPractitioner = generalSupport.NumericValue('#AddresPractitioner', 0, 99999);

                $('#DetailsOfMedicalPractitionersSaveBtn').prop('disabled', false);
                $('#DetailsOfMedicalPractitionersSaveBtn').html(caption);

                if (wm === 'Update') {
                    $('#DetailsOfMedicalPractitionersTbl').bootstrapTable('updateByUniqueId', { id: HT5DiabetesQuestionnaireUWSupport.currentRow.IdDetails, row: HT5DiabetesQuestionnaireUWSupport.currentRow });
                    $modal.modal('hide');
                }
                else {                    
                    $('#DetailsOfMedicalPractitionersTbl').bootstrapTable('append', HT5DiabetesQuestionnaireUWSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.DetailsOfMedicalPractitionersShowModal = function (md, title, row) {
        var formInstance = $("#DetailsOfMedicalPractitionersEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { PractitionerName: '', PhonePractitioner: '', eMailPractitioner: '', AddresPractitioner: 0 };

        md.data('id', row.IdDetails);
        md.find('.modal-title').text(title);

        HT5DiabetesQuestionnaireUWSupport.DetailsOfMedicalPractitionersRowToInput(row);


        md.appendTo("body");
        md.modal('show');
    };

    this.DetailsOfMedicalPractitionersRowToInput = function (row) {
        HT5DiabetesQuestionnaireUWSupport.currentRow = row;
        $('#PractitionerName').val(row.PractitionerName);
        $('#PhonePractitioner').val(row.PhonePractitioner);
        $('#eMailPractitioner').val(row.eMailPractitioner);
        AutoNumeric.set('#AddresPractitioner', row.AddresPractitioner);

    };





    this.AddresPractitioner_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      };


	    this.selected_Formatter = function (value, row, index) {          
          return {
                  disabled: $('#DetailsOfMedicalPractitionersTbl *').prop('disabled'),
                  checked: value
                 }
      };



  this.Init = function(){
    
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('');
        

    HT5DiabetesQuestionnaireUWSupport.ControlBehaviour();
    HT5DiabetesQuestionnaireUWSupport.ControlActions();
    HT5DiabetesQuestionnaireUWSupport.ValidateSetup();
    HT5DiabetesQuestionnaireUWSupport.Initialization();


  };
};

$(document).ready(function () {
   HT5DiabetesQuestionnaireUWSupport.Init();
});

window.DetailsOfMedicalPractitionersActionEvents = {
    'click .update': function (e, value, row, index) {
        HT5DiabetesQuestionnaireUWSupport.DetailsOfMedicalPractitionersShowModal($('#DetailsOfMedicalPractitionersPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
