<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.47.59
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("col723_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "col723_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>

  <SCRIPT>

//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $|$$Author: Iusr_llanquihue $"
</SCRIPT>    
<SCRIPT>
//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% InsStateZone: se controla el estado de los controles de la página
//--------------------------------------------------------------------------------------------
function InsStateZone(){
//--------------------------------------------------------------------------------------------
}

</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("COL723"))
	.Write(mobjMenu.MakeMenu("COL723", "COL723_K.aspx", 1, vbNullString))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="COL723_k" ACTION="valCollectionRep.aspx?mode=2">
	<BR><BR>
		<%Response.Write(mobjValues.ShowWindowsName("COL723"))%>
	<BR><BR>
        <TABLE WIDTH="100%">
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdInitDateCaption") %> </LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdInitDate", CStr(Today),  , GetLocalResourceObject("tcdInitDateToolTip"),  ,  ,  ,  , True)%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEndDateCaption") %> </LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdEndDate", CStr(Today),  , GetLocalResourceObject("tcdEndDateToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType,  , False,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeBranchToolTip"))%> </TD>
        </TR>
        <TR>
			<TD><%=mobjValues.CheckControl("chkReuse", GetLocalResourceObject("chkReuseCaption"), CStr(2), CStr(2),  , True)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeIntentionCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeIntention", "Table5641", eFunctions.Values.eValuesType.clngComboType,  , False,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeIntentionToolTip"))%> </TD>
        </TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>
<%mobjValues = Nothing%>
<SCRIPT>
//% insStateZone: se controla el estado de los campos de la página
//%----------------------------------------------------------------------------------------
function insStateZone(){
//%----------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		tcdInitDate.disabled=false;
		btn_tcdInitDate.disabled=false;
		tcdEndDate.disabled=false;
		btn_tcdEndDate.disabled=false;
		cbeBranch.disabled=false;
		chkReuse.disabled=false;
	}
}
</SCRIPT>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.47.59
Call mobjNetFrameWork.FinishPage("col723_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




