<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">

'- Variable de instancia de objeto eGeneralForm.Province
Dim mobjProvince As eGeneralForm.Province

'- Variable de instancia de objeto eGeneralForm.Tab_locat
Dim mobjTab_locat As eGeneralForm.Tab_locat


</script>
<HTML>
<HEAD>
<%
Response.Write("<SCRIPT>")
If Not IsNothing(Request.QueryString.Item("nZip_code")) And Not IsNothing(Request.QueryString.Item("nLocal")) Then
	mobjTab_locat = New eGeneralForm.Tab_locat
	If mobjTab_locat.Find(CShort(Request.QueryString.Item("nLocal"))) Then
		mobjProvince = New eGeneralForm.Province
		If mobjProvince.Find(mobjTab_locat.nProvince) Then
			Response.Write("opener.UpdValues(" & mobjProvince.nProvince & ",'" & LTrim(mobjProvince.sDescript) & "');")
		End If
		mobjProvince = Nothing
	End If
	mobjTab_locat = Nothing
End If
Response.Write("self.close();</SCRIPT>")
%>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
</HEAD>
<BODY>
<P>&nbsp;</P>
</BODY>
</HTML>





