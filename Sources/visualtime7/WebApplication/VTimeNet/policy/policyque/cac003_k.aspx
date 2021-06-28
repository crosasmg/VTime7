<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></script>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("CAC003", "CAC003_k.aspx", 1, ""))
End With

mobjMenu = Nothing%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $$Author: Iusr_llanquihue $"
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    var lintIndex = 0;
    
    for (lintIndex=0; lintIndex < document.forms[0].length; lintIndex++)
         document.forms[0].elements[lintIndex].disabled = false
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCAC003" ACTION="valPolicyQue.aspx?mode=1">
<BR></BR>
    <TABLE WIDTH="100%">
		<TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=40632><A NAME="Tipo de información"><%= GetLocalResourceObject("AnchorTipo de informaciónCaption") %></A></LABEL></TD>
        </TR>
        <TR>
			<TD COLSPAN="2"><HR></TD>
        </TR>
        <TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(40633, "optPolicy", GetLocalResourceObject("optPolicy_CStr1Caption"), eFunctions.Values.vbChecked, CStr(1),  , True,  , GetLocalResourceObject("optPolicy_CStr1ToolTip"))%></TD>
            <TD WIDTH="10%">&nbsp;</TD>
            <TD><LABEL ID=12614><%= GetLocalResourceObject("cbeOfficeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeOfficeToolTip"))%></TD>
        </TR>
        <TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(40634, "optPolicy", GetLocalResourceObject("optPolicy_CStr2Caption"), eFunctions.Values.vbUnChecked, CStr(2),  , True,  , GetLocalResourceObject("optPolicy_CStr2ToolTip"))%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=12613><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeBranchToolTip"))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing%>




