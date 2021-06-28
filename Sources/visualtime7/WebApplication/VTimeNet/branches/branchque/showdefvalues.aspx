<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values

Dim lstrFields As String
Dim lintControl As String



'------------------------------------------------------------------------------------------------
'% insShowAUC001: Muestra los valores de acuerdo a una condición
Private Sub insShowAUC001()
	'------------------------------------------------------------------------------------------------
	Dim lclsAuto_db As eBranches.Auto_db
	lclsAuto_db = New eBranches.Auto_db
	
	If lclsAuto_db.FindReapolicyAuto(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)) Then
		
		Response.Write("top.fraHeader.document.forms[0].cbeBranch.value=" & mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble) & ";")
		Response.Write("top.fraHeader.document.forms[0].cbeBranch.disabled=true;")
		Response.Write("top.fraHeader.document.forms[0].tctCapital.value=" & lclsAuto_db.nCapital & ";")
		Response.Write("top.fraHeader.document.forms[0].tctCapital.disabled=true;")
		Response.Write("top.fraHeader.document.forms[0].cbePayFreq.value=" & lclsAuto_db.nPayfreq & ";")
		Response.Write("top.fraHeader.document.forms[0].cbePayFreq.disabled=true;")
		Response.Write("top.fraHeader.document.forms[0].tctPremium.value=" & lclsAuto_db.nPremium & ";")
		Response.Write("top.fraHeader.document.forms[0].tctPremium.disabled=true;")
		Response.Write("top.fraHeader.document.forms[0].valProduct.Parameters.Param1.sValue=" & Request.QueryString.Item("nBranch") & ";")
		Response.Write("top.fraHeader.document.forms[0].valProduct.value=" & Request.QueryString.Item("nProduct") & ";")
		Response.Write("top.fraHeader.$('#valProduct').change();")
		Response.Write("top.fraHeader.document.forms[0].valProduct.disabled=true;")
		Response.Write("top.fraHeader.document.forms[0].tcdEffecDate.value='" & mobjValues.TypeToString(lclsAuto_db.dEffecdate, eFunctions.Values.eTypeData.etdDate) & "';")
		Response.Write("top.fraHeader.document.forms[0].tcdEffecDate.disabled=true;")
		Response.Write("top.fraHeader.document.forms[0].tcdNullDate.value='" & mobjValues.TypeToString(lclsAuto_db.dExpirdat, eFunctions.Values.eTypeData.etdDate) & "';")
		Response.Write("top.fraHeader.document.forms[0].tcdNullDate.disabled=true;")
		Select Case lclsAuto_db.sLicense_ty
			Case "1"
				Response.Write("top.fraHeader.document.forms[0].optLicence[0].checked=true;")
			Case "2"
				Response.Write("top.fraHeader.document.forms[0].optLicence[1].checked=true;")
		End Select
		Response.Write("top.fraHeader.document.forms[0].optLicence[0].disabled=true;")
		Response.Write("top.fraHeader.document.forms[0].optLicence[1].disabled=true;")
		Select Case lclsAuto_db.sPolitype
			Case "1"
				Response.Write("top.fraHeader.document.forms[0].optTypePolicy[0].checked=true;")
			Case "2"
				Response.Write("top.fraHeader.document.forms[0].optTypePolicy[1].checked=true;")
			Case "3"
				Response.Write("top.fraHeader.document.forms[0].optTypePolicy[2].checked=true;")
		End Select
		Response.Write("top.fraHeader.document.forms[0].optTypePolicy[0].disabled=true;")
		Response.Write("top.fraHeader.document.forms[0].optTypePolicy[1].disabled=true;")
		Response.Write("top.fraHeader.document.forms[0].optTypePolicy[2].disabled=true;")
		
		Response.Write("top.fraHeader.document.forms[0].cbeZone.value=" & lclsAuto_db.nAutoZone & ";")
		Response.Write("top.fraHeader.document.forms[0].cbeType.value=" & lclsAuto_db.nVehType & ";")
		Response.Write("top.fraHeader.document.forms[0].cbeVehMark.value=" & lclsAuto_db.nVehBrand & ";")
		Response.Write("top.fraHeader.document.forms[0].tctMotor.value='" & lclsAuto_db.sMotor & "';")
		Response.Write("top.fraHeader.document.forms[0].tctChassis.value='" & lclsAuto_db.sChassis & "';")
		Response.Write("top.fraHeader.document.forms[0].tctColor.value='" & lclsAuto_db.sColor & "';")
		Response.Write("top.fraHeader.document.forms[0].tctRegister.value='" & lclsAuto_db.sRegist & "';")
		Response.Write("top.fraHeader.document.forms[0].tctLVehModel.value='" & lclsAuto_db.sVehModel & "';")
		Response.Write("top.fraHeader.document.forms[0].tctLVehModel.disabled=true;")
	End If
	lclsAuto_db = Nothing
