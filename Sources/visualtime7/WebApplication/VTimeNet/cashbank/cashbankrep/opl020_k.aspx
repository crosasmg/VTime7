<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjMenu As eFunctions.Menues
Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "opl020_k"
%>
<HTML>
<HEAD>


<%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
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
function insStateZone(){
//--------------------------------------------------------------------------------------------------

}

//--------------------------------------------------------------------------------------------------
function MarkOtherCheck(Field)
//--------------------------------------------------------------------------------------------------
{
	if(Field.checked)
		self.document.forms[0].elements["chkOffice"].checked=true
	else
		self.document.forms[0].elements["chkOffice"].checked=false;
}
</SCRIPT>
<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("OPL020", "OPL020_K.aspx", 1, ""))
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmOrdersForPay" ACTION="ValCashBankRep.aspx?X=1">
    <BR><BR>
    <TABLE WIDTH="100%" BORDER=0>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=101602><A NAME="Fecha"><%= GetLocalResourceObject("AnchorFechaCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HORLine"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=101603><%= GetLocalResourceObject("tcdInitDateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdInitDate", CStr(Today),  , GetLocalResourceObject("tcdInitDateToolTip"))%></TD>
            <TD><LABEL ID=101604><%= GetLocalResourceObject("tcdEndDateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEndDate", CStr(Today),  , GetLocalResourceObject("tcdEndDateToolTip"))%></TD>
        </TR>
        <TR>
            <TD COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=101605><%= GetLocalResourceObject("cbeConceptCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeConcept", "Table293", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeConceptToolTip"))%></TD>
            <TD></TD>
            <TD></TD>
        </TR>
        <TR>
            <TD><LABEL ID=101606><%= GetLocalResourceObject("cbeOfficeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeOfficeToolTip"))%></TD>
            <TD></TD>
            <TD><%=mobjValues.CheckControl("chkOffice", GetLocalResourceObject("chkOfficeCaption"),  , CStr(1))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=101607><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyToolTip"))%></TD>
            <TD></TD>
            <TD></TD>
        </TR>
        <TR>
            <TD><LABEL ID=101607><%= GetLocalResourceObject("cbeSta_chequeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeSta_cheque", "Table187", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeSta_chequeToolTip"))%></TD>
            <TD></TD>
            <TD></TD>
        </TR>
        <TR>
            <TD COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=101608><A NAME="Cuenta bancaria"><%= GetLocalResourceObject("AnchorCuenta bancariaCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HORLine"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=101609><%= GetLocalResourceObject("valAccountNumCaption") %></LABEL></TD>
            <TD><%With mobjValues
	.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(.PossiblesValues("valAccountNum", "tabBank_acc_CurAcc", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  ,  , 4, GetLocalResourceObject("valAccountNumToolTip"), eFunctions.Values.eTypeCode.eString))
End With
%>
            </TD>
        </TR>
        </TR>
            <TD COLSPAN=2><%=mobjValues.CheckControl("chkAccount", GetLocalResourceObject("chkAccountCaption"),  ,  , "MarkOtherCheck(this);")%></TD>
            <TD></TD>
            <TD></TD>
        </TR>
    </TABLE>
<%
mobjValues = Nothing%>
</FORM>
</BODY>
</HTML>





