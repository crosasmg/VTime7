<%@ Page explicit="true" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
Dim mobjMenu As eFunctions.Menues
Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>


	<%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
	<SCRIPT LANGUAGE="JavaScript">

//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:00 $"        

//% insStateZone: Inhabilita determinados campos de acuerdo a la acción en tratamiento.
//------------------------------------------------------------------------------------------------
function insStateZone()
//------------------------------------------------------------------------------------------------
{
	with (self.document.forms[0])
	{
		valTable.disabled = false;
		btnvalTable.disabled = valTable.disabled;
		tcnYear.disabled = false;
		cbePerType.disabled = false;
		tcnPerNumber.disabled = false;
		cbeInforType.disabled = false;
		cbeCurrency.disabled = false;
		tcdEffecdate.disabled = false;
		btn_tcdEffecdate.disabled = tcdEffecdate.disabled;
	}
}
//insSetNumber: Se inhabilita el campo "Número" y le asigna el valor "1" si el tipo de período es "5-Anual"
//---------------------------------------------------------------------------------------------------------
function insSetNumber(){
//---------------------------------------------------------------------------------------------------------
    with (document.forms[0])
    {
		tcnPerNumber.disabled = (cbePerType.value == "5");
		if (tcnPerNumber.disabled) {
		    tcnPerNumber.value = "1";
		}
    }
}
//% insCancel: Ejecuta la acción Cancelar de la página   
//------------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------------
	return(true);
}   
</SCRIPT>
	<META HTTP-EQUIV="Content-Language" CONTENT="es">
    <%mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("AG005", "AG005_K.aspx", 1, ""))
mobjMenu = Nothing
%>
</HEAD>
<BODY  VLink=white LINK=white ALINK=white >
<BR>
<FORM METHOD="post" ID="FORM" NAME="frmGoals" ACTION="valAgent.aspx?Zone=1">
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valTableCaption") %></LABEL></TD>
            <TD COLSPAN="4"><%=mobjValues.PossiblesValues("valTable", "TabTab_goals", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("valTableToolTip"))%></TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=40015><a NAME="Período"><%= GetLocalResourceObject("AnchorPeríodoCaption") %></a></LABEL></TD>
            <TD COLSPAN=3>&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN = 2><HR></TD>
            <TD COLSPAN=3>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=8046><%= GetLocalResourceObject("tcnYearCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnYear", 4,  ,  , GetLocalResourceObject("tcnYearToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
            <TD WIDTH=5%>&nbsp;</TD>
            <TD><LABEL ID=8042><%= GetLocalResourceObject("cbeInforTypeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeInforType", "table276", 1,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeInforTypeToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbePerTypeCaption") %></LABEL></TD>
            <TD><%mobjValues.TypeList = 2
mobjValues.List = "6"
Response.Write(mobjValues.PossiblesValues("cbePerType", "table108", 1,  ,  ,  ,  ,  ,  , "insSetNumber();", True,  , GetLocalResourceObject("cbePerTypeToolTip")))
%>
			</TD>			
			<TD>&nbsp;</TD>
            <TD><LABEL ID=8040><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCurrency", "table11", 1,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnPerNumberCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPerNumber", 2,  ,  , GetLocalResourceObject("tcnPerNumberToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=8041><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate",  ,  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
    </TABLE>
<%
mobjValues = Nothing%>
</FORM>
</BODY>
</HTML>




