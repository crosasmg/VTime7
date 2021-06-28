<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAG002"
%>
<HTML>
<HEAD>



<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $"

//% insCancel: se ejecuta la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel()
//------------------------------------------------------------------------------------------
{
    return(true)
}

//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone()
//------------------------------------------------------------------------------------------
{
    with (self.document.forms[0])
    { 
        valComtabli.disabled = false
        tcdEffecdate.disabled = false
        btn_tcdEffecdate.disabled = false
        btnvalComtabli.disabled = false
    }
}
    </SCRIPT>

<%
With Response
	.Write(mobjValues.StyleSheet())
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MAG002_K.aspx", 1, ""))
	mobjMenu = Nothing
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR>
<BR>
<FORM METHOD="POST" ID="FORM" NAME="frmTabLifeComm" ACTION="valMantAgent.aspx?mode=1">
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=11751><%= GetLocalResourceObject("valComtabliCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valComtabli", "tabtab_comlif", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valComtabliToolTip"))%></TD>
            <TD><LABEL ID=11750><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today), True, GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing
%>





