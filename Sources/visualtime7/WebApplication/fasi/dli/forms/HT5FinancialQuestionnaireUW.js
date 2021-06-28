var HT5FinancialQuestionnaireUWSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5FinancialQuestionnaireUWFormId').val(),
            ClientName: $('#ClientName').val(),
            uwcaseid: generalSupport.NumericValue('#uwcaseid', 0, 99999),
            FinancialYear1: generalSupport.NumericValue('#Year1', 0, 9999),
            FinancialYear2: generalSupport.NumericValue('#Year2', 0, 9999),
            FinancialYear3: generalSupport.NumericValue('#Year3', 0, 9999),
            FinancialSalaryYear1: generalSupport.NumericValue('#SalaryYear1', 0, 9999999999),
            FinancialSalaryYear2: generalSupport.NumericValue('#SalaryYear2', 0, 999999999999),
            FinancialSalaryYear3: generalSupport.NumericValue('#SalaryYear3', 0, 999999999999),
            FinancialBusinesReported1: generalSupport.NumericValue('#BusinesReported1', 0, 999999999999),
            FinancialBusinesReported2: generalSupport.NumericValue('#BusinesReported2', 0, 999999999999),
            FinancialBusinesReported3: generalSupport.NumericValue('#BusinesReported3', 0, 999999999999),
            FinancialBonusYear1: generalSupport.NumericValue('#BonusYear1', 0, 999999999999),
            FinancialBonusYear2: generalSupport.NumericValue('#BonusYear2', 0, 999999999999),
            FinancialBonusYear3: generalSupport.NumericValue('#BonusYear3', 0, 999999999999),
            FinancialCommissionYear1: generalSupport.NumericValue('#CommissionYear1', 0, 999999999999),
            FinancialCommissionYear2: generalSupport.NumericValue('#CommissionYear2', 0, 999999999999),
            FinancialCommissionYear3: generalSupport.NumericValue('#CommissionYear3', 0, 999999999999),
            FinancialPensionProfitSharingYear1: generalSupport.NumericValue('#PensionProfitSharingYear1', 0, 999999999999),
            FinancialPensionProfitSharingYear2: generalSupport.NumericValue('#PensionProfitSharingYear2', 0, 999999999999),
            FinancialPensionProfitSharingYear3: generalSupport.NumericValue('#PensionProfitSharingYear3', 0, 999999999999),
            FinancialOtherIncomeYear1: generalSupport.NumericValue('#OtherIncomeYear1', 0, 999999999999),
            FinancialOtherIncomeYear2: generalSupport.NumericValue('#OtherIncomeYear2', 0, 999999999999),
            FinancialOtherIncomeYear3: generalSupport.NumericValue('#OtherIncomeYear3', 0, 999999999999),
            FinancialDescriptionOtherIncome: $('#DescriptionOtherIncome').val(),
            FinancialTotalEarnedIncomeYear1: generalSupport.NumericValue('#TotalEarnedIncomeYear1', 0, 999999999999),
            FinancialTotalEarnedIncomeYear2: generalSupport.NumericValue('#TotalEarnedIncomeYear2', 0, 999999999999),
            FinancialTotalEarnedIncomeYear3: generalSupport.NumericValue('#TotalEarnedIncomeYear3', 0, 999999999999),
            Year1: generalSupport.NumericValue('#iaYear1', 0, 9999),
            Year2: generalSupport.NumericValue('#iaYear2', 0, 9999),
            FinancialYear3: generalSupport.NumericValue('#iaYear3', 0, 9999),
            FinancialDividendsYear1: generalSupport.NumericValue('#DividendsYear1', 0, 999999999999),
            FinancialDividendsYear2: generalSupport.NumericValue('#DividendsYear2', 0, 999999999999),
            FinancialDividendsYear3: generalSupport.NumericValue('#DividendsYear3', 0, 999999999999),
            FinancialInterestYear1: generalSupport.NumericValue('#InterestYear1', 0, 999999999999),
            FinancialInterestYear2: generalSupport.NumericValue('#InterestYear2', 0, 999999999999),
            FinancialInterestYear3: generalSupport.NumericValue('#InterestYear3', 0, 999999999999),
            FinancialNetRentalsYear1: generalSupport.NumericValue('#NetRentalsYear1', 0, 999999999999),
            FinancialNetRentalsYear2: generalSupport.NumericValue('#NetRentalsYear2', 0, 999999999999),
            FinancialNetRentalsYear3: generalSupport.NumericValue('#NetRentalsYear3', 0, 999999999999),
            FinancialCapitalGainsYear1: generalSupport.NumericValue('#CapitalGainsYear1', 0, 999999999999),
            FinancialCapitalGainsYear2: generalSupport.NumericValue('#CapitalGainsYear2', 0, 999999999999),
            FinancialCapitalGainsYear3: generalSupport.NumericValue('#CapitalGainsYear3', 0, 999999999999),
            FinancialOtherEarnedIncomeYear1: generalSupport.NumericValue('#OtherEarnedIncomeYear1', 0, 999999999999),
            FinancialOtherEarnedIncomeYear2: generalSupport.NumericValue('#OtherEarnedIncomeYear2', 0, 999999999999),
            FinancialOtherEarnedIncomeYear3: generalSupport.NumericValue('#OtherEarnedIncomeYear3', 0, 999999999999),
            FinancialDescriptionOtherEarnedIncome: $('#DescriptionOtherEarnedIncome').val(),
            FinancialTotalUnearnedIncomeYear1: generalSupport.NumericValue('#TotalUnearnedIncomeYear1', 0, 999999999999),
            FinancialTotalUnearnedIncomeYear2: generalSupport.NumericValue('#TotalUnearnedIncomeYear2', 0, 999999999999),
            FinancialTotalUnearnedIncomeYear3: generalSupport.NumericValue('#TotalUnearnedIncomeYear3', 0, 999999999999),
            FinancialCash: generalSupport.NumericValue('#Cash', 0, 999999999999999),
            FinancialRealEstate: generalSupport.NumericValue('#RealEstate', 0, 999999999999999),
            FinancialBusinessEquity: generalSupport.NumericValue('#BusinessEquity', 0, 999999999999999),
            FinancialStocks: generalSupport.NumericValue('#Stocks', 0, 999999999999999),
            FinancialOtherAssets1: generalSupport.NumericValue('#OtherAssets1', 0, 999999999999999),
            FinancialDescribeOtherAssets1: $('#DescribeOtherAssets1').val(),
            FinancialOtherAssets2: generalSupport.NumericValue('#OtherAssets2', 0, 999999999999999),
            FinancialDescribeOtherAssets2: $('#DescribeOtherAssets2').val(),
            FinancialOtherAssets3: generalSupport.NumericValue('#OtherAssets3', 0, 999999999999999),
            FinancialDescribeOtherAssets3: $('#DescribeOtherAssets3').val(),
            FinancialOtherAssets4: generalSupport.NumericValue('#OtherAssets4', 0, 999999999999999),
            FinancialDescribeOtherAssets4: $('#DescribeOtherAssets4').val(),
            FinancialTotalAssets: generalSupport.NumericValue('#TotalAssets', 0, 999999999999999),
            FinancialMortgages: generalSupport.NumericValue('#Mortgages', 0, 999999999999999),
            FinancialLoans: generalSupport.NumericValue('#Loans', 0, 999999999999999),
            FinancialLiens: generalSupport.NumericValue('#Liens', 0, 999999999999999),
            FinancialBonds: generalSupport.NumericValue('#Bonds', 0, 999999999999999),
            FinancialOtherLiabilities1: generalSupport.NumericValue('#OtherLiabilities1', 0, 999999999999999),
            FinancialDescribeOtherLiabilities1: $('#DescribeOtherLiabilities1').val(),
            FinancialOtherLiabilities2: generalSupport.NumericValue('#OtherLiabilities2', 0, 999999999999999),
            FinancialDescribeOtherLiabilities2: $('#DescribeOtherLiabilities2').val(),
            FinancialOtherLiabilities3: generalSupport.NumericValue('#OtherLiabilities3', 0, 999999999999999),
            FinancialDescribeOtherLiabilities3: $('#DescribeOtherLiabilities3').val(),
            FinancialOtherLiabilities4: generalSupport.NumericValue('#OtherLiabilities4', 0, 999999999999999),
            FinancialDescribeOtherLiabilities4: $('#DescribeOtherLiabilities4').val(),
            FinancialTotalLiabilities: generalSupport.NumericValue('#TotalLiabilities', 0, 999999999999999),
            FinancialAdditionalInformation: $('#AdditionalInformation').val(),
            FinancialDateReceived: generalSupport.DatePickerValueInputToObject('#DateReceived')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#HT5FinancialQuestionnaireUWFormId').val(data.InstanceFormId);
        $('#ClientName').val(data.ClientName);
        AutoNumeric.set('#uwcaseid', data.uwcaseid);
        AutoNumeric.set('#Year1', data.FinancialYear1);
        AutoNumeric.set('#Year2', data.FinancialYear2);
        AutoNumeric.set('#Year3', data.FinancialYear3);
        AutoNumeric.set('#SalaryYear1', data.FinancialSalaryYear1);
        AutoNumeric.set('#SalaryYear2', data.FinancialSalaryYear2);
        AutoNumeric.set('#SalaryYear3', data.FinancialSalaryYear3);
        AutoNumeric.set('#BusinesReported1', data.FinancialBusinesReported1);
        AutoNumeric.set('#BusinesReported2', data.FinancialBusinesReported2);
        AutoNumeric.set('#BusinesReported3', data.FinancialBusinesReported3);
        AutoNumeric.set('#BonusYear1', data.FinancialBonusYear1);
        AutoNumeric.set('#BonusYear2', data.FinancialBonusYear2);
        AutoNumeric.set('#BonusYear3', data.FinancialBonusYear3);
        AutoNumeric.set('#CommissionYear1', data.FinancialCommissionYear1);
        AutoNumeric.set('#CommissionYear2', data.FinancialCommissionYear2);
        AutoNumeric.set('#CommissionYear3', data.FinancialCommissionYear3);
        AutoNumeric.set('#PensionProfitSharingYear1', data.FinancialPensionProfitSharingYear1);
        AutoNumeric.set('#PensionProfitSharingYear2', data.FinancialPensionProfitSharingYear2);
        AutoNumeric.set('#PensionProfitSharingYear3', data.FinancialPensionProfitSharingYear3);
        AutoNumeric.set('#OtherIncomeYear1', data.FinancialOtherIncomeYear1);
        AutoNumeric.set('#OtherIncomeYear2', data.FinancialOtherIncomeYear2);
        AutoNumeric.set('#OtherIncomeYear3', data.FinancialOtherIncomeYear3);
        $('#DescriptionOtherIncome').val(data.FinancialDescriptionOtherIncome);
        AutoNumeric.set('#TotalEarnedIncomeYear1', data.FinancialTotalEarnedIncomeYear1);
        AutoNumeric.set('#TotalEarnedIncomeYear2', data.FinancialTotalEarnedIncomeYear2);
        AutoNumeric.set('#TotalEarnedIncomeYear3', data.FinancialTotalEarnedIncomeYear3);
        AutoNumeric.set('#iaYear1', data.Year1);
        AutoNumeric.set('#iaYear2', data.Year2);
        AutoNumeric.set('#iaYear3', data.FinancialYear3);
        AutoNumeric.set('#DividendsYear1', data.FinancialDividendsYear1);
        AutoNumeric.set('#DividendsYear2', data.FinancialDividendsYear2);
        AutoNumeric.set('#DividendsYear3', data.FinancialDividendsYear3);
        AutoNumeric.set('#InterestYear1', data.FinancialInterestYear1);
        AutoNumeric.set('#InterestYear2', data.FinancialInterestYear2);
        AutoNumeric.set('#InterestYear3', data.FinancialInterestYear3);
        AutoNumeric.set('#NetRentalsYear1', data.FinancialNetRentalsYear1);
        AutoNumeric.set('#NetRentalsYear2', data.FinancialNetRentalsYear2);
        AutoNumeric.set('#NetRentalsYear3', data.FinancialNetRentalsYear3);
        AutoNumeric.set('#CapitalGainsYear1', data.FinancialCapitalGainsYear1);
        AutoNumeric.set('#CapitalGainsYear2', data.FinancialCapitalGainsYear2);
        AutoNumeric.set('#CapitalGainsYear3', data.FinancialCapitalGainsYear3);
        AutoNumeric.set('#OtherEarnedIncomeYear1', data.FinancialOtherEarnedIncomeYear1);
        AutoNumeric.set('#OtherEarnedIncomeYear2', data.FinancialOtherEarnedIncomeYear2);
        AutoNumeric.set('#OtherEarnedIncomeYear3', data.FinancialOtherEarnedIncomeYear3);
        $('#DescriptionOtherEarnedIncome').val(data.FinancialDescriptionOtherEarnedIncome);
        AutoNumeric.set('#TotalUnearnedIncomeYear1', data.FinancialTotalUnearnedIncomeYear1);
        AutoNumeric.set('#TotalUnearnedIncomeYear2', data.FinancialTotalUnearnedIncomeYear2);
        AutoNumeric.set('#TotalUnearnedIncomeYear3', data.FinancialTotalUnearnedIncomeYear3);
        AutoNumeric.set('#Cash', data.FinancialCash);
        AutoNumeric.set('#RealEstate', data.FinancialRealEstate);
        AutoNumeric.set('#BusinessEquity', data.FinancialBusinessEquity);
        AutoNumeric.set('#Stocks', data.FinancialStocks);
        AutoNumeric.set('#OtherAssets1', data.FinancialOtherAssets1);
        $('#DescribeOtherAssets1').val(data.FinancialDescribeOtherAssets1);
        AutoNumeric.set('#OtherAssets2', data.FinancialOtherAssets2);
        $('#DescribeOtherAssets2').val(data.FinancialDescribeOtherAssets2);
        AutoNumeric.set('#OtherAssets3', data.FinancialOtherAssets3);
        $('#DescribeOtherAssets3').val(data.FinancialDescribeOtherAssets3);
        AutoNumeric.set('#OtherAssets4', data.FinancialOtherAssets4);
        $('#DescribeOtherAssets4').val(data.FinancialDescribeOtherAssets4);
        AutoNumeric.set('#TotalAssets', data.FinancialTotalAssets);
        AutoNumeric.set('#Mortgages', data.FinancialMortgages);
        AutoNumeric.set('#Loans', data.FinancialLoans);
        AutoNumeric.set('#Liens', data.FinancialLiens);
        AutoNumeric.set('#Bonds', data.FinancialBonds);
        AutoNumeric.set('#OtherLiabilities1', data.FinancialOtherLiabilities1);
        $('#DescribeOtherLiabilities1').val(data.FinancialDescribeOtherLiabilities1);
        AutoNumeric.set('#OtherLiabilities2', data.FinancialOtherLiabilities2);
        $('#DescribeOtherLiabilities2').val(data.FinancialDescribeOtherLiabilities2);
        AutoNumeric.set('#OtherLiabilities3', data.FinancialOtherLiabilities3);
        $('#DescribeOtherLiabilities3').val(data.FinancialDescribeOtherLiabilities3);
        AutoNumeric.set('#OtherLiabilities4', data.FinancialOtherLiabilities4);
        $('#DescribeOtherLiabilities4').val(data.FinancialDescribeOtherLiabilities4);
        AutoNumeric.set('#TotalLiabilities', data.FinancialTotalLiabilities);
        $('#AdditionalInformation').val(data.FinancialAdditionalInformation);
        $('#DateReceived').val(generalSupport.ToJavaScriptDateCustom(data.FinancialDateReceived, generalSupport.DateFormat()));



    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#uwcaseid', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#Year1', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#Year2', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#Year3', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#SalaryYear1', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#SalaryYear2', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#SalaryYear3', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#BusinesReported1', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#BusinesReported2', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#BusinesReported3', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#BonusYear1', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#BonusYear2', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#BonusYear3', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#CommissionYear1', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#CommissionYear2', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#CommissionYear3', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#PensionProfitSharingYear1', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#PensionProfitSharingYear2', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#PensionProfitSharingYear3', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#OtherIncomeYear1', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#OtherIncomeYear2', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#OtherIncomeYear3', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#TotalEarnedIncomeYear1', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#TotalEarnedIncomeYear2', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#TotalEarnedIncomeYear3', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#iaYear1', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#iaYear2', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#iaYear3', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#DividendsYear1', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#DividendsYear2', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#DividendsYear3', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#InterestYear1', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#InterestYear2', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#InterestYear3', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#NetRentalsYear1', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#NetRentalsYear2', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#NetRentalsYear3', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#CapitalGainsYear1', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#CapitalGainsYear2', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#CapitalGainsYear3', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#OtherEarnedIncomeYear1', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#OtherEarnedIncomeYear2', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#OtherEarnedIncomeYear3', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#TotalUnearnedIncomeYear1', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#TotalUnearnedIncomeYear2', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#TotalUnearnedIncomeYear3', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#Cash', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#RealEstate', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#BusinessEquity', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#Stocks', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#OtherAssets1', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#OtherAssets2', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#OtherAssets3', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#OtherAssets4', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#TotalAssets', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#Mortgages', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#Loans', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#Liens', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#Bonds', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#OtherLiabilities1', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#OtherLiabilities2', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#OtherLiabilities3', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#OtherLiabilities4', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#TotalLiabilities', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });




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
					         HT5FinancialQuestionnaireUWSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   HT5FinancialQuestionnaireUWSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        app.core.AsyncWebMethod("/fasi/dli/forms/HT5FinancialQuestionnaireUWActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), true,
            JSON.stringify({
                id: $('#HT5FinancialQuestionnaireUWFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
                if (data.d.Success)
                    $('#HT5FinancialQuestionnaireUWFormId').val(data.d.Data.Instance.InstanceFormId);
                    
                if (data.d.Success === true && data.d.Data.LookUps) {
                    data.d.Data.LookUps.forEach(function (elementSource) {
                        generalSupport.RenderLookUp(elementSource.Key, data.d.Data.Instance[elementSource.Key], 'Initialization', elementSource.Items);
                    });
                }
                







                HT5FinancialQuestionnaireUWSupport.ActionProcess(data, 'Initialization');

                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)                    
                    window.history.replaceState({}, null, '/fasi/dli/forms/' + location.pathname.substring(location.pathname.lastIndexOf("/") + 1) + '?id=' + $('#HT5FinancialQuestionnaireUWFormId').val());
 
              
          

            });
    };




    this.ControlActions =   function () {

        $('#OtherIncomeYear1').change(function () {
         if ($('#OtherIncomeYear1').val() !== null && $('#OtherIncomeYear1').val() !== $('#OtherIncomeYear1').data('oldValue')) {
             $('#OtherIncomeYear1').data('oldValue', $('#OtherIncomeYear1').val() );             
           
            app.core.AsyncWebMethod("/fasi/dli/forms/HT5FinancialQuestionnaireUWActions.aspx/OtherIncomeYear1Change", false,
                 JSON.stringify({
                     instance: HT5FinancialQuestionnaireUWSupport.InputToObject()
                 }),
                 function (data) {
                     HT5FinancialQuestionnaireUWSupport.ActionProcess(data, 'OtherIncomeYear1Change');
           });
      }          
    });
        $('#OtherIncomeYear2').change(function () {
         if ($('#OtherIncomeYear2').val() !== null && $('#OtherIncomeYear2').val() !== $('#OtherIncomeYear2').data('oldValue')) {
             $('#OtherIncomeYear2').data('oldValue', $('#OtherIncomeYear2').val() );             
           
            app.core.AsyncWebMethod("/fasi/dli/forms/HT5FinancialQuestionnaireUWActions.aspx/OtherIncomeYear2Change", false,
                 JSON.stringify({
                     instance: HT5FinancialQuestionnaireUWSupport.InputToObject()
                 }),
                 function (data) {
                     HT5FinancialQuestionnaireUWSupport.ActionProcess(data, 'OtherIncomeYear2Change');
           });
      }          
    });
        $('#OtherIncomeYear3').change(function () {
         if ($('#OtherIncomeYear3').val() !== null && $('#OtherIncomeYear3').val() !== $('#OtherIncomeYear3').data('oldValue')) {
             $('#OtherIncomeYear3').data('oldValue', $('#OtherIncomeYear3').val() );             
           
            app.core.AsyncWebMethod("/fasi/dli/forms/HT5FinancialQuestionnaireUWActions.aspx/OtherIncomeYear3Change", false,
                 JSON.stringify({
                     instance: HT5FinancialQuestionnaireUWSupport.InputToObject()
                 }),
                 function (data) {
                     HT5FinancialQuestionnaireUWSupport.ActionProcess(data, 'OtherIncomeYear3Change');
           });
      }          
    });
        $('#OtherEarnedIncomeYear1').change(function () {
         if ($('#OtherEarnedIncomeYear1').val() !== null && $('#OtherEarnedIncomeYear1').val() !== $('#OtherEarnedIncomeYear1').data('oldValue')) {
             $('#OtherEarnedIncomeYear1').data('oldValue', $('#OtherEarnedIncomeYear1').val() );             
           
            app.core.AsyncWebMethod("/fasi/dli/forms/HT5FinancialQuestionnaireUWActions.aspx/OtherEarnedIncomeYear1Change", false,
                 JSON.stringify({
                     instance: HT5FinancialQuestionnaireUWSupport.InputToObject()
                 }),
                 function (data) {
                     HT5FinancialQuestionnaireUWSupport.ActionProcess(data, 'OtherEarnedIncomeYear1Change');
           });
      }          
    });
        $('#OtherEarnedIncomeYear2').change(function () {
         if ($('#OtherEarnedIncomeYear2').val() !== null && $('#OtherEarnedIncomeYear2').val() !== $('#OtherEarnedIncomeYear2').data('oldValue')) {
             $('#OtherEarnedIncomeYear2').data('oldValue', $('#OtherEarnedIncomeYear2').val() );             
           
            app.core.AsyncWebMethod("/fasi/dli/forms/HT5FinancialQuestionnaireUWActions.aspx/OtherEarnedIncomeYear2Change", false,
                 JSON.stringify({
                     instance: HT5FinancialQuestionnaireUWSupport.InputToObject()
                 }),
                 function (data) {
                     HT5FinancialQuestionnaireUWSupport.ActionProcess(data, 'OtherEarnedIncomeYear2Change');
           });
      }          
    });
        $('#OtherEarnedIncomeYear3').change(function () {
         if ($('#OtherEarnedIncomeYear3').val() !== null && $('#OtherEarnedIncomeYear3').val() !== $('#OtherEarnedIncomeYear3').data('oldValue')) {
             $('#OtherEarnedIncomeYear3').data('oldValue', $('#OtherEarnedIncomeYear3').val() );             
           
            app.core.AsyncWebMethod("/fasi/dli/forms/HT5FinancialQuestionnaireUWActions.aspx/OtherEarnedIncomeYear3Change", false,
                 JSON.stringify({
                     instance: HT5FinancialQuestionnaireUWSupport.InputToObject()
                 }),
                 function (data) {
                     HT5FinancialQuestionnaireUWSupport.ActionProcess(data, 'OtherEarnedIncomeYear3Change');
           });
      }          
    });
        $('#OtherAssets1').change(function () {
         if ($('#OtherAssets1').val() !== null && $('#OtherAssets1').val() !== $('#OtherAssets1').data('oldValue')) {
             $('#OtherAssets1').data('oldValue', $('#OtherAssets1').val() );             
           
            app.core.AsyncWebMethod("/fasi/dli/forms/HT5FinancialQuestionnaireUWActions.aspx/OtherAssets1Change", false,
                 JSON.stringify({
                     instance: HT5FinancialQuestionnaireUWSupport.InputToObject()
                 }),
                 function (data) {
                     HT5FinancialQuestionnaireUWSupport.ActionProcess(data, 'OtherAssets1Change');
           });
      }          
    });
        $('#OtherAssets2').change(function () {
         if ($('#OtherAssets2').val() !== null && $('#OtherAssets2').val() !== $('#OtherAssets2').data('oldValue')) {
             $('#OtherAssets2').data('oldValue', $('#OtherAssets2').val() );             
           
            app.core.AsyncWebMethod("/fasi/dli/forms/HT5FinancialQuestionnaireUWActions.aspx/OtherAssets2Change", false,
                 JSON.stringify({
                     instance: HT5FinancialQuestionnaireUWSupport.InputToObject()
                 }),
                 function (data) {
                     HT5FinancialQuestionnaireUWSupport.ActionProcess(data, 'OtherAssets2Change');
           });
      }          
    });
        $('#OtherAssets3').change(function () {
         if ($('#OtherAssets3').val() !== null && $('#OtherAssets3').val() !== $('#OtherAssets3').data('oldValue')) {
             $('#OtherAssets3').data('oldValue', $('#OtherAssets3').val() );             
           
            app.core.AsyncWebMethod("/fasi/dli/forms/HT5FinancialQuestionnaireUWActions.aspx/OtherAssets3Change", false,
                 JSON.stringify({
                     instance: HT5FinancialQuestionnaireUWSupport.InputToObject()
                 }),
                 function (data) {
                     HT5FinancialQuestionnaireUWSupport.ActionProcess(data, 'OtherAssets3Change');
           });
      }          
    });
        $('#OtherAssets4').change(function () {
         if ($('#OtherAssets4').val() !== null && $('#OtherAssets4').val() !== $('#OtherAssets4').data('oldValue')) {
             $('#OtherAssets4').data('oldValue', $('#OtherAssets4').val() );             
           
            app.core.AsyncWebMethod("/fasi/dli/forms/HT5FinancialQuestionnaireUWActions.aspx/OtherAssets4Change", false,
                 JSON.stringify({
                     instance: HT5FinancialQuestionnaireUWSupport.InputToObject()
                 }),
                 function (data) {
                     HT5FinancialQuestionnaireUWSupport.ActionProcess(data, 'OtherAssets4Change');
           });
      }          
    });
        $('#OtherLiabilities1').change(function () {
         if ($('#OtherLiabilities1').val() !== null && $('#OtherLiabilities1').val() !== $('#OtherLiabilities1').data('oldValue')) {
             $('#OtherLiabilities1').data('oldValue', $('#OtherLiabilities1').val() );             
           
            app.core.AsyncWebMethod("/fasi/dli/forms/HT5FinancialQuestionnaireUWActions.aspx/OtherLiabilities1Change", false,
                 JSON.stringify({
                     instance: HT5FinancialQuestionnaireUWSupport.InputToObject()
                 }),
                 function (data) {
                     HT5FinancialQuestionnaireUWSupport.ActionProcess(data, 'OtherLiabilities1Change');
           });
      }          
    });
        $('#OtherLiabilities2').change(function () {
         if ($('#OtherLiabilities2').val() !== null && $('#OtherLiabilities2').val() !== $('#OtherLiabilities2').data('oldValue')) {
             $('#OtherLiabilities2').data('oldValue', $('#OtherLiabilities2').val() );             
           
            app.core.AsyncWebMethod("/fasi/dli/forms/HT5FinancialQuestionnaireUWActions.aspx/OtherLiabilities2Change", false,
                 JSON.stringify({
                     instance: HT5FinancialQuestionnaireUWSupport.InputToObject()
                 }),
                 function (data) {
                     HT5FinancialQuestionnaireUWSupport.ActionProcess(data, 'OtherLiabilities2Change');
           });
      }          
    });
        $('#OtherLiabilities3').change(function () {
         if ($('#OtherLiabilities3').val() !== null && $('#OtherLiabilities3').val() !== $('#OtherLiabilities3').data('oldValue')) {
             $('#OtherLiabilities3').data('oldValue', $('#OtherLiabilities3').val() );             
           
            app.core.AsyncWebMethod("/fasi/dli/forms/HT5FinancialQuestionnaireUWActions.aspx/OtherLiabilities3Change", false,
                 JSON.stringify({
                     instance: HT5FinancialQuestionnaireUWSupport.InputToObject()
                 }),
                 function (data) {
                     HT5FinancialQuestionnaireUWSupport.ActionProcess(data, 'OtherLiabilities3Change');
           });
      }          
    });
        $('#OtherLiabilities4').change(function () {
         if ($('#OtherLiabilities4').val() !== null && $('#OtherLiabilities4').val() !== $('#OtherLiabilities4').data('oldValue')) {
             $('#OtherLiabilities4').data('oldValue', $('#OtherLiabilities4').val() );             
           
            app.core.AsyncWebMethod("/fasi/dli/forms/HT5FinancialQuestionnaireUWActions.aspx/OtherLiabilities4Change", false,
                 JSON.stringify({
                     instance: HT5FinancialQuestionnaireUWSupport.InputToObject()
                 }),
                 function (data) {
                     HT5FinancialQuestionnaireUWSupport.ActionProcess(data, 'OtherLiabilities4Change');
           });
      }          
    });
        $('#save').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#save'));
                btnLoading.start();

            app.core.AsyncWebMethod("/fasi/dli/forms/HT5FinancialQuestionnaireUWActions.aspx/saveClick", false,
                JSON.stringify({
                    instance: HT5FinancialQuestionnaireUWSupport.InputToObject()
                }),
                function (data) {
                    btnLoading.stop();

                    HT5FinancialQuestionnaireUWSupport.ActionProcess(data, 'saveClick');
                },
                function () {
                    btnLoading.stop();
                });
            event.preventDefault();
        });
        $('#submit').click(function (event) {
                var formInstance = $("#HT5FinancialQuestionnaireUWMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#submit'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/HT5FinancialQuestionnaireUWActions.aspx/submitClick", false,
                          JSON.stringify({
                                        instance: HT5FinancialQuestionnaireUWSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    HT5FinancialQuestionnaireUWSupport.ActionProcess(data, 'submitClick');
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


        $("#HT5FinancialQuestionnaireUWMainForm").validate({
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
                Year1: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 9999
                },
                Year2: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 9999
                },
                Year3: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 9999
                },
                SalaryYear1: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 9999999999
                },
                SalaryYear2: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                SalaryYear3: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                BusinesReported1: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                BusinesReported2: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                BusinesReported3: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                BonusYear1: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                BonusYear2: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                BonusYear3: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                CommissionYear1: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                CommissionYear2: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                CommissionYear3: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                PensionProfitSharingYear1: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                PensionProfitSharingYear2: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                PensionProfitSharingYear3: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                OtherIncomeYear1: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                OtherIncomeYear2: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                OtherIncomeYear3: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                DescriptionOtherIncome: {
                    required: true,
                    maxlength: 15
                },
                TotalEarnedIncomeYear1: {
                    AutoNumericRequired: true,
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                TotalEarnedIncomeYear2: {
                    AutoNumericRequired: true,
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                TotalEarnedIncomeYear3: {
                    AutoNumericRequired: true,
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                iaYear1: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 9999
                },
                iaYear2: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 9999
                },
                iaYear3: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 9999
                },
                DividendsYear1: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                DividendsYear2: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                DividendsYear3: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                InterestYear1: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                InterestYear2: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                InterestYear3: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                NetRentalsYear1: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                NetRentalsYear2: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                NetRentalsYear3: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                CapitalGainsYear1: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                CapitalGainsYear2: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                CapitalGainsYear3: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                OtherEarnedIncomeYear1: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                OtherEarnedIncomeYear2: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                OtherEarnedIncomeYear3: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                DescriptionOtherEarnedIncome: {
                    required: true,
                    maxlength: 15
                },
                TotalUnearnedIncomeYear1: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                TotalUnearnedIncomeYear2: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                TotalUnearnedIncomeYear3: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                Cash: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999999
                },
                RealEstate: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999999
                },
                BusinessEquity: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999999
                },
                Stocks: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999999
                },
                OtherAssets1: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999999
                },
                DescribeOtherAssets1: {
                    required: true,
                    maxlength: 15
                },
                OtherAssets2: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999999
                },
                DescribeOtherAssets2: {
                    required: true,
                    maxlength: 15
                },
                OtherAssets3: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999999
                },
                DescribeOtherAssets3: {
                    required: true,
                    maxlength: 15
                },
                OtherAssets4: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999999
                },
                DescribeOtherAssets4: {
                    required: true,
                    maxlength: 15
                },
                TotalAssets: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999999
                },
                Mortgages: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999999
                },
                Loans: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999999
                },
                Liens: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999999
                },
                Bonds: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999999
                },
                OtherLiabilities1: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999999
                },
                DescribeOtherLiabilities1: {
                    required: true,
                    maxlength: 15
                },
                OtherLiabilities2: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999999
                },
                DescribeOtherLiabilities2: {
                    required: true,
                    maxlength: 15
                },
                OtherLiabilities3: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999999
                },
                DescribeOtherLiabilities3: {
                    required: true,
                    maxlength: 15
                },
                OtherLiabilities4: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999999
                },
                DescribeOtherLiabilities4: {
                    required: true,
                    maxlength: 15
                },
                TotalLiabilities: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999999
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
                Year1: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 9999'
                },
                Year2: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 9999'
                },
                Year3: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 9999'
                },
                SalaryYear1: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 9999999999'
                },
                SalaryYear2: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                SalaryYear3: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                BusinesReported1: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                BusinesReported2: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                BusinesReported3: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                BonusYear1: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                BonusYear2: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                BonusYear3: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                CommissionYear1: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                CommissionYear2: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                CommissionYear3: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                PensionProfitSharingYear1: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                PensionProfitSharingYear2: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                PensionProfitSharingYear3: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                OtherIncomeYear1: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                OtherIncomeYear2: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                OtherIncomeYear3: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                DescriptionOtherIncome: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                TotalEarnedIncomeYear1: {
                    AutoNumericRequired: 'El campo es requerido',
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                TotalEarnedIncomeYear2: {
                    AutoNumericRequired: 'El campo es requerido',
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                TotalEarnedIncomeYear3: {
                    AutoNumericRequired: 'El campo es requerido',
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                iaYear1: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 9999'
                },
                iaYear2: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 9999'
                },
                iaYear3: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 9999'
                },
                DividendsYear1: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                DividendsYear2: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                DividendsYear3: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                InterestYear1: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                InterestYear2: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                InterestYear3: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                NetRentalsYear1: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                NetRentalsYear2: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                NetRentalsYear3: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                CapitalGainsYear1: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                CapitalGainsYear2: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                CapitalGainsYear3: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                OtherEarnedIncomeYear1: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                OtherEarnedIncomeYear2: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                OtherEarnedIncomeYear3: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                DescriptionOtherEarnedIncome: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                TotalUnearnedIncomeYear1: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                TotalUnearnedIncomeYear2: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                TotalUnearnedIncomeYear3: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                Cash: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999'
                },
                RealEstate: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999'
                },
                BusinessEquity: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999'
                },
                Stocks: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999'
                },
                OtherAssets1: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999'
                },
                DescribeOtherAssets1: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                OtherAssets2: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999'
                },
                DescribeOtherAssets2: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                OtherAssets3: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999'
                },
                DescribeOtherAssets3: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                OtherAssets4: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999'
                },
                DescribeOtherAssets4: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                TotalAssets: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999'
                },
                Mortgages: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999'
                },
                Loans: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999'
                },
                Liens: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999'
                },
                Bonds: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999'
                },
                OtherLiabilities1: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999'
                },
                DescribeOtherLiabilities1: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                OtherLiabilities2: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999'
                },
                DescribeOtherLiabilities2: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                OtherLiabilities3: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999'
                },
                DescribeOtherLiabilities3: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                OtherLiabilities4: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999'
                },
                DescribeOtherLiabilities4: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                TotalLiabilities: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999'
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

    };











  this.Init = function(){
    
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('HT5Cuestionario financiero');
        

    HT5FinancialQuestionnaireUWSupport.ControlBehaviour();
    HT5FinancialQuestionnaireUWSupport.ControlActions();
    HT5FinancialQuestionnaireUWSupport.ValidateSetup();
    HT5FinancialQuestionnaireUWSupport.Initialization();


  };
};

$(document).ready(function () {
   HT5FinancialQuestionnaireUWSupport.Init();
});

