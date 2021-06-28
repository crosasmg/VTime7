<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBudget" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mstrErrors As String
Dim mobjBudget As Object

'- Se define la contante para el manejo de errores en caso de advertencias

Dim mstrCommand As String
Dim mstrString As String


'% insValBudget: Se realizan las validaciones de las páginas
'--------------------------------------------------------------
Function insValBudget() As String
	Dim lintIndex As Object
	'--------------------------------------------------------------
	
	With Request
		Select Case .QueryString.Item("sCodispl")
			
			'+ CPC003: Consulta Presupuestaria - NDCB - Fecha.
			
			Case "CPC003"
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mobjBudget = New eBudget.Budget
					
					Response.Write("<SCRIPT>alert(""Pres.:" & .Form.Item("valBudget") & """)</" & "Script>")
					Response.Write("<SCRIPT>alert(""Ejercicio:" & .Form.Item("tcnYear") & """)</" & "Script>")
					Response.Write("<SCRIPT>alert(""Mes:" & .Form.Item("cbeMonth") & """)</" & "Script>")
					Response.Write("<SCRIPT>alert(""Moneda:" & .Form.Item("cbeCurrency") & """)</" & "Script>")
					Response.Write("<SCRIPT>alert(""Compañía:" & .Form.Item("tcnLedCompan") & """)</" & "Script>")
					
					insValBudget = mobjBudget.insValCPC003_K("CPC003_K", .Form.Item("sBud_code"), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLedCompan"), eFunctions.Values.eTypeData.etdDouble))
				Else
					insValBudget = vbNullString
				End If
				'+CP008: Actualizaciòn y Consultas de Presupuestos
			Case "CP008"
				mobjBudget = New eBudget.Budget_amo
				With Request
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						insValBudget = mobjBudget.insValCP008_k(mobjValues.StringToType(.Form.Item("tcnLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), "CP008", mobjValues.StringToType(.Form.Item("tcnYearWork"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrencyWork"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valBudgetWork"), mobjValues.StringToType(.Form.Item("tcnYearComp"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valBudgetComp"), mobjValues.StringToType(.Form.Item("cbeCurrencyComp"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valAccount"), .Form.Item("valAux"), .Form.Item("valUnit"), .Form.Item("chkTotAnnual"), mobjValues.StringToType(.Form.Item("tcnAnnualBudget"), eFunctions.Values.eTypeData.etdDouble))
					Else
						insValBudget = vbNullString
					End If
				End With
				
			Case Else
				insValBudget = "insValBudget: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
		End Select
	End With
End Function

'% insPostBudget: Se realizan las actualizaciones a las tablas
'----------------------------------------------------------------
Function insPostBudget() As Boolean
	Dim lintIndex As Integer
	Dim lstrSum As String
	'----------------------------------------------------------------
	
	Dim lblnPost As Boolean
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ CPC003: Consulta Presupuestaria - NDCB - Fecha.
		
		Case "CPC003"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					'Instrucciones
					lblnPost = True
				End If
			End With
			'+CP008: Asientos contables
			
		Case "CP008"
			mobjBudget = New eBudget.Budget_amo
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("nLedCompan") = .Form.Item("tcnLedCompan")
					Session("optMonth") = .Form.Item("optMonth")
					Session("chktotAnnual") = .Form.Item("chktotAnnual")
					Session("nYear") = .Form.Item("tcnYearWork")
					Session("nCurrency") = .Form.Item("cbeCurrencyWork")
					Session("sBud_Code") = .Form.Item("valBudgetWork")
					Session("tcnYearComp") = .Form.Item("tcnYearComp")
					Session("cbeCurrencyComp") = .Form.Item("cbeCurrencyComp")
					Session("valBudgetComp") = .Form.Item("valBudgetComp")
					Session("sAccount") = .Form.Item("valAccount")
					Session("tctDescript") = .Form.Item("tctDescript")
					If IsNothing(.Form.Item("valAux")) Then
						Session("sAuxAccount") = "                    "
					Else
						Session("sAuxAccount") = .Form.Item("valAux")
					End If
					
					If IsNothing(.Form.Item("valUnit")) Then
						Session("sCost_Cente") = "        "
					Else
						Session("sCost_Cente") = .Form.Item("valUnit")
					End If
					Session("tcnAnnualBudget") = .Form.Item("tcnAnnualBudget")
					
					lblnPost = True
				Else
					If CStr(Session("optMonth")) = "1" Then
						lstrSum = "2"
					Else
						lstrSum = "1"
					End If
					If Request.QueryString.Item("WindowType") <> "PopUp" Then
						If .Form.Item("tcnAuxMonth").Length > 0 Then
							For lintIndex = 1 To .Form.Item("tcnAuxMonth").Length
								lblnPost = mobjBudget.insPostCP008(mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), lstrSum, mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("sBud_Code"), Session("sAccount"), Session("sAuxAccount"), mobjValues.StringToType(Session("nYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcnAuxMonth").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcnAuxQuantity").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("sCost_Cente"))
								lintIndex = lintIndex + 1
							Next 
						Else
							lblnPost = mobjBudget.insPostCP008(mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), lstrSum, mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("sBud_Code"), Session("sAccount"), Session("sAuxAccount"), mobjValues.StringToType(Session("nYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAuxMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAuxQuantity"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("sCost_Cente"))
						End If
					Else
						lblnPost = mobjBudget.insPostCP008(mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), lstrSum, mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("sBud_Code"), Session("sAccount"), Session("sAuxAccount"), mobjValues.StringToType(Session("nYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQuantity"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("sCost_Cente"))
						Response.Write("<SCRIPT>alert(""Post es.:" & lblnPost & """)</" & "Script>")
						
					End If
				End If
			End With
			
			'------------------------------------------------------------------	
	End Select
	
	insPostBudget = lblnPost
End Function

</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values

mstrCommand = "&sModule=GeneralLedger&sProject=LedgerQue&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT SRC="/VTimeNet/scripts/GenFunctions.js"></SCRIPT>



		
	<%=mobjValues.StyleSheet()%>

<SCRIPT>
//------------------------------------------
function CancelErrors(){self.history.go(-1)}
//------------------------------------------

//----------------------------------
function NewLocation(Source,Codisp){
//----------------------------------
    var lstrLocation = "";
    
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
</HEAD>

<%If CDbl(Request.QueryString.Item("nZone")) = 1 Then%>
	    <BODY>
<%Else%>
	    <BODY CLASS="Header">
<%End If%>

<%'+ Si no se han validado los campos de la página

If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValBudget
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & server.URLEncode(Request.Form.ToString) & server.URLEncode(mstrCommand) & "&sQueryString=" & server.URLEncode(Request.Params.Get("Query_String")) & """,""BudgetError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostBudget() Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
				End If
			Else
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & "&sCodispl=" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & """" & ";</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & """" & ";</SCRIPT>")
				End If
			End If
		Else
			'+ Se recarga la página que invocó la PopUp
			
			Select Case Request.QueryString.Item("sCodispl")
				Case "CP008"
					Response.Write("<SCRIPT>opener.document.location.href='CP008.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
			End Select
		End If
	End If
End If
%>
</BODY>
</HTML>

<%
mobjValues = Nothing
mobjBudget = Nothing
%>





