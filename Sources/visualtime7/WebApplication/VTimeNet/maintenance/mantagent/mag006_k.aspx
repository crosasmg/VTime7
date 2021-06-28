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
mobjValues.sCodisplPage = "MAG006"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $"

//* Funcion que Finaliza las las acciones de la Pagina
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}

//* Funcion que cancela las las acciones de la Pagina
//-------------------------------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------------------------------
	return(true)
}

//+ Controla el estado de los campos de la página
//-------------------------------------------------------------------------------------------------------------------
function insStateZone()
//-------------------------------------------------------------------------------------------------------------------
{
    with (self.document.forms[0])
    {
        optCommType[0].disabled = false
        optCommType[1].disabled = false
        optCommType[2].disabled = false
        optCommType[3].disabled = false
        optCommType[4].disabled = false
    }
}
</SCRIPT>

<%
With Response
	.Write(mobjValues.StyleSheet())
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MAG005_K.aspx", 1, ""))
	mobjMenu = Nothing
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmTabCommission" ACTION="ValMantAgent.aspx?mode=1">
<BR><BR>
    <TABLE WIDTH="100%">
		<TR>
			<TD COLSPAN="3" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>    
        </TR>
        <TR>
			<TD COLSPAN="3" CLASS="Horline"></TD>		            
        <TD>        
        <TR>
			<TD COLSPAN="3">&nbsp</TD>
        </TR>
        </TR>
            <TD><%=mobjValues.OptionControl(100012, "optCommType", GetLocalResourceObject("optCommType_CStr0Caption"), CStr(1), CStr(0),  , True)%></TD>
            <TD><%=mobjValues.OptionControl(100012, "optCommType", GetLocalResourceObject("optCommType_CStr2Caption"),  , CStr(2),  , True)%></TD>
            <TD><%=mobjValues.OptionControl(0, "optCommType", GetLocalResourceObject("optCommType_CStr4Caption"),  , CStr(4),  , True)%></TD>        
        </TR>
            <TD><%=mobjValues.OptionControl(100013, "optCommType", GetLocalResourceObject("optCommType_CStr1Caption"),  , CStr(1),  , True)%></TD>
            <TD><%=mobjValues.OptionControl(100014, "optCommType", GetLocalResourceObject("optCommType_CStr3Caption"),  , CStr(3),  , True)%></TD>                    
        </TR>
    </TABLE>
<%
mobjValues = Nothing%>
</FORM>
</BODY>
</HTML>





