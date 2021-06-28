<script language="VB" runat="Server">



</script>
<%
    Dim mobjValues As eFunctions.Values = New eFunctions.Values 
If Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyQuery Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifQuery Or Session("nTransaction") = eCollection.Premium.PolTransac.clngQuotationQuery Or Session("nTransaction") = eCollection.Premium.PolTransac.clngProposalQuery Then
	mobjValues.ActionQuery = True
End If
%>




