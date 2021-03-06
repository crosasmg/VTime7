<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'**- The object to handling the general functions of load of values is defined.            
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%
Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "vic013_k"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>


    <%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("VIC013", "VIC013_k.aspx", 1, ""))
End With

mobjMenu = Nothing%>
<SCRIPT>

//**+ Source Safe control of version
//+ Para Control de Versiones de Source Safe

    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"

//**% insCancel: This function executes the action cancel of the page.
//% insCancel: Ejecuta la acción cancelar de la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}

//**% insStateZone: This function allows enabled/disabled the objects of the page.
//% insStateZone: Permite habilitar o inhabilitar los objetos.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    var lintIndex = 0;
    
    for (lintIndex=0; lintIndex < document.forms[0].length; lintIndex++)
         document.forms[0].elements[lintIndex].disabled = false
                   
    for (lintIndex=0; lintIndex < document.images.length; lintIndex++){
        if (document.images[lintIndex].belongtoolbar!=true)
         document.images[lintIndex].disabled = false
    }         
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="VIC013" ACTION="valBranchQue.aspx?Mode=1">
    <BR><BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="25%">&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdDate",  ,  , GetLocalResourceObject("tcdDateToolTip"),  ,  ,  ,  , True)%> </TD>
            <TD WIDTH="25%">&nbsp;</TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>





