<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">

    '- Se define la contante para el manejo de errores en caso de advertencias
    Dim mstrCommand As String

    '- Variable que guarda la cadena a pasar por el QueryString
    Dim mstrQueryString As String

    Dim mstrErrors As String
    Dim mobjValues As eFunctions.Values
    Dim sActualSchema As String


    '% insValSecurity: Se realizan las validaciones masivas de la forma dependiendo del sCodispl.
    '--------------------------------------------------------------------------------------------
    Function insValSecurity() As String
        '--------------------------------------------------------------------------------------------
        Dim lclsUser As eSecurity.User

        insValSecurity = vbNullString

        Dim lclsPolicy_security As eSecurity.Policy_security
        With Request
            Select Case .QueryString.Item("sCodispl")
            '+ SG001: Usuarios del sistema.
                Case "SG001"
                    lclsUser = New eSecurity.User
                    If Request.QueryString.Item("nZone") = "1" Then
                        insValSecurity = lclsUser.InsValSG001_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valUsercod"), eFunctions.Values.eTypeData.etdDouble, True))
                    Else
                        If CDbl(.QueryString.Item("nMainAction")) <> 401 And CDbl(.QueryString.Item("nMainAction")) <> 303 Then
                            lclsUser.bPasswordSet = .Form("chkSetPassword") = "1" Or .QueryString.Item("nMainAction") = 301.ToString()
                            If .QueryString.Item("WindowType") <> "PopUp" Then
                                insValSecurity = lclsUser.InsValSG001(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("hddnUsercode"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("valScheCode"), .Form.Item("cbeUserTyp"), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("sClient"), .Form.Item("tctInitials"), .Form.Item("tctPassword"), mobjValues.StringToType(.Form.Item("tcdDateFrom"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeDeparment"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("cbeStatregt"), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble, True))
                            End If
                        End If
                    End If

                    lclsUser = Nothing

                '+ SG099: Cambio de contraseña.
                Case "SG099"
                    lclsUser = New eSecurity.User
                    insValSecurity = lclsUser.InsValSG099_K(.QueryString.Item("sCodispl"), .Form.Item("tctOldPass"), .Form.Item("tctNewPass"), .Form.Item("tctRNewPass"), mobjValues.TypeToString(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    lclsUser = Nothing

                '+ SG852: Mantenimiento de pólizas con acceso restringido.
                Case "SG852"
                    lclsPolicy_security = New eSecurity.Policy_security
                    insValSecurity = lclsPolicy_security.insValSG852(.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("Valusers"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnUsercode_old"), eFunctions.Values.eTypeData.etdDouble))
                    lclsPolicy_security = Nothing

                Case Else
                    insValSecurity = "insValSecurity: Código lógico no encontrado (" & .QueryString.Item("sCodispl") & ")"
            End Select
        End With
    End Function

    '% insPostSecurity: Se realizan las actualizaciones a las tablas dependiendo del sCodispl de la 
    '% ventana.
    '--------------------------------------------------------------------------------------------
    Function insPostSecurity() As Boolean
        '--------------------------------------------------------------------------------------------
        Dim lclsUser As eSecurity.User

        insPostSecurity = True

        Dim lclsPolicy_security As eSecurity.Policy_security
        With Request
            Select Case Request.QueryString.Item("sCodispl")
            '+ SG001: Usuarios del sistema.
                Case "SG001"
                    With Request
                        If .QueryString.Item("nZone") = "1" Then
                            mstrQueryString = "&nUsercode=" & .Form.Item("valUsercod")
                        Else
                            If .QueryString.Item("nZone") = "2" And CDbl(.QueryString.Item("nMainAction")) <> 401 Then
                                lclsUser = New eSecurity.User

                                If .QueryString.Item("WindowType") <> "PopUp" Then
                                    lclsUser.bPasswordSet = .Form("chkSetPassword") = "1" Or .QueryString.Item("nMainAction") = 301.ToString()
                                    insPostSecurity = lclsUser.InsPostSG001(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valScheCode"), .Form.Item("cbeUserTyp"), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("sClient"), .Form.Item("tctInitials"), .Form.Item("tctPassword"), mobjValues.StringToType(.Form.Item("tcdDateFrom"), eFunctions.Values.eTypeData.etdDate), .Form.Item("cbeStatregt"), mobjValues.StringToType(.Form.Item("cbeDeparment"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("valMenu"), .Form.Item("chkChNextLogon"), .Form.Item("hddNeverChange"), .Form.Item("chkNeverExpires"), .Form.Item("chkLockedOut"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble, True))
                                End If
                            End If
                        End If
                    End With

                    lclsUser = Nothing

                '+ SG099: Cambio de contraseña.
                Case "SG099"
                    lclsUser = New eSecurity.User
                    insPostSecurity = lclsUser.InsPostSG099(mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctLogin"), .Form.Item("tctNewPass"), .Form.Item("tctOldPass"))
                    If Session("sSche_code") = Session("PasswordChangeSchema") Then
                        lclsUser.Find(Session("nUsercode"))
                        sActualSchema = lclsUser.sSche_code
                    End If
                    If insPostSecurity Then

                        Response.Write("<SCRIPT> alert('La contraseña fue cambiada satisfactoriamente') </" & "Script>")

                    Else
                        Response.Write("<SCRIPT> alert('La contraseña no se pudo cambiar') </" & "Script>")
                    End If
                    lclsUser = Nothing
                '+ SG852: Mantenimiento de pólizas con acceso restringido.
                Case "SG852"
                    lclsPolicy_security = New eSecurity.Policy_security
                    insPostSecurity = lclsPolicy_security.InsPostSG852(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("Valusers"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("hddnUsercode_old"), eFunctions.Values.eTypeData.etdDouble, True))
                    lclsPolicy_security = Nothing
                Case Else
                    insPostSecurity = False
            End Select
        End With
    End Function

</script>
<%
    Response.Expires = -1
    mstrCommand = "&sModule=Security&sProject=Security&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
    <LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>



    
</HEAD>
<BODY>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 5 $|$$Date: 17/04/04 19:42 $|$$Author: Nvaplat37 $"

//%CancelErrors: Va a la ventana anterior si se produce un error.
//------------------------------------------------------------------------------------
function CancelErrors(){
//------------------------------------------------------------------------------------
    self.history.go(-1)}

//%NewLocation: Se posiciona en la página seleccionada. 
//------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//------------------------------------------------------------------------------------
    var lstrLocation = "";
    
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
<SCRIPT src="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>
<%
    mobjValues = New eFunctions.Values

    '+ Si no se han validado los campos de la página.

    If Request.Form.Item("sCodisplReload") = vbNullString Then
        mstrErrors = insValSecurity()
        Session("sErrorTable") = mstrErrors
        Session("sForm") = Request.Form.ToString
    Else
        Session("sErrorTable") = vbNullString
        Session("sForm") = vbNullString
    End If

    '+ Si se produce un error en la ventana envía las validaciones respectivas.

    If mstrErrors > vbNullString Then
        With Response
            .Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
            .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.UrlEncode(mstrCommand) & "&sQueryString=" & Server.UrlEncode(Request.Params.Get("Query_String")) & """, ""SecurityErrors"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
            .Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
            .Write("</SCRIPT>")
        End With
    Else

        '+ Si no se produce ningún error en la ventana se realiza el llamado al Post.

        If insPostSecurity() Then
            If Request.QueryString.Item("WindowType") <> "PopUp" Then
                If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Then
                    If Session("sSche_code") = Session("PasswordChangeSchema") Then
                        Session("sSche_code") = sActualSchema
                        If Request.Form.Item("sCodisplReload") = vbNullString Then
                            Response.Write("<SCRIPT>top.location.href= '/VTimeNet/VisualTime/VisualTime.htm';</SCRIPT>")
                        Else
                            Response.Write("<SCRIPT>opener.top.location.href= '/VTimeNet/VisualTime/VisualTime.htm';window.close();</SCRIPT>")
                        End If

                    ElseIf Request.Form.Item("sCodisplReload") = vbNullString Then
                        Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
                    Else
                        Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
                    End If
                Else
                    If Request.Form.Item("sCodisplReload") = vbNullString Then
                        If Request.QueryString.Item("sCodispl") = "SG099" Then
                            Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
                        Else
                            Response.Write("<SCRIPT>self.history.go(-1);top.fraFolder.document.location='" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & mstrQueryString & "';</SCRIPT>")
                        End If
                    Else
                        Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location='" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & mstrQueryString & "';</SCRIPT>")
                    End If
                End If
            Else
                '+ Se recarga la página que invocó la PopUp.

                Select Case Request.QueryString.Item("sCodispl")
                    Case "SG099"
                        Response.Write("<SCRIPT>opener.document.location.href='SG099_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=304'</SCRIPT>")
                    Case "SG852"
                        Response.Write("<SCRIPT>top.opener.document.location.href='SG852_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
                End Select
            End If
        End If
    End If

    mobjValues = Nothing
%>
</BODY>
</HTML>




