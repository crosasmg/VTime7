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

mobjValues.sCodisplPage = "MIN003"
%>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


<SCRIPT>

//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone()
//------------------------------------------------------------------------------------------
{
	with (self.document.forms[0])
	{ 
	cbeDate.disabled = false
	btn_cbeDate.disabled = false
	}
}
//% insCancel: Se ejecuta al cancelar la transacción
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MIN003_K.aspx", 1, ""))
	mobjMenu = Nothing
End With
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmMantTasInc" ACTION=valmantfire.aspx?mode=1">
    <BR><BR>
    <TABLE>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeDateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("cbeDate", CStr(Today()), True, GetLocalResourceObject("cbeDateToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
    </TABLE>
<%
mobjValues = Nothing%>
</FORM>
</BODY>
</HTML>





