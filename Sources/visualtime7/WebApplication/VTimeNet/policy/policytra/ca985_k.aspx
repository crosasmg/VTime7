<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues

</script>
<%
mobjValues = New eFunctions.Values
Response.Expires = -1
%>
<HTML>
<HEAD>
<SCRIPT>		
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $"
//%insCancel:
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//%insStateZone:
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
}
</SCRIPT>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
	<%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>


    <%
mobjMenu = New eFunctions.Menues
With Response
	.Write(mobjMenu.MakeMenu("CA985", "CA985_K.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmReahPolicy_K" ACTION="ValPolicyTra.aspx?Zone=1">
<BR></BR>
    <TABLE WIDTH="100%">
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%
                    Response.Write(mobjValues.HiddenControl("tctCertype", "2"))
                    Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip")))
                %>
			</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><% Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  , eFunctions.Values.eValuesType.clngWindowType, false, ))%>
			</TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valIntermedCaption") %></LABEL></TD>
			<TD><%Response.Write(mobjValues.PossiblesValues("valIntermed", "tabintermedia_o", eFunctions.Values.eValuesType.clngWindowType, , , , , , , , False, 10, GetLocalResourceObject("valIntermedToolTip")))%></TD>

            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdAssign_dateCaption") %></LABEL></TD>
			<TD><%Response.Write(mobjValues.DateControl("tcdAssign_date", ,  , GetLocalResourceObject("tcdAssign_dateToolTip"),  ,  ,  ,  , False))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%mobjValues = Nothing%>





