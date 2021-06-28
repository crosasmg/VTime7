function ReloadCaseInformationTab(editProperty) {
	var isTrueEdit = (editProperty == 'True');
	var status = 0;
	var numCaso = $('#dpeCaseId_I').val()
	$("#case-info-container").load("controls/partials/_caseInformation.aspx", function () {
		LoadDefaultValues("ddlRiskClassification", "GetRiskClassType", ProxySyncLookUps).done(function () { });
		var postData = JSON.stringify({
			caseId: numCaso
		});
		if (numCaso != "") {
		    $loading.hide();
			ProxySyncUnderwritingCase.invoke("GetInformationCase", postData, function (data) {
				if (!jQuery.isEmptyObject(data.d)) {
					$('#lblTypeOfCase').html(data.d.UnderwritingCaseType);
					$('#lblLineOfBusiness').html(data.d.LineOfBusiness);
					$('#lblProduct').html(data.d.Product);
					if (data.d.Decision == 3 || data.d.Decision == 4) {
					    $('#lblPolicyCertificate').html(data.d.PolicyId + ' / ' + data.d.CertificateId);
					} else {
					    $('#lblPolicyCertificate').html(data.d.PolicyId);
					}
					$('#lblInsuredAmount').html(data.d.FaceAmount);
					$('#lblBatchNumber').html(data.d.BatchNumber);
					if (data.d.UnderwriterEdit == 0)
						$('#lblUnderwriterEditingCase').html("");
					else
						$('#lblUnderwriterEditingCase').html(data.d.UnderwriterEdit);
					if (data.d.UnderwriterUpdate == 0)
						$('#lblUnderwriterEditedCase').html("");
					else
						$('#lblUnderwriterEditedCase').html(data.d.UnderwriterUpdate);
					if (data.d.Reason == null) {
						$('#lblRejectionReason').css("visibility", "hidden");
					} else {
						$('#lblRejectionReason').html(data.d.Reason);
					}
					$('#lblTotalBalance').html(data.d.Balance);
					if (data.d.OpenDate.length === 0)
						$('#lblOpeningDate').css("visibility", "hidden");
					else
						$('#lblOpeningDate').html(data.d.OpenDate);
					if (data.d.CloseDate.length === 0)
						$('#lblClosingDate').css("visibility", "hidden");
					else
						$('#lblClosingDate').html(data.d.CloseDate);
					$("#ddlRiskClassification").val(data.d.RiskClassification);
					status = data.d.Status;
					if (isTrueEdit && status != 3) {
						$('#ddlRiskClassification').removeProp('disabled');
					}
					$('#div-info-case').css("visibility", "visible");
				}
			});
		}
		$("#ddlRiskClassification").change(function () {
			var postData = JSON.stringify({
				value: $("#ddlRiskClassification").val()
			});
			ProxyAsyncUnderwritingCase.invoke("SaveRiskClasification", postData, function () {});
		});
	});
}
