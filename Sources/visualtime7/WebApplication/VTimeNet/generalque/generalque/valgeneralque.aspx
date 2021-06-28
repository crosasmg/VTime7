<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralQue" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

Dim mstrErrors As String
Dim mobjValues As eFunctions.Values

'- Contador para uso genérico.
Dim mintCount As Object

'- Variable auxiliar para pase de valores del encabezado al folder
Dim mstrString As String

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String


'% insvalGeneralQue: Se realizan las validaciones de las formas
'--------------------------------------------------------------------------------------------
Function insvalGeneralQue() As String
	'--------------------------------------------------------------------------------------------
	Dim lobjGeneralQue As eGeneralQue.GeneralQue
	Dim lstrCertype As String
	lobjGeneralQue = New eGeneralQue.GeneralQue
	
	Select Case Request.QueryString.Item("sCodispl")
		Case "GE099"
			With mobjValues
				Select Case .StringToType(Request.Form.Item("cbeTypeQuery"), eFunctions.Values.eTypeData.etdDouble)
					Case "11" ' Cotización
						lstrCertype = "3"
					Case "1" 'Póliza
						lstrCertype = "2"
					Case "5" 'Solicitud
						lstrCertype = "1"
				End Select
				insvalGeneralQue = lobjGeneralQue.insvalHeaderGE099(.StringToType(Request.Form.Item("cbeTypeQuery"), eFunctions.Values.eTypeData.etdDouble), .StringToDate(Request.Form.Item("tcdDate")), lstrCertype, .StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("tcnPolicyO"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("valClieName"), .StringToType(Request.Form.Item("tcnClaim"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("tcnContr"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctCheque"), .StringToType(Request.Form.Item("valProvider"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("valIntermed"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble, True), Session("nusercode"))
				
			End With
	End Select
	lobjGeneralQue = Nothing
End Function

'% insPostGeneralQue: Se realizan las lecturas a las tablas
'--------------------------------------------------------------------------------------------
Function insPostGeneralQue() As Boolean
	'--------------------------------------------------------------------------------------------
	
	Dim lclsClient As eClient.Client
	insPostGeneralQue = False
	
	Select Case Request.QueryString.Item("sCodispl")
		'+ Actualización de cuentas bancarias y de caja    
		Case "GE099"
			Session("dEffecdate_GQ") = Request.Form.Item("tcdDate")
                Session("nCurrentQuery") = Request.Form.Item("cbeTypeQuery")
                mstrString = "&nFolder=" & Request.Form.Item("cbeTypeQuery")
			Select Case Session("nCurrentQuery")
				Case 1, 3, 5, 11 'Poliza/Certificado/Solicitud
					Session("nBranch_GQ") = Request.Form.Item("cbeBranch")
					Session("nProduct_GQ") = Request.Form.Item("valProduct")
					Session("nPolicy_GQ") = Request.Form.Item("tcnPolicy")
					Session("nCertif_GQ") = Request.Form.Item("tcnCertif")
				Case 4 'Cliente
					lclsClient = New eClient.Client
					Session("sClient_GQ") = lclsClient.ExpandCode(UCase(Request.Form.Item("valCliename")))
					lclsClient = Nothing
				Case 6 'Siniestro
					Session("nClaim_GQ") = Request.Form.Item("tcnClaim")
				Case 7 'Recibo
					Session("nBranch_GQ") = Request.Form.Item("cbeBranch")
					Session("nProduct_GQ") = Request.Form.Item("valProduct")
					Session("nReceipt_GQ") = Request.Form.Item("tcnClaim")
				Case 8 'Cheque
					Session("sCheque_GQ") = Request.Form.Item("tctCheque")
				Case 9 'Contrato
					Session("nContrat_GQ") = Request.Form.Item("tcnContr")
				Case 40 'Proveedeor
					Session("nProvider_GQ") = Request.Form.Item("valProvider")
				Case 60 'Loan/Lease'
					Session("sLoan_GQ") = Request.Form.Item("tctCheque")
				Case 77 'Intermediario
					Session("nIntermed_GQ") = Request.Form.Item("valIntermed")
				Case 13 'Reaseguro -- Compañias
					Session("nCompany_GQ") = Request.Form.Item("cbeCompany")
				Case 80 'Reaseguro - Prima Cedida
					Session("nPolicy_GQ") = Request.Form.Item("tcnPolicy")
				Case 81 'Reaseguro - Siniestro Cedida
					Session("nPolicy_GQ") = Request.Form.Item("tcnPolicy")
				Case 82 'Reaseguro - Distribucion del Capital
					Session("nPolicy_GQ") = Request.Form.Item("tcnPolicy")
			End Select
			insPostGeneralQue = True
	End Select
End Function

</script>
<%
Response.Expires = -1
%>
<HTML>
<HEAD>
<SCRIPT src="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>
<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>



        
<SCRIPT>
//-----------------------------------------------------------------------------
function CancelErrors(){self.history.go(-1)}
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//-----------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
</HEAD>
<BODY>
<%mstrCommand = "&sModule=Client&sProject=Client&sCodisplReload=" & Request.QueryString.Item("sCodispl")

mobjValues = New eFunctions.Values

If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
	'+ Si no se han validado los campos de la página
	If Request.Form.Item("sCodisplReload") = vbNullString Then
		mstrErrors = insvalGeneralQue
		Session("sErrorTable") = mstrErrors
		Session("sForm") = Request.Form.ToString
	Else
		Session("sErrorTable") = vbNullString
		Session("sForm") = vbNullString
	End If
	
	If mstrErrors > vbNullString Then
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""GeneralQueError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</SCRIPT>")
		End With
	Else
		If insPostGeneralQue() Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
				Else
                        Response.Write("<SCRIPT>;self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;</SCRIPT>")
				End If
				
				'+ Se mueve automaticamente a la siguiente página
				
			Else
				'+ Se recarga la página que invocó la PopUp
				Select Case Request.QueryString.Item("sCodispl")
					Case "GE099"
						Response.Write("<SCRIPT>opener.document.location.href='OP004_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "'</SCRIPT>")
				End Select
			End If
		End If
	End If
Else
	Response.Write("<SCRIPT>top.location.reload();</SCRIPT>")
End If
mobjValues = Nothing
%>
</BODY>
</HTML>




