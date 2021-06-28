<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values


'% insFind_Claim: Obtiene los datos del siniestro.
'--------------------------------------------------------------------------------------------
Private Sub insFind_Claim()
	'--------------------------------------------------------------------------------------------
	Dim lclsClaim As eClaim.Claim
	
	lclsClaim = New eClaim.Claim
	
	If lclsClaim.Find(mobjValues.StringToType(Request.QueryString.Item("nClaim"), eFunctions.Values.eTypeData.etdDouble), True) Then
		Response.Write("top.fraHeader.document.forms[0].cbeBranch.value = " & lclsClaim.nBranch & ";")
		Response.Write("top.fraHeader.document.forms[0].valProduct.value = " & lclsClaim.nProduct & ";")
		Response.Write("top.fraHeader.document.forms[0].tcnPolicy.value = " & lclsClaim.nPolicy & ";")
		Response.Write("top.fraHeader.document.forms[0].tcnCertif.value = " & lclsClaim.nCertif & ";")
	Else
		Response.Write("top.fraHeader.document.forms[0].cbeBranch.value = '';")
		Response.Write("top.fraHeader.document.forms[0].valProduct.value = '';")
		Response.Write("top.fraHeader.document.forms[0].tcnPolicy.value = '';")
		Response.Write("top.fraHeader.document.forms[0].tcnCertif.value = '';")
		Response.Write("top.fraHeader.document.forms[0].tcnClaim.value = '';")
	End If
	
	lclsClaim = Nothing
End Sub

'% insProvider: Obtiene el proveedor según la comuna seleccionada
'--------------------------------------------------------------------------------------------
Private Sub insProvider()
	'--------------------------------------------------------------------------------------------
	Dim lclsClaim As eClaim.Prof_ord
	Dim llngZone As Integer
	
	lclsClaim = New eClaim.Prof_ord
	
	llngZone = mobjValues.StringToType(Request.QueryString.Item("nZone"), eFunctions.Values.eTypeData.etdDouble)
	
	Dim lclsAddress As eGeneralForm.Address
	With lclsClaim
		'+ Si nos encontramos en la secuencia (OS001)
		If Request.QueryString.Item("sCodispl") = "OS001" Then
			'+ Si el campo comuna no tiene valor
			If llngZone <= 0 Then
				
				lclsAddress = New eGeneralForm.Address
				
				If lclsAddress.Find(Request.QueryString.Item("sKey"), 8, Today, True, False) Then
					llngZone = lclsAddress.nMunicipality
					If llngZone > 0 Then
						Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
						Response.Write(" cbeZone.value='" & llngZone & "';")
						Response.Write(" hddnMunicipality.value='" & llngZone & "';")
						Response.Write(" cbeZone.disabled=true;")
						Response.Write(" top.frames['fraFolder'].$('#cbeZone').change();")
						Response.Write(" btncbeZone.disabled=true;")
						Response.Write("}")
					End If
				End If
				lclsAddress = Nothing
			End If
		End If
		
		'+ Se obtiene el proveedor según rutina (de acuerdo a la comuna).
		If Request.QueryString.Item("sExecute") = "1" Then
			If .FindProviderOrder(llngZone) Then
				If .nProvider <> 0 Then
					Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
					Response.Write("    cbeProvider.value= " & mobjValues.TypeToString(.nProvider, eFunctions.Values.eTypeData.etdDouble) & ";")
					Response.Write("    cbeProvider.disabled=true;")
					Response.Write("    btncbeProvider.disabled=true;")
					Response.Write("    top.frames['fraFolder'].UpdateDiv('cbeProviderDesc','" & .sProviderName & "');")
					Response.Write("	 cbeStatus_ord.value= '2';")
					Response.Write("}")
				Else
					Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
					Response.Write("    cbeProvider.value='';")
					Response.Write("    cbeProvider.disabled=false;")
					Response.Write("    btncbeProvider.disabled=false;")
					Response.Write("    top.frames['fraFolder'].UpdateDiv('cbeProviderDesc','');")
					Response.Write("}")
				End If
			End If
		End If
	End With
	
	lclsClaim = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 18.00 $|$$Author: Nvaplat60 $"
</SCRIPT>
<%
Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "Find_Claim"
		Call insFind_Claim()
	Case "cbeZone"
		Call insProvider()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing

mobjValues = Nothing
%>
</HEAD>
<BODY>
<FORM NAME="ShowValues">
</FORM>
</BODY>
</HTML>




