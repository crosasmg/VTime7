<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eRemoteDB" %>
<script language="VB" runat="Server">

Dim mclsQuery As eRemoteDB.Query


</script>
<%Response.Expires = 0
mclsQuery = New eRemoteDB.Query
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
</HEAD>
<BODY>
</BODY>
</HTML>
<SCRIPT>
//+ Se recarga en el frame 'fraFolder' de la página la misma página, pero como PopUp
	var mstrLocation = '"' + top.opener.location + '"'
	self.document.location = mstrLocation.substr(1, mstrLocation.indexOf('?',1) - 1) + <%="'?" & Request.Params.Get("Query_String") & "'"%>;
</SCRIPT>
<%With mclsQuery
	If .OpenQuery("Windows", "sDescript", "sCodispl ='" & Request.QueryString.Item("sCodispl") & "'") Then
		Response.Write("<SCRIPT>top.document.title='" & .FieldToClass("sDescript") & "'</SCRIPT>")
		.CloseQuery()
	End If
End With
mclsQuery = Nothing
%>





