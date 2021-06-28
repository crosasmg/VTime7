<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas y menues
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "MCO689"
%>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}
</SCRIPT>
<%With Response
	.Write(mobjValues.StyleSheet())
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.MakeMenu("MCO689", "MCO689_k.aspx", 1, ""))
	mobjMenu = Nothing
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SI559" ACTION="valMantCollection.aspx?sMode=1">
<BR>
    
    <TABLE WIDTH="100%">
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeinsurareaCaption") %></LABEL></TD>
            <TD><%= mobjValues.PossiblesValues("cbeinsurarea", "table5001", eFunctions.Values.eValuesType.clngComboType, CStr(2), , , , , , , , 10, GetLocalResourceObject("cbeinsurareaToolTip"))%></TD>
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>
        </TR>
        <P>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
            <TD><%= mobjValues.OptionControl(0, "optBillType", GetLocalResourceObject("optBillType_1Caption"), CStr(1), "1", , , , GetLocalResourceObject("optBillType_1ToolTip"))%>
			<TD><%= mobjValues.OptionControl(0, "optBillType", GetLocalResourceObject("optBillType_2Caption"), , "2", , , , GetLocalResourceObject("optBillType_2ToolTip"))%>
            <TD>&nbsp;</TD>
        <TR>
        </TR>
            <TD>&nbsp;</TD>
            <TD><%= mobjValues.OptionControl(0, "optBillType", GetLocalResourceObject("optBillType_4Caption"),  , "4", , , , GetLocalResourceObject("optBillType_4ToolTip"))%>
			<TD><%= mobjValues.OptionControl(0, "optBillType", GetLocalResourceObject("optBillType_3Caption"),  , "3", , , , GetLocalResourceObject("optBillType_3ToolTip"))%>
			<TD>&nbsp;</TD>
        </TR>
    </TABLE>		
</BODY>
</FORM>
</HTML>
<%
mobjValues = Nothing%>






