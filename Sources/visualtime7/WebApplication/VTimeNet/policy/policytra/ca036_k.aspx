<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones 
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $|$$Author: Iusr_llanquihue $"  

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}
//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return (true);
}
//% insFinish: se controla la acción Finalizar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}
</SCRIPT>    
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>


    <%mobjMenu = New eFunctions.Menues
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("CA036_K", "CA036_k.aspx", 1, ""))
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmInvoicingPolicy" ACTION="ValBillGroupPolSeq.aspx?sMode=1">
	<P>&nbsp;</P>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=13700><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), vbNullString, "valProduct")%></TD>
        </TR>
        <TR>            
            <TD><LABEL ID=13702><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), vbNullString, eFunctions.Values.eValuesType.clngWindowType)%></TD>
            <TD WIDTH="10%"><LABEL ID=13701><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD WIDTH="25%"><%=mobjValues.NumericControl("tcnPolicy", 10, vbNullString,  , GetLocalResourceObject("tcnPolicyToolTip"), False)%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjMenu = Nothing
%>




