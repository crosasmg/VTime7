<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">
Dim mstrErrors As String
Dim mobjValues As eFunctions.Values
Dim mobjMantsys As Object

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String
Dim mstrQueryString As String



'% insvalMantSys: Se realizan las validaciones masivas de la forma	
'--------------------------------------------------------------------------------------------
Function insvalMantSys() As String
	'--------------------------------------------------------------------------------------------
	
	Select Case Request.QueryString.Item("sCodispl")
		'% Busqueda de MEnsajes de Error
		Case "MS001"
			With Request
				mobjMantsys = New eGeneral.Message
				If .QueryString.Item("WindowType") = "PopUp" Then
					insvalMantSys = mobjMantsys.insValMS001_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnCode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctMessage"))
				End If
			End With
		Case "MS002"
			mobjMantsys = New eGeneral.WinMessag
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalMantSys = mobjMantsys.insValMS002_K(.QueryString("sCodispl"), .Form.Item("tctCodispl"))
					Session("sCodisp") = .Form.Item("tctCodispl")
				Else
					If .QueryString.Item("WindowType") = "PopUp" And .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
						insvalMantSys = mobjMantsys.insValMS002(.QueryString("sCodispl"), Session("sCodisp"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnCodigo"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeErrorType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeStaterr"), eFunctions.Values.eTypeData.etdDouble))
					Else
						insvalMantSys = vbNullString
					End If
				End If
			End With
			
			'+ MS004 Factor de Cambio
		Case "MS004"
			With Request
				mobjMantsys = New eGeneral.Exchange
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalMantSys = mobjMantsys.insValMS004_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCurrency"), eFunctions.Values.eTypeData.etdDouble))
				Else
					If .QueryString.Item("WindowType") = "PopUp" And .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
						insvalMantSys = mobjMantsys.insValMS004(.QueryString("sCodispl"), .QueryString("Action"), 1, mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("tcdEffecdate")), mobjValues.StringToType(.Form.Item("tcnExchange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddExchange"), eFunctions.Values.eTypeData.etdDouble))
					End If
				End If
			End With
			
		Case "MS006"
			With Request
				mobjMantsys = New eGeneral.Actions
				If .QueryString.Item("WindowType") = "PopUp" Then
					insvalMantSys = mobjMantsys.insValMS006_K(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tcsDescript"), .Form.Item("tcsHel_actio"), .Form.Item("tcsStatregt"), Session("nUsercode"), .Form.Item("tcsPathImage"))
				End If
			End With
			
		Case "MS008"
			mobjMantsys = New eGeneral.Inquiry_as
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("eInquiry") = .Form.Item("cbeInquiry")
				Else
					If .QueryString.Item("WindowType") = "PopUp" And .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
						insvalMantSys = mobjMantsys.insValMS008_K(.QueryString("Action"), mobjValues.StringToType(Session("eInquiry"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tcWindows"), Session("nUsercode"))
					Else
						insvalMantSys = vbNullString
					End If
				End If
			End With
			
		Case "MS011"
			mobjMantsys = New eGeneral.Numerator
			With Request
				If .QueryString.Item("WindowType") = "PopUp" And .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
					insvalMantSys = mobjMantsys.insValMS011_K(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("cbenTypenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenOrd_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInitial"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEnd_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLastnumb"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
				Else
					insvalMantSys = vbNullString
				End If
			End With
			
			'+ MS012: Factor de Inflación		
		Case "MS012"
			With Request
				mobjMantsys = New eGeneral.Reval_fact
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalMantSys = mobjMantsys.InsValMS012_k(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeEcon_area"), eFunctions.Values.eTypeData.etdDouble, True))
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						insvalMantSys = mobjMantsys.InsValMS012(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nEcon_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIndexfac"), eFunctions.Values.eTypeData.etdDouble))
					End If
				End If
			End With
			
			'+ MS100 Última fecha de ejecución de procesos			
		Case "MS100"
			mobjMantsys = New eGeneral.Ctrol_date
			With Request
				If .QueryString.Item("WindowType") = "PopUp" And .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
					insvalMantSys = mobjMantsys.insValMS100_K(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("cbeType_proce"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"))
				Else
					insvalMantSys = vbNullString
				End If
				
			End With
			
		Case "MS5552"
			insvalMantSys = vbNullString
			mobjMantsys = New eAgent.tax_fixval
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				insvalMantSys = mobjMantsys.insValMS5552_K("MS5552", mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
			Else
				If Request.QueryString.Item("WindowType") <> "PopUp" Then
					insvalMantSys = vbNullString
				Else
					With Request
						insvalMantSys = mobjMantsys.insValMS5552(mobjValues.StringToType(.Form.Item("tctTypeSupport"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnpercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctTypeTax"), .QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
					End With
				End If
			End If
			
		Case "MS5577"
			mobjMantsys = New eAgent.Agencie
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				insvalMantSys = vbNullString
			Else
				With Request
					insvalMantSys = mobjMantsys.insValMS5577_k(mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBran_Off"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chkPay"), .QueryString("Action"), .QueryString("sCodispl"))
				End With
			End If
		Case Else
			insvalMantSys = "insvalMantSys: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostMantsys: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostMantsys() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	lblnPost = False
	Select Case Request.QueryString.Item("sCodispl")
		Case "MS001"
			With Request
				mobjMantsys = New eGeneral.Message
				If .QueryString.Item("WindowType") = "PopUp" Then
					If (.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) And .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionCondition)) Then
						lblnPost = mobjMantsys.insPostMS001_K(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnCode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctMessage"), Session("nUsercode"))
					Else
						lblnPost = True
					End If
				Else
					lblnPost = True
				End If
			End With
		Case "MS002"
			With Request
				mobjMantsys = New eGeneral.WinMessag
				If .QueryString.Item("WindowType") = "PopUp" Then
					If .QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
						lblnPost = True
					Else
						lblnPost = mobjMantsys.inspostMS002(.QueryString("sCodispl"), Session("sCodisp"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnCodigo"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeErrorType"), .Form.Item("cbeStaterr"), mobjValues.StringToType(.Form.Item("tcnnivel"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctCausa"), Session("nUsercode"))
					End If
				Else
					Session("sCodisp") = .Form.Item("tctCodispl")
					lblnPost = True
				End If
			End With
			
			'+ MS004 Factor de Cambio
		Case "MS004"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("nCurrency") = .Form.Item("valCurrency")
					lblnPost = True
				Else
					If .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) And .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
						mobjMantsys = New eGeneral.Exchange
						lblnPost = mobjMantsys.insPostMS004(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnExchange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					Else
						lblnPost = True
					End If
				End If
			End With
			
		Case "MS006"
			With Request
				mobjMantsys = New eGeneral.Actions
				If .QueryString.Item("WindowType") = "PopUp" Then
					If (.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) And .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionCondition)) Then
						lblnPost = mobjMantsys.insPostMS006_K(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tcsDescript"), .Form.Item("tcsHel_actio"), .Form.Item("tcsStatregt"), Session("nUsercode"), .Form.Item("tcsPathImage"))
					Else
						lblnPost = True
					End If
				Else
					lblnPost = True
				End If
			End With
			
		Case "MS008"
			With Request
				mobjMantsys = New eGeneral.Inquiry_as
				If .QueryString.Item("WindowType") = "PopUp" Then
					If (.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) And .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionCondition)) Then
						lblnPost = mobjMantsys.insPostMS008_K(.QueryString("Action"), mobjValues.StringToType(Session("eInquiry"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tcWindows"), Session("nUsercode"))
					Else
						lblnPost = True
					End If
				Else
					lblnPost = True
				End If
			End With
		Case "MS011"
			With Request
				mobjMantsys = New eGeneral.Numerator
				If .QueryString.Item("WindowType") = "PopUp" Then
					If .QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
						lblnPost = True
					Else
						lblnPost = mobjMantsys.inspostMS011_K(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("cbenTypenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenOrd_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInitial"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEnd_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLastnumb"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
					End If
				Else
					lblnPost = True
				End If
			End With
			
			'+ MS012: Factor de inflación
		Case "MS012"
			With Request
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = "&nEcon_area=" & .Form.Item("cbeEcon_area")
					lblnPost = True
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						lblnPost = mobjMantsys.InsPostMS012(CDbl(.QueryString.Item("nZone")) = 1, .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nEcon_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnIndexfac"), eFunctions.Values.eTypeData.etdDouble))
						mstrQueryString = "&nEcon_area=" & .QueryString.Item("nEcon_area")
					Else
						lblnPost = True
					End If
				End If
			End With
			
			
			'+ MS100 Última fecha de ejecución de procesos			
		Case "MS100"
			mobjMantsys = New eGeneral.Ctrol_date
			With Request
				If .QueryString.Item("WindowType") = "PopUp" And .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
					lblnPost = mobjMantsys.insPostMS100_K(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("cbeType_proce"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"))
				Else
					lblnPost = True
				End If
			End With
			
		Case "MS5552"
			mobjMantsys = New eAgent.tax_fixval
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = "&dEffecdate=" & .Form.Item("tcdEffecdate")
					lblnPost = True
				Else
					mstrQueryString = "&dEffecdate=" & .QueryString.Item("dEffecdate")
					If .QueryString.Item("WindowType") = "PopUp" Then
						lblnPost = mobjMantsys.insPostMS5552(mobjValues.StringToType(.Form.Item("tctTypeSupport"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnpercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctTypeTax"), .QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
					Else
						lblnPost = True
					End If
				End If
			End With
			
		Case "MS5577"
			mobjMantsys = New eAgent.Agencie
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				With Request
					lblnPost = mobjMantsys.insPostMS5577(mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBran_Off"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkPay"), .QueryString("Action"))
				End With
			Else
				lblnPost = True
			End If
	End Select
	insPostMantsys = lblnPost
End Function

</script>
<%Response.Expires = -1

mstrCommand = "&sModule=Maintenance&sProject=MantSys&sCodisplReload=" & Request.QueryString.Item("sCodispl")

mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 6 $|$$Date: 13/11/03 19:43 $|$$Author: Nvaplat15 $"
</SCRIPT>
<SCRIPT src="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>



	
<SCRIPT>

//% CancelErrors:
//--------------------------------------------------------------------------------------
function CancelErrors(){self.history.go(-1)}
//--------------------------------------------------------------------------------------

//% NewLocation:
//--------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//--------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>

</HEAD>

<BODY>
<FORM ID=form1 NAME=form1>

<%

'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalMantSys
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If
If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantsysError"",660,330);")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostMantsys() Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				Response.Write("<SCRIPT>insReloadTop(false);</SCRIPT>")
			Else
				If Request.QueryString.Item("nZone") = "1" Then
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
					End If
				Else
					Response.Write("<SCRIPT>insReloadTop(false);</SCRIPT>")
				End If
			End If
			'+ Se mueve automaticamente a la siguiente página
		Else
			Select Case Request.QueryString.Item("sCodispl")
				Case "MS001"
					Response.Write("<SCRIPT>top.opener.document.location.href='MS001_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&tcnCode=" & Server.URLEncode(Request.Form.Item("tcnCode")) & "&tctMessage=" & Server.URLEncode(Request.Form.Item("tctMessage")) & "&continue=" & Server.URLEncode("S") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
				Case "MS002"
					Response.Write("<SCRIPT>top.opener.document.location.href='MS002.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
				Case "MS004"
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>top.opener.document.location.href='MS004.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();top.opener.top.opener.document.location.href='MS004.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
					End If
				Case "MS006"
					Response.Write("<SCRIPT>top.opener.document.location.href='MS006_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302" & "'</SCRIPT>")
				Case "MS008"
					Response.Write("<SCRIPT>top.opener.document.location.href='MS008.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
				Case "MS011"
					Response.Write("<SCRIPT>top.opener.document.location.href='MS011_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
				Case "MS100"
					Response.Write("<SCRIPT>top.opener.document.location.href='MS100_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
				Case "MS5552"
					Response.Write("<SCRIPT>top.opener.document.location.href='MS5552.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302" & mstrQueryString & "'</SCRIPT>")
				Case "MS5577"
					Response.Write("<SCRIPT>top.opener.document.location.href='MS5577_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
				Case Else
					Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "' </SCRIPT>")
			End Select
		End If
	End If
End If

mobjValues = Nothing
mobjMantsys = Nothing
%>

</FORM>
</BODY>
</HTML>
</BODY>
</HTML>




