<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eFinance" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String

Dim mstrErrors As Object
Dim mobjFinancing As Object


'% insvalSequence: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insvalFinancing() As Object
	'--------------------------------------------------------------------------------------------
	Select Case Request.QueryString.Item("sCodispl")
		'+ Cobro de Cuota Inicial
		Case "FI005"
			mobjFinancing = New eFinance.financeCO
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				insvalFinancing = mobjFinancing.insValFI005_K(mobjValues.StringToType(Request.Form.Item("tcnContrat"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tcdEffecdate"))
			Else
				insvalFinancing = mobjFinancing.insValFI005(mobjValues.StringToType(Request.Form.Item("cbePayment_way"), eFunctions.Values.eTypeData.etdDouble))
			End If
			mobjFinancing = Nothing
			'+ Anulación de Contrato
		Case "FI006"
			mobjFinancing = New eFinance.financeCO
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				insvalFinancing = mobjFinancing.insValFI006_K(mobjValues.StringToType(Request.Form.Item("tcnContrat"), eFunctions.Values.eTypeData.etdDouble), Today, Request.Form.Item("tcdNulldate"))
			Else
				insvalFinancing = mobjFinancing.insValFI006(mobjValues.StringToType(Request.Form.Item("cbeCause"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeOption"), eFunctions.Values.eTypeData.etdDouble))
			End If
			mobjFinancing = Nothing
			'+ Reverso de cocro de giros y cuota inicial
		Case "FI014"
			mobjFinancing = New eFinance.FinanceDraft
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				insvalFinancing = mobjFinancing.insValFI014_K(mobjValues.StringToType(Request.Form.Item("tcnContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnDraft"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeCause"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tcdOpe_date"), mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdDouble))
			Else
				insvalFinancing = True
			End If
			mobjFinancing = Nothing
			
			
			'+FI012: Cobro de giros de financiamiento
			
		Case "FI012"
			mobjFinancing = New eFinance.financeCO
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalFinancing = mobjFinancing.insValFI012_k("FI012", mobjValues.StringToType(.Form.Item("tcnContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQ_Draft"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdStat_date"), eFunctions.Values.eTypeData.etdDate))
				Else
					insvalFinancing = mobjFinancing.insValFI012("FI012", mobjValues.StringToType(.Form.Item("tcnContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQ_Draft"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdStat_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnExchange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDscto_amo"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInterest"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbePayWay"))
				End If
			End With
			mobjFinancing = Nothing
			
			'+FI015: Modificación del encargado de cobro
			
		Case "FI015"
			mobjFinancing = New eFinance.financeCO
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalFinancing = mobjFinancing.insValFI015_k("FI015", mobjValues.StringToType(.Form.Item("tcnContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAgent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFirstDra"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLastDra"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInterest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnComAmo"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			mobjFinancing = Nothing
			
			
		Case Else
			insvalFinancing = "insvalFinancing: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostFinancing: Se realizan las actualizaciones de las ventanas
'--------------------------------------------------------------------------------------------
Function insPostFinancing() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	lblnPost = False
	
	Select Case Request.QueryString.Item("sCodispl")
		'+ Cobro de cuota inicial
		Case "FI005"
			mobjFinancing = New eFinance.financeCO
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				lblnPost = True
				Session("nContrat") = Request.Form.Item("tcnContrat")
				Session("dEffecdate") = Request.Form.Item("tcdEffecdate")
			Else
				lblnPost = mobjFinancing.insPostFI005(mobjValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tcdEffecdate"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
			End If
			mobjFinancing = Nothing
			'+ Anulación de Contrato
		Case "FI006"
			mobjFinancing = New eFinance.financeCO
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				lblnPost = True
				Session("nContrat") = Request.Form.Item("tcnContrat")
				Session("dNulldate") = Request.Form.Item("tcdNulldate")
			Else
				lblnPost = mobjFinancing.insPostFI006(mobjValues.StringToType(Request.Form.Item("cbeOption"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeCause"), eFunctions.Values.eTypeData.etdDouble), Session("dNulldate"), mobjValues.StringToType(Request.Form.Item("cbeCurr_cont"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnDscto_amo"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
			End If
			mobjFinancing = Nothing
			'+ Reverso de cobro de giros y cuota inicial
		Case "FI014"
			mobjFinancing = New eFinance.FinanceDraft
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				lblnPost = True
				Session("nContrat") = Request.Form.Item("tcnContrat")
				Session("nDraft") = Request.Form.Item("tcnDraft")
				Session("nCause") = Request.Form.Item("cbeCause")
				Session("dOpe_date") = Request.Form.Item("tcdOpe_date")
				
			Else
				lblnPost = mobjFinancing.insPostFI014(mobjValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nDraft"), eFunctions.Values.eTypeData.etdDouble), Today, Session("dOpe_date"), mobjValues.StringToType(Request.Form.Item("cbeCurr_cont"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
			End If
			
			'+FI012: Cobro de giros de financiamiento
			
		Case "FI012"
			mobjFinancing = New eFinance.financeCO
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					lblnPost = True
					Session("nContrat") = .Form.Item("tcnContrat")
					Session("nQ_Draft") = .Form.Item("tcnQ_Draft")
					Session("dStat_date") = .Form.Item("tcdStat_date")
				Else
					lblnPost = mobjFinancing.insPostFI012("FI012", .QueryString("nMainAction"), Session("nContrat"), Session("nQ_Draft"), Session("dStat_date"), mobjValues.StringToType(.Form.Item("tcnExpenses"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDscto_amo"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInterest"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), mobjValues.StringToType(.Form.Item("cbeCurr_cont"), eFunctions.Values.eTypeData.etdDouble))
					
				End If
			End With
			mobjFinancing = Nothing
			
			'+FI015: Modificación del encargado de cobro
			
		Case "FI015"
			mobjFinancing = New eFinance.financeCO
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					lblnPost = mobjFinancing.insPostFI015(mobjValues.StringToType(.Form.Item("tcnContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFirstDra"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLastDra"), eFunctions.Values.eTypeData.etdDouble), eFinance.FinanceDraft.eTypeMove.etmUpdPayment, Session("nUsercode"), mobjValues.StringToType(.Form.Item("tcnAgent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInterest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnComAmo"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			mobjFinancing = Nothing
			
	End Select
	insPostFinancing = lblnPost
End Function

</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mstrCommand = "&sModule=Finance&sProject=Financing&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
 	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%=mobjValues.StyleSheet()%>



	
</HEAD>
<BODY>
<FORM ID=FORM1 NAME=FORM1>
<SCRIPT>

function CancelErrors()
{self.history.go(-1)}

function NewLocation(Source,Codisp){
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp
    Source.location = lstrLocation
}
</SCRIPT>
<%
'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalFinancing
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""FinancingError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostFinancing Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				'+ FI012 : Según la acción se determina cual es la página siguiente del proceso.				
				If Request.QueryString.Item("sCodispl") = "FI012" Then
					'+ Si la acción es consulta o eliminación se recarga el top de la misma página.		    	    
					If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionCut) And Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) And CDbl(Request.Form.Item("cbePayWay")) = 1 Then
						Response.Write("<SCRIPT>top.document.location.href=""/VTimeNet/common/GoTo.aspx?sCodispl=OP001-" & Request.Form.Item("cbePayWay") & """;</SCRIPT>")
					Else
						Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
					End If
				Else
					Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
				End If
				Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
			Else
				If IsNothing(Request.QueryString.Item("sCodisplReload")) Then
					If Request.QueryString.Item("sCodispl") <> "FI015" And Request.QueryString.Item("sCodispl") <> "FI013" Then
						Response.Write("<SCRIPT>window.close();opener.top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();opener.top.fraHeader.document.location=""" & UCase(Request.QueryString.Item("sCodispl") & "_K") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
					End If
				Else
					Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
				End If
			End If
			'+ Se mueve automaticamente a la siguiente página
		Else
			'+ Se recarga la página que invocó la PopUp
			Select Case Request.QueryString.Item("sCodispl")
				Case "Codispl"
					Response.Write("<SCRIPT>opener.document.location.href='Codispl.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "'</SCRIPT>")
			End Select
		End If
	End If
End If

mobjFinancing = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>


	




