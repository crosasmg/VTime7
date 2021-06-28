<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'**- Object for the handling of the general functions of load of values.
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values

'**- Object for the handling of the generic routines.    
'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.sCodisplPage = "MVI012"

%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
    <HEAD>
        <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT>
//**+ For the Source Safe control "DO NOT REMOVE"
//+ Para Control de Versiones "NO REMOVER"
//------------------------------------------------------------------------------
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $"
//------------------------------------------------------------------------------
</SCRIPT>    
<SCRIPT>

//**% insCancel: This function executes the action to cancel of the page.
//% insCancel: Esta función ejecuta la acción Cancelar de la página.
//-------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------
	return true
}    

//**% insStateZone: This function enabled the fields of the form according to the action to execute.
//% insStateZone: Esta función habilita los campos de la forma según la acción a ejecutar.
//-------------------------------------------------------------------------------------------
    function insStateZone(){
//-------------------------------------------------------------------------------------------    
    switch (top.frames['fraSequence'].plngMainAction){
        case 301:
        case 302:
        case 401:
            self.document.forms[0].tcdEffecDate.disabled = false;
            self.document.forms[0].btn_tcdEffecDate.disabled = false;
    }
}
</SCRIPT>
	  <%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("MVI012", "MVI012_k.aspx", 1, ""))
End With

mobjMenu = Nothing%>
    </HEAD>
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="post" ID="FORM" NAME="frmFaceValues.aspx" ACTION="valMantNoTraLife.aspx?mode=1">
            <BR><BR>
            <TABLE>
                <TR>
                    <TD WIDTH=60%></TD>
                    <TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecDateCaption") %></LABEL></TD>
                    <TD><%=mobjValues.DateControl("tcdEffecDate", "",  , GetLocalResourceObject("tcdEffecDateToolTip"),  ,  ,  ,  , True)%></TD>
                </TR>
            </TABLE>
<%
mobjValues = Nothing%>
        </FORM>
    </BODY>
</HTML>