End Sub

'------------------------------------------------------------------------------------------------
'% insShowINC001: Muestra los valores de acuerdo a una condición
Private Sub insShowINC001()
	'dim eRemoteDB.Constants.intNull As Integer
	'------------------------------------------------------------------------------------------------
	Dim lclsFire As eBranches.Fire
	lclsFire = New eBranches.Fire
	If lclsFire.FindReapolicyFire(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("ncertif"), eFunctions.Values.eTypeData.etdDouble)) Then
		
		Response.Write("opener.document.forms[0].cbeBranch.value=" & mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble) & ";")
		Response.Write("opener.document.forms[0].cbeBranch.disabled=true;")
		'+ Suponiendo que no deberia haber capital menor a 0 
		If lclsFire.nCapital < 0 Then
			Response.Write("opener.document.forms[0].tctCapital.value=" & 0 & ";")
		Else
			Response.Write("opener.document.forms[0].tctCapital.value=" & lclsFire.nCapital & ";")
		End If
		Response.Write("opener.document.forms[0].tctCapital.disabled=true;")
		Response.Write("opener.document.forms[0].cbePayFreq.value=" & lclsFire.nPayfreq & ";")
		
		Response.Write("opener.document.forms[0].cbePayFreq.disabled=true;")
		If lclsFire.nPremium >= 0 Then
			Response.Write("opener.document.forms[0].tctPremium.value=" & lclsFire.nPremium & ";")
		Else
			Response.Write("opener.document.forms[0].tctPremium.value=" & 0 & ";")
		End If
		Response.Write("opener.document.forms[0].tctPremium.disabled=true;")
		Response.Write("opener.document.forms[0].valProduct.value=" & mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble) & ";")
		Response.Write("opener.$('#valProduct').change();")
		Response.Write("opener.document.forms[0].valProduct.disabled=true;")
		Response.Write("opener.document.forms[0].tcdEffecDate.value='" & mobjValues.StringToType(CStr(lclsFire.dEffecdate), eFunctions.Values.eTypeData.etdDate) & "';")
		Response.Write("opener.document.forms[0].tcdEffecDate.disabled=true;")
		Response.Write("opener.document.forms[0].tcdNullDate.value='" & mobjValues.StringToType(CStr(lclsFire.dExpirdat), eFunctions.Values.eTypeData.etdDate) & "';")
		Response.Write("opener.document.forms[0].tcdNullDate.disabled=true;")
		Select Case lclsFire.sPolitype
			Case "1"
				Response.Write("opener.document.forms[0].optTypePolicy[0].checked=true;")
			Case "2"
				Response.Write("opener.document.forms[0].optTypePolicy[1].checked=true;")
			Case "3"
				Response.Write("opener.document.forms[0].optTypePolicy[2].checked=true;")
		End Select
		Response.Write("opener.document.forms[0].optTypePolicy[0].disabled=true;")
		Response.Write("opener.document.forms[0].optTypePolicy[1].disabled=true;")
		Response.Write("opener.document.forms[0].optTypePolicy[2].disabled=true;")
		Response.Write("opener.document.forms[0].cbeArticle.value=" & lclsFire.nArticle & ";")
		Response.Write("opener.document.forms[0].cbeArticle.disabled=true;")
		Response.Write("opener.document.forms[0].valDetailArt.Parameters.Param1.sValue=" & lclsFire.nArticle & ";")
		If lclsFire.nDetailArt <> eRemoteDB.Constants.intNull Then
			Response.Write("opener.document.forms[0].valDetailArt.value=" & lclsFire.nDetailArt & ";")
		End If
		'Response.Write "opener.document.forms[0].valDetailArt.value=true;"
		Response.Write("opener.$('#valDetailArt').change();")
		Response.Write("opener.document.forms[0].cbeActivityCat.value=" & lclsFire.nActivityCat & ";")
		Response.Write("opener.document.forms[0].cbeActivityCat.disable=true;")
		Response.Write("opener.document.forms[0].cbeConstCat.value=" & lclsFire.nConstCat & ";")
		Response.Write("opener.document.forms[0].cbeConstCat.disable=true;")
		If lclsFire.nFloor_quan = eRemoteDB.Constants.intNull Then
			Response.Write("opener.document.forms[0].tctFloor_quan.value=" & 0 & ";")
		Else
			Response.Write("opener.document.forms[0].tctFloor_quan.value=" & mobjValues.StringToType(CStr(lclsFire.nFloor_quan), eFunctions.Values.eTypeData.etdDouble) & ";")
		End If
		If lclsFire.nSpCombType = eRemoteDB.Constants.intNull Then
			Response.Write("opener.document.forms[0].cbeCombType.value=" & 0 & ";")
		Else
			Response.Write("opener.document.forms[0].cbeCombType.value=" & lclsFire.nSpCombType & ";")
		End If
		If lclsFire.nSideCloseType = eRemoteDB.Constants.intNull Then
			Response.Write("opener.document.forms[0].cbeSideCloseType.value=" & 0 & ";")
		Else
			Response.Write("opener.document.forms[0].cbeSideCloseType.value=" & lclsFire.nSideCloseType & ";")
		End If
		If lclsFire.nIndPeriod = eRemoteDB.Constants.intNull Then
			Response.Write("opener.document.forms[0].tctIndPeriod.value=" & 0 & ";")
		Else
			Response.Write("opener.document.forms[0].tctIndPeriod.value=" & lclsFire.nIndPeriod & ";")
		End If
		Response.Write("opener.document.forms[0].cbeRoofType.value=" & lclsFire.nRoofType & ";")
		
		
	End If
	lclsFire = Nothing
