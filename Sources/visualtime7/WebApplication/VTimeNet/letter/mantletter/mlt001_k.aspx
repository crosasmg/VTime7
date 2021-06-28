<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 09/05/2003 10:50:01 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'**- Object for the handling of the genera
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("MLT001_K")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:50:01 a.m.
mobjValues.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "MLT001_K"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<SCRIPT> 
//**% insStateZone: This function enabled the fields of the header.
//% insStateZone: Esta función habilita los campos del encabezado.
//------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------
    document.forms[0].valGroup.disabled=false
    document.btnvalGroup.disabled=false
}
//------------------------------------------------------------------------------
function insPreZone(llngAction){
//------------------------------------------------------------------------------
//   switch (llngAction){
//       case 302:
//           document.location.href = document.location.href.replace(/&sReAction=.*/,"") + "&sReAction=302"
//           break;
//      case 301:
//           document.location.href = document.location.href.replace(/&sReAction=.*/,"") + "&sReAction=301"
//          break;
//   }
}
//**% insCancel: This function executes the action to cancel of the page.
//% insCancel: Esta función ejecuta la acción Cancelar de la página.
//------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------
   return true
}
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:50:01 a.m.
mobjMenu.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility
'Response.Write mobjMenu.MakeMenu("MLT001","MLT001_k.aspx",1,"")
Response.Write(mobjMenu.MakeMenu("MLT001", "MLT001_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MLT001" ACTION="valMantLetter.aspx?sMode=1">
<BR><BR>
    <TABLE WIDTH="100%" BORDER="0">
        <TR>
            <TD WIDTH="40%">&nbsp;</TD>
            <TD WIDTH="5%"><LABEL ID=7352>Grupo</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valGroup", "TabGroupParams", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  ,  , True,  ,"Grupo de variables")%>
			</TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 09/05/2003 10:50:01 a.m.
Call mobjNetFrameWork.FinishPage("MLT001_K")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>








