<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%
Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "AG954"
%>


<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $|$$Author: Iusr_llanquihue $"

//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone()
//------------------------------------------------------------------------------------------
{
    self.document.forms[0].nContrat_Pay.disabled = false;
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
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
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "AG954_k.aspx", 1, ""))
	mobjMenu = Nothing
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmAG954_k" ACTION="ValAgent.aspx?mode=1">
<BR></BR>
<TABLE WIDTH="100%">
    </TR>
        <TD WIDTH=120pcx><LABEL ID=0><%= GetLocalResourceObject("nContrat_PayCaption") %></LABEL></TD>
        <TD><%=mobjValues.PossiblesValues("nContrat_Pay", "TabContrat_pay", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.intNull), False,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("nContrat_PayToolTip"),  ,  ,  , True)%></TD>
    </TR>
</TABLE>
</FORM>
<%
mobjValues = Nothing%>
</BODY>
</HTML>




