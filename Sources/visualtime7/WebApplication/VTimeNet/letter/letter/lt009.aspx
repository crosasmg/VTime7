<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


Private Sub insPreLT009()
	Dim lclsProduct As eProduct.Product
	
	lclsProduct = New eProduct.Product
	
	If lclsProduct.FindProdMaster(session("nBranch"), session("nProduct")) Then
		Response.Write("<SCRIPT> document.forms[0].tctDescript.value = '" & lclsProduct.sDescript & "' </" & "Script>")
	End If
	
	lclsProduct = Nothing
End Sub

</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
%>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.setZone(2, "LT009", "LT009.aspx"))
End With
mobjMenu = Nothing%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="LT009">
	<%Response.Write(mobjValues.ShowWindowsName("LT009") & "<BR>")%>
	<BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="15%"><LABEL ID=7275>Descripción</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctDescript", 30, vbNullString,  ,vbNullString,  ,  ,  ,  , True)%></TD>
        </TR>
	</TABLE>
	<%
insPreLT009()
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>








