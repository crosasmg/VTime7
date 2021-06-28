<%@ Page Language="VB" AutoEventWireup="false" CodeFile="_caseInformation.aspx.vb" Inherits="Underwriting_Controls_Partials_caseInformation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<style>
		.normal-font{
			font-weight: 100 !important;
		}
		.padding-group{
			padding-left:3px;
			padding-top:10px;
		}
		.margin-label-col1{
			margin-left: -70px;
		}
		.margin-label-col2{
			margin-left: -50px;
		}
		.margin-label-title{
			margin-left: 20px;
		}
		.invisible-label{
			visibility:hidden;
		}
	</style>
</head>
<body>
	<form id="form-add-attachment" name="form-add-attachment" action="" class="form-horizontal">
		<div class="form-group padding-group" id="div-info-case" style="visibility:hidden;">
			<div class="col-md-3">
				<label for="lblTypeOfCase" class="control-label"><% Response.Write(GetLocalResourceObject("TypeOfCase"))%>:</label>
			</div>
			<div class="col-md-3">
				<label class="control-label normal-font" id="lblTypeOfCase">Nuevo Negocio</label>
			</div>
			<div class="col-md-2">
				<label for="lblOpeningDate" class="control-label"><% Response.Write(GetLocalResourceObject("OpeningDate"))%>:</label>
			</div>
			<div class="col-md-3 ">
 				<label class="control-label normal-font" id="lblOpeningDate">00/00/0000</label>
			</div>
			<div class="col-md-3">
				<label for="lblLineOfBusiness" class="control-label"><% Response.Write(GetLocalResourceObject("LineOfBusiness"))%>:</label>
			</div>
			<div class="col-md-3">
				<label class="control-label normal-font" id="lblLineOfBusiness">Vida individual</label>
			</div>
			<div class="col-md-2">
				<label for="lblClosingDate" class="control-label"><% Response.Write(GetLocalResourceObject("ClosingDate"))%>:</label>
			</div>
			<div class="col-md-3">
				<label class="control-label normal-font" id="lblClosingDate">00/00/0000</label>
			</div>
			<div class="col-md-3">
				<label for="lblProduct" class="control-label"><% Response.Write(GetLocalResourceObject("Product"))%>:</label>
			</div>
			<div class="col-md-3">
				<label class="control-label normal-font" id="lblProduct">Mi vida a Dorada</label>
			</div>
			<div class="col-md-2">
				<label for="ddlRiskClassification" class="control-label"><% Response.Write(GetLocalResourceObject("RiskClassification"))%>:</label>
			</div>
			<div class="col-md-3">
				<select class="form-control" id="ddlRiskClassification" disabled="disabled" name="ddlRiskClassification" title="<% Response.Write(GetLocalResourceObject("RiskClassification"))%>">
					<option />
				</select>
			</div>
			<div class="col-md-3">
				<label for="lblPolicyCertificate" class="control-label"><% Response.Write(GetLocalResourceObject("PolicyCertificate"))%>:</label>
			</div>
			<div class="col-md-3">
				<label class="control-label normal-font" id="lblPolicyCertificate">Poliza numero 21</label>
			</div>
			<div class="col-md-2">
				<label for="lblTotalBalance" class="control-label"><% Response.Write(GetLocalResourceObject("TotalBalance"))%>:</label>
			</div>
			<div class="col-md-3">
				<label class="control-label normal-font" id="lblTotalBalance">-40</label>
			</div>
			<div class="col-md-3">
				<label for="lblInsuredAmount" class="control-label"><% Response.Write(GetLocalResourceObject("InsuredAmount"))%>:</label>
			</div>
			<div class="col-md-3">
				<label class="control-label normal-font" id="lblInsuredAmount">22.000</label>
			</div>
			<div class="col-md-2">
				<label for="lblRejectionReason" class="control-label"><% Response.Write(GetLocalResourceObject("RejectionReason"))%>:</label>
			</div>
			<div class="col-md-3">
				<label class="control-label normal-font" id="lblRejectionReason">o</label>
			</div>
			<div class="col-md-3">
				<label for="lblBatchNumber" class="control-label"><% Response.Write(GetLocalResourceObject("BatchNumber"))%>:</label>
			</div>
			<div class="col-md-3">
				<label class="control-label normal-font" id="lblBatchNumber">1564984</label>
			</div>
			<div class="col-md-3">
				<label class="control-label invisible-label"><% Response.Write(GetLocalResourceObject("BatchNumber"))%>:</label>
			</div>
			<div class="col-md-3">
				<label class="control-label normal-font invisible-label">1564984</label>
			</div>
			<div class="col-md-3">
				<label for="lblUnderwriterEditingCase" class="control-label"><% Response.Write(GetLocalResourceObject("UnderwriterEditingCase"))%>:</label>
			</div>	
			<div class="col-md-3">	
				<label class="control-label normal-font" id="lblUnderwriterEditingCase">15654 Edward Perdomo</label>
			</div>
			<div class="col-md-2">
				<label for="lblUnderwriterEditedCase" class="control-label"><% Response.Write(GetLocalResourceObject("UnderwriterEditedCase"))%>:</label>
			</div>
			<div class="col-md-3">
				<label class="control-label normal-font" id="lblUnderwriterEditedCase">15654 Edward Perdomo</label>
			</div>
		</div>
	</form>
</body>
</html>
