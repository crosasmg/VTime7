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
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
	<SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $|$$Author: Iusr_llanquihue $"
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
	.Write(mobjValues.WindowsTitle("COL011"))
	.Write(mobjMenu.MakeMenu("COL011", "COL011_K.aspx", 1, ""))
End With
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmRAutoChargeBankCardType" ACTION="valCollectionRep.aspx?mode=1">
<BR></BR>
    <%Response.Write(mobjValues.ShowWindowsName("COL011"))%>
    <TABLE WIDTH="100%">
	    <TR>
	        <TD>&nbsp;</TD>
	    </TR>
		<TR>
			<TD WIDTH=120pcx><LABEL ID=13372><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></td>
				<TD><%=mobjValues.PossiblesValues("cbeBranch", "table10", eFunctions.Values.eValuesType.clngComboType, Session("nBranch"))%> </td>
			</TR>
	    <TR>
			<TD WIDTH=120pcx><LABEL ID=12942><%= GetLocalResourceObject("tcdinitDateCaption") %></LABEL></TD>
            <TD COLSPAN="2"><%=mobjValues.DateControl("tcdinitDate",  ,  , GetLocalResourceObject("tcdinitDateToolTip"))%></TD>
	    </TR>
		<TR>
			<TD WIDTH=120pcx><LABEL ID=12942><%= GetLocalResourceObject("tcdendDateCaption") %></LABEL></TD>
            <TD COLSPAN="2"><%=mobjValues.DateControl("tcdendDate",  ,  , GetLocalResourceObject("tcdendDateToolTip"))%></TD>

	    </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>




