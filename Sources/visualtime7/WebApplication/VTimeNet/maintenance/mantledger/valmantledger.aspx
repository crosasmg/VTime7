<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLedge" %>
<script language="VB" runat="Server">

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String

'+ Se define la variable para la construcción del "QueryString"	
Dim mstrQueryString As String

Dim mintChange As String
Dim mintIndex As Short
Dim mintIndex2 As Byte

Dim mstrErrors As String
Dim mobjValues As eFunctions.Values
Dim mobjeLedGer As Object


'**% insValMantLedger: The massive validations of the page are performed
'%   insValMantLedger: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValMantLedger() As String
	'--------------------------------------------------------------------------------------------
	
	Dim llngReceipt_Typ As Integer
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+   MCP001: Carga de guías contables
		Case "MCP001"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mobjeLedGer = New eLedge.Tab_lines
					
					'+ Si el area corresponde a caja ingreso se utiliza el campo nReceipt_Typ para guardar el tipo de
					'+ documento
					If .Form.Item("cbeArea") = "5" Then
						llngReceipt_Typ = mobjValues.StringToType(.Form.Item("cbeCollecDocTyp"), eFunctions.Values.eTypeData.etdDouble)
					Else
						llngReceipt_Typ = mobjValues.StringToType(.Form.Item("tctReceiptTy_h"), eFunctions.Values.eTypeData.etdDouble)
					End If
					
					insValMantLedger = mobjeLedGer.insValMCP001_k(mobjValues.StringToType(.Form.Item("cbeArea"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valTransacty"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTratypei"), eFunctions.Values.eTypeData.etdDouble), llngReceipt_Typ, mobjValues.StringToType(.Form.Item("optProducTy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboPay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboPayType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboPayTypeC"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboTypeAcc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboCompLed"), eFunctions.Values.eTypeData.etdDouble), "MCP001", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeGroup"), eFunctions.Values.eTypeData.etdDouble))
				Else
					If Request.QueryString.Item("WindowType") = "PopUp" Then
						mobjeLedGer = New eLedge.Det_lines
						
						insValMantLedger = mobjeLedGer.insValMCP001(Session("nComp_led"), mobjValues.StringToType(.Form.Item("chkDebit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkCredit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeParameter"), eFunctions.Values.eTypeData.etdDouble), 1, .Form.Item("tctAccount"), "MCP001", Session("nGroup"), .QueryString("Action"), Session("nArea_led"), Session("nTransac_ty"), Session("nTratypei"), Session("nReceipt_ty"), Session("nProduct_ty"), Session("sPay_type"), Session("nTyp_acco"), mobjValues.StringToType(.Form.Item("nConsec"), eFunctions.Values.eTypeData.etdDouble, True))
						
					Else
						insValMantLedger = vbNullString
					End If
				End If
			End With
			
			
			'+MCP774: Tabla de códigos equivalentes entre VisualTIME y FIN700
			
		Case "MCP774"
			With Request
				mobjeLedGer = New eLedge.Tab_equal
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantLedger = mobjeLedGer.InsValMCP774_k(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valLed_compan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboTypecode"), eFunctions.Values.eTypeData.etdDouble))
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						insValMantLedger = mobjeLedGer.InsValMCP774(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nLed_compan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nTypecode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctCodeVisual"), .Form.Item("tctCodeAsi"), .Form.Item("tctDescript"))
					End If
				End If
			End With
			
			'+MCP776: Control de transferencia de información
			
		Case "MCP776"
			With Request
				mobjeLedGer = New eLedge.Fin700_Lines
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantLedger = mobjeLedGer.InsValMCP776_k(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("valLed_compan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLed_year"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboLed_Month"), eFunctions.Values.eTypeData.etdDouble))
				Else
					insValMantLedger = vbNullString
				End If
			End With
			
			'+MCP775: Resumen para las interfaces contables
			
		Case "MCP775"
			With Request
				mobjeLedGer = New eLedge.Fin700_Lines
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantLedger = mobjeLedGer.InsValMCP775_k(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("valLed_compan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeArea_led"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeGroup"), eFunctions.Values.eTypeData.etdDouble))
				Else
					If Request.QueryString.Item("WindowType") = "PopUp" Then
						insValMantLedger = mobjeLedGer.InsValMCP775(.QueryString("sCodispl"), .Form.Item("tctAccount_FIN700"))
					Else
						insValMantLedger = vbNullString
					End If
				End If
			End With
			
		Case Else
			insValMantLedger = "insValMantLedger: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'**% insPostMantLedger: Tables updating are performed
'%   insPostMantLedger: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostMantLedger() As Boolean
	'dim eRemoteDB.Constants.intNull As String
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	Dim lstrVoucher As String
	
	lblnPost = False
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+   MCP001: Carga de guías contables
		Case "MCP001"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("nArea_led") = mobjValues.StringToType(.Form.Item("cbeArea"), eFunctions.Values.eTypeData.etdDouble)
					If Not mobjValues.StringToType(.Form.Item("valTransacty"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
						Session("nTransac_ty") = mobjValues.StringToType(.Form.Item("valTransacty"), eFunctions.Values.eTypeData.etdDouble)
					Else
						Session("nTransac_ty") = 0
					End If
					Session("nTratypei") = mobjValues.StringToType(.Form.Item("cbeTratypei"), eFunctions.Values.eTypeData.etdDouble)
					
					'+ Cuando se trate del area de caja ingreso entonces se usara el campo nReceipt_typ para guardar 
					'+ el tipo de documento
					If Session("nArea_led") = 5 Then
						Session("nReceipt_ty") = mobjValues.StringToType(.Form.Item("cbeCollecDocTyp"), eFunctions.Values.eTypeData.etdDouble)
					Else
						Session("nReceipt_ty") = mobjValues.StringToType(.Form.Item("tctReceiptTy_h"), eFunctions.Values.eTypeData.etdDouble)
					End If
					
					Session("nProduct_ty") = mobjValues.StringToType(.Form.Item("optProducTy"), eFunctions.Values.eTypeData.etdDouble)
					Session("sPay_type") = mobjeLedGer.sPay_type
					Session("nTyp_acco") = mobjValues.StringToType(.Form.Item("cboTypeAcc"), eFunctions.Values.eTypeData.etdDouble)
					Session("nComp_led") = mobjValues.StringToType(.Form.Item("cboCompLed"), eFunctions.Values.eTypeData.etdDouble)
					Session("nGroup") = .Form.Item("cbeGroup")
					lblnPost = True
				Else
					If Request.QueryString.Item("WindowType") = "PopUp" Then
						lblnPost = mobjeLedGer.insPostMCP001(Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("nConsec"), eFunctions.Values.eTypeData.etdDouble, True), Session("nArea_led"), mobjValues.StringToType(Session("nTransac_ty"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTratypei"), eFunctions.Values.eTypeData.etdDouble), Session("nReceipt_ty"), mobjValues.StringToType(Session("nProduct_ty"), eFunctions.Values.eTypeData.etdDouble), Session("sPay_type"), mobjValues.StringToType(Session("nTyp_acco"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkDebit"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("chkCredit"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeParameter"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeComplement"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctAccount"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nComp_led"), eFunctions.Values.eTypeData.etdDouble), 1, Request.Form.Item("valPayForm"))
						
					Else
						lblnPost = True
						
						If mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble) = 302 Or mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble) = 301 Then
							mobjeLedGer = New eLedge.Tab_lines
							lblnPost = mobjeLedGer.updTab_lines(Session("nArea_led"), Session("nTransac_ty"), Session("nTratypei"), Session("nReceipt_ty"), Session("nProduct_ty"), Session("sPay_type"), Session("nTyp_acco"), Session("nComp_led"), Session("nGroup"), Session("nUsercode"), "1")
						End If
					End If
				End If
			End With
			
			
			'+MCP774: Tabla de códigos equivalentes entre VisualTIME y FIN700
		Case "MCP774"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = "&nLed_compan=" & mobjValues.StringToType(.Form.Item("valLed_compan"), eFunctions.Values.eTypeData.etdDouble) & "&nTypecode=" & mobjValues.StringToType(.Form.Item("cboTypecode"), eFunctions.Values.eTypeData.etdDouble)
					lblnPost = True
				End If
				
				If Not Session("bQuery") And Request.QueryString.Item("WindowType") = "PopUp" And (Request.QueryString.Item("Action") = "Add" Or Request.QueryString.Item("Action") = "Update") Then
					lblnPost = mobjeLedGer.InsPostMCP774(CDbl(.QueryString.Item("nZone")) = 1, .QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nLed_compan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nTypecode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctCodeVisual"), .Form.Item("tctCodeAsi"), .Form.Item("tctDescript"))
				Else
					lblnPost = True
				End If
			End With
			
			'+MCP776: Control de transferencia de información
			
		Case "MCP776"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					
					mstrQueryString = "&nLed_compan=" & mobjValues.StringToType(.Form.Item("valLed_compan"), eFunctions.Values.eTypeData.etdDouble) & "&nLed_year=" & mobjValues.StringToType(.Form.Item("tcnLed_year"), eFunctions.Values.eTypeData.etdDouble) & "&nLed_Month=" & mobjValues.StringToType(.Form.Item("cboLed_Month"), eFunctions.Values.eTypeData.etdDouble) & "&sShowVoucher=" & .Form.Item("optShowVoucher")
					
					lblnPost = True
				Else
					lstrVoucher = vbNullString
					mintIndex = 0
					If .Form.Item("Sel").Length > 0 Then
						If Not IsNothing(.Form.Item("Sel")) Then
							For	Each mintChange In .Form.GetValues("Sel")
								mintIndex = mintIndex + 1
								mintIndex2 = CDbl(mintChange) + 1
								If mintChange <> eRemoteDB.Constants.intNull Then
									If lstrVoucher <> vbNullString Then
										lstrVoucher = lstrVoucher & ","
									End If
									lstrVoucher = lstrVoucher & mobjValues.StringToType(.Form.GetValues("hddVoucher").GetValue(mintIndex2 - 1), eFunctions.Values.eTypeData.etdDouble)
								End If
							Next mintChange
						End If
						If lstrVoucher <> vbNullString Then
							lblnPost = mobjeLedGer.insPostCPL777_K(mobjValues.StringToType(Request.QueryString.Item("nLed_compan"), eFunctions.Values.eTypeData.etdDouble), lstrVoucher)
						End If
					Else
						lblnPost = True
					End If
				End If
			End With
			
			'+MCP775: Resumen para las interfaces contables
			
		Case "MCP775"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = "&nLed_compan=" & mobjValues.StringToType(.Form.Item("valLed_compan"), eFunctions.Values.eTypeData.etdDouble) & "&nArea_led=" & mobjValues.StringToType(.Form.Item("cbeArea_led"), eFunctions.Values.eTypeData.etdDouble) & "&nGroup=" & mobjValues.StringToType(.Form.Item("cbeGroup"), eFunctions.Values.eTypeData.etdDouble)
					lblnPost = True
				Else
					If Request.QueryString.Item("WindowType") = "PopUp" Then
						lblnPost = mobjeLedGer.InsPostMCP775(CDbl(.QueryString.Item("nZone")) = 1, .QueryString("sCodispl"), mobjValues.StringToType(Request.QueryString.Item("nLed_compan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nArea_led"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTransaction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctAccount_base"), .Form.Item("tctAccount_FIN700"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					Else
						lblnPost = True
					End If
				End If
			End With
			
	End Select
	
	insPostMantLedger = lblnPost
End Function

</script>
<%
Response.Expires = -1

mstrCommand = "&sModule=GeneralLedGer&sProject=MantLedger&sCodisplReload=" & Request.QueryString.Item("sCodispl")

%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>



	
</HEAD>

<%If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If
%>

<SCRIPT>
//**% CancelErrors: This function is executed when the cancel button is pushed
//%   CancelErrors: Función que se ejecuta cuando se oprime el botón de cancelar
//-----------------------------------------------------------------------------------------
function CancelErrors() {
//-----------------------------------------------------------------------------------------
	self.history.go(-1)
}

//**% NewLocation: This function allows to establish the URL of the page to be loaded
//%   NewLocation: Función que permte establecer el URL de la página a cargar
//-----------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//-----------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
<SCRIPT SRC="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>
<%
mobjValues = New eFunctions.Values

If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) And Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
Else
	If Request.Form.Item("sCodisplReload") = vbNullString Then
		mstrErrors = insValMantLedger
		Session("sErrorTable") = mstrErrors
	Else
		Session("sErrorTable") = vbNullString
	End If
	
	'+   Se invoca al menejo de errores
	If mstrErrors > vbNullString Then
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & Server.URLEncode(Request.Form.ToString) & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """,""MantLedgerErrors"",660,330);")
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</SCRIPT>")
		End With
	Else
		If insPostMantLedger Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
				Else
					If Request.QueryString.Item("nZone") = "1" Then
						If Request.Form.Item("sCodisplReload") = vbNullString Then
							Response.Write("<SCRIPT>;top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
							';self.history.go(-1)
						Else
							Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
						End If
					Else
						Response.Write("<SCRIPT>;;top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
						'self.history.go(-1)
					End If
				End If
			Else
				
				'+   Se recarga la página que invocó la PopUp
				Select Case Request.QueryString.Item("sCodispl")
					Case "MCP001"
						Response.Write("<SCRIPT>top.opener.document.location.href='MCP001.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
					Case "MCP774"
						Response.Write("<SCRIPT>top.opener.document.location.href='MCP774.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nLed_compan=" & Request.QueryString.Item("nLed_compan") & "&nTypecode=" & Request.QueryString.Item("nTypecode") & mstrQueryString & "'</SCRIPT>")
					Case "MCP775"
						Response.Write("<SCRIPT>top.opener.document.location.href='MCP775.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nLed_compan=" & Request.QueryString.Item("nLed_compan") & "&nArea_led=" & Request.QueryString.Item("nArea_led") & "&nGroup=" & Request.QueryString.Item("nGroup") & mstrQueryString & "'</SCRIPT>")
				End Select
			End If
		End If
	End If
End If

mobjValues = Nothing
mobjeLedGer = Nothing
%>
</BODY>
</HTML>




