<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String

'- Variable para el manejo de los errores de la página, devueltos por insvalSequence
Dim mstrErrors As String

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjMantAgreementSeq As Object


'% insvalSequence: Se realizan las validaciones masivas de las páginas
'--------------------------------------------------------------------------------------------
Function insvalSequence() As String
        '--------------------------------------------------------------------------------------------
        Dim nSelCount As Integer = 0
	Select Case Request.QueryString.Item("sCodispl")
		'+ GE101 : Cancelación del proceso
		Case "GE101"
			insvalSequence = vbNullString
			
			'+ MVA646_K: Administración de convenios
		Case "MVA646_K"
			With Request
				mobjMantAgreementSeq = New eBranches.Agreement_al
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalSequence = mobjMantAgreementSeq.InsValMVA646_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble, True))
				End If
			End With
			
			'+ MVA646A : Datos generales del convenio
		Case "MVA646A"
                With Request
                    mobjMantAgreementSeq = New eBranches.Agreement_al
                    insvalSequence = mobjMantAgreementSeq.InsValMVA646A("MVA646A", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdNulldate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeAgree_Type"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valIntermed"), eFunctions.Values.eTypeData.etdInteger))
                End With
			
			'+ MVA646B : Tipos de intermediarios por convenio
		Case "MVA646B"
                With Request
                    If Not String.IsNullOrEmpty(.Form.Item("Sel")) Then
                        nSelCount = .Form.Item("Sel").Count
                    End If
                    mobjMantAgreementSeq = New eBranches.Int_typ_agre
                    insvalSequence = mobjMantAgreementSeq.InsValMVA646B("MVA646B", nSelCount)
                End With
			
			'+ MVA646C : Intermediarios por convenio
		Case "MVA646C"
                With Request
                    If Not String.IsNullOrEmpty(.Form.Item("Sel")) Then
                        nSelCount = .Form.Item("Sel").Count
                    End If

                    mobjMantAgreementSeq = New eBranches.Interm_agre
                    insvalSequence = mobjMantAgreementSeq.InsValMVA646C("MVA646C", nSelCount)
                End With
			
			'+ MVA646D : Planes del convenio
		Case "MVA646D"
			With Request
                    If Not String.IsNullOrEmpty(.Form.Item("Sel")) Then
                        nSelCount = .Form.Item("Sel").Count
                    End If

                    mobjMantAgreementSeq = New eBranches.Plan_agre
                    insvalSequence = mobjMantAgreementSeq.InsValMVA646D("MVA646D", nSelCount)
			End With
			
		Case Else
			insvalSequence = "insvalSequence: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostSequence: Se realizan las actualizaciones de las ventanas
