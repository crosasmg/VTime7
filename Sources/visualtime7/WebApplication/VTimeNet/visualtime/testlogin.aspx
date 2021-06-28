<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<%@ Import namespace="eRemoteDB" %>
<script language="VB" runat="Server">
Dim mobjValues As eFunctions.Values
Dim mstrMessages As String
Const clngUserLoginFail As Short = 1
Const clngUserLoginEmpty As Short = 2
Const clngUserPasswdFail As Short = 3
Const clngUserValid As Short = 4
Const clngUserLock As Short = 5
Const clngUserSchemaFail As Short = 6


'%insValidateUser. Esta función realiza la validación del usuario al sistema.
'---------------------------------------------------------------------------------------------
Private Function insValidateUser(ByVal sInitials As String, ByVal sPasswd As String) As Boolean
	'---------------------------------------------------------------------------------------------
	Dim lobjUserValidate As eSecurity.UserValidate
	Dim lobjUser As Object
	Dim lobjSchema As Object
	Dim lobjGeneral As Object
	Dim lintAccessOf As Object
	Dim lintIndCurrency As Object
	Dim lintLimits As Object
	Dim lintLevels As Object
	insValidateUser = False
	lobjUserValidate = New eSecurity.UserValidate
	If Request.QueryString.Item("sCallMode") = "WFVT" Then
		sInitials = Request.QueryString.Item("tctLogin")
		sPasswd = lobjUserValidate.StrDecode(Request.QueryString.Item("tctPasswd"))
	End If
	'+Se realiza la busqueda del usuario por medio de la iniciales 
	With lobjUserValidate
		If .ValidateUser(sInitials, sPasswd) Then
			insValidateUser = True
			Session("nUsercode") = .objUser.nUsercode
			Session("sSche_code") = .objUser.sSche_code
			Session("nOffice") = .objUser.nOffice
			Session("sAccesswo") = .objUser.sAccesswo
			Session("sPolicyNum") = .objOptSystem.sPolicyNum
			Session("sReceiptNum") = .objOptSystem.sReceiptNum
			Session("sUserClient") = .objUser.sClient
			Session("nCashNum") = .objCashBank.nCashNum
			Session("nOfficeAgen") = .objUser.nOfficeAgen
			Session("nAgency") = .objUser.nAgency
			Application.Lock()
			Application("cstrTypeCompany") = .objOptSystem.sTypeCompany
			Application("nCompany") = .objOptSystem.nCompany
			Application.UnLock()
		Else
			Session("sInitials") = vbNullString
			Session("sAccesswo") = vbNullString
			Select Case .UserStatus
				Case clngUserLoginEmpty
					Call insChargeMessage(12049)
				Case clngUserLoginFail
					Call insChargeMessage(99145)
				Case clngUserPasswdFail
					Call insChargeMessage(1903)
				Case clngUserLock
					Call insChargeMessage(12097)
				Case clngUserSchemaFail
					Call insChargeMessage(12097)
				Case 7 'clngAccessDenied
					Call insChargeMessage(99146)
				Case Else
					Call insChargeMessage(99145)
			End Select
		End If
	End With
	'UPGRADE_NOTE: Object lobjUser may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lobjUser = Nothing
End Function

'% insChargeMessage: Esta funcion se encarga de buscar el mensaje asociado al número pasado como
'%                   parámetro
'-----------------------------------------------------------------------------------------------
Private Function insChargeMessage(ByVal nError As Integer) As Object
	'-----------------------------------------------------------------------------------------------
	Dim lobjQuery As eRemoteDB.Query
	If nError <> 0 Then
		lobjQuery = New eRemoteDB.Query
		If lobjQuery.OpenQuery("Message", "sMessaged", "nerrornum = " & CStr(nError)) Then
			mstrMessages = lobjQuery.FieldToClass("sMessaged")
		End If
		'UPGRADE_NOTE: Object lobjQuery may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		lobjQuery = Nothing
	Else
		mstrMessages = vbNullString
	End If
End Function

</script>
<%Response.Expires = -1
Response.Buffer = True
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
    <TITLE>Visual Time</TITLE>
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%=mobjValues.StyleSheet()%>
<SCRIPT>
//% insDoLogin: se ejecuta la acción de la página
//-------------------------------------------------------------------------------------------
function insDoLogin(){
//-------------------------------------------------------------------------------------------
    document.forms[0].submit()
}

//+ Este código se utiliza para utilizar el enter como submit
    if (document.layers)
        document.captureEvents(Event.KEYPRESS);
    document.onkeypress = function(evt){
						      var key = document.all ? event.keyCode : evt.which ? evt.which : evt.keyCode;
							  if (key == 13) 
							      document.forms[0].submit();
                          }
</SCRIPT>
</HEAD>
<BODY>
<%
If Request.QueryString.Item("Validate") = "1" Then
	If insValidateUser(Request.Form.Item("tctLogin"), Request.Form.Item("tctPasswd")) Then
		Session("SessionID") = Session.SessionID
		Response.Cookies.Item("sUsuallyLog").Value = Request.Form.Item("tctLogin")
		'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
		Response.Cookies.Item("sUsuallyLog").Expires = DateAdd(Microsoft.VisualBasic.DateInterval.Day, Today.ToOADate, System.Date.FromOADate(1))
	End If
End If
If CStr(Session("SessionID")) <> "" And Request.QueryString.Item("Validate") = "1" Then
	Session("sCallMode") = Request.QueryString.Item("sCallMode")
	Session("WFAddress") = Request.QueryString.Item("URL")
	Session("sFrame") = Request.QueryString.Item("sFrame")
	If CStr(Session("sCallMode")) = "WFVT" Then
		Response.Redirect("/VTimeNet/Common/Goto.aspx?" & Request.Params.Get("Query_String"))
	Else
		Response.Write("<SCRIPT>top.document.location.reload()</SCRIPT>")
	End If
ElseIf CStr(Session("SessionID")) = "" Then 
	%>
<FORM METHOD=POST ACTION="Login.aspx?Validate=1" STYLE="POSITION: absolute; TOP:100px LEFT:300">
    <BR>
    <TABLE>
        <TR>
            <TD COLSPAN=2 ALIGN=center><STRONG><LABEL ID=41479>Inicio de sesión</LABEL></STRONG></TD>
        </TR>
        <TR>
            <TD><LABEL ID=41480>Usuario</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctLogin", 20, IIf(IsNothing(Response.Cookies.Item("sUsuallyLog").Value), "", Request.Cookies.Item("sUsuallyLog").Value), True, "")%></TD>
        </TR>
        <TR>
           <TD><LABEL ID=41481>Clave</LABEL></TD>
           <TD><%	With Response
		.Write(mobjValues.PasswordControl("tctPasswd", 20, ""))
		.Write(mobjValues.AnimatedButtonControl("btnLogin", "/VTimeNet/images/btnUnlockOff.png", "Iniciar sesión",  , "insDoLogin()"))
	End With
	%>
           </TD>
        </TR>
        <TR>
        </TR>
    </TABLE>
<%	If mstrMessages <> vbNullString Then%>
			<CENTER><LABEL ID=-1><%=mstrMessages%></LABEL></CENTER>
<%	End If%>
</FORM>
<%End If%>
</BODY>
</HTML>




