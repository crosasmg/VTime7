<%@ Page Language="VB" %>
<%@ Import namespace="eSecurity" %>
<%@ Import namespace="ePerformance" %>
<script language="VB" runat="Server">
Dim insValidateUser As Boolean

Dim lobjUserValidate As eSecurity.UserValidate




Private Sub insCreateXML()
	
	Dim lclsIntruction As ePerformance.Instruction
	lclsIntruction = New ePerformance.Instruction
	
	Dim lclsIntructions As ePerformance.Instructions
	lclsIntructions = New ePerformance.Instructions
	
	Response.Write("<?xml version='1.0'?><StoredList>")
	
	If lclsIntructions.insLoadScript("c:\Components\ePerformance\Scripts\Emisión de póliza para Incendio (PolicySeq).tsc") Then
		For	Each lclsIntruction In lclsIntructions
			Response.Write("<StoredName>")
			Response.Write("<Name>" & lclsIntruction.sCommand & "</Name>")
			Response.Write("<Time>" & CStr(lclsIntruction.nLast) & "</Time>")
			Response.Write("<Error>" & lclsIntruction.blnError & "</Error>")
			Response.Write("<ErrorNum>" & lclsIntruction.nError & "</ErrorNum>")
			Response.Write("<ErrorDescript>" & lclsIntruction.sDescription & "</ErrorDescript>")
			Response.Write("<Delay>" & lclsIntruction.nDelay & "</Delay>")
			Response.Write("</StoredName>")
		Next lclsIntruction
	End If
	Response.Write("<Total>" & lclsIntructions.Count & "</Total>")
	Response.Write("</StoredList>")
	
	lclsIntruction = Nothing
	lclsIntructions = Nothing
	
End Sub

</script>
<%
Response.CacheControl = False
Response.Expires = 0
lobjUserValidate = New eSecurity.UserValidate

'+Se realiza la busqueda del usuario por medio de la iniciales 
With lobjUserValidate
	If .ValidateUser("insudb", "insudb") Then
		insValidateUser = True
		Session("nUsercode") = .objUser.nUsercode
		Session("sSche_code") = .objUser.sSche_code
		Session("nOffice") = .objUser.nOffice
		Session("sAccesswo") = .objUser.sAccesswo
		
		Call insCreateXML()
	End If
End With

lobjUserValidate = Nothing

%>





