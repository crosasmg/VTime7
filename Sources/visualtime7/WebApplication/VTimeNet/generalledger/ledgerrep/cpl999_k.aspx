<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'**- Possibles values objects are defined
'-   Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "CPL999_K"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>

<SCRIPT>

//**% insCancel: This function is executed when the page is cancelled
//%   insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-------------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------------
	return true;
}
//**% insStateZone: This function allows to control the status of the items page
//%   insStateZone: Se controla el estado de los campos de la página
//-------------------------------------------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------------------------------------------------
}

//%insShowLastDate_Process: Obtiene la última fecha de ejecución del proceso según el área contable
//-------------------------------------------------------------------------------------------------
function insShowLastDate_Process(){
//-------------------------------------------------------------------------------------------------
    insDefValues('CPL999', 'nArea_Led=' + self.document.forms[0].cbeArea_Led.value, '/VTimeNet/GeneralLedger/LedgerRep');
}

</SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


  <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "CPL999_k.aspx", 1, ""))
	.Write(mobjValues.WindowsTitle("CPL999"))
End With

mobjMenu = Nothing%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="CPL015" ACTION="valLedgerRep.aspx?Mode=1">
	<BR><BR>
	<BR><BR>

<%Response.Write(mobjValues.ShowWindowsName("CPL999"))%>

	<TABLE WIDTH="100%">
	    <BR><BR>
	    <TR>
            <TD WIDTH=25%></TD>
            <TD WIDTH=25%><LABEL><%= GetLocalResourceObject("cbeArea_LedCaption") %></LABEL></TD>
            <%mobjValues.TypeList = CShort("1")
mobjValues.List = "1,2,3,4,5,6,40"%>
            <TD WIDTH=15%><%=mobjValues.PossiblesValues("cbeArea_Led", "Table178", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "insShowLastDate_Process();",  ,  , GetLocalResourceObject("cbeArea_LedToolTip"))%></TD>	    
            <TD WIDTH=35%></TD>            
	    </TR>

		<TR>
		    <TD WIDTH=25%></TD>
			<TD><LABEL ID=11288><%= GetLocalResourceObject("tcdInit_dateCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdInit_date",  , False, GetLocalResourceObject("tcdInit_dateToolTip"),  ,  ,  ,  , True, 1)%></TD>			
		</TR>
		<TR>
		    <TD WIDTH=25%></TD>
			<TD><LABEL ID=100879><%= GetLocalResourceObject("tcdTo_dateCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdTo_date",  ,  , GetLocalResourceObject("tcdTo_dateToolTip"),  ,  ,  ,  , False, 2)%></TD>        
		</TR>	
			
	    <TR>
	        <TD></TD>
	    </TR>
        <TR>
            <TD WIDTH=25%></TD>
            <TD WIDTH=25%></TD>
            <TD WIDTH=15% CLASS="HighLighted"><LABEL ID=40006><A NAME="Tipo de ejecución"><%= GetLocalResourceObject("AnchorTipo de ejecuciónCaption") %></A></LABEL></TD>            
            <TD WIDTH=35%></TD>
        </TR>
        <TR>
            <TD COLSPAN = 1></TD>
            <TD COLSPAN=2 CLASS="HORLINE"></TD>
            <TD COLSPAN = 1></TD>
        </TR>
	    <TR>
	        <TD WIDTH=25%></TD>
			<TD><%=mobjValues.OptionControl(0, "optExecute", GetLocalResourceObject("optExecute_CStr1Caption"), CStr(1), CStr(1),  ,  , 3)%></TD>
			<TD><%=mobjValues.OptionControl(0, "optExecute", GetLocalResourceObject("optExecute_CStr2Caption"), CStr(2), CStr(2),  ,  , 4)%></TD>		
		</TR>
	</TABLE>
	<TABLE WIDTH="100%">
	    <TR>
	        <TD WIDTH=20%></TD>
	        <TD WIDTH=15%></TD>	        
			<TD ALIGN="LEFT" COLSPAN=2><%=mobjValues.CheckControl("chkPrint", GetLocalResourceObject("chkPrintCaption"),  , CStr(1),  ,  ,  , GetLocalResourceObject("chkPrintToolTip"))%></TD>
        </TR>			
	</TABLE>
</FORM>
</BODY>
</HTML>	
<%
mobjValues = Nothing%>  






