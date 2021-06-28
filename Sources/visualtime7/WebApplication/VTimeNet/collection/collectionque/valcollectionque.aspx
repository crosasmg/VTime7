<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="eReports" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.44.07
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    
'~End Header Block VisualTimer Utility

Dim mstrErrors As String

'- Variable auxiliar para pase de valores del encabezado al folder
Dim mstrString As String

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String

Dim mobjValues As eFunctions.Values

Dim mobjCollectionQue As Object


'% insValCollectionQue: Se realizan las validaciones de las formas
'--------------------------------------------------------------------------------------------
Function insValCollectionQue() As String
	'--------------------------------------------------------------------------------------------
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ COC001: Consulta de operaciones de cobranzas
		Case "COC001"
			mobjCollectionQue = New eCollection.Premium_mo
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValCollectionQue = mobjCollectionQue.insValCOC001_k("COC001", mobjValues.StringToDate(.Form.Item("tcdInitDate")), mobjValues.StringToDate(.Form.Item("tcdEndDate")), mobjValues.StringToType(.Form.Item("tcnCashnum"), eFunctions.Values.eTypeData.etdDouble, True))
					
				End If
			End With
			
			'+ COC002: Consulta de recibos de una poliza
		Case "COC002"
			mobjCollectionQue = New eCollection.Premium
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValCollectionQue = mobjCollectionQue.insValCOC002_k("COC002", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnProposal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("optQuery"), eFunctions.Values.eTypeData.etdDouble))
					
				End If
			End With
			
			'+ COC003: Consulta de datos de un recibo
		Case "COC003"
			mobjCollectionQue = New eCollection.Premium
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValCollectionQue = mobjCollectionQue.insValCOC003_k("COC003", mobjValues.StringToType(.Form.Item("tcnReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), Session("sReceiptNum"))
					
				End If
			End With
			
			'+ COC006: Cons.de recibos de un organiz./productor
		Case "COC006"
			mobjCollectionQue = New eCollection.Premium
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValCollectionQue = mobjCollectionQue.insValCOC006_k("COC006", .Form.Item("chkUnderw"), .Form.Item("chkRenew"), .Form.Item("chkAll"), mobjValues.StringToType(.Form.Item("cbeReceiptListTyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("tcdDate")), mobjValues.StringToType(.Form.Item("valAgentCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valSupCode"), eFunctions.Values.eTypeData.etdDouble))
					
				End If
			End With
			
			'+ COC679: Cons. de pólizas para la generación de cartas de aviso de anulación
		Case "COC679"
			mobjCollectionQue = New eCollection.Premium
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValCollectionQue = mobjCollectionQue.insValCOC679_k("COC679", mobjValues.StringToDate(.Form.Item("tcdProcess")))
				Else
					insValCollectionQue = ""
				End If
			End With
			
			'+ COC747: Cons. del plan de pago de una  pólizas
		Case "COC747"
			mobjCollectionQue = New eCollection.Premium
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValCollectionQue = mobjCollectionQue.insValCOC747_k("COC747", mobjValues.StringToType(.Form.Item("cbeInsur_area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
				End If
			End With
			
			'+ COC009: Movimientos de un recibo
		Case "COC009"
			mobjCollectionQue = New eCollection.Premium
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValCollectionQue = mobjCollectionQue.insValCOC009_k("COC009", .Form.Item("tctCertype"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnReceipt"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType("0", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("0", eFunctions.Values.eTypeData.etdDouble), Session("sReceiptnum"))
				End If
			End With
			
			'+ COC625: Recibos asociados a un convenio / Desc. por planilla
		Case "COC625"
			mobjCollectionQue = New eCollection.Agreement
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValCollectionQue = mobjCollectionQue.insvalCOC625("COC625", mobjValues.StringToType(.Form.Item("tcnCod_agree"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdInit_date"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("tcdEnd_date"), eFunctions.Values.eTypeData.etdDate, True))
				End If
			End With
			
		Case Else
			insValCollectionQue = "insValCollectionQue: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostCollectionQue: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostCollectionQue() As Boolean
	Dim lclsPremium As Object
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	
	lblnPost = True
	
	Dim mobjDocuments As eReports.Report
	Select Case Request.QueryString.Item("sCodispl")
		'+ COC001: Consulta de operaciones de cobranzas
		Case "COC001"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&dInitDate=" & .Form.Item("tcdInitDate") & "&dEndDate=" & .Form.Item("tcdEndDate") & "&nCashNum=" & .Form.Item("tcnCashnum") & "&nOffice=" & .Form.Item("cbeOffice") & "&nCurrency=" & .Form.Item("cbeCurrency")
				End If
			End With
			
			'+ COC002: Consulta de recibos de una poliza
		Case "COC002"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nProponum=" & .Form.Item("tcnProposal") & "&nInd_PolPro=" & .Form.Item("optQuery")
				End If
			End With
			
			'+ COC003: Consulta de datos de un recibos
		Case "COC003"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&nReceipt=" & .Form.Item("tcnReceipt") & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&sReceiptNum=" & Session("sReceiptNum")
				End If
			End With
			
			'+ COC006: Cons.de recibos de un organiz./productor
		Case "COC006"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&sUnderw=" & .Form.Item("chkUnderw") & "&nReceiptListTyp=" & .Form.Item("cbeReceiptListTyp") & "&sRenew=" & .Form.Item("chkRenew") & "&sAll=" & .Form.Item("chkAll") & "&nCurrency=" & .Form.Item("cbeCurrency") & "&nCardType=" & .Form.Item("cbeCardType") & "&dDate=" & .Form.Item("tcdDate") & "&nTypeSearch=" & .Form.Item("optClient") & "&nIntermed=" & .Form.Item("valAgentCode") & "&nSupCode=" & .Form.Item("valSupCode") & "&nDays=" & .Form.Item("tcnDays")
				End If
			End With
			
			'+ COC679: Cons.de recibos de un organiz./productor
		Case "COC679"
			With Request
				If .QueryString.Item("nZone") = "1" Then
					mstrString = "&dProcess=" & .Form.Item("tcdProcess")
				Else
					lclsPremium = New eCollection.Premiums
					Call lclsPremium.insUpdateTmp_COC679(.Form.Item("hddKey"), .Form.Item("hddChains"), .QueryString("lsFirstRecord"), .QueryString("lsLastRecord"))
					lclsPremium = Nothing
					
					If Not String.IsNullOrempty(.Form.Item("hddChains")) Then
						mobjDocuments = New eReports.Report
						
						With mobjDocuments
							.ReportFilename = "COL679.rpt"
							.setStorProcParam(1, Request.Form.Item("hddKey"))
							.setStorProcParam(2, .setdate(Request.QueryString.Item("dProcess")))
							Response.Write((.Command))
						End With
						mobjDocuments = Nothing
					End If
				End If
			End With
			
			'+ COC747: Cons. del plan de pago de una  pólizas
		Case "COC747"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&nInsur_area=" & .Form.Item("cbeInsur_area") & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy")
				End If
			End With
			
			'+ COC009: Movimientos de un recibo
		Case "COC009"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&nReceipt=" & .Form.Item("tcnReceipt") & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&sCertype=" & .Form.Item("tctCertype")
				End If
			End With
			
			'+ COC625: Recibos asociados a un convenio / Desc. por planilla
		Case "COC625"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&nCod_agree=" & .Form.Item("tcnCod_agree") & "&dInit_date=" & .Form.Item("tcdInit_date") & "&dEnd_date=" & .Form.Item("tcdEnd_date") & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&sTypeReceipt=" & .Form.Item("optReceipt")
				End If
			End With
	End Select
	
	insPostCollectionQue = lblnPost
End Function

</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("valcollectionque")
%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT SRC="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>






<SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16.13 $|$$Author: Nvaplat60 $"
</SCRIPT>
</HEAD>
<%
If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%>
<BODY>
<%	
Else
	%>
<BODY CLASS="Header">
<%	
End If

mstrCommand = "&sModule=Collection&sProject=CollectionQue&sCodisplReload=" & Request.QueryString.Item("sCodispl")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "valcollectionque"

'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValCollectionQue
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""CollectionRepError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostCollectionQue() Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
			Else
				If Request.QueryString.Item("sCodispl") <> "OP004" Then
					Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;</SCRIPT>")
				Else
					Response.Write("<SCRIPT>insReloadTop(true,false)</SCRIPT>")
				End If
			End If
		End If
	End If
End If
mobjValues = Nothing
mobjCollectionQue = Nothing
%>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.44.07
Call mobjNetFrameWork.FinishPage("valcollectionque")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




