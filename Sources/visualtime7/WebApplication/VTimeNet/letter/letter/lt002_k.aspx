<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Object for the management of the general functions of load of values
'- Objeto para el manejo de las funciones generales de carga de valores   
Dim mobjValues As eFunctions.Values

'- Object for the management of the zones of the page
'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("LT002_K")

mobjValues = New eFunctions.Values
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = "LT002_K"

%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<SCRIPT> 
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
    document.forms[0].valTransaction.disabled=false
    document.btnvalTransaction.disabled=false
}
//-----------------------------------------------------------------------------
function insPreZone(llngAction){
//-----------------------------------------------------------------------------
}
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
mobjMenu.sSessionID = Session.SessionID
Response.Write(mobjMenu.MakeMenu("LT002", "LT002_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="LT002" ACTION="valLetter.aspx?sMode=1">
<BR><BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH=45pcx><LABEL ID=7263>Transacción</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valTransaction", "tabWindows", eFunctions.Values.eValuesType.clngWindowType, vbNullString,  ,  ,  ,  ,  , "this.value = this.value.toUpperCase()", True, 8,"Transaction", 2)%>
			</TD>
			
			<TD><%=mobjValues.HiddenControl("tcnPolicy_Ext", 0)%></TD>
            <TD><%=mobjValues.HiddenControl("tcnCertificat_Ext", 0)%></TD>
            <TD><%=mobjValues.HiddenControl("tcnCertif_Ext", 0)%></TD>
        </TR>
    </TABLE>
<%mobjValues = Nothing%>
</FORM>
</BODY>
</HTML>
<%
Call mobjNetFrameWork.FinishPage("LT002_K")
mobjNetFrameWork = Nothing
%>








