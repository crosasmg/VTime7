<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim lobjValues As eFunctions.Values

'- Objeto para el manejo de las funciones de la pagina 
Dim lclsClient As eClient.Client
Dim lclsEval_master As eClient.eval_master
Dim lclsDoc_req_cli As eClient.Doc_req_cli

Dim mstrErrors As String
Dim mstrString As Object

'+ Se define la contante para el manejo de errores en caso de advertencias 
Dim mstrCommand As String


'% insvalSequence: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insvalSequence() As String
	'--------------------------------------------------------------------------------------------
	Select Case Request.QueryString.Item("sCodispl")
		Case "BC668_K"
			lclsClient = New eClient.Client
			insvalSequence = lclsClient.insValBC668_K(0, Request.Form.Item("tctClient"), Request.Form.Item("cbeCertype"), lobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), lobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), lobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
		Case "BC802"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				lclsEval_master = New eClient.eval_master
				insvalSequence = lclsEval_master.InsValBC802(Session("Action_Docum"), lobjValues.StringToType(Request.Form.Item("tcdStartdate"), eFunctions.Values.eTypeData.etdDate), lobjValues.StringToType(Request.Form.Item("tcdExpirdat"), eFunctions.Values.eTypeData.etdDate), lobjValues.StringToType(Request.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("cbeCertype"), lobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
				
			End If
		Case "BC803"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				lclsDoc_req_cli = New eClient.Doc_req_cli
				
				insvalSequence = lclsDoc_req_cli.InsValBC803(Request.QueryString.Item("Action"), lobjValues.StringToType(Request.Form.Item("hddEval"), eFunctions.Values.eTypeData.etdDouble, True), lobjValues.StringToType(Request.Form.Item("cbeTypedoc"), eFunctions.Values.eTypeData.etdDouble), CInt(Request.Form.Item("cbeStatusdoc")), lobjValues.StringToType(Request.Form.Item("tcdDocreq"), eFunctions.Values.eTypeData.etdDate), lobjValues.StringToType(Request.Form.Item("tcdDocrec"), eFunctions.Values.eTypeData.etdDate), lobjValues.StringToType(Request.Form.Item("tcdDocdate"), eFunctions.Values.eTypeData.etdDate), lobjValues.StringToType(Request.Form.Item("tcdExpirdat"), eFunctions.Values.eTypeData.etdDate), lobjValues.StringToType(Request.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble, True), lobjValues.StringToType(Request.Form.Item("tcdDateto"), eFunctions.Values.eTypeData.etdDate))
			End If
		Case Else
			insvalSequence = "insvalSequence: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostSequence: Se realizan las actualizaciones de las ventanas
'--------------------------------------------------------------------------------------------
Function insPostSequence() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lstrSmoking As Object
	Dim lblnPost As Boolean
	Dim lstrCon_win As String
	Dim lintAction As Object
	
	lstrCon_win = "2"
	lblnPost = True
	Select Case Request.QueryString.Item("sCodispl")
		Case "BC668_K"
			Session("sClient") = Request.Form.Item("tctClient")
			Session("nBranch") = Request.Form.Item("cbeBranch")
			Session("nProduct") = Request.Form.Item("valProduct")
			Session("nPolicy") = Request.Form.Item("tcnPolicy")
			Session("nCertif") = Request.Form.Item("tcnCertif")
			Session("Action_Docum") = Request.QueryString.Item("nMainAction")
			Session("bQuery") = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
			lblnPost = True
			lclsClient = Nothing
			
		Case "BC802"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				With Request
					
					lblnPost = lclsEval_master.InsPostBC802(.QueryString.Item("Action"), lobjValues.StringToType(.Form.Item("tcnEval"), eFunctions.Values.eTypeData.etdDouble), Session("sClient"), lobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(.Form.Item("tcdStartdate"), eFunctions.Values.eTypeData.etdDate), lobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(.Form.Item("tcdExpirdat"), eFunctions.Values.eTypeData.etdDate), lobjValues.StringToType(.Form.Item("cbeStatus_eval"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(.Form.Item("tcnCumul"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeCertype"))
				End With
				lclsEval_master = Nothing
			End If
			
		Case "BC803"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				With Request
					lblnPost = lclsDoc_req_cli.InsPostBC803(.QueryString.Item("Action"), lobjValues.StringToType(.Form.Item("hddEval"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(.Form.Item("hddId"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(.Form.Item("cbeTypedoc"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(.Form.Item("cbeStatusdoc"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), lobjValues.StringToType(.Form.Item("tcdDocreq"), eFunctions.Values.eTypeData.etdDate), lobjValues.StringToType(.Form.Item("tcdDocrec"), eFunctions.Values.eTypeData.etdDate), lobjValues.StringToType(.Form.Item("tcdDocdate"), eFunctions.Values.eTypeData.etdDate), lobjValues.StringToType(.Form.Item("tcdExpirdat"), eFunctions.Values.eTypeData.etdDate), lobjValues.StringToType(.Form.Item("hddCapital"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(.Form.Item("tcdDateto"), eFunctions.Values.eTypeData.etdDate), lobjValues.StringToType(.Form.Item("tcdDatefree"), eFunctions.Values.eTypeData.etdDate))
				End With
				lclsDoc_req_cli = Nothing
			End If
	End Select
	insPostSequence = lblnPost
End Function

</script>
<%Response.Expires = -1

lobjValues = New eFunctions.Values

mstrCommand = "&sModule=Client&sProject=DocumSeq&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




    <%=lobjValues.StyleSheet()%>
<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 3/05/04 18:15 $"

//%CancelErrors: Acciones al efectual la cancelación de algún error.
//-----------------------------------------------------------------------------------------
function CancelErrors(){
//-----------------------------------------------------------------------------------------
	self.history.go(-1)
}

//% NewLocation: se recalcula el URL de la página
//-----------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//-----------------------------------------------------------------------------------------
    var lstrLocation = "";

    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation;
}
</SCRIPT>
</HEAD>
<BODY>
<FORM ID="valDocumentSeq" NAME="valDocumentSeq">
<%
If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
	'+ Si no se han validado los campos de la página
	If Request.Form.Item("sCodisplReload") = vbNullString Then
		mstrErrors = insvalSequence
		Session("sErrorTable") = mstrErrors
		Session("sForm") = Request.Form.ToString
	Else
		Session("sErrorTable") = vbNullString
		Session("sForm") = vbNullString
	End If
	
	If mstrErrors > vbNullString Then
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""DocumSeqError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
			.Write(lobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</SCRIPT>")
		End With
	Else
		If insPostSequence Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					'+ Se mueve automaticamente a la siguiente página
					Response.Write("<SCRIPT>top.document.location='/VTimeNet/Common/secWHeader.aspx?sCodispl=BC668_K&sProject=DocumSeq&sModule=Client&sConfig=InSequence&nAction=0&nMainAction=" & Request.QueryString.Item("nMainAction") & "';</SCRIPT>")
				Else
					'+ Se mueve automaticamente a la siguiente página
					Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Client/DocumSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=Yes" & "';</SCRIPT>")
				End If
			Else
				Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Client/DocumSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</SCRIPT>")
				'+ Se recarga la página que invocó la PopUp
				Select Case Request.QueryString.Item("sCodispl")
					Case "BC802"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&Index=" & Request.QueryString.Item("Index") & "'</SCRIPT>")
						'        			        Response.Write "<NOTSCRIPT>top.opener.document.location.href='BC802.aspx?                                  Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") &                                                                "&nMainAction=" & Request.QueryString("nMainAction") & "&Index=" & Request.QueryString("Index") & "'</SCRIPT>"
					Case "BC803"
						Response.Write("<SCRIPT>top.opener.document.location.href='BC803.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&Index=" & Request.QueryString.Item("Index") & "&nEval=" & Request.QueryString.Item("nEval") & "'</SCRIPT>")
				End Select
			End If
		End If
	End If
Else
	Response.Write("<SCRIPT>insReloadTop(true,false)</SCRIPT>")
End If

lobjValues = Nothing
%>
</BODY>
</HTML>




