<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'-   Objeto para el manejo de las funciones generales de carga de valores.
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1


mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "CAL970"

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>




<SCRIPT LANGUAGE=JavaScript>


//%   insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//%   insStateZone: Se controla el estado de los campos de la página.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
        
}


</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu("CAL970", "CAL970_K.aspx", 1, vbNullString))
mobjMenu = Nothing
%>
<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>
<FORM METHOD="post" ID="FORM" NAME="CAL970" ACTION="valPolicyRep.aspx?Mode=1">
	<TABLE WIDTH="100%">
        	<TR>
				<TD> <LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL> </TD>
				<TD> <%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  ,  ,  ,  ,  ,  , False, 1)%></TD>	
				<TD> <LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL> </TD>
			    <TD> <%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  , eFunctions.Values.eValuesType.clngWindowType, True,  ,  ,  ,  ,  , 2, True)%></TD>            			
			</TR>        
			<TR>
		  		<TD> <LABEL ID=0><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL> </TD>
				<TD> <%=mobjValues.NumericControl("tcnPolicy", 10, vbNullString,  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  ,  , False, 3)%></TD>
			</TR>
			<TR>
				<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , False, 5)%></TD>
			</TR>
			<TR>
			</TR>	
	</TABLE> 
	<BR>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>




