<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las funciones de menu

Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "MCO505"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>    


    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
<SCRIPT>

//% insCancel: Controla la acción cancelar de la página.
//------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------
	return (true);
}

//% insStateZone: Permite habilitar o inhabilitar controles de la página.
//------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------
    var lintIndex = 0;
    
    for (lintIndex=0; lintIndex < document.forms[0].length; lintIndex++)
    {
        document.forms[0].elements[lintIndex].disabled = false
			       
    for (lintIndex=0; lintIndex < document.images.length; lintIndex++)
    {
         if (document.images[lintIndex].belongtoolbar!=true)
             document.images[lintIndex].disabled = false
     }            
  }
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MCO505_K.aspx", 1, ""))
	mobjMenu = Nothing
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
	<FORM METHOD="POST" ID="FORM" NAME="MCO505" ACTION="valMantCollection.aspx?mode=1">
	<BR><BR>
		<TABLE WIDTH="100%">
			<TR>
				<TD WIDTH=10%><LABEL ID=9689><%= GetLocalResourceObject("tctClientCaption") %></LABEL></TD>
				<TD><%=mobjValues.ClientControl("tctClient", vbNullString,  , GetLocalResourceObject("tctClientToolTip"),  , False, "lblCliename",  ,  ,  ,  ,  ,  , True)%></TD>
				<TD><%=mobjValues.TextControl("lblCliename", 30, "",  ,  , True,  ,  ,  , True)%></TD>
			<TR>
		</TABLE>
	</FORM>
</BODY>
</HTML>





