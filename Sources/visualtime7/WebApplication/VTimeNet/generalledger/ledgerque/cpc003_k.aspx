<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjMenues As eFunctions.Menues
Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = -1

mobjMenues = New eFunctions.Menues
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "CPC003_K"
%>

<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenues.MakeMenu("CPC003", "CPC003_K.aspx", 1, ""))
End With
mobjMenues = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="CPC003" ACTION="valLedgerQue.aspx?sCodispl=XXXXXX">
	<BR>
	<%=mobjValues.ButtonLedCompan("LedCompan", 1, GetLocalResourceObject("LedCompanToolTip"))%>
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=8451><A NAME="Saldo"><%= GetLocalResourceObject("AnchorSaldoCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4"><HR></TD>
        </TR>
        <TR>
            <TD><%=mobjValues.OptionControl(0, "optMonth", GetLocalResourceObject("optMonth_Caption"))%></TD>
            <TD><%=mobjValues.OptionControl(0, "optSumm", GetLocalResourceObject("optSumm_Caption"))%></TD>
        </TR>
        <TR><TD></TD></TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4"><HR></TD>
        </TR>
        <TR>
            <TD><LABEL ID=8450><%= GetLocalResourceObject("tcnYearCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnYear", 10, "",  ,  ,  , 0)%></TD>
            <TD><LABEL ID=8447><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyToolTip"))%></TD>
        </TR>
            <TD><LABEL ID=8448><%= GetLocalResourceObject("valBudgetCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valBudget", "TabBudget", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valBudgetToolTip"))%></TD>
            <TD><LABEL ID=8449><%= GetLocalResourceObject("cbeMonthCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeMonth", "Table7013", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeMonthToolTip"))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>