'--------------------------------------------------------------------------------------------
Function insPostSequence() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	Dim llngAction As Integer
	
	lblnPost = False
	Select Case Request.QueryString.Item("sCodispl")
		'+GE101: Cancelación del proceso
		Case "GE101"
			lblnPost = insCancel
			
			'+ MVA646_K: Administración de convenios
		Case "MVA646_K"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					lblnPost = mobjMantAgreementSeq.InsPostMVA646_K(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
					Session("nAgreement") = mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble)
				End If
				
			End With
			
			'+ MVA646A : Datos generales del convenio
		Case "MVA646A"
			With Request
				If mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble) = eFunctions.Menues.TypeActions.clngActionQuery Then
					llngAction = eFunctions.Menues.TypeActions.clngActionQuery
				Else
					llngAction = eFunctions.Menues.TypeActions.clngActionUpdate
				End If
                    lblnPost = mobjMantAgreementSeq.InsPostMVA646A(llngAction, Session("nAgreement"), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddNulldate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("optLevelInt"), .Form.Item("cbeStatus"), Session("nUsercode"), mobjValues.StringToType(.Form.Item("cbeAgree_Type"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valIntermed"), eFunctions.Values.eTypeData.etdInteger))
			End With
			
			'+ MVA646B : Tipos de intermediarios por convenio
		Case "MVA646B"
			With Request
				lblnPost = mobjMantAgreementSeq.InsPostMVA646B(Session("nAgreement"), .Form.Item("hddsSel"), .Form.Item("hddnExist"), .Form.Item("hddIntertyp"), Session("nUsercode"))
			End With
			
			'+ MVA646C : Intermediarios por convenio
		Case "MVA646C"
			With Request
				lblnPost = mobjMantAgreementSeq.InsPostMVA646C(Session("nAgreement"), .Form.Item("hddsSel"), .Form.Item("hddnExist"), .Form.Item("hddIntermed"), Session("nUsercode"))
			End With
			
			'+ MVA646D : Planes del convenio
		Case "MVA646D"
			With Request
				lblnPost = mobjMantAgreementSeq.InsPostMVA646D(Session("nAgreement"), .Form.Item("hddsSel"), .Form.Item("hddnExist"), .Form.Item("hddBranch"), .Form.Item("hddProduct"), .Form.Item("hddModulec"), Session("nUsercode"))
			End With
	End Select
	insPostSequence = lblnPost
End Function

'% insCancel: Función que se ejecuta al cancelar la secuencia
'--------------------------------------------------------------------------------------------
Private Function insCancel() As Boolean
	'--------------------------------------------------------------------------------------------
	insCancel = True
	If Request.Form.Item("optElim") = "Delete" Then
		mobjMantAgreementSeq = New eBranches.Agreement_al
		mobjMantAgreementSeq.nAgreement = Session("nAgreement")
		mobjMantAgreementSeq.Delete()
	End If
	Response.Write("<SCRIPT>opener.top.location.reload();window.close()</" & "Script>")
End Function

'% insFinish: se activa al finalizar el proceso
'--------------------------------------------------------------------------------------------
Function insFinish() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lclsErrors As eGeneralForm.GeneralForm
	Dim lstrError As Object
	Dim lblnError As Boolean
	insFinish = True
	If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionAdd) Or Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Then
		lblnError = True
		mobjMantAgreementSeq = New eBranches.Agreement_al
            If mobjMantAgreementSeq.ValContent(Session("nAgreement")) Then
                
                If (mobjMantAgreementSeq.sLevelint = "1" And Trim(mobjMantAgreementSeq.WithInformation) = "MVA646A MVA646B MVA646D") Or (mobjMantAgreementSeq.sLevelint = "2" And Trim(mobjMantAgreementSeq.WithInformation) = "MVA646A MVA646C MVA646D") Then
                    lblnError = False
                End If
            End If
		If lblnError Then
			lclsErrors = New eGeneralForm.GeneralForm
			Session("sErrorTable") = lclsErrors.insValGE101("ClientSeq")
			Session("sForm") = Request.Form.ToString
			With Response
				.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
				.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""CoverSeqError"",660,330);")
				.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
				.Write("</" & "Script>")
			End With
			insFinish = False
			lclsErrors = Nothing
		Else
			If mobjMantAgreementSeq.Find(Session("nAgreement")) Then
				If mobjMantAgreementSeq.sStatregt = "2" Then
					mobjMantAgreementSeq.InsPostMVA646A(eFunctions.Menues.TypeActions.clngActionUpdate, Session("nAgreement"), mobjMantAgreementSeq.sDescript, mobjMantAgreementSeq.dStartdate, eRemoteDB.Constants.dtmNull, mobjMantAgreementSeq.sLevelint, "1", Session("nUsercode"))
				End If
			End If
		End If
	End If
End Function

</script>
<%
Response.Expires = 0
mobjValues = New eFunctions.Values

mstrCommand = "&sModule=Maintenance&sProject=MantAgreement_al&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <SCRIPT>
    //- Variable para el control de versiones
        document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:39 $|$$Author: Nvaplat61 $"
    </SCRIPT>
    <%=mobjValues.StyleSheet()%>



    
</HEAD>
<BODY>
<FORM NAME="ValAgreementSeq">
<%
'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalSequence
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
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantAgreement_alError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</SCRIPT>")
		End With
	Else
		If insPostSequence Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				'+ Si se está tratando con un frame y no con la ventana principal de la secuencia, 
				'+ se mueve automaticamente a la siguiente página
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Maintenance/MantAgreement_al/Sequence.aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location='/VTimeNet/Maintenance/MantAgreement_al/Sequence.aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
				End If
			Else
				'+ Se recarga la página que invocó la PopUp
				Select Case Request.QueryString.Item("sCodispl")
					Case "sCodispl"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "'</SCRIPT>")
				End Select
			End If
		End If
	End If
Else
	'+ Se recarga la página principal de la secuencia        
	If insFinish() Then
		With Response
			.Write("<SCRIPT>")
			.Write("insReloadTop(false)")
			.Write("</SCRIPT>")
		End With
	End If
End If
mobjMantAgreementSeq = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




