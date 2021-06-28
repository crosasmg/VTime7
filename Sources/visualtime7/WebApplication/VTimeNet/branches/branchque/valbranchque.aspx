<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

Dim mstrErrors As String
Dim mobjValues As eFunctions.Values
Dim mobjBranchQue As Object

'- Contador para uso genérico.
Dim mintCount As Object

'- Variable auxiliar para pase de valores del encabezado al folder
Dim mstrString As String
Dim mstrString2 As Object

'+ Auxiliar que contiene el número del elemento seleccionado de la colección.	
Dim mintAux As Object


'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String


'% insvalBranchQue: Se realizan las validaciones de las formas
'--------------------------------------------------------------------------------------------
Function insvalBranchQue() As String
	'--------------------------------------------------------------------------------------------
	Select Case Request.QueryString.Item("sCodispl")
		'+ AUC001: Póliza de automóvil
		Case "AUC001"
			mobjBranchQue = New eBranches.Auto_db
			With Request
				insvalBranchQue = mobjBranchQue.insVal_AUC001_K("AUC001", "QUERY", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbePayFreq"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdNullDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("optTypePolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("optLicence"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctRegister"), .Form.Item("tctMotor"), .Form.Item("tctChassis"), .Form.Item("tctColor"), mobjValues.StringToType(.Form.Item("cbeVehMark"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctLVehModel"), mobjValues.StringToType(.Form.Item("cbeType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeZone"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+ BVC001: Consulta de Base de datos de vehiculos
		Case "BVC001"
			mobjBranchQue = New ePolicy.Auto_db
			With Request
				If CDbl(.QueryString.Item("nZone")) = 2 Then
					insvalBranchQue = mobjBranchQue.insValBVC001("BVC001", .Form.Item("tctChassis"), .Form.Item("tctMotor"), .Form.Item("cbeLicense_ty"), .Form.Item("tctRegist"), .Form.Item("tcnClient"), .Form.Item("cboVehCode"), .Form.Item("cboDescBrand"), .Form.Item("tctVehmodel"), .Form.Item("tctColor"), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeVestatus"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			'+ INC001: Poliza de incendio
			
		Case "INC001"
			mobjBranchQue = New eBranches.Fire
			With Request
				insvalBranchQue = mobjBranchQue.insVal_INC001_K("AUC001", "QUERY", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbePayFreq"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdNullDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("optTypePolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctArticle"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctDetailArt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctActivityCat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctConstCat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctFloor_quan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctSpCombType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctSideCloseType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctIndPeriod"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctRoofType"), eFunctions.Values.eTypeData.etdDouble))
				
			End With
			
			'+ VIC005: Consulta de pólizas de Vida		
		Case "VIC005"
			mobjBranchQue = New eBranches.Life
			With Request
				insvalBranchQue = mobjBranchQue.insValVIC005("VIC005", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optTypePol"), mobjValues.StringToType(.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdExpirdat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbePayfreq"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_reinsu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+ VAC631: Consulta de pólizas de VidActiva 
		Case "VAC631"
			mobjBranchQue = New eBranches.ActiveLife
			With Request
				insvalBranchQue = mobjBranchQue.insValVAC631("VAC630", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("valClient"), .Form.Item("cbeStatusva"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOption"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbePayFreq"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCapitalDeath"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPremdeal"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeAgreement"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeTypeinvest"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnIntproject"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnWarminint"), eFunctions.Values.eTypeData.etdDouble, True))
			End With
			
			'**+ VIC012: Activity of a fund.
			'+ VIC012: Actividad de un fondo.
			
		Case "VIC012"
			mobjBranchQue = New ePolicy.Fund_move
			
			insvalBranchQue = vbNullString
			
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalBranchQue = mobjBranchQue.insValVIC012_k(Request.Form.Item("sCodispl"), mobjValues.StringToType(.Form.Item("cbeFund"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdDate"), eFunctions.Values.eTypeData.etdDate))
				End If
			End With
			
			'**+ VIC013: Summary of the Activity of a Fund.
			'+ VIC013: Resumen de la actividad de un fondo.
			
		Case "VIC013"
			mobjBranchQue = New ePolicy.Fund_stock
			
			insvalBranchQue = vbNullString
			
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalBranchQue = mobjBranchQue.insValVIC013_k(Request.Form.Item("sCodispl"), mobjValues.StringToType(.Form.Item("tcdDate"), eFunctions.Values.eTypeData.etdDate))
				End If
			End With
			
			'**+ VIC014: Entries to an Investment Fund.
			'+ VIC014: Transacciones de un fondo de inversión.
			
		Case "VIC014"
			mobjBranchQue = New ePolicy.Fund_stock
			
			insvalBranchQue = vbNullString
			
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalBranchQue = mobjBranchQue.insValVIC014_K(Request.Form.Item("sCodispl"), mobjValues.StringToType(.Form.Item("cbeFund"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdDate"), eFunctions.Values.eTypeData.etdDate))
				End If
			End With
			
		Case Else
			insvalBranchQue = "insvalBranchQue: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostBranchQue: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostBranchQue() As Boolean
	Dim ldblQuan_avail_1 As Double
	Dim ldblQuan_avail As Double
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	lblnPost = False
	
	Dim lclsFund_inv As ePolicy.Fund_inv
	Dim lclsFund_inv_1 As ePolicy.Fund_inv
	Select Case Request.QueryString.Item("sCodispl")
		Case "AUC001"
			'Session("showdata")="2"
			lblnPost = True
			mstrString = "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&nPayFreq=" & Request.Form.Item("cbePayFreq") & "&nCapital=" & Request.Form.Item("tctCapital") & "&nPremium=" & Request.Form.Item("tctPremium") & "&dEffectDate=" & Request.Form.Item("tcdEffecDate") & "&dNullDate=" & Request.Form.Item("tcdNullDate") & "&sTypePolicy=" & Request.Form.Item("optTypePolicy") & "&sLicense=" & Request.Form.Item("optLicence") & "&sRegister=" & Request.Form.Item("tctRegister") & "&sMotor=" & Request.Form.Item("tctMotor") & "&sChassis=" & Request.Form.Item("tctChassis") & "&sColor=" & Request.Form.Item("tctColor") & "&nVehMark=" & Request.Form.Item("cbeVehMark") & "&sVehModel=" & Request.Form.Item("tctLVehModel") & "&nType=" & Request.Form.Item("cbeType") & "&nZone=" & Request.Form.Item("cbeZone")
			'+ BVC001: Consulta de Base de datos de vehiculos
		Case "BVC001"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 2 Then
					'Response.Write(mobjBranchQue.sCodition)
					Session("SQL") = mobjBranchQue.sCodition
				End If
				lblnPost = True
				
			End With
			'+ INC001: Consulta de poliza de incendio
			
		Case "INC001"
			lblnPost = True
			mstrString = "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&nPayFreq=" & Request.Form.Item("cbePayFreq") & "&nCapital=" & Request.Form.Item("tctCapital") & "&nPremium=" & Request.Form.Item("tctPremium") & "&dEffectDate=" & Request.Form.Item("tcdEffecDate") & "&dNullDate=" & Request.Form.Item("tcdNullDate") & "&nTypePolicy=" & Request.Form.Item("optTypePolicy") & "&nArticle=" & Request.Form.Item("cbeArticle") & "&nDetailArt=" & Request.Form.Item("valDetailArt") & "&nActivityCat=" & Request.Form.Item("cbeActivityCat") & "&nConstCat=" & Request.Form.Item("cbeConstCat") & "&nFloor_quan=" & Request.Form.Item("tctFloor_quan") & "&nSpCombType=" & Request.Form.Item("cbeCombType") & "&nSideCloseType=" & Request.Form.Item("cbeSideCloseType") & "&nIndPeriod=" & Request.Form.Item("tctIndPeriod") & "&nRoofType=" & Request.Form.Item("tctRoofType")
			
			'+ VIC005: Consulta de pólizas de Vida
		Case "VIC005"
			lblnPost = True
			mstrString = "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&nPayFreq=" & Request.Form.Item("cbePayfreq") & "&nCapital=" & Request.Form.Item("tcnCapital") & "&nPremium=" & Request.Form.Item("tcnPremium") & "&dEffecDate=" & Request.Form.Item("tcdEffecDate") & "&nTypePolicy=" & Request.Form.Item("optTypePol") & "&nAge=" & Request.Form.Item("tcnAge") & "&nAge_reinsu=" & Request.Form.Item("tcnAge_reinsu") & "&nPolicy=" & Request.Form.Item("tcnPolicy") & "&dExpirdat=" & Request.Form.Item("tcdExpirdat")
			
			'+ VAC631: Consulta de pólizas de VidActiva
		Case "VAC631"
			With Request
				lblnPost = True
				mstrString = "&bFind=1&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertif=" & .Form.Item("tcnCertif") & "&sClient=" & .Form.Item("valClient") & "&sStatusva=" & .Form.Item("cbeStatusva") & "&nModulec=" & .Form.Item("valModulec") & "&nOption=" & .Form.Item("cbeOption") & "&nPayFreq=" & .Form.Item("cbePayFreq") & "&nCapitalDeath=" & .Form.Item("tcnCapitalDeath") & "&nPremdeal=" & .Form.Item("tcnPremdeal") & "&nTypeinvest=" & .Form.Item("cbeTypeinvest") & "&nIntproject=" & .Form.Item("tcnIntproject") & "&nWarminint=" & .Form.Item("tcnWarminint") & "&nAgreement=" & .Form.Item("cbeAgreement")
			End With
			
			'**+ VIC012: Fund Activity
			'+ VIC012: Actividades de fondos
			
		Case "VIC012"
			lblnPost = True
			
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("nFund") = mobjValues.StringToType(.Form.Item("cbeFund"), eFunctions.Values.eTypeData.etdDouble)
					Session("dDate") = mobjValues.StringToType(.Form.Item("tcdDate"), eFunctions.Values.eTypeData.etdDate)
					
					
					lclsFund_inv = New ePolicy.Fund_inv
					
					ldblQuan_avail = 0
					
					If lclsFund_inv.Find(Session("nFund")) Then
						ldblQuan_avail = lclsFund_inv.nQuan_avail
					End If
					
					Response.Write("<SCRIPT>top.fraHeader.UpdateDiv('nInitialBalance','" & mobjValues.TypeToString(ldblQuan_avail, eFunctions.Values.eTypeData.etdDouble, True, 5) & "');</" & "Script>")
					
					lclsFund_inv = Nothing
				End If
			End With
			
			'**+ VIC013: Summary of the Activity of a Fund.
			'+ VIC013: Resumen de la actividad de un fondo.
			
		Case "VIC013"
			lblnPost = True
			
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("dDate") = mobjValues.StringToType(.Form.Item("tcdDate"), eFunctions.Values.eTypeData.etdDate)
				End If
			End With
			
			'**+ VIC014: Entries to an Investment Fund
			'+ VIC014: Transacciones de un fondo de invercion
			
		Case "VIC014"
			lblnPost = True
			
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("nFund") = mobjValues.StringToType(.Form.Item("cbeFund"), eFunctions.Values.eTypeData.etdDouble)
					Session("dDate") = mobjValues.StringToType(.Form.Item("tcdDate"), eFunctions.Values.eTypeData.etdDate)
					
					
					lclsFund_inv_1 = New ePolicy.Fund_inv
					
					ldblQuan_avail_1 = 0
					
					If lclsFund_inv_1.Find(Session("nFund")) Then
						ldblQuan_avail_1 = lclsFund_inv_1.nQuan_avail
					End If
					
					Response.Write("<SCRIPT>top.fraHeader.UpdateDiv('nInitialBalance','" & mobjValues.TypeToString(ldblQuan_avail_1, eFunctions.Values.eTypeData.etdDouble, True, 5) & "');</" & "Script>")
					
					lclsFund_inv_1 = Nothing
				End If
			End With
			
	End Select
	insPostBranchQue = lblnPost
End Function

</script>
<%Response.Expires = -1
mstrCommand = "&sModule=Client&sProject=Client&sCodisplReload=" & Request.QueryString.Item("sCodispl")

mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT SRC="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>



		


<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 13.31 $|$$Author: Nvaplat60 $"
//--------------------------------------------------------------------------------------------------
function CancelErrors()
//--------------------------------------------------------------------------------------------------
{
    self.history.go(-1)
}
//--------------------------------------------------------------------------------------------------
function NewLocation(Source,Codisp)
//--------------------------------------------------------------------------------------------------
{
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
</HEAD>
<%
If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If
%>

<%
'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalBranchQue
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & Server.URLEncode(Request.Form.ToString) & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""BranchesError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostBranchQue() Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
			Else
				If insPostBranchQue Then
					Select Case Request.QueryString.Item("sCodispl")
						Case "OP004"
							Response.Write("<SCRIPT>insReloadTop(true,false)</SCRIPT>")
						Case "AUC001", "INC001", "VIC005", "VAC631"
							Response.Write("<SCRIPT>top.fraHeader.document.location.href=""" & Request.QueryString.Item("sCodispl") & "_K" & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;</SCRIPT>")
						Case "BVC001"
							Response.Write("<SCRIPT>top.opener.document.location.href='BVC001_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&sql=" & Server.URLEncode(Session("Sql")) & "'</SCRIPT>")
						Case "VIC012"
							Response.Write("<SCRIPT>;top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrString & """;</SCRIPT>")
						Case "VIC013"
							Response.Write("<SCRIPT>;top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrString & """;</SCRIPT>")
						Case "VIC014"
							Response.Write("<SCRIPT>;top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrString & """;</SCRIPT>")
						Case Else
							Response.Write("<SCRIPT>;self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;</SCRIPT>")
					End Select
				End If
			End If
			'+ Se mueve automaticamente a la siguiente página
		Else
			'+ Se recarga la página que invocó la PopUp
			Select Case Request.QueryString.Item("sCodispl")
				Case "AUC001"
					Response.Write("<SCRIPT>opener.document.location.href='AUC001_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "'</SCRIPT>")
				Case "BVC001"
					Response.Write("<SCRIPT>top.opener.document.location.href='BVC001_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&sql=" & Server.URLEncode(Session("Sql")) & "'</SCRIPT>")
				Case "INC001"
					Response.Write("<SCRIPT>opener.document.location.href='INC001_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "'</SCRIPT>")
			End Select
		End If
	End If
End If
mobjValues = Nothing
mobjBranchQue = Nothing
%>
</BODY>
</HTML>




