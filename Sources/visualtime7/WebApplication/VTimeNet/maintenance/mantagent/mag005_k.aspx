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
mobjValues.sCodisplPage = "MAG005"
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

function insStateZone()
{
    self.document.forms[0].cboTransacType.disabled = false
}
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
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
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MAG005_K.aspx", 1, ""))
	mobjMenu = Nothing
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmAutomaticAcc" ACTION="valMantAgent.aspx?mode=1">
<BR><BR>
    <TABLE WIDTH="100%">
            
        </TR>
            <TD><LABEL ID=11716><%= GetLocalResourceObject("cboTransacTypeCaption") %></LABEL></TD>
            <TD><%mobjValues.TypeOrder = 1
Response.Write(mobjValues.PossiblesValues("cboTransacType", "table6", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cboTransacTypeToolTip")))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing

%>




