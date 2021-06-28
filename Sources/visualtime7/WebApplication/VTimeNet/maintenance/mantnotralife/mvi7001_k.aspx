<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores.
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página.
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MVI7001"
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 12/09/03 12:12 $|$$Author: Nvaplat37 $"
	
//% insStateZone: Esta función actualiza el estado (habilita o disabilita) de los campos de la zona 
//% en proceso.
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
	self.document.forms[0].cbeBranch.disabled     = false
	self.document.forms[0].valProduct.disabled    = false
	self.document.forms[0].btnvalProduct.disabled = false
}

//% insCancel: Esta función ejecuta la acción Cancelar de la página.
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
	return true
}
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet() & vbCrLf)
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("MVI7001", "MVI7001_K.aspx", 1, ""))
mobjMenu = Nothing
%>    

</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MVI7001" ACTION="valMantNoTraLife.aspx?sTime=1">
    <BR>
    <TABLE WIDTH="100%" CELLPADDING=6>
        <TR>
			<TD WIDTH="15%">&nbsp;</TD>
            <TD WIDTH="15%">
                <LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL>
            </TD>
			<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), vbNullString, "valProduct",  ,  ,  ,  , True)%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), "", eFunctions.Values.eValuesType.clngWindowType, True, "")%></TD>
            <TD WIDTH="10%">&nbsp;</TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>





