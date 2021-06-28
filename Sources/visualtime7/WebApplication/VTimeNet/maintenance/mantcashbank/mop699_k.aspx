<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MOP699"
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT> 
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $|$$Author: Iusr_llanquihue $"

//% insStateZone: Habilita/inhabilita los campos según la acción
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
    self.document.forms[0].cbeCompany.disabled = false
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}

//%insFinish: Controla la acción "Finalizar" de la página.
//-----------------------------------------------------------------------------
function insFinish(){
//-----------------------------------------------------------------------------
   return true
}
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("MOP699", "MOP699_k.aspx", 1, ""))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MOP699" ACTION="ValMantCashBank.aspx?sMode=1">
<BR><BR>
  <TABLE WIDTH="100%">
    <TR>
      <TD WIDTH="20%"><TD>
      <TD WIDTH="20%"><LABEL ID=0><%= GetLocalResourceObject("cbeCompanyCaption") %></LABEL></TD>
      <TD WIDTH="50%"><%=mobjValues.PossiblesValues("cbeCompany", "Company", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True, 30, GetLocalResourceObject("cbeCompanyToolTip"), 2)%></TD>
    </TR>
  </TABLE>
</FORM>
</BODY>
</HTML>






