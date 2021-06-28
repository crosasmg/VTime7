var HT5CancerQuestionnaireUWSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5CancerQuestionnaireUWFormId').val(),
            ClientName: $('#ClientName').val(),
            uwcaseid: parseInt(0 + $('#uwcaseid').val(), 10),
            CancerTypeOfCancer: parseInt(0 + $('#TypeOfCancer').val(), 10),
            CancerDateDiagnosed: generalSupport.DatePickerValueInputToObject('#DateDiagnosed'),
            CancerStageOfCancer: parseInt(0 + $('#StageOfCancer').val(), 10),
            CancerScaleColonRectalCancer: parseInt(0 + $('#ScaleColonRectalCancer').val(), 10),
            CancerLevelMelanoma: parseInt(0 + $('#LevelMelanoma').val(), 10),
            CancerGradeProstateCancer: parseInt(0 + $('#GradeProstateCancer').val(), 10),
            CancerEvidenceOfRecurringCancer: $('input:radio[name=EvidenceOfRecurringCancer]:checked').val(),
            CancerDateEvidence: generalSupport.DatePickerValueInputToObject('#DateEvidence'),
            CancerLocationRecurringCancer: $('#LocationRecurringCancer').val(),
            CancerDateInitiallyTreated: generalSupport.DatePickerValueInputToObject('#DateInitiallyTreated'),
            CancerDateLastTreated: generalSupport.DatePickerValueInputToObject('#DateLastTreated'),
            CancerDateLastSeenByDoctor: generalSupport.DatePickerValueInputToObject('#DateLastSeenByDoctor'),
            CancerGrowthMayBeMalignant: $('input:radio[name=GrowthMayBeMalignant]:checked').val(),
            CancerDetailsEvidence: $('#DetailsEvidence').val(),
            CancerRemovedGrowth: $('input:radio[name=RemovedGrowth]:checked').val(),
            CancerDetailsInvestigations: $('#DetailsInvestigations').val(),
            CancerChemotherapyTreatmentFollowing: $('#ChemotherapyTreatmentFollowing').is(':checked'),
            CancerRadiotherapyTreatmentFollowing: $('#RadiotherapyTreatmentFollowing').is(':checked'),
            CancerMedicationTreatmentFollowing: $('#MedicationTreatmentFollowing').is(':checked'),
            CancerOtherTreatmentFollowing: $('#OtherTreatmentFollowing').val(),
            CancerStillFollowedUp: $('input:radio[name=StillFollowedUp]:checked').val(),
            CancerHowOften: $('#HowOften').val(),
            CancerDateOfDischargedFollowUp: generalSupport.DatePickerValueInputToObject('#DateOfDischargedFollowUp'),
            CancerSurgeryTreatment: $('#SurgeryTreatment').is(':checked'),
            CancerDateSurgeryTreatment: generalSupport.DatePickerValueInputToObject('#DateSurgeryTreatment'),
            CancerChemotherapyTreatment: $('#ChemotherapyTreatment').is(':checked'),
            CancerDateChemotherapyTreatment: generalSupport.DatePickerValueInputToObject('#DateChemotherapyTreatment'),
            CancerRadiationTreatment: $('#RadiationTreatment').is(':checked'),
            CancerDateRadiationTreatment: generalSupport.DatePickerValueInputToObject('#DateRadiationTreatment'),
            CancerHormoneTreatment: $('#HormoneTreatment').is(':checked'),
            CancerDateHormoneTreatment: generalSupport.DatePickerValueInputToObject('#DateHormoneTreatment'),
            CancerOtherTreatment: $('#OtherTreatment').is(':checked'),
            CancerDateOtherTreatment: generalSupport.DatePickerValueInputToObject('#DateOtherTreatment'),
            CancerNameTreatment: $('#NameTreatment').val(),
            CancerPrognosis: $('input:radio[name=Prognosis]:checked').val(),
            CancerDetailsPrognosis: $('#DetailsPrognosis').val(),
            CancerChemotherapyCurrentlyTreatment: $('#ChemotherapyCurrentlyTreatment').is(':checked'),
            CancerRadiationCurrentlyTreatment: $('#RadiationCurrentlyTreatment').is(':checked'),
            CancerHormoneCurrentlyTreatment: $('#HormoneCurrentlyTreatment').is(':checked'),
            CancerMedicationCurrentlyTreatment: $('#MedicationCurrentlyTreatment').is(':checked'),
            CancerOtherCurrentlyTreatment: $('#OtherCurrentlyTreatment').val(),
            CancerDetailsCurrentlyTreatment: $('#DetailsCurrentlyTreatment').val(),
            CancerYesRestrictedLifeStyle: $('input:radio[name=YesRestrictedLifeStyle]:checked').val(),
            DetailsAbsensesFromWork_DetailsAbsensesFromWork: generalSupport.NormalizeProperties($('#DetailsAbsensesFromWorkTbl').bootstrapTable('getData'), 'DateFrom,DateTo'),
            DetailsOfMedicalPractitioners_DetailsOfMedicalPractitioners: generalSupport.NormalizeProperties($('#DetailsOfMedicalPractitionersTbl').bootstrapTable('getData'), ''),
            CancerAdditionalInformation: $('#AdditionalInformation').val(),
            CancerDateReceived: generalSupport.DatePickerValueInputToObject('#DateReceived')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#HT5CancerQuestionnaireUWFormId').val(data.InstanceFormId);
        $('#ClientName').val(data.ClientName);
        $('#uwcaseid').val(data.uwcaseid);
        $('#TypeOfCancer').data('oldValue', data.CancerTypeOfCancer);
        $('#TypeOfCancer').val(data.CancerTypeOfCancer);
        $('#DateDiagnosed').val(generalSupport.ToJavaScriptDateCustom(data.CancerDateDiagnosed, generalSupport.DateFormat()));
        $('#StageOfCancer').data('oldValue', data.CancerStageOfCancer);
        $('#StageOfCancer').val(data.CancerStageOfCancer);
        $('#ScaleColonRectalCancer').data('oldValue', data.CancerScaleColonRectalCancer);
        $('#ScaleColonRectalCancer').val(data.CancerScaleColonRectalCancer);
        $('#LevelMelanoma').data('oldValue', data.CancerLevelMelanoma);
        $('#LevelMelanoma').val(data.CancerLevelMelanoma);
        $('#GradeProstateCancer').data('oldValue', data.CancerGradeProstateCancer);
        $('#GradeProstateCancer').val(data.CancerGradeProstateCancer);
        if($('input:radio[name=EvidenceOfRecurringCancer][value=' + data.CancerEvidenceOfRecurringCancer +']').length===0)
           $('input:radio[name=EvidenceOfRecurringCancer]').prop('checked', false);
        else
           $($('input:radio[name=EvidenceOfRecurringCancer][value=' + data.CancerEvidenceOfRecurringCancer +']')).prop('checked', true);
        $('#EvidenceOfRecurringCancer').data('oldValue', data.CancerEvidenceOfRecurringCancer);
        $('#EvidenceOfRecurringCancer').val(data.CancerEvidenceOfRecurringCancer);

        $('#DateEvidence').val(generalSupport.ToJavaScriptDateCustom(data.CancerDateEvidence, generalSupport.DateFormat()));
        $('#LocationRecurringCancer').val(data.CancerLocationRecurringCancer);
        $('#DateInitiallyTreated').val(generalSupport.ToJavaScriptDateCustom(data.CancerDateInitiallyTreated, generalSupport.DateFormat()));
        $('#DateLastTreated').val(generalSupport.ToJavaScriptDateCustom(data.CancerDateLastTreated, generalSupport.DateFormat()));
        $('#DateLastSeenByDoctor').val(generalSupport.ToJavaScriptDateCustom(data.CancerDateLastSeenByDoctor, generalSupport.DateFormat()));
        if($('input:radio[name=GrowthMayBeMalignant][value=' + data.CancerGrowthMayBeMalignant +']').length===0)
           $('input:radio[name=GrowthMayBeMalignant]').prop('checked', false);
        else
           $($('input:radio[name=GrowthMayBeMalignant][value=' + data.CancerGrowthMayBeMalignant +']')).prop('checked', true);
        $('#GrowthMayBeMalignant').data('oldValue', data.CancerGrowthMayBeMalignant);
        $('#GrowthMayBeMalignant').val(data.CancerGrowthMayBeMalignant);

        $('#DetailsEvidence').val(data.CancerDetailsEvidence);
        if($('input:radio[name=RemovedGrowth][value=' + data.CancerRemovedGrowth +']').length===0)
           $('input:radio[name=RemovedGrowth]').prop('checked', false);
        else
           $($('input:radio[name=RemovedGrowth][value=' + data.CancerRemovedGrowth +']')).prop('checked', true);
        $('#RemovedGrowth').data('oldValue', data.CancerRemovedGrowth);
        $('#RemovedGrowth').val(data.CancerRemovedGrowth);

        $('#DetailsInvestigations').val(data.CancerDetailsInvestigations);
        $('#ChemotherapyTreatmentFollowing').prop("checked", data.CancerChemotherapyTreatmentFollowing);
        $('#RadiotherapyTreatmentFollowing').prop("checked", data.CancerRadiotherapyTreatmentFollowing);
        $('#MedicationTreatmentFollowing').prop("checked", data.CancerMedicationTreatmentFollowing);
        $('#OtherTreatmentFollowing').val(data.CancerOtherTreatmentFollowing);
        if($('input:radio[name=StillFollowedUp][value=' + data.CancerStillFollowedUp +']').length===0)
           $('input:radio[name=StillFollowedUp]').prop('checked', false);
        else
           $($('input:radio[name=StillFollowedUp][value=' + data.CancerStillFollowedUp +']')).prop('checked', true);
        $('#StillFollowedUp').data('oldValue', data.CancerStillFollowedUp);
        $('#StillFollowedUp').val(data.CancerStillFollowedUp);

        $('#HowOften').val(data.CancerHowOften);
        $('#DateOfDischargedFollowUp').val(generalSupport.ToJavaScriptDateCustom(data.CancerDateOfDischargedFollowUp, generalSupport.DateFormat()));
        $('#SurgeryTreatment').prop("checked", data.CancerSurgeryTreatment);
        $('#DateSurgeryTreatment').val(generalSupport.ToJavaScriptDateCustom(data.CancerDateSurgeryTreatment, generalSupport.DateFormat()));
        $('#ChemotherapyTreatment').prop("checked", data.CancerChemotherapyTreatment);
        $('#DateChemotherapyTreatment').val(generalSupport.ToJavaScriptDateCustom(data.CancerDateChemotherapyTreatment, generalSupport.DateFormat()));
        $('#RadiationTreatment').prop("checked", data.CancerRadiationTreatment);
        $('#DateRadiationTreatment').val(generalSupport.ToJavaScriptDateCustom(data.CancerDateRadiationTreatment, generalSupport.DateFormat()));
        $('#HormoneTreatment').prop("checked", data.CancerHormoneTreatment);
        $('#DateHormoneTreatment').val(generalSupport.ToJavaScriptDateCustom(data.CancerDateHormoneTreatment, generalSupport.DateFormat()));
        $('#OtherTreatment').prop("checked", data.CancerOtherTreatment);
        $('#DateOtherTreatment').val(generalSupport.ToJavaScriptDateCustom(data.CancerDateOtherTreatment, generalSupport.DateFormat()));
        $('#NameTreatment').val(data.CancerNameTreatment);
        if($('input:radio[name=Prognosis][value=' + data.CancerPrognosis +']').length===0)
           $('input:radio[name=Prognosis]').prop('checked', false);
        else
           $($('input:radio[name=Prognosis][value=' + data.CancerPrognosis +']')).prop('checked', true);
        $('#Prognosis').data('oldValue', data.CancerPrognosis);
        $('#Prognosis').val(data.CancerPrognosis);

        $('#DetailsPrognosis').val(data.CancerDetailsPrognosis);
        $('#ChemotherapyCurrentlyTreatment').prop("checked", data.CancerChemotherapyCurrentlyTreatment);
        $('#RadiationCurrentlyTreatment').prop("checked", data.CancerRadiationCurrentlyTreatment);
        $('#HormoneCurrentlyTreatment').prop("checked", data.CancerHormoneCurrentlyTreatment);
        $('#MedicationCurrentlyTreatment').prop("checked", data.CancerMedicationCurrentlyTreatment);
        $('#OtherCurrentlyTreatment').val(data.CancerOtherCurrentlyTreatment);
        $('#DetailsCurrentlyTreatment').val(data.CancerDetailsCurrentlyTreatment);
        if($('input:radio[name=YesRestrictedLifeStyle][value=' + data.CancerYesRestrictedLifeStyle +']').length===0)
           $('input:radio[name=YesRestrictedLifeStyle]').prop('checked', false);
        else
           $($('input:radio[name=YesRestrictedLifeStyle][value=' + data.CancerYesRestrictedLifeStyle +']')).prop('checked', true);
        $('#YesRestrictedLifeStyle').data('oldValue', data.CancerYesRestrictedLifeStyle);
        $('#YesRestrictedLifeStyle').val(data.CancerYesRestrictedLifeStyle);

        $('#AdditionalInformation').val(data.CancerAdditionalInformation);
        $('#DateReceived').val(generalSupport.ToJavaScriptDateCustom(data.CancerDateReceived, generalSupport.DateFormat()));


        if (data.DetailsAbsensesFromWork_DetailsAbsensesFromWork !== null)
            $('#DetailsAbsensesFromWorkTbl').bootstrapTable('load', data.DetailsAbsensesFromWork_DetailsAbsensesFromWork);
        if (data.DetailsOfMedicalPractitioners_DetailsOfMedicalPractitioners !== null)
            $('#DetailsOfMedicalPractitionersTbl').bootstrapTable('load', data.DetailsOfMedicalPractitioners_DetailsOfMedicalPractitioners);

    };

    this.ControlBehaviour = function () {







        $('#DateDiagnosed_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateDiagnosed_group');
        $('#DateEvidence_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateEvidence_group');
        $('#DateInitiallyTreated_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateInitiallyTreated_group');
        $('#DateLastTreated_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateLastTreated_group');
        $('#DateLastSeenByDoctor_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateLastSeenByDoctor_group');
        $('#DateOfDischargedFollowUp_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateOfDischargedFollowUp_group');
        $('#DateSurgeryTreatment_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateSurgeryTreatment_group');
        $('#DateChemotherapyTreatment_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateChemotherapyTreatment_group');
        $('#DateRadiationTreatment_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateRadiationTreatment_group');
        $('#DateHormoneTreatment_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateHormoneTreatment_group');
        $('#DateOtherTreatment_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateOtherTreatment_group');
        $('#DateFrom_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateFrom_group');
        $('#DateTo_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateTo_group');
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
					         HT5CancerQuestionnaireUWSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   HT5CancerQuestionnaireUWSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        app.core.AsyncWebMethod("/fasi/dli/forms/HT5CancerQuestionnaireUWActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), true,
            JSON.stringify({
                id: $('#HT5CancerQuestionnaireUWFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
                if (data.d.Success)
                    $('#HT5CancerQuestionnaireUWFormId').val(data.d.Data.Instance.InstanceFormId);
                    
                if (data.d.Success === true && data.d.Data.LookUps) {
                    data.d.Data.LookUps.forEach(function (elementSource) {
                        generalSupport.RenderLookUp(elementSource.Key, data.d.Data.Instance[elementSource.Key], 'Initialization', elementSource.Items);
                    });
                }
                


    $("#DetailsAbsensesFromWorkTblPlaceHolder").replaceWith('<table id="DetailsAbsensesFromWorkTbl"><caption>Detalles de las ausencias del trabajo</caption></table>');
    HT5CancerQuestionnaireUWSupport.DetailsAbsensesFromWorkTblSetup($('#DetailsAbsensesFromWorkTbl'));
    $("#DetailsOfMedicalPractitionersTblPlaceHolder").replaceWith('<table id="DetailsOfMedicalPractitionersTbl"><caption>Direcciones de médicos</caption></table>');
    HT5CancerQuestionnaireUWSupport.DetailsOfMedicalPractitionersTblSetup($('#DetailsOfMedicalPractitionersTbl'));





                HT5CancerQuestionnaireUWSupport.ActionProcess(data, 'Initialization');

                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)                    
                    window.history.replaceState({}, null, '/fasi/dli/forms/' + location.pathname.substring(location.pathname.lastIndexOf("/") + 1) + '?id=' + $('#HT5CancerQuestionnaireUWFormId').val());
 
              
          

            });
    };




    this.ControlActions =   function () {

       $('input:radio[name=EvidenceOfRecurringCancer]').change(function () {
                     var data;
                if ($('#EvidenceOfRecurringCancer_True').is(":checked")){
 
   $('#LocationRecurringCancer').removeAttr('disabled');
   $('#DateEvidence').removeAttr('disabled');

}else{
 
   $('#DateEvidence').attr('disabled', 'disabled');
   $('#LocationRecurringCancer').attr('disabled', 'disabled');

}




        });
       $('input:radio[name=GrowthMayBeMalignant]').change(function () {
                     var data;
                if ($('#GrowthMayBeMalignant_True').is(":checked")){
 
   $('#DetailsEvidence').removeAttr('disabled');
}else{
 
   $('#DetailsEvidence').attr('disabled', 'disabled');

}



        });
       $('input:radio[name=RemovedGrowth]').change(function () {
                     var data;
                if ($('#RemovedGrowth_True').is(":checked")){
 
   $('#DetailsInvestigations').attr('disabled', 'disabled');

}else{
 
   $('#DetailsInvestigations').removeAttr('disabled');
}



        });
       $('input:radio[name=StillFollowedUp]').change(function () {
                     var data;
                if ($('#StillFollowedUp_True').is(":checked")){
 
   $('#HowOften').removeAttr('disabled');
   
$('#DateOfDischargedFollowUp').attr('disabled', 'disabled');
}else{

   $('#HowOften').attr('disabled', 'disabled');
   $('#DateOfDischargedFollowUp').removeAttr('disabled'); 
}



        });
        $('#SurgeryTreatment').change(function () {
                      var data;
                if ($('#SurgeryTreatment').is(":checked")){
 
   $('#DateSurgeryTreatment').removeAttr('disabled');
}else{

   $('#DateSurgeryTreatment').attr('disabled', 'disabled');
}



        });
        $('#ChemotherapyTreatment').change(function () {
                      var data;
                if ($('#ChemotherapyTreatment').is(":checked")){
 
   $('#DateChemotherapyTreatment').removeAttr('disabled');
}else{

   $('#DateChemotherapyTreatment').attr('disabled', 'disabled');
}



        });
        $('#RadiationTreatment').change(function () {
                      var data;
                if ($('#RadiationTreatment').is(":checked")){
 
   $('#DateRadiationTreatment').removeAttr('disabled');
}else{

   $('#DateRadiationTreatment').attr('disabled', 'disabled');
}



        });
        $('#HormoneTreatment').change(function () {
                      var data;
                if ($('#HormoneTreatment').is(":checked")){
 
   $('#DateHormoneTreatment').removeAttr('disabled');
}else{

   $('#DateHormoneTreatment').attr('disabled', 'disabled');
}



        });
        $('#OtherTreatment').change(function () {
                      var data;
                if ($('#OtherTreatment').is(":checked")){
 
   $('#DateOtherTreatment').removeAttr('disabled');
}else{

   $('#DateOtherTreatment').attr('disabled', 'disabled');
}



        });
       $('input:radio[name=Prognosis]').change(function () {
                     var data;
                if ($('#Prognosis_True').is(":checked")){
 
   $('#DetailsPrognosis').removeAttr('disabled');
}else{

   $('#DetailsPrognosis').attr('disabled', 'disabled');
}



        });
        $('#save').click(function (event) {
                var formInstance = $("#HT5CancerQuestionnaireUWMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#save'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/HT5CancerQuestionnaireUWActions.aspx/saveClick", false,
                          JSON.stringify({
                                        instance: HT5CancerQuestionnaireUWSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    HT5CancerQuestionnaireUWSupport.ActionProcess(data, 'saveClick');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('#submit').click(function (event) {
                var formInstance = $("#HT5CancerQuestionnaireUWMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#submit'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/HT5CancerQuestionnaireUWActions.aspx/submitClick", false,
                          JSON.stringify({
                                        instance: HT5CancerQuestionnaireUWSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    HT5CancerQuestionnaireUWSupport.ActionProcess(data, 'submitClick');
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


        $("#HT5CancerQuestionnaireUWMainForm").validate({
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
                    maxlength: 15
                },
                TypeOfCancer: {
                    required: true                },
                DateDiagnosed: {
                    required: true,
                    DatePicker: true
                },
                StageOfCancer: {
                    required: true                },
                ScaleColonRectalCancer: {
                },
                LevelMelanoma: {
                },
                GradeProstateCancer: {
                },
                EvidenceOfRecurringCancer: {
                    required: true,
                },
                DateEvidence: {
                    required: true,
                    DatePicker: true
                },
                LocationRecurringCancer: {
                    required: true,
                    maxlength: 15
                },
                DateInitiallyTreated: {
                    required: true,
                    DatePicker: true
                },
                DateLastTreated: {
                    required: true,
                    DatePicker: true
                },
                DateLastSeenByDoctor: {
                    required: true,
                    DatePicker: true
                },
                GrowthMayBeMalignant: {
                    required: true,
                },
                DetailsEvidence: {
                    required: true,
                    maxlength: 35
                },
                RemovedGrowth: {
                    required: true,
                },
                DetailsInvestigations: {
                    required: true,
                    maxlength: 35
                },
                OtherTreatmentFollowing: {
                    maxlength: 35
                },
                StillFollowedUp: {
                    required: true,
                },
                HowOften: {
                    required: true,
                    maxlength: 35
                },
                DateOfDischargedFollowUp: {
                    required: true,
                    DatePicker: true
                },
                DateSurgeryTreatment: {
                    required: true,
                    DatePicker: true
                },
                DateChemotherapyTreatment: {
                    required: true,
                    DatePicker: true
                },
                DateRadiationTreatment: {
                    required: true,
                    DatePicker: true
                },
                DateHormoneTreatment: {
                    required: true,
                    DatePicker: true
                },
                DateOtherTreatment: {
                    required: true,
                    DatePicker: true
                },
                NameTreatment: {
                    maxlength: 35
                },
                Prognosis: {
                },
                DetailsPrognosis: {
                    required: true,
                    maxlength: 35
                },
                OtherCurrentlyTreatment: {
                    maxlength: 15
                },
                DetailsCurrentlyTreatment: {
                    maxlength: 35
                },
                YesRestrictedLifeStyle: {
                },
                AdditionalInformation: {
                    maxlength: 0
                },
                DateReceived: {
                    DatePicker: true
                }
            },
            messages: {
                ClientName: {
                    maxlength: 'El campo permite 30 caracteres máximo'
                },
                uwcaseid: {
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                TypeOfCancer: {
                    required: 'El campo es requerido'                },
                DateDiagnosed: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                StageOfCancer: {
                    required: 'El campo es requerido'                },
                ScaleColonRectalCancer: {
                },
                LevelMelanoma: {
                },
                GradeProstateCancer: {
                },
                EvidenceOfRecurringCancer: {
                    required: 'El campo es requerido',
                },
                DateEvidence: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                LocationRecurringCancer: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                DateInitiallyTreated: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                DateLastTreated: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                DateLastSeenByDoctor: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                GrowthMayBeMalignant: {
                    required: 'El campo es requerido',
                },
                DetailsEvidence: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 35 caracteres máximo'
                },
                RemovedGrowth: {
                    required: 'El campo es requerido',
                },
                DetailsInvestigations: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 35 caracteres máximo'
                },
                OtherTreatmentFollowing: {
                    maxlength: 'El campo permite 35 caracteres máximo'
                },
                StillFollowedUp: {
                    required: 'El campo es requerido',
                },
                HowOften: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 35 caracteres máximo'
                },
                DateOfDischargedFollowUp: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                DateSurgeryTreatment: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                DateChemotherapyTreatment: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                DateRadiationTreatment: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                DateHormoneTreatment: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                DateOtherTreatment: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                NameTreatment: {
                    maxlength: 'El campo permite 35 caracteres máximo'
                },
                Prognosis: {
                },
                DetailsPrognosis: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 35 caracteres máximo'
                },
                OtherCurrentlyTreatment: {
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                DetailsCurrentlyTreatment: {
                    maxlength: 'El campo permite 35 caracteres máximo'
                },
                YesRestrictedLifeStyle: {
                },
                AdditionalInformation: {
                    maxlength: 'El campo permite 0 caracteres máximo'
                },
                DateReceived: {
                    DatePicker: 'La fecha indicada no es válida'
                }
            }
        });
        $("#DetailsAbsensesFromWorkEditForm").validate().destroy();
        $("#DetailsAbsensesFromWorkEditForm").validate({
            rules: {
                DateFrom: {
                    DatePicker: true
                },
                DateTo: {
                    DatePicker: true
                },
                Details: {
                    maxlength: 25
                }

            },
            messages: {
                DateFrom: {
                    DatePicker: 'La fecha indicada no es válida'
                },
                DateTo: {
                    DatePicker: 'La fecha indicada no es válida'
                },
                Details: {
                    maxlength: 'El campo permite 25 caracteres máximo'
                }

            }
        });
        $("#DetailsOfMedicalPractitionersEditForm").validate().destroy();
        $("#DetailsOfMedicalPractitionersEditForm").validate({
            rules: {
                PractitionerName: {
                    maxlength: 30
                },
                PhonePractitioner: {
                    maxlength: 15
                },
                eMailPractitioner: {
                    maxlength: 50,
                    email: true,
                    email: true
                },
                AddresPractitioner: {
                    maxlength: 45
                }

            },
            messages: {
                PractitionerName: {
                    maxlength: 'El campo permite 30 caracteres máximo'
                },
                PhonePractitioner: {
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                eMailPractitioner: {
                    maxlength: 'El campo permite 50 caracteres máximo',
                    email: 'Debes ingresar una dirección de correo electrónico válido'
                },
                AddresPractitioner: {
                    maxlength: 'El campo permite 45 caracteres máximo'
                }

            }
        });

    };

    this.DetailsAbsensesFromWorkTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'id',
            toolbar: '#DetailsAbsensesFromWorktoolbar',
            columns: [{
                field: 'selected',
                checkbox: true,
                formatter: 'HT5CancerQuestionnaireUWSupport.selected_Formatter'
            }, {
                field: 'DateFrom',
                title: 'Desde',
                events: 'DetailsAbsensesFromWorkActionEvents',
                formatter: 'tableHelperSupport.EditCommandOnlyDateFormatter',
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
                HT5CancerQuestionnaireUWSupport.DetailsAbsensesFromWorkRowToInput(row);
                
                
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
            HT5CancerQuestionnaireUWSupport.DetailsAbsensesFromWorkShowModal($('#DetailsAbsensesFromWorkPopup').modal({ show: false }), $(this).attr('data-modal-title'));
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

                HT5CancerQuestionnaireUWSupport.currentRow.DateFrom = generalSupport.DatePickerValue('#DateFrom');
                HT5CancerQuestionnaireUWSupport.currentRow.DateTo = generalSupport.DatePickerValue('#DateTo');
                HT5CancerQuestionnaireUWSupport.currentRow.Details = $('#Details').val();

                $('#DetailsAbsensesFromWorkSaveBtn').prop('disabled', false);
                $('#DetailsAbsensesFromWorkSaveBtn').html(caption);

                if (wm === 'Update') {
                    $('#DetailsAbsensesFromWorkTbl').bootstrapTable('updateByUniqueId', { id: HT5CancerQuestionnaireUWSupport.currentRow.id, row: HT5CancerQuestionnaireUWSupport.currentRow });
                    $modal.modal('hide');
                }
                else {                    
                    $('#DetailsAbsensesFromWorkTbl').bootstrapTable('append', HT5CancerQuestionnaireUWSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.DetailsAbsensesFromWorkShowModal = function (md, title, row) {
        var formInstance = $("#DetailsAbsensesFromWorkEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { DateFrom: null, DateTo: null, Details: '' };

        md.data('id', row.id);
        md.find('.modal-title').text(title);

        HT5CancerQuestionnaireUWSupport.DetailsAbsensesFromWorkRowToInput(row);


        md.appendTo("body");
        md.modal('show');
    };

    this.DetailsAbsensesFromWorkRowToInput = function (row) {
        HT5CancerQuestionnaireUWSupport.currentRow = row;
        $('#DateFrom').val(generalSupport.ToJavaScriptDateCustom(row.DateFrom, generalSupport.DateFormat()));
        $('#DateTo').val(generalSupport.ToJavaScriptDateCustom(row.DateTo, generalSupport.DateFormat()));
        $('#Details').val(row.Details);

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
                formatter: 'HT5CancerQuestionnaireUWSupport.selected_Formatter'
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
                title: 'Correo',
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
                HT5CancerQuestionnaireUWSupport.DetailsOfMedicalPractitionersRowToInput(row);
                
                
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
            HT5CancerQuestionnaireUWSupport.DetailsOfMedicalPractitionersShowModal($('#DetailsOfMedicalPractitionersPopup').modal({ show: false }), $(this).attr('data-modal-title'));
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

                HT5CancerQuestionnaireUWSupport.currentRow.PractitionerName = $('#PractitionerName').val();
                HT5CancerQuestionnaireUWSupport.currentRow.PhonePractitioner = $('#PhonePractitioner').val();
                HT5CancerQuestionnaireUWSupport.currentRow.eMailPractitioner = $('#eMailPractitioner').val();
                HT5CancerQuestionnaireUWSupport.currentRow.AddresPractitioner = $('#AddresPractitioner').val();

                $('#DetailsOfMedicalPractitionersSaveBtn').prop('disabled', false);
                $('#DetailsOfMedicalPractitionersSaveBtn').html(caption);

                if (wm === 'Update') {
                    $('#DetailsOfMedicalPractitionersTbl').bootstrapTable('updateByUniqueId', { id: HT5CancerQuestionnaireUWSupport.currentRow.IdDetails, row: HT5CancerQuestionnaireUWSupport.currentRow });
                    $modal.modal('hide');
                }
                else {                    
                    $('#DetailsOfMedicalPractitionersTbl').bootstrapTable('append', HT5CancerQuestionnaireUWSupport.currentRow);
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
        
        row = row || { PractitionerName: '', PhonePractitioner: '', eMailPractitioner: '', AddresPractitioner: '' };

        md.data('id', row.IdDetails);
        md.find('.modal-title').text(title);

        HT5CancerQuestionnaireUWSupport.DetailsOfMedicalPractitionersRowToInput(row);


        md.appendTo("body");
        md.modal('show');
    };

    this.DetailsOfMedicalPractitionersRowToInput = function (row) {
        HT5CancerQuestionnaireUWSupport.currentRow = row;
        $('#PractitionerName').val(row.PractitionerName);
        $('#PhonePractitioner').val(row.PhonePractitioner);
        $('#eMailPractitioner').val(row.eMailPractitioner);
        $('#AddresPractitioner').val(row.AddresPractitioner);

    };







	    this.selected_Formatter = function (value, row, index) {          
          return {
                  disabled: $('#DetailsAbsensesFromWorkTbl *').prop('disabled'),
                  checked: value
                 }
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
        masterSupport.setPageTitle('HT5Cuestionario de cáncer, tumor o quistes');
        

    HT5CancerQuestionnaireUWSupport.ControlBehaviour();
    HT5CancerQuestionnaireUWSupport.ControlActions();
    HT5CancerQuestionnaireUWSupport.ValidateSetup();
    HT5CancerQuestionnaireUWSupport.Initialization();


  };
};

$(document).ready(function () {
   HT5CancerQuestionnaireUWSupport.Init();
});

window.DetailsAbsensesFromWorkActionEvents = {
    'click .update': function (e, value, row, index) {
        HT5CancerQuestionnaireUWSupport.DetailsAbsensesFromWorkShowModal($('#DetailsAbsensesFromWorkPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.DetailsOfMedicalPractitionersActionEvents = {
    'click .update': function (e, value, row, index) {
        HT5CancerQuestionnaireUWSupport.DetailsOfMedicalPractitionersShowModal($('#DetailsOfMedicalPractitionersPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
