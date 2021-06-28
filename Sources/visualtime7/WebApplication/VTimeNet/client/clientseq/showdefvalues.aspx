<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values


'%InsShowAccount_Data: Muestra los datos de la cuenta
'--------------------------------------------------------------------------------------------
Sub InsShowAccount_Data()
	'--------------------------------------------------------------------------------------------
	Dim lclsbk_account As eClient.bk_account
	Dim lclsvalues As eFunctions.Values
    Exit Sub
	
	With Server
		lclsvalues = New eFunctions.Values
		lclsbk_account = New eClient.bk_account
	End With
	
	If lclsbk_account.Find_Agency(Session("sClient"), lclsvalues.StringToType(Request.QueryString.Item("nBank_code"), eFunctions.Values.eTypeData.etdDouble, True), Request.QueryString.Item("sAccount")) Then
		
		'If lclsbk_account.nBk_agency = eRemoteDB.Constants.intNull Then
			Response.Write("opener.document.forms[0].cbeBk_agency.value='';")
			Response.Write("opener.UpdateDiv('cbeBk_agencyDesc','');")
		'Else
		'	Response.Write("opener.document.forms[0].cbeBk_agency.value='" & lclsvalues.StringToType(lclsbk_account.nBk_agency, eFunctions.Values.eTypeData.etdDouble) & "';")
		'	Response.Write("opener.$('#cbeBk_agency').change();")
		'End If
	End If
	lclsbk_account = Nothing
	lclsvalues = Nothing
End Sub

'%InsShowCard_Data: Muestra los datos de la tarjeta de crédito
'--------------------------------------------------------------------------------------------
Sub InsShowCard_Data()
	'--------------------------------------------------------------------------------------------
	Dim lclscred_card As eClient.cred_card
	Dim lclsvalues As eFunctions.Values
	
	With Server
		lclsvalues = New eFunctions.Values
		lclscred_card = New eClient.cred_card
	End With
	
	If lclscred_card.Find(Session("sClient"), lclsvalues.StringToType(Request.QueryString.Item("nBank_code"), eFunctions.Values.eTypeData.etdDouble, True), Request.QueryString.Item("sAccount")) Then
		Response.Write("opener.document.forms[0].cbeTyp_card.value='" & lclscred_card.nCard_Type & "';")
		Response.Write("opener.document.forms[0].tcdExpirDat.value='" & lclsvalues.TypeToString(lclscred_card.dCardexpir, eFunctions.Values.eTypeData.etdDate) & "';")
	End If
	lclscred_card = Nothing
	lclsvalues = Nothing
    End Sub
    
    

'% insUpdUserAmend: se actualiza el campo nUser_amend de Policy o Certificat, según sea el caso
'--------------------------------------------------------------------------------------------
    Sub Update_ClientPEP()
        '--------------------------------------------------------------------------------------------
        Dim lclsClient As eClient.Client
             
        
        lclsClient = New eClient.Client
        '+ Se actualiza el campo en la tabla Policy        
        With lclsClient
            .sClient = Session("sClient")
            .sPEP = 2
            .Update_ClientPEP()
        End With
        
	
        lclsClient = Nothing
      
    End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15.57 $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM NAME="ShowDefValues">
</FORM>
</BODY>
</HTML>
<%
    
    Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("sField")
	Case "Account"
		If Request.QueryString.Item("sType_debit") = "1" Then
			InsShowAccount_Data()
		Else
			InsShowCard_Data()
            End If
    End Select
    Select Case Request.QueryString.Item("Field")
        Case "Client"
            Update_ClientPEP()
    End Select

    Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
    Response.Write("</SCRIPT>")

    mobjValues = Nothing
%>




