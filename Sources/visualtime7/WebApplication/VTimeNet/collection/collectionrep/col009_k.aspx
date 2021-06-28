<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.47.59
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("col009_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "col009_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
	<SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 3 $|$$Date: 28/09/04 16:43 $|$$Author: Jfrugero $"
    </SCRIPT>


<HTML>
<HEAD>
<SCRIPT>

//% insStateZone: se manejan los campos de la página
//-----------------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------------
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

</SCRIPT>
    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("COL009", Request.QueryString.Item("sWindowDescript")))
	.Write(mobjMenu.MakeMenu("COL009", "COL009_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmAutoAnnulment" ACTION="valCollectionRep.aspx?mode=1">
<BR></BR>
    <%Response.Write(mobjValues.ShowWindowsName("COL009"))%>
    <TABLE WIDTH="100%">
	    <TR><TD COLSPAN="5%">&nbsp;</TD></TR>
	    <TR>
	        <TD WIDTH="23%">&nbsp;</TD>
	        <TD WIDTH="25%">&nbsp;</TD>
	        <TD COLSPAN="2"><B><U><LABEL ID=9910><%= GetLocalResourceObject("AnchorCaption") %></LABEL></B></U></TD>
	    </TR>
	    <TR>
			<TD WIDTH="23%"><LABEL ID=12942><%= GetLocalResourceObject("tcdProcessDateCaption") %></LABEL></TD>
<TD WIDTH="25%"><% %>
<%=mobjValues.DateControl("tcdProcessDate", CStr(Today),  , GetLocalResourceObject("tcdProcessDateToolTip"))%></TD>
            <TD><%= mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_1Caption"), CStr(1), "1", , , , GetLocalResourceObject("optProcess_1ToolTip"))%></TD>
	    </TR>
	    <TR>
            <TD><LABEL ID=13372><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"))%> </TD>
			<TD><%= mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_2Caption"), , "2", , , , GetLocalResourceObject("optProcess_2ToolTip"))%></TD>
   	    <TR>
	    <TR>
            <TD><LABEL ID=13372><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"))%> </TD>
   	    <TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.47.59
Call mobjNetFrameWork.FinishPage("col009_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




