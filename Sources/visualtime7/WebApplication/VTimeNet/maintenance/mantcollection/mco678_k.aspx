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
mobjValues.sCodisplPage = "MCO678"
%>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 5/03/04 18:05 $|$$Author: Nvaplat11 $"
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
//% insChangeValues: se controla el cambio de valor de los campos
//-------------------------------------------------------------------------------------------
function insChangeValues(){
//-------------------------------------------------------------------------------------------
	if (self.document.forms[0].tcnCode.value!=0)
		insDefValues('Descript', 'nCode='+self.document.forms[0].tcnCode.value);
	else
	   {
		self.document.forms[0].tctDescript.value="";
		self.document.forms[0].tctShort_des.value="";
	    }	
}//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		tcnCode.disabled = false;
		tctDescript.disabled = false;
		tctShort_des.disabled = false;
		tcdEffecdate.disabled = false;
		btn_tcdEffecdate.disabled = false;
		tcnCollectorType.disabled = false;
		tcnContype.disabled = false;
		cbeInChannel.disabled = false;
		optsCollecAsig[0].disabled = false;
		optsCollecAsig[1].disabled = false;
		tcnDaysIni.disabled = false;
		tcnDaysEnd.disabled = false;	
	}
}
</SCRIPT>
<%With Response
	.Write(mobjValues.StyleSheet())
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.MakeMenu("MCO678", "MCO678_k.aspx", 1, ""))
	mobjMenu = Nothing
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="SI559" ACTION="valMantCollection.aspx?sMode=1">
<!--  Sirve para colocar un titulo al principio de la pagina -->
<BR><BR>
    <TABLE WIDTH="100%" >
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnCodeCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCode", 5,  ,  , GetLocalResourceObject("tcnCodeToolTip"),  ,  ,  ,  ,  , "insChangeValues()", True)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tctDescriptCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctDescript", 30,  ,  , GetLocalResourceObject("tctDescriptToolTip"),  ,  ,  ,  , True)%></TD>
            <TD WIDTH=15%>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tctShort_desCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctShort_des", 12,  ,  , GetLocalResourceObject("tctShort_desToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
            <TD><LABEL ID=13374><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate", Request.Form.Item("tcdEffecdate"),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
            <TD WIDTH=15%>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeStatregtCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeStatregt", "table26", eFunctions.Values.eValuesType.clngComboType, CStr(2),  ,  ,  ,  ,  ,  , True, 10, GetLocalResourceObject("cbeStatregtToolTip"))%></TD>
        </TR>
     	<TR>
            <TD><LABEL><%= GetLocalResourceObject("tcnCollectorTypeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("tcnCollectorType", "table5551", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  , True, 10, GetLocalResourceObject("tcnCollectorTypeToolTip"))%></TD>
            <TD WIDTH=15%>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnContypeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("tcnContype", "table5557", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  ,  , True, 10, GetLocalResourceObject("tcnContypeToolTip"))%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeInChannelCaption") %></LABEL></TD>
            <TD><%
mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeInChannel", "table5554", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True, 10, GetLocalResourceObject("cbeInChannelToolTip")))%></TD>
        </TR>
        <TR></TR>
        <TR>
			<TD COLSPAN=2 CLASS="HIGHLIGHTED"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
			<TD WIDTH=5%>&nbsp;</TD>
			<TD COLSPAN=2 CLASS="HIGHLIGHTED"><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
	    </TR>
	    <TR>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
			<TD></TD>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
	    </TR>
        <TR>
           <TD><%=mobjValues.OptionControl(0, "optsCollecAsig", GetLocalResourceObject("optsCollecAsig_1Caption"), CStr(1), "1",  , True,  , GetLocalResourceObject("optsCollecAsig_1ToolTip"))%>
           <TD>&nbsp;</TD>
           <TD>&nbsp;</TD>
           <TD><LABEL><%= GetLocalResourceObject("tcnDaysIniCaption") %></LABEL></TD>
           <TD><%=mobjValues.NumericControl("tcnDaysIni", 5, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnDaysIniToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
            <TD><%=mobjValues.OptionControl(0, "optsCollecAsig", GetLocalResourceObject("optsCollecAsig_2Caption"),  , "2",  , True,  , GetLocalResourceObject("optsCollecAsig_2ToolTip"))%>
            <TD>&nbsp;</TD>
			<TD>&nbsp;</TD>
			<TD><LABEL><%= GetLocalResourceObject("tcnDaysEndCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnDaysEnd", 5, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnDaysEndToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
        </TR>
</TABLE>
</BODY>
</FORM>
</HTML>
<%
mobjValues = Nothing
%>





