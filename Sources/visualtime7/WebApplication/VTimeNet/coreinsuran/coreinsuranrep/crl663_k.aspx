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
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "crl663_k"
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

//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 4/11/03 18:25 $"        

</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("CRL663", "CRL663_k.aspx", 1, ""))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="CRL663" ACTION="valCoReinsuranRep.aspx?sMode=1">
<BR></BR>
	<BR>
		<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>    
	</BR>

	<TABLE WIDTH="100%">
		<TR><TD COLSPAN="8"><BR></TD></TR>
		<TR>
			<TD WIDTH="15%"><LABEL ID=0><%= GetLocalResourceObject("tcnMonthCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.NumericControl("tcnMonth", 2, CStr(Month(Today)), True, GetLocalResourceObject("tcnMonthToolTip"))%></TD>
		</TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnYearCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.NumericControl("tcnYear", 4, CStr(Year(Today)), True, GetLocalResourceObject("tcnYearToolTip"))%></TD>
		</TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdDateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdDate", CStr(Today), True, GetLocalResourceObject("tcdDateToolTip"))%></TD>
		</TR>
	</TABLE>

</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing%>





