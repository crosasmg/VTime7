<%@ Page explicit="true" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
Dim mobjMenu As eFunctions.Menues
Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "opc717_k"
%>
<HTML>
<HEAD>


<%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
//--------------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		tcdStartDate.disabled = false;
		btn_tcdStartDate.disabled = false;
		tcdEndDate.disabled = false;
		btn_tcdEndDate.disabled = false; 
		cbeCurrency.disabled = false;
		cbeBank.disabled = false;
		cbeChequeLocat.disabled = false;
		tctDocnumbe.disabled = false;
		cbeCheque_stat.disabled = false;
		optTypeInfo[0].disabled = false;
		optTypeInfo[1].disabled = false;
		chkSupervisor.disabled = false;
		}
}
//--------------------------------------------------------------------------------------------------
function ChangeOptValue(){
//--------------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if (optTypeInfo[0].checked)
			cbeCard_Type.disabled = true;
		else
			cbeCard_Type.disabled = false;
		}
}
//--------------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------------
	return true;
}   
//--------------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------------
    return true;
}
//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 1 $|$$Date: 11/02/04 17:25 $|$$Author: Nvaplat7 $"
</SCRIPT>
<meta http-equiv="Content-Language" content="es">
    <%mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("OPC717", "OPC717.aspx", 1, ""))
mobjMenu = Nothing
%>
    <BR>
</HEAD>
<BODY>
<BR>
<FORM METHOD="post" ID="FORM" NAME="frmCheques" ACTION="valCashBank.aspx?X=1">
    <TABLE WIDTH="100%">            
        <TR>			
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdStartDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdStartDate",  ,  , GetLocalResourceObject("tcdStartDateToolTip"),  ,  ,  ,  , True, 1)%></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdEndDateCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdEndDate",  ,  , GetLocalResourceObject("tcdEndDateToolTip"),  ,  ,  ,  , True, 2)%></TD>			            
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeCurrency", "TabCurrency_b", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyToolTip"),  , 3)%></TD>
			<TD COLSPAN=2></TD>
		</TR>
		<TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40151><A NAME="Tipo de documento"><%= GetLocalResourceObject("AnchorTipo de documentoCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="Horline"></TD>
        </TR>
		<TR>
			<TD>&nbsp;</TD>
			<TD><%=mobjValues.OptionControl(0, "optTypeInfo", GetLocalResourceObject("optTypeInfo_CStr1Caption"), CStr(1), CStr(1), "ChangeOptValue();", True,  , GetLocalResourceObject("optTypeInfo_CStr1Caption"))%></TD>
			<TD>&nbsp;</TD>
			<TD><%=mobjValues.OptionControl(0, "optTypeInfo", GetLocalResourceObject("optTypeInfo_CStr2Caption"), CStr(False), CStr(2), "ChangeOptValue();", True,  , GetLocalResourceObject("optTypeInfo_CStr2Caption"))%></TD>
		</TR>
		<TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=0><A NAME="Datos del documento"><%= GetLocalResourceObject("AnchorDatos del documentoCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="Horline"></TD>
        </TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBankCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeBank", "Table7", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeBankToolTip"),  , 4)%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeChequeLocatCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeChequeLocat", "Table5553", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeChequeLocatToolTip"),  , 5)%></TD>
		</TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeCard_TypeCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeCard_Type", "Table183", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCard_TypeToolTip"))%></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="Horline"></TD>
        </TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeCheque_statCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeCheque_stat", "Table5576", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCheque_statToolTip"))%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tctDocnumbeCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tctDocnumbe", CShort("10"), "",  , GetLocalResourceObject("tctDocnumbeToolTip"),  ,  ,  ,  , True)%></TD>
		</TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
			<TD><%=mobjValues.CheckControl("chkSupervisor", "", CStr(False), CStr(1),  , True,  , GetLocalResourceObject("chkSupervisorToolTip"))%></TD>
			<TD COLSPAN=2>&nbsp;</TD>
		</TR>
    </TABLE>
</FORM>
</BODY>
</HTML>




