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
    Const clngAccessDenied As Short = 7
    Const clngUserSchemaLock As Short = 8
    Const clngSystemExpired As Short = 9

    Dim mobjTabGen As eGeneralForm.TabGen
    Dim mstrMultiCom As String
    Dim mblnValidUser As Boolean
    Dim mclsMultiCompany As eSecurity.MultiCompany
    Dim mlngCompany As Integer
    Dim marrInfoCompany() As Object
    Dim mblnNextComp As Boolean

    'Dim mstrPasswd As String
    'Dim mstrMultiCompany As String

    Dim mstrJustQuote As String
    Dim mstrQuoteType As String

    Dim mobjUserValidate As eSecurity.UserValidate
    'Dim mstrErrors As Object
    Dim mstrCommand As String
    'Dim lobjUserValidate2 As eSecurity.UserValidate
    
    
'%insValidateUser. Esta función realiza la validación del usuario al sistema.
'---------------------------------------------------------------------------------------------
    Private Function insValidateUser(ByVal sInitials As String) As Boolean
        '---------------------------------------------------------------------------------------------
        Dim lobjUserValidate As eSecurity.UserValidate
        Dim lobjUser As Object
        Dim lobjSchema As Object
        Dim lobjGeneral As Object
        Dim lintAccessOf As Object
        Dim lintIndCurrency As Object
        Dim lintLimits As Object
        Dim lintLevels As Object
        Dim sPasswd As String
        '((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
        Dim lobjOptionsInstallation As eGeneral.OptionsInstallation
        Dim lstrInitialsOld As Object
        Dim lstrAccessWoOld As Object
        Dim lstrInitialsConOld As Object
        Dim lstrAccessWoConOld As Object
        Dim llngMultiCompanyOld As Object

        '+Se realiza la busqueda del usuario por medio de la iniciales 
        lobjUserValidate = New eSecurity.UserValidate
        With lobjUserValidate
            '+ Si es solicitado un cambio de passWord desde la aplicación; es decir, no es la primera vez
		
            If mstrMultiCom = "1" Or mstrMultiCom.ToUpper() = "YES" Then
                mlngCompany = 1
                marrInfoCompany = mclsMultiCompany.GetUserInfo(mlngCompany)
                Session("nMultiCompany") = mlngCompany
                Session("sDesMultiCompany") = marrInfoCompany(0)
                Session("sInitials") = sInitials
                Session("sAccesswo") = "" 'lobjUserValidate.StrEncode(sPasswd)
                Session("sInitialsCon") = marrInfoCompany(1)
                Session("sAccesswoCon") = marrInfoCompany(2)
            Else
                Session("sInitials") = sInitials
                Session("sAccesswo") = "" 'lobjUserValidate.StrEncode(sPasswd)
                Session("sInitialsCon") = sInitials
                Session("sAccesswoCon") = Session("sAccesswo")
            End If
	
            '((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
        
        
            insValidateUser = False
            If Request.QueryString.Item("sCallMode") = "WFVT" Then
                sInitials = Request.QueryString.Item("tctLogin")
                sPasswd = ""
            End If
        
            '+Se realiza la busqueda del usuario por medio de la iniciales 
            If .ValidateUser(sInitials, "", , True) Then
                insValidateUser = True
                Session("nUsercode") = .objUser.nUsercode
                Session("sSche_code") = .objUser.sSche_code
                Session("nOffice") = .objUser.nOffice
                Session("sAccesswo") = .objUser.sAccesswo
                Session("sPolicyNum") = .objOptSystem.sPolicyNum
                Session("sReceiptNum") = .objOptSystem.sReceiptNum
                Session("sUserClient") = .objUser.sClient
                Session("nOfficeAgen") = .objUser.nOfficeagen
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
<%Response.Expires = -1441
	Response.Buffer = True
    mobjValues = New eFunctions.Values
    mstrCommand = "&sModule=Client&sProject=Client&sCodisplReload=" & Request.QueryString.Item("sCodispl")

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
    <script src="/VTimeNet/Scripts/GenFunctions.js" type="text/javascript"></script>
