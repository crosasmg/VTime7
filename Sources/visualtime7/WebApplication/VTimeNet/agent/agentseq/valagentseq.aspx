<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.57
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mobjValues As eFunctions.Values
Dim mobjAgentSeq As Object
Dim mstrErrors As String
Dim mintIntermedia As Integer

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String

Dim lintAction As Object


'% insvalSequence: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insvalSequence() As String
	Dim sValid As String
	'--------------------------------------------------------------------------------------------
	Select Case Request.QueryString.Item("sCodispl")
		Case "AG001_K"
			mobjAgentSeq = New eAgent.Intermedia
			mintIntermedia = mobjValues.StringToType(insGetNewIntermedia(Request.Form.Item("valIntermedia")), eFunctions.Values.eTypeData.etdDouble)
			insvalSequence = mobjAgentSeq.insValAG001_K(mobjValues.StringToType(lintAction, eFunctions.Values.eTypeData.etdDouble), mintIntermedia)
		Case "AG001"
			
			mobjAgentSeq = New eAgent.Intermedia
			
			'+Si se coloca como Valido
			If Request.Form.Item("chkValid") = "1" Then
				sValid = "1"
			Else
				sValid = "2"
			End If
			
			insvalSequence = mobjAgentSeq.insValAG001("AG001", mobjValues.StringToType(lintAction, eFunctions.Values.eTypeData.etdDouble), Session("nIntermed"), mobjValues.StringToType(Request.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeLegal_sch"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkAll"), mobjValues.StringToType(Request.Form.Item("cbeInsu_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("tctLegalNum"), Request.Form.Item("dtcClient"), mobjValues.StringToType(Request.Form.Item("cbeInterType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valSupervis"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valSup_Gen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeIntStatus"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valInsu_assist"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valInsu_assisLif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdInputDate"), eFunctions.Values.eTypeData.etdDate), sValid)
			
		Case "AG003"
			mobjAgentSeq = New eAgent.Intermedia
			insvalSequence = mobjAgentSeq.insValAG003("AG003", mobjValues.StringToType(lintAction, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate))
			
			'+ AG550: Datos particulares de Intermediarios		
		Case "AG550"
			mobjAgentSeq = New eAgent.Intermed_partic
			With Request
				If CBool(.Form.Item("blnNotBroker")) Then
					insvalSequence = vbNullString
				Else
					insvalSequence = mobjAgentSeq.insValAG550(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(lintAction, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSuperin_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdSuperin_num"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnWarran_pol"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull)
				End If
			End With
			
			'+ AG553: Ramos y productos permitidos		
		Case "AG553"
			mobjAgentSeq = New eAgent.branprod_allow
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					insvalSequence = mobjAgentSeq.insValAG553("AG553", .QueryString("Action"), mobjValues.StringToType(Session("nIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInstallments"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnStartMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEndMonth"), eFunctions.Values.eTypeData.etdDouble))
				Else
					insvalSequence = vbNullString
				End If
			End With
			
		Case "GE101"
			insvalSequence = ""
			
		Case Else
			insvalSequence = "insvalSequence: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostSequence: Se realizan las actualizaciones de las ventanas
'--------------------------------------------------------------------------------------------
Function insPostSequence() As Boolean
	Dim ldtmNullDate As Object
        Dim lintNullCode As Integer
        '--------------------------------------------------------------------------------------------
        Dim lblnPost As Boolean
	
        lblnPost = False
	
        Dim lobjAgentSeq_p As eAgent.branprod_allow
        Dim lclsErrors As eGeneralForm.GeneralForm
        Select Case Request.QueryString.Item("sCodispl")
		
            Case "AG001_K"
                mobjAgentSeq = New eAgent.Intermedia
                If lintAction = 401 Then
                    Session("nLastIntermediary") = mintIntermedia
                    Session("MenuOption") = lintAction
                End If
			
                lblnPost = mobjAgentSeq.insPostAG001_k(lintAction, mintIntermedia, Session("nUserCode"))
			
                If lblnPost Then
                    Session("nIntermed") = mintIntermedia
                End If
                lblnPost = True
			
            Case "AG001"
                mobjAgentSeq = New eAgent.Intermedia
                With Request
                    If (Request.Form.Item("lblNullDate") <> vbNullString Or Not IsNothing(Request.Form.Item("lblNullDate"))) Then
                        ldtmNullDate = mobjValues.StringToDate(Request.Form.Item("lblNullDate"))
                    Else
                        ldtmNullDate = eRemoteDB.Constants.dtmNull
                    End If
				
                    If Request.Form.Item("tctNullCode") <> vbNullString And Request.Form.Item("tctNullCode") <> "0" Then
                        If CDbl(Request.Form.Item("tctNullCode")) > 0 Then
                            lintNullCode = mobjValues.StringToType(Request.Form.Item("tctNullCode"), eFunctions.Values.eTypeData.etdDouble)
                        Else
                            lintNullCode = eRemoteDB.Constants.intNull
                        End If
                    Else
                        lintNullCode = eRemoteDB.Constants.intNull
                    End If
                    lblnPost = mobjAgentSeq.InsPostAG001(mobjValues.StringToType(Session("nIntermed"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), mobjValues.StringToType(.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEffecDate_old"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdInputDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeIntStatus"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeInterType"), eFunctions.Values.eTypeData.etdDouble, True), lintNullCode, ldtmNullDate, mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valSupervis"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctLegalNum"), .Form.Item("chkAll"), mobjValues.StringToType(.Form.Item("cbeInsu_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeLegal_sch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valSup_Gen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valInsu_Assist"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valInsu_AssisLif"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkValid"))
				
                    ldtmNullDate = Nothing
                    lintNullCode = Nothing
                End With
			
            Case "AG003"
                With Request
                    lblnPost = mobjAgentSeq.InsPostAG003(Session("nIntermed"), .Form.Item("chkColAgree"), .Form.Item("chkInterest"), mobjValues.StringToType(.Form.Item("cbeLifeComTable"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeGralComTable"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeExComm"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeEcoSche"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valSpec_Life"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("optLife_Sche"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("optGen_Sche"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeGoal_Life"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeGoal_Gen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEffecdate_old"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                End With
			
                '+ AG550: Datos particulares de Intermediarios		
            Case "AG550"
                With Request
                    If CBool(.Form.Item("blnNotBroker")) Then
                        lblnPost = True
                    Else
                        lblnPost = mobjAgentSeq.insPostAG550(mobjValues.StringToType(lintAction, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSuperin_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdSuperin_num"), eFunctions.Values.eTypeData.etdDate), , mobjValues.StringToType(.Form.Item("tcnWarran_pol"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With
			
                '+ AG553: Ramos y productos permitidos		
            Case "AG553"
                lobjAgentSeq_p = New eAgent.branprod_allow
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = lobjAgentSeq_p.insPostAG553("AG553", .QueryString.Item("Action"), mobjValues.StringToType(Session("nIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInstallments"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnStartMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEndMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        lblnPost = True
                    End If
                End With
			
                '+ Ventana de Fin de proceso		
            Case "GE101"
                If Request.Form.Item("optElim") = "Delete" Then
                    '+ Se elimina la información relacionada al cliente
				
                    mobjAgentSeq = New eAgent.Intermedia
                    lblnPost = mobjAgentSeq.Remove(CStr(Session("nIntermed")))
                Else
                    '+ Se verifica que no existan páginas marcadas como requeridas
				
                    lclsErrors = New eGeneralForm.GeneralForm
                    Response.Write(lclsErrors.insValGE101("AgentSeq"))
                    lblnPost = False
                    lclsErrors = Nothing
                End If
                Response.Write("<SCRIPT>top.opener.top.location.reload();</" & "Script>")
                Response.Write("<SCRIPT>window.close()</" & "Script>")
                lblnPost = False
        End Select
	insPostSequence = lblnPost
End Function

'% insFinish: Se activa cuando la acción es Finalizar
'--------------------------------------------------------------------------------------------
Function insFinish() As Boolean
	'--------------------------------------------------------------------------------------------
	'+ Se verifica que no existan páginas marcadas como requeridas
	Dim lclsAgentWin As eAgent.Intermedia
	Dim lclsErrors As eGeneralForm.GeneralForm
	Dim lclsIntermedia_his As eAgent.Intermed_his
	Dim mstrErrors As String
	
	lclsAgentWin = New eAgent.Intermedia
	lclsErrors = New eGeneralForm.GeneralForm
	lclsIntermedia_his = New eAgent.Intermed_his
	
	insFinish = False
	
	With lclsAgentWin
		If .ValRequired(Session("nIntermed")) Then
			If .Find(Session("nIntermed"), True) Then
				If InStr(1, .WithInformation, "AG001") > 0 Then
					insFinish = True
					If .nInt_status = 3 Then
						.nInt_status = 1
					End If
				Else
					.nInt_status = 3
				End If
				
				If insFinish Then
					'+ Se valida que el intermediario sea un Corredor				
					If .nInterTyp = 3 Then
						If InStr(1, .WithInformation, "AG550") <= 0 Then
							insFinish = False
							.nInt_status = 3
						End If
					End If
				End If
				
				.Update_status()
			End If
		End If
	End With
	
	With lclsIntermedia_his
		.nIntermed = mobjValues.StringToType(Session("nIntermed"), eFunctions.Values.eTypeData.etdDouble)
		.nInt_status = 1
		Call .UpdateIntermedia_his_Status()
	End With
	
	If Not insFinish Then
		mstrErrors = lclsErrors.insValGE101("AgentSeq")
		
		If (mstrErrors > vbNullString) Then
			
			Session("sErrorTable") = mstrErrors
			Session("sForm") = Request.Form.ToString
			With Response
				.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
				.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & server.URLEncode(mstrCommand) & "&sQueryString=" & server.URLEncode(Request.Params.Get("Query_String")) & """, ""AgentSeqError"",660,330);")
				.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
				.Write("</" & "Script>")
			End With
			
		End If
		insFinish = False
	End If
	
	lclsAgentWin = Nothing
	lclsErrors = Nothing
	lclsIntermedia_his = Nothing
End Function

'%insGetNewClient. Esta función se encarga de conseguir un código de cliente
'% para los clientes nuevos (Provisionales).
'--------------------------------------------------------------------------
Private Function insGetNewIntermedia(ByVal llngIntermedia As Object) As Object
	'--------------------------------------------------------------------------
	Dim lclsAgent As eAgent.Intermedia
	'+Si la acción es registrar, se busca automáticamente el código de cliente
	If lintAction = 301 Or lintAction = 306 Then
		If llngIntermedia = "0" Or llngIntermedia = vbNullString Then
			lclsAgent = New eAgent.Intermedia
			llngIntermedia = lclsAgent.GetNewIntermediaCode(Session("nUsercode"))
			lclsAgent = Nothing
		End If
	End If
	insGetNewIntermedia = llngIntermedia
End Function

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("valagentseq")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.57
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "valagentseq"
mstrCommand = "&sModule=Agent&sProject=AgentSeq&sCodisplReload=" & Request.QueryString.Item("sCodispl")

If Not String.IsNullOrEmpty(Request.QueryString.Item("nMainAction")) Then
    If Request.QueryString.getvalues("nMainAction").Count > 1 Then
	    lintAction = Request.QueryString.getvalues("nMainAction").GetValue(0)
    Else
	    lintAction = Request.QueryString.Item("nMainAction")
    End If
End If

%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%=mobjValues.StyleSheet()%>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<SCRIPT>
//+ Variable para el control de versiones 
    document.VssVersion="$$Revision: 3 $|$$Date: 6/08/04 19:03 $|$$Author: Nvaplat31 $"
</SCRIPT>
</HEAD>
<BODY>
<%Response.Write("<SCRIPT>")%>
function CancelErrors(){self.history.go(-1)}
function NewLocation(Source,Codisp){
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation;
}
</SCRIPT>
<%
If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
	'+ Si no se han validado los campos de la página
	If Request.Form.Item("sCodisplReload") = vbNullString Then
		mstrErrors = insvalSequence
		Session("sErrorTable") = mstrErrors
	Else
		Session("sErrorTable") = vbNullString
	End If
	
	If mstrErrors > vbNullString Then
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & server.URLEncode(Request.Form.ToString) & server.URLEncode(mstrCommand) & "&sQueryString=" & server.URLEncode(Request.Params.Get("Query_String")) & """,""AgentSeqErrors"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</SCRIPT>")
		End With
	Else
		If insPostSequence Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				
				'+ Si el campo oculto "tctOriginalForm" es distinto a blanco, se pasa su valor como parámetro a
				'+ la ventana Sequence.aspx - ACM - 08/08/2001
				If Request.Form.Item("tctOriginalForm") <> vbNullString Then
					'+ Se mueve automaticamente a la siguiente página						
					Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Agent/AgentSeq/Sequence.aspx?nAction=" & lintAction & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & "&sOriginalForm=" & Request.Form.Item("tctOriginalForm") & "';</SCRIPT>")
				End If
				
				'+ Se mueve automaticamente a la siguiente página
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Agent/AgentSeq/Sequence.aspx?nAction=" & lintAction & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location='/VTimeNet/Agent/AgentSeq/Sequence.aspx?nAction=" & lintAction & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
				End If
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					Response.Write("<SCRIPT language =javascript>self.history.go(-1) </script>")
				End If
			Else
				Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Agent/AgentSeq/Sequence.aspx?nAction=" & lintAction & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</SCRIPT>")
				'+ Se recarga la página que invocó la PopUp
				Select Case Request.QueryString.Item("sCodispl")
					Case "AG001"
						Response.Write("<SCRIPT>top.opener.document.location.href='AG001.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & lintAction & "&Index=" & Request.QueryString.Item("Index") & "'</SCRIPT>")
					Case "AG003"
						Response.Write("<SCRIPT>top.opener.document.location.href='AG003.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & lintAction & "'</SCRIPT>")
					Case "AG550"
						Response.Write("<SCRIPT>top.opener.document.location.href='AG550.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & lintAction & "'</SCRIPT>")
					Case "AG553"
						Response.Write("<SCRIPT>top.opener.document.location.href='AG553.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & lintAction & "'</SCRIPT>")
				End Select
			End If
		End If
	End If
Else
	If lintAction = eFunctions.Menues.TypeActions.clngActionQuery Then
		Session("MenuOption") = lintAction
		Response.Write("<SCRIPT>top.location.reload();</SCRIPT>")
	Else
		If insFinish Then
			Session("sOriginalForm") = vbNullString
			If CStr(Session("MenuOption")) <> vbNullString And Session("MenuOption") <> 401 Then
				Session("nLastIntermediary") = vbNullString
			End If
			Response.Write("<SCRIPT>top.location.reload();</SCRIPT>")
		End If
	End If
End If

mobjAgentSeq = Nothing
mobjValues = Nothing

%>

</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.57
Call mobjNetFrameWork.FinishPage("valagentseq")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




