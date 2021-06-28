<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

Dim mstrErrors As String
Dim mobjDiscoExprSeq As eProduct.Disco_expr

Dim mobjValues As eFunctions.Values

'- Se define la variable para envíar por querystring los datos
Dim mstrQueryString As String

'- Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String


'% insvalSequence: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insvalSequence() As String
	'--------------------------------------------------------------------------------------------
	Dim lintAcceptModule As Object
	
	Select Case Request.QueryString.Item("sCodispl")
		'+ DP08B1 : Información general. Recargos/Descuentos
		Case "DP08B1"
			mobjDiscoExprSeq = New eProduct.Disco_expr
			With Request
				insvalSequence = mobjDiscoExprSeq.insValDP08B1(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nDisexprc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnBill_item"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranchLedger"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranchStatis"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("chkCapitalSub"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapitalSub"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkTypMar"), .Form.Item("chkIVA"))
			End With
			
			'+ DP08B2 : Condiciones de cálculo					
		Case "DP08B2"
			mobjDiscoExprSeq = New eProduct.Disco_expr
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					
					If .QueryString.Item("nModulec") <> "0" And mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdLong) <> eRemoteDB.Constants.intNull Then
						lintAcceptModule = 1
					Else
						lintAcceptModule = 2
					End If
					
					insvalSequence = mobjDiscoExprSeq.insValDP08B2Upd(.Form.Item("tctPreRou"), mobjValues.StringToType(.Form.Item("tcnPreFix"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("optCapital"), eFunctions.Values.eTypeData.etdDouble), "%(prima) / o/oo (capital)", mobjValues.StringToType(.QueryString.Item("nDisXpreRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeModule"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lintAcceptModule, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble))
				Else
					insvalSequence = mobjDiscoExprSeq.insValDP08B2(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nDisexprc"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("tcnDisXpreFix"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrencyD"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDisXpreMin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDisXpreMax"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDisxPreRou"), .Form.Item("optDefpol"), mobjValues.StringToType(.Form.Item("cbeModule"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddRate"))
				End If
			End With
			
			'+ GE101: Cancelación del proceso
		Case "GE101"
			insvalSequence = vbNullString
			
		Case Else
			insvalSequence = "insvalSequence: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostSequence: Se realizan las actualizaciones de las ventanas
'--------------------------------------------------------------------------------------------
Function insPostSequence() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	
	lblnPost = False
	
	Select Case Request.QueryString.Item("sCodispl")
		'+ DP08B1 : Información general. Recargos/Descuentos
		Case "DP08B1"
			With Request
				lblnPost = mobjDiscoExprSeq.insPostDP08B1(mobjValues.StringToType(.QueryString.Item("nAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nDisexprc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")), mobjValues.StringToType(.Form.Item("cbeBranchStatis"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranchLedger"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranchReinsu"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnBill_item"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkCapitalAdd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkCapitalSub"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkPreSel"), .Form.Item("chkReturn"), .Form.Item("chkFraction"), .Form.Item("chkRequired"), mobjValues.StringToType(.Form.Item("tcnCapitalAdd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapitalSub"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapitalLev"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkTypMar"), .Form.Item("chkIVA"), mobjValues.StringToType(.Form.Item("cbenaply"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+ DP08B2 : Condiciones de cálculo					
		Case "DP08B2"
			mobjDiscoExprSeq = New eProduct.Disco_expr
			With Request
				If .QueryString.Item("WindowType") <> "PopUp" Then
					lblnPost = mobjDiscoExprSeq.insPostDP08B2(mobjValues.StringToType(.QueryString.Item("nAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nDisexprc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("chkDisXpreComm"), mobjValues.StringToType(.Form.Item("cbeCurrencyD"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDisXpreMax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDisXpreMin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDisXpreFix"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optCapitalAplied"), .Form.Item("tctDisxPreRou"), .Form.Item("optDefpol"), mobjValues.StringToType(.Form.Item("tcnDisXpreRate"), eFunctions.Values.eTypeData.etdDouble))
				Else
					lblnPost = mobjDiscoExprSeq.insPostDP08B2Upd("Upd", mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nDisexprc"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("cbeConcept"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPreFix"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPreMax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPreMin"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctPreRou"), .Form.Item("optCapital"), .Form.Item("tctPreComm"), .Form.Item("optDefpol"), mobjValues.StringToType(.Form.Item("cbeModule"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDisXpreRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), Session("sBrancht"))
					
					mstrQueryString = "&nCurrencyD=" & mobjValues.StringToType(.Form.Item("tcnCurrency"), eFunctions.Values.eTypeData.etdDouble) & "&nDisXpreFix=" & mobjValues.StringToType(.Form.Item("tcnPreFix"), eFunctions.Values.eTypeData.etdDouble) & "&nDisXpreMin=" & mobjValues.StringToType(.Form.Item("tcnPreMin"), eFunctions.Values.eTypeData.etdDouble) & "&nDisXpreMax=" & mobjValues.StringToType(.Form.Item("tcnPreMax"), eFunctions.Values.eTypeData.etdDouble) & "&sDisxPreRou=" & .Form.Item("tctPreRou") & "&nDisXpreRate=" & mobjValues.StringToType(.Form.Item("tcnDisRate"), eFunctions.Values.eTypeData.etdDouble) & "&nOptCapApl=" & .Form.Item("optCapital") & "&sPreComm=" & .Form.Item("tctPreComm")
				End If
			End With
			
			'+ GE101: Cancelación del proceso
		Case "GE101"
			lblnPost = insCancel()
			
	End Select
	insPostSequence = lblnPost
End Function

'% insCancel: Se controla la acción Cancelar de la secuencia
'--------------------------------------------------------------------------------------------
Private Function insCancel() As Boolean
	'--------------------------------------------------------------------------------------------
	'- Objeto para manejo de requisitos y exclusiones
	Dim lclsTab_reqexc As eProduct.Tab_reqexc
	Dim lclsDisco_expr As eProduct.Disco_expr
	
	'- Objeto para manejo de errores	
	Dim lobjError As eFunctions.Errors
	
	Dim lblnPost As Boolean
	
	insCancel = False
	
	If Request.Form.Item("optElim") = "Delete" Then
		lclsTab_reqexc = New eProduct.Tab_reqexc
		
		'+ Muestra el mensaje para eliminar registros
		If lclsTab_reqexc.valTab_reqexc(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), "3", mobjValues.StringToType(Session("nDisexprc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
			
			lobjError = New eFunctions.Errors
			With lobjError
				.Highlighted = True
				Response.Write(.ErrorMessage("DP008", 11372,  ,  ,  , True))
			End With
			lobjError = Nothing
		Else
			lclsDisco_expr = New eProduct.Disco_expr
			
			lblnPost = lclsDisco_expr.insPostDP008("Del", mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, mobjValues.StringToType(Session("nDisexprc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), vbNullString, vbNullString, eRemoteDB.Constants.intNull, vbNullString, vbNullString)
		End If
		lclsTab_reqexc = Nothing
	End If
	
	With Response
		.Write("<SCRIPT>")
		.Write("var lstrHref = '/VTimeNet/Product/ProductSeq/DP008.aspx?sOnSeq=1&sCodispl=DP008&nMainAction=302';")
		.Write("opener.top.opener.top.frames['fraFolder'].location.href=lstrHref;")
		.Write("</" & "Script>")
	End With
End Function

'% insFinish: realiza las acciones para finalizar la secuencia
'--------------------------------------------------------------------------------------------
Private Function insFinish() As Boolean
	'--------------------------------------------------------------------------------------------
	insFinish = True
	
	'+ Si no se está consultando, se valida que se haya incluido la información requerida en las ventanas	
	If Session("bQuery") = False Then
		mobjDiscoExprSeq = New eProduct.Disco_expr
		
		If Request.Form.Item("sCodisplReload") = vbNullString Then
			'+ Se verifica que no existan ventanas requeridas en la ventana
			mstrErrors = mobjDiscoExprSeq.insValDP08B1_K(Session("nBranch"), Session("nProduct"), Session("nDisexprc"), Session("dEffecdate"))
			Session("sErrorTable") = mstrErrors
		Else
			Session("sErrorTable") = vbNullString
		End If
		
		'+ Si no se han validado los campos de la página
		If mstrErrors > vbNullString Then
			With Response
				.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
				.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & Server.URLEncode(Request.Form.ToString) & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """,""DiscoExprErrors"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
				.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
				.Write("</" & "Script>")
			End With
			insFinish = False
		Else
			insFinish = mobjDiscoExprSeq.insPostDP08B1_K(Session("nBranch"), Session("nProduct"), Session("nDisexprc"), Session("dEffecdate"), Session("nUsercode"))
		End If
	End If
	
	mobjDiscoExprSeq = Nothing
End Function

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mstrCommand = "&sModule=Product&sProject=ProductSeq&sSubProject=DiscoExprSeq&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
 	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%=mobjValues.StyleSheet()%>



	
</HEAD>
<BODY>
<FORM ID=FORM1 NAME=FORM1>
<%
'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalSequence
	Session("sErrorTable") = mstrErrors
Else
	Session("sErrorTable") = vbNullString
End If

If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
	If mstrErrors > vbNullString Then
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & Server.URLEncode(Request.Form.ToString) & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """,""DiscoExprErrors"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</SCRIPT>")
		End With
	Else
		If insPostSequence Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				'+ Si se está tratando con un frame y no con la ventana principal de la secuencia, 
				'+ se mueve automaticamente a la siguiente página
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/DiscoExprSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=Yes&nDisexprc=" & Session("nDisexprc") & "';</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/DiscoExprSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
				End If
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					Response.Write("<SCRIPT LANGUAGE=JAVASCRIPT>self.history.go(-1);</SCRIPT>")
				End If
			Else
				'+ Se recarga la página que invocó la PopUp
				Select Case Request.QueryString.Item("sCodispl")
					Case "DP08B2"
						If Request.Form.Item("sCodisplReload") = vbNullString Then
							Response.Write("<SCRIPT>top.opener.document.location.href='DP08B2.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & mstrQueryString & "'</SCRIPT>")
						Else
							Response.Write("<SCRIPT>window.close();top.opener.top.opener.top.frames['fraFolder'].document.location.href='DP08B2.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & mstrQueryString & "'</SCRIPT>")
						End If
					Case "GE101"
						Response.Write("<SCRIPT>opener.top.close();</SCRIPT>")
				End Select
			End If
		End If
	End If
Else
	'+ Se recarga la página principal de la secuencia		
	If insFinish() Then
		With Response
			.Write("<SCRIPT>")
			.Write("top.opener.document.location.href='/VTimeNet/Product/ProductSeq/DP008.aspx?sOnSeq=1&sCodispl=DP008&nMainAction=302';")
			.Write("top.close()")
			.Write("</SCRIPT>")
		End With
	End If
End If

mobjDiscoExprSeq = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>





