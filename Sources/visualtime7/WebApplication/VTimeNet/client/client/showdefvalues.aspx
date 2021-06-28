<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
Dim mobjValues As eFunctions.Values


'% insShowReceiptClaim: Marcalas opciones recibos y sinientros pendientes
'------------------------------------------------------------------------------------------------
Private Sub insShowReceiptClaim()
	'------------------------------------------------------------------------------------------------
	Dim lclsPremium As eCollection.Premium
	Dim lclsClaim As eClaim.Claim
	Dim lobjClient As eClient.Client
	Dim sClient As String
	
	With Server
		lclsPremium = New eCollection.Premium
		lclsClaim = New eClaim.Claim
		lobjClient = New eClient.Client
	End With
	
	sClient = lobjClient.ExpandCode(UCase(Request.QueryString.Item("sClient")))
	If sClient <> vbNullString Then
		
		'+ Búsqueda de recibos del clientes
		If lclsPremium.FindClientReceipt("2", sClient) Then
			Response.Write("opener.document.forms[0].chkPremium.checked= true;")
		Else
			Response.Write("opener.document.forms[0].chkPremium.checked= false;")
		End If
		
		'+ Búsqueda de siniestros pendientes
		If lclsClaim.FindClientClaim(sClient) Then
			Response.Write("opener.document.forms[0].chkClaim.checked=true;")
		Else
			Response.Write("opener.document.forms[0].chkClaim.checked=false;")
		End If
	End If
	
	lclsClaim = Nothing
	lclsPremium = Nothing
	lobjClient = Nothing
End Sub

'% InsShowClientData: Muestra los datos del cliente
'--------------------------------------------------------------------------------------------
Sub InsShowClientData()
	'--------------------------------------------------------------------------------------------
	Dim lclsClient As eClient.Client
	Dim lblnChecked As String
	Dim lstrClient As String
	Dim lblnPerson_typ As String
	
	lclsClient = New eClient.Client
	With lclsClient
		lstrClient = .ExpandCode(Request.QueryString.Item("sClient"))
		
		Call .Find(lstrClient)
		
		If .nPerson_typ = 1 Then
			If .sSmoking = "1" Then
				lblnChecked = "true"
			Else
				lblnChecked = "false"
			End If
		Else
			lblnChecked = "false"
		End If
		
		If .nPerson_typ = 1 Then
			lblnPerson_typ = "false"
		Else
			lblnPerson_typ = "true"
		End If
		
		Response.Write("with (top.frames['fraFolder'].document.forms[0]){" & vbCrLf)
		Response.Write("    tcdBirthdate.value='" & mobjValues.TypeToString(.dBirthdat, eFunctions.Values.eTypeData.etdDate) & "';")
		Response.Write("    cbeSexclien.value='" & .sSexclien & "';")
		Response.Write("    chkSmoking.checked=" & lblnChecked & ";")
		
		If .nPerson_typ = 1 Then
			Response.Write("    cbeOccupat.sTabName='table16';")
		Else
			Response.Write("    cbeOccupat.sTabName='table417';")
		End If
		
		Response.Write("    cbeOccupat.value='" & mobjValues.TypeToString(.nSpeciality, eFunctions.Values.eTypeData.etdDouble) & "';")
		Response.Write("    top.frames['fraFolder'].$('#cbeOccupat').change();")
		
		If Request.QueryString.Item("sCodispl") <> "BC005" Then
			Response.Write("    btnPolicyValues.disabled=" & lblnPerson_typ & ";")
		End If
		Response.Write("    try{" & vbCrLf & "        btnQuery.disabled=" & lblnPerson_typ & ";" & vbCrLf & "    }catch(x){};" & vbCrLf)
		Response.Write("}")
	End With
	lclsClient = Nothing
End Sub

'% insValPolitype: valida el tipo de póliza para habilitar/deshabilitar el certificado
'% Debe ser invocada con funcion insDefValues
'--------------------------------------------------------------------------------------------
Sub insValPolitype()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim lstrFrame As String
	
	lclsPolicy = New ePolicy.Policy
	lstrFrame = Request.QueryString.Item("sFrame")
	If lstrFrame = vbNullString Then
		lstrFrame = "fraHeader"
	End If
	If lclsPolicy.Find("2", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
		
		'+ Asignación del Tipo de póliza
		Response.Write("with(top.frames['" & lstrFrame & "'].document.forms[0]){")
		Select Case lclsPolicy.sPolitype
			Case "1"
				Response.Write("tcnCertif.disabled=true;")
				Response.Write("tcnCertif.value=""0"";")
			Case "2"
				Response.Write("tcnCertif.disabled=false;")
				Response.Write("tcnCertif.value=""0"";")
				Response.Write("tcnCertif.focus();")
			Case "3"
				Response.Write("tcnCertif.disabled=false;")
				Response.Write("tcnCertif.value=""0"";")
				Response.Write("tcnCertif.focus();")
		End Select
		
		If Request.QueryString.Item("sExecCertif") = "1" Then
			Response.Write("if(tcnCertif.disabled)")
			Response.Write("top.frames['" & lstrFrame & "'].$('#tcnCertif').change();")
		End If
		Response.Write("}")
	Else
		Response.Write("top.frames['" & lstrFrame & "'].document.forms[0].tcnCertif.disabled=false;")
		Response.Write("top.frames['" & lstrFrame & "'].document.forms[0].tcnCertif.value="""";")
	End If
	lclsPolicy = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
Response.Write(mobjValues.StyleSheet)

%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
//+ Variable para el control de versiones
		document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15.57 $"
</SCRIPT>		
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM NAME="ShowValues">
</FORM>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "ReceiptClaim"
		Call insShowReceiptClaim()
	Case "Client"
		Call InsShowClientData()
	Case "insValsPolitype"
		Call insValPolitype()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing
%>