End Sub
'------------------------------------------------------------------------------------------------
'% insShowVIC005: Muestra los valores de acuerdo a una condición
Private Sub insShowVIC005()
	'------------------------------------------------------------------------------------------------
	Dim lclsLife As ePolicy.Life
	Dim lclsPolicy As ePolicy.Policy
	lclsLife = New ePolicy.Life
	lclsPolicy = New ePolicy.Policy
	
	If lclsLife.Find("2", CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), CDbl(Request.QueryString.Item("nPolicy")), CDbl(Request.QueryString.Item("nCertif")), CDate(Request.QueryString.Item("dEffecdate")), True) Then
		Call lclsPolicy.ValExistPolicyRec(CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), CDbl(Request.QueryString.Item("nPolicy")), Session("sTypeCompanyUser"))
		With Response
			.Write("top.fraHeader.document.forms[0].cbeBranch.value=" & mobjValues.TypeToString(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble) & ";")
			.Write("top.fraHeader.document.forms[0].cbeBranch.disabled=true;")
			.Write("top.fraHeader.document.forms[0].cbeBranch.onchange();")
			.Write("top.fraHeader.document.forms[0].tcnCapital.value='" & mobjValues.TypeToString(lclsLife.nCapital, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
			.Write("top.fraHeader.document.forms[0].tcnCapital.disabled=true;")
			.Write("top.fraHeader.document.forms[0].tcnAge.value='" & mobjValues.TypeToString(lclsLife.nAge, eFunctions.Values.eTypeData.etdDouble) & "';")
			.Write("top.fraHeader.document.forms[0].tcnAge.disabled=true;")
			.Write("top.fraHeader.document.forms[0].tcnAge_reinsu.value='" & mobjValues.TypeToString(lclsLife.nAge_reinsu, eFunctions.Values.eTypeData.etdDouble) & "';")
			.Write("top.fraHeader.document.forms[0].tcnAge_reinsu.disabled=true;")
			.Write("top.fraHeader.document.forms[0].tcdEffecdate.value='" & mobjValues.TypeToString(lclsPolicy.dStartdate, eFunctions.Values.eTypeData.etdDate) & "';")
			.Write("top.fraHeader.document.forms[0].tcdEffecdate.disabled=true;")
			.Write("top.fraHeader.document.forms[0].tcdExpirdat.value='" & mobjValues.TypeToString(lclsPolicy.dExpirdat, eFunctions.Values.eTypeData.etdDate) & "';")
			.Write("top.fraHeader.document.forms[0].tcdExpirdat.disabled=true;")
			.Write("top.fraHeader.document.forms[0].cbePayfreq.value=" & mobjValues.TypeToString(lclsPolicy.nPayfreq, eFunctions.Values.eTypeData.etdDouble) & ";")
			.Write("top.fraHeader.document.forms[0].cbePayfreq.disabled=true;")
			.Write("top.fraHeader.document.forms[0].tcnPremium.value='" & mobjValues.TypeToString(lclsLife.nPremium, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
			.Write("top.fraHeader.document.forms[0].tcnPremium.disabled=true;")
			.Write("top.fraHeader.document.forms[0].valProduct.value=" & mobjValues.TypeToString(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble) & ";")
			.Write("top.fraHeader.$('#valProduct').change();")
			Select Case lclsPolicy.sPolitype
				Case "1"
					Response.Write("top.fraHeader.document.forms[0].optTypePol[0].checked=true;")
				Case "2"
					Response.Write("top.fraHeader.document.forms[0].optTypePol[1].checked=true;")
			End Select
		End With
	End If
	lclsLife = Nothing
End Sub

'% insShowAuto_db: Se muestran los datos asociados al codigo de auto seleccionado
'%                 Se utiliza para el campo Código del vehiculo de la página BVC001.aspx
'--------------------------------------------------------------------------------------------
Sub insShowAuto_db()
	'--------------------------------------------------------------------------------------------
	Dim lclsAuto As ePolicy.Automobile
	lclsAuto = New ePolicy.Automobile
	If lclsAuto.Find_Tab_au_veh(Request.QueryString.Item("sVehcode")) Then
		With Response
			.Write("top.frames['fraFolder'].document.forms[0].cboDescBrand.value='" & mobjValues.TypeToString(lclsAuto.nVehBrand, eFunctions.Values.eTypeData.etdDouble) & "';")
			.Write("top.frames['fraFolder'].document.forms[0].tctVehmodel.value='" & lclsAuto.sVehModel1 & "';")
		End With
	End If
	
	lclsAuto = Nothing
End Sub

'% insShowData_Auto:  Se muestran los datos asociados al auto seleccionado,
'%					   si el número de placa ya está registrado en el sistema
'%					   Se utiliza en el campo Matrícula de la ventana AU001.aspx
'--------------------------------------------------------------------------------------------
Sub insShowData_Auto()
	'--------------------------------------------------------------------------------------------
	Dim lclsAuto As ePolicy.Automobile
	Dim lobjValues As eFunctions.Values
	
	lclsAuto = New ePolicy.Automobile
	lobjValues = New eFunctions.Values
	
	With Response
		If Request.QueryString.Item("Field") = "tctMotor" Then
			If IsNothing(Request.QueryString.Item("sMotor")) Then
				Call insDisabled()
			Else
				lstrFields = Request.QueryString.Item("sMotor")
				lintControl = "1"
				Call insFindAuto_db()
				.Write("top.frames['fraFolder'].document.forms[0].tctMotor.disabled=false;")
			End If
		ElseIf Request.QueryString.Item("Field") = "tctChassis" Then 
			If IsNothing(Request.QueryString.Item("sChassis")) Then
				Call insDisabled()
			Else
				lstrFields = Request.QueryString.Item("sChassis")
				lintControl = "2"
				Call insFindAuto_db()
				.Write("top.frames['fraFolder'].document.forms[0].tctChassis.disabled=false;")
			End If
		ElseIf Request.QueryString.Item("Field") = "tctRegist" Then 
			If IsNothing(Request.QueryString.Item("sRegist")) Then
				Call insDisabled()
			Else
				lstrFields = Request.QueryString.Item("sRegist")
				lintControl = "3"
				Call insFindAuto_db()
				.Write("top.frames['fraFolder'].document.forms[0].tctRegist.disabled=false;")
			End If
		ElseIf Request.QueryString.Item("Field") = "cbeLicense_ty" Then 
			If Request.QueryString.Item("sLicense_ty") = "3" Then
				If lclsAuto.next_seqregistauto() Then
					.Write("top.frames['fraFolder'].document.forms[0].tctRegist.value=" & lclsAuto.sRegist & ";")
					.Write("top.frames['fraFolder'].document.forms[0].tctRegist.disabled=true;")
				End If
			End If
		End If
	End With
	lobjValues = Nothing
	lclsAuto = Nothing
End Sub

'% insFindAuto_db: Rescata datos de base de datos de auto segun motor, chassis o patente
'--------------------------------------------------------------------------------------------
Sub insFindAuto_db()
	'--------------------------------------------------------------------------------------------
	Dim lclsAuto_db As ePolicy.Auto_db
	Dim lclsDigitClient As eClaim.Claim
	
	lclsAuto_db = New ePolicy.Auto_db
	lclsDigitClient = New eClaim.Claim
	
	With Response
		If lclsAuto_db.insValExistFields(Trim(lstrFields), mobjValues.StringToType(lintControl, eFunctions.Values.eTypeData.etdDouble)) Then
			.Write("top.frames['fraFolder'].document.forms[0].tctMotor.value=""" & lclsAuto_db.sMotor & """;")
			.Write("top.frames['fraFolder'].document.forms[0].tctChassis.value=""" & lclsAuto_db.sChassis & """;")
			.Write("top.frames['fraFolder'].document.forms[0].tctRegist.value=""" & lclsAuto_db.sRegist & """;")
			.Write("top.frames['fraFolder'].document.forms[0].cbeLicense_ty.value=""" & lclsAuto_db.sLicense_ty & """;")
			.Write("top.frames['fraFolder'].document.forms[0].tcnClient.value=""" & lclsAuto_db.sVeh_own & """;")
			.Write("top.frames['fraFolder'].document.forms[0].tcnClient_Digit.value=""" & lclsDigitClient.CalcDigit(lclsAuto_db.sVeh_own) & """;")
			.Write("top.frames['fraFolder'].UpdateDiv('tctClieName','" & lclsAuto_db.sVehownName & "','popup');")
			.Write("top.frames['fraFolder'].document.forms[0].cboVehCode.value=""" & lclsAuto_db.sVehCode & """;")
			.Write("top.frames['fraFolder'].document.forms[0].tctVehmodel.value=""" & lclsAuto_db.sVehModel & """;")
			.Write("top.frames['fraFolder'].document.forms[0].cboDescBrand.value=""" & lclsAuto_db.nVehBrand & """;")
			.Write("top.frames['fraFolder'].document.forms[0].tctColor.value=""" & lclsAuto_db.sColor & """;")
			.Write("top.frames['fraFolder'].document.forms[0].tcnYear.value=""" & lclsAuto_db.nYear & """;")
			.Write("top.frames['fraFolder'].document.forms[0].cbeVestatus.value=""" & lclsAuto_db.nVestatus & """;")
			
			.Write("top.frames['fraFolder'].document.forms[0].tctMotor.disabled=true;")
			.Write("top.frames['fraFolder'].document.forms[0].tctChassis.disabled=true;")
			.Write("top.frames['fraFolder'].document.forms[0].cbeLicense_ty.disabled=true;")
			.Write("top.frames['fraFolder'].document.forms[0].tctRegist.disabled=true;")
			.Write("top.frames['fraFolder'].document.forms[0].tcnClient.disabled=true;")
			.Write("top.frames['fraFolder'].document.forms[0].tcnClient_Digit.disabled=true;")
			.Write("top.frames['fraFolder'].document.forms[0].btntcnClient.disabled=true;")
			.Write("top.frames['fraFolder'].document.forms[0].cboVehCode.disabled=true;")
			.Write("top.frames['fraFolder'].document.forms[0].tctVehmodel.disabled=true;")
			.Write("top.frames['fraFolder'].document.forms[0].cboDescBrand.disabled=true;")
			.Write("top.frames['fraFolder'].document.forms[0].tctColor.disabled=true;")
			.Write("top.frames['fraFolder'].document.forms[0].tcnYear.disabled=true;")
			.Write("top.frames['fraFolder'].document.forms[0].cbeVestatus.disabled=true;")
		Else
			Call insDisabled()
		End If
	End With
	lclsAuto_db = Nothing
	lclsDigitClient = Nothing
End Sub

'% insDisabled: Se limpian las variables 
'--------------------------------------------------------------------------------------------
Sub insDisabled()
	'--------------------------------------------------------------------------------------------
	With Response
		.Write("top.frames['fraFolder'].document.forms[0].tctMotor.value=""" & "" & """;")
		.Write("top.frames['fraFolder'].document.forms[0].tctChassis.value=""" & "" & """;")
		.Write("top.frames['fraFolder'].document.forms[0].cbeLicense_ty.value=""" & "" & """;")
		.Write("top.frames['fraFolder'].document.forms[0].tctRegist.value=""" & "" & """;")
		.Write("top.frames['fraFolder'].document.forms[0].tcnClient.value=""" & "" & """;")
		.Write("top.frames['fraFolder'].document.forms[0].tcnClient_Digit.value=""" & "" & """;")
		.Write("top.frames['fraFolder'].UpdateDiv('tctClieName','','popup');")
		.Write("top.frames['fraFolder'].document.forms[0].cboVehCode.value=""" & "" & """;")
		.Write("top.frames['fraFolder'].document.forms[0].tctVehmodel.value=""" & "" & """;")
		.Write("top.frames['fraFolder'].document.forms[0].cboDescBrand.value=""" & "" & """;")
		.Write("top.frames['fraFolder'].document.forms[0].tctColor.value=""" & "" & """;")
		.Write("top.frames['fraFolder'].document.forms[0].tcnYear.value=""" & "" & """;")
		.Write("top.frames['fraFolder'].document.forms[0].cbeVestatus.value=""" & "" & """;")
		
		.Write("top.frames['fraFolder'].document.forms[0].tctMotor.disabled=false;")
		.Write("top.frames['fraFolder'].document.forms[0].tctChassis.disabled=false;")
		.Write("top.frames['fraFolder'].document.forms[0].cbeLicense_ty.disabled=false;")
		.Write("top.frames['fraFolder'].document.forms[0].tctRegist.disabled=false;")
		.Write("top.frames['fraFolder'].document.forms[0].tcnClient.disabled=false;")
		.Write("top.frames['fraFolder'].document.forms[0].tcnClient_Digit.disabled=false;")
		.Write("top.frames['fraFolder'].document.forms[0].btntcnClient.disabled=false;")
		.Write("top.frames['fraFolder'].document.forms[0].cboVehCode.disabled=false;")
		.Write("top.frames['fraFolder'].document.forms[0].tctVehmodel.disabled=false;")
		.Write("top.frames['fraFolder'].document.forms[0].cboDescBrand.disabled=false;")
		.Write("top.frames['fraFolder'].document.forms[0].tctColor.disabled=false;")
		.Write("top.frames['fraFolder'].document.forms[0].tcnYear.disabled=false;")
		.Write("top.frames['fraFolder'].document.forms[0].cbeVestatus.disabled=false;")
		
	End With
End Sub

'**% insShowVIC012_K: Displays the values according to a condition.
'% insShowVIC012_K: Muestra los valores de acuerdo a una condición.
'------------------------------------------------------------------------------------------------
Private Sub insShowVIC012_K()
	'------------------------------------------------------------------------------------------------
	Dim lclsFund_inv As ePolicy.Fund_inv
	Dim ldblQuan_avail As Double
	
	lclsFund_inv = New ePolicy.Fund_inv
	
	ldblQuan_avail = 0
	
	If lclsFund_inv.Find(CInt(Request.QueryString.Item("cbeFund"))) Then
		ldblQuan_avail = lclsFund_inv.nQuan_avail
	End If
	
	Response.Write("UpdateDiv('nUnitsAvail','" & mobjValues.TypeToString(ldblQuan_avail, eFunctions.Values.eTypeData.etdDouble, True, 5) & "','PopUp');")
	
	lclsFund_inv = Nothing
End Sub

'**% insShowVIC014_K: Displays the values according to a condition
'% insShowVIC014_K: Muestra los valores de acuerdo a una condición
'------------------------------------------------------------------------------------------------
Private Sub insShowVIC014_K()
	'------------------------------------------------------------------------------------------------
	Dim lclsFund_inv As ePolicy.Fund_inv
	Dim ldblQuan_avail As Double
	
	lclsFund_inv = New ePolicy.Fund_inv
	
	ldblQuan_avail = 0
	
	If lclsFund_inv.Find(CInt(Request.QueryString.Item("cbeFund"))) Then
		ldblQuan_avail = lclsFund_inv.nQuan_avail
	End If
	
	Session("nUnitsAvailable") = ldblQuan_avail
	
	Response.Write("UpdateDiv('nInitialBalance','" & mobjValues.TypeToString(ldblQuan_avail, eFunctions.Values.eTypeData.etdDouble, True, 5) & "','PopUp');")
	
	lclsFund_inv = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
%> 
<HTML>

<HEAD>
    <%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 13.31 $|$$Author: Nvaplat60 $"
</SCRIPT>
</HEAD>
<BODY>
	<FORM NAME="ShowValues">
	</FORM>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("sCodispl")
	Case "AUC001"
		Call insShowAUC001()
	Case "INC001"
		Call insShowINC001()
	Case "VIC005"
		Call insShowVIC005()
	Case "Auto_db"
		Call insShowAuto_db()
	Case "VIC012_K"
		Call insShowVIC012_K()
	Case "VIC014_K"
		Call insShowVIC014_K()
End Select

Select Case Request.QueryString.Item("Field")
	Case "cboVehCode"
		Call insShowAuto_db()
	Case "tctChassis"
		Call insShowData_Auto()
	Case "tctMotor"
		Call insShowData_Auto()
	Case "cbeLicense_ty"
		Call insShowData_Auto()
	Case "tctRegist"
		Call insShowData_Auto()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing

mobjValues = Nothing

%>




