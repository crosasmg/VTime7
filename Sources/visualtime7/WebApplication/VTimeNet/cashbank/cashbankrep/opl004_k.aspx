<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "opl004_k"
%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>


<SCRIPT>

//% insValOption: Valida la habilitación de campos según la opción
//-----------------------------------------------------------------------------
function insValOption(nValue){
//-----------------------------------------------------------------------------
    switch(nValue)
	{
		case(1):
		{
			document.forms[0].elements["tcnRequest"].disabled = false;
			document.forms[0].elements["cbeOffice"].value = '';
			document.forms[0].elements["cbeOffice"].disabled = true;
			document.forms[0].elements["cbeBank"].value = '';
			document.forms[0].elements["cbeBank"].disabled = true;
			break;
		}
		
		case(2):
		{
			document.forms[0].elements["cbeOffice"].disabled = false;
			document.forms[0].elements["tcnRequest"].value = '';
			document.forms[0].elements["tcnRequest"].disabled = true;
			document.forms[0].elements["cbeBank"].value = '';
			document.forms[0].elements["cbeBank"].disabled = true;
			break;
		}
		
		case(3):
		{
			document.forms[0].elements["cbeBank"].disabled = false;
			document.forms[0].elements["tcnRequest"].value = '';
			document.forms[0].elements["tcnRequest"].disabled = true;
			document.forms[0].elements["cbeOffice"].value = '';
			document.forms[0].elements["cbeOffice"].disabled = true;
			break;
		}
		
		case(4):
		{
			document.forms[0].elements["cbeBank"].value = '';
			document.forms[0].elements["cbeBank"].disabled = true;
			document.forms[0].elements["tcnRequest"].value = '';
			document.forms[0].elements["tcnRequest"].disabled = true;
			document.forms[0].elements["cbeOffice"].value = '';
			document.forms[0].elements["cbeOffice"].disabled = true;
			break;
		}
	}
}

//% insStateZone: habilita los campos de la forma
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
    var lintIndex = 0;
    
    if (typeof(document.forms[0])!='undefined')
    {
        for (lintIndex=0;lintIndex<document.forms[0].elements.length;lintIndex++)
            document.forms[0].elements[lintIndex].disabled = false;
        document.images["btn_tcdDateFrom"].disabled = false
        document.images["btn_tcdDateTo"].disabled = false
    }
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}
</SCRIPT>
<HTML>
<HEAD>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("OPL004", "OPL004_k.aspx", 1, vbNullString))
mobjMenu = Nothing
%>
<SCRIPT>

//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $" 
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="OPL004" ACTION="valCashBankRep.aspx?sMode=1">
    <BR><BR>
		<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>
	<BR><BR>
    <TABLE WIDTH="100%">
       	<TR>
       		<TD>&nbsp;</TD>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=0><A NAME="Fecha de los cheques a listar"><%= GetLocalResourceObject("AnchorFecha de los cheques a listarCaption") %></A></LABEL></TD>
        </TR>
        <TR>
			<TD>&nbsp;</TD>
			<TD COLSPAN="5"><HR></TD>
        </TR>
        <TR>
			<TD>&nbsp;</TD>
            <TD WIDTH="15%"><LABEL ID=0><%= GetLocalResourceObject("tcdDateFromCaption") %></LABEL></TD>
            <TD WIDTH="20%"><%=mobjValues.DateControl("tcdDateFrom", Session("dDateFrom"),  , GetLocalResourceObject("tcdDateFromToolTip"),  ,  ,  ,  , True)%></TD>
            <TD WIDTH="5%">&nbsp;</TD>
            <TD WIDTH="15%"><LABEL ID=0><%= GetLocalResourceObject("tcdDateToCaption") %></LABEL></TD>
            <TD WIDTH="20%"><%=mobjValues.DateControl("tcdDateTo", Session("dDateTo"),  , GetLocalResourceObject("tcdDateToToolTip"),  ,  ,  ,  , True)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR><TD>&nbsp</TD></TR>
       	<TR>
       		<TD>&nbsp;</TD>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=0><A NAME="Cheques a listar"><%= GetLocalResourceObject("AnchorCheques a listarCaption") %></A></LABEL></TD>
        </TR>
        <TR>
			<TD>&nbsp;</TD>
			<TD COLSPAN="5"><HR></TD>
        </TR>
        <TR>
			<TD>&nbsp;</TD>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "tcnTypeList", GetLocalResourceObject("tcnTypeList_CStr1Caption"), CStr(1), CStr(1), "insValOption(1)", True)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnRequestCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnRequest", 6, Session("nRequest"), False, "",  ,  ,  ,  ,  ,  , True)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
			<TD>&nbsp;</TD>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "tcnTypeList", GetLocalResourceObject("tcnTypeList_CStr2Caption"), CStr(2), CStr(2), "insValOption(2)", True)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeOfficeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeOffice", "TABLE9", eFunctions.Values.eValuesType.clngComboType, Session("nOffice"),  ,  ,  ,  ,  ,  , True, 2, "", eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
			<TD>&nbsp;</TD>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "tcnTypeList", GetLocalResourceObject("tcnTypeList_CStr3Caption"), CStr(2), CStr(3), "insValOption(3)", True)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeBankCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBank", "TABLE7", eFunctions.Values.eValuesType.clngComboType, Session("nBank"),  ,  ,  ,  ,  ,  , True, 2, GetLocalResourceObject("cbeBankToolTip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
			<TD>&nbsp;</TD>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "tcnTypeList", GetLocalResourceObject("tcnTypeList_CStr4Caption"), CStr(2), CStr(4), "insValOption(4)", True)%></TD>
            <TD>&nbsp;</TD>
        </TR>
    </TABLE>
    <SCRIPT>
        if(insDisabledButton(document.A304))ClientRequest(304,5);
        insValOption(1);
    </SCRIPT>
</FORM>
</BODY>
</HTML>






