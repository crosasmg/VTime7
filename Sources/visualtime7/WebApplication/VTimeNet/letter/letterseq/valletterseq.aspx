<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLetter" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">

Const CN_INDIVIDUAL As Short = 1
Const CN_MASIVE As Short = 2

Dim mobjValues As eFunctions.Values
Dim sValues As String

'**- The constant is defined for the managing mistake in case of warnings.	
'- Se define la constante para el manejo de errores en caso de advertencias.

Dim mstrCommand As String
'^Begin Header Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Body Block VisualTimer Utility

'**+ Variable that keeps the string to happen through the QueryString
'+ Variable que guarda la cadena a pasar por el QueryString
Dim mstrQueryString As String
Dim mblnUpdContent As Boolean
Dim mblnReload As Boolean

Dim mstrErrors As String
Dim mobjLetterSeq As Object
Dim lclsLettRequestWin As eLetter.LettRequestWin
Dim lstrImagen As Object


'**% insValSequence: There are realized the massive validations of the form
'% insValSequence: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValSequence() As String
	Dim eIniVal As Object

	Dim eEndVal As Integer
	'--------------------------------------------------------------------------------------------
	'^^Begin Trace Block 08/09/2005 05:41:07 p.m.
	Call insCommonFunction("valletterseq", Request.QueryString.Item("sCodispl"), eIniVal, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
	'~~End Trace Block
	Dim lblnPopUp As Boolean
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'**+ Validations corresponding to the request of sending
		'+ Validaciones correspondientes a la solicitud de envío 		
		
		Case "LT003_K"
			mobjLetterSeq = New eLetter.LettRequest
			With Request
				insValSequence = mobjLetterSeq.insValLT003_K(mobjValues.StringToType(.Form.Item("tcnLettRequest"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToDate(.Form.Item("tcdEffecdate")), mobjValues.StringToType(.Form.Item("tcnLetterNum"), eFunctions.Values.eTypeData.etdInteger, True), Request.QueryString.Item("nMainAction"))
				Session("nParameter") = mobjLetterSeq.oletter.nParameter
			End With
		Case "LT003"
			mobjLetterSeq = New eLetter.LettRequest
			With Request
				insValSequence = mobjLetterSeq.insValLT003(Session("nLettRequest"), Session("dInpDate"), Session("nLetterNum"), mobjValues.StringToType(.Form.Item("optTypeReq"), eFunctions.Values.eTypeData.etdInteger, True), .Form.Item("chkSendMail") & .Form.Item("chkSendEMail") & .Form.Item("chkSendFax"), mobjValues.StringToDate(.Form.Item("tcdExpDate")), mobjValues.StringToDate(.Form.Item("tcdPrintDate")), .Form.Item("valUser"), Request.QueryString.Item("nMainAction"))
			End With
			
			'**+ Condition of massive request of sending
			'+ Condición de solicitud de envíos masiva
			
		Case "LT030"
			mobjLetterSeq = New eLetter.LettValues
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				insValSequence = mobjLetterSeq.insValLT030Upd("LT030", mobjValues.StringToType(Request.Form.Item("valLett_group"), eFunctions.Values.eTypeData.etdInteger), Request.Form.Item("valVariables"), mobjValues.StringToType(Request.Form.Item("cboOperator"), eFunctions.Values.eTypeData.etdInteger), Request.Form.Item("tctInitial"), Request.Form.Item("tctEnd"))
			Else
				insValSequence = mobjLetterSeq.insValLT030("LT030", mobjValues.StringToType(Session("nConditions"), eFunctions.Values.eTypeData.etdInteger))
			End If
			
		Case "LT031"
			mobjLetterSeq = New eLetter.LettRequest
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				lblnPopUp = True
			Else
				lblnPopUp = False
			End If
			With Request
				insValSequence = mobjLetterSeq.insValLT031(Session("nLettRequest"), Session("dInpDate"), Session("nLetterNum"), .Form.Item("tctLettParam"), .Form.Item("tcnLettParam"), Session("nTypeRequest"), lblnPopUp, .Form.Item("tcnStatusGrid"))
			End With
		Case Else
			insValSequence = "insValSequence: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
	'^^Begin Trace Block 08/09/2005 05:41:07 p.m.
	Call insCommonFunction("valletterseq", Request.QueryString.Item("sCodispl"), eEndVal, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
	'~~End Trace Block
End Function


'**% insPostSequence: The updates of the windows are realized
'% insPostSequence: Se realizan las actualizaciones de las ventanas
'--------------------------------------------------------------------------------------------
Function insPostSequence() As Boolean
	Dim eIniPost As Object
	Dim eEndPost As Integer

	Dim sen As Boolean
	'--------------------------------------------------------------------------------------------
	'^^Begin Trace Block 08/09/2005 05:41:07 p.m.
	Call insCommonFunction("valletterseq", Request.QueryString.Item("sCodispl"), eIniPost, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
	'~~End Trace Block
	Dim lblnPost As Boolean
	Dim lintLettRequest As Object
	
	lblnPost = False
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'**+ Operations on the request of sending
		'+ Operaciones sobre la solicitud de envíos
		
		Case "LT003_K"
			If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401 Then
				lblnPost = mobjLetterSeq.insPostLT003_K(Request.QueryString.Item("nMainAction"), 0, mobjValues.StringToType(Request.Form.Item("tcnLetterNum"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToDate(Request.Form.Item("tcdEffecdate")), Session("nUsercode"), Session("nUsercode"))
			Else
				lblnPost = True
			End If
			mblnReload = True
			
			'**+ Update on the values of the request (detail of the request)
			'+ Actualización sobre los valores de la solicitud (detalle de la solicitud)
			
		Case "LT003"
			Session("sPrint") = 2
            If Request.Form.Item("chkPrint") <> vbNullString Then
				sen = True
                Session("sPrint") = 1
			End If
			lblnPost = mobjLetterSeq.insPostLT003(301, Session("nLettRequest"), Session("nLetterNum"), Session("dInpDate"), mobjValues.StringToType(Request.Form.Item("tcdExpDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("optTypeReq"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(CStr(insCalSendType), eFunctions.Values.eTypeData.etdInteger), vbNullString, mobjValues.StringToType(Request.Form.Item("tcdPrintDate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), Request.Form.Item("valUser"), sen)
			Session("nParameter") = mobjLetterSeq.oletter.nParameter
			mblnReload = lblnPost
			
			'**+ Condition of massive request of sending
			'+ Condición de solicitud de envíos masiva
			
		Case "LT030"
			If mobjValues.StringToType(Request.Form.Item("cboOperator"), eFunctions.Values.eTypeData.etdInteger) = 7 Then
				sValues = Request.Form.Item("tctInitial") & " AND " & Request.Form.Item("tctEnd")
			Else
				sValues = Request.Form.Item("tctInitial")
			End If
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				lblnPost = True
			Else
				lblnPost = mobjLetterSeq.insPostLT030(mobjValues.StringToType(Session("nLettRequest"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnConcec"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("valLett_group"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnParameters"), eFunctions.Values.eTypeData.etdInteger, True), Request.Form.Item("valVariables"), sValues, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("cboOperator"), eFunctions.Values.eTypeData.etdInteger, True), Session("sAction"))
			End If
			
			'**+ Update of the values of the parameters of the request			
			'+ Actualización de los valores de los parámetros de la solicitud
			
		Case "LT031"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				
				lblnPost = mobjLetterSeq.insPostLT031(Session("nLettRequest"), mobjValues.StringToType(Request.Form.GetValues("nParameter").GetValue(1 - 1), eFunctions.Values.eTypeData.etdInteger, True), Mid(Request.Form.Item("tctLettParam"), 1, 12), Request.Form.Item("tcnLettParam"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger))
				
				lblnPost = True
			Else
				lblnPost = True
			End If
	End Select
	
	insPostSequence = lblnPost
	If insPostSequence Then
		Select Case Request.QueryString.Item("sCodispl")
			Case "LT003_K"
				If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
					lintLettRequest = mobjValues.StringToType(Request.Form.Item("tcnLettRequest"), eFunctions.Values.eTypeData.etdLong, True)
				Else
					lintLettRequest = mobjValues.StringToType(mobjLetterSeq.nLettRequest, eFunctions.Values.eTypeData.etdLong, True)
				End If
				Session("nLettRequest") = lintLettRequest
				
				Session("dInpDate") = mobjValues.StringToDate(Request.Form.Item("tcdEffecdate"))
				Session("nLetterNum") = mobjValues.StringToType(Request.Form.Item("tcnLetterNum"), eFunctions.Values.eTypeData.etdInteger, True)
			Case "LT003"
				'				Session("sClient")		= Request.Form("tctClient")
				Session("nTypeRequest") = Request.Form.Item("optTypeReq")
		End Select
	End If
	'^^Begin Trace Block 08/09/2005 05:41:07 p.m.
	Call insCommonFunction("valletterseq", Request.QueryString.Item("sCodispl"), eEndPost, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
	'~~End Trace Block
End Function


'**% insCancel: This routine is activated when the user cancels the transaction that 
'**% this executing.
'% insCancel: Esta rutina es activada cuando el usuario cancela la transacción que este
'% ejecutando.
'--------------------------------------------------------------------------------------------
Function insCancel() As Object
	'--------------------------------------------------------------------------------------------
End Function

'**% insFinish: it is activated on having finished the process
'% insFinish: se activa al finalizar el proceso
'--------------------------------------------------------------------------------------------
Function insFinish() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lclsLettRequestWin As eLetter.LettRequestWin
	Dim lclsErrors As eGeneralForm.GeneralForm
	lclsLettRequestWin = New eLetter.LettRequestWin
	lclsErrors = New eGeneralForm.GeneralForm
	
	If Not lclsLettRequestWin.insValContent(Session("nLettRequest"), Session("sClient"), CInt(Request.QueryString.Item("nMainAction"))) Then
		Session("sErrorTable") = lclsErrors.insValGE101("ClientSeq")
		Session("sForm") = Request.Form.ToString
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""LetterSeqError"",660,330);")
			.Write("</" & "Script>")
		End With
		insFinish = False
	Else
		insFinish = True
		insMailing()
	End If
	lclsErrors = Nothing
	lclsLettRequestWin = Nothing
End Function

'**% insMailing: Realizes the managing of sending of post office with particular 
'**%             characteristics associated with the transaction.
'%insMailing: Realiza el manejo de envío de correos con características particulares 
'%            asociadas a la transacción.
'-----------------------------------------------------------------------------------------------------------------
Private Sub insMailing()
	'-----------------------------------------------------------------------------------------------------------------
	Dim lclsLetter As eLetter.Letter
	
	lclsLetter = New eLetter.Letter
	
	'**+ Do merge. In the method "insprepareMerge", compare if the request is masive or 
	'**+ individual
	'+ Merge. En el método "insprepareMerge", compára si la petición es masive o el individuo.
	If Session("sPrint") = vbNullString Then
        Session("sPrint") = 2
    End If
    
	lclsLetter.PrepareMergeMasive(Session("nLettRequest"), Session("nParameter"), Session("dInpDate"), Session("nTypeRequest"), Session("nLetterNum"), "LT003_K", CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction"))), Session("nUsercode"), Session("sPrint"))
	lclsLetter = Nothing
End Sub

'**% insCalSendType: Calculates the binary chain to sending
'% insCalSendType: Calcula la cadena binaria a enviar
'-------------------------------------------------------------------------------------
Private Function insCalSendType() As Double
	'-------------------------------------------------------------------------------------
	Dim lintSendType As Double
	
	lintSendType = 0
	If Request.Form.Item("chkSendEMail") <> vbNullString Then
		lintSendType = lintSendType + 1
	End If

	If Request.Form.Item("chkSendMail") <> vbNullString Then
		lintSendType = lintSendType + 2
	End If
	
	If Request.Form.Item("chkSendFax") <> vbNullString Then
		lintSendType = lintSendType + 4
	End If
	insCalSendType = lintSendType
End Function
'**% AfterPost: This procedure performs processes after the page is posted.
'% AfterPost: Este procedimiento ejecuta procesos después que la página es posteada.
'------------------------------------------------------------------------------------------------
Private Function AfterPost() As String
	'------------------------------------------------------------------------------------------------
	Dim mobjAfterPost As eFunctions.AfterProcess
	Dim sessitem As String
	Dim strSessionVariables As String
	Dim strFormVariables As String
	Dim FormItems As Object
	Dim objArray As String
	
	mobjAfterPost = New eFunctions.AfterProcess
	
	
	strSessionVariables = ""
	For	Each sessitem In Session.Contents
		If Not IsNothing(Session.Contents.Item(sessitem)) Then
			strSessionVariables = strSessionVariables & (sessitem & "=Session object cannot be displayed.&")
		Else
			If IsArray(Session.Contents.Item(sessitem)) Then
				For	Each objArray In Session.Contents.Item(sessitem)
					strSessionVariables = strSessionVariables & "&" & Session.Contents(sessitem) & "(" & sessitem & "):" & Session.Contents.Item(sessitem)(objArray)
				Next objArray
			Else
				strSessionVariables = strSessionVariables & (sessitem & "=" & Session.Contents.Item(sessitem) & "&")
			End If
		End If
	Next sessitem
	
	If Not IsNothing(Request.Form) Then
		For	Each FormItems In Request.Form
			If IsArray(Request.Form.Item(FormItems)) Then
				If Not IsNothing(Request.Form.Item(FormItems)) Then
					For	Each objArray In Request.Form.GetValues(FormItems)
						strFormVariables = strFormVariables & "&" & Request.Form.Item(FormItems) & "(" & FormItems & "):" & Request.Form.GetValues(FormItems).GetValue(objArray - 1)
					Next objArray
				End If
			Else
				strFormVariables = strFormVariables & (FormItems & "=" & Request.Form.Item(FormItems) & "&")
			End If
		Next FormItems
	End If
	
	
	AfterPost = mobjAfterPost.AfterPost(strFormVariables, Request.Params.Get("Query_String"), strSessionVariables)
	
	mobjAfterPost = Nothing
	
End Function

</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("valLetterSeq")
'~End Header Block VisualTimer Utility
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
mobjValues.sSessionID = Session.SessionID

mblnUpdContent = Request.QueryString.Item("WindowType") <> "PopUp"
mstrQueryString = vbNullString
mobjValues.sCodisplPage = "valLetterSeq"
mblnReload = False
mstrCommand = "sModule=Letter&sProject=LetterSeq&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
 	<META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%=mobjValues.StyleSheet()%>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Trace.aspx" -->

<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->

</HEAD>
<BODY>
<FORM id=form1 name=form1>
<%
'**+ If it is not they have validated the fields of the page.
'+ Si no se han validado los campos de la página.

If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValSequence
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
	If mstrErrors > vbNullString Then
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""LetterSeqError"",660,330);")
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</SCRIPT>")
		End With
		
	Else
		lclsLettRequestWin = New eLetter.LettRequestWin
		If insPostSequence Then
			With Request
				If .QueryString.Item("sCodispl") = "LT031" And CDbl(Request.QueryString.Item("nAction")) = 390 Then
					lstrImagen = "1"
					Response.Write(lclsLettRequestWin.InsReloadSequence("Letter", "LetterSeq", vbNullString, .QueryString.Item("sCodispl"), .QueryString.Item("WindowType") = "PopUp", .Form.Item("sCodisplReload") <> vbNullString, mblnUpdContent, lstrImagen,  ,  ,  , True, .Form.Item("chkContinue"), .QueryString.Item("Action"), .QueryString.Item("Index"), .QueryString.Item("sWindowDescript"), .QueryString.Item("nWindowTy"), mstrQueryString,  ,  , Request.QueryString.Item("nMainAction")))
				Else
					lstrImagen = "2"
					Response.Write(lclsLettRequestWin.InsReloadSequence("Letter", "LetterSeq", vbNullString, .QueryString.Item("sCodispl"), .QueryString.Item("WindowType") = "PopUp", .Form.Item("sCodisplReload") <> vbNullString, mblnUpdContent, lstrImagen,  ,  ,  , mblnReload, .Form.Item("chkContinue"), .QueryString.Item("Action"), .QueryString.Item("Index"), .QueryString.Item("sWindowDescript"), .QueryString.Item("nWindowTy"), mstrQueryString,  ,  , Request.QueryString.Item("nMainAction")))
				End If
				
				
			End With
			Response.Write(AfterPost)
		End If
	End If
	lclsLettRequestWin = Nothing
Else
	
	'**+ There is recharged the principal page of the sequence
	'+ Se recarga la página principal de la secuencia
	
	If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
		If insFinish() Then
			Response.Write("<SCRIPT>top.location.reload();</SCRIPT>")
		End If
	End If
End If
mobjLetterSeq = Nothing
mobjValues = Nothing


%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
Call mobjNetFrameWork.FinishPage("valLetterSeq")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>







