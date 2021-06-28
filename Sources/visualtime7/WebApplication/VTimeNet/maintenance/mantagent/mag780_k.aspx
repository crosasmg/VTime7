<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las funciones de menu
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAG780"
%>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">





<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $|$$Author: Iusr_llanquihue $"    

//% insStateZone: Activa los campos del encabezado de la ventana
//------------------------------------------------------------------------------------------
function insStateZone()
//------------------------------------------------------------------------------------------
{
	self.document.forms[0].cbeInterType.disabled = false
}

//% insCancel: Se activa al cancelar la transacción
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: Se activa al finalizar la transacción
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MAG780_K.aspx", 1, ""))
	mobjMenu = Nothing
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>
<FORM METHOD="post" ID="FORM" NAME="MAG780_k" ACTION="valMantAgent.aspx?mode=1">
    <TABLE WIDTH="100%">
        </TR>
            <TD><LABEL ID=11728><%= GetLocalResourceObject("cbeInterTypeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeInterType", "TabInter_typ", 1, "",  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeInterTypeToolTip"))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing
%>




