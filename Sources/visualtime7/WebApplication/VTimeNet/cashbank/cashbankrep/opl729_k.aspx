<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues

'- Variables para el manejo de la caja    
    

</script>
<%Response.Expires = 0
   mobjValues = New eFunctions.Values
    mobjValues.sCodisplPage = "opl729_k"

%>


<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT>
//%Variable para el control de Versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 16/10/03 16:47 $"
</SCRIPT>        
<SCRIPT LANGUAGE="JavaScript"> 
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------

}
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}
</SCRIPT>    
<%
mobjMenu = New eFunctions.Menues
Response.Write(mobjValues.StyleSheet())
    Response.Write(mobjValues.WindowsTitle("OPL729"))
    Response.Write(mobjMenu.MakeMenu("OPL729", "OPL729_k.aspx", 1, ""))
    Response.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), "OPL729_k.aspx"))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<SCRIPT>
//- Variable que almacena la fecha del sistema
	
	var mdtmDateSystem = GetDateSystem()
	
</SCRIPT>	
<SCRIPT>
//-------------------------------------------------------------------------------------------
function insInitialFields(){
	document.forms["OPL729"].elements["tcdProcDat"].value = mdtmDateSystem	
}	

//-------------------------------------------------------------------------------------------

</SCRIPT>  
<FORM METHOD="post" ID="FORM" NAME="OPL729" ACTION="valCashBankRep.aspx?Zone=1">
<BR><BR>
<TABLE WIDTH="100%">
    <TR>    
        <TD WIDTH="20%"><LABEL ID=0><%= GetLocalResourceObject("tcdProcDatCaption") %></LABEL></TD>
        <TD WIDTH="70%"><%=mobjValues.DateControl("tcdProcDat", Request.Form.Item("tcdProcDat"), True, GetLocalResourceObject("tcdProcDatToolTip"),  ,  ,  ,  ,  , 2)%></TD>
    </TR>
    
</TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
Response.Write("<SCRIPT>insInitialFields()</SCRIPT>")
%>    






