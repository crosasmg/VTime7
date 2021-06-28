<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjMenu = New eFunctions.Menues
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "MIN001"
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
    <%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/tmenu.js"></SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>



<SCRIPT>

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
  return true;
}

//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    var lintIndex = 0;
    for (lintIndex=0;lintIndex<document.forms[0].length;lintIndex++)
        document.forms[0].elements[lintIndex].disabled = false
    document.forms[0].btnnActivity.disabled = false;
}

//% insCancel: Ejecuta rutinas necesarias en el momento de finalizar la página
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}
</SCRIPT>

<%
With Response
	.Write(mobjValues.WindowsTitle("MIN001"))
	.Write(mobjMenu.MakeMenu("MIN001", "MIN001_k.aspx", 1, ""))
	.Write("<BR>")
End With
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ACTION="valMantFire.aspx?time=1" id=form1 name=form1>
  <TABLE WIDTH=100%>
    <TR>
      <TD WIDTH=10%><LABEL ID=111><%= GetLocalResourceObject("nActivityCaption") %></LABEL></TD>
      <TD><%=mobjValues.PossiblesValues("nActivity", "table118", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("nActivityToolTip"))%></TD>
	</TR>
  </TABLE>
<%
mobjValues = Nothing%>
</FORM>
</BODY>
</HTML>





