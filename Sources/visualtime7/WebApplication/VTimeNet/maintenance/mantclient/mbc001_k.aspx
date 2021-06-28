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

mobjValues.sCodisplPage = "MBC001"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
<SCRIPT>
//% insCancel: se controla la acción Cancelar de la ventana
//-------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------
   return true;
}

//% insStateZone: se controla el estado de los campos de la página
//-------------------------------------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        optTypClie[0].disabled = false
        optTypClie[1].disabled = false
        optTransa[0].disabled = false
        optTransa[1].disabled = false
        optTransa[2].disabled = false
    }
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MBC001_K.aspx", 1, ""))
	mobjMenu = Nothing
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmClientSeq" ACTION="ValMantClient.aspx?mode=1">
<BR><BR>
    <TABLE WIDTH="100%">
            
		<TR>
			<TD ALIGN="LEFT" CLASS="HighLighted"><LABEL ID=100810><A NAME="Tipo de cliente"><%= GetLocalResourceObject("AnchorTipo de clienteCaption") %></A></LABEL></TD>
			<TD WIDTH="5"></TD>
			<TD CLASS="HighLighted"><LABEL ID=100804><A NAME="Transacción"><%= GetLocalResourceObject("AnchorTransacciónCaption") %></A></LABEL></TD>
		</TR>
		<TR>
			<TD WIDTH="47%" COLSPAN="1"><HR></TD>
			<TD WIDTH="5"></TD>
			<TD WIDTH="47%" COLSPAN="1"><HR></TD>
		</TR>
		<TR>
            <TD><%=mobjValues.OptionControl(100805, "optTypClie", GetLocalResourceObject("optTypClie_CStr1Caption"), CStr(1), CStr(1),  , True,  , GetLocalResourceObject("optTypClie_CStr1Caption"))%></TD>
            <TD WIDTH="5"></TD>
            <TD><%=mobjValues.OptionControl(100806, "optTransa", GetLocalResourceObject("optTransa_CStr1Caption"), CStr(1), CStr(1),  , True,  , GetLocalResourceObject("optTransa_CStr1Caption"))%></TD>
		</TR>
		<TR>
            <TD><%=mobjValues.OptionControl(100807, "optTypClie", GetLocalResourceObject("optTypClie_CStr2Caption"),  , CStr(2),  , True,  , GetLocalResourceObject("optTypClie_CStr2Caption"))%></TD>
            <TD WIDTH="5"></TD>
            <TD><%=mobjValues.OptionControl(100808, "optTransa", GetLocalResourceObject("optTransa_CStr2Caption"),  , CStr(2),  , True,  ,  GetLocalResourceObject("optTransa_CStr2Caption"))%></TD>
        </TR>
        <TR>
			<TD></TD>
			<TD WIDTH="5"></TD>
			<TD><%=mobjValues.OptionControl(100809, "optTransa", GetLocalResourceObject("optTransa_CStr3Caption"),  , CStr(3),  , True,  , GetLocalResourceObject("optTransa_CStr3Caption"))%></TD>
        </TR>

    </TABLE>
</FORM>
</BODY>
</HTML>





