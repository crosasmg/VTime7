<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralForm" %>
<%@ Import namespace="eSecurity" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">
Const clngUserLoginFail As Short = 1
Const clngUserLoginEmpty As Short = 2
Const clngUserPasswdFail As Short = 3
Const clngUserValid As Short = 4
Const clngUserLock As Short = 5
Const clngUserSchemaFail As Short = 6
Const clngAccessDenied As Short = 7
Const clngUserSchemaLock As Short = 8
Const clngSystemExpired As Short = 9

Dim mobjValues As eFunctions.Values
Dim mobjTabGen As eGeneralForm.TabGen
Dim mstrMessages As String
Dim mstrMultiCom As String
Dim mblnValidUser As Boolean
Dim mclsMultiCompany As eSecurity.MultiCompany
Dim mlngCompany As Integer
Dim marrInfoCompany() As Object
Dim mblnNextComp As Boolean

Dim mstrPasswd As String
Dim mstrMultiCompany As String

Dim mstrJustQuote As String
Dim mstrQuoteType As String

Dim mobjUserValidate As eSecurity.UserValidate
Dim mstrErrors As Object

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String
Dim lobjUserValidate2 As eSecurity.UserValidate

    Dim lclsGeneral As New eGeneral.GeneralFunction

'%insValidateUser. Esta función realiza la validación del usuario al sistema.
'---------------------------------------------------------------------------------------------
Private Function insValidateUser(ByVal sInitials As String, ByVal sPasswd As String, ByVal nCompany As String) As Boolean
	'---------------------------------------------------------------------------------------------
	Dim lobjUserValidate As eSecurity.UserValidate
	Dim lobjUser As Object
	Dim lobjOptionsInstallation As eGeneral.OptionsInstallation
	Dim lstrInitialsOld As Object
	Dim lstrAccessWoOld As Object
	Dim lstrInitialsConOld As Object
	Dim lstrAccessWoConOld As Object
	Dim llngMultiCompanyOld As Object
	
	insValidateUser = False
	
	lobjUserValidate = New eSecurity.UserValidate
	
	If Request.QueryString.Item("sCallMode") = "WFVT" Then
		sInitials = Request.QueryString.Item("tctLogin")
		sPasswd = lobjUserValidate.StrDecode(Request.QueryString.Item("tctPasswd"))
	End If
	
	'+Se realiza la busqueda del usuario por medio de la iniciales 
	With lobjUserValidate
		'+ Si es solicitado un cambio de passWord desde la aplicación; es decir, no es la primera vez
		If Request.QueryString.Item("sChangeLogin") = "1" Then
			lstrInitialsOld = Session("sInitials")
			lstrAccessWoOld = Session("sAccesswo")
			lstrInitialsConOld = Session("sInitialsCon")
			lstrAccessWoConOld = Session("sAccesswoCon")
			llngMultiCompanyOld = Session("nMultiCompany")
		End If
		
            If mstrMultiCom = "1" Or mstrMultiCom.ToUpper() = "YES" Then
                mlngCompany = mobjValues.StringToType(Request.Form.Item("cboCompanies"), eFunctions.Values.eTypeData.etdDouble)
                marrInfoCompany = mclsMultiCompany.GetUserInfo(mlngCompany)
                Session("nMultiCompany") = mlngCompany
                Session("sDesMultiCompany") = marrInfoCompany(0)
                Session("sInitials") = sInitials
                Session("sAccesswo") = lobjUserValidate.StrEncode(sPasswd)
                Session("sInitialsCon") = marrInfoCompany(1)
                Session("sAccesswoCon") = marrInfoCompany(2)
            Else
                Session("sInitials") = sInitials
                Session("sAccesswo") = lobjUserValidate.StrEncode(sPasswd)
                Session("sInitialsCon") = sInitials
                Session("sAccesswoCon") = Session("sAccesswo")
            End If
		
		If .ValidateUser(sInitials, sPasswd, Request.QueryString.Item("sChangeLogin")) Then
			insValidateUser = True
			Session("nUsercode") = .objUser.nUsercode
			Session("sSche_code") = .objUser.sSche_code
			Session("nOffice") = .objUser.nOffice
			Session("sPolicyNum") = .objOptSystem.sPolicyNum
			Session("sReceiptNum") = .objOptSystem.sReceiptNum
			Session("sUserClient") = .objUser.sClient
			Session("nCashNum") = .objUser.nCashNum
			Session("nOfficeAgen") = .objUser.nOfficeAgen
			Session("nAgency") = .objUser.nAgency
                'Session("PasswordChangeSchema") = .sPasswordChangeSchema
                Session("sTypeUser") = .objUser.sType
			
			Session("nLedCompan") = .objOptSystem.nCompany
			Session("sTypeCompanyUser") = .objOptSystem.sTypeCompany
			Session("nCompanyUser") = .objOptSystem.nCompany
			Session("nInsur_area") = .objOptSystem.nInsur_Area
			Session("nNum_Fem_SDB") = .objOptSystem.nNum_Fem
			lobjOptionsInstallation = New eGeneral.OptionsInstallation
			Session("nNum_Fem_SA") = lobjOptionsInstallation.getfem("Version", vbNullString, "Fems")
			lobjOptionsInstallation = Nothing
		Else
			Session("sInitials") = lstrInitialsOld
			Session("sAccesswo") = lstrAccessWoOld
			Session("sInitialsCon") = lstrInitialsConOld
			Session("sAccesswoCon") = lstrAccessWoConOld
			Session("nMultiCompany") = llngMultiCompanyOld
			
            Select Case .UserStatus
                Case eSecurity.UserValidate.eUserStatus.clngUserLoginEmpty
                    mstrMessages = GetLocalResourceObject("UserLoginEmptyMessage") '12049
					
                Case eSecurity.UserValidate.eUserStatus.clngUserLoginFail
                    mstrMessages = GetLocalResourceObject("UserLoginFailMessage") '99145
					
                Case eSecurity.UserValidate.eUserStatus.clngUserPasswdFail
                    mstrMessages = GetLocalResourceObject("UserPasswdFailMessage") '1903
					
                Case eSecurity.UserValidate.eUserStatus.clngUserLock
                    mstrMessages = GetLocalResourceObject("UserLockMessage") '12097
					
                Case eSecurity.UserValidate.eUserStatus.clngUserSchemaFail
                    mstrMessages = GetLocalResourceObject("UserLockMessage") '12097
					
                Case eSecurity.UserValidate.eUserStatus.clngAccessDenied
                    mstrMessages = GetLocalResourceObject("AccessDeniedMessage") '99146
					
                Case eSecurity.UserValidate.eUserStatus.clngUserSchemaLock
                    mstrMessages = GetLocalResourceObject("UserSchemaLockMessage") '
                        
                Case (eSecurity.UserValidate.eUserStatus.clngSystemExpired)
                    mstrMessages = GetLocalResourceObject("SystemExpiredMessage") & lobjUserValidate.dSysExpired

                Case eSecurity.UserValidate.eUserStatus.clngSystemExpired
                    mstrMessages = GetLocalResourceObject("SystemExpiredMessage") & lobjUserValidate.dSysExpired

                Case eSecurity.UserValidate.eUserStatus.clngReachedMaxAttempts
                    mstrMessages = GetLocalResourceObject("ReachedMaxAttemptsMessage")

                Case Else
                    mstrMessages = GetLocalResourceObject("UserLoginFailMessage") '99145
					
            End Select		
        End If
	End With
	
	lobjUser = Nothing
	lobjUserValidate = Nothing
