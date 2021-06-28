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

mobjValues.sCodisplPage = "crl006_k"
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
Response.Write(mobjMenu.MakeMenu("CRL006", "CRL006_k.aspx", 1, ""))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="CRL006" ACTION="valCoReinsuranRep.aspx?sMode=1">
<BR></BR>
	<BR>
		<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>    
	</BR>
	<TABLE WIDTH="100%">
		<TR>
			<TD width="25%"><LABEL ID=0><%= GetLocalResourceObject("tcdDateFromCaption") %></LABEL></TD>
			<TD width="25%"><%=mobjValues.DateControl("tcdDateFrom", Session("dDateFrom"), False, GetLocalResourceObject("tcdDateFromToolTip"))%></TD>
			<TD width="25%"><LABEL ID=0><%= GetLocalResourceObject("cbeCompanyCaption") %></LABEL></TD>
			<TD width="25%"><%=mobjValues.PossiblesValues("cbeCompany", "company", 1, Session("nCompany"),  ,  ,  ,  ,  ,  ,  , 4, GetLocalResourceObject("cbeCompanyToolTip"), 1)%></TD>
		</TR>
		<TR>
			<TD width="25%"><LABEL ID=0><%= GetLocalResourceObject("tcdDateToCaption") %></LABEL></TD>
			<TD width="25%"><%=mobjValues.DateControl("tcdDateTo", Session("dDateTo"), False, GetLocalResourceObject("tcdDateToToolTip"))%></TD>
			<TD width="25%"><LABEL ID=0><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
			<TD width="25%"><%=mobjValues.PossiblesValues("cbeCurrency", "TABLE11", 1, Session("nCurrency"),  ,  ,  ,  ,  ,  ,  , 2, GetLocalResourceObject("cbeCurrencyToolTip"), 1)%></TD>
		</TR>
		<TR>
			<TD width="25%">&nbsp;</TD>
			<TD width="25%">&nbsp;</TD>
			<TD width="25%"><LABEL ID=0><%= GetLocalResourceObject("cbeCessTypeCaption") %></LABEL></TD>
      
			<TD width="25%">
				<%mobjValues.TypeList = 2
mobjValues.List = "0,3"
Response.Write(mobjValues.PossiblesValues("cbeCessType", "TABLE534", 1, Session("nCessType"),  ,  ,  ,  ,  ,  ,  , 2, GetLocalResourceObject("cbeCessTypeToolTip"), 1))
%>
			</TD>
		</TR>
		<TR>
			<TD width="25%">&nbsp;</TD>
			<TD width="25%">&nbsp;</TD>
			<TD width="25%"><LABEL ID=0><%= GetLocalResourceObject("cbeBranchReiCaption") %></LABEL></TD>
			<TD width="25%"><%=mobjValues.PossiblesValues("cbeBranchRei", "TABLE10", 1, Session("nBranchRei"),  ,  ,  ,  ,  ,  ,  , 4, GetLocalResourceObject("cbeBranchReiToolTip"), 1)%></TD>
		</TR>
	</TABLE>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing%>




