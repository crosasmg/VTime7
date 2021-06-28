<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'**- Object for the handling of the general functions of load of values.  
'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values

'**- Object for the handling of the zones of the page.  
'- Objeto para el manejo de las zonas de la página.

Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "crl009_k"
%>

<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>



<SCRIPT> 
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
}
//-----------------------------------------------------------------------------
function insPreZone(llngAction){
//-----------------------------------------------------------------------------
}
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}
</SCRIPT>

<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("CRL009", "CRL009_k.aspx", 1, ""))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="CRL009" ACTION="valCoReinsuranRep.aspx?sMode=1">
<BR></BR>
	<BR>
		<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>    
	</BR>

	<TABLE WIDTH="100%" BORDER=0>
		<TR>
			<TD width="17%">&nbsp;</TD>
			<TD width="17%">&nbsp;</TD>
			<TD width="10%"><LABEL ID=0><%= GetLocalResourceObject("tcdDateToCaption") %></LABEL></TD>
			<TD width="17%"><%=mobjValues.DateControl("tcdDateTo", Session("dDateTo"), True, GetLocalResourceObject("tcdDateToToolTip"))%></TD>
			<TD width="17%">&nbsp;</TD>
			<TD width="17%">&nbsp;</TD>
		</TR>
	</TABLE>
	<TABLE>	
        <TR> <TD>&nbsp;</TD></TR>
        <TR>
			<TD width="38%">&nbsp;&nbsp;</TD>
			<TD><LABEL ID=LABEL1><%= GetLocalResourceObject("AnchorCaption") %> </LABEL>&nbsp;</TD>
            <TD> 
                <%Response.Write(mobjValues.OptionControl(0, "optEjecucion", GetLocalResourceObject("optEjecucion_2Caption"), "1", "2"))%>
            </TD>
        </TR>
        <TR>
			<TD width="17%">&nbsp;</TD>
			<TD width="17%">&nbsp;</TD>
            <TD> 
                <%Response.Write(mobjValues.OptionControl(0, "optEjecucion", GetLocalResourceObject("optEjecucion_1Caption"),  , "1"))%>
            </TD>
         </TR>   
	</TABLE>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing%>




