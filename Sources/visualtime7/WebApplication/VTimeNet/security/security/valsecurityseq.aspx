<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">

    Dim mobjValues As eFunctions.Values
    Dim lobjSecuritySeq As eSecurity.Windows
    Dim mstrErrors As String
    Dim mstrFinish As String

    '+ Se define la constante para el manejo de errores en caso de advertencias.
    Dim mstrCommand As String


    '% insValSecurity: Se realizan las validaciones masivas de cada una de las páginas.
    '--------------------------------------------------------------------------------------------
    Function insValSecurity() As String
        '--------------------------------------------------------------------------------------------
        insValSecurity = vbNullString

        Select Case Request.QueryString.Item("sCodispl")

        '+ Se realizan las validaciones del encabezado de la página 
        '+ SG005_K -Transacciones del sistema.
            Case "SG005_k"
                insValSecurity = lobjSecuritySeq.insValSG005_K(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("valCodispl"), Request.Form.Item("tctPseudo"), mobjValues.StringToType(Request.Form.Item("cbeWindowty"), eFunctions.Values.eTypeData.etdDouble))

            '+ Validaciones del frame SG005 - Transacciones del sistema.
            Case "SG005"
                insValSecurity = lobjSecuritySeq.insValSG005(Request.QueryString.Item("sCodispl"), Session("sCodispLog"), Session("sPseudo"), mobjValues.StringToType(Session("nWindowty"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctDescript"), Request.Form.Item("tctShort_des"), Request.Form.Item("tctPseudo"), Request.Form.Item("tctCodisp"), mobjValues.StringToType(Request.Form.Item("cbeModules"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("chkDirectGo"), Request.Form.Item("valCodMen"), mobjValues.StringToType(Request.Form.Item("tcnSequence"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnAmelevel"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnInqLevel"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("nImage_index"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("chkAutorep"), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble))

            '+ Validaciones del frame SG006 - Información de tablas generales.

            Case "SG006"
                insValSecurity = lobjSecuritySeq.insValSG006(Request.QueryString.Item("sCodispl"), Session("sCodispLog"), mobjValues.StringToType(Request.Form.Item("tcnG_identi"), eFunctions.Values.eTypeData.etdDouble))

            '+ Validaciones del frame SG009 - Horario restringido de transacciones.
            Case "SG009"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    insValSecurity = lobjSecuritySeq.insValSG009(Request.QueryString.Item("Action"), Request.QueryString.Item("sCodispl"), Session("sCodispLog"), Request.Form.Item("sHour_Start"), Request.Form.Item("sHour_End"))
                End If

            Case "SG016"
                insValSecurity = ""

            Case "GE101"
                insValSecurity = ""

            Case Else
                insValSecurity = "insValSecurity: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
        End Select
    End Function

    '% insPostSecurity: Se realizan las actualizaciones de las ventanas.
    '--------------------------------------------------------------------------------------------
    Function insPostSecurity() As Boolean
        Dim llngAction As String
        Dim lstrMessage As String
        Dim lintQueryMenu As Byte
        Dim lintFirst As Byte
        Dim lintExistAction As Byte
        Dim lintQuan As Double
        Dim lstrDescript As String
        Dim lintIndex As Long
        '--------------------------------------------------------------------------------------------
        Dim lblnPost As Boolean

        lblnPost = True

        Dim lclsErrors As eGeneral.GeneralFunction
        Select Case Request.QueryString.Item("sCodispl")

        '+ SG005_k - Transacciones del sistema.
            Case "SG005_k"
                lblnPost = lobjSecuritySeq.insPostSG005_k(mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("valCodispl"), Request.Form.Item("tctPseudo"), mobjValues.StringToType(Request.Form.Item("cbeWindowty"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble))


                If lblnPost Then
                    Session("sCodispLog") = Request.Form.Item("valCodispl")
                    Session("sPseudo") = Request.Form.Item("tctPseudo")
                    Session("nWindowty") = Request.Form.Item("cbeWindowty")
                End If

            '+ Frame SG005 - Transacciones del sistema.
            Case "SG005"
                Session("sPseudo") = Request.Form.Item("tctPseudo")

                lblnPost = lobjSecuritySeq.insPostSG005(Session("sCodispLog"), Request.Form.Item("tctDescript"), Request.Form.Item("tctShort_des"), Request.Form.Item("tctPseudo"), Request.Form.Item("tctCodisp"), mobjValues.StringToType(Request.Form.Item("cbeModules"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("cbeStatregt"), mobjValues.StringToType(Request.Form.Item("tcnAmelevel"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnInqLevel"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnSequence"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkDirectGo"), Request.Form.Item("valCodMen"), mobjValues.StringToType(Request.Form.Item("nImage_index"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkAutorep"), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnLength_Notes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnHeight"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("cbetypereport"), eFunctions.Values.eTypeData.etdLong), Request.Form.Item("tcsFilePath"), Request.Form.Item("tcsHelpPath"))

            '+ Validaciones del frame SG006 - Información de tablas generales.

            Case "SG006"
                lblnPost = lobjSecuritySeq.insPostSG006(Session("sCodispLog"), mobjValues.StringToType(Request.Form.Item("tcnG_identi"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble))

            '+ Frame SG009 - Horario restringido de transacciones.

            Case "SG009"
                lblnPost = lobjSecuritySeq.insPostSG009(Request.QueryString.Item("Action"), Session("sCodispLog"), Request.Form.Item("sHour_Start"), Request.Form.Item("sHour_End"), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble))


            '+ Frame SG016 - Acciones de las transacciones.

            Case "SG016"
                lobjSecuritySeq.sCodispl = Session("sCodispLog")

                lblnPost = lobjSecuritySeq.DeleteWin_actions()

                If lblnPost Then

                    lintIndex = 0
                    lintQuan = 1
                    lintFirst = 1
                    lintQueryMenu = 0
                    lintExistAction = 0

                    If Not IsNothing(Request.Form.Item("nAction")) Then
                        For Each llngAction In Request.Form.GetValues("nAction")
                            lintIndex = lintIndex + 1

                            If Request.Form.GetValues("nSelValueMain").GetValue(lintIndex - 1) = 1 Then
                                If lintFirst = 1 Then
                                    With lobjSecuritySeq
                                        .nActions = eFunctions.Menues.TypeActions.clngMenuActions
                                        .nCounter = lintQuan
                                        .nUsercode = Session("nUserCode")
                                    End With

                                    lblnPost = lobjSecuritySeq.AddWin_actions

                                    lintQuan = lintQuan + 1

                                    lintFirst = 2
                                End If

                                With lobjSecuritySeq
                                    .nActions = CInt(Request.Form.GetValues("nAction").GetValue(lintIndex - 1))
                                    .nCounter = lintQuan
                                    .nUsercode = Session("nUserCode")
                                End With

                                lblnPost = lobjSecuritySeq.AddWin_actions

                                lintQuan = lintQuan + 1

                                If Request.Form.GetValues("nSelValueMain").GetValue(lintIndex - 1) = 1 And Request.Form.GetValues("nAction").GetValue(lintIndex - 1) = eFunctions.Menues.TypeActions.clngMenuInquiry And lintQueryMenu = 0 Then
                                    lintQueryMenu = 1
                                End If
                            End If
                        Next llngAction
                    End If

                    If lintQueryMenu = 1 Then
                        lintFirst = 1
                        lintIndex = 0

                        If Not IsNothing(Request.Form.Item("nActionQuery")) Then
                            For Each llngAction In Request.Form.GetValues("nActionQuery")
                                lintIndex = lintIndex + 1

                                If CDbl(Request.Form.GetValues("nSelValueQuery").GetValue(lintIndex - 1)) = 1 Then

                                    If lintFirst = 1 Then
                                        With lobjSecuritySeq
                                            .nActions = eFunctions.Menues.TypeActions.clngMenuInquiry
                                            .nCounter = lintQuan
                                            .nUsercode = Session("nUserCode")
                                        End With

                                        lblnPost = lobjSecuritySeq.AddWin_actions

                                        lintQuan = lintQuan + 1

                                        lintFirst = 2
                                    End If

                                    If Request.Form.GetValues("nAction").GetValue(lintIndex - 1) = CStr(eFunctions.Menues.TypeActions.clngActionFirst) Then

                                        If lintExistAction = 1 Then
                                            With lobjSecuritySeq
                                                .nActions = eFunctions.Menues.TypeActions.clngMenuDelimiter
                                                .nCounter = lintQuan
                                                .nUsercode = Session("nUserCode")
                                            End With

                                            lblnPost = lobjSecuritySeq.AddWin_actions

                                            lintQuan = lintQuan + 1
                                        End If
                                    Else
                                        lintExistAction = 1
                                    End If

                                    With lobjSecuritySeq
                                        .nActions = CInt(Request.Form.GetValues("nActionQuery").GetValue(lintIndex - 1))
                                        .nCounter = lintQuan
                                        .nUsercode = Session("nUserCode")
                                    End With

                                    lblnPost = lobjSecuritySeq.AddWin_actions

                                    lintQuan = lintQuan + 1
                                End If
                            Next llngAction
                        End If
                    End If
                End If

            '+ Ventana de Fin de proceso.

            Case "GE101"
                If Request.Form.Item("optElim") = "Delete" Then
                    '+ Se elimina la información relacionada al cliente.
                    lblnPost = lobjSecuritySeq.Delete(Session("sCodispLog"))
                    If lobjSecuritySeq.nIndic = 1 Then
                        lclsErrors = New eGeneral.GeneralFunction
                        lstrMessage = lclsErrors.insLoadMessage(12173)
                        Response.Write("<SCRIPT>alert(""Men. 12173: " & lstrMessage & """);</" & "Script>")
                        lclsErrors = Nothing
                    End If
                Else

                    lstrDescript = CStr(Session("sCodispLog")) & "- En proceso de instalación"

                    lblnPost = lobjSecuritySeq.insPostSG005(Session("sCodispLog"), lstrDescript, Request.Form.Item("tctShort_des"), Session("sPseudo"), Session("sCodispLog"), mobjValues.StringToType(Request.Form.Item("cbeModules"), eFunctions.Values.eTypeData.etdDouble), "2", mobjValues.StringToType(Request.Form.Item("tcnAmelevel"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnInqLevel"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnSequence"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkDirectGo"), Request.Form.Item("valCodMen"), mobjValues.StringToType(Request.Form.Item("nImage_index"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkAutorep"))
                End If

                Response.Write("<SCRIPT>opener.top.location.reload();</" & "Script>")
                Response.Write("<SCRIPT>window.close()</" & "Script>")

                lblnPost = False
        End Select

        insPostSecurity = lblnPost
    End Function

    '% insFinish: Se activa cuando la acción es Finalizar
    '--------------------------------------------------------------------------------------------
    Function insFinish() As Boolean
        '--------------------------------------------------------------------------------------------
        '+ Se verifica que no existan páginas marcadas como requeridas.

        Dim lclsSecurityWin As eSecurity.Windows
        Dim lclsErrors As eGeneralForm.GeneralForm

        lclsSecurityWin = New eSecurity.Windows
        lclsErrors = New eGeneralForm.GeneralForm

        insFinish = False

        Dim lobjError As eFunctions.Errors
        If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionCut) Then
            With lclsSecurityWin
                If .valSequenWinFinish(Session("sCodispLog"), Session("nWindowty")) Then
                    insFinish = True

                    If .sStatregt = "2" Then
                        .sStatregt = "1"
                    End If
                Else
                    .sStatregt = "3"
                End If

                .Update(Session("Estado"))
            End With

            If Not insFinish Then

                lobjError = New eFunctions.Errors

                With lobjError
                    mstrFinish = .ErrorMessage("SG005", 3902)
                    mstrFinish = .Confirm()
                End With

                lobjError = Nothing

                Session("sErrorTable") = mstrFinish

                With Response
                    .Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
                    .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & Server.URLEncode(Request.Form.ToString) & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """,""ValSecurityErrors"",660,330);")
                    .Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
                    .Write("</" & "Script>")
                End With
            End If
        Else
            insFinish = lclsSecurityWin.insPostSG005(Session("sCodispLog"), vbNullString, vbNullString, vbNullString, vbNullString, eRemoteDB.Constants.intNull, vbNullString, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, vbNullString, vbNullString, eRemoteDB.Constants.intNull, mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), vbNullString, mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble))
        End If

        lclsSecurityWin = Nothing
        lclsErrors = Nothing
    End Function

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
lobjSecuritySeq = New eSecurity.Windows

mstrCommand = "&sModule=Security&sProject=Security&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%=mobjValues.StyleSheet()%>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




</HEAD>
<BODY>
<%Response.Write("<SCRIPT>")%>

//-----------------------------------------------------------------------------
function CancelErrors(){
//-----------------------------------------------------------------------------
    self.history.go(-1)}

//-----------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//-----------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation;
}

</SCRIPT>
<%
If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
	'+ Si no se han validado los campos de la página.
	If Request.Form.Item("sCodisplReload") = vbNullString Then
		mstrErrors = insValSecurity
		Session("sErrorTable") = mstrErrors
		Session("sForm") = Request.Form.ToString
	Else
		Session("sErrorTable") = vbNullString
		Session("sForm") = vbNullString
	End If
	
	If mstrErrors > vbNullString Then
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & Server.URLEncode(Request.Form.ToString) & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """,""ValSecurityErrors"",660,330);")
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</SCRIPT>")
		End With
	Else
		If insPostSecurity Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				'+ Se mueve automáticamente a la siguiente página.
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Security/Security/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location='/VTimeNet/Security/Security/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
					If CDbl(Request.QueryString.Item("nZone")) = 1 Then
						Response.Write("<SCRIPT>self.history.go(-1)</SCRIPT>")
					End If
				End If
			Else
				Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location=""/VTimeNet/Security/Security/Sequence.aspx?nAction=0" & Request.QueryString.Item("nMainAction") & "&sGoToNext=NO&nOpener=" & Request.QueryString.Item("sCodispl") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
				'+ Se recarga la página que invocó la PopUp.
				Select Case Request.QueryString.Item("sCodispl")
					Case "SG005"
						Response.Write("<SCRIPT>opener.document.location.href='SG005.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&Index=" & Request.QueryString.Item("Index") & "'</SCRIPT>")
					Case "SG006"
						Response.Write("<SCRIPT>opener.document.location.href='SG006.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
					Case "SG009"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "'</SCRIPT>")
					Case "SG016"
						Response.Write("<SCRIPT>opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
				End Select
			End If
		End If
	End If
Else
	If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
		Response.Write("<SCRIPT>top.location.reload();</SCRIPT>")
	Else
		If insFinish Then
			Response.Write("<SCRIPT>top.location.reload();</SCRIPT>")
		End If
	End If
End If

lobjSecuritySeq = Nothing
mobjValues = Nothing

%>

</BODY>
</HTML>