</HEAD>
<BODY>
<%
    
    If Request.QueryString.Item("Validate") = "1" Then
        If insValidateUser(Request.QueryString.Item("ini")) Then
            Session("SessionID") = Session.SessionID
        End If
    End If
    
    If CStr(Session("SessionID")) <> "" And Request.QueryString.Item("Validate") = "1" Then
        Dim ef As New eFunctions.Values()
        Session("sCallMode") = Request.QueryString.Item("sCallMode")
        Session("WFAddress") = Request.QueryString.Item("URL")
        Session("sFrame") = Request.QueryString.Item("sFrame")
        If CStr(Session("sCallMode")) = "WFVT" Then
            Dim qs As String
            Session("sCertype") = "2"
            Session("nBranch") = Request.QueryString("br")
            Session("nPolicy") = Request.QueryString("pn")
            Session("nPropoNum") = Request.QueryString("pp")
            Session("nProduct") = Request.QueryString("pr")
            Session("dEffecdate") = ef.StringToDate(Request.QueryString("nr"))

            qs = "&nBranch=" & Request.QueryString("br") & "&nProduct=" & Request.QueryString("pr") & "&nPolicy=" & Request.QueryString("pn") & "&nCertif=" & Request.QueryString("cn") & "&nPropoNum=" & Request.QueryString("pp") & "&dEffecDate=" & ef.TypeToString(Session("dEffecdate"), Values.eTypeData.etdDate) & "&nOperat=" & Request.QueryString("nOperat") & "&sCodisplOri=BUC"
            Response.Redirect("/VTimeNet/Common/Goto.aspx?sCodispl=" & Request.QueryString("sCodispl") & qs)
        ElseIf CStr(Session("sCallMode")) = "PRINTPOL" Then
            Dim qs As String
            
            qs = "nTypeReport=" & Request.QueryString("tr") & _
             "&sCertype=" & Request.QueryString("ct") & _
             "&nBranch=" & Request.QueryString("br") & _
             "&nProduct=" & Request.QueryString("pr") & _
             "&nPolicy=" & Request.QueryString("pp") & _
             "&nProponum=0" & _
             "&nMovement=" & Request.QueryString("nm") & _
             "&dEffecdate=" & ef.TypeToString(Request.QueryString("ed"), Values.eTypeData.etdDate) & _
             "&sImpression=" & "False" & _
             "&nTypeOption=" & "1" & _
             "&nCertif=" & Request.QueryString("cn") & _
             "&sReport=" & _
             "&nType_hist=" & Request.QueryString("th") & _
             "&nTratypep=" & Request.QueryString("tp")
            Response.Redirect("/VTimeNet/policy/policyrep/resvalpolicyrep.aspx?" & qs)
        ElseIf CStr(Session("sCallMode")) = "PRINTCLAIM" Then
            Dim mobjDocuments As New eReports.Report
            Dim lclsPolicyHist As ePolicy.Policy_his
            lclsPolicyHist = New ePolicy.Policy_his
            If lclsPolicyHist.insCreaPolicy_his_v2("2", Request.QueryString("br"), Request.QueryString("pr"), Request.QueryString("pn"), ef.StringToDate(Request.QueryString("nr")), eRemoteDB.Constants.intNull, Session("nUsercode"), 0, 4) = True Then '*Genera registro en policy_his 
                
                With mobjDocuments
                    .sCodispl = "SIL762"
                    .ReportFilename = "SIL762_V.rpt" 'listo		
                    .nReport = 4
                    .setStorProcParam(1, Request.QueryString("cl"))
                    .setStorProcParam(2, Request.QueryString("br"))
                    .setStorProcParam(3, Request.QueryString("pr"))
                    .setStorProcParam(4, Request.QueryString("pn"))
                    .setStorProcParam(5, Request.QueryString("cn"))
                    .setStorProcParam(6, 1)
                    .setStorProcParam(7, 1)
                    .setStorProcParam(8, "1")
                    .setStorProcParam(9, vbNullString)
                    .setStorProcParam(10, vbNullString)
                    .nMovement = lclsPolicyHist.nMovement
                    .Merge = False
                    .nGenPolicy = 1
                    .nForzaRep = 1
                    .nTratypep = 1
                    .MergeCertype = "2"
                    .MergeBranch = Request.QueryString("br")
                    .MergeProduct = Request.QueryString("pr")
                    .MergePolicy = Request.QueryString("pn")
                    .MergeCertif = Request.QueryString("cn")
                    .sPolitype = ""
                
                    Response.Write(.Command)
                End With
            End If
        ElseIf CStr(Session("sCallMode")) = "PRINTANULPOL" Then
            Dim mobjDocuments As New eReports.Report
            Dim lclsPolicy As New ePolicy.Policy
			With mobjDocuments
				.ReportFilename = "CAL033.rpt"
				.sCodispl = "CAL033"
				.setStorProcParam(1, mobjValues.StringToType(Request.QueryString("br"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(2, mobjValues.StringToType(Request.QueryString("pr"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(3, mobjValues.StringToType(Request.QueryString("pn"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(4, mobjValues.StringToType(Request.QueryString("cn"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(5, .setdate(Request.QueryString("ed")))
				.setStorProcParam(6, "2")
				.setStorProcParam(7, "1")
				.setStorProcParam(8, 0)
				.setStorProcParam(9, 0)
				.setStorProcParam(10, "")
				.setStorProcParam(11, mobjValues.StringToType(Request.QueryString("pp"), eFunctions.Values.eTypeData.etdDouble))
                lclsPolicy.Find("2", Request.QueryString("br"), Request.QueryString("pr"), Request.QueryString("pn"), True)
                .Merge = False
                .nGenPolicy = 1
                .nMovement = lclsPolicy.nMov_histor
                .nForzaRep = 1
                .nTratypep = 2
                .nCopyPolicy = 1
                .MergeCertype = "2"
                .MergeBranch = Request.QueryString("br")
                .MergeProduct = Request.QueryString("pr")
                .MergePolicy = Request.QueryString("pn")
                .MergeCertif = Request.QueryString("cn")
                lclsPolicy = Nothing
				Response.Write((.Command))
			End With
        End If
    End If%>
</BODY>
</HTML>