End Function

'%insBatchEnabled: Funcion temporal para condicionar modo de accion
'                  de procesos masivos 1-Nueva forma 2-Forma tradicional
'------------------------------------------------
Function insBatchEnabled() As String
	'------------------------------------------------
	Dim mclsSecur_sche As eSecurity.Secur_sche
	Dim lstrRet As String
	
	lstrRet = "2"
	
	'+Si tiene acceso al modulo de proceso masivos, se ocupa la nueva
	'+implementacion de procesos batch
	mclsSecur_sche = New eSecurity.Secur_sche
	If mclsSecur_sche.valTransAccess(Session("sSche_code"), "DMEBTC", "1") Then
		lstrRet = "1"
	End If
	mclsSecur_sche = Nothing
	
	insBatchEnabled = lstrRet
	
End Function

</script>
<%Response.Expires = -1441

mstrCommand = "&sModule=Client&sProject=Client&sCodisplReload=" & Request.QueryString.Item("sCodispl")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.09.35
    mobjValues.sSessionID = New Random().Next(100000000, 900000000)
    mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "login"
mobjTabGen = New eGeneralForm.TabGen
mclsMultiCompany = New eSecurity.MultiCompany
mblnNextComp = False
mstrMultiCom = mobjValues.insGetSetting("MultiCompany", "2", "Database")

