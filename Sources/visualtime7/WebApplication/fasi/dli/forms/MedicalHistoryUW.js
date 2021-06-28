var MedicalHistoryUWSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#MedicalHistoryUWFormId').val(),
            ClientName: $('#ClientName').val(),
            uwcaseid: parseInt(0 + $('#uwcaseid').val(), 10),
            MedicalHistoryHaveAPersonalPhysician: $('input:radio[name=HaveAPersonalPhysician]:checked').val(),
            MedicalHistoryPhysicianFirstName: $('#PhysicianFirstName').val(),
            MedicalHistoryPhysicianLastName: $('#PhysicianLastName').val(),
            MedicalHistoryAddress: $('#Address').val(),
            MedicalHistoryState: $('#State').val(),
            MedicalHistoryCity: $('#City').val(),
            MedicalHistoryZip: $('#Zip').val(),
            Telephone_Telephone: generalSupport.NormalizeProperties($('#TelephoneTbl').bootstrapTable('getData'), ''),
            MedicalHistoryDateLastSeenAPhysician: generalSupport.DatePickerValueInputToObject('#DateLastSeenAPhysician'),
            MedicalHistoryReasonLastSeen: $('#ReasonLastSeenPhysician').val(),
            MedicalTreatmentMedicalHistory_MedicalTreatmentMedicalHistory: generalSupport.NormalizeProperties($('#MedicalTreatmentMedicalHistoryTbl').bootstrapTable('getData'), ''),
            MedicalHistoryDisorderEyesEarsNoseThroat: $('#DisorderEyesEarsNoseThroat').is(':checked'),
            MedicalHistoryHeadaches: $('#Headaches').is(':checked'),
            MedicalHistoryDizziness: $('#Dizziness').is(':checked'),
            MedicalHistoryElevatedBloodPressure: $('#ElevatedBloodPressure').is(':checked'),
            MedicalHistoryChestDiscomfort: $('#ChestDiscomfort').is(':checked'),
            MedicalHistoryIrregularHeartbeat: $('#IrregularHeartbeat').is(':checked'),
            MedicalHistoryHeartAttack: $('#HeartAttack').is(':checked'),
            MedicalHistoryHeartMurmur: $('#HeartMurmur').is(':checked'),
            MedicalHistoryDisorderHeartOrBlood: $('#DisorderHeartOrBlood').is(':checked'),
            MedicalHistoryStrokeTransientIschemicAttack: $('#StrokeTransientIschemicAttack').is(':checked'),
            MedicalHistoryShortnessBreath: $('#ShortnessBreath').is(':checked'),
            MedicalHistoryAsthma: $('#Asthma').is(':checked'),
            MedicalHistorySleepApnea: $('#SleepApnea').is(':checked'),
            MedicalHistoryEmphysemaChronicBronchitis: $('#EmphysemaChronicBronchitis').is(':checked'),
            MedicalHistoryAllergies: $('#Allergies').is(':checked'),
            MedicalHistoryRespiratoryDisorder: $('#RespiratoryDisorder').is(':checked'),
            MedicalHistoryDisorderEsophagus: $('#DisorderEsophagus').is(':checked'),
            MedicalHistoryDigestiveDisorder: $('#DigestiveDisorder').is(':checked'),
            MedicalHistoryDisorderIntestinesColonRectum: $('#DisorderIntestinesColonRectum').is(':checked'),
            MedicalHistoryCrohnsDisease: $('#CrohnsDisease').is(':checked'),
            MedicalHistoryHepatitis: $('#Hepatitis').is(':checked'),
            MedicalHistoryOtherLiverDisorder: $('#OtherLiverDisorder').is(':checked'),
            MedicalHistoryKidneyDisorder: $('#KidneyDisorder').is(':checked'),
            MedicalHistoryUrineAbnormality: $('#UrineAbnormality').is(':checked'),
            MedicalHistoryBladderDisorder: $('#BladderDisorder').is(':checked'),
            MedicalHistoryPregnacyComplications: $('#PregnacyComplications').is(':checked'),
            MedicalHistoryDisorderREproductiveOrgans: $('#DisorderREproductiveOrgans').is(':checked'),
            MedicalHistorySexuallyTransmittedDisease: $('#SexuallyTransmittedDisease').is(':checked'),
            MedicalHistoryDiabetesGlucoseIntolerance: $('#DiabetesGlucoseIntolerance').is(':checked'),
            MedicalHistoryDisorderPancreas: $('#DisorderPancreas').is(':checked'),
            MedicalHistoryOtherEndocrineDisorders: $('#OtherEndocrineDisorders').is(':checked'),
            MedicalHistoryBloodTransfusion: $('#BloodTransfusion').is(':checked'),
            MedicalHistoryAnemiaBloodAbnormality: $('#AnemiaBloodAbnormality').is(':checked'),
            MedicalHistoryCancerMalignatTumor: $('#CancerMalignatTumor').is(':checked'),
            MedicalHistoryLeukemia: $('#Leukemia').is(':checked'),
            MedicalHistoryBenignTumor: $('#BenignTumor').is(':checked'),
            MedicalHistoryConnectiveTissueDisorder: $('#ConnectiveTissueDisorder').is(':checked'),
            MedicalHistoryParalysis: $('#Paralysis').is(':checked'),
            MedicalHistoryRheumatoidArthritis: $('#RheumatoidArthritis').is(':checked'),
            MedicalHistoryBackSpineNeckDisorder: $('#BackSpineNeckDisorder').is(':checked'),
            MedicalHistoryMuscleDisorder: $('#MuscleDisorder').is(':checked'),
            MedicalHistorySciaticaNeuritis: $('#SciaticaNeuritis').is(':checked'),
            MedicalHistoryOtherBoneDisorder: $('#OtherBoneDisorder').is(':checked'),
            MedicalHistoryAlcoholCounselingTreatment: $('#AlcoholCounselingTreatment').is(':checked'),
            MedicalHistoryAlcoholism: $('#Alcoholism').is(':checked'),
            MedicalHistoryDrugCounselingTreatment: $('#DrugCounselingTreatment').is(':checked'),
            MedicalHistorySuicideAttempt: $('#SuicideAttempt').is(':checked'),
            MedicalHistoryPanicAttack: $('#PanicAttack').is(':checked'),
            MedicalHistoryDepression: $('#Depression').is(':checked'),
            MedicalHistorySeizuresNeurologicalDisorder: $('#SeizuresNeurologicalDisorder').is(':checked'),
            MedicalHistoryOtherMentalDisorders: $('#OtherMentalDisorders').is(':checked'),
            MedicalHistoryAnyOtherDeseaseDisorderCondition: $('#AnyOtherDeseaseDisorderCondition').is(':checked'),
            MedicalConditionsDetailsGrid_MedicalConditionsDetails: generalSupport.NormalizeProperties($('#MedicalConditionsDetailsGridTbl').bootstrapTable('getData'), 'LastTreated,LastEpisode'),
            MedicalHistoryCurrentlyPregnant: $('input:radio[name=CurrentlyPregnant]:checked').val(),
            MedicalHistoryDatePregnant: generalSupport.DatePickerValueInputToObject('#DatePregnant'),
            MedicalHistoryHaveSurgeriesDiagnosticsTestNotCompleted: $('input:radio[name=HaveSurgeriesDiagnosticsTestNotCompleted]:checked').val(),
            MedicalHistoryHaveBeenWightChange: $('input:radio[name=HaveBeenWightChange]:checked').val(),
            MedicalHistoryWeightTwelveMonthsAgo: generalSupport.NumericValue('#WeightTwelveMonthsAgo', 0, 9999),
            MedicalHistoryPresentWeight: generalSupport.NumericValue('#PresentWeight', 0, 9999),
            MedicalHistoryReasonForWeightChange: $('#ReasonForWeightChange').val(),
            MedicalHistoryTreatedByAidsArcHIV: $('input:radio[name=TreatedByAidsArcHIV]:checked').val(),
            MedicalHistoryParticipateRegularExerciseProgram: $('input:radio[name=ParticipateRegularExerciseProgram]:checked').val(),
            MedicalHistoryMilitaryService: $('#MilitaryService').val(),
            MedicalHistoryUsedPrescriptionDrugs: $('input:radio[name=UsedPrescriptionDrugs]:checked').val(),
            DetailsPrescriptionDrugs_DetailsPrescriptionDrugs: generalSupport.NormalizeProperties($('#DetailsPrescriptionDrugsTbl').bootstrapTable('getData'), 'DateLastUse'),
            MedicalHistoryNeverUsed: $('#NeverUsed').is(':checked'),
            MedicalHistoryCigarettes: $('#Cigarettes').is(':checked'),
            MedicalHistoryDateCigarettes: generalSupport.DatePickerValueInputToObject('#DateCigarettes'),
            MedicalHistoryNumberPacksDaily: generalSupport.NumericValue('#NumberPacksDaily', 0, 99),
            MedicalHistoryPipe: $('#Pipe').is(':checked'),
            MedicalHistoryDatePipe: generalSupport.DatePickerValueInputToObject('#DatePipe'),
            MedicalHistoryCigar: $('#Cigar').is(':checked'),
            MedicalHistoryDateCigar: generalSupport.DatePickerValueInputToObject('#DateCigar'),
            MedicalHistoryNicotinePatch: $('#NicotinePatch').is(':checked'),
            MedicalHistoryDateNicotinePatch: generalSupport.DatePickerValueInputToObject('#DateNicotinePatch'),
            MedicalHistoryNicotineGum: $('#NicotineGum').is(':checked'),
            MedicalHistoryDateNicotineGum: generalSupport.DatePickerValueInputToObject('#DateNicotineGum'),
            MedicalHistoryChewingTobacco: $('#ChewingTobacco').is(':checked'),
            MedicalHistoryDateChewingTobacco: generalSupport.DatePickerValueInputToObject('#DateChewingTobacco'),
            MedicalHistoryOtherTobacco: $('#OtherTobacco').is(':checked'),
            MedicalHistoryDateOtherTobacco: generalSupport.DatePickerValueInputToObject('#DateOtherTobacco'),
            BiologicalFamilyCensusGrid_BiologicalFamilyCensus: generalSupport.NormalizeProperties($('#BiologicalFamilyCensusGridTbl').bootstrapTable('getData'), ''),
            MedicalHistoryAdditionalInformation: $('#AdditionalInformation').val(),
            MedicalHistoryDateQuestionnaire: generalSupport.DatePickerValueInputToObject('#DateQuestionnaire')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#MedicalHistoryUWFormId').val(data.InstanceFormId);
        $('#ClientName').val(data.ClientName);
        $('#uwcaseid').val(data.uwcaseid);
        if($('input:radio[name=HaveAPersonalPhysician][value=' + data.MedicalHistoryHaveAPersonalPhysician +']').length===0){
           $('input:radio[name=HaveAPersonalPhysician]').prop('checked', false);
           $('input:radio[name=HaveAPersonalPhysician].default').prop('checked', true);
        }
        else
           $($('input:radio[name=HaveAPersonalPhysician][value=' + data.MedicalHistoryHaveAPersonalPhysician +']')).prop('checked', true);
        $('#HaveAPersonalPhysician').data('oldValue', data.MedicalHistoryHaveAPersonalPhysician);
        $('#HaveAPersonalPhysician').val(data.MedicalHistoryHaveAPersonalPhysician);

        $('#PhysicianFirstName').val(data.MedicalHistoryPhysicianFirstName);
        $('#PhysicianLastName').val(data.MedicalHistoryPhysicianLastName);
        $('#Address').val(data.MedicalHistoryAddress);
        $('#State').val(data.MedicalHistoryState);
        $('#City').val(data.MedicalHistoryCity);
        $('#Zip').val(data.MedicalHistoryZip);
        $('#DateLastSeenAPhysician').val(generalSupport.ToJavaScriptDateCustom(data.MedicalHistoryDateLastSeenAPhysician, generalSupport.DateFormat()));
        $('#ReasonLastSeenPhysician').val(data.MedicalHistoryReasonLastSeen);
        $('#DisorderEyesEarsNoseThroat').prop("checked", data.MedicalHistoryDisorderEyesEarsNoseThroat);
        $('#Headaches').prop("checked", data.MedicalHistoryHeadaches);
        $('#Dizziness').prop("checked", data.MedicalHistoryDizziness);
        $('#ElevatedBloodPressure').prop("checked", data.MedicalHistoryElevatedBloodPressure);
        $('#ChestDiscomfort').prop("checked", data.MedicalHistoryChestDiscomfort);
        $('#IrregularHeartbeat').prop("checked", data.MedicalHistoryIrregularHeartbeat);
        $('#HeartAttack').prop("checked", data.MedicalHistoryHeartAttack);
        $('#HeartMurmur').prop("checked", data.MedicalHistoryHeartMurmur);
        $('#DisorderHeartOrBlood').prop("checked", data.MedicalHistoryDisorderHeartOrBlood);
        $('#StrokeTransientIschemicAttack').prop("checked", data.MedicalHistoryStrokeTransientIschemicAttack);
        $('#ShortnessBreath').prop("checked", data.MedicalHistoryShortnessBreath);
        $('#Asthma').prop("checked", data.MedicalHistoryAsthma);
        $('#SleepApnea').prop("checked", data.MedicalHistorySleepApnea);
        $('#EmphysemaChronicBronchitis').prop("checked", data.MedicalHistoryEmphysemaChronicBronchitis);
        $('#Allergies').prop("checked", data.MedicalHistoryAllergies);
        $('#RespiratoryDisorder').prop("checked", data.MedicalHistoryRespiratoryDisorder);
        $('#DisorderEsophagus').prop("checked", data.MedicalHistoryDisorderEsophagus);
        $('#DigestiveDisorder').prop("checked", data.MedicalHistoryDigestiveDisorder);
        $('#DisorderIntestinesColonRectum').prop("checked", data.MedicalHistoryDisorderIntestinesColonRectum);
        $('#CrohnsDisease').prop("checked", data.MedicalHistoryCrohnsDisease);
        $('#Hepatitis').prop("checked", data.MedicalHistoryHepatitis);
        $('#OtherLiverDisorder').prop("checked", data.MedicalHistoryOtherLiverDisorder);
        $('#KidneyDisorder').prop("checked", data.MedicalHistoryKidneyDisorder);
        $('#UrineAbnormality').prop("checked", data.MedicalHistoryUrineAbnormality);
        $('#BladderDisorder').prop("checked", data.MedicalHistoryBladderDisorder);
        $('#PregnacyComplications').prop("checked", data.MedicalHistoryPregnacyComplications);
        $('#DisorderREproductiveOrgans').prop("checked", data.MedicalHistoryDisorderREproductiveOrgans);
        $('#SexuallyTransmittedDisease').prop("checked", data.MedicalHistorySexuallyTransmittedDisease);
        $('#DiabetesGlucoseIntolerance').prop("checked", data.MedicalHistoryDiabetesGlucoseIntolerance);
        $('#DisorderPancreas').prop("checked", data.MedicalHistoryDisorderPancreas);
        $('#OtherEndocrineDisorders').prop("checked", data.MedicalHistoryOtherEndocrineDisorders);
        $('#BloodTransfusion').prop("checked", data.MedicalHistoryBloodTransfusion);
        $('#AnemiaBloodAbnormality').prop("checked", data.MedicalHistoryAnemiaBloodAbnormality);
        $('#CancerMalignatTumor').prop("checked", data.MedicalHistoryCancerMalignatTumor);
        $('#Leukemia').prop("checked", data.MedicalHistoryLeukemia);
        $('#BenignTumor').prop("checked", data.MedicalHistoryBenignTumor);
        $('#ConnectiveTissueDisorder').prop("checked", data.MedicalHistoryConnectiveTissueDisorder);
        $('#Paralysis').prop("checked", data.MedicalHistoryParalysis);
        $('#RheumatoidArthritis').prop("checked", data.MedicalHistoryRheumatoidArthritis);
        $('#BackSpineNeckDisorder').prop("checked", data.MedicalHistoryBackSpineNeckDisorder);
        $('#MuscleDisorder').prop("checked", data.MedicalHistoryMuscleDisorder);
        $('#SciaticaNeuritis').prop("checked", data.MedicalHistorySciaticaNeuritis);
        $('#OtherBoneDisorder').prop("checked", data.MedicalHistoryOtherBoneDisorder);
        $('#AlcoholCounselingTreatment').prop("checked", data.MedicalHistoryAlcoholCounselingTreatment);
        $('#Alcoholism').prop("checked", data.MedicalHistoryAlcoholism);
        $('#DrugCounselingTreatment').prop("checked", data.MedicalHistoryDrugCounselingTreatment);
        $('#SuicideAttempt').prop("checked", data.MedicalHistorySuicideAttempt);
        $('#PanicAttack').prop("checked", data.MedicalHistoryPanicAttack);
        $('#Depression').prop("checked", data.MedicalHistoryDepression);
        $('#SeizuresNeurologicalDisorder').prop("checked", data.MedicalHistorySeizuresNeurologicalDisorder);
        $('#OtherMentalDisorders').prop("checked", data.MedicalHistoryOtherMentalDisorders);
        $('#AnyOtherDeseaseDisorderCondition').prop("checked", data.MedicalHistoryAnyOtherDeseaseDisorderCondition);
        if($('input:radio[name=CurrentlyPregnant][value=' + data.MedicalHistoryCurrentlyPregnant +']').length===0){
           $('input:radio[name=CurrentlyPregnant]').prop('checked', false);
           $('input:radio[name=CurrentlyPregnant].default').prop('checked', true);
        }
        else
           $($('input:radio[name=CurrentlyPregnant][value=' + data.MedicalHistoryCurrentlyPregnant +']')).prop('checked', true);
        $('#CurrentlyPregnant').data('oldValue', data.MedicalHistoryCurrentlyPregnant);
        $('#CurrentlyPregnant').val(data.MedicalHistoryCurrentlyPregnant);

        $('#DatePregnant').val(generalSupport.ToJavaScriptDateCustom(data.MedicalHistoryDatePregnant, generalSupport.DateFormat()));
        if($('input:radio[name=HaveSurgeriesDiagnosticsTestNotCompleted][value=' + data.MedicalHistoryHaveSurgeriesDiagnosticsTestNotCompleted +']').length===0){
           $('input:radio[name=HaveSurgeriesDiagnosticsTestNotCompleted]').prop('checked', false);
           $('input:radio[name=HaveSurgeriesDiagnosticsTestNotCompleted].default').prop('checked', true);
        }
        else
           $($('input:radio[name=HaveSurgeriesDiagnosticsTestNotCompleted][value=' + data.MedicalHistoryHaveSurgeriesDiagnosticsTestNotCompleted +']')).prop('checked', true);
        $('#HaveSurgeriesDiagnosticsTestNotCompleted').data('oldValue', data.MedicalHistoryHaveSurgeriesDiagnosticsTestNotCompleted);
        $('#HaveSurgeriesDiagnosticsTestNotCompleted').val(data.MedicalHistoryHaveSurgeriesDiagnosticsTestNotCompleted);

        if($('input:radio[name=HaveBeenWightChange][value=' + data.MedicalHistoryHaveBeenWightChange +']').length===0){
           $('input:radio[name=HaveBeenWightChange]').prop('checked', false);
           $('input:radio[name=HaveBeenWightChange].default').prop('checked', true);
        }
        else
           $($('input:radio[name=HaveBeenWightChange][value=' + data.MedicalHistoryHaveBeenWightChange +']')).prop('checked', true);
        $('#HaveBeenWightChange').data('oldValue', data.MedicalHistoryHaveBeenWightChange);
        $('#HaveBeenWightChange').val(data.MedicalHistoryHaveBeenWightChange);

        AutoNumeric.set('#WeightTwelveMonthsAgo', data.MedicalHistoryWeightTwelveMonthsAgo);
        AutoNumeric.set('#PresentWeight', data.MedicalHistoryPresentWeight);
        $('#ReasonForWeightChange').val(data.MedicalHistoryReasonForWeightChange);
        if($('input:radio[name=TreatedByAidsArcHIV][value=' + data.MedicalHistoryTreatedByAidsArcHIV +']').length===0){
           $('input:radio[name=TreatedByAidsArcHIV]').prop('checked', false);
           $('input:radio[name=TreatedByAidsArcHIV].default').prop('checked', true);
        }
        else
           $($('input:radio[name=TreatedByAidsArcHIV][value=' + data.MedicalHistoryTreatedByAidsArcHIV +']')).prop('checked', true);
        $('#TreatedByAidsArcHIV').data('oldValue', data.MedicalHistoryTreatedByAidsArcHIV);
        $('#TreatedByAidsArcHIV').val(data.MedicalHistoryTreatedByAidsArcHIV);

        if($('input:radio[name=ParticipateRegularExerciseProgram][value=' + data.MedicalHistoryParticipateRegularExerciseProgram +']').length===0){
           $('input:radio[name=ParticipateRegularExerciseProgram]').prop('checked', false);
           $('input:radio[name=ParticipateRegularExerciseProgram].default').prop('checked', true);
        }
        else
           $($('input:radio[name=ParticipateRegularExerciseProgram][value=' + data.MedicalHistoryParticipateRegularExerciseProgram +']')).prop('checked', true);
        $('#ParticipateRegularExerciseProgram').data('oldValue', data.MedicalHistoryParticipateRegularExerciseProgram);
        $('#ParticipateRegularExerciseProgram').val(data.MedicalHistoryParticipateRegularExerciseProgram);

        $('#MilitaryService').data('oldValue', data.MedicalHistoryMilitaryService);
        $('#MilitaryService').val(data.MedicalHistoryMilitaryService);
        if($('input:radio[name=UsedPrescriptionDrugs][value=' + data.MedicalHistoryUsedPrescriptionDrugs +']').length===0){
           $('input:radio[name=UsedPrescriptionDrugs]').prop('checked', false);
           $('input:radio[name=UsedPrescriptionDrugs].default').prop('checked', true);
        }
        else
           $($('input:radio[name=UsedPrescriptionDrugs][value=' + data.MedicalHistoryUsedPrescriptionDrugs +']')).prop('checked', true);
        $('#UsedPrescriptionDrugs').data('oldValue', data.MedicalHistoryUsedPrescriptionDrugs);
        $('#UsedPrescriptionDrugs').val(data.MedicalHistoryUsedPrescriptionDrugs);

        $('#NeverUsed').prop("checked", data.MedicalHistoryNeverUsed);
        $('#Cigarettes').prop("checked", data.MedicalHistoryCigarettes);
        $('#DateCigarettes').val(generalSupport.ToJavaScriptDateCustom(data.MedicalHistoryDateCigarettes, generalSupport.DateFormat()));
        AutoNumeric.set('#NumberPacksDaily', data.MedicalHistoryNumberPacksDaily);
        $('#Pipe').prop("checked", data.MedicalHistoryPipe);
        $('#DatePipe').val(generalSupport.ToJavaScriptDateCustom(data.MedicalHistoryDatePipe, generalSupport.DateFormat()));
        $('#Cigar').prop("checked", data.MedicalHistoryCigar);
        $('#DateCigar').val(generalSupport.ToJavaScriptDateCustom(data.MedicalHistoryDateCigar, generalSupport.DateFormat()));
        $('#NicotinePatch').prop("checked", data.MedicalHistoryNicotinePatch);
        $('#DateNicotinePatch').val(generalSupport.ToJavaScriptDateCustom(data.MedicalHistoryDateNicotinePatch, generalSupport.DateFormat()));
        $('#NicotineGum').prop("checked", data.MedicalHistoryNicotineGum);
        $('#DateNicotineGum').val(generalSupport.ToJavaScriptDateCustom(data.MedicalHistoryDateNicotineGum, generalSupport.DateFormat()));
        $('#ChewingTobacco').prop("checked", data.MedicalHistoryChewingTobacco);
        $('#DateChewingTobacco').val(generalSupport.ToJavaScriptDateCustom(data.MedicalHistoryDateChewingTobacco, generalSupport.DateFormat()));
        $('#OtherTobacco').prop("checked", data.MedicalHistoryOtherTobacco);
        $('#DateOtherTobacco').val(generalSupport.ToJavaScriptDateCustom(data.MedicalHistoryDateOtherTobacco, generalSupport.DateFormat()));
        $('#AdditionalInformation').val(data.MedicalHistoryAdditionalInformation);
        $('#DateQuestionnaire').val(generalSupport.ToJavaScriptDateCustom(data.MedicalHistoryDateQuestionnaire, generalSupport.DateFormat()));


        if (data.Telephone_Telephone !== null)
            $('#TelephoneTbl').bootstrapTable('load', data.Telephone_Telephone);
        if (data.MedicalTreatmentMedicalHistory_MedicalTreatmentMedicalHistory !== null)
            $('#MedicalTreatmentMedicalHistoryTbl').bootstrapTable('load', data.MedicalTreatmentMedicalHistory_MedicalTreatmentMedicalHistory);
        if (data.MedicalConditionsDetailsGrid_MedicalConditionsDetails !== null)
            $('#MedicalConditionsDetailsGridTbl').bootstrapTable('load', data.MedicalConditionsDetailsGrid_MedicalConditionsDetails);
        if (data.DetailsPrescriptionDrugs_DetailsPrescriptionDrugs !== null)
            $('#DetailsPrescriptionDrugsTbl').bootstrapTable('load', data.DetailsPrescriptionDrugs_DetailsPrescriptionDrugs);
        if (data.BiologicalFamilyCensusGrid_BiologicalFamilyCensus !== null)
            $('#BiologicalFamilyCensusGridTbl').bootstrapTable('load', data.BiologicalFamilyCensusGrid_BiologicalFamilyCensus);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#CountryCode', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#AreaCode', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#Number', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#BestTimeToCall', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#Extension', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#Type', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#MedicalTest', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#WeightTwelveMonthsAgo', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#PresentWeight', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#NumberPacksDaily', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#AgeLiving', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#AgeDeath', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#OnsetAgeHeartDisease', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#OnsetAgeKidneyDisease', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#OnsetAgeHighBloodPressure', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#OnsetAgeDiabetes', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#OnsetAgeMentalIllness', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#OnsetAgeCancer', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: 0
        });




        $('#DateLastSeenAPhysician_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateLastSeenAPhysician_group');
        $('#LastTreated_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#LastTreated_group');
        $('#LastEpisode_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#LastEpisode_group');
        $('#DatePregnant_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DatePregnant_group');
        $('#DateLastUse_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateLastUse_group');
        $('#DateCigarettes_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateCigarettes_group');
        $('#DatePipe_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DatePipe_group');
        $('#DateCigar_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateCigar_group');
        $('#DateNicotinePatch_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateNicotinePatch_group');
        $('#DateNicotineGum_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateNicotineGum_group');
        $('#DateChewingTobacco_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateChewingTobacco_group');
        $('#DateOtherTobacco_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateOtherTobacco_group');
        $('#DateQuestionnaire_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateQuestionnaire_group');


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               if (source == 'Initialization')
					         MedicalHistoryUWSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   MedicalHistoryUWSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        app.core.AsyncWebMethod("/fasi/dli/forms/MedicalHistoryUWActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), true,
            JSON.stringify({
                id: $('#MedicalHistoryUWFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
                if (data.d.Success)
                    $('#MedicalHistoryUWFormId').val(data.d.Data.Instance.InstanceFormId);
                    
                if (data.d.Success === true && data.d.Data.LookUps) {
                    data.d.Data.LookUps.forEach(function (elementSource) {
                        generalSupport.RenderLookUp(elementSource.Key, data.d.Data.Instance[elementSource.Key], 'Initialization', elementSource.Items);
                    });
                }
                


    $("#TelephoneTblPlaceHolder").replaceWith('<table id="TelephoneTbl"><caption>Teléfonos</caption></table>');
    MedicalHistoryUWSupport.TelephoneTblSetup($('#TelephoneTbl'));
    $("#MedicalTreatmentMedicalHistoryTblPlaceHolder").replaceWith('<table id="MedicalTreatmentMedicalHistoryTbl"><caption>Medicamentos (Rx)/ Tratamiento (tx)/ Terapia</caption></table>');
    MedicalHistoryUWSupport.MedicalTreatmentMedicalHistoryTblSetup($('#MedicalTreatmentMedicalHistoryTbl'));
    $("#MedicalConditionsDetailsGridTblPlaceHolder").replaceWith('<table id="MedicalConditionsDetailsGridTbl"><caption>Detalles</caption></table>');
    MedicalHistoryUWSupport.MedicalConditionsDetailsGridTblSetup($('#MedicalConditionsDetailsGridTbl'));
    $("#DetailsPrescriptionDrugsTblPlaceHolder").replaceWith('<table id="DetailsPrescriptionDrugsTbl"><caption>En caso afirmativo, indique el nombre, la forma, cantidad, frecuencia y duración del uso, y fecha de la última utilización</caption></table>');
    MedicalHistoryUWSupport.DetailsPrescriptionDrugsTblSetup($('#DetailsPrescriptionDrugsTbl'));
    $("#BiologicalFamilyCensusGridTblPlaceHolder").replaceWith('<table id="BiologicalFamilyCensusGridTbl"><caption>Censo biológico familiar</caption></table>');
    MedicalHistoryUWSupport.BiologicalFamilyCensusGridTblSetup($('#BiologicalFamilyCensusGridTbl'));





                MedicalHistoryUWSupport.ActionProcess(data, 'Initialization');

                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)                    
                    window.history.replaceState({}, null, '/fasi/dli/forms/' + location.pathname.substring(location.pathname.lastIndexOf("/") + 1) + '?id=' + $('#MedicalHistoryUWFormId').val());
 
              
          

            });
    };




    this.ControlActions =   function () {

        $('input:radio[name=HaveAPersonalPhysician]').change(function () {
         if ($('input:radio[name=HaveAPersonalPhysician]:checked').val() !== null) {
           app.core.AsyncWebMethod("/fasi/dli/forms/MedicalHistoryUWActions.aspx/HaveAPersonalPhysicianChange", false,
                 JSON.stringify({
                     instance: MedicalHistoryUWSupport.InputToObject()
                 }),
                 function (data) {
                     MedicalHistoryUWSupport.ActionProcess(data, 'HaveAPersonalPhysicianChange');
             });
      }          
    });
        $('input:radio[name=CurrentlyPregnant]').change(function () {
         if ($('input:radio[name=CurrentlyPregnant]:checked').val() !== null) {
           app.core.AsyncWebMethod("/fasi/dli/forms/MedicalHistoryUWActions.aspx/CurrentlyPregnantChange", false,
                 JSON.stringify({
                     instance: MedicalHistoryUWSupport.InputToObject()
                 }),
                 function (data) {
                     MedicalHistoryUWSupport.ActionProcess(data, 'CurrentlyPregnantChange');
             });
      }          
    });
        $('input:radio[name=HaveBeenWightChange]').change(function () {
         if ($('input:radio[name=HaveBeenWightChange]:checked').val() !== null) {
           app.core.AsyncWebMethod("/fasi/dli/forms/MedicalHistoryUWActions.aspx/HaveBeenWightChangeChange", false,
                 JSON.stringify({
                     instance: MedicalHistoryUWSupport.InputToObject()
                 }),
                 function (data) {
                     MedicalHistoryUWSupport.ActionProcess(data, 'HaveBeenWightChangeChange');
             });
      }          
    });
        $('input:radio[name=UsedPrescriptionDrugs]').change(function () {
         if ($('input:radio[name=UsedPrescriptionDrugs]:checked').val() !== null) {
           app.core.AsyncWebMethod("/fasi/dli/forms/MedicalHistoryUWActions.aspx/UsedPrescriptionDrugsChange", false,
                 JSON.stringify({
                     instance: MedicalHistoryUWSupport.InputToObject()
                 }),
                 function (data) {
                     MedicalHistoryUWSupport.ActionProcess(data, 'UsedPrescriptionDrugsChange');
             });
      }          
    });
        $('#NeverUsed').change(function () {
         if ($('#NeverUsed').is(':checked') !== null && $('#NeverUsed').is(':checked') !== $('#NeverUsed').data('oldValue')){         
             $('#NeverUsed').data('oldValue', $('#NeverUsed').is(':checked') );
             
             app.core.AsyncWebMethod("/fasi/dli/forms/MedicalHistoryUWActions.aspx/NeverUsedChange", false,
                 JSON.stringify({
                     instance: MedicalHistoryUWSupport.InputToObject()
                 }),
                 function (data) {
                     MedicalHistoryUWSupport.ActionProcess(data, 'NeverUsedChange');
             });
         }
        });
        $('#Cigarettes').change(function () {
         if ($('#Cigarettes').is(':checked') !== null && $('#Cigarettes').is(':checked') !== $('#Cigarettes').data('oldValue')){         
             $('#Cigarettes').data('oldValue', $('#Cigarettes').is(':checked') );
             
             app.core.AsyncWebMethod("/fasi/dli/forms/MedicalHistoryUWActions.aspx/CigarettesChange", false,
                 JSON.stringify({
                     instance: MedicalHistoryUWSupport.InputToObject()
                 }),
                 function (data) {
                     MedicalHistoryUWSupport.ActionProcess(data, 'CigarettesChange');
             });
         }
        });
        $('#Pipe').change(function () {
         if ($('#Pipe').is(':checked') !== null && $('#Pipe').is(':checked') !== $('#Pipe').data('oldValue')){         
             $('#Pipe').data('oldValue', $('#Pipe').is(':checked') );
             
             app.core.AsyncWebMethod("/fasi/dli/forms/MedicalHistoryUWActions.aspx/PipeChange", false,
                 JSON.stringify({
                     instance: MedicalHistoryUWSupport.InputToObject()
                 }),
                 function (data) {
                     MedicalHistoryUWSupport.ActionProcess(data, 'PipeChange');
             });
         }
        });
        $('#Cigar').change(function () {
         if ($('#Cigar').is(':checked') !== null && $('#Cigar').is(':checked') !== $('#Cigar').data('oldValue')){         
             $('#Cigar').data('oldValue', $('#Cigar').is(':checked') );
             
             app.core.AsyncWebMethod("/fasi/dli/forms/MedicalHistoryUWActions.aspx/CigarChange", false,
                 JSON.stringify({
                     instance: MedicalHistoryUWSupport.InputToObject()
                 }),
                 function (data) {
                     MedicalHistoryUWSupport.ActionProcess(data, 'CigarChange');
             });
         }
        });
        $('#NicotinePatch').change(function () {
         if ($('#NicotinePatch').is(':checked') !== null && $('#NicotinePatch').is(':checked') !== $('#NicotinePatch').data('oldValue')){         
             $('#NicotinePatch').data('oldValue', $('#NicotinePatch').is(':checked') );
             
             app.core.AsyncWebMethod("/fasi/dli/forms/MedicalHistoryUWActions.aspx/NicotinePatchChange", false,
                 JSON.stringify({
                     instance: MedicalHistoryUWSupport.InputToObject()
                 }),
                 function (data) {
                     MedicalHistoryUWSupport.ActionProcess(data, 'NicotinePatchChange');
             });
         }
        });
        $('#NicotineGum').change(function () {
         if ($('#NicotineGum').is(':checked') !== null && $('#NicotineGum').is(':checked') !== $('#NicotineGum').data('oldValue')){         
             $('#NicotineGum').data('oldValue', $('#NicotineGum').is(':checked') );
             
             app.core.AsyncWebMethod("/fasi/dli/forms/MedicalHistoryUWActions.aspx/NicotineGumChange", false,
                 JSON.stringify({
                     instance: MedicalHistoryUWSupport.InputToObject()
                 }),
                 function (data) {
                     MedicalHistoryUWSupport.ActionProcess(data, 'NicotineGumChange');
             });
         }
        });
        $('#ChewingTobacco').change(function () {
         if ($('#ChewingTobacco').is(':checked') !== null && $('#ChewingTobacco').is(':checked') !== $('#ChewingTobacco').data('oldValue')){         
             $('#ChewingTobacco').data('oldValue', $('#ChewingTobacco').is(':checked') );
             
             app.core.AsyncWebMethod("/fasi/dli/forms/MedicalHistoryUWActions.aspx/ChewingTobaccoChange", false,
                 JSON.stringify({
                     instance: MedicalHistoryUWSupport.InputToObject()
                 }),
                 function (data) {
                     MedicalHistoryUWSupport.ActionProcess(data, 'ChewingTobaccoChange');
             });
         }
        });
        $('#OtherTobacco').change(function () {
         if ($('#OtherTobacco').is(':checked') !== null && $('#OtherTobacco').is(':checked') !== $('#OtherTobacco').data('oldValue')){         
             $('#OtherTobacco').data('oldValue', $('#OtherTobacco').is(':checked') );
             
             app.core.AsyncWebMethod("/fasi/dli/forms/MedicalHistoryUWActions.aspx/OtherTobaccoChange", false,
                 JSON.stringify({
                     instance: MedicalHistoryUWSupport.InputToObject()
                 }),
                 function (data) {
                     MedicalHistoryUWSupport.ActionProcess(data, 'OtherTobaccoChange');
             });
         }
        });
        $('#SaveDraft').click(function (event) {
                var formInstance = $("#MedicalHistoryUWMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#SaveDraft'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/MedicalHistoryUWActions.aspx/SaveDraftClick", false,
                          JSON.stringify({
                                        instance: MedicalHistoryUWSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    MedicalHistoryUWSupport.ActionProcess(data, 'SaveDraftClick');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('#Submit').click(function (event) {
                var formInstance = $("#MedicalHistoryUWMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#Submit'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/MedicalHistoryUWActions.aspx/SubmitClick", false,
                          JSON.stringify({
                                        instance: MedicalHistoryUWSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    MedicalHistoryUWSupport.ActionProcess(data, 'SubmitClick');
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


        $("#MedicalHistoryUWMainForm").validate({
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
                HaveAPersonalPhysician: {
                },
                PhysicianFirstName: {
                    required: true,
                    maxlength: 30
                },
                PhysicianLastName: {
                    required: true,
                    maxlength: 30
                },
                Address: {
                    required: true,
                    maxlength: 30
                },
                State: {
                    required: true,
                    maxlength: 15
                },
                City: {
                    required: true,
                    maxlength: 30
                },
                Zip: {
                    required: true,
                    maxlength: 15
                },
                DateLastSeenAPhysician: {
                    required: true,
                    DatePicker: true
                },
                ReasonLastSeenPhysician: {
                    required: true,
                    maxlength: 0
                },
                CurrentlyPregnant: {
                },
                DatePregnant: {
                    required: true,
                    DatePicker: true
                },
                HaveSurgeriesDiagnosticsTestNotCompleted: {
                },
                HaveBeenWightChange: {
                },
                WeightTwelveMonthsAgo: {
                    AutoNumericRequired: true,
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 9999
                },
                PresentWeight: {
                    AutoNumericRequired: true,
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 9999
                },
                ReasonForWeightChange: {
                    required: true,
                    maxlength: 40
                },
                TreatedByAidsArcHIV: {
                },
                ParticipateRegularExerciseProgram: {
                },
                MilitaryService: {
                },
                UsedPrescriptionDrugs: {
                },
                DateCigarettes: {
                    required: true,
                    DatePicker: true
                },
                NumberPacksDaily: {
                    AutoNumericRequired: true,
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 99
                },
                DatePipe: {
                    required: true,
                    DatePicker: true
                },
                DateCigar: {
                    required: true,
                    DatePicker: true
                },
                DateNicotinePatch: {
                    required: true,
                    DatePicker: true
                },
                DateNicotineGum: {
                    required: true,
                    DatePicker: true
                },
                DateChewingTobacco: {
                    required: true,
                    DatePicker: true
                },
                DateOtherTobacco: {
                    required: true,
                    DatePicker: true
                },
                AdditionalInformation: {
                    maxlength: 0
                },
                DateQuestionnaire: {
                    required: true,
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
                HaveAPersonalPhysician: {
                },
                PhysicianFirstName: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 30 caracteres máximo'
                },
                PhysicianLastName: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 30 caracteres máximo'
                },
                Address: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 30 caracteres máximo'
                },
                State: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                City: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 30 caracteres máximo'
                },
                Zip: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                DateLastSeenAPhysician: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                ReasonLastSeenPhysician: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 0 caracteres máximo'
                },
                CurrentlyPregnant: {
                },
                DatePregnant: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                HaveSurgeriesDiagnosticsTestNotCompleted: {
                },
                HaveBeenWightChange: {
                },
                WeightTwelveMonthsAgo: {
                    AutoNumericRequired: 'El campo es requerido',
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 9999'
                },
                PresentWeight: {
                    AutoNumericRequired: 'El campo es requerido',
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 9999'
                },
                ReasonForWeightChange: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 40 caracteres máximo'
                },
                TreatedByAidsArcHIV: {
                },
                ParticipateRegularExerciseProgram: {
                },
                MilitaryService: {
                },
                UsedPrescriptionDrugs: {
                },
                DateCigarettes: {
                    required: 'Entrada obligatoria',
                    DatePicker: 'La fecha indicada no es válida'
                },
                NumberPacksDaily: {
                    AutoNumericRequired: 'El campo es requerido',
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99'
                },
                DatePipe: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                DateCigar: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                DateNicotinePatch: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                DateNicotineGum: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                DateChewingTobacco: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                DateOtherTobacco: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                AdditionalInformation: {
                    maxlength: 'El campo permite 0 caracteres máximo'
                },
                DateQuestionnaire: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                }
            }
        });
        $("#TelephoneEditForm").validate().destroy();
        $("#TelephoneEditForm").validate({
            rules: {
                CountryCode: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999,
                    required: true
                },
                AreaCode: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999,
                    required: true
                },
                Number: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 9999999,
                    required: true
                },
                BestTimeToCall: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 99,
                    required: true
                },
                Extension: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 99999
                },
                Sequence: {
                    maxlength: 2
                }

            },
            messages: {
                CountryCode: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999',
                    required: 'El campo es requerido'
                },
                AreaCode: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999',
                    required: 'El campo es requerido'
                },
                Number: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 9999999',
                    required: 'El campo es requerido'
                },
                BestTimeToCall: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99',
                    required: 'El campo es requerido'
                },
                Extension: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999'
                },
                Sequence: {
                    maxlength: 'El campo permite 2 caracteres máximo'
                }

            }
        });
        $("#MedicalTreatmentMedicalHistoryEditForm").validate().destroy();
        $("#MedicalTreatmentMedicalHistoryEditForm").validate({
            rules: {
                NameOfMedication: {
                    required: true,
                    maxlength: 15
                },
                Dosage: {
                    required: true,
                    maxlength: 15
                },
                DatePeriod: {
                    maxlength: 10
                },
                Type: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 99999,
                    required: true
                },
                ExplainTreatment: {
                    required: true,
                    maxlength: 15
                },
                id: {
                    maxlength: 15
                }

            },
            messages: {
                NameOfMedication: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                Dosage: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                DatePeriod: {
                    maxlength: 'El campo permite 10 caracteres máximo'
                },
                Type: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999',
                    required: 'El campo es requerido'
                },
                ExplainTreatment: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                id: {
                    maxlength: 'El campo permite 15 caracteres máximo'
                }

            }
        });
        $("#MedicalConditionsDetailsGridEditForm").validate().destroy();
        $("#MedicalConditionsDetailsGridEditForm").validate({
            rules: {
                MedicalTest: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 99999
                },
                PhysicianName: {
                    maxlength: 30
                },
                LastTreated: {
                    DatePicker: true
                },
                LastEpisode: {
                    DatePicker: true
                },
                ReasonLastSeen: {
                    maxlength: 15
                },
                MedicationTreatmentTherapy: {
                    maxlength: 20
                },
                TestsTypeDateResults: {
                    maxlength: 20
                },
                AdditionalInformationGrid: {
                    maxlength: 20
                },
                IdCondicion: {
                    maxlength: 15
                }

            },
            messages: {
                MedicalTest: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999'
                },
                PhysicianName: {
                    maxlength: 'El campo permite 30 caracteres máximo'
                },
                LastTreated: {
                    DatePicker: 'La fecha indicada no es válida'
                },
                LastEpisode: {
                    DatePicker: 'La fecha indicada no es válida'
                },
                ReasonLastSeen: {
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                MedicationTreatmentTherapy: {
                    maxlength: 'El campo permite 20 caracteres máximo'
                },
                TestsTypeDateResults: {
                    maxlength: 'El campo permite 20 caracteres máximo'
                },
                AdditionalInformationGrid: {
                    maxlength: 'El campo permite 20 caracteres máximo'
                },
                IdCondicion: {
                    maxlength: 'El campo permite 15 caracteres máximo'
                }

            }
        });
        $("#DetailsPrescriptionDrugsEditForm").validate().destroy();
        $("#DetailsPrescriptionDrugsEditForm").validate({
            rules: {
                DrugName: {
                    required: true,
                    maxlength: 15
                },
                Form: {
                    required: true,
                    maxlength: 15
                },
                Amount: {
                    required: true,
                    maxlength: 15
                },
                Frequency: {
                    required: true                },
                LengthUse: {
                    required: true,
                    maxlength: 15
                },
                DateLastUse: {
                    required: true,
                    DatePicker: true
                },
                IdPrescripcion: {
                    maxlength: 15
                }

            },
            messages: {
                DrugName: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                Form: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                Amount: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                Frequency: {
                    required: 'El campo es requerido'                },
                LengthUse: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                DateLastUse: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                IdPrescripcion: {
                    maxlength: 'El campo permite 15 caracteres máximo'
                }

            }
        });
        $("#BiologicalFamilyCensusGridEditForm").validate().destroy();
        $("#BiologicalFamilyCensusGridEditForm").validate({
            rules: {
                Relation: {
                },
                Gender: {
                },
                AgeLiving: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999
                },
                AgeDeath: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999
                },
                CauseDeath: {
                    maxlength: 15
                },
                OnsetAgeHeartDisease: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999
                },
                OnsetAgeKidneyDisease: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999
                },
                OnsetAgeHighBloodPressure: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999
                },
                OnsetAgeDiabetes: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999
                },
                OnsetAgeMentalIllness: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999
                },
                OnsetAgeCancer: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999
                },
                IdCenso: {
                    maxlength: 15
                }

            },
            messages: {
                Relation: {
                },
                Gender: {
                },
                AgeLiving: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999'
                },
                AgeDeath: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999'
                },
                CauseDeath: {
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                OnsetAgeHeartDisease: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999'
                },
                OnsetAgeKidneyDisease: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999'
                },
                OnsetAgeHighBloodPressure: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999'
                },
                OnsetAgeDiabetes: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999'
                },
                OnsetAgeMentalIllness: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999'
                },
                OnsetAgeCancer: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999'
                },
                IdCenso: {
                    maxlength: 'El campo permite 15 caracteres máximo'
                }

            }
        });

    };
    this.LookUpForFrequencyFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#Frequency>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForRelationFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#Relation>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForGenderFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#Gender>option[value='" + value + "']").text();
        }
        return result;
    };

    this.TelephoneTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'Sequence',
            toolbar: '#Telephonetoolbar',
            columns: [{
                field: 'selected',
                checkbox: true,
                formatter: 'MedicalHistoryUWSupport.selected_Formatter'
            }, {
                field: 'CountryCode',
                title: 'País',
                formatter: 'MedicalHistoryUWSupport.CountryCode_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'AreaCode',
                title: 'Código de área',
                formatter: 'MedicalHistoryUWSupport.AreaCode_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'Number',
                title: 'Número',
                events: 'TelephoneActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'BestTimeToCall',
                title: 'Mejor hora para llamar',
                formatter: 'MedicalHistoryUWSupport.BestTimeToCall_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'Extension',
                title: 'Extensión',
                formatter: 'MedicalHistoryUWSupport.Extension_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'Sequence',
                title: 'Sequence',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });


        $('#TelephoneTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#TelephoneTbl');
            $('#TelephoneRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#TelephoneRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#TelephoneTbl').bootstrapTable('getSelections'), function (row) {		
                MedicalHistoryUWSupport.TelephoneRowToInput(row);
                
                
                return row.Sequence;
            });
            
          $('#TelephoneTbl').bootstrapTable('remove', {
                field: 'Sequence',
                values: ids
           });

            $('#TelephoneRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#TelephoneCreateBtn').click(function () {
            var formInstance = $("#TelephoneEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            MedicalHistoryUWSupport.TelephoneShowModal($('#TelephonePopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#TelephonePopup').find('#TelephoneSaveBtn').click(function () {
            var formInstance = $("#TelephoneEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#TelephonePopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#TelephoneSaveBtn').html();
                $('#TelephoneSaveBtn').html('Procesando...');
                $('#TelephoneSaveBtn').prop('disabled', true);

                MedicalHistoryUWSupport.currentRow.CountryCode = generalSupport.NumericValue('#CountryCode', 0, 999);
                MedicalHistoryUWSupport.currentRow.AreaCode = generalSupport.NumericValue('#AreaCode', 0, 999);
                MedicalHistoryUWSupport.currentRow.Number = generalSupport.NumericValue('#Number', 0, 9999999);
                MedicalHistoryUWSupport.currentRow.BestTimeToCall = generalSupport.NumericValue('#BestTimeToCall', 0, 99);
                MedicalHistoryUWSupport.currentRow.Extension = generalSupport.NumericValue('#Extension', 0, 99999);
                MedicalHistoryUWSupport.currentRow.Sequence = parseInt(0 + $('#Sequence').val(), 10);

                $('#TelephoneSaveBtn').prop('disabled', false);
                $('#TelephoneSaveBtn').html(caption);

                if (wm === 'Update') {
                    $('#TelephoneTbl').bootstrapTable('updateByUniqueId', { id: MedicalHistoryUWSupport.currentRow.Sequence, row: MedicalHistoryUWSupport.currentRow });
                    $modal.modal('hide');
                }
                else {                    
                    $('#TelephoneTbl').bootstrapTable('append', MedicalHistoryUWSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.TelephoneShowModal = function (md, title, row) {
        var formInstance = $("#TelephoneEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { CountryCode: 0, AreaCode: 0, Number: 0, BestTimeToCall: 0, Extension: 0, Sequence: 0 };

        md.data('id', row.Sequence);
        md.find('.modal-title').text(title);

        MedicalHistoryUWSupport.TelephoneRowToInput(row);


        md.appendTo("body");
        md.modal('show');
    };

    this.TelephoneRowToInput = function (row) {
        MedicalHistoryUWSupport.currentRow = row;
        AutoNumeric.set('#CountryCode', row.CountryCode);
        AutoNumeric.set('#AreaCode', row.AreaCode);
        AutoNumeric.set('#Number', row.Number);
        AutoNumeric.set('#BestTimeToCall', row.BestTimeToCall);
        AutoNumeric.set('#Extension', row.Extension);
        $('#Sequence').val(row.Sequence);

    };
    this.MedicalTreatmentMedicalHistoryTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'id',
            toolbar: '#MedicalTreatmentMedicalHistorytoolbar',
            columns: [{
                field: 'selected',
                checkbox: true,
                formatter: 'MedicalHistoryUWSupport.selected_Formatter'
            }, {
                field: 'NameOfMedication',
                title: 'Medicamento',
                sortable: false,
                halign: 'center'
            }, {
                field: 'Dosage',
                title: 'Dósis',
                sortable: false,
                halign: 'center'
            }, {
                field: 'DatePeriod',
                title: 'Fecha',
                sortable: false,
                halign: 'center'
            }, {
                field: 'Type',
                title: 'Tipo',
                formatter: 'MedicalHistoryUWSupport.Type_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'ExplainTreatment',
                title: 'Tratamiento',
                sortable: false,
                halign: 'center'
            }, {
                field: 'id',
                title: 'id',
                events: 'MedicalTreatmentMedicalHistoryActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });


        $('#MedicalTreatmentMedicalHistoryTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#MedicalTreatmentMedicalHistoryTbl');
            $('#MedicalTreatmentMedicalHistoryRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#MedicalTreatmentMedicalHistoryRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#MedicalTreatmentMedicalHistoryTbl').bootstrapTable('getSelections'), function (row) {		
                MedicalHistoryUWSupport.MedicalTreatmentMedicalHistoryRowToInput(row);
                
                
                return row.id;
            });
            
          $('#MedicalTreatmentMedicalHistoryTbl').bootstrapTable('remove', {
                field: 'id',
                values: ids
           });

            $('#MedicalTreatmentMedicalHistoryRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#MedicalTreatmentMedicalHistoryCreateBtn').click(function () {
            var formInstance = $("#MedicalTreatmentMedicalHistoryEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            MedicalHistoryUWSupport.MedicalTreatmentMedicalHistoryShowModal($('#MedicalTreatmentMedicalHistoryPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#MedicalTreatmentMedicalHistoryPopup').find('#MedicalTreatmentMedicalHistorySaveBtn').click(function () {
            var formInstance = $("#MedicalTreatmentMedicalHistoryEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#MedicalTreatmentMedicalHistoryPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#MedicalTreatmentMedicalHistorySaveBtn').html();
                $('#MedicalTreatmentMedicalHistorySaveBtn').html('Procesando...');
                $('#MedicalTreatmentMedicalHistorySaveBtn').prop('disabled', true);

                MedicalHistoryUWSupport.currentRow.NameOfMedication = $('#NameOfMedication').val();
                MedicalHistoryUWSupport.currentRow.Dosage = $('#Dosage').val();
                MedicalHistoryUWSupport.currentRow.DatePeriod = $('#DatePeriod').val();
                MedicalHistoryUWSupport.currentRow.Type = generalSupport.NumericValue('#Type', 0, 99999);
                MedicalHistoryUWSupport.currentRow.ExplainTreatment = $('#ExplainTreatment').val();
                MedicalHistoryUWSupport.currentRow.id = parseInt(0 + $('#id').val(), 10);

                $('#MedicalTreatmentMedicalHistorySaveBtn').prop('disabled', false);
                $('#MedicalTreatmentMedicalHistorySaveBtn').html(caption);

                if (wm === 'Update') {
                    $('#MedicalTreatmentMedicalHistoryTbl').bootstrapTable('updateByUniqueId', { id: MedicalHistoryUWSupport.currentRow.id, row: MedicalHistoryUWSupport.currentRow });
                    $modal.modal('hide');
                }
                else {                    
                    $('#MedicalTreatmentMedicalHistoryTbl').bootstrapTable('append', MedicalHistoryUWSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.MedicalTreatmentMedicalHistoryShowModal = function (md, title, row) {
        var formInstance = $("#MedicalTreatmentMedicalHistoryEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { NameOfMedication: '', Dosage: '', DatePeriod: '', Type: 0, ExplainTreatment: '', id: 0 };

        md.data('id', row.id);
        md.find('.modal-title').text(title);

        MedicalHistoryUWSupport.MedicalTreatmentMedicalHistoryRowToInput(row);


        md.appendTo("body");
        md.modal('show');
    };

    this.MedicalTreatmentMedicalHistoryRowToInput = function (row) {
        MedicalHistoryUWSupport.currentRow = row;
        $('#NameOfMedication').val(row.NameOfMedication);
        $('#Dosage').val(row.Dosage);
        $('#DatePeriod').val(row.DatePeriod);
        AutoNumeric.set('#Type', row.Type);
        $('#ExplainTreatment').val(row.ExplainTreatment);
        $('#id').val(row.id);

    };
    this.MedicalConditionsDetailsGridTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'Id',
            toolbar: '#MedicalConditionsDetailsGridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true,
                formatter: 'MedicalHistoryUWSupport.selected_Formatter'
            }, {
                field: 'MedicalTest',
                title: 'Identificador',
                formatter: 'MedicalHistoryUWSupport.MedicalTest_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'PhysicianName',
                title: 'Nombre médico',
                sortable: false,
                halign: 'center'
            }, {
                field: 'StillTreatment',
                title: '¿Bajo tratamiento?',
                formatter: 'MedicalHistoryUWSupport.StillTreatment_IsCheck',
                sortable: false,
                halign: 'center'
            }, {
                field: 'LastTreated',
                title: 'Fecha tratamiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'LastEpisode',
                title: 'Última vez',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'ReasonLastSeen',
                title: 'Razón',
                sortable: false,
                halign: 'center'
            }, {
                field: 'MedicationTreatmentTherapy',
                title: 'Medicación',
                sortable: false,
                halign: 'center'
            }, {
                field: 'TestsTypeDateResults',
                title: 'Fecha diagnóstico',
                sortable: false,
                halign: 'center'
            }, {
                field: 'AdditionalInformation',
                title: 'Información adicional',
                sortable: false,
                halign: 'center'
            }, {
                field: 'Id',
                title: 'Id',
                events: 'MedicalConditionsDetailsGridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });


        $('#MedicalConditionsDetailsGridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#MedicalConditionsDetailsGridTbl');
            $('#MedicalConditionsDetailsGridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#MedicalConditionsDetailsGridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#MedicalConditionsDetailsGridTbl').bootstrapTable('getSelections'), function (row) {		
                MedicalHistoryUWSupport.MedicalConditionsDetailsGridRowToInput(row);
                
                
                return row.Id;
            });
            
          $('#MedicalConditionsDetailsGridTbl').bootstrapTable('remove', {
                field: 'Id',
                values: ids
           });

            $('#MedicalConditionsDetailsGridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#MedicalConditionsDetailsGridCreateBtn').click(function () {
            var formInstance = $("#MedicalConditionsDetailsGridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            MedicalHistoryUWSupport.MedicalConditionsDetailsGridShowModal($('#MedicalConditionsDetailsGridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#MedicalConditionsDetailsGridPopup').find('#MedicalConditionsDetailsGridSaveBtn').click(function () {
            var formInstance = $("#MedicalConditionsDetailsGridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#MedicalConditionsDetailsGridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#MedicalConditionsDetailsGridSaveBtn').html();
                $('#MedicalConditionsDetailsGridSaveBtn').html('Procesando...');
                $('#MedicalConditionsDetailsGridSaveBtn').prop('disabled', true);

                MedicalHistoryUWSupport.currentRow.MedicalTest = generalSupport.NumericValue('#MedicalTest', 0, 99999);
                MedicalHistoryUWSupport.currentRow.PhysicianName = $('#PhysicianName').val();
                MedicalHistoryUWSupport.currentRow.StillTreatment = $('#StillTreatment').is(':checked');
                MedicalHistoryUWSupport.currentRow.LastTreated = generalSupport.DatePickerValue('#LastTreated');
                MedicalHistoryUWSupport.currentRow.LastEpisode = generalSupport.DatePickerValue('#LastEpisode');
                MedicalHistoryUWSupport.currentRow.ReasonLastSeen = $('#ReasonLastSeen').val();
                MedicalHistoryUWSupport.currentRow.MedicationTreatmentTherapy = $('#MedicationTreatmentTherapy').val();
                MedicalHistoryUWSupport.currentRow.TestsTypeDateResults = $('#TestsTypeDateResults').val();
                MedicalHistoryUWSupport.currentRow.AdditionalInformation = $('#AdditionalInformationGrid').val();
                MedicalHistoryUWSupport.currentRow.Id = parseInt(0 + $('#IdCondicion').val(), 10);

                $('#MedicalConditionsDetailsGridSaveBtn').prop('disabled', false);
                $('#MedicalConditionsDetailsGridSaveBtn').html(caption);

                if (wm === 'Update') {
                    $('#MedicalConditionsDetailsGridTbl').bootstrapTable('updateByUniqueId', { id: MedicalHistoryUWSupport.currentRow.Id, row: MedicalHistoryUWSupport.currentRow });
                    $modal.modal('hide');
                }
                else {                    
                    $('#MedicalConditionsDetailsGridTbl').bootstrapTable('append', MedicalHistoryUWSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.MedicalConditionsDetailsGridShowModal = function (md, title, row) {
        var formInstance = $("#MedicalConditionsDetailsGridEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { MedicalTest: 0, PhysicianName: '', StillTreatment: null, LastTreated: null, LastEpisode: null, ReasonLastSeen: '', MedicationTreatmentTherapy: '', TestsTypeDateResults: '', AdditionalInformation: '', Id: 0 };

        md.data('id', row.Id);
        md.find('.modal-title').text(title);

        MedicalHistoryUWSupport.MedicalConditionsDetailsGridRowToInput(row);


        md.appendTo("body");
        md.modal('show');
    };

    this.MedicalConditionsDetailsGridRowToInput = function (row) {
        MedicalHistoryUWSupport.currentRow = row;
        AutoNumeric.set('#MedicalTest', row.MedicalTest);
        $('#PhysicianName').val(row.PhysicianName);
        $('#StillTreatment').prop("checked", row.StillTreatment);
        $('#LastTreated').val(generalSupport.ToJavaScriptDateCustom(row.LastTreated, generalSupport.DateFormat()));
        $('#LastEpisode').val(generalSupport.ToJavaScriptDateCustom(row.LastEpisode, generalSupport.DateFormat()));
        $('#ReasonLastSeen').val(row.ReasonLastSeen);
        $('#MedicationTreatmentTherapy').val(row.MedicationTreatmentTherapy);
        $('#TestsTypeDateResults').val(row.TestsTypeDateResults);
        $('#AdditionalInformationGrid').val(row.AdditionalInformation);
        $('#IdCondicion').val(row.Id);

    };
    this.DetailsPrescriptionDrugsTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'Id',
            toolbar: '#DetailsPrescriptionDrugstoolbar',
            columns: [{
                field: 'selected',
                checkbox: true,
                formatter: 'MedicalHistoryUWSupport.selected_Formatter'
            }, {
                field: 'DrugName',
                title: 'Nombre de la droga',
                sortable: false,
                halign: 'center'
            }, {
                field: 'Form',
                title: '',
                sortable: false,
                halign: 'center'
            }, {
                field: 'Amount',
                title: 'Cantidad',
                sortable: false,
                halign: 'center'
            }, {
                field: 'Frequency',
                title: 'Frecuencia',
                formatter: 'MedicalHistoryUWSupport.LookUpForFrequencyFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'LengthUse',
                title: 'Tiempo de uso',
                sortable: false,
                halign: 'center'
            }, {
                field: 'DateLastUse',
                title: 'Fecha último consumo',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'Id',
                title: 'Id',
                events: 'DetailsPrescriptionDrugsActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });


        $('#DetailsPrescriptionDrugsTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#DetailsPrescriptionDrugsTbl');
            $('#DetailsPrescriptionDrugsRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#DetailsPrescriptionDrugsRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#DetailsPrescriptionDrugsTbl').bootstrapTable('getSelections'), function (row) {		
                MedicalHistoryUWSupport.DetailsPrescriptionDrugsRowToInput(row);
                
                
                return row.Id;
            });
            
          $('#DetailsPrescriptionDrugsTbl').bootstrapTable('remove', {
                field: 'Id',
                values: ids
           });

            $('#DetailsPrescriptionDrugsRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#DetailsPrescriptionDrugsCreateBtn').click(function () {
            var formInstance = $("#DetailsPrescriptionDrugsEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            MedicalHistoryUWSupport.DetailsPrescriptionDrugsShowModal($('#DetailsPrescriptionDrugsPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#DetailsPrescriptionDrugsPopup').find('#DetailsPrescriptionDrugsSaveBtn').click(function () {
            var formInstance = $("#DetailsPrescriptionDrugsEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#DetailsPrescriptionDrugsPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#DetailsPrescriptionDrugsSaveBtn').html();
                $('#DetailsPrescriptionDrugsSaveBtn').html('Procesando...');
                $('#DetailsPrescriptionDrugsSaveBtn').prop('disabled', true);

                MedicalHistoryUWSupport.currentRow.DrugName = $('#DrugName').val();
                MedicalHistoryUWSupport.currentRow.Form = $('#Form').val();
                MedicalHistoryUWSupport.currentRow.Amount = $('#Amount').val();
                MedicalHistoryUWSupport.currentRow.Frequency = parseInt(0 + $('#Frequency').val(), 10);
                MedicalHistoryUWSupport.currentRow.LengthUse = $('#LengthUse').val();
                MedicalHistoryUWSupport.currentRow.DateLastUse = generalSupport.DatePickerValue('#DateLastUse');
                MedicalHistoryUWSupport.currentRow.Id = parseInt(0 + $('#IdPrescripcion').val(), 10);

                $('#DetailsPrescriptionDrugsSaveBtn').prop('disabled', false);
                $('#DetailsPrescriptionDrugsSaveBtn').html(caption);

                if (wm === 'Update') {
                    $('#DetailsPrescriptionDrugsTbl').bootstrapTable('updateByUniqueId', { id: MedicalHistoryUWSupport.currentRow.Id, row: MedicalHistoryUWSupport.currentRow });
                    $modal.modal('hide');
                }
                else {                    
                    $('#DetailsPrescriptionDrugsTbl').bootstrapTable('append', MedicalHistoryUWSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.DetailsPrescriptionDrugsShowModal = function (md, title, row) {
        var formInstance = $("#DetailsPrescriptionDrugsEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { DrugName: '', Form: '', Amount: '', Frequency: 0, LengthUse: '', DateLastUse: null, Id: 0 };

        md.data('id', row.Id);
        md.find('.modal-title').text(title);

        MedicalHistoryUWSupport.DetailsPrescriptionDrugsRowToInput(row);


        md.appendTo("body");
        md.modal('show');
    };

    this.DetailsPrescriptionDrugsRowToInput = function (row) {
        MedicalHistoryUWSupport.currentRow = row;
        $('#DrugName').val(row.DrugName);
        $('#Form').val(row.Form);
        $('#Amount').val(row.Amount);
        $('#Frequency').val(row.Frequency);
        $('#Frequency').trigger('change');
        $('#LengthUse').val(row.LengthUse);
        $('#DateLastUse').val(generalSupport.ToJavaScriptDateCustom(row.DateLastUse, generalSupport.DateFormat()));
        $('#IdPrescripcion').val(row.Id);

    };
    this.BiologicalFamilyCensusGridTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'Id',
            toolbar: '#BiologicalFamilyCensusGridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true,
                formatter: 'MedicalHistoryUWSupport.selected_Formatter'
            }, {
                field: 'Relation',
                title: 'Relación',
                formatter: 'MedicalHistoryUWSupport.LookUpForRelationFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'Gender',
                title: 'Sexo',
                formatter: 'MedicalHistoryUWSupport.LookUpForGenderFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'AgeLiving',
                title: 'Edad (si viven)',
                formatter: 'MedicalHistoryUWSupport.AgeLiving_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'AgeDeath',
                title: 'Edad de muerte',
                formatter: 'MedicalHistoryUWSupport.AgeDeath_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'CauseDeath',
                title: 'Causa de fallecimiento',
                sortable: false,
                halign: 'center'
            }, {
                field: 'OnsetAgeHeartDisease',
                title: 'Corazón (Edad inicio)',
                formatter: 'MedicalHistoryUWSupport.OnsetAgeHeartDisease_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'OnsetAgeKidneyDisease',
                title: 'Riñon (Edad inicio)',
                formatter: 'MedicalHistoryUWSupport.OnsetAgeKidneyDisease_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'OnsetAgeHighBloodPressure',
                title: 'Presión arterial (Edad inicio)',
                formatter: 'MedicalHistoryUWSupport.OnsetAgeHighBloodPressure_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'OnsetAgeDiabetes',
                title: 'Diábetes (Edad inicio)',
                formatter: 'MedicalHistoryUWSupport.OnsetAgeDiabetes_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'OnsetAgeMentalIllness',
                title: 'Enfermendad mental (Edad inicio)',
                formatter: 'MedicalHistoryUWSupport.OnsetAgeMentalIllness_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'OnsetAgeCancer',
                title: 'Cáncer (Edad inicio)',
                formatter: 'MedicalHistoryUWSupport.OnsetAgeCancer_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'Id',
                title: 'Id',
                events: 'BiologicalFamilyCensusGridActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });


        $('#BiologicalFamilyCensusGridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#BiologicalFamilyCensusGridTbl');
            $('#BiologicalFamilyCensusGridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#BiologicalFamilyCensusGridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#BiologicalFamilyCensusGridTbl').bootstrapTable('getSelections'), function (row) {		
                MedicalHistoryUWSupport.BiologicalFamilyCensusGridRowToInput(row);
                
                
                return row.Id;
            });
            
          $('#BiologicalFamilyCensusGridTbl').bootstrapTable('remove', {
                field: 'Id',
                values: ids
           });

            $('#BiologicalFamilyCensusGridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#BiologicalFamilyCensusGridCreateBtn').click(function () {
            var formInstance = $("#BiologicalFamilyCensusGridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            MedicalHistoryUWSupport.BiologicalFamilyCensusGridShowModal($('#BiologicalFamilyCensusGridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#BiologicalFamilyCensusGridPopup').find('#BiologicalFamilyCensusGridSaveBtn').click(function () {
            var formInstance = $("#BiologicalFamilyCensusGridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#BiologicalFamilyCensusGridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#BiologicalFamilyCensusGridSaveBtn').html();
                $('#BiologicalFamilyCensusGridSaveBtn').html('Procesando...');
                $('#BiologicalFamilyCensusGridSaveBtn').prop('disabled', true);

                MedicalHistoryUWSupport.currentRow.Relation = parseInt(0 + $('#Relation').val(), 10);
                MedicalHistoryUWSupport.currentRow.Gender = parseInt(0 + $('#Gender').val(), 10);
                MedicalHistoryUWSupport.currentRow.AgeLiving = generalSupport.NumericValue('#AgeLiving', 0, 999);
                MedicalHistoryUWSupport.currentRow.AgeDeath = generalSupport.NumericValue('#AgeDeath', 0, 999);
                MedicalHistoryUWSupport.currentRow.CauseDeath = $('#CauseDeath').val();
                MedicalHistoryUWSupport.currentRow.OnsetAgeHeartDisease = generalSupport.NumericValue('#OnsetAgeHeartDisease', 0, 999);
                MedicalHistoryUWSupport.currentRow.OnsetAgeKidneyDisease = generalSupport.NumericValue('#OnsetAgeKidneyDisease', 0, 999);
                MedicalHistoryUWSupport.currentRow.OnsetAgeHighBloodPressure = generalSupport.NumericValue('#OnsetAgeHighBloodPressure', 0, 999);
                MedicalHistoryUWSupport.currentRow.OnsetAgeDiabetes = generalSupport.NumericValue('#OnsetAgeDiabetes', 0, 999);
                MedicalHistoryUWSupport.currentRow.OnsetAgeMentalIllness = generalSupport.NumericValue('#OnsetAgeMentalIllness', 0, 999);
                MedicalHistoryUWSupport.currentRow.OnsetAgeCancer = generalSupport.NumericValue('#OnsetAgeCancer', 0, 999);
                MedicalHistoryUWSupport.currentRow.Id = parseInt(0 + $('#IdCenso').val(), 10);

                $('#BiologicalFamilyCensusGridSaveBtn').prop('disabled', false);
                $('#BiologicalFamilyCensusGridSaveBtn').html(caption);

                if (wm === 'Update') {
                    $('#BiologicalFamilyCensusGridTbl').bootstrapTable('updateByUniqueId', { id: MedicalHistoryUWSupport.currentRow.Id, row: MedicalHistoryUWSupport.currentRow });
                    $modal.modal('hide');
                }
                else {                    
                    $('#BiologicalFamilyCensusGridTbl').bootstrapTable('append', MedicalHistoryUWSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.BiologicalFamilyCensusGridShowModal = function (md, title, row) {
        var formInstance = $("#BiologicalFamilyCensusGridEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { Relation: 0, Gender: 0, AgeLiving: 0, AgeDeath: 0, CauseDeath: '', OnsetAgeHeartDisease: 0, OnsetAgeKidneyDisease: 0, OnsetAgeHighBloodPressure: 0, OnsetAgeDiabetes: 0, OnsetAgeMentalIllness: 0, OnsetAgeCancer: 0, Id: 0 };

        md.data('id', row.Id);
        md.find('.modal-title').text(title);

        MedicalHistoryUWSupport.BiologicalFamilyCensusGridRowToInput(row);


        md.appendTo("body");
        md.modal('show');
    };

    this.BiologicalFamilyCensusGridRowToInput = function (row) {
        MedicalHistoryUWSupport.currentRow = row;
        $('#Relation').val(row.Relation);
        $('#Relation').trigger('change');
        $('#Gender').val(row.Gender);
        $('#Gender').trigger('change');
        AutoNumeric.set('#AgeLiving', row.AgeLiving);
        AutoNumeric.set('#AgeDeath', row.AgeDeath);
        $('#CauseDeath').val(row.CauseDeath);
        AutoNumeric.set('#OnsetAgeHeartDisease', row.OnsetAgeHeartDisease);
        AutoNumeric.set('#OnsetAgeKidneyDisease', row.OnsetAgeKidneyDisease);
        AutoNumeric.set('#OnsetAgeHighBloodPressure', row.OnsetAgeHighBloodPressure);
        AutoNumeric.set('#OnsetAgeDiabetes', row.OnsetAgeDiabetes);
        AutoNumeric.set('#OnsetAgeMentalIllness', row.OnsetAgeMentalIllness);
        AutoNumeric.set('#OnsetAgeCancer', row.OnsetAgeCancer);
        $('#IdCenso').val(row.Id);

    };





    this.CountryCode_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      };
    this.AreaCode_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      };
    this.Number_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      };
    this.BestTimeToCall_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99,
            decimalPlaces: 0,
            minimumValue: 0
        });
      };
    this.Extension_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      };
    this.Type_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      };
    this.MedicalTest_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      };
    this.AgeLiving_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      };
    this.AgeDeath_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      };
    this.OnsetAgeHeartDisease_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      };
    this.OnsetAgeKidneyDisease_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      };
    this.OnsetAgeHighBloodPressure_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      };
    this.OnsetAgeDiabetes_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      };
    this.OnsetAgeMentalIllness_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      };
    this.OnsetAgeCancer_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      };


	    this.selected_Formatter = function (value, row, index) {          
          return {
                  disabled: $('#TelephoneTbl *').prop('disabled'),
                  checked: value
                 }
      };
	    this.selected_Formatter = function (value, row, index) {          
          return {
                  disabled: $('#MedicalTreatmentMedicalHistoryTbl *').prop('disabled'),
                  checked: value
                 }
      };
	    this.selected_Formatter = function (value, row, index) {          
          return {
                  disabled: $('#MedicalConditionsDetailsGridTbl *').prop('disabled'),
                  checked: value
                 }
      };
	    this.StillTreatment_IsCheck = function (value, row, index) {
        var icon = "";
        
        if (value === '') {
            icon = "glyphicon glyphicon-unchecked";
        } else {
            icon = "glyphicon glyphicon-check";
        }
       
        return '<div class="text-center" >' +
            '<i class="' + icon + '"></i>' +
            '</div>';
      };
	    this.selected_Formatter = function (value, row, index) {          
          return {
                  disabled: $('#DetailsPrescriptionDrugsTbl *').prop('disabled'),
                  checked: value
                 }
      };
	    this.selected_Formatter = function (value, row, index) {          
          return {
                  disabled: $('#BiologicalFamilyCensusGridTbl *').prop('disabled'),
                  checked: value
                 }
      };



  this.Init = function(){
    
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('Historia médica');
        

    MedicalHistoryUWSupport.ControlBehaviour();
    MedicalHistoryUWSupport.ControlActions();
    MedicalHistoryUWSupport.ValidateSetup();
    MedicalHistoryUWSupport.Initialization();


  };
};

$(document).ready(function () {
   MedicalHistoryUWSupport.Init();
});

window.TelephoneActionEvents = {
    'click .update': function (e, value, row, index) {
        MedicalHistoryUWSupport.TelephoneShowModal($('#TelephonePopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.MedicalTreatmentMedicalHistoryActionEvents = {
    'click .update': function (e, value, row, index) {
        MedicalHistoryUWSupport.MedicalTreatmentMedicalHistoryShowModal($('#MedicalTreatmentMedicalHistoryPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.MedicalConditionsDetailsGridActionEvents = {
    'click .update': function (e, value, row, index) {
        MedicalHistoryUWSupport.MedicalConditionsDetailsGridShowModal($('#MedicalConditionsDetailsGridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.DetailsPrescriptionDrugsActionEvents = {
    'click .update': function (e, value, row, index) {
        MedicalHistoryUWSupport.DetailsPrescriptionDrugsShowModal($('#DetailsPrescriptionDrugsPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.BiologicalFamilyCensusGridActionEvents = {
    'click .update': function (e, value, row, index) {
        MedicalHistoryUWSupport.BiologicalFamilyCensusGridShowModal($('#BiologicalFamilyCensusGridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
