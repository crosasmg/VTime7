﻿<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'**- Object for the managing of the general functions of load of values.
'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values

'**- Object for the managing of the zones of the page.
'- Objeto para el manejo de las zonas de la página.

Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
%>



<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT>

//**% insStateZone: Enabled the fiels of the form.
//% insStateZone: Habilita los campos de la forma.
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
    if (typeof(document.forms[0])!='undefined'){		
		document.forms[0].elements["cbeBranch"].disabled = false;
	}
}

//**% insCancel: This procedure to cancel the page.
//% insCancel: Este procedimiento permite cancelar la página.
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}

</SCRIPT>
<HTML>
<HEAD>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("vil8032", "vil8032_k.aspx", 1, vbNullString))
mobjMenu = Nothing
%>    
<SCRIPT>

//**+ For the Source Safe control "DO NOT REMOVE"
//+ Para Control de Versiones "NO REMOVER"

    document.VssVersion="$$Revision: 2 $|$$Date: 10-05-06 12:08 $" 
</SCRIPT>
     
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="VIL7012" ACTION="valPolicyRep.aspx?sMode=1">
    <BR><BR>
    <%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))%>
    
    <BR><BR>

    <TABLE WIDTH="30%">
        <tr>
            <td><LABEL ID=0><%= GetLocalResourceObject("tcdIniDateCaption") %> </LABEL></td>
            <td><%=mobjValues.DateControl("tcdIniDate",  ,  , GetLocalResourceObject("tcdIniDateToolTip"))%></td>
        </tr>
        <tr>
            <td><LABEL ID=LABEL1><%= GetLocalResourceObject("tcdEndDateCaption") %></LABEL></td>
            <td><%=mobjValues.DateControl("tcdEndDate",  ,  , GetLocalResourceObject("tcdEndDateToolTip"))%></td>        
		</tr>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>