'+ Se obtienen las variables que determinan si el sistema se está ejecutando
'+ en modo exclusivo "Cotizador"
mstrJustQuote = mobjValues.insGetSetting("JustQuote", "2", "Quoteizer")
mstrQuoteType = mobjValues.insGetSetting("QuoteType", "2", "Quoteizer")
Session("sJustQuote") = mstrJustQuote
Session("sQuoteType") = mstrQuoteType

mobjUserValidate = New eSecurity.UserValidate
%>
<HTML>
<HEAD>
    <TITLE>Visual Time</TITLE>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
    
	<%=mobjValues.StyleSheet()%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15-09-09 19:13 $|$$Author: Mpalleres $"


//% insDoLoginBatch: se ejecuta la acción de la página cuando es llamada desde batch
//-------------------------------------------------------------------------------------------
function insDoLoginBatch(){
//-------------------------------------------------------------------------------------------
    self.document.forms[0].tctLogin.value = '<%=Request.QueryString.Item("sLogin")%>'
    self.document.forms[0].tctPasswd.value = '<%=Request.QueryString.Item("sPasswd")%>'
    self.document.forms[0].cboCompanies.value = '<%=Request.QueryString.Item("nCompany")%>'
    self.document.forms[0].submit();
}

//% insDoLogin: se ejecuta la acción de la página
//-------------------------------------------------------------------------------------------
function insDoLogin(){
//-------------------------------------------------------------------------------------------
    self.document.forms[0].submit();
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
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


</HEAD>
<BODY>
<%  If mstrMultiCom = "1" Or mstrMultiCom.ToUpper() = "YES" Then
        If CStr(Session("sJustQuote")) = "1" Then
            If CStr(Session("sDesMultiCompany")) <> vbNullString Then
                Response.Write("<SCRIPT>top.document.title='" & GetGlobalResourceObject("BackOfficeResource", "QuotationTool") & "(" & Session("sDesMultiCompany") & ")'</SCRIPT>")
            Else
                Response.Write("<SCRIPT>top.document.title='" & GetGlobalResourceObject("BackOfficeResource", "QuotationTool") & "'</SCRIPT>")
            End If
        Else
            If CStr(Session("sDesMultiCompany")) <> vbNullString Then
                Response.Write("<SCRIPT>top.document.title='" & GetGlobalResourceObject("BackOfficeResource", "MainMenu") & "(" & Session("sDesMultiCompany") & ")'</SCRIPT>")
            End If
        End If
    Else
        If CStr(Session("sJustQuote")) = "1" Then
            Response.Write("<SCRIPT>top.document.title='" & GetGlobalResourceObject("BackOfficeResource", "QuotationTool") & "'</SCRIPT>")
        End If
    End If

If Request.QueryString.Item("Validate") = "1" Then
	mstrPasswd = ""
	If insValidateUser(Request.Form.Item("tctLogin"), Request.Form.Item("tctPasswd"), Request.Form.Item("cboCompanies")) Then
		mblnValidUser = True
        Session("SessionID") = mobjValues.sSessionID
		Response.Cookies.Item("sUsuallyLog").Value = Request.Form.Item("tctLogin")
            Response.Cookies.Item("sUsuallyLog").Expires = DateAdd(Microsoft.VisualBasic.DateInterval.Day, Today.ToOADate, System.DateTime.FromOADate(1))
		
            If mstrMultiCom = "1" Or mstrMultiCom.ToUpper() = "YES" Then
                Response.Cookies.Item("sUsuallyCompany").Value = Request.Form.Item("cboCompanies")
                Response.Cookies.Item("sUsuallyCompany").Expires = DateAdd(Microsoft.VisualBasic.DateInterval.Day, Today.ToOADate, System.DateTime.FromOADate(1))
            End If
	Else
		'+Se limpia la session
		'+Se agrego esto porque bajo ciertas condiciones, al ingresar un cliente invalido, 
		'+los siguientes intentos también daban error pese a ser datos correctos.
		'+Con esto se busco que se limpiaran las variables de session, 
		'+especialmente las relacionadas con la coneccion a la bd
		mblnValidUser = False
		If Request.QueryString.Item("sChangeLogin") = vbNullString Then
			Session.Abandon()
		Else
			If Request.QueryString.Item("nCompanyNumber") = vbNullString Then
				mblnNextComp = True
			Else
				mblnNextComp = False
			End If
		End If
	End If
Else
	If Request.QueryString.Item("sChangeLogin") = "1" Then
		mblnNextComp = True
	End If
End If

If mblnNextComp Then
	Response.Cookies.Item("sUsuallyLog").Value = Session("sInitials")
	'+ Si no se conoce el código de la compañía, busca la próxima
	If Request.QueryString.Item("nCompanyNumber") = vbNullString Then
		mstrMultiCompany = mobjTabGen.FindNextValue("Table5638", Session("nMultiCompany"), True)
	Else
		'+ Si se conecta a una compañía específica
		mstrMultiCompany = Request.QueryString.Item("nCompanyNumber")
	End If
	Session("nMultiCompany") = mstrMultiCompany
	Response.Cookies.Item("sUsuallyCompany").Value = mstrMultiCompany
	lobjUserValidate2 = New eSecurity.UserValidate
	mstrPasswd = lobjUserValidate2.StrDecode(Session("sAccesswo"))
	lobjUserValidate2 = Nothing
End If

If mblnValidUser And Request.QueryString.Item("Validate") = "1" Then
	
	Session("sCallMode") = Request.QueryString.Item("sCallMode")
	Session("WFAddress") = Request.QueryString.Item("URL")
	Session("sFrame") = Request.QueryString.Item("sFrame")
	
	'+ Variable que almacena el número de version del servidor de aplicaciones
	'		Session("nNum_Fem_SA") = mobjValues.GetNumFem_Configxml
	
    '+Variable para controlar ejecución de procesos batch
	'+Se mantendrá activa miestras se apruebe el nuevo 
	'+manejo de procesos masivos
	'+Permite condicionar entre modo antiguo y el nuevo
	'Session("BatchEnabled") = insBatchEnabled()
	' activa el modulo de proceso batch de forma que utiliza el campo online de cada interfaz
	' mastersheet
        Session("BatchEnabled") = "1"
        
        '+Se valida que la Unidad de Fomento (UF) este actualizada
        If Not lclsGeneral.ValCurrency(4, System.DateTime.Now) Then
            Response.Write("<SCRIPT>alert('Adv. " & lclsGeneral.insLoadMessage(2) & "')</SCRIPT>")
        End If
        lclsGeneral = Nothing
        
        
	
	'+Si se está ejecutando el Cotizador en modo "Stand Alone", se verifica la
	'+la expiracón de la versión existente
	If CStr(Session("sJustQuote")) = "1" And CStr(Session("sQuoteType")) = "1" Then
		If mobjUserValidate.insValSysEspired(Session("sSche_code")) Then
			Response.Write("<SCRIPT>alert(""" & GetLocalResourceObject("QuotationExpiredMessage") & "(" & GetGlobalResourceObject("BackOfficeResource", "Days") & ")"");</SCRIPT>")
		End If
	End If
	
	If CStr(Session("sCallMode")) = "WFVT" Then
		Response.Redirect("/VTimeNet/Common/Goto.aspx?" & Request.Params.Get("Query_String"))
    ElseIf Session("sSche_code") = Session("PasswordChangeSchema") Then
        Server.Transfer("/VTimeNet/Common/Goto.aspx?sPopUp=2&sCodispl=SG099")
        'Response.Write("<SCRIPT>top.document.location.href='/VTimeNet/Common/GoTo.aspx?sPopUp=2&sCodispl=SG099'</SCRIPT>")
        'Response.Write("<SCRIPT>alert(999);window.onload= function x(){if (window.confirm('yendo')){top.location.href='/VTimeNet/Common/GoTo.aspx?sPopUp=2&sCodispl=SG099';}};</SCRIPT>")
    Else
        If Request.QueryString.Item("sChangeLogin") = "1" Then
            Session("sChangeLogin_sCodispl") = Request.QueryString.Item("sCodispl")
            With Response
                .Write("<SCRIPT>")
                If Request.QueryString.Item("nCompanyNumber") = vbNullString Then
                    .Write("top.window.close();")
                    .Write("top.opener.top.document.location.reload();")
                Else
                    Session("sChangeLogin_Parameters") = Replace(Request.QueryString.Item("sChangeLogin_Parameters"), "|", "&")
                    .Write("top.opener.top.close();")
                    .Write("top.opener.top.opener.top.document.location.reload();")
                End If
                .Write("</SCRIPT>")
            End With
        Else
            Response.Write("<SCRIPT>top.document.location.reload()</SCRIPT>")
        End If
    End If
ElseIf (Not mblnValidUser And CStr(Session("SessionId")) = vbNullString) Or Request.QueryString.Item("sChangeLogin") = "1" Then 
%>
<FORM METHOD=POST ACTION="Login.aspx?Validate=1&sChangeLogin=<%=Request.QueryString.Item("sChangeLogin")%>&sCodispl=<%=Request.QueryString.Item("sCodispl")%>&nCompanyNumber=<%=Request.QueryString.Item("nCompanyNumber")%>&sChangeLogin_Parameters=<%=Request.QueryString.Item("sChangeLogin_Parameters")%>" STYLE="POSITION: absolute; TOP:100px LEFT:300" id=form1 name=form1>
<BR>
<TABLE>
    <TR>
        <TD COLSPAN=2 ALIGN=center><STRONG><LABEL ID=41479><%= GetLocalResourceObject("AnchorCaption") %></LABEL></STRONG></TD>
    </TR>
    <TR>
        <TD><LABEL ID=41480><%= GetLocalResourceObject("tctLoginCaption") %></LABEL></TD>
        <TD><%=mobjValues.TextControl("tctLogin", 20, IIf(IsNothing(Response.Cookies.Item("sUsuallyLog").Value), "", Request.Cookies.Item("sUsuallyLog").Value), True, "")%></TD>
    </TR>
    <TR>
       <TD><LABEL ID=41481><%= GetLocalResourceObject("tctPasswdCaption") %></LABEL></TD>
       <TD><%	With Response
		.Write(mobjValues.PasswordControl("tctPasswd", 20, mstrPasswd))
                   If mstrMultiCom = "2" Or mstrMultiCom.ToUpper() <> "YES" Then
                       .Write(mobjValues.AnimatedButtonControl("btnLogin", "/VTimeNet/images/btnUnlockOff.png", GetLocalResourceObject("btnLoginToolTip"), , "insDoLogin()"))
                   End If
               End With
	%> 
       </TD> 
    </TR> 
    <%	If mstrMultiCom = "1" Or mstrMultiCom.ToUpper() = "YES" Then%> 
    <TR> 
       <TD><LABEL ID=41481><%= GetLocalResourceObject("btnLoginCaption") %></LABEL></TD> 
       <TD> <%		
		mobjValues.BlankPosition = False
		If IIf(IsNothing(Response.Cookies.Item("sUsuallyCompany").Value), "", Request.Cookies.Item("sUsuallyCompany").Value) <> vbNullString Then
			mlngCompany = CInt(IIf(IsNothing(Response.Cookies.Item("sUsuallyCompany").Value), "", Request.Cookies.Item("sUsuallyCompany").Value))
		Else
			mlngCompany = 0
		End If
		
		Response.Write(mclsMultiCompany.ComboBoxCompanies(mlngCompany))
		Response.Write(mobjValues.AnimatedButtonControl("btnLogin", "/VTimeNet/images/btnUnlockOff.png", GetLocalResourceObject("btnLoginToolTip"),  , "insDoLogin()"))
		%>
		</TD>
    </TR>
    <%	End If%>
    <TR>
    </TR>
</TABLE>    
<%	If mstrMessages <> vbNullString Then%>
	<CENTER><LABEL ID=-1><%= mstrMessages %></LABEL></CENTER>
<%	End If%>
</FORM>
<%	
End If
mobjValues = Nothing
mobjTabGen = Nothing
mclsMultiCompany = Nothing
mobjUserValidate = Nothing
%>
</BODY>
</HTML>
<%
If mblnNextComp Then
	Response.Write("<SCRIPT>insDoLogin();</SCRIPT>")
End If

If Request.QueryString.Item("sCallMode") = "Batch" And CStr(Session("SessionId")) = vbNullString Then
	Response.Write("<SCRIPT>insDoLoginBatch();</SCRIPT>")
End If
%>




