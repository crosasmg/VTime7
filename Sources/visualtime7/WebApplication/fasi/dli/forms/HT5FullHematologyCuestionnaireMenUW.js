var HT5FullHematologyCuestionnaireMenUWSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5FullHematologyCuestionnaireMenUWFormId').val(),
            ClientName: $('#ClientName').val(),
            uwcaseid: AutoNumeric.getNumber('#uwcaseid'),
            FullHematologyRedSeriesHematies: AutoNumeric.getNumber('#Hematies'),
            FullHematologyRedSeriesHemoglobin: AutoNumeric.getNumber('#Hemoglobin'),
            FullHematologyRedSeriesHematocrit: AutoNumeric.getNumber('#Hematocrit'),
            FullHematologyRedSeriesMVC: AutoNumeric.getNumber('#MVC'),
            FullHematologyRedSeriesMCH: AutoNumeric.getNumber('#MCH'),
            FullHematologyRedSeriesMCHC: AutoNumeric.getNumber('#MCHC'),
            FullHematologyRedSeriesRWD: AutoNumeric.getNumber('#RWD'),
            FullHematologyBloodChemistryBUN: AutoNumeric.getNumber('#BUN'),
            FullHematologyBloodChemistryCreatinine: AutoNumeric.getNumber('#Creatinine'),
            FullHematologyBloodChemistryGlycemia: AutoNumeric.getNumber('#Glycemia'),
            FullHematologyBloodChemistryUricAcid: AutoNumeric.getNumber('#UricAcid'),
            FullHematologyBloodChemistryCholesterol: AutoNumeric.getNumber('#Cholesterol'),
            FullHematologyBloodChemistryTriglycerides: AutoNumeric.getNumber('#Triglycerides'),
            FullHematologyBloodChemistryCalcium: AutoNumeric.getNumber('#Calcium'),
            FullHematologyBloodChemistryPhosphorus: AutoNumeric.getNumber('#Phosphorus'),
            FullHematologyBloodChemistryAlkalinePhosphatase: AutoNumeric.getNumber('#AlkalinePhosphatase'),
            FullHematologyBloodChemistryOxaloaceticTransaminase: AutoNumeric.getNumber('#OxaloaceticTransaminase'),
            FullHematologyBloodChemistryPyruvicTransaminase: AutoNumeric.getNumber('#PyruvicTransaminase'),
            FullHematologyBloodChemistryTotalBilirubin: AutoNumeric.getNumber('#TotalBilirubin'),
            FullHematologyBloodChemistryDirectBilirubin: AutoNumeric.getNumber('#DirectBilirubin'),
            FullHematologyBloodChemistryIndirectBilirubin: AutoNumeric.getNumber('#IndirectBilirubin'),
            FullHematologyBloodChemistryTotalProteins: AutoNumeric.getNumber('#TotalProteins'),
            FullHematologyBloodChemistryAlbumin: AutoNumeric.getNumber('#Albumin'),
            FullHematologyBloodChemistryGlobulin: AutoNumeric.getNumber('#Globulin'),
            FullHematologyBloodChemistryIndiceAG: AutoNumeric.getNumber('#IndiceAG'),
            FullHematologyBloodChemistryVDRL: $('input:radio[name=VDRL]:checked').val(),
            FullHematologyWhiteSeriesLeukocyte: AutoNumeric.getNumber('#Leukocyte'),
            FullHematologyWhiteSeriesNeutrophils: AutoNumeric.getNumber('#Neutrophils'),
            FullHematologyWhiteSeriesLymphocytes: AutoNumeric.getNumber('#Lymphocytes'),
            FullHematologyWhiteSeriesMonocytes: AutoNumeric.getNumber('#Monocytes'),
            FullHematologyWhiteSeriesEosinophils: AutoNumeric.getNumber('#Eosinophils'),
            FullHematologyWhiteSeriesMPV: AutoNumeric.getNumber('#MPV'),
            FullHematologyWhiteSeriesPlatelet: AutoNumeric.getNumber('#Platelet'),
            FullHematologyHIVMethod: parseInt(0 + $('#Method').val(), 10),
            FullHematologyHIVResult: $('#Result').is(':checked'),
            FullHematologyRemarks: $('#Remarks').val(),
            FullHematologyDateReceived: $('#DateReceived').val() !== '' ? moment($('#DateReceived').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD')
        };
        return data;
    };

    this.ObjectToInput = function (data) {
        $('#HT5FullHematologyCuestionnaireMenUWFormId').val(data.InstanceFormId);
        $('#ClientName').val(data.ClientName);
        AutoNumeric.set('#uwcaseid', data.uwcaseid);
        AutoNumeric.set('#Hematies', data.FullHematologyRedSeriesHematies);
        AutoNumeric.set('#Hemoglobin', data.FullHematologyRedSeriesHemoglobin);
        AutoNumeric.set('#Hematocrit', data.FullHematologyRedSeriesHematocrit);
        AutoNumeric.set('#MVC', data.FullHematologyRedSeriesMVC);
        AutoNumeric.set('#MCH', data.FullHematologyRedSeriesMCH);
        AutoNumeric.set('#MCHC', data.FullHematologyRedSeriesMCHC);
        AutoNumeric.set('#RWD', data.FullHematologyRedSeriesRWD);
        AutoNumeric.set('#BUN', data.FullHematologyBloodChemistryBUN);
        AutoNumeric.set('#Creatinine', data.FullHematologyBloodChemistryCreatinine);
        AutoNumeric.set('#Glycemia', data.FullHematologyBloodChemistryGlycemia);
        AutoNumeric.set('#UricAcid', data.FullHematologyBloodChemistryUricAcid);
        AutoNumeric.set('#Cholesterol', data.FullHematologyBloodChemistryCholesterol);
        AutoNumeric.set('#Triglycerides', data.FullHematologyBloodChemistryTriglycerides);
        AutoNumeric.set('#Calcium', data.FullHematologyBloodChemistryCalcium);
        AutoNumeric.set('#Phosphorus', data.FullHematologyBloodChemistryPhosphorus);
        AutoNumeric.set('#AlkalinePhosphatase', data.FullHematologyBloodChemistryAlkalinePhosphatase);
        AutoNumeric.set('#OxaloaceticTransaminase', data.FullHematologyBloodChemistryOxaloaceticTransaminase);
        AutoNumeric.set('#PyruvicTransaminase', data.FullHematologyBloodChemistryPyruvicTransaminase);
        AutoNumeric.set('#TotalBilirubin', data.FullHematologyBloodChemistryTotalBilirubin);
        AutoNumeric.set('#DirectBilirubin', data.FullHematologyBloodChemistryDirectBilirubin);
        AutoNumeric.set('#IndirectBilirubin', data.FullHematologyBloodChemistryIndirectBilirubin);
        AutoNumeric.set('#TotalProteins', data.FullHematologyBloodChemistryTotalProteins);
        AutoNumeric.set('#Albumin', data.FullHematologyBloodChemistryAlbumin);
        AutoNumeric.set('#Globulin', data.FullHematologyBloodChemistryGlobulin);
        AutoNumeric.set('#IndiceAG', data.FullHematologyBloodChemistryIndiceAG);
        if($('input:radio[name=VDRL][value=' + data.FullHematologyBloodChemistryVDRL +']').length===0)
           $('input:radio[name=VDRL]').prop('checked', false);
        else
           $($('input:radio[name=VDRL][value=' + data.FullHematologyBloodChemistryVDRL +']')).prop('checked', true);
        $('#VDRL').data('oldValue', data.FullHematologyBloodChemistryVDRL);
        $('#VDRL').val(data.FullHematologyBloodChemistryVDRL);

        AutoNumeric.set('#Leukocyte', data.FullHematologyWhiteSeriesLeukocyte);
        AutoNumeric.set('#Neutrophils', data.FullHematologyWhiteSeriesNeutrophils);
        AutoNumeric.set('#Lymphocytes', data.FullHematologyWhiteSeriesLymphocytes);
        AutoNumeric.set('#Monocytes', data.FullHematologyWhiteSeriesMonocytes);
        AutoNumeric.set('#Eosinophils', data.FullHematologyWhiteSeriesEosinophils);
        AutoNumeric.set('#MPV', data.FullHematologyWhiteSeriesMPV);
        AutoNumeric.set('#Platelet', data.FullHematologyWhiteSeriesPlatelet);
        $('#Method').data('oldValue', data.FullHematologyHIVMethod);
        $('#Method').val(data.FullHematologyHIVMethod);
        $('#Result').prop("checked", data.FullHematologyHIVResult);
        $('#Remarks').val(data.FullHematologyRemarks);
        $('#DateReceived').val(generalSupport.ToJavaScriptDateCustom(data.FullHematologyDateReceived, 'DD/MM/YYYY'));



    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#uwcaseid', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#Hematies', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#Hemoglobin', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#Hematocrit', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#MVC', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#MCH', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#MCHC', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "9999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#RWD', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#BUN', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#Creatinine', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#Glycemia', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#UricAcid', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#Cholesterol', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "9999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#Triglycerides', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "9999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#Calcium', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#Phosphorus', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#AlkalinePhosphatase', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#OxaloaceticTransaminase', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#PyruvicTransaminase', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#TotalBilirubin', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#DirectBilirubin', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#IndirectBilirubin', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#TotalProteins', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#Albumin', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#Globulin', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#IndiceAG', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#Leukocyte', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#Neutrophils', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#Lymphocytes', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#Monocytes', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#Eosinophils', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#MPV', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#Platelet', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999999",
            decimalPlaces: 0,
            minimumValue: "0"
        });




        $('#DateReceived_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });


    };

    this.ActionProcess = function (data) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                HT5FullHematologyCuestionnaireMenUWSupport.ObjectToInput(data.d.Data);
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
            url: "/fasi/dli/forms/HT5FullHematologyCuestionnaireMenUWActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                id: $('#HT5FullHematologyCuestionnaireMenUWFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            success: function (data) {
                $.LoadingOverlay("hide");

                HT5FullHematologyCuestionnaireMenUWSupport.ActionProcess(data);
                
                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/HT5FullHematologyCuestionnaireMenUW.aspx?id=' + $('#HT5FullHematologyCuestionnaireMenUWFormId').val());
              
          

            },
            error: function (qXHR, textStatus, errorThrown) {
                $.LoadingOverlay("hide");
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };




    this.ControlActions = function () {

        $('#save').click(function (event) {
            var formInstance = $("#HT5FullHematologyCuestionnaireMenUWMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#save'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5FullHematologyCuestionnaireMenUWActions.aspx/saveClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5FullHematologyCuestionnaireMenUWSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5FullHematologyCuestionnaireMenUWSupport.ActionProcess(data);
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
        $('#submit').click(function (event) {
            var formInstance = $("#HT5FullHematologyCuestionnaireMenUWMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#submit'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5FullHematologyCuestionnaireMenUWActions.aspx/submitClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5FullHematologyCuestionnaireMenUWSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5FullHematologyCuestionnaireMenUWSupport.ActionProcess(data);
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
    
        $("#HT5FullHematologyCuestionnaireMenUWMainForm").validate({
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

                },
                Hematies: {
                    AutoNumericRequired: true                },
                Hemoglobin: {
                    AutoNumericRequired: true                },
                Hematocrit: {
                    AutoNumericRequired: true                },
                MVC: {
                    AutoNumericRequired: true                },
                MCH: {
                    AutoNumericRequired: true                },
                MCHC: {
                    AutoNumericRequired: true                },
                RWD: {
                    AutoNumericRequired: true                },
                BUN: {
                    AutoNumericRequired: true                },
                Creatinine: {
                    AutoNumericRequired: true                },
                Glycemia: {
                    AutoNumericRequired: true                },
                UricAcid: {
                    AutoNumericRequired: true                },
                Cholesterol: {

                },
                Triglycerides: {

                },
                Calcium: {
                    AutoNumericRequired: true                },
                Phosphorus: {
                    AutoNumericRequired: true                },
                AlkalinePhosphatase: {
                    AutoNumericRequired: true                },
                OxaloaceticTransaminase: {
                    AutoNumericRequired: true                },
                PyruvicTransaminase: {
                    AutoNumericRequired: true                },
                TotalBilirubin: {

                },
                DirectBilirubin: {

                },
                IndirectBilirubin: {

                },
                TotalProteins: {
                    AutoNumericRequired: true                },
                Albumin: {
                    AutoNumericRequired: true                },
                Globulin: {
                    AutoNumericRequired: true                },
                IndiceAG: {
                    AutoNumericRequired: true                },
                VDRL: {
                    required: true
                },
                Leukocyte: {
                    AutoNumericRequired: true                },
                Neutrophils: {
                    AutoNumericRequired: true                },
                Lymphocytes: {
                    AutoNumericRequired: true                },
                Monocytes: {
                    AutoNumericRequired: true                },
                Eosinophils: {
                    AutoNumericRequired: true                },
                MPV: {
                    AutoNumericRequired: true                },
                Platelet: {
                    AutoNumericRequired: true                },
                Method: {
                    required: true
                },
                Remarks: {
                    required: true
                },
                DateReceived: {
                    required: true
                }
            },
            messages: {
                uwcaseid: {

                },
                Hematies: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                Hemoglobin: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                Hematocrit: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                MVC: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                MCH: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                MCHC: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                RWD: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                BUN: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                Creatinine: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                Glycemia: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                UricAcid: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                Cholesterol: {

                },
                Triglycerides: {

                },
                Calcium: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                Phosphorus: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                AlkalinePhosphatase: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                OxaloaceticTransaminase: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                PyruvicTransaminase: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                TotalBilirubin: {

                },
                DirectBilirubin: {

                },
                IndirectBilirubin: {

                },
                TotalProteins: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                Albumin: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                Globulin: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                IndiceAG: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                VDRL: {
                    required: 'El campo es requerido.'
                },
                Leukocyte: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                Neutrophils: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                Lymphocytes: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                Monocytes: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                Eosinophils: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                MPV: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                Platelet: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                Method: {
                    required: 'El campo es requerido.'
                },
                Remarks: {
                    required: 'El campo es requerido.'
                },
                DateReceived: {
                    required: 'El campo es requerido'
                }
            }
        });

    };





};

$(document).ready(function () {
    moment.locale('es');
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('HT5Hematología completa - Hombres');
        

    HT5FullHematologyCuestionnaireMenUWSupport.ControlBehaviour();
    HT5FullHematologyCuestionnaireMenUWSupport.ControlActions();
    HT5FullHematologyCuestionnaireMenUWSupport.ValidateSetup();
    HT5FullHematologyCuestionnaireMenUWSupport.Initialization();





});

