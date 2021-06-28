var HT5HeartDiseaseQuestionnaireUWSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5HeartDiseaseQuestionnaireUWFormId').val(),
            ClientName: $('#ClientName').val(),
            uwcaseid: parseInt(0 + $('#uwcaseid').val(), 10),
            HeartDiseaseQuestionnaireCardiomyopathy: $('#Cardiomyopathy').is(':checked'),
            HeartDiseaseQuestionnaireIschaemicHeartDisease: $('#IschaemicHeartDisease').is(':checked'),
            HeartDiseaseQuestionnaireMitralOrOtherValve: $('#MitralOrOtherValve').is(':checked'),
            HeartDiseaseQuestionnaireOtherDiagnosis: $('#OtherDiagnosis').is(':checked'),
            HeartDiseaseQuestionnaireDetailsSpecificDiagnosis: $('#DetailsSpecificDiagnosis').val(),
            HeartDiseaseQuestionnaireSymptomsAccompaniedByOther: $('input:radio[name=SymptomsAccompaniedByOther]:checked').val(),
            HeartDiseaseQuestionnaireDescribeBodySymptoms: $('#DescribeBodySymptoms').val(),
            HeartDiseaseQuestionnaireDateSymptomsInitiallyOccur: $('#DateSymptomsInitiallyOccur').val() !== '' ? moment($('#DateSymptomsInitiallyOccur').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD'),
            HeartDiseaseQuestionnaireSymptomsRelatedWithEvent: $('input:radio[name=SymptomsRelatedWithEvent]:checked').val(),
            HeartDiseaseQuestionnaireFrequencyOfTheSymptoms: parseInt(0 + $('#FrequencyOfTheSymptoms').val(), 10),
            HeartDiseaseQuestionnaireDetailsEventRelated: $('#DetailsEventRelated').val(),
            HeartDiseaseQuestionnaireDuringOfTheSymptoms: parseInt(0 + $('#DuringOfTheSymptoms').val(), 10),
            HeartDiseaseQuestionnaireDateOfLastOccurrence: $('#DateOfLastOccurrence').val() !== '' ? moment($('#DateOfLastOccurrence').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD'),
            HeartDiseaseQuestionnaireSuddenly: $('#Suddenly').is(':checked'),
            HeartDiseaseQuestionnaireGradually: $('#Gradually').is(':checked'),
            HeartDiseaseQuestionnaireAtRest: $('#AtRest').is(':checked'),
            HeartDiseaseQuestionnaireOnlyOnPhysicalActivity: $('#OnlyOnPhysicalActivity').is(':checked'),
            HeartDiseaseQuestionnaireSymptomsBetter: $('#SymptomsBetter').is(':checked'),
            HeartDiseaseQuestionnaireSymptomsWorse: $('#SymptomsWorse').is(':checked'),
            HeartDiseaseQuestionnaireYesConsultedSpecialist: $('input:radio[name=YesConsultedSpecialist]:checked').val(),
            DetailsOfMedicalPractitioners_DetailsOfMedicalPractitioners: generalSupport.NormalizeProperties($('#DetailsOfMedicalPractitionersTbl').bootstrapTable('getData'), ''),
            HeartDiseaseQuestionnaireTypeTreatmentHad: parseInt(0 + $('#TypeTreatmentHad').val(), 10),
            HeartDiseaseQuestionnaireStillReceivingTreatment: $('input:radio[name=StillReceivingTreatment]:checked').val(),
            HeartDiseaseQuestionnaireDetailsStillRecievingTreatment: $('#DetailsStillRecievingTreatment').val(),
            HeartDiseaseQuestionnaireSurgeryOrInvestigationContemplated: $('input:radio[name=SurgeryOrInvestigationContemplated]:checked').val(),
            TreatmentPrescribed_TreatmentPrescribed: generalSupport.NormalizeProperties($('#TreatmentPrescribedTbl').bootstrapTable('getData'), ''),
            HeartDiseaseQuestionnaireCoronaryAngiogram: $('#CoronaryAngiogram').is(':checked'),
            HeartDiseaseQuestionnaireThalliumPerfusionScan: $('#ThalliumPerfusionScan').is(':checked'),
            HeartDiseaseQuestionnaireResting: $('#Resting').is(':checked'),
            HeartDiseaseQuestionnaireExercise: $('#Exercise').is(':checked'),
            HeartDiseaseQuestionnaireEndoscopy: $('#Endoscopy').is(':checked'),
            HeartDiseaseQuestionnaireEchocardiogram: $('#Echocardiogram').is(':checked'),
            HeartDiseaseQuestionnaireSestamibiStress: $('#SestamibiStress').is(':checked'),
            HeartDiseaseQuestionnaireOther: $('#Other').is(':checked'),
            HeartDiseaseQuestionnaireSpecifyOther: $('#SpecifyOther').val(),
            HeartDiseaseQuestionnaireYesRestrictedInLifeStyle: $('input:radio[name=YesRestrictedInLifeStyle]:checked').val(),
            DetailsAbsensesFromWork_DetailsAbsensesFromWork: generalSupport.NormalizeProperties($('#DetailsAbsensesFromWorkTbl').bootstrapTable('getData'), 'DateFrom,DateTo'),
            HeartDiseaseQuestionnaireAdditionalInformation: $('#AdditionalInformation').val(),
            HeartDiseaseQuestionnaireDateReceived: $('#DateReceived').val() !== '' ? moment($('#DateReceived').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD')
        };
        return data;
    };

    this.ObjectToInput = function (data) {
        $('#HT5HeartDiseaseQuestionnaireUWFormId').val(data.InstanceFormId);
        $('#ClientName').val(data.ClientName);
        $('#uwcaseid').val(data.uwcaseid);
        $('#Cardiomyopathy').prop("checked", data.HeartDiseaseQuestionnaireCardiomyopathy);
        $('#IschaemicHeartDisease').prop("checked", data.HeartDiseaseQuestionnaireIschaemicHeartDisease);
        $('#MitralOrOtherValve').prop("checked", data.HeartDiseaseQuestionnaireMitralOrOtherValve);
        $('#OtherDiagnosis').prop("checked", data.HeartDiseaseQuestionnaireOtherDiagnosis);
        $('#DetailsSpecificDiagnosis').val(data.HeartDiseaseQuestionnaireDetailsSpecificDiagnosis);
        if($('input:radio[name=SymptomsAccompaniedByOther][value=' + data.HeartDiseaseQuestionnaireSymptomsAccompaniedByOther +']').length===0)
           $('input:radio[name=SymptomsAccompaniedByOther]').prop('checked', false);
        else
           $($('input:radio[name=SymptomsAccompaniedByOther][value=' + data.HeartDiseaseQuestionnaireSymptomsAccompaniedByOther +']')).prop('checked', true);
        $('#SymptomsAccompaniedByOther').data('oldValue', data.HeartDiseaseQuestionnaireSymptomsAccompaniedByOther);
        $('#SymptomsAccompaniedByOther').val(data.HeartDiseaseQuestionnaireSymptomsAccompaniedByOther);

        $('#DescribeBodySymptoms').val(data.HeartDiseaseQuestionnaireDescribeBodySymptoms);
        $('#DateSymptomsInitiallyOccur').val(generalSupport.ToJavaScriptDateCustom(data.HeartDiseaseQuestionnaireDateSymptomsInitiallyOccur, 'DD/MM/YYYY'));
        if($('input:radio[name=SymptomsRelatedWithEvent][value=' + data.HeartDiseaseQuestionnaireSymptomsRelatedWithEvent +']').length===0)
           $('input:radio[name=SymptomsRelatedWithEvent]').prop('checked', false);
        else
           $($('input:radio[name=SymptomsRelatedWithEvent][value=' + data.HeartDiseaseQuestionnaireSymptomsRelatedWithEvent +']')).prop('checked', true);
        $('#SymptomsRelatedWithEvent').data('oldValue', data.HeartDiseaseQuestionnaireSymptomsRelatedWithEvent);
        $('#SymptomsRelatedWithEvent').val(data.HeartDiseaseQuestionnaireSymptomsRelatedWithEvent);

        $('#FrequencyOfTheSymptoms').data('oldValue', data.HeartDiseaseQuestionnaireFrequencyOfTheSymptoms);
        $('#FrequencyOfTheSymptoms').val(data.HeartDiseaseQuestionnaireFrequencyOfTheSymptoms);
        $('#DetailsEventRelated').val(data.HeartDiseaseQuestionnaireDetailsEventRelated);
        $('#DuringOfTheSymptoms').data('oldValue', data.HeartDiseaseQuestionnaireDuringOfTheSymptoms);
        $('#DuringOfTheSymptoms').val(data.HeartDiseaseQuestionnaireDuringOfTheSymptoms);
        $('#DateOfLastOccurrence').val(generalSupport.ToJavaScriptDateCustom(data.HeartDiseaseQuestionnaireDateOfLastOccurrence, 'DD/MM/YYYY'));
        $('#Suddenly').prop("checked", data.HeartDiseaseQuestionnaireSuddenly);
        $('#Gradually').prop("checked", data.HeartDiseaseQuestionnaireGradually);
        $('#AtRest').prop("checked", data.HeartDiseaseQuestionnaireAtRest);
        $('#OnlyOnPhysicalActivity').prop("checked", data.HeartDiseaseQuestionnaireOnlyOnPhysicalActivity);
        $('#SymptomsBetter').prop("checked", data.HeartDiseaseQuestionnaireSymptomsBetter);
        $('#SymptomsWorse').prop("checked", data.HeartDiseaseQuestionnaireSymptomsWorse);
        if($('input:radio[name=YesConsultedSpecialist][value=' + data.HeartDiseaseQuestionnaireYesConsultedSpecialist +']').length===0)
           $('input:radio[name=YesConsultedSpecialist]').prop('checked', false);
        else
           $($('input:radio[name=YesConsultedSpecialist][value=' + data.HeartDiseaseQuestionnaireYesConsultedSpecialist +']')).prop('checked', true);
        $('#YesConsultedSpecialist').data('oldValue', data.HeartDiseaseQuestionnaireYesConsultedSpecialist);
        $('#YesConsultedSpecialist').val(data.HeartDiseaseQuestionnaireYesConsultedSpecialist);

        $('#TypeTreatmentHad').data('oldValue', data.HeartDiseaseQuestionnaireTypeTreatmentHad);
        $('#TypeTreatmentHad').val(data.HeartDiseaseQuestionnaireTypeTreatmentHad);
        if($('input:radio[name=StillReceivingTreatment][value=' + data.HeartDiseaseQuestionnaireStillReceivingTreatment +']').length===0)
           $('input:radio[name=StillReceivingTreatment]').prop('checked', false);
        else
           $($('input:radio[name=StillReceivingTreatment][value=' + data.HeartDiseaseQuestionnaireStillReceivingTreatment +']')).prop('checked', true);
        $('#StillReceivingTreatment').data('oldValue', data.HeartDiseaseQuestionnaireStillReceivingTreatment);
        $('#StillReceivingTreatment').val(data.HeartDiseaseQuestionnaireStillReceivingTreatment);

        $('#DetailsStillRecievingTreatment').val(data.HeartDiseaseQuestionnaireDetailsStillRecievingTreatment);
        if($('input:radio[name=SurgeryOrInvestigationContemplated][value=' + data.HeartDiseaseQuestionnaireSurgeryOrInvestigationContemplated +']').length===0)
           $('input:radio[name=SurgeryOrInvestigationContemplated]').prop('checked', false);
        else
           $($('input:radio[name=SurgeryOrInvestigationContemplated][value=' + data.HeartDiseaseQuestionnaireSurgeryOrInvestigationContemplated +']')).prop('checked', true);
        $('#SurgeryOrInvestigationContemplated').data('oldValue', data.HeartDiseaseQuestionnaireSurgeryOrInvestigationContemplated);
        $('#SurgeryOrInvestigationContemplated').val(data.HeartDiseaseQuestionnaireSurgeryOrInvestigationContemplated);

        $('#CoronaryAngiogram').prop("checked", data.HeartDiseaseQuestionnaireCoronaryAngiogram);
        $('#ThalliumPerfusionScan').prop("checked", data.HeartDiseaseQuestionnaireThalliumPerfusionScan);
        $('#Resting').prop("checked", data.HeartDiseaseQuestionnaireResting);
        $('#Exercise').prop("checked", data.HeartDiseaseQuestionnaireExercise);
        $('#Endoscopy').prop("checked", data.HeartDiseaseQuestionnaireEndoscopy);
        $('#Echocardiogram').prop("checked", data.HeartDiseaseQuestionnaireEchocardiogram);
        $('#SestamibiStress').prop("checked", data.HeartDiseaseQuestionnaireSestamibiStress);
        $('#Other').prop("checked", data.HeartDiseaseQuestionnaireOther);
        $('#SpecifyOther').val(data.HeartDiseaseQuestionnaireSpecifyOther);
        if($('input:radio[name=YesRestrictedInLifeStyle][value=' + data.HeartDiseaseQuestionnaireYesRestrictedInLifeStyle +']').length===0)
           $('input:radio[name=YesRestrictedInLifeStyle]').prop('checked', false);
        else
           $($('input:radio[name=YesRestrictedInLifeStyle][value=' + data.HeartDiseaseQuestionnaireYesRestrictedInLifeStyle +']')).prop('checked', true);
        $('#YesRestrictedInLifeStyle').data('oldValue', data.HeartDiseaseQuestionnaireYesRestrictedInLifeStyle);
        $('#YesRestrictedInLifeStyle').val(data.HeartDiseaseQuestionnaireYesRestrictedInLifeStyle);

        $('#AdditionalInformation').val(data.HeartDiseaseQuestionnaireAdditionalInformation);
        $('#DateReceived').val(generalSupport.ToJavaScriptDateCustom(data.HeartDiseaseQuestionnaireDateReceived, 'DD/MM/YYYY'));


        if (data.DetailsOfMedicalPractitioners_DetailsOfMedicalPractitioners !== null)
            $('#DetailsOfMedicalPractitionersTbl').bootstrapTable('load', data.DetailsOfMedicalPractitioners_DetailsOfMedicalPractitioners);
        if (data.TreatmentPrescribed_TreatmentPrescribed !== null)
            $('#TreatmentPrescribedTbl').bootstrapTable('load', data.TreatmentPrescribed_TreatmentPrescribed);
        if (data.DetailsAbsensesFromWork_DetailsAbsensesFromWork !== null)
            $('#DetailsAbsensesFromWorkTbl').bootstrapTable('load', data.DetailsAbsensesFromWork_DetailsAbsensesFromWork);

    };

    this.ControlBehaviour = function () {







        $('#DateSymptomsInitiallyOccur_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });
        $('#DateOfLastOccurrence_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });
        $('#DateFrom_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });
        $('#DateTo_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });
        $('#DateReceived_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });


    };

    this.ActionProcess = function (data) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                HT5HeartDiseaseQuestionnaireUWSupport.ObjectToInput(data.d.Data);
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
            url: "/fasi/dli/forms/HT5HeartDiseaseQuestionnaireUWActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                id: $('#HT5HeartDiseaseQuestionnaireUWFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            success: function (data) {
                $.LoadingOverlay("hide");

                HT5HeartDiseaseQuestionnaireUWSupport.ActionProcess(data);
                
                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/HT5HeartDiseaseQuestionnaireUW.aspx?id=' + $('#HT5HeartDiseaseQuestionnaireUWFormId').val());
              
          

            },
            error: function (qXHR, textStatus, errorThrown) {
                $.LoadingOverlay("hide");
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };




    this.ControlActions = function () {

        $('#OtherDiagnosis').change(function () {

                if ($('#OtherDiagnosis').is(':checked') == true){
                    }                    
                    else {

                        }


        });
       $('input:radio[name=SymptomsAccompaniedByOther]').change(function () {

                if ($('input:radio[name=SymptomsAccompaniedByOther]:checked').val() == 'true'){
                    }                    
                    else {

                        }


        });
       $('input:radio[name=StillReceivingTreatment]').change(function () {

                if ($('input:radio[name=StillReceivingTreatment]:checked').val() == 'true'){
                    }                    
                    else {

                        }


        });
        $('#Other').change(function () {

                if ($('#Other').is(':checked') == true){
                    }                    
                    else {

                        }


        });
        $('#button8').click(function (event) {
            var formInstance = $("#HT5HeartDiseaseQuestionnaireUWMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#button8'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5HeartDiseaseQuestionnaireUWActions.aspx/button8Click",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5HeartDiseaseQuestionnaireUWSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5HeartDiseaseQuestionnaireUWSupport.ActionProcess(data);
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
        $('#button7').click(function (event) {
            var formInstance = $("#HT5HeartDiseaseQuestionnaireUWMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#button7'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5HeartDiseaseQuestionnaireUWActions.aspx/button7Click",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5HeartDiseaseQuestionnaireUWSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5HeartDiseaseQuestionnaireUWSupport.ActionProcess(data);
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
    
        $("#HT5HeartDiseaseQuestionnaireUWMainForm").validate({
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
                    required: true
                },
                DetailsSpecificDiagnosis: {
                    required: true
                },
                DescribeBodySymptoms: {
                    required: true
                },
                DetailsStillRecievingTreatment: {
                    required: true
                },
                SpecifyOther: {
                    required: true
                }
            },
            messages: {
                uwcaseid: {
                    required: 'El campo es requerido.'
                },
                DetailsSpecificDiagnosis: {
                    required: 'El campo es requerido'
                },
                DescribeBodySymptoms: {
                    required: 'El campo es requerido'
                },
                DetailsStillRecievingTreatment: {
                    required: 'El campo es requerido'
                },
                SpecifyOther: {
                    required: 'El campo es requerido'
                }
            }
        });
        $("#DetailsOfMedicalPractitionersEditForm").validate({
            rules: {


            },
            messages: {


            }
        });
        $("#TreatmentPrescribedEditForm").validate({
            rules: {


            },
            messages: {


            }
        });
        $("#DetailsAbsensesFromWorkEditForm").validate({
            rules: {


            },
            messages: {


            }
        });

    };

    this.DetailsOfMedicalPractitionersTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'IdDetails',
        toolbar: '#DetailsOfMedicalPractitionerstoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
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
                HT5HeartDiseaseQuestionnaireUWSupport.DetailsOfMedicalPractitionersRowToInput(row);
                
                
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
            HT5HeartDiseaseQuestionnaireUWSupport.DetailsOfMedicalPractitionersShowModal($('#DetailsOfMedicalPractitionersPopup').modal({ show: false }), $(this).attr('data-modal-title'));
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

                HT5HeartDiseaseQuestionnaireUWSupport.currentRow.PractitionerName = $('#PractitionerName').val();
                HT5HeartDiseaseQuestionnaireUWSupport.currentRow.PhonePractitioner = $('#PhonePractitioner').val();
                HT5HeartDiseaseQuestionnaireUWSupport.currentRow.eMailPractitioner = $('#eMailPractitioner').val();
                HT5HeartDiseaseQuestionnaireUWSupport.currentRow.AddresPractitioner = $('#AddresPractitioner').val();

                $('#DetailsOfMedicalPractitionersSaveBtn').prop('disabled', false);
                $('#DetailsOfMedicalPractitionersSaveBtn').html(caption);

                if (wm === 'Update') {
                    $('#DetailsOfMedicalPractitionersTbl').bootstrapTable('updateByUniqueId', { id: HT5HeartDiseaseQuestionnaireUWSupport.currentRow.IdDetails, row: HT5HeartDiseaseQuestionnaireUWSupport.currentRow });
                    $modal.modal('hide');
                }
                else {                    
                    $('#DetailsOfMedicalPractitionersTbl').bootstrapTable('append', HT5HeartDiseaseQuestionnaireUWSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });
    };

    this.DetailsOfMedicalPractitionersShowModal = function (md, title, row) {
        row = row || { PractitionerName: null, PhonePractitioner: null, eMailPractitioner: null, AddresPractitioner: null };

        md.data('id', row.IdDetails);
        md.find('.modal-title').text(title);

        HT5HeartDiseaseQuestionnaireUWSupport.DetailsOfMedicalPractitionersRowToInput(row);


        md.modal('show');
    };

    this.DetailsOfMedicalPractitionersRowToInput = function (row) {
        HT5HeartDiseaseQuestionnaireUWSupport.currentRow = row;
        $('#PractitionerName').val(row.PractitionerName);
        $('#PhonePractitioner').val(row.PhonePractitioner);
        $('#eMailPractitioner').val(row.eMailPractitioner);
        $('#AddresPractitioner').val(row.AddresPractitioner);

    };
    this.TreatmentPrescribedTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'id',
        toolbar: '#TreatmentPrescribedtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'DatePeriod',
                title: 'Fecha',
                events: 'TreatmentPrescribedActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'Dosage',
                title: 'Dosis',
                sortable: false,
                halign: 'center'
            }, {
                field: 'NameOfMedication',
                title: 'Medicamento',
                sortable: false,
                halign: 'center'
            }]
        });


        $('#TreatmentPrescribedTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TreatmentPrescribedTbl');
            $('#TreatmentPrescribedRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TreatmentPrescribedRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TreatmentPrescribedTbl').bootstrapTable('getSelections'), function (row) {		
                HT5HeartDiseaseQuestionnaireUWSupport.TreatmentPrescribedRowToInput(row);
                
                
                return row.id;
            });
            
          $('#TreatmentPrescribedTbl').bootstrapTable('remove', {
                field: 'id',
                values: ids
           });

            $('#TreatmentPrescribedRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TreatmentPrescribedCreateBtn').click(function () {
            var formInstance = $("#TreatmentPrescribedEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            HT5HeartDiseaseQuestionnaireUWSupport.TreatmentPrescribedShowModal($('#TreatmentPrescribedPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TreatmentPrescribedPopup').find('#TreatmentPrescribedSaveBtn').click(function () {
            var formInstance = $("#TreatmentPrescribedEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TreatmentPrescribedPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TreatmentPrescribedSaveBtn').html();
                $('#TreatmentPrescribedSaveBtn').html('Procesando...');
                $('#TreatmentPrescribedSaveBtn').prop('disabled', true);

                HT5HeartDiseaseQuestionnaireUWSupport.currentRow.DatePeriod = $('#DatePeriod').val();
                HT5HeartDiseaseQuestionnaireUWSupport.currentRow.Dosage = $('#Dosage').val();
                HT5HeartDiseaseQuestionnaireUWSupport.currentRow.NameOfMedication = $('#NameOfMedication').val();

                $('#TreatmentPrescribedSaveBtn').prop('disabled', false);
                $('#TreatmentPrescribedSaveBtn').html(caption);

                if (wm === 'Update') {
                    $('#TreatmentPrescribedTbl').bootstrapTable('updateByUniqueId', { id: HT5HeartDiseaseQuestionnaireUWSupport.currentRow.id, row: HT5HeartDiseaseQuestionnaireUWSupport.currentRow });
                    $modal.modal('hide');
                }
                else {                    
                    $('#TreatmentPrescribedTbl').bootstrapTable('append', HT5HeartDiseaseQuestionnaireUWSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });
    };

    this.TreatmentPrescribedShowModal = function (md, title, row) {
        row = row || { DatePeriod: null, Dosage: null, NameOfMedication: null };

        md.data('id', row.id);
        md.find('.modal-title').text(title);

        HT5HeartDiseaseQuestionnaireUWSupport.TreatmentPrescribedRowToInput(row);


        md.modal('show');
    };

    this.TreatmentPrescribedRowToInput = function (row) {
        HT5HeartDiseaseQuestionnaireUWSupport.currentRow = row;
        $('#DatePeriod').val(row.DatePeriod);
        $('#Dosage').val(row.Dosage);
        $('#NameOfMedication').val(row.NameOfMedication);

    };
    this.DetailsAbsensesFromWorkTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'id',
        toolbar: '#DetailsAbsensesFromWorktoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'DateFrom',
                title: 'Desde',
                events: 'DetailsAbsensesFromWorkActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DateTo',
                title: 'Hasta',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'Details',
                title: 'Detalles',
                sortable: false,
                halign: 'center'
            }]
        });


        $('#DetailsAbsensesFromWorkTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#DetailsAbsensesFromWorkTbl');
            $('#DetailsAbsensesFromWorkRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#DetailsAbsensesFromWorkRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#DetailsAbsensesFromWorkTbl').bootstrapTable('getSelections'), function (row) {		
                HT5HeartDiseaseQuestionnaireUWSupport.DetailsAbsensesFromWorkRowToInput(row);
                
                
                return row.id;
            });
            
          $('#DetailsAbsensesFromWorkTbl').bootstrapTable('remove', {
                field: 'id',
                values: ids
           });

            $('#DetailsAbsensesFromWorkRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#DetailsAbsensesFromWorkCreateBtn').click(function () {
            var formInstance = $("#DetailsAbsensesFromWorkEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            HT5HeartDiseaseQuestionnaireUWSupport.DetailsAbsensesFromWorkShowModal($('#DetailsAbsensesFromWorkPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#DetailsAbsensesFromWorkPopup').find('#DetailsAbsensesFromWorkSaveBtn').click(function () {
            var formInstance = $("#DetailsAbsensesFromWorkEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#DetailsAbsensesFromWorkPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#DetailsAbsensesFromWorkSaveBtn').html();
                $('#DetailsAbsensesFromWorkSaveBtn').html('Procesando...');
                $('#DetailsAbsensesFromWorkSaveBtn').prop('disabled', true);

                HT5HeartDiseaseQuestionnaireUWSupport.currentRow.DateFrom = $('#DateFrom').val() !== '' ? moment($('#DateFrom').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD');
                HT5HeartDiseaseQuestionnaireUWSupport.currentRow.DateTo = $('#DateTo').val() !== '' ? moment($('#DateTo').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD');
                HT5HeartDiseaseQuestionnaireUWSupport.currentRow.Details = $('#Details').val();

                $('#DetailsAbsensesFromWorkSaveBtn').prop('disabled', false);
                $('#DetailsAbsensesFromWorkSaveBtn').html(caption);

                if (wm === 'Update') {
                    $('#DetailsAbsensesFromWorkTbl').bootstrapTable('updateByUniqueId', { id: HT5HeartDiseaseQuestionnaireUWSupport.currentRow.id, row: HT5HeartDiseaseQuestionnaireUWSupport.currentRow });
                    $modal.modal('hide');
                }
                else {                    
                    $('#DetailsAbsensesFromWorkTbl').bootstrapTable('append', HT5HeartDiseaseQuestionnaireUWSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });
    };

    this.DetailsAbsensesFromWorkShowModal = function (md, title, row) {
        row = row || { DateFrom: null, DateTo: null, Details: null };

        md.data('id', row.id);
        md.find('.modal-title').text(title);

        HT5HeartDiseaseQuestionnaireUWSupport.DetailsAbsensesFromWorkRowToInput(row);


        md.modal('show');
    };

    this.DetailsAbsensesFromWorkRowToInput = function (row) {
        HT5HeartDiseaseQuestionnaireUWSupport.currentRow = row;
        $('#DateFrom').val(generalSupport.ToJavaScriptDateCustom(row.DateFrom, 'DD/MM/YYYY'));
        $('#DateTo').val(generalSupport.ToJavaScriptDateCustom(row.DateTo, 'DD/MM/YYYY'));
        $('#Details').val(row.Details);

    };




};

$(document).ready(function () {
    moment.locale('es');
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('HT5Cuestionario de enfermedades del corazón');
        

    HT5HeartDiseaseQuestionnaireUWSupport.ControlBehaviour();
    HT5HeartDiseaseQuestionnaireUWSupport.ControlActions();
    HT5HeartDiseaseQuestionnaireUWSupport.ValidateSetup();
    HT5HeartDiseaseQuestionnaireUWSupport.Initialization();

    $("#DetailsOfMedicalPractitionersTblPlaceHolder").replaceWith('<table id="DetailsOfMedicalPractitionersTbl"><caption >Si su respuesta es SI, por favor especifique</caption></table>');
    HT5HeartDiseaseQuestionnaireUWSupport.DetailsOfMedicalPractitionersTblSetup($('#DetailsOfMedicalPractitionersTbl'));
    $("#TreatmentPrescribedTblPlaceHolder").replaceWith('<table id="TreatmentPrescribedTbl"><caption >14. Por favor escriba todos los medicamentos, no mencionados anteriormente en este cuestionario, que usted esta tomando con regulridad o de maneraa intermitente, ya sea para esta o cualquier otra condición o enfermedad</caption></table>');
    HT5HeartDiseaseQuestionnaireUWSupport.TreatmentPrescribedTblSetup($('#TreatmentPrescribedTbl'));
    $("#DetailsAbsensesFromWorkTblPlaceHolder").replaceWith('<table id="DetailsAbsensesFromWorkTbl"><caption >Si si respuesta es SI, por favor especifique</caption></table>');
    HT5HeartDiseaseQuestionnaireUWSupport.DetailsAbsensesFromWorkTblSetup($('#DetailsAbsensesFromWorkTbl'));




});

window.DetailsOfMedicalPractitionersActionEvents = {
    'click .update': function (e, value, row, index) {
        HT5HeartDiseaseQuestionnaireUWSupport.DetailsOfMedicalPractitionersShowModal($('#DetailsOfMedicalPractitionersPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.TreatmentPrescribedActionEvents = {
    'click .update': function (e, value, row, index) {
        HT5HeartDiseaseQuestionnaireUWSupport.TreatmentPrescribedShowModal($('#TreatmentPrescribedPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.DetailsAbsensesFromWorkActionEvents = {
    'click .update': function (e, value, row, index) {
        HT5HeartDiseaseQuestionnaireUWSupport.DetailsAbsensesFromWorkShowModal($('#DetailsAbsensesFromWorkPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
