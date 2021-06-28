<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'-   Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CPL779")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "CPL779"

mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>

<SCRIPT>

//%   insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-------------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------------
	return true;
}

//%   insStateZone: Se controla el estado de los campos de la página
//-------------------------------------------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------------------------------------------------
}


</SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


	<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "CPL779_K.aspx", 1, ""))
	.Write(mobjValues.WindowsTitle("CPL779"))
End With

mobjMenu = Nothing
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="CPL779" ACTION="valLedgerRep.aspx?Mode=1">
	<BR><BR>
	<BR><BR>

<%
Response.Write(mobjValues.ShowWindowsName("CPL779"))
%>

	<TABLE WIDTH="100%">
	    <BR><BR>
	    <TR>
            <TD WIDTH=25%></TD>
            <TD WIDTH=25%><LABEL><%= GetLocalResourceObject("tcdProc_dateCaption") %></LABEL></TD>
            <TD WIDTH=15%><%=mobjValues.DateControl("tcdProc_date", CStr(eRemoteDB.Constants.dtmNull), True, GetLocalResourceObject("tcdProc_dateToolTip"), False, "", "", "", False, 1)%></TD>	    
            <TD WIDTH=35%></TD>            
	    </TR>
	    <TR>
            <TD WIDTH=25%></TD>
            <TD WIDTH=25%><LABEL><%= GetLocalResourceObject("cbeConceptCaption") %></LABEL></TD>
            <%mobjValues.TypeList = CShort("1")
mobjValues.List = "7"
mobjValues.BlankPosition = False%>
            <TD WIDTH=15%><%=mobjValues.PossiblesValues("cbeConcept", "Table293", eFunctions.Values.eValuesType.clngComboType, "7",  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeConceptToolTip"))%></TD>	    
            <TD WIDTH=35%></TD>            
	    </TR>
	</TABLE>
</FORM>
</BODY>
</HTML>	
<%
mobjValues = Nothing
'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CPL779")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>  





