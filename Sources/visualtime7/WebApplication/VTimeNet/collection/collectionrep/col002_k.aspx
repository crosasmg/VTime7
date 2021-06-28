<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
Dim table183 As String
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


<SCRIPT>
//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $|$$Author: Iusr_llanquihue $"
</SCRIPT>    
<SCRIPT>
// insStateZone :
//-----------------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------------
}

//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

</SCRIPT>
    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("COL002"))
	.Write(mobjMenu.MakeMenu("COL002", "COL002_K.aspx", 1, ""))
End With
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmRAutoChargeBankCardType" ACTION="valCollectionRep.aspx?mode=1">
<BR></BR>
    <%Response.Write(mobjValues.ShowWindowsName("COL002"))%>
    <TABLE WIDTH="100%">
	    <TR>
	        <TD>&nbsp;</TD>
	    </TR>
	    <TR>
			<TD><LABEL ID=12942><%= GetLocalResourceObject("tcdProcessDateCaption") %></LABEL></TD>
<TD COLSPAN="2"><% %>
<%=mobjValues.DateControl("tcdProcessDate", CStr(Today),  , GetLocalResourceObject("tcdProcessDateToolTip"))%></TD>
	    </TR>
	    	
	    <TR>
	        <TD WIDTH="100%" COLSPAN="5">&nbsp;</TD>
	    </TR>
	    <TR>
	        <TD COLSPAN="5" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
	    </TR>
	    <TR>
	        <TD WIDTH="100%" COLSPAN="5"><HR></TD>
	    </TR>
	    <TR>
			<TD><LABEL ID=12942><%= GetLocalResourceObject("cbeCardTypeCaption") %></LABEL></TD>
            <TD COLSPAN="2"><%=mobjValues.PossiblesValues("cbeCardType", table183, eFunctions.Values.eValuesType.clngComboType)%></TD>
	    </TR>
	    <TR>
            <TD><%=mobjValues.OptionControl(0, "optTypBank", GetLocalResourceObject("optTypBank_1Caption"),  , "1")%>
            <TD>&nbsp;</TD>
			<TD><%=mobjValues.OptionControl(0, "optTypBank", GetLocalResourceObject("optTypBank_2Caption"), CStr(1), "2")%>
	    </TR>
	    <TR>
	    <TD>&nbsp;</TD>
	    </TR>
	    <TR>
	    <TD>&nbsp;</TD>
	    </TR>
	    <TR>
            <TD COLSPAN="2"><%=mobjValues.CheckControl("chkDef", GetLocalResourceObject("chkDefCaption"),  , "0")%></TD>
	    </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>




