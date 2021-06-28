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

mobjValues.sCodisplPage = "MCO741"
%>


<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"
</SCRIPT> 
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT>
function insCancel(){return(true)}
function insStateZone()
{
    with (self.document.forms[0])
    {
        optTypBankAgree[0].disabled = false
        optTypBankAgree[1].disabled = false
    }
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MCO741_k.aspx", 1, ""))
	mobjMenu = Nothing
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmBankAgree" ACTION="valMantCollection.aspx?mode=1">
<BR><BR>    
    <TABLE>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
        </TR>
        <TR>    
            <TD COLSPAN=2>&nbsp;</TD>
            <TD><%=mobjValues.OptionControl(100200, "optTypBankAgree", GetLocalResourceObject("optTypBankAgree_1Caption"), CStr(1), "1",  , True)%></TD>
        </TR> 
		<TR>
		    <TD COLSPAN=2>&nbsp;</TD>
			<TD><%=mobjValues.OptionControl(100201, "optTypBankAgree", GetLocalResourceObject("optTypBankAgree_2Caption"),  , "2",  , True)%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>





